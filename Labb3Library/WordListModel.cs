using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labb3Library
{
    class WordListModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string[] Languages { get; set; }
        public List<WordModel> Words { get; set; }
    }
}
