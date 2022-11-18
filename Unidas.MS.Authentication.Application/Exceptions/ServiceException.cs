namespace MsSensediaTemplate.Application.Exceptions
{
    public class ServiceException : Exception
    {
        internal ServiceException(string businessMessage)
               : base(businessMessage)
        {
        }
    }
}
