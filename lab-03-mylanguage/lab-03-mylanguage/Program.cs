using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

internal class Program
{
    private static async Task Main(string[] args)
    {
        RandomizerFullName fullNameGenerator = new(new FieldOptionsFullName());
        RandomizerEmailAddress emailAddressGenerator = new(new FieldOptionsEmailAddress());
        RandomizerCountry countryGenerator = new(new FieldOptionsCountry());
        RandomizerCity cityGenerator = new(new FieldOptionsCity());

        string template = "{0} from {1}, {2}.\nYour email address: {3}";

        var generateData = () =>
        {
            List<string> data = new()
            {
                fullNameGenerator.Generate() ?? "ERR",
                cityGenerator.Generate() ?? "ERR",
                countryGenerator.Generate() ?? "ERR",
                emailAddressGenerator.Generate() ?? "ERR"
            };
            return data.ToArray();
        };

        if (args.Length != 0 && args[0] == "serve")
            await StartListener(template , generateData);
        else
            GenerateRandomData(template, generateData);
    }

    private static void GenerateRandomData(string template, Func<string[]> generateData)
    {
        string formattedTemplate = Regex.Replace(template, @"\{\d\}", "{#}");

        WriteString(formattedTemplate, generateData());

    }
    private static void WriteString(string line, params string?[] strings)
    {
        string[] lineTokens = line.Split("{#}");

        Console.OutputEncoding = Encoding.UTF8;
        for (int i = 0; i < lineTokens.Length; i++)
        {
            Console.Write(lineTokens[i]);
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (i < strings.Length)
                Console.Write(strings[i]);
            Console.ResetColor();
        }
    }

    private static async Task StartListener(string template, Func<string[]> generateData)
    {
        HttpListener server = new();

        server.Prefixes.Add("http://*:8080/");
        server.Start();
        Console.WriteLine("Listening http://*:8080/");

        while (true)
        {
            var context = await server.GetContextAsync();

            string colorTag = "<font color=\"cyan\">$&</font>";
            string formattedTemplate = Regex.Replace(template, @"\{\d\}", colorTag).Replace("\n", @"<br/>");

            var response = context.Response;
            string responseText =
    @$"<!DOCTYPE html>
    <html>
        <head>
            <meta charset='utf8'>
            <title>METANIT.COM</title>
        </head>
        <body>
            <h2>
            {string.Format(formattedTemplate, generateData())}</h2>
        </body>
    </html>
            ";

            byte[] buffer = Encoding.UTF8.GetBytes(responseText);

            response.ContentLength64 = buffer.Length;
            using Stream output = response.OutputStream;
            await output.WriteAsync(buffer);
            await output.FlushAsync();
        }
    }
}