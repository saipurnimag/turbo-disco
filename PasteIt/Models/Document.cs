using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PasteIt.Models
{
    public class Document
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public String Code { get; set; }

        public DateTime? TimeSavedAt { get; set; }

        public String Content { get; set; }

        public String Syntax { get; set; }

        public String Title { get; set; }

        public String DeleteIn { get; set; }

        public Document()
        {
            Id = ObjectId.GenerateNewId();
        }
    }
}
