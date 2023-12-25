using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcode.AiAssistModule.Core.Models
{
    public class OpenAiTextRequest
    {
        public string ProductId { get; set; }
        public string Language { get; set; } = "en-US";
        public string Prompt { get; set; }
        public int DescLength { get; set; } = 100;
        public string DescriptionType { get; set; } = "FullReview";
    }
}
