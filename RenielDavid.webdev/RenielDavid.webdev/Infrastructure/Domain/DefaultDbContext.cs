using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RenielDavid.webdev.Infrastructure.Domain.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Type = RenielDavid.webdev.Infrastructure.Domain.Models.Type;

namespace RenielDavid.webdev.Infrastructure.Domain
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
          : base(options)
        {
        }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Service> Services { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Role> roles = new List<Role>();
            List<User> users = new List<User>();
            List<UserLogin> userLogins = new List<UserLogin>();
            List<UserRole> userRoles = new List<UserRole>();
            List<Video> videos = new List<Video>();
            List<Service> services = new List<Service>();
            videos.Add(new Video()
            {
                VideoId = Guid.Parse("00965ecf-acae-46fe-8775-d7834b07fd96"),
                Title = "Turning Red",
                Description = "patayan",
                DateOfPublish = DateTime.Now,
                Type = Type.Series,
                ServiceId = Guid.Parse("1cef3b99-b69d-4b12-9b0a-b63c5fddcdc7")
            });

            videos.Add(new Video()
            {
                VideoId = Guid.Parse("7ce68d5c-5b65-495a-8a63-14aeb48da87d"),
                Title = "Master",
                Description = "buhayan",
                DateOfPublish = DateTime.Now,
                Type = Type.Movie,
                ServiceId = Guid.Parse("e9ff85ab-f0c1-4de9-a9f2-5bee39150e4f")
            });


            videos.Add(new Video()
            {
                VideoId = Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac2"),
                Title = "Kimi",
                Description = "aso",
                DateOfPublish = DateTime.Now,
                Type = Type.Movie,
                ServiceId = Guid.Parse("e9735c93-7e74-4fbc-83e8-45d5999ccb1a")
            });
            services.Add(new Service()
            {
                ServiceId = Guid.Parse("1cef3b99-b69d-4b12-9b0a-b63c5fddcdc7"),
                Title = "NETFLIX",
                Description = "is an American subscription video on-demand over-the-top streaming service and production company based in Los Gatos, California. Founded in 1997 by Reed Hastings and Marc Randolph in Scotts Valley, California, it offers a film and television series library through distribution deals as well as its own productions, known as Netflix Originals.",
                Abbreviation = "NFLX"
            });
            services.Add(new Service()
            {
                ServiceId = Guid.Parse("e9ff85ab-f0c1-4de9-a9f2-5bee39150e4f"),
                Title = "amazon prime video",
                Description = "Watch award-winning Prime Originals on the web or Prime Video app. Enjoy Anywhere. Watch The Grand Tour. Unlimited Streaming. Start Free Trial. Download and Go.",
                Abbreviation = "APV"
            });

            services.Add(new Service()
            {
                ServiceId = Guid.Parse("e9735c93-7e74-4fbc-83e8-45d5999ccb1a"),
                Title = "Crunchyroll",
                Description = "Crunchyroll is an American subscription video on-demand over-the-top streaming service owned by Sony through a joint venture between Sony Pictures and Sony Music Entertainment Japan's Aniplex. The company primarily distributes films and television series from East Asian media, including Japanese anime outside Asia.",
                Abbreviation = "HIME"
            });

            roles.Add(new Role()
            {
                RoleId = Guid.Parse("00965ecf-acae-46fe-8775-d7834b07fd96"),
                Name = "Staff",
                Description = "People who work in school but not teaching",
                Abbreviation = "Stf"
            });


            roles.Add(new Role()
            {
                RoleId = Guid.Parse("2fb7085a-762f-440c-87de-59f75f85e935"),
                Name = "Admin",
                Description = "Admin",
                Abbreviation = "Adm"
            });

            modelBuilder.Entity<Role>().HasData(roles);


            users.Add(new User()
            {
                Id = Guid.Parse("2d72f000-dbbd-419b-8af2-f571e1486ac0"),
                Name = "Joy",
                DateOfBirth = DateTime.Now,
                EmailAddress = "Joy@mailinator.com",
                Gender = Gender.Male,
                RoleId = Guid.Parse("2fb7085a-762f-440c-87de-59f75f85e935")
            });

            userLogins.AddRange(new List<UserLogin>()
            {
                new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId =Guid.Parse("2d72f000-dbbd-419b-8af2-f571e1486ac0"),
                    Type = "General",
                    Key = "Password",
                    Value = BCrypt.Net.BCrypt.EnhancedHashPassword("Accord605")
                },
                new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId =Guid.Parse("2d72f000-dbbd-419b-8af2-f571e1486ac0"),
                    Type = "General",
                    Key = "IsActive",
                    Value = "true"
                },
                new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId =Guid.Parse("2d72f000-dbbd-419b-8af2-f571e1486ac0"),
                    Type = "General",
                    Key = "LoginRetries",
                    Value = "0"
                }
            });

            userRoles.Add(new UserRole()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac0"),
                RoleId = Guid.Parse("00965ecf-acae-46fe-8775-d7834b07fd96"),
            });


            users.Add(new User()
            {
                Id = Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac1"),
                Name = "Reniel",
                DateOfBirth = DateTime.Now,
                EmailAddress = "renielDavid@gmail.com",
                Gender = Gender.Male,
                RoleId = Guid.Parse("00965ecf-acae-46fe-8775-d7834b07fd96")
            });

            userLogins.AddRange(new List<UserLogin>()
            {
                new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId =Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac1"),
                    Type = "General",
                    Key = "Password",
                    Value = BCrypt.Net.BCrypt.EnhancedHashPassword("Accord605")
                },
                new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId =Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac1"),
                    Type = "General",
                    Key = "IsActive",
                    Value = "true"
                },
                new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId =Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac1"),
                    Type = "General",
                    Key = "LoginRetries",
                    Value = "0"
                }
            });

            userRoles.Add(new UserRole()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac1"),
                RoleId = Guid.Parse("00965ecf-acae-46fe-8775-d7834b07fd96"),
            });

           

            userRoles.Add(new UserRole()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("2d72f000-dbbd-419b-8af2-f571e1486ac0"),
                RoleId = Guid.Parse("2fb7085a-762f-440c-87de-59f75f85e935"),
            });


            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<UserLogin>().HasData(userLogins);
            modelBuilder.Entity<UserRole>().HasData(userRoles);
            modelBuilder.Entity<Video>().HasData(videos);
            modelBuilder.Entity<Role>().HasData(roles);
        }

    }
}
