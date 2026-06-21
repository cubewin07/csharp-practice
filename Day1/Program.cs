class Test
{
    public static void Main(String[] strings)
    {
        
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
}

class Student
{
    private double _gpa;
    public string Name {get; set;}
    public int Age {get; set;}

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