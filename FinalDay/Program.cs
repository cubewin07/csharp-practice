class FinalDay
{
    public static void Main(string[] args)
    {
        List<int> nums = [2,5,4,6];
        List<string> names = ["Thang", "cubewin", "BrIan"];

        Console.WriteLine(Sum(nums));
        Console.WriteLine(Find(names, "brian"));
    }

    public static int Sum(List<int> nums)
    {
        int sum = 0;

        foreach(int num in nums)
        {
            sum += num;
        }

        return sum;
    }

    public static int Find(List<string> names, string target)
    {
        for(int i = 0; i < names.Count; i++)
        {
            if(string.Equals(names[i], target, StringComparison.OrdinalIgnoreCase))
                return ++i;
        }

        return -1;
    }
}