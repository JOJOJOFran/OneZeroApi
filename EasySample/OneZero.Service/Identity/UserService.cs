using OneZero.Service.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Service
{
    public class UserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }


    }
}
