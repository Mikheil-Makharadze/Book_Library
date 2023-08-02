using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorCreateDTO
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Surname { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Image { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //Relationship
        public virtual ICollection<int>? BooksId { get; set; }
    }
}
