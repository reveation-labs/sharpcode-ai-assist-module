using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.AIModule.Core.Models
{
    public enum OpenAiModels
    {
        Gpt_4,
        Gpt_4_0613,
        Gpt_4_32k,
        Gpt_4_32k_0613,
        Gpt_4_1106_preview,
        Gpt_4_vision_preview,
        Gpt_3__5_Turbo_16k,
        Gpt_3__5_Turbo_1106,
        Dall_e_2,
        Dall_e_3
    }

    public static class OpenAiModelsExtensions
    {
        public static string ToLowercaseString(this OpenAiModels model)
        {
            return model.ToString().ToLower().Replace("__", ".").Replace("_", "-");
        }
    }

}
