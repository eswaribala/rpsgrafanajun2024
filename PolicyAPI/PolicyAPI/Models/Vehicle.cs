using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolicyAPI.Models
{
    public enum FuelType { PETROL,DIESEL, ELECTRIC, GAS}
    [Table("Vehicle")]
    public class Vehicle
    {
        [Key]
        [Column("Registration_No")]
        public string RegistrationNo { get; set; }
        [Column("Maker",TypeName ="varchar(100)")]
        [Required]
        public string Maker { get; set; }

        [Column("DOR")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        [Required]
        public string DOR { get; set; }
        [Column("Chassis_No", TypeName = "varchar(100)")]
        [Required]
        public string ChassisNo { get; set; }
        [Column("Engine_No", TypeName = "varchar(100)")]
        [Required]
        public string EngineNo { get; set; }
        [Column("Fuel_Type")]
        [EnumDataType(typeof(FuelType))]
        [Required]
        public FuelType FuelType { get; set; }
        [Column("Color")]
        [Required]
        public string Color {  get; set; }


    }
}
