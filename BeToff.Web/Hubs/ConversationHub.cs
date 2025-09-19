
using BeToff.BLL.Dto.Request;
using BeToff.BLL.Dto.Response;
using BeToff.BLL.Interface;
using BeToff.BLL.Service.Interface;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Security.Claims;
namespace BeToff.Web.Hubs
{
    public class ConversationHub : Hub
    {
        // Join the conversion
        //Get the Userid
        //Add current User the Groups with the Group's name is id connection
        protected readonly IChatService _chatService;
        protected readonly IUserBc _userBc;
        
        public ConversationHub(IChatService chatService, IUserBc userBc)
        {
            _chatService = chatService;
            _userBc = userBc;
        }
        public override async Task OnConnectedAsync()
        {
            
            var CurrentUser = Context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var conversation = await _chatService.LoadConversationByUser(CurrentUser);
            var Dico = new Dictionary<string, List<UserResponseDto>>();
            
            if (conversation.Count != 0) {
                foreach (var item in conversation)
                {
                    var receiverId = "";
                    foreach (var x in item.Participant)
                    {
                        if (!x.Equals(CurrentUser))
                        {
                            receiverId = x;
                        }
                    }
                    //Recuperer l'evoyeur et le receveur en BD
                    var sender = await _userBc.GetSpecificuser(CurrentUser);
                    var receiver = await _userBc.GetSpecificuser(receiverId);

                    Dico.Add(item.Id, [sender, receiver]);
                    await Groups.AddToGroupAsync(Context.ConnectionId, item.Id);

                }
                var Serialized = JsonConvert.SerializeObject(Dico);
                Clients.User(CurrentUser).SendAsync("CachingMessage", Serialized);
            }
            await base.OnConnectedAsync();
        }

        //public override async Task OnDisconnectedAsync()
        //{
        //    var user = Context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        //    var conversation = await _chatService.LoadConversationByUser(user);

        //    if (conversation.Count != 0)
        //    {
        //        foreach (var item in conversation)
        //        {
        //            await Groups.RemoveFromGroupAsync(Context.ConnectionId, item.Id);
        //        }
        //    }
        //    await base.OnDisconnectedAsync();
        //}


        // send a message
        //Send message to the Group
        public async Task SendMessage(string conversation, string content)
        {
            var user = Context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var message = new MessageCreateDto {
                Content = content,
                Conversation = conversation,
                SenderId = user,
                SendAt = DateTime.Now,
            };
            await Clients.Group(conversation).SendAsync("Message", message);
            await _chatService.SaveMessage(message);
        }
          
    }
}
