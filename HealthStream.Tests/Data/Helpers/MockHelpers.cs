using System.Collections.Generic;
using System.Data;
using Moq;

namespace HealthStream.Tests.Data.Helpers
{
    static class MockHelpers
    {
        public static Mock<IDbCommand> CreateMockDbCommandForQuery<T>(List<T> objectsToReturn )
        {
            var commandMock = new Mock<IDbCommand>();
            commandMock.Setup(m => m.CreateParameter()).Returns(new Mock<IDbDataParameter>().Object);
            commandMock.Setup(m => m.Parameters).Returns(MockHelpers.CreateMockParameterCollection().Object);
            commandMock.Setup(m => m.ExecuteReader()).Returns(MockHelpers.CreateMockDataReader<T>(objectsToReturn).Object);


            return commandMock;
        }

        public static Mock<IDataParameterCollection> CreateMockParameterCollection()
        {
            var list = new List<IDbDataParameter>();
            var parameterCollection = new Mock<IDataParameterCollection>();
            parameterCollection
                .Setup(c => c.Add(It.IsAny<IDbDataParameter>()))
                .Returns((IDbDataParameter p) =>
                {
                    list.Add(p);
                    return list.Count - 1;
                });
            parameterCollection.Setup(c => c[It.IsAny<int>()]).Returns((int i) => list[i]);
            parameterCollection.Setup(c => c.Count).Returns(() => list.Count);

            return parameterCollection;
        }

        public static Mock<IDataReader> CreateMockDataReader<T>(List<T> objectsToReturn )
        {
            var mockDataReader = new Mock<IDataReader>();
            var count = 0;

            mockDataReader.Setup(m => m.Read())
                .Returns(() => count < objectsToReturn.Count)
                .Callback(() => count++);

            var properties = typeof (T).GetProperties();
            var index = 0;
            foreach (var pi in properties)
            {
                var index1 = index;
                mockDataReader.Setup(r => r.GetFieldType(index1)).Returns(pi.PropertyType);
                mockDataReader.Setup(r => r.GetName(index1)).Returns(pi.Name);
                mockDataReader.Setup(r => r[index1])
                    .Returns(typeof(T)
                        .GetProperty(pi.Name)
                        .GetValue(objectsToReturn[count], null)
                    );

                index++;
            }

            mockDataReader.Setup(r => r.FieldCount).Returns(properties.Length);
            return mockDataReader;
        }
    }
}
