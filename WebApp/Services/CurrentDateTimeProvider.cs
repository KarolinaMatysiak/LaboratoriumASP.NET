namespace WebApp.Models;

public class CurrentDateTimeProvider : IDateTimeProvider
{
    public DateTime Now()
    {
        return DateTime.Now;
    }
}