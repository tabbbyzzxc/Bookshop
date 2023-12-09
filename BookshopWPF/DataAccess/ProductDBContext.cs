using Bookshop.ProductsLib;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DataAccess
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
        {
            var dbExists = Database.EnsureCreated();
            if (dbExists)
            {
                GenerateRandomProducts();
            }

        }

        public ProductDbContext(DbContextOptions options)
            : base(options)
        {
            var dbExists = Database.EnsureCreated();
            if (dbExists)
            {
                GenerateRandomProducts();
            }
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderLine>().HasOne(o => o.Order).WithMany(o => o.OrderList).HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<InvoiceLine>().HasOne(o => o.Invoice).WithMany(o => o.InvoiceLines).HasForeignKey(o => o.InvoiceId);
            modelBuilder.Entity<CartLine>().HasOne(o => o.Cart).WithMany(o => o.CartLines).HasForeignKey(o => o.CartId);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString:
               "Server=localhost;Port=5432;User Id=postgres;Password=159874;Database=ProductDb;");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            base.OnConfiguring(optionsBuilder);
        }

        private void GenerateRandomProducts()
        {
            string[] languages = { "Ukrainian", "English", "Spanish", "French", "German" };
            string json = "[\r\n  {\r\n    \"Title\": \"To Kill a Mockingbird\",\r\n    \"Author\": \"Harper Lee\",\r\n    \"Description\": \"A classic novel set in the 1930s, exploring racial injustice and the loss of innocence in a small Southern town.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"1984\",\r\n    \"Author\": \"George Orwell\",\r\n    \"Description\": \"A dystopian novel set in a totalitarian society, where the government controls every aspect of people's lives.\",\r\n    \"Genre\": \"Science Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Great Gatsby\",\r\n    \"Author\": \"F. Scott Fitzgerald\",\r\n    \"Description\": \"A story of wealth, love, and the American Dream, set in the 1920s.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"Pride and Prejudice\",\r\n    \"Author\": \"Jane Austen\",\r\n    \"Description\": \"A classic romance novel featuring the spirited Elizabeth Bennet and the proud Mr. Darcy.\",\r\n    \"Genre\": \"Romance\"\r\n  },\r\n  {\r\n    \"Title\": \"To the Lighthouse\",\r\n    \"Author\": \"Virginia Woolf\",\r\n    \"Description\": \"A modernist novel that explores the inner lives of the Ramsay family during their summer vacations.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Catcher in the Rye\",\r\n    \"Author\": \"J.D. Salinger\",\r\n    \"Description\": \"A coming-of-age story narrated by Holden Caulfield, a disenchanted teenager in New York City.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Lord of the Rings\",\r\n    \"Author\": \"J.R.R. Tolkien\",\r\n    \"Description\": \"An epic fantasy trilogy that follows the journey of Frodo Baggins to destroy the One Ring and save Middle-earth.\",\r\n    \"Genre\": \"Fantasy\"\r\n  },\r\n  {\r\n    \"Title\": \"Harry Potter and the Philosopher's Stone\",\r\n    \"Author\": \"J.K. Rowling\",\r\n    \"Description\": \"The first book in the popular Harry Potter series, introducing the magical world of Hogwarts School of Witchcraft and Wizardry.\",\r\n    \"Genre\": \"Fantasy\"\r\n  },\r\n  {\r\n    \"Title\": \"The Hobbit\",\r\n    \"Author\": \"J.R.R. Tolkien\",\r\n    \"Description\": \"A fantasy adventure novel that follows Bilbo Baggins as he embarks on a quest to reclaim the Lonely Mountain from the dragon Smaug.\",\r\n    \"Genre\": \"Fantasy\"\r\n  },\r\n  {\r\n    \"Title\": \"Brave New World\",\r\n    \"Author\": \"Aldous Huxley\",\r\n    \"Description\": \"A dystopian novel set in a future society where citizens are genetically engineered and controlled by the state.\",\r\n    \"Genre\": \"Science Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"Moby-Dick\",\r\n    \"Author\": \"Herman Melville\",\r\n    \"Description\": \"A tale of obsession and revenge, as Captain Ahab pursues the great white whale across the oceans.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Odyssey\",\r\n    \"Author\": \"Homer\",\r\n    \"Description\": \"An ancient Greek epic poem that tells the story of Odysseus and his ten-year journey back home after the Trojan War.\",\r\n    \"Genre\": \"Classics\"\r\n  },\r\n  {\r\n    \"Title\": \"The Alchemist\",\r\n    \"Author\": \"Paulo Coelho\",\r\n    \"Description\": \"A philosophical novel about a young shepherd named Santiago who embarks on a journey to discover his personal legend.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Hunger Games\",\r\n    \"Author\": \"Suzanne Collins\",\r\n    \"Description\": \"The first book in a dystopian trilogy, following Katniss Everdeen as she fights for survival in a televised competition.\",\r\n    \"Genre\": \"Science Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"Jane Eyre\",\r\n    \"Author\": \"Charlotte Brontë\",\r\n    \"Description\": \"A Gothic romance novel that tells the story of Jane Eyre, an orphaned governess who falls in love with the brooding Mr. Rochester.\",\r\n    \"Genre\": \"Romance\"\r\n  },\r\n  {\r\n    \"Title\": \"The Picture of Dorian Gray\",\r\n    \"Author\": \"Oscar Wilde\",\r\n    \"Description\": \"A novel about a young man named Dorian Gray who remains eternally youthful while his portrait ages and shows the effects of his sins.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Chronicles of Narnia\",\r\n    \"Author\": \"C.S. Lewis\",\r\n    \"Description\": \"A series of fantasy novels that transports readers to the magical land of Narnia, where talking animals and mythical creatures exist.\",\r\n    \"Genre\": \"Fantasy\"\r\n  },\r\n  {\r\n    \"Title\": \"The Kite Runner\",\r\n    \"Author\": \"Khaled Hosseini\",\r\n\"Description\": \"A novel set in Afghanistan, exploring themes of guilt, redemption, and the enduring bond between friends.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"Lord of the Flies\",\r\n    \"Author\": \"William Golding\",\r\n    \"Description\": \"A story of a group of boys stranded on a deserted island, where their struggle for power and survival leads to chaos and violence.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"Gone with the Wind\",\r\n    \"Author\": \"Margaret Mitchell\",\r\n    \"Description\": \"An epic historical novel set in the American South during the Civil War and Reconstruction era, featuring the strong-willed Scarlett O'Hara.\",\r\n    \"Genre\": \"Historical Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Fault in Our Stars\",\r\n    \"Author\": \"John Green\",\r\n    \"Description\": \"A heart-wrenching story about two teenagers with cancer who fall in love and embark on a journey of love and loss.\",\r\n    \"Genre\": \"Young Adult\"\r\n  },\r\n  {\r\n    \"Title\": \"The Book Thief\",\r\n    \"Author\": \"Markus Zusak\",\r\n    \"Description\": \"A novel set in Nazi Germany, following a young girl named Liesel Meminger who steals books to share with others during World War II.\",\r\n    \"Genre\": \"Historical Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"A Game of Thrones\",\r\n    \"Author\": \"George R.R. Martin\",\r\n    \"Description\": \"The first book in the fantasy series 'A Song of Ice and Fire,' filled with political intrigue, epic battles, and complex characters.\",\r\n    \"Genre\": \"Fantasy\"\r\n  },\r\n  {\r\n    \"Title\": \"The Girl with the Dragon Tattoo\",\r\n    \"Author\": \"Stieg Larsson\",\r\n    \"Description\": \"A gripping crime thriller featuring Lisbeth Salander, a computer hacker, and Mikael Blomkvist, an investigative journalist.\",\r\n    \"Genre\": \"Mystery\"\r\n  },\r\n  {\r\n    \"Title\": \"The Little Prince\",\r\n    \"Author\": \"Antoine de Saint-Exupéry\",\r\n    \"Description\": \"A poetic novella that tells the story of a young prince who travels from planet to planet, learning important life lessons along the way.\",\r\n    \"Genre\": \"Children's Literature\"\r\n  },\r\n  {\r\n    \"Title\": \"The Da Vinci Code\",\r\n    \"Author\": \"Dan Brown\",\r\n    \"Description\": \"A fast-paced thriller featuring symbologist Robert Langdon as he uncovers hidden secrets and puzzles in art and history.\",\r\n    \"Genre\": \"Mystery\"\r\n  },\r\n  {\r\n    \"Title\": \"The Diary of a Young Girl\",\r\n    \"Author\": \"Anne Frank\",\r\n    \"Description\": \"The poignant diary of Anne Frank, a Jewish girl hiding from the Nazis during World War II, offering a glimpse into the horrors of the Holocaust.\",\r\n    \"Genre\": \"Biography\"\r\n  },\r\n  {\r\n    \"Title\": \"The Name of the Wind\",\r\n    \"Author\": \"Patrick Rothfuss\",\r\n    \"Description\": \"The first book in the fantasy series 'The Kingkiller Chronicle,' chronicling the life and adventures of the enigmatic Kvothe.\",\r\n    \"Genre\": \"Fantasy\"\r\n  },\r\n  {\r\n    \"Title\": \"The Handmaid's Tale\",\r\n    \"Author\": \"Margaret Atwood\",\r\n    \"Description\": \"A dystopian novel set in a totalitarian society where women are subjugated and used for reproductive purposes.\",\r\n    \"Genre\": \"Science Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Adventures of Huckleberry Finn\",\r\n    \"Author\": \"Mark Twain\",\r\n    \"Description\": \"A classic novel following the adventures of Huck Finn and his friend Jim, an escaped slave, as they travel along the Mississippi River.\",\r\n    \"Genre\": \"Adventure\"\r\n  },\r\n  {\r\n    \"Title\": \"The Shining\",\r\n    \"Author\": \"Stephen King\",\r\n    \"Description\": \"A horror novel about an aspiring writer and his family who become caretakers of an isolated hotel, where supernatural forces drive the father to madness.\",\r\n    \"Genre\": \"Horror\"\r\n  },\r\n  {\r\n    \"Title\": \"Frankenstein\",\r\n    \"Author\": \"Mary Shelley\",\r\n    \"Description\": \"A Gothic novel exploring the ethical and moral implications of creating life, as scientist Victor Frankenstein brings a creature to life with disastrous consequences.\",\r\n    \"Genre\": \"Horror\"\r\n  },\r\n  {\r\n    \"Title\": \"The Grapes of Wrath\",\r\n    \"Author\": \"John Steinbeck\",\r\n    \"Description\": \"A novel that follows the Joad family as they migrate from the Dust Bowl to California during the Great Depression.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"Slaughterhouse-Five\",\r\n    \"Author\": \"Kurt Vonnegut\",\r\n    \"Description\": \"A satirical novel that combines elements of science fiction and war, following the life of Billy Pilgrim, a World War II soldier who becomes unstuck in time.\",\r\n    \"Genre\": \"Science Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Scarlet Letter\",\r\n    \"Author\": \"Nathaniel Hawthorne\",\r\n    \"Description\": \"A novel set in 17th-century Puritan Boston, where a woman named Hester Prynne is condemned for committing adultery and must wear a scarlet letter 'A' as a mark of shame.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Count of Monte Cristo\",\r\n    \"Author\": \"Alexandre Dumas\",\r\n    \"Description\": \"An adventure novel that follows Edmond Dantès as he seeks revenge against those who wronged him and takes on a new identity as the wealthy and enigmatic Count of Monte Cristo.\",\r\n    \"Genre\": \"Adventure\"\r\n  },\r\n  {\r\n    \"Title\": \"The Lion, the Witch and the Wardrobe\",\r\n    \"Author\": \"C.S. Lewis\",\r\n    \"Description\": \"The first book in the fantasy series 'The Chronicles of Narnia,' where four siblings enter a magical wardrobe and find themselves in the land of Narnia, where they must battle the White Witch.\",\r\n    \"Genre\": \"Fantasy\"\r\n  },\r\n  {\r\n    \"Title\": \"Crime and Punishment\",\r\n    \"Author\": \"Fyodor Dostoevsky\",\r\n    \"Description\": \"A psychological novel that delves into the mind of Raskolnikov, a poor ex-student who commits a heinous crime and grapples with guilt and redemption.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"Wuthering Heights\",\r\n    \"Author\": \"Emily Brontë\",\r\n    \"Description\": \"A Gothic novel that follows the tumultuous and passionate relationship between Catherine Earnshaw and Heathcliff on the windswept moors of Yorkshire.\",\r\n    \"Genre\": \"Romance\"\r\n  },\r\n  {\r\n    \"Title\": \"One Hundred Years of Solitude\",\r\n    \"Author\": \"Gabriel García Márquez\",\r\n    \"Description\": \"A magical realist novel that chronicles the Buendía family across multiple generations in the fictional town of Macondo.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Brothers Karamazov\",\r\n    \"Author\": \"Fyodor Dostoevsky\",\r\n    \"Description\": \"A philosophical novel that explores themes of morality, faith, and family through the lives of the Karamazov brothers.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Divine Comedy\",\r\n    \"Author\": \"Dante Alighieri\",\r\n    \"Description\": \"An epic poem that takes the reader on a journey through Hell, Purgatory, and Heaven, guided by the poet Virgil.\",\r\n    \"Genre\": \"Poetry\"\r\n  },\r\n  {\r\n    \"Title\": \"Les Misérables\",\r\n    \"Author\": \"Victor Hugo\",\r\n    \"Description\": \"A historical novel set in 19th-century France, following the lives of Jean Valjean, Javert, and other characters during the social and political unrest of the time.\",\r\n    \"Genre\": \"Historical Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"A Tale of Two Cities\",\r\n    \"Author\": \"Charles Dickens\",\r\n    \"Description\": \"A historical novel set in London and Paris before and during the French Revolution, featuring a cast of characters caught up in the tumultuous events of the era.\",\r\n    \"Genre\": \"Historical Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"Don Quixote\",\r\n    \"Author\": \"Miguel de Cervantes\",\r\n    \"Description\": \"A satirical novel that follows the adventures of Alonso Quixano, who believes himself to be a knight-errant named Don Quixote, and his loyal squire Sancho Panza.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"Anna Karenina\",\r\n    \"Author\": \"Leo Tolstoy\",\r\n    \"Description\": \"A novel that explores themes of love, passion, and societal norms through the tragic affair between Anna Karenina and Count Vronsky.\",\r\n    \"Genre\": \"Romance\"\r\n  },\r\n  {\r\n    \"Title\": \"The Great Expectations\",\r\n    \"Author\": \"Charles Dickens\",\r\n    \"Description\": \"A bildungsroman novel that follows the life of Pip, an orphaned boy who rises from poverty to wealth and encounters various colorful characters along the way.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"War and Peace\",\r\n    \"Author\": \"Leo Tolstoy\",\r\n    \"Description\": \"A historical novel set during the Napoleonic Wars, which explores themes of love, war, and the human condition through the lives of several interconnected characters.\",\r\n    \"Genre\": \"Historical Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Adventures of Tom Sawyer\",\r\n    \"Author\": \"Mark Twain\",\r\n    \"Description\": \"A novel that follows the mischievous and imaginative young boy Tom Sawyer as he navigates the small-town life along the Mississippi River.\",\r\n    \"Genre\": \"Adventure\"\r\n  },\r\n  {\r\n    \"Title\": \"Sense and Sensibility\",\r\n    \"Author\": \"Jane Austen\",\r\n    \"Description\": \"A novel that contrasts the lives and romantic experiences of the Dashwood sisters, Elinor and Marianne, as they navigate the complexities of love and society.\",\r\n    \"Genre\": \"Romance\"\r\n  },\r\n  {\r\n    \"Title\": \"A Clockwork Orange\",\r\n    \"Author\": \"Anthony Burgess\",\r\n    \"Description\": \"A dystopian novel that follows the story of Alex, a teenager involved in violent and criminal activities, and explores themes of free will, morality, and rehabilitation.\",\r\n    \"Genre\": \"Science Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Adventures of Huckleberry Finn\",\r\n    \"Author\": \"Mark Twain\",\r\n    \"Description\": \"A novel that follows the adventures of Huck Finn and his friend Jim, an escaped slave, as they travel along the Mississippi River and challenge societal norms.\",\r\n    \"Genre\": \"Adventure\"\r\n  },\r\n  {\r\n    \"Title\": \"Slaughterhouse-Five\",\r\n    \"Author\": \"Kurt Vonnegut\",\r\n    \"Description\": \"A science fiction-infused anti-war novel that follows the experiences of Billy Pilgrim, a World War II soldier who becomes unstuck in time.\",\r\n\"Genre\": \"Science Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Sound and the Fury\",\r\n    \"Author\": \"William Faulkner\",\r\n    \"Description\": \"A modernist novel that employs multiple narrators to tell the story of the Compson family and explores themes of time, memory, and decay.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"Crime and Punishment\",\r\n    \"Author\": \"Fyodor Dostoevsky\",\r\n    \"Description\": \"A psychological novel that delves into the mind of Raskolnikov, a troubled student who commits a murder and grapples with the moral and psychological consequences.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Grapes of Wrath\",\r\n    \"Author\": \"John Steinbeck\",\r\n    \"Description\": \"A novel set during the Great Depression, which follows the Joad family as they leave their Oklahoma farm and migrate to California in search of a better life.\",\r\n    \"Genre\": \"Historical Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Old Man and the Sea\",\r\n    \"Author\": \"Ernest Hemingway\",\r\n    \"Description\": \"A novella that tells the story of an aging Cuban fisherman named Santiago and his battle with a giant marlin in the Gulf Stream.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"Wuthering Heights\",\r\n    \"Author\": \"Emily Brontë\",\r\n    \"Description\": \"A Gothic novel that unfolds the passionate and destructive love story of Heathcliff and Catherine Earnshaw in the Yorkshire moors.\",\r\n    \"Genre\": \"Romance\"\r\n  },\r\n  {\r\n    \"Title\": \"Frankenstein\",\r\n    \"Author\": \"Mary Shelley\",\r\n    \"Description\": \"A science fiction novel that explores themes of creation, identity, and the consequences of unchecked ambition through the story of Victor Frankenstein and his creature.\",\r\n    \"Genre\": \"Science Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"The Iliad\",\r\n    \"Author\": \"Homer\",\r\n    \"Description\": \"An ancient Greek epic poem that recounts the events of the Trojan War, including the rage of Achilles and the fall of Troy.\",\r\n    \"Genre\": \"Classics\"\r\n  },\r\n  {\r\n    \"Title\": \"The Count of Monte Cristo\",\r\n    \"Author\": \"Alexandre Dumas\",\r\n    \"Description\": \"An adventure novel that follows the story of Edmond Dantès, who is wrongfully imprisoned and seeks revenge against those who betrayed him.\",\r\n    \"Genre\": \"Adventure\"\r\n  },\r\n  {\r\n    \"Title\": \"The Scarlet Letter\",\r\n    \"Author\": \"Nathaniel Hawthorne\",\r\n    \"Description\": \"A novel set in 17th-century Puritan Boston, which explores themes of sin, guilt, and redemption through the story of Hester Prynne and her illicit child.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"Pride and Prejudice\",\r\n    \"Author\": \"Jane Austen\",\r\n    \"Description\": \"A classic romance novel that follows the story of Elizabeth Bennet and her relationship with the proud Mr. Darcy in 19th-century England.\",\r\n    \"Genre\": \"Romance\"\r\n  },\r\n  {\r\n    \"Title\": \"To Kill a Mockingbird\",\r\n    \"Author\": \"Harper Lee\",\r\n    \"Description\": \"A coming-of-age novel that addresses racial injustice and morality through the eyes of Scout Finch, a young girl growing up in the segregated Southern United States.\",\r\n    \"Genre\": \"Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"1984\",\r\n    \"Author\": \"George Orwell\",\r\n    \"Description\": \"A dystopian novel set in a totalitarian society where individuality and independent thought are suppressed, and surveillance and propaganda are pervasive.\",\r\n    \"Genre\": \"Science Fiction\"\r\n  },\r\n  {\r\n    \"Title\": \"Moby-Dick\",\r\n    \"Author\": \"Herman Melville\",\r\n    \"Description\": \"An epic novel that follows the obsessive quest of Captain Ahab to seek revenge on the white whale, Moby-Dick, and explores themes of fate, nature, and obsession.\",\r\n    \"Genre\": \"Adventure\"\r\n  }\r\n]";
            var random = new Random();
            List<Book> list = JsonSerializer.Deserialize<List<Book>>(json);


            IEnumerable<Book> books = list.Skip(20).Take(list.Count - 20).Select(x => new Book
            {
                Title = x.Title,
                Author = x.Author,
                Description = x.Description,
                Genre = x.Genre,
                PageQuantity = random.Next(100, 500),
                PaperType = AppConstants.PaperTypes[random.Next(AppConstants.PaperTypes.Length)],
                BuyPrice = random.Next(50, 120),
                SellPrice = random.Next(120, 200),
                Quantity = random.Next(1, 200),
                Language = languages[random.Next(languages.Length)],
                UniqueId = Guid.NewGuid()
            });

            Books.AddRange(books);

            SaveChanges();

            var listTemp = books.ToList();
            var oredrs = new List<Order>
            {
                new Order
                {
                    Date = DateTime.Now,
                    OrderList = new List<OrderLine>
                    {
                        new OrderLine { Book = listTemp[0], Quantity = 1 },
                        new OrderLine { Book = listTemp[1], Quantity = 2 }
                    }
                },
                new Order
                {
                    Date = DateTime.Now,
                    OrderList = new List<OrderLine>
                    {
                        new OrderLine { Book = listTemp[2], Quantity = 4 },
                        new OrderLine { Book = listTemp[3], Quantity = 2 }
                    }
                },
                new Order
                {
                    Date = DateTime.Now,
                    OrderList = new List<OrderLine>
                    {
                        new OrderLine { Book = listTemp[4], Quantity = 2 },
                        new OrderLine { Book = listTemp[8], Quantity = 2 },
                        new OrderLine { Book = listTemp[5], Quantity = 7 },
                        new OrderLine { Book = listTemp[5], Quantity = 4 }
                    }
                }
            };

            Orders.AddRange(oredrs);

            SaveChanges();
        }
    }
}
