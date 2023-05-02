using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;





namespace Labb3Library
{
    public class WordList
    {
        public string Name { get { return model.Name; } }
        public string[] Languages { get { return model.Languages; } }

        private WordListModel model;

        public WordList(string name, params string[] languages)
        {
            if (GetLists().Contains(name))
            {
                throw new ArgumentException("List already exists");
            }
            model = new WordListModel();
            model.Name = name;
            model.Languages = languages;
            model.Id = Guid.NewGuid();
            model.Words = new List<WordModel>();
        }

        private WordList(WordListModel model)
        {
            this.model = model;
        }
        
        public static string[] GetLists() 
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000&appName=mongosh+1.5.0");
            var database = client.GetDatabase("Labb3");
            var collection = database.GetCollection<WordListModel>("WordLists");
            var lists = collection.Find(new BsonDocument()).ToList();
            string[] names = new string[lists.Count];
            for (int i = 0; i < lists.Count; i++)
            {
                names[i] = lists[i].Name;
            }
            return names;
        }
        

        public static WordList LoadList(string name)
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000&appName=mongosh+1.5.0");
            var database = client.GetDatabase("Labb3");
            var collection = database.GetCollection<WordListModel>("WordLists");
            var filter = Builders<WordListModel>.Filter.Eq("Name", name);
            var model = collection.Find(filter).FirstOrDefault();
            return new WordList(model);
        }

        public void Save()
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000&appName=mongosh+1.5.0");
            var database = client.GetDatabase("Labb3");
            var collection = database.GetCollection<WordListModel>("WordLists");
            var filter = Builders<WordListModel>.Filter.Eq("_id", model.Id);
            collection.ReplaceOne(filter, model, new ReplaceOptions { IsUpsert=true} );
        }


        public void Add(params string[] translations)
        {
            if (translations.Length != Languages.Length)
            {
                throw new ArgumentException("Number of translations does not match number of languages in the word list");
            }

            WordModel word = new WordModel(translations);
            model.Words.Add(word);
        }

        public bool Remove(string word)
        {
            for (int i = 0; i < model.Words.Count; i++)
            {
                var translations = model.Words[i].Translations;
                if (translations.Contains(word))
                {
                    int index = Array.IndexOf(translations, word);
                    return model.Words.Remove(model.Words[i]);
                }
            }
            return false;
        }

        public void List(int sortByTranslation, Action<string[]> showTranslations)
        {
            var words = model.Words.OrderBy(w => w.Translations[sortByTranslation]);
            foreach (var word in words)
            {
                showTranslations(word.Translations);
            }
        }
        public int Count()
        {
            return model.Words.Count;
        }
        public IEnumerable<string[]> Words()
        {
            return model.Words.Select(w => w.Translations);
        }
        public WordModel GetWordToPractice()
        {
            var rng = new Random();
            int fromLanguage, toLanguage;
            do
            {
                fromLanguage = rng.Next(Languages.Length);
                toLanguage = rng.Next(Languages.Length);
            } while (fromLanguage == toLanguage);

            var word = new WordModel(fromLanguage, toLanguage, model.Words[rng.Next(model.Words.Count)].Translations);
            return word;
        }

    }
}
