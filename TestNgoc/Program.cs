using Microsoft.EntityFrameworkCore;

public class Program
{
  public abstract class Question
  {
    public Guid Id { get; set; }

    public string QuestionText { get; set; } = null!;

    public double Mark { get; set; }

    public string Answer { get; set; } = null!;

    public abstract bool CheckAnswer(string userAnswer);
  }

  public class ListeningQuestion : Question
  {
    public string AudioUrl { get; set; } = null!;

    public override bool CheckAnswer(string userAnswer)
    {
      throw new NotImplementedException();
    }
  }

  public class ReadingQuestion : Question
  {
    public string Paragraph { get; set; } = null!;

    public override bool CheckAnswer(string userAnswer)
    {
      throw new NotImplementedException();
    }
  }

  public class AppDbContext : DbContext
  {
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<ListeningQuestion> ListeningQuestions { get; set; } = null!;
    public DbSet<ReadingQuestion> ReadingQuestions { get; set; } = null!;

    public AppDbContext()
    {
      this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);

      optionsBuilder.UseSqlite("Data source=data.sqlite3");

      optionsBuilder.LogTo(s => Console.WriteLine(s), Microsoft.Extensions.Logging.LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Question>().ToTable("Questions");
      modelBuilder.Entity<ListeningQuestion>().ToTable("Listening_Questions");
      modelBuilder.Entity<ReadingQuestion>().ToTable("Reading_Questions");
    }
  }

  /***************************************************************************/

  private static void Main(string[] args)
  {
    using var dbs = new AppDbContext();

    var listeningQuestion = new ListeningQuestion()
    {
      AudioUrl = "https://djfkls",
      Mark = 2.0,
      QuestionText = "Nhập những gì bạn nghe được",
      Answer = "bla bla"
    };

    dbs.ListeningQuestions.Add(listeningQuestion);

    dbs.SaveChanges();

    Console.ReadLine();

    var query = dbs.ListeningQuestions.Where(u => u.Answer == "A").OrderBy(u => u.Id);
    //IEnumerable<ListeningQuestion>
    //Head -> Node1 -> Node2 -> Node3 -> ..
    //Ở 1 node bất kì, muốn đi tới node tiếp -> next()
    //Lập lịch
    // Entityframework -> hỗ trợ chuyển cái lịch đó thành SQL
    // Làm sao để biết khi nào chuyển
    // tolist, tolistasync, foreach, await foreach, count,...

    foreach (var question in query)
    {
      //...
    }
  }
}

// Giai thich ienumerable