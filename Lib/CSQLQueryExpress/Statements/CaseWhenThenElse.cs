using System;
using System.Linq.Expressions;

namespace CSQLQueryExpress
{
    public static class Case
    {
        public static ICaseWhen When(bool when)
        {
            return default;
        }
    }

    public interface ICase<T>
    {
        ICaseWhen<T> When(T when);
    }

    public interface ICaseWhen<T>
    {
        ICaseThen<T, TT> Then<TT>(TT then);
    }

    public interface ICaseWhen<TW, TT>
    {
        ICaseThen<TW, TT> Then(TT then);
    }

    public interface ICaseThen<TW, TT>
    {
        ICaseWhen<TW, TT> When(TW when);

        TT Else(TT then);
    }

    public interface ICaseWhen
    {
        ICaseThen<T> Then<T>(T then);
    }

    public interface ICaseThen<T>
    {
        ICaseWhen When(bool when);

        T Else(T then);
    }

    public interface ICaseElse<T>
    {

    }
 }
