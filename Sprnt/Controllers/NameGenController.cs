using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Sprinter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sprinter.Controllers
{
    public class NameGenController : Controller
    {
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddWord()
        {
            string uri = "mongodb://vsebastian:p@ssw0rd32@ds047812.mongolab.com:47812/appharbor_vh0wnjnb";

            MongoUrl mongoUrl = new MongoUrl(uri);
            var client = new MongoClient(mongoUrl);

            var db = client.GetDatabase(mongoUrl.DatabaseName);

            var words = db.GetCollection<BsonDocument>("Word");

            var word1 = new Word
            {
                Text = "Ape",
                Type = Enums.WordType.Noun.ToString()
            };

            var bsonObject = word1.ToBsonDocument();

            await words.InsertOneAsync(bsonObject);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

    }
}
