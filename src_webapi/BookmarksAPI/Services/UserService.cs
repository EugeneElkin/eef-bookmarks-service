namespace BookmarksAPI.Services
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BookmarksAPI.Exceptions;
    using BookmarksAPI.Services.Interfaces;
    using DataWorkShop;
    using DataWorkShop.Entities;
    using DataWorkShop.Entities.Structures;
    using EEFApps.ApiInstructions.DataInstructions.Instructions;
    using EEFApps.ApiInstructions.DataInstructions.Instructions.Structures;

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

        public async Task<User> AuthenticateAsync(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new CustomException("User name is required");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new CustomException("Password is required");
            }

            var dbUser = (await new ReceivingListInstruction<User>(this.context, new ListInstructionParams<User> {
                filterExpr = usr => usr.UserName == userName
            }).Execute()).FirstOrDefault();

            // Check user existence
            if (dbUser == null)
            {
                throw new CustomException("Incorrect user name or password");
            }

            // Check password correctness
            if (!VerifyPasswordHash(password, dbUser.PasswordHash, dbUser.PasswordSalt))
            {
                throw new CustomException("Incorrect user name or password");
            }

            return dbUser;
        }

        public async Task<User> CreateAsync(User newUser, string password)
        {
            if (string.IsNullOrWhiteSpace(newUser.UserName))
            {
                throw new CustomException("User name is required");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new CustomException("Password is required");
            }

            var numberOfExistingUsers = await new ReceivingCountedInstruction<User>(this.context, new ListInstructionParams<User> {
                filterExpr = usr => usr.UserName == newUser.UserName
            }).Execute();

            if (numberOfExistingUsers > 0)
            {
                throw new CustomException("User name \"" + newUser.UserName + "\" is already taken");
            }

            byte[] passwordHash, passwordSalt;
            this.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            newUser.Status = UserStatusType.Active;

            var createdUser = await new CreationInstruction<User>(this.context, newUser).Execute();

            // TODO: add registration confirmation
            return createdUser;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64)
            {
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            }

            if (storedSalt.Length != 128)
            {
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
