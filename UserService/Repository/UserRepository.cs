using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using UserService.Abstraction;
using UserService.Context;
using UserService.DTO;
using UserService.Model;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void UserAdd(string email, string password, RoleId roleId)
        {
            using (var context = new UserContext())
            {
                if (roleId == RoleId.Admin)
                {
                    var count = context.Users.Count(x => x.RoleId == RoleId.Admin);
                    if (count > 0)
                    {
                        throw new System.Exception("Admin already exists");
                    }
                }

                var user = new User
                {
                    Email = email,
                    RoleId = roleId,
                    Salt = new byte[16]
                };

                new Random().NextBytes(user.Salt);
                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();

                using (var shaM = SHA512.Create())
                {
                    user.Password = shaM.ComputeHash(data);
                }

                context.Add(user);
                context.SaveChanges();
            }
        }

        public RoleType UserCheck(string name, string password)
        {
            using (var context = new UserContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Email == name);
                if (user == null)
                {
                    throw new System.Exception("User not found");
                }

                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();
                byte[] hash;
                using (var shaM = SHA512.Create())
                {
                    hash = shaM.ComputeHash(data);
                }

                if (hash.SequenceEqual(user.Password))
                {
                    return _mapper.Map<RoleType>(user.RoleId);
                }

                throw new System.Exception("Wrong password");
            }
        }
    }
}