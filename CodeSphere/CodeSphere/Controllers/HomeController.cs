using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Models;
using CodeSphere.Repositories;
using CodeSphere.ViewModels.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeSphere.Data.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository homeRepository;

        public HomeController(IHomeRepository homeRepository)
        {
            this.homeRepository = homeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)).Cast<Roles>().ToArray())
            {
                _ = await this.homeRepository.CreateRole(role.ToString());
            }

            HomeViewModel model = new HomeViewModel
            {
                TotalRegisteredUsers = this.homeRepository.GetRegisteredUsersCount(),
                TotalBlogPosts = this.homeRepository.GetPostsCount(),
                Administrators = await this.homeRepository.GetAllAdministrators(),
            };

            return this.View(model);
        }

        [HttpGet]
        public IActionResult GetLatestBlogPosts()
        {
            ICollection<HomeLatestPostViewModel> latestPosts = this.homeRepository.GetLatestPosts();
            return new JsonResult(latestPosts);
        }

        [HttpGet]
        public async Task<IActionResult> GetHolidayTheme()
        {
            ICollection<string> icons = await this.homeRepository.GetHolidayThemeIcons();
            return new JsonResult(icons);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
