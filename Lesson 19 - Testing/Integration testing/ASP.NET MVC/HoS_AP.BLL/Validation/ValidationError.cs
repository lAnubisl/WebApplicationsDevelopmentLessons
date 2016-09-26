namespace HoS_AP.BLL.Validation
{
    public class ValidationError
    {
        private readonly string property, message;

        public ValidationError(string property, string errorMessage)
        {
            this.property = property;
            this.message = errorMessage;
        }

        public string Property { get { return property; } }

        public string Message { get { return message; } }
    }
}