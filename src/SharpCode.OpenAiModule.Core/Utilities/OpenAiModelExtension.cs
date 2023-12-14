using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCode.OpenAiModule.Core.Enums;

namespace SharpCode.OpenAiModule.Core.Utilities
{
    public static class OpenAiModelExtension
    {
        public static string ToLowercaseString(this OpenAiImageModels.Models model)
        {
            return model.ToString().ToLower().Replace("__", ".").Replace("_", "-");
        }

        public static string SizeConverter(this OpenAiImageModels.Size size)
        {
            return size.ToString().Replace("Size", "");
        }
    }
}
