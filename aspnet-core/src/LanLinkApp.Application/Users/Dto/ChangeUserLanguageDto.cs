using System.ComponentModel.DataAnnotations;

namespace LanLinkApp.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}