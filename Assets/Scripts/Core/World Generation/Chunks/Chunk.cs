using UnityEngine;
using System.Collections.Generic;
using Minecraft.Core.WorldGeneration.Chunks;
using Minecraft.Core.Placeables;

namespace Minecraft.Core.WorldGeneration
{
    public sealed class Chunk : MonoBehaviour
    {
        public const int ChunkSideWidth = 16, ChunkHeight = 256;

        public Vector3Int ChunkPosition => transform.localPosition.GetVector3Int();
        public ChunkData Data => chunkData;
        public int ObserverCount => observers.Count;

        private World locatedInWorld = null;
        private ChunkPlaceableHolder placeableHolder;
        private ChunkData chunkData;

        private ISet<IWorldObserver> observers;

        private void Awake()
        {
            placeableHolder = GetComponent<ChunkPlaceableHolder>();
        }

        public void Create(World locatedInWorld)
        {
            this.locatedInWorld = locatedInWorld;
            chunkData = new ChunkData(ChunkPosition, ChunkSideWidth, ChunkHeight);
            observers = new HashSet<IWorldObserver>();
        }

        public void RecreateChunk(World locatedInWorld, ChunkData chunkData)
        {
            this.locatedInWorld = locatedInWorld;
            this.chunkData = new ChunkData(ChunkPosition, ChunkSideWidth, ChunkHeight);
            observers = new HashSet<IWorldObserver>();

            //Recreating blocks
            for(int x = 0; x < ChunkSideWidth; x++)
            {
                for (int z = 0; z < ChunkSideWidth; z++)
                {
                    for (int y = 0; y < ChunkHeight; y++)
                    {
                        Vector3Int placeablePosition = new Vector3Int(x, y, z);
                        var placeable = chunkData.GetPlaceableAt(placeablePosition);

                        if (placeable != null)
                            _ = SetPlaceable(placeable, placeablePosition);
                    }
                }
            }
        }

        public bool AddObserver(IWorldObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                return true;
            }

            return false;
        }

        public bool RemoveObserver(IWorldObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
                return true;
            }

            Debug.LogError($"Chunk::RemoveObserver: Cannot remove observer, because it is not present in observers' list");
            return false;
        }

        public IPlaceableStateHolder SetPlaceable(IPlaceable placeable, Vector3Int position)
        {
            if (!IsWithinChunk(position.GetVector3()))
                return null;

            var placeableInstance = placeableHolder.SetPlaceable(placeable, GetLocalPosition(position).GetVector3Int());
            _ = chunkData.SetPlaceableAt(placeable, GetLocalPosition(position).GetVector3Int());

            placeable.OnInstanceCreated(locatedInWorld, placeableInstance);

            return placeableInstance;
        }

        public IPlaceableStateHolder Place(IPlaceable placeable, Vector3Int position)
        {
            if (!IsWithinChunk(position.GetVector3()))
                return null;

            var placeableInstance = placeableHolder.SetPlaceable(placeable, GetLocalPosition(position).GetVector3Int());
            _ = chunkData.SetPlaceableAt(placeable, GetLocalPosition(position).GetVector3Int());

            placeable.OnInstanceCreated(locatedInWorld, placeableInstance);
            placeable.OnPlaced(locatedInWorld, placeableInstance);

            return placeableInstance;
        }

        public IPlaceableStateHolder GetPlaceableStateHolder(Vector3Int position)
        {
            return placeableHolder.GetPlaceableStateHolder(GetLocalPosition(position).GetVector3Int());
        }

        public bool IsWithinChunk(Vector3 position)
        {
            return Data.IsWithinChunkBounds(GetLocalPosition(position).GetVector3Int());
        }

        private Vector3 GetLocalPosition(Vector3 position)
        {
            return position - ChunkPosition.GetVector3();
        }

        public static Vector3Int ConvertToChunkPosition(Vector3 position)
        {
            int x = Mathf.FloorToInt(position.x / ChunkSideWidth) * ChunkSideWidth;
            int y = 0;
            int z = Mathf.FloorToInt(position.z / ChunkSideWidth) * ChunkSideWidth;

            return new Vector3Int(x, y, z);
        }
    }
}
