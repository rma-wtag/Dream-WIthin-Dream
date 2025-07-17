using Dream_Within_Dream.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream_Within_Dream.TicketBooking
{
    public class TicketService
    {
        private int AvailableTickets;

        private readonly object _locker = new object();
        public TicketService(int initialTicket)
        {
            AvailableTickets = initialTicket;
        }

        public bool BookTicket(Dream dream) {
            lock (_locker) {
                if (AvailableTickets <= 0)
                {
                    Console.WriteLine($"Dream {dream.Id} (Level {dream.Level}) - {dream.Dreamer}: No tickets left!");
                    return false;
                }
                AvailableTickets--;
                Console.WriteLine($"Dream {dream.Id} (Level {dream.Level}) - {dream.Dreamer}: Ticket booked! Remaining: {AvailableTickets}");
                return true;
            }
        }
    }
}
