DECLARE @ProcedureSchema sysname = '{ProcedureSchema}'
DECLARE @ProcedureName sysname = '{ProcedureName}'

SELECT count(*) from 
    sys.dm_exec_describe_first_result_set_for_object(OBJECT_ID(@ProcedureSchema + '.' + @ProcedureName), 0) pr
    join sys.types typ on
        pr.system_type_id = typ.system_type_id AND pr.system_type_id = typ.user_type_id