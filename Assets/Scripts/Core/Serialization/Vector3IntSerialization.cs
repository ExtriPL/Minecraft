using Mirror;
using UnityEngine;

namespace Minecraft.Core.Serialization
{
    public static class Vector3IntSerialization
    {
        public static void WriteVector3IntObject(this NetworkWriter writer, Vector3Int vector)
        {
            writer.WriteInt(vector.x);
            writer.WriteInt(vector.y);
            writer.WriteInt(vector.z);
        }
        
        public static Vector3Int ReadVector3IntObject(this NetworkReader reader)
        {
            int x = reader.ReadInt();
            int y = reader.ReadInt();
            int z = reader.ReadInt();

            return new Vector3Int(x, y, z);
        }
    }
}
