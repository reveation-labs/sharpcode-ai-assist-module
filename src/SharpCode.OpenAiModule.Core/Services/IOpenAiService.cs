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
        public Task<string> GenerateDescription(string model, string prompt, int descLength);

        public Task<string> TranslateDescription(string model, string text, string language);

        public Task<string> RephraseDescription(string model, string text);

        public Task<string> GenerateImage(GenerateImageRequest generateImageRequest);

    }
}
