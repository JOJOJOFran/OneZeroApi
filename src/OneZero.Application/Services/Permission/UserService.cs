using OneZero.Application.Models.Permissions;
using OneZero.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Application.Services.Permission
{
    public class UserService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<User, Guid> _userRepository;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<User, Guid>();
        }


    }
}
