using Xunit;
using Moq; // Assuming you are using Moq for mocking
using Microsoft.AspNetCore.Routing;
using Sharpcode.AiAssistModule.Core.Services;
using VirtoCommerce.CatalogModule.Core.Services;
using Sharpcode.AiAssistModule.Data.Services;
using Sharpcode.AiAssistModule.Core.Models;
using Sharpcode.AiAssistModule.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using OpenAI;
using VirtoCommerce.Platform.Core.Settings;
using Microsoft.Extensions.Logging;
using VirtoCommerce.Platform.Data.Settings;
using Microsoft.Extensions.Options;
using Sharpcode.AiAssistModule.Core;
using static Sharpcode.AiAssistModule.Core.Enums.OpenAiImageModels;
using Newtonsoft.Json;
using System;

namespace Sharpcode.AiAssistModule.Tests
{
    public class OpenAiServiceTests
    {
        private IOpenAiClient _openAiClient;
        private IOptions<OpenAiOptions> _options;
        private Mock<ISettingsManager> _mockSettingsManager;
        private Mock<ILogger<OpenAiClient>> _mockLogger;
        private Mock<IItemService> _mockItemService;
        private OpenAiService _openAiService;

        public OpenAiServiceTests()
        {
            _mockSettingsManager = new Mock<ISettingsManager>();
            _mockLogger = new Mock<ILogger<OpenAiClient>>();
            _options = Options.Create(new OpenAiOptions() { ApiKey = Env.Var("ApiKey") });
            _openAiClient = new OpenAiClient(_mockSettingsManager.Object, _options, _mockLogger.Object);
            _mockItemService = new Mock<IItemService>();

            _openAiService = new OpenAiService(_openAiClient, _mockItemService.Object);
            _mockSettingsManager.Setup(sm => sm.GetObjectSettingAsync(ModuleConstants.Settings.General.OpenAiTextModel.Name, null, null)).ReturnsAsync(new ObjectSettingEntry() { Value = "gpt-3.5-turbo-1106" });
        }

        #region Description Generation Tests

        [Fact]
        public async void GenerateDescription()
        {
            var openAiTextRequest = new OpenAiTextRequest
            {
                Language = "en",
                DescLength = 100,
                DescriptionType = "QuickReview",
                Prompt = "The UltraFit Wireless Earbuds offer noise-cancelling technology, 12-hour battery life, and a sweat-resistant design."
            };

            var response = await _openAiService.GenerateDescription(openAiTextRequest);
            Assert.NotNull(response);
        }
        #endregion

        #region Translation Tests

        [Fact]
        public async void TranslateDescription()
        {
            var openAiTextRequest = new OpenAiTextRequest
            {
                Prompt = "The UltraFit Wireless Earbuds offer noise-cancelling technology, 12-hour battery life, and a sweat-resistant design.",
                Language = "fr-FR"

            };

            var response = await _openAiService.TranslateDescription(openAiTextRequest);
            Assert.NotNull(response);
        }
        #endregion

        #region Rephrasing Tests

        [Fact]
        public async void RephraseDescription()
        {
            var openAiTextRequest = new OpenAiTextRequest
            {
                Prompt = "The UltraFit Wireless Earbuds offer noise-cancelling technology, 12-hour battery life, and a sweat-resistant design."
            };

            var response = await _openAiService.RephraseDescription(openAiTextRequest);
            Assert.NotNull(response);
        }

        #endregion

        #region Product Image Generation Tests

        [Fact]
        public async void GenerateImage()
        {
            var openAiImageRequest = new OpenAiImageRequest
            {
                Prompt = "The UltraFit Wireless Earbuds offer noise-cancelling technology, 12-hour battery life, and a sweat-resistant design.",
                Model = Models.Dall_e_2,
                Size = Size.Size256x256,
                N = 1,
                Quality = Quality.standard,
                ResponseFormat = ResponseFormat.b64_json
            };

            var responses = await _openAiService.GenerateImage(openAiImageRequest);

            foreach (var response in responses)
            {
                Assert.NotNull(response);
                Assert.NotEmpty(Convert.FromBase64String(response));
            }
            
        }

        #endregion
    }
}
