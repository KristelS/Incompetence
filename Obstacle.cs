using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Incompetence
{
    class Obstacle
    {
        protected Vector2 position;
        protected int radius;

        public static List<Obstacle> obstacles = new List<Obstacle>();


        public Vector2 Position { get { return position; } }

        public int Radius { get { return radius; } }

        public Obstacle(Vector2 newPos)
        {
            position = newPos;
        }



        public static bool didCollide(Vector2 playerPos)
        {
            Rectangle personRectangle =
                new Rectangle((int)playerPos.X, (int)playerPos.Y,
                32, 32);
            foreach (Obstacle o in Obstacle.obstacles)
            {
                Rectangle blockRectangle =
                    new Rectangle((int)o.Position.X + 16, (int)o.Position.Y + 16,
                    32, 32);

                if (personRectangle.Intersects(blockRectangle))
                    return true;

            }
            return false;
        }

        public static void SpawnObstacles()
        {
            Obstacle.obstacles.Add(new Border(new Vector2(1, 68)));
            Obstacle.obstacles.Add(new Border(new Vector2(170, 68)));
            Obstacle.obstacles.Add(new Border(new Vector2(202, 68)));
            Obstacle.obstacles.Add(new Border(new Vector2(234, 68)));
            Obstacle.obstacles.Add(new Border(new Vector2(266, 68)));
            Obstacle.obstacles.Add(new Border(new Vector2(276, 68)));
            Obstacle.obstacles.Add(new Border(new Vector2(276, 100)));
            Obstacle.obstacles.Add(new Border(new Vector2(308, 100)));
            Obstacle.obstacles.Add(new Border(new Vector2(308, 132)));
            Obstacle.obstacles.Add(new Border(new Vector2(340, 132)));
            Obstacle.obstacles.Add(new Border(new Vector2(340, 164)));
            Obstacle.obstacles.Add(new Border(new Vector2(372, 164)));
        }
    }

    class Border : Obstacle
    {
        public Border(Vector2 newPos) : base(newPos)
        {
            radius = 15;
        }
    }
}