
namespace UserService.Services.Exceptions
{
    class UserWithRequestedEmailAlreadyExistsException : BadRequestException
    {
        public UserWithRequestedEmailAlreadyExistsException()
        {

        }
        public UserWithRequestedEmailAlreadyExistsException(string email) : base($"Email:{email} already exists!")
        {

        }
    }
}
