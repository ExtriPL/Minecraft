using Minecraft.Core.Textures;
using UnityEngine;

namespace Minecraft.Core.Placeables
{
    public interface IPlaceableStateHolder
    {
        Vector3Int Position { get; }
        IPlaceable StoredPlaceable { get; }
        ITextureMapper TextureMapper { get; }
        GameObject Instance { get; }

        void StorePlaceable(IPlaceable placeable);
    }
}
