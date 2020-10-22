using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using Snips.Domain.BusinessObjects;
using Snips.Domain.Interfaces;
using Snips.Mongo.Repositories;

namespace Snips.Mongo.DI
{
    public static class MongoServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfigurationSection mongoDbSettingsConfigurationSection)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", camelCaseConvention, type => true);

            services.Configure<MongoSettings>(mongoDbSettingsConfigurationSection);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoSettings>>().Value);
            services.AddTransient<IRepository<Directory>, Repository<Directory>>(sp =>
                new Repository<Directory>(sp.GetRequiredService<MongoSettings>(), Collections.Directories));
            services.AddTransient<IRepository<Snippet>, Repository<Snippet>>(sp =>
                new Repository<Snippet>(sp.GetRequiredService<MongoSettings>(), Collections.Snippets));
            return services;
        }
    }
}