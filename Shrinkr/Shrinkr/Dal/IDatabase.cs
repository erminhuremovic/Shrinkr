using System.Collections.Generic;
using Shrinkr.Dto;

namespace Shrinkr.Dal
{
    public interface IDatabase
    {
        List<UrlMapping> UrlMappings { get; }

        void Add(string token, string longUrl);
    }
}
