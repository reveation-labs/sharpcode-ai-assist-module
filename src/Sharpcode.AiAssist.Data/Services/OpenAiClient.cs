using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenAI.Interfaces;
using OpenAI.Managers;
using OpenAI;
using Sharpcode.AiAssistModule.Core.Services;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using System.Diagnostics;
using Newtonsoft.Json;
using Sharpcode.AiAssistModule.Core.Models;
using Sharpcode.AiAssistModule.Core.Utilities;
using Microsoft.Extensions.Logging;
using Sharpcode.AiAssistModule.Core.Enums;
using VirtoCommerce.Platform.Core.Settings;
using Sharpcode.AiAssistModule.Core;

namespace Sharpcode.AiAssistModule.Data.Services
{
    public class OpenAiClient : IOpenAiClient
    {
        private readonly IOpenAIService _openAiService;
        private readonly OpenAiOptions _openAiOptions;
        private readonly ILogger<OpenAiClient> _logger;
        private readonly ISettingsManager _settingsManager;

        public OpenAiClient(ISettingsManager settingsManager, IOptions<OpenAiOptions> openAiOptions, ILogger<OpenAiClient> logger)
        {
            _settingsManager = settingsManager;
            _openAiOptions = openAiOptions.Value;
            _openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = _openAiOptions.ApiKey,
            });
            _logger = logger;
        }

        public async Task<string> UseOpenAiClient(string systemMessage, string userMessage, int maxTokens = 2000)
        {
            var stopwatch = Stopwatch.StartNew();
            var openAiModel = await _settingsManager.GetValueAsync(ModuleConstants.Settings.General.OpenAiTextModel.Name, _openAiOptions.DefaultModelId);

            var completionResult = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem(systemMessage),
                    ChatMessage.FromUser(userMessage),
                },
                Model = openAiModel
            });
            stopwatch.Stop();

            _logger.LogDebug("Time Taken: " + JsonConvert.SerializeObject(stopwatch.Elapsed));
            _logger.LogDebug("Usage: " + JsonConvert.SerializeObject(completionResult.Usage));
            
            if (completionResult.Successful)
            {
                return completionResult.Choices.First().Message.Content;
            }

            return completionResult.Error.Message;

        }

        public async Task<List<string>> UseOpenAiImageClient(OpenAiImageRequest generateImageRequest)
        {
            var stopwatch = Stopwatch.StartNew();
            var imageResult = await _openAiService.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = generateImageRequest.Prompt,
                N = generateImageRequest.N,
                Size = generateImageRequest.Size.SizeConverter(),
                ResponseFormat = generateImageRequest.ResponseFormat.ToString(),
                Quality = generateImageRequest.Quality.ToString(),
                User = "TestUser",
                Model = generateImageRequest.Model.ToLowercaseString(),
                Style = generateImageRequest.Style.ToString()
            });
            stopwatch.Stop();

            _logger.LogDebug("Time Taken: " + JsonConvert.SerializeObject(stopwatch.Elapsed));

            if (imageResult.Successful)
            {
                if (generateImageRequest.ResponseFormat == OpenAiImageModels.ResponseFormat.url)
                {
                    return imageResult.Results.Select(r => r.Url).ToList();
                }
                else
                {
                    return imageResult.Results.Select(r => r.B64).ToList();
                }  
            }

            return imageResult.Error.Messages;
        }
    }
}
