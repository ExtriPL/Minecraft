using Minecraft.Core.WorldGeneration;
using Mirror;
using UnityEngine;

namespace Minecraft.Core
{
    public interface IWorldObserver
    {
        uint LoadingRange { get; }
        Vector3 Position { get; }
        void OnObservationStarted(ChunkHolder chunkHolder);
        [TargetRpc]
        void TargetSetPlaceable(Vector3Int chunkPosition, IPlaceable placeable, Vector3Int placeablePosition);
    }
}
