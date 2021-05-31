using AuthorInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorInfo.API.Contexts
{
    public class AuthorInfoContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public AuthorInfoContext(DbContextOptions<AuthorInfoContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasData(
                new Author()
                {
                    Id = 1,
                    Name = "Ian Rankin",
                    ShortBio = "Ian James Rankin (born 28 April 1960) is a Scottish crime writer, best known for his Inspector Rebus novels."
                },
                new Author()
                {
                    Id = 2,
                    Name = "Philip Kerr",
                    ShortBio = "Philip Ballantyne Kerr (22 February 1956 – 23 March 2018) was a British author, best known for his Bernie Gunther series of historical detective thrillers."
                },
                new Author()
                {
                    Id = 3,
                    Name = "Adrian McKinty",
                    ShortBio = "Adrian McKinty is a Northern Irish writer of crime and mystery novels and young adult fiction, best known for his Sean Duffy novels set in Northern Ireland during The Troubles."
                });

            modelBuilder.Entity<Book>()
                .HasData(
                new Book()
                {
                    Id = 1,
                    AuthorId = 1,
                    Title = "Knots and Crosses",
                    Synopsis = "Knots and Crosses is a 1987 crime novel by Ian Rankin. It is the first of the Inspector Rebus novels. It was written while Rankin was a postgraduate student at the University of Edinburgh."
                },
                new Book()
                {
                    Id = 2,
                    AuthorId = 1,
                    Title = "Hide and Seek",
                    Synopsis = "Hide and Seek is a 1991 crime novel by Ian Rankin. It is the second of the Inspector Rebus novels."
                },
                new Book()
                {
                    Id = 3,
                    AuthorId = 2,
                    Title = "March Violets",
                    Synopsis = "March Violets is a historical detective novel and the first written by Philip Kerr featuring detective Bernhard \"Bernie\" Gunther. March Violets is the first of the trilogy by Kerr called Berlin Noir."
                },
                new Book()
                {
                    Id = 4,
                    AuthorId = 2,
                    Title = "The Pale Criminal",
                    Synopsis = "The Pale Criminal is a historical detective novel and the second in the Berlin Noir trilogy of Bernhard Gunther novels written by Philip Kerr."
                },
                new Book()
                {
                    Id = 5,
                    AuthorId = 3,
                    Title = "Dead I Well May Be",
                    Synopsis = "Dead I Well May be is a 2003 novel by Irish/Australian author Adrian McKinty. It is his second novel, following Orange Rhymes With Everything, and was nominated for the CWA Ian Fleming Steel Dagger award for the best thriller of the year."
                },
                new Book()
                {
                    Id = 6,
                    AuthorId = 3,
                    Title = "In the Morning I'll Be Gone",
                    Synopsis = "In the Morning I'll be Gone is a 2014 novel by Belfast born novelist Adrian McKinty which won the 2014 Ned Kelly Award for Best Novel. It is the third in the author's Sean Duffy series, following The Cold Cold Ground and I Hear the Sirens in the Street."
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
