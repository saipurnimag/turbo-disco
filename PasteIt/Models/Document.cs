using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PasteIt.Models
{
    public enum ExpireIn
    {
        ONE_DAY = 1,
        ONE_WEEK,
        ONE_MONTH,
        ONE_YEAR,
        CUSTOM
    }
    public class Document
    {

        public String Code { get; set; }

        public ExpireIn DeleteIn { get; set; }

        public DateTime? TimeSavedAt { get; set; }

        public String Content { get; set; }

        public String Syntax { get; set; }

        public String Title { get; set; }
        
        public DateTime expireAt { get; set; }
    }
}
