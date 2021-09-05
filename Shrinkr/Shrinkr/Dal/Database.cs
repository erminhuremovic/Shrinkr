using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Shrinkr.Dto;

namespace Shrinkr.Dal
{
    public class Database : IDatabase
    {
        private const string DatabaseName = "ShrinkrData.db";
        private const string UrlMappingsName = "UrlMappings";

        public List<UrlMapping> UrlMappings { get => this.GetUrlMappingsFromDatabase(); } 

        public void Add(string token, string shortUrl, string longUrl)
        {
            using (var database = new LiteDatabase(DatabaseName))
            {
                var collection = database.GetCollection<UrlMapping>(UrlMappingsName);

                var newUrlMapping = new UrlMapping
                {
                    Token = token,
                    ShortUrl = shortUrl,
                    LongUrl = longUrl
                };

                collection.Insert(newUrlMapping);
                collection.EnsureIndex(x => x.Token);
            }
        }

        private List<UrlMapping> GetUrlMappingsFromDatabase()
        {
            using (var database = new LiteDatabase(DatabaseName))
            {
                var collection = database.GetCollection<UrlMapping>(UrlMappingsName);

                return collection.FindAll().ToList();
            }
        }
    }
}
