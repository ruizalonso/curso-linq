using System;
using System.Globalization;
using System.Text.Json;
class LinqQueries
{
    private List<Book> books = new List<Book>();
    public LinqQueries()
    {
        using (StreamReader reader = new StreamReader("books.json"))
        {
            string json = reader.ReadToEnd();
            List<Book>? allBooks = JsonSerializer.Deserialize<List<Book>>(json, new System.Text.Json.JsonSerializerOptions()
            { PropertyNameCaseInsensitive = true });
            if (allBooks != null)
                this.books = allBooks;
        }
    }

    public List<Book> GetBooks() => this.books.OrderBy(p => p.PublishedDate).ToList();
    public List<Book> GetBooksByPublishYear() => this.books.Where(p => p.PublishedDate.Year >= 2000).OrderBy(p => p.PublishedDate).ToList();
    public List<Book> GetBooksByPagesAndAction() => this.books.Where(p => p.PageCount > 250 && p.Title.ToLower().Contains("in action")).OrderBy(p => p.Title).ToList();
    public List<Book> GetBooksByPublishYear(int year) => this.books.Where(p => p.PublishedDate.Year == year).OrderBy(p => p.Title).ToList();
    //All -> todos los elementos de una colección deben cumplir una condicion
    public bool AllBooksWithStatus() => this.books.All(p => p.Status != string.Empty);
    // any -> algun elemento de la colección cumple la condicion. 
    public bool AnyPublishByYear(int year) => this.books.Any(p => p.PublishedDate.Year == year);
    // OrderBy 
    public List<Book> GetBooksByCategory(string category) => this.books.Where(p => p.Categories.Contains(category)).OrderBy(p => p.Title).ToList();
    // Take -> Seleccionar cantidad especifica de elementos
    public List<Book> GetMotsRecentBooks(string category) => this.books.Where(p => p.Categories.Contains(category)).OrderByDescending(p => p.PublishedDate).Take(3).ToList();
    // Skip -> Omitir cantidad especifica de elementos y devolver los restantes 
    public List<Book> GetTowBooks() => this.books.Where(p => p.PageCount > 400).OrderByDescending(p => p.PageCount).Take(4).Skip(2).ToList();
    public List<Book> GetTowBooks2() => this.books.Where(p => p.PageCount > 400).OrderByDescending(p => p.PageCount).ToList();
    public List<Book> GetSelectedBooks() => this.books.Take(3).Select(p => new Book() { Title = p.Title, PageCount = p.PageCount }).ToList();
    // Count y LongCount pueden recibir condiciones
    public long CountBooks() => this.books.LongCount(p => p.PageCount >= 200 && p.PageCount <= 500);
    public DateTime GetOldestPublishBook() => this.books.Min(p => p.PublishedDate);
    public int GetBookWithTheMostPages() => this.books.Max(p => p.PageCount);
    public Book GetBookWithTheMinPages() => this.books.Where(p => p.PageCount > 0).MinBy(p => p.PageCount);
    public Book GetLatestPublishBook() => this.books.MaxBy(p => p.PublishedDate);
    public int TotalPagesOfBooks() => this.books.Where(p => p.PageCount >= 0 && p.PageCount <= 500).Sum(p => p.PageCount);
    //Sirve para hacer acumulación o concatenación de valores. ej: sumas, multiplicaciones, concatenaciones de strings, etc
    public string GetBooksPostTo2015() => this.books
        .Where(p => p.PublishedDate.Year > 2015)
        .Aggregate("", (title, next) =>
        {
            if (title != string.Empty)
            {
                title += " - " + next.Title;
            }
            else
            {
                title += next.Title;
            }
            return title;
        });
    public double GetTitleCharactersMedia() => this.books.Average(p => p.Title.Length);
    public double GetBookPagesMedia() => this.books.Where(p => p.PageCount > 0).Average(p => p.PageCount);
    public List<IGrouping<int, Book>> GetBooksSince2000OrderByYear() => this.books.Where(p => p.PublishedDate.Year >= 2000).GroupBy(p => p.PublishedDate.Year).ToList();
    //Agrupa elementos de un diccionario
     public ILookup<char, Book> GetBooksForFisrtLetter() => this.books.ToLookup(p => p.Title[0], p => p);
}
