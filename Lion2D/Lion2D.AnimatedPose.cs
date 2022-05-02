using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Lion2D
{
    ///<summary>An animated pose.</summary>
    public class AnimatedPose : IDrawablePose
    {
        private int currentFrame = 0;
        private Image[] frames;

        private Timer tmrAnimation;

        ///<value type="Size">Get the size of the first frame.</value>
        public Size Size
        {
            get { return frames[0].Size; }
        }

        ///<value type="Image[]">Get the array of frames.</value>
        public Image[] Frames
        {
            get { return frames; }
        }

        ///<value type="int">Get or set the animation speed.</value>
        public int Speed
        {
            get { return tmrAnimation.Interval; }
            set { tmrAnimation.Interval = value; }
        }

        ///<summary>Setup a new instance of the AnimatedPose class.</summary>
        ///<param name="frames" type="Image[]">The frames to use.</param>
        ///<param name="speed" type="int">The animation speed.</param>
        public AnimatedPose(Image[] frames, int speed)
        {
            Bitmap bmp;

            //Make sure all frames are bitmaps with transparency
            for (int i = 0; i < frames.Length; i++)
            {
                bmp = new Bitmap(frames[i]);
                bmp.MakeTransparent(Color.White);
                frames[i] = bmp;
            }

            this.frames = frames;

            //Setup animation timer
            tmrAnimation = new Timer();
            tmrAnimation.Interval = speed;
            tmrAnimation.Tick += new EventHandler(OnTick);
        }

        internal void OnTick(object sender, EventArgs e)
        {
            NextFrame();
        }

        internal void NextFrame()
        {
            if((++currentFrame) >= Frames.Length)
                currentFrame = 0;
        }

        ///<summary>Start animation.</summary>
        public void Start()
        {
            tmrAnimation.Start();
        }

        ///<summary>Stop animation.</summary>
        public void Stop()
        {
            tmrAnimation.Stop();
        }

        ///<summary>Draw an animated pose.</summary>
        public void Draw(Graphics g, Rectangle rect)
        {
            g.DrawImage(Frames[(int)currentFrame], rect);
        }
    }
}
