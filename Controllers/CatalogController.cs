using Microsoft.AspNetCore.Mvc;
using LibraryData;
using System.Linq;
using LibraryCatalog2.Models.Catalog;
using LibraryServices;

namespace Library1.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAsset _assets;
        public CatalogController(ILibraryAsset assets)
        {
            _assets = assets;
        }

        public IActionResult Index()
        {
            var AssetModels = _assets.GetAll(); //makes data available from the getall function from the interface

            var ListingResult = AssetModels //contains all the data that we want to present in the view 
                .Select(result => new AssetIndexListingModel
                {
                    Id = result.Id,
                    ImageUrl = result.ImageUrl,
                    AuthorOrDirector = _assets.GetAuthorOrDirectorById(result.Id),
                    DeweyCallNumber = _assets.GetDeweyIndex(result.Id),
                    Title = result.Title,
                    Type = _assets.GetType(result.Id)
                });
            
            //var model = new AssetIndexModel()
            //{
              //  Assets = ListingResult
            //};
            return View(ListingResult);
        }
    }
}