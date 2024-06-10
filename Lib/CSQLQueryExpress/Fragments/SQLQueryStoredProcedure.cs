using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CSQLQueryExpress.Extensions;
using CSQLQueryExpress.Schema;

namespace CSQLQueryExpress.Fragments
{
    public class SQLQueryStoredProcedure : ISQLQuery, ISQLQueryFragment
    {
        private readonly IList<ISQLQueryFragment> _fragments;
        private readonly ISQLStoredProcedure _procedure;

        internal SQLQueryStoredProcedure(IList<ISQLQueryFragment> fragments, ISQLStoredProcedure procedure)
        {
            _fragments = fragments;
            _procedure = procedure;

            _fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType => SQLQueryFragmentType.StoredProcedure;

        private bool _addResultParameter;
        public SQLQueryStoredProcedure AddResultParameter()
        {
            _addResultParameter = true;
            return this;
        }

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer(FragmentType)))
            {
                yield return fragment;
            }
        }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            var procedureType = _procedure.GetType();

            var procedureParameters = procedureType.GetReadableProperties()
                .Select(p => new Tuple<ParameterAttribute, object>(p.GetCustomAttribute<ParameterAttribute>(), p.GetValue(_procedure)));

            var parametersNames = new List<string>();

            foreach (var parameter in procedureParameters)
            {
                var parameterName = expressionTranslator.MakeStoredProcedureParameter(
                    parameter.Item1.Name,
                    parameter.Item2,
                    parameter.Item1.Output
                        ? SQLQueryParameterValueDirection.Output
                        : SQLQueryParameterValueDirection.Input
                    );

                parametersNames.Add(parameter.Item1.Output
                    ? $"{parameterName} OUTPUT"
                    : parameterName);
            }

            var procedureAttribute = procedureType.GetCustomAttribute<StoredProcedureAttribute>();

            var databaseAttribute = procedureType.GetCustomAttribute<DatabaseAttribute>();

            string executeCommand;

            if (_addResultParameter)
            {
                var resultParameter = expressionTranslator.MakeStoredProcedureParameter("RC", null, SQLQueryParameterValueDirection.Result);

                if (databaseAttribute != null)
                {
                    executeCommand = $"DECLARE {resultParameter} AS INT {Environment.NewLine}EXECUTE {resultParameter} = [{databaseAttribute.Name}].[{procedureAttribute.Schema}].[{procedureAttribute.Name}] {string.Join(", ", parametersNames)}";
                }
                else
                {
                    executeCommand = $"DECLARE {resultParameter} AS INT {Environment.NewLine}EXECUTE {resultParameter} = [{procedureAttribute.Schema}].[{procedureAttribute.Name}] {string.Join(", ", parametersNames)}";
                }

                parametersNames.Add(resultParameter);
            }
            else
            {
                if (databaseAttribute != null)
                {
                    executeCommand = $"EXECUTE [{databaseAttribute.Name}].[{procedureAttribute.Schema}].[{procedureAttribute.Name}] {string.Join(", ", parametersNames)}";
                }
                else
                {
                    executeCommand = $"EXECUTE [{procedureAttribute.Schema}].[{procedureAttribute.Name}] {string.Join(", ", parametersNames)}";
                }
            }

            return executeCommand;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class SQLQueryStoredProcedure<T> : SQLQueryStoredProcedure
    {
        internal SQLQueryStoredProcedure(IList<ISQLQueryFragment> fragments, ISQLStoredProcedure<T> procedure)
            : base(fragments, procedure)
        {
            
        }

        public new SQLQueryStoredProcedure<T> AddResultParameter()
        {
            base.AddResultParameter();
            return this;
        }
    }
}
