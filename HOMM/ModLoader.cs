using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HOMM
{
    public abstract class HommMod
    {
        public string Name;
        public string Version;

        public abstract void OnEnable();
        
        public abstract void OnDisable();
    }
    
    public class ModLoader
    {
        private const string ModsDir = "mods";
        
        private static readonly IDictionary<string, HommMod> Mods = new Dictionary<string, HommMod>();
        
        public static void RegisterMod(string name, HommMod mod)
        {
            if (Mods.ContainsKey(name))
            {
                throw new ArgumentException($"Mod with name '{name}' already has");
            }
            
            Mods.Add(name, mod);
        }

        public static bool UnregisterMod(string name) => Mods.Remove(name);

        public static HommMod GetMod(string name) => Mods[name];

        public static IReadOnlyDictionary<string, HommMod> GetMods() =>
            (IReadOnlyDictionary<string, HommMod>) Mods;

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
                    if (!type.IsSubclassOf(typeof(HommMod))) continue;

                    var mod = (HommMod) Activator.CreateInstance(type);
                    var modName = (string) type.GetField("Name").GetValue(mod);
                    
                    RegisterMod(modName, mod);
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