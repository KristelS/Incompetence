using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Incompetence
{
    class Item
    {
        public static bool isPickedUp = false;

        protected Vector2 position;
        protected int radius;

        public static List<Item> items = new List<Item>();


        public Vector2 Position { get { return position; } }

        public int Radius { get { return radius; } }
        public bool IsPickedUp { get { return isPickedUp; } set { } }

        public Item(Vector2 newPos)
        {
            position = newPos;
        }



        public static bool didCollideItem(Vector2 playerPos)
        {
            Rectangle personRectangle =
                new Rectangle((int)playerPos.X, (int)playerPos.Y,
                32, 32);
            foreach (Item item in Item.items)
            {
                Rectangle blockRectangle =
                    new Rectangle((int)item.Position.X + 16, (int)item.Position.Y + 16,
                    32, 32);

                if (personRectangle.Intersects(blockRectangle))
                {
                    item.IsPickedUp = true;
                    return true;
                }

            }
            return false;
        }

        public static void SpawnItems()
        {
            

        }
    }


    class PotionRed : Item
    {
        public PotionRed(Vector2 newPos) : base(newPos)
        {
            radius = 1;
        }
    }

    class PotionBlue : Item
    {
        public PotionBlue(Vector2 newPos) : base(newPos)
        {
            radius = 2;
        }
    }

    class PotionGreen : Item
    {
        public PotionGreen(Vector2 newPos) : base(newPos)
        {
            radius = 3;
        }
    }

    class CraftItems : Item
    {
        public CraftItems(Vector2 newPos) : base(newPos)
        {
            radius = 50;
        }
    }

}
