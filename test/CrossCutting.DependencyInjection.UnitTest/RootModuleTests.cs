using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection.UnitTest;

public class RootModuleTests
{
    [Fact]
    public void AddRootModule_ShouldNotThrow()
    {
        // arrange
        var services = new ServiceCollection();

        var config = new Dictionary<string, string>()
        {
            {"ConnectionStrings:Redis","localhost:6379" },
            {"ConnectionStrings:SqlServer", "Server=localhost;Database=kadoma;User Id=sa;Password=yourStrong(!)Password;Encrypt=false;" }
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(config!)
            .Build();

        // act
        var act = () => services.AddRootModule(configuration);

        // assert
        act.Should().NotThrow();
    }
}