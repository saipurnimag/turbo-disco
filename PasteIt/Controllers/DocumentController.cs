using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PasteIt.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PasteIt.Controllers
{
    

    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        // GET: api/<controller>/{code}
        [HttpGet("{code}")]
        public Object Get(String code)
        {
            var filter = Builders<Object>.Filter.Eq("Code", code);
            Document document = (Document)Program.collection.Find<Object>(filter).FirstOrDefault();
            if (document == null)
            {
                string[] s = { "No such url found. I think your code has expired" };
                return s;
            }
            return document;
        }


        // POST api/<controller>
        [HttpPost]
        public async Task<Object> PostAsync([FromBody]Document value)
        {
            //generate a 6 digit code which is already not present in Mongo
            String code = generateId();
            Console.WriteLine("returning " + code);
            Document newDocument = new Document();
            newDocument.Code = code;
            newDocument.Syntax = value.Syntax;
            newDocument.Title = value.Title;
            newDocument.TimeSavedAt = DateTime.Now;
            
            switch (value.DeleteIn)
            {
                case ExpireIn.ONE_DAY:
                    newDocument.expireAt = DateTime.Now.AddDays(1);
                    break;
                case ExpireIn.ONE_WEEK:
                    newDocument.expireAt = DateTime.Now.AddDays(7);
                    break;
                case ExpireIn.ONE_MONTH:
                    newDocument.expireAt = DateTime.Now.AddMonths(1);
                    break;
                case ExpireIn.ONE_YEAR:
                    newDocument.expireAt = DateTime.Now.AddYears(1);
                    break;
                case ExpireIn.CUSTOM:
                    break;
                default:
                    break;
            }
            
            newDocument.Content = value.Content;
            bool result = storeInDatabase(newDocument);
            if(result)
            {
                return newDocument;
            }
            FailureMessage obj = new FailureMessage("Sorry couldn't save your meassge. Please try again");
            return obj;
        }

        private bool storeInDatabase(Document newDocument)
        {
            
           Program.collection.InsertOne(newDocument);
           return true;
           
        }

        private String generateId()
        {
            String code = "";
            StringBuilder stringbuilder = new StringBuilder(code);
            try
            {
                for (int i = 0; i < 6; ++i)
                {
                    int a = 0;
                    int b = Program.characters.Length;
                    int idx = Program.randomNum.Next(a, b);
                    code += Program.characters[idx] + "";
                }
            }
            catch(Exception e)
            {
                return e.ToString();
            }
            return code;
        }
    }
}

public class FailureMessage
{
    public String message;
    public FailureMessage(String msg)
    {
        message = msg;
    }
}
