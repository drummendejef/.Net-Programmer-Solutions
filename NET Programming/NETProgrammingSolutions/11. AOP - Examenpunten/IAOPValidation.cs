
namespace _09.AOP___Examenpunten
{
    public interface IAOPValidation
    {
        void IAOPValidation();
        string ErrorMessage { get; set; }
        bool IsValid { get; set; }
    }
}
