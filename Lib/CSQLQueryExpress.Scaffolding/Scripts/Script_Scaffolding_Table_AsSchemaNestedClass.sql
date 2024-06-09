DECLARE @TableSchema sysname = '{TableSchema}'
DECLARE @TableName sysname = '{TableName}'
DECLARE @Namespace VARCHAR(MAX) = '{Namespace}'
DECLARE @ClassName VARCHAR(MAX) = '{ClassName}'
DECLARE @Result VARCHAR(MAX) = '
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;
using CSQLQueryExpress.Schema;

namespace ' + @Namespace + '
{
    public partial class ' + @TableSchema + '
    {
	    [Table("'+ @TableName + '", Schema = "'+ @TableSchema + '")]
	    public class ' + @ClassName + ' : ISQLQueryEntity
	    {'

SELECT @Result = @Result + DbGenComputedAttr + DbGenIdentityAttr + ReqAttr + '
		    [Column("'+ ColumnName + '")]
		    public ' + ColumnType + NullableSign + ' ' + CASE WHEN ColumnName = @TableName THEN CONCAT(ColumnName, CAST(1 AS VARCHAR(1))) ELSE ColumnName END + ' { get; set; }
'
FROM
(
    SELECT 
        REPLACE(col.name, ' ', '_') ColumnName,
        column_id ColumnId,
        CASE typ.name 
            WHEN 'bigint' THEN 'long'
            WHEN 'binary' THEN 'byte[]'
            WHEN 'bit' THEN 'bool'
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
            WHEN 'sysname' THEN 'string'
            ELSE 'UNKNOWN_' + typ.name
        END ColumnType,
        CASE 
            WHEN col.is_nullable = 1 AND typ.name IN ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier') 
            THEN '?' 
            ELSE '' 
        END NullableSign,
		CASE 
            WHEN col.is_nullable = 0 AND typ.name NOT IN ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier') 
            THEN '
		    [Required]' 
            ELSE '' 
        END ReqAttr,
        CASE WHEN col.is_identity = 1
            THEN'
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]'
            ELSE ''
        END DbGenIdentityAttr,
        CASE WHEN col.is_computed = 1
            THEN'
		    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]'
            ELSE ''
        END DbGenComputedAttr
    FROM sys.columns col
        JOIN sys.types typ ON
            col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id
    WHERE object_id = OBJECT_ID(@TableSchema + '.' + @TableName) 
) t
ORDER BY ColumnId

SET @Result = @Result  + '
	    }
    }
}'

SELECT @Result