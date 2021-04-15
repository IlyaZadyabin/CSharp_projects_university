using System;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
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

        public CustomV1DataOnGrid customV1DataOnGrid { get; set; }
        public V1Data selectedCollection { get; set; }

        private void v1DataCollectionFilter(object sender, FilterEventArgs e) {
            e.Accepted = e.Item is V1DataCollection;
        }

        private void v1DataOnGridFilter(object sender, FilterEventArgs e) {
            e.Accepted = e.Item is V1DataOnGrid;
        }

        public MainWindow()
        {
            v1MainCollection = new V1MainCollection();
            customV1DataOnGrid = new CustomV1DataOnGrid(v1MainCollection);
            InitializeComponent();
            DataContext = this;
        }

        private void New(object sender, RoutedEventArgs e) {
            if (IsDataLossWarningIgnored(sender, e)) {
                v1MainCollection.RemoveAll();
            }
        }
        private void Open(object sender, RoutedEventArgs e) {
            if (IsDataLossWarningIgnored(sender, e)) {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == true) {
                    v1MainCollection.Load(openFileDialog.FileName);
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

        private void AddCustomV1DataOnGrid(object sender, RoutedEventArgs e) {
            customV1DataOnGrid.AddToCollection();
        }

        private void CanSaveCommandHandler(object sender, CanExecuteRoutedEventArgs e) {
            if (v1MainCollection.IsCollectionChanged == true) e.CanExecute = true;
            else e.CanExecute = false;
        }
        private void SaveCommandHandler(object sender, ExecutedRoutedEventArgs e) {
            Save(sender, e);
        }
        private void OpenCommandHandler(object sender, ExecutedRoutedEventArgs e) {
            Open(sender, e);
        }
        private void CanDeleteCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (selectedCollection != null) {
                e.CanExecute = true;
            } else {
                e.CanExecute = false;
            }
        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            try {
                v1MainCollection.Remove(selectedCollection.Info, selectedCollection.Date);
            } catch (Exception exception) {
                MessageBox.Show("Error occured: " + exception.Message);
            }
        }
        public static RoutedCommand AddCustomCommand = new ("AddCustom", typeof(MainWindow));

        private void CanAddCustomCommandHandler(object sender, CanExecuteRoutedEventArgs e) {
            if (Add_Custom_Grid != null) {
                foreach (FrameworkElement child in Add_Custom_Grid.Children) {
                    if (Validation.GetHasError(child) == true) {
                        e.CanExecute = false;
                        return;
                    }
                    e.CanExecute = true;
                }
            }
            else e.CanExecute = false;
        }
        private void AddCustomCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            customV1DataOnGrid.AddToCollection();
        }
    }
    public class VecAbsValueConverter : IValueConverter
    {
        string text;

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            Vector3 vec = (Vector3)value;
            text = "X: " + vec.X + " Y: " + vec.Y + " Z: " + vec.Z + " Length: " + vec.Length();
            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return text;
        }
    }

    public class FirstElemConverter : IValueConverter
    {
        string text;

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            V1DataOnGrid v1DataOnGrid = (V1DataOnGrid)value;
            if (v1DataOnGrid != null && v1DataOnGrid.Count() > 0)
            {
                return v1DataOnGrid.FirstOrDefault().ToString();
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return text;
        }
    }

    public class LastElemConverter : IValueConverter
    {
        string text;

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            V1DataOnGrid v1DataOnGrid = (V1DataOnGrid)value;
            if (v1DataOnGrid != null && v1DataOnGrid.Count() > 0)
                return v1DataOnGrid.ElementAtOrDefault(v1DataOnGrid.Count() - 1).ToString();
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return text;
        }
    }

    public class CustomV1DataOnGrid : INotifyPropertyChanged, IDataErrorInfo
    {
        private V1MainCollection v1MainCollection_ref;
        private string info = "";
        private int amount_of_nodes;
        private float minValue;
        private float maxValue;

        public void AddToCollection()
        {
            V1DataOnGrid v1DataOnGrid = new V1DataOnGrid(Info, DateTime.Now, new FieldLibrary.Grid(0, 2, amount_of_nodes));           
            v1DataOnGrid.InitRandom(minValue, maxValue);
            v1MainCollection_ref.Add(v1DataOnGrid);

            Info = "";
            Amount_of_nodes = "0";
            MinValue = "0";
            MaxValue = "0";
    }
        public V1MainCollection V1MainCollection_ref
        {
            get { return v1MainCollection_ref; }
            set { v1MainCollection_ref = value; }
        }

        public CustomV1DataOnGrid(V1MainCollection collection) {
            v1MainCollection_ref = collection;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                OnPropertyChanged("Info");
            }
        }
        public string Amount_of_nodes
        {
            get { return amount_of_nodes.ToString(); }
            set
            {
                _ = int.TryParse(value, out amount_of_nodes);
                OnPropertyChanged("Amount_of_nodes");
            }
        }
        public string MinValue
        {
            get { return minValue.ToString(); }
            set
            {
                _ = float.TryParse(value, out minValue);
                OnPropertyChanged("MinValue");
                OnPropertyChanged("MaxValue");
            }
        }
        public string MaxValue
        {
            get { return maxValue.ToString(); }
            set
            {
                _ = float.TryParse(value, out maxValue);
                OnPropertyChanged("MaxValue");
                OnPropertyChanged("MinValue");
            }
        }

        public string Error { get { return "Error Text"; } }
        public string this[string property]
        {
            get
            {
                string msg = null;
                switch (property)
                {
                    case "Info":
                        if (Info.Length == 0) msg = "Info must contain at least one symbol";
                        if (V1MainCollection_ref.Where(v1data => v1data.Info == Info).Any()) msg = "Info must differ from other V1MainCollection elements";
                        break;
                    case "Amount_of_nodes":
                        if (amount_of_nodes < 3) msg = "Amount of nodes must be at least 3";
                        break;
                    case "MinValue":
                        if (minValue >= maxValue) msg = "Min value must be less then max value";
                        break;
                    case "MaxValue":
                        if (minValue >= maxValue) msg = "Min value must be less then max value";
                        break;
                    default:
                        break;
                }
                return msg;
            }
        }
    }
}