using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingClient.Models
{
    public class APIClients
    {
           public int Id { get; set; }
           public string MovieName { get; set; }
            public int Price { get; set; }
           public string MovieDescription { get; set; }
           public string MoviePoster { get; set; }
        
    }
}
