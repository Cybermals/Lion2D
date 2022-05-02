using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Lion2D;


namespace TestLion2D
{
    public class Ball : Sprite
    {
        public int velocityX = 5;
        public int velocityY = 5;

        public Ball(IDrawablePose pose)
        {
            this.Size = new Size(32, 32);
            this.Poses.Add("normal", pose);
            this.Pose = "normal";
        }

        public void Bounce()
        {
            Point newLocation = new Point(this.Location.X + velocityX,
                this.Location.Y + velocityY);

            if(newLocation.X < 0 | newLocation.X + this.Size.Width > 512)
                velocityX = -velocityX;

            if(newLocation.Y < 0 | newLocation.Y + this.Size.Height > 384)
                velocityY = -velocityY;

            this.Move(velocityX, velocityY);
        }

        public void ChangeDirection(Ball sp)
        {
            if((velocityX + sp.velocityX) / 2 == 0)
                velocityY = -velocityY;
            else
                velocityX = -velocityX;

            this.Move(velocityX, velocityY);
        }
    }
}
