using System.ComponentModel.DataAnnotations;

namespace SantoPecado
{
    public class Address
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debes introducir un nombre."), MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debes introducir una dirección."), MaxLength(100)]
        public string Line1 { get; set; }

        [MaxLength(100)]
        public string Line2 { get; set; }

        [Required(ErrorMessage = "Debes introducir una ciudad."), MaxLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "Debes introducir una provincia."), MaxLength(20)]
        public string Region { get; set; }

        [Required(ErrorMessage = "Debes introducir un código postal."), MaxLength(20)]
        public string PostalCode { get; set; }
    }
}
