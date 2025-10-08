using System.Threading.Tasks;

namespace HealthCareApp.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendInvitationEmail(string recipientEmail, string recipientName, string invitationLink);
    }
}