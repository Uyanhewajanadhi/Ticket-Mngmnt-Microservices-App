using System.ComponentModel.DataAnnotations;

namespace TicketingMicroservice.Database
{
    public class Ticketing
    {
        [Key]
        public int TicketId { get; set; }
        public string UserName { get; set; }
        public string Boarding { get; set; }
        public string Destination { get; set; }
    }
}
