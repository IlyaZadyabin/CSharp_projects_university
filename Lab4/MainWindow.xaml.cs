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
using FieldLibrary;

namespace Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void New(object sender, RoutedEventArgs e)
        {
           // V1Main
            MessageBox.Show("New");
        }
        private void Open(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open");
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save");
        }
        private void AddDefaults(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AddDefaults");
        }
        private void AddDefaultV1DataCollection(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AddDefaultV1DataCollection");
        }
        private void AddDefaultV1DataOnGrid(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AddDefaultV1DataOnGrid");
        }
        private void AddElementFromFile(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AddElementFromFile");
        }
        private void Remove(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Remove");
        }
    }
}
