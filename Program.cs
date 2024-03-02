using SplashKitSDK;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using TetrisCustomGame;
using static System.Reflection.Metadata.BlobBuilder;

namespace Tetris
{
    public class Program
    {
        static void Main(string[] args) 
        {
            Menu menu = new Menu();
            Control[] controls = new Control[] { new Control(KeyCode.LeftKey, KeyCode.RightKey, KeyCode.DownKey, KeyCode.SpaceKey, KeyCode.NKey, KeyCode.MKey, KeyCode.UpKey), new Control(KeyCode.AKey, KeyCode.DKey, KeyCode.SKey, KeyCode.FKey, KeyCode.QKey, KeyCode.EKey, KeyCode.TabKey) };
            menu.Draw();
            int player_count = menu.PlayerCount;
            Player[] player = new Player[menu.PlayerCount];
            Bitmap _background = new Bitmap("background", "./background/background.jpg");
            for (int i = 0; i < player_count; i++)
            {
                player[i] = new Player();
                player[i].Game = new Game(800 * (i + 1) - 400);
                player[i].Game.Prepare();
                player[i].Game.Control = controls[Math.Abs(player_count - (i + 1))];
            }
            Music song1 = SplashKit.LoadMusic("song1", "./song/theme.mp3");
            Window window = new Window("Tetris", 800 * player_count, 800);
            song1.Play();
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.RefreshScreen();
                SplashKit.DrawBitmap(_background, 0, 0);
                for (int i = 0; i < player_count; i++)
                {
                    player[i].Game.Run();
                }
                SplashKit.Delay(150);
            } while ((!SplashKit.WindowCloseRequested("Tetris")) && player_count != 0);
        }
    }
}
