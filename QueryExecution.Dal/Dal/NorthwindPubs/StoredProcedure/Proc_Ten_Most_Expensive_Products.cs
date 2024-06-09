
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [StoredProcedure("Ten Most Expensive Products", Schema = "dbo")]
	    public class Proc_Ten_Most_Expensive_Products : ISQLStoredProcedure<Proc_Ten_Most_Expensive_Products_Result>
	    {
	    }
    }
}