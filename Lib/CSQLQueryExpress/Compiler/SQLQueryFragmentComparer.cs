using System;
using System.Collections.Generic;

namespace CSQLQueryExpress
{
    internal class SQLQueryFragmentComparer : IComparer<ISQLQueryFragment>
    {
        private static readonly IDictionary<SQLQueryFragmentType, IDictionary<SQLQueryFragmentType, int>> Order;
        
        static SQLQueryFragmentComparer()
        {
            Order = new Dictionary<SQLQueryFragmentType, IDictionary<SQLQueryFragmentType, int>>();

            AddFragmentOrders(Order);
        }

        private readonly IDictionary<SQLQueryFragmentType, int> _fragmentOrder;

        public SQLQueryFragmentComparer(SQLQueryFragmentType callerFragment)
        {
            if (!Order.TryGetValue(callerFragment, out _fragmentOrder))
            {
                throw new InvalidOperationException($"Fragments order for {callerFragment} is not supported");
            }
        }

        public int Compare(ISQLQueryFragment x, ISQLQueryFragment y)
        {
            if (!_fragmentOrder.TryGetValue(x.FragmentType, out int xOrder) ||
                !_fragmentOrder.TryGetValue(y.FragmentType, out int yOrder))
            {
                throw new InvalidOperationException($"Fragments order beetween {x.FragmentType} and {y.FragmentType} is not supported");
            }

            return xOrder.CompareTo(yOrder);
        }

        private static void AddFragmentOrders(IDictionary<SQLQueryFragmentType, IDictionary<SQLQueryFragmentType, int>> order)
        {
            order.Add(SQLQueryFragmentType.Insert, GetInsertOrder());
            order.Add(SQLQueryFragmentType.InsertBySelect, GetDefaultOrder());
            order.Add(SQLQueryFragmentType.Delete, GetDeleteOrder());
            order.Add(SQLQueryFragmentType.Update, GetUpdateOrder());
            order.Add(SQLQueryFragmentType.Truncate, GetTruncateOrder());
            order.Add(SQLQueryFragmentType.Drop, GetDropOrder());
            order.Add(SQLQueryFragmentType.Union, GetUnionOrder());
            order.Add(SQLQueryFragmentType.Select, GetDefaultOrder());
            order.Add(SQLQueryFragmentType.SelectCte, GetDefaultOrder());
            order.Add(SQLQueryFragmentType.Into, GetDefaultOrder());
            order.Add(SQLQueryFragmentType.FromUnion, GetDefaultOrder());
            order.Add(SQLQueryFragmentType.ForXml, GetDefaultOrder());
            order.Add(SQLQueryFragmentType.StoredProcedure, GetProcedureOrder());
            order.Add(SQLQueryFragmentType.Batch, GetBatchOrder());
            order.Add(SQLQueryFragmentType.MultipleResultSets, GetMultipleResultSetsOrder());
        }

        static IDictionary<SQLQueryFragmentType, int> GetInsertOrder()
        {
            var fragmentsOrder = new Dictionary<SQLQueryFragmentType, int>
            {
                { SQLQueryFragmentType.Insert, 0 },
                { SQLQueryFragmentType.InsertBySelect, 0 },
                { SQLQueryFragmentType.Output, 2 },
                { SQLQueryFragmentType.InsertValues, 3 },
                { SQLQueryFragmentType.Select, 3 },
                { SQLQueryFragmentType.SelectCte, 3 },
                { SQLQueryFragmentType.From, 4 },
                { SQLQueryFragmentType.FromBySelect, 4 }
            };

            return fragmentsOrder;
        }

        static IDictionary<SQLQueryFragmentType, int> GetDeleteOrder()
        {
            var fragmentsOrder = new Dictionary<SQLQueryFragmentType, int>
            {
                { SQLQueryFragmentType.Delete, 0 },
                { SQLQueryFragmentType.From, 1 },
                { SQLQueryFragmentType.Output, 2 },
                { SQLQueryFragmentType.Select, 3 },
                { SQLQueryFragmentType.SelectCte, 3 },
                { SQLQueryFragmentType.FromBySelect, 4 },
                { SQLQueryFragmentType.Where, 5 }
            };

            return fragmentsOrder;
        }

        static IDictionary<SQLQueryFragmentType, int> GetUpdateOrder()
        {
            var fragmentsOrder = new Dictionary<SQLQueryFragmentType, int>
            {
                { SQLQueryFragmentType.Update, 0 },
                { SQLQueryFragmentType.Output, 1 },
                { SQLQueryFragmentType.Select, 2 },
                { SQLQueryFragmentType.SelectCte, 2 },
                { SQLQueryFragmentType.From, 3 },
                { SQLQueryFragmentType.FromBySelect, 3 },
                { SQLQueryFragmentType.Join, 4 },
                { SQLQueryFragmentType.JoinBySelect, 4 },
                { SQLQueryFragmentType.Where, 5 },
                { SQLQueryFragmentType.Group, 6 },
                { SQLQueryFragmentType.GroupHaving, 7 }
            };

            return fragmentsOrder;
        }

        static IDictionary<SQLQueryFragmentType, int> GetTruncateOrder()
        {
            var fragmentsOrder = new Dictionary<SQLQueryFragmentType, int>
            {
                { SQLQueryFragmentType.Truncate, 0 }
            };

            return fragmentsOrder;
        }

        static IDictionary<SQLQueryFragmentType, int> GetDropOrder()
        {
            var fragmentsOrder = new Dictionary<SQLQueryFragmentType, int>
            {
                { SQLQueryFragmentType.Drop, 0 }
            };

            return fragmentsOrder;
        }

        static IDictionary<SQLQueryFragmentType, int> GetUnionOrder()
        {
            var fragmentsOrder = new Dictionary<SQLQueryFragmentType, int>
            {
                { SQLQueryFragmentType.Union, 0 }
            };

            return fragmentsOrder;
        }

        static IDictionary<SQLQueryFragmentType, int> GetProcedureOrder()
        {
            var fragmentsOrder = new Dictionary<SQLQueryFragmentType, int>
            {
                { SQLQueryFragmentType.StoredProcedure, 0 }
            };

            return fragmentsOrder;
        }

        static IDictionary<SQLQueryFragmentType, int> GetBatchOrder()
        {
            var fragmentsOrder = new Dictionary<SQLQueryFragmentType, int>
            {
                { SQLQueryFragmentType.Batch, 0 }
            };

            return fragmentsOrder;
        }

        static IDictionary<SQLQueryFragmentType, int> GetMultipleResultSetsOrder()
        {
            var fragmentsOrder = new Dictionary<SQLQueryFragmentType, int>
            {
                { SQLQueryFragmentType.MultipleResultSets, 0 }
            };

            return fragmentsOrder;
        }

        static IDictionary<SQLQueryFragmentType, int> GetDefaultOrder()
        {            
            var fragmentOrder = new Dictionary<SQLQueryFragmentType, int>
            {
                { SQLQueryFragmentType.Insert, 0 },
                { SQLQueryFragmentType.InsertBySelect, 0 },
                { SQLQueryFragmentType.InsertValues, 1 },
                { SQLQueryFragmentType.Delete, 0 },
                { SQLQueryFragmentType.Update, 0 },
                { SQLQueryFragmentType.Truncate, 0 },
                { SQLQueryFragmentType.Drop, 0 },
                { SQLQueryFragmentType.Output, 1 },
                { SQLQueryFragmentType.Select, 2 },
                { SQLQueryFragmentType.SelectCte, 2 },
                { SQLQueryFragmentType.Into, 3 },
                { SQLQueryFragmentType.From, 4 },
                { SQLQueryFragmentType.FromBySelect, 4 },
                { SQLQueryFragmentType.FromUnion, 4 },
                { SQLQueryFragmentType.Join, 5 },
                { SQLQueryFragmentType.JoinBySelect, 5 },
                { SQLQueryFragmentType.Where, 6 },
                { SQLQueryFragmentType.Group, 7 },
                { SQLQueryFragmentType.GroupHaving, 8 },
                { SQLQueryFragmentType.Union, 9 },
                { SQLQueryFragmentType.Order, 10 },
                { SQLQueryFragmentType.Page, 11 },
                { SQLQueryFragmentType.ForXml, 12 }
            };

            return fragmentOrder;
        }
    }
}
