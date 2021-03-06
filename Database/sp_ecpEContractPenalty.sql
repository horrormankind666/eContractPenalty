USE [Infinity]
GO
/****** Object:  StoredProcedure [dbo].[sp_ecpEContractPenalty]    Script Date: 23/7/2564 10:53:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author		:	<Anussara Wanwang>
-- Create date	:	<20-03-2015>
-- Description	:	<Description,,>
-- =============================================
ALTER procedure [dbo].[sp_ecpEContractPenalty]
(
	@ordertable int = null,	
	@startrow int = null,
	@endrow int = null,
	@section char(1)= null,
	@userid varchar(50) = null,
	@username varchar(50) = null,
	@password varchar(50) = null,
	@userlevel varchar(50) = null,
	@usernameold varchar(50) = null,
	@passwordold varchar(50) = null,	
	@name varchar(100) = null,
	@studentid varchar(100) = null,
	@cp1id varchar(10) = null,
	@cp2id varchar(10) = null,
	@cmd varchar(max) = null,
	@usecontractinterest varchar(1) = null,
	@casegraduate varchar(1) = null,
	@dlevel varchar(1) = null,
	@faculty varchar(2) = null,
	@program varchar(5) = null,
	@major varchar(4) = null,
	@groupnum varchar(1) = null,
	@statussend varchar(1) = null,
	@statusreceiver varchar(1) = null,
	@statusedit varchar(1) = null,
	@statuscancel varchar(1) = null,
	@statusrepay varchar(1) = null,
	@statusreply varchar(1) = null,
	@replyresult varchar(1) = null,
	@formatpayment varchar(1) = null,
	@statuspayment varchar(1) = null,
	@datestart varchar(10) = null,
	@dateend varchar(10) = null,
	@actioncomment varchar(1) = null,
	@capital varchar(20) = null,
	@interest varchar(20) = null,
	@pay varchar(20) = null,
	@paiddate varchar(10) = null,
	@statusstepofwork varchar(2) = null,
	@acadamicyear varchar(4) = null
)
as
begin
	declare @sql varchar(max) = null
	declare @where varchar (1000) = null
	declare @where1 varchar (1000) = null
	declare @order varchar (100) = null
	declare @nostudentid varchar (400) = null
	declare @nostudentstatus varchar (400) = null
	declare @dlevelfix varchar(400) = null
	declare @trackingstatusoraa varchar(400) = null
	declare @trackingstatusorla varchar(400) = null
	declare @trackingstatusorfa varchar(400) = null
	declare @repaystatus1 varchar(400) = null
	declare @repaystatus2 varchar(400) = null
	
	set @nostudentid = '(left(std.studentCode, 2) not in ("00", "11", "22", "97", "98", "99"))'
	set @nostudentstatus =  '(std.status not between 103 and 108)'
	set @dlevelfix = '(cp.dLevel in ("U", "B"))'
	set @trackingstatusoraa = '(convert(varchar, cptbc.StatusSend) + convert(varchar, cptbc.StatusReceiver) + convert(varchar, cptbc.StatusEdit) + convert(varchar, cptbc.StatusCancel)) in ("1111", "1112", "2111", "2121", "2122", "2211", "2212")'
	set @trackingstatusorla = '(convert(varchar, cptbc.StatusSend) + convert(varchar, cptbc.StatusReceiver) + convert(varchar, cptbc.StatusEdit) + convert(varchar, cptbc.StatusCancel)) in ("2111", "2121", "2122", "2211", "2212")'
	set @trackingstatusorfa = '(convert(varchar, cptbc.StatusSend) + convert(varchar, cptbc.StatusReceiver) + convert(varchar, cptbc.StatusEdit) + convert(varchar, cptbc.StatusCancel)) in ("2211")'
	set @repaystatus1 = '(convert(varchar, cptbc.StatusSend) + convert(varchar, cptbc.StatusReceiver) + convert(varchar, cptbc.StatusEdit) + convert(varchar, cptbc.StatusCancel)) in ("2211", "2212")'
	set @repaystatus2 = '(convert(varchar, cptbc.StatusSend) + convert(varchar, cptbc.StatusReceiver) + convert(varchar, cptbc.StatusEdit) + convert(varchar, cptbc.StatusCancel)) in ("2211")'
	
	--ตรวจสอบสิทธิ์ผู้ใช้งานว่าเป็นเจ้าหน้าที่กองทะเบียนหรือเจ้าหน้าที่กองกฏหมาย
	if (@ordertable = 1)
	begin
		set @sql = 'select * from ecpTabUser where (Username = "' + @username + '") and (Password = "' + @password + '")'

		exec (@sql)
	end
	
	--select ecpTabUser
	if (@ordertable = 36)
	begin
		set @where = ''
				
		if (@userlevel <> null)
			set @where = '(cptu.UserLevel = "' + @userlevel + '")'
		
		if (@userid <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '

			set @where = @where + '(cptu.ID = "' + @userid + '")'
		end
		
		if ((@username <> null) and
			(@password <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '

			set @where = @where + '(cptu.Username = "' + @username + '") and (cptu.Password = "' + @password + '")'
		end
				
		if (@name <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptu.Name like "%' + @name + '%")'
		end
				
		if (@where <> '') 
			set @where = ' and (' + @where + ')'
				
		set @sql = 'select	count(cptu.ID) as CountCPTabUser
					from	ecpTabUser as cptu
					where	(cptu.UserSection = "' + @section + '")' + @where

		exec (@sql)

		set @sql = 'select	*
					from	(
								select	row_number() over(order by cptu.Username) as RowNum,
										cptu.ID,
										cptu.Username,
										cptu.Password,
										cptu.Name,
										replace(cptu.PhoneNumber, "-", " ") as PhoneNumber,
										replace(cptu.MobileNumber, "-", " ") as MobileNumber,
										cptu.Email,
										cptu.UserSection,
										cptu.UserLevel
								from	ecpTabUser as cptu
								where	(cptu.UserSection = "' + @section + '")' + @where + '
							) as cptu1'

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (cptu1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)
	end
	
	--select ecpTabUser on repeat
	IF (@ordertable = 37)
	begin
		set @where = ''	
			
		if ((@username <> null) and
			(@password = null))
		begin
			set @where = ' where (cptu.Username = "' + @username + '")'
			
			if (@usernameold <> null)
				set @where = @where + ' and (cptu.Username <> "' + @usernameold + '")'
		end
			
		if ((@username = null) and
			(@password <> null))
		begin
			set @where = ' where (cptu.Password = "' + @Password + '")'
			
			if (@passwordold <> null)
				set @where = @where + ' and (cptu.Password <> "' + @passwordold + '")'
		end

		set @sql = 'select	cptu.Username,
							cptu.Password 
				    from	ecpTabUser as cptu'
		set @sql = @sql + @where

		exec (@sql)
	end
	
	--select ecpTabCalDate
	if (@ordertable = 2)
	begin
		set @where = ''	
			
		if (@cp1id <> null)
			set @where = ' where (ecptcd.ID = ' + @cp1id + ')'
					
		set @sql = 'select	ecptcd.ID,
							ecptcd.CalDateCondition,
							ecptcd.PenaltyFormula 
				    from	ecpTabCalDate AS ecptcd'
		set @sql = @sql + @where					   
		set @sql = @sql + ' order by ecptcd.ID'

		exec (@sql)
	end
	
	--select ecpTabInterest
	if (@ordertable = 3)
	begin
		set @where = ''		
		
		if (@cp1id <> null)
			set @where = ' where (cpti.ID = ' + @cp1id + ')'

		if (@usecontractinterest <> null)
			set @where = ' where (cpti.UseContractInterest = ' + @usecontractinterest + ')'
			
		set @sql = 'select	cpti.ID,
							cpti.InContractInterest,
							cpti.OutContractInterest,
							cpti.UseContractInterest
				    from	ecpTabInterest as cpti'
		set @sql = @sql + @where					   
		set @sql = @sql + ' order by cpti.ID'

		exec (@sql)
	end

	--select ecpTabPayBreakContract
	if (@ordertable = 4)
	begin
		set @where = ''	
			
		if (@cp1id <> null)
			set @where = ' where (cptpbc.ID = ' + @cp1id + ')'

		set @sql = 'select	cptpbc.ID,
							cptpbc.FacultyCode,
							bf.nameTh as FactTName,
							cptpbc.ProgramCode,
							cp.nameTh as ProgTName,
							cptpbc.MajorCode,
							cptpbc.GroupNum,
							cptpbc.AmountCash,
							cptpbc.Dlevel, 
							(
								case cptpbc.Dlevel
									when "U" then "ต่ำกว่าปริญญาตรี"
									when "B" then "ปริญญาตรี"
								else
									null
								end
							) as DlevelName,														
							cptpbc.CaseGraduate, 
							(
								case cptpbc.CaseGraduate
									when "1" then "ก่อนสำเร็จการศึกษา"
									when "2" then "หลังสำเร็จการศึกษา"
								else
									null
								end
							) as CaseGraduateName,																					
							cptpbc.CalDateCondition as IDCalDate,
							cptcd.CalDateCondition,
							cptcd.PenaltyFormula,
							cptpbc.AmtIndemnitorYear
				    from	ecpTabPayBreakContract as cptpbc inner join
							acaFaculty as bf on cptpbc.FacultyCode = bf.facultyCode inner join
							ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cptpbc.ProgramCode + cptpbc.MajorCode + cptpbc.GroupNum + cptpbc.Dlevel) = (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) inner join
							acaProgram as cp on bf.id = cp.facultyId and (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) = (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel) inner join
							ecpTabCalDate as cptcd on cptpbc.CalDateCondition = cptcd.ID'
		set @sql = @sql + @where					   
		set @sql = @sql + ' order by cptpbc.CaseGraduate, DlevelName, cptpbc.ProgramCode'
		
		exec (@sql)
	end

	--select faculty
	if (@ordertable = 5)
	begin
		set @sql = 'select	bf.id,
							bf.FacultyCode,
							bf.nameTh as FactTName
				    from	acaFaculty as bf'
		set @sql = @sql + ' order by bf.FacultyCode'
		
		exec (@sql)
	end

	--select program on faculty
	if (@ordertable = 6)
	begin
		set @where = ''		
		
		if ((@dlevel = null) and
			(@faculty <> null))
			set @where = ' where (' + @dlevelfix + ') and (bf.facultyCode = "' + @faculty + '")'

		if ((@dlevel <> null) and
			(@faculty <> null))
			set @where = ' where (' + @dlevelfix + ') and (bf.facultyCode = "' + @faculty + '") and (cp.dLevel = "' + @dlevel + '")'

		set @sql = 'select	cp.id,
							cp.ProgramCode,
							cp.MajorCode,
							cp.GroupNum,
							cp.nameTh as ProgTName,
							cp.dLevel,
							(
								case cp.dLevel
									when "U" then "ต่ำกว่าปริญญาตรี"
									when "B" then "ปริญญาตรี"
								else
									null
								end
							) as DLevelName		
				    from	acaProgram as cp inner join
							acaFaculty as bf on cp.facultyId = bf.id'
		set @sql = @sql + @where				    
		set @sql = @sql + ' order by cp.ProgramCode'

		exec (@sql)
	end

	--selectT ecpTabPayBreakContract on repeat
	if (@ordertable = 7)
	begin
		set @where = ''	
			
		if ((@casegraduate <> null) and
			(@dlevel <> null) and
			(@faculty <> null) and
			(@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			set @where = ' where ((cptpbc.CaseGraduate = ' + @casegraduate + ') and
								  (cptpbc.Dlevel = "' + @dlevel + '") and
								  (cptpbc.FacultyCode = "' + @faculty + '") and
								  (cptpbc.ProgramCode = "' + @program + '") and
								  (cptpbc.MajorCode = "' + @major + '") and
								  (cptpbc.GroupNum = "' + @groupnum + '"))'
			
			if (@cp1id <> null)
				set @where = @where + ' and (cptpbc.ID <> ' + @cp1id + ')'
		end
		
		set @sql = 'select	cptpbc.ID,
							cptpbc.CalDateCondition,
							cptpbc.AmtIndemnitorYear,
							cptpbc.AmountCash
				    from	ecpTabPayBreakContract as cptpbc inner join
							acaFaculty as bf on cptpbc.FacultyCode = bf.facultyCode inner join
							ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cptpbc.ProgramCode + cptpbc.MajorCode + cptpbc.GroupNum + cptpbc.Dlevel) = (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) inner join
							acaProgram as cp on bf.id = cp.facultyId and (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) = (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel) inner join
							ecpTabCalDate as cptcd on cptpbc.CalDateCondition = cptcd.ID'
		set @sql = @sql + @where					   
		set @sql = @sql + ' order by cptpbc.ID'

		exec (@sql)
	end

	--select stdStudent
	if (@ordertable = 8)
	begin
		set @where = ''

		if (@studentid <> null)
			set @where = ' and ((std.studentCode like "' + @studentid + '%") or (perpes.firstName like "%' + @studentid + '%") or (perpes.lastName like "%' + @studentid + '%"))'
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where		
			
			set @where = @where + ' and (cptpg.FacultyCode = "' + @faculty + '")'
		end

		if ((@program <> null) and 
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where
			
			set @where = @where + ' and ((cptpg.ProgramCode = "' + @program + '") and (cptpg.MajorCode = "' + @major + '") and (cptpg.GroupNum = "' + @groupnum + '"))'
		end

		if (@where <> '') 
			set @where = @where

		set @where = ' where ' + @nostudentid + @where

		set @sql = 'select	count(std.id) as CountStudent
					from	stdStudent as std left join
							acaFaculty as bf on std.facultyId = bf.id left join
							acaProgram as cp on std.facultyId = cp.facultyId and std.programId = cp.id inner join
							perPerson as perpes on std.personId = perpes.id left join
							perTitlePrefix as bt on perpes.perTitlePrefixId = bt.id inner join
							ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel) = (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) left join
							ectDocMgmt as cd on std.id = cd.StudentID and std.programId = cd.programId'
		set @sql = @sql + @where					

		exec (@sql)

		set @sql = 'select	*
					from	(
								select	row_number() over(order by std.id) as RowNum,
										std.studentCode as StudentID,
										perpes.perTitlePrefixId as TitleCode,
										bt.enTitleInitials as TitleEName,
										bt.thTitleFullName as TitleTName,
										perpes.enFirstName as FirstName,
										perpes.enLastName as LastName,
										perpes.firstName as ThaiFName,
										perpes.lastName as ThaiLName,
										cptpg.FacultyCode,
										bf.nameTh as FactTName,
										cptpg.ProgramCode,
										cp.nameTh as ProgTName,
										cptpg.MajorCode,
										cptpg.GroupNum,
										cptpg.DLevel,
										(
											case cptpg.DLevel
												when "U" then "ต่ำกว่าปริญญาตรี"
												when "B" then "ปริญญาตรี"
											else
												null
											end
										) as DLevelName,														
										(substring(convert(varchar, std.admissionDate, 103), 1, 6) + convert(varchar, convert(int, substring(convert(varchar, std.admissionDate, 103), 7, 4)) + 543)) as AdmissionDate,
										(substring(convert(varchar, std.graduateDate, 103), 1, 6) + convert(varchar, convert(int, substring(convert(varchar, std.graduateDate, 103), 7, 4)) + 543)) as GraduateDate,
										(substring(convert(varchar, cd.contractDateSignByStudent, 103), 1, 6) + convert(varchar, convert(int, substring(convert(varchar, cd.contractDateSignByStudent, 103), 7, 4)) + 543)) as ContractDate,
										(substring(convert(varchar, cd.contractDateSignByParent2, 103), 1, 6) + convert(varchar, convert(int, substring(convert(varchar, cd.contractDateSignByParent2, 103), 7, 4)) + 543)) as ContractDateAgreement,
										vwp.TitleTName as GuarantorTitleTName,
										vwp.FirstName as GuarantorFirstName,
										vwp.LastName as GuarantorLastName
								from	stdStudent as std left join
										acaFaculty as bf on std.facultyId = bf.id left join
										acaProgram as cp on std.facultyId = cp.facultyId and std.programId = cp.id inner join
										perPerson as perpes on std.personId = perpes.id left join
										perTitlePrefix as bt on perpes.perTitlePrefixId = bt.id inner join
										ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel) = (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) left join
										ectDocMgmt as cd on std.id = cd.StudentID and std.programId = cd.programId left join
										vw_ectGetListParent as vwp on cd.StudentID = vwp.id and cd.parentCode = vwp.Type ' + @where + '
							) as std1 '

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (std1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)

		if ((@studentid <> null) and
			(@faculty = null) and
			(@program = null) and
			(@major = null) and
			(@groupnum = null))
		begin
			set @where = ' where spl.studentCode = "' + @studentid + '"'
			set @sql = 'select	distinct
								spl.studentCode,
								spl.pictureName as FileName,
								"" as FolderName
						from	stdStudent as spl'
			set @sql = @sql + @where					   					
			set @sql = @sql + ' order by spl.studentCode'
			
			exec (@sql)
		end
	end

	--select student in ecpTransBreakContract
	if (@ordertable = 9)
	begin
		set @where = ''
	
		if (@studentid <> null)
			set @where = ' where (cptbc.StudentID = "' + @studentid + '") and (cptbc.StatusCancel = 1)'
		
		set @sql = 'select	cptbc.ID,
							cptbc.StudentID 
					from	ecpTransBreakContract as cptbc'
		set @sql = @sql + @where
		
		exec (@sql)
	end
	
	--select ecpTransBreakContract
	if (@ordertable = 10)
	begin
		set @where = ''
		set @order = ''
	
		if (@cp1id <> null)
			set @where = '(cptbc.ID = ' + @cp1id + ')'

		if ((@statussend <> null) and
			(@statusedit <> null) and
			(@statuscancel <> null))
			set @where = '(((cptbc.StatusSend = ' + @statussend + ') and (cptbc.StatusReceiver = 1) and (cptbc.StatusEdit = 1) and (cptbc.StatusCancel = 1)) or
						   ((cptbc.StatusSend = 2) and (cptbc.StatusReceiver = 1) and (cptbc.StatusEdit = ' + @statusedit + ') and (cptbc.StatusCancel = 1)) or
						   (cptbc.StatusCancel = ' + @statuscancel + '))'
		else
			begin
				if (@statussend <> null)
					set @where = '((cptbc.StatusSend = ' + @statussend + ') and (cptbc.StatusReceiver = 1) and (cptbc.StatusEdit = 1) and (cptbc.StatusCancel = 1))'

				if (@statusreceiver <> null)
					set @where = '((cptbc.StatusSend = 2) and (cptbc.StatusReceiver = ' + @statusreceiver + ') and (cptbc.StatusEdit = 1) and (cptbc.StatusCancel = 1))'
		
				if (@statusedit <> null)
					set @where = '((cptbc.StatusSend = 2) and (cptbc.StatusReceiver = 1) and (cptbc.StatusEdit = ' + @statusedit + ') and (cptbc.StatusCancel = 1))'

				if (@statuscancel <> null)
					set @where = '(cptbc.StatusCancel = ' + @statuscancel + ')'
			end
		
		if (@studentid <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '

			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		end
		
		if(@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end
		
		if ((@datestart <> null) and 
			(@dateend <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '

			if (@section = '1')
				set @where = @where + '(convert(date, cptbc.DateTimeSend) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" and "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'
			
			if (@section = '2')
				set @where = @where + '(convert(date, cptbc.DateTimeCreate) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" and "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'
		end
		
		if (@where <> '') 
			set @where = ' where (' + @where + ') and '
		else
			set @where = ' where '
		
		if (@section = '1')
			set @where = @where + @trackingstatusorla

		if (@section = '2')
			set @where = @where + @trackingstatusoraa

		if (@section = '3')
			set @where = @where + @trackingstatusorfa		
		
		set @sql = 'select	count(cptbc.ID) as CountCPTransBreakContract
					from	ecpTransBreakContract as cptbc'
		set @sql = @sql + @where	

		exec (@sql)

		if (@section = '1')
			set @order = 'cptbc.DateTimeSend'

		if (@section = '2')
			set @order = 'cptbc.DateTimeCreate'

		if (@section = '3')
			set @order = 'cptbc.StudentID'
		
		set @sql = 'select	*
					from	(
								select	row_number() over(order by ' + @order + ' desc) as RowNum,
										cptbc.ID,
										cptbc.StudentID,
										cptbc.TitleCode,
										cptbc.TitleEName,
										cptbc.TitleTName,
										cptbc.FirstEName,
										cptbc.LastEName,
										cptbc.FirstTName,
										cptbc.LastTName, 
										cptbc.FacultyCode,
										cptbc.FacultyName,
										cptbc.ProgramCode,
										cptbc.ProgramName,
										cptbc.MajorCode,
										cptbc.GroupNum,
										cptbc.DLevel,
										(
											case cptbc.DLevel
												when "U" then "ต่ำกว่าปริญญาตรี"
												when "B" then "ปริญญาตรี"
											else
												null
											end
										) as DLevelName,																							
										cptbc.PursuantBook,
										cptbc.Pursuant,
										cptbc.PursuantBookDate,
										cptbc.Location, 
										cptbc.InputDate,
										cptbc.StateLocation,
										cptbc.StateLocationDate,
										cptbc.ContractDate,
										cptbc.ContractDateAgreement,
										cptbc.Guarantor,
										cptbc.ScholarFlag,
										cptbc.ScholarshipMoney,
										cptbc.ScholarshipYear,
										cptbc.ScholarshipMonth,
										cptbc.EducationDate,
										cptbc.GraduateDate,
										cptbc.ContractForceStartDate,
										cptbc.ContractForceEndDate,
										cptbc.CaseGraduate,
										cptbc.CivilFlag,
										cptbc.CalDateCondition,
										cptbc.IndemnitorYear,
										cptbc.IndemnitorCash,
										cptbc.StatusSend,
										cptbc.StatusReceiver,
										cptbc.StatusEdit,
										cptbc.StatusCancel,
										cptbc.DateTimeCreate,
										cptbc.DateTimeModify,
										cptbc.DateTimeCancel,
										cptbc.DateTimeSend,
										cptbc.DateTimeReceiver,
										spl1.FileName,
										spl1.FolderName,
										(case when (cptpbc.AmtIndemnitorYear <> 0) then "Y" else "N" end) as SetAmtIndemnitorYear
								from	ecpTransBreakContract as cptbc left join
										ecpTabPayBreakContract as cptpbc on cptbc.FacultyCode = cptpbc.FacultyCode and (cptbc.ProgramCode + cptbc.MajorCode + cptbc.GroupNum + cptbc.DLevel) = (cptpbc.ProgramCode + cptpbc.MajorCode + cptpbc.GroupNum + cptpbc.Dlevel) and (cptbc.CaseGraduate = cptpbc.CaseGraduate) and (cptbc.CalDateCondition = cptpbc.CalDateCondition) left join 
										(
											select	distinct
													spl.studentCode as StudentID,
													spl.pictureName as FileName,
													"" as FolderName
											from	stdStudent as spl
										) as spl1 on cptbc.StudentID = spl1.StudentID' + @where + ' 
							) as cptbc1'		

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (cptbc1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)
	end

	--select province
	if (@ordertable = 11)
	begin
		set @sql = 'select	 bp.id as ProvinceID,
							 bp.provinceNameTH as ProvinceTName
				    from	 plcProvince as bp
					where	 (bp.plcCountryId = "217")
					order by bp.provinceNameTH'
		
		exec (@sql)
	end

	--select ecpTransRequireContract*/
	if (@ordertable = 12)
	begin
		set @where = ''
		
		if (@cp1id <> null)
			set @where = '(cptbc.ID = ' + @cp1id + ')'

		if (@statusrepay <> null)
			set @where = '(cptrc.StatusRepay = ' + @statusrepay + ')'
		
		if (@statusreply <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '

			set @where = @where + '(cptrp.StatusReply = ' + @statusreply + ')'
		end

		if (@replyresult <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptrp.ReplyResult = ' + @replyresult + ')'
		end

		if (@statuspayment <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptrc.StatusPayment = ' + @statuspayment + ')'
		end

		if (@studentid <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		end
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end
		
		if ((@datestart <> null) and
			(@dateend <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(convert(date, cptbc.DateTimeReceiver) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" and "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'
		end
		
		if (@where <> '') 
			set @where = ' where (' + @where + ') and '
		else
			set @where = ' where '
		
		if (@cp1id <> null)
			set @where = @where + @repaystatus1
		else
			set @where = @where + @repaystatus2			
		
		set @sql = 'select	count(cptrc.ID) as CountRepay
					from	ecpTransRequireContract as cptrc left join
							ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
							plcProvince as bp on cptrc.Province = bp.id inner join
							ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID'
		set @sql = @sql + @where

		exec (@sql)
		
		set @sql = 'select	*
					from	(
								select	row_number() over(order by cptbc.DateTimeReceiver desc) as RowNum,
										cptrc.ID,
										cptrc.BCID,
										isnull(cptrc.StudyLeave, "N") as StudyLeave,
										cptrc.IndemnitorAddress,
										bp.id as ProvinceID,
										bp.provinceNameTH as ProvinceTName,
										cptrc.RequireDate,
										cptrc.ApproveDate,
										cptrc.BeforeStudyLeaveStartDate,
										cptrc.BeforeStudyLeaveEndDate,
										cptrc.StudyLeaveStartDate,
										cptrc.StudyLeaveEndDate,
										cptrc.AfterStudyLeaveStartDate,
										cptrc.AfterStudyLeaveEndDate,
										cptrc.ActualMonthScholarship,
										cptrc.ActualScholarship,
										cptrc.TotalPayScholarship,
										cptrc.ActualMonth,
										cptrc.ActualDay,
										cptrc.AllActualDate,
										cptrc.ActualDate,
										cptrc.RemainDate,
										cptrc.SubtotalPenalty,
										cptrc.TotalPenalty,
										cptrc.LawyerFullname,
										replace(cptrc.LawyerPhoneNumber, "-", " ") as LawyerPhoneNumber,
										replace(cptrc.LawyerMobileNumber, "-", " ") as LawyerMobileNumber,
										cptrc.LawyerEmail,
										cptrc.StatusRepay,
										cptrc.StatusPayment,	
										cptrp.RepayDate,
										cptrp.StatusReply,
										cptrp.ReplyResult,
										cptrp.ReplyDate,										
										cptbc.StudentID,
										cptbc.TitleTName,
										cptbc.FirstTName,
										cptbc.LastTName, 
										cptbc.FacultyCode,
										cptbc.FacultyName,
										cptbc.ProgramCode,
										cptbc.ProgramName,
										cptbc.MajorCode,
										cptbc.GroupNum,
										cptbc.DLevel,
										(
											case cptbc.DLevel
												when "U" then "ต่ำกว่าปริญญาตรี"
												when "B" then "ปริญญาตรี"
											else
												null
											end
										) as DLevelName,																																
										cptbc.PursuantBook,
										cptbc.Pursuant,
										cptbc.PursuantBookDate,
										cptbc.Location,
										cptbc.InputDate,
										cptbc.StateLocation,
										cptbc.StateLocationDate,
										cptbc.ContractDate,
										cptbc.ContractDateAgreement,
										cptbc.Guarantor,
										cptbc.ScholarFlag,
										cptbc.ScholarshipMoney,
										cptbc.ScholarshipYear,
										cptbc.ScholarshipMonth,
										cptbc.EducationDate,
										cptbc.GraduateDate,
										cptbc.ContractForceStartDate,
										cptbc.ContractForceEndDate,
										cptbc.CaseGraduate,
										cptbc.CivilFlag,
										cptbc.CalDateCondition,
										cptbc.IndemnitorYear,
										cptbc.IndemnitorCash,
										cptbc.StatusSend,
										cptbc.StatusReceiver,
										cptbc.StatusEdit,
										cptbc.StatusCancel,
										cptbc.DateTimeCreate,
										cptbc.DateTimeModify,
										cptbc.DateTimeCancel,
										cptbc.DateTimeSend,
										cptbc.DateTimeReceiver,
										spl1.FileName,
										spl1.FolderName,
										(case when (cptpbc.AmtIndemnitorYear <> 0) then "Y" else "N" end) as SetAmtIndemnitorYear
								from	ecpTransRequireContract as cptrc left join
										ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
										plcProvince as bp on cptrc.Province = bp.id inner join
										ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID left join
										ecpTabPayBreakContract as cptpbc on cptbc.FacultyCode = cptpbc.FacultyCode and (cptbc.ProgramCode + cptbc.MajorCode + cptbc.GroupNum + cptbc.DLevel) = (cptpbc.ProgramCode + cptpbc.MajorCode + cptpbc.GroupNum + cptpbc.Dlevel) and (cptbc.CaseGraduate = cptpbc.CaseGraduate) and (cptbc.CalDateCondition = cptpbc.CalDateCondition) left join 
										(
											select	distinct
													spl.studentCode as StudentID,
													spl.pictureName as FileName,
													"" as FolderName
											from	stdStudent as spl
										) as spl1 on cptbc.StudentID = spl1.StudentID' + @where + ' 
							) as cptrc1'

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (cptrc1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)
	end
	
	--search status repay detail
	if (@ordertable = 13)
	begin
		set @where = '(cptrc.StatusRepay <> 0) and (cptrc.StatusPayment <> 2) and (cptrc.StatusPayment <> 3) '
		
		if (@cp2id <> null)
			set @where = @where + 'and (cptrp.RCID = ' + @cp2id + ') '

		if (@where <> '') 
			set @where = ' where (' + @where + ') and '
		else
			set @where = ' where '
		
		set @where = @where + @repaystatus1

		set @sql = 'select	cptrp.RCID,
							cptrp.StatusRepay,
							cptrp.StatusReply,
							cptrp.ReplyResult
					from	ecpTransRepayContract as cptrp inner join
							ecpTransRequireContract as cptrc on cptrp.RCID = cptrc.ID inner join
							ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID '
		set @sql = @sql + @where
		set @sql = @sql + ' order by cptrp.RCID, cptrp.StatusRepay'
		
		exec (@sql)
	end
	
	--select CPTabScholarship*/
	if (@ordertable = 14)
	begin
		set @where = ''	
			
		if (@cp1id <> null)
			set @where = ' where (cptss.ID = ' + @cp1id + ')'

		set @sql = 'select	cptss.ID,
							cptss.FacultyCode,
							bf.nameTh as FactTName,
							cptss.ProgramCode,
							cp.nameTh as ProgTName,
							cptss.MajorCode,
							cptss.GroupNum,
							cptss.ScholarshipMoney,
							cptss.Dlevel, 
							(
								case cptss.Dlevel
									when "U" then "ต่ำกว่าปริญญาตรี"
									when "B" then "ปริญญาตรี"
								else
									null
								end
							) as DlevelName																			
				    from	ecpTabScholarship as cptss inner join
							acaFaculty as bf on cptss.FacultyCode = bf.facultyCode inner join
							ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cptss.ProgramCode + cptss.MajorCode + cptss.GroupNum + cptss.Dlevel) = (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) inner join
							acaProgram as cp on bf.id = cp.facultyId and (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) = (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel) '
		set @sql = @sql + @where
		set @sql = @sql + ' order by DlevelName, cptss.FacultyCode, cptss.ProgramCode'
		
		exec (@sql)
	end

	--select ecpTabScholarship on repeat
	if (@ordertable = 15)
	begin
		set @where = ''	
			
		if ((@dlevel <> null) and
			(@faculty <> null) and
			(@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			set @where = ' where ((cptss.Dlevel = "' + @dlevel + '") and
								  (cptss.FacultyCode = "' + @faculty + '") and
								  (cptss.ProgramCode = "' + @program + '") and
								  (cptss.MajorCode = "' + @major + '") and
								  (cptss.GroupNum = "' + @groupnum + '"))'							  
			
			if (@cp1id <> null)
				set @where = @where + ' and (cptss.ID <> ' + @cp1id + ')'
		end
		
		set @sql = 'select	cptss.ID, cptss.ScholarshipMoney
				    from	ecpTabScholarship as cptss inner join
							acaFaculty as bf on cptss.FacultyCode = bf.facultyCode inner join
							ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cptss.ProgramCode + cptss.MajorCode + cptss.GroupNum + cptss.Dlevel) = (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) inner join
							acaProgram as cp on bf.id = cp.facultyId and (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) = (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel) '
		set @sql = @sql + @where					   
		set @sql = @sql + ' order by cptss.ID'

		exec (@sql)
	end

	--select ecpTransRepayContract*/
	if (@ordertable = 16)
	begin
		set @where = ''
		
		if (@cp2id <> null)
			set @where = '(cptrc.ID = ' + @cp2id + ')'
		
		if (@where <> '') 
			set @where = ' where (' + @where + ') and '
		else
			set @where = ' where '
		
		set @where = @where + @repaystatus2
		set @sql = 'select	cptrc.ID,
							cptrc.BCID,
							cptrc.SubtotalPenalty,
							cptrc.TotalPenalty,
							cptrc.StatusRepay,
							cptrp.StatusReply,
							cptrp.ReplyResult,
							cptrp.RepayDate,
							cptrp.ReplyDate,
							cptrp.Pursuant,
							cptrp.PursuantBookDate,
							cptrc.StatusPayment
					from	ecpTransRepayContract as cptrp inner join
							ecpTransRequireContract as cptrc on cptrp.RCID = cptrc.ID and cptrp.StatusRepay = cptrc.StatusRepay inner join
							ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID'
		set @sql = @sql + @where
		set @sql = @sql + ' order by cptrp.RCID, cptrp.StatusRepay'
		
		exec (@sql)
	end

	--search max reply date
	if (@ordertable = 17)
	begin
		set @where = ''
		
		if (@cp2id <> null)
			set @where = '(cptrp.RCID = ' + @cp2id + ') '

		if (@where <> '') 
			set @where = ' where ' + @where
		
		set @sql = 'select	cptrp.RCID,
							cptrp.StatusRepay,
							cptrp.RepayDate,
							cptrp.StatusReply,
							cptrp.ReplyResult,
							cptrp.ReplyDate
					from	ecpTransRepayContract as cptrp inner join
							(
								select	 cptrp.RCID,
										 max(cptrp.StatusRepay) as StatusRepay
								from	 ecpTransRepayContract as cptrp
								group by cptrp.RCID
							) as cptrp1 on cptrp.RCID = cptrp1.RCID and cptrp.StatusRepay = cptrp1.StatusRepay'
		set @sql = @sql + @where
		set @sql = @sql + ' order by cptrp.RCID, cptrp.StatusRepay'

		exec (@sql)
	end

	--select ecpTransRepayContract no current status repay
	if (@ordertable = 18)
	begin
		set @where = ''
		
		if ((@cp2id <> null) and
			(@statusrepay <> null))
			set @where = '(cptrp.RCID = ' + @cp2id + ') and (cptrp.StatusRepay <> ' + @statusrepay + ')'
		
		if (@where <> '') 
			set @where = ' where (' + @where + ') and '
		else
			set @where = ' where '
		
		set @where = @where + @repaystatus2
		set @sql = 'select	cptrc.ID,
							cptrc.BCID,
							cptrp.StatusRepay,
							cptrp.StatusReply,
							cptrp.ReplyResult,
							cptrp.RepayDate,
							cptrp.ReplyDate,
							cptrp.Pursuant,
							cptrp.PursuantBookDate
					from	ecpTransRepayContract as cptrp inner join
							ecpTransRequireContract as cptrc on cptrp.RCID = cptrc.ID inner join
							ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID'
		set @sql = @sql + @where
		set @sql = @sql + ' order by cptrp.StatusRepay'
		
		exec (@sql)
	end

	--select payment on ecpTransRequireContract
	if (@ordertable = 19)
	begin
		set @where = ''
	
		if (@cp2id <> null)
			set @where = '(cptrc.ID = ' + @cp2id + ')'

		if (@statusrepay <> null)
			set @where = '(cptrc.StatusRepay = ' + @statusrepay + ')'

		if (@statuspayment <> null)
			set @where = '(cptrc.StatusPayment = ' + @statuspayment + ')'

		if (@studentid <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		end
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end

		if ((@datestart <> null) and
			(@dateend <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(rp1rpy.ReplyDate between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" and "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'
		end
			
		if (@where <> '') 
			set @where = ' where (' + @where + ') and '
		else
			set @where = ' where '
		
		set @where = @where + '(cptrp.StatusReply = 2) and (cptrp.ReplyResult = 1) and ' + @repaystatus2
		
		set @sql = 'select	count(cptrc.ID) as CountPayment
					from	ecpTransRepayContract as cptrp inner join
							ecpTransRequireContract as cptrc on cptrp.RCID = cptrc.ID and cptrp.StatusRepay = cptrc.StatusRepay inner join
							ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID left join
							(
								select	RCID,
										(convert(date, (convert(varchar, (convert(int, substring(ReplyDate, 7, 4)) - 543)) + "-" + substring(ReplyDate, 4, 2) + "-" + substring(ReplyDate, 1, 2)))) as ReplyDate
								from	ecpTransRepayContract
								where	(StatusRepay = 1) and
										(ReplyDate is not null)
							) as rp1rpy on cptrp.RCID = rp1rpy.RCID'
		set @sql = @sql + @where					   										
		
		exec (@sql)
		
		set @sql = 'select	*
					from	(
								select	row_number() over(order by rp1rpy.ReplyDate desc) as RowNum,
										cptrc.ID,
										cptrc.BCID,
										cptrc.IndemnitorAddress,
										bp.id as ProvinceID,
										bp.provinceNameTH as ProvinceTName,
										cptrc.RequireDate,
										cptrc.ApproveDate,
										cptrc.ActualMonthScholarship,
										cptrc.ActualScholarship,
										cptrc.TotalPayScholarship,
										cptrc.ActualMonth,
										cptrc.ActualDay,
										cptrc.AllActualDate,
										cptrc.ActualDate,
										cptrc.RemainDate,
										cptrc.SubtotalPenalty,
										cptrc.TotalPenalty,
										cptrc.LawyerFullname,
										replace(cptrc.LawyerPhoneNumber, "-", " ") as LawyerPhoneNumber,
										replace(cptrc.LawyerMobileNumber, "-", " ") as LawyerMobileNumber,
										cptrc.LawyerEmail,
										tpmsum.TotalPayCapital,
										tpmsum.TotalPayInterest,
										tpmsum.TotalPay,
										tpmmax.TotalRemain,
										tpmmax.RemainAccruedInterest,
										cptrc.StatusRepay,
										cptrc.StatusPayment,
										cptrc.FormatPayment,
										cptrp.StatusReply,
										cptrp.ReplyDate,
										rpdhis.ReplyDateHistory,
										rp1rpy.ReplyDate as Repay1ReplyDate,
										cptbc.StudentID,
										cptbc.TitleTName,
										cptbc.FirstTName,
										cptbc.LastTName, 
										cptbc.FacultyCode,
										cptbc.FacultyName,
										cptbc.ProgramCode,
										cptbc.ProgramName,
										cptbc.MajorCode,
										cptbc.GroupNum,
										cptbc.DLevel,
										(
											case cptbc.DLevel
												when "U" then "ต่ำกว่าปริญญาตรี"
												when "B" then "ปริญญาตรี"
											else
												null
											end
										) as DLevelName,																																									
										cptbc.PursuantBook,
										cptbc.Pursuant,
										cptbc.PursuantBookDate,
										cptbc.Location,
										cptbc.InputDate,
										cptbc.StateLocation,
										cptbc.StateLocationDate,
										cptbc.ContractDate,
										cptbc.Guarantor,
										cptbc.ScholarFlag,
										cptbc.ScholarshipMoney,
										cptbc.ScholarshipYear,
										cptbc.ScholarshipMonth,
										cptbc.EducationDate,
										cptbc.GraduateDate,
										cptbc.ContractForceStartDate,
										cptbc.ContractForceEndDate,
										cptbc.CaseGraduate,
										cptbc.CivilFlag,
										cptbc.CalDateCondition,
										cptbc.IndemnitorYear,
										cptbc.IndemnitorCash,
										cptbc.StatusSend,
										cptbc.StatusReceiver,
										cptbc.StatusEdit,
										cptbc.StatusCancel,
										cptbc.DateTimeCreate,
										cptbc.DateTimeModify,
										cptbc.DateTimeCancel,
										cptbc.DateTimeSend,
										cptbc.DateTimeReceiver,
										spl1.FileName,
										spl1.FolderName
								from	ecpTransRepayContract as cptrp inner join
										ecpTransRequireContract as cptrc on cptrp.RCID = cptrc.ID and cptrp.StatusRepay = cptrc.StatusRepay left join
										plcProvince as bp on cptrc.Province = bp.id inner join
										ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID left join
										(
											select	distinct
													spl.studentCode as StudentID,
													spl.pictureName as FileName,
													"" as FolderName
											from	stdStudent as spl
										) as spl1 on cptbc.StudentID = spl1.StudentID left join
										(
											select	RCID,
													(case when (len(c.ReplyDate) > 0) then (left(isnull(c.ReplyDate, ""), (len(isnull(c.ReplyDate, "")) - 1))) else null end) as ReplyDateHistory
											from	(	
														select	distinct
																RCID
														from	ecpTransRepayContract
													) as a
													cross apply
													(
														select	 (b.ReplyDate  + ",")
														from	 ecpTransRepayContract as b
														where	 (a.RCID = b.RCID)
														order by b.StatusRepay
														for xml path("")
													) c (ReplyDate) 
										) as rpdhis on cptrp.RCID = rpdhis.RCID left join
										(
											select	RCID,
													(convert(date, (convert(varchar, (convert(int, substring(ReplyDate, 7, 4)) - 543)) + "-" + substring(ReplyDate, 4, 2) + "-" + substring(ReplyDate, 1, 2)))) as ReplyDate
											from	ecpTransRepayContract
											where	(StatusRepay = 1) and
													(ReplyDate is not null)
										) as rp1rpy on cptrp.RCID = rp1rpy.RCID left join
										(
											select	 RCID,
													 sum(PayCapital) as TotalPayCapital,
													 sum(PayInterest) as TotalPayInterest,
													 sum(TotalPay) as TotalPay
											from	 ecpTransPayment
											group by RCID
										) as tpmsum on cptrp.RCID = tpmsum.RCID left join
										(
											select	cptpy.RCID,
													cptpy.RemainAccruedInterest,
													cptpy.TotalRemain
											from	ecpTransPayment as cptpy inner join
													(
														select	 max(ID) as ID,
																 RCID
														from	 ecpTransPayment
														group by RCID
													) as cptpy1 on cptpy.ID = cptpy1.ID
										) as tpmmax on cptrp.RCID = tpmmax.RCID' + @where + '
							) as cptrc1'

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (cptrc1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)
	end
	
	--select last comment on reject ecpTransBreakContract
	if (@ordertable = 20)
	begin
		set @where = ''
		
		if ((@cp1id <> null) and
			(@actioncomment <> null))
			set @where = '(cptrj.BCID = ' + @cp1id + ') and (cptrj.Action = "' + @actioncomment + '")'

		if (@where <> '') 
			set @where = ' HAVING ' + @where
		
		set @sql = 'select	cptrj.ID,
							cptrj.BCID,
							cptrj.Comment, cptrj.DateTimeReject
					from	ecpTransReject as cptrj inner join
							(
								select	 max(cptrj.ID) as ID,
										 cptrj.BCID
								from	 ecpTransReject as cptrj inner join
										 ecpTransBreakContract as cptbc ON cptrj.BCID = cptbc.ID
								group by cptrj.BCID, cptrj.Action' + @where +' 
							) as cptrj1 on cptrj.ID = cptrj1.ID'

		exec (@sql)
	end

	--select ecpTransPayment
	if (@ordertable = 21)
	begin
		set @where = ''
		set @where1 = ''
		
		if (@cp1id <> null)
			set @where = '(cptpy.RCID = ' + @cp1id + ')'
		
		if (@cp2id <> null)
			set @where = '(cptpy.ID = ' + @cp2id + ')'

		if ((@datestart <> null) and
			(@dateend <> null))
			set @where1 = '(convert(date, convert(varchar, (convert(int, substring(cptpy.DateTimePayment, 7, 4)) - 543)) + "-" + substring(cptpy.DateTimePayment, 4, 2) + "-" + substring(cptpy.DateTimePayment, 1, 2)) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" and "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'			

		if (@where <> '') 
			set @where = ' where (' + @where + ') and '
		else
			set @where = ' where '
			
		if (@where1 <> '') 
			set @where1 = @where + @where1 + ' and '
		else
			set @where1 = @where
		
		set @where = @where + '(cptrp.StatusReply = 2) and (cptrp.ReplyResult = 1) and ' + @repaystatus2
		set @where1 = @where1 + '(cptrp.StatusReply = 2) and (cptrp.ReplyResult = 1) and ' + @repaystatus2

		set @sql = 'select	cptpy1.*
					from	ecpTransRequireContract as cptrc inner join
							ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
							ecpTransPayment as cptpy on cptrc.ID = cptpy.RCID inner join
							ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID inner join
							(
								select	row_number() over(order by cptpy.ID) as RowNum,
										cptpy.ID,
										cptpy.RCID,
										cptpy.CalInterestYesNo,
										cptpy.OverpaymentDateStart,
										cptpy.OverpaymentDateEnd,
										cptpy.OverpaymentYear,
										cptpy.OverpaymentDay,
										cptpy.OverpaymentInterest,
										cptpy.OverpaymentTotalInterest,
										cptpy.PayRepayDateStart,
										cptpy.PayRepayDateEnd,
										cptpy.PayRepayYear,
										cptpy.PayRepayDay,
										cptpy.PayRepayInterest,
										cptpy.PayRepayTotalInterest,
										cptpy.DateTimePayment,
										cptpy.Capital,
										cptpy.Interest,
										cptpy.TotalAccruedInterest,
										cptpy.TotalPayment,
										cptpy.PayCapital,
										cptpy.PayInterest,
										cptpy.TotalPay,
										cptpy.RemainCapital,
										cptpy.AccruedInterest,
										cptpy.RemainAccruedInterest,
										cptpy.TotalRemain,
										cptpy.Channel,
										cptpy.ReceiptNo,
										cptpy.ReceiptBookNo,
										cptpy.ReceiptDate,
										cptpy.ReceiptSendNo,
										cptpy.ReceiptFund,
										cptpy.ReceiptCopy,
										cptpy.ChequeNo,
										cptpy.ChequeBank,
										cptpy.ChequeBankBranch,
										cptpy.ChequeDate,
										cptpy.CashBank,
										cptpy.CashBankBranch,
										cptpy.CashBankAccount,
										cptpy.CashBankAccountNo,
										cptpy.CashBankDate
								from	ecpTransRequireContract as cptrc inner join
										ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
										ecpTransPayment as cptpy on cptrc.ID = cptpy.RCID inner join
										ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID' + @where + '
							) as cptpy1 on cptpy.ID = cptpy1.ID'
		set @sql = @sql + @where1					   										
		
		exec (@sql)
	end

	--select last transaction payment
	if (@ordertable = 22)
	begin
		set @where = ''
		
		if (@cp2id <> null)
			set @where = '(cptpy.RCID = ' + @cp2id + ')'
		
		if (@where <> '') 
			set @where = ' having ' + @where
		
		set @sql = 'select	cptpy.ID,
							cptpy.RCID,
							cptpy.DateTimePayment,
							cptpy.RemainAccruedInterest,
							cptpy.TotalRemain
					from	ecpTransPayment as cptpy inner join
							(
								select	 max(cptpy.ID) as ID,
										 cptpy.RCID
								from	 ecpTransRequireContract as cptrc inner join
										 ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
										 ecpTransPayment as cptpy on cptrc.ID = cptpy.RCID inner join
										 ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID
								group by cptpy.RCID' + @where + '
							) as cptpy1 on cptpy.ID = cptpy1.ID'
		
		exec (@sql)
	end

	--select ecpTabProgram
	if (@ordertable = 23)
	begin
		set @where = ''	
			
		if (@cp1id <> null)
			set @where = ' where (cptpg.ID = ' + @cp1id + ')'

		set @sql = 'select	cptpg.ID,
							cptpg.FacultyCode,
							bf.nameTh as FactTName,
							cptpg.ProgramCode,
							cp.nameTh as ProgTName,
							cptpg.MajorCode,
							cptpg.GroupNum,
							cptpg.Dlevel, 
							(
								case cptpg.Dlevel
									when "U" then "ต่ำกว่าปริญญาตรี"
									when "B" then "ปริญญาตรี"
								else
									null
								end					
							) as DlevelName																			
				    from	ecpTabProgram as cptpg inner join
							acaFaculty as bf on cptpg.FacultyCode = bf.facultyCode inner join
							acaProgram as cp on bf.id = cp.facultyId and (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) = (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel)'
		set @sql = @sql + @where					   
		set @sql = @sql + ' order by DlevelName, cptpg.FacultyCode, cptpg.ProgramCode'
		
		exec (@sql)
	end

	--select ecpTabProgram on repeat
	if (@ordertable = 24)
	begin
		set @where = ''	
			
		if ((@dlevel <> null) and
			(@faculty <> null) and
			(@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			set @where = ' where ((cptpg.Dlevel = "' + @dlevel + '") and
								  (cptpg.FacultyCode = "' + @faculty + '") and
								  (cptpg.ProgramCode = "' + @program + '") and
								  (cptpg.MajorCode = "' + @major + '") and
								  (cptpg.GroupNum = "' + @groupnum + '"))'							  
			
			if (@cp1id <> null)
				set @where = @where + ' and (cptpg.ID <> ' + @cp1id + ')'
		end
		
		set @sql = 'select	cptpg.ID
				    from	ecpTabProgram as cptpg inner join
							acaFaculty as bf on cptpg.FacultyCode = bf.facultyCode inner join
							acaProgram as cp ON bf.id = cp.facultyId and (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) = (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel) '
		set @sql = @sql + @where					   
		set @sql = @sql + ' order by cptpg.ID'
		
		exec (@sql)
	end

	--select faculty from ecpTabProgram
	IF (@ordertable = 25)
	begin
		set @sql = 'select	 bf.id,
							 cptpg.FacultyCode,
							 bf.nameTh as FactTName
				    from	 ecpTabProgram as cptpg inner join
							 acaFaculty as bf on cptpg.FacultyCode = bf.facultyCode
					group by bf.id,cptpg.FacultyCode, bf.nameTh'
		set @sql = @sql + ' order by cptpg.FacultyCode'					   

		exec (@sql)
	end

	--select program on faculty from ecpTabProgram
	if (@ordertable = 26)
	begin
		set @where = ''		
		
		if ((@dlevel = null) and
			(@faculty <> null))
			set @where = ' where (cptpg.FacultyCode = "' + @faculty + '")'
			
		if ((@dlevel <> null) and
			(@faculty <> null))
			set @where = ' where (cptpg.FacultyCode = "' + @faculty + '") and (cptpg.DLevel = "' + @dlevel + '")'

		set @sql = 'select	cptpg.ProgramCode,
							cptpg.MajorCode,
							cptpg.GroupNum,
							cp.nameTh as ProgTName,
							cptpg.DLevel,
							(
								case cptpg.DLevel
									when "U" then "ต่ำกว่าปริญญาตรี"
									when "B" then "ปริญญาตรี"
								else
									null
								end
							) as DLevelName	
				    from	ecpTabProgram as cptpg inner join
							acaFaculty as bf on cptpg.FacultyCode = bf.facultyCode inner join
							acaProgram as cp on bf.id = cp.facultyId and (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) = (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel) '
		set @sql = @sql + @where				    
		set @sql = @sql + ' order by cptpg.ProgramCode'
		
		exec (@sql)
	end

	--select title name
	if (@ordertable = 27)
	begin
		set @sql = 'select	 bt.id as TitleCode,
							 bt.enTitleInitials as TitleEName,
							 bt.thTitleFullName as TitleTName,
							 ge.enGenderInitials as Sex
				    from	 perTitlePrefix as bt left join
							 perGender as ge on bt.perGenderId = ge.id 
					order by bt.id'
		exec (@sql)
	end

	--select ReportTableCalCapitalAndInterest
	if (@ordertable = 28)
	begin
		set @where = ''
		
		if (@cp2id <> null)
			set @where = '(cptrc.ID = ' + @cp2id + ')'

		if (@studentid <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		end
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end
			
		if (@where <> '') 
			set @where = ' where (' + @where + ') and '
		else
			set @where = ' where '
		
		set @where = @where + '(cptrc.StatusPayment <> 3) and (cptrp.StatusReply = 2) and (cptrp.ReplyResult = 1) and ' + @repaystatus2		
		set @sql = 'select	count(cptrc.ID) as CountReportTableCalCapitalAndInterest
					from	ecpTransRepayContract as cptrp inner join
							ecpTransRequireContract as cptrc on cptrp.RCID = cptrc.ID and cptrp.StatusRepay = cptrc.StatusRepay inner join
							ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID'
		set @sql = @sql + @where					   										
		
		exec (@sql)
		
		set @sql = 'select	*
					from	(
								select	row_number() over(order by cptbc.StudentID) as RowNum,
										cptrc.ID,
										cptrc.BCID,
										cptrc.TotalPenalty,
										cptrc.StatusRepay,
										cptrc.StatusPayment,
										cptrc.FormatPayment,
										cptrp.StatusReply,
										cptrp.ReplyResult,
										cptbc.StudentID,
										cptbc.TitleTName,
										cptbc.FirstTName,
										cptbc.LastTName, 
										cptbc.FacultyCode,
										cptbc.FacultyName,
										cptbc.ProgramCode,
										cptbc.ProgramName,
										cptbc.MajorCode,
										cptbc.GroupNum,
										cptbc.DLevel,
										(
											case cptbc.DLevel
												when "U" then "ต่ำกว่าปริญญาตรี"
												when "B" then "ปริญญาตรี"
											else
												null
											end
										) as DLevelName,																																									
										cptbc.StatusSend,
										cptbc.StatusReceiver,
										cptbc.StatusEdit,
										cptbc.StatusCancel,
										spl1.FileName,
										spl1.FolderName
								from	ecpTransRepayContract as cptrp inner join
										ecpTransRequireContract as cptrc on cptrp.RCID = cptrc.ID and cptrp.StatusRepay = cptrc.StatusRepay inner join
										ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID left join
										(
											select	distinct
													spl.studentCode as StudentID,
													spl.pictureName as FileName,
													"" as FolderName
											from	stdStudent as spl
										) as spl1 on cptbc.StudentID = spl1.StudentID' + @where + '
							) as cptrc1'

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (cptrc1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'			
		
		exec (@sql)
	end
	
	--select sum PayCapital, PayInterest, TotalPay on payment
	if (@ordertable = 29)
	begin
		set @where = ''
		
		if (@cp2id <> null)
			set @where = '(cptpy.RCID = ' + @cp2id + ')'
		
		if (@where <> '') 
			set @where = ' having ' + @where
		
		set @sql = 'select	 sum(cptpy.PayCapital) as SumPayCapital,
							 sum(cptpy.PayInterest) as SumPayInterest,
							 sum(cptpy.TotalPay) as SumTotalPay,
							 cptpy.RCID
					from	 ecpTransRequireContract as cptrc inner join
							 ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
							 ecpTransPayment as cptpy on cptrc.ID = cptpy.RCID inner join
							 ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID
					group by cptpy.RCID'
		set @sql = @sql + @where
		
		exec (@sql)
	end
	
	--select ListCalCPReportTableCalCapitalAndInterest
	if (@ordertable = 30)
	begin
		if (@paiddate <> null)
			set @paiddate = convert(varchar, convert(int, substring(@paiddate, 7, 4)) - 543) + substring(@paiddate, 4, 2) + substring(@paiddate, 1, 2)
		
		set @sql = 'select	PaidPeriod,
							Capital,
							Paid,
							Interest,
							PayTotal,
							(substring(convert(varchar(10), PaidDate, 103), 1, 6) + convert(varchar, convert(INT, substring(convert(varchar(10), PaidDate, 103), 7, 4)) + 543)) as PaidDate
					from	fnc_ecpGetInterest("' + @capital + '", "' + @pay + '", "' + @paiddate + '", "' + @interest + '")'
		
		exec (@sql)
		
		set @sql = 'select	sum(Paid) as SumPaid,
							sum(Interest) as SumINterest,
							sum(PayTotal) as SumPayTotal
					from	fnc_ecpGetInterest("' + @capital + '", "' + @pay + '", "' + @paiddate + '", "' + @interest + '")'					
		
		exec (@sql)
	end

	--select ReportStepOfWork
	if (@ordertable = 31)
	begin
		set @where = ''
		
		if (@acadamicyear <> null)
			set @where = '(LEFT(cptbc.StudentID, 2) = "' + @acadamicyear + '")'
					
		if (@statusstepofwork <> null)
		begin
			set @where = (
							case @statusstepofwork
								when '1'  then '((cptbc.StatusSend = 1) and (cptbc.StatusReceiver = 1) and (cptbc.StatusEdit = 1) and (cptbc.StatusCancel = 1))'
								when '2'  then '((cptbc.StatusSend = 2) and (cptbc.StatusReceiver = 1) and (cptbc.StatusEdit = 1) and (cptbc.StatusCancel = 1))'
								when '3'  then '((cptbc.StatusSend = 2) and (cptbc.StatusReceiver = 2) and (cptbc.StatusEdit = 1) and (cptbc.StatusCancel = 1))'
								when '4'  then '((cptbc.StatusSend = 2) and (cptbc.StatusReceiver = 1) and (cptbc.StatusEdit = 2) and (cptbc.StatusCancel = 1))'
								when '5'  then '(cptbc.StatusCancel = 2)'
								when '6'  then '(cptrc.StatusRepay = 0)'
								when '7'  then '((cptrc.StatusRepay = 1) and (cptrp.StatusReply = 1))'
								when '8'  then '((cptrc.StatusRepay = 1) and (cptrp.StatusReply = 2) and (cptrp.ReplyResult = 1))'
								when '9'  then '((cptrc.StatusRepay = 1) and (cptrp.StatusReply = 2) and (cptrp.ReplyResult = 2))'
								when '10' then '((cptrc.StatusRepay = 2) and (cptrp.StatusReply = 1))'
								when '11' then '((cptrc.StatusRepay = 2) and (cptrp.StatusReply = 2) and (cptrp.ReplyResult = 1))'
								when '12' then '((cptrc.StatusRepay = 2) and (cptrp.StatusReply = 2) and (cptrp.ReplyResult = 2))'
								when '13' then '((cptrc.StatusPayment = 1) or (cptrc.StatusPayment is null))'
								when '14' then '(cptrc.StatusPayment = 2)'
								when '15' then '(cptrc.StatusPayment = 3)'
							end
						 )
		end

		if (@studentid <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		end
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end
			
		if (@where <> '') 
			set @where = ' where (' + @where + ') and '
		else
			set @where = ' where '

		if (@section = '1')
			set @where = @where + @trackingstatusorla

		if (@section = '2')
			set @where = @where + @trackingstatusoraa

		if (@section = null)
			set @where = @where + '(cptbc.StatusCancel = 1)'
				
		set @sql = 'select	count(cptbc.ID) as CountReportStepOfWork
					from	ecpTransBreakContract as cptbc left join
							ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID left join
							ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay'
		set @sql = @sql + @where					   										
		
		exec (@sql)
		
		set @sql = 'select	*
					from	(
								select	row_number() over(order by cptbc.StudentID) as RowNum,
										cptbc.ID as BCID,
										cptrc.ID as RCID,
										cptbc.StudentID,
										cptbc.TitleTName,
										cptbc.FirstTName,
										cptbc.LastTName, 
										cptbc.FacultyCode,
										cptbc.FacultyName,
										cptbc.ProgramCode,
										cptbc.ProgramName,
										cptbc.MajorCode,
										cptbc.GroupNum,
										cptbc.DLevel,
										(
											case cptbc.DLevel
												when "U" then "ต่ำกว่าปริญญาตรี"
												when "B" then "ปริญญาตรี"
											else
												null
											end
										) as DLevelName,																																									
										cptbc.StatusSend,
										cptbc.StatusReceiver,
										cptbc.StatusEdit,
										cptbc.StatusCancel,
										cptrc.StatusRepay,
										cptrp.StatusReply,
										cptrp.ReplyResult,
										cptrc.StatusPayment,
										spl1.FileName,
										spl1.FolderName
								from	ecpTransBreakContract as cptbc left join
										ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID left join
										ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
										(
											select	distinct
													spl.studentCode as StudentID,
													spl.pictureName as FileName, 
													"" as FolderName
											from	stdStudent as spl
										) as spl1 on cptbc.StudentID = spl1.StudentID' + @where + ' 
							) as cptbc1'

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (cptbc1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)
	end
	
	--select ReportStatisticRepay
	if (@ordertable = 32)
	begin
		set @sql = 'select	row_number() over(order by t1.AcadamicYear) as RowNum,
							t1.AcadamicYear,
							t2.CountProgram,
							t3.CountStudent,
							t4.CountStudentNoPayment,
							t5.CountStudentPaymentComplete,
							t6.CountStudentPaymentIncomplete,
							t7.SumTotalPenalty,
							t8.SumTotalPay
					from	(
								select	 distinct
										 left(cptbc.StudentID, 2) as AcadamicYear
								from	 ecpTransBreakContract as cptbc
								where	 (cptbc.StatusCancel = 1)
							) as t1 left join		 
							(
								select	 tt1.AcadamicYear,
										 count(tt1.ProgramCode) as CountProgram
								from	 (
											select	distinct
													left(cptbc.StudentID, 2) as AcadamicYear,
													cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum
											from	ecpTransBreakContract as cptbc
											where	(cptbc.StatusCancel = 1)
										 ) as tt1
								group by tt1.AcadamicYear
							) as t2 on t1.AcadamicYear = t2.AcadamicYear left join		 
							(
								select	 tt1.AcadamicYear,
										 count(tt1.StudentID) as CountStudent
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptbc.StudentID
											from	ecpTransBreakContract as cptbc
											where	(cptbc.StatusCancel = 1)
										 ) as tt1
								group by tt1.AcadamicYear
							) as t3 on t1.AcadamicYear = t3.AcadamicYear left join
							(
								select	 tt1.AcadamicYear,
										 count(tt1.StudentID) as CountStudentNoPayment
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptbc.StudentID
											from	ecpTransBreakContract as cptbc left join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID left join
													ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay
											where	(cptbc.StatusCancel = 1) and
													((cptrc.StatusPayment = 1) or (cptrc.StatusPayment is null))
										 ) as tt1
								group by tt1.AcadamicYear
							) as t4 on t1.AcadamicYear = t4.AcadamicYear left join	
							(
								select	 tt1.AcadamicYear,
										 count(tt1.StudentID) as CountStudentPaymentComplete
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptbc.StudentID
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay
											where	(cptbc.StatusCancel = 1) and
													(cptrc.StatusPayment = 3)
										 ) as tt1
								group by tt1.AcadamicYear
							) as t5 on t1.AcadamicYear = t5.AcadamicYear left join		 
							(
								select	 tt1.AcadamicYear,
										 count(tt1.StudentID) as CountStudentPaymentIncomplete
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptbc.StudentID
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay							
											where	(cptbc.StatusCancel = 1) and
													(cptrc.StatusPayment = 2)
										 ) as tt1
								group by tt1.AcadamicYear
							) as t6 on t1.AcadamicYear = t6.AcadamicYear left join		  
		  					(
								select	 tt1.AcadamicYear,
										 sum(tt1.TotalPenalty) as SumTotalPenalty
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptrc.TotalPenalty
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID
											where	(cptbc.StatusCancel = 1)
										 ) as tt1
								group by tt1.AcadamicYear
							) as t7 on t1.AcadamicYear = t7.AcadamicYear left join							 
							(
								select	 tt1.AcadamicYear,
										 sum(tt1.PayCapital) as SumTotalPay
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptpy.PayCapital
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													ecpTransRepayContract as cptrp ON cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
													ecpTransPayment as cptpy on cptrc.ID = cptpy.RCID
											where	(cptbc.StatusCancel = 1)
										 ) as tt1
								group by tt1.AcadamicYear
							) as t8 on t1.AcadamicYear = t8.AcadamicYear'

		exec (@sql)
	end
	
	--select ReportStatisticRepayByProgram
	if (@ordertable = 33)
	begin
		set @sql = 'select	row_number() over(order by t1.ProgramCode) as RowNum,
							t1.AcadamicYear,
							t1.FacultyCode,
							t1.FacultyName,
							t1.ProgramCode,
							t1.ProgramName,
							t1.MajorCode,
							t1.GroupNum,
							t2.CountStudent,
							t3.CountStudentNoPayment,
							t4.CountStudentPaymentComplete,
							t5.CountStudentPaymentIncomplete,
							t6.SumTotalPenalty,
							t7.SumTotalPay
					FROM	(
								select	 distinct
										 (left(cptbc.StudentID, 2) + cptbc.FacultyCode + cptbc.FacultyName + cptbc.ProgramCode + cptbc.ProgramName + cptbc.MajorCode + cptbc.GroupNum) as ss,
										 left(cptbc.StudentID, 2) as AcadamicYear,
										 cptbc.FacultyCode,
										 cptbc.FacultyName,
										 cptbc.ProgramCode,
										 cptbc.ProgramName,
										 cptbc.MajorCode,
										 cptbc.GroupNum
								from	 ecpTransBreakContract as cptbc
								where	 (cptbc.StatusCancel = 1) and
										 (left(cptbc.StudentID, 2) = "' + @acadamicyear + '")
							) as t1 left join
							(
								select	 (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 count(tt1.StudentID) as CountStudent
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptbc.StudentID,
													cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum
											from	ecpTransBreakContract as cptbc
											where	(cptbc.StatusCancel = 1) and
													(left(cptbc.StudentID, 2) = "' + @acadamicyear + '")
										 ) as tt1
								group by (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t2 on t1.ss = t2.ss left join
							(
								select	 (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 count(tt1.StudentID) as CountStudentNoPayment
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptbc.StudentID,
													cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum
											from	ecpTransBreakContract as cptbc left join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID left join
													ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay
											where	(cptbc.StatusCancel = 1) and
													(left(cptbc.StudentID, 2) = "' + @acadamicyear + '") and
													((cptrc.StatusPayment = 1) or (cptrc.StatusPayment is null))
										 ) as tt1
								group by (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t3 on t1.ss = t3.ss left join		
							(
								select	 (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 count(tt1.StudentID) as CountStudentPaymentComplete
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptbc.StudentID,
													cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay
											where	(cptbc.StatusCancel = 1) and
													(left(cptbc.StudentID, 2) = "' + @acadamicyear + '") and
													(cptrc.StatusPayment = 3)
										 ) as tt1
								group by (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t4 on t1.ss = t4.ss left join		 
							(
								select	 (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 count(tt1.StudentID) as CountStudentPaymentIncomplete
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptbc.StudentID,
													cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay							
											where	(cptbc.StatusCancel = 1) and
													(left(cptbc.StudentID, 2) = "' + @acadamicyear + '") and
													(cptrc.StatusPayment = 2)
										 ) as tt1
								group by (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t5 on t1.ss = t5.ss left join		
							(
								select	 (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 sum(tt1.TotalPenalty) as SumTotalPenalty
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum,
													cptrc.TotalPenalty
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID
											where	(cptbc.StatusCancel = 1) and
													(left(cptbc.StudentID, 2) = "' + @acadamicyear + '")
										 ) as tt1
								group by (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t6 on t1.ss = t6.ss left join
							(
								select	 (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 sum(tt1.PayCapital) as SumTotalPay
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum,
													cptpy.PayCapital
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
													ecpTransPayment as cptpy on cptrc.ID = cptpy.RCID
											where	(cptbc.StatusCancel = 1) and
													(left(cptbc.StudentID, 2) = "' + @acadamicyear + '")
										 ) as tt1
								group by (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t7 on t1.ss = t7.ss'
		
		exec (@sql)
	end

	--select ReportNoticeRepayComplete
	if (@ordertable = 34)
	begin
		set @where = ''
		
		if (@cp1id <> null)
			set @where = '(cptbc.ID = ' + @cp1id + ')'

		if (@studentid <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		end
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '

			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end
			
		if (@where <> '') 
			set @where = ' and (' + @where + ')'
				
		set @sql = 'select	count(cptbc.ID) as CountReportNoticeRepayComplete
					from	ecpTransBreakContract as cptbc inner join
							ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
							ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay
					where	' + @repaystatus2 + ' and
							(cptrc.StatusPayment = 3)' + @where
		
		exec (@sql)
		
		set @sql = 'select	*
					from	(
								select	row_number() over(order by cptbc.StudentID) as RowNum,
										cptbc.ID as BCID,
										cptrc.ID as RCID,
										cptbc.StudentID,
										cptbc.TitleTName,
										cptbc.FirstTName,
										cptbc.LastTName, 
										cptbc.FacultyCode,
										cptbc.FacultyName,
										cptbc.ProgramCode,
										cptbc.ProgramName,
										cptbc.MajorCode,
										cptbc.GroupNum,
										cptbc.GraduateDate,
										cptbc.IndemnitorYear,
										cptrc.IndemnitorAddress,
										cptrc.TotalPenalty,
										cptrc.StatusPayment
								from	ecpTransBreakContract as cptbc inner join
										ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
										ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay
								where	' + @repaystatus2 + ' and
										(cptrc.StatusPayment = 3)' + @where + '
							) as cptbc1'
						
		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (cptbc1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)
	end
		
	--SELECT ReportNoticeClaimDebt
	if (@ordertable = 35)
	begin
		set @where = ''
		
		if (@cp1id <> null)
			set @where = '(cptbc.ID = ' + @cp1id + ')'
				
		if (@studentid <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		end
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end
			
		if (@where <> '') 
			set @where = ' and (' + @where + ')'
				
		set @sql = 'select	count(cptbc.ID) as CountReportNoticeClaimDebt
					from	ecpTransBreakContract as cptbc inner join
							ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
							ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay
					where	' + @repaystatus2 + ' and
							(cptrc.StatusRepay <> 0)' + @where
		
		exec (@sql)
		
		set @sql = 'select	*
					from	(
								select	row_number() over(order by cptbc.StudentID) as RowNum,
										cptbc.ID as BCID,
										cptrc.ID as RCID,
										cptbc.StudentID,
										cptbc.TitleTName,
										cptbc.FirstTName,
										cptbc.LastTName, 
										cptbc.FacultyCode,
										cptbc.FacultyName,
										cptbc.ProgramCode,
										cptbc.ProgramName,
										cptbc.MajorCode,
										cptbc.GroupNum,
										cptbc.IndemnitorYear,
										cptbc.IndemnitorCash,
										cptbc.ContractDate,
										cptbc.ContractDateAgreement,
										cptbc.Guarantor,
										isnull(cptrc.StudyLeave, "N") as StudyLeave,
										cptrc.RequireDate,
										cptrc.ApproveDate,
										cptrc.BeforeStudyLeaveStartDate,
										cptrc.BeforeStudyLeaveEndDate,
										cptrc.StudyLeaveStartDate,
										cptrc.StudyLeaveEndDate,
										cptrc.AfterStudyLeaveStartDate,
										cptrc.AfterStudyLeaveEndDate,
										cptrc.SubtotalPenalty,
										cptrc.TotalPenalty,
										cptrc.LawyerFullname,
										replace(cptrc.LawyerPhoneNumber, "-", " ") as LawyerPhoneNumber,
										replace(cptrc.LawyerMobileNumber, "-", " ") as LawyerMobileNumber,
										cptrc.LawyerEmail,
										cptrc.StatusRepay,
										cptrc.StatusPayment
								from	ecpTransBreakContract as cptbc inner join
										ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
										ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay
								where	' + @repaystatus2 + ' and
										(cptrc.StatusRepay <> 0)' + @where + '
							) as cptbc1'
		
		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (cptbc1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)
	end
		
	--select ReportStatisticContract
	if (@ordertable = 38)
	begin
		set @sql = 'select	row_number() over(order by t1.AcadamicYear) as RowNum,
							t1.AcadamicYear,
							t2.CountStudent,
							t3.CountStudentSignContract,
							t4.CountStudentContractPenalty
					from	(
								select	 distinct 
										 left(std.studentCode, 2) as AcadamicYear
								from	 stdStudent as std left join
										 acaFaculty as bf on std.facultyId = bf.id left join
										 acaProgram as cp on std.facultyId = cp.facultyId and std.programId = cp.id inner join
										 perPerson as perpes on std.personId = perpes.id left join
										 perTitlePrefix as bt on perpes.perTitlePrefixId = bt.id inner join
										 ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel)= (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel)
								where	 ' + @nostudentid + ' and
										 (std.studentCode is not null)
							) as t1 left join
							(
								select	 tt1.AcadamicYear,
										 count(tt1.StudentID) as CountStudent
								from	 (
											select	distinct
													left(std.studentCode, 2) as AcadamicYear,
													std.studentCode as StudentID
											from	stdStudent as std left join
													acaFaculty as bf on std.facultyId = bf.id left join
													acaProgram as cp on std.facultyId = cp.facultyId and std.programId = cp.id inner join
													perPerson as perpes on std.personId = perpes.id left join
													perTitlePrefix as bt on perpes.perTitlePrefixId = bt.id inner join
													ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel)= (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel)
											where	' + @nostudentid + ' and ' + @nostudentstatus + '
										 ) as tt1 
								group by tt1.AcadamicYear
							) as t2 on t1.AcadamicYear = t2.AcadamicYear left join
		 					(
								select	 tt1.AcadamicYear,
										 count(tt1.StudentID) as CountStudentSignContract
								from	 (
											select	distinct
													left(std.studentCode, 2) as AcadamicYear,
													std.studentCode as StudentID
											from	stdStudent as std left join
													acaFaculty as bf on std.facultyId = bf.id left join
													acaProgram as cp on std.facultyId = cp.facultyId and std.programId = cp.id inner join
													perPerson as perpes on std.personId = perpes.id left join
													perTitlePrefix as bt on perpes.perTitlePrefixId = bt.id inner join
													ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel)= (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) inner join
													ectDocMgmt as cd on std.id = cd.StudentID and std.programId = cd.programId
											where	(cd.operationType = "S" or cd.operationType = "M") and ' + @nostudentid + ' and ' + @nostudentstatus + '
										 ) as tt1									 
								group by tt1.AcadamicYear
							) as t3 on t1.AcadamicYear = t3.AcadamicYear left join
							(
								select	 tt1.AcadamicYear,
										 count(tt1.StudentID) as CountStudentContractPenalty
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptbc.StudentID
											from	ecpTransBreakContract as cptbc
											where	(cptbc.StatusCancel = 1)
										 ) as tt1
								group by tt1.AcadamicYear
							) as t4 on t1.AcadamicYear = t4.AcadamicYear'

		exec (@sql)
	end

	--select ReportStatisticContractByProgram
	if (@ordertable = 39)
	begin
		set @sql = 'select	row_number() over(order by t1.ProgramCode) as RowNum,
							t1.AcadamicYear,
							t1.FacultyCode,
							t1.FactTName,
							t1.ProgramCode,
							t1.ProgTName,
							t1.MajorCode,
							t1.GroupNum,
							t2.CountStudent,
							t3.CountStudentSignContract,
							t4.CountStudentContractPenalty
					from	(
								select	 distinct
										 (left(std.studentCode, 2) + bf.facultyCode + bf.nameTh + cp.programCode + cp.nameTh + cp.majorCode + cp.groupNum) as ss,
										 left(std.studentCode, 2) as AcadamicYear,
										 bf.facultyCode as FacultyCode,
										 bf.nameTh as FactTName,
										 cp.programCode as ProgramCode,
										 cp.nameTh as ProgTName,
										 cp.majorCode as MajorCode,
										 cp.groupNum as GroupNum
								from	 stdStudent as std left join
										 acaFaculty as bf on std.facultyId = bf.id left join
										 acaProgram as cp on std.facultyId = cp.facultyId and std.programId = cp.id inner join
										 perPerson as perpes on std.personId = perpes.id left join
										 perTitlePrefix as bt on perpes.perTitlePrefixId = bt.id inner join
										 ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel)= (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel)
								where	 ' + @nostudentid + ' and
										 (left(std.studentCode, 2) = "' + @acadamicyear + '")
							) as t1 left join
							(
								select	 (tt1.AcadamicYear + tt1.FacultyCode + tt1.FactTName + tt1.ProgramCode + tt1.ProgTName + tt1.MajorCode + tt1.GroupNum) as ss,
										 count(tt1.StudentID) as CountStudent
								from	 (
											select	left(std.studentCode, 2) as AcadamicYear,
													std.studentCode as StudentID,
													bf.facultyCode as FacultyCode,
													bf.nameTh as FactTName,
													cp.programCode as ProgramCode,
													cp.nameTh as ProgTName,
													cp.majorCode as MajorCode,
													cp.groupNum as GroupNum
											from	stdStudent as std left join
													acaFaculty as bf on std.facultyId = bf.id left join
													acaProgram as cp on std.facultyId = cp.facultyId and std.programId = cp.id inner join
													perPerson as perpes on std.personId = perpes.id left join
													perTitlePrefix as bt on perpes.perTitlePrefixId = bt.id inner join
													ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel) = (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel)
											where	' + @nostudentid + ' and ' + @nostudentstatus + ' and
													(LEFT(std.studentCode, 2) = "' + @acadamicyear + '")
										 ) as tt1
								group by (tt1.AcadamicYear + tt1.FacultyCode + tt1.FactTName + tt1.ProgramCode + tt1.ProgTName + tt1.MajorCode + tt1.GroupNum)
							) as t2 on t1.ss = t2.ss left join
							(
								select	 (tt1.AcadamicYear + tt1.FacultyCode + tt1.FactTName + tt1.ProgramCode + tt1.ProgTName + tt1.MajorCode + tt1.GroupNum) as ss,
										 count(tt1.StudentID) as CountStudentSignContract
								from	 (
											select	left(std.studentCode, 2) as AcadamicYear,
													std.studentCode as StudentID,
													bf.facultyCode as FacultyCode,
													bf.nameTh as FactTName,
													cp.programCode as ProgramCode,
													cp.nameTh as ProgTName,
													cp.majorCode as MajorCode,
													cp.groupNum as GroupNum
											from	stdStudent as std left join
													acaFaculty as bf on std.facultyId = bf.id left join
													acaProgram as cp on std.facultyId = cp.facultyId and std.programId = cp.id inner join
													perPerson as perpes on std.personId = perpes.id left join
													perTitlePrefix as bt on perpes.perTitlePrefixId = bt.id inner join
													ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel)= (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) inner join
													ectDocMgmt as cd on std.id = cd.StudentID and std.programId = cd.programId
											where	(cd.operationType = "S" or cd.operationType = "M") and ' + @nostudentid + ' and ' + @nostudentstatus + ' and 
													(left(std.studentCode, 2) = "' + @acadamicyear + '")
										 ) as tt1 
								group by (tt1.AcadamicYear + tt1.FacultyCode + tt1.FactTName + tt1.ProgramCode + tt1.ProgTName + tt1.MajorCode + tt1.GroupNum)
							) as t3 ON t1.ss = t3.ss left join
							(
								select	 (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 count(tt1.StudentID) as CountStudentContractPenalty
								from	 (
											select	left(cptbc.StudentID, 2) as AcadamicYear,
													cptbc.StudentID,
													cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum												
											from	ecpTransBreakContract as cptbc
											where	(cptbc.StatusCancel = 1) and
													(left(cptbc.StudentID, 2) = "' + @acadamicyear + '")
										 ) as tt1
								group by (tt1.AcadamicYear + tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t4 on t1.ss = t4.ss'

		exec (@sql)
	end
			 
	--select ReportStudentSignContract
	if (@ordertable = 40)
	begin
		set @where = ''
		
		if (@acadamicyear <> null)
			set @where = '(left(std.studentCode, 2) = "' + @acadamicyear + '")'
			
		if (@studentid <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((std.studentCode like "' + @studentid + '%") or (perpes.firstName like "%' + @studentid + '%") or (perpes.lastName like "%' + @studentid + '%"))'
		end
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptpg.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptpg.ProgramCode = "' + @program + '") and (cptpg.MajorCode = "' + @major + '") and (cptpg.GroupNum = "' + @groupnum + '"))'
		end
			
		if (@where <> '')
			set @where = ' and (' + @where + ')'
			
		set @sql = 'select	count(std.id) as CountReportStudentSignContract		
					from	stdStudent as std left join
							acaFaculty as bf on std.facultyId = bf.id left join
							acaProgram as cp on std.facultyId = cp.facultyId and std.programId = cp.id inner join
							perPerson as perpes on std.personId = perpes.id left join
							perTitlePrefix as bt on perpes.perTitlePrefixId = bt.id inner join
							ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel) = (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) left join
							ectDocMgmt as cd on std.id = cd.StudentID and std.programId = cd.programId left join
							vw_ectGetListParent as vwp on cd.StudentID = vwp.id and cd.parentCode = vwp.Type
					where	(cd.operationType = "S" or cd.operationType = "M") and ' + @nostudentid + ' and ' + @nostudentstatus + @where

		exec (@sql)
		
		set @sql = 'select	*									
					from	(
								select	row_number() over(order by std.studentCode) as RowNum,
										std.studentCode as StudentID,
										bt.thTitleFullName as TitleTName,
										perpes.firstName as ThaiFName,
										perpes.lastName as ThaiLName,
										cptpg.FacultyCode,
										bf.nameTh as FactTName,
										cptpg.ProgramCode,
										cp.nameTh as ProgTName,
										cptpg.MajorCode,
										cptpg.GroupNum,
										cptpg.DLevel,
										(
											case cptpg.DLevel
												when "U" then "ต่ำกว่าปริญญาตรี"
												when "B" then "ปริญญาตรี"
											else
												null
											end
										) as DLevelName,	
										cd.contractDateSignByStudent,
										vwp.TitleTName as GuarantorTitleTName,
										vwp.FirstName as GuarantorFirstName,
										vwp.LastName as GuarantorLastName
								from	stdStudent as std left join
										acaFaculty as bf on std.facultyId = bf.id left join
										acaProgram as cp on std.facultyId = cp.facultyId and std.programId = cp.id inner join
										perPerson as perpes on std.personId = perpes.id left join
										perTitlePrefix as bt on perpes.perTitlePrefixId = bt.id inner join
										ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel) = (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) left join
										ectDocMgmt as cd on std.id = cd.StudentID and std.programId = cd.programId left join
										vw_ectGetListParent as vwp on cd.StudentID = vwp.id and cd.parentCode = vwp.Type
								where	(cd.operationType = "S" or cd.operationType = "M") and ' + @nostudentid + ' and ' + @nostudentstatus + @where + '
							) as std1'

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (std1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)
	end

	--select ReportStatisticPaymentByDate
	if (@ordertable = 41)
	begin
		set @where = ''
		set @where1 = ''
		
		if (@cp1id <> null)
			set @where = '(cptbc.ID = ' + @cp1id + ')'
				
		if (@studentid <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") OR (cptbc.LastTName like "%' + @studentid + '%"))'
		end
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '		
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end

		if (@formatpayment <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptrc.FormatPayment = ' + @formatpayment + ')'		
		end
		
		if (@where <> '') 
			set @where = ' and (' + @where + ')'	

		if ((@datestart <> null) and
			(@dateend <> null))
			set @where1 = 'where (convert(date, convert(varchar, (convert(int, substring(cptpy.DateTimePayment, 7, 4)) - 543)) + "-" + substring(cptpy.DateTimePayment, 4, 2) + "-" + substring(cptpy.DateTimePayment, 1, 2)) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" and "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'			
			
		set @sql = 'select	count(cptbc.StudentID) as CountReportStatisticPaymentByDate		
					from	ecpTransBreakContract as cptbc inner join
							ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
							ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
							(
								select	 distinct
										 spl.studentCode,
										 spl.pictureName as FileName
								from	 stdStudent as spl
							) as spl1 on cptbc.StudentID = spl1.studentCode inner join
							(
								select	 cptpy1.RCID
								from	( 
											select	cptpy.RCID,
													cptpy.PayCapital
											from	ecpTransPayment as cptpy ' + @where1 + '
										 ) as cptpy1	 
								group by cptpy1.RCID
							) as cptpy2 on cptrc.ID = cptpy2.RCID
					where	(cptbc.StatusCancel = 1) and
							(cptrc.StatusPayment <> 1)' + @where
		
		exec (@sql)
		
		set @sql = 'select	*									
					from	(
								select	row_number() over(order by cptbc.StudentID) as RowNum,
										cptbc.ID as BCID,
										cptrc.ID as RCID,
										cptrc.TotalPenalty,
										cptpy2.TotalPay,
										cptrc.FormatPayment,
										(
											case cptrc.FormatPayment
												when 1 then "ชำระแบบเต็มจำนวน"
												when 2 then "ชำระแบบผ่อนชำระ"
											else
												null
											end
										) as FormatPaymentName,
										cptbc.StudentID,
										cptbc.TitleTName,
										cptbc.FirstTName,
										cptbc.LastTName, 
										cptbc.FacultyCode,
										cptbc.FacultyName,
										cptbc.ProgramCode,
										cptbc.ProgramName,
										cptbc.MajorCode,
										cptbc.GroupNum,
										cptbc.DLevel,
										(
											case cptbc.DLevel
												when "U" then "ต่ำกว่าปริญญาตรี"
												when "B" then "ปริญญาตรี"
											else
												null
											end	
										) as DLevelName,																																									
										cptbc.StatusCancel,
										cptrc.StatusPayment,
										spl1.FileName									
								from	ecpTransBreakContract as cptbc inner join
										ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
										ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
										(
											select	 distinct
													 spl.studentCode,
													 spl.pictureName as FileName
											from	 stdStudent as spl
										) as spl1 on cptbc.StudentID = spl1.studentCode inner join
										(
											select	 cptpy1.RCID,
													 sum(cptpy1.PayCapital) as TotalPay
											from	 (
														select	cptpy.RCID,
																cptpy.PayCapital
														from	ecpTransPayment as cptpy ' + @where1 + '
													 ) as cptpy1	 
											group by cptpy1.RCID
										) as cptpy2 on cptrc.ID = cptpy2.RCID
								where	(cptbc.StatusCancel = 1) and
										(cptrc.StatusPayment <> 1)' + @where + '
							) as cptbc1'

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (cptbc1.RowNum between ' + convert(VARCHAR, @startrow) + ' and ' + convert(varchar, @endrow) + ')'			
		
		exec (@sql)
	end

	
	--select ReportEContract
	if (@ordertable = 42)
	begin
		set @where = ''
		
		if (@acadamicyear <> null)
		begin
			set @where = '(left(std.studentCode, 2) = right("' + @acadamicyear + '", 2))'
		end
					  			
		if (@studentid <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((std.StudentCode like "' + @studentid + '%") or (perpes.firstName like "%' + @studentid + '%") or (perpes.lastName like "%' + @studentid + '%"))'
		end
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptpg.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cp.ProgramCode = "' + @program + '") and (cp.MajorCode = "' + @major + '") and (cp.GroupNum = "' + @groupnum + '"))'
		end

		if (@where <> '') 
			set @where = ' and ' + @where

		set @sql = 'select	count(std.id) as CountReportEContract
					from	stdStudent as std left join
							acaFaculty as bf on std.facultyId = bf.id left join
							acaProgram as cp on std.facultyId = cp.facultyId and std.programId = cp.id inner join
							perPerson as perpes on std.personId = perpes.id left join
							perTitlePrefix as bt on perpes.perTitlePrefixId = bt.id inner join
							ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel) = (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) left join
							ectDocMgmt as cd on std.id = cd.StudentID and std.programId = cd.programId left join
							vw_ectGetListParent as vwp on cd.StudentID = vwp.id and cd.parentCode = vwp.Type
					where	(cd.operationType = "S" or cd.operationType = "M" or cd.operationType = "O")' + @where + ' and ' + @nostudentid 

		exec (@sql)
		
		set @sql = 'select	*
					from	(
								select	row_number() over(order by std.studentCode) as RowNum,
										std.StudentCode as StudentID,
										bt.thTitleFullName as TitleTName,
										perpes.firstName as ThaiFName,
										perpes.lastName as ThaiLName, 
										cptpg.FacultyCode,
										bf.nameTh as FactTName,
										cp.ProgramCode,
										cp.nameTh as ProgTName, 
										cp.MajorCode,
										cp.GroupNum,
										cptpg.DLevel,
										(
											case cptpg.DLevel
												when "U" then "ต่ำกว่าปริญญาตรี"
												when "B" then "ปริญญาตรี"
											else
												null
											end
										) as DLevelName,	
										cd.contractDateSignByStudent,
										vwp.TitleTName as GuarantorTitleTName,
										vwp.FirstName as GuarantorFirstName,
										vwp.LastName AS GuarantorLastName,
										cd.QuotaCode,
										cd.operationType,
										cd.contractSignByStudent,
										cd.parentContractSignFlagF1 & cd.parentContractSignFlagF2 as parentContractSignFlagF,
										cd.parentContractSignFlagM1 & cd.parentContractSignFlagM2 as parentContractSignFlagM,
										replace(replace(cd.contractPath, "E:\econtract.mahidol.ac.th\ElectronicContract\", ""), "\", "/") as contractPath,
										replace(replace(cd.garranteePath, "E:\econtract.mahidol.ac.th\ElectronicContract\", ""), "\", "/") as garranteePath,
										replace(replace(cd.warranPath, "E:\econtract.mahidol.ac.th\ElectronicContract\", ""), "\", "/") as warranPath
								from	stdStudent as std left join
										acaFaculty as bf on std.facultyId = bf.id left join
										acaProgram as cp on std.facultyId = cp.facultyId and std.programId = cp.id inner join
										perPerson as perpes on std.personId = perpes.id left join
										perTitlePrefix as bt on perpes.perTitlePrefixId = bt.id inner join
										ecpTabProgram as cptpg on bf.facultyCode = cptpg.FacultyCode and (cp.programCode + cp.majorCode + cp.groupNum + cp.dLevel) = (cptpg.ProgramCode + cptpg.MajorCode + cptpg.GroupNum + cptpg.Dlevel) left join
										ectDocMgmt as cd on std.id = cd.StudentID and std.programId = cd.programId left join
										vw_ectGetListParent as vwp ON cd.StudentID = vwp.id and cd.parentCode = vwp.Type
								where	(cd.operationType = "S" or cd.operationType = "M" or cd.operationType = "O")' + @where + ' and ' + @nostudentid + '
							) as std1'

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (std1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)
	end
	
	--select ReportDebtorContract
	if (@ordertable = 43)
	begin
		set @where = ''
		
		if ((@datestart <> null) and
			(@dateend <> null))
			set @where = ' and (convert(date, convert(varchar, (convert(int, substring(cptrp.ReplyDate, 7, 4)) - 543)) + "-" + substring(cptrp.ReplyDate, 4, 2) + "-" + substring(cptrp.ReplyDate, 1, 2)) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" AND "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'
		
		set @sql = 'select	row_number() over(order by t1.ProgramCode) as RowNum,
							t1.FacultyCode,
							t1.FacultyName,
							t1.ProgramCode,
							t1.ProgramName,
							t1.MajorCode,
							t1.GroupNum,
							t1.DLevel,
							t1.DLevelName,
							t2.CountStudentDebtor,
							t3.SumTotalPenalty,
							t4.SumTotalPayCapital,
							t4.SumTotalPayInterest
					from	(
								select	 distinct
										(cptbc.FacultyCode + cptbc.FacultyName + cptbc.ProgramCode + cptbc.ProgramName + cptbc.MajorCode + cptbc.GroupNum) as ss,
										 cptbc.FacultyCode,
										 cptbc.FacultyName,
										 cptbc.ProgramCode,
										 cptbc.ProgramName,
										 cptbc.MajorCode,
										 cptbc.GroupNum,
										 cptbc.DLevel,
										 (
											case cptbc.DLevel
												when "U" then "ต่ำกว่าปริญญาตรี"
												when "B" then "ปริญญาตรี"
											else
												null
											end
										 ) as DLevelName
								from	 ecpTransBreakContract as cptbc inner join
										 ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
										 (
											select	cptrp.RCID,
													cptrp.StatusRepay,
													cptrp.ReplyDate
											from	ecpTransRepayContract as cptrp
											where	(cptrp.StatusReply = 2) and
													(cptrp.ReplyResult = 1)
										 ) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay
								where	 (cptbc.StatusCancel = 1)' + @where + '
							) as t1 left join
							(
								select	 (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 count(tt1.StudentID) as CountStudentDebtor
								from	 (
											select	cptbc.StudentID,
													cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													(
														select	cptrp.RCID,
																cptrp.StatusRepay,
																cptrp.ReplyDate
														from	ecpTransRepayContract as cptrp
														where	(cptrp.StatusReply = 2) and
																(cptrp.ReplyResult = 1)
													) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay
											where	(cptbc.StatusCancel = 1)' + @where + '
										 ) as tt1
								group by (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t2 on t1.ss = t2.ss left join		 	 
							(
								select	 (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 sum(tt1.TotalPenalty) as SumTotalPenalty
								from	 (
											select	cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum,
													cptrc.TotalPenalty
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													(
														select	cptrp.RCID,
																cptrp.StatusRepay,
																cptrp.ReplyDate
														from	ecpTransRepayContract as cptrp
														where	(cptrp.StatusReply = 2) and
																(cptrp.ReplyResult = 1)
													) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay
											where	(cptbc.StatusCancel = 1)' + @where + '
										 ) as tt1
								group by (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t3 on t1.ss = t3.ss left join							 
	 						(
								select	 (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 sum(tt1.PayCapital) as SumTotalPayCapital,
										 sum(tt1.PayInterest) as SumTotalPayInterest
								from	 (
											select	cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum,
													cptpy.PayCapital,
													cptpy.PayInterest,
													cptpy.TotalPay
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													(
														select	cptrp.RCID,
																cptrp.StatusRepay,
																cptrp.ReplyDate
														from	ecpTransRepayContract as cptrp
														where	(cptrp.StatusReply = 2) and
																(cptrp.ReplyResult = 1)
													) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
													ecpTransPayment as cptpy on cptrc.ID = cptpy.RCID
											where	(cptbc.StatusCancel = 1)' + @where + '
										 ) as tt1
								group by (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t4 on t1.ss = t4.ss'

		exec (@sql)							 
	end

	--select ReportDebtorContractByProgram
	if (@ordertable = 44)
	begin
		set @where = ''
				
		if (@studentid <> null)
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end

		if (@formatpayment <> null)			
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptrc.FormatPayment = ' + @formatpayment + ')'
		end

		if ((@datestart <> null) and
			(@dateend <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(convert(date, convert(varchar, (convert(int, substring(cptrp.ReplyDate, 7, 4)) - 543)) + "-" + substring(cptrp.ReplyDate, 4, 2) + "-" + substring(cptrp.ReplyDate, 1, 2)) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" and "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'
		end
					
		if (@where <> '') 
			set @where = ' and (' + @where + ')'	
			
		set @sql = 'select	count(cptbc.StudentID) as CountReportDebtorContractByProgram		
					from	ecpTransBreakContract as cptbc inner join
							ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
							(
								select	 cptrp.RCID,
										 cptrp.StatusRepay,
										 cptrp.ReplyDate
								from	 ecpTransRepayContract as cptrp
								where	 (cptrp.StatusReply = 2) and
										 (cptrp.ReplyResult = 1)
							) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
							(
								select	 cptpy.RCID
								from	 ecpTransPayment as cptpy
								group by cptpy.RCID
							) as cptpy on cptrc.ID = cptpy.RCID
					where	(cptbc.StatusCancel = 1)' + @where
		
		exec (@sql)
		
		set @sql = 'select	*
					from	(
								select	row_number() over(order by cptbc.ProgramCode, cptbc.StudentID) as RowNum,
										cptbc.ID as BCID,
										cptrc.ID as RCID,
										cptrc.TotalPenalty,
										cptpy.PayCapital,
										cptpy.PayInterest,
										cptrp.ReplyDate,
										cptrc.FormatPayment,
										(
											case cptrc.FormatPayment
												when 1 then "ชำระแบบเต็มจำนวน"
												when 2 then "ชำระแบบผ่อนชำระ"
											else
												null
											end
										) as FormatPaymentName,
										cptbc.StudentID,
										cptbc.TitleTName,
										cptbc.FirstTName,
										cptbc.LastTName, 
										cptbc.FacultyCode,
										cptbc.FacultyName,
										cptbc.ProgramCode,
										cptbc.ProgramName,
										cptbc.MajorCode,
										cptbc.GroupNum,
										cptbc.StatusSend,
										cptbc.StatusReceiver,
										cptbc.StatusEdit,
										cptbc.StatusCancel
								from	ecpTransBreakContract as cptbc inner join
										ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
										(
											select	 cptrp.RCID,
													 cptrp.StatusRepay,
													 cptrp.ReplyDate
											from	 ecpTransRepayContract as cptrp
											where	 (cptrp.StatusReply = 2) and
													 (cptrp.ReplyResult = 1)
										) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
										(
											select	 cptpy.RCID,
													 sum(cptpy.PayCapital) as PayCapital,
													 sum(cptpy.PayInterest) as PayInterest,
													 sum(cptpy.TotalPay) as TotalPay
											from	 ecpTransPayment as cptpy
											group by cptpy.RCID
										) as cptpy on cptrc.ID = cptpy.RCID
								where	(cptbc.StatusCancel = 1)' + @where + '
							) as cptbc1'

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (cptbc1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)
	end
	
	--select ReportDebtorContractPaid
	if (@ordertable = 45)
	begin
		set @where = ''

		if ((@datestart <> null) and
			(@dateend <> null))
			set @where = ' and (convert(date, convert(varchar, (convert(int, substring(cptrp.ReplyDate, 7, 4)) - 543)) + "-" + substring(cptrp.ReplyDate, 4, 2) + "-" + substring(cptrp.ReplyDate, 1, 2)) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" AND "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'			

		set @sql = 'select	row_number() over(order by t1.ProgramCode) as RowNum,
							t1.FacultyCode,
							t1.FacultyName,
							t1.ProgramCode,
							t1.ProgramName,
							t1.MajorCode,
							t1.GroupNum,
							t1.DLevel,
							t1.DLevelName,
							t2.CountStudentDebtor,
							t3.SumTotalPenalty,
							t4.SumTotalPayCapital,
							t4.SumTotalPayInterest							
					from	(
								select	 distinct
										 (cptbc.FacultyCode + cptbc.FacultyName + cptbc.ProgramCode + cptbc.ProgramName + cptbc.MajorCode + cptbc.GroupNum) as ss,
										 cptbc.FacultyCode,
										 cptbc.FacultyName,
										 cptbc.ProgramCode,
										 cptbc.ProgramName,
										 cptbc.MajorCode,
										 cptbc.GroupNum,
										 cptbc.DLevel,
										 (
											case cptbc.DLevel
												when "U" then "ต่ำกว่าปริญญาตรี"
												when "B" then "ปริญญาตรี"
											else
												null
											end
										 ) as DLevelName
								from	 ecpTransBreakContract as cptbc inner join
										 ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
										 ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
										 (
											select	 cptpy.RCID
											from	 ecpTransPayment as cptpy
											group by cptpy.RCID
										 ) as cptpy on cptrc.ID = cptpy.RCID
								where	 (cptbc.StatusCancel = 1) and
										 (cptrc.StatusPayment <> 1)' + @where + '
							) as t1 left join
							(
								select	 (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 count(tt1.StudentID) as CountStudentDebtor
								from	 (
											select	cptbc.StudentID,
													cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
													(
														select	 cptpy.RCID
														from	 ecpTransPayment as cptpy
														group by cptpy.RCID
													) as cptpy on cptrc.ID = cptpy.RCID							 							 												
											where	(cptbc.StatusCancel = 1) and
													(cptrc.StatusPayment <> 1)' + @where + '
										 ) as tt1
								group by (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t2 on t1.ss = t2.ss left join		 	 
							(
								select	 (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 sum(tt1.TotalPenalty) as SumTotalPenalty
								from 	 (
											select	cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum,
													cptrc.TotalPenalty
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
													(
														select	 cptpy.RCID
														from	 ecpTransPayment as cptpy
														group by cptpy.RCID
													) as cptpy on cptrc.ID = cptpy.RCID							 							 												
											where	(cptbc.StatusCancel = 1) and
													(cptrc.StatusPayment <> 1)' + @where + '
										 ) as tt1
								group by (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t3 on t1.ss = t3.ss left join							 
	 						(
								select	 (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 sum(tt1.PayCapital) as SumTotalPayCapital,
										 sum(tt1.PayInterest) as SumTotalPayInterest
								from	 (
											select	cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum,
													cptpy.PayCapital,
													cptpy.PayInterest,
													cptpy.TotalPay
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
													(
														select	 cptpy.RCID,
																 sum(cptpy.PayCapital) as PayCapital,
																 sum(cptpy.PayInterest) as PayInterest,
																 sum(cptpy.TotalPay) as TotalPay
														from	 ecpTransPayment as cptpy
														group by cptpy.RCID
													) as cptpy on cptrc.ID = cptpy.RCID							 							 												
											where	(cptbc.StatusCancel = 1) and
													(cptrc.StatusPayment <> 1)' + @where + '
										 ) as tt1
								group by (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t4 on t1.ss = t4.ss'
		
		exec (@sql)							 
	end
	
	--select ReportDebtorContractPaidByProgram
	if (@ordertable = 46)
	begin
		set @where = ''
				
		if (@studentid <> null)
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end
		
		if (@formatpayment <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptrc.FormatPayment = ' + @formatpayment + ')'		
		end
		
		if ((@datestart <> null) and
			(@dateend <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(convert(date, convert(varchar, (convert(int, substring(cptrp.ReplyDate, 7, 4)) - 543)) + "-" + substring(cptrp.ReplyDate, 4, 2) + "-" + substring(cptrp.ReplyDate, 1, 2)) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" AND "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'
		end
			
		if (@where <> '') 
			set @where = ' and (' + @where + ')'	
						
		set @sql = 'select	count(cptbc.StudentID) as CountReportDebtorContractByProgram		
					from	ecpTransBreakContract as cptbc inner join
							ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
							(
								select	 cptrp.RCID,
										 cptrp.StatusRepay,
										 cptrp.ReplyDate
								from	 ecpTransRepayContract as cptrp
								where	 (cptrp.StatusReply = 2) and
										 (cptrp.ReplyResult = 1)
							) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
							(
								select	 cptpy.RCID
								from	 ecpTransPayment as cptpy
								group by cptpy.RCID
							) as cptpy on cptrc.ID = cptpy.RCID
					where	(cptbc.StatusCancel = 1) and
							(cptrc.StatusPayment <> 1)' + @where

		exec (@sql)
		
		set @sql = 'select	*
					from	(
								select	row_number() over(order by cptbc.ProgramCode, cptbc.StudentID) as RowNum,
										cptbc.ID as BCID,
										cptrc.ID as RCID,
										cptrc.TotalPenalty,
										cptpy.PayCapital,
										cptpy.PayInterest,
										cptrp.ReplyDate,
										cptrc.FormatPayment,
										(
											case cptrc.FormatPayment
												when 1 then "ชำระแบบเต็มจำนวน"
												when 2 then "ชำระแบบผ่อนชำระ"
											else
												null
											end
										) as FormatPaymentName,									
										cptbc.StudentID,
										cptbc.TitleTName,
										cptbc.FirstTName,
										cptbc.LastTName, 
										cptbc.FacultyCode,
										cptbc.FacultyName,
										cptbc.ProgramCode,
										cptbc.ProgramName,
										cptbc.MajorCode,
										cptbc.GroupNum,
										cptbc.StatusSend,
										cptbc.StatusReceiver,
										cptbc.StatusEdit,
										cptbc.StatusCancel
								from	ecpTransBreakContract as cptbc inner join
										ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
										(
											select	 cptrp.RCID,
													 cptrp.StatusRepay,
													 cptrp.ReplyDate
											from	 ecpTransRepayContract as cptrp
											where	 (cptrp.StatusReply = 2) and
													 (cptrp.ReplyResult = 1)
										) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
										(
											select	 cptpy.RCID,
													 sum(cptpy.PayCapital) as PayCapital,
													 sum(cptpy.PayInterest) as PayInterest,
													 sum(cptpy.TotalPay) as TotalPay
											from	 ecpTransPayment as cptpy
											group by cptpy.RCID
										) as cptpy on cptrc.ID = cptpy.RCID
								where	(cptbc.StatusCancel = 1) and
										(cptrc.StatusPayment <> 1)' + @where + '
							) as cptbc1'

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (cptbc1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)
	end
	
	--select ReportDebtorContractRemain
	if (@ordertable = 47)
	begin
		set @where = ''
		
		if ((@datestart <> null) and
			(@dateend <> null))
			set @where = ' and (convert(date, convert(varchar, (convert(int, substring(cptrp.ReplyDate, 7, 4)) - 543)) + "-" + substring(cptrp.ReplyDate, 4, 2) + "-" + substring(cptrp.ReplyDate, 1, 2)) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" and "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'
		
		set @sql = 'select	row_number() over(order by t1.ProgramCode) as RowNum,
							t1.FacultyCode,
							t1.FacultyName,
							t1.ProgramCode,
							t1.ProgramName,
							t1.MajorCode,
							t1.GroupNum,
							t1.DLevel,
							t1.DLevelName,
							t2.CountStudentDebtor,
							isnull(t3.SumTotalPenalty, 0) as SumTotalPenalty,
							isnull(t4.SumTotalPayCapital, 0) as SumTotalPayCapital,
							isnull(t4.SumTotalPayInterest, 0) as SumTotalPayInterest
					from	(
								select	distinct
										(cptbc.FacultyCode + cptbc.FacultyName + cptbc.ProgramCode + cptbc.ProgramName + cptbc.MajorCode + cptbc.GroupNum) as ss,
										cptbc.FacultyCode,
										cptbc.FacultyName,
										cptbc.ProgramCode,
										cptbc.ProgramName,
										cptbc.MajorCode,
										cptbc.GroupNum,
										cptbc.DLevel,
										(
											case cptbc.DLevel
												when "U" then "ต่ำกว่าปริญญาตรี"
												when "B" then "ปริญญาตรี"
											else
												null
											end
										) as DLevelName
								from	ecpTransBreakContract as cptbc inner join
										ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
										(
											select	 cptrp.RCID,
													 cptrp.StatusRepay,
													 cptrp.ReplyDate
											from	 ecpTransRepayContract as cptrp
											where	 (cptrp.StatusReply = 2) and
													 (cptrp.ReplyResult = 1)
										) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
										(
											select	 cptpy.RCID
											from	 ecpTransPayment as cptpy
											group by cptpy.RCID
										) as cptpy on cptrc.ID = cptpy.RCID
								where	(cptbc.StatusCancel = 1) and
										(cptrc.StatusPayment <> 3)' + @where + '
							) as t1 left join
							(
								select	 (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 count(tt1.StudentID) as CountStudentDebtor
								from	 (
											select	cptbc.StudentID,
													cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													(
														select	 cptrp.RCID,
																 cptrp.StatusRepay,
																 cptrp.ReplyDate
														from	 ecpTransRepayContract as cptrp
														where	 (cptrp.StatusReply = 2) and
																 (cptrp.ReplyResult = 1)
													) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
													(
														select	 cptpy.RCID
														from	 ecpTransPayment as cptpy
														group by cptpy.RCID
													) as cptpy on cptrc.ID = cptpy.RCID
											where	(cptbc.StatusCancel = 1) and
													(cptrc.StatusPayment <> 3)' + @where + '
										 ) as tt1
								group by (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t2 on t1.ss = t2.ss left join		 	 
							(
								select	 (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 sum(tt1.TotalPenalty) as SumTotalPenalty
								from	 (
											select	cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum,
													cptrc.TotalPenalty
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													(
														select	 cptrp.RCID,
																 cptrp.StatusRepay,
																 cptrp.ReplyDate
														from	 ecpTransRepayContract as cptrp
														where	 (cptrp.StatusReply = 2) and
																 (cptrp.ReplyResult = 1)
													) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
													(
														select	 cptpy.RCID
														from	 ecpTransPayment as cptpy
														group by cptpy.RCID
													) as cptpy on cptrc.ID = cptpy.RCID
											where	(cptbc.StatusCancel = 1) and
													(cptrc.StatusPayment <> 3)' + @where + '
										 ) as tt1
								group by (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t3 ON t1.ss = t3.ss left join							 
	 						(
								select	 (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum) as ss,
										 sum(tt1.PayCapital) as SumTotalPayCapital,
										 sum(tt1.PayInterest) as SumTotalPayInterest
								from	 (
											select	cptbc.FacultyCode,
													cptbc.FacultyName,
													cptbc.ProgramCode,
													cptbc.ProgramName,
													cptbc.MajorCode,
													cptbc.GroupNum,
													cptpy.PayCapital,
													cptpy.PayInterest,
													cptpy.TotalPay
											from	ecpTransBreakContract as cptbc inner join
													ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
													(
														select	cptrp.RCID,
																cptrp.StatusRepay,
																cptrp.ReplyDate
														from	ecpTransRepayContract as cptrp
														where	(cptrp.StatusReply = 2) and
																(cptrp.ReplyResult = 1)
													) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
													ecpTransPayment as cptpy on cptrc.ID = cptpy.RCID
											where	(cptbc.StatusCancel = 1) and
													(cptrc.StatusPayment <> 3)' + @where + '
										 ) as tt1
								group by (tt1.FacultyCode + tt1.FacultyName + tt1.ProgramCode + tt1.ProgramName + tt1.MajorCode + tt1.GroupNum)
							) as t4 on t1.ss = t4.ss'
		
		exec (@sql)							 
	end

	--select ReportDebtorContractReaminByProgram
	if (@ordertable = 48)
	begin
		set @where = ''
				
		if (@studentid <> null)
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end

		if (@formatpayment <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptrc.FormatPayment = ' + @formatpayment + ')'		
		end

		if ((@datestart <> null) and
			(@dateend <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(convert(date, convert(varchar, (convert(int, substring(cptrp.ReplyDate, 7, 4)) - 543)) + "-" + substring(cptrp.ReplyDate, 4, 2) + "-" + substring(cptrp.ReplyDate, 1, 2)) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" and "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'
		end
					
		if (@where <> '') 
			set @where = ' and (' + @where + ')'	
			
		set @sql = 'select	count(cptbc.StudentID) as CountReportDebtorContractByProgram		
					from	ecpTransBreakContract as cptbc inner join
							ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
							(
								select	 cptrp.RCID,
										 cptrp.StatusRepay,
										 cptrp.ReplyDate
								from	 ecpTransRepayContract as cptrp
								where	 (cptrp.StatusReply = 2) and
										 (cptrp.ReplyResult = 1)
							) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
							(
								select	 cptpy.RCID
								from	 ecpTransPayment as cptpy
								group by cptpy.RCID
							) as cptpy on cptrc.ID = cptpy.RCID
					where	(cptbc.StatusCancel = 1) and
							(cptrc.StatusPayment <> 3)' + @where
		
		exec (@sql)
		
		set @sql = 'select	*									
					from	(
								select	row_number() over(order by cptbc.ProgramCode, cptbc.StudentID) as RowNum,
										cptbc.ID AS BCID,
										cptrc.ID AS RCID,
										cptrc.TotalPenalty,
										cptpy.PayCapital,
										cptpy.PayInterest,
										cptrp.ReplyDate,
										cptrc.FormatPayment,
										(
											case cptrc.FormatPayment
												when 1 then "ชำระแบบเต็มจำนวน"
												when 2 then "ชำระแบบผ่อนชำระ"
											else
												null
											end
										) as FormatPaymentName,
										cptbc.StudentID,
										cptbc.TitleTName,
										cptbc.FirstTName,
										cptbc.LastTName, 
										cptbc.FacultyCode,
										cptbc.FacultyName,
										cptbc.ProgramCode,
										cptbc.ProgramName,
										cptbc.MajorCode,
										cptbc.GroupNum,
										cptbc.StatusSend,
										cptbc.StatusReceiver,
										cptbc.StatusEdit,
										cptbc.StatusCancel
								from	ecpTransBreakContract as cptbc inner join
										ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
										(
											select	 cptrp.RCID,
													 cptrp.StatusRepay,
													 cptrp.ReplyDate
											from	 ecpTransRepayContract as cptrp
											where	 (cptrp.StatusReply = 2) and
													 (cptrp.ReplyResult = 1)
										) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
										(
											select	 cptpy.RCID,
													 sum(cptpy.PayCapital) as PayCapital,
													 sum(cptpy.PayInterest) as PayInterest,
													 sum(cptpy.TotalPay) as TotalPay
											from	 ecpTransPayment as cptpy
											group by cptpy.RCID
										) as cptpy on cptrc.ID = cptpy.RCID
								where	(cptbc.StatusCancel = 1) and
										(cptrc.StatusPayment <> 3)' + @where + '
							) as cptbc1'

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (cptbc1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)
	end
		
	--select ExportDebtorContract
	if (@ordertable = 49)
	begin
		set @where = ''
				
		if (@studentid <> null)
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end

		if (@formatpayment <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptrc.FormatPayment = ' + @formatpayment + ')'		
		end

		if ((@datestart <> null) and
			(@dateend <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(convert(date, convert(varchar, (convert(int, substring(cptrp.ReplyDate, 7, 4)) - 543)) + "-" + substring(cptrp.ReplyDate, 4, 2) + "-" + substring(cptrp.ReplyDate, 1, 2)) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" and "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'
		end
			
		if (@where <> '') 
			set @where = ' and (' + @where + ')'	
			
		set @sql = 'select	 cptbc.ID as BCID,
							 cptrc.ID as RCID,
							 cptbc.StudentID,
							 cptbc.TitleTName,
							 cptbc.FirstTName,
							 cptbc.LastTName, 
							 cptbc.FacultyCode,
							 cptbc.FacultyName,
							 cptbc.ProgramCode,
							 cptbc.ProgramName,
							 cptbc.MajorCode,
							 cptbc.GroupNum,
							 cptbc.DLevel,
							 (
								case cptbc.DLevel
									when "U" then "ต่ำกว่าปริญญาตรี"
									when "B" then "ปริญญาตรี"
								else
									null
								end
							 ) as DLevelName,							 
							 cptbc.StatusSend,
							 cptbc.StatusReceiver,
							 cptbc.StatusEdit,
							 cptbc.StatusCancel,
							 cptbc.CivilFlag,
							 cptbc.IndemnitorYear,
							 cptrc.AllActualDate,
							 cptbc.IndemnitorCash,
							 cptrc.RequireDate,
							 cptrc.ApproveDate,
							 cptrc.ActualDate,
							 cptrc.RemainDate,
							 cptrc.TotalPenalty,
							 cptrp.ReplyDate,
							 cptrc.FormatPayment,
							 (
								case cptrc.FormatPayment
									when 1 then "ชำระแบบเต็มจำนวน"
									when 2 then "ชำระแบบผ่อนชำระ"
								else
									null
								end
							 ) as FormatPaymentName
					from	 ecpTransBreakContract as cptbc inner join
	 						 ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
							 (
								select	cptrp.RCID,
										cptrp.StatusRepay,
										cptrp.ReplyDate
								from	ecpTransRepayContract as cptrp
								where	(cptrp.StatusReply = 2) and
										(cptrp.ReplyResult = 1)
							 ) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay
					where	 (cptbc.StatusCancel = 1)' + @where + ' 
					order by cptbc.ProgramCode, cptbc.StudentID'

		exec (@sql)
		
		set @sql = 'select	sum(tt1.TotalPenalty) as TotalPenalty
					from	(
								select	cptrc.TotalPenalty
								from	ecpTransBreakContract as cptbc inner join
	 									ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
										(
											select	cptrp.RCID,
													cptrp.StatusRepay,
													cptrp.ReplyDate
											from	ecpTransRepayContract as cptrp
											where	(cptrp.StatusReply = 2) and
													(cptrp.ReplyResult = 1)
										) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay
								where	(cptbc.StatusCancel = 1)' + @where + '
							) as tt1'
		
		exec (@sql)		
	end
	
	--select ExportDebtorContractPaid
	if (@ordertable = 50)
	begin
		set @where = ''
		set @where1 = ''
				
		if (@studentid <> null)
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end

		if (@formatpayment <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptrc.FormatPayment = ' + @formatpayment + ')'		
		end
										
		if ((@datestart <> null) and
			(@dateend <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(convert(date, convert(varchar, (convert(int, substring(cptrp.ReplyDate, 7, 4)) - 543)) + "-" + substring(cptrp.ReplyDate, 4, 2) + "-" + substring(cptrp.ReplyDate, 1, 2)) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" and "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'	
		end
		
		if (@where <> '') 
			set @where = ' and (' + @where + ')'	
			
		set @sql = 'select	 cptbc.ID as BCID,
							 cptrc.ID as RCID,
							 cptbc.StudentID,
							 cptbc.TitleTName,
							 cptbc.FirstTName,
							 cptbc.LastTName, 
							 cptbc.FacultyCode,
							 cptbc.FacultyName,
							 cptbc.ProgramCode,
							 cptbc.ProgramName,
							 cptbc.MajorCode,
							 cptbc.GroupNum,
							 cptbc.DLevel,
							 (
								case cptbc.DLevel
									when "U" then "ต่ำกว่าปริญญาตรี"
									when "B" then "ปริญญาตรี"
								else
									null
								end
							 ) as DLevelName,
							 cptbc.StatusSend,
							 cptbc.StatusReceiver,
							 cptbc.StatusEdit,
							 cptbc.StatusCancel,
							 cptrp.ReplyDate,
							 cptpy.DateTimePayment,
							 cptpy.Capital,
							 cptpy.Interest,
							 cptpy.TotalAccruedInterest,
							 cptpy.TotalInterest,
							 cptpy.TotalPayment,
							 cptpy.ReceiptDate,
							 cptpy.ReceiptBookNo,
							 cptpy.ReceiptNo,
							 cptpy.PayCapital,
							 cptpy.PayInterest,
							 cptpy.TotalPay,
							 cptpy.ReceiptSendNo,
							 cptpy.ReceiptFund,
							 cptpy.RemainCapital,
							 cptpy.AccruedInterest,
							 cptpy.RemainAccruedInterest,
							 cptpy.TotalRemain,
							 cptrc.FormatPayment,
							 (
								case cptrc.FormatPayment
									when 1 then "ชำระแบบเต็มจำนวน"
									when 2 then "ชำระแบบผ่อนชำระ"
								else
									null
								end
							 ) as FormatPaymentName
					from	 ecpTransBreakContract as cptbc inner join
							 ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
							 (
								select	cptrp.RCID,
										cptrp.StatusRepay,
										cptrp.ReplyDate
								from	ecpTransRepayContract as cptrp
								where	(cptrp.StatusReply = 2) and
										(cptrp.ReplyResult = 1)
							 ) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
							 (
								select	 cptpy.RCID,
										 cptpy.ID,
										 cptpy.Capital,
										 cptpy.Interest,
										 cptpy.TotalAccruedInterest,
										 (cptpy.Interest + cptpy.TotalAccruedInterest) as TotalInterest,
										 cptpy.TotalPayment,
										 cptpy.PayCapital,
										 cptpy.PayInterest,
										 cptpy.TotalPay,
										 cptpy.RemainCapital,
										 cptpy.AccruedInterest,
										 cptpy.RemainAccruedInterest,
										 cptpy.TotalRemain,
										 cptpy.DateTimePayment,
										 cptpy.ReceiptDate,
										 cptpy.ReceiptBookNo,
										 cptpy.ReceiptNo,
										 cptpy.ReceiptSendNo,
										 cptpy.ReceiptFund			 
								from	 ecpTransPayment as cptpy inner join
										 (
											select	 cptpy.RCID,
													 max(cptpy.ID) as MaxPeriod
											from	 ecpTransPayment as cptpy
											group by cptpy.RCID
										 ) as cptpy1 on cptpy.ID = cptpy1.MaxPeriod
							 ) as cptpy on cptrc.ID = cptpy.RCID
					where	 (cptbc.StatusCancel = 1) and
							 (cptrc.StatusPayment <> 1)' + @where + ' 
					order by cptbc.ProgramCode, cptbc.StudentID, cptpy.ID'

		exec (@sql)
				
		set @sql = 'select	sum(t1.Capital) as TotalCapital,
							sum(t1.TotalInterest) as TotalInterest,
							sum(t1.TotalPayment) as TotalPayment,
							sum(t1.PayCapital) as PayCapital,
							sum(t1.PayInterest) as PayInterest,
							sum(t1.TotalPay) as TotalPay,
							sum(t1.RemainCapital) as RemainCapital,
							sum(t1.RemainInterest) as RemainInterest,
							sum(t1.TotalRemain) as TotalRemain
					from	(
								select	cptpy.RCID,
										cptpy.ID,
										cptpy.Capital,
										cptpy.Interest,
										cptpy.TotalInterest,
										cptpy.TotalPayment,
										cptpy.PayCapital,
										cptpy.PayInterest,
										cptpy.TotalPay,
										cptpy.RemainCapital,
										cptpy.RemainAccruedInterest as RemainInterest,
										cptpy.TotalRemain
								from	ecpTransBreakContract as cptbc inner join
										ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
										(
											select	cptrp.RCID,
													cptrp.StatusRepay,
													cptrp.ReplyDate
											from	ecpTransRepayContract as cptrp
											where	(cptrp.StatusReply = 2) and
													(cptrp.ReplyResult = 1)
										) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay inner join
										(
											select	cptpy.RCID,
													cptpy.ID,
													cptpy.Capital,
													cptpy.Interest,
													(cptpy.Interest + cptpy.TotalAccruedInterest) as TotalInterest,
													cptpy.TotalPayment,
													cptpy.PayCapital,
													cptpy.PayInterest,
													cptpy.TotalPay,
													cptpy.RemainCapital,
													cptpy.RemainAccruedInterest,
													cptpy.TotalRemain				 
											from	ecpTransPayment as cptpy inner join
													(
														select	 cptpy.RCID,
																 max(cptpy.ID) as MaxPeriod
														from	 ecpTransPayment as cptpy
														group by cptpy.RCID
													) as cptpy1 on cptpy.ID = cptpy1.MaxPeriod
										) as cptpy on cptrc.ID = cptpy.RCID
								where	(cptbc.StatusCancel = 1) and
										(cptrc.StatusPayment <> 1)' + @where + '
							) as t1'
		exec (@sql)
	end
	
	--select ExportDebtorContractRemain
	if (@ordertable = 51)
	begin
		set @where = ''
				
		if (@studentid <> null)
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end

		if (@formatpayment <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptrc.FormatPayment = ' + @formatpayment + ')'		
		end

		if ((@datestart <> null) and
			(@dateend <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(convert(date, convert(varchar, (convert(int, substring(cptrp.ReplyDate, 7, 4)) - 543)) + "-" + substring(cptrp.ReplyDate, 4, 2) + "-" + substring(cptrp.ReplyDate, 1, 2)) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" and "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'
		end
			
		if (@where <> '') 
			set @where = ' and (' + @where + ')'	
			
		set @sql = 'select	 cptbc.ID as BCID,
							 cptrc.ID as RCID,
							 cptbc.StudentID,
							 cptbc.TitleTName,
							 cptbc.FirstTName,
							 cptbc.LastTName, 
							 cptbc.FacultyCode,
							 cptbc.FacultyName,
							 cptbc.ProgramCode,
							 cptbc.ProgramName,
							 cptbc.MajorCode,
							 cptbc.GroupNum,
							 cptbc.DLevel,
							 (
								case cptbc.DLevel
									when "U" then "ต่ำกว่าปริญญาตรี"
									when "B" then "ปริญญาตรี"
								else
									null
								end
							 ) as DLevelName,							 
							 cptbc.StatusSend,
							 cptbc.StatusReceiver,
							 cptbc.StatusEdit,
							 cptbc.StatusCancel,
							 cptbc.CivilFlag,
							 cptbc.IndemnitorYear,
							 cptrc.AllActualDate,
							 cptbc.IndemnitorCash,
							 cptrc.RequireDate,
							 cptrc.ApproveDate,
							 cptrc.ActualDate,
							 cptrc.RemainDate,
							 cptrc.TotalPenalty,
							 cptpy.RemainCapital,
							 cptpy.AccruedInterest,
							 cptpy.RemainAccruedInterest,
							 cptpy.TotalRemain,
							 cptrp.ReplyDate,
							 cptrc.StatusPayment,
							 cptrc.FormatPayment,
							 (
								case cptrc.FormatPayment
									when 1 then "ชำระแบบเต็มจำนวน"
									when 2 then "ชำระแบบผ่อนชำระ"
								else
									null
								end
							 ) as FormatPaymentName
					from	 ecpTransBreakContract as cptbc inner join
	 						 ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
	 						 (
								select	 cptrp.RCID,
										 cptrp.StatusRepay,
										 cptrp.ReplyDate
								from	 ecpTransRepayContract as cptrp
								where	 (cptrp.StatusReply = 2) and
										 (cptrp.ReplyResult = 1)
							 ) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
							 (
								select	 cptpy.RCID,
										 cptpy.ID,
										 cptpy.Capital,
										 cptpy.Interest,
										 cptpy.TotalAccruedInterest,
										 (cptpy.Interest + cptpy.TotalAccruedInterest) as TotalInterest,
										 cptpy.TotalPayment,
										 cptpy.PayCapital,
										 cptpy.PayInterest,
										 cptpy.TotalPay,
										 cptpy.RemainCapital,
										 cptpy.AccruedInterest,
										 cptpy.RemainAccruedInterest,
										 cptpy.TotalRemain
								from	 ecpTransPayment as cptpy inner join
										 (
											select	 cptpy.RCID,
													 max(cptpy.ID) as MaxPeriod
											from	 ecpTransPayment as cptpy
											group by cptpy.RCID
										 ) as cptpy1 on cptpy.ID = cptpy1.MaxPeriod
							 ) as cptpy on cptrc.ID = cptpy.RCID
					where	 (cptbc.StatusCancel = 1) and
							 (cptrc.StatusPayment <> 3)' + @where + ' 
					order by cptbc.ProgramCode, cptbc.StudentID'
		
		exec (@sql)
		
		set @sql = 'select	sum(tt1.TotalPenalty) as TotalPenalty
					from	(
								select	cptrc.TotalPenalty
								from	ecpTransBreakContract as cptbc inner join
	 									ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID inner join
										(
											select	 cptrp.RCID,
													 cptrp.StatusRepay,
													 cptrp.ReplyDate
											from	 ecpTransRepayContract as cptrp
											where	 (cptrp.StatusReply = 2) and
													 (cptrp.ReplyResult = 1)
										) as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
										(
											select	 cptpy.RCID
											from	 ecpTransPayment as cptpy
											group by cptpy.RCID
										) as cptpy on cptrc.ID = cptpy.RCID
								where	(cptbc.StatusCancel = 1) and
										(cptrc.StatusPayment <> 3)' + @where + '
							) as tt1'
		
		exec (@sql)
	end

	--select ecpTransRequireContract*/
	if (@ordertable = 53)
	begin
		set @where = ''
		
		if (@cp1id <> null)
			set @where = '(cptbc.ID = ' + @cp1id + ')'

		if (@statusrepay <> null)
			set @where = '(cptrc.StatusRepay = ' + @statusrepay + ')'
		
		if (@statusreply <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '

			set @where = @where + '(cptrp.StatusReply = ' + @statusreply + ')'
		end

		if (@replyresult <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptrp.ReplyResult = ' + @replyresult + ')'
		end

		if (@statuspayment <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptrc.StatusPayment = ' + @statuspayment + ')'
		end

		if (@studentid <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.StudentID like "' + @studentid + '%") or (cptbc.FirstTName like "%' + @studentid + '%") or (cptbc.LastTName like "%' + @studentid + '%"))'
		end
		
		if (@faculty <> null)
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(cptbc.FacultyCode = "' + @faculty + '")'
		end
		
		if ((@program <> null) and
			(@major <> null) and
			(@groupnum <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '((cptbc.ProgramCode = "' + @program + '") and (cptbc.MajorCode = "' + @major + '") and (cptbc.GroupNum = "' + @groupnum + '"))'
		end
		
		if ((@datestart <> null) and
			(@dateend <> null))
		begin
			if (@where <> '')
				set @where = @where + ' and '
			
			set @where = @where + '(convert(date, cptbc.DateTimeReceiver) between "' + convert(varchar, (convert(int, substring(@datestart, 7, 4)) - 543)) + "-" + substring(@datestart, 4, 2) + "-" + substring(@datestart, 1, 2) + '" and "' + convert(varchar, (convert(int, substring(@dateend, 7, 4)) - 543)) + "-" + substring(@dateend, 4, 2) + "-" + substring(@dateend, 1, 2) + '")'
		end
		
		if (@where <> '') 
			set @where = ' where (' + @where + ') and '
		else
			set @where = ' where '
		
		if (@cp1id <> null)
			set @where = @where + @repaystatus1
		else
			set @where = @where + @repaystatus2			
		
		set @sql = 'select	count(cptrc.ID) as CountRepay
					from	ecpTransRequireContract as cptrc left join
							ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
							plcProvince as bp on cptrc.Province = bp.id inner join
							ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID'
		set @sql = @sql + @where

		exec (@sql)
		
		set @sql = 'select	*
					from	(
								select	row_number() over(order by cptbc.DateTimeReceiver desc) as RowNum,
										cptrc.ID,
										cptrc.BCID,
										cptrc.StatusRepay,
										cptrc.StatusPayment,	
										cptrp.RepayDate,
										isnull(cptrp1.StatusReply, cptrp.StatusReply) as StatusReply,
										isnull(cptrp1.ReplyResult, cptrp.ReplyResult) as ReplyResult,
										(case when (cptrp1.StatusReply = "2") and (cptrp1.ReplyResult = "1") then cptrp1.ReplyDate else null end) as ReplyDate,
										cptbc.StudentID,
										cptbc.TitleTName,
										cptbc.FirstTName,
										cptbc.LastTName, 
										cptbc.FacultyCode,
										cptbc.FacultyName,
										cptbc.ProgramCode,
										cptbc.ProgramName,
										cptbc.MajorCode,
										cptbc.GroupNum,
										cptbc.StatusSend,
										cptbc.StatusReceiver,
										cptbc.StatusEdit,
										cptbc.StatusCancel,
										cptbc.DateTimeCreate,
										cptbc.DateTimeModify,
										cptbc.DateTimeCancel,
										cptbc.DateTimeSend,
										cptbc.DateTimeReceiver
								from	ecpTransRequireContract as cptrc left join
										ecpTransRepayContract as cptrp on cptrc.ID = cptrp.RCID and cptrc.StatusRepay = cptrp.StatusRepay left join
										(
											select	cptrp.RCID,
													cptrp.StatusRepay,
													cptrp.RepayDate,
													cptrp.StatusReply,
													cptrp.ReplyResult,
													cptrp.ReplyDate
											from	ecpTransRepayContract as cptrp inner join
													(
														select	 cptrp.RCID,
																 max(cptrp.StatusRepay) as StatusRepay
														from	 ecpTransRepayContract as cptrp
														group by cptrp.RCID
											) as cptrp1 on cptrp.RCID = cptrp1.RCID and cptrp.StatusRepay = cptrp1.StatusRepay
										) as cptrp1 on cptrp.RCID = cptrp1.RCID and cptrp.StatusRepay = cptrp1.StatusRepay left join
										plcProvince as bp on cptrc.Province = bp.id inner join
										ecpTransBreakContract as cptbc on cptrc.BCID = cptbc.ID left join
										ecpTabPayBreakContract as cptpbc on cptbc.FacultyCode = cptpbc.FacultyCode and (cptbc.ProgramCode + cptbc.MajorCode + cptbc.GroupNum + cptbc.DLevel) = (cptpbc.ProgramCode + cptpbc.MajorCode + cptpbc.GroupNum + cptpbc.Dlevel) and (cptbc.CaseGraduate = cptpbc.CaseGraduate) and (cptbc.CalDateCondition = cptpbc.CalDateCondition)' + @where + ' 
							) as cptrc1'

		if ((@startrow <> null) and
			(@endrow <> null))
			set @sql = @sql + ' where (cptrc1.RowNum between ' + convert(varchar, @startrow) + ' and ' + convert(varchar, @endrow) + ')'
		
		exec (@sql)
	end

	--insert update delete
	if (@ordertable = 52)
	begin
		set @sql = null

		if (@cmd <> null)
		begin
			select @sql = @cmd	
		
			begin tran
				exec (@sql)
				
				if (@@ERROR <> 0)
				begin
					rollback tran
					return
				end
			commit tran
		end
	end
end