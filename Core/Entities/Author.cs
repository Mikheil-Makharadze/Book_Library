﻿namespace Core.Entities
{
    public class Author : BaseEntity
    {
        public Author()
        {
            AuthorBooks = new List<AuthorBook>();
        }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Image { get; set; }

        //Relationship
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
