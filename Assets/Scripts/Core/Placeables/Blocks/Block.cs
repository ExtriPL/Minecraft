using Minecraft.Core.Textures;
using Minecraft.Core.WorldGeneration;
using UnityEngine;

namespace Minecraft.Core.Placeables.Blocks
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
        [SerializeField]
        private bool isOpaque = true;

        public BlockTexture Texture => texture;
        public virtual GameObject Model => blockModel;
        public string RegistryName => registryName;
        public bool IsOpaque => isOpaque;

        public virtual void OnInstanceCreated(World world, IPlaceableStateHolder instance)
        {
            ITextureMapper textureMapper = instance.Instance.GetComponent<ITextureMapper>();
            textureMapper.MapTextures(Texture);

            BlockState state = instance.Instance.GetComponent<BlockState>();
            state.StorePlaceable(this);
        }

        public virtual void OnPlaced(World world, IPlaceableStateHolder instance) { }

        public virtual void OnNeighbourChanged(World world, IPlaceableStateHolder instance, IPlaceableStateHolder neighbourInstance, Vector3Int relativePosition) 
        {
            /*
             * If magnitude is equals to one, only one of {x, y, z} is set.
             * Therefor relative position indicates side of the block, not a vertex
             */
            if (relativePosition.sqrMagnitude == 1)
            {
                var textureMapper = instance.TextureMapper as BlockTextureMapper;
                bool sideShouldBeVisible = neighbourInstance == null || !neighbourInstance.StoredPlaceable.IsOpaque;
                textureMapper.SetSideVisibility(relativePosition, sideShouldBeVisible);
            }
        }

        public void SetTexture(BlockTexture texture)
        {
            this.texture = texture;
        }
    }
}
