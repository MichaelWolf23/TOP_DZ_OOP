using System;
using System.Collections.Generic;

public abstract class Document
{
    protected string Author { get; set; }
    public abstract void Render();
    protected Document(string author)
    {
        Author = author;
    }
}

public class TextDocument : Document
{
    public string Content {  get; set; }
    public TextDocument(string author, string content) : base(author)
    {
        Content = content;
    }
    public override void Render()
    {
        Console.WriteLine($"[Текст] Автор: {Author}");
        Console.WriteLine($"Содержимое: {Content}");
    }
}
public class ImageDocument : Document
{
    public string Resolution { get; set; }
    public ImageDocument(string author, string resolution) : base(author)
    {
        Resolution = resolution;
    }
    public override void Render()
    {
        Console.WriteLine($"[Изображение] Автор: {Author}");
        Console.WriteLine($"Рендеринг изображения с разрешением {Resolution}");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Document> documents = new List<Document>()
        {
            new TextDocument("Лев Толстой", " Все счастливые семьи похожи друг на друга..."),
            new ImageDocument("Иван Шишкин", "3558x2180"),
            new TextDocument("Михаил Булгаков", "В белом плаще с кровавым подбоем..."),

        };
        Console.WriteLine("--- Рендеринг документов ---\n");

        Console.WriteLine("Начинаю рендеринг...");
        foreach (Document document in documents)
        {
            Console.WriteLine("--------------------");
            document.Render();
        }
    }
}