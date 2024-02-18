using GameNLO.Extensions;
using GameNLO.Models;
using GameNLO.Services.GameEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameNLO.Pages
{
    public class IndexModel(GameEngine engine) : PageModel
    {
        public BoxModel[,] Map { get; set; }

        public DateTime EndDateTime { get; set; }

        public IActionResult OnGet()
        {
            HttpContext.GetOrCreateUserId();

            if (engine.GameModel is null)
            {
                Thread.Sleep(1000); //Just wait until the object is created
                return Redirect("/");
            }
            else
            {
                Map = engine.GameModel.Map;
                EndDateTime = engine.GameModel.EndTime;
            }

            return Page();
        }
    }
}
