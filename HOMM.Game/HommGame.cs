using HOMM.BattleObjects;
using HOMM.Game.Scenes;
using HOMM.Game.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HOMM.Game
{
    public class HommGame : Microsoft.Xna.Framework.Game
    {
        public static HommGame Instance;
        public static SpriteFont Font;
        
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private Battle _battle;
        private BaseScene _scene;

        public HommGame()
        {
            Instance = this;
            
            Content.RootDirectory = "Content";
            
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
                PreferMultiSampling = true,
            };
            _graphics.ApplyChanges();
            
            IsMouseVisible = true;
            
            Resolution.Init(ref _graphics);
            Resolution.SetVirtualResolution(1280, 720);
            Resolution.SetResolution(1280, 720, true);
            
            ChangeScene(new CreateScene(this));
        }

        protected override void Initialize()
        {
            _scene?.Initialize();
            Camera.Initialize();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _scene?.LoadContent();

            Font = Content.Load<SpriteFont>("Fonts/Arial");
            
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
            _scene?.UnloadContent();
            _spriteBatch.Dispose();
            
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            _scene?.Update();
            Camera.Update(new Vector2((float) Resolution.VirtualWidth / 2, (float) Resolution.VirtualHeight / 2));
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Resolution.BeginDraw();
            
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Camera.GetTransformMatrix());
            _scene?.Draw(_spriteBatch);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
        
        public Battle GetCurrentBattle() => _battle;

        public void ChangeScene(BaseScene scene, bool initialize = false)
        {
            _scene = scene;

            if (!initialize) return;
            
            _scene.Initialize();
            _scene.LoadContent();
        }
        
        public BaseScene GetCurrentScene() => _scene;

        public void StartBattle()
        {
            var scene = _scene as CreateScene;
            if (scene == null) return;

            _battle = scene.CreateBattle();
            _battle.Start();
            ChangeScene(new BattleScene(_battle, this), true);
        }
    }
}