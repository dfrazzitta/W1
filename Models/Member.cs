using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;



namespace W1.Models;
//9001
public class Member
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

 
    [Required]
    public string? LotNo { get; set; }

    [Required]
    [StringLength(6)]
    public string? Price { get; set; }


    [Required]
    [StringLength(50)]
    [Display(Name = "Agent Last Name")]
    [Column("AgentLastName")]
    public string? AgentLastName { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
    [Column("AgentFirstName")]
    [Display(Name = "Agent First Name")]
    public string? AgentFirstName { get; set; }


    [Required]
     
    [EmailAddress(ErrorMessage = "Invalid primary email address")]
    public string? Email { get; set; }
     
 
    [Required(ErrorMessage = "You must provide a phone number")]
    [Display(Name = "Office Phone")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
    public string? OfficePhone { get; set; }

    [Required(ErrorMessage = "You must provide a phone number")]
    [Display(Name = "Cell Phone")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
    public string? CellPhone { get; set; }


    [Required(ErrorMessage = "You must provide a jpg Image")]
    [Display(Name = "AgentUrl")]
    public string? AgentUrl { get; set; }

    [Required(ErrorMessage = "You must provide a jpg Image")]
    [Display(Name = "Image")]
    public string? ImageName { get; set; }
}