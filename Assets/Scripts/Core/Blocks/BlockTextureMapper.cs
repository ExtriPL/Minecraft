using Minecraft.Core.Textures;
using UnityEngine;

namespace Minecraft.Core.Blocks
{
    public class BlockTextureMapper : MonoBehaviour, ITextureMapper
    {
        [SerializeField]
        private Texture2D defaultTexture;
        [SerializeField]
        private MeshRenderer modelRenderer;

        public void MapTextures(IObjectTexture texture)
        {
            BlockTexture blockTexture = RetrieveBlockTexture(texture);

            Material material = modelRenderer.material;

            material.SetTexture("_Up", blockTexture.up);
            material.SetTexture("_Down", blockTexture.down);
            material.SetTexture("_Front", blockTexture.front);
            material.SetTexture("_Back", blockTexture.back);
            material.SetTexture("_Left", blockTexture.left);
            material.SetTexture("_Right", blockTexture.right);

            modelRenderer.material = material;
            Debug.Log("Map");
        }

        private BlockTexture RetrieveBlockTexture(IObjectTexture texture)
        {
            if (texture is BlockTexture)
                return (BlockTexture)texture;

            Texture2D up = texture.Textures.Count >= 1 ? texture.Textures[0] : defaultTexture;
            Texture2D down = texture.Textures.Count >= 2 ? texture.Textures[1] : defaultTexture;
            Texture2D front = texture.Textures.Count >= 3 ? texture.Textures[2] : defaultTexture;
            Texture2D back = texture.Textures.Count >= 4 ? texture.Textures[3] : defaultTexture;
            Texture2D left = texture.Textures.Count >= 5 ? texture.Textures[4] : defaultTexture;
            Texture2D right = texture.Textures.Count >= 6 ? texture.Textures[5] : defaultTexture;

            return new BlockTexture(up, down, front, back, left, right);
        }
    }
}
