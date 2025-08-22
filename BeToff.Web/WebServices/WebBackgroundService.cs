using BeToff.BLL.Service.Impl;
using BeToff.BLL.Service.Interface;
using BeToff.DAL.Interface;
namespace BeToff.Web.WebServices
{
    public class WebBackgroundService : BackgroundService
    {
        //protected readonly IUserInvitationService _userInvitationService;
        protected readonly IServiceProvider _serviceProvider;
        
        public WebBackgroundService(IServiceProvider serviceProvider)
        {
            //_userInvitationService = userInvitationService;
            _serviceProvider = serviceProvider;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scheduledTime = new TimeSpan(0, 0, 0); // Midnight

            using ( var scope = _serviceProvider.CreateScope())
            {
                var Service = scope.ServiceProvider.GetRequiredService<IUserInvitationService>();
                while (!stoppingToken.IsCancellationRequested)
                {
                    if (DateTime.Now.TimeOfDay >= scheduledTime && DateTime.Now.TimeOfDay < scheduledTime.Add(TimeSpan.FromMinutes(1)))
                    {
                        var result = await Service.AllInvitation();

                        if (result != null)
                        {
                            foreach (var item in result)
                            {
                                await Service.CancelInvitationByTimeWasted(item.Id.ToString());
                            }
                            
                        }

                    }
                    await Task.Delay(TimeSpan.FromSeconds(120), stoppingToken);
                }


            }

           
            
        }
    }
}
