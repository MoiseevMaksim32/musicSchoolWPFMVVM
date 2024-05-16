using musicSchoolWPFMVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace musicSchoolWPFMVVM.ViewModel
{
    public class DataMangeVM : INotifyPropertyChanged
    {
        // - поля для студента
        public static int groupMusicID { get; set; }
        public static int specilityID { get; set; }
        public static int positionID {  get; set; }
        public static int genreID { get; set; }
        public static string fio { get; set; }
        public static string telephone { get; set; }
        public static bool gender { get; set; }
        public static int experience { get; set; }
        public static string email { get; set; }
        public static DateTime concertDate { get; set; }


        // - свойства для выделенных элементов
        public TabItem SelectedTabItem { get; set; }
        public static Student SelectedStudent { get; set; } 
        public static Employee SelectedEmployee { get; set; }
        public static Concert SelectedConcert { get; set; }
        public static GroupMusic SelectedGroupMusic { get; set; }
        public static Speciality SelectedSpeciality { get; set; }
        public static Position SelectedPosition { get;set; }
        public static Genres SelectedGenres { get; set; }
        public static StudentDTO SelectedStudentDTO { get; set; }
        public static EmployeeDTO SelectedEmployeeDTO { get; set; }
        public static ConcertDTO SelectedConcertDTO { get; set; }
        public static string TextBoxSearchStudent { get; set; }


        //  - лист студентво + событие по отображению
        private List<Student> allStudent = DataModel.GetAllModels<Student>("student");
        public List<Student> AllStudent { get { return allStudent; } set { allStudent = value; NotifyPropertyChanged("AllStudent"); } }

        // - лист для специальностей + событие по отображению 

        private List<Speciality> allSpeciality = DataModel.GetAllModels<Speciality>("speciality");
        public List<Speciality> AllSpeciality { get { return allSpeciality; } set { allSpeciality = value; NotifyPropertyChanged("AllSpeciality"); } }


        // - Лист мызыкальных групп + событие для отображения элементов
        private List<GroupMusic> allGroupMusic = DataModel.GetAllModels<GroupMusic>("group_music");
        public List<GroupMusic> AllGroupMusic { get { return allGroupMusic; } set { allGroupMusic = value; NotifyPropertyChanged("AllGroupMusic"); } }

        // - Листа для DTO студентов для правильного отображения 
        private List<StudentDTO> allStudentDTO = DataModel.getAllStudentDTO();
        public List<StudentDTO> AllStudentDTO { get { return allStudentDTO; } set { allStudentDTO = value; NotifyPropertyChanged("AllStudentDTO"); } }

        private List<Employee> allEmployee = DataModel.GetAllModels<Employee>("employee");
        public List<Employee> AllEmployee { get { return allEmployee; } set { allEmployee = value; NotifyPropertyChanged("AllEmployee"); }  }

        // - лист для DTO работников для правильного отобраяжения 
        private List<EmployeeDTO> allEmployeeDTO  = DataModel.getAllEmployeeDTO();
        public List<EmployeeDTO> AllEmployeeDTO { get { return allEmployeeDTO; } set { allEmployeeDTO = value;  NotifyPropertyChanged("AllEmployeeDTO"); } }
        private List<Concert> allConcert = DataModel.GetAllModels<Concert>("concert");
        public List<Concert> AllConcert { get { return allConcert; } set { allConcert = value; NotifyPropertyChanged("AllConcert"); } }

        // - Дист для DTO концертов для правильного отображения 
        private List<ConcertDTO>  allConcertDTO = DataModel.getAllConcertDTO();
        public List<ConcertDTO> AllConcertDTO { get { return allConcertDTO; } set { allConcertDTO = value; NotifyPropertyChanged("AllConcertDTO"); } }

        private List<Position> allPosition = DataModel.GetAllModels<Position>("position");
        public List<Position> AllPosition { get { return allPosition; } set { allPosition = value; NotifyPropertyChanged("AllPosition"); } }

        private List<Genres> allGenres = DataModel.GetAllModels<Genres>("genres");
        public List<Genres> AllGenres { get { return allGenres; } set { allGenres = value; NotifyPropertyChanged("AllGenres"); } }

        // - создание студентов 

        private RelayCommand createNewStudent;
        public RelayCommand CreateNewStudent
        {
            get
            {
                return createNewStudent ?? new RelayCommand( obj =>
                {
                    Window wnd = obj as Window;
                    Student student = new Student()
                    {
                       ID = 0,
                       GroupMusicID = SelectedGroupMusic.ID,
                       SpecilityID = SelectedSpeciality.ID,
                       Fio = fio,
                       Telephone = telephone,
                       Gender = gender,
                       Email = email,
                    };
                    DataModel.CreateModels<Student>("student", student);
                    UpdateAllDataView();
                    SetNullValuesToParameter();
                    wnd.Close();
                });
            }
        }

        // - создание работников 
        private RelayCommand createNewEmployee;
        public RelayCommand CreateNewEmployee
        {
            get
            {
                return createNewEmployee ?? new RelayCommand(obj => {
                    Window wnd = obj as Window;
                    Employee employee = new Employee() {
                        ID = 0,
                        PositionID = SelectedPosition.ID,
                        Fio = fio,
                        Telephone = telephone,
                        Gender = gender,
                        Experience = experience,
                        Email = email,
                    };
                    DataModel.CreateModels<Employee>("employee", employee);
                    UpdateAllDataView(); 
                    SetNullValuesToParameter(); 
                    wnd.Close();
                });
            }
        }

        // - создания концерта 
        private RelayCommand createNewConcert;
        public RelayCommand CreateNewConcert
        {
            get
            {
                return createNewConcert ?? new RelayCommand(obj => {
                    Window wnd = obj as Window;
                    Concert concert = new Concert() {
                    ID= 0,
                    GroupMusicID = SelectedGroupMusic.ID,
                    GenreID = SelectedGenres.ID,
                    ConcertDate = concertDate,
                    };
                    DataModel.CreateModels<Concert>("concert", concert);
                    UpdateAllDataView();
                    SetNullValuesToParameter();
                    wnd.Close();
                });
            }
        }

        // Удаление всех элементов 

        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj => {
                    if (SelectedTabItem.Name == "StudentTab" && SelectedStudentDTO != null)
                    {
                        DataModel.DeleteModels<Student>("student", SelectedStudentDTO.ID);
                        UpdateAllDataView();
                    }
                    else if (SelectedTabItem.Name == "DataEmployee" && SelectedEmployeeDTO != null)
                    {
                        DataModel.DeleteModels<Employee>("employee", SelectedEmployeeDTO.ID);
                        UpdateAllDataView();
                    }
                    else if (SelectedTabItem.Name == "DataConcert" && SelectedConcertDTO != null)
                    {
                        DataModel.DeleteModels<Concert>("concert", SelectedConcertDTO.ID);
                        UpdateAllDataView();
                    }
                    // дальше добавляем проверки и логику удаления для будующих таблиц
                    SetNullValuesToParameter() ;
                });
            }
        }

        // - Изменения элеметнов студентов

        private RelayCommand editStudent;
        public RelayCommand EditStudent
        {
            get
            {
                return editStudent ?? new RelayCommand(obj => {
                    Window window = obj as Window;
                    Student student = new Student()
                    {
                        ID = SelectedStudent.ID,
                        GroupMusicID = SelectedGroupMusic.ID,
                        SpecilityID = SelectedSpeciality.ID,
                        Fio = fio,
                        Telephone = telephone,
                        Gender = gender,
                        Email = email,
                    };
                    DataModel.UpdateModel<Student>("student", student);
                    SetNullValuesToParameter();
                    UpdateAllDataView();
                    window.Close();
                });
            }
        }

        // Изменение элементов работников
        private RelayCommand editEmployee;
        public RelayCommand EditEmployee
        {
            get
            {
                return editEmployee ?? new RelayCommand(obj => {
                    Window window = obj as Window;
                    Employee employee = new Employee()
                    {
                        ID = SelectedEmployee.ID,
                        PositionID = SelectedPosition.ID,
                        Fio = fio,
                        Telephone = telephone,
                        Gender = gender,
                        Experience = experience,
                        Email = email,
                    };
                    DataModel.UpdateModel<Employee>("employee", employee);
                    SetNullValuesToParameter();
                    UpdateAllDataView();
                    window.Close();
                });
            }
        }

        // Изменение элементов сокцерта
        private RelayCommand editConcert;
        public RelayCommand EditConcert
        {
            get
            {
                return edirConcert ?? new RelayCommand(obj => {
                    Window window = obj as Window;
                   Concert concert = new Concert()
                    {
                        ID = SelectedConcert.ID,
                        GroupMusicID = SelectedGroupMusic.ID,
                        GenreID = SelectedGenres.ID,
                        ConcertDate = concertDate,
                    };
                    DataModel.UpdateModel<Concert>("concert", concert);
                    SetNullValuesToParameter();
                    UpdateAllDataView();
                    window.Close();
                });
            }
        }

        // Изменение элементов концерта
        private RelayCommand edirConcert;
        public RelayCommand EdirConcert
        {
            get
            {
                return edirConcert ?? new RelayCommand(obj => {
                    Window window = obj as Window;
                    Concert concert = new Concert()
                    {
                        ID = SelectedConcertDTO.ID,
                        GroupMusicID = SelectedGroupMusic.ID,
                        GenreID = SelectedGenres.ID,
                        ConcertDate = concertDate,
                    };
                    DataModel.UpdateModel<Concert>("concert", concert);
                    SetNullValuesToParameter();
                    UpdateAllDataView();
                    window.Close();
                });
            }
        }

        // - Фильтрация данных
        private RelayCommand filtratuinStudent;
        public RelayCommand FiltratuinStudent
        {
            get
            {
                return filtratuinStudent ?? new RelayCommand(obj => {
                    AllStudentDTO = DataModel.getAllStudentDTO();
                    if (SelectedGroupMusic!=null)
                        AllStudentDTO = AllStudentDTO.Where(x => x.GroupMusicStr.Equals(SelectedGroupMusic.GroupMusicName)).ToList();
                    if (SelectedSpeciality != null)
                        AllStudentDTO = AllStudentDTO.Where(x => x.SpecilityStr.Equals(SelectedSpeciality.SpecialityName)).ToList();
                    MainWindow.AllStudentDTOView.ItemsSource = AllStudentDTO;
                });
            }
        }

        private RelayCommand filtratuinEmployee;
        public RelayCommand FiltratuinEmployee
        {
            get
            {
                return filtratuinEmployee ?? new RelayCommand(obj => { 
                AllEmployeeDTO = DataModel.getAllEmployeeDTO();
                    if(SelectedPosition!= null)
                    {
                        AllEmployeeDTO = AllEmployeeDTO.Where(x => x.PositionStr.Equals(SelectedPosition.PositionName)).ToList();
                    }
                    MainWindow.AllEmployeeDTOView.ItemsSource = AllEmployeeDTO;
                });
            }
        }

        private RelayCommand filtratuinConcert;
        public RelayCommand FiltratuinConcert
        {
            get
            {
                return filtratuinConcert ?? new RelayCommand(obj => {
                AllConcertDTO = DataModel.getAllConcertDTO();
                    if (SelectedGroupMusic != null)
                    {
                        AllConcertDTO = AllConcertDTO.Where(x => x.GroupMusicStr.Equals(SelectedGroupMusic.GroupMusicName)).ToList();
                    }
                    if(SelectedGenres != null)
                    {
                        AllConcertDTO = AllConcertDTO.Where(x => x.GenresStr.Equals(SelectedGenres.GenreName)).ToList();
                    }
                    MainWindow.AllConcertDTOView.ItemsSource = AllConcertDTO;
                });
            }
        }

        // - Отмена фильтрации
        private RelayCommand noFiltratuin;
        public RelayCommand NoFiltratuin
        {
            get
            {
                return noFiltratuin ?? new RelayCommand(obj => {
                    UpdateAllDataView();
                    SetNullValuesToParameter();
                });
            }
        }

        // - попытка сделать поиск
        private string searchStudent;
        public string SearchStudent
        {
            set
            {
                searchStudent = value;
                AllStudentDTO = DataModel.getAllStudentDTO();
                if (!searchStudent.Equals(""))
                {
                     AllStudentDTO = AllStudentDTO.Where(x => x.GroupMusicStr.StartsWith(searchStudent) || x.SpecilityStr.StartsWith(searchStudent) || x.Fio.StartsWith(searchStudent) || x.Telephone.StartsWith(searchStudent) || x.Email.StartsWith(searchStudent) || x.GenderStr.StartsWith(searchStudent)).ToList();
                    MainWindow.AllStudentDTOView.ItemsSource = AllStudentDTO;
                }
                else
                {
                    AllStudentDTO = DataModel.getAllStudentDTO();
                    MainWindow.AllStudentDTOView.ItemsSource = AllStudentDTO;
                    MainWindow.AllCBSpeciality.SelectedItem = null;
                    MainWindow.AllCBGroupMusic.SelectedItem = null;
                }
            }
            get { return searchStudent; }
        }

        private string searchEmployee;
        public string SearchEmployee
        {
            set
            {
                searchEmployee = value;
                AllEmployeeDTO = DataModel.getAllEmployeeDTO();
                if (!searchEmployee.Equals(""))
                {
                    AllEmployeeDTO = AllEmployeeDTO.Where(x => x.PositionStr.StartsWith(searchEmployee) || x.Fio.StartsWith(searchEmployee) || x.Telephone.StartsWith(searchEmployee) || x.GenderStr.StartsWith(searchEmployee) || x.Experience.ToString().StartsWith(searchEmployee) || x.Email.StartsWith(searchEmployee)).ToList();
                    MainWindow.AllEmployeeDTOView.ItemsSource = AllEmployeeDTO;
                }
                else
                {
                    AllEmployeeDTO = DataModel.getAllEmployeeDTO();
                    MainWindow.AllEmployeeDTOView.ItemsSource = AllEmployeeDTO;
                    MainWindow.AllCBPosition.SelectedItem = null;
                }
            }
        }

        private string searchConcert;
        public string SearchConcert
        {
            set
            {
                searchConcert = value;
                AllConcertDTO = DataModel.getAllConcertDTO();
                if (!searchConcert.Equals(""))
                {
                    AllConcertDTO = AllConcertDTO.Where(x => x.GroupMusicStr.StartsWith(searchConcert) || x.GenresStr.StartsWith(searchConcert) || x.ConcertDate.ToString().StartsWith(searchConcert)).ToList();
                    MainWindow.AllConcertDTOView.ItemsSource = AllConcertDTO;
                }
                else
                {
                    AllConcertDTO = DataModel.getAllConcertDTO();
                    MainWindow.AllConcertDTOView.ItemsSource = AllConcertDTO;
                    MainWindow.AllCBGroupMusicConcert.SelectedItem = null;
                    MainWindow.AllCBGenres.SelectedItem = null;
                }
            }
        }
        // - Открытие окна создания 

        private RelayCommand openCreateStudentWnd;
        public RelayCommand OpenCreateStudentWnd
        {
            get
            {
                return openCreateStudentWnd ?? new RelayCommand(obj =>
                {
                    OpenCreateStudentWindowsMethod();
                });
            }
        }
        private void OpenCreateStudentWindowsMethod()
        {
            CreateStudent newCreateStudent = new CreateStudent();
            SetCenterPositionAndOpen(newCreateStudent);
        }

        // Открытие окна создания работника 
        private RelayCommand openCreateEmployeeWnd;
        public RelayCommand OpenCreateEmployeeWnd
        {
            get
            {
                return openCreateEmployeeWnd ?? new RelayCommand(obj => {
                OpenCreateEmployeeWindowsMethod();
                });
            }
        }
        private void OpenCreateEmployeeWindowsMethod()
        {
            CreateEmployee newCreateEmployee = new CreateEmployee();
            SetCenterPositionAndOpen(newCreateEmployee);
        }

        // Открытие окна создания концертов
        private RelayCommand openCreateConcertWnd;
        public RelayCommand OpenCreateConcertWnd
        {
            get
            {
                return openCreateConcertWnd ?? new RelayCommand(obj => {
                    OpenCreateConcertWindowsMethod();
                });
            }
        }
        private void OpenCreateConcertWindowsMethod()
        {
            CreateConcert newCreateConcert = new CreateConcert();
            SetCenterPositionAndOpen(newCreateConcert);
        }

        // - Открытие окна изменения записей 

        private RelayCommand openUpdateStudentWnd;
        public RelayCommand OpenUpdateStudentWnd
        {
            get
            {
                return openUpdateStudentWnd ?? new RelayCommand(obj => { 
                    OpenUpdateStudentWindowsMethod();
                });
            }
        }

        private void OpenUpdateStudentWindowsMethod()
        {
           int lenComboBoxSpecility = 0;
           int lenComboBoxGroupMusicID = 0;
            SelectedStudent = AllStudent.Where(x => x.ID.Equals(SelectedStudentDTO.ID)).First();
            foreach (var i in AllSpeciality)
            {
                if (SelectedStudent.SpecilityID.Equals(i.ID)) {
                    specilityID = lenComboBoxSpecility;
                } // Получения id для отображения в comboBox
                lenComboBoxSpecility++;
            }
            foreach (var i in AllGroupMusic)
            {
                if (SelectedStudent.GroupMusicID.Equals(i.ID)) {
                    groupMusicID = lenComboBoxGroupMusicID;
                }
                lenComboBoxGroupMusicID++;
            }
            MessageBox.Show(specilityID.ToString());
            MessageBox.Show(groupMusicID.ToString());
            UpdateStudent nuwUpdateStudent = new UpdateStudent(SelectedStudent);
            SetCenterPositionAndOpen(nuwUpdateStudent);
        }

        // - Открытие окна изменения записей работника 
        private RelayCommand openUpdatEmployeeWnd;
        public RelayCommand OpenUpdateEmployeeWnd
        {
            get
            {
                return openUpdateStudentWnd ?? new RelayCommand(obj => {
                    OpenUpdateEmployeeWindowsMethod();
                });
            }
        }

        private void OpenUpdateEmployeeWindowsMethod()
        {
            int lenComboBoxPostion = 0;
            SelectedEmployee = AllEmployee.Where(x => x.ID.Equals(SelectedEmployeeDTO.ID)).First();
            foreach (var i in AllPosition)
            {
                if (SelectedEmployee.PositionID.Equals(i.ID))
                {
                    positionID = lenComboBoxPostion;
                }
                lenComboBoxPostion++;
            }
            UpdateEmployee nuwUpdateEmployee = new UpdateEmployee(SelectedEmployee);
            SetCenterPositionAndOpen(nuwUpdateEmployee);
        }

        // - Открытие окна изменения записей концертов
        private RelayCommand openUpdateConcertWnd;
        public RelayCommand OpenUpdateConcertWnd
        {
            get
            {
                return openUpdateConcertWnd ?? new RelayCommand(obj => {
                    OpenUpdateConcertWindowsMethod();
                });
            }
        }

        private void OpenUpdateConcertWindowsMethod()
        {
            int lenComboBoGroupMusicID = 0;
            int lenComboBoxGenres = 0;
            SelectedConcert = AllConcert.Where(x => x.ID.Equals(SelectedConcertDTO.ID)).First();
            foreach (var i in AllGroupMusic)
            {
                if (SelectedConcert.GroupMusicID.Equals(i.ID))
                {
                    groupMusicID = lenComboBoGroupMusicID;
                }
                lenComboBoGroupMusicID++;
            }
            foreach (var i in AllGenres)
            {
                if (SelectedConcert.GenreID.Equals(i.ID))
                {
                    genreID = lenComboBoxGenres;
                }
                lenComboBoxGenres++;
            }
            UpdateConcert nuwUpdateConcert = new UpdateConcert(SelectedConcert);
            SetCenterPositionAndOpen(nuwUpdateConcert);
        }

        // - установка окна по центру 

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = System.Windows.Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        // - метод задания null значений для переменых

        private void SetNullValuesToParameter()
        {
            // параметры студента
            SelectedStudent = null;
            SelectedSpeciality = null;
            SelectedGroupMusic = null;
            SelectedPosition = null;
            SelectedConcert = null;
            SelectedEmployee = null;
            SelectedGenres = null;
            fio = null;
            telephone = null;
            gender = false;
            experience = 0;
            email = null;
          //  concertDate = null;
        }

      
        // - методы для обновления событий 

        private void UpdateAllDataView()
        {
            Thread.Sleep(500);// Это убого и выглядит как кастыль надо думать как переделывать !!!!!!!!!
            UpdateAllStudentView();
            UpdateAllPositionView();
            UpdateAllConcertView();
        }
        // - метод обновления DataGrid
        private void UpdateAllStudentView()
        {
            
            AllStudent = DataModel.GetAllModels<Student>("student");
            // AllGroupMusic = DataModel.GetAllModels<GroupMusic>("group_music");
            // AllSpeciality = DataModel.GetAllModels<Speciality>("speciality");

            AllStudentDTO = DataModel.getAllStudentDTO();
            MainWindow.AllCBGroupMusic.ItemsSource = null;
            MainWindow.AllCBSpeciality.ItemsSource = null;
            MainWindow.AllStudentDTOView.ItemsSource = null;

            MainWindow.AllCBGroupMusic.Items.Clear();
            MainWindow.AllCBSpeciality.Items.Clear();
            MainWindow.AllStudentDTOView.Items.Clear();

            MainWindow.AllCBGroupMusic.ItemsSource = AllGroupMusic;
            MainWindow.AllCBSpeciality.ItemsSource = AllSpeciality;
            MainWindow.AllStudentDTOView.ItemsSource = AllStudentDTO;

            MainWindow.AllCBSpeciality.Items.Refresh();
            MainWindow.AllCBGroupMusic.Items.Refresh();
            MainWindow.AllStudentDTOView.Items.Refresh();

            MainWindow.AllCBSpeciality.SelectedItem = null;
            MainWindow.AllCBGroupMusic.SelectedItem = null;
        }

        private void UpdateAllPositionView()
        {
            AllEmployee = DataModel.GetAllModels<Employee>("employee");
            AllEmployeeDTO = DataModel.getAllEmployeeDTO();
            MainWindow.AllCBPosition.ItemsSource = null;
            MainWindow.AllEmployeeDTOView.ItemsSource = null;

            MainWindow.AllCBPosition.Items.Clear();
            MainWindow.AllEmployeeDTOView.Items.Clear();

            MainWindow.AllCBPosition.ItemsSource = AllPosition;
            MainWindow.AllEmployeeDTOView.ItemsSource = AllEmployeeDTO;

            MainWindow.AllCBPosition.Items.Refresh();
            MainWindow.AllEmployeeDTOView.Items.Refresh();

            MainWindow.AllCBPosition.SelectedItem = null;
        }

        private void UpdateAllConcertView()
        {
            AllConcert = DataModel.GetAllModels<Concert>("concert");
            AllConcertDTO = DataModel.getAllConcertDTO();
            MainWindow.AllCBGroupMusicConcert.ItemsSource = null;
            MainWindow.AllCBGenres.ItemsSource = null;
            MainWindow.AllConcertDTOView.ItemsSource = null;

            MainWindow.AllCBGroupMusicConcert.Items.Clear();
            MainWindow.AllCBGenres.Items.Clear();
            MainWindow.AllConcertDTOView.Items.Clear();

            MainWindow.AllCBGroupMusicConcert.ItemsSource = AllGroupMusic;
            MainWindow.AllCBGenres.ItemsSource = AllGenres;
            MainWindow.AllConcertDTOView.ItemsSource = AllConcertDTO;

            MainWindow.AllCBGroupMusicConcert.Items.Refresh();
            MainWindow.AllCBGenres.Items.Refresh();
            MainWindow.AllConcertDTOView.Items.Refresh();

            MainWindow.AllCBGroupMusicConcert.SelectedItem = null;
            MainWindow.AllCBGenres.SelectedItem = null;
        }

        // методы кторытые появились при реализации интерфейса

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
