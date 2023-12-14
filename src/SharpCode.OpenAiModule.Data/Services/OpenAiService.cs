using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.Interfaces;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using SharpCode.OpenAiModule.Core.Services;
using OpenAI;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SharpCode.OpenAiModule.Core.Models;

namespace SharpCode.OpenAiModule.Data.Services
{
    public class OpenAiService : IOpenAiService
    {
        private readonly IOpenAiClient _openAiClient;

        public OpenAiService(IOpenAiClient openAiClient)
        {
            _openAiClient = openAiClient;
        }

        public async Task<string> GenerateDescription(string model, string prompt, int descLength)
        {
            var systemMessage = $"You are expert at creating detailed and straightforward product descriptions for e-commerce websites. Aim for a tone that emphasizes features and benefits while maintaining a professional and informative style. Prioritize clarity and conciseness in your descriptions. Make sure you write it in {descLength} words.";
            return await _openAiClient.UseOpenAiClient(model, systemMessage, prompt);
        }

        public async Task<string> TranslateDescription(string model, string text, string language)
        {
            var systemMessage = $"You are expert in {language} langauge. You are a translator. Help in translation.";
            return await _openAiClient.UseOpenAiClient(model, systemMessage, text);
        }

        public async Task<string> RephraseDescription(string model, string text)
        {
            var systemMessage = "You are expert in generating SEO friendly content. Help in rephrasing the following text.";
            return await _openAiClient.UseOpenAiClient(model, systemMessage, text);
        }

        public async Task<string> GenerateImage(GenerateImageRequest generateImageRequest)
        {
            return await _openAiClient.UseOpenAiImageClient(generateImageRequest);
        }
    }
}
