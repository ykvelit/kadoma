using Microsoft.EntityFrameworkCore;

namespace Data.UnitTest;
public class ApplicationDbContextTests
{
    [Fact]
    public void Ctor_ShouldCreateDbContext()
    {
        // arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        // act
        var act = () => new ApplicationDbContext(options);

        // assert
        act.Should().NotThrow();
    }
}
