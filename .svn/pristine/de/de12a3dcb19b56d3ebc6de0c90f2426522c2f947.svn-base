using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Winumeration.Api
{
    public static class WinUser
    {
        public delegate IntPtr WindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public delegate IntPtr CallWndProc(int nCode, UIntPtr wParam, CWPSTRUCT lParam);
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        public delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lParam);
        public delegate void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hWnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        public enum ShowWindow : int
        {
            SW_HIDE = 0,
            SW_MAXIMIZE = 3,
            SW_MINIMIZE = 6,
            SW_RESTORE = 9,
            SW_SHOW = 5,
            SW_SHOWDEFAULT = 10,
            SW_SHOWMAXIMIZED = 3,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOWNORMAL = 1
        }

        [Flags]
        public enum WindowsEventFlags : uint
        {
            WINEVENT_INCONTEXT = 4,
            WINEVENT_OUTOFCONTEXT = 0,
            WINEVENT_SKIPOWNPROCESS = 2,
            WINEVENT_SKIPOWNTHREAD = 1
        } 
        /// <summary>
        /// This enumeration lists known accessible event types.
        /// </summary>
        public enum Events : uint
        {
            /// <summary>
            ///  Sent when a sound is played.  Currently nothing is generating this, we
            ///  are going to be cleaning up the SOUNDSENTRY feature in the control panel
            ///  and will use this at that time.  Applications implementing WinEvents
            ///  are perfectly welcome to use it.  Clients of IAccessible* will simply
            ///  turn around and get back a non-visual object that describes the sound.
            /// </summary>
            EVENT_SYSTEM_SOUND = 0x0001,

            /// <summary>
            /// Sent when an alert needs to be given to the user.  MessageBoxes generate
            /// alerts for example.
            /// </summary>
            EVENT_SYSTEM_ALERT = 0x0002,

            /// <summary>
            /// Sent when the foreground (active) window changes, even if it is changing
            /// to another window in the same thread as the previous one.
            /// </summary>
            EVENT_SYSTEM_FOREGROUND = 0x0003,

            /// <summary>
            /// Sent when entering into and leaving from menu mode (system, app bar, and
            /// track popups).
            /// </summary>
            EVENT_SYSTEM_MENUSTART = 0x0004,

            /// <summary>
            /// Sent when entering into and leaving from menu mode (system, app bar, and
            /// track popups).
            /// </summary>
            EVENT_SYSTEM_MENUEND = 0x0005,

            /// <summary>
            /// Sent when a menu popup comes up and just before it is taken down.  Note
            /// that for a call to TrackPopupMenu(), a client will see EVENT_SYSTEM_MENUSTART
            /// followed almost immediately by EVENT_SYSTEM_MENUPOPUPSTART for the popup
            /// being shown.
            /// </summary>
            EVENT_SYSTEM_MENUPOPUPSTART = 0x0006,

            /// <summary>
            /// Sent when a menu popup comes up and just before it is taken down.  Note
            /// that for a call to TrackPopupMenu(), a client will see EVENT_SYSTEM_MENUSTART
            /// followed almost immediately by EVENT_SYSTEM_MENUPOPUPSTART for the popup
            /// being shown.
            /// </summary>
            EVENT_SYSTEM_MENUPOPUPEND = 0x0007,


            /// <summary>
            /// Sent when a window takes the capture and releases the capture.
            /// </summary>
            EVENT_SYSTEM_CAPTURESTART = 0x0008,

            /// <summary>
            /// Sent when a window takes the capture and releases the capture.
            /// </summary>
            EVENT_SYSTEM_CAPTUREEND = 0x0009,

            /// <summary>
            /// Sent when a window enters and leaves move-size dragging mode.
            /// </summary>
            EVENT_SYSTEM_MOVESIZESTART = 0x000A,

            /// <summary>
            /// Sent when a window enters and leaves move-size dragging mode.
            /// </summary>
            EVENT_SYSTEM_MOVESIZEEND = 0x000B,

            /// <summary>
            /// Sent when a window enters and leaves context sensitive help mode.
            /// </summary>
            EVENT_SYSTEM_CONTEXTHELPSTART = 0x000C,

            /// <summary>
            /// Sent when a window enters and leaves context sensitive help mode.
            /// </summary>
            EVENT_SYSTEM_CONTEXTHELPEND = 0x000D,

            /// <summary>
            /// Sent when a window enters and leaves drag drop mode.  Note that it is up
            /// to apps and OLE to generate this, since the system doesn't know.  Like
            /// EVENT_SYSTEM_SOUND, it will be a while before this is prevalent.
            /// </summary>
            EVENT_SYSTEM_DRAGDROPSTART = 0x000E,

            /// <summary>
            /// Sent when a window enters and leaves drag drop mode.  Note that it is up
            /// to apps and OLE to generate this, since the system doesn't know.  Like
            /// EVENT_SYSTEM_SOUND, it will be a while before this is prevalent.
            /// </summary>
            EVENT_SYSTEM_DRAGDROPEND = 0x000F,

            /// <summary>
            /// Sent when a dialog comes up and just before it goes away.
            /// </summary>
            EVENT_SYSTEM_DIALOGSTART = 0x0010,

            /// <summary>
            /// Sent when a dialog comes up and just before it goes away.
            /// </summary>
            EVENT_SYSTEM_DIALOGEND = 0x0011,

            /// <summary>
            /// Sent when beginning and ending the tracking of a scrollbar in a window,
            /// and also for scrollbar controls.
            /// </summary>
            EVENT_SYSTEM_SCROLLINGSTART = 0x0012,

            /// <summary>
            /// Sent when beginning and ending the tracking of a scrollbar in a window,
            /// and also for scrollbar controls.
            /// </summary>
            EVENT_SYSTEM_SCROLLINGEND = 0x0013,

            /// <summary>
            /// Sent when beginning and ending alt-tab mode with the switch window.
            /// </summary>
            EVENT_SYSTEM_SWITCHSTART = 0x0014,

            /// <summary>
            /// Sent when beginning and ending alt-tab mode with the switch window.
            /// </summary>
            EVENT_SYSTEM_SWITCHEND = 0x0015,

            /// <summary>
            /// Sent when a window minimizes.
            /// </summary>
            EVENT_SYSTEM_MINIMIZESTART = 0x0016,

            /// <summary>
            /// Sent just before a window restores.
            /// </summary>
            EVENT_SYSTEM_MINIMIZEEND = 0x0017,

            /// <summary>
            /// hwnd + ID + idChild is created item
            /// </summary>
            EVENT_OBJECT_CREATE = 0x8000,

            /// <summary>
            /// hwnd + ID + idChild is destroyed item
            /// </summary>
            EVENT_OBJECT_DESTROY = 0x8001,

            /// <summary>
            /// hwnd + ID + idChild is shown item
            /// </summary>
            EVENT_OBJECT_SHOW = 0x8002,

            /// <summary>
            /// hwnd + ID + idChild is hidden item
            /// </summary>
            EVENT_OBJECT_HIDE = 0x8003,

            /// <summary>
            /// hwnd + ID + idChild is parent of zordering children
            /// </summary>
            EVENT_OBJECT_REORDER = 0x8004,

            /// <summary>
            /// hwnd + ID + idChild is focused item
            /// </summary>
            EVENT_OBJECT_FOCUS = 0x8005,

            /// <summary>
            /// hwnd + ID + idChild is selected item (if only one), or idChild is OBJID_WINDOW if complex
            /// </summary>
            EVENT_OBJECT_SELECTION = 0x8006,

            /// <summary>
            /// hwnd + ID + idChild is item added
            /// </summary>
            EVENT_OBJECT_SELECTIONADD = 0x8007,

            /// <summary>
            /// hwnd + ID + idChild is item removed
            /// </summary>
            EVENT_OBJECT_SELECTIONREMOVE = 0x8008,

            /// <summary>
            /// hwnd + ID + idChild is parent of changed selected items
            /// </summary>
            EVENT_OBJECT_SELECTIONWITHIN = 0x8009,

            /// <summary>
            /// hwnd + ID + idChild is item w/ state change
            /// </summary>
            EVENT_OBJECT_STATECHANGE = 0x800A,

            /// <summary>
            /// hwnd + ID + idChild is moved/sized item
            /// </summary>
            EVENT_OBJECT_LOCATIONCHANGE = 0x800B,

            /// <summary>
            /// hwnd + ID + idChild is item w/ name change
            /// </summary>
            EVENT_OBJECT_NAMECHANGE = 0x800C,

            /// <summary>
            /// hwnd + ID + idChild is item w/ desc change
            /// </summary>
            EVENT_OBJECT_DESCRIPTIONCHANGE = 0x800D,

            /// <summary>
            /// hwnd + ID + idChild is item w/ value change
            /// </summary>
            EVENT_OBJECT_VALUECHANGE = 0x800E,

            /// <summary>
            /// hwnd + ID + idChild is item w/ new parent
            /// </summary>
            EVENT_OBJECT_PARENTCHANGE = 0x800F,

            /// <summary>
            /// hwnd + ID + idChild is item w/ help change
            /// </summary>
            EVENT_OBJECT_HELPCHANGE = 0x8010,

            /// <summary>
            /// hwnd + ID + idChild is item w/ def action change
            /// </summary>
            EVENT_OBJECT_DEFACTIONCHANGE = 0x8011,

            /// <summary>
            /// hwnd + ID + idChild is item w/ keybd accel change
            /// </summary>
            EVENT_OBJECT_ACCELERATORCHANGE = 0x8012,

            /// <summary>
            /// The lowest possible event value
            /// </summary>
            EVENT_MIN = 0x00000001,

            /// <summary>
            /// The highest possible event value
            /// </summary>
            EVENT_MAX = 0x7FFFFFFF,
        }

        [Flags]
        public enum WindowStyles : uint
        {
            WS_OVERLAPPED = 0x00000000,
            WS_POPUP = 0x80000000,
            WS_CHILD = 0x40000000,
            WS_MINIMIZE = 0x20000000,
            WS_VISIBLE = 0x10000000,
            WS_DISABLED = 0x08000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_MAXIMIZE = 0x01000000,
            WS_BORDER = 0x00800000,
            WS_DLGFRAME = 0x00400000,
            WS_VSCROLL = 0x00200000,
            WS_HSCROLL = 0x00100000,
            WS_SYSMENU = 0x00080000,
            WS_THICKFRAME = 0x00040000,
            WS_GROUP = 0x00020000,
            WS_TABSTOP = 0x00010000,

            WS_MINIMIZEBOX = 0x00020000,
            WS_MAXIMIZEBOX = 0x00010000,

            WS_CAPTION = WS_BORDER | WS_DLGFRAME,
            WS_TILED = WS_OVERLAPPED,
            WS_ICONIC = WS_MINIMIZE,
            WS_SIZEBOX = WS_THICKFRAME,
            WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,

            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
            WS_CHILDWINDOW = WS_CHILD,

            //Extended Window Styles

            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_ACCEPTFILES = 0x00000010,
            WS_EX_TRANSPARENT = 0x00000020,

            //#if(WINVER >= 0x0400)

            WS_EX_MDICHILD = 0x00000040,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_WINDOWEDGE = 0x00000100,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_CONTEXTHELP = 0x00000400,

            WS_EX_RIGHT = 0x00001000,
            WS_EX_LEFT = 0x00000000,
            WS_EX_RTLREADING = 0x00002000,
            WS_EX_LTRREADING = 0x00000000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_RIGHTSCROLLBAR = 0x00000000,

            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_APPWINDOW = 0x00040000,

            WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE),
            WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST),

            //#endif /* WINVER >= 0x0400 */

            //#if(WIN32WINNT >= 0x0500)

            WS_EX_LAYERED = 0x00080000,

            //#endif /* WIN32WINNT >= 0x0500 */

            //#if(WINVER >= 0x0500)

            WS_EX_NOINHERITLAYOUT = 0x00100000, // Disable inheritence of mirroring by children
            WS_EX_LAYOUTRTL = 0x00400000, // Right to left mirroring

            //#endif /* WINVER >= 0x0500 */

            //#if(WIN32WINNT >= 0x0500)

            WS_EX_COMPOSITED = 0x02000000,
            WS_EX_NOACTIVATE = 0x08000000

            //#endif /* WIN32WINNT >= 0x0500 */

        }

        public enum GetWindowLong : int {
            GWL_EXSTYLE = -20,
            GWL_STYLE = -16,
            GWL_WNDPROC = -4,
            GWL_HINSTANCE = -6,
            GWL_HWNDPARENT = -8,
            GWL_ID = -12,
            GWL_USERDATA = -21,
            DWL_DLGPROC = 4,
            DWL_MSGRESULT = 0,
            DWL_USER = 8
        }

        public enum GetWindow : int
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6,
            GW_MAX = 6,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CWPSTRUCT
        {
            public IntPtr lparam;
            public IntPtr wparam;
            public int message;
            public IntPtr hwnd;
        }

        public enum WindowMessage : int
        {
            WM_USER = 0x0400,
            WM_ACTIVATE = 0x6,
            WM_ACTIVATEAPP = 0x1C,
            WM_ASKCBFORMATNAME = 0x30C,
            WM_CANCELJOURNAL = 0x4B,
            WM_CANCELMODE = 0x1F,
            WM_CHANGECBCHAIN = 0x30D,
            WM_CHAR = 0x102,
            WM_CHARTOITEM = 0x2F,
            WM_CHILDACTIVATE = 0x22,
            WM_CHOOSEFONT_GETLOGFONT = (WM_USER + 1),
            WM_CHOOSEFONT_SETFLAGS = (WM_USER + 102),
            WM_CHOOSEFONT_SETLOGFONT = (WM_USER + 101),
            WM_CLEAR = 0x303,
            WM_CLOSE = 0x10,
            WM_COMMAND = 0x111,
            WM_COMMNOTIFY = 0x44, // no longer suported
            WM_COMPACTING = 0x41,
            WM_COMPAREITEM = 0x39,
            WM_CONVERTREQUESTEX = 0x108,
            WM_COPY = 0x301,
            WM_COPYDATA = 0x4A,
            WM_CREATE = 0x1,
            WM_CTLCOLORBTN = 0x135,
            WM_CTLCOLORDLG = 0x136,
            WM_CTLCOLOREDIT = 0x133,
            WM_CTLCOLORLISTBOX = 0x134,
            WM_CTLCOLORMSGBOX = 0x132,
            WM_CTLCOLORSCROLLBAR = 0x137,
            WM_CTLCOLORSTATIC = 0x138,
            WM_CUT = 0x300,
            WM_DDE_FIRST = 0x3E0,
            WM_DDE_ACK = (WM_DDE_FIRST + 4),
            WM_DDE_ADVISE = (WM_DDE_FIRST + 2),
            WM_DDE_DATA = (WM_DDE_FIRST + 5),
            WM_DDE_EXECUTE = (WM_DDE_FIRST + 8),
            WM_DDE_INITIATE = (WM_DDE_FIRST),
            WM_DDE_LAST = (WM_DDE_FIRST + 8),
            WM_DDE_POKE = (WM_DDE_FIRST + 7),
            WM_DDE_REQUEST = (WM_DDE_FIRST + 6),
            WM_DDE_TERMINATE = (WM_DDE_FIRST + 1),
            WM_DDE_UNADVISE = (WM_DDE_FIRST + 3),
            WM_DEADCHAR = 0x103,
            WM_DELETEITEM = 0x2D,
            WM_DESTROY = 0x2,
            WM_DESTROYCLIPBOARD = 0x307,
            WM_DEVMODECHANGE = 0x1B,
            WM_DRAWCLIPBOARD = 0x308,
            WM_DRAWITEM = 0x2B,
            WM_DROPFILES = 0x233,
            WM_ENABLE = 0xA,
            WM_ENDSESSION = 0x16,
            WM_ENTERIDLE = 0x121,
            WM_ENTERMENULOOP = 0x211,
            WM_ERASEBKGND = 0x14,
            WM_EXITMENULOOP = 0x212,
            WM_FONTCHANGE = 0x1D,
            WM_GETFONT = 0x31,
            WM_GETDLGCODE = 0x87,
            WM_GETHOTKEY = 0x33,
            WM_GETMINMAXINFO = 0x24,
            WM_GETTEXT = 0xD,
            WM_GETTEXTLENGTH = 0xE,
            WM_HOTKEY = 0x312,
            WM_HSCROLL = 0x114,
            WM_HSCROLLCLIPBOARD = 0x30E,
            WM_ICONERASEBKGND = 0x27,
            WM_IME_CHAR = 0x286,
            WM_IME_COMPOSITION = 0x10F,
            WM_IME_COMPOSITIONFULL = 0x284,
            WM_IME_CONTROL = 0x283,
            WM_IME_ENDCOMPOSITION = 0x10E,
            WM_IME_KEYDOWN = 0x290,
            WM_IME_KEYLAST = 0x10F,
            WM_IME_KEYUP = 0x291,
            WM_IME_NOTIFY = 0x282,
            WM_IME_SELECT = 0x285,
            WM_IME_SETCONTEXT = 0x281,
            WM_IME_STARTCOMPOSITION = 0x10D,
            WM_INITDIALOG = 0x110,
            WM_INITMENU = 0x116,
            WM_INITMENUPOPUP = 0x117,
            WM_KEYDOWN = 0x100,
            WM_KEYFIRST = 0x100,
            WM_KEYLAST = 0x108,
            WM_KEYUP = 0x101,
            WM_KILLFOCUS = 0x8,
            WM_LBUTTONDBLCLK = 0x203,
            WM_LBUTTONDOWN = 0x201,
            WM_LBUTTONUP = 0x202,
            WM_MBUTTONDBLCLK = 0x209,
            WM_MBUTTONDOWN = 0x207,
            WM_MBUTTONUP = 0x208,
            WM_MDIACTIVATE = 0x222,
            WM_MDICASCADE = 0x227,
            WM_MDICREATE = 0x220,
            WM_MDIDESTROY = 0x221,
            WM_MDIGETACTIVE = 0x229,
            WM_MDIICONARRANGE = 0x228,
            WM_MDIMAXIMIZE = 0x225,
            WM_MDINEXT = 0x224,
            WM_MDIREFRESHMENU = 0x234,
            WM_MDIRESTORE = 0x223,
            WM_MDISETMENU = 0x230,
            WM_MDITILE = 0x226,
            WM_MEASUREITEM = 0x2C,
            WM_MENUCHAR = 0x120,
            WM_MENUSELECT = 0x11F,
            WM_MOUSEACTIVATE = 0x21,
            WM_MOUSEFIRST = 0x200,
            WM_MOUSELAST = 0x209,
            WM_MOUSEMOVE = 0x200,
            WM_MOVE = 0x3,
            WM_NCACTIVATE = 0x86,
            WM_NCCALCSIZE = 0x83,
            WM_NCCREATE = 0x81,
            WM_NCDESTROY = 0x82,
            WM_NCHITTEST = 0x84,
            WM_NCLBUTTONDBLCLK = 0xA3,
            WM_NCLBUTTONDOWN = 0xA1,
            WM_NCLBUTTONUP = 0xA2,
            WM_NCMBUTTONDBLCLK = 0xA9,
            WM_NCMBUTTONDOWN = 0xA7,
            WM_NCMBUTTONUP = 0xA8,
            WM_NCMOUSEMOVE = 0xA0,
            WM_NCPAINT = 0x85,
            WM_NCRBUTTONDBLCLK = 0xA6,
            WM_NCRBUTTONDOWN = 0xA4,
            WM_NCRBUTTONUP = 0xA5,
            WM_NEXTDLGCTL = 0x28,
            WM_NULL = 0x0,
            WM_OTHERWINDOWCREATED = 0x42, // no longer suported
            WM_OTHERWINDOWDESTROYED = 0x43, // no longer suported
            WM_PAINT = 0xF,
            WM_PAINTCLIPBOARD = 0x309,
            WM_PAINTICON = 0x26,
            WM_PALETTECHANGED = 0x311,
            WM_PALETTEISCHANGING = 0x310,
            WM_PARENTNOTIFY = 0x210,
            WM_PASTE = 0x302,
            WM_PENWINFIRST = 0x380,
            WM_PENWINLAST = 0x38F,
            WM_POWER = 0x48,
            WM_PRINT = 0x317,
            WM_PSD_ENVSTAMPRECT = (WM_USER + 5),
            WM_PSD_FULLPAGERECT = (WM_USER + 1),
            WM_PSD_GREEKTEXTRECT = (WM_USER + 4),
            WM_PSD_MARGINRECT = (WM_USER + 3),
            WM_PSD_MINMARGINRECT = (WM_USER + 2),
            WM_PSD_PAGESETUPDLG = (WM_USER),
            WM_PSD_YAFULLPAGERECT = (WM_USER + 6),
            WM_QUERYDRAGICON = 0x37,
            WM_QUERYENDSESSION = 0x11,
            WM_QUERYNEWPALETTE = 0x30F,
            WM_QUERYOPEN = 0x13,
            WM_QUEUESYNC = 0x23,
            WM_QUIT = 0x12,
            WM_RBUTTONDBLCLK = 0x206,
            WM_RBUTTONDOWN = 0x204,
            WM_RBUTTONUP = 0x205,
            WM_RENDERALLFORMATS = 0x306,
            WM_RENDERFORMAT = 0x305,
            WM_SETCURSOR = 0x20,
            WM_SETFOCUS = 0x7,
            WM_SETFONT = 0x30,
            WM_SETHOTKEY = 0x32,
            WM_SETREDRAW = 0xB,
            WM_SETTEXT = 0xC,
            WM_SHOWWINDOW = 0x18,
            WM_SIZE = 0x5,
            WM_SIZECLIPBOARD = 0x30B,
            WM_SPOOLERSTATUS = 0x2A,
            WM_SYSCHAR = 0x106,
            WM_SYSCOLORCHANGE = 0x15,
            WM_SYSCOMMAND = 0x112,
            WM_SYSDEADCHAR = 0x107,
            WM_SYSKEYDOWN = 0x104,
            WM_SYSKEYUP = 0x105,
            WM_TIMECHANGE = 0x1E,
            WM_TIMER = 0x113,
            WM_UNDO = 0x304,
            WM_VKEYTOITEM = 0x2E,
            WM_VSCROLL = 0x115,
            WM_VSCROLLCLIPBOARD = 0x30A,
            WM_WINDOWPOSCHANGED = 0x47,
            WM_WINDOWPOSCHANGING = 0x46,
            WM_WININICHANGE = 0x1A
        }

        public enum WindowsHook : int
        {
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }

        public enum SystemMetric
        {
            SM_ARRANGE = 56,
            SM_CLEANBOOT = 67,
            SM_CMONITORS = 80,
            SM_CMOUSEBUTTONS = 43,
            SM_CXBORDER = 5,
            SM_CXCURSOR = 13,
            SM_CXDLGFRAME = 7,
            SM_CXDOUBLECLK = 36,
            SM_CXDRAG = 68,
            SM_CXEDGE = 45,
            SM_CXFIXEDFRAME = 7,
            SM_CXFOCUSBORDER = 83,
            SM_CXFRAME = 32,
            SM_CXFULLSCREEN = 16,
            SM_CXHSCROLL = 21,
            SM_CXHTHUMB = 10,
            SM_CXICON = 11,
            SM_CXICONSPACING = 38,
            SM_CXMAXIMIZED = 61,
            SM_CXMAXTRACK = 59,
            SM_CXMENUCHECK = 71,
            SM_CXMENUSIZE = 54,
            SM_CXMIN = 28,
            SM_CXMINIMIZED = 57,
            SM_CXMINSPACING = 47,
            SM_CXMINTRACK = 34,
            SM_CXPADDEDBORDER = 92,
            SM_CXSCREEN = 0,
            SM_CXSIZE = 30,
            SM_CXSIZEFRAME = 32,
            SM_CXSMICON = 49,
            SM_CXSMSIZE = 52,
            SM_CXVIRTUALSCREEN = 78,
            SM_CXVSCROLL = 2,
            SM_CYBORDER = 6,
            SM_CYCAPTION = 4,
            SM_CYCURSOR = 14,
            SM_CYDLGFRAME = 8,
            SM_CYDOUBLECLK = 37,
            SM_CYDRAG = 69,
            SM_CYEDGE = 46,
            SM_CYFIXEDFRAME = 8,
            SM_CYFOCUSBORDER = 84,
            SM_CYFRAME = 33,
            SM_CYFULLSCREEN = 17,
            SM_CYHSCROLL = 3,
            SM_CYICON = 12,
            SM_CYICONSPACING = 39,
            SM_CYKANJIWINDOW = 18,
            SM_CYMAXIMIZED = 62,
            SM_CYMAXTRACK = 60,
            SM_CYMENU = 15,
            SM_CYMENUCHECK = 72,
            SM_CYMENUSIZE = 55,
            SM_CYMIN = 29,
            SM_CYMINIMIZED = 58,
            SM_CYMINSPACING = 48,
            SM_CYMINTRACK = 35,
            SM_CYSCREEN = 1,
            SM_CYSIZE = 31,
            SM_CYSIZEFRAME = 33,
            SM_CYSMCAPTION = 51,
            SM_CYSMICON = 50,
            SM_CYSMSIZE = 53,
            SM_CYVIRTUALSCREEN = 79,
            SM_CYVSCROLL = 20,
            SM_CYVTHUMB = 9,
            SM_DBCSENABLED = 42,
            SM_DEBUG = 22,
            SM_DIGITIZER = 94,
            SM_IMMENABLED = 82,
            SM_MAXIMUMTOUCHES = 95,
            SM_MEDIACENTER = 87,
            SM_MENUDROPALIGNMENT = 40,
            SM_MIDEASTENABLED = 74,
            SM_MOUSEPRESENT = 19,
            SM_MOUSEHORIZONTALWHEELPRESENT = 91,
            SM_MOUSEWHEELPRESENT = 75,
            SM_NETWORK = 63,
            SM_PENWINDOWS = 41,
            SM_REMOTECONTROL = 0x2001,
            SM_REMOTESESSION = 0x1000,
            SM_SAMEDISPLAYFORMAT = 81,
            SM_SECURE = 44,
            SM_SERVERR2 = 89,
            SM_SHOWSOUNDS = 70,
            SM_SHUTTINGDOWN = 0x2000,
            SM_SLOWMACHINE = 73,
            SM_STARTER = 88,
            SM_SWAPBUTTON = 23,
            SM_TABLETPC = 86,
            SM_XVIRTUALSCREEN = 76,
            SM_YVIRTUALSCREEN = 77,
        }

        [Flags()]
        public enum DeviceContext : uint
        {
            /// <summary>DCX_WINDOW: Returns a DC that corresponds to the window rectangle rather
            /// than the client rectangle.</summary>
            DCX_WINDOW = 0x00000001,
            /// <summary>DCX_CACHE: Returns a DC from the cache, rather than the OWNDC or CLASSDC
            /// window. Essentially overrides CS_OWNDC and CS_CLASSDC.</summary>
            DCX_CACHE = 0x00000002,
            /// <summary>DCX_NORESETATTRS: Does not reset the attributes of this DC to the
            /// default attributes when this DC is released.</summary>
            DCX_NORESETATTRS = 0x00000004,
            /// <summary>DCX_CLIPCHILDREN: Excludes the visible regions of all child windows
            /// below the window identified by hWnd.</summary>
            DCX_CLIPCHILDREN = 0x00000008,
            /// <summary>DCX_CLIPSIBLINGS: Excludes the visible regions of all sibling windows
            /// above the window identified by hWnd.</summary>
            DCX_CLIPSIBLINGS = 0x00000010,
            /// <summary>DCX_PARENTCLIP: Uses the visible region of the parent window. The
            /// parent's WS_CLIPCHILDREN and CS_PARENTDC style bits are ignored. The origin is
            /// set to the upper-left corner of the window identified by hWnd.</summary>
            DCX_PARENTCLIP = 0x00000020,
            /// <summary>DCX_EXCLUDERGN: The clipping region identified by hrgnClip is excluded
            /// from the visible region of the returned DC.</summary>
            DCX_EXCLUDERGN = 0x00000040,
            /// <summary>DCX_INTERSECTRGN: The clipping region identified by hrgnClip is
            /// intersected with the visible region of the returned DC.</summary>
            DCX_INTERSECTRGN = 0x00000080,
            /// <summary>DCX_EXCLUDEUPDATE: Unknown...Undocumented</summary>
            DCX_EXCLUDEUPDATE = 0x00000100,
            /// <summary>DCX_INTERSECTUPDATE: Unknown...Undocumented</summary>
            DCX_INTERSECTUPDATE = 0x00000200,
            /// <summary>DCX_LOCKWINDOWUPDATE: Allows drawing even if there is a LockWindowUpdate
            /// call in effect that would otherwise exclude this window. Used for drawing during
            /// tracking.</summary>
            DCX_LOCKWINDOWUPDATE = 0x00000400,
            /// <summary>DCX_VALIDATE When specified with DCX_INTERSECTUPDATE, causes the DC to
            /// be completely validated. Using this function with both DCX_INTERSECTUPDATE and
            /// DCX_VALIDATE is identical to using the BeginPaint function.</summary>
            DCX_VALIDATE = 0x00200000,
        }

        [Flags]
        public enum PrintFlags : int
        {
            PRF_CHECKVISIBLE = 0x01,
            PRF_NONCLIENT = 0x02,
            PRF_CLIENT = 0x04,
            PRF_ERASEBKGND = 0x08,
            PRF_CHILDREN = 0x10,
            PRF_OWNED = 0x20
        }

        [Flags]
        public enum SetWindowPos : uint
        {
            // ReSharper disable InconsistentNaming

            /// <summary>
            ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            SWP_ASYNCWINDOWPOS = 0x4000,

            /// <summary>
            ///     Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            SWP_DEFERERASE = 0x2000,

            /// <summary>
            ///     Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            SWP_DRAWFRAME = 0x0020,

            /// <summary>
            ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            SWP_FRAMECHANGED = 0x0020,

            /// <summary>
            ///     Hides the window.
            /// </summary>
            SWP_HIDEWINDOW = 0x0080,

            /// <summary>
            ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOACTIVATE = 0x0010,

            /// <summary>
            ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            SWP_NOCOPYBITS = 0x0100,

            /// <summary>
            ///     Retains the current position (ignores X and Y parameters).
            /// </summary>
            SWP_NOMOVE = 0x0002,

            /// <summary>
            ///     Does not change the owner window's position in the Z order.
            /// </summary>
            SWP_NOOWNERZORDER = 0x0200,

            /// <summary>
            ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            ///     Same as the SWP_NOOWNERZORDER flag.
            /// </summary>
            SWP_NOREPOSITION = 0x0200,

            /// <summary>
            ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            SWP_NOSENDCHANGING = 0x0400,

            /// <summary>
            ///     Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            SWP_NOSIZE = 0x0001,

            /// <summary>
            ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOZORDER = 0x0004,

            /// <summary>
            ///     Displays the window.
            /// </summary>
            SWP_SHOWWINDOW = 0x0040,

            // ReSharper restore InconsistentNaming
        }
    }
}
