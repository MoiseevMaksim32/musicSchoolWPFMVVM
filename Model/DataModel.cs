using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace musicSchoolWPFMVVM.Model
{
    public static class DataModel
    {
        
        public static async void CreateModels<T>(string path, T module)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5164/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            await client.PostAsJsonAsync(path, module);
        }

        public static async void UpdateModel<T>(string path, T module)
            where T : Modele
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5164/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            await client.PutAsJsonAsync(path + "/" + module.ID, module);
        }

        public static List<T> GetAllModels<T>(string path) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5164/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string reponse = client.GetStringAsync(path).Result;
            var models = JsonConvert.DeserializeObject<List<T>>(reponse);
            return models;
        }

        public static void DeleteModels<T>(string path, int id) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5164/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DeleteAsync(path+"/"+id);
        }

        public static List<StudentDTO> getAllStudentDTO()
        {
            List<Student> students = GetAllModels<Student>("student");
            List<GroupMusic> groupMusics = GetAllModels<GroupMusic>("group_music");
            List<Speciality> specialities = GetAllModels<Speciality>("speciality");
            List<StudentDTO> studentDTOs = new List<StudentDTO>();
            foreach (Student student in students)
            {
                StudentDTO studentDTO = new StudentDTO()
                {
                    ID = student.ID,
                    GroupMusicStr = groupMusics.Where(x => x.ID.Equals(student.GroupMusicID)).Select(x => x.GroupMusicName).First(),
                    SpecilityStr = specialities.Where(x => x.ID.Equals(student.SpecilityID)).Select(x => x.SpecialityName).First(),
                    Fio = student.Fio,
                    Telephone = student.Telephone,
                    GenderStr = student.Gender ? "Женский" : "Мужской",
                    Email = student.Email
                };
                studentDTOs.Add(studentDTO);
            }
            return studentDTOs;
        }

        public static List<EmployeeDTO> getAllEmployeeDTO()
        {
            List<Position> positions = GetAllModels<Position>("position");
            List<Employee> employees = GetAllModels<Employee>("employee");
            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            foreach(Employee employee in employees)
            {
                EmployeeDTO employeeDTO = new EmployeeDTO() { 
                    ID = employee.ID,
                    PositionStr = positions.Where(x => x.ID.Equals(employee.PositionID)).Select(x => x.PositionName).First(),
                    Fio = employee.Fio,
                    Telephone = employee.Telephone,
                    GenderStr = employee.Gender ? "Женский" : "Мужской",
                    Experience = employee.Experience,
                    Email = employee.Email
                };
                employeeDTOs.Add(employeeDTO);
            }
            return employeeDTOs;
        }

        public static List<ConcertDTO> getAllConcertDTO()
        {
            List<Concert> concerts = GetAllModels<Concert>("concert");
            List<GroupMusic> groupMusics = GetAllModels<GroupMusic>("group_music");
            List<Genres> genres = GetAllModels<Genres>("genres");
            List<ConcertDTO> concertDTOs = new List<ConcertDTO>();
            foreach(Concert concert in concerts)
            {
                ConcertDTO concertDTO = new ConcertDTO() { 
                    ID = concert.ID,
                    GroupMusicStr = groupMusics.Where(x => x.ID.Equals(concert.GroupMusicID)).Select(x => x.GroupMusicName).First(),
                    GenresStr = genres.Where(x => x.ID.Equals(concert.GenreID)).Select(x => x.GenreName).First(),
                    ConcertDate = concert.ConcertDate,
                };
                concertDTOs.Add(concertDTO);
            }
            return concertDTOs;
        }
    }
}
