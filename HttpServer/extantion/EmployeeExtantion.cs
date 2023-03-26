using System.Linq.Expressions;
using HttpServer.models;

namespace HttpServer.extantion;

public static class EmployeeExtantion
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> employees,
        bool condition,
        Expression<Func<T, bool>> predicate) => condition ? employees.Where(predicate) : employees;
}