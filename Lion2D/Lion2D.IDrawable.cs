using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace Lion2D
{
    ///<summary>Drawable object interface.</summary>
    internal interface IDrawable
    {
        void Draw(Graphics g);
    }
}
