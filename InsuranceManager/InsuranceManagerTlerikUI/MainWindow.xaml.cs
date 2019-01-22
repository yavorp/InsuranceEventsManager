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

namespace InsuranceManagerTlerikUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Accident> accidents;
            using (InsuranceManager.DataAccess.DataContext context = new InsuranceManager.DataAccess.DataContext())
            {
                accidents = context.Accidents.ToList();
            }
            LblTest.Content = accidents[0].FirstName + ' ' + accidents[0].LastName + ' ' + accidents[0].Description;
            

        }
    }
}
