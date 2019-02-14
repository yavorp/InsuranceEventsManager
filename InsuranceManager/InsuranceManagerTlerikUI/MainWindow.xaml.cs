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
        ObservableCollection<AccidentUtil> accidentView = new ObservableCollection<AccidentUtil>();
        ObservableCollection<AccidentUtil> accidentsToBeHandled = new ObservableCollection<AccidentUtil>();
        ObservableCollection<AccidentUtil> handledAccidents = new ObservableCollection<AccidentUtil>();
        List<CustomClass> statistics = new List<CustomClass>();
        //Accidents that are not handled are with index=0, accidents to be handled are with index=1, handled accidents index=2
        public MainWindow()
        {
            InitializeComponent();

            System.Threading.Tasks.Task.Run(async () =>
            {
                accidentView = await GetAccidentsAsync();
                accidentsToBeHandled = await GeetAccidentsToBeHandledAsync();
                handledAccidents = await GeetAccidentsHandledAsync();
                accidentsGrid.Dispatcher.Invoke(() =>
                {
                    accidentsGrid.ItemsSource = accidentView;
                    statistics.Add(new CustomClass() { StringVal = "Инциденти, които не са обработени", Value = accidentView.Count });
                    accidentsGrid.DataContext = accidentView;
                    statistics.Add(new CustomClass() { Value = accidentsToBeHandled.Count, StringVal = "Събития, които ще се обработват" });
                    statistics.Add(new CustomClass() { Value = handledAccidents.Count, StringVal = "Обработени събития" });

                    accidentsToBeHandledGrid.ItemsSource = accidentsToBeHandled;
                    TxtUnhandledEvents.DataContext = statistics[0];
                    TxtToBeHandledEvents.DataContext = statistics[1];
                    TxtHandledEvents.DataContext = statistics[2];
                    
                });

            });

        }
        private void RadGridView_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {

        }

        private async System.Threading.Tasks.Task<ObservableCollection<AccidentUtil>> GetAccidentsAsync()
        {
            List<Accident> accidents;

            using (InsuranceManager.DataAccess.DataContext context = new InsuranceManager.DataAccess.DataContext())
            {
                accidents = await context.Accidents.Where(a => a.Status != Status.ToBeHandled && a.Status != Status.Handled).ToListAsync();
            }

            return accidents.GetObservable(a =>
              {
                  var item = new AccidentUtil(a);
                  item.PropertyChanged += HandlerForChange;
                  return item;
              });
        }

        private async System.Threading.Tasks.Task<ObservableCollection<AccidentUtil>> GeetAccidentsToBeHandledAsync()
        {
            List<Accident> accidents;
            using (InsuranceManager.DataAccess.DataContext context = new InsuranceManager.DataAccess.DataContext())
            {
                accidents = await context.Accidents.Where(a => a.Status == Status.ToBeHandled).ToListAsync();
            }
            return accidents.GetObservable(a =>
            {
                var item = new AccidentUtil(a);
                item.PropertyChanged += HandlerForChange;
                return item;
            });
        }

        private void HandledEvent()
        {
            System.Threading.Tasks.Task.Run(async () =>
            {
                accidentsToBeHandled = await GeetAccidentsToBeHandledAsync();
                handledAccidents = await GeetAccidentsHandledAsync();
                accidentsToBeHandledGrid.Dispatcher.Invoke(() =>
                {
                    accidentsToBeHandledGrid.ItemsSource = accidentsToBeHandled;
                    statistics[1].Value = accidentsToBeHandled.Count;
                    statistics[2].Value = handledAccidents.Count;

                });

            });

        }

        private async System.Threading.Tasks.Task<ObservableCollection<AccidentUtil>> GeetAccidentsHandledAsync()
        {
            List<Accident> accidents;
            using (InsuranceManager.DataAccess.DataContext context = new InsuranceManager.DataAccess.DataContext())
            {
                accidents = await context.Accidents.Where(a => a.Status == Status.Handled).ToListAsync();
            }
            return accidents.GetObservable(a =>
            {
                var item = new AccidentUtil(a);
                item.PropertyChanged += HandlerForChange;
                return item;
            });
        }

        //Updates database and UI grids EventsToBeHandled and Events that are not Handled
        private async void HandlerForChange(object sender, EventArgs args)
        {
            AccidentUtil accident = (AccidentUtil)sender;
            using (InsuranceManager.DataAccess.DataContext context = new InsuranceManager.DataAccess.DataContext())
            {
                var accidentDb = await context.Accidents.FirstAsync(a => a.Id == accident.ID);
                accidentDb.Status = (Status)accident.StatusId;
                accidentDb.LastModified = DateTime.Now;
                accident.LastModified = AccidentUtil.DateToString(accidentDb.LastModified);
                context.SaveChanges();
                if (accidentDb.Status == Status.ToBeHandled && !accidentsToBeHandled.Contains(accident))
                {
                    accidentView.Remove(accident);
                    statistics[0].Value = accidentView.Count;
                    accidentsToBeHandled.Add(accident);
                    statistics[1].Value = accidentsToBeHandled.Count;
                }

            }
        }

        private void OpenWindow()
        {
            HandleWindow window = new HandleWindow();
            Dispatcher.Invoke(() => { window.accidentToBeHandled = accidentsToBeHandledGrid.SelectedItem as AccidentUtil; });
            window.myEventHandler += HandledEvent;
            window.Show();
            System.Windows.Threading.Dispatcher.Run();
        }

        private void BtnSendAccidentToHandleWindow_Click(object sender, RoutedEventArgs e)
        {
            var item = accidentsToBeHandledGrid.SelectedItem;
            if (item != null)
            {
                Thread newWindowThread = new Thread(new ThreadStart(OpenWindow));
                newWindowThread.SetApartmentState(ApartmentState.STA);
                newWindowThread.IsBackground = true;
                newWindowThread.Start();
            }
            else
            {
                MessageBox.Show("Изберете събитие за обработка");
            }
        }

    }

    //helper class to visualize info in Pie chart for statistics
    public class CustomClass
    {
        public int Value { get; set; }
        public string StringVal { get; set; }
        public override string ToString()
        {
            return $"{StringVal} : {Value}";
        }
    }
}
