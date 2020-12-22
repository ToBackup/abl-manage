using Dialog;
using ED.BLL;
using ED.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using AduSkin.Controls.Metro;

namespace ED.GUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        IEDInstance instance = null;
        IEDSett set = null;
        string _titlehead = "QPCR";
        public BindSoure bs { get; set; }

        public MainWindow()
        {
            instance = new Export();
            set = instance.GetEDSett();
            //set.Write("OutTypeList", "I,II");

            string value = set.Read("OutTypeList");

            bs = new BindSoure()
            {
                Items = value?.Split(','),
                SelectedItem = set.Read("OutType"),
                Title = _titlehead,
                JoinDb = Convert.ToBoolean(set.Read("JoinDb"))
            };

            InitializeComponent();
            this.DataContext = bs;

            //bar.Visibility = Visibility.Collapsed;
            //cbType.ItemsSource = lt;
            //cbType.SelectedItem = value;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //bar.Value = 50;
            FolderSelectDialog fsd = new FolderSelectDialog();

            if(fsd.ShowDialog())
            {
                IEDLoad load = instance.GetEDLoad(cbType.Text);
                load.ProgressEvent += Load_ProgressEvent;
                bs.ShowProgress();

                load.Load((int)bar.Maximum, fsd.FileNames);
                //for (int i = 0; i < 100; i++)
                //{
                //    Load_ProgressEvent(i, null);
                //    Thread.Sleep(100);
                //}
                bs.Title = _titlehead;
                bs.ShowLab("完成");
            }
        }

        int count = 0;
        string str;
        private void Load_ProgressEvent(double pro, string tag)
        {
            Dispatcher.Invoke(new Action<DependencyProperty, object>(bar.SetValue), System.Windows.Threading.DispatcherPriority.Background, ProgressBar.ValueProperty, Math.Round(pro));

            if (count % 3 == 0)
                str = "中.";
            else
                str += ".";

            bs.Title = string.Format("{0} [{1}{2}]", _titlehead, tag, str);
            count ++;
        }

        private void Exc_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (AduMessageBox.ShowYesNo("是否确认退出？", null,"是","否", MessageBoxImage.Question) != MessageBoxResult.Yes)
                e.Cancel = true;
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = set.Read("OutType");

            if (value != bs.SelectedItem)
                set.Write("OutType", bs.SelectedItem);
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow cw = new ConfigWindow();
            cw.ShowDialog();
        }

        private void AduCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AduCheckBox acb = sender as AduCheckBox;
            set.Write("JoinDb", acb.IsChecked.ToString());
        }
    }

    public class BindSoure : INotifyPropertyChanged
    {
        public BindSoure()
        {
            _show = Visibility.Collapsed;
        }
        string _title;
        string _selecteditem;
        string[] _items;
        bool _joinDb;
        Visibility _show;
        string _labText;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
            }
        }
        public string SelectedItem
        {
            get => _selecteditem;
            set
            {
                _selecteditem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedItem"));
            }
        }
        public string[] Items
        {
            get => _items;
            set
            {
                _items = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Items"));
            }
        }
        public bool JoinDb
        {
            get => _joinDb;
            set
            {
                _joinDb = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("JoinDb"));
            }
        }

        public Visibility Progress 
        { 
            get => _show; 
            set
            {
                _show = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Progress"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Lab"));
            }
        }
        public Visibility Lab
        {
            get => _show == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
        public string LabText 
        { 
            get => _labText; 
            set
            {
                _labText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabText"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ShowProgress()
        {
            Progress = Visibility.Visible;
        }
        public void ShowLab(string str)
        {
            LabText = str;
            Progress = Visibility.Collapsed;
        }
    }
}
