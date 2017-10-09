using AutoMapper;

namespace Flash.BitMarket.Web.Infrastructure.AutoMapper
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
