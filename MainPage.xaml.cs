using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace ToDoUWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public class ToDo : INotifyPropertyChanged
    {
        public ToDo()
        {
            Visibility = Visibility.Collapsed;
        }
        public string Content { get; set; }

        private Visibility visibility;
        public Visibility Visibility
        {
            get { return visibility; }
            set
            {
                if (visibility != value)
                {
                    visibility = value;
                    NotifyPropertyChanged("Visibility");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<ToDo> toDos;
        public MainPage()
        {
            this.InitializeComponent();
            toDos = new ObservableCollection<ToDo>();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            toDos.Add(new ToDo { Content = textBoxAdd.Text });
            textBoxAdd.Text = "";
        }
        private void remove_Click(object sender, RoutedEventArgs e)
        {
            var item = listView.SelectedItem as ToDo;
            if (item != null)
            {
                toDos.Remove(item);
            }
        }
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.RemovedItems)
            {
                var toDo = item as ToDo;
                toDo.Visibility = Visibility.Collapsed;
            }

            foreach (var item in e.AddedItems)
            {
                var toDo = item as ToDo;
                toDo.Visibility = Visibility.Visible;
            }
        }
    }
}
