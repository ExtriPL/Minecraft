using Minecraft.Core.Blocks;
using Mirror;
using System;
using UnityEngine;

namespace Minecraft.Core.WorldGeneration
{
    [RequireComponent(typeof(ChunkHolder))]
    public class World : NetworkBehaviour
    {
        private ChunkHolder chunks;

        private void Awake()
        {
            chunks = GetComponent<ChunkHolder>();
        }

        public BlockState SetBlock(Block block, Vector3Int position)
        {
            return SetPlaceable(block, position) as BlockState;
        }

        public IPlaceableStateHolder SetPlaceable(IPlaceable placeable, Vector3Int position)
        {
            return chunks.SetPlaceable(placeable, position);
        }

        public BlockState PlaceBlock(Block block, Vector3Int position)
        {
            return Place(block, position) as BlockState;
        }

        public IPlaceableStateHolder Place(IPlaceable placeable, Vector3Int position)
        {
            return chunks.Place(placeable, position);
        }

        /// <summary>
        /// Finds block state in provided position.
        /// Returns nothing if blockstate has been not found.
        /// </summary>
        /// <param name="position">Position of blockstate</param>
        /// <returns>Block state at provided position</returns>
        public BlockState GetBlockState(Vector3Int position)
        {
            if (GetPlaceableStateHolder(position) is BlockState blockState)
                return blockState;

            return null;
        }

        public IPlaceableStateHolder GetPlaceableStateHolder(Vector3Int position)
        {
            return chunks.GetPlaceableStateHolder(position);
        }
    }
}
