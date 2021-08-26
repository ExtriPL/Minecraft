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
    }
}
