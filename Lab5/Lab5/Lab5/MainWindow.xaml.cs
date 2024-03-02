using System.Collections.ObjectModel;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Calculator calculator = new Calculator();
        ObservableCollection<ExpressionResult> ResList = new ObservableCollection<ExpressionResult>();

        public class ExpressionResult
        {
            public string Expression { get; set; }
            public bool Result { get; set; }
            public SolidColorBrush MessageColor { get; set; }
            public int LastMemIndex { get; set; }
            public ExpressionResult(string Expression, int LastMemIndex, bool Result)
            {
                this.Expression = Expression;
                this.LastMemIndex = LastMemIndex;
                this.Result = Result;
                if (Result) {
                    this.MessageColor = Brushes.White;
                }
                else
                {
                    this.MessageColor = Brushes.Orange;
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            Log.ItemsSource = ResList;
        }
        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Log.SelectedItem != null)
            {
                if (((ExpressionResult)Log.SelectedItem).Result)
                {
                    Expression.Text = "#" + ((ExpressionResult)Log.SelectedItem).LastMemIndex.ToString();
                }
            }
        }

        private void TxtBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsDown && e.Key == Key.Enter)
            {
                if (calculator.CalculateExpression(Expression.Text))
                {
                    ResList.Add(new ExpressionResult(Expression.Text + "=" + calculator.GetLastMem(), calculator.GetLastMemIndex(), true));
                }
                else
                {
                    ResList.Add(new ExpressionResult(Expression.Text + "=ERROR", calculator.GetLastMemIndex(), false));
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (calculator.CalculateExpression(Expression.Text))
            {
                ResList.Add(new ExpressionResult(Expression.Text + "=" + calculator.GetLastMem(), calculator.GetLastMemIndex(), true));
            }
            else
            {
                ResList.Add(new ExpressionResult(Expression.Text + "=ERROR", calculator.GetLastMemIndex(), false));
            }
        }

        private void Log_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}