using System.Collections.Generic;
using UnityEngine;

namespace Minecraft.Core
{
    public class RegistryManager : MonoBehaviour
    {
        private List<IRegistryObject> registryObjects = new List<IRegistryObject>();
        private Dictionary<string, IRegistryObject> registry = new Dictionary<string, IRegistryObject>();

        private static RegistryManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            FindRegistryObjects();
            RegisterObjects();
        }

        private void FindRegistryObjects()
        {
            foreach(Object obj in Resources.LoadAll(@"Config/", typeof(IRegistryObject)))
            {
                if (obj is IRegistryObject registryObject)
                    registryObjects.Add(registryObject);
            }
        }

        private void RegisterObjects()
        {
            foreach (IRegistryObject registryObject in registryObjects)
                _ = AddToRegistry(registryObject);
        }

        public static bool AddToRegistry(IRegistryObject registryObject)
        {
            string registryName = registryObject.RegistryName;

            if (!instance.registry.ContainsKey(registryName))
            {
                instance.registry.Add(registryName, registryObject);
                return true;
            }

            Debug.LogError($"RegistryManager::AddToRegistry: Cannot register {registryName} because it is already present in registry");
            return false;
        }

        public static IRegistryObject GetRegistryObject(string registryName)
        {
            if (instance.registry.TryGetValue(registryName, out IRegistryObject registryObject))
                return registryObject;

            Debug.LogError($"RegistryManager::GetRegistryObject: Object {registryName} not found in registry");
            return null;
        }
    }
}
