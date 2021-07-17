namespace CleanApplication.Application.IntegrationTests
{
    using NUnit.Framework;
    using System.Threading.Tasks;
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
