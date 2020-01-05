namespace HOMM.Game
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ModLoader.LoadMods();
            ModLoader.EnableMods();
            
            using (var game = new HommGame())
            {
                game.Run();
            }
            
            ModLoader.DisableMods();
            ModLoader.UnloadMods();
        }
    }
}