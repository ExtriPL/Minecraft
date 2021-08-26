using Mirror;

namespace Minecraft.Core.Serialization
{
    public static class IRegistryObjectSerialization
    {
        public static void WriteIRegistryObject(this NetworkWriter writer, IRegistryObject registryObject)
        {
            writer.WriteString(registryObject.RegistryName);
        }

        public static IRegistryObject ReadIRegistryObject(this NetworkReader reader)
        {
            IRegistryObject registryObject = RegistryManager.GetRegistryObject(reader.ReadString());

            return registryObject;
        }
    }
}
