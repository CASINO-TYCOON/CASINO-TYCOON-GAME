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

namespace CasinoTycoon_v1._1
{
    class Tile
    {
        public Texture2D texture;
        public Rectangle rect;
        public Boolean isObstacle;

        public Tile(Texture2D t, Rectangle r, Boolean o)
        {
            texture = t;
            rect = r;
            isObstacle = o;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, Color.White);
        }
    }
}
