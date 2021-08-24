using Minecraft.Core.Textures;
using UnityEngine;

namespace Minecraft.Core.Blocks
{
    [CreateAssetMenu(fileName = "New Block", menuName = "CovidCraft/Blocks/Base Block")]
    public class Block : ScriptableObject, IPlaceable
    {
        [SerializeField, HideInInspector]
        private BlockTexture texture;
        [SerializeField]
        private GameObject blockModel;

        public BlockTexture Texture => texture;
        public virtual GameObject Model => blockModel;

        public virtual void OnInstanceCreated(GameObject instance)
        {
            ITextureMapper textureMapper = instance.GetComponent<ITextureMapper>();
            textureMapper.MapTextures(Texture);

            BlockState state = instance.GetComponent<BlockState>();
            state.StoreBlock(this);
        }

        public void SetTexture(BlockTexture texture)
        {
            this.texture = texture;
        }
    }
}
