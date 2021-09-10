using Minecraft.Core.WorldGeneration;
using Mirror;
using UnityEngine;
using System.Linq;
using System.Collections;
using Minecraft.Core.Placeables;

namespace Minecraft.Core.Entities.Player
{
    public class PlayerWorldLoader : NetworkBehaviour, IWorldObserver
    {
        private const uint MinViewDistance = 1, MaxViewDistance = 20;

        [SerializeField, Range(MinViewDistance, MaxViewDistance)]
        private uint viewDistance = (int)(MinViewDistance + MaxViewDistance) / 2;
        public uint LoadingRange => viewDistance;
        public Vector3 Position => transform.position;
        public NetworkIdentity Identity => netIdentity;

        private ChunkHolder chunkHolder;
        private SyncHashSet<Vector3Int> loadedChunks = new SyncHashSet<Vector3Int>();

        public void OnObservationStarted(ChunkHolder chunkHolder)
        {
            this.chunkHolder = chunkHolder;

            if (!hasAuthority)
                return;

            IPlaceable cobblestone = (IPlaceable)RegistryManager.GetRegistryObject("CobblestoneBlock");

            int x = Random.Range(-16, 16);
            int y = Random.Range(0, 3);
            int z = Random.Range(-16, 16);

            _ = chunkHolder.SetPlaceable(cobblestone, new Vector3Int(x, y, z));
        }

        [Command]
        private void CmdRequestLoadedChunksChange(uint range, NetworkConnectionToClient sender = null)
        {
            range = (uint)Mathf.Clamp(range, MinViewDistance, MaxViewDistance);
            var requestedChunks = chunkHolder.GetChunkPositionsAround(transform.position, range);

            var chunksToLoad = requestedChunks.Except(loadedChunks);
            var chunksToUnload = loadedChunks.Except(loadedChunks);

            if (sender.connectionId != 0)
            {
                foreach (var chunkPosition in chunksToLoad)
                {
                    var chunkData = chunkHolder.GetChunkData(chunkPosition);
                    chunkHolder.AddChunkObserver(chunkPosition, this);
                    SendChunkData(chunkData);
                }

                foreach (var chunkPosition in chunksToUnload)
                {
                    chunkHolder.RemoveChunkObserver(chunkPosition, this);
                    TargetUnloadChunkLocally(chunkPosition);
                }
            }

            loadedChunks.ExceptWith(chunksToUnload);
            loadedChunks.UnionWith(chunksToLoad);
        }

        [Server]
        private void SendChunkData(ChunkData chunkData)
        {
            TargetLoadChunkLocally(chunkData.ChunkPosition);

            for(int x = 0; x < Chunk.ChunkSideWidth; x++)
            {
                for(int z = 0; z < Chunk.ChunkSideWidth; z++)
                {
                    for(int y = 0; y < Chunk.ChunkHeight; y++)
                    {
                        var localPosition = new Vector3Int(x, y, z);
                        var placeablePosition = chunkData.ChunkPosition + localPosition;

                        var placeable = chunkData.GetPlaceableAt(localPosition);
                        if (placeable == null)
                            continue;

                        TargetSetPlaceableLocally(chunkData.ChunkPosition, placeable, placeablePosition);
                    }
                }
            }
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (!hasAuthority)
                return;

            StartCoroutine(WorldUpdateLoop());
        }

        [Client]
        private IEnumerator WorldUpdateLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                if (chunkHolder == null)
                    continue;

                CmdRequestLoadedChunksChange(LoadingRange);
            }
        }

        public void SetPlaceable(Vector3Int chunkPosition, IPlaceable placeable, Vector3Int placeablePosition)
        {
            if (isServer)
                TargetSetPlaceableLocally(chunkPosition, placeable, placeablePosition);
            else
                CmdSetPlaceable(chunkPosition, placeable, placeablePosition);
        }

        [Command]
        private void CmdSetPlaceable(Vector3Int chunkPosition, IPlaceable placeable, Vector3Int placeablePosition)
        {
            _ = chunkHolder.SetPlaceable(placeable, placeablePosition);
            TargetPlaceLocally(chunkPosition, placeable, placeablePosition);
        }

        [TargetRpc]
        private void TargetSetPlaceableLocally(Vector3Int chunkPosition, IPlaceable placeable, Vector3Int placeablePosition)
        {
            chunkHolder.AddChunkObserver(chunkPosition, this);
            _ = chunkHolder.SetPlaceable(placeable, placeablePosition, false);
        }

        public void Place(Vector3Int chunkPosition, IPlaceable placeable, Vector3Int placeablePosition)
        {
            if (isServer)
                TargetPlaceLocally(chunkPosition, placeable, placeablePosition);
            else
                CmdPlace(chunkPosition, placeable, placeablePosition);
        }

        [Command]
        private void CmdPlace(Vector3Int chunkPosition, IPlaceable placeable, Vector3Int placeablePosition)
        {
            _ = chunkHolder.Place(placeable, placeablePosition);
            TargetPlaceLocally(chunkPosition, placeable, placeablePosition);
        }

        [TargetRpc]
        private void TargetPlaceLocally(Vector3Int chunkPosition, IPlaceable placeable, Vector3Int placeablePosition)
        {
            chunkHolder.AddChunkObserver(chunkPosition, this);
            _ = chunkHolder.Place(placeable, placeablePosition, false);
        }

        public void LoadChunk(Vector3Int chunkPosition)
        {
            if (isServer)
                TargetLoadChunkLocally(chunkPosition);
            else
                CmdLoadChunk(chunkPosition);
        }

        [Command]
        private void CmdLoadChunk(Vector3Int chunkPosition)
        {
            if (!loadedChunks.Contains(chunkPosition))
                _ = loadedChunks.Add(chunkPosition);

            TargetLoadChunkLocally(chunkPosition);
        }

        [TargetRpc]
        private void TargetLoadChunkLocally(Vector3Int chunkPosition)
        {
            chunkHolder.AddChunkObserver(chunkPosition, this);
        }

        public void UnloadChunk(Vector3Int chunkPosition)
        {
            if (isServer)
                TargetUnloadChunkLocally(chunkPosition);
            else
                CmdUnloadChunk(chunkPosition);
        }

        [Command]
        private void CmdUnloadChunk(Vector3Int chunkPosition)
        {
            if (loadedChunks.Contains(chunkPosition))
                _ = loadedChunks.Remove(chunkPosition);

            TargetUnloadChunkLocally(chunkPosition);
        }

        [TargetRpc]
        private void TargetUnloadChunkLocally(Vector3Int chunkPosition)
        {
            chunkHolder.RemoveChunkObserver(chunkPosition, this);
        }
    }
}
