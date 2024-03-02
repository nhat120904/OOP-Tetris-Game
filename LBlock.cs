using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCustomGame
{
    public class LBlock : Block
    {
        public LBlock(int x, int y, int position) : base(Color.RGBColor(240, 160, 0), x, y, new int[,] { { 0, 0 ,6 }, { 6, 6, 6 } }, position)
        { }
    }
}
