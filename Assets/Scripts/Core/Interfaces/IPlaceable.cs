using Minecraft.Core.WorldGeneration;
using UnityEngine;

namespace Minecraft.Core
{
    public interface IPlaceable : IRegistryObject
    {
        GameObject Model { get; }
        bool IsOpaque { get; }

        void OnInstanceCreated(World world, IPlaceableStateHolder instance);
        void OnPlaced(World world, IPlaceableStateHolder instance);
        void OnNeighbourChanged(World world, IPlaceableStateHolder instance, IPlaceableStateHolder neighbourInstance, Vector3Int relativePosition);
    }
}
