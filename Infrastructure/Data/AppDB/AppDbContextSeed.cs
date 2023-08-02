using Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.AppDB
{
    public class AppDBContextSeed
    {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context!.Database.EnsureCreated();

            if (!(context.Books.Any() || context.Authors.Any()))
            {
                await context.Authors.AddRangeAsync(new List<Author>()
                    {
                        new Author
                        {
                            Name = "Terry",
                            Surname = "Pratchett",
                            BirthDate = new DateTime(1948, 4, 28),
                            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7a/10.12.12TerryPratchettByLuigiNovi1.jpg/800px-10.12.12TerryPratchettByLuigiNovi1.jpg"
                        },
                        new Author
                        {
                            Name = "Neil",
                            Surname = "Gaiman",
                            BirthDate = new DateTime(1960, 11, 10),
                            Image = "https://upload.wikimedia.org/wikipedia/commons/b/bc/Kyle-cassidy-neil-gaiman-April-2013.jpg"
                        },
                        new Author
                        {
                            Name = "Stephen",
                            Surname = "King",
                            BirthDate = new DateTime(1947, 9, 21),
                            Image = "https://upload.wikimedia.org/wikipedia/commons/e/e3/Stephen_King%2C_Comicon.jpg"
                        },
                        new Author
                        {
                            Name = "Fyodor",
                            Surname = "Dostoevsky",
                            BirthDate = new DateTime(1821, 11, 11),
                            Image = "https://upload.wikimedia.org/wikipedia/commons/7/78/Vasily_Perov_-_%D0%9F%D0%BE%D1%80%D1%82%D1%80%D0%B5%D1%82_%D0%A4.%D0%9C.%D0%94%D0%BE%D1%81%D1%82%D0%BE%D0%B5%D0%B2%D1%81%D0%BA%D0%BE%D0%B3%D0%BE_-_Google_Art_Project.jpg"
                        },
                        new Author
                        {
                            Name = "Owen",
                            Surname = "King",
                            BirthDate = new DateTime(1977, 2, 21),
                            Image = "https://owen-king.com/wp-content/uploads/2022/07/owen-king-author2.jpg"
                        },
                        new Author
                        {
                            Name = "Peter",
                            Surname = "Straub",
                            BirthDate = new DateTime(1943, 3, 2),
                            Image = "https://upload.wikimedia.org/wikipedia/commons/3/38/Peter_Straub.jpg"
                        },
                        new Author
                        {
                            Name = "J.R.R.",
                            Surname = "Tolkien",
                            BirthDate = new DateTime(1892, 1, 3),
                            Image = "https://upload.wikimedia.org/wikipedia/commons/d/d4/J._R._R._Tolkien%2C_ca._1925.jpg"
                        }
                    });

                context.SaveChanges();

                await context.Books.AddRangeAsync(new List<Book>()
                    {
                        new Book
                        {
                            Title = "Good Omens",
                            Description = "A comedic fantasy novel about the impending apocalypse and the unlikely partnership between an angel and a demon.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/0/0a/Goodomenscover.jpg",
                            Rating = 9.2,
                            PublishDate = new DateTime(1990, 5, 1),
                            IsTaken = false
                        },
                        new Book
                        {
                            Title = "It",
                            Description = "A horror novel about a shape-shifting monster that terrorizes the town of Derry.",
                            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1a/It_%281986%29_front_cover%2C_first_edition.jpg/800px-It_%281986%29_front_cover%2C_first_edition.jpg",
                            Rating = 8.7,
                            PublishDate = new DateTime(1986, 9, 15),
                            IsTaken = false
                        },
                        new Book
                        {
                            Title = "Crime and Punishment",
                            Description = "A psychological novel that explores the mind of a young man who commits a murder and deals with the consequences.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/4/4b/Crimeandpunishmentcover.png",
                            Rating = 9.5,
                            PublishDate = new DateTime(1866, 12, 22),
                            IsTaken = false
                        },
                        new Book
                        {
                            Title = "Sleeping Beauties",
                            Description = "A supernatural thriller where women around the world fall into a mysterious sleep, and men are left to face chaos and a strange phenomenon.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/f/ff/Sleeping_Beauties_novel.png",
                            Rating = 8.3,
                            PublishDate = new DateTime(2017, 9, 26),
                            IsTaken = false
                        },
                        new Book
                        {
                            Title = "The Shining",
                            Description = "A psychological horror novel about a family's stay at an isolated hotel, where the supernatural forces of the hotel drive the father to madness.",
                            Image = "https://upload.wikimedia.org/wikipedia/commons/0/09/The_Shining_%281977%29_front_cover%2C_first_edition.jpg",
                            Rating = 9.0,
                            PublishDate = new DateTime(1977, 1, 28),
                            IsTaken = false
                        },
                        new Book
                        {
                            Title = "The Talisman",
                            Description = "A fantasy novel about a young boy's journey through parallel universes to find a magical talisman to save his dying mother.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/8/89/Talisman1983Cover.jpg",
                            Rating = 8.6,
                            PublishDate = new DateTime(1984, 11, 8),
                            IsTaken = false
                        },
                        new Book
                        {
                            Title = "The Lord of the Rings",
                            Description = "An epic high fantasy trilogy that follows the quest of a group of characters to destroy the powerful One Ring and defeat the Dark Lord.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif",
                            Rating = 9.8,
                            PublishDate = new DateTime(1954, 7, 29),
                            IsTaken = false
                        },
                        new Book
                        {
                            Title = "The Hobbit",
                            Description = "A fantasy novel that serves as a prelude to 'The Lord of the Rings,' following the adventures of Bilbo Baggins as he embarks on a quest with a group of dwarves.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/4/4a/TheHobbit_FirstEdition.jpg",
                            Rating = 9.1,
                            PublishDate = new DateTime(1937, 9, 21),
                            IsTaken = false
                        },
                        new Book
                        {
                            Title = "Neverwhere",
                            Description = "A dark urban fantasy novel about an ordinary man who enters the hidden world of London Below and becomes embroiled in a dangerous adventure.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/1/13/Neverwhere.jpg",
                            Rating = 8.4,
                            PublishDate = new DateTime(1996, 9, 16),
                            IsTaken = false
                        },
                        new Book
                        {
                            Title = "American Gods",
                            Description = "A contemporary fantasy novel that explores the clash between the old gods of mythology and the new gods of technology in America.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/4/49/American_gods.jpg",
                            Rating = 8.9,
                            PublishDate = new DateTime(2001, 6, 19),
                            IsTaken = false
                        }
                    });

                context.SaveChanges();


                await context.AuthorBooks.AddRangeAsync(new List<AuthorBook>()
                    {
                        new AuthorBook
                        {
                            AuthorId = 1,
                            BookId = 1,
                        },
                        new AuthorBook
                        {
                            AuthorId = 2,
                            BookId = 1,
                        },
                        new AuthorBook
                        {
                            AuthorId = 3,
                            BookId = 2,
                        },
                        new AuthorBook
                        {
                            AuthorId = 4,
                            BookId = 3,
                        },
                        new AuthorBook
                        {
                            AuthorId = 3,
                            BookId = 4,
                        },
                        new AuthorBook
                        {
                            AuthorId = 5,
                            BookId = 4,
                        },
                        new AuthorBook
                        {
                            AuthorId = 3,
                            BookId = 5,
                        },
                        new AuthorBook
                        {
                            AuthorId = 3,
                            BookId = 6,
                        },
                        new AuthorBook
                        {
                            AuthorId = 6,
                            BookId = 6,
                        },
                        new AuthorBook
                        {
                            AuthorId = 7,
                            BookId = 7,
                        },
                        new AuthorBook
                        {
                            AuthorId = 7,
                            BookId = 8,
                        },
                        new AuthorBook
                        {
                            AuthorId = 2,
                            BookId = 9,
                        },
                        new AuthorBook
                        {
                            AuthorId = 2,
                            BookId = 10,
                        }
                    });

                await context.SaveChangesAsync();


            }
        }
    }
}
