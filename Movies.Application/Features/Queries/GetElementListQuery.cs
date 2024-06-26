﻿using MediatR;
using Movies.Domain.Entity;
using Movies.Domain.Interface;
using System.Linq.Expressions;

namespace Movies.Application.Features.Queries;

//public record GetElementListQuery<T>(Expression<Func<T, bool>>? CondLambda = null) : IRequest<List<T>>;

//public class GetElementListHandler<T> : IRequestHandler<GetElementListQuery<T>, List<T>> where T : class, IEntity
//{
//    private readonly IBase<T> _baseEntity;
//    public GetElementListHandler(IBase<T> baseEntity) => _baseEntity = baseEntity;

//    public async Task<List<T>> Handle(GetElementListQuery<T> request, CancellationToken cancellationToken)
//    {
//        return await _baseEntity.GetListAsync(request.CondLambda, cancellationToken);
//    }
//}

public record GetElementListQuery<T>(Expression<Func<T, bool>>? CondLambda = null) : IRequest<ResponseResult<List<T>>>;

public class GetElementListHandler<T>(IFactoryUow baseEntity)
    : IRequestHandler<GetElementListQuery<T>, ResponseResult<List<T>>>
    where T : class, IEntity
{
    public async Task<ResponseResult<List<T>>> Handle(GetElementListQuery<T> request, CancellationToken cancellationToken)
        => new(
            request.CondLambda != null ? await baseEntity.Repository<T>().GetListAsync(request.CondLambda, cancellationToken)
            : await baseEntity.Repository<T>().GetListAsync(cancellationToken));
}