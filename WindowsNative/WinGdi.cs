using System;

namespace WindowsNative
{
    public static class WinGdi
    {
        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            MERGECOPY = 0x00C000CA,
            MERGEPAINT = 0x00BB0226,
            PATCOPY = 0x00F00021,
            PATPAINT = 0x00FB0A09,
            PATINVERT = 0x005A0049,
            DSTINVERT = 0x00550009,
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062,
            CAPTUREBLT = 0x40000000 //only if WinVer >= 5.0.0 (see wingdi.h)
        }

        /// <summary>
        /// Return value for GetStretchBltMode function as currrent stretching mode
        /// </summary>
        [Flags]
        public enum StretchingMode
        {
            /// <summary>
            /// Performs a Boolean AND operation using the color values for the eliminated 
            /// and existing pixels. If the bitmap is a monochrome bitmap, this mode preserves
            /// black pixels at the expense of white pixels.
            /// </summary>
            BLACKONWHITE = 1,
            /// <summary>
            /// Deletes the pixels. This mode deletes all eliminated lines of pixels 
            /// without trying to preserve their information
            /// </summary>
            COLORONCOLOR = 3,
            /// <summary>
            /// Maps pixels from the source rectangle into blocks of pixels in the destination rectangle.
            /// The average color over the destination block of pixels approximates the color of the source pixels. 
            /// This option is not supported on Windows 95/98/Me
            /// </summary>
            HALFTONE = 4,
            /// <summary>
            /// Performs a Boolean AND operation using the color values for the eliminated 
            /// and existing pixels. If the bitmap is a monochrome bitmap, this mode preserves
            /// black pixels at the expense of white pixels (same as BLACKONWHITE)
            /// </summary>
            STRETCH_ANDSCANS = 1,
            /// <summary>
            /// Deletes the pixels. This mode deletes all eliminated lines of pixels 
            /// without trying to preserve their information (same as COLORONCOLOR)
            /// </summary>
            STRETCH_DELETESCANS = 3,
            /// <summary>
            /// Maps pixels from the source rectangle into blocks of pixels in the destination rectangle.
            /// The average color over the destination block of pixels approximates the color of the source pixels. 
            /// This option is not supported on Windows 95/98/Me (same as HALFTONE)
            /// </summary>
            STRETCH_HALFTONE = 4,
            /// <summary>
            /// Performs a Boolean OR operation using the color values for the eliminated and existing pixels.
            /// If the bitmap is a monochrome bitmap, this mode preserves white pixels at the expense of 
            /// black pixels(same as WHITEONBLACK)
            /// </summary>
            STRETCH_ORSCANS = 2,
            /// <summary>
            /// Performs a Boolean OR operation using the color values for the eliminated and existing pixels.
            /// If the bitmap is a monochrome bitmap, this mode preserves white pixels at the expense of black pixels.
            /// </summary>
            WHITEONBLACK = 2,
            /// <summary>
            /// Fail to stretch
            /// </summary>
            ERROR = 0
        }
    }
}
