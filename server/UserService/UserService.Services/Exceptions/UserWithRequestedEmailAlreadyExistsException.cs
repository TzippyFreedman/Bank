
namespace UserService.Services.Exceptions
{
   public class UserWithRequestedEmailAlreadyExistsException : BadRequestException
    {
        public UserWithRequestedEmailAlreadyExistsException()
        {

        }
        public UserWithRequestedEmailAlreadyExistsException(string email) : base($"Email:{email} already exists!")
        {

        }
    }
}
