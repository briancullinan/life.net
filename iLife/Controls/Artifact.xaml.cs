using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using iLife.Utilities.Extensions;

namespace iLife.Controls
{
    /// <summary>
    /// Interaction logic for Artifact.xaml
    /// </summary>
    public partial class Artifact : UserControl
    {
        public Artifact()
        {
            InitializeComponent();
        }

        public static DependencyProperty CategoryProperty = DependencyProperty.Register("CategoryProperty", typeof(string), typeof(Artifact), new PropertyMetadata("Some Category"));

        public string Category
        {
            get
            {
                return (string)GetValue(CategoryProperty);
            }
            set
            {
                SetValue(CategoryProperty, value);
            }
        }
    }
}
