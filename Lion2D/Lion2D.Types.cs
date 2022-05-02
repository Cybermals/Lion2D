using System.Runtime.InteropServices;


namespace Lion2D
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DeviceMode
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string deviceName;
        public ushort specVersion;
        public ushort driverVersion;
        public ushort size;
        public ushort driverExtra;
        public DevMode fields;
        public PointL position;
        public uint orientation;
        public uint fixedOutput;
        public short color;
        public short duplex;
        public short yResolution;
        public short ttOption;
        public short collate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string formName;
        public ushort logPixels;
        public uint bitsPerPel;
        public uint pelsWidth;
        public uint pelsHeight;
        public uint displayFlags;
        public uint displayFrequency;

        //Windows ME
        public uint cmMethod;
        public uint cmIntent;
        public uint mediaType;
        public uint ditherType;
        public uint reserved;
        public uint reserved2;

        //Windows NT
        public uint panningWidth;
        public uint panningHeight;
    }


    [StructLayout(LayoutKind.Sequential)]
    internal struct PointL
    {
        public int x;
        public int y;
    }
}