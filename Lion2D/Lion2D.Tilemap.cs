using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace Lion2D
{
    ///<summary>A tile map object.</summary>
    public class TileMap : IDrawable
    {
        private Size tileSize = new Size(16, 16);
        private Rectangle viewport = new Rectangle(0, 0, 512, 384);
        private Image[] tiles;
        private byte[,] data;

        ///<value type="Size">The size of each tile in this tile map (in pixels).</value>
        public Size TileSize
        {
            get { return tileSize; }
            set { tileSize = value; }
        }

        ///<value type="Rectangle">The viewport of this tile map (in pixels).</value>
        public Rectangle Viewport
        {
            get { return viewport; }
            set { viewport = value; }
        }

        ///<value type="Point">The location of the current viewport.</value>
        public Point Location
        {
            get { return viewport.Location; }
            set { viewport.Location = value; }
        }

        ///<value type="Size">The size of the current viewport.</value>
        public Size Size
        {
            get { return viewport.Size; }
            set { viewport.Size = value; }
        }

        ///<value type="Image[]">An array of images to use as tiles.</value>
        public Image[] Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }

        ///<value type="byte[,]">A 2D array of map data.</value>
        public byte[,] Data
        {
            get { return data; }
            set { data = value; }
        }

        ///<summary>Setup a new tile map.</summary>
        ///<param name="tiles" type="Image[]">An array of equally sized images to use as
        ///tiles.</param>
        ///<param name="data" type="byte[][]">An array of map data.</param>
        public TileMap(Image[] tiles, byte[,] data)
        {
            Tiles = tiles;
            Data = data;
        }

        ///<summary>Draw this tile map.</summary>
        public void Draw(Graphics g)
        {
            int startX = Viewport.X / TileSize.Width;
            int startY = Viewport.Y / TileSize.Height;
            int endX = startX + Viewport.Width / TileSize.Width;
            int endY = startY + Viewport.Height / TileSize.Height;

            for (int y = startY; y < Data.GetLength(0); y++)
            {
                if(y > endY)
                    break;

                for (int x = startX; x < Data.GetLength(1); x++)
                {
                    if(x > endX)
                        break;

                    if(Data[y, x] > 0)
                        g.DrawImage(Tiles[Data[y, x] - 1], x * TileSize.Width - Viewport.X,
                            y * TileSize.Height - Viewport.Y);
                }
            }
        }
    }
}
