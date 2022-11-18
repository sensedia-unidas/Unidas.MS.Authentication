using System.Collections.ObjectModel;

namespace MsSensediaTemplate.Domain.Models.Cars
{
    public sealed class CarTransactionCollection
    {
        private readonly IList<ICarTransaction> _transactions;

        public CarTransactionCollection()
        {
            _transactions = new List<ICarTransaction>();
        }

        public IReadOnlyCollection<ICarTransaction> GetTransactions()
        {
            IReadOnlyCollection<ICarTransaction> transactions = new ReadOnlyCollection<ICarTransaction>(_transactions);
            return transactions;
        }

        public string GetLastTransactionType()
        {

            if (_transactions.Count == 0)
                return "New";


            ICarTransaction transaction = _transactions[_transactions.Count - 1];
            return transaction.Action;
        }

        public ICarTransaction? GetLastTransaction()
        {

            if (_transactions.Count == 0)
                return null;


            ICarTransaction transaction = _transactions[_transactions.Count - 1];
            return transaction;
        }

        public void Add(ICarTransaction transaction)
        {
            _transactions.Add(transaction);
        }

        public void Add(IEnumerable<ICarTransaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                Add(transaction);
            }
        }

        public (long Latitude, long Longitude) GetCurrentLocation()
        {
            ICarTransaction transaction = _transactions[_transactions.Count - 1];

            return (transaction.Latitude, transaction.Longitude);
        }
    }
}
