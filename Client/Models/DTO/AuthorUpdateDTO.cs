using System.ComponentModel.DataAnnotations;

namespace Client.Models.DTO
{
    public class AuthorUpdateDTO
    {
        public int Id { get; set; }
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
