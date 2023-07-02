using System.ComponentModel.DataAnnotations;

namespace Clinet.Models.DTO
{
    public class AuthorCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }


        //Relationship
        public virtual ICollection<int>? booksId { get; set; }
    }
}
