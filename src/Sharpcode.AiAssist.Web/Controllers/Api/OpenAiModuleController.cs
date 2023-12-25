using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenAI.ObjectModels;
using Sharpcode.AiAssistModule.Core;
using Sharpcode.AiAssistModule.Core.Enums;
using Sharpcode.AiAssistModule.Core.Models;
using Sharpcode.AiAssistModule.Core.Services;
using Sharpcode.AiAssistModule.Core.Utilities;
using VirtoCommerce.Platform.Core.Exceptions;

namespace Sharpcode.AiAssistModule.Web.Controllers.Api
{
    [Route("api/openai")]
    public class AiAssistModuleController : Controller
    {
        private readonly IOpenAiService _aiService;
        private readonly ILogger<AiAssistModuleController> _logger;
        public AiAssistModuleController(IOpenAiService aiService, ILogger<AiAssistModuleController> logger)
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
        public async Task<ActionResult<List<string>>> GenerateImage([FromBody] OpenAiImageRequest generateImageRequest)
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
