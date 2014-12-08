using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace finalProject
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ActionScene : GameScene
    {
        const int GRID_HEIGHT = 13;
        const int GRID_WIDTH = 17;
        const int DESTRUCTABLE_COUNT = 50;
        private SpriteBatch spriteBatch;
        private Vector2 stage;
        public Player[] players;
        public GridCell[,] grid; // 13 x 17
        float width, height;


        public ActionScene(Game1 game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.stage = ContentManager.Stage;
            GenerateGrid();
            Vector2 pSpeed = new Vector2(2, 2); // base speed is the same for both players;

            players = new Player[2];
            KeyBindings p1Bindings = new KeyBindings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space);
            Vector2 p1Pos = new Vector2(width +1, height +1);
            Player p1 = new Player(game, spriteBatch, ContentManager.Player1Tex, p1Pos, pSpeed, p1Bindings, Player.Direction.Right);
            p1.DrawOrder = 2;
            players[0] = p1;
            this.Components.Add(p1);


            KeyBindings p2Bindings = new KeyBindings(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.NumPad0);
            Vector2 p2Pos = new Vector2(width * (GRID_WIDTH -2), height * (GRID_HEIGHT -2)); // dimensions of both players are constant, using p1's values because it's already instantiated.
            Player p2 = new Player(game, spriteBatch, ContentManager.Player2Tex, p2Pos, pSpeed, p2Bindings, Player.Direction.Left);
            players[1] = p2;
            p2.DrawOrder = 2;
            this.Components.Add(p2);
            CollisionManager cm = new CollisionManager(game, players, grid);
            this.Components.Add(cm);
            
        }

        private void GenerateGrid()
        {
            grid = new GridCell[GRID_HEIGHT, GRID_WIDTH];
            width = ContentManager.Stage.X / GRID_WIDTH;
            height = ContentManager.Stage.Y / GRID_HEIGHT;

            for (int i = 0; i < GRID_HEIGHT; i++)
            {
                for (int j = 0; j < GRID_WIDTH; j++)
                {
                    Rectangle r = new Rectangle((int)(j * width),(int)(i * height), (int)width, (int)height);
                    // Set up the walls along the border
                    if (i == 0 || i == GRID_HEIGHT - 1 || j == 0 || j == GRID_WIDTH - 1)
                    {
                        Wall wall = new Wall(Game, spriteBatch, r, false);
                        GridCell gc = new GridCell(r, GridCell.CellType.Wall);
                        grid[i, j] = gc;
                        this.Components.Add(wall);
                    }
                    // Set up the walls inside the box;
                    else if (i % 2 == 0 && j % 2 == 0)
                    {
                        Wall wall = new Wall(Game, spriteBatch, r, false);
                        GridCell gc = new GridCell(r, GridCell.CellType.Wall);
                        grid[i, j] = gc;
                        this.Components.Add(wall);
                    }
                    else
                    {
                        GridCell gc = new GridCell(r, GridCell.CellType.Empty);
                       grid[i,j] = gc;

                    }
                }
            }

            // Set up the destructable walls
            int destructableCounter = 0;
            Random rand = new Random();
            while (destructableCounter < DESTRUCTABLE_COUNT)
            {
                int i = rand.Next(1, GRID_HEIGHT - 1);
                int j = rand.Next(1, GRID_WIDTH - 1);
                
                if (grid[i, j].Type == GridCell.CellType.Empty)
                {
                   if (i == 1 && j == 1)
                   {
                       // ensures player one starting block is always empty.
                       continue;
                   }
                   if (i == GRID_HEIGHT -2 && j == GRID_WIDTH -2)
                   {
                       // ensures player one starting block is always empty.
                       continue;
                   }
                   Rectangle r = new Rectangle((int)(j * width),(int)(i * height), (int)width, (int)height);
                   Wall destructable = new Wall(Game, spriteBatch, r, true );
                   grid[i, j] = new GridCell(r, GridCell.CellType.Destructable);
                   this.Components.Add(destructable);
                   destructableCounter++;
                }

            }

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}
