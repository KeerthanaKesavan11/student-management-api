namespace StudentManagement.API.Exception
{
    [Serializable]
    public class NotFoundException : IOException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string? message, IOException? innerException) : base(message, innerException)
        {
        }
    }
}
