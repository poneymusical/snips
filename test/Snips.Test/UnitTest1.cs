using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using Snips.Domain.BusinessObjects;
using Snips.Mongo;
using Snips.Mongo.Repositories;
using Xunit;

namespace Snips.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", camelCaseConvention, type => true);

            var settings = new MongoSettings
            {
                ConnectionString = "###REPLACE_ME###",
                DatabaseName = "snips"
            };

            var directoryRepository = new Repository<Directory>(settings, Collections.Directories);
            var directory = new Directory
            {
                Id = Guid.NewGuid(),
                Name = "Développement"
            };
            await directoryRepository.Insert(directory);
            
            var directory2 = new Directory
            {
                Id = Guid.NewGuid(),
                Name = ".net core",
                ParentDirectoryId = directory.Id
            };
            await directoryRepository.Insert(directory2);

            var directory3 = new Directory
            {
                Id = Guid.NewGuid(),
                Name = "Infrastructure"
            };
            await directoryRepository.Insert(directory3);

            var snippetRepository = new Repository<Snippet>(settings, Collections.Snippets);

            await snippetRepository.Insert(new Snippet
            {
                Id = Guid.NewGuid(),
                Title = "Faire le setup d'Entity Framework Core",
                Content = "## HOWTO basic\nRTFM",
                DirectoryId = directory2.Id
            });
            
            await snippetRepository.Insert(new Snippet
            {
                Id = Guid.NewGuid(),
                Title = "FluentValidation",
                Content = "## HOWTO fluent\nRTFM",
                DirectoryId = directory2.Id
            });
            
            await snippetRepository.Insert(new Snippet
            {
                Id = Guid.NewGuid(),
                Title = "Environnements test/acc/prod",
                Content = "## Test\n* WASABI",
                DirectoryId = directory.Id
            });
            
            await snippetRepository.Insert(new Snippet
            {
                Id = Guid.NewGuid(),
                Title = "Cluster Kubernetes",
                Content = "## CONTENT\ncontent content **content**",
                DirectoryId = directory3.Id
            });
            
            await snippetRepository.Insert(new Snippet
            {
                Id = Guid.NewGuid(),
                Title = "Un snippet lié à rien",
                Content = "Comment ne pas se faire licencier pour les nuls"
            });
        }
    }
}