using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicSchoolWPFMVVM.Model
{
    public interface Modele
    {
        int ID { get; set; }
    }
    public class Speciality : Modele
    {
        public int ID { get; set; }
        public string SpecialityName { get; set; }

        public override string ToString()
        {
            return SpecialityName;
        }
    }

    public class Genres : Modele
    {
        public int ID { get; set; }
        public string GenreName { get; set; }

        public override string ToString()
        {
            return GenreName;
        }
    }

    public class Position : Modele
    {
        public int ID { get; set; }
        public string PositionName { get; set; }

        public override string ToString()
        {
            return PositionName;
        }
    }

    public class Employee : Modele
    {
        public int ID { get; set; }
        public int PositionID { get; set; }
        public string Fio { get; set; }
        public string Telephone { get; set; }
        public bool Gender { get; set; }
        public int Experience { get; set; }
        public string Email { set; get; }
    }

    public class GroupMusic : Modele
    {
        public int ID { get; set; }
        public string GroupMusicName { get; set; }
        public int EmployeeTeacherID { get; set; }
        public int EmployeeAccompanistID { get; set; }

        public override string ToString()
        {
            return GroupMusicName;
        }
    }

    public class Student : Modele
    {
        public int ID { get; set; }
        public int GroupMusicID { get; set; }
        public int SpecilityID { get; set; }
        public string Fio { get; set; }
        public string Telephone { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
    }

    public class Concert : Modele
    {
        public int ID { get; set; }
        public int GroupMusicID { get; set; }
        public int GenreID { get; set; }
        public DateTime ConcertDate { get; set; }
    }

    public class StudentDTO : Modele
    {
        public int ID { get; set; }
        public string GroupMusicStr { get; set; }
        public string SpecilityStr { get; set; }
        public string Fio { get; set; }
        public string Telephone { get; set; }
        public string GenderStr { get; set; }
        public string Email { get; set; }
    }

    public class EmployeeDTO : Modele
    {
        public int ID { get; set; }
        public string PositionStr { get; set; }
        public string Fio { get; set; }
        public string Telephone { get; set; }
        public string GenderStr { get; set; }
        public int Experience {  get; set; }
        public string Email { get; set; }
    }

    public class ConcertDTO : Modele
    {
        public int ID { get; set; }
        public  string GroupMusicStr { get; set; }
        public  string GenresStr { get; set; }
        public DateTime ConcertDate { get; set; }
    }
}
