namespace BookmarksAPI.Services
{
    using BookmarksAPI.Exceptions;
    using DataWorkShop;
    using DataWorkShop.Entities;
    using EEFApps.ApiInstructions.DataInstructions.Instructions;
    using EEFApps.ApiInstructions.DataInstructions.Instructions.Structures;
    using System;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private BookmarksDbContext context;

        public UserService(BookmarksDbContext context)
        {
            this.context = context;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var user = await new ReceivingInstruction<User, string>(this.context, id).Execute();
            return user;
        }

        public async Task<User> CreateAsync(User newUser, string password)
        {
            if (string.IsNullOrWhiteSpace(newUser.Username))
            {
                throw new CustomException("Username is required");
            }

            // validation
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new CustomException("Password is required");
            }

            var numberOfExistingUsers = await new ReceivingCountedInstruction<User>(this.context, new ListInstructionParams<User> {
                filterExpr = usr => usr.Username == newUser.Username
            }).Execute();

            if (numberOfExistingUsers > 0)
            {
                throw new CustomException("Username \"" + newUser.Username + "\" is already taken");
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            var createdUser = await new CreationInstruction<User>(this.context, newUser).Execute();

            return createdUser;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
