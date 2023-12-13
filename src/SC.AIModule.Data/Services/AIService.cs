using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.Interfaces;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using SC.AIModule.Core.Services;
using OpenAI;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace SC.AIModule.Data.Services
{
    public class AIService : IAIService
    {
        private readonly IOpenAIClient _openAIClient;

        public AIService(IOpenAIClient openAIClient)
        {
            _openAIClient = openAIClient;
        }

        public async Task<string> GenerateDescription(string model, string prompt, int descLength)
        {
            var systemMessage = $"You are expert at creating detailed and straightforward product descriptions for e-commerce websites. Aim for a tone that emphasizes features and benefits while maintaining a professional and informative style. Prioritize clarity and conciseness in your descriptions. Make sure you write it in {descLength} words.";
            return await _openAIClient.UseOpenAIClient(model, systemMessage, prompt);
        }

        public async Task<string> TranslateDescription(string model, string text, string language)
        {
            var systemMessage = $"You are expert in {language} langauge. You are a translator. Help in translation.";
            return await _openAIClient.UseOpenAIClient(model, systemMessage, text);
        }

        public async Task<string> RephraseDescription(string model, string text)
        {
            var systemMessage = "You are expert in generating SEO friendly content. Help in rephrasing the following text.";
            return await _openAIClient.UseOpenAIClient(model, systemMessage, text);
        }

        public async Task<string> GenerateImage(string model, string prompt, string size, int numOfImages, string quality)
        {
            return await _openAIClient.UseOpenAIImageClient(model, prompt, size, numOfImages, quality);
        }
    }
}
