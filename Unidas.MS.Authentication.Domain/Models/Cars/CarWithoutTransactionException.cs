namespace MsSensediaTemplate.Domain.Models.Cars
{
    public sealed class CarWithoutTransactionException : DomainException
    {
        internal CarWithoutTransactionException(string message)
            : base(message)
        { }
    }
}