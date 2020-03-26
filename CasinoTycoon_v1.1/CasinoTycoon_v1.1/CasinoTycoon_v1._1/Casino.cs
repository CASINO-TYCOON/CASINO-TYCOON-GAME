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
        Texture2D[,] casinoMap;
        Rectangle[,] casinoMapRect;
        Texture2D floor;
        Texture2D slots;
        public enum Direction { up, down, left, right};
        public Casino(Texture2D f, Texture2D s)
        {
            casinoMap = new Texture2D[10,20];
            casinoMapRect = new Rectangle[10, 20];
            floor = f;
            slots = s;
            loadCasino();
        }

        public void loadCasino()
        {
            ReadFile(@"Content/Casino/casinoLayout.txt");
            //int x = 0, y = 0;
            //for(int i = 0; i < casinoMapRect.GetLength(0); i++)
            //{
            //    y += 20;
            //    for(int j = 0; j < casinoMapRect.GetLength(1); j++)
            //    {
            //        casinoMapRect[i, j] = new Rectangle(x, y, 20, 20);
            //        x += 20;
            //    }
            //}
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
                        for(int i = 0; i < casinoMapRect.GetLength(0); i++)
                        {
                            char[] line = reader.ReadLine().ToCharArray();
                            for(int j = 0; j < line.Length; j++)
                            {
                                char curr = line[j];
                                if (curr.Equals('.'))
                                    casinoMap[i, j] = floor;
                                if (curr.Equals('*'))
                                    casinoMap[i, j] = slots;

                                casinoMapRect[i,j] = new Rectangle(x, y, 100, 100);
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
            if(direction == Direction.right && casinoMapRect[0,casinoMapRect.GetLength(1)-1].X > 700)
            {
                for(int i = 0; i < casinoMapRect.GetLength(0); i++)
                {
                    for(int j = 0; j < casinoMapRect.GetLength(1); j++)
                    {
                        casinoMapRect[i, j].X -= 5;
                    }
                }
            }

            
            if (direction == Direction.left && casinoMapRect[0, 0].X < 0)
            {
                for (int i = 0; i < casinoMapRect.GetLength(0); i++)
                {
                    for (int j = 0; j < casinoMapRect.GetLength(1); j++)
                    {
                        casinoMapRect[i, j].X += 5;
                    }
                }
            }
            
            if (direction == Direction.up && casinoMapRect[0, 0].Y < 0)
            {
                for (int i = 0; i < casinoMapRect.GetLength(0); i++)
                {
                    for (int j = 0; j < casinoMapRect.GetLength(1); j++)
                    {
                        casinoMapRect[i, j].Y += 5;
                    }
                }
            }
            Console.WriteLine(casinoMapRect[casinoMapRect.GetLength(0) - 1, 0].Y);
            if (direction == Direction.down && casinoMapRect[casinoMapRect.GetLength(0)-1,0].Y > 500)
            {
                for (int i = 0; i < casinoMapRect.GetLength(0); i++)
                {
                    for (int j = 0; j < casinoMapRect.GetLength(1); j++)
                    {
                        casinoMapRect[i, j].Y -= 5;
                    }
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            for(int i = 0; i < casinoMap.GetLength(0); i++)
            {
                for(int j = 0; j < casinoMap.GetLength(1); j++)
                {
                    Texture2D currText = casinoMap[i,j];
                    Rectangle currRect = casinoMapRect[i,j];
                    sb.Draw(currText, currRect, Color.White);
                }
            }
            sb.End();
            
        }
    }
}
