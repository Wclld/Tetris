namespace Extensions
{
    public static class IntExtension
    {
        public static bool IsInRange(this int value, int min, int max)
        {
            return max > value && value >= min;
        }
    }
}