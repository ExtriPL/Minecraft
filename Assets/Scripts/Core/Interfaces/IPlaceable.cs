using Minecraft.Core.WorldGeneration;
using UnityEngine;

namespace Minecraft.Core
{
    public interface IPlaceable : IRegistryObject
    {
        GameObject Model { get; }

        void OnInstanceCreated(GameObject instance);
        void OnPlaced(World world, Vector3Int position);
    }
}
