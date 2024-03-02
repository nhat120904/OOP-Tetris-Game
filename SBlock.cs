using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace TetrisCustomGame
{
    public class SBlock : Block
    {
        public SBlock(int x, int y, int position) : base(Color.RGBColor(0, 240, 0), x, y, new int[,] { { 0, 3, 3}, { 3, 3, 0} }, position)
        { }
    }
}
