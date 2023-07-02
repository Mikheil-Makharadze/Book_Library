using Core.Entities;
using static System.Reflection.Metadata.BlobBuilder;

namespace API.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Rating { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsTaken { get; set; }

        //Relationship
        public virtual ICollection<AuthorDTO> Authors { get; set; } = new List<AuthorDTO>();
    }
}
