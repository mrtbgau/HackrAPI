using Bogus;

namespace API.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        public async Task<object> GenerateIdentityAsync()
        {
            var faker = new Faker();
            var fakeIdentity = new
            {
                FullName = faker.Name.FullName(),
                Gender = faker.PickRandom(new[] { "Male", "Female", "Non-binary" }),
                Address = faker.Address.FullAddress(),
                Email = faker.Internet.Email(),
                Phone = faker.Phone.PhoneNumber(),
                BirthDate = faker.Date.Past(30, DateTime.Now.AddYears(-18)).ToString("yyyy-MM-dd"),
                PhotoUrl = "https://thispersondoesnotexist.com"
            };

            return await Task.FromResult(fakeIdentity);
        }
    }
}
