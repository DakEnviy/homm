using HOMM.Game.Commands;
using HOMM.Game.Utils;

namespace HOMM.Game.Components
{
    public class ActionComponent : ImageComponent, IClickable
    {
        private readonly string _actionName;
        private readonly string _skillName;
        private readonly string _imageName;
        
        public ActionComponent(string actionName, Microsoft.Xna.Framework.Game game)
            : this(actionName, actionName, null, game) {}
            
        public ActionComponent(string actionName, string skillName, Microsoft.Xna.Framework.Game game)
            : this(actionName, actionName, skillName, game) {}

        public ActionComponent(string actionName, string imageName, string skillName, Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            _actionName = actionName;
            _skillName = skillName;
            _imageName = imageName;
            
            Width = 30;
            Height = 30;
        }
        
        public override void LoadContent()
        {
            LoadImage(_skillName == null
                ? TextureLoader.Load("Actions/" + _imageName, Game.Content)
                : TextureLoader.Load("Skills/" + _skillName, Game.Content)
            );
        }

        public void OnLeftClick()
        {
            switch (_actionName)
            {
                case "attack":
                    CommandsManager.SelectCommand(new AttackCommand(HommGame.Instance.GetCurrentBattle().GetCurrentStack()));
                    break;
                case "wait":
                    CommandsManager.Execute(new WaitCommand(HommGame.Instance.GetCurrentBattle().GetCurrentStack()));
                    break;
                case "defend":
                    CommandsManager.Execute(new DefendCommand(HommGame.Instance.GetCurrentBattle().GetCurrentStack()));
                    break;
                case "surrender":
                    CommandsManager.Execute(new SurrenderCommand(HommGame.Instance.GetCurrentBattle().GetCurrentArmy()));
                    break;
                case "use_skill":
                    CommandsManager.SelectCommand(new UseSkillCommand(HommGame.Instance.GetCurrentBattle().GetCurrentStack(), _skillName));
                    break;
                default:
                    break;
            }
        }

        public void OnRightClick() {}
    }
}