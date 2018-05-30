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
    enum GameState { StartScreen, GameOn, BossLevel, GameOver };
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameState gamestate = GameState.StartScreen;
        //string[] songList = { "04 Emperor's Throne Room.mp3", "03 Battle Of The Heroes.mp3", "02 The Millennium Falcon_Imperial Cruiser Pursuit.mp3", "10 Mos Eisley Spaceport.mp3", "11 Cantina Band.mp3", "02 Duel Of The Fates.mp3", "12 Cantina Band #2.mp3" };
        Enemy enemy;
        Player player;
        Gameover gameover;
        int Level = 1;
        Map map;
        //All for the Intro - Ignore
        Random r = new Random();
        Point ackbarPos = new Point(555, 800);
        Point TextPos = new Point(200, 929);
        Rectangle ackbar;
        MediaPlayer music = new MediaPlayer();
        DispatcherTimer timer = new DispatcherTimer();
        Label scrollingText = new Label();
        int delay = 0;
        double width = 90;
        double height = 129;
        double textheight = 400;
        double textwidth = 800;
        int otherCounter = 0;
        bool IntroHasRun = false;
        int keycounter = 0;
        //Intro Variables Over

        //UI Elements
        Label level;
        Label gold;
        Label strength;
        Label exp;
        Label hp;
        Label armour;
        Rectangle controls;
        //End of UI Elements

        Item[] items = new Item[10];
        public MainWindow()
        {
            InitializeComponent();
            if (IntroHasRun == false)
            {
                PlayIntro();
            }
            gameover = new Gameover(canvas, this);
            controls = new Rectangle();
            controls.Visibility = Visibility.Hidden;
            controls.Height = 800;
            controls.Width = 1200;
            controls.Fill = new ImageBrush(new BitmapImage(new Uri("controls.png", UriKind.Relative)));
            canvas.Children.Add(controls);
        }
        //Initializes and Runs the Intro- Ends with the song or the escape key
        private void PlayIntro()
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100 / 60);
            timer.Tick += tick;
            timer.Start();
            if (IntroHasRun == false)
            {
                IntroHasRun = true;
                music.MediaEnded += IntroOver;
                ackbar = new Rectangle();
                ackbar.Height = height;
                ackbar.Width = width;
                ackbar.Fill = new ImageBrush(new BitmapImage(new Uri("Untitledbutackbarbutwhite.png", UriKind.Relative)));
                scrollingText.Height = 400;
                scrollingText.Width = 800;
                scrollingText.HorizontalContentAlignment = HorizontalAlignment.Center;
                scrollingText.VerticalContentAlignment = VerticalAlignment.Top;
                scrollingText.Content = "Rogue II" + "\r\n" + "A Game";
                scrollingText.FontSize = 42;
                scrollingText.FontFamily = new FontFamily("Times New Roman");
                scrollingText.Foreground = Brushes.Yellow;
                scrollingText.Background = Brushes.Black;
                Canvas.SetLeft(scrollingText, TextPos.X);
                Canvas.SetTop(scrollingText, TextPos.Y);
                canvas.Children.Add(scrollingText);
                canvas.Children.Add(ackbar);
                Canvas.SetLeft(ackbar, ackbarPos.X);
                Canvas.SetTop(ackbar, ackbarPos.Y);
                //music.Open(new Uri("01 Star Wars Main Title And Ambush On Coruscant.mp3", UriKind.Relative));
                //music.Play();
            }
        }
        //Runs intro at 60fps
        private void tick(object sender, EventArgs e)
        {
            if (gamestate == GameState.StartScreen)
            {
                delay++;
                if (delay >= 3)
                {
                    delay = 0;
                    TextPos.Y -= 3;
                    ackbarPos.Y -= 3;
                    otherCounter++;
                    if (height > 3 && width > 3 && otherCounter == 6)
                    {
                        height -= 0.5 * 129 / 90;
                        width -= 0.5;
                        otherCounter = 0;
                        if (scrollingText.FontSize >= 0.50)
                        {
                            scrollingText.FontSize -= 0.25;
                        }
                        TextPos.Y -= 1;
                        ackbarPos.X += 0.25;
                    }
                    scrollingText.Width = textwidth;
                    scrollingText.Height = textheight;
                    ackbar.Width = width;
                    ackbar.Height = height;
                    Canvas.SetLeft(scrollingText, TextPos.X);
                    Canvas.SetTop(scrollingText, TextPos.Y);
                    Canvas.SetTop(ackbar, ackbarPos.Y);
                    Canvas.SetLeft(ackbar, ackbarPos.X);
                    if (Keyboard.IsKeyDown(Key.Escape))
                    {
                        //music.Stop();
                        //NextSong();
                        //music.MediaEnded += NextSongEvent;
                        gamestate = GameState.GameOn;
                        scrollingText.Visibility = Visibility.Hidden;
                        ackbar.Visibility = Visibility.Hidden;
                        GenerateItems();
                        player = new Player(canvas, this);
                        player.rectangle.Visibility = Visibility.Visible;
                        generateScreen();
                        map = new Map(canvas);
                        map.generateMap(1);
                        enemy = new Enemy(canvas, this);
                    }
                }
            }

        }
        //Is Called when intro song ends
        private void IntroOver(object sender, EventArgs e)
        {
            //music.Stop();
            //NextSong();
            //music.MediaEnded += NextSongEvent;
            gamestate = GameState.GameOn;
            scrollingText.Visibility = Visibility.Hidden;
            ackbar.Visibility = Visibility.Hidden;
            player.rectangle.Visibility = Visibility.Visible;
        }
        /*private void NextSong()
        {
            music.Stop();
            int id = r.Next(0, songList.Length);
            string songLink = songList[id];
            window.Title = songLink;
            music.Open(new Uri((songLink), UriKind.Relative));
            music.Play();
        }
        private void NextSongEvent(object sender, EventArgs e)
        {
            music.Stop();
            int random = r.Next(0, songList.Length);
            string songLink = songList[random];
            window.Title = songLink;
            music.Open(new Uri((songLink), UriKind.Relative));
            music.Play();
        }*/

        //Everything happens on a KeyDown Event
        private void key(object sender, KeyEventArgs e)
        {
            keycounter++;
            if(keycounter==25)
            {
                keycounter = 0;
                if(player.HP<player.MaxHP)
                {
                    player.HP += 1;
                }
                
            }
            if (e.Key == Key.F1)
            {
                if (controls.Visibility == Visibility.Hidden)
                {
                    canvas.Children.Remove(controls);
                    canvas.Children.Add(controls);
                    controls.Visibility = Visibility.Visible;
                }
                else
                {
                    controls.Visibility = Visibility.Hidden;
                }

            }
            else if (e.Key == Key.P)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    Item it = items[i];
                    if (it.VisibleOverride == false)
                    {
                        it.rectangle.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                if (gamestate == GameState.GameOn)
                {
                    if (e.Key == Key.R)
                    {
                        //Rest
                        if (player.Strength != player.MaxStrength)
                        {
                            player.Strength += 1;
                        }
                        player.XP += 1;
                    }

                    for (int i = 0; i < items.Length; i++)
                    {
                        Item it = items[i];
                        player.itemPickUp(it);
                        it.ItemVisibility(player);
                    }
                    if (e.Key == Key.Enter)
                    {
                        //NextSong();
                    }
                    //Console.WriteLine("KeyDown Event" + e.Key.ToString());
                    player.move(e.Key);
                    map.mapCollide(player);
                    enemy.enemyMove(player);
                    player.enemydont(enemy);
                    map.mapCollide(enemy);
                    player.melee(enemy, lblCombat, lblCombatEnemy);
                    player.XPUpdate();
                    player.death(lblCombat, gameover);
                    screenUpdate();

                }
            }
        }
        private void GenerateItems()
        {
            for (int i = 0; i < items.Length; i++)
            {
                Type type = (Type)r.Next(0, 7);
                if (i == 1)
                {
                    type = Type.Melee;
                }
                else if (i == 0)
                {
                    type = Type.Collectible;
                }
                Point itempos = new Point(r.Next(0, 30) * 30, r.Next(0, 20) * 30);
                items[i] = new Item(canvas, this, itempos, type, Level);
            }
        }
        private void generateScreen()
        {
            level = new Label();
            level.FontFamily = new FontFamily("Times New Roman");
            level.Width = 120;
            level.Height = 40;
            Canvas.SetLeft(level, 210);
            Canvas.SetTop(level, 600);
            level.Content = "Level:         " + Level;
            level.Foreground = Brushes.White;
            level.FontSize = 18;
            canvas.Children.Add(level);

            gold = new Label();
            gold.FontFamily = new FontFamily("Times New Roman");
            gold.Width = 120;
            gold.Height = 40;
            Canvas.SetLeft(gold, 330);
            Canvas.SetTop(gold, 600);
            gold.Content = "Gold:       " + player.GoldCount;
            canvas.Children.Add(gold);
            gold.FontSize = 18;
            gold.Foreground = Brushes.White;

            hp = new Label();
            hp.FontFamily = new FontFamily("Times New Roman");
            hp.Width = 120;
            hp.Height = 40;
            Canvas.SetLeft(hp, 450);
            Canvas.SetTop(hp, 600);
            hp.Content = "HP:  " + player.HP + "(  " + player.MaxHP + ")";
            canvas.Children.Add(hp);
            hp.FontSize = 18;
            hp.Foreground = Brushes.White;

            strength = new Label();
            strength.FontFamily = new FontFamily("Times New Roman");
            strength.Width = 120;
            strength.Height = 40;
            Canvas.SetLeft(strength, 570);
            Canvas.SetTop(strength, 600);
            strength.Content = "Str:  " + player.Strength + "(  " + player.MaxStrength + ")";
            canvas.Children.Add(strength);
            strength.FontSize = 18;
            strength.Foreground = Brushes.White;

            armour = new Label();
            armour.FontFamily = new FontFamily("Times New Roman");
            armour.Width = 120;
            armour.Height = 40;
            Canvas.SetLeft(armour, 690);
            Canvas.SetTop(armour, 600);
            armour.Content = "Arm:    " + player.Armour;
            canvas.Children.Add(armour);
            armour.FontSize = 18;
            armour.Foreground = Brushes.White;

            exp = new Label();
            exp.FontFamily = new FontFamily("Times New Roman");
            exp.Width = 120;
            exp.Height = 40;
            Canvas.SetLeft(exp, 810);
            Canvas.SetTop(exp, 600);
            exp.Content = "Exp: " + player.Level + "/      " + player.XP;
            canvas.Children.Add(exp);
            exp.FontSize = 18;
            exp.Foreground = Brushes.White;
        }
        private void screenUpdate()
        {
            level.Content = "Level:         " + Level;
            armour.Content = "Arm:    " + player.Armour;
            exp.Content = "Exp: " + player.Level + "/      " + player.XP;
            strength.Content = "Str:  " + player.Strength + "(  " + player.MaxStrength + ")";
            hp.Content = "HP:  " + player.HP + "(  " + player.MaxHP + ")";
            gold.Content = "Gold:       " + player.GoldCount;
        }
    }
}
