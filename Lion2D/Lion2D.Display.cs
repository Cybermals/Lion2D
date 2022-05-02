using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace Lion2D
{
    ///<summary>Represents the main monitor.</summary>
    public class Display
    {
        [DllImport("user32.dll")]
        private static extern bool EnumDisplaySettings(string deviceName, int modeNum,
            ref DeviceMode devMode);

        [DllImport("user32.dll")]
        private static extern long ChangeDisplaySettings(ref DeviceMode devMode,
            ChangeDispSettings flags);

        [DllImport("user32.dll")]
        private static extern long ChangeDisplaySettings(IntPtr devMode,
            ChangeDispSettings flags);

        private Display() { }

        ///<summary>Change the display mode to a given resolution and color depth.
        ///</summary>
        ///<param name="width" type="int">The requested width.</param>
        ///<param name="height" type="int">The requested height.</param>
        ///<returns type="bool">True if the display mode was successfully changed.</returns>
        public static bool ChangeMode(int width, int height)
        {
            //Find the requested display mode
            int mode = 0;
            DeviceMode dm = new DeviceMode();
            dm.size = (ushort)Marshal.SizeOf(dm);

            while(EnumDisplaySettings(null, mode, ref dm))
            {
#if DEBUG
                Console.WriteLine("Display Mode for {0}: {1} x {2}", dm.deviceName,
                    dm.pelsWidth, dm.pelsHeight);
#endif

                //Check resolution
                if(dm.pelsWidth == (uint)width && dm.pelsHeight == (uint)height)
                {
                    dm.fields = DevMode.PelsWidth | DevMode.PelsHeight;
                    ChangeDisplaySettings(ref dm, ChangeDispSettings.Fullscreen);
                    return true;
                }

                mode++;
            }

            return false;
        }

        ///<summary>Restore the display mode to its default.</summary>
        public static void RestoreMode()
        {
            ChangeDisplaySettings(IntPtr.Zero, 0);
        }
    }
}
