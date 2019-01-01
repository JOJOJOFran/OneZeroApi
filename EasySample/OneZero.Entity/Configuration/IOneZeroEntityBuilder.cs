using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace OneZero.Entity.Configuration
{
    public interface IOneZeroEntityBuilder
    {
         IServiceCollection Services { get; }
    }
}