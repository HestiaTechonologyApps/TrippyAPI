namespace Trippy.Domain.DTO
{
    // Request to start a call session
    public class StartCallRequestDto
    {
        public int UserId { get; set; }
        public int StaffId { get; set; }
    }

    // Response after starting a call session
    public class StartCallResponseDto
    {
        public int SessionId { get; set; }
        public string AgoraChannelName { get; set; } = string.Empty;
        public string AgoraToken { get; set; } = string.Empty;
        public decimal StaffRate { get; set; }
    }

    // Request to end a call session
    public class EndCallRequestDto
    {
        public int SessionId { get; set; }
    }

    // Response after ending a call session
    public class EndCallResponseDto
    {
        public int SessionId { get; set; }
        public int DurationMinutes { get; set; }
        public decimal DeductedCoins { get; set; }
        public decimal RemainingBalance { get; set; }
    }

    // Wallet information DTO
    public class WalletDto
    {
        public int UserId { get; set; }
        public decimal BalanceCoins { get; set; }
    }
}
