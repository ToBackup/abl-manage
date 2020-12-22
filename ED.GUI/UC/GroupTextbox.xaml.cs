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

namespace ED.GUI.UC
{
    /// <summary>
    /// GroupTextbox.xaml 的交互逻辑
    /// </summary>
    public partial class GroupTextbox : UserControl
    {
        public GroupTextbox()
        {
            InitializeComponent();
        }

        private static readonly DependencyProperty LeftContextProperty = DependencyProperty.Register("LeftContext", typeof(string), typeof(GroupTextbox));
        public string LeftContext
        {
            get => (string)GetValue(LeftContextProperty);
            set => SetValue(LeftContextProperty, value);
        }
        private static readonly DependencyProperty RightContextProperty = DependencyProperty.Register("RightContext", typeof(string), typeof(GroupTextbox));
        public string RightContext
        {
            get => (string)GetValue(RightContextProperty);
            set => SetValue(RightContextProperty, value);
        }
    }
}
