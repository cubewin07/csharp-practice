class Collections_LINQ
{
    static void Main(String[] args)
    {
        // Object initializers
        var students = new Dictionary<string, double>()
        {
            ["Alex"] = 3
        };

        students["Alice"] = 3.9;
        students["Johb"] = 2.8;

        // Safe lookup - avoids KeyNotFoundException
        if(students.TryGetValue("Alice", out double gpa))
        {
            Console.WriteLine(gpa);
        }

        // Iterate
        foreach(var kvp in students)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }

        // Check existence
        bool exists = students.ContainsKey("Alice");
    }
}