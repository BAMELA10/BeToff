using BeToff.BLL.Dto.Request;
using BeToff.BLL.Dto.Response;
using BeToff.BLL.Interface;
using BeToff.BLL.Service.Interface;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Security.Claims;

namespace BeToff.Web.Hubs
{
    public class ConversationGroupsHub : Hub
    {
        protected readonly IChatGroupService _chatService;
        protected readonly IUserBc _userBc;
        protected readonly IRegistrationBc _registrationBc;
        public ConversationGroupsHub(IChatGroupService chatService, IUserBc userBc, IRegistrationBc registrationBc)
        {
            _chatService = chatService;
            _userBc = userBc;
            _registrationBc = registrationBc;
        }
        public async Task SendMessage(string conversation, string content)
        {
            var user = Context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var message = new MessageCreateDto
            {
                Content = content,
                Conversation = conversation,
                SenderId = user,
                SendAt = DateTime.Now,
            };
            await Clients.Group(conversation).SendAsync("MessageGroup", message);
            await _chatService.SaveMessage(message);
        }
        public async Task JoinGroupConversation()
        {
            var CurrentUser = Context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var conversation = await _chatService.LoadConversationByUser(CurrentUser);
            var Dico = new Dictionary<string, IEnumerable<UserResponseDto>>();
            var ListReceiverDto = new List<UserResponseDto>();

            if (conversation.Count != 0)
            {
                foreach (var item in conversation)
                {
                    var receiverId = new List<string>();
                    foreach (var x in item.Participants)
                    {
                        if (!x.Equals(CurrentUser))
                        {
                            receiverId.Add(x);
                        }
                    }
                    UserResponseDto sender = await _userBc.GetSpecificuser(CurrentUser);
                    foreach (var x in receiverId)
                    {
                        var receiver = await _userBc.GetSpecificuser(x);
                        ListReceiverDto.Add(receiver);
                    }
                    IEnumerable<UserResponseDto>  FinalList = new[] { sender }.Concat(ListReceiverDto);
                    Dico.Add(item.id, FinalList);
                    await Groups.AddToGroupAsync(Context.ConnectionId, item.id);

                }
                var Serialized = JsonConvert.SerializeObject(Dico);
                await Clients.User(CurrentUser).SendAsync("CachingGroupMessage", Serialized);
            }


        }
        public override async Task OnConnectedAsync()
        {
            await this.JoinGroupConversation();
            await base.OnConnectedAsync();
        }
    }
}
