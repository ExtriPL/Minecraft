using Mirror;
using UnityEngine;

namespace Minecraft.Core
{
    public class SpawnablePrefabLoader : MonoBehaviour
    {
        public void LoadSpawnablePrefabs(NetworkManager networkManager)
        {
            foreach (var spawnablePrefab in Resources.LoadAll(@"Spawnable Prefabs", typeof(GameObject)))
            {
                GameObject instance = spawnablePrefab as GameObject;
                networkManager.spawnPrefabs.Add(instance);
            }
        }
    }
}
