using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.AIModule.Core.Enums
{
    public class OpenAiImageModels
    {
        public enum Models
        {
            Dall_e_2,
            Dall_e_3
        }

        public enum Quality
        {
            standard,
            hd
        }

        public enum ResponseFormat
        {
            url,
            b64_json
        }

        public enum Size
        {
            Size256x256,
            Size512x512,
            Size1024x1024,
            Size1792x1024,
            Size1024x1792
        }

        public enum Style
        {
            vivid,
            natural
        }
    }
    
}
