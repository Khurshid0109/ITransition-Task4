using System.Net.Mail;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Management.Service.Helpers;
public class ManagementEmailAttribute:ValidationAttribute
{
    private readonly Regex _emailRegex;

    public ManagementEmailAttribute()
    {
        _emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);
    }

    public override bool IsValid(object value)
    {
        if (value is string email)
        {
            if (_emailRegex.IsMatch(email))
            {
                try
                {
                    var mail = new MailAddress(email);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        return false;
    }

}
