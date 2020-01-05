using HOMM.Game.Utils;

namespace HOMM.Game.Components
{
    public class UnitComponent : ImageComponent
    {
        private readonly string _unitName;
        private readonly string _imageName;

        public UnitComponent(string unitName, Microsoft.Xna.Framework.Game game)
            : this(unitName, unitName, game) {}

        public UnitComponent(string unitName, string imageName, Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            _unitName = unitName;
            _imageName = imageName;
            
            Width = 100;
            Height = 100;
        }

        public override void LoadContent()
        {
            LoadImage(TextureLoader.Load("Units/" + _imageName, Game.Content));
        }
    }
}