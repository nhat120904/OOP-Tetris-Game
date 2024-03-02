using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCustomGame
{
    public class TetrisMap : ICanDraw
    {
        int[,] map;
        int _rows, _cols;
        int _position;
        int _score;
        SoundEffect sound_effect = SplashKit.LoadSoundEffect("clear", "song/se.mp3");

        public int Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        public int Columns
        {
            get { return _cols; }
            set { _cols = value; }
        }

        public int[,] Map
        {
            get { return map; }
        }

        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public TetrisMap(int rows, int columns, int position)
        {
            _rows = rows;
            _cols = columns;
            map = new int[rows, columns];
            Position = position;
        }

        public int Score
        {
            get { return _score; }
        }

        public int ReadMap(int rows, int columns)
        {
            return map[rows, columns];
        }

        public void UpdateMap(int rows, int columns, int value)
        {
            map[rows, columns] = value;
        }
       
        public void Initialize()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = 0;
                }
            }
        }
        public bool CheckRowFull(int row)
        {
            for (int i = 0; i < _cols; i++)
            {
                if (map[row, i] == 0)
                    return false;
            }
            return true;
        }

        public bool CheckGameOver()
        {
            for (int i = 0; i< _cols; i++)
            {
                if (map[0, i] != 0)
                    return true;
            }
            return false;
        }
        public void ClearRow(int row)
        {
            for (int i = 0; i < _cols; i++)
            {
                map[row, i] = 0;
            }
            sound_effect.Play();
        }

        public void MoveRows(int startRow)
        {
            for (int i = startRow; i > 0; i--)
            {
                for (int j = 0;j < _cols; j++)
                {
                    map[i, j] = map[i - 1, j];
                }
            }
        }
        
        public void CheckAllRows()
        {
            for (int  row = 0; row < _rows; row++)
            {
               if (CheckRowFull(row))
                {
                    ClearRow(row);
                    MoveRows(row);
                    _score += 1;
                }
            }
        }
        public Color OutlineColor(Color color)
        {
            float newR = color.R * 1 / 2;
            float newG = color.G * 1 / 2;
            float newB = color.B * 1 / 2;
            return SplashKit.RGBColor(newR, newG, newB);
        }

        public void Draw()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (Map[i, j] != 0)
                    {
                        int x = i * 40;
                        int y = Position + j * 40;
                        switch (Map[i, j])
                        {
                            case 1:
                                SplashKit.FillRectangle(OutlineColor(Color.RGBColor(240, 0, 0)), y, x, 40, 40);
                                SplashKit.FillRectangle(Color.RGBColor(240, 0, 0), y + 4, x + 4, 32, 32);
                                break;
                            case 2:
                                SplashKit.FillRectangle(OutlineColor(Color.RGBColor(160, 0, 240)), y, x, 40, 40);
                                SplashKit.FillRectangle(Color.RGBColor(160, 0, 240), y + 4, x + 4, 32, 32);
                                break;
                            case 3:
                                SplashKit.FillRectangle(OutlineColor(Color.RGBColor(0, 240, 0)), y, x, 40, 40);
                                SplashKit.FillRectangle(Color.RGBColor(0, 240, 0), y + 4, x + 4, 32, 32);
                                break;
                            case 4:
                                SplashKit.FillRectangle(OutlineColor(Color.RGBColor(0, 0, 240)), y, x, 40, 40);
                                SplashKit.FillRectangle(Color.RGBColor(0, 0, 240), y + 4, x + 4, 32, 32);
                                break;
                            case 5:
                                SplashKit.FillRectangle(OutlineColor(Color.RGBColor(240, 240, 0)), y, x, 40, 40);
                                SplashKit.FillRectangle(Color.RGBColor(240, 240, 0), y + 4, x + 4, 32, 32);
                                break;
                            case 6:
                                SplashKit.FillRectangle(OutlineColor(Color.RGBColor(240, 160, 0)), y, x, 40, 40);
                                SplashKit.FillRectangle(Color.RGBColor(240, 160, 0), y + 4, x + 4, 32, 32);
                                break;
                            case 7:
                                SplashKit.FillRectangle(OutlineColor(Color.RGBColor(0, 240, 240)), y, x, 40, 40);
                                SplashKit.FillRectangle(Color.RGBColor(0, 240, 240), y + 4, x + 4, 32, 32);
                                break;
                        }
                    }
                }
            }
        }

    }

}
