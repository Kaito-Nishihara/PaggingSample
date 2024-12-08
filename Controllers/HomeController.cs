using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using PaggingSample.Models;
using PaggingSample.Repository;
using System.Diagnostics;
using X.PagedList;

namespace PaggingSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;

        public HomeController(ILogger<HomeController> logger,IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async  Task<IActionResult> Index(int? page, PagingDropDown PagingDropDown)
        {
            var products = (IPagedList<Product>)await _productRepository.GetListAsync(filter: x => true ,page: page ?? 1,size: 5);

            // 1ÉyÅ[ÉWÇ†ÇΩÇËÇÃï\é¶åèêî
            var pageSize = 5;
            var pageNumber = (page ?? 1); // pageÇ™nullÇÃèÍçáÇÕ1ÉyÅ[ÉWñ⁄Çï\é¶
            
            if (page is not null)
            {
                Thread.Sleep(1000);
                return PartialView("_ProductList", products);
            }
            
            return View(new ProductViewModel() { 
                PagedList = products,
                PagingDropDown = new PagingDropDown(),
            });           
        }

        public ActionResult ProductList(int? page)
        {
            var products = new List<Product>()
            {
                new Product(){Id = 1,Name = "Name1" , Price = "1,000â~"},
                new Product(){Id = 2,Name = "Name2" , Price = "2,000â~"},
                new Product(){Id = 3,Name = "Name3" , Price = "3,000â~"},
                new Product(){Id = 4,Name = "Name4" , Price = "4,000â~"},
                new Product(){Id = 5,Name = "Name5" , Price = "5,000â~"},
                new Product(){Id = 6,Name = "Name6" , Price = "6,000â~"},
                new Product(){Id = 7,Name = "Name7" , Price = "7,000â~"},
                new Product(){Id = 8,Name = "Name8" , Price = "8,000â~"},
                new Product(){Id = 9,Name = "Name9" , Price = "9,000â~"},
                new Product(){Id = 10,Name = "Name10" , Price = "10,000â~"},
            };
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            // ïîï™ÉrÉÖÅ[Çï‘Ç∑
            return PartialView("_ProductList", products.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Privacy()
        {
            var viewModel = new PrivateModel()
            {
                DecimalValue = 10000
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
