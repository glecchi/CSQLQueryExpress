using CSQLQueryExpress.Fragments;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CSQLQueryExpress
{
    internal class SQLQueryTranslator : ExpressionVisitor, ISQLQueryTranslator
    {
        private readonly ISQLQueryParametersBuilder _parametersBuilder;
        private readonly ISQLQueryTableNameResolver _aliasBuilder;
        private readonly StringBuilder _queryBuilder = new StringBuilder();
        private SQLQueryFragmentType _fragmentType;

        public SQLQueryTranslator(
            ISQLQueryParametersBuilder parametersBuilder, 
            ISQLQueryTableNameResolver aliasBuilder)
        {
            _parametersBuilder = parametersBuilder;
            _aliasBuilder = aliasBuilder;
        }

        public string Translate(Expression expression, SQLQueryFragmentType fragmentType)
        {
            _fragmentType = fragmentType;

            _queryBuilder.Clear();

            base.Visit(expression);
            return _queryBuilder.ToString();
        }

        public string MakeParameter(object value)
        {
            return _parametersBuilder.AddParameter(value);
        }

        public string MakeStoredProcedureParameter(string name, object value, SQLQueryParameterDirection direction)
        {
            return _parametersBuilder.AddStoredProcedureParameter(name, value, direction);
        }

        public string GetTableAlias(Type tableType)
        {
            var tableName = _aliasBuilder.ResolveTableName(tableType);
            return tableName.TableAlias;
        }

        public string GetTableName(Type tableType)
        {
            var tableName = _aliasBuilder.ResolveTableName(tableType);
            return tableName.TableName;
        }

        private readonly Regex _matchColumnOnly = new Regex(@"(_t[0-9]*\.*)");
        
        public string GetColumnsWithoutTableAlias(string column)
        {
            var tableAlias = _matchColumnOnly.Match(column).Groups[0].Value;
            return column.Replace(tableAlias, string.Empty);
        }

        private bool _memberExpressionFromBinaryExpression;
        
        protected override Expression VisitBinary(BinaryExpression node)
        {
            _queryBuilder.Append("(");

            _memberExpressionFromBinaryExpression = node.Left.NodeType == ExpressionType.MemberAccess && 
                node.NodeType != ExpressionType.And && node.NodeType != ExpressionType.AndAlso && 
                node.NodeType != ExpressionType.Or && node.NodeType != ExpressionType.OrElse;

            this.Visit(node.Left);

            _memberExpressionFromBinaryExpression = false;

            switch (node.NodeType)
            {
                case ExpressionType.Add:
                    _queryBuilder.Append(" + ");
                    break;

                case ExpressionType.AddAssign:
                    _queryBuilder.Append(" += ");
                    break;

                case ExpressionType.Subtract:
                    _queryBuilder.Append(" - ");
                    break;

                case ExpressionType.SubtractAssign:
                    _queryBuilder.Append(" -= ");
                    break;

                case ExpressionType.And:
                    _queryBuilder.Append(" AND ");
                    break;

                case ExpressionType.AndAlso:
                    _queryBuilder.Append(" AND ");
                    break;

                case ExpressionType.Or:
                    _queryBuilder.Append(" OR ");
                    break;

                case ExpressionType.OrElse:
                    _queryBuilder.Append(" OR ");
                    break;

                case ExpressionType.Equal:
                    if (IsNullConstant(node.Right))
                    {
                        _queryBuilder.Append(" IS ");
                    }
                    else
                    {
                        _queryBuilder.Append(" = ");
                    }
                    break;

                case ExpressionType.NotEqual:
                    if (IsNullConstant(node.Right))
                    {
                        _queryBuilder.Append(" IS NOT ");
                    }
                    else
                    {
                        _queryBuilder.Append(" <> ");
                    }
                    break;

                case ExpressionType.LessThan:
                    _queryBuilder.Append(" < ");
                    break;

                case ExpressionType.LessThanOrEqual:
                    _queryBuilder.Append(" <= ");
                    break;

                case ExpressionType.GreaterThan:
                    _queryBuilder.Append(" > ");
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    _queryBuilder.Append(" >= ");
                    break;

                case ExpressionType.Modulo:
                    _queryBuilder.Append(" % ");
                    break;
                                    
                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported.", node.NodeType));

            }

            this.Visit(node.Right);
            _queryBuilder.Append(")");

            return node;
        }

        protected bool IsNullConstant(Expression exp)
        {
            return (exp.NodeType == ExpressionType.Constant && ((ConstantExpression)exp).Value == null);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var declaringType = node.Method.DeclaringType;
            if (declaringType == typeof(string))
            {
                return VisitStringMethodCall(node);
            }
            else if (declaringType == typeof(DateTime) || declaringType == typeof(DateTimeOffset))
            {
                return VisitDateTimeMethodCall(node);
            }
            else if (declaringType == typeof(SQLQueryMathematicalExpressions))
            {
                return VisitSQLQueryMathematicalMethodCall(node);
            }
            else if (declaringType == typeof(SQLQueryConditionExtension))
            {
                return VisitQueryConditionMethodCall(node);
            }
            else if (declaringType == typeof(SQLQuerySelectionExtension))
            {
                return VisitQuerySelectionMethodCall(node);
            }
            else if (declaringType == typeof(SQLQueryConversionExtensions))
            {
                return VisitQueryConversionMethodCall(node);
            }
            else if (declaringType == typeof(SQLQueryDefinitionExtensions))
            {
                return VisitQueryDefinitionMethodCall(node);
            }
            else if (declaringType == typeof(SQLQueryPaginationExtensions))
            {
                return VisitQueryPaginationMethodCall(node);
            }
            else if (declaringType == typeof(SQLQueryOperationExtensions))
            {
                return VisitQueryOperationMethodCall(node);
            }
            else if (declaringType == typeof(SQLQueryAssignmentExtensions))
            {
                return VisitQueryAssignmentMethodCall(node);
            }
            else if (declaringType == typeof(Sys))
            {
                return VisitSysMethodCall(node);
            }
            else if (declaringType == typeof(Count))
            {
                return VisitCountMethodCall(node);
            }
            else if (declaringType == typeof(Row) || declaringType == typeof(IRowNumber))
            {
                return VisitRowMethodCall(node);
            }
            else if (declaringType == typeof(Case) ||
                declaringType == typeof(ICaseWhen) ||
                (declaringType.IsGenericType && 
                 (declaringType.GetGenericTypeDefinition() == typeof(ICase<>) ||
                  declaringType.GetGenericTypeDefinition() == typeof(ICaseThen<>) ||
                  declaringType.GetGenericTypeDefinition() == typeof(ICaseThen<,>) ||
                  declaringType.GetGenericTypeDefinition() == typeof(ICaseWhen<>) ||
                  declaringType.GetGenericTypeDefinition() == typeof(ICaseWhen<,>))))
            {
                return VisitCaseThenElseMethodCall(node);
            }
            else if (declaringType == typeof(AppLock))
            {
                return VisitAppLockMethodCall(node);
            }
            
            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression VisitSQLQueryMathematicalMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(SQLQueryMathematicalExpressions.Abs))
            {
                _queryBuilder.Append("ABS(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryMathematicalExpressions.Ceiling))
            {
                _queryBuilder.Append("CEILING(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryMathematicalExpressions.Floor))
            {
                _queryBuilder.Append("FLOOR(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryMathematicalExpressions.Round))
            {
                _queryBuilder.Append("ROUND(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryMathematicalExpressions.Sqrt))
            {
                _queryBuilder.Append("SQRT(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression VisitSysMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(Sys.DateFromParts) ||
                node.Method.Name == nameof(Sys.DateTimeFromParts) ||
                node.Method.Name == nameof(Sys.DateTime2FromParts) ||
                node.Method.Name == nameof(Sys.DateTimeOffsetFromParts) ||
                node.Method.Name == nameof(Sys.TimeFromParts))
            {
                _queryBuilder.Append(node.Method.Name.ToUpper());
                _queryBuilder.Append("(");

                for (int idx = 0; idx < node.Arguments.Count; idx++)
                {
                    if (idx > 0)
                    {
                        _queryBuilder.Append(", ");
                    }

                    if ((node.Method.Name == nameof(Sys.DateTimeOffsetFromParts) && idx == 9) ||
                        (node.Method.Name == nameof(Sys.DateTime2FromParts) && idx == 7) ||
                        (node.Method.Name == nameof(Sys.TimeFromParts) && idx == 4))
                    {
                        _queryBuilder.Append($"{GetValue<int>(node.Arguments[idx])}");
                    }
                    else
                    {
                        Visit(node.Arguments[idx]);
                    }
                }

                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(Sys.EoMonth))
            {
                _queryBuilder.Append(node.Method.Name.ToUpper());
                _queryBuilder.Append("(");

                for (int idx = 0; idx < node.Arguments.Count; idx++)
                {
                    if (idx > 0)
                    {
                        _queryBuilder.Append(", ");
                    }

                    Visit(node.Arguments[idx]);
                }

                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(Sys.DateTimeOffset) ||
                node.Method.Name == nameof(Sys.DateTime) ||
                node.Method.Name == nameof(Sys.UtcDateTime))
            {
                _queryBuilder.Append($"SYS{node.Method.Name.ToUpper()}()");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression VisitCountMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(Count.All))
            {
                _queryBuilder.Append("COUNT(*)");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression VisitAppLockMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(AppLock.Test))
            {
                _queryBuilder.Append("APPLOCK_TEST(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[2]);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[3]);
                _queryBuilder.Append(")");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression VisitCaseThenElseMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(Case.When))
            {
                var hasCase = false;
                if (node.Object is MethodCallExpression methodCallExp)
                {
                    Visit(methodCallExp);

                    hasCase = methodCallExp.Method.Name == nameof(SQLQueryConditionExtension.Case);
                }
                                
                if (!hasCase)
                {
                    int whenCount = 0;
                    CountWhen(node, ref whenCount);

                    hasCase = whenCount > 1;
                }

                _queryBuilder.Append(!hasCase ? "CASE WHEN " : " WHEN ");
                
                var conditions = node.Arguments;
                foreach (var condition in conditions)
                {
                    Visit(condition);
                }

                return node;
            }
            else if (node.Method.Name == nameof(ICaseWhen.Then))
            {
                if (node.Object is MethodCallExpression methodCallExp)
                {
                    Visit(methodCallExp);
                }

                _queryBuilder.Append(" THEN ");
                Visit(node.Arguments[0]);

                return node;
            }
            else if (node.Method.Name == nameof(ICaseThen<object>.Else))
            {
                Visit(((MethodCallExpression)node.Object).Object);

                _queryBuilder.Append(" THEN ");
                Visit(((MethodCallExpression)node.Object).Arguments[0]);
                _queryBuilder.Append(" ELSE ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" END");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private void CountWhen(MethodCallExpression node, ref int whenCount)
        {
            if (node.Method.Name == nameof(Case.When))
            {
                whenCount++;
            }

            if (node.Object is MethodCallExpression methodCallExp)
            {
                CountWhen(methodCallExp, ref whenCount);
            }
        }

        private Expression VisitRowMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(Row.Number))
            {
                _queryBuilder.Append("ROW_NUMBER()");

                return node;
            }
            else if (node.Method.Name == nameof(IRowNumber.Over))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" OVER(");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(")");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression VisitDateTimeMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(DateTime.AddYears))
            {
                _queryBuilder.Append("DATEADD(YEAR, ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(DateTime.AddMonths))
            {
                _queryBuilder.Append("DATEADD(MONTH, ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(DateTime.AddDays))
            {
                _queryBuilder.Append("DATEADD(DAY, ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(DateTime.AddHours))
            {
                _queryBuilder.Append("DATEADD(HOUR, ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(DateTime.AddMinutes))
            {
                _queryBuilder.Append("DATEADD(MINUTE, ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(DateTime.AddSeconds))
            {
                _queryBuilder.Append("DATEADD(SECOND, ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(DateTime.AddMilliseconds))
            {
                _queryBuilder.Append("DATEADD(MILLISECOND, ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(DateTime.Parse) ||
                node.Method.Name == nameof(DateTime.ParseExact))
            {
                Visit(Expression.Constant(GetExpressionResultValue(node)));

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression VisitStringMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(string.StartsWith))
            {
                _queryBuilder.Append("(");
                this.Visit(node.Object);
                _queryBuilder.Append(" LIKE ");
                var valueExpression = (ConstantExpression)node.Arguments[0];
                var parameterName = _parametersBuilder.AddParameter($"{valueExpression.Value}%");
                _queryBuilder.Append(parameterName);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(string.EndsWith))
            {
                _queryBuilder.Append("(");
                this.Visit(node.Object);
                _queryBuilder.Append(" LIKE ");
                var valueExpression = (ConstantExpression)node.Arguments[0];
                var parameterName = _parametersBuilder.AddParameter($"%{valueExpression.Value}");
                _queryBuilder.Append(parameterName);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(string.Contains))
            {
                _queryBuilder.Append("(");
                this.Visit(node.Object);
                _queryBuilder.Append(" LIKE ");
                var valueExpression = (ConstantExpression)node.Arguments[0];
                var parameterName = _parametersBuilder.AddParameter($"%{valueExpression.Value}%");
                _queryBuilder.Append(parameterName);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(string.Substring))
            {
                if (node.Arguments.Count == 1)
                {
                    _queryBuilder.Append("LEFT(");
                    Visit(node.Object);
                    _queryBuilder.Append(", ");
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append(")");
                }
                else
                {
                    _queryBuilder.Append("SUBSTRING(");
                    Visit(node.Object);
                    _queryBuilder.Append(", ");
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append(", ");
                    Visit(node.Arguments[1]);
                    _queryBuilder.Append(")");
                }

                return node;
            }
            else if (node.Method.Name == nameof(string.Trim))
            {
                _queryBuilder.Append("TRIM(");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(string.TrimStart))
            {
                _queryBuilder.Append("LTRIM(");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(string.TrimEnd))
            {
                _queryBuilder.Append("RTRIM(");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(string.ToUpper))
            {
                _queryBuilder.Append("UPPER(");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(string.ToLower))
            {
                _queryBuilder.Append("LOWER(");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(string.Replace))
            {
                _queryBuilder.Append("REPLACE(");
                Visit(node.Object);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(string.IndexOf))
            {
                _queryBuilder.Append("CHARINDEX(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Object);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(string.Concat))
            {
                _queryBuilder.Append("CONCAT(");
                var idx = node.Arguments.Count - 1;
                foreach (var arg in node.Arguments)
                {
                    Visit(arg);

                    if (idx-- > 0)
                    {
                        _queryBuilder.Append(", ");
                    }
                }
                _queryBuilder.Append(")");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression VisitStringNegationMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(string.StartsWith))
            {
                _queryBuilder.Append("(");
                this.Visit(node.Object);
                _queryBuilder.Append(" NOT LIKE ");
                var valueExpression = (ConstantExpression)node.Arguments[0];
                var parameterName = _parametersBuilder.AddParameter($"{valueExpression.Value}%");
                _queryBuilder.Append(parameterName);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(string.EndsWith))
            {
                _queryBuilder.Append("(");
                this.Visit(node.Object);
                _queryBuilder.Append(" NOT LIKE ");
                var valueExpression = (ConstantExpression)node.Arguments[0];
                var parameterName = _parametersBuilder.AddParameter($"%{valueExpression.Value}");
                _queryBuilder.Append(parameterName);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(string.Contains))
            {
                _queryBuilder.Append("(");
                this.Visit(node.Object);
                _queryBuilder.Append(" NOT LIKE ");
                var valueExpression = (ConstantExpression)node.Arguments[0];
                var parameterName = _parametersBuilder.AddParameter($"%{valueExpression.Value}%");
                _queryBuilder.Append(parameterName);
                _queryBuilder.Append(")");

                return node;
            }
           
            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        public Expression VisitQueryPaginationMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(SQLQueryPaginationExtensions.Offset))
            {
                _queryBuilder.Append("OFFSET ");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(" ROWS");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryPaginationExtensions.Fetch))
            {
                if (node.Arguments[0] is MethodCallExpression)
                {
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append(" FETCH NEXT ");
                    Visit(node.Arguments[1]);
                }
                else
                {
                    _queryBuilder.Append("FETCH FIRST ");
                    Visit(node.Arguments[1]);
                }

                _queryBuilder.Append(" ROWS ONLY");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryPaginationExtensions.PartitionBy))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append("PARTITION BY ");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(" ");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryPaginationExtensions.OrderBy))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append("ORDER BY ");
                Visit(node.Arguments[1]);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryPaginationExtensions.Over))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" OVER(");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(")");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression VisitQuerySelectionMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(SQLQuerySelectionExtension.All))
            {
                var parameterExp = (ParameterExpression)node.Arguments[0];
                var alias = _aliasBuilder.ResolveTableName(parameterExp.Type);
                _queryBuilder.Append($"{alias.TableAlias}.*");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQuerySelectionExtension.As))
            {
                this.Visit(node.Arguments[0]);
                _queryBuilder.Append(" AS ");
                if (node.Arguments[1] is MemberExpression memberExp)
                {
                    _queryBuilder.Append(_aliasBuilder.ResolveColumnName(memberExp.Type, memberExp.Member));
                }
                else
                {
                    var unaryExp = (UnaryExpression)node.Arguments[1];
                    var opMemberExp = (MemberExpression)unaryExp.Operand;
                    _queryBuilder.Append(_aliasBuilder.ResolveColumnName(opMemberExp.Type, opMemberExp.Member));
                }

                return node;
            }
            else if (node.Method.Name == nameof(SQLQuerySelectionExtension.Inserted))
            {
                _queryBuilder.Append($"INSERTED.");
                if (node.Arguments[0] is MethodCallExpression methodCallExp && methodCallExp.Method.Name == nameof(SQLQuerySelectionExtension.All))
                {
                    _queryBuilder.Append($"*");

                    return node;
                }
                else if (node.Arguments[0] is MemberExpression memberxp)
                {
                    _queryBuilder.Append(_aliasBuilder.ResolveColumnName(memberxp.Type, memberxp.Member));

                    return node;
                }
            }
            else if (node.Method.Name == nameof(SQLQuerySelectionExtension.Deleted))
            {
                _queryBuilder.Append($"DELETED.");
                if (node.Arguments[0] is MethodCallExpression methodCallExp && methodCallExp.Method.Name == nameof(SQLQuerySelectionExtension.All))
                {
                    _queryBuilder.Append($"*");

                    return node;
                }
                else if (node.Arguments[0] is MemberExpression memberxp)
                {
                    _queryBuilder.Append(_aliasBuilder.ResolveColumnName(memberxp.Type, memberxp.Member));

                    return node;
                }
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression VisitQueryConversionMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(SQLQueryConversionExtensions.Compress))
            {
                _queryBuilder.Append("COMPRESS(");
                this.Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConversionExtensions.Decompress))
            {
                _queryBuilder.Append("DECOMPRESS(");
                this.Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConversionExtensions.Cast))
            {
                _queryBuilder.Append("CAST(");
                this.Visit(node.Arguments[0]);
                _queryBuilder.Append(" AS ");
                if (node.Arguments.Count == 2)
                {
                    this.Visit(node.Arguments[1]);
                }
                else
                {
                    this.Visit(GetConstantExpressionOfDbType(node.Method.ReturnType));
                }
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConversionExtensions.Convert))
            {
                _queryBuilder.Append("CONVERT(");
                if (node.Arguments.Count == 3)
                {
                    this.Visit(node.Arguments[1]);
                    _queryBuilder.Append(", ");
                    this.Visit(node.Arguments[0]);
                    _queryBuilder.Append($", {((ConstantExpression)node.Arguments[2]).Value}");
                }
                else if (node.Arguments.Count == 2)
                {
                    this.Visit(node.Arguments[1]);
                    _queryBuilder.Append(", ");
                    this.Visit(node.Arguments[0]);
                }
                else
                {
                    this.Visit(GetConstantExpressionOfDbType(node.Method.ReturnType));
                    _queryBuilder.Append(", ");
                    this.Visit(node.Arguments[0]);
                }
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConversionExtensions.Unicode))
            {
                _queryBuilder.Append("UNICODE(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConversionExtensions.Ascii))
            {
                _queryBuilder.Append("ASCII(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConversionExtensions.Collate))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append($" COLLATE {((ConstantExpression)node.Arguments[1]).Value}");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression GetConstantExpressionOfDbType(Type returnType)
        {
            SqlDbType? sqlDbType = null;

            if (returnType == typeof(Int16))
            {
                sqlDbType = SqlDbType.SmallInt;
            }
            else if (returnType == typeof(Int32))
            {
                sqlDbType = SqlDbType.Int;
            }
            else if (returnType == typeof(Int64))
            {
                sqlDbType = SqlDbType.BigInt;
            }
            else if (returnType == typeof(DateTime))
            {
                sqlDbType = SqlDbType.DateTime2;
            }
            else if (returnType == typeof(DateTimeOffset))
            {
                sqlDbType = SqlDbType.DateTimeOffset;
            }
            else if (returnType == typeof(double))
            {
                sqlDbType = SqlDbType.Decimal;
            }
            else if (returnType == typeof(TimeSpan))
            {
                sqlDbType = SqlDbType.Time;
            }
            
            if (sqlDbType.HasValue)
            {
                return Expression.Constant(sqlDbType.Value);
            }

            throw new NotSupportedException(string.Format("The implicit conversion in SqlDbType of '{0}' is not supported.", returnType.Name));
        }

        private Expression VisitQueryDefinitionMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(SQLQueryDefinitionExtensions.Max))
            {
                this.Visit(node.Arguments[0]);
                _queryBuilder.Append("(MAX)");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryDefinitionExtensions.Size))
            {
                this.Visit(node.Arguments[0]);
                _queryBuilder.Append($"({((ConstantExpression)node.Arguments[1]).Value})");
                
                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryDefinitionExtensions.Precision))
            {
                this.Visit(node.Arguments[0]);

                if (node.Arguments.Count == 1)
                {
                    _queryBuilder.Append($"({((ConstantExpression)node.Arguments[1]).Value})");
                }
                else
                {
                    _queryBuilder.Append($"({((ConstantExpression)node.Arguments[1]).Value}, {((ConstantExpression)node.Arguments[2]).Value})");
                }

                return node;
            }
            if (node.Method.Name == nameof(SQLQueryDefinitionExtensions.Asc))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" ASC");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryDefinitionExtensions.Desc))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" DESC");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression VisitQueryConditionMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(SQLQueryConditionExtension.IsNull))
            {
                TranslateIsNullCondition(node);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.IsNotNull))
            {
                TranslateIsNotNullCondition(node);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.In))
            {
                TranslateInCondition(node);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.NotIn))
            {
                TranslateNotInCondition(node);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.Case))
            {
                _queryBuilder.Append("CASE ");
                this.Visit(node.Arguments[0]);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.Exists))
            {
                TranslateExistsCondition(node);
                
                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.NotExists))
            {
                TranslateNotExistsCondition(node);
                
                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.Between))
            {
                TranslateBetweenCondition(node);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.NotBetween))
            {
                TranslateNotBetweenCondition(node);

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression VisitQueryConditionNegationMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(SQLQueryConditionExtension.IsNull))
            {
                TranslateIsNotNullCondition(node);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.IsNotNull))
            {
                TranslateIsNullCondition(node);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.In))
            {
                TranslateNotInCondition(node);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.NotIn))
            {
                TranslateInCondition(node);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.Exists))
            {
                TranslateNotExistsCondition(node);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.NotExists))
            {
                TranslateExistsCondition(node);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.Between))
            {
                TranslateNotBetweenCondition(node);

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryConditionExtension.NotBetween))
            {
                TranslateBetweenCondition(node);

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private void TranslateBetweenCondition(MethodCallExpression methodCallExp)
        {
            this.Visit(methodCallExp.Arguments[0]);
            _queryBuilder.Append(" BETWEEN ");
            this.Visit(methodCallExp.Arguments[1]);
            _queryBuilder.Append(" AND ");
            this.Visit(methodCallExp.Arguments[2]);
        }

        private void TranslateNotBetweenCondition(MethodCallExpression methodCallExp)
        {
            this.Visit(methodCallExp.Arguments[0]);
            _queryBuilder.Append(" NOT BETWEEN ");
            this.Visit(methodCallExp.Arguments[1]);
            _queryBuilder.Append(" AND ");
            this.Visit(methodCallExp.Arguments[2]);
        }

        private void TranslateExistsCondition(MethodCallExpression methodCallExp)
        {
            _queryBuilder.Append("EXISTS ");
            this.Visit(methodCallExp.Arguments[1]);
        }

        private void TranslateNotExistsCondition(MethodCallExpression methodCallExp)
        {
            _queryBuilder.Append("NOT EXISTS ");
            this.Visit(methodCallExp.Arguments[1]);
        }

        private void TranslateInCondition(MethodCallExpression methodCallExp)
        {
            _queryBuilder.Append("(");
            this.Visit(methodCallExp.Arguments[0]);
            _queryBuilder.Append(" IN (");
            this.Visit(methodCallExp.Arguments[1]);
            _queryBuilder.Append("))");
        }

        private void TranslateNotInCondition(MethodCallExpression methodCallExp)
        {
            _queryBuilder.Append("(");
            this.Visit(methodCallExp.Arguments[0]);
            _queryBuilder.Append(" NOT IN (");
            this.Visit(methodCallExp.Arguments[1]);
            _queryBuilder.Append("))");
        }

        private void TranslateIsNullCondition(MethodCallExpression methodCallExp)
        {
            if (methodCallExp.Arguments.Count == 1)
            {
                _queryBuilder.Append("(");
                this.Visit(methodCallExp.Arguments[0]);
                _queryBuilder.Append(" IS NULL");
                _queryBuilder.Append(")");
            }
            else
            {
                _queryBuilder.Append("ISNULL(");
                this.Visit(methodCallExp.Arguments[0]);
                _queryBuilder.Append(", ");
                this.Visit(methodCallExp.Arguments[1]);
                _queryBuilder.Append(")");
            }
        }

        private void TranslateIsNotNullCondition(MethodCallExpression methodCallExp)
        {
            _queryBuilder.Append("(");
            this.Visit(methodCallExp.Arguments[0]);
            _queryBuilder.Append(" IS NOT NULL");
            _queryBuilder.Append(")");
        }

        private Expression VisitQueryOperationMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(SQLQueryOperationExtensions.Sum))
            {
                _queryBuilder.Append("SUM(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryOperationExtensions.Sign))
            {
                _queryBuilder.Append("SIGN(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryOperationExtensions.Count))
            {
                if (!node.Arguments[0].Type.IsClass)
                {
                    _queryBuilder.Append("COUNT(");
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append(")");

                    return node;
                }
            }
            else if (node.Method.Name == nameof(SQLQueryOperationExtensions.CountDistinct))
            {
                if (!node.Arguments[0].Type.IsClass)
                {
                    _queryBuilder.Append("COUNT(DISTINCT ");
                    Visit(node.Arguments[0]);
                    _queryBuilder.Append(")");

                    return node;
                }
            }
            else if (node.Method.Name == nameof(SQLQueryOperationExtensions.Max))
            {
                _queryBuilder.Append("MAX(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryOperationExtensions.Min))
            {
                _queryBuilder.Append("MIN(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryOperationExtensions.Left))
            {
                _queryBuilder.Append("LEFT(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryOperationExtensions.Right))
            {
                _queryBuilder.Append("RIGHT(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryOperationExtensions.Len))
            {
                _queryBuilder.Append("LEN(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryOperationExtensions.Avg))
            {
                _queryBuilder.Append("AVG(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryOperationExtensions.AvgDistinct))
            {
                _queryBuilder.Append("AVG(DISTINCT ");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(")");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryOperationExtensions.Path))
            {
                _queryBuilder.Append("PATH ");
                if (node.Arguments.Count == 2)
                {
                    _queryBuilder.Append($"('{((ConstantExpression)node.Arguments[1]).Value}')");
                }

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryOperationExtensions.Root))
            {
                Visit(node.Arguments[0]);

                _queryBuilder.Append(", root ");
                _queryBuilder.Append($"('{((ConstantExpression)node.Arguments[1]).Value}')");

                return node;
            }
            else if (node.Method.Name == nameof(SQLQueryOperationExtensions.Stuff))
            {
                _queryBuilder.Append("STUFF(");
                Visit(node.Arguments[0]);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[1]);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[2]);
                _queryBuilder.Append(", ");
                Visit(node.Arguments[3]);
                _queryBuilder.Append(")");

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private Expression VisitQueryAssignmentMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(SQLQueryAssignmentExtensions.Set))
            {
                Visit(node.Arguments[0]);
                _queryBuilder.Append(" = ");
                Visit(node.Arguments[1]);

                return node;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));
        }

        private readonly Type BoolType = typeof(bool);
        private readonly Type NullBoolType = typeof(bool?);
        private readonly IList<Type> BooleanTypes = new List<Type> { typeof(bool), typeof(bool?) };

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression != null && 
                (
                    node.Expression.NodeType == ExpressionType.Parameter ||
                    (
                        node.Expression.NodeType == ExpressionType.MemberAccess) &&
                        node.Expression.Type.IsGenericType && node.Expression.Type.GetGenericTypeDefinition() == typeof(Nullable<>)
                    )
                )
            {
                var type = node.Expression.NodeType == ExpressionType.Parameter
                    ? node.Expression.Type
                    : ((MemberExpression)node.Expression).Expression.Type;

                var member = node.Expression.NodeType == ExpressionType.Parameter
                    ? node.Member
                    : ((MemberExpression)node.Expression).Member;

                var tableName = _aliasBuilder.ResolveTableName(type);
                var columnName = _aliasBuilder.ResolveColumnName(type, member);

                if (_fragmentType == SQLQueryFragmentType.Where &&
                    !_memberExpressionFromBinaryExpression &&
                    BooleanTypes.Contains(node.Expression.NodeType == ExpressionType.Parameter ? node.Type : node.Expression.Type))
                {
                    ExpressionType typeExpression;
                    Expression valueExpression;
                    if (node.Member.Name == nameof(Nullable<bool>.HasValue))
                    {
                        typeExpression = ExpressionType.NotEqual;
                        valueExpression = Expression.Constant(null);
                    }
                    else
                    {
                        typeExpression = ExpressionType.Equal;
                        valueExpression = (node.Expression.NodeType == ExpressionType.Parameter 
                            ? node.Type 
                            : node.Expression.Type) == NullBoolType
                                ? (Expression)Expression.Convert(Expression.Constant(true), NullBoolType)
                                : Expression.Constant(true);
                    }

                    Visit(Expression.MakeBinary(
                        typeExpression, 
                        node.Expression.NodeType == ExpressionType.Parameter 
                            ? node 
                            : node.Expression,
                        valueExpression));
                }
                else
                {
                    _queryBuilder.Append($"{tableName.TableAlias}.{columnName}");
                }

                return node;
            }
            else if (node.Expression != null && node.Expression.NodeType == ExpressionType.Constant)
            {                
                object container = ((ConstantExpression)node.Expression).Value;

                var member = node.Member;
                if (member is FieldInfo field)
                {
                    object value = field.GetValue(container);
                    Visit(Expression.Constant(value));
                }
                else if (member is PropertyInfo propery)
                {
                    object value = propery.GetValue(container, null);
                    Visit(Expression.Constant(value));
                }
                else
                {
                    Visit(node.Expression);
                }

                return node;
            }
            else if (node.NodeType == ExpressionType.MemberAccess)
            {
                if (node.Expression == null)
                {
                    object value = GetMemberValue(node);

                    Visit(Expression.Constant(value));

                    return node;
                }
                else if (node.Expression is MethodCallExpression methodCall &&
                    (
                        methodCall.Method.DeclaringType == typeof(DateTime) ||
                        methodCall.Method.DeclaringType == typeof(DateTime?) ||
                        methodCall.Method.DeclaringType == typeof(DateTimeOffset) ||
                        methodCall.Method.DeclaringType == typeof(DateTimeOffset?)
                    ) &&
                    methodCall.Method.Name == nameof(DateTime.Subtract)) 
                {
                    _queryBuilder.Append("DATEDIFF(");
                    var memberName = node.Member.Name.ToUpper().Replace("TOTAL", string.Empty);
                    memberName = memberName.Substring(0, memberName.Length - 1);
                    _queryBuilder.Append($"{memberName}, ");
                    Visit(methodCall.Arguments[0]);
                    _queryBuilder.Append(", ");
                    Visit(methodCall.Object);
                    _queryBuilder.Append(")");

                    return node;
                }
                else if (node.Expression != null && node.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    var memberExp = (MemberExpression)node.Expression;
                    if (memberExp.Type == typeof(DateTime) || memberExp.Type == typeof(DateTimeOffset) ||
                        (
                            memberExp.Expression.NodeType == ExpressionType.MemberAccess &&
                            (
                                ((MemberExpression)memberExp.Expression).Type == typeof(DateTime?) ||
                                ((MemberExpression)memberExp.Expression).Type == typeof(DateTimeOffset?)
                            )
                        ))
                    {
                        if (TryGetDatePart(node.Member, out string partName))
                        {
                            _queryBuilder.Append($"DATEPART({partName}, ");
                            Visit(memberExp);
                            _queryBuilder.Append(")");

                            return node;
                        }
                    }
                    else if (memberExp.Expression != null && memberExp.Expression.NodeType == ExpressionType.Constant)
                    {
                        object container = ((ConstantExpression)memberExp.Expression).Value;
                        container = TryExtractCSharpGeneratedClass(memberExp.Member, container);

                        var member = node.Member;
                        if (member is FieldInfo field)
                        {
                            object value = field.GetValue(container);
                            Visit(Expression.Constant(value));
                        }
                        else if (member is PropertyInfo propery)
                        {
                            object value = propery.GetValue(container, null);
                            Visit(Expression.Constant(value));
                        }
                        else
                        {
                            Visit(node.Expression);
                        }

                        return node;
                    }
                }
            }


            throw new NotSupportedException(string.Format("The member '{0}' is not supported.", node.Member.Name));
        }

        private static bool TryGetDatePart(MemberInfo member, out string partName)
        {
            partName = null;

            switch (member.Name)
            {
                case nameof(DateTime.Year):
                case nameof(DateTime.Month):
                case nameof(DateTime.Day):
                case nameof(DateTime.Hour):
                case nameof(DateTime.Minute):
                case nameof(DateTime.Second):
                case nameof(DateTime.Millisecond):
                    partName = member.Name.ToUpper();
                    break;
                case nameof(DateTime.DayOfYear):
                    partName = "DAYOFYEAR";
                    break;
                case nameof(DateTime.DayOfWeek):
                    partName = "WEEKDAY";
                    break;
                default:
                    break;
            }

            return partName != null;
        }

        private static object TryExtractCSharpGeneratedClass(MemberInfo memberInfo, object container)
        {
            var containerTypeName = container.GetType().Name;
            if (IsCSharpGeneratedClass(containerTypeName, "DisplayClass") ||
                IsCSharpGeneratedClass(containerTypeName, "AnonymousType"))
            {
                if (memberInfo is FieldInfo fieldDisplayClassMember)
                {
                    container = fieldDisplayClassMember.GetValue(container);
                }
                else if (memberInfo is PropertyInfo propertyDisplayClassMember)
                {
                    container = propertyDisplayClassMember.GetValue(container, null);
                }
            }

            return container;
        }

        private static bool IsCSharpGeneratedClass(string typeName, string pattern)
        {
            return typeName.Contains("<>") && typeName.Contains("__") && typeName.Contains(pattern);
        }

        private static object GetMemberValue(MemberExpression node)
        {
            var objectMember = Expression.Convert(node, typeof(object));

            var getterLambda = Expression.Lambda<Func<object>>(objectMember);

            var getter = getterLambda.Compile();

            var value = getter();

            return value;
        }

        private T GetValue<T>(Expression node)
        {
            if (node.NodeType == ExpressionType.Constant)
            {
                return (T)((ConstantExpression)node).Value;
            }
            else if (node.NodeType == ExpressionType.MemberAccess)
            {
                return (T)GetMemberValue((MemberExpression)node);
            }
            else if (node.NodeType == ExpressionType.Call ||
                node.NodeType == ExpressionType.New)
            {
                return (T)GetExpressionResultValue(node);
            }

            throw new NotSupportedException(string.Format("Get Value from expression type '{0}' is not supported.", node.NodeType));
        }

        private static object GetExpressionResultValue(Expression node)
        {
            return Expression.Lambda(node).Compile().DynamicInvoke();
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value == null)
            {
                _queryBuilder.Append("NULL");
            }
            else if (node.Value is Type valueType)
            {
                var alias = _aliasBuilder.ResolveTableNameAsAlias(valueType);
                _queryBuilder.Append(alias);
            }
            else if (node.Value is SqlDbType dbType)
            {
                _queryBuilder.Append(dbType.ToString().ToUpper());
            }
            else if (node.Value is SQLQuerySelect sqlQuery)
            {
                if (sqlQuery.FragmentType == SQLQueryFragmentType.SelectCte ||
                    sqlQuery.IsHierarchicalSelectFromCte())
                {
                    throw new NotSupportedException($"Queries with CTE TABLEs is not supported in InLine {nameof(SQLQuerySelect)}.");
                }

                _queryBuilder.Append($"({SQLQueryCompiler.CompileQuery(sqlQuery, _parametersBuilder, _aliasBuilder)})");
            }
            else if (node.Value is AppLockMode lockMode)
            {
                _queryBuilder.Append($"'{lockMode}'");
            }
            else if (node.Value is AppLockOwner lockOwner)
            {
                _queryBuilder.Append($"'{lockOwner}'");
            }
            else if (node.Value is byte[])
            {
                string parameterName = _parametersBuilder.AddParameter(node.Value);
                _queryBuilder.Append(parameterName);
            }
            else if (node.Value is IList listValues)
            {                
                var parameters = new List<string>();
                foreach (var v in listValues)
                {
                    string parameterName = _parametersBuilder.AddParameter(v);
                    parameters.Add(parameterName);
                }

                _queryBuilder.Append(string.Join(", ", parameters));
            }
            else
            {
                string parameterName = _parametersBuilder.AddParameter(node.Value);
                _queryBuilder.Append(parameterName);
            }

            return node;
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            var valuesExp = node.Initializers.SelectMany(i => i.Arguments);
            
            var listVal = new List<object>();

            foreach (var vExp in valuesExp)
            {
                var val = (ConstantExpression)vExp;
                listVal.Add(val.Value);
            }
            
            Visit(Expression.Constant(listVal));

            return node;
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            var idx = node.Expressions.Count - 1;
            foreach (var exp in node.Expressions)
            {
                Visit(exp);

                if (idx-- > 0)
                {
                    _queryBuilder.Append(", ");
                }
            }
            
            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Negate)
            {
                _queryBuilder.Append("-(");

                base.VisitUnary(node);

                _queryBuilder.Append(")");

                return node;
            }
            else if (_fragmentType == SQLQueryFragmentType.Where &&
                node.NodeType == ExpressionType.Not && 
                node.Operand.NodeType == ExpressionType.MemberAccess &&
                BooleanTypes.Contains(((MemberExpression)node.Operand).Expression.NodeType == ExpressionType.Parameter 
                    ? node.Operand.Type : 
                    ((MemberExpression)node.Operand).Expression.Type))
            {
                Expression valueExpression;
                if (((MemberExpression)node.Operand).Member.Name == nameof(Nullable<bool>.HasValue))
                {
                    valueExpression = Expression.Constant(null);
                }
                else
                {
                    valueExpression = (((MemberExpression)node.Operand).Expression.NodeType == ExpressionType.Parameter 
                        ? node.Operand.Type 
                        : ((MemberExpression)node.Operand).Expression.Type) == NullBoolType
                            ? (Expression)Expression.Convert(Expression.Constant(false), NullBoolType)
                            : Expression.Constant(false);
                }

                Visit(Expression.MakeBinary(
                    ExpressionType.Equal, 
                    ((MemberExpression)node.Operand).Expression.NodeType == ExpressionType.Parameter 
                        ? node.Operand 
                        : ((MemberExpression)node.Operand).Expression,
                    valueExpression));

                return node;
            }
            else if (_fragmentType == SQLQueryFragmentType.Where &&
               node.NodeType == ExpressionType.Not &&
               node.Operand.NodeType == ExpressionType.Call)
            {
                var methodCallExp = (MethodCallExpression)node.Operand;
                var declaringType = methodCallExp.Method.DeclaringType;
                if (declaringType == typeof(SQLQueryConditionExtension))
                {
                    return VisitQueryConditionNegationMethodCall(methodCallExp);
                }
                else if (declaringType == typeof(string))
                {
                    return VisitStringNegationMethodCall(methodCallExp);
                }
            }

            return base.VisitUnary(node);
        }
    }
}