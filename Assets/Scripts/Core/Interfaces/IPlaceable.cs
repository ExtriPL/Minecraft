using UnityEngine;

namespace Minecraft.Core
{
    public interface IPlaceable
    {
        GameObject Model { get; }

        void OnInstanceCreated(GameObject instance);
        //void OnPlaced();
    }
}
