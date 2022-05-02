using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace Lion2D
{
    ///<summary>An overlay text block.</summary>
    public class TextBlock : IDrawable
    {
        private string text;
        private Font font;
        private Brush brush;
        private Point location;

        ///<value type="string">Get or set the text.</value>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        ///<value type="Font">Get or set the font.</value>
        public Font Font
        {
            get { return font; }
            set { font = value; }
        }

        ///<value type="Brush">Get or set the brush.</value>
        public Brush Brush
        {
            get { return brush; }
            set { brush = value; }
        }

        ///<value type="Point">Get or set the location.</value>
        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        ///<summary>Setup a new text block.</summary>
        ///<param name="text" type="string">The text.</param>
        ///<param name="font" type="Font">The font.</param>
        ///<param name="brush" type="Brush">The brush.</param>
        public TextBlock(string text, Font font, Brush brush) :
            this(text, font, brush, 0, 0) { }

        ///<summary>Setup a new text block.</summary>
        ///<param name="text" type="string">The text.</param>
        ///<param name="font" type="Font">The font.</param>
        ///<param name="brush" type="Brush">The brush.</param>
        ///<param name="x" type="int">The x-coordinate.</param>
        ///<param name="y" type="int">The y-coordinate.</param>
        public TextBlock(string text, Font font, Brush brush, int x, int y)
        {
            this.Text = text;
            this.Font = font;
            this.Brush = brush;
            this.Location = new Point(x, y);
        }

        ///<summary>Draw this text block.</summary>
        public void Draw(Graphics g)
        {
            g.DrawString(this.Text, this.Font, this.Brush, this.Location);
        }
    }
}
