using System.Linq;
using Microsoft.AspNetCore.Builder;
/*using Microsoft.EntityFrameworkCore;*/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Platform.Core.Settings;
using SharpCode.OpenAiModule.Core;

using OpenAI.Extensions;
using System;
using SharpCode.OpenAiModule.Core.Services;
using SharpCode.OpenAiModule.Data.Services;
using OpenAI;

namespace SharpCode.OpenAiModule.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {
        // Initialize database
        var connectionString = Configuration.GetConnectionString(ModuleInfo.Id) ??
                               Configuration.GetConnectionString("VirtoCommerce");

        /*serviceCollection.AddDbContext<AIModuleDbContext>(options => options.UseSqlServer(connectionString));*/
        serviceCollection.Configure<OpenAiOptions>(Configuration.GetSection("OpenAIServiceOptions").Bind);
        /*var apikey = new OpenAiOptions() { ApiKey = Configuration.GetSection("OpenAIServiceOptions.ApiKey") }
            ;*/
        /*serviceCollection.AddOpenAIService(settings => { settings.ApiKey = apikey.ToString(); });*/


        // Override models
        //AbstractTypeFactory<OriginalModel>.OverrideType<OriginalModel, ExtendedModel>().MapToType<ExtendedEntity>();
        //AbstractTypeFactory<OriginalEntity>.OverrideType<OriginalEntity, ExtendedEntity>();

        // Register services
        serviceCollection.AddTransient<IOpenAiService, OpenAiService>();
        serviceCollection.AddTransient<IOpenAiClient, OpenAIClient>();
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
            .Select(x => new Permission { ModuleId = ModuleInfo.Id, GroupName = "AIModule", Name = x })
            .ToArray());

        // Apply migrations
        using var serviceScope = serviceProvider.CreateScope();
       /* using var dbContext = serviceScope.ServiceProvider.GetRequiredService<AIModuleDbContext>();
        dbContext.Database.Migrate();*/
    }

    public void Uninstall()
    {
        // Nothing to do here
    }
}
