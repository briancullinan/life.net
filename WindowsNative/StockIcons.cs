using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Interop;

namespace WindowsNative
{
    public class StockIcons
    {
        private StockIcons()
        {
            
        }

        private StockIcons(ShellApi.StockIconOptions flags)
        {
            _flags = flags;
        }

        public static StockIcons Default
        {
            get
            {
                return new StockIcons();
            }
        }

        private ShellApi.StockIconOptions _flags;
        private static ImageSource MakeImage(ShellApi.StockIconIdentifier identifier, ShellApi.StockIconOptions flags)
        {
            var iconHandle = GetIcon(identifier, flags);
            ImageSource imageSource;
            try
            {
                imageSource = Imaging.CreateBitmapSourceFromHIcon(iconHandle, Int32Rect.Empty, null);
            }
            finally
            {
                User32.DestroyIcon(iconHandle);
            }
            return imageSource;
        }
        
        private static IntPtr GetIcon(ShellApi.StockIconIdentifier identifier, ShellApi.StockIconOptions flags)
        {
            var info = new ShellApi.StockIconInfo
                {
                    StuctureSize = (UInt32) Marshal.SizeOf(typeof (ShellApi.StockIconInfo))
                };
            var hResult = Shell32.SHGetStockIconInfo(identifier, flags, ref info);
            if (hResult < 0)
                throw new COMException("SHGetStockIconInfo execution failure", hResult);
            return info.Handle;
        }

        private BitmapSource GetBitmapSource(ShellApi.StockIconIdentifier identifier)
        {
            var bitmapSource = (BitmapSource)MakeImage(identifier, ShellApi.StockIconOptions.Handle | _flags);
            bitmapSource.Freeze();
            return bitmapSource;
        }

        public BitmapSource DocumentNotAssociated
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DocumentNotAssociated); }
        }

        public BitmapSource DocumentAssociated
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DocumentAssociated); }
        }

        public BitmapSource Application
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Application); }
        }

        public BitmapSource Folder
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Folder); }
        }

        public BitmapSource FolderOpen
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.FolderOpen); }
        }

        public BitmapSource Drive525
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Drive525); }
        }

        public BitmapSource Drive35
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Drive35); }
        }

        public BitmapSource DriveRemove
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DriveRemove); }
        }

        public BitmapSource DriveFixed
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DriveFixed); }
        }

        public BitmapSource DriveNetwork
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DriveNetwork); }
        }

        public BitmapSource DriveNetworkDisabled
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DriveNetworkDisabled); }
        }

        public BitmapSource DriveCD
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DriveCD); }
        }

        public BitmapSource DriveRAM
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DriveRAM); }
        }

        public BitmapSource World
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.World); }
        }

        public BitmapSource Server
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Server); }
        }

        public BitmapSource Printer
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Printer); }
        }

        public BitmapSource MyNetwork
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MyNetwork); }
        }

        public BitmapSource Find
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Find); }
        }

        public BitmapSource Help
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Help); }
        }

        public BitmapSource Share
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Share); }
        }

        public BitmapSource Link
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Link); }
        }

        public BitmapSource SlowFile
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.SlowFile); }
        }

        public BitmapSource Recycler
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Recycler); }
        }

        public BitmapSource RecyclerFull
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.RecyclerFull); }
        }

        public BitmapSource MediaCDAudio
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaCDAudio); }
        }

        public BitmapSource Lock
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Lock); }
        }

        public BitmapSource AutoList
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.AutoList); }
        }

        public BitmapSource PrinterNet
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.PrinterNet); }
        }

        public BitmapSource ServerShare
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.ServerShare); }
        }

        public BitmapSource PrinterFax
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.PrinterFax); }
        }

        public BitmapSource PrinterFaxNet
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.PrinterFaxNet); }
        }

        public BitmapSource PrinterFile
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.PrinterFile); }
        }

        public BitmapSource Stack
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Stack); }
        }

        public BitmapSource MediaSVCD
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaSVCD); }
        }

        public BitmapSource StuffedFolder
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.StuffedFolder); }
        }

        public BitmapSource DriveUnknown
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DriveUnknown); }
        }

        public BitmapSource DriveDVD
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DriveDVD); }
        }

        public BitmapSource MediaDVD
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaDVD); }
        }

        public BitmapSource MediaDVDRAM
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaDVDRAM); }
        }

        public BitmapSource MediaDVDRW
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaDVDRW); }
        }

        public BitmapSource MediaDVDR
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaDVDR); }
        }

        public BitmapSource MediaDVDROM
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaDVDROM); }
        }

        public BitmapSource MediaCDAudioPlus
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaCDAudioPlus); }
        }

        public BitmapSource MediaCDRW
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaCDRW); }
        }

        public BitmapSource MediaCDR
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaCDR); }
        }

        public BitmapSource MediaCDBurn
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaCDBurn); }
        }

        public BitmapSource MediaBlankCD
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaBlankCD); }
        }

        public BitmapSource MediaCDROM
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaCDROM); }
        }

        public BitmapSource AudioFiles
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.AudioFiles); }
        }

        public BitmapSource ImageFiles
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.ImageFiles); }
        }

        public BitmapSource VideoFiles
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.VideoFiles); }
        }

        public BitmapSource MixedFiles
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MixedFiles); }
        }

        public BitmapSource FolderBack
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.FolderBack); }
        }

        public BitmapSource FolderFront
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.FolderFront); }
        }

        public BitmapSource Shield
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Shield); }
        }

        public BitmapSource Warning
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Warning); }
        }

        public BitmapSource Info
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Info); }
        }

        public BitmapSource Error
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Error); }
        }

        public BitmapSource Key
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Key); }
        }

        public BitmapSource Software
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Software); }
        }

        public BitmapSource Rename
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Rename); }
        }

        public BitmapSource Delete
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Delete); }
        }

        public BitmapSource MediaAudioDVD
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaAudioDVD); }
        }

        public BitmapSource MediaMovieDVD
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaMovieDVD); }
        }

        public BitmapSource MediaEnhancedCD
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaEnhancedCD); }
        }

        public BitmapSource MediaEnhancedDVD
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaEnhancedDVD); }
        }

        public BitmapSource MediaHDDVD
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaHDDVD); }
        }

        public BitmapSource MediaBluRay
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaBluRay); }
        }

        public BitmapSource MediaVCD
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaVCD); }
        }

        public BitmapSource MediaDVDPlusR
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaDVDPlusR); }
        }

        public BitmapSource MediaDVDPlusRW
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaDVDPlusRW); }
        }

        public BitmapSource DesktopPC
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DesktopPC); }
        }

        public BitmapSource MobilePC
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MobilePC); }
        }

        public BitmapSource Users
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Users); }
        }

        public BitmapSource MediaSmartMedia
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaSmartMedia); }
        }

        public BitmapSource MediaCompactFlash
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.MediaCompactFlash); }
        }

        public BitmapSource DeviceCellPhone
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DeviceCellPhone); }
        }

        public BitmapSource DeviceCamera
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DeviceCamera); }
        }

        public BitmapSource DeviceVideoCamera
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DeviceVideoCamera); }
        }

        public BitmapSource DeviceAudioPlayer
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.DeviceAudioPlayer); }
        }

        public BitmapSource NetworkConnect
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.NetworkConnect); }
        }

        public BitmapSource Internet
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Internet); }
        }

        public BitmapSource ZipFile
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.ZipFile); }
        }

        public BitmapSource Settings
        {
            get { return GetBitmapSource(ShellApi.StockIconIdentifier.Settings); }
        }

        public StockIcons this[ShellApi.StockIconOptions flags]
        {
            get { return new StockIcons(flags); }
        }
    }
}
