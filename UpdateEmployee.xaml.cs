using musicSchoolWPFMVVM.Model;
using musicSchoolWPFMVVM.ViewModel;
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
using System.Windows.Shapes;

namespace musicSchoolWPFMVVM
{
    /// <summary>
    /// Логика взаимодействия для UpdateEmployee.xaml
    /// </summary>
    public partial class UpdateEmployee : Window
    {
        public UpdateEmployee(Employee employee)
        {
            InitializeComponent();
            DataContext = new DataMangeVM();
            DataMangeVM.SelectedEmployee = employee;
            DataMangeVM.fio = employee.Fio;
            DataMangeVM.telephone = employee.Telephone;
            DataMangeVM.gender = employee.Gender;
            DataMangeVM.experience = employee.Experience;
            DataMangeVM.email = employee.Email;
        }
    }
}
