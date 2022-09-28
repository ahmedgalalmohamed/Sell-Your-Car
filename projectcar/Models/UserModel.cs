using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectcar.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [MaxLength(25,ErrorMessage ="Must First Name Is max 20 character")]
        [MinLength(3, ErrorMessage = "Must First Name Is min 3 character")]
        [Required(ErrorMessage ="This Field Required")]
        public string fname { get; set; }
        [MaxLength(25, ErrorMessage = "Must Last Name Is max 20 character")]
        [MinLength(3, ErrorMessage = "Must Last Name Is min 3 character")]
        [Required(ErrorMessage = "This Field Required")]
        public string? lname { get; set; }
        [MaxLength(50, ErrorMessage = "Must Email Is max 50 character")]
        [Required(ErrorMessage = "This Field Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(50, ErrorMessage = "Must Username Is max 50 character")]
        [Required(ErrorMessage = "This Field Required")]
        public string Username { get; set; }
        public string Gender { get; set; }
        public string? CompanyName { get; set; }
        [Required(ErrorMessage = "This Field Required")]
        [RegularExpression("01[0125][0-9]{8}",ErrorMessage ="Error Phone To Egypt")]
        public string Phone { get; set; }
        [MaxLength(50, ErrorMessage = "Must Address Is max 50 character")]
        public string? Address { get; set; }
        public string role { get; set; } = "user";
        public string? state { get; set; } = "run";

        public string? Img { get; set; }
        [MaxLength(50, ErrorMessage = "Must Password Is max 50 character")]
        [MinLength(8,ErrorMessage = "Must Password Is min 8 character")]
        [Required(ErrorMessage = "This Field Required")]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password",ErrorMessage ="Not Matched")]
        public string ConfirmPas { get; set; }

        public ICollection<CarModel> Cars { set; get; } 
    }
}
