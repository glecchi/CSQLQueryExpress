using System;
using System.Collections.Generic;
using System.Text;

namespace SQLQueryBuilder
{
    internal class SQLQueryFragmentComparer : IComparer<ISQLQueryFragment>
    {
        private readonly IDictionary<SQLQueryFragmentType, int> _order;
        public SQLQueryFragmentComparer()
        {
            _order = new Dictionary<SQLQueryFragmentType, int>();

            _order.Add(SQLQueryFragmentType.Insert, 0);
            _order.Add(SQLQueryFragmentType.InsertValues, 1);

            _order.Add(SQLQueryFragmentType.Delete, 0);

            _order.Add(SQLQueryFragmentType.Update, 0);

            _order.Add(SQLQueryFragmentType.Truncate, 0);

            _order.Add(SQLQueryFragmentType.Drop, 0);

            _order.Add(SQLQueryFragmentType.Output, 1);

            _order.Add(SQLQueryFragmentType.Select, 2);
            _order.Add(SQLQueryFragmentType.Into, 3);
            
            _order.Add(SQLQueryFragmentType.From, 4);
            _order.Add(SQLQueryFragmentType.FromBySelect, 4);
            _order.Add(SQLQueryFragmentType.Join, 5);
            _order.Add(SQLQueryFragmentType.JoinBySelect, 5);
            _order.Add(SQLQueryFragmentType.Where, 6);
            _order.Add(SQLQueryFragmentType.Group, 7);
            _order.Add(SQLQueryFragmentType.GroupHaving, 8);

            _order.Add(SQLQueryFragmentType.Union, 9);

            _order.Add(SQLQueryFragmentType.Order, 10);
            _order.Add(SQLQueryFragmentType.Page, 11);

            _order.Add(SQLQueryFragmentType.ForXml, 12);
        }

        public int Compare(ISQLQueryFragment x, ISQLQueryFragment y)
        {
            return _order[x.FragmentType].CompareTo(_order[y.FragmentType]);
        }
    }
}
