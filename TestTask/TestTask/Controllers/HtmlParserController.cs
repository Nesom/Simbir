using System;
using System.Collections.Generic;
using TestTask.Data;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using ProjectInformatics.Logging;
using Microsoft.Extensions.Logging;

namespace TestTask.Controllers
{
    public class HtmlParserController : Controller
    {
        private IDatabase db;
        public HtmlParserController(IDatabase context, ILogger<HtmlParserController> logger)
        {
            db = LoggingAdvice<IDatabase>.Create(context, logger);
        }

        /// <summary>
        /// Загружает страницу и выводит результат
        /// </summary>
        /// <param name="text">Ссылка на веб-страницу</param>
        /// <returns></returns>
        public IActionResult LoadWebPage(string text)
        {
            var web = new HtmlWeb();
            var dict = new Dictionary<string, int>();
            web.OverrideEncoding = System.Text.Encoding.UTF8;
            var nodes = new List<HtmlNode>();
            try
            {
                var doc = web.Load(text);
                dict = ParsePage(doc);
            }
            catch(UriFormatException)
            {
                return View("Views/Home/Index.cshtml", "URI was not in the correct format");
            }
            catch (ArgumentNullException)
            {
                return View("Views/Home/Index.cshtml", "Value cannot be null");
            }
            return View(dict);
        }
        /// <summary>
        /// Парсит страницу по словам и сохраняет в базу данных
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public Dictionary<string, int> ParsePage(HtmlDocument doc)
        {
            var dict = new Dictionary<string, int>();
            var htmlNodes = doc.DocumentNode.SelectNodes("//body");
            foreach (var e in htmlNodes)
            {
                var splittedText = e.InnerText.Split(new char[] { ' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in splittedText)
                {
                    if (!dict.ContainsKey(word))
                        dict[word] = 0;
                    dict[word]++;
                }
            }
            db.AddToDb(dict);
            System.IO.File.WriteAllText("web.html", doc.ParsedText);
            return dict;
        }
    }
}