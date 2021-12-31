using System.Threading.Tasks;

namespace NotMvcButCoreWebUi.Services
{
    public interface IEmailSender
    {
        Task Sender(string Message);
    }
}