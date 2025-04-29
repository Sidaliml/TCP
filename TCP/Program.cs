using TCPCommunication.Data;
using Microsoft.EntityFrameworkCore;
using TCPCommunication.Data.Seed;

namespace TCP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (!context.Users.Any())
                {
                    var users = SeedData.GenerateUsers(10);
                    context.Users.AddRange(users);
                    context.SaveChanges();

                    var messages = SeedData.GenerateMessages(20, users);
                    context.Messages.AddRange(messages);
                    context.SaveChanges();

                    var posts = SeedData.GeneratePosts(15, users);
                    context.Posts.AddRange(posts);
                    context.SaveChanges();
                }
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }
    }
}
