using System.Collections.Generic;
using UnityEngine;

namespace Minecraft.Core
{
    public static class NumericExtensions
    {
        public static bool IsBetween(this int number, int min, int max)
        {
            return number >= min && number <= max;
        }

        public static bool IsBetween(this float number, float min, float max)
        {
            return number >= min && number <= max;
        }

        public static Vector3 GetVector3(this Vector3Int vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }

        public static Vector3Int GetVector3Int(this Vector3 vector)
        {
            return new Vector3Int((int)vector.x, (int)vector.y, (int)vector.z);
        }

        public static ISet<Vector3Int> GetAround(this Vector3Int vector, int range)
        {
            ISet<Vector3Int> positions = new HashSet<Vector3Int>();

            for(int dX = -range; dX <= range; dX++)
            {
                for(int dY = -range; dY <= range; dY++)
                {
                    for(int dZ = -range; dZ <= range; dZ++)
                    {
                        if (dX == 0 && dY == 0 && dZ == 0)
                            continue;

                        var displacement = new Vector3Int(dX, dY, dZ);
                        positions.Add(vector + displacement);
                    }
                }
            }

            return positions;
        }
    }
}
