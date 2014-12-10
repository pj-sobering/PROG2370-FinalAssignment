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
        const int DESTRUCTABLE_COUNT = 50;
        const int STARTING_AREA_CLEAR = 4;
        const int GRID_HEIGHT = 13;
        const int GRID_WIDTH = 17;
        const int MONSTER_COUNT = 1;
        private SpriteBatch spriteBatch;
        private int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        private Player[] players;

        public Player[] Players
        {
            get { return players; }
            set { players = value; }
        }
        private Monster[] monsters;

        public Monster[] Monsters
        {
            get { return monsters; }
            set { monsters = value; }
        }
        private GridCell[,] grid; // 13 x 17

        public GridCell[,] Grid
        {
            get { return grid; }
            set { grid = value; }
        }
        public CollisionManager cm;
        float width, height;
        Text scoreString;
        Text livesString;
        Text levelString;
        Game1 game;

        public ActionScene(Game1 game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.level = 1;
            GenerateGrid();
            SpawnMonsters();
            Vector2 pSpeed = new Vector2(2, 2); // base speed is the same for both players;

            players = new Player[2];
            KeyBindings p1Bindings = new KeyBindings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space);
            Vector2 p1Pos = new Vector2(width +1, height +1);
            Player p1 = new Player(game, spriteBatch, ContentManager.Player1Tex, p1Pos, pSpeed, p1Bindings, Player.State.Right);
            p1.DrawOrder = 2;
            players[0] = p1;
            this.Components.Add(p1);

            KeyBindings p2Bindings = new KeyBindings(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.NumPad0);
            Vector2 p2Pos = new Vector2(width * (GRID_WIDTH -2), height * (GRID_HEIGHT -2)); // dimensions of both players are constant, using p1's values because it's already instantiated.
            Player p2 = new Player(game, spriteBatch, ContentManager.Player2Tex, p2Pos, pSpeed, p2Bindings, Player.State.Left);
            players[1] = p2;
            p2.DrawOrder = 2;
            this.Components.Add(p2);

            string message = "Total score: " + Player.Score.ToString();
            Vector2 mesPos = new Vector2(0, 0);
            scoreString = new Text(game, spriteBatch, ContentManager.Font, mesPos, message, Color.Cyan);
            this.Components.Add(scoreString);


            string message1 = "Lives " + Player.Lives.ToString();
            Vector2 messageSize = ContentManager.Font.MeasureString(message1);
            Vector2 mesPos1 = new Vector2(ContentManager.Stage.X - messageSize.X, 0);
            livesString = new Text(game, spriteBatch, ContentManager.Font, mesPos1, message1, Color.Cyan);
            this.Components.Add(livesString);

            string levelMessage = "Level " + level.ToString();
            float x = ContentManager.Stage.X /2 - (ContentManager.Font.MeasureString(levelMessage).X / 2);
            levelString = new Text(game, spriteBatch, ContentManager.Font, new Vector2(x, 0), levelMessage, Color.White);
            this.Components.Add(levelString);


            cm = new CollisionManager(game, players, grid, monsters);
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
                     ;
                     Rectangle r = new Rectangle((int)Math.Ceiling(width * j), (int)Math.Ceiling(height * i), (int)width +1, (int)height +1);
                    // Set up the walls along the border
                    if (i == 0 || i == GRID_HEIGHT - 1 || j == 0 || j == GRID_WIDTH - 1)
                    {
                        Wall wall = new Wall(Game, spriteBatch, r, false, true);
                        GridCell gc = new GridCell(r, new Vector2(i,j), wall);
                        grid[i, j] = gc;
                        this.Components.Add(wall);
                    }
                    // Set up the walls inside the box;
                    else if (i % 2 == 0 && j % 2 == 0)
                    {
                        Wall wall = new Wall(Game, spriteBatch, r, false);
                        GridCell gc = new GridCell(r, new Vector2(i,j), wall);
                        grid[i, j] = gc;
                        this.Components.Add(wall);
                    }
                    else
                    {
                        GridCell gc = new GridCell(r, new Vector2(i,j));
                        grid[i,j] = gc;
                    }
                }
            }
            SpawnDestructible();
        }

        private void SpawnDestructible()
        {
            // Set up the destructable walls
            int destructableCounter = 0;
            Random rand = new Random();
            while (destructableCounter < DESTRUCTABLE_COUNT)
            {
                int i = rand.Next(1, GRID_HEIGHT - 1);
                int j = rand.Next(1, GRID_WIDTH - 1);

                if (grid[i, j].Wall == null)
                {
                    if (i < STARTING_AREA_CLEAR && j < STARTING_AREA_CLEAR)
                    {
                        // ensures player one starting area is always empty.
                        continue;
                    }
                    if (i >= (GRID_HEIGHT - STARTING_AREA_CLEAR) && j >= GRID_WIDTH - STARTING_AREA_CLEAR)
                    {
                        // ensures player one starting areas is always empty.
                        continue;
                    }
                    Rectangle r = new Rectangle((int)Math.Ceiling(width * j), (int)Math.Ceiling(height * i), (int)width + 1, (int)height + 1);
                    Wall destructable = new Wall(Game, spriteBatch, r, true);
                    grid[i, j] = new GridCell(r, new Vector2(i, j), destructable);
                    this.Components.Add(destructable);
                    destructableCounter++;

                    Console.WriteLine("Looping through grid" + destructableCounter.ToString());
                }
            }
        }

        private void SpawnMonsters()
        {
            monsters = new Monster[MONSTER_COUNT];
            int monsterCount = 0;
            Random rand = new Random();
            while (monsterCount < MONSTER_COUNT)
            {
                int randomizerOffset = 1;
                int i = rand.Next(randomizerOffset, GRID_HEIGHT);
                int j = rand.Next(randomizerOffset, GRID_WIDTH);
                if (i < STARTING_AREA_CLEAR && j < STARTING_AREA_CLEAR)
                {
                    // ensures player one starting block is always empty.
                    continue;
                }
                if (i >= (GRID_HEIGHT - STARTING_AREA_CLEAR) && j >= GRID_WIDTH - STARTING_AREA_CLEAR)
                {
                    // ensures player two starting areas is always clear.
                    continue;
                }
                if (grid[i,j].Wall == null)
                {
                    int x = grid[i,j].Destination.X + ((grid[i,j].Destination.Width /2) - (int)(Monster.Width /2));
                    int y = grid[i,j].Destination.Y + ((grid[i,j].Destination.Height /2) - (int) (Monster.Height /2)); 
                    Vector2 pos = new Vector2(x, y);
                    Monster m = new Monster(game, spriteBatch, ContentManager.MonsterTex, pos, new Vector2(2, 2), grid);
                    m.DrawOrder = 0;
                    monsters[monsterCount] = m;
                    this.Components.Add(m);
                    monsterCount++;
                    Console.WriteLine("Monster added to board");
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
            scoreString.Message = "Total score: " + Player.Score.ToString();

            if (Player.Lives >= 0)
            {
                string message1 = "Lives " + Player.Lives.ToString();
                Vector2 messageSize = ContentManager.Font.MeasureString(message1);
                Vector2 mesPos1 = new Vector2(ContentManager.Stage.X - messageSize.X, 0);
                livesString.Position = mesPos1;
                livesString.Message = message1;
            }
            levelString.Message = "Level " + level.ToString();

            bool stillAlive = false;
            foreach (Monster m in monsters)
            {
                if (m.MonsterState != Monster.State.Dead)
                {
                    stillAlive = true;
                }
            }

            if (stillAlive == false)
            {
                this.level = 2;
                foreach (Player p in players)
                {
                    p.ReturnToStart();
                }
                for (int i = 0; i < GRID_HEIGHT; i++)
                {
                    for (int j = 0; j < GRID_WIDTH; j++)
                    {
                        if (Grid[i, j].Wall != null && Grid[i, j].Wall.Destructible == true)
                        {
                            grid[i, j].Wall.Enabled = false;
                            grid[i, j].Wall.Visible = false;
                            grid[i, j].Wall = null;
                        }
                    }
                }
                SpawnDestructible();
                SpawnMonsters();
            }
            base.Update(gameTime);
        }
    }
}
