namespace Management.Service.Helpers;
public class ManagementException:Exception
{
    public int StatusCode { get; set; }
    public ManagementException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}
