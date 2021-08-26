using Minecraft.Core.Textures;
using Minecraft.Core.WorldGeneration;
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
        [SerializeField]
        private string registryName;

        public BlockTexture Texture => texture;
        public virtual GameObject Model => blockModel;
        public string RegistryName => registryName;

        public virtual void OnInstanceCreated(GameObject instance)
        {
            ITextureMapper textureMapper = instance.GetComponent<ITextureMapper>();
            textureMapper.MapTextures(Texture);

            BlockState state = instance.GetComponent<BlockState>();
            state.StorePlaceable(this);
        }

        public virtual void OnPlaced(World world, Vector3Int position) { }

        public void SetTexture(BlockTexture texture)
        {
            this.texture = texture;
        }
    }
}
