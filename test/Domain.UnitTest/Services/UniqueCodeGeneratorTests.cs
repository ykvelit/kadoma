using Domain.Services;

namespace Domain.UnitTest.Services;
public class UniqueCodeGeneratorTests
{
    private readonly UniqueCodeGenerator _subject;

    public UniqueCodeGeneratorTests()
    {
        _subject = new UniqueCodeGenerator();
    }

    [Fact]
    public void Generate_ShouldReturnWithLength()
    {
        // arrange
        var length = 10;

        // act
        var code = _subject.Generate(length);

        // assert
        code.Length.Should().Be(length);
    }
}
