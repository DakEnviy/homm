using System.Collections.Generic;
using HOMM.Game.Utils;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HOMM.Game.Components
{
    public class ContainerComponent : BaseComponent
    {
        protected IList<BaseComponent> Children;
        
        public ContainerComponent(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            Children = new List<BaseComponent>();
        }

        public override void Initialize()
        {
            foreach (var child in Children)
            {
                if (!child.IsInitialized()) child.Initialize();
            }
            
            base.Initialize();
        }

        public override void LoadContent()
        {
            foreach (var child in Children)
            {
                child.LoadContent();
            }
        }

        public override void UnloadContent()
        {
            foreach (var child in Children)
            {
                child.UnloadContent();
            }
        }

        public override void Update()
        {
            var mouse = Mouse.GetState();
            var leftClicked = Input.MouseLeftClicked();
            var rightClicked = Input.MouseRightClicked();

            foreach (var child in Children)
            {
                if (!child.IsEnabled()) continue;
                child.Update();
                    
                var clickable = child as IClickable;
                if (clickable == null || !child.Contains(mouse.X, mouse.Y)) continue;
                    
                if (leftClicked) clickable.OnLeftClick();
                if (rightClicked) clickable.OnRightClick();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            foreach (var child in Children)
            {
                if (child.IsVisible()) child.Draw(spriteBatch);
            }
        }

        public void AddChild(BaseComponent component)
        {
            Children.Add(component);
        }

        public void RemoveChild(BaseComponent component)
        {
            Children.Remove(component);
        }

        public IList<BaseComponent> GetChildren() => Children;
        
        public void SetChildren(IEnumerable<BaseComponent> children)
        {
            Children = new List<BaseComponent>(children);
        }
    }
}