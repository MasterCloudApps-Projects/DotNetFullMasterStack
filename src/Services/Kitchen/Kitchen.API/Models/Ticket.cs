using System.Collections.Generic;

namespace Kitchen.API.Models
{
    public class Ticket
    {
        private enum TicketStatusEnum
        {
            CREATE_PENDING, AWAITING_ACCEPTANCE, ACCEPTED, PREPARING, READY_FOR_PICKUP, PICKED_UP, CANCEL_PENDING, CANCELLED, REVISION_PENDING,
        }

        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public List<TicketLine> TicketLines { get; set; }
    }
}