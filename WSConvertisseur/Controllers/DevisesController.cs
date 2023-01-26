using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        private List<Devise> devises;

        /// <summary>
        /// Currencies's list
        /// </summary>
        public List<Devise> Devises
        {
            get { return devises; }
            set { devises = value; }
        }

        private Devise devise;

        /// <summary>
        /// Single currency
        /// </summary>
        public Devise Devise
        {
            get { return devise; }
            set { devise = value; }
        }

        /// <summary>
        /// Create all currencies
        /// </summary>
        public DevisesController()
        {
            Devises = new List<Devise>();
            Devises.Add(new Devise(1, "Dollar", 1.08));
            Devises.Add(new Devise(2, "Franc Suisse", 1.07));
            Devises.Add(new Devise(3, "Yen", 120));
        }

        /// <summary>
        /// Get all currencies.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the currencies are found</response>
        // GET: api/<DevisesController>
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<Devise> GetAll()
        {
            return Devises;
        }

        /// <summary>
        /// Get a single currency.
        /// </summary>
        /// <returns>Http get</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // GET api/<DevisesController>/5
        [HttpGet("{id}", Name = "GetDevise")]
        public ActionResult<Devise> GetById(int id)
        {
            Devise devise = devises.FirstOrDefault((d) => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            return devise;
        }

        /// <summary>
        /// Post a single currency.
        /// </summary>
        /// <returns>Http post</returns>
        /// <response code="201">When the currency is post</response>
        /// <response code="400">When the currency is not post</response>
        // POST api/<DevisesController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Devises.Add(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }

        /// <summary>
        /// Put a single currency.
        /// </summary>
        /// <returns>Http put</returns>
        /// <param name="id">The id of the currency</param>
        /// <param name="devise">The currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // PUT api/<DevisesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != devise.Id)
            {
                return BadRequest();
            }
            int index = Devises.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            Devises[index] = devise;
            return NoContent();
        }

        /// <summary>
        /// Delete a single currency.
        /// </summary>
        /// <returns>Http delete</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // DELETE api/<DevisesController>/5
        [HttpDelete("{id}")]
        public ActionResult<Devise> Delete(int id)
        {
            Devise devise = Devises.FirstOrDefault((d) => d.Id == id);
            if (devise == null)
            {
                return NoContent();
            }
            Devises.Remove(devise);
            return devise;
        }
    }
}
