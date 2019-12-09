namespace HOMM
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var modLoader = new ModLoader();
            
            modLoader.LoadMods();
        }
    }
}