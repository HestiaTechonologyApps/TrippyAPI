namespace Trippy.Domain.DTO
{
    public class StaffUserUpdateDto
    {
        public int StaffUserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? Bio { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public bool? IsAudioEnbaled { get; set; }
        public bool? IsVideoEnabled { get; set; }
        public bool? IsBlocked { get; set; }
        public bool? IsDeleted { get; set; }
        public string? KycDocument { get; set; }
        public string? KycDocumentNumber { get; set; }
        public bool? IsKYCCompleted { get; set; }
        public DateTime? KycCompletedDate { get; set; }
        public decimal? CustomerCoinsPerSecondVideo { get; set; }
        public decimal? CustomerCoinsPerSecondAudio { get; set; }
        public decimal? CompanyCoinsPerSecondVideo { get; set; }
        public decimal? CompanyCoinsPerSecondAudio { get; set; }
        public string? ProfileImagePath { get; set; }
        public decimal? WalletBalance { get; set; }
        public bool? IsOnline { get; set; } = true;
        public int? Priority { get; set; } = 1;
        // Add more fields as needed
    }

}
