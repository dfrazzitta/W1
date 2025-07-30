using System.ComponentModel.DataAnnotations;


namespace W1.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
}
