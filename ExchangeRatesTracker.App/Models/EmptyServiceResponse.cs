namespace ExchangeRatesTracker.App.Models
{
    public class EmptyServiceResponse
    {
        public EmptyServiceResponse()
        { }

        public EmptyServiceResponse(string errorMessage)
        {
            Errors.Add(errorMessage);
        }

        public EmptyServiceResponse(IEnumerable<string> errorMessages)
        {
            Errors.AddRange(errorMessages);
        }

        public List<string> Errors { get; private set; } = new List<string>();

        public bool HasErrors
        {
            get
            {
                return Errors.Count > 0;
            }
        }

        public static EmptyServiceResponse Success
        {
            get
            {
                return new EmptyServiceResponse();
            }
        }
    }
}