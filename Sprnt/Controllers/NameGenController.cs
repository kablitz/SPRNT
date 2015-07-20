using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Sprinter.Enums;
using Sprinter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Sprinter.ViewModel;

namespace Sprinter.Controllers
{
    public class NameGenController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index(NameGenViewModel viewModel)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddWord()
        {
            string uri = "mongodb://vsebastian:password101@ds047812.mongolab.com:47812/appharbor_vh0wnjnb";

            //MongoUrl mongoUrl = new MongoUrl(uri);
            var client = new MongoClient(uri);
            var db = client.GetDatabase("appharbor_vh0wnjnb");

            var words = db.GetCollection<BsonDocument>("Word");

            var word1 = new Word
            {
                Text = "Ape",
                Type = Enums.WordType.Noun.ToString()
            };

            var bsonObject = word1.ToBsonDocument();

            try
            {
                await words.InsertOneAsync(bsonObject);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        private int RandomEnumIndex<T>(T en) where T : IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("en must be enum type");

            var count = Enum.GetNames(typeof(T)).Length;

            Random rnd = new Random();
            
            return rnd.Next(0, count);
        }
    }
}
