using DevTrackR.API.Entities;
using DevTrackR.API.Models;
using DevTrackR.API.Repository;
using Microsoft.AspNetCore.Mvc;


namespace DevTrackR.API.Controllers
{
    [ApiController]
    [Route("api/packages")]
    public class PackagesController : ControllerBase
    {

        private readonly IPackageRepository _repository;

        public PackagesController(IPackageRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var packages = _repository.GetAll();
            

            return Ok(packages);
        }

        [HttpGet("{code}")]
        public IActionResult GetByCode(string code)
        {
            var package = _repository.GetByCode(code);

            if (package == null)
                return NotFound();

            return Ok(package);
        }


        /// <summary>
        /// Cadastro de um pacote
        /// </summary>
        /// <remark>
        /// {
        /// "title": "Product package title",
        /// "weight": 1.0,
        /// "senderName": "Person Name",
        /// "senderEmail": "personEmail@gmail.com"
        /// }
        /// </remark>
        /// <param name="model"> Dados do pacote</param>        
        /// <returns>Objeto recém criado.</returns>
        /// <response code="201">Cadastro realizado com sucesso.</response>
        /// <response code="400">Dados inválidos!</response>

        [HttpPost]
        public IActionResult Post(AddPackageInputModel model)
        {
            if(model.Title.Length < 10 )
            {
                return BadRequest("Title lenght must be at least 10 chacarters");
            }

            var package = new Package(model.Title, model.Weight);
            
            _repository.Add(package);

            return CreatedAtAction("GetByCode", new { code = package.Code}, package);
        }

      

        [HttpPost("{code}/updates")]
        public IActionResult PostUpdate(string code, AddPackageUpdateInputModel model)
        {
            var package = _repository.GetByCode(code);

            if (package == null)
                return NotFound();

            
            package.AddUpdate(model.Status, model.Delivered);
           _repository.Update(package);

            return NoContent();
        }
    }
}
