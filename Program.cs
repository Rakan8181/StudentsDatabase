namespace StudentsDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=C:\\Users\\44734\\source\\repos\\StudentsDatabase\\Student Database.db;Mode=ReadWrite;";
            Menu(connectionString);


        }
        static void Menu(string connectionstring)
        {
            DataBase db = new(connectionstring);
            bool test = true;
            while (test)
            {
                Console.WriteLine("1: Get Classes for each course ");
                Console.WriteLine("2: Get students for each class");
                Console.WriteLine("3: Get classes for each student");
                Console.WriteLine("4: Add a student to a class");
                Console.WriteLine("5: Remove a student from a class");
                Console.WriteLine("6: Validate a student's options");
                Console.WriteLine("7: Exit");
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case ("1"):
                        Console.WriteLine("Enter the course you would like to display the classes for: ");
                        Console.WriteLine("Options: History,Computing,Maths,Economics,Geography,French,Spanish");
                        string courseChoice = Console.ReadLine();
                        List<string> classes = db.GetClasses(courseChoice);
                        Console.WriteLine("Classes:");

                        foreach (var class_ in classes)
                        {
                            Console.WriteLine(class_);
                        }
                        break;
                    case ("2"):
                        Console.WriteLine("Enter the class you want to display students in that class: ");
                        Console.WriteLine("GCSE choices available: Maths GCSE, Computing GCSE, French GCSE, Spanish GCSE, History GCSE, Geography GCSE");
                        Console.WriteLine("A Levels: Maths A Level,Computing A Level, French A Level, Spanish A Level, Economics A Level, Geography A Level ");
                        Console.WriteLine("Uni Classes: Maths University");
                        string _class = Console.ReadLine();
                        List<string> students = db.StudentsinClass(_class);
                        Console.WriteLine($"Classes: for {_class} ");
                        foreach (var student in students)
                        {
                            Console.WriteLine(student);
                        }
                        break;

                    case ("3"):
                        Console.WriteLine("Enter the second name of the student, to display the classes for that student");
                        string secondName = Console.ReadLine();
                        List<string> options = db.StudentOptions(secondName);
                        Console.WriteLine($"Classes: for {secondName} ");
                        foreach (var option in options)
                        {
                            Console.WriteLine(option);
                        }
                        break;
                    case ("4"):
                        Console.WriteLine("Enter the second name of the student you would like to add to a class: ");
                        string secondname = Console.ReadLine();
                        Console.WriteLine("Enter the class: ");
                        _class = Console.ReadLine();
                        db.AddStudenttoClass(secondname, _class);
                        break;
                    case ("5"):
                        Console.WriteLine("Enter the second name of the student you would like to remove from a class: ");
                        secondname = Console.ReadLine();
                        Console.WriteLine("Enter the class: ");
                        _class = Console.ReadLine();
                        db.RemoveStudentfromClass(secondname, _class);
                        break;
                    case ("6"):
                        Console.WriteLine("Requirement: 1 Language, 1 Humanity, 1 STEM subject, 5 subjects");
                        Console.WriteLine("Enter a second name to validate choice of classes: ");
                        secondName = Console.ReadLine();
                        db.ValidateStudentClasses(secondName);
                        break;
                    case ("7"):
                        test = false;
                        break;
                    default:
                        test = false;
                        break;
                    
                }
                Console.WriteLine();


            }
            

            
            //let a student add/delete a class
        }
    }
}