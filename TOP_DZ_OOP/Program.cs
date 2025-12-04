using System;
using System.Collections.Generic;
using System.Text;


public interface ITextPlugin
{
    string Process(string input);
}

public class ToUpperPlugin : ITextPlugin
{
    public string Process(string input)
    {
        return input.ToUpper();
    }
}

public class SpaceRemoverPlugin : ITextPlugin
{
    public string Process(string input)
    {
        return input.Replace(" ", "");
    }
}

public class ReversePlugin : ITextPlugin
{
    public string Process(string input)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = input.Length - 1; i >= 0; i--)
        {
            sb.Append(input[i]);
        }

        return sb.ToString();
    }
}

public class TranslitPlugin : ITextPlugin
{
    private readonly Dictionary<char, string> _translitMap = new Dictionary<char, string>
        {
            {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"}, {'е', "e"}, {'ё', "yo"},
            {'ж', "zh"}, {'з', "z"}, {'и', "i"}, {'й', "y"}, {'к', "k"}, {'л', "l"}, {'м', "m"},
            {'н', "n"}, {'о', "o"}, {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"}, {'у', "u"},
            {'ф', "f"}, {'х', "kh"}, {'ц', "ts"}, {'ч', "ch"}, {'ш', "sh"}, {'щ', "shch"},
            {'ъ', ""}, {'ы', "y"}, {'ь', ""}, {'э', "e"}, {'ю', "yu"}, {'я', "ya"}
        };

    public string Process(string input)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char c in input)
        {
            char lowerC = char.ToLower(c);

            if (_translitMap.ContainsKey(lowerC))
            {
                string replacement = _translitMap[lowerC];

                if (char.IsUpper(c) && replacement.Length > 0)
                {
                    replacement = char.ToUpper(replacement[0]) + replacement.Substring(1);
                }
                sb.Append(replacement);
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}
public static class TextProcessor
{
    public static string TextTransform(string text, List<ITextPlugin> plugins)
    {
        foreach (ITextPlugin plugin in plugins)
        {
            text = plugin.Process(text);
        }
        return text;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        string text = "Hello World! This is a test.";
        string textRu = "Привет Мир! Это тест.";

        List<ITextPlugin> plugins = new List<ITextPlugin>
        { 
            new ToUpperPlugin(),
            new SpaceRemoverPlugin(),
            new ReversePlugin()
        };

        List<ITextPlugin> pluginTranslit = new List<ITextPlugin>
        {
            new TranslitPlugin()
        };


        Console.WriteLine("--- Система обработки текста на плагинах ---\n");
        Console.WriteLine($"Исходная строка: {text}\n");
        Console.WriteLine("Примененные плагины:");

        foreach (ITextPlugin plugin in plugins)
        {
            Console.WriteLine($"- {plugin}");
        }

        Console.WriteLine($"\nРезультат после обработки: {TextProcessor.TextTransform(text, plugins)}");

        Console.WriteLine("\n--- Система обработки текста на плагине TranslitPlugin ---\n");
        Console.WriteLine($"Исходная строка: {textRu}\n");
        Console.WriteLine("Примененные плагины:");

        foreach (ITextPlugin plugin in pluginTranslit)
        {
            Console.WriteLine($"- {plugin}");
        }

        Console.WriteLine($"\nРезультат после обработки: {TextProcessor.TextTransform(textRu, pluginTranslit)}");
    }
}