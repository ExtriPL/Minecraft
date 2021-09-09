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
        public IPlaceable StoredPlaceable => storedBlock;
        public ITextureMapper TextureMapper { get; private set; }
        public GameObject Instance => gameObject;

        private void Awake()
        {
            TextureMapper = GetComponent<ITextureMapper>();
        }

        public void StorePlaceable(IPlaceable placeable)
        {
            if(placeable is Block block)
            {
                storedBlock = block;
                return;
            }

            Debug.LogError($"BlockState::StorePlaceable: Cannot store IPlaceable which is not a Block. Registry name: {placeable.RegistryName}");
        }
    }
}
