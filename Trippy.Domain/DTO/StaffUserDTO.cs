using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Trippy.Domain.Entities;

namespace Trippy.Domain.DTO
{

    public class StaffUserDTO
    {
       
        public int StaffUserId { get; set; } // Primary key
        public int AppUserId { get; set; } // Primary key
        public String Bio { get; set; } = "";
        public string Name { get; set; } = ""; // User's display name
        public string? Email { get; set; } = ""; // Optional email ID
        public string? MobileNumber { get; set; } = "";// Optional phone number
        public string? Gender { get; set; } // Male/Female/Other
        public DateTime RegisteredDate { get; set; } // Account creation timestamp
        public bool IsBlocked { get; set; } // True = blocked by admin
        public bool IsDeleted { get; set; } // True = bldeleted by user
        public string? Address { get; set; }
        public string? ReferredBy { get; set; } // Referral code of the referrer
        public string? ReferralCode { get; set; } // Unique referral code of this user
        public string? KycDocument { get; set; }
        public string? KycDocumentNumber { get; set; }
        public bool IsKYCCompleted { get; set; } = false; // Whether KYC is verified
        public DateTime? KycCompletedDate { get; set; }
        public bool IsAudioEnbaled { get; set; } = true;
        public bool IsVideoEnabled { get; set; } = true;
        public bool IsOnline { get; set; } = true;
        public decimal? StarRating { get; set; } = 0;
        public decimal CustomerCoinsPerSecondVideo { get; set; } = 0;
        public decimal CustomerCoinsPerSecondAudio { get; set; } = 0;
        public decimal CompanyCoinsPerSecondVideo { get; set; } = 0;
        public decimal CompanyCoinsPerSecondAudio { get; set; } = 0;
        public string ProfileImagePath { get; set; } = ""; // Path to profile image
        public DateTime? LastLogin { get; set; }
        public decimal WalletBalance { get; set; } = 0m;
        public string? Prefferedlanguage { get; set; } = "";
        public string? Interests { get; set; } = "";
        public bool IsAvailable { get; set; }
        public int Priority { get; set; } = 1;

    }


    public class StaffListUserDTO
    {
        public int StaffUserId { get; set; } // Primary key
        public int AppUserId { get; set; } // Primary key

        public string Name { get; set; } = "";
        public string? MobileNumber { get; set; }
        public string RegisteredDate { get; set; }
        public string? Gender { get; set; } // Male/Female/Other

        public string Status { get; set; }
        public bool IsBlocked { get; set; }
        public decimal? WalletBalance { get; set; }

    }

  

    public class StaffUserOnlineDTO
    {
        public int StaffUserId { get; set; }
        public int AppUserId { get; set; }
        public string Name { get; set; } = "";
        public string ProfilePic { get; set; } = "";
        public decimal? StarRating { get; set; } = 0;

        public bool IsAudioEnbaled { get; set; } = true;
        public bool IsVideoEnabled { get; set; } = true;

        public decimal? AudiocallRatePerMinute { get; set; } = 0;
        public decimal? VideoCallRatePerMinute { get; set; } = 0;
        public string? Status { get; set; } = "Online";
        public String Bio { get; set; } = "";
        public bool IsOnline { get; set; } = true; // True if the staff user is currently online
        public bool IsAvailable { get; set; } =true; // True if the staff user is currently online
        
        public List<string> PreferredLanguages { get; set; } = new List<string>(); //arrray of string
        public List<string> Interests { get; set; } = new List<string>(); //arrray of string
        public int Priority { get; set; } = 0; // Priority for online staff, higher number means higher priority
        //aray of string
    }

    public class StaffUserToggleDto
    {
        public int StaffUserId { get; set; }
        public bool Value { get; set; }
    }

}
