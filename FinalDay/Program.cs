class FinalDay
{
    public static void Main(string[] args)
    {
        List<int> nums = [2,5,4,6];
        List<string> names = ["Thang", "cubewin", "BrIan"];
        List<int> arr1 = [32, 43, 4, 3, 46, 50];


        Console.WriteLine(Sum(nums));
        Console.WriteLine(Find(names, "brian"));
        // FilterAbove(arr1, 10).ForEach(num => Console.WriteLine(num));
        CustomSort(arr1).ForEach(Console.WriteLine);
        

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

    public static List<int> CustomSort(List<int> arr)
    {
        var nums = new List<int>(arr);
        for(int i = 0; i < nums.Count; i++)
        {
            for(int j = i + 1; j < nums.Count; j++)
            {
                if(nums[j] < nums[i])
                {
                    int temp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = temp;
                }
            }
        }

        return nums;
    }
}