using Microsoft.Xna.Framework;
using StarField;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ResolutionBuddy;

namespace StarFieldDemo
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Stars field;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
			Resolution.Init(ref graphics);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content. Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// Change Virtual Resolution
			Resolution.SetDesiredResolution(1280, 720);

			//set the desired resolution
			Resolution.SetScreenResolution(1024, 768, false);

			// TODO: Add your initialization logic here
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			//TODO: use this.Content to load your game content here

			field = new Stars(GraphicsDevice, Resolution.TitleSafeArea);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Allows the game to exit
			if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
			Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				this.Exit();
			}

			// TODO: Add your update logic here

			const float scrollSpeed = 10.0f;

			Vector2 vel = Vector2.Zero;
			if (Keyboard.GetState().IsKeyDown(Keys.Up))
			{
				vel.Y += scrollSpeed;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Down))
			{
				vel.Y -= scrollSpeed;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Left))
			{
				vel.X += scrollSpeed;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Right))
			{
				vel.X -= scrollSpeed;
			}

			field.Update(vel, Resolution.TitleSafeArea);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			// Clear to Black
			graphics.GraphicsDevice.Clear(new Color(0,0,64));

			// Calculate Proper Viewport according to Aspect Ratio
			Resolution.ResetViewport();

			spriteBatch.Begin(SpriteSortMode.Immediate,
			BlendState.NonPremultiplied,
			null, null, null, null,
			Resolution.TransformationMatrix());

			field.Render(spriteBatch);

			//prim.Rectangle(Resolution.TitleSafeArea, Color.Red);

			spriteBatch.End();

			// The real drawing happens inside the screen manager component.
			base.Draw(gameTime);
		}
	}
}