
using FormValidation.Resources;
using FormValidation.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
namespace FormValidation.Models
{
    public class AddCommentModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "RequiredTemplate")]
        [Display(ResourceType = typeof(DisplayNames), Name = "Comment")]
        [StringLength(10)]
        [RegularExpression(@"[a-zа-я]+")]
        public string Comment { get; set; }

        [MyValidationAttribute]
        public int Age { get; set; }
    }
}