using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class Age
{
    public string? name { get; set; }
    public DateTime? birthDate { get; set; }

    public int age = 0;

    

    public bool IsValid()
    {
        if (name is null)
        {
            return false;
        }
        
        if (birthDate > DateTime.Now || birthDate is null)
        {
            return false;
            
        }

        return true;
        
    }
    
    public int Birth()
    {
       
            age = DateTime.Now.Year - birthDate.Value.Year;
            return age;
        
        
    }
 
}