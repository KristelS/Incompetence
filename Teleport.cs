using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Incompetence
{
    class Teleport
    {
        protected Vector2 position;
        protected string whereTo;

        public static List<Teleport> teleports = new List<Teleport>();


        public Vector2 Position { get { return position; } }

        public string WhereTo { get { return whereTo; } }

        public Teleport(Vector2 newPos)
        {
            position = newPos;
        }



        public static bool didCollideTeleports(Vector2 playerPos)
        {
            Rectangle personRectangle =
                new Rectangle((int)playerPos.X, (int)playerPos.Y,
                32, 32);
            foreach (Teleport t in Teleport.teleports)
            {
                Rectangle blockRectangle =
                    new Rectangle((int)t.Position.X + 16, (int)t.Position.Y + 16,
                    32, 32);

                if (personRectangle.Intersects(blockRectangle))
                    return true;

            }
            return false;
        }

        public static void SpawnObstacles()
        {
            
        }
    }

    class TeleportTutBoss : Teleport
    {
        public TeleportTutBoss(Vector2 newPos) : base(newPos)
        {
            whereTo = "tutBoss";
        }
    }
}
