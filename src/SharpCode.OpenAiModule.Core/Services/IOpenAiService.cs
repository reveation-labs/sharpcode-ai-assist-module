using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCode.OpenAiModule.Core.Models;

namespace SharpCode.OpenAiModule.Core.Services
{
    public interface IOpenAiService
    {
        public Task<string> GenerateDescription(string productId, string prompt, int descLength);

        public Task<string> TranslateDescription(string text, string language);

        public Task<string> RephraseDescription(string text, string tone);

        public Task<string> GenerateImage(GenerateImageRequest generateImageRequest);

    }
}
