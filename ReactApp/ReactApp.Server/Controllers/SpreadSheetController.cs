using Microsoft.AspNetCore.Mvc;
using Northwnd;
using NStandard.Drawing;
using Spread;
using System.Text.Json;

namespace ReactApp.Server.Controllers;

[Route("[controller]/[action]")]
public class SpreadSheetController : Controller
{
    private readonly ILogger<WeatherForecastController> _logger;

    public SpreadSheetController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ISpreadSheet Sheet1()
    {
        var context = new NorthwndMemoryContext().IncludeAll();
        var content = SpreadStyle.Default with
        {
            Border = true,
            Center = true,
        };
        var sheet = new SpreadSheet("Categories")
        {
            new Vert(content)
            {
                new Hori(content with
                {
                    Bold = true,
                    BackgroundColor = new(0x61AFFE),
                })
                {
                    "Category", "Product",
                    new Vert
                    {
                        "Profit",
                        new Hori
                        {
                            "Year", "Sale", "Total"
                        }
                    }
                },

                from c in context.Categories
                select new Hori
                {
                    c.CategoryName,
                    new Vert
                    {
                        from p in c.Products
                        orderby p.UnitPrice
                        select new Hori
                        {
                            p.ProductName,

                            new Vert
                            {
                                from o in p.OrderDetails
                                group o by o.OrderLink.OrderDate!.Value.Year into g
                                select new Hori
                                {
                                    g.Key,
                                    g.Sum(x => x.UnitPrice * x.Quantity),
                                }
                            },

                            p.OrderDetails.Sum(x => x.UnitPrice * x.Quantity),
                        }
                    }
                }
            }
        };
        return sheet;
    }
}
