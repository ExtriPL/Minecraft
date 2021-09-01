using UnityEngine;

namespace Minecraft.Core.WorldGeneration
{
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject chunkTemplate;
        private World world;

        private void Awake()
        {
            world = GetComponent<World>();
        }

        public Chunk CreateChunkAt(Vector3Int chunkPosition)
        {
            GameObject chunkInstance = Instantiate(chunkTemplate, transform);
            chunkInstance.transform.localPosition = chunkPosition;

            var chunk = chunkInstance.GetComponent<Chunk>();
            chunk.Create(world);

            return chunk;
        }
    }
}
