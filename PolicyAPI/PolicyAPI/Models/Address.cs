using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PolicyAPI.Models
{
    [Table("Address")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Address_Id")]
        public long AddressId {  get; set; }
        [Column("Door_No",TypeName ="varchar(10)")]
        public string DoorNo {  get; set; }
        [Column("Street_Name", TypeName = "varchar(100)")]
        public string StreetName { get; set; }
        [Column("City", TypeName = "varchar(100)")]
        public string City { get; set; }
        [Column("Country", TypeName = "varchar(100)")]
        public string Country { get; set; }
        [Column("State", TypeName = "varchar(100)")]
        public string State { get; set; }
        [Column("PinCode")]
        public long PinCode { get; set; }


        [ForeignKey("PolicyHolder")]
        [Column("AdharCard_No_FK")]      
        public string AdharCardNo { get; set; }
        [JsonIgnore]
        public PolicyHolder PolicyHolder { get; set; }

    }
}
