using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Rogue_II_NoMusic
{
    class Gameover
    {
        public Rectangle gameoverScreen = new Rectangle();
        public Gameover(Canvas c, MainWindow w)
        {

            gameoverScreen.Height = 800;
            gameoverScreen.Width = 1200;
            gameoverScreen.Fill = Brushes.Black;
            gameoverScreen.Visibility = Visibility.Hidden;

        }
    }
}