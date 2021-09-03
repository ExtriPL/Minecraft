using Minecraft.Core.WorldGeneration;
using Mirror;
using System.Threading.Tasks;
using UnityEngine;

namespace Minecraft.Core
{
    public class GameNetworkManager : NetworkManager
    {
        [SerializeField]
        private ChunkHolder chunkHolder;

        public override void Start()
        {
            base.Start();
            GetComponent<SpawnablePrefabLoader>().LoadSpawnablePrefabs(this);
        }

        public override async void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);

            while (conn.identity == null)
                await Task.Delay(50);

            var observer = conn.identity.GetComponent<IWorldObserver>();

            if (chunkHolder.IsWorldObserverRegistered(observer))
                return;

            chunkHolder.AddWorldObserver(observer);
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            base.OnServerAddPlayer(conn);
            var observer = conn.identity.GetComponent<IWorldObserver>();

            if (chunkHolder.IsWorldObserverRegistered(observer))
                return;

            chunkHolder.AddWorldObserver(observer);
        }
    }
}
