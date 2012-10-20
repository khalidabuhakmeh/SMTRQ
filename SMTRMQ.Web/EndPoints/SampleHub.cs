using MassTransit;
using SMTRMQ.Core;
using SignalR;
using SignalR.Hubs;

namespace SMTRMQ.Web.EndPoints
{
    [HubName("sampleHub")]
    public class SampleHub : Hub
    {
        static SampleHub()
        {
            Bus.Instance.SubscribeHandler<ProgramMessage>((msg) =>
            {
                GlobalHost.ConnectionManager.GetHubContext<SampleHub>().Clients.addMessage(msg.Text);
            });
        }

        public void Send(string message)
        {
            Bus.Instance.Publish(new WebMessage { Text = message });
            
        }
    }
}
