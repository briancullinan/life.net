using System;
using System.Collections.Generic;
using System.Windows;

namespace Files
{
    /// <summary>
    /// Interaction logic for FileItem.xaml
    /// </summary>
    public partial class FileItem : IEqualityComparer<FileItem>
    {
        private static readonly DependencyProperty FilesystemProperty = DependencyProperty.Register(
            "Filesystem",
            typeof(Filesystem),
            typeof(FileItem),
            new PropertyMetadata(null));

        private Filesystem Filesystem
        {
            get { return (Filesystem)GetValue(FilesystemProperty); }
            set { SetValue(FilesystemProperty, value); }
        }

        public FileItem(Filesystem file)
        {
            Filesystem = file;
            InitializeComponent();
        }

        public bool Equals(FileItem x, FileItem y)
        {
            return x.Filesystem.EventId == y.Filesystem.EventId;
        }

        public int GetHashCode(FileItem obj)
        {
            throw new NotImplementedException();
        }
    }
}
