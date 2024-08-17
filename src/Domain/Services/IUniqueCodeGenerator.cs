namespace Domain.Services;

public interface IUniqueCodeGenerator
{
    string Generate(int length);
}