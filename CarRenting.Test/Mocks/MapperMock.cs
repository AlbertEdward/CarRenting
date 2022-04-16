using AutoMapper;
using Moq;

namespace CarRenting.Test.Mock
{
    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperMock = new Mock<IMapper>();

                mapperMock
                    .SetupGet(m => m.ConfigurationProvider)
                    .Returns(Moq.Mock.Of<IConfigurationProvider>());

                return mapperMock.Object;
            }
        }
    }
}
