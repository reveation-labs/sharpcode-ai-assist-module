using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Sharpcode.AiAssistModule.Tests.Helpers
{
    internal class Env
    {
        internal static string Var(string name)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Env>()
                .Build();

            var value = configuration[name];
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }

            value = Environment.GetEnvironmentVariable(name);
            if (string.IsNullOrEmpty(value))
            {
                throw new ApplicationException($"Secret / Env var not set: {name}");
            }

            return value;
        }
    }
}
