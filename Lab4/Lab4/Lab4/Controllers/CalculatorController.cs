using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lab4ra.Controllers
{
    public class CalculatorController : ApiController
    {

        // GET api/values
        // возвращаем список всех вычисленных значений калькулятора
        public IEnumerable<int> Get()
        {
            return WebApiApplication.calculator.GetMem();
        }

        // GET api/values/5
        // возвращаем вычисленное значение по номеру (id) если 0 - то последнее
        public string Get(int id)
        {
            return WebApiApplication.calculator.GetMem(id);
        }

        // POST api/values
        // Получаем список параметров для вычислений и передаём их в калькулятор
        public IHttpActionResult Post([FromBody] List<String> value)
        {
            for(int i=0;i< value.Count; i++)
            {
                if (!WebApiApplication.calculator.Calculate(value[i]))
                {
                    return this.StatusCode(HttpStatusCode.NotAcceptable);
                }
            }
            return this.StatusCode(HttpStatusCode.OK);
        }

        // PUT api/values/5
        // изменяет вычисленное значение с индексом id
        public IHttpActionResult Put(int id, [FromBody] string value)
        {
            int num;
            if (int.TryParse(value, out num))
            {
                if (WebApiApplication.calculator.UpdateMem(id, num))
                {
                    return this.StatusCode(HttpStatusCode.OK);
                }
                else
                {
                    return this.StatusCode(HttpStatusCode.NotFound);
                }
            }
            else
            {
                return this.StatusCode(HttpStatusCode.NotAcceptable);
            }
        }

        // DELETE api/values/5
        // удаялет вычисленное значение с индексом id
        public IHttpActionResult Delete(int id)
        {
            if (WebApiApplication.calculator.DeleteMem(id))
            {
                return this.StatusCode(HttpStatusCode.OK);
            }
            else
            {
                return this.StatusCode(HttpStatusCode.NotFound);
            }


        }
    }
}
