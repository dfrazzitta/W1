using W1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;



namespace W1.Models
{
    public class User   
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]

        public string? FirstName { get; set; }


        [Required]
        [StringLength(50)]

        public string? LastName { get; set; }


        [Required]
        [StringLength(50)]

        public string? FullName { get; set; }



        [Required]
        [StringLength(50)]

        public string? LotNo { get; set; }

        [Required]
        [StringLength(50)]

        public string? Cell { get; set; }


         
    }
}
