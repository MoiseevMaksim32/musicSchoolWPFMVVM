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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace musicSchoolWPFMVVM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DataGrid AllStudentDTOView;
        public static DataGrid AllEmployeeDTOView;
        public static DataGrid AllConcertDTOView;
        public static ComboBox AllCBGroupMusic;
        public static ComboBox AllCBSpeciality;
        public static ComboBox AllCBPosition;
        public static ComboBox AllCBGroupMusicConcert;
        public static ComboBox AllCBGenres;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataMangeVM();
            AllStudentDTOView = DataStudent;
            AllEmployeeDTOView = DataEmployee;
            AllConcertDTOView = DataConcert;
            AllCBGroupMusic = CBGroupMusic;
            AllCBSpeciality = CBSpeciality;
            AllCBPosition = CBPosition;
            AllCBGroupMusicConcert = CBGroupMusicConcert;
            AllCBGenres = CBGenreConcert;
        }
    }
}
