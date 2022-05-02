using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace Lion2D
{
    ///<summary>Drawable pose interface.</summary>
    public interface IDrawablePose
    {
        ///<summary>Draw this pose.</summary>
        void Draw(Graphics g, Rectangle rect);
    }
}
