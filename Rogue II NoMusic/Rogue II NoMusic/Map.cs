using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Rogue_II_NoMusic
{
    class Map
    {
        //Class variables.
        Canvas canvas;
        Window window = new Window();
        List<Rectangle> rectangle = new List<Rectangle>();
        int mapNum;
        enum maplist { Hoth }
        string line;
        string tile;
        public int xPos;
        public int yPos = 0;
        public string[,] grid = new string[40, 20];
        Brush wallcolour;
        int lineNumber = -1;

        public Map(Canvas C)
        {
            canvas = C;
            if(mapNum == 0)
            {
                wallcolour = Brushes.CornflowerBlue;
            }
        }

        //A method to generate a given map from the read file.
        public void generateMap(int mapLevel)
        {
            mapNum = mapLevel;
            mapNum = 0;

            //Generates a grid from the text file that can be read by other classes and used to generate the map.
            StreamReader streamReader = new System.IO.StreamReader("map" + mapNum + ".txt");
            while (!streamReader.EndOfStream)
            {
                lineNumber++;
                line = streamReader.ReadLine();
                for (int i = 0; i < line.Length; i++)
                {
                    tile = line[i].ToString();
                    grid[i, lineNumber] = tile;
                }
            }

            //Takes the values from the grid array and creates rectangles (which represent the map) on the screen.
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 40; x++)
                {
                    xPos = (x * 30);
                    yPos = (y * 30);
                    rectangle.Add(new Rectangle());
                    rectangle[rectangle.Count - 1].Width = 30;
                    rectangle[rectangle.Count - 1].Height = 30;
                    rectangle[rectangle.Count - 1].StrokeThickness = 1;
                    Canvas.SetLeft(rectangle[rectangle.Count - 1], xPos);
                    Canvas.SetTop(rectangle[rectangle.Count - 1], yPos);

                    //Outside the room.
                    if (grid[x, y] == ".")
                    {
                        rectangle[rectangle.Count - 1].Fill = Brushes.Black;
                    }

                    //Walls.
                    if (grid[x, y] == "|")
                    {
                        rectangle[rectangle.Count - 1].Fill = wallcolour;
                    }

                    //Inside the room.
                    if (grid[x, y] == ",")
                    {
                        rectangle[rectangle.Count - 1].Fill = Brushes.Transparent;
                    }
                    canvas.Children.Add(rectangle[rectangle.Count - 1]);
                }
            }
        }
        public void mapCollide(Player p)
        {
            for (int i = 0; i < rectangle.Count; i++)
            {
                Point point = new Point(Canvas.GetLeft(rectangle[i]), Canvas.GetTop(rectangle[i]));
                if (point == p.pos && rectangle[i].Fill == wallcolour)
                {
                    p.pos = p.previouspos;
                    Canvas.SetLeft(p.rectangle, p.pos.X);
                    Canvas.SetTop(p.rectangle, p.pos.Y);
                }
            }
        }
        public void mapCollide(Enemy e)
        {
            for (int i = 0; i < rectangle.Count; i++)
            {
                Point point = new Point(Canvas.GetLeft(rectangle[i]), Canvas.GetTop(rectangle[i]));
                if (point == e.enemyPos && rectangle[i].Fill == wallcolour)
                {
                    e.enemyPos = e.previousPos;
                    Canvas.SetLeft(e.enemyRectangle, e.enemyPos.X);
                    Canvas.SetTop(e.enemyRectangle, e.enemyPos.Y);
                }
            }
        }
        public Rect RectangleToRect(Rectangle rectangle)
        {
            double xpos = Canvas.GetLeft(rectangle);
            double ypos = Canvas.GetTop(rectangle);
            Point Pos = new Point();
            Pos.X = xpos;
            Pos.Y = ypos;
            return new Rect() { Width = rectangle.Width, Height = rectangle.Height, Location = Pos };
        }
    }
}