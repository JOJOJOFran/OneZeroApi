using OneZero.Common.Dtos;
using OneZero.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Domain
{

    public interface IMapService
    {

        TDestination ConvertToModel<TSource, TDestination>(TSource dataDto) where TSource : DataDto where TDestination : IEntity<Guid>;
        TDestination ConvertToDataDto<TSource, TDestination>(TSource entity) where TDestination : DataDto where TSource : IEntity<Guid>;
    }
}
