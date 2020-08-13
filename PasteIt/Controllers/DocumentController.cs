using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PasteIt.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PasteIt.Controllers
{
    

    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public String Get(int id)
        {
            String code = generateId();
            Console.WriteLine("returning "+ code);
            
            return code;
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
            newDocument.DeleteIn = value.DeleteIn;
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

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Document form)
        {

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
