using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class BookCreateDTO
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Title { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Description { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Image { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public double Rating { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTaken { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        //Relationship
        [Required]
        public virtual ICollection<int> AuthorsId { get; set; } = null!;
    }
}
