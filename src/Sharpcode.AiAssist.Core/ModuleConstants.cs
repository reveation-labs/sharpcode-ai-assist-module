using System.Collections.Generic;
using Sharpcode.AiAssistModule.Core.Models;
using VirtoCommerce.Platform.Core.Settings;

namespace Sharpcode.AiAssistModule.Core;

public static class ModuleConstants
{
    public static class Security
    {
        public static class Permissions
        {
            public const string Access = "AiAssistModule:access";
            public const string Create = "AiAssistModule:create";
            public const string Read = "AiAssistModule:read";
            public const string Update = "AiAssistModule:update";
            public const string Delete = "AiAssistModule:delete";

            public static string[] AllPermissions { get; } =
            {
                Access,
                Create,
                Read,
                Update,
                Delete,
            };
        }
    }

    public static class Settings
    {
        public static class General
        {
            public static SettingDescriptor OpenAiTextModel { get; } = new SettingDescriptor
            {
                Name = "AiAssistModule.OpenAiTextModel",
                GroupName = "AiAssistModule|General",
                ValueType = SettingValueType.ShortText,
                AllowedValues = ["gpt-4","gpt-4-0613","gpt-4-32k","gpt-4-32k-0613","gpt-4-1106-preview","gpt-3.5-turbo-16k","gpt-3.5-turbo-1106"]
            };

            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return OpenAiTextModel;

                }
            }
        }

        public static IEnumerable<SettingDescriptor> AllSettings
        {
            get
            {
                return General.AllGeneralSettings;
            }
        }
    }

    public static string GenerateSystemMessage = "You are expert at creating concise and straightforward product descriptions in for e-commerce websites." +
                                                "You write in a tone that emphasizes features and benefits while maintaining a professional and informative style." +
                                                "You write key features in short and simple bullet points." +
                                                "Make sure you generate the product description in {0} language." +
                                                "Make sure you generate approx {1} words. " +
                                                "Generate the description according to {2} type. " +
                                                "The output format should be **HTML Format**. " +
                                                "Do not add <html> tag or backticks in output.";

    public static string TranslateSystemMessage = "You are expert in {0} langauge." +
                                                "You are a translator." +
                                                "The output format should be **HTML Format**." +
                                                "Do not add <html> tag or any extra quotes in output." +
                                                "The following is a product description in markdown format, extract the text and convert it in {0}.";

    public static string RephraseSystemMessage = "You are expert in rephrasing content in SEO-Friendly tone. " +
                                                "Help in rephrasing.The input is in html markdown format, extract the text and then rephrase it. " +
                                                "Also make sure you generate the output in the same language as the product description. " +
                                                "The output format should be **HTML Format**. " +
                                                "Do not add <html> tag or any extra quotes in output.";

    public static string GenerateImageSystemMessage = "Generate realistic product images suitable for e-commerce websites based on the provided description. " +
                                                      "Do not add text to the images.";

    public static string IncludeProductPropMessage = "These are the product properties, add the properties which you find relevant.Make sure to remove any product id or sku or anything related to these. {0}";

}
