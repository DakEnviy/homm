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
    }
    
    public class ModLoader
    {
        private const string ModsDir = "mods";
        
        public void LoadMods()
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

                    var modObj = Activator.CreateInstance(type);
                    var modName = type.GetField("Name").GetValue(modObj);
                    var modVersion = type.GetField("Version").GetValue(modObj);
                    
                    Console.WriteLine(modName);
                    Console.WriteLine(modVersion);
                }
            }
        }
    }
}