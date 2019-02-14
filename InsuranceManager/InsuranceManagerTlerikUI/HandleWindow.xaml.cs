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
    /// Interaction logic for HandleWindow.xaml
    /// </summary>
    /// 
    public delegate void MyEventHandler();
    public partial class HandleWindow : Window
    {
        public AccidentUtil accidentToBeHandled;
        private WorkshopUtil currentWorkshop;
        private MechanicUtil currentMechanic;
        private ObservableCollection<WorkshopUtil> workshopsView;
        public HandleWindow()
        {
            InitializeComponent();
            System.Threading.Tasks.Task.Run(async () =>
            {
                workshopsView = await GetAccidentsAsync();
                CmbWorkshop.Dispatcher.Invoke(() =>
                {
                    CmbWorkshop.ItemsSource = workshopsView;
                });
            });
        }



        private async System.Threading.Tasks.Task<ObservableCollection<WorkshopUtil>> GetAccidentsAsync()
        {
            List<Workshop> workshops;

            using (InsuranceManager.DataAccess.DataContext context = new InsuranceManager.DataAccess.DataContext())
            {
                workshops = await context.Workshops.Include(t => t.Mechanics.Select(m => m.Tasks)).ToListAsync();
            }

            return workshops.GetObservable(a =>
            {
                var item = new WorkshopUtil(a);
                return item;
            });
        }
        public MyEventHandler myEventHandler;

        async void ChangeAccidentProp()
        {
            using (var context = new DataContext())
            {
                var accidentDb = context.Accidents.SingleOrDefault(a => a.Id == accidentToBeHandled.ID);
                accidentDb.Status = Status.Handled;
                accidentDb.LastModified = DateTime.Now;
                context.SaveChanges();
            }
        }
        private void BtnHandleEvenet_Click(object sender, RoutedEventArgs e)
        {
            TaskUtil task = new TaskUtil();
            uint days;
            
                
            if(uint.TryParse(TxtBoxNumberOfDays.Text, out days))
            {
                if (DateTimePickerStartDate.SelectedDate != null && DateTimePickerStartDate.SelectedDate>=DateTime.Now)
                {
                    task.StartDate = DateTimePickerStartDate.SelectedDate.Value;
                    task.EndDate = task.StartDate.AddDays(days);
                }
                else
                {
                    MessageBox.Show("Въведи валидна дата");
                    return;
                }
                if (currentMechanic.CanBeAdded(task))
                {
                    using (var context = new InsuranceManager.DataAccess.DataContext())
                    {
                        var mechanic = context.Mechanics.Include(m=>m.Tasks).SingleOrDefault(m => m.Id == currentMechanic.Id);
                        var taskDb = new InsuranceManager.Models.Task
                        {
                            StartDate = task.StartDate,
                            EndDate = task.EndDate
                        };
                        mechanic?.Tasks?.Add(taskDb);
                        context.SaveChanges();
                        ChangeAccidentProp();
                    }
                    myEventHandler?.Invoke();
                }
                else
                {
                    MessageBox.Show("Две събития се препокриват");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Въведи валидна продължителност");
                return;
            }

        }

        private void CmbWorkshop_DropDownClosed(object sender, EventArgs e)
        {
            currentWorkshop = CmbWorkshop.SelectedItem as WorkshopUtil;
            if (currentWorkshop != null)
            {
                CmbMechanic.ItemsSource = currentWorkshop.Mechanics;
                CmbMechanic.IsEnabled = true;
            }
        }

        private void CmbMechanic_DropDownClosed(object sender, EventArgs e)
        {
            currentMechanic = CmbMechanic.SelectedItem as MechanicUtil;
            if (currentMechanic != null)
            {
                List<DateTime> blackOutDates = new List<DateTime>();
                foreach (var item in currentMechanic.Tasks)
                {
                    
                    int i = 0;
                    DateTime time = item.StartDate;
                    while (time <= item.EndDate)
                    {
                        blackOutDates.Add(time);
                        time = time.AddDays(1);
                    }
                }
                DateTimePickerStartDate.BlackoutDates = blackOutDates;
                DateTimePickerStartDate.IsEnabled = true;
            }
        }

        private void DateTimePickerStartDate_LostFocus(object sender, RoutedEventArgs e)
        {
            TxtBoxNumberOfDays.IsEnabled = true;
        }

        private void CloseWindows_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
