using Mirror;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Minecraft.Core.WorldGeneration
{
    [RequireComponent(typeof(WorldGenerator))]
    public class ChunkHolder : NetworkBehaviour
    {
        private IDictionary<Vector3Int, Chunk> chunks = new Dictionary<Vector3Int, Chunk>();
        private ISet<IWorldObserver> worldObservers = new HashSet<IWorldObserver>();
        private WorldGenerator worldGenerator;
        private World world;

        private void Awake()
        {
            worldGenerator = GetComponent<WorldGenerator>();
            world = GetComponent<World>();
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            IPlaceable cobblestone = (IPlaceable)RegistryManager.GetRegistryObject("CobblestoneBlock");

            for (int x = -16; x <= 16; x++)
            {
                for (int z = -16; z <= 16; z++)
                {
                    Vector3Int position = new Vector3Int(x, 0, z);

                    SetPlaceable(cobblestone, position);
                }
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

        public void AddChunkObserver(Vector3Int chunkPosition, IWorldObserver observer)
        {
            GetChunkAt(chunkPosition).AddObserver(observer);
        }

        public void RemoveChunkObserver(Vector3Int chunkPosition, IWorldObserver observer)
        {
            GetChunkAt(chunkPosition).RemoveObserver(observer);
        }

        [Client]
        public void LoadChunk(Vector3Int chunkPosition, IWorldObserver observer)
        {
            if(chunks.ContainsKey(chunkPosition))
            {
                var existingChunk = GetChunkAt(chunkPosition);
                _ = existingChunk.AddObserver(observer);
                return;
            }

            var chunk = worldGenerator.CreateChunkAt(chunkPosition);
            chunk.AddObserver(observer);
            chunks.Add(chunkPosition, chunk);
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
                worldObservers.Add(observer);
        }

        public bool IsWorldObserverRegistered(IWorldObserver observer) => worldObservers.Contains(observer);
    }
}
