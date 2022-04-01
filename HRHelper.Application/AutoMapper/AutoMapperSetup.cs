using AutoMapper;
using HRHelper.Application.ViewModels;
using HRHelper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRHelper.Application.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            #region ViewToDomain
            CreateMap<UserViewModel, User>();
            #endregion


            #region DomainToView
            CreateMap<User, UserViewModel>(); 
            #endregion
        }
    }
}
