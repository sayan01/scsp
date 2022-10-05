using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using scsp.Models;
    public class SCSPDataContext : DbContext
    {
        public SCSPDataContext (DbContextOptions<SCSPDataContext> options)
            : base(options)
        {
        }

        public DbSet<scsp.Models.Photo> Photo { get; set; } = default!;

        public DbSet<scsp.Models.User> User { get; set; } = default!;

        public DbSet<scsp.Models.Post> Post { get; set; } = default!;

        public DbSet<scsp.Models.Message> Message { get; set; } = default!;

        public DbSet<scsp.Models.Donation> Donation { get; set; } = default!;

        public DbSet<scsp.Models.Comment> Comment { get; set; } = default!;
    }
