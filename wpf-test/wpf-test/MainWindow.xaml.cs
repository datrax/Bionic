using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace wpf_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        class student
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string hui;

        }
  
        public MainWindow()
        {
            InitializeComponent();
           // ObservableCollection<student> coll = new ObservableCollection<student>();
            List<object> _customers = new List<object>();
         //   coll.Add(new student() { Name = "Sasha", Surname = "makaryk" ,hui="sdggd"});
            _customers.Add(new student() { Name = "Sasha", Surname = "makaryk",hui="sdg" });



            //Datagr1.Items.Add(sd);
            Datagr1.ItemsSource = _customers;


        }
    }



}


