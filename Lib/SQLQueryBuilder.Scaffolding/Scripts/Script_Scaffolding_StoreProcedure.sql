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
            WHEN 'char' THEN 'string'
            WHEN 'date' THEN 'DateTime'
            WHEN 'datetime' THEN 'DateTime'
            WHEN 'datetime2' THEN 'DateTime'
            WHEN 'datetimeoffset' THEN 'DateTimeOffset'
            WHEN 'decimal' THEN 'decimal'
            WHEN 'float' THEN 'double'
            WHEN 'image' THEN 'byte[]'
            WHEN 'int' THEN 'int'
            WHEN 'money' THEN 'decimal'
            WHEN 'nchar' THEN 'string'
            WHEN 'ntext' THEN 'string'
            WHEN 'numeric' THEN 'decimal'
            WHEN 'nvarchar' THEN 'string'
            WHEN 'real' THEN 'float'
            WHEN 'smalldatetime' THEN 'DateTime'
            WHEN 'smallint' THEN 'short'
            WHEN 'smallmoney' THEN 'decimal'
            WHEN 'text' THEN 'string'
            WHEN 'time' THEN 'TimeSpan'
            WHEN 'timestamp' THEN 'long'
            WHEN 'tinyint' THEN 'byte'
            WHEN 'uniqueidentifier' THEN 'Guid'
            WHEN 'varbinary' THEN 'byte[]'
            WHEN 'varchar' THEN 'string'
			WHEN 'xml' THEN 'string'
            ELSE 'UNKNOWN_' + typ.name
        END ParameterType,
        CASE 
            WHEN pr.is_nullable = 1 AND typ.name IN ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier') 
            THEN '?' 
            ELSE '' 
        END NullableSign,
		CASE 
            WHEN pr.is_nullable = 0 AND typ.name NOT IN ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier') 
            THEN '
		[Required]' 
            ELSE '' 
        END ReqAttr,
        CASE
            when pr.is_output = 0
            then '
        [Parameter("'+ REPLACE(REPLACE(pr.name, ' ', '_'), '@', '') + '")]'
            else '
        [Parameter("'+ REPLACE(REPLACE(pr.name, ' ', '_'), '@', '') + '", Output = true)]'
        end ParamAttr
    from sys.parameters pr
        join sys.types typ on
            pr.system_type_id = typ.system_type_id AND pr.user_type_id = typ.user_type_id
    where object_id = object_id(@ProcedureSchema + '.' + @ProcedureName)
) t
order by ParameterId

set @Result = @Result  + '
	}
}'

SELECT @Result