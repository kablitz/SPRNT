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
using Sprinter.Helpers;
using System.Configuration;

namespace Sprinter.Controllers
{
    public class NameGenController : Controller
    {
        string _connection = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
        string _database = ConfigurationManager.AppSettings["MongoDBName"];

        private Random _rndNoun;
        private Random _rndAdjective;
        private Random _rndVerb;

        public ActionResult Index()
        {
            return View(new NameGenViewModel());
        }

        [HttpPost]
        public ActionResult Index(NameGenViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            _rndNoun = new Random();
            _rndAdjective = new Random();
            _rndVerb = new Random();

            var sprintName = string.Empty;

            var firstWordType = EnumUtil.ParseEnum<WordType>(viewModel.FirstWord);
            var secondWordType = EnumUtil.ParseEnum<WordType>(viewModel.SecondWord);
            var thirdWordType = EnumUtil.ParseEnum<WordType>(viewModel.ThirdWord);

            sprintName += GetWord(firstWordType) + " ";
            sprintName += GetWord(secondWordType) + " ";
            sprintName += GetWord(thirdWordType);

            viewModel.Name = sprintName;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddWord()
        {
            var client = new MongoClient(_connection);
            var db = client.GetDatabase(_database);

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

        private string GetWord(WordType type)
        {
            switch (type)
            {
                case WordType.Verb:
                    
                    var verbCcount = Enum.GetNames(typeof(Verb)).Length;
                    return ((Verb)_rndNoun.Next(0, verbCcount)).ToReadable();

                case WordType.Adjective:

                    var adjectiveCount = Enum.GetNames(typeof(Adjective)).Length;
                    return ((Adjective)_rndAdjective.Next(0, adjectiveCount)).ToReadable();

                case WordType.Noun:
                    var nounCount = Enum.GetNames(typeof(Noun)).Length;
                    return ((Noun)_rndVerb.Next(0, nounCount)).ToReadable();
                
                default:
                    return string.Empty;
            }
        }
    }
}
