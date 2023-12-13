using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.AIModule.Core.Services
{
    public interface IAIService
    {
        public Task<string> GenerateDescription(string model, string prompt, int descLength);

        public Task<string> TranslateDescription(string model, string text, string language);

        public Task<string> RephraseDescription(string model, string text);

        public Task<string> GenerateImage(string model, string prompt, string size, int numOfImages, string quality);

    }
}
