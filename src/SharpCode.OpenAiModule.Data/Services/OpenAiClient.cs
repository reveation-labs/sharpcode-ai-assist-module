using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenAI.Interfaces;
using OpenAI.Managers;
using OpenAI;
using SharpCode.OpenAiModule.Core.Services;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using System.Diagnostics;
/*using System.Text.Json.Serialization;*/
using Newtonsoft.Json;
using SharpCode.OpenAiModule.Core.Models;
using SharpCode.OpenAiModule.Core.Utilities;
using Microsoft.Extensions.Logging;

namespace SharpCode.OpenAiModule.Data.Services
{
    public class OpenAiClient : IOpenAiClient
    {
        private readonly IOpenAIService _openAiService;
        private readonly OpenAiOptions _openAiOptions;
        private readonly ILogger<OpenAiClient> _logger;

        public OpenAiClient(IOptions<OpenAiOptions> openAiOptions, ILogger<OpenAiClient> logger)
        {
            _openAiOptions = openAiOptions.Value;
            _openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = _openAiOptions.ApiKey,
            });
            _logger = logger;
        }

        public async Task<string> UseOpenAiClient(string model, string systemMessage, string userMessage, int maxTokens = 2000)
        {
            var stopwatch = Stopwatch.StartNew();
            var completionResult = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem(systemMessage),
                    ChatMessage.FromUser(userMessage),
                },

                Model = model,
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

        public async Task<string> UseOpenAiImageClient(GenerateImageRequest generateImageRequest)
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
                return (string.Join("\n", imageResult.Results.Select(r => r.Url)));
            }

            return imageResult.Error.Message;
        }
    }
}
