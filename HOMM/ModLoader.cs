using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HOMM
{
    public interface IHommMod
    {
        string Name();
        string Version();
        void OnEnable();
        void OnDisable();
    }
    
    public static class ModLoader
    {
        private const string ModsDir = "mods";
        
        private static readonly IDictionary<string, IHommMod> Mods = new Dictionary<string, IHommMod>();
        
        public static void RegisterMod(IHommMod mod)
        {
            var name = mod.Name();
            
            if (Mods.ContainsKey(name))
            {
                throw new ArgumentException($"Mod with name '{name}' already has");
            }
            
            Mods.Add(name, mod);
        }

        public static bool UnregisterMod(string name) => Mods.Remove(name);

        public static IHommMod GetMod(string name) => Mods[name];

        public static IReadOnlyDictionary<string, IHommMod> GetMods() =>
            (IReadOnlyDictionary<string, IHommMod>) Mods;

        public static void LoadMods()
        {
            var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (rootPath == null) return;
            
            IList<Assembly> mods = Directory.GetFiles(Path.Combine(rootPath, ModsDir), "*.dll")
                .Select(Assembly.LoadFrom).ToList();
            
            foreach (var modAsm in mods)
            {
                foreach (var type in modAsm.GetTypes())
                {
                    if (!typeof(IHommMod).IsAssignableFrom(type)) continue;

                    var mod = (IHommMod) Activator.CreateInstance(type);
                    
                    RegisterMod(mod);
                }
            }
        }

        public static void UnloadMods()
        {
            Mods.Clear();
        }

        public static void EnableMods()
        {
            foreach (var mod in Mods.Values)
            {
                mod.OnEnable();
            }
        }
        
        public static void DisableMods()
        {
            foreach (var mod in Mods.Values)
            {
                mod.OnDisable();
            }
        }
    }
}