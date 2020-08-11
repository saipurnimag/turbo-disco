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
        public Document Post([FromBody]Document value)
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
            return newDocument;
        }

        private String generateId()
        {
            String code = "";
            StringBuilder stringbuilder = new StringBuilder(code);
            
            for (int i = 0; i < 6; ++i)
            {
                int idx = Program.randomNum.Next(0, Program.characters.Length);

                Console.Write(Program.characters[idx]);
                code += Program.characters[idx] + "";
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
