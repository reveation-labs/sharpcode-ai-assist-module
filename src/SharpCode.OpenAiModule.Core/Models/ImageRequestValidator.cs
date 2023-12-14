using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using SharpCode.OpenAiModule.Core.Enums;

namespace SharpCode.OpenAiModule.Core.Models
{
    public class ImageRequestValidator : AbstractValidator<GenerateImageRequest>
    {
        public ImageRequestValidator()
        {
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Prompt == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(request.Prompt), "Prompt cannot be empty"));
                }
                if (request.Model == OpenAiImageModels.Models.Dall_e_2)
                {
                    if(request.Prompt.Length > 1000)
                    {
                        context.AddFailure(new ValidationFailure(nameof(request.Prompt), "Prompt length cannot be more than 1000 characters for Dall-e-2"));
                    }
                    if(request.N > 10)
                    {
                        context.AddFailure(new ValidationFailure(nameof(request.N), "N cannot be more than 10 for Dall-e-2"));
                    }
                    if(request.Quality != OpenAiImageModels.Quality.standard)
                    {
                        context.AddFailure(new ValidationFailure(nameof(request.Quality), "Quality can only be Standard for Dall-e-2"));
                    }
                    if (request.Size != OpenAiImageModels.Size.Size256x256 && request.Size != OpenAiImageModels.Size.Size512x512 && request.Size != OpenAiImageModels.Size.Size1024x1024)
                    {
                        context.AddFailure(new ValidationFailure(nameof(request.Size), "Invalid size for Dall-e-2"));
                    }
                }
                else if (request.Model == OpenAiImageModels.Models.Dall_e_3)
                {
                    if (request.Prompt.Length > 4000)
                    {
                        context.AddFailure(new ValidationFailure(nameof(request.Prompt), "Prompt length cannot be more than 4000 characters for Dall-e-3"));
                    }
                    if (request.N != 1)
                    {
                        context.AddFailure(new ValidationFailure(nameof(request.N), "N cannot be more than 1 for Dall-e-3"));
                    }
                    if (request.Size != OpenAiImageModels.Size.Size1024x1792 && request.Size != OpenAiImageModels.Size.Size1792x1024 && request.Size != OpenAiImageModels.Size.Size1024x1024)
                    {
                        context.AddFailure(new ValidationFailure(nameof(request.Size), "Invalid size for Dall-e-3"));
                    }
                }
            });
        }
    }
}
