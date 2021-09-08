﻿using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Minecraft.Core.WorldGeneration
{
    [RequireComponent(typeof(WorldGenerator))]
    public class ChunkHolder : NetworkBehaviour
    {
        private IDictionary<Vector3Int, Chunk> chunks = new Dictionary<Vector3Int, Chunk>();
        private ISet<IWorldObserver> worldObservers = new HashSet<IWorldObserver>();
        private WorldGenerator worldGenerator;

        #region Common

        private void Awake()
        {
            worldGenerator = GetComponent<WorldGenerator>();
        }

        private Chunk GetChunkAt(Vector3 position)
        {
            Vector3Int chunkPosition = Chunk.ConvertToChunkPosition(position);

            if (chunks.ContainsKey(chunkPosition))
                return chunks[chunkPosition];

            var createdChunk = worldGenerator.CreateChunkAt(chunkPosition);
            chunks.Add(chunkPosition, createdChunk);
            return createdChunk;
        }

        private Vector3Int GetChunkPositionAt(Vector3 position)
        {
            return GetChunkAt(position).ChunkPosition;
        }

        /// <summary>
        /// Set IPlaceable at given position in the world.
        /// If sync == true, sends info about setting to all other clients and server
        /// </summary>
        /// <returns>IPlaceableStateHolder which holds placed IPlaceable</returns>
        public IPlaceableStateHolder SetPlaceable(IPlaceable placeable, Vector3Int position, bool sync = true)
        {
            Chunk chunk = GetChunkAt(position.GetVector3());

            if (sync)
            {
                worldObservers.ToList().ForEach(observer =>
                {
                    observer.SetPlaceable(chunk.ChunkPosition, placeable, position);
                });
            }

            return chunk.SetPlaceable(placeable, position);
        }

        /// <summary>
        /// Places IPlaceable at given position in the world.
        /// If sync == true, sends info about placing to all other clients and server
        /// </summary>
        /// <returns>IPlaceableStateHolder which holds placed IPlaceable</returns>
        public IPlaceableStateHolder Place(IPlaceable placeable, Vector3Int position, bool sync = true)
        {
            Chunk chunk = GetChunkAt(position.GetVector3());

            if (sync)
            {
                worldObservers.ToList().ForEach(observer =>
                {
                    observer.Place(chunk.ChunkPosition, placeable, position);
                });
            }

            return chunk.Place(placeable, position);
        }

        /// <summary>
        /// Adds IWorldObserver to registered WorldObservers
        /// </summary>
        public void AddWorldObserver(IWorldObserver observer)
        {
            if (!worldObservers.Contains(observer))
            {
                worldObservers.Add(observer);
                observer.OnObservationStarted(this);
            }
        }

        public bool IsWorldObserverRegistered(IWorldObserver observer) => worldObservers.Contains(observer);

        /// <summary>
        /// Adds IWorldObserver to chunk located at chunkPosition
        /// </summary>
        public void AddChunkObserver(Vector3Int chunkPosition, IWorldObserver observer)
        {
            GetChunkAt(chunkPosition).AddObserver(observer);
        }

        /// <summary>
        /// Removes IWorldObserver from chunk present at chunkPosition
        /// </summary>
        public void RemoveChunkObserver(Vector3Int chunkPosition, IWorldObserver observer)
        {
            if(!chunks.ContainsKey(chunkPosition))
            {
                Debug.LogError($"ChunkHolder::RemoveChunkObserver: Cannot remove observer from chunk at {chunkPosition}, because it is not loaded");
                return;
            }

            GetChunkAt(chunkPosition).RemoveObserver(observer);
        }

        #endregion

        #region Server

        /// <summary>
        /// Returns positions of all chunks surrounding chunk in which is given position
        /// </summary>
        /// <param name="position">Given position</param>
        /// <param name="range">Distance from center chunk</param>
        [Server]
        public ISet<Vector3Int> GetChunkPositionsAround(Vector3 position, uint range)
        {
            var chunkPositions = new HashSet<Vector3Int>();
            Vector3Int centerChunkPosition = GetChunkPositionAt(position);

            for(int dX = (int)-range; dX <= range; dX++)
            {
                for (int dZ = (int)-range; dZ <= range; dZ++)
                {
                    Vector3Int displacement = new Vector3Int(dX, 0, dZ) * Chunk.ChunkSideWidth;
                    Vector3Int chunkPosition = centerChunkPosition + displacement;

                    chunkPositions.Add(chunkPosition);
                }
            }

            return chunkPositions;
        }

        /// <summary>
        /// Get chunk's data in which is given position
        /// </summary>
        /// <param name="chunkPosition">Given position</param>
        [Server]
        public ChunkData GetChunkData(Vector3Int chunkPosition)
        {
            return GetChunkAt(chunkPosition.GetVector3()).Data;
        }

        #endregion
    }
}