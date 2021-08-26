using Mirror;

namespace Minecraft.Core.Serialization
{
    public static class IPlaceableSerialization
    {
        public static void WriteIPlaceableObject(this NetworkWriter writer, IPlaceable placeable)
        {
            writer.WriteString(placeable.RegistryName);
        }

        public static IPlaceable ReadIPlaceableObject(this NetworkReader reader)
        {
            IPlaceable placeable = (IPlaceable)RegistryManager.GetRegistryObject(reader.ReadString());

            return placeable;
        }
    }
}
