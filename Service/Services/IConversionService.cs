namespace Service.Repositories;
using Domain.Entities;
public interface IConversionService
{
    ConversionResponse convertText(string inputText);
}
