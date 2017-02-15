using System;
using System.Collections.Generic;
using System.Text;

// ADD THIS PART TO YOUR CODE
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Linq;

namespace RoomTemperatureTracker
{

    public class CrudDocumentDB
    {
        // ADD THIS PART TO YOUR CODE
        private const string EndpointUri = "https://seyondocumentdb.documents.azure.com:443/";
        private const string PrimaryKey = "aGrxrFjKxAVHSiwFzCkEqBf8SI2t52QqZIwS4PkBHKWMp5Y2KaJopZBgsgTUpO6rB4GCajIcEpQknMqS04YSMQ==";
        private DocumentClient client;
        private static Database db;
        private static DocumentCollection collection;

        public CrudDocumentDB()
        {
            this.client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
        }

        // ADD THIS PART TO YOUR CODE
        private void WriteToConsoleAndPromptToContinue(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        // ADD THIS PART TO YOUR CODE
        public async  Task<List<Temperature>> ExecuteTemperatureQuery(string databaseName, string collectionName)
        {
           db=await this.CreateDatabaseIfNotExists(databaseName);

           collection= await this.CreateDocumentCollectionIfNotExists(databaseName, collectionName);


            List<Temperature> Items = new List<Temperature>();
            // Set some common query options
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            // Here we find the Andersen family via its LastName
            var TemperatureQuery =  this.client.CreateDocumentQuery<Temperature>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), queryOptions).AsDocumentQuery();

            while (TemperatureQuery.HasMoreResults)
            {
                Items.AddRange(await TemperatureQuery.ExecuteNextAsync<Temperature>());
            }

            return Items;
        }

        // ADD THIS PART TO YOUR CODE
        public async Task<Database> CreateDatabaseIfNotExists(string databaseName)
        {
            // Check to verify a database with the id=FamilyDB does not exist
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == databaseName).AsEnumerable().FirstOrDefault();
            Console.WriteLine("1. Query for a database returned: {0}", database == null ? "no results" : database.Id);

            //check if a database was returned
            if (database == null)
            {
                //**************************
                // 2 -  Create a Database
                //**************************
                database = await client.CreateDatabaseAsync(new Database { Id = databaseName });
                Console.WriteLine("\n2. Created Database: id - {0} and selfLink - {1}", database.Id, database.SelfLink);
            }

            return database;
        }
    

        // ADD THIS PART TO YOUR CODE
        public async Task<DocumentCollection> CreateDocumentCollectionIfNotExists(string databaseName, string collectionName)
        {
            DocumentCollection collection = client.CreateDocumentCollectionQuery(db.SelfLink).Where(c => c.Id == collectionName).AsEnumerable().FirstOrDefault();

            if (collection == null)
            {
                DocumentCollection c1 = await client.CreateDocumentCollectionAsync(db.SelfLink, new DocumentCollection { Id = collectionName });

                Console.WriteLine("\n1.1. Created Collection \n{0}", c1);

            }

            return collection;
        }

    }
}
