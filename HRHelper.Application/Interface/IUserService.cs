using HRHelper.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRHelper.Application.Interface
{
    public interface IUserService
    {
        List<UserViewModel> Get();

        bool Post(UserViewModel userViewModel);

        UserViewModel GetById(string id);

        bool Update(UserViewModel userViewModel);

        bool Delete(string id);

        UserAuthenticateResponseViewModel Login(UserAuthenticateRequestViewModel user);
    }
}
