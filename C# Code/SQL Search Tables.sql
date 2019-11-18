	
	create table #AllObjects (DatabaseName nvarchar(128),
	                          ObjectName nvarchar(128),
	                          ObjectType nvarchar(60),
							  ObjectID int,
							  SchemaName nvarchar(128))

	create table #AllColumns (DatabaseName nvarchar(128),
	                          TableName nvarchar(128),
	                          ColumnName nvarchar(128),
							  DataType nvarchar(128),
							  ColumnLength int,
							  ColumnNo int,
							  TableObjectId int)
	
	create table #CommentText(DatabaseName nvarchar(128),
	                          ObjectName nvarchar(128),
							  ObjectId int,
							  LineId int,
							  ObjectText nvarchar(4000))

	create table #TmpObjects( ObjectId int)

	declare @ObjectName nvarchar(776) = NULL
	declare @TmpObjectName nvarchar(128)
	declare @dbname sysname
	declare @objid	int
	declare @BlankSpaceAdded   int
	declare @BasePos       int
	declare @CurrentPos    int
    declare @TextLength    int
	declare @LineId        int
	declare @AddOnLen      int
	declare @LFCR          int --lengths of line feed carriage return
	declare @DefinedLength int
	declare @SyscomText	nvarchar(4000)
	declare @Line          nvarchar(255)
	declare @DetailId int
	declare @Text nvarchar(255)

	select @DefinedLength = 255
	select @BlankSpaceAdded = 0
	select @LFCR = 2
	select @LineId = 1

	set @ObjectName = 'CAT_LU_CompanyScore'	
	
	insert into #AllObjects (DatabaseName, ObjectName, ObjectType, ObjectID, SchemaName)
	select db_name(),  AO.name, AO.type_desc, AO.object_id, S.name
	  from sys.all_objects AO inner join sys.schemas S on AO.schema_id = S.schema_id
	 where AO.name like '%' + @ObjectName + '%'

	 insert into #AllColumns (DatabaseName, TableName, ColumnName, DataType, ColumnLength, ColumnNo, TableObjectId)
	 select db_name(),
	        AO.name,
	        AC.name,
	        type_name(AC.user_type_id),
			convert(int, AC.max_length),
			AC.column_id,
			AO.object_id
		from sys.all_columns AC inner join sys.all_objects AO on AC.object_id = AO.object_id
	where AC.name like '%' + @ObjectName + '%'
	
	insert into #AllColumns (DatabaseName, TableName, ColumnName, DataType, ColumnLength, ColumnNo, TableObjectId)
	 select db_name(),
	        AO.name,
	        AC.name,
	        type_name(AC.user_type_id),
			convert(int, AC.max_length),
			AC.column_id,
			AO.object_id
		from sys.all_columns AC inner join sys.all_objects AO on AC.object_id = AO.object_id
	where AO.name in (select distinct ObjectName from #AllObjects)

	insert into #AllObjects (DatabaseName, ObjectName, ObjectType, ObjectID, SchemaName)
	select db_name(), AO.name, AO.type_desc, AO.object_id, S.name
	  from sys.all_objects AO inner join sys.schemas S on AO.schema_id = S.schema_id
	 where AO.object_id in (select distinct TableObjectId from #AllColumns where TableObjectId not in (select ObjectID from #AllObjects))

	insert into #TmpObjects(ObjectId)
	     select id
	       from syscomments
	      where text like '%' + @ObjectName + '%'

	insert into #AllObjects (DatabaseName, ObjectName, ObjectType, ObjectID, SchemaName)
	select db_name(),  AO.name, AO.type_desc, AO.object_id, S.name
	  from sys.all_objects AO inner join sys.schemas S on AO.schema_id = S.schema_id
	 where AO.object_id in (select ObjectId from #TmpObjects)

	declare ms_crs_syscom  cursor local for
			select id,
				   text
			  from syscomments 
			 where id in (select distinct ObjectId from #TmpObjects)
			   and encrypted = 0
		  order by id, number, colid
		for read only

	open ms_crs_syscom

	fetch next from ms_crs_syscom into @DetailId, @SyscomText

	while @@fetch_status >= 0
	begin

		set  @BasePos = 1
		set  @CurrentPos = 1
		set  @TextLength = len(@SyscomText)

		select @TmpObjectName = name
		  from sys.all_objects
		where object_id = @DetailId

		while @CurrentPos  != 0
		begin
			--Looking for end of line followed by carriage return
			select @CurrentPos = charindex(char(13) + char(10), @SyscomText, @BasePos)

			--If carriage return found
			if @CurrentPos != 0
			begin
				/*If new value for @Lines length will be > then the
				**set length then insert current contents of @line
				**and proceed.
				*/
				while (isnull(LEN(@Line),0) + @BlankSpaceAdded + @CurrentPos-@BasePos + @LFCR) > @DefinedLength
				begin
					select @AddOnLen = @DefinedLength-(isnull(LEN(@Line),0) + @BlankSpaceAdded)

					set @Text = isnull(@Line, N'') + isnull(SUBSTRING(@SyscomText, @BasePos, @AddOnLen), N'')

					insert into #CommentText (DatabaseName, ObjectName, ObjectId, LineId, ObjectText)
					     select db_name(), @TmpObjectName, @DetailId, @LineId, @Text
                
					select @Line = null, 
						   @LineId = @LineId + 1,
						   @BasePos = @BasePos + @AddOnLen, 
						   @BlankSpaceAdded = 0
				end

				set @Line = isnull(@Line, N'') + isnull(substring(@SyscomText, @BasePos, @CurrentPos-@BasePos + @LFCR), N'')

				select @BasePos = @CurrentPos + 2

				insert into #CommentText (DatabaseName, ObjectName, ObjectId, LineId, ObjectText)
				     select db_name(), @TmpObjectName, @DetailId, @LineId, @Line

				set @LineId = @LineId + 1
            
				set @Line = NULL
			end
			else
			--else carriage return not found
			begin
				if @BasePos <= @TextLength
				begin
					while (isnull(len(@Line),0) + @BlankSpaceAdded + @TextLength - @BasePos + 1 ) > @DefinedLength
					begin
						set @AddOnLen = @DefinedLength - (isnull(len(@Line),0) + @BlankSpaceAdded)

						set @Text = isnull(@Line, N'') + isnull(substring(@SyscomText, @BasePos, @AddOnLen), N'')

						insert into #CommentText (DatabaseName, ObjectName, ObjectId, LineId, ObjectText)
						     select db_name(), @TmpObjectName, @DetailId, @LineId, @Text

						select @Line = null, 
							   @LineId = @LineId + 1,
							   @BasePos = @BasePos + @AddOnLen, 
							   @BlankSpaceAdded = 0
					end

					select @Line = isnull(@Line, N'') + isnull(substring(@SyscomText, @BasePos, @TextLength - @BasePos + 1 ), N'')

					if len(@Line) < @DefinedLength and charindex(' ', @SyscomText, @TextLength+1 ) > 0
					begin
						select @Line = @Line + ' ', @BlankSpaceAdded = 1
					end

					IF @Line is not null
					begin
						insert into #CommentText (DatabaseName, ObjectName, ObjectId, LineId, ObjectText)
						     select db_name(), @TmpObjectName,  @DetailId, @LineId, @Line
					end

					set @Line = null
				end
			end
		end

		fetch next from ms_crs_syscom into @DetailId, @SyscomText
	end

	close  ms_crs_syscom
	deallocate 	ms_crs_syscom

	select * from #AllObjects order by ObjectName
	select distinct * from #AllColumns order by TableName, ColumnNo

	select DatabaseName, ObjectName, ObjectId, ObjectText 
	  from #CommentText 
  order by ObjectName, LineId

	drop table #CommentText
	drop table #TmpObjects
	drop table #AllObjects
	drop table #AllColumns