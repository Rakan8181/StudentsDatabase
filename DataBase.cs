using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection;

namespace StudentsDatabase
{
    internal class DataBase
    {
        private string _connectionString;

        public DataBase(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<string> GetClasses(string subject)
        {
            List<string> classes = new List<string>();
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "select * from Class join Courses on Class.CourseID = Courses.CourseID where Courses.Title = (@Name);";
                var nameParameter = command.Parameters.Add("@Name", SqliteType.Text);
                nameParameter.Value = subject;
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var name = dataReader.GetString(0);
                    classes.Add(name);
                }
                return classes;

            }
        }
        public List<string> StudentsinClass(string _class)
        {
            List<string> students = new List<string>();
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "select s.FirstName,s.SecondName from ClassAttendance c join Students s on c.StudentID = s.StudentID where c.Class = (@Name);";
                var nameParameter = command.Parameters.Add("@Name", SqliteType.Text);
                nameParameter.Value = _class;
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var student = dataReader.GetString(0);
                    students.Add(student);
                }
                return students;
            }
        }
        public List<string> StudentOptions(string secondName)
        {
            List<string> classes = new List<string>();
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "select c.Class from Students s join ClassAttendance c on s.StudentID = c.StudentID where s.SecondName = (@Name);";
                var nameParameter = command.Parameters.Add("@Name", SqliteType.Text);
                nameParameter.Value = secondName;
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var _class = dataReader.GetString(0);
                    classes.Add(_class);
                    
                }
                return classes;

            }
        }
        public int GetStudentID(string secondName)
        {
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "select StudentID from Students where SecondName = (@Name) ";
                var nameParameter = command.Parameters.Add("@Name", SqliteType.Text);
                nameParameter.Value = secondName;
                var dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    string value = dataReader.GetString(0);
                    int StudentID = int.Parse(value);
                    return StudentID;
                }
                return (-1);
            }
        }
        public void AddStudenttoClass(string secondName, string _class)
        {
            int StudentID = GetStudentID(secondName);
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "insert into ClassAttendance (Class,StudentID) values ((@Class),(@StudentID))";
                var nameParameter1 = command.Parameters.Add("@Class", SqliteType.Text);
                nameParameter1.Value = _class;
                var nameParameter2 = command.Parameters.Add("@StudentID", SqliteType.Text);
                nameParameter2.Value = StudentID;
                command.ExecuteNonQuery();
                Console.WriteLine($"{secondName} has been added to {_class}");
            }
        }
        public void RemoveStudentfromClass(string secondName, string _class)
        {
            int StudentID = GetStudentID(secondName);
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "delete from ClassAttendance where StudentID = (@StudentID) and Class = (@Class);";
                var nameParameter1 = command.Parameters.Add("@StudentID", SqliteType.Text);
                nameParameter1.Value = StudentID;
                var nameParameter2 = command.Parameters.Add("@Class", SqliteType.Text);
                nameParameter2.Value = _class;
                command.ExecuteNonQuery();
                Console.WriteLine($"{secondName} has been removed from {_class}");
            }
        }
        public void ValidateStudentClasses(string secondName)
        {
            List<string> mfl = GetClasses("Spanish");
            mfl.AddRange(GetClasses("French"));
            List<string> humanities = GetClasses("Geography");
            humanities.AddRange(GetClasses("History"));
            List<string> stem = GetClasses("Maths");
            stem.AddRange(GetClasses("Computing"));
            stem.AddRange(GetClasses("Economics"));

            //must have 1 mfl, 1 humanity, and 1 stem
            int StudentID = GetStudentID(secondName);
            List<string> classes = StudentOptions(secondName);
            bool humanity = false;
            bool stem_ = false;
            bool language = false;
            foreach (string className in classes)
            {
                Console.WriteLine(className);
                if (mfl.Contains(className)){
                    language = true;
                }
                else if (humanities.Contains(className))
                {
                    humanity = true;
                }
                else if (stem.Contains(className))
                {
                    stem_ = true;
                }
            }
            int numSubjects = classes.Count();
            bool num = false;
            if (numSubjects > 4) {
                num = true;
            }

            if (language &&  stem_ && humanity && num) {
                Console.WriteLine("Valid options");
                
            }
            else
            {
                Console.WriteLine($"MFL: {language} ");
                Console.WriteLine($"Humanity: {humanity}");
                Console.WriteLine($"STEM: {stem_}");
                Console.WriteLine($"Number of Subjects: {numSubjects}");
                Console.WriteLine("All 3 need to be true as well as having at least 5 subjects");
            }
        }

    }
}
