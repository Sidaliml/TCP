using Bogus;
using TCPCommunication.Models;

namespace TCPCommunication.Data.Seed
{
    public static class SeedData
    {
        public static List<User> GenerateUsers(int count)
        {
            var faker = new Faker<User>()
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.Email, f => f.Internet.Email());

            return faker.Generate(count);
        }

        public static List<Message> GenerateMessages(int count, List<User> users)
        {
            var faker = new Faker<Message>()
                .RuleFor(m => m.Content, f => f.Lorem.Sentence())
                .RuleFor(m => m.SentAt, f => f.Date.Recent())
                .RuleFor(m => m.UserId, f => f.PickRandom(users).Id);

            return faker.Generate(count);
        }

        public static List<Post> GeneratePosts(int count, List<User> users)
        {
            var faker = new Faker<Post>()
                .RuleFor(p => p.Title, f => f.Lorem.Sentence(3))
                .RuleFor(p => p.Content, f => f.Lorem.Paragraph())
                .RuleFor(p => p.CreatedAt, f => f.Date.Past())
                .RuleFor(p => p.UserId, f => f.PickRandom(users).Id);

            return faker.Generate(count);
        }
    }
}
