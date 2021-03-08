using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
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
using Microsoft.Win32;

namespace Lab_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int MaxAmountOfResults { get { return v1MainCollection.GetMaxAmount; } set { }  }
        public V1MainCollection v1MainCollection { get; set; }
        public V1Data selectedCollection { get; set; }

        private void v1DataCollectionFilter(object sender, FilterEventArgs e)
        {
            e.Accepted = e.Item is V1DataCollection;
        }
        //private void v1DataCollectionDetailsFilter(object sender, FilterEventArgs e)
        //{
        //    e.Accepted = e.Item is V1
        //}

        public MainWindow()
        {
            v1MainCollection = new V1MainCollection();
            InitializeComponent();

            ((INotifyCollectionChanged)listBox_Main.Items).CollectionChanged += ListBox_CollectionChanged;

            DataContext = this;
        }

        private void ListBox_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Property.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
            v1MainCollection.IsCollectionChanged = true;
        }

        private void New(object sender, RoutedEventArgs e)
        {
            if (IsDataLossWarningIgnored(sender, e))
            {
                v1MainCollection.RemoveAll();
                
                
                
                //listBox_Main.Items.Refresh();
            }
        }
        private void Open(object sender, RoutedEventArgs e)
        {
            if (IsDataLossWarningIgnored(sender, e))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == true)
                {
                    v1MainCollection.Load(openFileDialog.FileName);
                    //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
                }
            }
            
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true) {
                v1MainCollection.Save(saveFileDialog.FileName);
            }
        }
        private void AddDefaults(object sender, RoutedEventArgs e)
        {
            v1MainCollection.AddDefaults();
        }
        private void AddDefaultV1DataCollection(object sender, RoutedEventArgs e)
        {
            V1DataCollection col = new V1DataCollection("col_def", new DateTime(2008, 5, 1, 8, 30, 52));
            col.InitRandom(3, 1, 7, 2, 15);
            v1MainCollection.Add(col);
        }
        private void AddDefaultV1DataOnGrid(object sender, RoutedEventArgs e)
        {
            FieldLibrary.Grid grid = new FieldLibrary.Grid(0, 1, 4);
            V1DataOnGrid v1DataOnGrid = new V1DataOnGrid("grid_def", new DateTime(2008, 6, 1, 7, 47, 0), grid);
            v1DataOnGrid.InitRandom(4, 10);
            v1MainCollection.Add(v1DataOnGrid);
        }
        private void AddElementFromFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            try {
                if (openFileDialog.ShowDialog() == true)
                    v1MainCollection.Add(new V1DataCollection(openFileDialog.FileName));                
            } catch (Exception exception) {
                MessageBox.Show("Error occured: " + exception.Message);
            }
        }
        private void Remove(object sender, RoutedEventArgs e)
        {
            if (selectedCollection != null) {
                try {
                    v1MainCollection.Remove(selectedCollection.Info, selectedCollection.Date);
                } catch (Exception exception) {
                    MessageBox.Show("Error occured: " + exception.Message);
                }
            } else {
                MessageBox.Show("Nothing is selected");
            }
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!IsDataLossWarningIgnored(sender, new RoutedEventArgs()))
                e.Cancel = true;
        }
        private bool IsDataLossWarningIgnored(object sender, RoutedEventArgs e)
        {
            if (v1MainCollection != null && v1MainCollection.IsCollectionChanged)
            {
                MessageBoxResult result = MessageBox.Show("You're risking to lose data. Would you like to save your collection?", "Warning", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Save(sender, e);
                    return false;
                }
            }
            return true;
        }
    }
}
