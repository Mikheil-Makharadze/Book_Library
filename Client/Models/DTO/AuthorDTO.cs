

namespace Clinet.Models.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }


        //Relationship
        public virtual ICollection<BookDTO> books { get; set; } = new List<BookDTO>();
    }
}
