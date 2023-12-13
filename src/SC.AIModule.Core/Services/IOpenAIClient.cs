using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.AIModule.Core.Services
{
    public interface IOpenAIClient
    {
        public Task<string> UseOpenAIClient(string model, string systemMessage, string userMessage, int maxTokens = 2000);
        public Task<string> UseOpenAIImageClient(string model, string prompt, string size, int numOfImages, string quality);
    }
}
