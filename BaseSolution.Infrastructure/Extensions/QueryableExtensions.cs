using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static BaseSolution.Application.ValueObjects.Common.QueryConstant;

namespace BaseSolution.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginationResponse<TTargetEntity>> PaginateAsync<TSourceEntity, TTargetEntity>(
        this IQueryable<TSourceEntity> queryable, PaginationRequest request, IMapper mapper,
        CancellationToken cancellationToken) where TSourceEntity : ICreatedBase
    {
        // Force to sort by CreateTime asc 
        IQueryable<TSourceEntity> finalQuery = queryable.OrderBy(x => x.CreatedTime);

        // Handle search and order
        if (request.SortByFields is { Count: > 0 })
        {
            finalQuery = finalQuery.OrderByFields(request.SortByFields);
        }

        if (request.SearchByFields is { Count: > 0 })
            foreach (var searchByField in request.SearchByFields)
            {
                finalQuery = finalQuery.SearchByContain(searchByField);
            }

        // Hit to the db to get data back to client side
        var result = await finalQuery
            .ProjectTo<TTargetEntity>(mapper.ConfigurationProvider)
        .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize + 1)
            .ToListAsync(cancellationToken);

        bool hasNext = result.Count == request.PageSize + 1;

        return new PaginationResponse<TTargetEntity>()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            HasNext = hasNext,
            Data = result.Take(request.PageSize).ToList()
        };
    }


    /// <summary>
    ///     Allow to sort by multiple fiels
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fieldsToSort"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IQueryable<T> OrderByFields<T>(this IQueryable<T> source, IEnumerable<SortModel> fieldsToSort)
    {
        var expression = source.Expression;
        var count = 0;
        foreach (var item in fieldsToSort)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var memberSelector = Expression.PropertyOrField(parameter, item.ColName);
            var method = string.Equals(item.SortDirection, "desc", StringComparison.OrdinalIgnoreCase)
                ? count == 0 ? "OrderByDescending" : "ThenByDescending"
                : count == 0
                    ? "OrderBy"
                    : "ThenBy";
            expression = Expression.Call(typeof(Queryable), method,
                new[] { source.ElementType, memberSelector.Type },
                expression, Expression.Quote(Expression.Lambda(memberSelector, parameter)));
            count++;
        }

        return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
    }

    /// <summary>
    ///     Search by contains and string field by specific field name and its value
    /// </summary>
    /// <param name="source"></param>
    /// <param name="searchModel"></param>
    /// <typeparam name="T">Source Type</typeparam>
    /// <returns></returns>
    public static IQueryable<T> SearchByContain<T>(this IQueryable<T> source, SearchModel searchModel)
    {
        // Return source if value is null
        if (searchModel.SearchValue == null)
            return source;
        var parameter = Expression.Parameter(typeof(T), "item");
        var memberSelector = searchModel.SearchFieldName.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);
        var memberType = memberSelector.Type;
        var value = searchModel.SearchValue;
        // Cast value to type of Member
        if (value != null && value.GetType() != memberType)
            value = (string)Convert.ChangeType(value, memberType);

        if (searchModel.MatchType == MatchTypes.Contain)
        {
            // Get the method Contains of string type
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            if (containsMethod == null)
                return source;
            // Convert value into expression contains
            var constantExpressionValue = Expression.Constant(value, typeof(string));
            // To lower case both sides
            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            if (toLowerMethod == null)
                return source;
            var toLowerLeftSideExp = Expression.Call(memberSelector, toLowerMethod);
            var toLowerRightSideExp = Expression.Call(constantExpressionValue, toLowerMethod);

            //
            // Add left side and right side
            var expression = Expression.Call(toLowerLeftSideExp, containsMethod, toLowerRightSideExp);
            // Build the predicate
            var predicate = Expression.Lambda<Func<T, bool>>(expression, parameter);
            // Add to the where method
            return source.Where(predicate);

        }

        if (searchModel.MatchType == MatchTypes.Equal)
        {
            // Get the method Equals of object type
            var equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });
            if (equalsMethod == null)
                return source;

            // Convert value into expression
            var constantExpressionValue = Expression.Constant(value, typeof(string));

            // Build the expression
            var expression = Expression.Call(memberSelector, equalsMethod, constantExpressionValue);
            var predicate = Expression.Lambda<Func<T, bool>>(expression, parameter);

            // Add to the where method
            return source.Where(predicate);
        }

        return source;
    }
}