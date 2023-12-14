using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.AIModule.Core.Models;

namespace SC.AIModule.Core.Services
{
    public interface IOpenAiClient
    {
        public Task<string> UseOpenAiClient(string model, string systemMessage, string userMessage, int maxTokens = 2000);
        public Task<string> UseOpenAiImageClient(GenerateImageRequest generateImageRequest);
    }
}
