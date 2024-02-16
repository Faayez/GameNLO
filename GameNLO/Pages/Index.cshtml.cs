using GameNLO.Extensions;
using GameNLO.Models;
using GameNLO.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameNLO.Pages
{
    public class IndexModel(GameEngine engine, IGameService gameService) : PageModel
    {
        public BoxModel[,] Map { get; set; } = engine.GameModel.Map;

        [BindProperty]
        public int SelectedNodeLatitude { get; set; }

        [BindProperty]
        public int SelectedNodeLongitude { get; set; }

        public string? Message { get; set; }

        public bool IsUserEligible { get; set; }

        public IActionResult OnGet()
        {
            ValidateUser();

            return Page();
        }

        public IActionResult OnPost()
        {
            ValidateUser();
            if (!IsUserEligible)
            {
                return new PageResult();
            }

            var user = GetUser();
            var prize = gameService.OpenBox(user, SelectedNodeLatitude, SelectedNodeLongitude);
            if (prize.HasValue)
            {
                Message = $"You have one {prize.Value:C0}";
            }
            else
            {
                Message = "Maybe next time :(";
            }

            return new PageResult();
        }

        private UserModel GetUser()
        {
            return new UserModel
            {
                Id = HttpContext.GetOrCreateUserId()
            };
        }

        private void ValidateUser()
        {
            var userId = HttpContext.GetUserId();
            if (userId.HasValue)
            {
                IsUserEligible = gameService.IsUserEligible(userId.Value);
                if (!IsUserEligible)
                {
                    Message = "You have played this round. You are not allowed to play again.";
                }
            }
            else
            {
                IsUserEligible = true;
            }
        }
    }
}
