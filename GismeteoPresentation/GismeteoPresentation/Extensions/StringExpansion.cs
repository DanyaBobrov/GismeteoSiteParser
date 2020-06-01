namespace GismeteoPresentation.Extension
{
    public static class StringExpansion
    {
        public static bool IsEmpty(this string source)
        {
            return source == null || source == string.Empty;
        }
    }
}
