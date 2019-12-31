using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompanyReader
{
    [BsonIgnoreExtraElements]
    public class Company
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        public int founded_year { get; set; }

        public string name { get; set; }
        
        public string homepage_url { get; set; }
    }
}