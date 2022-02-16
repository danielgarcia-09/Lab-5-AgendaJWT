using AgendaManager.Bl.Dto;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AgendaManager.Tests
{
    public class UserServiceShould : BaseTestService
    {
        private readonly ITestOutputHelper _output;

        public UserServiceShould(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task AddUser()
        {
            var user = new UserDto
            {
                Id = 1,
                Name = "Daniel",
                LastName = "Garcia",
                Email = "dgarcia@gmail.com",
                Password = "string1234",
                Deleted = false
            };
            var result = await _userService.Create(user);
            Assert.NotNull(result);

            _output.WriteLine(result.Name);
        }

        [Fact]
        public async Task GetUsers()
        {
            var user1 = new UserDto
            {
                Id = 1,
                Name = "Daniel",
                LastName = "Garcia",
                Email = "dgarcia@gmail.com",
                Password = "string1234",
                Deleted = false
            };
            var user2 = new UserDto
            {
                Id = 2,
                Name = "Hector",
                LastName = "Gonzo",
                Email = "hgonzo@gmail.com",
                Password = "string1234",
                Deleted = false
            };
            var user3 = new UserDto
            {
                Id = 2,
                Name = "Pedro",
                LastName = "Gonzaga",
                Email = "pgonzaga@gmail.com",
                Password = "sting1234",
                Deleted = false
            };
            await _userService.Create(user1);
            await _userService.Create(user2);
            await _userService.Create(user3);

            var allUsers = _userService.Get();

            Assert.NotNull(allUsers);
            Assert.True(allUsers.Count() >= 2);
        }

        [Fact]
        public async Task GetUserById()
        {
            var user = new UserDto
            {
                Id = 2,
                Name = "Hector",
                LastName = "Gonzo",
                Email = "hgonzo@gmail.com",
                Password = "string1234",
                Deleted = false
            };

            await _userService.Create(user);
            
            var result = await _userService.GetById(1);

            Assert.NotNull(result);
            _output.WriteLine(result.Name);
        }

        [Fact]
        public async Task UpdateUser()
        {
            var user = new UserDto
            {
                Id = 2,
                Name = "Hector",
                LastName = "Gonzo",
                Email = "hgonzo@gmail.com",
                Password = "string1234",
                Deleted = false
            };

            var addedUser = await _userService.Create(user);

            var userUpdate = new UserDto
            {
                Id = 2,
                Name = "Pedro",
                LastName = "Gonzaga",
                Email = "pgonzaga@gmail.com",
                Password = "sting1234",
                Deleted = false
            };

            var updatedUser = await _userService.Update(addedUser.Id, userUpdate);

            Assert.NotEqual(addedUser, updatedUser);

        }

        [Fact]
        public async Task DeleteUser()
        {
            var user = new UserDto
            {
                Id = 2,
                Name = "Hector",
                LastName = "Gonzo",
                Email = "hgonzo@gmail.com",
                Password = "string1234",
                Deleted = false
            };

            await _userService.Create(user);

            var isDeleted = await _userService.Delete(user.Id);

            Assert.True(isDeleted);
        }

    }
}
