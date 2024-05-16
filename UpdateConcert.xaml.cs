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
    /// Логика взаимодействия для UpdateConcert.xaml
    /// </summary>
    public partial class UpdateConcert : Window
    {
        public UpdateConcert(Concert concert)
        {
            InitializeComponent();
            DataContext = new DataMangeVM();
            DataMangeVM.SelectedConcert = concert;
            DataMangeVM.concertDate = concert.ConcertDate;
        }
    }
}
