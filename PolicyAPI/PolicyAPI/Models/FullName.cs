using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolicyAPI.Models
{
    [Owned]
    public class FullName
    {
        [Required]
        [Column("First_Name" , TypeName = "varchar(100)")]
        [RegularExpression("^[a-zA-Z]{5,25}$", 
            ErrorMessage = "First Name Should be in alphabets within the range of 5,25")]
        [DefaultValue("")]
        public string FirstName {  get; set; } = string.Empty;
        [Required]
        [DefaultValue("")]
        [Column("Last_Name", TypeName = "varchar(100)")]
        [RegularExpression("^[a-zA-Z]{5,25}$",
            ErrorMessage = "Last Name Should be in alphabets within the range of 5,25")]
        public string LastName { get; set; }=string.Empty;
        [Column("Middle_Name", TypeName = "varchar(50)")]
        [DefaultValue("")]
        public string MiddleName {  get; set; } = string.Empty; 
    }
}
