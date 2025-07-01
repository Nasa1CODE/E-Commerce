using System;
using System.Linq.Expressions;

namespace Core.Interface;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    bool IsDistinct { get; }

}

public interface ISpecification<T, IResult> : ISpecification<T>
{
    Expression<Func<T, IResult>>? Select { get; }
}

