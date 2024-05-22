using System;
using System.Linq.Expressions;

namespace SQLQueryBuilder.Statements
{
    public static class Case
    {
        public static ICaseWhen When(Expression<Func<bool>> when)
        {
            return default;
        }
    }

    public interface ICase<T>
    {
        ICaseWhen<T> When(Expression<Func<T>> when);

    }

    public interface ICaseWhen<T>
    {
        ICaseThen<T, TT> Then<TT>(Expression<Func<TT>> expression);
    }

    public interface ICaseWhen<TW, TT>
    {
        ICaseThen<TW, TT> Then(Expression<Func<TT>> expression);
    }

    public interface ICaseThen<TW, TT>
    {
        ICaseWhen<TW, TT> When(Expression<Func<TW>> when);

        TT Else(Expression<Func<TT>> then);
    }

    public interface ICaseWhen
    {
        ICaseThen<T> Then<T>(Expression<Func<T>> expression);
    }

    public interface ICaseThen<T>
    {
        ICaseWhen When(Expression<Func<bool>> when);

        T Else(Expression<Func<T>> then);
    }

    public interface ICaseElse<T>
    {

    }
 }
