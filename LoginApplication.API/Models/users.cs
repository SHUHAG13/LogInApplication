using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginApplication.API.Models
{
    public class users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }
        public string emailId { get; set; }
        public string password { get; set; }
        public DateTime createdDate { get; set; }
        public string fullName { get; set; }
        public string mobileNo { get; set; }
    }

    public class UserLogin
    {
        public string emailId { get; set; }
        public string password { get; set; }
    }
}
