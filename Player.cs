using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCustomGame
{
    public class Player
    {
        Game _game;
        public Game Game 
        { 
            get { return _game; } 
            set { _game = value; }
        }
        public Player() { }
    }
}
