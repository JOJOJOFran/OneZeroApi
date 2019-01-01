using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace OneZero.Entity.Configuration
{
    public class OneZeroEntityBuilder : IOneZeroEntityBuilder
    {
        public IServiceCollection Services { get; }

        public OneZeroEntityBuilder(IServiceCollection services)
        {
            Services = services;
        }
    }
}
