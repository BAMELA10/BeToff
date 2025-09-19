using BeToff.BLL.Interface;
using BeToff.BLL.Service.Interface;
using BeToff.Web.Hubs;
using BeToff.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeToff.Entities;

namespace BeToff.Web.Controllers
{
    public class ChatController : Controller
    {
        protected readonly IChatService _chatService;
        protected readonly IUserBc _userBc;

        public ChatController(IChatService chatService, IUserBc userBc)
        {
            _chatService = chatService;
            _userBc = userBc;
        }
        public async Task<IActionResult> Index()
        {
            var CurrentUser = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            // Get all conversation for specific user
            var conversation = await _chatService.LoadConversationByUser(CurrentUser);
            var conversationViewList = new List<ConversationViewModel>();
            foreach (var item in conversation)
            {
                var receiverId = "";
                foreach(var x in item.Participant)
                {
                    if (!x.Equals(CurrentUser))
                    {
                        receiverId = x;
                    }
                }
                //Recuperer l'evoyeur et le receveur en BD
                var sender = await _userBc.GetSpecificuser(CurrentUser);
                var receiver = await _userBc.GetSpecificuser(receiverId);

                var finalItem = new ConversationViewModel
                {
                    Conversation = item,
                    Sender = sender,
                    Receiver = receiver

                };

                conversationViewList.Add(finalItem);
            }
            var users = await _userBc.AllUser();
            var model = new ConversationListViewModel
            {
                Responses = conversationViewList,
                Count = conversationViewList.Count,
                User = users.Select(c => new SelectListItem
                {
                    Value = c.Id,
                    Text = c.FullName
                })

            };
            // Get all user
            //await _userBc.G
            return View(model);
        }

        public async Task<IActionResult> NewConversation(string Dest) 
        {
            if (!String.IsNullOrEmpty(Dest))
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await _chatService.InitializeConversation(user, Dest);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(Dest);
            }
            
            
        }

        [Route("/Chat/Conversation/{Id}")]
        public async Task<IActionResult> Conversation(string Id)
        {
            var CurrentUser = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            // Get all message for a specific conversation
            var messages = await _chatService.LoadMessageForSpecificConversation(Id);
            var conversation = await _chatService.TakeConversation(Id);
            var receiverId = "";
            foreach (var x in conversation.Participant)
            {
                if (!x.Equals(CurrentUser))
                {
                    receiverId = x;
                }
            }
            var receiver = await _userBc.GetSpecificuser(receiverId);
            if (messages == null)
            {
                return BadRequest();
            }
            else
            {
                var messagesViewList = new List<MessageViewModel>();
                if (messages.Count != 0)
                {
                    foreach (var item in messages)
                    {
                        //Recuperer l'evoyeur et le receveur en BD
                        Console.WriteLine(item.Sender);
                        var sender = await _userBc.GetSpecificuser(item.Sender);

                        var finalItem = new MessageViewModel
                        {
                            Messages = item,
                            Sender = sender,

                        };

                        messagesViewList.Add(finalItem);
                    }
                    
                    // Get conversation's detail
                    var model = new MessageListViewModel
                    {
                        message = messagesViewList,
                        Count = messagesViewList.Count,
                        Conversation = conversation,
                        Receiver = receiver,
                    };

                    return View(model);
                }
                else
                {
                    var model = new MessageListViewModel
                    {
                        message = new List<MessageViewModel>(),
                        Count = 0,
                        Conversation = conversation,
                        Receiver = receiver,
                    };
                    return View(model);
                }

            }
            
            
        }
    }
}
