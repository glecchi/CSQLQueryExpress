using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CSQLQueryExpress.Fragments
{
    public class SQLQuerySelect : ISQLQueryFragment, ISQLQuery
    {
        protected readonly IList<ISQLQueryFragment> _fragments;
        protected readonly Expression[] _select;
        internal Expression[] Select { get { return _select; } }

        internal SQLQuerySelect(IList<ISQLQueryFragment> fragments)
            : this(fragments, null)
        {
            
        }

        internal SQLQuerySelect(IList<ISQLQueryFragment> fragments, Expression[] select)
        {
            _fragments = fragments;
            _select = select;

            _fragments.Add(this);
        }

        protected bool _useCte;
        protected bool _count;
        protected bool _distinct;
        protected int? _top;

        public SQLQueryFragmentType FragmentType { get { return !_useCte ? SQLQueryFragmentType.Select : SQLQueryFragmentType.SelectCte; } }

        public SQLQuerySelect Count()
        {
            if (_top.HasValue)
            {
                throw new NotSupportedException("Select TOP with COUNT is not supported");
            }

            _count = true;
            return this;
        }

        public SQLQuerySelect Distinct()
        {
            _distinct = true;
            return this;
        }

        public SQLQuerySelect Top(int count)
        {
            if (_count)
            {
                throw new NotSupportedException("Select TOP with COUNT is not supported");
            }

            _top = count;
            return this;
        }

        public SQLQueryInto<TI> Into<TI>() where TI : ISQLQueryEntity
        {
            if (_useCte)
            {
                throw new NotSupportedException("SELECT INTO in WITH CTE declaration is not supported");
            }

            return new SQLQueryInto<TI>(_fragments);
        }

        public SQLQueryInsert<TI> Insert<TI>() where TI : ISQLQueryEntity
        {
            return new SQLQueryInsert<TI>(_fragments, this);
        }

        //public SQLQueryForXml<TX> ForXml<TX>()
        //{
        //    return new SQLQueryForXml<TX>(_fragments);
        //}

        //public SQLQueryForXml<TX> ForXml<TX>(Expression<Func<SQLQueryForXml<TX>, object>> forXml)
        //{
        //    return new SQLQueryForXml<TX>(_fragments, forXml);
        //}

        public virtual string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            var selectBase = new StringBuilder();

            selectBase.Append($"SELECT ");

            if (_count)
            {
                if (_distinct)
                {
                    if (_select != null && _select.Length > 0)
                    {
                        selectBase.Append($"{Environment.NewLine}COUNT(DISTINCT {string.Join($", ", _select.Select(s => expressionTranslator.Translate(s)))})");
                    }
                    else
                    {
                        selectBase.Append($"COUNT(DISTINCT *)");
                    }
                }
                else
                {
                    if (_select != null && _select.Length > 0)
                    {
                        selectBase.Append($"{Environment.NewLine}COUNT({string.Join($", ", _select.Select(s => expressionTranslator.Translate(s)))})");
                    }
                    else
                    {
                        selectBase.Append($"COUNT(*)");
                    }

                }
            }
            else if (_distinct)
            {
                selectBase.Append("DISTINCT ");

                if (_top.HasValue)
                {
                    selectBase.Append($"TOP({_top}) ");
                }

                if (_select != null && _select.Length > 0)
                {
                    selectBase.Append($"{Environment.NewLine}{string.Join($", {Environment.NewLine}", _select.Select(s => expressionTranslator.Translate(s)))}");
                }
                else
                {
                    selectBase.Append($"*");
                }
            }
            else
            {
                if (_top.HasValue)
                {
                    selectBase.Append($"TOP({_top}) ");
                }

                if (_select != null && _select.Length > 0)
                {
                    selectBase.Append($"{Environment.NewLine}{string.Join($", {Environment.NewLine}", _select.Select(s => expressionTranslator.Translate(s)))}");
                }
                else
                {
                    selectBase.Append($"*");
                }
            }

            selectBase.Append(" ");

            return selectBase.ToString();
        }

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer(FragmentType)))
            {
                yield return fragment;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class SQLQuerySelect<T> : SQLQuerySelect
    {
        internal SQLQuerySelect(IList<ISQLQueryFragment> fragments, Expression[] select)
            : base(fragments, select)
        {

        }

        public new SQLQuerySelect<T> Count()
        {
            _count = true;
            return this;
        }

        public new SQLQuerySelect<T> Distinct()
        {
            _distinct = true;
            return this;
        }

        public new SQLQuerySelect<T> Top(int count)
        {
            _top = count;
            return this;
        }

        public SQLQueryInto<T> Into()
        {
            if (_useCte)
            {
                throw new NotSupportedException("SELECT INTO in CTE TABLE declaration is not supported");
            }

            if (!typeof(ISQLQueryEntity).IsAssignableFrom(typeof(T)))
            {
                throw new NotSupportedException("SELECT INTO with T not a ISQLQueryEntity is not supported");
            }

            return new SQLQueryInto<T>(_fragments);
        }

        public SQLQuerySelect<T> ToCteTable()
        {
            if (_fragments.Any(f => f.FragmentType == SQLQueryFragmentType.SelectCte))
            {
                throw new NotSupportedException("CTE TABLE declaration FROM CTE TABLE is not supported");
            }

            _useCte = true;
            return this;
        }

        public new SQLQueryInsert<TI> Insert<TI>() where TI : ISQLQueryEntity
        {
            return new SQLQueryInsert<TI>(_fragments, this);
        }

        //public SQLQueryForXml<T> ForXml()
        //{
        //    return new SQLQueryForXml<T>(_fragments);
        //}

        //public SQLQueryForXml<T> ForXml(Expression<Func<SQLQueryForXml<T>, object>> forXml)
        //{
        //    return new SQLQueryForXml<T>(_fragments, forXml);
        //}

        public override string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            var selectBase = base.Translate(expressionTranslator);

            if (FragmentType == SQLQueryFragmentType.SelectCte)
            {
                return $"{expressionTranslator.GetTableAlias(typeof(T))} AS {Environment.NewLine}( {Environment.NewLine}{selectBase} ";
            }

            return selectBase;
        }
    }
}
