using System;
using System.Collections.Generic;
using Shrinkr.Dto;

namespace Shrinkr.Dal
{
    public class Database : IDatabase
    {
        public List<UrlMapping> UrlMappings { get; } = new List<UrlMapping>();

        public void Add(string token, string longUrl)
        {
            throw new NotImplementedException();
        }
    }
}
