class FinalDay
{
    public static void Main(string[] args)
    {
        List<int> nums = [2,5,4,6];
        List<string> names = ["Thang", "cubewin", "BrIan"];
        List<int> arr1 = [32, 43, 4, 3, 46, 50];


        Console.WriteLine(Sum(nums));
        Console.WriteLine(Find(names, "brian"));
        FilterAbove(arr1, 10).ForEach(num => Console.WriteLine(num));

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

    public static List<int> FilterAbove(List<int> nums, int threshold)
    {
        return nums.Where(num => num > threshold).ToList();
    }
}