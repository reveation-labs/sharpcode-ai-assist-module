using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCode.OpenAiModule.Core.Models;

namespace SharpCode.OpenAiModule.Core.Services
{
    public interface IOpenAiClient
    {
        public Task<string> UseOpenAiClient(string model, string systemMessage, string userMessage, int maxTokens = 2000);
        public Task<string> UseOpenAiImageClient(GenerateImageRequest generateImageRequest);
    }
}
