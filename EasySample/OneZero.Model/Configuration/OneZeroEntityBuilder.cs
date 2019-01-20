using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace OneZero.Model.Configuration
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
