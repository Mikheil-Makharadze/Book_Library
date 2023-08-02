namespace Core.Entities
{
    public class Book : BaseEntity
    {
        public Book()
        {
                AuthorBooks = new List<AuthorBook>();
        }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double? Rating { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public bool IsTaken { get; set; }

        //Relationship
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
