using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity
{
    public class EFLoggerProvider : ILoggerProvider
    {
        private ILogger recordLogger;

        public EFLoggerProvider()
        {

        }

        public EFLoggerProvider(ILogger logger)
        {
            recordLogger = logger;
        }

        public ILogger CreateLogger(string categoryName) => new EFLogger(categoryName,recordLogger);
        public void Dispose() { }
    }
}
