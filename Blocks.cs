using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCustomGame
{
    public class Block : ICanDraw
    {
        Color _color;
        int _x, _y;
        protected int[,] _layout;
        int _position = 0;
        int _positionX = 400;


        public int X
        { get { return _x; } }
        public int Y
        { get { return _y; } }

        public Block(Color color, int x, int y, int[,] layout, int position)
        {
            _x = x;
            _y = y;
            _layout = layout;
            _color = color;
            _positionX = position;
        }

        public int Height
        { get { return _layout.GetLength(0); } }
        public int Width
        { get { return _layout.GetLength(1); } }

        public int[,] Layout
        { get { return _layout; } }

        public void Draw()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (_layout[i, j] != 0)
                    {
                        int x = _positionX + (_x + j) * 40;
                        int y = _position + i * 40;
                        SplashKit.FillRectangle(OutlineColor(_color), x, y, 40, 40);
                        SplashKit.FillRectangle(_color, x+4, y+4, 32, 32);
                    }
                }
            }
        }

        public void DrawInQueue(int order)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (_layout[i, j] != 0)
                    {
                        int x = _positionX + j * 40 - 180;
                        int y = 90 + order * 100 + i * 40;
                        SplashKit.FillRectangle(OutlineColor(_color), x, y, 40, 40);
                        SplashKit.FillRectangle(_color, x + 4, y + 4, 32, 32);
                    }
                }
            }
        }

        public void MoveDown()
        {
            _y += 1;
            _position += 40;
        }
        public void MoveUp()
        { 
            _y -= 1;
        }
        public void MoveLeft() 
        {
            if (_x > 0)
            {
                _x -= 1;
            }
        }
        public void MoveRight() 
        {
            _x += 1;
        }

        public void RotateLeft()
        {
            int[,] layout2 = RotateBlockCounterClockwise(_layout);
            _layout = layout2;
        }
        public void RotateRight()
        {
            int[,] layout2 = RotateBlockClockwise(_layout);
            _layout = layout2;
        }

        public int[,] RotateBlockCounterClockwise(int[,] oldBlock)
        {
            int[,] newBlock = new int[oldBlock.GetLength(1), oldBlock.GetLength(0)];
            int newColumn, newRow = 0;
            for (int oldColumn = oldBlock.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                newColumn = 0;
                for (int oldRow = 0; oldRow < oldBlock.GetLength(0); oldRow++)
                {
                    newBlock[newRow, newColumn] = oldBlock[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            return newBlock;
        }

        public int[,] RotateBlockClockwise(int[,] oldBlock)
        {
            int[,] newBlock = new int[oldBlock.GetLength(1), oldBlock.GetLength(0)];
            int newColumn, newRow;
            newColumn = oldBlock.GetLength(0) - 1;
            for (int oldRow = 0; oldRow <= oldBlock.GetLength(0) - 1; oldRow++)
            {
                newRow = 0;
                for (int oldColumn = 0; oldColumn <= oldBlock.GetLength(1) - 1; oldColumn++)
                {
                    newBlock[newRow, newColumn] = oldBlock[oldRow, oldColumn];
                    newRow++;
                }
                newColumn--;
            }
            return newBlock;
        }

        public Color OutlineColor(Color color)
        {
            float newR = color.R * 1/2;
            float newG = color.G * 1/2;
            float newB = color.B * 1/2;
            return SplashKit.RGBColor(newR, newG, newB);
        }

        public Block Clone() 
        {
            return (Block)this.MemberwiseClone();
        }

    }
}
