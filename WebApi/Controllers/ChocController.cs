using Microsoft.AspNetCore.Mvc;
using NStandard.Text.Json;
using Spread;
using Spread.Test;
using System.Text.Json;

namespace ReactWeb.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ChocController : Controller
{
    private readonly ILogger<ChocController> _logger;

    public ChocController(ILogger<ChocController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ContentResult Get()
    {
        var book = new SpreadBook
        {
            new SpreadSheet("simple")
            {
                SimpleSheet.Grid,
            }
        };

        var xml = JsonXmlSerializer.DeserializeXmlNode(book.ToJson(new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            WriteIndented = true
        })).OuterXml;

        return Json();
    }

    [HttpGet]
    public ContentResult Json()
    {
        var book = new SpreadBook
        {
            new SpreadSheet("simple")
            {
                SimpleSheet.Grid,
            }
        };

        return new ContentResult
        {
            ContentType = "text/json",
            Content = book.ToJson(new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                WriteIndented = true
            }),
        };
    }

    [HttpGet]
    public ContentResult Xml()
    {
        var book = new SpreadBook
        {
            new SpreadSheet("simple")
            {
                SimpleSheet.Grid,
            }
        };

        return new ContentResult
        {
            ContentType = "text/xml",
            Content = JsonXmlSerializer.DeserializeXmlNode(book.ToJson(new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                WriteIndented = true
            })).OuterXml,
        };
    }
}