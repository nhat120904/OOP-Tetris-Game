using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace TetrisCustomGame
{
    public class TBlock: Block
    {
        public TBlock(int x, int y, int position) : base(Color.RGBColor(160, 0, 240), x, y, new int[,] { { 0, 2, 0 }, { 2, 2, 2 } }, position) 
        { }
    }
}
