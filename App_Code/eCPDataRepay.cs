/*
Description         : สำหรับการแจ้งชำระหนี้
Date Created        : ๐๖/๐๘/๒๕๕๕
Last Date Modified  : ๑๔/๑๑/๒๕๖๔
Create By           : Yutthaphoom Tawana
*/

using System;
using System.Web;

public class eCPDataRepay {
    public static string StatusRepayNext(
        string _action,
        string _statusRepay
    ) {
        string _nextStatus = String.Empty;

        _nextStatus = _action.Equals("add") ? eCPUtil._repayStatus[int.Parse(_statusRepay) + 1] : _nextStatus;
        _nextStatus = _action.Equals("update") ? eCPUtil._repayStatus[int.Parse(_statusRepay)] + " ( รอเอกสารตอบกลับ )" : _nextStatus;
        _nextStatus = _action.Equals("stop") ? "แจ้งชำระหนี้ครบ 2 ครั้งแล้ว" : _nextStatus;

        return _nextStatus;
    }

    public static string ChkActionRepay(
        string _statusRepay,
        string _statusReply
    ) {
        string _action;

        if (!_statusRepay.Equals("2")) {
            _action = (String.IsNullOrEmpty(_statusReply) || _statusReply.Equals("2")) ? "add" : "update";
        }
        else
            _action = _statusReply.Equals("2") ? "stop" : "update";

        return _action;
    }
    
    private static string DetailCalInterestOverpayment(string[,] _data) {
        string _html = String.Empty;
        string[] _repayDate = eCPUtil.RepayDate(_data[0, 8]);
        string _repayDateStartDefault = !String.IsNullOrEmpty(_repayDate[0]) ? _repayDate[0] : String.Empty;
        string _repayDateEndDefault = !String.IsNullOrEmpty(_repayDate[1]) ? _repayDate[1] : String.Empty;
        string _overpaymentDateStartDefault = !String.IsNullOrEmpty(_repayDate[2]) ? _repayDate[2] : String.Empty;
        string _overpaymentDateEndDefault = Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd"));
        string _overpaymentInterestDefault = String.Empty;
        /*
        string _totalPenaltyDefault = double.Parse(_data[0, 1]).ToString("#,##0.00");
        */
        string _totalPenaltyDefault = double.Parse(_data[0, 11]).ToString("#,##0.00");
        string _capitalDefault = _totalPenaltyDefault;
        double[] _overpayment;
        double _overpaymentYear = 0;
        double _overpaymentDay = 0;
        string[] _contractInterest;

        IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");
        DateTime _dateA = DateTime.Parse(_overpaymentDateStartDefault, _provider);
        DateTime _dateB = DateTime.Parse(_overpaymentDateEndDefault, _provider);
        
        _overpayment = Util.CalcDate(_dateA, _dateB);
        _overpaymentYear = _overpayment[4];
        _overpaymentDay = _overpayment[5];

        _contractInterest = eCPUtil.GetContractInterest();
        _overpaymentInterestDefault = double.Parse(_contractInterest[1]).ToString("#,##0.00");

        _html += "<div class='form-content' id='detail-cal-interest-overpayment'>" +
                 "  <div>" +
                 "      <input type='hidden' id='repay-date-end-hidden' value='" + _repayDateEndDefault + "' />" +
                 "      <input type='hidden' id='overpayment-date-start-hidden' value='" + _overpaymentDateStartDefault + "' />" +
                 "      <input type='hidden' id='overpayment-date-end-hidden' value='" + _overpaymentDateEndDefault + "' />" +
                 "      <input type='hidden' id='overpayment-year-hidden' value='" + _overpaymentYear + "' />" +
                 "      <input type='hidden' id='overpayment-day-hidden' value='" + _overpaymentDay + "' />" +
                 "      <input type='hidden' id='overpayment-interest-hidden' value='" + _overpaymentInterestDefault + "' />" +
                 "      <input type='hidden' id='capital-hidden' value='" + _capitalDefault + "' />" +
                 "      <div>" +
                 "          <div class='content-left' id='payment-label'>" +
                 "              <div class='form-label-discription-style'><div class='form-label-style'>การดำเนินการชำระหนี้</div></div>" +
                 "          </div>" +
                 "          <div class='content-left' id='payment-input'>" +
                 "              <div class='form-input-style'>" +
                 "                  <div class='form-input-content'>" +
                 "                      <div>ผู้ผิดสัญญาชำระหนี้ภายใน <span>" + eCPUtil.PAYMENT_AT_LEAST + "</span> วัน</div>" +
                 "                      <div class='form-input-content-line'>ตั้งแต่วันที่ <span>" + Util.LongDateTH(_repayDateStartDefault) + "</span> ถึงวันที่ <span>" + Util.LongDateTH(_repayDateEndDefault) + "</span></div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='cal-interest-overpayment-label'>" +
                 "                  <div class='form-label-style'>คำนวณดอกเบี้ยผิดนัดชำระ</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณากดปุ่มคำนวณเพื่อทำการคำนวณระยะเวลาผิดนัดชำระ</div>" +
                 "                      <div class='form-discription-line2-style'>และคำนวณดอกเบี้ยผิดนัดชำระ</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='cal-interest-overpayment-input'>" +
                 "                  <div class='button-style2'>" +
                 "                      <ul>" +
                 "                          <li><a href='javascript:void(0)' onclick='CalculateInterestOverpayment()'>คำนวณ</a></li>" +
                 "                      </ul>" +
                 "                  </div>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='overpayment-date-start-label'>ระยะเวลาผิดนัดชำระตั้งแต่วันที่</div>" +
                 "                      <div class='content-left' id='overpayment-date-start-input'><input class='inputbox calendar' type='text' id='overpayment-date-start' readonly value='' /></div>" +
                 "                      <div class='content-left' id='overpayment-date-end-label'>ถึงวันที่</div>" +
                 "                      <div class='content-left' id='overpayment-date-end-input'><input class='inputbox calendar' type='text' id='overpayment-date-end' readonly value='' /></div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='overpayment-year-day-label'>นับระยะเวลาการผิดนัดชำระได้</div>" +
                 "                      <div class='content-left' id='overpayment-year-input'><input class='inputbox textbox-numeric' type='text' id='overpayment-year' value='' style='width:32px' /></div>" +
                 "                      <div class='content-left' id='overpayment-year-unit-label'>ปี</div>" +
                 "                      <div class='content-left' id='overpayment-day-input'><input class='inputbox textbox-numeric' type='text' id='overpayment-day' value='' style='width:39px' /></div>" +
                 "                      <div class='content-left' id='overpayment-day-unit-label'>วัน</div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='overpayment-interest-label'>ดอกเบี้ยผิดนัดชำระอัตราร้อยละ</div>" +
                 "                      <div class='content-left' id='overpayment-interest-input'><input class='inputbox textbox-numeric' type='text' id='overpayment-interest' onblur=Trim('overpayment-interest');AddCommas('overpayment-interest',2) onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:100px' /></div>" +
                 "                      <div class='content-left' id='overpayment-interest-unit-label'>ต่อปี</div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='capital-label'>ยอดเงินค่าปรับผิดสัญญา</div>" +
                 "                      <div class='content-left' id='capital-input'><input class='inputbox textbox-numeric' type='text' id='capital' value='' style='width:100px' /></div>" +
                 "                      <div class='content-left' id='capital-unit-label'>บาท</div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='total-interest-overpayment-label'>ยอดดอกเบี้ยจากการผิดนัดชำระ</div>" +
                 "                      <div class='content-left' id='total-interest-overpayment-input'><input class='inputbox textbox-numeric' type='text' id='total-interest-overpayment' value='' style='width:100px' /></div>" +
                 "                      <div class='content-left' id='total-interest-overpayment-unit-label'>บาท</div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='total-payment-label'>ยอดเงินที่ต้องรับผิดชอบชดใช้</div>" +
                 "                      <div class='content-left' id='total-payment-input'><input class='inputbox textbox-numeric' type='text' id='total-payment' value='' style='width:100px' /></div>" +
                 "                      <div class='content-left' id='total-payment-unit-label'>บาท</div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ResetFrmCalInterestOverpayment(false)'>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string DetailCalInterestOverpayment(string _cp2id) {
        string _html = String.Empty;
        string _statusRepay = String.Empty;
        string[,] _data1;
        string[,] _data2;

        _data1 = eCPDB.ListCPTransRepayContract(_cp2id);

        if (_data1.GetLength(0) > 0) {
            _statusRepay = _data1[0, 2];

            _data2 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(_cp2id, _statusRepay);

            if (_data2.GetLength(0) > 0)
                _data1[0, 8] = _data2[0, 5];

            _html += DetailCalInterestOverpayment(_data1);
        }

        return _html;
    } 
    
    private static string AddUpdateCPTransRepayContract(string[,] _data) {
        int _i;
        string _html = String.Empty;
        string _cp1id = _data[0, 2];
        string _cp2id = _data[0, 1];
        string _statusRepayDefault = _data[0, 18];
        string _statusPaymentDefault = _data[0, 58];
        string _statusReplyDefault = String.Empty;
        string _replyResultDefault = String.Empty;
        string _repayDateDefault = String.Empty;
        string _replyDateDefault = String.Empty;
        string _replyDateMark = String.Empty;
        string[] _repayDate;        
        string _repayDateStart = String.Empty;
        string _repayDateEnd = String.Empty;
        string _previousRepayDateEnd = String.Empty;
        string _overpaymentDateStart = String.Empty;
        string _pursuantDefault = String.Empty;
        string _pursuantBookDateDefault = String.Empty;

        string[,] _data1;
        string[,] _data2;
        string[] _statusRepayCurrent;
        string _action;
        double[] _overpayment;

        _data1 = eCPDB.ListCPTransRepayContract(_cp2id);

        if (_data1.GetLength(0) > 0) {
            _statusReplyDefault = _data1[0, 3];
            _replyResultDefault = _data1[0, 4];
            _repayDateDefault = _data1[0, 5];
            _replyDateDefault = _data1[0, 6];
            _replyDateMark = _data1[0, 8];
            _repayDate = eCPUtil.RepayDate(_replyDateMark);
            _repayDateStart = !String.IsNullOrEmpty(_repayDate[0]) ? _repayDate[0] : String.Empty;
            _repayDateEnd = !String.IsNullOrEmpty(_repayDate[1]) ? _repayDate[1] : String.Empty;
            _overpaymentDateStart = !String.IsNullOrEmpty(_repayDate[2]) ? _repayDate[2] : String.Empty;
            _pursuantDefault = _data1[0, 12];
            _pursuantBookDateDefault = _data1[0, 13];
        }

        _statusRepayCurrent = (eCPDB.SearchRepayStatusDetail(_cp2id, _statusRepayDefault, _statusPaymentDefault)).Split(new char[] { ';' });
        _action = ChkActionRepay(_statusRepayDefault, _statusReplyDefault);
        _data2 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(_cp2id, _statusRepayDefault);

        if (_data2.GetLength(0) > 0) {
            _replyDateMark = _data2[0, 5];
            _repayDate = eCPUtil.RepayDate(_replyDateMark);
            _repayDateStart = !String.IsNullOrEmpty(_repayDate[0]) ? _repayDate[0] : String.Empty;
            _repayDateEnd = !String.IsNullOrEmpty(_repayDate[1]) ? _repayDate[1] : String.Empty;
            _overpaymentDateStart = !String.IsNullOrEmpty(_repayDate[2]) ? _repayDate[2] : String.Empty;
        }

        _html += "<div class='form-content' id='addupdate-cp-trans-repay-contract'>" +
                 "  <div>" +
                 "      <input type='hidden' id='action' value='" + _action + "' />" +
                 "      <input type='hidden' id='cp2id' value='" + _cp2id + "' />" +
                 "      <input type='hidden' id='status-repay-hidden' value='" + (_action.Equals("add") ? (int.Parse(_statusRepayDefault) + 1).ToString() : _statusRepayDefault) + "' />" +
                 "      <input type='hidden' id='repay-date-hidden' value='" + (_action.Equals("add") ? Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd")) : _repayDateDefault) + "' />" +
                 "      <input type='hidden' id='reply-date-hidden' value='" + _replyDateDefault + "' />" +
                 "      <input type='hidden' id='pursuant-hidden' value='" + _pursuantDefault + "' />" +
                 "      <input type='hidden' id='pursuant-book-date-hidden' value='" + _pursuantBookDateDefault + "' />";
        /*
        _data2 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(_cp2id, _statusRepayDefault);
        */

        if (_data2.GetLength(0) > 0) {
            _html += "  <div>" +
                     "      <div class='content-left' id='history-repay-label'>" +
                     "          <div class='form-label-discription-style clear-bottom'><div class='form-label-style'>ประวัติการแจ้งชำระหนี้</div></div>" +
                     "      </div>" +
                     "      <div class='content-left' id='history-repay-input'>" +
                     "          <div class='form-input-style clear-bottom'>" +
                     "              <div class='form-input-content'>";

            for(_i = 0; _i < _data2.GetLength(0); _i++) {
                _previousRepayDateEnd = (_i < _data2.GetLength(0)) ? _data2[_i, 5] : _previousRepayDateEnd;

                _html += "              <div><span>" + eCPUtil._repayStatus[int.Parse(_data2[_i, 1])] + "</span></div>" +
                         "              <div class='form-input-content-line'>แจังให้ผู้ผิดสัญญาชำระหนี้เมื่อวันที่ <span>" + Util.LongDateTH(_data2[_i, 4]) + "</span></div>" +
                         "              <div class='form-input-content-line'>หนังสือขอให้ชดใช้เงินที่ อว 78/<span>" + (!String.IsNullOrEmpty(_data2[_i, 6]) ? _data2[_i, 6] : " -") + "</span> ลงวันที่ <span>" + (!String.IsNullOrEmpty(_data2[_i, 7]) ? Util.LongDateTH(_data2[_i, 7]) : " -") + "</span></div>" +
                         "              <div class='form-input-content-line'>รับเอกสารตอบกลับจากไปรษณีย์เมื่อวันที่ <span>" + Util.LongDateTH(_data2[_i, 5]) + "</span></div>" +
                         "              <div class='form-input-content-line'>ผลการรับทราบการแจ้งชำระหนี้ <span>" + eCPUtil._resultReply[int.Parse(_data2[_i, 3]) - 1] + "</span></div>" +
                         "              <div class='form-input-content-line'><a class='text-underline' href='javascript:void(0)' onclick=PrintNoticeClaimDebt('" + _cp1id + "'," + _data2[_i, 1] + ",'" + _previousRepayDateEnd + "')>พิมพ์แบบหนังสือทวงถามครั้งที่ " + _data2[_i, 1] + "</a></div>";

                if (_data2[_i, 1].Equals("1"))
                    _html += "          <div class='form-input-content-line'><a class='text-underline' href='javascript:void(0)' onclick=PrintFormRequestCreateAndUpdateDebtor('" + _cp1id + "')>พิมพ์แบบขอสร้างและปรับปรุงข้อมูลหลักลูกหนี้</a></div>";
            }

            _html += "              </div>" +
                     "          </div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='clear'></div>" +
                     "  <div class='box3'></div>";
        }

        if ((_statusRepayDefault.Equals("0") && _action.Equals("add")) || (_action.Equals("update"))) {
            _html += "  <div>" +
                     "      <div class='content-left' id='status-repay" + _statusRepayDefault + "-current-label'>" +
                     "          <div class='form-label-discription-style " + ((_statusRepayDefault.Equals("0") && _action.Equals("add")) ? "clear-bottom" : "") + "'><div class='form-label-style'>สถานะการแจ้งชำระหนี้ปัจจุบัน</div></div>" +
                     "      </div>" +
                     "      <div class='content-left' id='status-repay" + _statusRepayDefault + "-current-input'>" +
                     "          <div class='form-input-style " + ((_statusRepayDefault.Equals("0") && _action.Equals("add")) ? "clear-bottom" : "") + "'>" +
                     "              <div class='form-input-content'>" +
                     "                  <div><span>" + eCPUtil._repayStatusDetail[int.Parse(_statusRepayCurrent[0])] + "</span></div>";

            if (_action.Equals("update")) {
                _html += "              <div class='form-input-content-line'><a class='text-underline' href='javascript:void(0)' onclick=PrintNoticeClaimDebt('" + _cp1id + "'," + _statusRepayDefault + ",'" + _previousRepayDateEnd + "')>พิมพ์แบบหนังสือทวงถามครั้งที่ " + _statusRepayDefault + "</a></div>";

                if (_statusRepayDefault.Equals("1"))
                    _html += "          <div class='form-input-content-line'><a class='text-underline' href='javascript:void(0)' onclick=PrintFormRequestCreateAndUpdateDebtor('" + _cp1id + "')>พิมพ์แบบขอสร้างและปรับปรุงข้อมูลหลักลูกหนี้</a></div>";
            }

            _html += "              </div>" +
                     "          </div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='clear'></div>";

            if (_action.Equals("update")) {
                _html += "<div>" +
                         "  <div class='form-label-discription-style'>" +
                         "      <div id='repay-date-label'>" +
                         "          <div class='form-label-style'>วันที่แจังให้ผู้ผิดสัญญาชำระหนี้</div>" +
                         "          <div class='form-discription-style'>" +
                         "              <div class='form-discription-line1-style'>กรุณาใส่วันที่ที่ได้แจ้งให้ผู้ผิดสัญญาหรือผู้ค้ำประกัน</div>" +
                         "              <div class='form-discription-line2-style'>หรือผู้ได้รับมอบหมายชำระหนี้</div>" +
                         "          </div>" +
                         "      </div>" +
                         "  </div>";

                if (_data2.GetLength(0) > 0)
                    _html += "<input type='hidden' id='previous-reply-date' value='" + _data2[_data2.GetLength(0) - 1, 5] + "' />";

                _html += "  <div class='form-input-style'>" +
                         "      <div class='form-input-content' id='repay-date-input'><input class='inputbox calendar' type='text' id='repay-date' readonly value='' /></div>" +
                         "  </div>" +
                         "</div>" +
                         "<div class='clear'></div>";
            }
        }

        if ((_statusRepayDefault.Equals("1") && _action.Equals("add")) || (_statusRepayDefault.Equals("2") && _action.Equals("stop"))) {
            _html += "  <div id='status-repay" + _statusRepayDefault + "-" + _action + "'>" +
                     "      <div class='content-left' id='status-repay23-current-label'>" +
                     "          <div class='form-label-discription-style clear-bottom'><div class='form-label-style'>สถานะการแจ้งชำระหนี้ปัจจุบัน</div></div>" +
                     "      </div>" +
                     "      <div class='content-left' id='status-repay23-current-input'>" +
                     "          <div class='form-input-style clear-bottom'>" +
                     "              <div class='form-input-content'>" +
                     "                  <div><span>" + eCPUtil._repayStatus[int.Parse(_statusRepayDefault)] + "</span></div>" +
                     "                  <div class='form-input-content-line'>แจังให้ผู้ผิดสัญญาชำระหนี้เมื่อวันที่ <span>" + Util.LongDateTH(_repayDateDefault) + "</span></div>" +
                     "                  <div class='form-input-content-line'>หนังสือขอให้ชดใช้เงินที่ อว 78/<span>" + (!String.IsNullOrEmpty(_pursuantDefault) ? _pursuantDefault : " -") + "</span> ลงวันที่ <span>" + (!String.IsNullOrEmpty(_pursuantBookDateDefault) ? Util.LongDateTH(_pursuantBookDateDefault) : " -") + "</span></div>" +
                     "                  <div class='form-input-content-line'>รับเอกสารตอบกลับจากไปรษณีย์เมื่อวันที่ <span>" + Util.LongDateTH(_replyDateDefault) + "</span></div>" +
                     "                  <div class='form-input-content-line'>ผลการรับทราบการแจ้งชำระหนี้ <span>" + eCPUtil._resultReply[int.Parse(_replyResultDefault) - 1] + "</span></div>" +
                     "                  <div class='form-input-content-line'><a class='text-underline' href='javascript:void(0)' onclick=PrintNoticeClaimDebt('" + _cp1id + "'," + _statusRepayDefault + ",'" + _previousRepayDateEnd + "')>พิมพ์แบบหนังสือทวงถามครั้งที่ " + _statusRepayDefault + "</a></div>";

            if (_statusRepayDefault.Equals("1") && _action.Equals("add"))
                _html += "              <div class='form-input-content-line'><a class='text-underline' href='javascript:void(0)' onclick=PrintFormRequestCreateAndUpdateDebtor('" + _cp1id + "')>พิมพ์แบบขอสร้างและปรับปรุงข้อมูลหลักลูกหนี้</a></div>";

            _html += "              </div>" +
                     "          </div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='clear'></div>";
        }

        if (_action.Equals("update")) {
            _html += "  <div id='notice-claim-debt'>" +
                     "      <div>" +
                     "          <div class='form-label-discription-style'>" +
                     "              <div id='notice-claim-debt-label'>" +
                     "                  <div class='form-label-style'>หนังสือขอให้ชดใช้เงิน ( ครั้งที่ " + _statusRepayDefault + ")</div>" +
                     "                  <div class='form-discription-style'>" +
                     "                      <div class='form-discription-line1-style'>กรุณาใส่ข้อมูลเกี่ยวกับหนังสือขอให้ชดใช้เงิน แต่ละครั้ง</div>" +
                     "                      <div class='form-discription-line2-style'>ซึ่งจะเป็นการบันทึกข้อมูลย้อนหลังพร้อมกับการบันทึก</div>" +
                     "                      <div class='form-discription-line3-style'>วันที่รับเอกสาร</div>" +
                     "                  </div>" +
                     "              </div>" +
                     "          </div>" +
                     "          <div class='form-input-style'>" +
                     "              <div class='form-input-content' id='notice-claim-debt-input'>" +
                     "                  <div>" +
                     "                      <div class='content-left' id='pursuant-label'>หนังสือมหาวิทยาลัยมหิดลที่ อว 78/</div>" +
                     "                      <div class='content-left' id='pursuant-input'><input class='inputbox' type='text' id='pursuant' value='' style='width:193px' /></div>" +
                     "                  </div>" +
                     "                  <div class='clear'></div>" +
                     "                  <div>" +
                     "                      <div class='content-left' id='pursuant-book-date-label'>ลงวันที่</div>" +
                     "                      <div class='content-left' id='pursuant-book-date-input'><input class='inputbox calendar' type='text' id='pursuant-book-date' readonly value='' /></div>" +
                     "                  </div>" +
                     "                  <div class='clear'></div>" +
                     "              </div>" +
                     "          </div>" +
                     "      </div>" +
                     "      <div class='clear'></div>" +
                     "  </div>" +
                     "  <div>" +
                     "      <div class='form-label-discription-style'>" +
                     "          <div id='reply-date-label'>" +
                     "              <div class='form-label-style'>วันที่รับเอกสารตอบกลับจากไปรษณีย์</div>" +
                     "              <div class='form-discription-style'>" +
                     "                  <div class='form-discription-line1-style'>กรุณาใส่วันที่ที่ได้รับเอกสารตอบกลับจากไปรษณีย์</div>" +
                     "                  <div class='form-discription-line2-style'>หลังจากที่แจ้งชำระหนี้ให้ผู้ผิดสัญญาหรือผู้ค้ำประกัน</div>" +
                     "                  <div class='form-discription-line3-style'>หรือผู้ได้รับมอบหมายทราบ</div>" +
                     "              </div>" +
                     "          </div>" +
                     "      </div>" +
                     "      <div class='form-input-style'>" +
                     "          <div class='form-input-content' id='reply-date-input'><input class='inputbox calendar' type='text' id='reply-date' readonly value='' /></div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='clear'></div>" +
                     "  <div>" +
                     "      <div class='form-label-discription-style " + ((_statusRepayDefault.Equals("2")) && (!String.IsNullOrEmpty(_overpaymentDateStart)) ? "clear-bottom" : "") + "'>" +
                     "          <div id='detail-reply-label'>" +
                     "              <div class='form-label-style'>ผลการรับทราบการแจ้งชำระหนี้</div>" +
                     "              <div class='form-discription-style'>" +
                     "                  <div class='form-discription-line1-style'>กรุณาใส่ผลการรับทราบการแจ้งชำระหนี้ของผู้ผิดสัญญา</div>" +
                     "                  <div class='form-discription-line2-style'>หรือผู้ค้ำประกัน หรือผู้ได้รับมอบหมาย</div>" +
                     "              </div>" +
                     "          </div>" +
                     "      </div>" +
                     "      <div class='form-input-style " + ((_statusRepayDefault.Equals("2")) && (!String.IsNullOrEmpty(_overpaymentDateStart)) ? "clear-bottom" : "") + "'>" +
                     "          <div class='form-input-content' id='detail-reply-input'>" +
                     "              <div>" +
                     "                  <div class='content-left' id='reply-yes-input'><input class='radio' type='radio' name='reply-result' value='1' /></div>" +
                     "                  <div class='content-left' id='reply-yes-label'>" + eCPUtil._resultReply[0] + "</div>" +
                     "              </div>" +
                     "              <div class='clear'></div>" +
                     "              <div>" +
                     "                  <div class='content-left' id='reply-no-input'><input class='radio' type='radio' name='reply-result' value='2' /></div>" +
                     "                  <div class='content-left' id='reply-no-label'>" + eCPUtil._resultReply[1] + "</div>" +
                     "              </div>" +
                     "              <div class='clear'></div>" +
                     "          </div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='clear'></div>";
        }

        if ((_statusRepayDefault.Equals("1") && _action.Equals("add")) || (_statusRepayDefault.Equals("2") && _action.Equals("update")) || (_statusRepayDefault.Equals("2") && _action.Equals("stop"))) {
            if (!String.IsNullOrEmpty(_overpaymentDateStart)) {
                IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");
                DateTime _dateA = DateTime.Parse(_overpaymentDateStart, _provider);
                DateTime _dateB = DateTime.Parse(Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd")), _provider);

                _overpayment = Util.CalcDate(_dateA, _dateB);

                string _clearBottom = String.Empty;

                if ((_action.Equals("add") || _action.Equals("stop")) && _statusPaymentDefault.Equals("1"))
                    _clearBottom = "clear-bottom";

                _html += "<div class='box3'></div>" +
                         "<div>" + 
                         "  <div class='content-left' id='payment-label'>" +
                         "      <div class='form-label-discription-style " + _clearBottom + "'><div class='form-label-style'>การดำเนินการชำระหนี้</div></div>" +
                         "  </div>" +
                         "  <div class='content-left' id='payment-input'>" +
                         "      <div class='form-input-style " + _clearBottom + "'>" +
                         "          <div class='form-input-content'>" +
                         "              <div>ผู้ผิดสัญญาชำระหนี้ภายใน <span>" + eCPUtil.PAYMENT_AT_LEAST + "</span> วัน</div>" +
                         "              <div class='form-input-content-line'>ตั้งแต่วันที่ <span>" + Util.LongDateTH(_repayDateStart) + "</span> ถึงวันที่ <span>" + Util.LongDateTH(_repayDateEnd) + "</span></div>" +
                         "              <div class='form-input-content-line'>ระยะเวลาผิดนัดชำระ <span>" + (_statusPaymentDefault.Equals("1") && !_overpayment[4].Equals(0) ? _overpayment[4].ToString("#,##0") : "-") + "</span> ปี <span>" + (_statusPaymentDefault.Equals("1") && !_overpayment[5].Equals(0) ? _overpayment[5].ToString("#,##0") : "-") + "</span> วัน</div>" +
                         "          </div>" +
                         "      </div>" +
                         "  </div>" +
                         "</div>" +
                         "<div class='clear'></div>";
            }
        }

        /*
        if (!(_statusReplyDefault + _replyResultDefault).Equals("21"))
        {
        */
        if ((_action.Equals("add") || _action.Equals("stop")) && _statusPaymentDefault.Equals("1")) {
            _html += "  <div class='box3'></div>" +
                     "  <div>" +
                     "      <div class='content-left' id='status-repay-label'>" +
                     "          <div class='form-label-discription-style " + (_action.Equals("stop") && !String.IsNullOrEmpty(_overpaymentDateStart) ? "" : "") + "'><div class='form-label-style'>สถานะการแจ้งชำระหนี้ที่จะดำเนินการต่อ</div></div>" +
                     "      </div>" +
                     "      <div class='content-left' id='status-repay-input'>" +
                     "          <div class='form-input-style " + (_action.Equals("stop") && !String.IsNullOrEmpty(_overpaymentDateStart) ? "" : "") + "'><div class='form-input-content'><span>" + StatusRepayNext(_action, _statusRepayDefault) + "</span></div></div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='clear'></div>";

            if (_action.Equals("add")) {
                _html += "<div>" +
                         "  <div class='form-label-discription-style " + (!String.IsNullOrEmpty(_overpaymentDateStart) ? "" : "") + "'>" +
                         "      <div id='repay-date-label'>" +
                         "          <div class='form-label-style'>วันที่แจังให้ผู้ผิดสัญญาชำระหนี้</div>" +
                         "          <div class='form-discription-style'>" +
                         "              <div class='form-discription-line1-style'>กรุณาใส่วันที่ที่ได้แจ้งให้ผู้ผิดสัญญาหรือผู้ค้ำประกัน</div>" +
                         "              <div class='form-discription-line2-style'>หรือผู้ได้รับมอบหมายชำระหนี้</div>" +
                         "          </div>" +
                         "      </div>" +
                         "  </div>" +                 
                         "  <input type='hidden' id='previous-reply-date' value='" + _replyDateDefault + "' />" +
                         "  <div class='form-input-style " + (!String.IsNullOrEmpty(_overpaymentDateStart) ? "" : "") + "'>" +
                         "      <div class='form-input-content' id='repay-date-input'><input class='inputbox calendar' type='text' id='repay-date' readonly value='' /></div>" +
                         "  </div>" +
                         "</div>" +
                         "<div class='clear'></div>";
            }
        }
        /*
        }
        */

        _html += "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1' id='button-style1" + (!_action.Equals("stop") && _statusPaymentDefault.Equals("1") ? "1" : "2") + "'>" +
                 /*
                 "      <div class='button-style1' id='button-style1" + (!_action.Equals("stop") && !(_statusReplyDefault + _replyResultDefault).Equals("21") ? "1" : "2") + "'>" +
                 */
                 "          <ul>";

        /*
        if (!_action.Equals("stop") && !(_statusReplyDefault + _replyResultDefault).Equals("21"))
        */
        if (!_action.Equals("stop") && _statusPaymentDefault.Equals("1"))
        {
            _html += "          <li><a href='javascript:void(0)' onclick=ConfirmActionCPTransRepayContract('" + _action + "')>บันทึก</a></li>" +
                     "          <li><a href='javascript:void(0)' onclick='ResetFrmCPTransRepayContract(false)'>ล้าง</a></li>";
        }                            

        _html += "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>" +
                 "<iframe class='export-target' id='export-target' name='export-target' src='#'></iframe>" +
                 "<form id='export-setvalue' method='post' target='export-target'>" +
                 "  <input id='export-send' name='export-send' value='' type='hidden' />" +
                 "  <input id='export-order' name='export-order' value='' type='hidden' />" +
                 "  <input id='export-type' name='export-type' value='' type='hidden' />" +
                 "</form>";

        return _html;
    }

    private static string ViewCPTransRepayContract(string[,] _data) {
        int _i;
        string _html = String.Empty;
        string _cp1id = _data[0, 2];
        string _cp2id = _data[0, 1];
        string _statusRepayDefault = _data[0, 18];
        string _statusPaymentDefault = _data[0, 58];
        string _statusReplyDefault = String.Empty;
        string _replyResultDefault = String.Empty;
        string _repayDateDefault = String.Empty;
        string _replyDateDefault = String.Empty;
        string _replyDateMark = String.Empty;
        string[] _repayDate;
        string _repayDateStart = String.Empty;
        string _repayDateEnd = String.Empty;
        string _previousRepayDateEnd = String.Empty;
        string _overpaymentDateStart = String.Empty;
        string[,] _data1;
        string[,] _data2;
        string[] _statusRepayCurrent;
        string _action;
        double[] _overpayment;

        _data1 = eCPDB.ListCPTransRepayContract(_cp2id);

        if (_data1.GetLength(0) > 0) {
            _statusReplyDefault = _data1[0, 3];
            _replyResultDefault = _data1[0, 4];
            _repayDateDefault = _data1[0, 5];
            _replyDateDefault = _data1[0, 6];
            _replyDateMark = _data1[0, 8];
            _repayDate = eCPUtil.RepayDate(_replyDateMark);
            _repayDateStart = !String.IsNullOrEmpty(_repayDate[0]) ? _repayDate[0] : String.Empty;
            _repayDateEnd = !String.IsNullOrEmpty(_repayDate[1]) ? _repayDate[1] : String.Empty;
            _overpaymentDateStart = !String.IsNullOrEmpty(_repayDate[2]) ? _repayDate[2] : String.Empty;
        }

        _statusRepayCurrent = (eCPDB.SearchRepayStatusDetail(_cp2id, _statusRepayDefault, _statusPaymentDefault)).Split(new char[] { ';' });
        _action = ChkActionRepay(_statusRepayDefault, _statusReplyDefault);
        _data2 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(_cp2id, _statusRepayDefault);

        if (_data2.GetLength(0) > 0) {
            _replyDateMark = _data2[0, 5];
            _repayDate = eCPUtil.RepayDate(_replyDateMark);
            _repayDateStart = !String.IsNullOrEmpty(_repayDate[0]) ? _repayDate[0] : String.Empty;
            _repayDateEnd = !String.IsNullOrEmpty(_repayDate[1]) ? _repayDate[1] : String.Empty;
            _overpaymentDateStart = !String.IsNullOrEmpty(_repayDate[2]) ? _repayDate[2] : String.Empty;
        }
        
        _html += "<div class='form-content' id='view-cp-trans-repay-contract'>" +
                 "  <div>";

        /*
        _data2 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(_cp2id, _statusRepayDefault);
        */

        if (_data2.GetLength(0) > 0) {
            _html += "  <div>" +
                     "      <div class='content-left' id='history-repay-label'>" +
                     "          <div class='form-label-discription-style clear-bottom'><div class='form-label-style'>ประวัติการแจ้งชำระหนี้</div></div>" +
                     "      </div>" +
                     "      <div class='content-left' id='history-repay-input'>" +
                     "          <div class='form-input-style clear-bottom'>" +
                     "              <div class='form-input-content'>";

            for(_i = 0; _i < _data2.GetLength(0); _i++) {
                _previousRepayDateEnd = (_i < _data2.GetLength(0)) ? _data2[_i, 5] : _previousRepayDateEnd;

                _html += "              <div><span>" + eCPUtil._repayStatus[int.Parse(_data2[_i, 1])] + "</span></div>" +
                         "              <div class='form-input-content-line'>แจังให้ผู้ผิดสัญญาชำระหนี้เมื่อวันที่ <span>" + Util.LongDateTH(_data2[_i, 4]) + "</span></div>" +
                         "              <div class='form-input-content-line'>รับเอกสารตอบกลับจากไปรษณีย์เมื่อวันที่ <span>" + Util.LongDateTH(_data2[_i, 5]) + "</span></div>" +
                         "              <div class='form-input-content-line'>ผลการรับทราบการแจ้งชำระหนี้ <span>" + eCPUtil._resultReply[int.Parse(_data2[_i, 3]) - 1] + "</span></div>" +
                         "              <div class='form-input-content-line'><a class='text-underline' href='javascript:void(0)' onclick=PrintNoticeClaimDebt('" + _cp1id + "'," + _data2[_i, 1] + ",'" + _previousRepayDateEnd + "')>พิมพ์แบบหนังสือทวงถามครั้งที่ " + _data2[_i, 1] + "</a></div>";

                if (_data2[_i, 1].Equals("1"))
                    _html += "          <div class='form-input-content-line'><a class='text-underline' href='javascript:void(0)' onclick=PrintFormRequestCreateAndUpdateDebtor('" + _cp1id + "')>พิมพ์แบบขอสร้างและปรับปรุงข้อมูลหลักลูกหนี้</a></div>";
            }

            _html += "              </div>" +
                     "          </div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='clear'></div>" +
                     "  <div class='box3'></div>";
        }

        if ((_statusRepayDefault.Equals("0") && _action.Equals("add")) || (_action.Equals("update"))) {
            _html += "  <div>" +
                     "      <div class='content-left' id='status-repay" + _statusRepayDefault + "-current-label'>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>สถานะการแจ้งชำระหนี้ปัจจุบัน</div></div>" +
                     "      </div>" +
                     "      <div class='content-left' id='status-repay" + _statusRepayDefault + "-current-input'>" +
                     "          <div class='form-input-style'>" +
                     "              <div class='form-input-content'>" +
                     "                  <div><span>" + eCPUtil._repayStatusDetail[int.Parse(_statusRepayCurrent[0])] + "</span></div>";

            if (_action.Equals("update")) {
                _html += "              <div class='form-input-content-line'><a class='text-underline' href='javascript:void(0)' onclick=PrintNoticeClaimDebt('" + _cp1id + "'," + _statusRepayDefault + ",'" + _previousRepayDateEnd + "')>พิมพ์แบบหนังสือทวงถามครั้งที่ " + _statusRepayDefault + "</a></div>";

                if (_statusRepayDefault.Equals("1"))
                    _html += "          <div class='form-input-content-line'><a class='text-underline' href='javascript:void(0)' onclick=PrintFormRequestCreateAndUpdateDebtor('" + _cp1id + "')>พิมพ์แบบขอสร้างและปรับปรุงข้อมูลหลักลูกหนี้</a></div>";
            }

            _html += "              </div>" +
                     "          </div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='clear'></div>";
        }

        if ((_statusRepayDefault.Equals("1") && _action.Equals("add")) || (_statusRepayDefault.Equals("2") && _action.Equals("stop"))) {
            _html += "  <div id='status-repay" + _statusRepayDefault + "-" + _action + "'>" +
                     "      <div class='content-left' id='status-repay23-current-label'>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>สถานะการแจ้งชำระหนี้ปัจจุบัน</div></div>" +
                     "      </div>" +
                     "      <div class='content-left' id='status-repay23-current-input'>" +
                     "          <div class='form-input-style'>" +
                     "              <div class='form-input-content'>" +
                     "                  <div><span>" + eCPUtil._repayStatus[int.Parse(_statusRepayDefault)] + "</span></div>" +
                     "                  <div class='form-input-content-line'>แจังให้ผู้ผิดสัญญาชำระหนี้เมื่อวันที่ <span>" + Util.LongDateTH(_repayDateDefault) + "</span></div>" +
                     "                  <div class='form-input-content-line'>รับเอกสารตอบกลับจากไปรษณีย์เมื่อวันที่ <span>" + Util.LongDateTH(_replyDateDefault) + "</span></div>" +
                     "                  <div class='form-input-content-line'>ผลการรับทราบการแจ้งชำระหนี้ <span>" + eCPUtil._resultReply[int.Parse(_replyResultDefault) - 1] + "</span></div>" +
                     "                  <div class='form-input-content-line'><a class='text-underline' href='javascript:void(0)' onclick=PrintNoticeClaimDebt('" + _cp1id + "'," + _statusRepayDefault + ",'" + _previousRepayDateEnd + "')>พิมพ์แบบหนังสือทวงถามครั้งที่ " + _statusRepayDefault + "</a></div>";

            if (_statusRepayDefault.Equals("1") && _action.Equals("add"))
                _html += "              <div class='form-input-content-line'><a class='text-underline' href='javascript:void(0)' onclick=PrintFormRequestCreateAndUpdateDebtor('" + _cp1id + "')>พิมพ์แบบขอสร้างและปรับปรุงข้อมูลหลักลูกหนี้</a></div>";

            _html += "              </div>" +
                     "          </div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='clear'></div>";
        }

        _html += "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>" +
                 "<iframe class='export-target' id='export-target' name='export-target' src='#'></iframe>" +
                 "<form id='export-setvalue' method='post' target='export-target'>" +
                 "  <input id='export-send' name='export-send' value='' type='hidden' />" +
                 "  <input id='export-order' name='export-order' value='' type='hidden' />" +
                 "  <input id='export-type' name='export-type' value='' type='hidden' />" +
                 "</form>";

        return _html;
    }

    public static string AddUpdateCPTransRepayContract(string _cp1id) {
        string _html = String.Empty;
        string[,] _data;

        _data = eCPDB.ListDetailCPTransRequireContract(_cp1id);

        if (_data.GetLength(0) > 0)
            _html += AddUpdateCPTransRepayContract(_data);

        return _html;
    }

    public static string ViewCPTransRepayContract(string _cp1id) {
        string _html = String.Empty;
        string[,] _data;

        _data = eCPDB.ListDetailCPTransRequireContract(_cp1id);

        if (_data.GetLength(0) > 0)
            _html += ViewCPTransRepayContract(_data);

        return _html;
    }

    public static string ListRepay(HttpContext _c) {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string _pageHtml = String.Empty;
        string[,] _data, _data1;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        string _trackingStatus = String.Empty;
        string[] _iconStatus;
        int _recordCount;
        int _i;
        int[] _resultPage = new int[2];
        int _currentPage;
        string[] _repayDate;
        IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");
        DateTime _dateA;
        DateTime _dateB;
        double[] _overpaymentArray;
        string _overpayment = String.Empty;

        _recordCount = eCPDB.CountRepay(_c);

        if (_recordCount > 0) {
            _data = eCPDB.ListRepay1(_c);

            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++) {
                _overpayment = String.Empty;
                /*
                _data1 = eCPDB.ListCPTransRepayContract(_data[_i, 1]);

                if ((_data1.GetLength(0) > 0) && (!String.IsNullOrEmpty(_data1[0, 8])) && (_data1[0, 7].Equals("1")))
                {
                    _repayDate = eCPUtil.RepayDate(_data1[0, 8]);
                    _dateA = DateTime.Parse(_repayDate[2], _provider);
                    _dateB = DateTime.Parse(Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd")), _provider);
                    _overpaymentArray = Util.CalcDate(_dateA, _dateB);
                    _overpayment = !_overpaymentArray[0].Equals(0) ? _overpaymentArray[0].ToString("#,##0") : "-";
                }
                */
                if ((!String.IsNullOrEmpty(_data[_i, 21])) && (_data[_i, 15].Equals("1"))) {
                    _repayDate = eCPUtil.RepayDate(_data[_i, 21]);
                    _dateA = DateTime.Parse(_repayDate[2], _provider);
                    _dateB = DateTime.Parse(Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd")), _provider);
                    _overpaymentArray = Util.CalcDate(_dateA, _dateB);
                    _overpayment = !_overpaymentArray[0].Equals(0) ? _overpaymentArray[0].ToString("#,##0") : "-";
                }

                _groupNum = !_data[_i, 9].Equals("0") ? " ( กลุ่ม " + _data[_i, 9] + " )" : "";
                _trackingStatus = _data[_i, 10] + _data[_i, 11] + _data[_i, 12] + _data[_i, 13];
                _callFunc = "ViewRepayStatusViewTransRequireContract('" + _data[_i, 2] + "','" + _data[_i, 1] + "','" + _trackingStatus + "','r')";
                _iconStatus = _data[_i, 16].Split(new char[] {';'});
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _html += "<ul class='table-row-content " + _highlight + "' id='repay" + _data[_i, 1] + "'>" +
                         "  <li id='tab2-table-content-repay-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='tab2-table-content-repay-col2' onclick=" + _callFunc + "><div>" + _data[_i, 3] + "</div></li>" +
                         "  <li class='table-col' id='tab2-table-content-repay-col3' onclick=" + _callFunc + "><div>" + _data[_i, 4] + _data[_i, 5] + " " + _data[_i, 6] + "</div></li>" +
                         "  <li class='table-col' id='tab2-table-content-repay-col4' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 7] + "</span>- " + _data[_i, 8] + _groupNum + "</div></li>" +
                         "  <li class='table-col' id='tab2-table-content-repay-col5' onclick=" + _callFunc + "><div>" + (!String.IsNullOrEmpty(_data[_i, 17]) ? _data[_i, 17] : "-") + "</div></li>" +
                         "  <li class='table-col' id='tab2-table-content-repay-col6' onclick=" + _callFunc + "><div>" + (!String.IsNullOrEmpty(_overpayment) ? _overpayment : "-") + "</div></li>" +
                         "  <li class='table-col' id='tab2-table-content-repay-col7' onclick=" + _callFunc + ">" +
                         "      <div class='icon-status-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus[1] + "'></li>" +
                         "              <li class='" + _iconStatus[2] + "'></li>" +
                         "              <li class='" + _iconStatus[3] + "'></li>" +
                         "              <li class='" + _iconStatus[4] + "'></li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "</ul>";
            }

            _html += "</div>";
      
            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "transrepaycontract", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }

    public static string ListSearchRepayStatusCalInterestOverpayment(string _cp2id) {
        string _result = String.Empty;

        _result = eCPDB.ChkRepayStatusCalInterestOverpayment(_cp2id);

        return _result;
    }
}
