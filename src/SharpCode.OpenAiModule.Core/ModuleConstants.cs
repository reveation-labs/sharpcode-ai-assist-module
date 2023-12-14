using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Settings;

namespace SharpCode.OpenAiModule.Core;

public static class ModuleConstants
{
    public static class Security
    {
        public static class Permissions
        {
            public const string Access = "OpenAiModule:access";
            public const string Create = "OpenAiModule:create";
            public const string Read = "OpenAiModule:read";
            public const string Update = "OpenAiModule:update";
            public const string Delete = "OpenAiModule:delete";

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
            public static SettingDescriptor OpenAiModuleEnabled { get; } = new SettingDescriptor
            {
                Name = "OpenAiModule.OpenAiModuleEnabled",
                GroupName = "OpenAiModule|General",
                ValueType = SettingValueType.Boolean,
                DefaultValue = false,
            };

            public static SettingDescriptor OpenAiModulePassword { get; } = new SettingDescriptor
            {
                Name = "OpenAiModule.OpenAiModulePassword",
                GroupName = "OpenAiModule|Advanced",
                ValueType = SettingValueType.SecureString,
                DefaultValue = "qwerty",
            };

            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return OpenAiModuleEnabled;
                    yield return OpenAiModulePassword;
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
}
