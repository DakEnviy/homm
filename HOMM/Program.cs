namespace HOMM
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ModLoader.LoadMods();
            ModLoader.EnableMods();
            ModLoader.DisableMods();
            ModLoader.UnloadMods();
        }
    }
}