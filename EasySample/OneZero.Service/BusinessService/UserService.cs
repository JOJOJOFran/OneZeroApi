using Microsoft.Extensions.Logging;
using OneZero.Entity.Identity;
using OneZero.Model;
using OneZero.Model.Input;
using OneZero.Model.Input.Identity;
using OneZero.Service.BusinessService;
using OneZero.Service.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Service.BusniessService
{
    public class UserService
    {
        private readonly IRepository<User,Guid> _repository;
        private readonly ILogger _logger;
        private Dto<DtoData> _dto;

        public UserService(IRepository<User, Guid> repository, Dto<DtoData> dto, ILogger<UserService> logger)
        {
            _repository = repository;
            _logger = logger;
            _dto=dto;
    }

        public async Task<Dto<DtoData>> AddAsync(InputModel input)
        {
           return await _repository.AddAsync(EntityConvert(input));
        }


        public async Task<Dto<DtoData>> DeleteAsync(Guid Id)
        {
            return await _repository.DeleteAsync(Id);
        }


        public async Task<Dto<DtoData>> GetItemAsync(InputModel input)
        {
            //return await _repository.GetItemAsync()
            throw new NotImplementedException();
        }


        public Task<Dto<DtoData>> GetListAsync(InputModel input)
        {
            throw new NotImplementedException();
        }

        public Task<Dto<DtoData>> UpdateAsync(InputModel input)
        {
            throw new NotImplementedException();
        }

        public User EntityConvert(InputModel input)
        {
            return new User() { };
        }


    }
}
