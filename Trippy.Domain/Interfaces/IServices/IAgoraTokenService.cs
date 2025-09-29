public interface IAgoraTokenService
{
    string GenerateToken(string channelName, int userId);
}
