namespace GameNLO.Hubs
{
    public interface IStatusHubClient
    {
        Task ValidateUser(bool isUserEligible, string message);

        Task UpdateStatus(string status, int latitude, int longitude);

        Task SendMessage(string status, string icon);
    }
}
