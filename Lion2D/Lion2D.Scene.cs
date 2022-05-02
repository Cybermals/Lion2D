using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Lion2D
{
    ///<summary>Scene animation handler delegate.</summary>
    public delegate void SceneAnimationHandler(Scene scene);

    ///<summary>A high-level 2D scene.</summary>
    ///<remarks>The Scene object is a control that manages backgrounds, sprites, and an
    ///overlay of text and image blocks for a 2D scene.</remarks>
    public class Scene : UserControl
    {
        private Rectangle rectangle;
        private Bitmap backBuffer;
        private Graphics gBack;
        private Graphics gFront;
        private Timer tmrRender;

        private ArrayList backgrounds;
        private ArrayList sprites;
        private ArrayList overlay;

        ///<summary>Animate event.</summary>
        ///<remarks>This event fires just before the scene is redrawn.</remarks>
        public event SceneAnimationHandler Animate;

        ///<summary>Sprite collision event.</summary>
        public event CollisionHandler SpriteCollision;

        ///<value type="bool">Set the auto update state.</value>
        public bool AutoUpdate
        {
            set
            {
                if(value)
                    tmrRender.Start();
                else
                    tmrRender.Stop();
            }
        }

        ///<value type="ArrayList">Get the collection of backgrounds in the scene.</value>
        public ArrayList Backgrounds
        {
            get { return backgrounds; }
        }

        ///<value type="ArrayList">Get the collection of sprites in the scene.</value>
        public ArrayList Sprites
        {
            get { return sprites; }
        }

        ///<value type="ArrayList">Get the collection of overlay text and image blocks.</value>
        public ArrayList Overlay
        {
            get { return overlay; }
        }

        ///<summary>Setup a new instance of the Scene class.</summary>
        ///<param name="width" type="int">Width of the scene in pixels.</param>
        ///<param name="height" type="int">Height of the scene in pixels.</param>
        public Scene(int width, int height)
        {
            //Set initial size
            this.Size = new Size(width, height);
            rectangle = new Rectangle(0, 0, width, height);

            //Create back buffer and get graphics object for front buffer
            backBuffer = new Bitmap(width, height);
            gBack = Graphics.FromImage(backBuffer);
            gFront = this.CreateGraphics();

            //Set initial background color
            this.BackColor = Color.Black;

            //Create backgrounds, sprites, and hud blocks array lists
            backgrounds = new ArrayList();
            sprites = new ArrayList();
            overlay = new ArrayList();

            //Create render timer
            tmrRender = new Timer();
            tmrRender.Interval = 25;
            tmrRender.Tick += new EventHandler(OnRender);

            //Add event handlers
            this.Animate += new SceneAnimationHandler(OnAnimate);
            this.SpriteCollision += new CollisionHandler(OnCollide);
        }

        ///<summary>Paint event handler.</summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Render();
        }

        private void OnRender(object sender, EventArgs e)
        {
            Render();
        }

        ///<summary>Scene animation handler.</summary>
        public virtual void OnAnimate(Scene scene) { }

        ///<summary>Collision event handler</summary>
        ///<remarks>See CollisionHandler delegate.</remarks>
        public virtual void OnCollide(Sprite sp1, Sprite sp2) { }

        ///<summary>Render the current scene.</summary>
        public void Render()
        {
            //Fire animate event
            Animate(this);

            //Clear back buffer
            gBack.Clear(this.BackColor);

            //Render all backgrounds
            foreach (IDrawable bg in Backgrounds)
                bg.Draw(gBack);

            //Do collision check
            foreach (Sprite sp1 in Sprites)
                foreach (Sprite sp2 in Sprites)
                    if(sp1.IsOverlapping(sp2))
                        SpriteCollision(sp1, sp2);

            //Render all sprites
            foreach (Sprite sprite in Sprites)
                sprite.Draw(gBack);

            //Render all overlay blocks
            foreach (IDrawable block in Overlay)
                block.Draw(gBack);

            //Swap buffers
            gFront.DrawImage(backBuffer, 0, 0);
        }
    }
}
