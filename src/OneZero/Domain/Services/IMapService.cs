using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Domain.Services
{

    public interface IMapService
    {
        TDestination ConvertToModel<TSource, TDestination>(TSource dataDto) where TSource : DataDto where TDestination : class,IEntity<Guid>;
        TDestination ConvertToDataDto< TSource, TDestination>(TSource entity) where TDestination : DataDto where TSource : class, IEntity<Guid>;
        //TDestination ConvertToModel<TSource, TDestination>(TSource dataDto);
        //TDestination ConvertToDataDto<TSource, TDestination>(TSource entity);
    }
}
