using Minecraft.Core.Placeables;
using UnityEngine;

namespace Minecraft.Core.WorldGeneration
{
    [System.Serializable]
    public struct ChunkData
    {
        private IPlaceable[] placedObjects;
        private int chunkSideSize, chunkHeight;
        private Vector3Int chunkPosition;

        public readonly int ChunkCapacity => chunkSideSize * chunkSideSize * chunkHeight;
        public readonly Vector3Int ChunkPosition => chunkPosition;

        public ChunkData(Vector3Int chunkPosition, int chunkSideSize, int chunkHeight)
        {
            this.chunkPosition = chunkPosition;
            this.chunkSideSize = chunkSideSize;
            this.chunkHeight = chunkHeight;

            placedObjects = new IPlaceable[chunkSideSize * chunkSideSize * chunkHeight];
        }
        
        public IPlaceable GetPlaceableAt(Vector3Int localPosition)
        {
            if (IsWithinChunkBounds(localPosition))
                return placedObjects[GetPositionInPlacedObjects(localPosition)];

            return null;
        }

        public bool SetPlaceableAt(IPlaceable placeable, Vector3Int localPosition)
        {
            if(IsWithinChunkBounds(localPosition))
            {
                placedObjects[GetPositionInPlacedObjects(localPosition)] = placeable;
                return true;
            }

            return false;
        }

        public bool IsWithinChunkBounds(Vector3Int localPosition)
        {
            return localPosition.x.IsBetween(0, chunkSideSize - 1)
                && localPosition.y.IsBetween(0, chunkHeight - 1)
                && localPosition.z.IsBetween(0, chunkSideSize - 1);
        }

        private int GetPositionInPlacedObjects(Vector3Int localPosition)
        {
            return localPosition.x + chunkSideSize * localPosition.z + (chunkSideSize * chunkSideSize) * localPosition.y;
        }
    }
}
