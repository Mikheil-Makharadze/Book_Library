namespace API.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// /
        /// </summary>
        public string Surname { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Image { get; set; }


        //Relationship
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<BookDTO>? Books { get; set; } = new List<BookDTO>();
    }
}
