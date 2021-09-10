using Minecraft.Core.Placeables;
using Mirror;

namespace Minecraft.Core.Serialization
{
    public static class IPlaceableSerialization
    {
        public static void WriteIPlaceableObject(this NetworkWriter writer, IPlaceable placeable)
        {
            if(placeable != null)
                writer.WriteString(placeable.RegistryName);
        }

        public static IPlaceable ReadIPlaceableObject(this NetworkReader reader)
        {
            if (reader.Length == 0)
                return null;

            IPlaceable placeable = (IPlaceable)RegistryManager.GetRegistryObject(reader.ReadString());

            return placeable;
        }
    }
}
