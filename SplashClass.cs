using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Incompetence
{
    class SplashClass
    {

        protected Vector2 position;
        protected int radius;

        public static List<SplashClass> splashes = new List<SplashClass>();


        public Vector2 Position { get { return position; } }

        public int Radius { get { return radius; } }

        public SplashClass(Vector2 newPos)
        {
            position = newPos;
        }

        public static void SpawnItems()
        {


        }
    }


    class SplashScreen : SplashClass
    {
        public SplashScreen(Vector2 newPos) : base(newPos)
        {
            radius = 1;
        }
    }

    class SplashHelp: SplashClass
    {
        public SplashHelp(Vector2 newPos) : base(newPos)
        {
            radius = 2;
        }
    }

}
