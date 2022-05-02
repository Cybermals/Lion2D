using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace Lion2D
{
    ///<summary>A static pose.</summary>
    public class Pose : IDrawablePose
    {
        private Image image;

        ///<value type="Size">Get the size of a pose.</value>
        public Size Size
        {
            get { return Image.Size; }
        }

        ///<value type="Image">Get or set the image for a pose.</value>
        public Image Image
        {
            get { return image; }
            set
            {
                Bitmap bmp = new Bitmap(value);
                bmp.MakeTransparent(Color.White);
                image = bmp;
            }
        }

        ///<summary>Setup a new instance of the Pose class.</summary>
        ///<param name="image" type="Image">The image to use for a pose.</param>
        public Pose(Image image)
        {
            this.Image = image;
        }

        ///<summary>Draw a pose.</summary>
        public void Draw(Graphics g, Rectangle rect)
        {
            g.DrawImage(Image, rect);
        }
    }
}
