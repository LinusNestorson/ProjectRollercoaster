namespace ProjectRollercoaster.Server.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using ProjectRollercoaster.Shared;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    /// <summary>
    /// Backend part of authorization handling.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Recieves and handles login request from client.
        /// </summary>
        /// <param name="email">User Email</param>
        /// <param name="password">User Password</param>
        /// <returns>Success or Failure response.</returns>
        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return response;
        }

        /// <summary>
        /// Recieves and handles regist request from client.
        /// </summary>
        /// <param name="user">User information.</param>
        /// <param name="password">user password</param>
        /// <returns>Response that depends on status of the request.</returns>
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int> { Success = false, Message = "User already exists." };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = user.Id, Message = "User is added." };
        }

        /// <summary>
        /// Communicating with database to see if user is in it.
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <returns>True if user exists or False if she do not.</returns>
        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates hash from password and salts the passwor using HMACSHA521 encryption.
        /// </summary>
        /// <param name="password">Password from user.</param>
        /// <param name="passwordHash">Hashed password</param>
        /// <param name="passwordSalt">Salt of passowrd.</param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Verifies if password is valid with the hashed and salted passowrd in database.
        /// </summary>
        /// <param name="password">User password.</param>
        /// <param name="passwordHash">Hashed Password.</param>
        /// <param name="passwordSalt">Salt of password.</param>
        /// <returns>True or false based on outcome of verification.</returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Handles creation of Json Web Token.
        /// </summary>
        /// <param name="user">User information.</param>
        /// <returns>Json Web Token.</returns>
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}