using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace Lion2D
{
    ///<summary>Collision handler delegate.</summary>
    ///<param name="sp1" type="Sprite">The first sprite.</param>
    ///<param name="sp2" type="Sprite">The second sprite.</param>
    public delegate void CollisionHandler(Sprite sp1, Sprite sp2);

    ///<summary>A sprite.</summary>
    ///<remarks>Sprite objects represent game characters/items. Each sprite has a collection of
    ///static and/or animated poses.</remarks>
    public class Sprite
    {
        private Rectangle rectangle;
        private Hashtable poses;
        private string pose = String.Empty;

        ///<value type="Point">Get or set the location.</value>
        public Point Location
        {
            get { return rectangle.Location; }
            set { rectangle.Location = value; }
        }

        ///<value type="Size">Get or set the size.</value>
        public Size Size
        {
            get { return rectangle.Size; }
            set { rectangle.Size = value; }
        }

        ///<value type="Rectangle">Get the rectangular area.</value>
        public Rectangle Rectangle
        {
            get { return rectangle; }
        }

        ///<value type="Poses">Get the collection of poses.</value>
        public Hashtable Poses
        {
            get { return poses; }
        }

        ///<value type="string">Get or set the name of the current pose.</value>
        public string Pose
        {
            get { return pose; }
            set { pose = value; }
        }

        ///<summary>Setup a new instance of the Sprite class.</summary>
        public Sprite() : this(0, 0, 0, 0) { }

        ///<summary>Setup a new sprite.</summary>
        ///<param name="width" type="int">The width of the sprite.</param>
        ///<param name="height" type="int">The height of the sprite.</param>
        public Sprite(int width, int height) : this(0, 0, width, height) { }

        ///<summary>Setup a new sprite.</summary>
        ///<param name="x" type="int">The x-coordinate.</param>
        ///<param name="y" type="int">The y-coordinate.</param>
        ///<param name="width" type="int">The width.</param>
        ///<param name="height" type="int">The height.</param>
        public Sprite(int x, int y, int width, int height)
        {
            this.rectangle = new Rectangle(x, y, width, height);
            this.poses = new Hashtable();
        }

        ///<summary>Move a sprite.</summary>
        ///<param name="x" type="int">Amount to move horizontally.</param>
        ///<param name="y" type="int">Amount to move vertically.</param>
        public void Move(int x, int y)
        {
            rectangle.Offset(x, y);
        }

        ///<summary>Test if a sprite is inside another sprite.</summary>
        ///<param name="sp" type="Sprite">The other sprite.</param>
        ///<returns type="bool">True or false.</returns>
        public bool IsInside(Sprite sp)
        {
            if(sp == this)
                return false;

            return sp.Rectangle.Contains(Rectangle);
        }

        ///<summary>Test if a sprite is overlapping another sprite.</summary>
        ///<param name="sp" type="Sprite">The other sprite.</param>
        ///<returns type="bool">True or false.</returns>
        public bool IsOverlapping(Sprite sp)
        {
            if(sp == this)
                return false;

            return sp.Rectangle.IntersectsWith(Rectangle);
        }

        internal void Draw(Graphics g)
        {
            //Draw this sprite
            if(Poses[Pose] != null)
                ((IDrawablePose)Poses[Pose]).Draw(g, Rectangle);
        }
    }
}
