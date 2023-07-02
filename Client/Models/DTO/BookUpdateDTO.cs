using System.ComponentModel.DataAnnotations;

namespace Client.Models.DTO
{
    public class BookUpdateDTO
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public double Rating { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        public bool IsTaken { get; set; } = false;

        //Relationship
        [Required]
        public virtual ICollection<int> AuthorsId { get; set; }
    }
}
