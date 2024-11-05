using ClinicService.Models;
using ClinicService.Models.Requests;
using ClinicService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetRepository _petRepository;

        public PetController(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreatePetRequest request)
        {
            int res = _petRepository.Create(new Pet
            {
                ClientId = request.ClientId,
                Name = request.Name,
                Birthday = request.Birthday
            });
            return Ok(res);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdatePetRequest request)
        {
            int res = _petRepository.Update(new Pet
            {
                ClientId = request.ClientId,
                Name = request.Name,
                PetId = request.PetId,
                Birthday = request.Birthday
            });
            return Ok(res);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] int petId)
        {
            int res =_petRepository.Delete(petId);
            return Ok(res);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return Ok(_petRepository.GetAll());
        }

        [HttpGet("get/{petId}")]
        public IActionResult GetById([FromRoute] int petId)
        {
            return Ok(_petRepository.GetById(petId));
        }
    }
}
