using Mirror;

namespace Minecraft.Core
{
    public class GameNetworkManager : NetworkManager
    {
        public override void Start()
        {
            base.Start();
            GetComponent<SpawnablePrefabLoader>().LoadSpawnablePrefabs(this);
        }
    }
}
