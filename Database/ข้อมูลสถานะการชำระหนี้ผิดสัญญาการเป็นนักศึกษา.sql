use Infinity

select	 cptbc.StudentID,
		 cptbc.ID as BCID,
		 cptrc.ID as RCID,
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
				when 'U' then 'ต่ำกว่าปริญญาตรี'
				when 'B' then 'ปริญญาตรี'
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
		 cptbc.DateTimeSend,
		 (case when (cptbc.StatusSend = 1) then 'รอส่งรายการแจ้ง' else (left(convert(varchar, cptbc.DateTimeSend, 103), 6) + convert(varchar, (year(cptbc.DateTimeSend) + 543))) end) as SendResult,
		 cptbc.StatusReceiver,
		 cptbc.DateTimeReceiver,
		 (case when (cptbc.StatusReceiver = 1) then null else (left(convert(varchar, cptbc.DateTimeReceiver, 103), 6) + convert(varchar, (year(cptbc.DateTimeReceiver) + 543))) end) as ReceiverResult,
		 cptbc.StatusEdit,
		 cptbc.StatusCancel,
		 cptbc.DateTimeCreate,
		 cptbc.DateTimeModify,
		 cptbc.DateTimeCancel,		 		 
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
		 replace(cptrc.LawyerPhoneNumber, '-', ' ') as LawyerPhoneNumber,
		 replace(cptrc.LawyerMobileNumber, '-', ' ') as LawyerMobileNumber,
		 cptrc.LawyerEmail,
		 tpmsum.TotalPayCapital,
		 tpmsum.TotalPayInterest,
		 tpmsum.TotalPay,
		 tpmmax.TotalRemain,
		 tpmmax.RemainAccruedInterest,
		 cptrc.StatusRepay,
		 cptrc.StatusPayment,
		 (
			case cptrc.StatusPayment 				
				when 2 then 'อยู่ระหว่างการชำระหนี้'
				when 3 then 'ชำระหนี้เรียบร้อย'
			else
				case (convert(varchar, isnull(cptrc.StatusRepay, '')) + convert(varchar, isnull(cptrp.StatusReply, '')) + convert(varchar, isnull(cptrp.ReplyResult, '')))
					when '000' then 'รอแจ้งชำระหนี้'
					when '110' then 'แจ้งชำระหนี้ครั้งที่ 1 ( รอเอกสารตอบกลับ )'
					when '121' then 'แจ้งชำระหนี้ครั้งที่ 1 ( ผู้ผิดสัญญารับทราบให้ชำระหนี้ )'
					when '122' then 'แจ้งชำระหนี้ครั้งที่ 1 ( ผู้ผิดสัญญาไม่รับทราบให้ชำระหนี้ )'
					when '210' then 'แจ้งชำระหนี้ครั้งที่ 2 ( รอเอกสารตอบกลับ )'
					when '221' then 'แจ้งชำระหนี้ครั้งที่ 2 ( ผู้ผิดสัญญารับทราบให้ชำระหนี้ )'
					when '222' then 'แจ้งชำระหนี้ครั้งที่ 2 ( ผู้ผิดสัญญาไม่รับทราบให้ชำระหนี้ )'
				else
					null
				end
			end
		 ) as StatusPaymentResult,
		 cptrc.FormatPayment,
		 cptrp.StatusReply,
		 cptrp.ReplyResult,		 
		 cptrp.ReplyDate,
		 rpdhis.ReplyDateHistory,
		 rp1rpy.ReplyDate as Repay1ReplyDate
from	 ecpTransBreakContract as cptbc left join
		 ecpTransRequireContract as cptrc on cptbc.ID = cptrc.BCID left join		
		 ecpTransRepayContract as cptrp on cptrp.RCID = cptrc.ID and cptrp.StatusRepay = cptrc.StatusRepay left join
		 plcProvince as bp on bp.id = cptrc.Province left join
		 (
			select	RCID,
					(case when (len(c.ReplyDate) > 0) then (left(isnull(c.ReplyDate, ''), (len(isnull(c.ReplyDate, '')) - 1))) else null end) as ReplyDateHistory
			from	(	
						select	distinct
								RCID
						from	ecpTransRepayContract
					) as a
					cross apply
					(
						select	 (b.ReplyDate  + ',')
						from	 ecpTransRepayContract as b
						where	 (a.RCID = b.RCID)
						order by b.StatusRepay
						for xml path('')
					) c (ReplyDate) 
		 ) as rpdhis on cptrp.RCID = rpdhis.RCID left join
		 (
			select	RCID,
					(convert(date, (convert(varchar, (convert(int, substring(ReplyDate, 7, 4)) - 543)) + '-' + substring(ReplyDate, 4, 2) + '-' + substring(ReplyDate, 1, 2)))) as ReplyDate
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
		 ) as tpmmax on cptrp.RCID = tpmmax.RCID
where	 (cptbc.StatusCancel = 1)
order by cptbc.ProgramCode, cptbc.StudentID