using Minecraft.Core.Textures;
using UnityEngine;

namespace Minecraft.Core.Blocks
{
    public class BlockState : MonoBehaviour, IPlaceableStateHolder
    {
        [SerializeField]
        private Block storedBlock;

        public Vector3Int Position => transform.position.GetVector3Int();
        public Block StoredBlock => storedBlock;

        private ITextureMapper textureMapper;

        private void Awake()
        {
            textureMapper = GetComponent<ITextureMapper>();
        }

        public void StoreBlock(Block block)
        {
            storedBlock = block;
        }
    }
}
