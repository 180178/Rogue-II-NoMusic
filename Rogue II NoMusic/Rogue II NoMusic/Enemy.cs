using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rogue_II_NoMusic
{
    class Enemy
    {
        //Player player;
        Canvas canvas;
        Map map;
        int levelProgress = 1;
        Window window;
        //the point where the enemy is located
        public Point enemyPos = new Point(300, 300);
        //the enemyRectangle is where the sprite will be, and there will be collision with it
        public Rectangle enemyRectangle = new Rectangle();
        //the minibossRectangle is where the mini boss sprite will be.
        public Rectangle minibossRectangle = new Rectangle();
        public Point bossPos = new Point(x: 1, y: 1);
        //these ints are for enemy stats
        public int hp;
        public int bossHP;
        public int maxHP;
        public int bossMaxHP;
        public int strength;
        public int bossStrength;
        public int armour;
        public int bossArmour;
        public int bosslevel;
        ImageBrush stormtSprite = new ImageBrush(new BitmapImage(new Uri("StormtrooperForward.png", UriKind.Relative)));
        //higher level, more exp for player
        public int level;
        public string enemyType;
        public bool alive = true;

        Random random = new Random();
        public Enemy(Canvas c, Window w)
        {
            canvas = c;
            window = w;
            //System.IO.StreamReader streamReader = new System.IO.StreamReader("map" + levelProgress + ".txt");
            enemyRectangle = new Rectangle();
            minibossRectangle = new Rectangle();
            enemyRectangle.Height = 30;
            enemyRectangle.Width = 30;
            minibossRectangle.Height = 30;
            minibossRectangle.Width = 30;
            if (levelProgress == 1)
            {   //stormtrooper hoth
                enemyType = "Stormtrooper";
                enemyRectangle.Fill = stormtSprite;
                Canvas.SetLeft(enemyRectangle, 100);
                hp = 10;
                maxHP = 10;
                strength = 12;
                armour = 3;
                level = 1;
                canvas.Children.Add(enemyRectangle);
                //enemyRectangle.Visibility = Visibility.Hidden;
                // wampa?
                //minibossRectangle.Fill = new ImageBrush(new BitmapImage(new Uri("wampa.png", UriKind.Relative)));
                minibossRectangle.Fill = Brushes.Red;
                Canvas.SetLeft(minibossRectangle, 150);
                bossHP = 18;
                bossMaxHP = 18;
                bossStrength = 17;
                bossArmour = 1;
                bosslevel = 5;
                canvas.Children.Add(minibossRectangle);
                //minibossRectangle.Visibility = Visibility.Hidden;
            }
            if (levelProgress == 2)
            {
                // second generic enemy
                enemyRectangle.Fill = Brushes.Blue;
                Canvas.SetLeft(enemyRectangle, 100);
                hp = 22;
                maxHP = 22;
                strength = 22;
                armour = 6;
                level = 5;
                canvas.Children.Add(enemyRectangle);
                //enemyRectangle.Visibility = Visibility.Hidden;
                // second miniboss
                minibossRectangle.Fill = Brushes.Red;
                Canvas.SetLeft(minibossRectangle, 150);
                bossHP = 30;
                bossMaxHP = 30;
                bossStrength = 20;
                bossArmour = 11;
                level = 10;
                canvas.Children.Add(minibossRectangle);
                //minibossRectangle.Visibility = Visibility.Hidden;

            }
            if (levelProgress == 3)
            {
                //third generic enemy
                enemyRectangle.Fill = Brushes.Yellow;
                Canvas.SetLeft(enemyRectangle, 100);
                hp = 33;
                maxHP = 33;
                strength = 30;
                armour = 5;
                level = 8;
                canvas.Children.Add(enemyRectangle);
                //enemyRectangle.Visibility = Visibility.Hidden;
                //third miniboss
                minibossRectangle.Fill = Brushes.Red;
                Canvas.SetLeft(minibossRectangle, 150);
                bossHP = 44;
                maxHP = 44;
                bossStrength = 38;
                bossArmour = 17;

            }

        }

        public void enemyMove(Player player)
        {
             Point previousPos = enemyPos;
            //Player down and right from enemy
            if (player.pos.X >= enemyPos.X && player.pos.Y >= enemyPos.Y)
            {
                if (player.pos.X - enemyPos.X >= player.pos.Y - enemyPos.Y)
                {
                    if (player.pos.X <= enemyPos.X)
                    {
                        enemyPos.X -= 30;
                    }
                    else
                    {
                        enemyPos.X += 30;
                    }
                }
                else
                {
                    if (player.pos.Y <= enemyPos.Y)
                    {
                        enemyPos.Y -= 30;
                    }
                    else
                    {
                        enemyPos.Y += 30;
                    }
                }


                Canvas.SetLeft(enemyRectangle, enemyPos.X);
                Canvas.SetTop(enemyRectangle, enemyPos.Y);
            }
            //down and left
            if (player.pos.X <= enemyPos.X && player.pos.Y >= enemyPos.Y)
            {
                if (enemyPos.X - player.pos.X >= player.pos.Y - enemyPos.Y)
                {
                    if (player.pos.X <= enemyPos.X)
                    {
                        enemyPos.X -= 30;
                    }
                    else
                    {
                        enemyPos.X += 30;
                    }
                }
                else
                {
                    if (player.pos.Y <= enemyPos.Y)
                    {
                        enemyPos.Y -= 30;
                    }
                    else
                    {
                        enemyPos.Y += 30;
                    }
                }
                Canvas.SetLeft(enemyRectangle, enemyPos.X);
                Canvas.SetTop(enemyRectangle, enemyPos.Y);
            }
            // up and right 
            if (player.pos.X >= enemyPos.X && player.pos.Y <= enemyPos.Y)
            {
                if (enemyPos.X - player.pos.X >= enemyPos.Y - player.pos.Y)
                {
                    if (player.pos.X <= enemyPos.X)
                    {
                        enemyPos.X -= 30;
                    }
                    else
                    {
                        enemyPos.X += 30;
                    }
                }
                else
                {
                    if (player.pos.Y >= enemyPos.Y)
                    {
                        enemyPos.Y += 30;
                    }
                    else
                    {
                        enemyPos.Y -= 30;
                    }
                }
                Canvas.SetLeft(enemyRectangle, enemyPos.X);
                Canvas.SetTop(enemyRectangle, enemyPos.Y);
            }
            // up and left
            if (player.pos.X <= enemyPos.X && player.pos.Y <= enemyPos.Y)
            {

                if (enemyPos.X - player.pos.X >= enemyPos.Y - player.pos.Y)
                {
                    if (player.pos.X >= enemyPos.X)
                    {
                        enemyPos.X += 30;
                    }
                    else
                    {
                        enemyPos.X -= 30;
                    }
                }
                else
                {
                    if (player.pos.Y >= enemyPos.Y)
                    {
                        enemyPos.Y += 30;
                    }
                    else
                    {
                        enemyPos.Y -= 30;
                    }
                }
                Canvas.SetLeft(enemyRectangle, enemyPos.X);
                Canvas.SetTop(enemyRectangle, enemyPos.Y);
            }
            /*if(enemyPos == player.pos)
            {
                enemyPos = previousPos;
            }*/
        }

        public void death()
        {
            canvas.Children.Remove(enemyRectangle);
            //player.XP = player.XP + (level * 10);
        }
    }
}
