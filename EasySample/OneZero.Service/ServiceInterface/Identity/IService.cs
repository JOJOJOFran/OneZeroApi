using OneZero.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Service.ServiceInterface.Identity
{
    public interface IService
    {
        Task<Dto<DtoData>> GetItemByIdAsync();

        Task<Dto<DtoData>> GetListAsync();

        Task<Dto<DtoData>> AddItemAsync();

        Task<Dto<DtoData>> AddListAsync();
    }
}
