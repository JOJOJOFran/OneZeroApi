using OneZero.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Service.ServiceInterface.Identity
{
    public interface IService
    {
        Task<IDto<IDtoData>> GetItemByIdAsync();

        Task<IDto<IDtoData>> GetListAsync();

        Task<IDto<IDtoData>> AddItemAsync();

        Task<IDto<IDtoData>> AddListAsync();
    }
}
