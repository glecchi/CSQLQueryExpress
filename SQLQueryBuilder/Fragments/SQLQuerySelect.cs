using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SQLQueryBuilder.Fragments
{
    public class SQLQuerySelect : ISQLQueryFragment, ISQLQuery
    {
        protected readonly IList<ISQLQueryFragment> _fragments;
        protected readonly Expression[] _select;
        internal Expression[] Select { get { return _select; } }

        internal SQLQuerySelect(IList<ISQLQueryFragment> fragments, Expression[] select)
        {
            _fragments = fragments;
            _select = select;

            _fragments.Add(this);
        }

        protected bool _useWithTable;
        internal bool UseWithTable { get { return _useWithTable; } }

        protected bool _count;
        protected bool _distinct;
        protected int? _top;

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Select; } }

        public SQLQuerySelect Count()
        {
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
            _top = count;
            return this;
        }

        public SQLQueryInto<TI> Into<TI>() where TI : ISQLQueryEntity
        {
            if (_useWithTable)
            {
                throw new NotSupportedException("SELECT INTO in WITH TABLE declaration is not supported");
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

            selectBase.Append(_top.HasValue ? $"SELECT TOP({_top}){Environment.NewLine}" : $"SELECT{Environment.NewLine}");

            if (_count)
            {
                if (_distinct)
                {
                    if (_select != null && _select.Length > 0)
                    {
                        selectBase.Append($" COUNT(DISTINCT {string.Join($", ", _select.Select(s => expressionTranslator.Translate(s)))})");
                    }
                    else
                    {
                        selectBase.Append($" COUNT(DISTINCT *)");
                    }
                }
                else
                {
                    if (_select != null && _select.Length > 0)
                    {
                        selectBase.Append($" COUNT({string.Join($", ", _select.Select(s => expressionTranslator.Translate(s)))})");
                    }
                    else
                    {
                        selectBase.Append($" COUNT(*)");
                    }

                }
            }
            else if (_distinct)
            {
                if (_select != null && _select.Length > 0)
                {
                    selectBase.Append($" DISTINCT {string.Join($",{Environment.NewLine} ", _select.Select(s => expressionTranslator.Translate(s)))}");
                }
                else
                {
                    selectBase.Append($" DISTINCT *");
                }
            }
            else
            {
                if (_select != null && _select.Length > 0)
                {
                    selectBase.Append($" {string.Join($",{Environment.NewLine} ", _select.Select(s => expressionTranslator.Translate(s)))}");
                }
                else
                {
                    selectBase.Append($" *");
                }
            }

            return selectBase.ToString();
        }

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer()))
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
            if (_useWithTable)
            {
                throw new NotSupportedException("SELECT INTO in WITH TABLE declaration is not supported");
            }

            if (!typeof(ISQLQueryEntity).IsAssignableFrom(typeof(T)))
            {
                throw new NotSupportedException("SELECT INTO with T not a ISQLQueryEntity is not supported");
            }

            return new SQLQueryInto<T>(_fragments);
        }

        public SQLQuerySelect<T> ToWithTable()
        {
            if (_fragments.Any(f => f is SQLQuerySelect sel && sel.UseWithTable))
            {
                throw new NotSupportedException("WITH TABLE declaration FROM WITH TABLE is not supported");
            }

            _useWithTable = true;
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

            if (UseWithTable)
            {
                return $"WITH {expressionTranslator.GetTableAlias(typeof(T))} AS {Environment.NewLine}( {Environment.NewLine}{selectBase} ";
            }

            return selectBase;
        }
    }
}
