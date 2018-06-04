using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Rogue_II_NoMusic
{
    class Gameover
    {
        Canvas canvas;
        public Rectangle gameoverScreen = new Rectangle();
        public Button restart = new Button();
        public Button quit = new Button();
        public Label gameover = new Label();
        public Label score = new Label();
        public Rectangle deathstar = new Rectangle();
        bool hasrun = false;
        public Gameover(Canvas c, MainWindow w)
        {
            canvas = c;
            gameoverScreen.Height = 800;
            gameoverScreen.Width = 1200;
            gameoverScreen.Fill = Brushes.Black;
            gameoverScreen.Visibility = Visibility.Visible;
            
            restart.Width = 200;
            restart.Height = 75;
            restart.Content = "Restart Game";
            restart.FontSize = 24;
            restart.Click += restartclick;
            Canvas.SetLeft(restart, 200);
            Canvas.SetTop(restart, 500);

            quit.Width = 200;
            quit.Height = 75;
            quit.Content = "Quit Game";
            quit.FontSize = 24;
            quit.Click += quitclick;
            Canvas.SetLeft(quit, 800);
            Canvas.SetTop(quit, 500);

            gameover.Width = 400;
            gameover.Height = 200;
            gameover.FontSize = 32;
            gameover.Foreground = Brushes.Yellow;
            gameover.Content = "Game Over";
            gameover.HorizontalContentAlignment = HorizontalAlignment.Center;
            gameover.VerticalContentAlignment = VerticalAlignment.Center;
            gameover.FontFamily = new FontFamily("Comic Sans MS");
            Canvas.SetLeft(gameover,400);
            Canvas.SetTop(gameover, 350);

            score.Width = 1200;
            score.Height = 50;
            score.HorizontalContentAlignment = HorizontalAlignment.Center;
            score.VerticalContentAlignment = VerticalAlignment.Center;
            score.FontSize = 20;
            score.Content = "";
            score.Foreground = Brushes.Yellow;
            Canvas.SetTop(score, 650);

            deathstar.Height = 300;
            deathstar.Width = 300;
            deathstar.Fill = new ImageBrush(new BitmapImage(new Uri("deathstar.png",UriKind.Relative)));
            Canvas.SetLeft(deathstar, 450);
            Canvas.SetTop(deathstar, 50);


        }
        //Displays gameover screen
        public void endgame()
        {
            if (hasrun == false)
            {
                hasrun = true;
                canvas.Children.Add(gameoverScreen);
                canvas.Children.Add(deathstar);
                canvas.Children.Add(score);
                canvas.Children.Add(gameover);
                canvas.Children.Add(quit);
                canvas.Children.Add(restart);
            }
        }
        //Checks and outputs player score
        public void endgamescore(Player p,int mapnum)
        {
            score.Content = "You Reached Map " + mapnum.ToString() + " With " + p.GoldCount + " Gold And  Level " + p.Level;
        }
        //Allows game restart on button click
        public void restartclick(object sender,EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        //Quits game on other button click
        public void quitclick(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}