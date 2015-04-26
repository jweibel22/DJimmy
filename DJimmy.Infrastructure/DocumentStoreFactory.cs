using System.Reflection;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace DJimmy.Infrastructure
{
    public static class DocumentStoreFactory
    {
        static DocumentStore documentStore = new DocumentStore { Url = "http://localhost:8080", DefaultDatabase = "DJimmy3" };

        static DocumentStoreFactory()
        {
            documentStore.Initialize();
            IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), documentStore);
            documentStore.Conventions.DefaultQueryingConsistency = ConsistencyOptions.AlwaysWaitForNonStaleResultsAsOfLastWrite;

            //documentStore.Conventions.ShouldCacheRequest = url => true;
        }

        public static DocumentStore DocumentStore
        {
            get
            {
                return documentStore;
            }
        }
    }
}
