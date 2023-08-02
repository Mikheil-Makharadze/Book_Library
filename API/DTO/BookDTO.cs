namespace API.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class BookDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Rating { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime PublishDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsTaken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //Relationship
        public virtual ICollection<AuthorDTO> Authors { get; set; } = new List<AuthorDTO>();
    }
}
