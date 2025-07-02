using System;
using System.Linq.Expressions;

namespace Core.Interface;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    bool IsDistinct { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnbled { get; }
    IQueryable<T> ApplyCriteria (IQueryable<T> query);
}

public interface ISpecification<T, IResult> : ISpecification<T>
{
    Expression<Func<T, IResult>>? Select { get; }
}

