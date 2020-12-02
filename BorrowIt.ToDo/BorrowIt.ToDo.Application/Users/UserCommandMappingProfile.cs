using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BorrowIt.ToDo.Application.Users.Commands;
using BorrowIt.ToDo.Domain.Model.Users;

namespace BorrowIt.ToDo.Application.Users
{
    public class UserCommandMappingProfile : Profile
    {
        public UserCommandMappingProfile()
        {
            CreateMap<SynchronizeUserCommand, UserDataStructure>()
                .ConstructUsing(x => new UserDataStructure(x.Id, x.UserName));
        }
    }
}
