using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FunctionalApproach.Controllers
{
    using Model;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly Func<IEnumerable<Value>> _readAll;
        private readonly Func<int, Value> _readAllById;
        private readonly Func<Value, Value> _create;
        private readonly Func<int, Value, Value> _update;
        private readonly Action<int> _delete;

        public ValuesController(Func<IEnumerable<Value>> readAll,
            Func<int, Value> readAllById,
            Func<Value, Value> create,
            Func<int, Value, Value> update,
            Action<int> delete)
        {
            _readAll = readAll;
            _readAllById = readAllById;
            _create = create;
            _update = update;
            _delete = delete;
        }

        // GET api/value
        [HttpGet]
        public IEnumerable<Value> Get()
        {
            return _readAll();
        }

        // GET api/value/5
        [HttpGet("{id}")]
        public Value Get(int id)
        {
            return _readAllById(id);
        }

        // POST api/value
        [HttpPost]
        public Value Post([FromBody]Value value)
        {
            return _create(value);
        }

        // PUT api/value/5
        [HttpPut("{id}")]
        public Value Put(int id, [FromBody]Value value)
        {
            return _update(id, value);
        }

        // DELETE api/value/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _delete(id);
        }
    }
}
