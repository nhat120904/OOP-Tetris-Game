using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace TetrisCustomGame
{
    public class ZBlock : Block
    {
        public ZBlock(int x, int y, int position) : base(Color.RGBColor(240, 0, 0), x, y, new int[,] { { 1, 1, 0 }, { 0, 1, 1 } }, position)
        { }
    }
}
