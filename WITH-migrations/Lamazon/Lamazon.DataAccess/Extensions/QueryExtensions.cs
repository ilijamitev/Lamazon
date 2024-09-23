using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Lamazon.DataAccess.Extensions
{
    public static class QueryExtensions
    {
        private static readonly MethodInfo OrderByMethod = typeof(Queryable).GetMethods().Single(method => method.Name == "OrderBy" && method.GetParameters().Length == 2);
        private static readonly MethodInfo OrderByDescendingMethod = typeof(Queryable).GetMethods().Single(method => method.Name == "OrderByDescending" && method.GetParameters().Length == 2);

        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T));

            Expression orderByProperty = null;

            var propertiesList = propertyName.Split(".", StringSplitOptions.RemoveEmptyEntries);
            if (propertiesList.Length > 1)
            {
                orderByProperty = Expression.PropertyOrField(parameterExpression, propertiesList[0]);
                for (int i = 1; i < propertiesList.Length; i++)
                {
                    orderByProperty = Expression.PropertyOrField(orderByProperty, propertiesList[i]);
                }
            }
            else
            {
                orderByProperty = Expression.Property(parameterExpression, propertyName);
            }

            LambdaExpression lambda = Expression.Lambda(orderByProperty, parameterExpression);
            MethodInfo genericMethod = OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }

        public static IQueryable<T> OrderByPropertyDescending<T>(this IQueryable<T> source, string propertyName)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T));
            Expression orderByProperty = null;

            var propertiesList = propertyName.Split(".", StringSplitOptions.RemoveEmptyEntries);
            if (propertiesList.Length > 1)
            {
                orderByProperty = Expression.PropertyOrField(parameterExpression, propertiesList[0]);
                for (int i = 1; i < propertiesList.Length; i++)
                {
                    orderByProperty = Expression.PropertyOrField(orderByProperty, propertiesList[i]);
                }
            }
            else
            {
                orderByProperty = Expression.Property(parameterExpression, propertyName);
            }

            LambdaExpression lambda = Expression.Lambda(orderByProperty, parameterExpression);
            MethodInfo genericMethod = OrderByDescendingMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }
    }
}
