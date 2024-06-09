DECLARE @DatabaseName sysname = '{DatabaseName}'
DECLARE @ProcedureSchema sysname = '{ProcedureSchema}'
DECLARE @ProcedureName sysname = '{ProcedureName}'
DECLARE @Namespace sysname = '{Namespace}'
DECLARE @ClassName sysname = '{ClassName}'
DECLARE @SQLStoredProcedureInterface sysname = '{SQLStoredProcedureInterface}'
DECLARE @Result varchar(max) = '
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace ' + @Namespace + '
{
    public partial class ' + @ProcedureSchema + '
    {
        [Database("'+ @DatabaseName + '")]
	    [StoredProcedure("'+ @ProcedureName + '", Schema = "'+ @ProcedureSchema + '")]
	    public class ' + @ClassName + ' : ' + @SQLStoredProcedureInterface + '
	    {'

select @Result = @Result + ReqAttr + ParamAttr + '
		    public ' + ParameterType + NullableSign + ' ' + case when ParameterName = @ProcedureName then concat(ParameterName, cast(1 as varchar(1))) else ParameterName end + ' { get; set; }
'
from
(
    select 
        REPLACE(REPLACE(pr.name, ' ', '_'), '@', '') ParameterName,
        parameter_id ParameterId,
        CASE typ.name 
            WHEN 'bigint' THEN 'long'
            WHEN 'binary' then 'byte[]'
            when 'bit' then 'bool'
            when 'char' then 'string'
            when 'date' then 'DateTime'
            when 'datetime' then 'DateTime'
            when 'datetime2' then 'DateTime'
            when 'datetimeoffset' then 'DateTimeOffset'
            when 'decimal' then 'decimal'
            when 'float' then 'double'
            when 'image' then 'byte[]'
            when 'int' then 'int'
            when 'money' then 'decimal'
            when 'nchar' then 'string'
            when 'ntext' then 'string'
            when 'numeric' then 'decimal'
            when 'nvarchar' then 'string'
            when 'real' then 'float'
            when 'smalldatetime' then 'DateTime'
            when 'smallint' then 'short'
            when 'smallmoney' then 'decimal'
            when 'text' then 'string'
            when 'time' then 'TimeSpan'
            when 'timestamp' then 'long'
            when 'tinyint' then 'byte'
            when 'uniqueidentifier' then 'Guid'
            when 'varbinary' then 'byte[]'
            when 'varchar' then 'string'
			when 'xml' then 'string'
            else 'UNKNOWN_' + typ.name
        end ParameterType,
        case 
            when pr.is_nullable = 1 and typ.name in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier') 
            then '?' 
            else '' 
        end NullableSign,
		case 
            when pr.is_nullable = 0 and typ.name NOT in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier') 
            then '
		    [Required]' 
            else '' 
        end ReqAttr,
        case
            when pr.is_output = 0
            then '
            [Parameter("'+ REPLACE(REPLACE(pr.name, ' ', '_'), '@', '') + '")]'
            else '
            [Parameter("'+ REPLACE(REPLACE(pr.name, ' ', '_'), '@', '') + '", Output = true)]'
        end ParamAttr
    from sys.parameters pr
        join sys.types typ on
            pr.system_type_id = typ.system_type_id AND pr.user_type_id = typ.user_type_id
    where object_id = object_id(@ProcedureSchema + '.' + @ProcedureName) AND pr.is_output = 0
) t
order by ParameterId

set @Result = @Result  + '
	    }
    }
}'

SELECT @Result