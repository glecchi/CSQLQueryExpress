namespace CSQLQueryExpress
{
    public static class SQLQueryExtensions
    {
        /// <summary>
        /// Compile a <see cref="ISQLQuery"/> instance in a <see cref="SQLQueryCompiled"/>.
        /// </summary>
        /// <param name="query">SQL query expression built with <see cref="SQLQuery"/></param>
        /// <param name="settings">[OPTIONAL] SQL query compiler setting <see cref="SQLQueryCompilerSettings"/></param>
        /// <returns>A compiled expression <see cref="SQLQueryCompiled"/>.</returns>
        public static SQLQueryCompiled Compile(this ISQLQuery query, SQLQueryCompilerSettings settings = null)
        {
            return SQLQueryCompiler.Compile(query, settings);
        }

        /// <summary>
        /// Compile a <see cref="ISQLQuery"/> instance in a <see cref="SQLQueryCompiled"/>.
        /// </summary>
        /// <param name="query">SQL query expression built with <see cref="SQLQuery"/>,</param>
        /// <param name="parameterBuilder">The SQL query parameter builder.</param>
        /// <param name="tableNameResolver">The SQL query table name resolver.</param>
        /// <returns>A compiled expression <see cref="SQLQueryCompiled"/>.</returns>
        public static SQLQueryCompiled Compile(this ISQLQuery query,
            ISQLQueryParametersBuilder parameterBuilder,
            ISQLQueryTableNameResolver tableNameResolver)
        {
            return SQLQueryCompiler.Compile(query, parameterBuilder, tableNameResolver);
        }
    }
}
