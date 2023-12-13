using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenAI.ObjectModels;
using SC.AIModule.Core;
using SC.AIModule.Core.Models;
using SC.AIModule.Core.Services;
using VirtoCommerce.Platform.Core.Exceptions;

namespace SC.AIModule.Web.Controllers.Api
{
    [Route("api/ai-module")]
    public class AIModuleController : Controller
    {
        private readonly IAIService _aiService;
        private readonly ILogger<AIModuleController> _logger;
        public AIModuleController(IAIService aiService, ILogger<AIModuleController> logger)
        {
            _aiService = aiService;
            _logger = logger;
        }

        [HttpGet]
        [Route("generate")]
        public async Task<ActionResult<string>> Generate(string prompt, OpenAiModels model = OpenAiModels.Gpt_3__5_Turbo_1106, int descLength = 100)
        {
            if(prompt == null)
            {
                return BadRequest();
            }

            try
            {
                return await _aiService.GenerateDescription(model.ToLowercaseString(), prompt, descLength);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ExpandExceptionMessage());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        [HttpGet]
        [Route("translate")]
        public async Task<ActionResult<string>> Translate(string text, string language, OpenAiModels model = OpenAiModels.Gpt_3__5_Turbo_1106)
        {
            if (text == null)
            {
                return BadRequest();
            }

            try
            {
                return await _aiService.TranslateDescription(model.ToLowercaseString(), text, language);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ExpandExceptionMessage());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        [HttpGet]
        [Route("rephrase")]
        public async Task<ActionResult<string>> Rephrase(string text, OpenAiModels model = OpenAiModels.Gpt_3__5_Turbo_1106)
        {
            if (text == null)
            {
                return BadRequest();
            }

            try
            {
                return await _aiService.RephraseDescription(model.ToLowercaseString(), text);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ExpandExceptionMessage());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);

            }
        }

        [HttpGet]
        [Route("image")]
        public async Task<ActionResult<string>> GenerateImage(string text, string size, int numOfImages, string quality, OpenAiModels model = OpenAiModels.Gpt_3__5_Turbo_1106)
        {
            if (text == null)
            {
                return BadRequest();
            }

            try
            {
                return await _aiService.GenerateImage(model.ToLowercaseString(), text, size, numOfImages, quality);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ExpandExceptionMessage());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);

            }
        }
    }
}
