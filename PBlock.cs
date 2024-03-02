using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace TetrisCustomGame
{
    public class PBlock : Block
    {
        public PBlock(int x, int y, int position) : base(Color.RGBColor(0, 0, 240), x, y, new int[,] { { 4, 0, 0 }, { 4, 4, 4 } }, position)
        { }
    }
}
