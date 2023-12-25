using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharpcode.AiAssistModule.Core.Models;

namespace Sharpcode.AiAssistModule.Core.Services
{
    public interface IOpenAiClient
    {
        public Task<string> UseOpenAiClient(string systemMessage, string userMessage, int maxTokens = 2000);
        public Task<List<string>> UseOpenAiImageClient(OpenAiImageRequest generateImageRequest);
    }
}
