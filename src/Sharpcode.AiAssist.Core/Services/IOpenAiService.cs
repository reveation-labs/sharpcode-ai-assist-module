using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharpcode.AiAssistModule.Core.Models;

namespace Sharpcode.AiAssistModule.Core.Services
{
    public interface IOpenAiService
    {
        public Task<string> GenerateDescription(OpenAiTextRequest openAiTextRequest);

        public Task<string> TranslateDescription(OpenAiTextRequest openAiTextRequest);

        public Task<string> RephraseDescription(OpenAiTextRequest openAiTextRequest);

        public Task<List<string>> GenerateImage(OpenAiImageRequest generateImageRequest);

    }
}
