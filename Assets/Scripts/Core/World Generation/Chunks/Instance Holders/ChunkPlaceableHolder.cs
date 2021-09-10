using System.Collections.Generic;
using UnityEngine;

namespace Minecraft.Core.WorldGeneration.Chunks
{
    public class ChunkPlaceableHolder : MonoBehaviour
    {
        private IDictionary<Vector3Int, IPlaceableStateHolder> placeableStateHolders;

        private void Awake()
        {
            placeableStateHolders = new Dictionary<Vector3Int, IPlaceableStateHolder>();
        }

        public IPlaceableStateHolder SetPlaceable(IPlaceable placeable, Vector3Int localPosition)
        {
            DestroyPlaceableStateHolder(localPosition);

            GameObject placeableInstance = Instantiate(placeable.Model, transform);
            placeableInstance.transform.localPosition = localPosition;
            var stateHolder = placeableInstance.GetComponent<IPlaceableStateHolder>();

            placeableStateHolders.Add(localPosition, stateHolder);

            return stateHolder;
        }

        public IPlaceableStateHolder GetPlaceableStateHolder(Vector3Int localPosition)
        {
            if (placeableStateHolders.ContainsKey(localPosition))
                return placeableStateHolders[localPosition];

            return null;
        }

        private void DestroyPlaceableStateHolder(Vector3Int localPosition)
        {
            if(placeableStateHolders.ContainsKey(localPosition))
            {
                var placeableStateHolder = placeableStateHolders[localPosition];

                Destroy(placeableStateHolder.Instance);
                _ = placeableStateHolders.Remove(localPosition);
            }
        }
    }
}
