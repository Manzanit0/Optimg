using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

namespace Optimg.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TetGetMethod()
        {
            var functions = new Functions();
            var request = new APIGatewayProxyRequest();
            var context = new TestLambdaContext();
            
            var response = functions.Get(request, context);
            
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Hello AWS Serverless", response.Body);
        }
    }
}
