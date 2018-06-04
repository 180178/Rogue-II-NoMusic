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
        public Point pos = new Point(300,300);
        public Rectangle rectangle;
        bool enemyhasdied = false;

        //Player constructor
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
            Canvas.SetLeft(rectangle, pos.X);
            Canvas.SetTop(rectangle, pos.Y);
            canvas.Children.Add(rectangle);
            rectangle.Visibility = Visibility.Visible;
        }
        //Alows player to move
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
        //Picks up item
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
                        MaxHP += 2;
                        break;
                    case Type.Gold:
                        GoldCount += item.GoldCount;
                        break;
                    case Type.Collectible:
                        GoldCount += item.GoldCount;
                        break;
                    default:
                        break;
                }
            }
            Armour = 4;
            if (Inventory[meleeSlot] != null&&Inventory[rangedSlot]!=null)
            {
                MaxStrength = 16 + Inventory[meleeSlot].StrBoost+Inventory[rangedSlot].StrBoost;
            }
            else if (Inventory[rangedSlot] != null)
            {
                MaxStrength = 16+ Inventory[rangedSlot].StrBoost;
            }
            else if (Inventory[meleeSlot] != null)
            {
                MaxStrength = 16+Inventory[meleeSlot].StrBoost;
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
        }
        //Handles all combat
        public void melee(Enemy enemy, Label lp, Label le)
        {
            Console.WriteLine("Enemy " + enemy.enemyPos.ToString());
            Console.WriteLine("Player " + pos.ToString());
            Point[] points = new Point[4];
            Point left = new Point(pos.X - rectangle.Width, pos.Y);
            points[0] = left;
            Point right = new Point(pos.X + (rectangle.Width ), pos.Y);
            points[1] = right;
            Point up = new Point(pos.X, pos.Y - rectangle.Height);
            points[2] = up;
            Point down = new Point(pos.X, pos.Y + (rectangle.Height ));
            points[3] = down;

            bool combat = false;
            if (Alive == true)
            {


                for (int i = 0; i < 4; i++)
                {
                    if (enemy.enemyPos == points[i]&&enemy.alive==true)
                    {
                        combat = true;
                        if (r.Next(0, Level + 1) < Level)
                        {
                            int dmg = Strength - enemy.armour;
                            enemy.hp -= dmg;
                            lp.Content = "You Hit";
                        }
                        else
                        {
                            lp.Content = "You Miss";
                        }
                        if (r.Next(0, enemy.level + 1) < enemy.level)
                        {
                            le.Content = enemy.enemyType + " Hit";
                            int dmg = enemy.strength - Armour;
                            HP -= dmg;
                        }
                        else
                        {
                            le.Content = enemy.enemyType + " Miss";
                        }

                    }
                    if (enemy.bossPos == points[i] && enemy.bossalive == true)
                    {
                        combat = true;
                        if (r.Next(0, Level + 1) < Level)
                        {
                            int dmg = Strength - enemy.bossArmour;
                            enemy.bossHP -= dmg;
                            XP += enemy.bosslevel;
                            lp.Content = "You Hit";
                        }
                        else
                        {
                            lp.Content = "You Miss";
                        }
                        if (r.Next(0, enemy.bosslevel + 1) < enemy.bosslevel)
                        {
                            le.Content = "Mini Boss" + " Hit";
                            int dmg = enemy.bossStrength - Armour;
                            HP -= dmg;
                        }
                        else
                        {
                            le.Content = "Mini Boss" + " Miss";
                        }
                    }
                }
                if (combat == false)
                {
                    le.Content = "";
                    lp.Content = "";
                }
                Console.WriteLine(combat.ToString());
                if (enemy.hp <= 0 && enemyhasdied == false)
                {
                    enemyhasdied = true;
                    le.Content = enemy.enemyType.ToString() + " Defeated";
                    enemy.death();
                    XP += enemy.level;
                }
                if(enemy.bossHP<=0)
                {
                    enemy.bossdeath();
                }
            }

        }
        //Decided to remove this feature, left the method here for maybe later use
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
        //Increases level based on XP
        public void XPUpdate()
        {
            int previousXP = XP;
            if (XP >= (Level * Level * Level * Level))
            {
                XP = previousXP - Level * Level * Level * Level;
                Level += 1;
            }
        }
        //Checks if player is dead
        public void death(Label l, Gameover gameover,Map m)
        {
            if (HP <= 0)
            {
                int maplevel = m.mapNum+1;
                Alive = false;
                l.Content = "Player Defeated";
                rectangle.Visibility = Visibility.Hidden;
                //gameover.gameoverScreen.Visibility = Visibility.Visible;
                //Label lblGameoverText = new Label();
                //lblGameoverText.Content = "Game Over.";
                //Label lblPlayAgain = new Label();
                //lblPlayAgain.Content = "Press 1 to Play Again.";
                gameover.endgame();
                gameover.endgamescore(this, maplevel);
            }
        }
        //Prevents enemy and player being on the same tile
        public void enemydont(Enemy e)
        {
            if(e.enemyPos == pos)
            {
                e.enemyPos = e.previousPos;
            }
        }

    }
}