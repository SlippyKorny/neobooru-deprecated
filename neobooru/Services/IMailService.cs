using System.Threading.Tasks;
using neobooru.Models;

namespace neobooru.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}