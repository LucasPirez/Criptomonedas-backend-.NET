using CryptoTracker_backend.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker_backend.Test.ControllerTests.AlertControllerTest
{
    internal class MockAlerts
    {

        private readonly List<AlertResponseDTO> mockAlerts = new() { new AlertResponseDTO() {

                CoinId = "ethereum",
                DateCreate = new DateTime(),
                MaxPrice= 2000,
                MinPrice = 1000,

            },new AlertResponseDTO() {

                CoinId = "bitcoin",
                DateCreate = new DateTime(),
                MaxPrice= 40000,
                MinPrice = 1000

            } };


        public List<AlertResponseDTO> GetAlerts()
        {
            return mockAlerts;
        }
    }
}
