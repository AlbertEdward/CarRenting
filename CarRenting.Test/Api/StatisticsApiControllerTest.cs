using CarRenting.Controllers.Api;
using CarRenting.Test.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarRenting.Test.Api
{
    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            var statisticsController = new StatisticsApiController(StatiscticsServiceMock.Instance);

            var result = statisticsController.GetStatistics();

            Assert.NotNull(result);
            Assert.Equal(5, result.TotalCars);
            Assert.Equal(10, result.TotalRents);
            Assert.Equal(15, result.TotalUsers);
        }
    }
}
