using Microsoft.AspNetCore.Mvc;
using BeToff.BLL.Service.Interface;
using BeToff.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using BeToff.Web.Models;
using BeToff.Web.Models;
using NuGet.Protocol.Plugins;
using BeToff.Entities;
using Microsoft.EntityFrameworkCore;
using BeToff.BLL.Dto.Response;
using Microsoft.AspNetCore.Http.Features;
using BeToff.BLL.Mapping;
using System.Security.Claims;
using BeToff.BLL.Service.Impl;

namespace BeToff.Web.Controllers
{
    public class InvitationController : Controller
    {
        private readonly IUserInvitationService _invitaionService;
        private readonly IHubContext<NotificationHub> _notificationHub;
        private readonly IHubContext<ConversationGroupsHub> _conversationGroupHub;
        private readonly BeToffDbContext _dbContext;
        private readonly IChatGroupService _chatService;

        public InvitationController(IUserInvitationService invitaionService, IHubContext<NotificationHub> notificationHub, BeToffDbContext dbContext, IChatGroupService chatService, IHubContext<ConversationGroupsHub> conversationGroupHub)
        {
            _invitaionService = invitaionService;
            _notificationHub = notificationHub;
            _dbContext = dbContext;
            _chatService = chatService;
            _conversationGroupHub = conversationGroupHub;

        }


        public async Task<IActionResult> Index()
        {
            string UserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var Received = await _invitaionService.ReceiveInvitationByReceiverFromDatabase(UserId);
            var Sended = await _invitaionService.ReceiveInvitationBySenderFromDatabase(UserId);

            List<InvitationResponseDto> ListInvitationSended = [];
            List<InvitationResponseDto> ListInvitationReceive = [];

            if (Received != null && Sended != null)
            {
                

                foreach (var Item in Sended)
                {
                    var CLeanInvitation = InvitationMapper.ToDto(Item);
                    ListInvitationSended.Add(CLeanInvitation);
                };
                foreach (var Item in Received)
                {
                    var CLeanInvitation = InvitationMapper.ToDto(Item);
                    ListInvitationReceive.Add(CLeanInvitation);
                };
                
            }


            var model = new InvitationViewModel()
            {
                InvitationsReceived = ListInvitationReceive,
                InvitationsSended = ListInvitationSended

            };
            return View(model);
        }
        public IActionResult SendInvitation()
        {
            string SenderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var model = new InvitationCreateViewModel()
            {

                Recievers = _dbContext.Users.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.FullName.ToString()
                })
                .Where(x => x.Value != SenderId)
                .ToList(),

                Famillies = _dbContext.Famillies
                .Join(_dbContext.Registration, 
                    r => r.Id,
                    f => f.FamillyId, 
                    (r, f) => new {
                        IdFamilly = r.Id,
                        FamillName = r.Name,
                        UserRegistered = f.UserId
                    })
                .Where(x => x.UserRegistered.Equals(Guid.Parse(SenderId)))
                .Select(c => new SelectListItem
                {
                    Value = c.IdFamilly.ToString(),
                    Text = c.FamillName.ToString()
                })
                .ToList()

            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SendInvitation(InvitationCreateViewModel NewInvitation)
        {
            if (ModelState.IsValid)
            {
                string SenderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await _invitaionService.SendInvitationBySenderToDatabase(SenderId, NewInvitation.ReceiverId.ToString(), NewInvitation.FamillyItemId.ToString());
                var InvitationReceiver = await _invitaionService.ReceiveInvitationByReceiverFromDatabase(NewInvitation.ReceiverId.ToString());

                if(InvitationReceiver != null)
                {
                    List<InvitationResponseDto> ListInvitation = [];
                    foreach (var Item in InvitationReceiver)
                    {
                        var CLeanInvitation = InvitationMapper.ToDto(Item);
                        ListInvitation.Add(CLeanInvitation);
                    }
                    string ReceiverId = NewInvitation.ReceiverId.ToString();
                    await _notificationHub.Clients.User(NewInvitation.ReceiverId.ToString()).SendAsync("ListOfInvitationForReceiver", ListInvitation);
                }
                return RedirectToAction(nameof(Index));
            }
            //
            return View(NewInvitation);

        }
        public async Task<IActionResult> AcceptionInvitation(string Id)
        {
            await _invitaionService.AcceptInvitation(Id);
            var CurrentUser = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var conversation = await _chatService.LoadConversationByUser(CurrentUser);
            int lastIndex = conversation.Count - 1;
            await _conversationGroupHub.Clients.Group(conversation[lastIndex].id).SendAsync("SignalReconexion", conversation[lastIndex].id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RefuseInvitation(string Id)
        {
            await _invitaionService.RefuseInvitation(Id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteInvitation(string Id)
        {
            await _invitaionService.DeleteInvitation(Id);
            return RedirectToAction(nameof(Index));
        }

    }
}
