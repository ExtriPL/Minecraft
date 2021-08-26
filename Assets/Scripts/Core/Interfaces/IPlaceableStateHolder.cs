using UnityEngine;

namespace Minecraft.Core
{
    public interface IPlaceableStateHolder
    {
        Vector3Int Position { get; }
        IPlaceable StoredPlaceable { get; }

        void StorePlaceable(IPlaceable placeable);
    }
}
