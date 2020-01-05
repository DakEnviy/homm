using System.Collections.Generic;
using HOMM.Game.Components;
using HOMM.Game.Utils;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HOMM.Game.Scenes
{
    public abstract class BaseScene
    {
        private readonly Microsoft.Xna.Framework.Game _game;
        private readonly IList<BaseComponent> _components;

        protected BaseScene(Microsoft.Xna.Framework.Game game)
        {
            _game = game;
            _components = new List<BaseComponent>();
        }

        public virtual void Initialize()
        {
            foreach (var component in _components)
            {
                component.Initialize();
            }
        }

        public virtual void LoadContent()
        {
            foreach (var component in _components)
            {
                component.LoadContent();
            }
        }

        public virtual void UnloadContent()
        {
            foreach (var component in _components)
            {
                component.UnloadContent();
            }
        }

        public virtual void Update()
        {
            var mouse = Mouse.GetState();
            var leftClicked = Input.MouseLeftClicked();
            var rightClicked = Input.MouseRightClicked();

            foreach (var component in _components)
            {
                if (!component.IsEnabled()) continue;
                component.Update();
                    
                var clickable = component as IClickable;
                if (clickable == null || !component.Contains(mouse.X, mouse.Y)) continue;
                    
                if (leftClicked) clickable.OnLeftClick();
                if (rightClicked) clickable.OnRightClick();
            }
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
            {
                if (component.IsVisible()) component.Draw(spriteBatch);
            }
        }

        public BaseComponent AddComponent(BaseComponent component, bool initialize = false)
        {
            if (!component.IsInitialized() && initialize)
            {
                component.Initialize();
                component.LoadContent();
            }
            
            _components.Add(component);

            return component;
        }

        public bool RemoveComponent(BaseComponent component)
        {
            component.UnloadContent();
            
            return _components.Remove(component);
        }
    }
}