using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.AIModule.Core.Enums;
using OI = SC.AIModule.Core.Enums.OpenAiImageModels;

namespace SC.AIModule.Core.Models
{
    public class GenerateImageRequest
    {
        public string Prompt { get; set; }

        public OI.Models Model { get; set; } = OI.Models.Dall_e_3;

        public int N { get; set; } = 1;

        public OI.Quality Quality { get; set; } = OI.Quality.standard;

        public OI.ResponseFormat ResponseFormat { get; set; } = OI.ResponseFormat.url;

        public OI.Size Size { get; set; } = OI.Size.Size1024x1024;

        public OI.Style Style { get; set; } = OI.Style.natural;
    }
}

