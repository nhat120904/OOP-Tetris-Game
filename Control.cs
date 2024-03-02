using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCustomGame
{
    public class Control
    {
        public Control() { }
        KeyCode moveLeft, moveRight, moveDown, blockFall, rotateLeft, rotateRight, changeBackground;

        public Control(KeyCode moveLeft, KeyCode moveRight, KeyCode moveDown, KeyCode blockFall, KeyCode rotateLeft, KeyCode rotateRight, KeyCode changeBackground)
        {
            MoveLeft = moveLeft;
            MoveRight = moveRight;
            MoveDown = moveDown;
            BlockFall = blockFall;
            RotateLeft = rotateLeft;
            RotateRight = rotateRight;
            ChangeBackground = changeBackground;
        }

        public KeyCode MoveLeft
        {
            get { return moveLeft; }
            set { moveLeft = value; }
        }
        public KeyCode MoveRight
        { get { return moveRight; } set {  moveRight = value; } }
        public KeyCode MoveDown
        { get { return moveDown; } set { moveDown = value; } }
        public KeyCode BlockFall
        { get { return blockFall; } set {  blockFall = value; } }
        public KeyCode RotateLeft
        { get { return rotateLeft; } set {  rotateLeft = value; } }
        public KeyCode RotateRight
        { get { return rotateRight; } set { rotateRight = value; } }
        public KeyCode ChangeBackground
        { get { return changeBackground; } set {  changeBackground = value; } }

    }
}
