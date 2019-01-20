using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace OneZero.EntityFramwork.Configuration
{
    public interface IOneZeroEntityBuilder
    {
         IServiceCollection Services { get; }
    }
}