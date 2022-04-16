using CarRenting.Services;
using CarRenting.Services.Statistics;
using Moq;

namespace CarRenting.Test.Mock
{
    public class StatiscticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalCars = 5,
                        TotalRents = 10,
                        TotalUsers = 15
                    });

                return statisticsServiceMock.Object;
            }
        }
        
    }
}
