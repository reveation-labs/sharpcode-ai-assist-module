using System.Linq;
using Microsoft.AspNetCore.Builder;
/*using Microsoft.EntityFrameworkCore;*/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Platform.Core.Settings;
using Sharpcode.AiAssistModule.Core;

using OpenAI.Extensions;
using System;
using Sharpcode.AiAssistModule.Core.Services;
using Sharpcode.AiAssistModule.Data.Services;
using OpenAI;

namespace Sharpcode.AiAssistModule.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {
        // Initialize database
        var connectionString = Configuration.GetConnectionString(ModuleInfo.Id) ??
                               Configuration.GetConnectionString("VirtoCommerce");

        serviceCollection.Configure<OpenAiOptions>(Configuration.GetSection("OpenAIServiceOptions").Bind);

        // Register services
        serviceCollection.AddTransient<IOpenAiService, OpenAiService>();
        serviceCollection.AddTransient<IOpenAiClient, OpenAiClient>();
    }

    public void PostInitialize(IApplicationBuilder appBuilder)
    {
        var serviceProvider = appBuilder.ApplicationServices;

        // Register settings
        var settingsRegistrar = serviceProvider.GetRequiredService<ISettingsRegistrar>();
        settingsRegistrar.RegisterSettings(ModuleConstants.Settings.AllSettings, ModuleInfo.Id);

        // Register permissions
        var permissionsRegistrar = serviceProvider.GetRequiredService<IPermissionsRegistrar>();
        permissionsRegistrar.RegisterPermissions(ModuleConstants.Security.Permissions.AllPermissions
            .Select(x => new Permission { ModuleId = ModuleInfo.Id, GroupName = "AiAssistModule", Name = x })
            .ToArray());

        // Apply migrations
        using var serviceScope = serviceProvider.CreateScope();

    }

    public void Uninstall()
    {
        // Nothing to do here
    }
}
