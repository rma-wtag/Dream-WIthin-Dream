using Dream_Within_Dream.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream_Within_Dream.TicketBooking
{
    public class TicketBooker
    {
        private readonly List<Dream> _rootDreams;
        private readonly TicketService _ticketService;
        public TicketBooker(List<Dream> dreams,TicketService ticketService)
        {
            _rootDreams = dreams;
            _ticketService = ticketService;
        }

        private IEnumerable<Dream> GetDreamsAtLevel(int level)
        {
            var result = new List<Dream>();

            void Traverse(Dream d)
            {
                if (d.Level == level)
                    result.Add(d);

                if (d.InnerDreams != null)
                    foreach (var inner in d.InnerDreams)
                        Traverse(inner);
            }

            foreach (var root in _rootDreams)
                Traverse(root);

            return result;
        }

        public async Task BookTicketsAtLevelAsync(int level)
        {
            var dreamsAtLevel = GetDreamsAtLevel(level).ToList();

            var tasks = dreamsAtLevel.Select(dream => Task.Run(() => _ticketService.BookTicket(dream)));

            await Task.WhenAll(tasks);
        }

    }
}
