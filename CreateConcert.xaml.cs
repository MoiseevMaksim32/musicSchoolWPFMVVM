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
    /// Логика взаимодействия для CreateConcert.xaml
    /// </summary>
    public partial class CreateConcert : Window
    {
        public CreateConcert()
        {
            InitializeComponent();
            DataContext = new DataMangeVM();
        }
    }
}
