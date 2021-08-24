using System.Collections.Generic;
using UnityEngine;

namespace Minecraft.Core.Textures
{
    public interface IObjectTexture
    {
        List<Texture2D> Textures { get; }
    }
}
