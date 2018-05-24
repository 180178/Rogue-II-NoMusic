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
    class Player
    {
        //Stats
        public int Level;
        public int MaxStrength = 16;
        public int Strength;
        public int MaxHP = 12;
        public int HP;
        public int Armour = 4;
        public int GoldCount;
        public int XP;
        public int Collectibles;
        public int RangedDmg;

        int rangedSlot = 0;
        int meleeSlot = 1;
        int helmetSlot = 2;
        int chestSlot = 3;
        int pantsSlot = 4;
        int conSlot = 5;
        Random r = new Random();
        Item[] Inventory = new Item[6];
        bool hasForce = false;
        bool Alive = true;
        //ImageBrush PixelArt = new ImageBrush(new BitmapImage(new Uri()));
        Canvas canvas;
        Window window;
        int counter = 0;
        public Point previouspos;
        public Point pos = new Point(100, 100);
        public Rectangle rectangle;
        public Player(Canvas c, Window w)
        {
            //Initialize stats
            Strength = MaxStrength;
            HP = MaxHP;
            GoldCount = 0;
            XP = 0;
            Level = 1;
            canvas = c;
            window = w;
            rectangle = new Rectangle();
            rectangle.Height = 30;
            rectangle.Width = 30;
            //rectangle.Fill = Brushes.White;
            rectangle.Fill = new ImageBrush(new BitmapImage(new Uri("@.png", UriKind.Relative)));
            canvas.Children.Add(rectangle);
            rectangle.Visibility = Visibility.Hidden;
        }
        public void move(Key key)
        {
            previouspos = pos;
            if (key == Key.Up)
            {
                pos.Y -= 30;
                counter++;
            }
            if (key == Key.Down)
            {
                pos.Y += 30;
                counter++;
            }
            if (key == Key.Left)
            {
                pos.X -= 30;
                counter++;
            }
            if (key == Key.Right)
            {
                pos.X += 30;
                counter++;
            }
            Canvas.SetLeft(rectangle, pos.X);
            Canvas.SetTop(rectangle, pos.Y);
        }
        //Change parameter to Map map
        public void reveal()
        {

        }
        //Changed to Item[] itemArray
        public void itemPickUp(Item item)
        {
            if (this.pos == item.pos && item.VisibleOverride == false)
            {
                item.VisibleOverride = true;
                switch (item.type)
                {
                    case Type.Melee:
                        Inventory[meleeSlot] = item;
                        break;
                    case Type.Ranged:
                        Inventory[rangedSlot] = item;
                        break;
                    case Type.Helmet:
                        Inventory[helmetSlot] = item;
                        break;
                    case Type.Chestplate:
                        Inventory[chestSlot] = item;
                        break;
                    case Type.Pants:
                        Inventory[pantsSlot] = item;
                        break;
                    case Type.Consumable:
                        Inventory[conSlot] = item;
                        break;
                    case Type.Gold:
                        GoldCount += item.GoldCount;
                        break;
                    default:
                        break;
                }
            }
            Armour = 4;
            if (Inventory[meleeSlot] != null)
            {
                MaxStrength = 16 + Inventory[meleeSlot].StrBoost;
            }
            if (Inventory[helmetSlot] != null)
            {
                Armour += Inventory[helmetSlot].ArmourBoost;
            }
            if (Inventory[chestSlot] != null)
            {
                Armour += Inventory[chestSlot].ArmourBoost;
            }
            if (Inventory[pantsSlot] != null)
            {
                Armour += Inventory[pantsSlot].ArmourBoost;
            }
            if (Inventory[rangedSlot] != null)
            {
                RangedDmg = Inventory[rangedSlot].RangedDmg;
            }
        }
        //Change to Enemy enemy
        public void melee(Enemy enemy, Label lp, Label le)
        {
            Point[] points = new Point[4];
            Point left = new Point(pos.X - rectangle.Width, pos.Y);
            Point right = new Point(pos.X + (rectangle.Width * 2), pos.Y);
            Point up = new Point(pos.X, pos.Y - rectangle.Height);
            Point down = new Point(pos.X, pos.Y + (rectangle.Height * 2));
            for (int i = 0; i < 4; i++)
            {
                if (r.Next(0, Level + 1) < Level)
                {
                    if (enemy.enemyPos == points[i])
                    {
                        int dmg = Strength - enemy.armour;
                        enemy.hp -= dmg;
                        XP += enemy.level;
                        lp.Content = "You Hit";
                    }
                    if (enemy.bossPos == points[i])
                    {
                        int dmg = Strength - enemy.bossArmour;
                        enemy.bossHP -= dmg;
                        XP += enemy.bosslevel;
                        lp.Content = "You Hit";
                    }
                }
                else
                {
                    if (enemy.bossPos == points[i])
                    {
                        lp.Content = "You Miss";
                    }
                }
                if (r.Next(0, enemy.level + 1) < enemy.level)
                {
                    if (enemy.enemyPos == points[i])
                    {
                        le.Content = enemy.enemyType + " Hit";
                    }
                }
                else
                {
                    if (enemy.enemyPos == points[i])
                    {
                        le.Content = enemy.enemyType + " Maybe Not";
                    }

                }
            }

        }
        //Change to Enemy[] enemyArray, change 8 to enemyArray.Length
        public void ranged(Point[] pArray)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) && Keyboard.IsKeyDown(Key.A))
            {
                for (int i = 0; i < pArray.Length; i++)
                {
                    Point location = pArray[i];
                    if (this.pos.X == location.X && this.pos.Y > location.Y && location.Y + 20 >= this.pos.Y)
                    {
                        //int dmg = Inventory[rangedSlot].dmg
                        //enemyArray[i].Health -=dmg;+
                    }
                }
            }
        }
        public void XPUpdate()
        {
            int previousXP = XP;
            if (XP == (Level * Level * Level * Level))
            {
                XP = previousXP - Level * Level * Level * Level;
                Level += 1;
            }
        }

    }
}