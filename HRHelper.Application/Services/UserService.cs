using AutoMapper;
using HRHelper.Application.Interface;
using HRHelper.Application.ViewModels;
using HRHelper.Domain.Entities;
using HRHelper.Domain.Interfaces;
using HRHelper.Login.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRHelper.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }


        public List<UserViewModel> Get()
        {
            List<UserViewModel> _userViewModels = new List<UserViewModel>();

            IEnumerable<User> _users = this.userRepository.GetAll();
            _userViewModels = mapper.Map<List<UserViewModel>>(_users);
            //foreach (User _user in _users)
            //{
            //    _userViewModels.Add(new UserViewModel { Name = _user.Name, Email = _user.Email, Id = _user.Id });
            //}

            return _userViewModels;
        }

        public UserViewModel GetById(string id)
        {
            if(!Guid.TryParse(id, out Guid userId))
            {
                throw new Exception("UserId is not valid!");
            }

            User _user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);
            if(_user == null)
            {
                throw new Exception("User not found!");
            }

            return mapper.Map<UserViewModel>(_user);
        }

        public bool Post(UserViewModel userViewModel)
        {
            //User _user = new User
            //{
            //    Id = new Guid(),
            //    Name = userViewModel.Name,
            //    Email = userViewModel.Email,
            //    DateCreated = DateTime.Now,
            //    IsDeleted = false,
            //    DateUpdated = null
            //};
            User _user = mapper.Map<User>(userViewModel);
            _user.Password = MyPasswordEncryption(_user.Password);


            this.userRepository.Create(_user);

            return true;
        }

        public bool Update(UserViewModel userViewModel)
        {
            User _user = this.userRepository.Find(x => x.Id == userViewModel.Id && !x.IsDeleted);
            if (_user == null)
            {
                throw new Exception("User not found!");
            }
            else if(_user.Password == userViewModel.Password)
            {
                _user = mapper.Map<User>(userViewModel);
            }
            else
            {
                _user = mapper.Map<User>(userViewModel);
                _user.Password = MyPasswordEncryption(_user.Password);
            }

            
            

            this.userRepository.Update(_user);

            return true;
        }

        public bool Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid userId))
            {
                throw new Exception("UserId is not valid!");
            }

            User _user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);
            if (_user == null)
            {
                throw new Exception("User not found!");
            }

            return this.userRepository.Delete(_user);
        }


        #region Login
        public UserAuthenticateResponseViewModel Login(UserAuthenticateRequestViewModel user)
        {
            if (user.Email == null || user.Password == null)
            {
                throw new Exception("Email and Password are required!");
            }

            user.Password = MyPasswordEncryption(user.Password);

            User _user = this.userRepository.Find(x => !x.IsDeleted && x.Email.ToLower() == user.Email.ToLower() && x.Password.ToLower() == user.Password.ToLower());
            if(_user == null)
            {
                throw new Exception("User not found!");
            }

            return new UserAuthenticateResponseViewModel(mapper.Map<UserViewModel>(_user), TokenService.GenerateToken(_user));

        }


        private string MyPasswordEncryption(string password)
        {
            HashAlgorithm sha1 = new SHA1CryptoServiceProvider();
            byte[] encryptedPassword = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sb = new StringBuilder();
            foreach(var ch in encryptedPassword)
            {
                sb.Append(ch.ToString("X2"));
            }


            return sb.ToString();
        }

        #endregion

    }
}
