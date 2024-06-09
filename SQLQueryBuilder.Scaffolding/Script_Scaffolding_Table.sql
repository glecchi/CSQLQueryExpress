DECLARE @TableSchema sysname = '{TableSchema}'
DECLARE @TableName sysname = '{TableName}'
DECLARE @Namespace sysname = '{Namespace}'
DECLARE @ClassName sysname = '{ClassName}'
DECLARE @Result varchar(max) = '
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace ' + @Namespace + '
{
    public partial class ' + @TableSchema + '
    {
	    [Table("'+ @TableName + '", Schema = "'+ @TableSchema + '")]
	    public class ' + @ClassName + ' : ISQLQueryEntity
	    {'

select @Result = @Result + DbGenComputedAttr + DbGenIdentityAttr + ReqAttr + '
		    [Column("'+ ColumnName + '")]
		    public ' + ColumnType + NullableSign + ' ' + case when ColumnName = @TableName then concat(ColumnName, cast(1 as varchar(1))) else ColumnName end + ' { get; set; }
'
from
(
    select 
        replace(col.name, ' ', '_') ColumnName,
        column_id ColumnId,
        case typ.name 
            when 'bigint' then 'long'
            when 'binary' then 'byte[]'
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
        end ColumnType,
        case 
            when col.is_nullable = 1 and typ.name in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier') 
            then '?' 
            else '' 
        end NullableSign,
		case 
            when col.is_nullable = 0 and typ.name NOT in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier') 
            then '
		    [Required]' 
            else '' 
        end ReqAttr,
        case when col.is_identity = 1
            then'
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]'
            else ''
        END DbGenIdentityAttr,
        case when col.is_computed = 1
            then'
		    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]'
            else ''
        END DbGenComputedAttr
    from sys.columns col
        join sys.types typ on
            col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id
    where object_id = object_id(@TableSchema + '.' + @TableName) 
) t
order by ColumnId

set @Result = @Result  + '
	    }
    }
}'

SELECT @Result