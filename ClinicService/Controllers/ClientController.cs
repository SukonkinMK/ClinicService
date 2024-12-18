﻿using ClinicService.Models;
using ClinicService.Models.Requests;
using ClinicService.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ClinicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController :ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost("create")]
        [SwaggerOperation(OperationId = "ClientCreate")]
        public ActionResult<int> Create([FromBody] CreateClientRequest createClient)
        {
            int res = _clientRepository.Create(new Client
            {
                Document = createClient.Document,
                SurName = createClient.SurName,
                FirstName = createClient.FirstName,
                Patronymic = createClient.Patronymic,
                Birthday = createClient.Birthday
            });
            return Ok(res);
        }
        
        [HttpPut("update")]
        [SwaggerOperation(OperationId = "ClientUpdate")]
        public ActionResult<int> Update([FromBody] UpdateClientRequest updateClient)
        {
            int res = _clientRepository.Update(new Client
            {
                ClientId = updateClient.ClientId,
                SurName = updateClient.SurName,
                FirstName = updateClient.FirstName,
                Patronymic = updateClient.Patronymic,
                Birthday = updateClient.Birthday,
                Document = updateClient.Document
            });
            return Ok(res);
        }

        [HttpDelete("delete")]
        [SwaggerOperation(OperationId = "ClientDelete")]
        public ActionResult<int> Delete([FromQuery] int clientId)
        {
            int res = _clientRepository.Delete(clientId);
            return Ok(res);
        }

        [HttpGet("getAll")]
        [SwaggerOperation(OperationId = "ClientGetAll")]
        public ActionResult<List<Client>> GetAll()
        {
            return Ok(_clientRepository.GetAll());
        }

        [HttpGet("get/{clientId}")]
        [SwaggerOperation(OperationId = "ClientGetById")]
        public ActionResult<Client> GetById([FromRoute] int clientId)
        {
            return Ok(_clientRepository.GetById(clientId));
        }
    }
}
