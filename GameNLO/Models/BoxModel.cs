namespace GameNLO.Models
{
    public class BoxModel
    {
        public bool IsShown => RequestedUserId.HasValue;

        public Guid? RequestedUserId { get; set; }

        public decimal? Prize { get; set; }
    }
}
