using System.Reflection;

class Test
{
    public static void Main(String[] strings)
    {
        // Modern Object creation - Target-typed new(), only happen when you already declared the type
        Student a = new("John", 17, 3.5);
        // Object initializer - new no args constructor
        var b = new Student { Name = "Alex", Age = 15, GPA = 3.8};
        // Named arguments
        var c = new Student(gpa: 4, name: "Nolan", age: 17);

        try
        {
            var invalid = new Student("hehe", 50, 5);
            
        } catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(DescribeStudent(null));
        }


        
        // List<Student> students = new List<Student>() {a, b, c};
        List<Student> students = [];
        
        students.Add(a);
        students.Add(b);
        students.Add(c);

        foreach(Student student in students)
        {
            string grade = LetterGrade(student.GPA);
            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, grade: {grade}");
        }
    }

    public static string LetterGrade(double gpa)
    {
        return _ = gpa switch
        {
            >= 3.7 => "A",
            >= 2.7 => "B",
            >= 1.7 => "C",
            _ => "D"
        };
    }

    public static string DescribeStudent(Student? s)
    {
        return s != null ? s.ToString() : "No student";
    }
}

class Student
{
    private double _gpa;
    public string Name {get; set;}
    public int Age {get; set;}

    public Student() { }

    public Student(string name, int age, double gpa)
    {
        Name = name;
        Age = age;
        GPA = gpa;
    }

    public double GPA
    {
        get => _gpa;

        set
        {
            if(value < 0 || value > 4) throw new ArgumentException("Invalid GPA");
            _gpa = value;
        }
    }

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}, GPA: {_gpa}";
    }
}