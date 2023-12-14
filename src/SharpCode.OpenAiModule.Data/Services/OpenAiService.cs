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
using VirtoCommerce.CatalogModule.Core.Services;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SharpCode.OpenAiModule.Data.Services
{
    public class OpenAiService : IOpenAiService
    {
        private readonly IOpenAiClient _openAiClient;
        private readonly IItemService _itemService;
        public OpenAiService(IOpenAiClient openAiClient, IItemService itemService)
        {
            _openAiClient = openAiClient;
            _itemService = itemService;
        }

        public async Task<string> GenerateDescription(string productId, string prompt, int descLength)
        {
            if(productId != null)
            {
                var product = await _itemService.GetByIdAsync(productId);
                var properties = string.Join("; ", product.Properties.Select(p => $"{p.Name}: {string.Join(",", p.Values)}"));
                prompt += "These are the product properties, add the properties which you find relevant. Make sure to remove any product id or sku or anything related to these." + properties;
            }

            var systemMessage = $"You are expert at creating detailed and straightforward product descriptions for e-commerce websites. Aim for a tone that emphasizes features and benefits while maintaining a professional and informative style. Prioritize clarity and conciseness in your descriptions. Make sure you write it in {descLength} words.";
            return await _openAiClient.UseOpenAiClient(systemMessage, prompt);
        }

        public async Task<string> TranslateDescription(string text, string language)
        {
            var systemMessage = $"You are expert in {language} langauge. You are a translator. Help in translation.";
            return await _openAiClient.UseOpenAiClient(systemMessage, text);
        }

        public async Task<string> RephraseDescription(string text, string tone)
        {
            var systemMessage = $"You are expert in rephrasing content in {tone} tone. Help in rephrasing the following text.";
            return await _openAiClient.UseOpenAiClient(systemMessage, text);
        }

        public async Task<string> GenerateImage(GenerateImageRequest generateImageRequest)
        {
            return await _openAiClient.UseOpenAiImageClient(generateImageRequest);
        }
    }
}
