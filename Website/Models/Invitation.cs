using System;
using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Invitation
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Informal name")]
        public string InformalName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Language")]
        public string LanguageCode { set; get; }

        [Display(Name = "Allowed guests")]
        public int AllowedGuests { get; set; }

        [Required]
        [Display(Name = "Guest of")]
        public string GuestOf { get; set; }

        public DateTimeOffset? InvitationSentAt { get; set; }
        public DateTimeOffset? LastViewedAt { get; set; }

        [Display(Name = "Is attending?")]
        public bool? IsAttending { get; set; }
        public int? Guests { get; set; }

        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
    }
}