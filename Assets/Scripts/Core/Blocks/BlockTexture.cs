using Minecraft.Core.Textures;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft.Core.Blocks
{
    [System.Serializable]
    public struct BlockTexture : IObjectTexture
    {
        public Texture2D up, down, front, back, left, right;
        public bool areAllSidesDifferent;
        public List<Texture2D> Textures => textures;

        private readonly List<Texture2D> textures;

        public BlockTexture(Texture2D up, Texture2D down, Texture2D front, Texture2D back, Texture2D left, Texture2D right)
        {
            this.up = up;
            this.down = down;
            this.front = front;
            this.back = back;
            this.left = left;
            this.right = right;
            areAllSidesDifferent = false;

            textures = new List<Texture2D>() { up, down, front, back, left, right };
        }
    }
}
