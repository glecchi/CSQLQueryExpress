﻿DECLARE @ProcedureSchema sysname = '{ProcedureSchema}'
DECLARE @ProcedureName sysname = '{ProcedureName}'
DECLARE @Namespace VARCHAR(MAX) = '{Namespace}'
DECLARE @ClassName VARCHAR(MAX) = '{ClassName}'
DECLARE @Result VARCHAR(MAX) = 
'using System;

namespace ' + @Namespace + '
{
    public class ' + @ClassName + '
	{'

SELECT @Result = @Result + '
		public ' + ParameterType + NullableSign + ' ' + CASE WHEN ParameterName = @ProcedureName THEN CONCAT(ParameterName, CAST(1 AS VARCHAR(1))) ELSE ParameterName END + ' { get; set; }
'
FROM
(
    SELECT 
        REPLACE(REPLACE(pr.name, ' ', '_'), '@', '') ParameterName,
        pr.column_ordinal ParameterId,
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
        END ParameterType,
        CASE 
            WHEN pr.is_nullable = 1 AND typ.NAME IN ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier') 
            THEN '?' 
            ELSE '' 
        END NullableSign
    FROM 
	    sys.dm_exec_describe_first_result_set_for_object(OBJECT_ID(@ProcedureSchema + '.' + @ProcedureName), 0) pr
        JOIN sys.types typ ON
            pr.system_type_id = typ.system_type_id AND pr.system_type_id = typ.user_type_id
) t
ORDER BY ParameterId

SET @Result = @Result  + '
	}
}'

SELECT @Result