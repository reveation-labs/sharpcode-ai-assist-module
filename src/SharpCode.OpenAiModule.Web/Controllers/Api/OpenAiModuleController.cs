using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenAI.ObjectModels;
using SharpCode.OpenAiModule.Core;
using SharpCode.OpenAiModule.Core.Enums;
using SharpCode.OpenAiModule.Core.Models;
using SharpCode.OpenAiModule.Core.Services;
using SharpCode.OpenAiModule.Core.Utilities;
using VirtoCommerce.Platform.Core.Exceptions;

namespace SharpCode.OpenAiModule.Web.Controllers.Api
{
    [Route("api/openai")]
    public class OpenAiModuleController : Controller
    {
        private readonly IOpenAiService _aiService;
        private readonly ILogger<OpenAiModuleController> _logger;
        public OpenAiModuleController(IOpenAiService aiService, ILogger<OpenAiModuleController> logger)
        {
            _aiService = aiService;
            _logger = logger;
        }

        [HttpPost]
        [Route("generate")]
        [Authorize(ModuleConstants.Security.Permissions.Access)]
        public async Task<ActionResult<string>> Generate([FromBody] OpenAiTextRequest openAiTextRequest)
        {
            if(string.IsNullOrEmpty(openAiTextRequest.Prompt) || string.IsNullOrEmpty(openAiTextRequest.DescriptionType))
            {
                return BadRequest();
            }

            try
            {
                return await _aiService.GenerateDescription(openAiTextRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ExpandExceptionMessage());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        [HttpPost]
        [Route("translate")]
        [Authorize(ModuleConstants.Security.Permissions.Access)]
        public async Task<ActionResult<string>> Translate([FromBody] OpenAiTextRequest openAiTextRequest)
        {
            if (string.IsNullOrEmpty(openAiTextRequest.Language))
            {
                return BadRequest();
            }

            try
            {
                return await _aiService.TranslateDescription(openAiTextRequest);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ExpandExceptionMessage());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        [HttpPost]
        [Route("rephrase")]
        [Authorize(ModuleConstants.Security.Permissions.Access)]
        public async Task<ActionResult<string>> Rephrase([FromBody] OpenAiTextRequest openAiTextRequest)
        {
            if (string.IsNullOrEmpty(openAiTextRequest.Prompt))
            {
                return BadRequest();
            }

            try
            {
                return await _aiService.RephraseDescription(openAiTextRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ExpandExceptionMessage());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);

            }
        }

        [HttpPost]
        [Route("generate-image")]
        [Authorize(ModuleConstants.Security.Permissions.Access)]
        public async Task<ActionResult<string>> GenerateImage([FromBody] GenerateImageRequest generateImageRequest)
        {
            var validationResult = await new ImageRequestValidator().ValidateAsync(generateImageRequest);

            if (validationResult.IsValid)
            {
                try
                {
                    return await _aiService.GenerateImage(generateImageRequest);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ExpandExceptionMessage());
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);

                }
            }

            return BadRequest(validationResult.Errors);   
        }
    }
}
