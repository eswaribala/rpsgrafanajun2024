using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PolicyAPI.Models
{
    public enum Gender { MALE,FEMALE,TRANSGENDER}
    [Table("PolicyHolder")]
    public class PolicyHolder
    {
        [Key]
        [Column("Adhar_Card",TypeName ="varchar(25)")]
        public string AdharCardNo { get; set; } =string.Empty;
        public FullName? Name { get; set; }
        [Column("Gender")]
        [EnumDataType(typeof(Gender))]       
        public Gender? Gender { get; set; }
        [Column("DOB")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        public DateTime DOB { get; set; }
        [Column("Email")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$",
            ErrorMessage = "Email Format Not matching")]
        [DefaultValue("")]
        public string Email { get; set; } =string.Empty;
        [Column("Phone")]
        [RegularExpression("^([+]\\d{2}[ ])?\\d{10}$",
            ErrorMessage = "Mobile No Format Not matching")]
        public long Phone { get; set; }



    }
}
