using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class ValidationOfDate: ValidationAttribute
{

    public ValidationOfDate()
    {
        ErrorMessage = "Data musi być mniejsza lub równa dzisiejszej dacie.";
    }

    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return false;
        }

        if (value is DateTime date)
        {
            return date <= DateTime.Now; // zwraca true, jeśli data jest mniejsza lub równa dzisiejszej dacie
        }

        return false;
    }
}