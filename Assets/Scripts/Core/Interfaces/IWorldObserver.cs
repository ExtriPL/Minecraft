using Minecraft.Core.WorldGeneration;
using Mirror;
using UnityEngine;

namespace Minecraft.Core
{
    public interface IWorldObserver
    {
        uint LoadingRange { get; }
        Vector3 Position { get; }
        NetworkIdentity Identity { get; }

        void OnObservationStarted(ChunkHolder chunkHolder);

        void SetPlaceable(Vector3Int chunkPosition, IPlaceable placeable, Vector3Int placeablePosition);
        void Place(Vector3Int chunkPosition, IPlaceable placeable, Vector3Int placeablePosition);
        void LoadChunk(Vector3Int chunkPosition);
        void UnloadChunk(Vector3Int chunkPosition);
    }
}
