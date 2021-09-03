using Mirror;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft.Core.WorldGeneration
{
    [RequireComponent(typeof(WorldGenerator))]
    public class ChunkHolder : NetworkBehaviour
    {
        private IDictionary<Vector3Int, Chunk> chunks = new Dictionary<Vector3Int, Chunk>();
        private ISet<IWorldObserver> worldObservers = new HashSet<IWorldObserver>();
        private WorldGenerator worldGenerator;

        #region Common

        private void Awake()
        {
            worldGenerator = GetComponent<WorldGenerator>();
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            IPlaceable cobblestone = (IPlaceable)RegistryManager.GetRegistryObject("CobblestoneBlock");

            for(int i = 0; i < 16; i++)
            {
                int x = Random.Range(-16, 16);
                int y = Random.Range(0, 3);
                int z = Random.Range(-16, 16);

                SetPlaceable(cobblestone, new Vector3Int(x, y, z));
            }
        }

        private Chunk GetChunkAt(Vector3 position)
        {
            Vector3Int chunkPosition = Chunk.ConvertToChunkPosition(position);

            if (chunks.ContainsKey(chunkPosition))
                return chunks[chunkPosition];

            var createdChunk = worldGenerator.CreateChunkAt(chunkPosition);
            chunks.Add(chunkPosition, createdChunk);
            return createdChunk;
        }

        private Vector3Int GetChunkPositionAt(Vector3 position)
        {
            return GetChunkAt(position).ChunkPosition;
        }

        public IPlaceableStateHolder SetPlaceable(IPlaceable placeable, Vector3Int position)
        {
            Chunk chunk = GetChunkAt(position.GetVector3());

            return chunk.SetPlaceable(placeable, position);
        }

        public IPlaceableStateHolder Place(IPlaceable placeable, Vector3Int position)
        {
            Chunk chunk = GetChunkAt(position.GetVector3());

            return chunk.Place(placeable, position);
        }

        public void AddWorldObserver(IWorldObserver observer)
        {
            if (!worldObservers.Contains(observer))
            {
                observer.OnObservationStarted(this);
                worldObservers.Add(observer);
            }
        }

        public bool IsWorldObserverRegistered(IWorldObserver observer) => worldObservers.Contains(observer);

        public void AddChunkObserver(Vector3Int chunkPosition, IWorldObserver observer)
        {
            GetChunkAt(chunkPosition).AddObserver(observer);
        }

        public void RemoveChunkObserver(Vector3Int chunkPosition, IWorldObserver observer)
        {
            if(!chunks.ContainsKey(chunkPosition))
            {
                Debug.LogError($"ChunkHolder::RemoveChunkObserver: Cannot remove observer from chunk at {chunkPosition}, because it is not loaded");
                return;
            }

            GetChunkAt(chunkPosition).RemoveObserver(observer);
        }

        #endregion

        #region Server

        [Server]
        public ISet<Vector3Int> GetChunkPositionsAround(Vector3 position, uint range)
        {
            var chunkPositions = new HashSet<Vector3Int>();
            Vector3Int centerChunkPosition = GetChunkPositionAt(position);

            for(int dX = (int)-range; dX <= range; dX++)
            {
                for (int dZ = (int)-range; dZ <= range; dZ++)
                {
                    Vector3Int displacement = new Vector3Int(dX, 0, dZ) * Chunk.ChunkSideWidth;
                    Vector3Int chunkPosition = centerChunkPosition + displacement;

                    chunkPositions.Add(chunkPosition);
                }
            }

            return chunkPositions;
        }

        [Server]
        public ChunkData GetChunkData(Vector3Int chunkPosition)
        {
            return GetChunkAt(chunkPosition.GetVector3()).Data;
        }

        #endregion
    }
}
