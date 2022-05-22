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
        public static List<Obstacle> treesBorders = new List<Obstacle>();


        public Vector2 Position { get { return position; } }

        public int Radius { get { return radius; } }

        public Obstacle(Vector2 newPos)
        {
            position = newPos;
        }



        public static bool didCollide(Vector2 playerPos)
        {
            Rectangle personRectangle =
                new Rectangle((int)playerPos.X, (int)playerPos.Y + 16,
                16, 16);
            foreach (Obstacle o in Obstacle.treesBorders)
            {
                Rectangle blockRectangle =
                    new Rectangle((int)o.Position.X + 16, (int)o.Position.Y + 16,
                    32, 32);

                if (personRectangle.Intersects(blockRectangle))
                {
                    
                    return true;
                }

            }
            return false;
        }

        public static void SpawnObstacles()
        {
            Obstacle.obstacles.Add(new Tree(new Vector2(350 + 320, 200 + 320)));

            Obstacle.obstacles.Add(new Tree2(new Vector2(384 - 40 + 320, 160 - 135 + 320)));
            Obstacle.obstacles.Add(new Tree2(new Vector2(864 - 40 + 320, 352 - 135 + 320)));
            Obstacle.obstacles.Add(new Tree2(new Vector2(800 - 40 + 320, 544 - 135 + 320)));
            Obstacle.obstacles.Add(new Tree2(new Vector2(128 - 40 + 320, 576 - 135 + 320)));

            Obstacle.obstacles.Add(new Tree(new Vector2(64 - 40 + 320, 160 - 135 + 320)));
            Obstacle.obstacles.Add(new Tree(new Vector2(896 - 40 + 320, 160 - 135 + 320)));
            Obstacle.obstacles.Add(new Tree(new Vector2(704 - 40 + 320, 512 - 135 + 320)));
            Obstacle.obstacles.Add(new Tree(new Vector2(256 - 40 + 320, 544 - 135 + 320)));

            Obstacle.treesBorders.Add(new Border(new Vector2(736, 320)));
            Obstacle.treesBorders.Add(new Border(new Vector2(736, 352)));
            Obstacle.treesBorders.Add(new Border(new Vector2(736, 384)));
            Obstacle.treesBorders.Add(new Border(new Vector2(768, 384)));
            Obstacle.treesBorders.Add(new Border(new Vector2(864, 384)));
            Obstacle.treesBorders.Add(new Border(new Vector2(896, 384)));
            Obstacle.treesBorders.Add(new Border(new Vector2(896, 352)));
            Obstacle.treesBorders.Add(new Border(new Vector2(896, 320)));

        }

        public static void SpawnLevel2Obstacles()
        {
            Obstacle.treesBorders.Add(new Border(new Vector2(416, 608)));
            Obstacle.treesBorders.Add(new Border(new Vector2(416, 640)));
            Obstacle.treesBorders.Add(new Border(new Vector2(416, 672)));
            Obstacle.treesBorders.Add(new Border(new Vector2(416, 704)));
            Obstacle.treesBorders.Add(new Border(new Vector2(416, 736)));

            Obstacle.treesBorders.Add(new Border(new Vector2(448, 768)));
            Obstacle.treesBorders.Add(new Border(new Vector2(480, 768)));

            Obstacle.treesBorders.Add(new Border(new Vector2(16 * 32, 25 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(17 * 32, 25 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(18 * 32, 25 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(18 * 32, 25 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(19 * 32, 25 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(20 * 32, 25 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(21 * 32, 25 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(22 * 32, 25 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(23 * 32, 25 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(24 * 32, 25 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(25 * 32, 25 * 32)));

            Obstacle.treesBorders.Add(new Border(new Vector2(26 * 32, 26 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(27 * 32, 26 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(28 * 32, 26 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(29 * 32, 26 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(30 * 32, 26 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(31 * 32, 26 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(32 * 32, 26 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(33 * 32, 26 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(34 * 32, 26 * 32)));

            Obstacle.treesBorders.Add(new Border(new Vector2(35 * 32, 25 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(36 * 32, 24 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(36 * 32, 23 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(37 * 32, 22 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(36 * 32, 21 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(35 * 32, 21 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(35 * 32, 20 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(35 * 32, 19 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(36 * 32, 18 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(37 * 32, 17 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(38 * 32, 16 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(38 * 32, 15 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(37 * 32, 14 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(36 * 32, 13 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(35 * 32, 13 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(34 * 32, 13 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(33 * 32, 12 * 32)));

            Obstacle.treesBorders.Add(new Border(new Vector2(32 * 32, 11 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(31 * 32, 11 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(30 * 32, 11 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(29 * 32, 12 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(28 * 32, 12 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(27 * 32, 13 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(26 * 32, 14 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(25 * 32, 14 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(24 * 32, 14 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(23 * 32, 14 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(22 * 32, 15 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(21 * 32, 15 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(20 * 32, 14 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(19 * 32, 14 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(18 * 32, 15 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(17 * 32, 16 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(16 * 32, 16 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(15 * 32, 17 * 32)));
            Obstacle.treesBorders.Add(new Border(new Vector2(14 * 32, 18 * 32)));

        }
    }

    class Border : Obstacle
    {
        public Border(Vector2 newPos) : base(newPos)
        {
            radius = 15;
        }
    }

    class Tree : Obstacle
    {
        public Tree(Vector2 newPos) : base(newPos)
        {

        }
    }

    class Tree2 : Obstacle
    {
        public Tree2(Vector2 newPos) : base(newPos)
        {

        }
    }

    class Nothing : Obstacle
    {
        public Nothing(Vector2 newPos) : base(newPos)
        {

        }
    }
}