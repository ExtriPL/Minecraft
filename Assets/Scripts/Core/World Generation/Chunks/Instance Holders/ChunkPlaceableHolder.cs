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

        public GameObject SetPlaceable(IPlaceable placeable, Vector3Int localPosition)
        {
            DestroyPlaceableStateHolder(localPosition);

            GameObject placeableInstance = Instantiate(placeable.Model, transform);
            placeableInstance.transform.localPosition = localPosition;
            placeableStateHolders.Add(localPosition, placeableInstance.GetComponent<IPlaceableStateHolder>());

            return placeableInstance;
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
