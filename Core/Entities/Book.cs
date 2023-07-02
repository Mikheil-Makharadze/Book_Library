using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Rating { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsTaken { get; set; }

        //Relationship
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
