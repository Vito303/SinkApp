using BlazorViewer.Data;
using BlazorViewer.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BlazorViewer.Service
{
    public class TimeService
    {
        private readonly IHubContext<TimeHub> _context;

        public TimeService(IHubContext<TimeHub> context)
        {
            _context = context;

            var aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000;

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += OnTimedEvent;

            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;

            // Start the timer
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            CallLoadData(e.SignalTime);
        }

        private void CallLoadData(DateTime dateAndTime)
        {
            Task.Run(async () =>
            {
                await _context.Clients.All.SendAsync("RefreshTime", dateAndTime);
                Console.WriteLine("The Elapsed event was raised at {0}",  dateAndTime);
            });
        }
    }
}
