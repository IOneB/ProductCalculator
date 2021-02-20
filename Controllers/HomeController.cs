using Microsoft.AspNetCore.Mvc;
using ProductCalculator.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCalculator.Controllers
{
    public class HomeController : Controller
    {
        static List<Product> _products = new List<Product>();

        static HomeController()
        {
            if (System.IO.File.Exists("products"))
                _products = JsonSerializer.Deserialize<List<Product>>(System.IO.File.ReadAllText("products"));
            else
                _products = new List<Product>();

            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(5000);
                    await System.IO.File.WriteAllTextAsync("products", JsonSerializer.Serialize(_products));
                }
            });
        }

        public IActionResult Index() => View(_products);

        [HttpPost]
        public void Save(List<Product> products)
        {
            _products = products;
        }
    }
}
