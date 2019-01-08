using Microsoft.Extensions.Logging;
using OneZero.Model;
using OneZero.Service.BusinessService;
using OneZero.Service.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Service.BusniessService
{
    public class UserService: IBusniessService
    {
        private readonly IUserRepository _repository;
        private readonly ILogger _logger;

        public UserService(IUserRepository repository, ILogger<UserService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<Dto<DtoData>> AddAsync(InputModel input)
        {
            throw new NotImplementedException();
        }

        public Task<Dto<DtoData>> DeleteAsync(InputModel input)
        {
            throw new NotImplementedException();
        }

        public Task<Dto<DtoData>> GetItemAsync(InputModel input)
        {
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
    }
}
