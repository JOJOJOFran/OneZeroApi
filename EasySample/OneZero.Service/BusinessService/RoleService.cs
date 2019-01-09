﻿using OneZero.Model;
using OneZero.Service.BusinessService;
using OneZero.Service.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Service.BusniessService
{
    public class RoleService : IBusniessService
    {
        private IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
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