using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace TetrisCustomGame
{
    public class OBlock : Block
    {
        public OBlock(int x, int y, int position) : base(Color.RGBColor(240, 240, 0), x, y, new int[,] { { 5, 5 }, { 5, 5 } }, position) 
        { }
    }
}
