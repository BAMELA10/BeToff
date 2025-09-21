using BeToff.BLL.Interface;
using BeToff.BLL.Service.Interface;
using BeToff.Web.Hubs;
using BeToff.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeToff.Entities;
using BeToff.BLL.Dto.Response;
using NuGet.Protocol.Plugins;
using BeToff.BLL.Mapping;

namespace BeToff.Web.Controllers
{
    public class ChatController : Controller
    {
        protected readonly IChatService _chatService;
        protected readonly IUserBc _userBc;
        protected readonly IChatGroupService _chatGroupService;
        protected readonly IFamillyBc _famillyBc;

        public ChatController(IChatService chatService, IUserBc userBc, IFamillyBc famillyBc, IChatGroupService chatGroupService)
        {
            _chatService = chatService;
            _userBc = userBc;
            _chatGroupService = chatGroupService;
            _famillyBc = famillyBc;
        }
        public async Task<IActionResult> Index()
        {
            var CurrentUser = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            // Get all conversation for specific user
            var conversation = await _chatService.LoadConversationByUser(CurrentUser);
            var conversationGroup = await _chatGroupService.LoadConversationByUser(CurrentUser);
            var conversationViewList = new List<ConversationViewModel>();
            var conversationGroupViewList = new List<ConversationGroupViewModel>();
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
            foreach (var item in conversationGroup)
            {
                var Family = await _famillyBc.SelectFamilly(item.Family);
                var DtoFamily = FamillyMapper.ToDto(Family);

                var finalItem1 = new ConversationGroupViewModel
                {
                    ConversationGroup = item,
                    Family = DtoFamily

                };

                conversationGroupViewList.Add(finalItem1);
            }
            var users = await _userBc.AllUser();
            var model = new ConversationListViewModel
            {
                Responses = conversationViewList,
                ResponsesGroup = conversationGroupViewList,
                Count = conversationViewList.Count,
                User = users.Select(c => new SelectListItem
                {
                    Value = c.Id,
                    Text = c.FullName
                })

            };
            

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


        [Route("/Chat/ConversationGroup/{Id}")]
        public async Task<IActionResult> ConversationGroup(string Id)
        {
            var CurrentUser = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            // Get all message for a specific conversation
            var messages = await _chatService.LoadMessageForSpecificConversation(Id);
            var conversation = await _chatGroupService.TakeConversation(Id);
            var Family = await _famillyBc.SelectFamilly(conversation.Family);
            var DtoFamily = FamillyMapper.ToDto(Family);
            var ListReceiverDto = new List<UserResponseDto>();
            var receiverId = new List<string>();
            foreach (var x in conversation.Participants)
            {
                if (!x.Equals(CurrentUser))
                {
                    receiverId.Add(x);
                }
            }
            foreach (var x in receiverId)
            {
                var receiver = await _userBc.GetSpecificuser(x);
                ListReceiverDto.Add(receiver);
            }

            //var sender = await _userBc.GetSpecificuser(CurrentUser);
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
                        ConversationGroup = conversation,
                        Receivers = ListReceiverDto,
                        FamillyResponse = DtoFamily
                    };

                    return View(model);
                }
                else
                {
                    var model = new MessageListViewModel
                    {
                        message = new List<MessageViewModel>(),
                        Count = 0,
                        ConversationGroup = conversation,
                        Receivers = ListReceiverDto,
                        FamillyResponse = DtoFamily
                    };
                    return View(model);
                }

            }

        }
    }
}
