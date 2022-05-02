using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace Lion2D
{
    ///<summary>An overlay image block.</summary>
    public class ImageBlock : IDrawable
    {
        private Image image;
        private Point location;

        ///<value type="Image">Get or set the image.</value>
        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        ///<value type="Point">Get or set the location.</value>
        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        ///<value type="Size">Get the size of the image.</value>
        public Size Size
        {
            get { return image.Size; }
        }

        ///<summary>Setup a new image block.</summary>
        ///<param name="image" type="Image">The image.</param>
        public ImageBlock(Image image) : this(image, 0, 0) { }

        ///<summary>Setup a new image block.</summary>
        ///<param name="image" type="Image">The image.</param>
        ///<param name="x" type="int">The x-coordinate.</param>
        ///<param name="y" type="int">The y-coordinate.</param>
        public ImageBlock(Image image, int x, int y)
        {
            this.Image = image;
            this.Location = new Point(x, y);
        }

        ///<summary>Draw this image block.</summary>
        public void Draw(Graphics g)
        {
            g.DrawImage(this.Image, this.Location);
        }
    }
}
