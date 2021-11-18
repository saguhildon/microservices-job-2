using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TaxiBookingAPI.Models
{
    public class TaxiBookingContext : DbContext
    {
        public TaxiBookingContext(DbContextOptions<TaxiBookingContext> options)
            : base(options)
        {
        }

        public DbSet<TaxiBooking> TaxiBookingItems { get; set; } = null!;
        public DbSet<currentlocation> currentlocationItems { get; set; } = null!;
    }
}
