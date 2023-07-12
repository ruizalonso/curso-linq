// See https://aka.ms/new-console-template for more information
LinqQueries queries = new LinqQueries();

// PrintValues(queries.GetBooks());
// PrintValues(queries.GetBooksByPublishYear());
// PrintValues(queries.GetBooksByPagesAndAction());
// PrintValues(queries.GetBooksByPublishYear(1991));
// PrintValues(queries.GetBooksByCategory("Java"));
// PrintValues(queries.GetMotsRecentBooks("Java"));
// PrintValues(queries.GetTowBooks2());
// PrintValues(queries.GetTowBooks());
// PrintValues(queries.GetSelectedBooks());


// PrintValuesGruped(queries.GetBooksSince2000OrderByYear());
PrintValuesToDictionary(queries.GetBooksForFisrtLetter(), 'T');


// Console.WriteLine(queries.AllBooksWithStatus());
// Console.WriteLine(queries.AnyPublishByYear(1991));
// Console.WriteLine(queries.GetOldestPublishBook().ToShortDateString());
// Console.WriteLine(queries.GetBookWithTheMostPages());
// Console.WriteLine(queries.GetBookWithTheMinPages().Title);
// Console.WriteLine($"{queries.GetLatestPublishBook().Title} - {queries.GetLatestPublishBook().PublishedDate}");
// Console.WriteLine(queries.TotalPagesOfBooks());
// Console.WriteLine(queries.GetBooksPostTo2015());
// Console.WriteLine(queries.GetTitleCharactersMedia().ToString("0.00"));
// Console.WriteLine(queries.GetBookPagesMedia().ToString("0.00"));

void PrintValues(List<Book> booksList)
{
    Console.WriteLine("| {0, -60} | {1, 7} | {2, 11}\n", "Titulo", "Páginas", "Fecha de Publicación");
    booksList.ForEach(p => Console.WriteLine("| {0, -60} | {1, 7} | {2, 11}", p.Title, p.PageCount, p.PublishedDate.ToString("dd/MM/yyyy")));
    Console.WriteLine($"{booksList.Count()} libros encontrados");
}

void PrintValuesGruped(IEnumerable<IGrouping<int, Book>> list)
{
    foreach (var group in list)
    {
        Console.WriteLine($"Grupo: {group.Key}");
        Console.WriteLine("| {0, -60} | {1, 15} | {2, 15}\n ", "Título", "Págias", "Publicación");

        foreach(var item in group)
        {
            Console.WriteLine("| {0, -60} | {1, 15} | {2, 15}\n ", item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
        }
    }
}

void PrintValuesToDictionary(ILookup<char, Book> list, char letter) 
{
    Console.WriteLine("| {0, -60} | {1, 15} | {2, 15}\n ", "Título", "Págias", "Publicación");
    foreach(var item in list[letter])
    {
        Console.WriteLine("| {0, -60} | {1, 15} | {2, 15}\n ", item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
    }
}