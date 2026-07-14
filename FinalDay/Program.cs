class FinalDay
{
    public static void Main(string[] args)
    {
        List<int> nums = [2,5,4,6];

        Console.WriteLine(Sum(nums));
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
}