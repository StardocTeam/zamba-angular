
namespace Zamba.LicenseCore
{
    public interface ILicense
    {
        string Name { get; set; }
        int Configured { get; set; }
        int Available { get; }
        int Used { get; set; }
    }
}
