namespace CSQLQueryExpress
{
    public sealed class SQLQueryCompilerSettings
    {
        /// <summary>
        /// The prefix of all query paramenters. Default is @p. Example @p0, @p1.
        /// </summary>
        public string QueryParameterPrefix { get; set; } = "@p";

        /// <summary>
        /// The prefix of all stored procedure paramenters. Default is @. Example @parameterName.
        /// </summary>
        public string StoredProcedureParameterPrefix { get; set; } = "@";

        /// <summary>
        /// The prefix of all query tables alias. Default is _t. Example _t1, _t2.
        /// </summary>
        public string TableAliasPrefix { get; set; } = "_t";
    }
}
