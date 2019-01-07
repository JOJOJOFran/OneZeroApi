using OneZero.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Service.BusinessService
{
    public interface IBusniessService
    {
        Task<Dto<DtoData>> GetItemAsync(InputModel input);

        Task<Dto<DtoData>> GetListAsync(InputModel input);

        Task<Dto<DtoData>> AddAsync(InputModel input);

        Task<Dto<DtoData>> DeleteAsync(InputModel input);

        Task<Dto<DtoData>> UpdateAsync(InputModel input);
    }
}
