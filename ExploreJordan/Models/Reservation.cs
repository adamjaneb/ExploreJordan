using System;
using System.ComponentModel.DataAnnotations;

namespace ExploreJordan.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
