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
using VirtoCommerce.CatalogModule.Data.Search.Indexing;

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

        public async Task<string> GenerateDescription(OpenAiTextRequest openAiTextRequest)
        {
            var systemMessage = $"You are expert at creating concise and straightforward product descriptions in for e-commerce websites. " +
                $"You write in a tone that emphasizes features and benefits while maintaining a professional and informative style. " +
                $"You write key features in short and simple bullet points. " +
                $"The output format should be **HTML Format**. " +
                $"Do not add <html> tag or backticks in output.";

            if (!string.IsNullOrEmpty(openAiTextRequest.ProductId))
            {
                var product = await _itemService.GetByIdAsync(openAiTextRequest.ProductId);
                var properties = string.Join("; ", product.Properties.Select(p => $"{p.Name}: {string.Join(",", p.Values)}"));
                openAiTextRequest.Prompt += "These are the product properties, add the properties which you find relevant. Make sure to remove any product id or sku or anything related to these." + properties;
            }

            if(!string.IsNullOrEmpty(openAiTextRequest.Language))
            {
                systemMessage += $"Make sure you generate the product description in {openAiTextRequest.Language} language";
            }

            openAiTextRequest.Prompt += $"Make sure you generate approx {openAiTextRequest.DescLength} words. " +
                $"Generate the description according to {openAiTextRequest.DescriptionType} type.";

            return await _openAiClient.UseOpenAiClient(systemMessage, openAiTextRequest.Prompt);
        }

        public async Task<string> TranslateDescription(OpenAiTextRequest openAiTextRequest)
        {
            var systemMessage = $"You are expert in {openAiTextRequest.Language} langauge. You are a translator. " +
                $"The output format should be **HTML Format**. " +
                $"Do not add <html> tag or any extra quotes in output.";

            var textToTranslate = string.Empty;

            if (string.IsNullOrEmpty(openAiTextRequest.Prompt))
            {
                var product = (await _itemService.GetByIdAsync(openAiTextRequest.ProductId));
                var defaultLang = product.Catalog.DefaultLanguage.LanguageCode;
                textToTranslate = product.Reviews.FirstOrDefault(r => r.LanguageCode == defaultLang).Content;
            }
            else
            {
                textToTranslate = openAiTextRequest.Prompt;
            }

            openAiTextRequest.Prompt = $"The following is a product description in markdown format, extract the text and convert it to {openAiTextRequest.Language}. Here is the text to translate : {textToTranslate}";

            return await _openAiClient.UseOpenAiClient(systemMessage, openAiTextRequest.Prompt);
        }

        public async Task<string> RephraseDescription(OpenAiTextRequest openAiTextRequest)
        {
            var systemMessage = $"You are expert in rephrasing content in SEO-Friendly tone. " +
                $"Help in rephrasing.The input is in html markdown format, extract the text and then rephrase it." +
                $"Also make sure you generate the output in the same language as the product description. " +
                $"The output format should be **HTML Format**. " +
                $"Do not add <html> tag or any extra quotes in output.";
            return await _openAiClient.UseOpenAiClient(systemMessage, openAiTextRequest.Prompt);
        }

        public async Task<List<string>> GenerateImage(OpenAiImageRequest generateImageRequest)
        {
            generateImageRequest.Prompt += "Generate realistic product images suitable for e-commerce websites based on the provided description.";

            if (!string.IsNullOrEmpty(generateImageRequest.ProductId))
            {
                var product = await _itemService.GetByIdAsync(generateImageRequest.ProductId);
                var properties = string.Join("; ", product.Properties.Select(p => $"{p.Name}: {string.Join(",", p.Values)}"));
                generateImageRequest.Prompt += "These are the product properties, add the properties which you find relevant. Do not add text to the images" + properties;
            }
            return await _openAiClient.UseOpenAiImageClient(generateImageRequest);
        }
    }
}
