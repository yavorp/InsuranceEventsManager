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
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;

namespace InsuranceManagerTlerikUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        List<Accident> accidents;
        ObservableCollection<AccidentUtil> accidentView = new ObservableCollection<AccidentUtil>();
        public MainWindow()
        {
            InitializeComponent();
            System.Threading.Tasks.Task.Run(async () =>
            {
                Thread.Sleep(9000);
                accidentView = await GetAccidentsAsync();
                accidentsGrid.Dispatcher.Invoke(() =>
                {
                    accidentsGrid.ItemsSource = accidentView;
                    accidentsGrid.DataContext = accidentView;
                });
            });
        }

        private void RadGridView_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {

        }
        async System.Threading.Tasks.Task<ObservableCollection<AccidentUtil>> GetAccidentsAsync()
        {
            using (InsuranceManager.DataAccess.DataContext context = new InsuranceManager.DataAccess.DataContext())
            {
                accidents = await context.Accidents.ToListAsync();
            }

            return accidents.GetObservable(a =>
            {
                var item = new AccidentUtil(a);
                item.PropertyChanged += HandlerForChange;
                return item;
            });
        }


        private async void HandlerForChange(object sender, EventArgs args)
        {
            AccidentUtil accident = (AccidentUtil)sender;
            using (InsuranceManager.DataAccess.DataContext context = new InsuranceManager.DataAccess.DataContext())
            {
                var accidentDb = await context.Accidents.FindAsync(accident.ID);
                accidentDb.Status = (Status)accident.StatusId;
                accidentDb.LastModified = DateTime.Now;
                var f = (AccidentUtil)sender;
                f.LastModified = AccidentUtil.DateToString(accidentDb.LastModified);
                await context.SaveChangesAsync();
            }
        }

        private void OpenWindow()
        {
            HandleWindow window = new HandleWindow();
            window.Show();
        }

        private void OpenNewWindowHandler(object sender, EventArgs args)
        {
            System.Threading.Tasks.Task.Factory.StartNew(OpenWindow);
        }


    }

    public static class Ext
    {
        public static ObservableCollection<T> GetObservable<T, U>(this IEnumerable<U> items, Func<U, T> map) where T : class
        {
            return items.Aggregate(new ObservableCollection<T>(), (acc, i) =>
            {
                acc.Add(map(i));
                return acc;
            });
        }
    }

    internal class SomeInfo
    {
        public int Value { get; set; }
        public string Label { get; set; }
    }
}
