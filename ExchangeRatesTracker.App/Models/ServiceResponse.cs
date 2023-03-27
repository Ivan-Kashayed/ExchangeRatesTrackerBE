namespace ExchangeRatesTracker.App.Models
{
    public class ServiceResponse<T> : EmptyServiceResponse
    {
        private readonly T? _body;

        public ServiceResponse(T body)
        {
            _body = body;
        }

        public ServiceResponse(string errorMessage) : base(errorMessage)
        { }

        public ServiceResponse(IEnumerable<string> errorMessages) : base(errorMessages)
        { }

        public ServiceResponse()
        { }

        public T? Body { get => _body; }
    }
}
