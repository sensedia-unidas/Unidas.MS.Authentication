using MsSensediaTemplate.Domain.Models.Cars;
namespace MsSensediaTemplate.Application.ViewModels.Car.Results
{
    public sealed class CarResult
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Plate { get; set; }

        public List<CarTransactionResult> Transactions { get; }



        public CarResult(Guid id, string description, string plate, List<CarTransactionResult> transactions)
        {
            Id = id;
            Description = description;
            Plate = plate;
            Transactions = transactions;
        }

        public CarResult(Cars car)
        {
            Id = car.Id;
            Description = car.Description;
            Plate = car.Plate;

            List<CarTransactionResult> transactionResults = new List<CarTransactionResult>();
            foreach (ICarTransaction transaction in car.GetTransactions())
            {
                CarTransactionResult transctionResult = new CarTransactionResult(transaction.Action, transaction.RentedBy, transaction.TransactionDate, transaction.Latitude, transaction.Longitude);
                transactionResults.Add(transctionResult);
            }

            Transactions = transactionResults;

        }
    }
}
