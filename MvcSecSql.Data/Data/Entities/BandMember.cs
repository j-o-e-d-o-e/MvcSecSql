using System.ComponentModel.DataAnnotations;

namespace MvcSecSql.Data.Data.Entities
{
    public class BandMember
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Instrument { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        public int BandId { get; set; }

        public Band Band { get; set; }
    }
}