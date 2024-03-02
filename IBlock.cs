using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCustomGame
{
    public class IBlock : Block
    {
        public IBlock(int x, int y, int position) : base(Color.RGBColor(0, 240, 240), x, y, new int[,] { { 7, 7, 7, 7 } }, position)
        { }
    }
}
