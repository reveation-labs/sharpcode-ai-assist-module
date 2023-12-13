using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenAI.Interfaces;
using OpenAI.Managers;
using OpenAI;
using SC.AIModule.Core.Services;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using System.Diagnostics;
/*using System.Text.Json.Serialization;*/
using Newtonsoft.Json;

namespace SC.AIModule.Data.Services
{
    public class OpenAIClient : IOpenAIClient
    {
        private readonly IOpenAIService _openAIService;
        private readonly OpenAiOptions _openAiOptions;

        public OpenAIClient(IOptions<OpenAiOptions> openAiOptions)
        {
            _openAiOptions = openAiOptions.Value;
            _openAIService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = _openAiOptions.ApiKey,
            });
        }

        public async Task<string> UseOpenAIClient(string model, string systemMessage, string userMessage, int maxTokens = 2000)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var completionResult = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem(systemMessage),
                    ChatMessage.FromUser(userMessage),
                },

                Model = model,
            });
            stopwatch.Stop();

            Console.WriteLine("Time Taken: " + JsonConvert.SerializeObject(stopwatch.Elapsed));
            Console.WriteLine("Usage: " + JsonConvert.SerializeObject(completionResult.Usage));
            
            if (completionResult.Successful)
            {
                return completionResult.Choices.First().Message.Content;
            }

            return completionResult.Error.Message;

        }

        public async Task<string> UseOpenAIImageClient(string model, string prompt, string size, int numOfImages, string quality)
        {
            var imageResult = await _openAIService.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = prompt,
                N = numOfImages,
                Size = size,
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                Quality = quality,
                User = "TestUser",
                Model = model,
                Style = StaticValues.ImageStatics.Style.Natural
            });
            
            if (imageResult.Successful)
            {
                return (string.Join("\n", imageResult.Results.Select(r => r.Url)));
            }

            return imageResult.Error.Message;
        }
    }
}
