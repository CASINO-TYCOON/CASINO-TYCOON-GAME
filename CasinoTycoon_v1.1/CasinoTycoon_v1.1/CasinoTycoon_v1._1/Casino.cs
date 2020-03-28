using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace CasinoTycoon_v1._1
{
    class Casino
    {
        Tile[,] casinoMap;
        //Rectangle[,] casinoMapRect;
        Texture2D floor;
        Texture2D slots;
        Texture2D door;
        public enum Direction { up, down, left, right};
        public Casino(Texture2D f, Texture2D s, Texture2D d)
        {
            casinoMap = new Tile[10,20];
            //casinoMapRect = new Rectangle[10, 20];
            floor = f;
            slots = s;
            door = d;
            loadCasino();
        }

        public void loadCasino()
        {
            ReadFile(@"Content/Casino/casinoLayout.txt");
        }
        public void ReadFile(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        int x = 0, y = 0;
                        for(int i = 0; i < 10; i++)
                        {
                            char[] line = reader.ReadLine().ToCharArray();
                            for(int j = 0; j < line.Length; j++)
                            {
                                char curr = line[j];
                                Rectangle rectangle = new Rectangle(x, y, 100, 100);
                                if (curr.Equals('.'))
                                {
                                    //casinoMap[i, j] = floor;
                                    casinoMap[i, j] = new Tile(floor, rectangle, false); ;
                                }
                                    
                                if (curr.Equals('s'))
                                {
                                    //casinoMap[i, j] = slots;
                                    casinoMap[i, j] = new Tile(slots, rectangle, true);
                                }

                                if(curr.Equals('d'))
                                {
                                    casinoMap[i, j] = new Tile(door, rectangle, true);
                                }
                                x += 100;
                            }
                            x = 0;
                            y += 100;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public void move(Direction direction)
        {
            if(direction == Direction.right && casinoMap[0,casinoMap.GetLength(1)-1].rect.X > 700)
            {
                for(int i = 0; i < casinoMap.GetLength(0); i++)
                {
                    for(int j = 0; j < casinoMap.GetLength(1); j++)
                    {
                        casinoMap[i, j].rect.X -= 5;
                    }
                }
            }

            
            if (direction == Direction.left && casinoMap[0, 0].rect.X < 0)
            {
                for (int i = 0; i < casinoMap.GetLength(0); i++)
                {
                    for (int j = 0; j < casinoMap.GetLength(1); j++)
                    {
                        casinoMap[i, j].rect.X += 5;
                    }
                }
            }
            
            if (direction == Direction.up && casinoMap[0, 0].rect.Y < 0)
            {
                for (int i = 0; i < casinoMap.GetLength(0); i++)
                {
                    for (int j = 0; j < casinoMap.GetLength(1); j++)
                    {
                        casinoMap[i, j].rect.Y += 5;
                    }
                }
            }
            //Console.WriteLine(casinoMapRect[casinoMapRect.GetLength(0) - 1, 0].Y);
            if (direction == Direction.down && casinoMap[casinoMap.GetLength(0)-1,0].rect.Y > 500)
            {
                for (int i = 0; i < casinoMap.GetLength(0); i++)
                {
                    for (int j = 0; j < casinoMap.GetLength(1); j++)
                    {
                        casinoMap[i, j].rect.Y -= 5;
                    }
                }
            }
        }

        public Boolean collision(Rectangle playerRect)
        {
            Boolean collision = false;
            for(int i = 0; i < casinoMap.GetLength(0); i++)
            {
                for(int j = 0; j < casinoMap.GetLength(1); j++)
                {
                    Rectangle currRect = casinoMap[i, j].rect;
                    if(playerRect.Intersects(currRect) && casinoMap[i,j].isObstacle)
                    {
                        collision = true;
                    }
                }
            }
            return collision;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            for(int i = 0; i < casinoMap.GetLength(0); i++)
            {
                for(int j = 0; j < casinoMap.GetLength(1); j++)
                {
                    Texture2D currText = casinoMap[i,j].texture;
                    Rectangle currRect = casinoMap[i,j].rect;
                    sb.Draw(currText, currRect, Color.White);
                }
            }
            sb.End();
            
        }
    }
}
