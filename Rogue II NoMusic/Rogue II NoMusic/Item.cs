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
    enum Type { Melee, Ranged, Helmet, Pants, Gold, Chestplate, Consumable, Collectible }
    class Item
    {
        Random r = new Random();
        Canvas canvas;
        Window window;

        public Rectangle rectangle;
        public Type type;
        public Point pos;
        public int StrBoost;
        public int RangedDmg;
        public int ArmourBoost;
        public int HealthBoost;
        public int GoldCount;
        public bool isVisible = false;
        public bool VisibleOverride = false;
        public Item(Canvas c, Window w, Point p, Type t, int level)
        {
            canvas = c;
            window = w;
            pos = p;
            type = t;

            rectangle = new Rectangle();
            rectangle.Height = 30;
            rectangle.Width = 30;
            Canvas.SetLeft(rectangle, pos.X);
            Canvas.SetTop(rectangle, pos.Y);
            switch (type)
            {
                case Type.Melee:
                    StrBoost = r.Next(level, (level + 1) ^ 2 + 1);
                    rectangle.Fill = new ImageBrush(new BitmapImage(new Uri("sword.png", UriKind.Relative)));
                    break;
                case Type.Ranged:
                    RangedDmg = r.Next(level, level * 3 + 1);
                    rectangle.Fill = new ImageBrush(new BitmapImage(new Uri("blaster.png", UriKind.Relative)));
                    break;
                case Type.Helmet:
                    ArmourBoost = r.Next(level, level * 2 + 1);
                    rectangle.Fill = new ImageBrush(new BitmapImage(new Uri("helmet.png", UriKind.Relative)));
                    break;
                case Type.Pants:
                    ArmourBoost = r.Next(level, level * 2 + 1);
                    rectangle.Fill = new ImageBrush(new BitmapImage(new Uri("pants.png", UriKind.Relative)));
                    break;
                case Type.Chestplate:
                    ArmourBoost = r.Next(level, level * 2 + 1);
                    rectangle.Fill = new ImageBrush(new BitmapImage(new Uri("chest.png", UriKind.Relative)));
                    break;
                case Type.Consumable:
                    HealthBoost = r.Next(level, level * 5 + 1);
                    rectangle.Fill = new ImageBrush(new BitmapImage(new Uri("deathSticks.png", UriKind.Relative)));
                    break;
                case Type.Gold:
                    GoldCount = r.Next(level * 2, level ^ 3 + 1);
                    rectangle.Fill = new ImageBrush(new BitmapImage(new Uri("brick.png", UriKind.Relative)));
                    break;
                case Type.Collectible:
                    rectangle.Fill = new ImageBrush(new BitmapImage(new Uri("coll.png", UriKind.Relative)));
                    break;
                default:
                    break;
            }
            canvas.Children.Add(rectangle);
        }
        public void ItemVisibility(Player player)
        {
            rectangle.Visibility = Visibility.Hidden;
            Rect play = RectangleToRect(player.rectangle);
            Rect itemr = RectangleToRect(rectangle);
            if (play.IntersectsWith(itemr))
            {
                if (VisibleOverride == false)
                {
                    rectangle.Visibility = Visibility.Visible;
                }
            }
            /*if (((player.pos.X < pos.X && player.pos.X + 100 > pos.X) || (player.pos.X > pos.X && player.pos.X - 100 < pos.X))&&((player.pos.Y < pos.Y && player.pos.Y + 100 > pos.Y) || (player.pos.Y > pos.Y && player.pos.Y - 100 < pos.Y)))
            {
                if(VisibleOverride == false)
                {
                    rectangle.Visibility = Visibility.Visible;
                }
            }*/
        }
        public Rect RectangleToRect(Rectangle rectangle)
        {
            double xpos = Canvas.GetLeft(rectangle);
            double ypos = Canvas.GetTop(rectangle);
            Point Pos = new Point();
            Pos.X = xpos - 15;
            Pos.Y = ypos - 15;
            return new Rect() { Width = rectangle.Width + 30, Height = rectangle.Height + 30, Location = Pos };
        }

    }
}
