using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCustomGame
{
    public class Menu : ICanDraw
    {
        Bitmap _background = new Bitmap("bg", "./background/menu.jpg");
        int player_count;
        bool seleted = false;
        public Menu()
        {}
        public void Draw()
        {
            Window window = new Window("Menu", 800, 800);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.RefreshScreen();
                SplashKit.DrawBitmap(_background, 0, 0);
                SplashKit.DrawText("WELCOME TO", Color.White, SplashKit.LoadFont("font1", "font/PressStart2P-Regular.ttf"), 60, 113, 150);
                SplashKit.FillRectangle(Color.Blue, 115, 520, 160, 100);
                SplashKit.FillRectangle(Color.Blue, 525, 520, 160, 100);
                SplashKit.DrawText("Game mode", Color.White, SplashKit.LoadFont("font1", "font/PressStart2P-Regular.ttf"), 19, 318, 560);
                SplashKit.DrawText("1 player", Color.White, SplashKit.LoadFont("font1", "font/PressStart2P-Regular.ttf"), 20, 118, 560);
                SplashKit.DrawText("2 player", Color.White, SplashKit.LoadFont("font1", "font/PressStart2P-Regular.ttf"), 20, 527, 560);
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Point2D pt = SplashKit.MousePosition();
                    if ((pt.X > 115) && (pt.X < 115 + 160) && (pt.Y > 520) && (pt.Y < 520 + 100))
                    {
                        player_count = 1;
                        seleted = true;
                    }
                    else if ((pt.X > 525) && (pt.X < 525 + 160) && (pt.Y > 520) && (pt.Y < 520 + 100))
                    {
                        player_count = 2;
                        seleted = true;
                    }
                }
            } while (!SplashKit.WindowCloseRequested("Menu") && (seleted == false));
            if ((seleted == true) || SplashKit.WindowCloseRequested("Menu"))
            {
                SplashKit.CloseWindow("Menu");
            }
        }

        public int PlayerCount
        { get { return player_count; } }
    }
}
