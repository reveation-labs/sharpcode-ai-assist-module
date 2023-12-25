using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.Interfaces;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using Sharpcode.AiAssistModule.Core.Services;
using OpenAI;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Sharpcode.AiAssistModule.Core.Models;
using VirtoCommerce.CatalogModule.Core.Services;
using static OpenIddict.Abstractions.OpenIddictConstants;
using VirtoCommerce.CatalogModule.Data.Search.Indexing;
using Sharpcode.AiAssistModule.Core;

namespace Sharpcode.AiAssistModule.Data.Services
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

        public async Task<string> GenerateDescription(OpenAiTextRequest openAiTextRequest)
        {
            var systemMessage = string.Format(ModuleConstants.GenerateSystemMessage, openAiTextRequest.Language, openAiTextRequest.DescLength, openAiTextRequest.DescriptionType);

            if (!string.IsNullOrEmpty(openAiTextRequest.ProductId))
            {
                var product = await _itemService.GetByIdAsync(openAiTextRequest.ProductId);
                var properties = string.Join("; ", product.Properties.Select(p => $"{p.Name}: {string.Join(",", p.Values)}"));
                openAiTextRequest.Prompt += string.Format(ModuleConstants.IncludeProductPropMessage, properties);
            }

            return await _openAiClient.UseOpenAiClient(systemMessage, openAiTextRequest.Prompt);
        }

        public async Task<string> TranslateDescription(OpenAiTextRequest openAiTextRequest)
        {
            var systemMessage = string.Format(ModuleConstants.TranslateSystemMessage, openAiTextRequest.Language);
            return await _openAiClient.UseOpenAiClient(systemMessage, openAiTextRequest.Prompt);
        }

        public async Task<string> RephraseDescription(OpenAiTextRequest openAiTextRequest)
        {
            var systemMessage = ModuleConstants.RephraseSystemMessage;
            return await _openAiClient.UseOpenAiClient(systemMessage, openAiTextRequest.Prompt);
        }

        public async Task<List<string>> GenerateImage(OpenAiImageRequest generateImageRequest)
        {
            var finalPrompt = ModuleConstants.GenerateImageSystemMessage + generateImageRequest.Prompt;

            if (!string.IsNullOrEmpty(generateImageRequest.ProductId))
            {
                var product = await _itemService.GetByIdAsync(generateImageRequest.ProductId);
                var properties = string.Join("; ", product.Properties.Select(p => $"{p.Name}: {string.Join(",", p.Values)}"));
                finalPrompt += string.Format(ModuleConstants.IncludeProductPropMessage, properties);
            }

            generateImageRequest.Prompt = finalPrompt;

            return await _openAiClient.UseOpenAiImageClient(generateImageRequest);
        }
    }
}
