using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCustomGame
{
    public class Game : ICanDraw
    {
        TetrisMap map;
        int _position;
        string[] bgImagePath = { "background/background1.jpg", "background/background2.jpg", "background/background3.jpg", "background/background4.jpg", "background/background5.jpg" };
        bool hasEnded = false;
        int count = 0;
        int counter = 150;
        int delayBlock = 600;
        Block block;
        Queue<Block> nextBlocks = new Queue<Block>();
        Bitmap background = new Bitmap("0", "background/background1.jpg");
        Font font = SplashKit.LoadFont("font1", "./font/PressStart2P-Regular.ttf");
        Control control;

        public Control Control
        {
            get { return control; }
            set { control = value; }
        }

        public Block Block
        {
            get { return block; }
            set { block = value; }
        }
        public Game(int position)
        {
            _position = position;
        }
        public TetrisMap Map
        {
            get { return map; }
            set { map = value; }
        }

        public int Position
        { get { return _position; } set { _position = value; } }

        public void Prepare()
        {
            Map = new TetrisMap(20, 10, _position);
            Map.Initialize();
            Block = CreateRandomBlock();
            for (int i = 0; i < 3; i++)
            {
                nextBlocks.Enqueue(CreateRandomBlock());
            }
        }
        public void Run()
        {
            try
            {
                if (!Map.CheckGameOver())
                {
                    counter += 150;
                    bool checkHasMoveDown = false;
                    Draw();
                    Map.Draw();
                    Block.Draw();
                    Process();
                   
                }
                else
                {
                    EndGame();
                }
            }
            catch (Exception e)
            {
                EndGame();
            }

        }

        public void EndGame()
        {
            Draw();
            Map.Draw();
            Block.Draw();
            hasEnded = true;
            if (hasEnded)
            {
                SplashKit.FillRectangle(SplashKit.RGBAColor(0, 0, 0, 0.9), Position, 0, 400, 800);
            }
            SplashKit.DrawText("GAME OVER...", Color.White, font, 25, Position + 50, 330);
            SplashKit.DrawText("Your score is", Color.White, font, 25, Position + 50, 400);
            SplashKit.DrawText(Map.Score.ToString(), Color.White, font, 50, Position + 180, 460);
        }
        public void Process()
        {
            bool checkHasMoveDown = false;
            if (SplashKit.KeyDown(control.MoveLeft))
            {
                if ((block.X > 0) && !CheckBlockCollidedWithWallLeft())
                {
                    block.MoveLeft();
                }
            }
            else if (SplashKit.KeyDown(control.MoveRight))
            {
                if ((block.X < Map.Columns - block.Width) && !CheckBlockCollidedWithWallRight())
                {
                    block.MoveRight();
                }
            }
            if (SplashKit.KeyDown(control.RotateLeft))
            {
                if (CanRotateBlockLeft())
                {
                    block.RotateLeft();
                }

            }
            else if (SplashKit.KeyDown(control.RotateRight))
            {
                if (CanRotateBlockRight())
                {
                    block.RotateRight();
                }
            }
            if (SplashKit.KeyDown(control.MoveDown))
            {
                if (!CheckBlockWentToBottom())
                {
                    block.MoveDown();
                    checkHasMoveDown = true;
                }
            }
            if (SplashKit.KeyDown(control.BlockFall))
            {
                while ((!CheckBlockWentToBottom()) && (block.Y + block.Height - 1 < Map.Rows - 1))
                {
                    block.MoveDown();
                }
                checkHasMoveDown = true;
            }

            //make the block fall down for 1 row
            if ((counter % delayBlock == 0) && (checkHasMoveDown == false))
            {
                block.MoveDown();
                counter = 0;
            }

            if (CheckBlockWentToBottom())
            {
                AddBlock();
                block = nextBlocks.Dequeue();
                nextBlocks.Enqueue(CreateRandomBlock());

            }
            Map.CheckAllRows();

            //increase the speed of block falling whenever player's score increase by 10
            if ((Map.Score % 10 == 0) && (delayBlock > 150) && (Map.Score != 0))
            {
                delayBlock -= 150;
            }
        }

        public void Draw()
        {
            //draw background for the game
            SplashKit.DrawBitmap(background, Position, 0);
            if (SplashKit.KeyTyped(control.ChangeBackground))
            {
                count = (count + 1) % bgImagePath.Length;
                background = new Bitmap(count.ToString(), bgImagePath[count]);
                SplashKit.DrawBitmap(background, Position, 0);
            }

            //draw score box
            SplashKit.FillRectangle(Color.Black, Position - 200, 600, 200, 200);
            SplashKit.DrawRectangle(Color.White, Position - 190, 610, 180, 180);
            SplashKit.DrawText("Score", Color.White, font, 30, Position - 170, 620);
            SplashKit.DrawText(Map.Score.ToString(), Color.White, font, 50, Position - 120, 690);

            //draw info box showing next upcoming blocks
            int order = -1;
            SplashKit.FillRectangle(Color.Black, Position - 200, 0, 200, 400);
            SplashKit.DrawRectangle(Color.White, Position - 190, 80, 180, 300);
            SplashKit.DrawText("Next", Color.White, font, 30, Position - 150, 20);
            foreach (Block block in nextBlocks)
            {
                order++;
                block.DrawInQueue(order);
            }

            //draw control guide
            SplashKit.FillRectangle(Color.RandomRGB(255), Position - 400, 400, 400, 200);
            SplashKit.DrawText("GAME CONTROLS:", Color.Black, font, 15, Position - 300, 420);
            SplashKit.DrawText(SplashKit.KeyName(control.MoveLeft) + " KEY: MOVE LEFT", Color.Black, font, 15, Position - 390, 450);
            SplashKit.DrawText(SplashKit.KeyName(control.MoveRight) + " KEY: MOVE RIGHT", Color.Black, font, 15, Position - 390, 470);
            SplashKit.DrawText(SplashKit.KeyName(control.MoveDown) + " KEY: MOVE DOWN", Color.Black, font, 15, Position - 390, 490);
            SplashKit.DrawText(SplashKit.KeyName(control.BlockFall) + " KEY: BLOCK FALL", Color.Black, font, 15, Position - 390, 510);
            SplashKit.DrawText(SplashKit.KeyName(control.RotateLeft) + " KEY: ROTATE LEFT", Color.Black, font, 15, Position - 390, 530);
            SplashKit.DrawText(SplashKit.KeyName(control.RotateRight) + " KEY: ROTATE RIGHT", Color.Black, font, 15, Position - 390, 550);
            SplashKit.DrawText(SplashKit.KeyName(control.ChangeBackground) + " KEY: CHANGE BACKGROUND", Color.Black, font, 15, Position - 390, 570);
        }

        private Block CreateRandomBlock()
        {
            Random random = new Random();
            int blockType = random.Next(7);

            switch (blockType)
            {
                case 0:
                    return new IBlock(random.Next(6), 0, Position);
                case 1:
                    return new SBlock(random.Next(7), 0, Position);
                case 2:
                    return new ZBlock(random.Next(7), 0, Position);
                case 3:
                    return new OBlock(random.Next(8), 0, Position);
                case 4:
                    return new LBlock(random.Next(7), 0, Position);
                case 5:
                    return new TBlock(random.Next(7), 0, Position);
                case 6:
                    return new PBlock(random.Next(7), 0, Position);
                default:
                    throw new InvalidOperationException("Invalid block type generated.");
            }
        }


        public bool CheckBlockWentToBottom()
        {
            for (int i = block.X; i < block.X + block.Width; i++)
            {
                if (block.Y + block.Height - 1 >= Map.Rows)
                {
                    return true;
                }
            }

            if (CheckBlockCollidedWithOtherBlockAtBottom())
            {
                return true;
            }

            return false;
        }

        public bool CheckBlockCollidedWithOtherBlockAtBottom()
        {
            for (int i = 0; i < block.Height; i++)
            {
                for (int j = 0; j < block.Width; j++)
                {
                    if (block.Layout[i, j] != 0)
                    {
                        int x = block.X + j;
                        int y = block.Y + i;
                        if (Map.ReadMap(y, x) != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void AddBlock()
        {
            for (int i = 0; i < block.Height; i++)
            {
                for (int j = 0; j < block.Width; j++)
                {
                    if (block.Layout[i, j] != 0)
                    {
                        Map.UpdateMap(block.Y + i - 1, block.X + j, block.Layout[i, j]);
                    }
                }
            }
        }

        public bool CheckBlockCollidedWithWallRight()
        {
            for (int i = 0; i < block.Height; i++)
            {
                for (int j = 0; j < block.Width; j++)
                {
                    if (block.Layout[i, j] != 0)
                    {
                        int x = block.X + j;
                        int y = block.Y + i;

                        if (x < 0 || x >= Map.Columns || y < 0 || y >= Map.Rows)
                        {
                            return true;
                        }

                        if (Map.ReadMap(y, x + 1) != 0 && (x < Map.Columns - 1))
                        {
                            return true;
                        }

                    }
                }
            }
            return false;
        }

        public bool CheckBlockCollidedWithWallLeft()
        {
            for (int i = 0; i < block.Height; i++)
            {
                for (int j = 0; j < block.Width; j++)
                {
                    if (block.Layout[i, j] != 0)
                    {
                        int x = block.X + j;
                        int y = block.Y + i;
                        if (x < 0 || x >= Map.Columns || y < 0 || y >= Map.Rows)
                        {
                            return true;
                        }

                        if (x - 1 > 0)
                        {
                            if (Map.ReadMap(y, x - 1) != 0)
                            { return true; }
                        }
                    }
                }
            }
            return false;
        }

        public bool CanRotateBlockRight()
        {
            // Create a copy of the block and rotate it
            Block rotatedBlock = block.Clone();
            rotatedBlock.RotateRight();

            //Check whether any of the cells in the rotated block are outside the game board
            for (int i = 0; i < rotatedBlock.Height; i++)
            {
                for (int j = 0; j < rotatedBlock.Width; j++)
                {
                    if (rotatedBlock.Layout[i, j] != 0)
                    {
                        int x = rotatedBlock.X + j;
                        int y = rotatedBlock.Y + i;

                        // Check if block is out of bounds of the game board
                        if (x < 0 || x >= Map.Columns || y < 0 || y >= Map.Rows)
                        {
                            return false;
                        }

                        // Check if block collides with another block already on board
                        if (Map.ReadMap(y, x) != 0)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public bool CanRotateBlockLeft()
        {
            // Create a copy of the block and rotate it
            Block rotatedBlock = block.Clone();
            rotatedBlock.RotateLeft();


            //Check whether any of the cells in the rotated block are outside the game board
            for (int i = 0; i < rotatedBlock.Height; i++)
            {
                for (int j = 0; j < rotatedBlock.Width; j++)
                {
                    if (rotatedBlock.Layout[i, j] != 0)
                    {
                        int x = rotatedBlock.X + j;
                        int y = rotatedBlock.Y + i;

                        // Check if block is out of bounds of the game board
                        if (x < 0 || x >= Map.Columns || y < 0 || y >= Map.Rows)
                        {
                            return false;
                        }

                        //Check if block collides with another block already on board
                        if (Map.ReadMap(y, x) != 0)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

    }
}
