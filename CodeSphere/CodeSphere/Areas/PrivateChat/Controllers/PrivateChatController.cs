using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using CodeSphere.Areas.PrivateChat.ViewModels.PrivateChat;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.Areas.PrivateChat.Repositories.PrivateChat;

namespace CodeSphere.Areas.PrivateChat.Controllers
{
    [Authorize]
    [Area(GlobalConstants.PrivateChatArea)]
    public class PrivateChatController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPrivateChatRepository privateChatRepository;

        public PrivateChatController(
            UserManager<ApplicationUser> userManager,
            IPrivateChatRepository privateChatRepository)
        {
            this.userManager = userManager;
            this.privateChatRepository = privateChatRepository;
        }

        [Route("PrivateChat/With/{username?}/Group/{group?}")]
        public async Task<IActionResult> Index(string username, string group)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            if (string.IsNullOrEmpty(group))
                group = string.Join(GlobalConstants.ChatGroupNameSeparator, new[] { currentUser.UserName, username }.OrderBy(x => x));

            bool isAvailableToChat = await this.privateChatRepository.IsUserAbleToChat(username, group, currentUser);

            if (!isAvailableToChat)
            {
                this.TempData["Error"] = ErrorMessages.NotAbleToChat;
                return this.RedirectToAction("Index", "Profile", new { username });
            }

            var model = new PrivateChatViewModel
            {
                FromUser = await this.userManager.GetUserAsync(this.HttpContext.User),
                ToUser = await this.userManager.FindByNameAsync(username),
                ChatMessages = await this.privateChatRepository.ExtractAllMessages(group),
                GroupName = group,
                Emojis = this.privateChatRepository.GetAllEmojis(),
                AllChatThemes = this.privateChatRepository.GetAllThemes(),
                ChatThemeViewModel = this.privateChatRepository.GetGroupTheme(group),
                AllStickers = this.privateChatRepository.GetAllStickers(currentUser),
                AllQuickChatReplies = this.privateChatRepository.GetAllQuickReplies(currentUser),
            };

            return this.View(model);
        }

        [HttpGet]
        [Route("PrivateChat/With/{username?}/Group/{group?}/LoadMoreMessages/{messagesSkipCount?}")]
        public async Task<IActionResult> LoadMoreMessages(string username, string group, int? messagesSkipCount)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            bool isAvailableToChat = await this.privateChatRepository.IsUserAbleToChat(username, group, currentUser);

            if (!isAvailableToChat)
            {
                this.TempData["Error"] = ErrorMessages.NotAbleToChat;
                return this.RedirectToAction("Index", "Profile", new { username });
            }

            if (messagesSkipCount == null)
            {
                messagesSkipCount = 0;
            }

            ICollection<LoadMoreMessagesViewModel> data =
                await this.privateChatRepository.LoadMoreMessages(group, (int)messagesSkipCount, currentUser);
            return new JsonResult(data);
        }

        [HttpPost]
        [Route("PrivateChat/With/{username?}/Group/{group?}/ChangeChatTheme")]
        public async Task ChangeChatTheme(string username, string group, string themeId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            bool isAvailableToChat = await this.privateChatRepository.IsUserAbleToChat(username, group, currentUser);

            if (isAvailableToChat)
            {
                await this.privateChatRepository.ChangeChatTheme(username, group, themeId);
            }
        }

        [HttpPost]
        [Route("PrivateChat/With/{toUsername?}/Group/{group?}/SendFiles")]
        public async Task<IActionResult> SendFiles(IList<IFormFile> files, string group, string toUsername, string fromUsername, string message)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            bool isAvailableToChat = await this.privateChatRepository.IsUserAbleToChat(toUsername, group, currentUser);

            if (!isAvailableToChat)
            {
                this.TempData["Error"] = ErrorMessages.NotAbleToChat;
                return this.RedirectToAction("Index", "Profile", new { Username = toUsername });
            }

            var result = await this.privateChatRepository
                .SendMessageWitFilesToUser(files, group, toUsername, fromUsername, message);

            return new JsonResult(result);
        }

        [HttpPost]
        [Route("PrivateChat/With/{username?}/Group/{group?}/AddChatQuickReply")]
        public async Task<IActionResult> AddChatQuickReply(string username, string group, string quickReplyText)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            bool isAvailableToChat = await this.privateChatRepository.IsUserAbleToChat(username, group, currentUser);

            if (!isAvailableToChat)
            {
                this.TempData["Error"] = ErrorMessages.NotAbleToChat;
                return this.RedirectToAction("Index", "Profile", new { Username = username });
            }

            if (quickReplyText == string.Empty || quickReplyText == null)
            {
                this.TempData["Error"] = ErrorMessages.CannotAddEmptyQuickReply;
                return this.RedirectToAction("Index", "Profile", new { Username = username });
            }

            QuickChatReplyViewModel result =
                await this.privateChatRepository.AddQuickChatReply(currentUser, quickReplyText);

            return new JsonResult(result);
        }

        [HttpPost]
        [Route("PrivateChat/With/{username?}/Group/{group?}/RemoveChatQuickReply")]
        public async Task<IActionResult> RemoveChatQuickReply(string username, string group, string id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            bool isAvailableToChat = await this.privateChatRepository.IsUserAbleToChat(username, group, currentUser);

            if (!isAvailableToChat)
            {
                this.TempData["Error"] = ErrorMessages.NotAbleToChat;
                return this.RedirectToAction("Index", "Profile", new { Username = username });
            }

            if (id == string.Empty || id == null)
            {
                this.TempData["Error"] = ErrorMessages.ChatQuickReplyDoesNotExist;
                return this.RedirectToAction("Index", "Profile", new { Username = username });
            }

            Tuple<bool, string> result = await this.privateChatRepository.RemoveQuickChatReply(currentUser, id);

            return new JsonResult(result);
        }
    }
}