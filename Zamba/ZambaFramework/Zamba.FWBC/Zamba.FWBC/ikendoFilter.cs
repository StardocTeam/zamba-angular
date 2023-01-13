namespace Zamba.Framework
{
    public interface ikendoFilter
    {
        string Field { get; set; }
        string Operator { get; set; }
        string Value { get; set; }
        string DataBaseColumn { get; set; }
        bool Enabled { get; set; }
        long FilterID { get; set; }
    }
}