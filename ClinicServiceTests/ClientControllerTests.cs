using ClinicService.Controllers;
using ClinicService.Models;
using ClinicService.Models.Requests;
using ClinicService.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicServiceTests
{
    public class ClientControllerTests
    {
        private ClientController _clientController;
        private Mock<IClientRepository> _clientRepository;
        public ClientControllerTests()
        {
            _clientRepository = new Mock<IClientRepository>();
            _clientController = new ClientController(_clientRepository.Object);
        }

        [Fact]
        public void GetAllClientsTest()
        {
            List<Client> clients = new List<Client>();
            clients.Add(new Client());
            clients.Add(new Client());
            clients.Add(new Client());
            _clientRepository.Setup(repo => repo.GetAll()).Returns(clients);

            var operationResut = _clientController.GetAll();

            Assert.IsType<OkObjectResult>(operationResut.Result);
            Assert.IsAssignableFrom<List<Client>>(((OkObjectResult)operationResut.Result).Value);
            _clientRepository.Verify(repo => repo.GetAll(), Times.AtLeastOnce());
        }

        public static readonly object[][] CorrectCreateClientData =
        {
            new object[] { new DateTime(1985,5,20), "123 1234", "Иванов", "Иван", "Иванович" },
            new object[] { new DateTime(1987,5,20), "123 1234", "Иванов", "Иван", "Иванович" },
            new object[] { new DateTime(1979,5,20), "123 1234", "Иванов", "Иван", "Иванович" }
        };

        [Theory]
        [MemberData(nameof(CorrectCreateClientData))]
        public void CreateClientTest(DateTime birthday, string document, string surname, string firstname, string patronymic)
        {
            _clientRepository.Setup(repo => repo.Create(It.IsNotNull<Client>())).Returns(1).Verifiable();

            var operationResult = _clientController.Create(new CreateClientRequest
            {
                Birthday = birthday,
                Document = document,
                FirstName = firstname,
                Patronymic = patronymic,
                SurName = surname
            });

            Assert.IsType<OkObjectResult>(operationResult.Result);
            Assert.IsAssignableFrom<int>(((OkObjectResult)operationResult.Result).Value);
            _clientRepository.Verify(repo => repo.Create(It.IsNotNull<Client>()), Times.AtLeastOnce());
        }
    }
}
