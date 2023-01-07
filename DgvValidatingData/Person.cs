using Extensions.Winforms;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DgvValidatingData
{
    //[TypeDescriptionProvider(typeof(MetadataTypeTypeDescriptionProvider))]
    public class Person : BaseModel
    {
        [Display(Name = "Id")]
        [Browsable(false)]
        public int? Id { get; set; }

        [Display(Name = "First Name", Description = "First name.", Order = 1)]
        [Required(ErrorMessage = "{0}是必须的"), StringLength(10, ErrorMessage = "长度限制为{0}")]
        [RegularExpression("w+")]
        [UIHint("TextBox")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name", Description = "Last name", Order = 2)]
        [UIHint("Rollover")]
        public string? LastName { get; set; }

        [Display(Name = "Password", Description = "Password", Order = 3)]
        [UIHint("Password")]
        public string? Password { get; set; }

        [Display(Name = "Birth Date", Description = "Date of birth.", Order = 4)]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        [UIHint("Calendar")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Homepage", Description = "Url of homepage.", Order = 5)]
        [Required]
        [Url]
        [UIHint("Link")]
        public string? Url { get; set; }

        [Display(Name = "Member", Description = "Is member?", Order = 3)]
        [Required(ErrorMessage = "{0}是必须的")]
        public bool IsMember { get; set; }

        [Display(Order = 3, Name = "Level")]
        [Range(0, 99, ErrorMessage = "{0} 范围是 {1} - {2}")]
        [UIHint("Integer")]
        public int MemberLevel { get; set; }

        [Display(Order = 3, Name = "Prepaid")]
        [DataType(DataType.Currency)]
        [UIHint("Numeric")]
        public decimal Prepaid { get; set; }

    }

}