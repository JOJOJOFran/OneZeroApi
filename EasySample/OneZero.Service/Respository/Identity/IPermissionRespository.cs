using OneZero.Entity.Identity;
using OneZero.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Service.Respository
{
    public interface IPermissionRespository : IRespository<PermissionType, Guid,Dto,DtoData>
    {
    }
}
