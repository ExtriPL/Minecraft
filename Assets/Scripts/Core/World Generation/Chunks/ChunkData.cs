using UnityEngine;

namespace Minecraft.Core.WorldGeneration
{
    public struct ChunkData
    {
        private IPlaceable[] placedObjects;
        private int chunkSideSize, chunkHeight;

        public readonly int ChunkCapacity => chunkSideSize * chunkSideSize * chunkHeight;

        public ChunkData(int chunkSideSize, int chunkHeight)
        {
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
