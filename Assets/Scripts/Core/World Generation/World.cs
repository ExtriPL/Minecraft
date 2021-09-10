using Minecraft.Core.Placeables;
using Minecraft.Core.Placeables.Blocks;
using Mirror;
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
            var placeableStateHolder = chunks.SetPlaceable(placeable, position);
            UpdateNeighbourPlaceableStateHolders(placeableStateHolder, position);

            return placeableStateHolder;
        }

        public BlockState PlaceBlock(Block block, Vector3Int position)
        {
            return Place(block, position) as BlockState;
        }

        public IPlaceableStateHolder Place(IPlaceable placeable, Vector3Int position)
        {
            var placeableStateHolder = chunks.Place(placeable, position);
            UpdateNeighbourPlaceableStateHolders(placeableStateHolder, position);

            return placeableStateHolder;
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

        private void UpdateNeighbourPlaceableStateHolders(IPlaceableStateHolder placeableStateHolder, Vector3Int position)
        {
            var aroundPositions = position.GetAround(1);
            
            foreach(var neighbourPosition in aroundPositions)
            {
                var neighbour = GetPlaceableStateHolder(neighbourPosition);
                var relativePosition = position - neighbourPosition;

                neighbour?.StoredPlaceable.OnNeighbourChanged(this, neighbour, placeableStateHolder, relativePosition);
                placeableStateHolder?.StoredPlaceable.OnNeighbourChanged(this, placeableStateHolder, neighbour, -relativePosition);
            }
        }
    }
}
