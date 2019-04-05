using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.EntityFrameworkCore
{
    public interface IEntityRegister
    {
        Type DbContextType { get; set; }

        void RegisterToManageCenter();
    }
}
