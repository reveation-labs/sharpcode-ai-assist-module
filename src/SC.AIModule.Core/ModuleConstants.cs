using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Settings;

namespace SC.AIModule.Core;

public static class ModuleConstants
{
    public static class Security
    {
        public static class Permissions
        {
            public const string Access = "AIModule:access";
            public const string Create = "AIModule:create";
            public const string Read = "AIModule:read";
            public const string Update = "AIModule:update";
            public const string Delete = "AIModule:delete";

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
            public static SettingDescriptor AIModuleEnabled { get; } = new SettingDescriptor
            {
                Name = "AIModule.AIModuleEnabled",
                GroupName = "AIModule|General",
                ValueType = SettingValueType.Boolean,
                DefaultValue = false,
            };

            public static SettingDescriptor AIModulePassword { get; } = new SettingDescriptor
            {
                Name = "AIModule.AIModulePassword",
                GroupName = "AIModule|Advanced",
                ValueType = SettingValueType.SecureString,
                DefaultValue = "qwerty",
            };

            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return AIModuleEnabled;
                    yield return AIModulePassword;
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
