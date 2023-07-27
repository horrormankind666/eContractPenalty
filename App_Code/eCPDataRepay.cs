
/*
=============================================
Author      : <ยุทธภูมิ ตวันนา>
Create date : <๐๖/๐๘/๒๕๕๕>
Modify date : <๒๙/๐๕/๒๕๖๖>
Description : <สำหรับการแจ้งชำระหนี้>
=============================================
*/

using System;
using System.Web;

public class eCPDataRepay {
    public static string StatusRepayNext(
        string action,
        string statusRepay
    ) {
        string nextStatus = string.Empty;

        nextStatus = (action.Equals("add") ? eCPUtil.repayStatus[int.Parse(statusRepay) + 1] : nextStatus);
        nextStatus = (action.Equals("update") ? eCPUtil.repayStatus[int.Parse(statusRepay)] + " ( รอเอกสารตอบกลับ )" : nextStatus);
        nextStatus = (action.Equals("stop") ? "แจ้งชำระหนี้ครบ 2 ครั้งแล้ว" : nextStatus);

        return nextStatus;
    }

    public static string ChkActionRepay(
        string statusRepay,
        string statusReply
    ) {
        string action;

        if (!statusRepay.Equals("2"))
            action = (string.IsNullOrEmpty(statusReply) || statusReply.Equals("2") ? "add" : "update");
        else
            action = (statusReply.Equals("2") ? "stop" : "update");

        return action;
    }
    
    private static string DetailCalInterestOverpayment(string[,] data) {
        string html = string.Empty;
        string[] repayDate = eCPUtil.RepayDate(data[0, 8]);
        string repayDateStartDefault = (!string.IsNullOrEmpty(repayDate[0]) ? repayDate[0] : string.Empty);
        string repayDateEndDefault = (!string.IsNullOrEmpty(repayDate[1]) ? repayDate[1] : string.Empty);
        string overpaymentDateStartDefault = (!string.IsNullOrEmpty(repayDate[2]) ? repayDate[2] : string.Empty);
        string overpaymentDateEndDefault = Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd"));
        /*
        string totalPenaltyDefault = double.Parse(data[0, 1]).ToString("#,##0.00");
        */
        string totalPenaltyDefault = double.Parse(data[0, 11]).ToString("#,##0.00");
        string capitalDefault = totalPenaltyDefault;

        IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
        DateTime dateA = DateTime.Parse(overpaymentDateStartDefault, provider);
        DateTime dateB = DateTime.Parse(overpaymentDateEndDefault, provider);

        double[] overpayment = Util.CalcDate(dateA, dateB);
        double overpaymentYear = overpayment[4];
        double overpaymentDay = overpayment[5];
        string[] contractInterest = eCPUtil.GetContractInterest();
        string overpaymentInterestDefault = double.Parse(contractInterest[1]).ToString("#,##0.00");

        html += (
            "<div class='form-content' id='detail-cal-interest-overpayment'>" +
            "   <div>" +
            "       <input type='hidden' id='repay-date-end-hidden' value='" + repayDateEndDefault + "' />" +
            "       <input type='hidden' id='overpayment-date-start-hidden' value='" + overpaymentDateStartDefault + "' />" +
            "       <input type='hidden' id='overpayment-date-end-hidden' value='" + overpaymentDateEndDefault + "' />" +
            "       <input type='hidden' id='overpayment-year-hidden' value='" + overpaymentYear + "' />" +
            "       <input type='hidden' id='overpayment-day-hidden' value='" + overpaymentDay + "' />" +
            "       <input type='hidden' id='overpayment-interest-hidden' value='" + overpaymentInterestDefault + "' />" +
            "       <input type='hidden' id='capital-hidden' value='" + capitalDefault + "' />" +
            "       <div>" +
            "           <div class='content-left' id='payment-label'>" +
            "               <div class='form-label-discription-style'>" +
            "                   <div class='form-label-style'>การดำเนินการชำระหนี้</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='content-left' id='payment-input'>" +
            "               <div class='form-input-style'>" +
            "                   <div class='form-input-content'>" +
            "                       <div>" +
            "                           ผู้ผิดสัญญาชำระหนี้ภายใน <span>" + eCPUtil.PAYMENT_AT_LEAST + "</span> วัน" +
            "                       </div>" +
            "                       <div class='form-input-content-line'>" +
            "                           ตั้งแต่วันที่ <span>" + Util.LongDateTH(repayDateStartDefault) + "</span> ถึงวันที่ <span>" + Util.LongDateTH(repayDateEndDefault) + "</span>" +
            "                       </div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='cal-interest-overpayment-label'>" +
            "                   <div class='form-label-style'>คำนวณดอกเบี้ยผิดนัดชำระ</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณากดปุ่มคำนวณเพื่อทำการคำนวณระยะเวลาผิดนัดชำระ</div>" +
            "                       <div class='form-discription-line2-style'>และคำนวณดอกเบี้ยผิดนัดชำระ</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='cal-interest-overpayment-input'>" +
            "                   <div class='button-style2'>" +
            "                       <ul>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick='CalculateInterestOverpayment()'>คำนวณ</a>" +
            "                           </li>" +
            "                       </ul>" +
            "                   </div>" +
            "                   <div>" +
            "                       <div class='content-left' id='overpayment-date-start-label'>ระยะเวลาผิดนัดชำระตั้งแต่วันที่</div>" +
            "                       <div class='content-left' id='overpayment-date-start-input'>" +
            "                           <input class='inputbox calendar' type='text' id='overpayment-date-start' readonly value='' />" +
            "                       </div>" +
            "                       <div class='content-left' id='overpayment-date-end-label'>ถึงวันที่</div>" +
            "                       <div class='content-left' id='overpayment-date-end-input'>" +
            "                           <input class='inputbox calendar' type='text' id='overpayment-date-end' readonly value='' />" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "                   <div>" +
            "                       <div class='content-left' id='overpayment-year-day-label'>นับระยะเวลาการผิดนัดชำระได้</div>" +
            "                       <div class='content-left' id='overpayment-year-input'>" +
            "                           <input class='inputbox textbox-numeric' type='text' id='overpayment-year' value='' style='width:32px' />" +
            "                       </div>" +
            "                       <div class='content-left' id='overpayment-year-unit-label'>ปี</div>" +
            "                       <div class='content-left' id='overpayment-day-input'>" +
            "                           <input class='inputbox textbox-numeric' type='text' id='overpayment-day' value='' style='width:39px' />" +
            "                       </div>" +
            "                       <div class='content-left' id='overpayment-day-unit-label'>วัน</div>" +
            "                  </div>" +
            "                  <div class='clear'></div>" +
            "                  <div>" +
            "                       <div class='content-left' id='overpayment-interest-label'>ดอกเบี้ยผิดนัดชำระอัตราร้อยละ</div>" +
            "                       <div class='content-left' id='overpayment-interest-input'>" +
            "                           <input class='inputbox textbox-numeric' type='text' id='overpayment-interest' onblur=Trim('overpayment-interest');AddCommas('overpayment-interest',2) onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:100px' />" +
            "                       </div>" +
            "                       <div class='content-left' id='overpayment-interest-unit-label'>ต่อปี</div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "                   <div>" +
            "                       <div class='content-left' id='capital-label'>ยอดเงินค่าปรับผิดสัญญา</div>" +
            "                       <div class='content-left' id='capital-input'>" +
            "                           <input class='inputbox textbox-numeric' type='text' id='capital' value='' style='width:100px' />" +
            "                       </div>" +
            "                       <div class='content-left' id='capital-unit-label'>บาท</div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "                   <div>" +
            "                       <div class='content-left' id='total-interest-overpayment-label'>ยอดดอกเบี้ยจากการผิดนัดชำระ</div>" +
            "                       <div class='content-left' id='total-interest-overpayment-input'>" +
            "                           <input class='inputbox textbox-numeric' type='text' id='total-interest-overpayment' value='' style='width:100px' />" +
            "                       </div>" +
            "                       <div class='content-left' id='total-interest-overpayment-unit-label'>บาท</div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "                   <div>" +
            "                       <div class='content-left' id='total-payment-label'>ยอดเงินที่ต้องรับผิดชอบชดใช้</div>" +
            "                       <div class='content-left' id='total-payment-input'>" +
            "                           <input class='inputbox textbox-numeric' type='text' id='total-payment' value='' style='width:100px' />" +
            "                       </div>" +
            "                       <div class='content-left' id='total-payment-unit-label'>บาท</div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrmCalInterestOverpayment(false)'>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string DetailCalInterestOverpayment(string cp2id) {
        string html = string.Empty;
        string[,] data1 = eCPDB.ListCPTransRepayContract(cp2id);

        if (data1.GetLength(0) > 0) {
            string statusRepay = data1[0, 2];
            string[,] data2 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(cp2id, statusRepay);

            if (data2.GetLength(0) > 0)
                data1[0, 8] = data2[0, 5];

            html += (
                DetailCalInterestOverpayment(data1)
            );
        }

        return html;
    } 
    
    private static string AddUpdateCPTransRepayContract(string[,] data) {        
        string html = string.Empty;
        string cp1id = data[0, 2];
        string cp2id = data[0, 1];
        string statusRepayDefault = data[0, 18];
        string statusPaymentDefault = data[0, 58];
        string statusReplyDefault = string.Empty;
        string replyResultDefault = string.Empty;
        string repayDateDefault = string.Empty;
        string replyDateDefault = string.Empty;
        string replyDateMark;
        string[] repayDate;
        string repayDateStart = string.Empty;
        string repayDateEnd = string.Empty;
        string previousRepayDateEnd = string.Empty;
        string overpaymentDateStart = string.Empty;
        string pursuantDefault = string.Empty;
        string pursuantBookDateDefault = string.Empty;
        string[,] data1 = eCPDB.ListCPTransRepayContract(cp2id);

        if (data1.GetLength(0) > 0) {
            statusReplyDefault = data1[0, 3];
            replyResultDefault = data1[0, 4];
            repayDateDefault = data1[0, 5];
            replyDateDefault = data1[0, 6];
            replyDateMark = data1[0, 8];
            repayDate = eCPUtil.RepayDate(replyDateMark);
            repayDateStart = (!string.IsNullOrEmpty(repayDate[0]) ? repayDate[0] : string.Empty);
            repayDateEnd = (!string.IsNullOrEmpty(repayDate[1]) ? repayDate[1] : string.Empty);
            overpaymentDateStart = (!string.IsNullOrEmpty(repayDate[2]) ? repayDate[2] : string.Empty);
            pursuantDefault = data1[0, 12];
            pursuantBookDateDefault = data1[0, 13];
        }

        string[] statusRepayCurrent = (eCPDB.SearchRepayStatusDetail(cp2id, statusRepayDefault, statusPaymentDefault)).Split(new char[] { ';' });
        string action = ChkActionRepay(statusRepayDefault, statusReplyDefault);
        string[,] data2 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(cp2id, statusRepayDefault);

        if (data2.GetLength(0) > 0) {
            replyDateMark = data2[0, 5];
            repayDate = eCPUtil.RepayDate(replyDateMark);
            repayDateStart = (!string.IsNullOrEmpty(repayDate[0]) ? repayDate[0] : string.Empty);
            repayDateEnd = (!string.IsNullOrEmpty(repayDate[1]) ? repayDate[1] : string.Empty);
            overpaymentDateStart = (!string.IsNullOrEmpty(repayDate[2]) ? repayDate[2] : string.Empty);
        }

        html += ( 
            "<div class='form-content' id='addupdate-cp-trans-repay-contract'>" +
            "   <div>" +
            "       <input type='hidden' id='action' value='" + action + "' />" +
            "       <input type='hidden' id='cp2id' value='" + cp2id + "' />" +
            "       <input type='hidden' id='status-repay-hidden' value='" + (action.Equals("add") ? (int.Parse(statusRepayDefault) + 1).ToString() : statusRepayDefault) + "' />" +
            "       <input type='hidden' id='repay-date-hidden' value='" + (action.Equals("add") ? Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd")) : repayDateDefault) + "' />" +
            "       <input type='hidden' id='reply-date-hidden' value='" + replyDateDefault + "' />" +
            "       <input type='hidden' id='pursuant-hidden' value='" + pursuantDefault + "' />" +
            "       <input type='hidden' id='pursuant-book-date-hidden' value='" + pursuantBookDateDefault + "' />"
        );
        /*
        data2 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(cp2id, statusRepayDefault);
        */
        if (data2.GetLength(0) > 0) {
            html += (
                "   <div>" +
                "       <div class='content-left' id='history-repay-label'>" +
                "           <div class='form-label-discription-style clear-bottom'>" +
                "               <div class='form-label-style'>ประวัติการแจ้งชำระหนี้</div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='content-left' id='history-repay-input'>" +
                "           <div class='form-input-style clear-bottom'>" +
                "               <div class='form-input-content'>"
            );

            for(int i = 0; i < data2.GetLength(0); i++) {
                previousRepayDateEnd = (i < data2.GetLength(0)) ? data2[i, 5] : previousRepayDateEnd;

                html += (
                    "               <div>" +
                    "                   <span>" + eCPUtil.repayStatus[int.Parse(data2[i, 1])] + "</span>" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   แจังให้ผู้ผิดสัญญาชำระหนี้เมื่อวันที่ <span>" + Util.LongDateTH(data2[i, 4]) + "</span>" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   หนังสือขอให้ชดใช้เงินที่ อว 78/<span>" + (!string.IsNullOrEmpty(data2[i, 6]) ? data2[i, 6] : " -") + "</span> ลงวันที่ <span>" + (!string.IsNullOrEmpty(data2[i, 7]) ? Util.LongDateTH(data2[i, 7]) : " -") + "</span>" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   รับเอกสารตอบกลับจากไปรษณีย์เมื่อวันที่ <span>" + Util.LongDateTH(data2[i, 5]) + "</span>" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   ผลการรับทราบการแจ้งชำระหนี้ <span>" + eCPUtil.resultReply[int.Parse(data2[i, 3]) - 1] + "</span>" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   <a class='text-underline' href='javascript:void(0)' onclick=PrintNoticeClaimDebt('" + cp1id + "'," + data2[i, 1] + ",'" + previousRepayDateEnd + "')>พิมพ์แบบหนังสือทวงถามครั้งที่ " + data2[i, 1] + "</a>" +
                    "               </div>"
                );

                if (data2[i, 1].Equals("1"))
                    html += (
                        "           <div class='form-input-content-line'>" +
                        "               <a class='text-underline' href='javascript:void(0)' onclick=PrintFormRequestCreateAndUpdateDebtor('" + cp1id + "')>พิมพ์แบบขอสร้างและปรับปรุงข้อมูลหลักลูกหนี้</a>" +
                        "           </div>"
                    );
            }

            html += (
                "               </div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='clear'></div>" +
                "   <div class='box3'></div>"
            );
        }

        if ((statusRepayDefault.Equals("0") && action.Equals("add")) ||
            action.Equals("update")) {
            html += (
                "   <div>" +
                "       <div class='content-left' id='status-repay" + statusRepayDefault + "-current-label'>" +
                "           <div class='form-label-discription-style " + (statusRepayDefault.Equals("0") && action.Equals("add") ? "clear-bottom" : "") + "'>" +
                "               <div class='form-label-style'>สถานะการแจ้งชำระหนี้ปัจจุบัน</div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='content-left' id='status-repay" + statusRepayDefault + "-current-input'>" +
                "           <div class='form-input-style " + (statusRepayDefault.Equals("0") && action.Equals("add") ? "clear-bottom" : "") + "'>" +
                "               <div class='form-input-content'>" +
                "                   <div>" +
                "                       <span>" + eCPUtil.repayStatusDetail[int.Parse(statusRepayCurrent[0])] + "</span>" +
                "                   </div>"
            );

            if (action.Equals("update")) {
                html += (
                    "               <div class='form-input-content-line'>" +
                    "                   <a class='text-underline' href='javascript:void(0)' onclick=PrintNoticeClaimDebt('" + cp1id + "'," + statusRepayDefault + ",'" + previousRepayDateEnd + "')>พิมพ์แบบหนังสือทวงถามครั้งที่ " + statusRepayDefault + "</a>" +
                    "               </div>"
                );

                if (statusRepayDefault.Equals("1"))
                    html += (
                        "           <div class='form-input-content-line'>" +
                        "               <a class='text-underline' href='javascript:void(0)' onclick=PrintFormRequestCreateAndUpdateDebtor('" + cp1id + "')>พิมพ์แบบขอสร้างและปรับปรุงข้อมูลหลักลูกหนี้</a>" +
                        "           </div>"
                    );
            }

            html += (
                "               </div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='clear'></div>"
            );

            if (action.Equals("update")) {
                html += (
                    "<div>" +
                    "   <div class='form-label-discription-style'>" +
                    "       <div id='repay-date-label'>" +
                    "           <div class='form-label-style'>วันที่แจังให้ผู้ผิดสัญญาชำระหนี้</div>" +
                    "           <div class='form-discription-style'>" +
                    "               <div class='form-discription-line1-style'>กรุณาใส่วันที่ที่ได้แจ้งให้ผู้ผิดสัญญาหรือผู้ค้ำประกัน</div>" +
                    "               <div class='form-discription-line2-style'>หรือผู้ได้รับมอบหมายชำระหนี้</div>" +
                    "           </div>" +
                    "       </div>" +
                    "   </div>"
                );

                if (data2.GetLength(0) > 0)
                    html += (
                        "<input type='hidden' id='previous-reply-date' value='" + data2[data2.GetLength(0) - 1, 5] + "' />"
                    );

                html += (
                    "   <div class='form-input-style'>" +
                    "       <div class='form-input-content' id='repay-date-input'>" +
                    "           <input class='inputbox calendar' type='text' id='repay-date' readonly value='' />" +
                    "       </div>" +
                    "   </div>" +
                    "</div>" +
                    "<div class='clear'></div>"
                );
            }
        }

        if ((statusRepayDefault.Equals("1") && action.Equals("add")) ||
            (statusRepayDefault.Equals("2") && action.Equals("stop"))) {
            html += (
                "   <div id='status-repay" + (statusRepayDefault + "-" + action) + "'>" +
                "       <div class='content-left' id='status-repay23-current-label'>" +
                "           <div class='form-label-discription-style clear-bottom'>" +
                "               <div class='form-label-style'>สถานะการแจ้งชำระหนี้ปัจจุบัน</div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='content-left' id='status-repay23-current-input'>" +
                "           <div class='form-input-style clear-bottom'>" +
                "               <div class='form-input-content'>" +
                "                   <div>" +
                "                       <span>" + eCPUtil.repayStatus[int.Parse(statusRepayDefault)] + "</span>" +
                "                   </div>" +
                "                   <div class='form-input-content-line'>" +
                "                       แจังให้ผู้ผิดสัญญาชำระหนี้เมื่อวันที่ <span>" + Util.LongDateTH(repayDateDefault) + "</span>" +
                "                   </div>" +
                "                   <div class='form-input-content-line'>" +
                "                       หนังสือขอให้ชดใช้เงินที่ อว 78/<span>" + (!string.IsNullOrEmpty(pursuantDefault) ? pursuantDefault : " -") + "</span> ลงวันที่ <span>" + (!string.IsNullOrEmpty(pursuantBookDateDefault) ? Util.LongDateTH(pursuantBookDateDefault) : " -") + "</span>" +
                "                   </div>" +
                "                   <div class='form-input-content-line'>" +
                "                       รับเอกสารตอบกลับจากไปรษณีย์เมื่อวันที่ <span>" + Util.LongDateTH(replyDateDefault) + "</span>" +
                "                   </div>" +
                "                   <div class='form-input-content-line'>" +
                "                       ผลการรับทราบการแจ้งชำระหนี้ <span>" + eCPUtil.resultReply[int.Parse(replyResultDefault) - 1] + "</span>" +
                "                   </div>" +
                "                   <div class='form-input-content-line'>" +
                "                       <a class='text-underline' href='javascript:void(0)' onclick=PrintNoticeClaimDebt('" + cp1id + "'," + statusRepayDefault + ",'" + previousRepayDateEnd + "')>พิมพ์แบบหนังสือทวงถามครั้งที่ " + statusRepayDefault + "</a>" +
                "                   </div>"
            );

            if (statusRepayDefault.Equals("1") && 
                action.Equals("add"))
                html += (
                    "               <div class='form-input-content-line'>" +
                    "                   <a class='text-underline' href='javascript:void(0)' onclick=PrintFormRequestCreateAndUpdateDebtor('" + cp1id + "')>พิมพ์แบบขอสร้างและปรับปรุงข้อมูลหลักลูกหนี้</a>" +
                    "               </div>"
                );

            html += (
                "               </div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='clear'></div>"
            );
        }

        if (action.Equals("update")) {
            html += (
                "   <div id='notice-claim-debt'>" +
                "       <div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div id='notice-claim-debt-label'>" +
                "                   <div class='form-label-style'>หนังสือขอให้ชดใช้เงิน ( ครั้งที่ " + statusRepayDefault + ")</div>" +
                "                   <div class='form-discription-style'>" +
                "                       <div class='form-discription-line1-style'>กรุณาใส่ข้อมูลเกี่ยวกับหนังสือขอให้ชดใช้เงิน แต่ละครั้ง</div>" +
                "                       <div class='form-discription-line2-style'>ซึ่งจะเป็นการบันทึกข้อมูลย้อนหลังพร้อมกับการบันทึก</div>" +
                "                       <div class='form-discription-line3-style'>วันที่รับเอกสาร</div>" +
                "                   </div>" +
                "               </div>" +
                "           </div>" +
                "           <div class='form-input-style'>" +
                "               <div class='form-input-content' id='notice-claim-debt-input'>" +
                "                   <div>" +
                "                       <div class='content-left' id='pursuant-label'>หนังสือมหาวิทยาลัยมหิดลที่ อว 78/</div>" +
                "                       <div class='content-left' id='pursuant-input'>" +
                "                           <input class='inputbox' type='text' id='pursuant' value='' style='width:193px' />" +
                "                       </div>" +
                "                   </div>" +
                "                   <div class='clear'></div>" +
                "                   <div>" +
                "                       <div class='content-left' id='pursuant-book-date-label'>ลงวันที่</div>" +
                "                       <div class='content-left' id='pursuant-book-date-input'>" +
                "                           <input class='inputbox calendar' type='text' id='pursuant-book-date' readonly value='' />" +
                "                       </div>" +
                "                   </div>" +
                "                   <div class='clear'></div>" +
                "               </div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='clear'></div>" +
                "   </div>" +
                "   <div>" +
                "       <div class='form-label-discription-style'>" +
                "           <div id='reply-date-label'>" +
                "               <div class='form-label-style'>วันที่รับเอกสารตอบกลับจากไปรษณีย์</div>" +
                "               <div class='form-discription-style'>" +
                "                   <div class='form-discription-line1-style'>กรุณาใส่วันที่ที่ได้รับเอกสารตอบกลับจากไปรษณีย์</div>" +
                "                   <div class='form-discription-line2-style'>หลังจากที่แจ้งชำระหนี้ให้ผู้ผิดสัญญาหรือผู้ค้ำประกัน</div>" +
                "                   <div class='form-discription-line3-style'>หรือผู้ได้รับมอบหมายทราบ</div>" +
                "               </div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='form-input-style'>" +
                "           <div class='form-input-content' id='reply-date-input'>" +
                "               <input class='inputbox calendar' type='text' id='reply-date' readonly value='' />" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='clear'></div>" +
                "   <div>" +
                "       <div class='form-label-discription-style " + (statusRepayDefault.Equals("2") && !string.IsNullOrEmpty(overpaymentDateStart) ? "clear-bottom" : "") + "'>" +
                "           <div id='detail-reply-label'>" +
                "               <div class='form-label-style'>ผลการรับทราบการแจ้งชำระหนี้</div>" +
                "               <div class='form-discription-style'>" +
                "                   <div class='form-discription-line1-style'>กรุณาใส่ผลการรับทราบการแจ้งชำระหนี้ของผู้ผิดสัญญา</div>" +
                "                   <div class='form-discription-line2-style'>หรือผู้ค้ำประกัน หรือผู้ได้รับมอบหมาย</div>" +
                "               </div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='form-input-style " + (statusRepayDefault.Equals("2") && !string.IsNullOrEmpty(overpaymentDateStart) ? "clear-bottom" : "") + "'>" +
                "           <div class='form-input-content' id='detail-reply-input'>" +
                "               <div>" +
                "                   <div class='content-left' id='reply-yes-input'>" +
                "                       <input class='radio' type='radio' name='reply-result' value='1' />" +
                "                   </div>" +
                "                   <div class='content-left' id='reply-yes-label'>" + eCPUtil.resultReply[0] + "</div>" +
                "               </div>" +
                "               <div class='clear'></div>" +
                "               <div>" +
                "                   <div class='content-left' id='reply-no-input'>" +
                "                       <input class='radio' type='radio' name='reply-result' value='2' />" +
                "                   </div>" +
                "                   <div class='content-left' id='reply-no-label'>" + eCPUtil.resultReply[1] + "</div>" +
                "               </div>" +
                "               <div class='clear'></div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='clear'></div>"
            );
        }

        if ((statusRepayDefault.Equals("1") && action.Equals("add")) || 
            (statusRepayDefault.Equals("2") && action.Equals("update")) ||
            (statusRepayDefault.Equals("2") && action.Equals("stop"))) {
            if (!string.IsNullOrEmpty(overpaymentDateStart)) {
                IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
                DateTime dateA = DateTime.Parse(overpaymentDateStart, provider);
                DateTime dateB = DateTime.Parse(Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd")), provider);

                double[] overpayment = Util.CalcDate(dateA, dateB);
                string clearBottom = string.Empty;

                if ((action.Equals("add") || action.Equals("stop")) &&
                    statusPaymentDefault.Equals("1"))
                    clearBottom = "clear-bottom";

                html += (
                    "<div class='box3'></div>" +
                    "<div>" + 
                    "   <div class='content-left' id='payment-label'>" +
                    "       <div class='form-label-discription-style " + clearBottom + "'>" +
                    "           <div class='form-label-style'>การดำเนินการชำระหนี้</div>" +
                    "       </div>" +
                    "   </div>" +
                    "   <div class='content-left' id='payment-input'>" +
                    "       <div class='form-input-style " + clearBottom + "'>" +
                    "           <div class='form-input-content'>" +
                    "               <div>" +
                    "                   ผู้ผิดสัญญาชำระหนี้ภายใน <span>" + eCPUtil.PAYMENT_AT_LEAST + "</span> วัน" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   ตั้งแต่วันที่ <span>" + Util.LongDateTH(repayDateStart) + "</span> ถึงวันที่ <span>" + Util.LongDateTH(repayDateEnd) + "</span>" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   ระยะเวลาผิดนัดชำระ <span>" + (statusPaymentDefault.Equals("1") && !overpayment[4].Equals(0) ? overpayment[4].ToString("#,##0") : "-") + "</span> ปี <span>" + (statusPaymentDefault.Equals("1") && !overpayment[5].Equals(0) ? overpayment[5].ToString("#,##0") : "-") + "</span> วัน" +
                    "               </div>" +
                    "           </div>" +
                    "       </div>" +
                    "   </div>" +
                    "</div>" +
                    "<div class='clear'></div>"
                );
            }
        }

        /*
        if (!(statusReplyDefault + replyResultDefault).Equals("21")) {
        */
        if ((action.Equals("add") || action.Equals("stop")) &&
            statusPaymentDefault.Equals("1")) {
            html += (
                "   <div class='box3'></div>" +
                "   <div>" +
                "       <div class='content-left' id='status-repay-label'>" +
                "           <div class='form-label-discription-style " + (action.Equals("stop") && !string.IsNullOrEmpty(overpaymentDateStart) ? "" : "") + "'>" +
                "               <div class='form-label-style'>สถานะการแจ้งชำระหนี้ที่จะดำเนินการต่อ</div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='content-left' id='status-repay-input'>" +
                "           <div class='form-input-style " + (action.Equals("stop") && !string.IsNullOrEmpty(overpaymentDateStart) ? "" : "") + "'>" +
                "               <div class='form-input-content'>" +
                "                   <span>" + StatusRepayNext(action, statusRepayDefault) + "</span>" +
                "               </div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='clear'></div>"
            );

            if (action.Equals("add")) {
                html += (
                    "<div>" +
                    "   <div class='form-label-discription-style " + (!string.IsNullOrEmpty(overpaymentDateStart) ? "" : "") + "'>" +
                    "       <div id='repay-date-label'>" +
                    "           <div class='form-label-style'>วันที่แจังให้ผู้ผิดสัญญาชำระหนี้</div>" +
                    "           <div class='form-discription-style'>" +
                    "               <div class='form-discription-line1-style'>กรุณาใส่วันที่ที่ได้แจ้งให้ผู้ผิดสัญญาหรือผู้ค้ำประกัน</div>" +
                    "               <div class='form-discription-line2-style'>หรือผู้ได้รับมอบหมายชำระหนี้</div>" +
                    "           </div>" +
                    "       </div>" +
                    "   </div>" +                 
                    "   <input type='hidden' id='previous-reply-date' value='" + replyDateDefault + "' />" +
                    "   <div class='form-input-style " + (!string.IsNullOrEmpty(overpaymentDateStart) ? "" : "") + "'>" +
                    "       <div class='form-input-content' id='repay-date-input'>" +
                    "           <input class='inputbox calendar' type='text' id='repay-date' readonly value='' />" +
                    "       </div>" +
                    "   </div>" +
                    "</div>" +
                    "<div class='clear'></div>"
                );
            }
        }
        /*
        }
        */

        html += (
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1' id='button-style1" + (!action.Equals("stop") && statusPaymentDefault.Equals("1") ? "1" : "2") + "'>" +
            /*
            "       <div class='button-style1' id='button-style1" + (!action.Equals("stop") && !(statusReplyDefault + replyResultDefault).Equals("21") ? "1" : "2") + "'>" +
            */
            "           <ul>"
        );

        /*
        if (!action.Equals("stop") &&
            !(statusReplyDefault + replyResultDefault).Equals("21"))
        */
        if (!action.Equals("stop") &&
            statusPaymentDefault.Equals("1")) {
            html += (
                "           <li>" +
                "               <a href='javascript:void(0)' onclick=ConfirmActionCPTransRepayContract('" + action + "')>บันทึก</a>" +
                "           </li>" +
                "           <li>" +
                "               <a href='javascript:void(0)' onclick='ResetFrmCPTransRepayContract(false)'>ล้าง</a>" +
                "           </li>"
            );
        }                            

        html += (
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>" +
            "<iframe class='export-target' id='export-target' name='export-target'></iframe>" +
            "<form id='export-setvalue' method='post' target='export-target'>" +
            "   <input id='export-send' name='export-send' value='' type='hidden' />" +
            "   <input id='export-order' name='export-order' value='' type='hidden' />" +
            "   <input id='export-type' name='export-type' value='' type='hidden' />" +
            "</form>"
        );

        return html;
    }

    private static string ViewCPTransRepayContract(string[,] data) {        
        string html = string.Empty;
        string cp1id = data[0, 2];
        string cp2id = data[0, 1];
        string statusRepayDefault = data[0, 18];
        string statusPaymentDefault = data[0, 58];
        string statusReplyDefault = string.Empty;
        string replyResultDefault = string.Empty;
        string repayDateDefault = string.Empty;
        string replyDateDefault = string.Empty;
        string replyDateMark;
        string[] repayDate;
        string repayDateStart;
        string repayDateEnd;
        string previousRepayDateEnd = string.Empty;
        string overpaymentDateStart;
        string[,] data1 = eCPDB.ListCPTransRepayContract(cp2id);

        if (data1.GetLength(0) > 0) {
            statusReplyDefault = data1[0, 3];
            replyResultDefault = data1[0, 4];
            repayDateDefault = data1[0, 5];
            replyDateDefault = data1[0, 6];
            replyDateMark = data1[0, 8];
            repayDate = eCPUtil.RepayDate(replyDateMark);
            repayDateStart = (!string.IsNullOrEmpty(repayDate[0]) ? repayDate[0] : string.Empty);
            repayDateEnd = (!string.IsNullOrEmpty(repayDate[1]) ? repayDate[1] : string.Empty);
            overpaymentDateStart = (!string.IsNullOrEmpty(repayDate[2]) ? repayDate[2] : string.Empty);
        }

        string[] statusRepayCurrent = (eCPDB.SearchRepayStatusDetail(cp2id, statusRepayDefault, statusPaymentDefault)).Split(new char[] { ';' });
        string action = ChkActionRepay(statusRepayDefault, statusReplyDefault);
        string[,] data2 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(cp2id, statusRepayDefault);

        if (data2.GetLength(0) > 0) {
            replyDateMark = data2[0, 5];
            repayDate = eCPUtil.RepayDate(replyDateMark);
            repayDateStart = (!string.IsNullOrEmpty(repayDate[0]) ? repayDate[0] : string.Empty);
            repayDateEnd = (!string.IsNullOrEmpty(repayDate[1]) ? repayDate[1] : string.Empty);
            overpaymentDateStart = (!string.IsNullOrEmpty(repayDate[2]) ? repayDate[2] : string.Empty);
        }
        
        html += (
            "<div class='form-content' id='view-cp-trans-repay-contract'>" +
            "   <div>"
        );

        /*
        data2 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(cp2id, statusRepayDefault);
        */

        if (data2.GetLength(0) > 0) {
            html += (
                "   <div>" +
                "       <div class='content-left' id='history-repay-label'>" +
                "           <div class='form-label-discription-style clear-bottom'>" +
                "               <div class='form-label-style'>ประวัติการแจ้งชำระหนี้</div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='content-left' id='history-repay-input'>" +
                "           <div class='form-input-style clear-bottom'>" +
                "               <div class='form-input-content'>"
            );

            for(int i = 0; i < data2.GetLength(0); i++) {
                previousRepayDateEnd = (i < data2.GetLength(0) ? data2[i, 5] : previousRepayDateEnd);

                html += (
                    "               <div>" +
                    "                   <span>" + eCPUtil.repayStatus[int.Parse(data2[i, 1])] + "</span>" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   แจังให้ผู้ผิดสัญญาชำระหนี้เมื่อวันที่ <span>" + Util.LongDateTH(data2[i, 4]) + "</span>" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   รับเอกสารตอบกลับจากไปรษณีย์เมื่อวันที่ <span>" + Util.LongDateTH(data2[i, 5]) + "</span>" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   ผลการรับทราบการแจ้งชำระหนี้ <span>" + eCPUtil.resultReply[int.Parse(data2[i, 3]) - 1] + "</span>" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   <a class='text-underline' href='javascript:void(0)' onclick=PrintNoticeClaimDebt('" + cp1id + "'," + data2[i, 1] + ",'" + previousRepayDateEnd + "')>พิมพ์แบบหนังสือทวงถามครั้งที่ " + data2[i, 1] + "</a>" +
                    "               </div>"
                );

                if (data2[i, 1].Equals("1"))
                    html += (
                        "           <div class='form-input-content-line'>" +
                        "               <a class='text-underline' href='javascript:void(0)' onclick=PrintFormRequestCreateAndUpdateDebtor('" + cp1id + "')>พิมพ์แบบขอสร้างและปรับปรุงข้อมูลหลักลูกหนี้</a>" +
                        "           </div>"
                    );
            }

            html += (
                "               </div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='clear'></div>" +
                "   <div class='box3'></div>"
            );
        }

        if ((statusRepayDefault.Equals("0") && action.Equals("add")) ||
            action.Equals("update")) {
            html += (
                "   <div>" +
                "       <div class='content-left' id='status-repay" + statusRepayDefault + "-current-label'>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>สถานะการแจ้งชำระหนี้ปัจจุบัน</div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='content-left' id='status-repay" + statusRepayDefault + "-current-input'>" +
                "           <div class='form-input-style'>" +
                "               <div class='form-input-content'>" +
                "                   <div>" +
                "                       <span>" + eCPUtil.repayStatusDetail[int.Parse(statusRepayCurrent[0])] + "</span>" +
                "                   </div>"
            );

            if (action.Equals("update")) {
                html += (
                    "               <div class='form-input-content-line'>" +
                    "                   <a class='text-underline' href='javascript:void(0)' onclick=PrintNoticeClaimDebt('" + cp1id + "'," + statusRepayDefault + ",'" + previousRepayDateEnd + "')>พิมพ์แบบหนังสือทวงถามครั้งที่ " + statusRepayDefault + "</a>" +
                    "               </div>"
                );

                if (statusRepayDefault.Equals("1"))
                    html += (
                        "           <div class='form-input-content-line'>" +
                        "               <a class='text-underline' href='javascript:void(0)' onclick=PrintFormRequestCreateAndUpdateDebtor('" + cp1id + "')>พิมพ์แบบขอสร้างและปรับปรุงข้อมูลหลักลูกหนี้</a>" +
                        "           </div>"
                    );
            }

            html += (
                "               </div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='clear'></div>"
            );
        }

        if ((statusRepayDefault.Equals("1") && action.Equals("add")) ||
            (statusRepayDefault.Equals("2") && action.Equals("stop"))) {
            html += (
                "   <div id='status-repay" + (statusRepayDefault + "-" + action) + "'>" +
                "       <div class='content-left' id='status-repay23-current-label'>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>สถานะการแจ้งชำระหนี้ปัจจุบัน</div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='content-left' id='status-repay23-current-input'>" +
                "           <div class='form-input-style'>" +
                "               <div class='form-input-content'>" +
                "                   <div>" +
                "                       <span>" + eCPUtil.repayStatus[int.Parse(statusRepayDefault)] + "</span>" +
                "                   </div>" +
                "                   <div class='form-input-content-line'>" +
                "                       แจังให้ผู้ผิดสัญญาชำระหนี้เมื่อวันที่ <span>" + Util.LongDateTH(repayDateDefault) + "</span>" +
                "                   </div>" +
                "                   <div class='form-input-content-line'>" +
                "                       รับเอกสารตอบกลับจากไปรษณีย์เมื่อวันที่ <span>" + Util.LongDateTH(replyDateDefault) + "</span>" +
                "                   </div>" +
                "                   <div class='form-input-content-line'>" +
                "                       ผลการรับทราบการแจ้งชำระหนี้ <span>" + eCPUtil.resultReply[int.Parse(replyResultDefault) - 1] + "</span>" +
                "                   </div>" +
                "                   <div class='form-input-content-line'>" +
                "                       <a class='text-underline' href='javascript:void(0)' onclick=PrintNoticeClaimDebt('" + cp1id + "'," + statusRepayDefault + ",'" + previousRepayDateEnd + "')>พิมพ์แบบหนังสือทวงถามครั้งที่ " + statusRepayDefault + "</a>" +
                "                   </div>"
            );

            if (statusRepayDefault.Equals("1") &&
                action.Equals("add"))
                html += (
                    "               <div class='form-input-content-line'>" +
                    "                   <a class='text-underline' href='javascript:void(0)' onclick=PrintFormRequestCreateAndUpdateDebtor('" + cp1id + "')>พิมพ์แบบขอสร้างและปรับปรุงข้อมูลหลักลูกหนี้</a>" +
                    "               </div>"
                );

            html += (
                "               </div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='clear'></div>"
            );
        }

        html += (
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>" +
            "<iframe class='export-target' id='export-target' name='export-target'></iframe>" +
            "<form id='export-setvalue' method='post' target='export-target'>" +
            "   <input id='export-send' name='export-send' value='' type='hidden' />" +
            "   <input id='export-order' name='export-order' value='' type='hidden' />" +
            "   <input id='export-type' name='export-type' value='' type='hidden' />" +
            "</form>"
        );

        return html;
    }

    public static string AddUpdateCPTransRepayContract(string cp1id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListDetailCPTransRequireContract(cp1id);

        if (data.GetLength(0) > 0)
            html += (
                AddUpdateCPTransRepayContract(data)
            );

        return html;
    }

    public static string ViewCPTransRepayContract(string cp1id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListDetailCPTransRequireContract(cp1id);

        if (data.GetLength(0) > 0)
            html += (
                ViewCPTransRepayContract(data)
            );

        return html;
    }

    public static string ListRepay(HttpContext c) {
        string html = string.Empty;        
        string pageHtml = string.Empty;
        int recordCount = eCPDB.CountRepay(c);

        if (recordCount > 0) {
            string[,] data = eCPDB.ListRepay1(c);
            string highlight;
            string groupNum;
            string trackingStatus;
            string overpayment;
            double[] overpaymentArray;
            string[] iconStatus;
            string[] repayDate;
            string callFunc;

            IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
            DateTime dateA;
            DateTime dateB;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                overpayment = string.Empty;
                /*
                string[,] data1 = eCPDB.ListCPTransRepayContract(data[i, 1]);

                if (data1.GetLength(0) > 0 &&
                    (!string.IsNullOrEmpty(data1[0, 8]) && data1[0, 7].Equals("1"))) {
                    repayDate = eCPUtil.RepayDate(data1[0, 8]);
                    dateA = DateTime.Parse(repayDate[2], provider);
                    dateB = DateTime.Parse(Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd")), provider);
                    overpaymentArray = Util.CalcDate(dateA, dateB);
                    overpayment = (!overpaymentArray[0].Equals(0) ? overpaymentArray[0].ToString("#,##0") : "-");
                }
                */
                if (!string.IsNullOrEmpty(data[i, 21]) &&
                    data[i, 15].Equals("1")) {
                    repayDate = eCPUtil.RepayDate(data[i, 21]);
                    dateA = DateTime.Parse(repayDate[2], provider);
                    dateB = DateTime.Parse(Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd")), provider);
                    overpaymentArray = Util.CalcDate(dateA, dateB);
                    overpayment = (!overpaymentArray[0].Equals(0) ? overpaymentArray[0].ToString("#,##0") : "-");
                }

                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                groupNum = (!data[i, 9].Equals("0") ? (" ( กลุ่ม " + data[i, 9] + " )") : "");
                trackingStatus = (data[i, 10] + data[i, 11] + data[i, 12] + data[i, 13]);
                iconStatus = data[i, 16].Split(new char[] {';'});
                callFunc = ("ViewRepayStatusViewTransRequireContract('" + data[i, 2] + "','" + data[i, 1] + "','" + trackingStatus + "','r')");
                
                html += (
                    "<ul class='table-row-content " + highlight + "' id='repay" + data[i, 1] + "'>" +
                    "   <li id='tab2-table-content-repay-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab2-table-content-repay-col2' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 3] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab2-table-content-repay-col3' onclick=" + callFunc + ">" +
                    "       <div>" + (data[i, 4] + data[i, 5] + " " + data[i, 6]) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab2-table-content-repay-col4' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='programcode-col'>" + data[i, 7] + "</span>- " + (data[i, 8] + groupNum) +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab2-table-content-repay-col5' onclick=" + callFunc + ">" +
                    "       <div>" + (!string.IsNullOrEmpty(data[i, 17]) ? data[i, 17] : "-") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab2-table-content-repay-col6' onclick=" + callFunc + ">" +
                    "       <div>" + (!string.IsNullOrEmpty(overpayment) ? overpayment : "-") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab2-table-content-repay-col7' onclick=" + callFunc + ">" +
                    "       <div class='icon-status-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus[1] + "'></li>" +
                    "               <li class='" + iconStatus[2] + "'></li>" +
                    "               <li class='" + iconStatus[3] + "'></li>" +
                    "               <li class='" + iconStatus[4] + "'></li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );

            int currentPage = (string.IsNullOrEmpty(c.Request["currentpage"]) ? 0 : int.Parse(c.Request["currentpage"]));
            int[] resultPage = PageNavigate.CalPage(recordCount, currentPage, eCPUtil.ROW_PER_PAGE);

            pageHtml += (
                "<div class='content-data-top-bottom'>" +
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "transrepaycontract", eCPUtil.ROW_PER_PAGE) + "</div>" +
                "   <div class='clear'></div>" +
                "</div>"
            );
        }

        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list>" +
            "<pagenav>" + pageHtml + "<pagenav>"
        );
    }

    public static string ListSearchRepayStatusCalInterestOverpayment(string cp2id) {
        string result = eCPDB.ChkRepayStatusCalInterestOverpayment(cp2id);

        return result;
    }
}
