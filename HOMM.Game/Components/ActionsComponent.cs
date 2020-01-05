using System.Collections.Generic;
using System.Linq;
using HOMM.Events;
using HOMM.Events.Turn;
using HOMM.Game.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HOMM.Game.Components
{
    public class ActionsComponent : ContainerComponent
    {
        private const float Margin = 5;
        private const int Padding = 5;
        
        private IList<ActionComponent> _skillComponents;
        
        public ActionsComponent(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            _skillComponents = new List<ActionComponent>();
            
            AddChild(new ActionComponent("attack", game));
            AddChild(new ActionComponent("wait", game));
            AddChild(new ActionComponent("defend", game));
            AddChild(new ActionComponent("surrender", game));
            
            UpdatePositionsAndSize();
            UpdateSkills();

            EventBus.NextTurn += OnNextTurn;
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (var component in _skillComponents)
            {
                component.Initialize();
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            
            foreach (var component in _skillComponents)
            {
                component.LoadContent();
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            
            foreach (var component in _skillComponents)
            {
                component.UnloadContent();
            }
        }

        public override void Update()
        {
            base.Update();
            
            UpdatePositionsAndSize();
            
            var mouse = Mouse.GetState();
            var leftClicked = Input.MouseLeftClicked();
            var rightClicked = Input.MouseRightClicked();

            foreach (var component in _skillComponents)
            {
                if (!component.IsEnabled()) continue;
                component.Update();
                    
                var clickable = component as IClickable;
                if (clickable == null || !component.Contains(mouse.X, mouse.Y)) continue;
                    
                if (leftClicked) clickable.OnLeftClick();
                if (rightClicked) clickable.OnRightClick();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(Position, new Vector2(Width, Height), Color.White);
            
            base.Draw(spriteBatch);

            foreach (var component in _skillComponents)
            {
                component.Draw(spriteBatch);
            }
        }
        
        private void UpdatePositionsAndSize()
        {
            BaseComponent child = null;
            for (var i = 0; i < Children.Count; ++i)
            {
                child = Children[i];
                child.SetPosition(Position + new Vector2((child.GetWidth() + Margin) * i + Padding, Padding));
            }

            for (var i = 0; i < _skillComponents.Count; ++i)
            {
                child = _skillComponents[i];
                child.SetPosition(Position + new Vector2(Width - Padding - child.GetWidth() - (child.GetWidth() + Margin) * i, Padding));
            }

            if (child == null) return;
            
            Width = Resolution.VirtualWidth;
            Height = child.GetHeight() + Padding * 2;
        }

        private void OnNextTurn(object sender, NextTurnEventArgs args)
        {
            UpdateSkills(true);
        }

        private void UpdateSkills(bool loadContent = false)
        {
            _skillComponents = HommGame.Instance.GetCurrentBattle()
                .GetCurrentStack()
                .GetBaseUnit()
                .GetSkillNames()
                .Select(skillName =>
                    new ActionComponent("use_skill", skillName, Game)
                ).ToList();

            if (!loadContent) return;
            
            foreach (var component in _skillComponents)
            {
                component.Initialize();
                component.LoadContent();
            }
        }
    }
}