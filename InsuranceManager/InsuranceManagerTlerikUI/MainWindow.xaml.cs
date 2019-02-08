using InsuranceManager.Models;
using InsuranceManager.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
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
using InsuranceManagerTlerikUI.Code;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.Input;
using Telerik.Windows.Data;
using System.ComponentModel;

namespace InsuranceManagerTlerikUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        List<Accident> accidents;
        ObservableCollection<AccidentUtil> accidentView;
        public MainWindow()
        {
            InitializeComponent();
            using (InsuranceManager.DataAccess.DataContext context = new InsuranceManager.DataAccess.DataContext())
            {
                accidents = context.Accidents.ToList();
            }

            
            accidentView = new ObservableCollection<AccidentUtil>();
            foreach (var item in accidents)
            {
                accidentView.Add(new AccidentUtil(item));
            }
            this.accidentsGrid.ItemsSource = accidentView;
            this.DataContext = accidentView;
            
        }

        private void RadGridView_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {

        }

        private void HandlerForChange(object sender, RoutedEventArgs args)
        {
            
        }

        private void GridViewColumn_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        //private static ObservableCollection<T> GetObservable<T, U>(this IEnumerable<U> items, Func<U, T> map) where T: class
        //{
        //    return items.Aggregate(new ObservableCollection<T>(), (acc, i) =>
        //    {
        //        acc.Add(map(i));
        //        return acc;
        //    });
        //}
    }
}
