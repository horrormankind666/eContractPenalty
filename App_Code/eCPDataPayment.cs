/*
Description         : สำหรับการทำรายการชำระหนี้ตามรายการแจ้ง
Date Created        : ๐๖/๐๘/๒๕๕๕
Last Date Modified  : ๑๐/๐๗/๒๕๖๕
Create By           : Yutthaphoom Tawana
*/

using System;
using System.Collections;
using System.Web;

public class eCPDataPayment {
    public static string ChkBalance() {
        string _html = String.Empty;

        _html += "<div class='form-content' id='chk-balance'>" +
                 "  <div>" +
                 "      <div class='content-left' id='chk-balance-label'>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>เงินต้นคงเหลือยกมา</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>ดอกเบี้ยครั้งนี้</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>ดอกเบี้ยค้างจ่ายยกมา</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>ยอดเงินคงเหลือต้องชำระ</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินที่ได้รับชำระครั้งนี้</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>รับชำระเงินต้นครั้งนี้</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>รับชำระดอกเบี้ยครั้งนี้</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>เงินต้นคงเหลือยกไป</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>ดอกเบี้ยค้างจ่ายครั้งนี้</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>รวมยอดดอกเบี้ยค้างจ่ายยกไป</div></div>" +
                 "      </div>" +
                 "      <div class='content-left' id='chk-balance-input'>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span class='chk-balance-val' id='chk-balance-capital'></span><span class='chk-balance-unit' id='chk-balance-capital-unit'> บาท</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span class='chk-balance-val' id='chk-balance-total-interest'></span><span class='chk-balance-unit' id='chk-balance-total-interest-unit'> บาท</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span class='chk-balance-val' id='chk-balance-total-accrued-interest'></span><span class='chk-balance-unit' id='chk-balance-total-accrued-interest-unit'> บาท</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span class='chk-balance-val' id='chk-balance-total-payment'></span><span class='chk-balance-unit' id='chk-balance-total-payment-unit'> บาท</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span class='chk-balance-val' id='chk-balance-pay'></span><span class='chk-balance-unit' id='chk-balance-pay-unit'> บาท</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span class='chk-balance-val' id='chk-balance-pay-capital'></span><span class='chk-balance-unit' id='chk-balance-pay-capital-unit'> บาท</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span class='chk-balance-val' id='chk-balance-pay-interest'></span><span class='chk-balance-unit' id='chk-balance-pay-interest-unit'> บาท</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span class='chk-balance-val' id='chk-balance-remain-capital'></span><span class='chk-balance-unit' id='chk-balance-remain-capital-unit'> บาท</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span class='chk-balance-val' id='chk-balance-accrued-interest'></span><span class='chk-balance-unit' id='chk-balance-accrued-interest-unit'> บาท</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span class='chk-balance-val' id='chk-balance-remain-accrued-interest'></span><span class='chk-balance-unit' id='chk-balance-remain-accrued-interest-unit'> บาท</span></div></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string AddDetailPayChannel(string _payChannel) {
        string _html = String.Empty;

        _html += "<div class='form-content' id='add-detail-pay-channel'>" +
                 "  <div>" +
                 "      <div class='content-left'>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>ช่องทางหรือวิธีการชำระหนี้</div></div>" +
                 "      </div>" +
                 "      <div class='content-left'>" +
                 "          <div class='form-input-style'><div class='form-input-content'><span>" + eCPUtil._payChannel[int.Parse(_payChannel) - 1] + "</span></div></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>";
        /*
        if (_payChannel.Equals("1")) {
            _html += "<div id='pay-channel-1-receipt-no'>" +
                     "  <div class='form-label-discription-style'><div class='form-label-style'>เลขที่ใบเสร็จ</div></div>" +
                     "  <div class='form-input-style'><div class='form-input-content'><input class='inputbox' type='text' id='receipt-no' onblur=Trim('receipt-no'); value='' style='width:200px' /></div></div>" +
                     "</div>" +
                     "<div class='clear'></div>" +
                     "<div id='pay-channel-1-receipt-book-no'>" +
                     "  <div class='form-label-discription-style'><div class='form-label-style'>เล่มที่</div></div>" +
                     "  <div class='form-input-style'><div class='form-input-content'><input class='inputbox' type='text' id='receipt-book-no' onblur=Trim('receipt-book-no'); value='' style='width:200px' /></div></div>" +
                     "</div>" +
                     "<div class='clear'></div>" +
                     "<div id='pay-channel-1-receipt-date'>" +
                     "  <div class='form-label-discription-style'><div class='form-label-style'>วันที่บนใบเสร็จ</div></div>" +
                     "  <div class='form-input-style'><div class='form-input-content'><input class='inputbox calendar' type='text' id='receipt-date' readonly value='' /></div></div>" +
                     "</div>" + 
                     "<div class='clear'></div>";
        }
        */

        if (_payChannel.Equals("2")) {
            _html += "<div id='pay-channel-2-cheque-no'>" +
                     "  <div class='form-label-discription-style'><div class='form-label-style'>เลขที่เช็ค</div></div>" +
                     "  <div class='form-input-style'><div class='form-input-content'><input class='inputbox' type='text' id='cheque-no' onblur=Trim('cheque-no'); value='' style='width:200px' /></div></div>" +
                     "</div>" +
                     "<div class='clear'></div>" +
                     "<div id='pay-channel-2-cheque-bank'>" +
                     "  <div class='form-label-discription-style'><div class='form-label-style'>ธนาคาร</div></div>" +
                     "  <div class='form-input-style'><div class='form-input-content'><input class='inputbox' type='text' id='cheque-bank' onblur=Trim('cheque-bank'); value='' style='width:200px' /></div></div>" +
                     "</div>" +
                     "<div class='clear'></div>" +
                     "<div id='pay-channel-2-cheque-bank-branch'>" +
                     "  <div class='form-label-discription-style'><div class='form-label-style'>สาขา</div></div>" +
                     "  <div class='form-input-style'><div class='form-input-content'><input class='inputbox' type='text' id='cheque-bank-branch' onblur=Trim('cheque-bank-branch'); value='' style='width:200px' /></div></div>" +
                     "</div>" +
                     "<div class='clear'></div>" +
                     "<div id='pay-channel-2-cheque-date'>" +
                     "  <div class='form-label-discription-style'><div class='form-label-style'>วันที่บนเช็ค</div></div>" +
                     "  <div class='form-input-style'><div class='form-input-content'><input class='inputbox calendar' type='text' id='cheque-date' readonly value='' /></div></div>" +
                     "</div>" +
                     "<div class='clear'></div>";
        }

        if (_payChannel.Equals("3")) {
            _html += "<div id='pay-channel-3-cash-bank'>" +
                     "  <div class='form-label-discription-style'><div class='form-label-style'>ธนาคาร</div></div>" +
                     " <div class='form-input-style'><div class='form-input-content'><input class='inputbox' type='text' id='cash-bank' onblur=Trim('cash-bank'); value='' style='width:200px' /></div></div>" +
                     "</div>" +
                     "<div class='clear'></div>" +
                     "<div id='pay-channel-3-cash-bank-branch'>" +
                     "  <div class='form-label-discription-style'><div class='form-label-style'>สาขา</div></div>" +
                     "  <div class='form-input-style'><div class='form-input-content'><input class='inputbox' type='text' id='cash-bank-branch' onblur=Trim('cash-bank-branch'); value='' style='width:200px' /></div></div>" +
                     "</div>" +
                     "<div class='clear'></div>" +
                     "<div id='pay-channel-3-cash-bank-account'>" +
                     "  <div class='form-label-discription-style'><div class='form-label-style'>ชื่อบัญชี</div></div>" +
                     "  <div class='form-input-style'><div class='form-input-content'><input class='inputbox' type='text' id='cash-bank-account' onblur=Trim('cash-bank-account'); value='' style='width:200px' /></div></div>" +
                     "</div>" +
                     "<div class='clear'></div>" +
                     "<div id='pay-channel-3-cash-bank-account-no'>" +
                     "  <div class='form-label-discription-style'><div class='form-label-style'>เลขที่บัญชี</div></div>" +
                     "  <div class='form-input-style'><div class='form-input-content'><input class='inputbox' type='text' id='cash-bank-account-no' onblur=Trim('cash-bank-account-no'); value='' style='width:200px' /></div></div>" +
                     "</div>" +
                     "<div class='clear'></div>" +
                     "<div id='pay-channel-3-cash-bank-date'>" +
                     "  <div class='form-label-discription-style'><div class='form-label-style'>วันที่บนใบนำฝาก</div></div>" +
                     "  <div class='form-input-style'><div class='form-input-content'><input class='inputbox calendar' type='text' id='cash-bank-date' readonly value='' /></div></div>" +
                     "</div>" +
                     "<div class='clear'></div>";
        }
                  
        _html += "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateAddDetailPayChannel()'>ตกลง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmAddDetailPayChannel()>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    private static string CalInterestYesNo(string _typeInterest) {
        string _html = String.Empty;

        _html += "<div id='cal-interest-yesno'>" +
                 "  <input type='hidden' id='type-interest-hidden' value='" + _typeInterest + "'>" +
                 "  <div class='content-left' id='cal-interest-yesno-label'>" +
                 "      <div class='form-label-discription-style'><div class='form-label-style'>คิด / ไม่คิดดอกเบี้ย</div></div>" +
                 "  </div>" +
                 "  <div class='content-left' id='cal-interest-yesno-input'>" +
                 "      <div class='form-input-style'>" +
                 "          <div class='form-input-content'>" +
                 "              <div>" +
                 "                  <div class='content-left' id='cal-interest-yes-input'><input class='radio' type='radio' checked name='cal-interest-yesno' value='Y' /></div>" +
                 "                  <div class='content-left' id='cal-interest-yes-label'>" + eCPUtil._calInterestYesNo[0, 0] + "</div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "              <div>" +
                 "                  <div class='content-left' id='cal-interest-no-input'><input class='radio' type='radio' name='cal-interest-yesno' value='N' /></div>" +
                 "                  <div class='content-left' id='cal-interest-no-label'>" + eCPUtil._calInterestYesNo[1, 0] + "</div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>" +
                 "<div class='clear'></div>";

        return _html;
    }

    private static string FrmOverpayment(
        string _formatPayment,
        string _statusPayment,
        string _repayDateStart,
        string _repayDateEnd,
        double _overpaymentYear,
        double _overpaymentDay
    ) {
        string _html = String.Empty;

        _html += "<div class='box3'></div>";

        if (_statusPayment.Equals("1")) {
            if ((_overpaymentYear + _overpaymentDay) > 0) {
                _html += CalInterestYesNo("overpayment") +
                         "<div id='cal-interest-overpayment'>" +
                         "  <div>" +
                         "      <div class='content-left' id='payment-label'>" +
                         "          <div class='form-label-discription-style'><div class='form-label-style'>การดำเนินการชำระหนี้</div></div>" +
                         "      </div>" +
                         "      <div class='content-left' id='payment-input'>" +
                         "          <div class='form-input-style'>" +
                         "              <div class='form-input-content'>" +
                         "                  <div>ผู้ผิดสัญญาชำระหนี้ภายใน <span>" + eCPUtil.PAYMENT_AT_LEAST + "</span> วัน</div>" +
                         "                  <div class='form-input-content-line'>ตั้งแต่วันที่ <span>" + Util.LongDateTH(_repayDateStart) + "</span> ถึงวันที่ <span>" + Util.LongDateTH(_repayDateEnd) + "</span></div>" +
                         "              </div>" +
                         "          </div>" +
                         "      </div>" +
                         "  </div>" +
                         "  <div class='clear'></div>" +                            
                         "  <input type='hidden' id='overpayment-date-start-old' value=''>" +
                         "  <input type='hidden' id='overpayment-date-end-old' value=''>" +
                         "  <input type='hidden' id='overpayment-interest-old' value=''>" +
                         "  <div>" +
                         "      <div class='form-label-discription-style'>" +
                         "          <div id='cal-interest-overpayment-label'>" +
                         "              <div class='form-label-style'>คำนวณดอกเบี้ยผิดนัดชำระ</div>" +
                         "              <div class='form-discription-style'>" +
                         "                  <div class='form-discription-line1-style'>กรุณากดปุ่มคำนวณเพื่อทำการคำนวณระยะเวลาผิดนัดชำระ</div>" +
                         "                  <div class='form-discription-line2-style'>และคำนวณดอกเบี้ยผิดนัดชำระ</div>" +
                         "              </div>" +
                         "          </div>" +
                         "      </div>" +
                         "      <div class='form-input-style'>" +
                         "          <div class='form-input-content' id='cal-interest-overpayment-input'>" +
                         "              <div class='button-style2'>" +
                         "                  <ul>" +
                         "                      <li><a href='javascript:void(0)' onclick='CalInterestOverpayment()'>คำนวณ</a></li>" +
                         "                  </ul>" +
                         "              </div>" +
                         "              <div>" +
                         "                  <div class='content-left' id='overpayment-date-start-label'>ระยะเวลาผิดนัดชำระตั้งแต่วันที่</div>" +
                         "                  <div class='content-left' id='overpayment-date-start-input'><input class='inputbox calendar' type='text' id='overpayment-date-start' readonly value='' /></div>" +
                         "                  <div class='content-left' id='overpayment-date-end-label'>ถึงวันที่</div>" +
                         "                  <div class='content-left' id='overpayment-date-end-input'><input class='inputbox calendar' type='text' id='overpayment-date-end' readonly value='' /></div>" +
                         "              </div>" +
                         "              <div class='clear'></div>" +
                         "              <div>" +
                         "                  <div class='content-left' id='overpayment-year-day-label'>นับระยะเวลาการผิดนัดชำระได้</div>" +
                         "                  <div class='content-left' id='overpayment-year-input'><input class='inputbox textbox-numeric' type='text' id='overpayment-year' value='' style='width:32px' /></div>" +
                         "                  <div class='content-left' id='overpayment-year-unit-label'>ปี</div>" +
                         "                  <div class='content-left' id='overpayment-day-input'><input class='inputbox textbox-numeric' type='text' id='overpayment-day' value='' style='width:39px' /></div>" +
                         "                  <div class='content-left' id='overpayment-day-unit-label'>วัน</div>" +
                         "              </div>" +
                         "              <div class='clear'></div>" +
                         "              <div>" +
                         "                  <div class='content-left' id='overpayment-interest-label'>ดอกเบี้ยผิดนัดชำระอัตราร้อยละ</div>" +
                         "                  <div class='content-left' id='overpayment-interest-input'><input class='inputbox textbox-numeric' type='text' id='overpayment-interest' onblur=Trim('overpayment-interest');AddCommas('overpayment-interest',2) onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:100px' /></div>" +
                         "                  <div class='content-left' id='overpayment-interest-unit-label'>ต่อปี</div>" +
                         "              </div>" +
                         "              <div class='clear'></div>" +
                         "              <div>" +
                         "                  <div class='content-left' id='total-interest-overpayment-label'>ยอดดอกเบี้ยจากการผิดนัดชำระ</div>" +
                         "                  <div class='content-left' id='total-interest-overpayment-input'><input class='inputbox textbox-numeric' type='text' id='total-interest-overpayment' value='' style='width:100px' /></div>" +
                         "                  <div class='content-left' id='total-interest-overpayment-unit-label'>บาท</div>" +
                         "              </div>" +
                         "              <div class='clear'></div>" +
                         "          </div>" +
                         "      </div>" +
                         "  </div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
            }
        }

        if (_statusPayment.Equals("2") || (_formatPayment.Equals("2") && (_overpaymentYear + _overpaymentDay) <= 0)) {
            _html += CalInterestYesNo("payrepay") +
                     "<div id='cal-interest-payrepay'>" +
                     "  <input type='hidden' id='pay-repay-date-end-old' value=''>" +
                     "  <input type='hidden' id='pay-repay-interest-old' value=''>" +
                     "  <div>" +
                     "      <div class='form-label-discription-style'>" +
                     "          <div id='cal-interest-pay-repay-label'>" +
                     "              <div class='form-label-style'>คำนวณดอกเบี้ยผ่อนชำระ</div>" +
                     "              <div class='form-discription-style'>" +
                     "                  <div class='form-discription-line1-style'>กรุณากดปุ่มคำนวณเพื่อทำการคำนวณระยะเวลา</div>" +
                     "                  <div class='form-discription-line2-style'>คิดดอกเบี้ย และคำนวณดอกเบี้ยผ่อนชำระ</div>" +
                     "              </div>" +
                     "          </div>" +
                     "      </div>" +
                     "      <div class='form-input-style'>" +
                     "          <div class='form-input-content' id='cal-interest-pay-repay-input'>" +
                     "              <div class='button-style2'>" +
                     "                  <ul>" +
                     "                      <li><a href='javascript:void(0)' onclick='CalInterestPayRepay()'>คำนวณ</a></li>" +
                     "                  </ul>" +
                     "              </div>" +
                     "              <div>" +
                     "                  <div class='content-left' id='pay-repay-date-start-label'>ระยะเวลาคิดดอกเบี้ยตั้งแต่วันที่</div>" +
                     "                  <div class='content-left' id='pay-repay-date-start-input'><input class='inputbox calendar' type='text' id='pay-repay-date-start' readonly value='' /></div>" +
                     "                  <div class='content-left' id='pay-repay-date-end-label'>ถึงวันที่</div>" +
                     "                  <div class='content-left' id='pay-repay-date-end-input'><input class='inputbox calendar' type='text' id='pay-repay-date-end' readonly value='' /></div>" +
                     "              </div>" +
                     "              <div class='clear'></div>" +
                     "              <div>" +
                     "                  <div class='content-left' id='pay-repay-year-day-label'>นับระยะเวลาคิดดอกเบี้ยได้</div>" +
                     "                  <div class='content-left' id='pay-repay-year-input'><input class='inputbox textbox-numeric' type='text' id='pay-repay-year' value='' style='width:32px' /></div>" +
                     "                  <div class='content-left' id='pay-repay-year-unit-label'>ปี</div>" +
                     "                  <div class='content-left' id='pay-repay-day-input'><input class='inputbox textbox-numeric' type='text' id='pay-repay-day' value='' style='width:39px' /></div>" +
                     "                  <div class='content-left' id='pay-repay-day-unit-label'>วัน</div>" +
                     "              </div>" +
                     "              <div class='clear'></div>" +
                     "              <div>" +
                     "                  <div class='content-left' id='pay-repay-interest-label'>ดอกเบี้ยผ่อนชำระอัตราร้อยละ</div>" +
                     "                  <div class='content-left' id='pay-repay-interest-input'><input class='inputbox textbox-numeric' type='text' id='pay-repay-interest' onblur=Trim('pay-repay-interest');AddCommas('pay-repay-interest',2) onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:100px' /></div>" +
                     "                  <div class='content-left' id='pay-repay-interest-unit-label'>ต่อปี</div>" +
                     "              </div>" +
                     "              <div class='clear'></div>" +
                     "              <div>" +
                     "                  <div class='content-left' id='total-interest-pay-repay-label'>ยอดดอกเบี้ยผ่อนชำระ</div>" +
                     "                  <div class='content-left' id='total-interest-pay-repay-input'><input class='inputbox textbox-numeric' type='text' id='total-interest-pay-repay' value='' style='width:100px' /></div>" +
                     "                  <div class='content-left' id='total-interest-pay-repay-unit-label'>บาท</div>" +
                     "              </div>" +
                     "              <div class='clear'></div>" +
                     "          </div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='clear'></div>" +
                     "</div>";
        }

        return _html;
    }

    private static string FrmPayment(string formatPayment) {
        string html = String.Empty;
        string formatPaymentName = String.Empty;
        string lawyerFullname = String.Empty;
        string lawyerPhoneNumber = String.Empty;
        string lawyerMobileNumber = String.Empty;
        string lawyerEmail = String.Empty;

        switch (formatPayment) {
            case "1":
                formatPaymentName = "full";
                break;
            case "2":
                formatPaymentName = "pay";
                break;
        }

        string userid = eCPUtil.GetUserID();
        string[,] user = eCPDB.ListDetailCPTabUser(userid, "", "", "");
        lawyerFullname = user[0, 3];
        lawyerPhoneNumber = user[0, 6];
        lawyerMobileNumber = user[0, 7];
        lawyerEmail = user[0, 8];

        html += "<div class='box3'></div>" +
                "<div id='format-payment-" + formatPaymentName + "-repay'>" +
                "   <input type='hidden' id='pay-channel-index-hidden' value=''>" +
                "   <input type='hidden' id='cheque-no-hidden' value=''>" +
                "   <input type='hidden' id='cheque-bank-hidden' value=''>" +
                "   <input type='hidden' id='cheque-bank-branch-hidden' value=''>" +
                "   <input type='hidden' id='cheque-date-hidden' value=''>" +
                "   <input type='hidden' id='cash-bank-hidden' value=''>" +
                "   <input type='hidden' id='cash-bank-branch-hidden' value=''>" +
                "   <input type='hidden' id='cash-bank-account-hidden' value=''>" +
                "   <input type='hidden' id='cash-bank-account-no-hidden' value=''>" +
                "   <input type='hidden' id='cash-bank-date-hidden' value=''>" +
                "   <input type='hidden' id='lawyer-fullname-hidden' value='" + lawyerFullname + "' />" +
                "   <input type='hidden' id='lawyer-phonenumber-hidden' value='" + lawyerPhoneNumber + "' />" +
                "   <input type='hidden' id='lawyer-mobilenumber-hidden' value='" + lawyerMobileNumber + "' />" +
                "   <input type='hidden' id='lawyer-email-hidden' value='" + lawyerEmail + "' />" +
                "   <div class='form-label-discription-style'>" +
                "       <div id='detail-payment-label'>" +
                "           <div class='form-label-style'>รายละเอียดการชำระหนี้</div>" +
                "           <div class='form-discription-style'>" +
                "               <div class='form-discription-line1-style'>กรุณาใส่รายละเอียดการชำระหนี้</div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='form-input-style'>" +
                "       <div class='form-input-content' id='detail-payment-input'>" +
                "           <div>" +
                "               <div class='content-left' id='capital-label'>จำนวนเงินต้นคงเหลือที่ยกมา</div>" +
                "               <div class='content-left' id='capital-input'><input class='inputbox textbox-numeric' type='text' id='capital' value='' style='width:110px' /></div>" +
                "               <div class='content-left' id='capital-unit-label'>บาท</div>" +
                "           </div>" +
                "           <div class='clear'></div>";

        if (formatPayment.Equals("1")) {
            html += "       <div>" +
                    "           <div class='content-left' id='total-payment-label'>จำนวนเงินที่ชำระ</div>" +
                    "           <div class='content-left' id='total-payment-input'><input class='inputbox textbox-numeric' type='text' id='total-payment' onblur=Trim('total-payment');AddCommas('total-payment',2); onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:110px' /></div>" +
                    "           <div class='content-left' id='total-payment-unit-label'>บาท</div>" +
                    "       </div>" +
                    "       <div class='clear'></div>" +
                    "       <div>" +
                    "           <div class='content-left' id='total-interest-overpayment-label'>จำนวนเงินที่หักชำระดอกเบี้ยผิดนัด</div>" +
                    "           <div class='content-left' id='total-interest-overpayment-input'><input class='inputbox textbox-numeric' type='text' id='total-interest-overpayment' onblur=Trim('total-interest-overpayment');AddCommas('total-interest-overpayment',2); onkeyup='ExtractNumber(this,2,false);doCalculatePayment()' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:110px' /></div>" +
                    "           <div class='content-left' id='total-interest-overpayment-unit-label'>บาท</div>" +
                    "       </div>" +
                    "       <div class='clear'></div>" +
                    "       <div>" +
                    "           <div class='content-left' id='pay-label'>จำนวนเงินที่หักชำระเงินต้น</div>" +
                    "           <div class='content-left' id='pay-input'><input class='inputbox textbox-numeric' type='text' id='pay' onblur=Trim('pay');AddCommas('pay',2); onkeyup='ExtractNumber(this,2,false);doCalculatePayment()' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:110px' /></div>" +
                    "           <div class='content-left' id='pay-unit-label'>บาท</div>" +
                    "       </div>" +
                    "       <div class='clear'></div>" +
                    "       <div>" +
                    "           <div class='content-left' id='overpay-label'>จำนวนเงินส่วนที่ชำระเกิน</div>" +
                    "           <div class='content-left' id='overpay-input'><input class='inputbox textbox-numeric' type='text' id='overpay' onblur=Trim('overpay');AddCommas('overpay',2); onkeyup='ExtractNumber(this,2,false);doCalculatePayment()' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:110px' /></div>" +
                    "           <div class='content-left' id='overpay-unit-label'>บาท</div>" +
                    "       </div>" +
                    "       <div class='clear'></div>" +
                    "       <div>" +
                    "           <div class='content-left' id='remain-capital-label'>จำนวนเงินต้นคงเหลือ</div>" +
                    "           <div class='content-left' id='remain-capital-input'><input class='inputbox textbox-numeric' type='text' id='remain-capital' value='' style='width:110px' /></div>" +
                    "           <div class='content-left' id='remain-capital-unit-label'>บาท</div>" +
                    "       </div>" +
                    "       <div class='clear'></div>";
        }

        if (formatPayment.Equals("2")) {
            html += "       <div>" +
                    "           <div class='content-left' id='total-accrued-interest-label'>จำนวนดอกเบี้ยคงค้างที่ยกมา</div>" +
                    "           <div class='content-left' id='total-accrued-interest-input'><input class='inputbox textbox-numeric' type='text' id='total-accrued-interest' value='' style='width:110px' /></div>" +
                    "           <div class='content-left' id='total-accrued-interest-unit-label'>บาท</div>" +
                    "       </div>" +
                    "       <div class='clear'></div>" +
                    "       <div>" +
                    "           <div class='content-left' id='pay-label'>จำนวนเงินที่ชำระในงวดนี้</div>" +
                    "           <div class='content-left' id='pay-input'><input class='inputbox textbox-numeric' type='text' id='pay' onblur=Trim('pay');AddCommas('pay',2); onkeyup='ExtractNumber(this,2,false);doCalculatePayment()' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:110px' /></div>" +
                    "           <div class='content-left' id='pay-unit-label'>บาท</div>" +
                    "       </div>" +
                    "       <div class='clear'></div>" +
                    "       <div id='interest-payment'>" +
                    "           <div class='content-left' id='interest-payment-label'>จำนวนเงินที่หักชำระดอกเบี้ย</div>" +
                    "           <div class='content-left' id='interest-payment-input'>" +
                    "               <div>" +
                    "                   <div class='content-left label' id='total-interest-overpayment-before-label'>ดอกเบี้ยผิดนัดก่อนผ่อนชำระ ( ถ้ามี )</div>" +
                    "                   <div class='content-left input' id='total-interest-overpayment-before-input'><input class='inputbox textbox-numeric' type='text' id='total-interest-overpayment-before' onblur=Trim('total-interest-overpayment-before');AddCommas('total-interest-overpayment-before',2); onkeyup='ExtractNumber(this,2,false);doCalculatePayment()' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:110px' /></div>" +
                    "                   <div class='content-left unit' id='total-interest-overpayment-before-unit-label'>บาท</div>" +
                    "               </div>" +
                    "               <div class='clear'></div>" +
                    "               <div>" +
                    "                   <div class='content-left label' id='total-interest-pay-repay-label'>ดอกเบี้ยผ่อนชำระในงวดนี้</div>" +
                    "                   <div class='content-left input' id='total-interest-pay-repay-input'><input class='inputbox textbox-numeric' type='text' id='total-interest-pay-repay' onblur=Trim('total-interest-pay-repay');AddCommas('total-interest-pay-repay',2); onkeyup='ExtractNumber(this,2,false);doCalculatePayment()' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:110px' /></div>" +
                    "                   <div class='content-left unit' id='total-interest-pay-repay-unit-label'>บาท</div>" +
                    "               </div>" +
                    "               <div class='clear'></div>" +
                    "               <div>" +
                    "                   <div class='content-left label' id='total-interest-overpayment-label'>ดอกเบี้ยผิดนัดผ่อนชำระ ( ถ้ามี )</div>" +
                    "                   <div class='content-left input' id='total-interest-overpayment-input'><input class='inputbox textbox-numeric' type='text' id='total-interest-overpayment' onblur=Trim('total-interest-overpayment');AddCommas('total-interest-overpayment',2); onkeyup='ExtractNumber(this,2,false);doCalculatePayment()' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:110px' /></div>" +
                    "                   <div class='content-left unit' id='total-interest-overpayment-unit-label'>บาท</div>" +
                    "               </div>" +
                    "               <div class='clear'></div>" +
                    "           </div>" +
                    "       </div>" +
                    "       <div class='clear'></div>" +
                    "       <div style='margin-top: 10px'>" +
                    "           <div class='content-left' id='pay-capital-label'>จำนวนเงินที่หักชำระเงินต้นในงวดนี้</div>" +
                    "           <div class='content-left' id='pay-capital-input'><input class='inputbox textbox-numeric' type='text' id='pay-capital' value='' style='width:110px' /></div>" +
                    "           <div class='content-left' id='pay-capital-unit-label'>บาท</div>" +
                    "       </div>" +
                    "       <div class='clear'></div>" +
                    "       <div>" +
                    "           <div class='content-left' id='remain-capital-label'>จำนวนเงินต้นคงเหลือ</div>" +
                    "           <div class='content-left' id='remain-capital-input'><input class='inputbox textbox-numeric' type='text' id='remain-capital' value='' style='width:110px' /></div>" +
                    "           <div class='content-left' id='remain-capital-unit-label'>บาท</div>" +
                    "       </div>" +
                    "       <div class='clear'></div>" +
                    "       <div>" +
                    "           <div class='content-left' id='remain-accrued-interest-label'>จำนวนดอกเบี้ยคงค้าง ( ถ้ามี )</div>" +
                    "           <div class='content-left' id='remain-accrued-interest-input'><input class='inputbox textbox-numeric' type='text' id='remain-accrued-interest' value='' style='width:110px' /></div>" +
                    "           <div class='content-left' id='remain-accrued-interest-unit-label'>บาท</div>" +
                    "       </div>" +
                    "       <div class='clear'></div>";
        }

        /*
        "           <div>" +
        "               <div class='content-left' id='capital-label'>จำนวนเงินต้นคงเหลือยกมา</div>" +
        "               <div class='content-left' id='capital-input'><input class='inputbox textbox-numeric' type='text' id='capital' value='' style='width:100px' /></div>" +
        "               <div class='content-left' id='capital-unit-label'>บาท</div>" +
        "           </div>" +
        "           <div class='clear'></div>" +
        "           <div>" +
        "               <div class='content-left' id='total-interest-label'>จำนวนเงินดอกเบี้ยครั้งนี้</div>" +
        "               <div class='content-left' id='total-interest-input'><input class='inputbox textbox-numeric' type='text' id='total-interest' value='' style='width:100px' /></div>" +
        "               <div class='content-left' id='total-interest-unit-label'>บาท</div>" +
        "           </div>" +
        "           <div class='clear'></div>" +
        "           <div>" +
        "               <div class='content-left' id='total-accrued-interest-label'>จำนวนเงินดอกเบี้ยค้างจ่ายยกมา</div>" +
        "               <div class='content-left' id='total-accrued-interest-input'><input class='inputbox textbox-numeric' type='text' id='total-accrued-interest' value='' style='width:100px' /></div>" +
        "               <div class='content-left' id='total-accrued-interest-unit-label'>บาท</div>" +
        "           </div>" +
        "           <div class='clear'></div>" +
        "           <div>" +
        "               <div class='content-left' id='total-payment-label'>รวมยอดเงินที่ต้องชำระครั้งนี้</div>" +
        "               <div class='content-left' id='total-payment-input'><input class='inputbox textbox-numeric' type='text' id='total-payment' value='' style='width:100px' /></div>" +
        "               <div class='content-left' id='total-payment-unit-label'>บาท</div>" +
        "           </div>" +
        "           <div class='clear'></div>" +
        "           <div>" +
        "               <div class='content-left' id='pay-label'>จำนวนเงินที่ได้รับชำระ</div>" +
        "               <div class='content-left' id='pay-input'><input class='inputbox textbox-numeric' type='text' id='pay' onblur=Trim('pay');AddCommas('pay',2); onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:100px' /></div>" +
        "               <div class='content-left' id='pay-unit-label'>บาท</div>" +
        "           </div>" +
        "           <div class='clear'></div>" +
        */

        html += "           <div>" +
                "               <div class='content-left' id='payment-date-label'>วันเดือนปีรับชำระหนี้</div>" +
                "               <div class='content-left' id='payment-date-input'><input class='inputbox calendar' type='text' id='payment-date' readonly value='' /></div>" +
                "           </div>" +
                "           <div class='clear'></div>" +
                "           <div>" +
                "               <div class='content-left' id='channel-label'>จ่ายชำระด้วย</div>" +
                "               <div class='content-left' id='channel-input'>" +
                "                   <div>" +
                "                       <div class='content-left' id='pay-channel1-input'><input class='radio' type='radio' name='pay-channel' value='1' /></div>" +
                "                       <div class='content-left' id='pay-channel1-label'>" + eCPUtil._payChannel[0] + "</div>" +
                "                   </div>" +
                "                   <div class='clear'></div>" +
                "                   <div>" +
                "                       <div class='content-left' id='pay-channel2-input'><input class='radio' type='radio' name='pay-channel' value='2' /></div>" +
                "                       <div class='content-left' id='pay-channel2-label'>" + eCPUtil._payChannel[1] + "</div>" +
                "                   </div>" +
                "                   <div class='clear'></div>" +
                "                   <div>" +
                "                       <div class='content-left' id='pay-channel3-input'><input class='radio' type='radio' name='pay-channel' value='3' /></div>" +
                "                       <div class='content-left' id='pay-channel3-label'>" + eCPUtil._payChannel[2] + "</div>" +
                "                   </div>" +
                "                   <div class='clear'></div>" +
                "               </div>" +
                "           </div>" +
                "           <div class='clear'></div>" +
                "       </div>" +
                "   </div>" +
                "</div>" +
                "<div class='clear'></div>" +
                "<div>" +
                "   <div class='form-label-discription-style clear-bottom'>" +
                "       <div id='detail-receipt-label'>" +
                "           <div class='form-label-style'>รายละเอียดใบเสร็จ</div>" +
                "           <div class='form-discription-style'>" +
                "               <div class='form-discription-line1-style'>กรุณาใส่รายละเอียดใบเสร็จ</div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='form-input-style clear-bottom'>" +
                "       <div class='form-input-content' id='detail-receipt-input'>" +
                "           <div>" +
                "               <div class='content-left' id='receipt-no-label'>เลขที่ใบเสร็จ</div>" +
                "               <div class='content-left' id='receipt-no-input'><input class='inputbox' type='text' id='receipt-no' onblur=Trim('receipt-no'); value='' style='width:190px' /></div>" +
                "           </div>" +
                "           <div class='clear'></div>" +
                "           <div>" +
                "               <div class='content-left' id='receipt-book-no-label'>เล่มที่</div>" +
                "               <div class='content-left' id='receipt-book-no-input'><input class='inputbox' type='text' id='receipt-book-no' onblur=Trim('receipt-book-no'); value='' style='width:190px' /></div>" +
                "           </div>" +
                "           <div class='clear'></div>" +
                "           <div>" +
                "               <div class='content-left' id='receipt-date-label'>ลงวันที่</div>" +
                "               <div class='content-left' id='receipt-date-input'><input class='inputbox calendar' type='text' id='receipt-date' readonly value='' /></div>" +
                "           </div>" +
                "           <div class='clear'></div>" +
                "           <div>" +
                "               <div class='content-left' id='receipt-send-no-label'>เลขที่ใบนำส่ง</div>" +
                "               <div class='content-left' id='receipt-send-no-input'><input class='inputbox' type='text' id='receipt-send-no' onblur=Trim('receipt-send-no'); value='' style='width:190px' /></div>" +
                "           </div>" +
                "           <div class='clear'></div>" +
                "           <div>" +
                "               <div class='content-left' id='receipt-fund-label'>เข้ากองทุน</div>" +
                "               <div class='content-left' id='receipt-fund-input'><input class='inputbox' type='text' id='receipt-fund' onblur=Trim('receipt-fund'); value='' style='width:280px' /></div>" +
                "           </div>" +
                "           <div class='clear'></div>" +
                "           <div>" +
                "               <div class='content-left' id='receipt-copy-label'>สำเนาใบเสร็จรับเงิน</div>" +
                "               <div class='content-left' id='receipt-copy-input'>" +
                "                   <input type='hidden' id='receipt-copy' value=''>" +
                "                   <div class='uploadfile-container'>" +
                "                       <img class='preloading-inline hidden' src='Image/PreloadingInline.gif' />" +
                "                       <form class='uploadfile-form' method='post' enctype='multipart/form-data'>" +
                "                           <div class='uploadfile-button browse'>" +
                "                               <span><a class='text-underline' href='javascript:void(0)'>เลือกเอกสาร<input type='file' id='receipt-copy-file' /></a></span>" +
                "                           </div>" +
                "                       </form>" +
                "                       <div class='form-discription-style'>" +
                "                           <div class='form-discription-line1-style'>( เฉพาะไฟล์นามสกุล .pdf )</div>" +
                "                       </div>" +
                "                   </div>" +
                "                   <div id='receipt-copy-preview-container'>" +
                "                       <canvas class='hidden' id='receipt-copy-preview'></canvas>" +
                "                       <div class='hidden' id='receipt-copy-nopreview'>ไม่สามารถแสดงตัวอย่างเอกสาร</div>" +
                "                       <div class='hidden' id='receipt-copy-linkpreview'>" +
                "                           <a class='text-underline' href='javascript:void(0)'>ดูเอกสาร</a>" +
                "                           <form id='download-receiptcopy-form' action='FileProcess.aspx' method='POST' target='download-receiptcopy'>" +
                "                               <input type='hidden' id='action' name='action' value='download' />" +
                "                               <input type='hidden' id='filename' name='filename' value='ReceiptCopy' />" +
                "                               <input type='hidden' id='file' name='file' value='' />" +
                "                           </form>" +
                "                           <iframe class='export-target' id='download-receiptcopy' name='download-receiptcopy'></iframe>" +
                "                       </div>" +
                "                   </div>" +
                "               </div>" +
                "           </div>" +
                "           <div class='clear'></div>" +
                "       </div>" +
                "   </div>" +
                "</div>" +
                "<div class='clear'></div>";

        return html;
    }

    private static string AddCpTransPaymentPayRepay(string[,] data) {
        string _html = String.Empty;
        string totalPenaltyDefault = data[0, 4];
        string statusPayment = data[0, 7];
        string remainDefault = data[0, 27];
        string capitalDefault = statusPayment.Equals("1") ? totalPenaltyDefault : remainDefault;
        string totalAccruedInterestDefault = statusPayment.Equals("1") ? "0.00" : data[0, 26];
        string totalInterestOverpaymentBeforeDefault = "0.00";
        string totalInterestPayRepayDefault = "0.00";
        string totalInterestOverpaymentDefault = "0.00";        
        string totalPaymentDefault = (double.Parse(capitalDefault) + double.Parse(totalAccruedInterestDefault)).ToString();

        /*
        string _totalPenaltyDefault = _data[0, 5];
        */
        /*
        string totalPenaltyDefault = data[0, 4];
        string statusPayment = data[0, 7];
        string replyDateDefault = data[0, 24];
        string paymentDateDefault = data[0, 25];
        string remainDefault = data[0, 27];
        string[] repayDate;
        string repayDateStartDefault = String.Empty;
        string repayDateEndDefault = String.Empty;
        string overpaymentDateStartDefault = String.Empty;
        string overpaymentDateEndDefault = String.Empty;
        string overpaymentInterestDefault = String.Empty;
        string payRepayDateStartDefault = String.Empty;
        string payRepayDateEndDefault = String.Empty;
        string payRepayInterestDefault = String.Empty;
        string capitalDefault = statusPayment.Equals("1") ? totalPenaltyDefault : remainDefault;
        string totalInterestDefault = String.Empty;
        string totalAccruedInterestDefault = statusPayment.Equals("1") ? "0.00" : data[0, 26];
        string totalPaymentDefault = String.Empty;
        double[] overpayment;
        double overpaymentYear = 0;
        double overpaymentDay = 0;
        double[] payRepay;
        double payRepayYear = 0;
        double payRepayDay = 0;
        string[] contractInterest;
               
        IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
        DateTime dateA;
        DateTime dateB;

        if (statusPayment.Equals("1")) {
            repayDate = eCPUtil.RepayDate(replyDateDefault);
            repayDateStartDefault = !String.IsNullOrEmpty(repayDate[0]) ? repayDate[0] : String.Empty;
            repayDateEndDefault = !String.IsNullOrEmpty(repayDate[1]) ? repayDate[1] : String.Empty;
            overpaymentDateStartDefault = !String.IsNullOrEmpty(repayDate[2]) ? repayDate[2] : String.Empty;
            overpaymentDateEndDefault = Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd"));
            
            dateA = DateTime.Parse(overpaymentDateStartDefault, provider);
            dateB = DateTime.Parse(overpaymentDateEndDefault, provider);

            overpayment = Util.CalcDate(dateA, dateB);
            overpaymentYear = overpayment[4];
            overpaymentDay = overpayment[5];
        }

        if (statusPayment.Equals("2") || (overpaymentYear + overpaymentDay) <= 0) {
            payRepayDateStartDefault = !String.IsNullOrEmpty(paymentDateDefault) ? Util.ConvertDateTH(DateTime.Parse(paymentDateDefault, provider).AddDays(1).ToString()) : Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd"));
            payRepayDateEndDefault = Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd"));

            dateA = DateTime.Parse(payRepayDateStartDefault, provider);
            dateB = DateTime.Parse(payRepayDateEndDefault, provider);

            if (dateA > dateB) {
                payRepayDateEndDefault = Util.ConvertDateTH(dateA.ToString());
                dateB = dateA;
            }

            payRepay = Util.CalcDate(dateA, dateB);
            payRepayYear = payRepay[4];
            payRepayDay = payRepay[5];
        }
              
        contractInterest = eCPUtil.GetContractInterest();
        overpaymentInterestDefault = double.Parse(contractInterest[1]).ToString("#,##0.00");
        payRepayInterestDefault = double.Parse(contractInterest[0]).ToString("#,##0.00");
        paymentDateDefault = (overpaymentYear + overpaymentDay) > 0 ? overpaymentDateEndDefault : payRepayDateEndDefault;
        */
        _html += "<div class='form-content' id='add-cp-trans-payment'>" +
                 "  <div id='add-trans-payment'>" +
                 "      <input type='hidden' id='statuspayment-hidden' value='" + statusPayment + "' />" +
                 /*
                 "      <input type='hidden' id='repay-date-end-hidden' value='" + repayDateEndDefault + "' />" +
                 "      <input type='hidden' id='overpayment-hidden' value='" + (overpaymentYear + overpaymentDay) + "' />" +
                 "      <input type='hidden' id='overpayment-date-start-hidden' value='" + overpaymentDateStartDefault + "' />" +
                 "      <input type='hidden' id='overpayment-date-end-hidden' value='" + overpaymentDateEndDefault + "' />" +
                 "      <input type='hidden' id='overpayment-year-hidden' value='" + overpaymentYear + "' />" +
                 "      <input type='hidden' id='overpayment-day-hidden' value='" + overpaymentDay + "' />" +
                 "      <input type='hidden' id='overpayment-interest-hidden' value='" + overpaymentInterestDefault + "' />" +
                 "      <input type='hidden' id='pay-repay-date-start-hidden' value='" + payRepayDateStartDefault + "' />" +
                 "      <input type='hidden' id='pay-repay-date-end-hidden' value='" + payRepayDateEndDefault + "' />" +
                 "      <input type='hidden' id='pay-repay-year-hidden' value='" + payRepayYear + "' />" +
                 "      <input type='hidden' id='pay-repay-day-hidden' value='" + payRepayDay + "' />" +
                 "      <input type='hidden' id='pay-repay-interest-hidden' value='" + payRepayInterestDefault + "' />" +
                 "      <input type='hidden' id='subtotal-penalty-hidden' value='" + double.Parse(totalPenaltyDefault).ToString("#,##0.00") + "' />" +
                 */
                 "      <input type='hidden' id='capital-hidden' value='" + double.Parse(capitalDefault).ToString("#,##0.00") + "' />" +
                 "      <input type='hidden' id='total-payment-hidden' value='" + double.Parse(totalPaymentDefault).ToString("#,##0.00") + "' />" +
                 "      <input type='hidden' id='total-accrued-interest-hidden' value='" + double.Parse(totalAccruedInterestDefault).ToString("#,##0.00") + "' />" +
                 "      <input type='hidden' id='total-interest-overpayment-before-hidden' value='" + totalInterestOverpaymentBeforeDefault + "' />" +
                 "      <input type='hidden' id='total-interest-pay-repay-hidden' value='" + totalInterestPayRepayDefault + "' />" +
                 "      <input type='hidden' id='total-interest-overpayment-hidden' value='" + totalInterestOverpaymentDefault + "' />" +                                  
                 "      <input type='hidden' id='payment-date-hidden' value='" + Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd")) + "' />" +
                 /*
                 "      <input type='hidden' id='payment-date-hidden' value='" + _paymentDateDefault + "' />" +
                 */
                 "      <input type='hidden' id='pay-repay-least-hidden' value='" + eCPUtil.PAY_REPAY_LEAST.ToString("#,##0") + "' />" +

                        DetailPenaltyAndFormatPayment(data, false) +
                        /*
                        FrmOverpayment("2", _statusPayment, _repayDateStartDefault, _repayDateEndDefault, _overpaymentYear, _overpaymentDay) +
                        */
                        FrmPayment("2") +

                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 /*
                 "              <li id='button-style11'><a href='javascript:void(0)' onclick='ChkBalanceCPTransPaymentPayRepay()'>ตรวจสอบยอดคงเหลือ</a></li>" +
                 */
                 "              <li><a href='javascript:void(0)' onclick='ConfirmActionCPTransPaymentPayRepay()'>บันทึก</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmAddCPTransPayment()>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'add-cp-trans-payment')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    private static string AddCpTransPaymentFullRepay(string[,] data) {
        string html = String.Empty;
        string totalPenaltyDefault = data[0, 4];
        string statusPayment = data[0, 7];
        string capitalDefault = totalPenaltyDefault;
        string totalInterestOverpaymentDefault = "0.00";
        string totalPaymentDefault = capitalDefault;
        /*
        string _totalPenaltyDefault = _data[0, 5];
        */
        /*
        string totalPenaltyDefault = data[0, 4];
        string statusPayment = data[0, 7];
        string replyDateDefault = data[0, 24];
        string[] repayDate = eCPUtil.RepayDate(replyDateDefault);
        string repayDateStartDefault = !String.IsNullOrEmpty(repayDate[0]) ? repayDate[0] : String.Empty;
        string repayDateEndDefault = !String.IsNullOrEmpty(repayDate[1]) ? repayDate[1] : String.Empty;
        string overpaymentDateStartDefault = !String.IsNullOrEmpty(repayDate[2]) ? repayDate[2] : String.Empty;
        string overpaymentDateEndDefault = Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd"));
        string overpaymentInterestDefault = String.Empty;
        string capitalDefault = String.Empty;
        string totalInterestDefault = String.Empty;
        string totalAccruedInterestDefault = String.Empty;
        string totalPaymentDefault = String.Empty;        
        double[] overpayment;
        double overpaymentYear = 0;
        double overpaymentDay = 0;
        string[] contractInterest;        

        IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
        DateTime dateA = DateTime.Parse(overpaymentDateStartDefault, provider);
        DateTime dateB = DateTime.Parse(overpaymentDateEndDefault, provider);

        overpayment = Util.CalcDate(dateA, dateB);
        overpaymentYear = overpayment[4];
        overpaymentDay = overpayment[5];
        capitalDefault = totalPenaltyDefault;
        totalInterestDefault = (overpayment[4] + overpayment[5]) > 0 ? totalInterestDefault : "0.00";
        totalAccruedInterestDefault = "0.00";
        totalPaymentDefault = (overpayment[4] + overpayment[5]) > 0 ? totalPaymentDefault : (double.Parse(capitalDefault) + double.Parse(totalInterestDefault) + double.Parse(totalAccruedInterestDefault)).ToString("#,##0.00");
        contractInterest = eCPUtil.GetContractInterest();
        overpaymentInterestDefault = double.Parse(contractInterest[1]).ToString("#,##0.00");
        */

        html += "<div class='form-content' id='add-cp-trans-payment'>" +
                "   <div id='add-trans-payment'>" +
                "       <input type='hidden' id='statuspayment-hidden' value='" + statusPayment + "' />" +                
                /*
                "       <input type='hidden' id='repay-date-end-hidden' value='" + repayDateEndDefault + "' />" +
                "       <input type='hidden' id='overpayment-hidden' value='" + (overpaymentYear + overpaymentDay) + "' />" +
                "       <input type='hidden' id='overpayment-date-start-hidden' value='" + overpaymentDateStartDefault + "' />" +
                "       <input type='hidden' id='overpayment-date-end-hidden' value='" + overpaymentDateEndDefault + "' />" +
                "       <input type='hidden' id='overpayment-year-hidden' value='" + overpaymentYear + "' />" +
                "       <input type='hidden' id='overpayment-day-hidden' value='" + overpaymentDay + "' />" +
                "       <input type='hidden' id='overpayment-interest-hidden' value='" + overpaymentInterestDefault + "' />" +
                "       <input type='hidden' id='subtotal-penalty-hidden' value='" + double.Parse(totalPenaltyDefault).ToString("#,##0.00") + "' />" +
                */
                "       <input type='hidden' id='capital-hidden' value='" + double.Parse(capitalDefault).ToString("#,##0.00") + "' />" +
                "       <input type='hidden' id='total-payment-hidden' value='" + double.Parse(totalPaymentDefault).ToString("#,##0.00") + "' />" +
                "       <input type='hidden' id='total-interest-overpayment-hidden' value='" + totalInterestOverpaymentDefault + "' />" +
                /*
                "       <input type='hidden' id='total-accrued-interest-hidden' value='" + totalAccruedInterestDefault + "' />" +
                */                
                "       <input type='hidden' id='payment-date-hidden' value='" + Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd")) + "' />" +
                        
                        DetailPenaltyAndFormatPayment(data, false) +
                        /*
                        FrmOverpayment("1", _statusPayment, _repayDateStartDefault, _repayDateEndDefault, _overpaymentYear, _overpaymentDay) +
                        */
                        FrmPayment("1") +
                 
                "   </div>" +
                "   <div class='button'>" +
                "       <div class='button-style1'>" +
                "           <ul>" +
                /*
                "               <li id='button-style11'><a href='javascript:void(0)' onclick='ChkBalanceCPTransPaymentFullRepay()'>ตรวจสอบยอดคงเหลือ</a></li>" +
                */
                "               <li><a href='javascript:void(0)' onclick='ConfirmActionCPTransPaymentFullRepay()'>บันทึก</a></li>" +
                "               <li><a href='javascript:void(0)' onclick=ResetFrmAddCPTransPayment()>ล้าง</a></li>" +
                "               <li><a href='javascript:void(0)' onclick=CloseFrm(false,'add-cp-trans-payment')>ปิด</a></li>" +
                "           </ul>" +
                "       </div>" +
                "   </div>" +
                "</div>";

        return html;
    }

    public static string DetailTransPayment(string _cp2id) {
        string _html = String.Empty;
        string[,] _data;

        _data = eCPDB.ListDetailTransPayment(_cp2id);

        if (_data.GetLength(0) > 0) {
            string _formatPayment = _data[0, 43];
            string _subtotalPenaltyDefault = double.Parse(_data[0, 44]).ToString("#,##0.00");
            string _totalPenaltyDefault = double.Parse(_data[0, 45]).ToString("#,##0.00");
            string _calInterestYesNoDefault = !String.IsNullOrEmpty(_data[0, 2]) ? eCPUtil._calInterestYesNo[Util.FindIndexArray2D(1, eCPUtil._calInterestYesNo, _data[0, 2]) - 1, 0] : String.Empty;
            string _overpaymentTotalInterestBeforeDefault = !String.IsNullOrEmpty(_data[0, 46]) ? double.Parse(_data[0, 46]).ToString("#,##0.00") : "0.00";
            string _overpaymentDateStartDefault = !String.IsNullOrEmpty(_data[0, 3]) ? Util.LongDateTH(_data[0, 3]) : String.Empty;
            string _overpaymentDateEndDefault = !String.IsNullOrEmpty(_data[0, 4]) ? Util.LongDateTH(_data[0, 4]) : String.Empty;
            string _overpaymentYearDefault = !String.IsNullOrEmpty(_data[0, 5]) ? double.Parse(_data[0, 5]).ToString("#,##0") : String.Empty;
            string _overpaymentDayDefault = !String.IsNullOrEmpty(_data[0, 6]) ? double.Parse(_data[0, 6]).ToString("#,##0") : String.Empty;
            string _overpaymentInterestDefault = !String.IsNullOrEmpty(_data[0, 7]) ? double.Parse(_data[0, 7]).ToString("#,##0.00") : String.Empty;
            string _overpaymentTotalInterestDefault = !String.IsNullOrEmpty(_data[0, 8]) ? double.Parse(_data[0, 8]).ToString("#,##0.00") : "0.00";
            string _payRepayDateStartDefault = !String.IsNullOrEmpty(_data[0, 9]) ? Util.LongDateTH(_data[0, 9]) : String.Empty;
            string _payRepayDateEndDefault = !String.IsNullOrEmpty(_data[0, 10]) ? Util.LongDateTH(_data[0, 10]) : String.Empty;
            string _payRepayYearDefault = !String.IsNullOrEmpty(_data[0, 11]) ? double.Parse(_data[0, 11]).ToString("#,##0") : String.Empty;
            string _payRepayDayDefault = !String.IsNullOrEmpty(_data[0, 12]) ? double.Parse(_data[0, 12]).ToString("#,##0") : String.Empty;
            string _payRepayInterestDefault = !String.IsNullOrEmpty(_data[0, 13]) ? double.Parse(_data[0, 13]).ToString("#,##0.00") : String.Empty;
            string _payRepayTotalInterestDefault = !String.IsNullOrEmpty(_data[0, 14]) ? double.Parse(_data[0, 14]).ToString("#,##0.00") : "0.00";
            string _dateTimePaymentDefault = Util.LongDateTH(_data[0, 15]);
            string _capitalDefault = double.Parse(_data[0, 16]).ToString("#,##0.00");
            string _interestDefault = double.Parse(_data[0, 17]).ToString("#,##0.00");
            string _totalAccruedInterestDefault = double.Parse(_data[0, 18]).ToString("#,##0.00");
            string _totalPaymentDefault = double.Parse(_data[0, 19]).ToString("#,##0.00");
            string _payCapitalDefault = double.Parse(_data[0, 20]).ToString("#,##0.00");
            string _payInterestDefault = double.Parse(_data[0, 21]).ToString("#,##0.00");
            string _totalPayDefault = double.Parse(_data[0, 22]).ToString("#,##0.00");
            string _overpayDefault = !String.IsNullOrEmpty(_data[0, 47]) ? double.Parse(_data[0, 47]).ToString("#,##0.00") : "0.00";
            string _remainCapitalDefault = double.Parse(_data[0, 23]).ToString("#,##0.00");
            string _accruedInterestDefault = double.Parse(_data[0, 24]).ToString("#,##0.00");
            string _remainAccruedInterestDefault = double.Parse(_data[0, 25]).ToString("#,##0.00");
            string _totalRemainDefault = double.Parse(_data[0, 26]).ToString("#,##0.00");
            string _channelDefault = _data[0, 27];
            string _receiptNoDefault = !String.IsNullOrEmpty(_data[0, 28]) ? _data[0, 28] : "-";
            string _receiptBookNoDefault = !String.IsNullOrEmpty(_data[0, 29]) ? _data[0, 29] : "-";
            string _receiptDateDefault = !String.IsNullOrEmpty(_data[0, 30]) ? Util.LongDateTH(_data[0, 30]) : "-";
            string _receiptSendNoDefault = !String.IsNullOrEmpty(_data[0, 31]) ? _data[0, 31] : "-";
            string _receiptFundDefault = !String.IsNullOrEmpty(_data[0, 32]) ? _data[0, 32] : "-";
            string _receiptCopyDefault = _data[0, 42];
            string _chequeNoDefault = !String.IsNullOrEmpty(_data[0, 33]) ? _data[0, 33] : "-";
            string _chequeBankDefault = !String.IsNullOrEmpty(_data[0, 34]) ? _data[0, 34] : "-";
            string _chequeBankBranchDefault = !String.IsNullOrEmpty(_data[0, 35]) ? _data[0, 35] : "-";
            string _chequeDateDefault = !String.IsNullOrEmpty(_data[0, 36]) ? Util.LongDateTH(_data[0, 36]) : "-";
            string _cashBankDefault = !String.IsNullOrEmpty(_data[0, 37]) ? _data[0, 37] : "-";
            string _cashBankBranchDefault = !String.IsNullOrEmpty(_data[0, 38]) ? _data[0, 38] : "-";
            string _cashBankAccountDefault = !String.IsNullOrEmpty(_data[0, 39]) ? _data[0, 39] : "-";
            string _cashBankAccountNoDefault = !String.IsNullOrEmpty(_data[0, 40]) ? _data[0, 40] : "-";
            string _cashBankDateDefault = !String.IsNullOrEmpty(_data[0, 41]) ? Util.LongDateTH(_data[0, 41]) : "-";
            string _lawyerFullnameDefault = _data[0, 48];
            string _lawyerPhoneNumberDefault = _data[0, 49];
            string _lawyerMobileNumberDefault = _data[0, 50];
            string _lawyerEmailDefault = _data[0, 51];
            string _lawyerDefault = String.Empty;

            ArrayList _lawyerPhoneNumber = new ArrayList();

            if (!String.IsNullOrEmpty(_lawyerPhoneNumberDefault))
                _lawyerPhoneNumber.Add(_lawyerPhoneNumberDefault);

            if (!String.IsNullOrEmpty(_lawyerMobileNumberDefault))
                _lawyerPhoneNumber.Add(_lawyerMobileNumberDefault);

            if (!String.IsNullOrEmpty(_lawyerFullnameDefault))
                _lawyerDefault += "คุณ<span>" + _lawyerFullnameDefault + "</span>";

            if (_lawyerPhoneNumber.Count > 0)
                _lawyerDefault += " ( <span>" + String.Join(", ", _lawyerPhoneNumber.ToArray()) + "</span> )";

            if (!String.IsNullOrEmpty(_lawyerEmailDefault))
                _lawyerDefault += " อีเมล์ <span>" + _lawyerEmailDefault + "</span>";

            _html += "<div class='form-content' id='detail-trans-payment'>" +
                     "  <div id='box-detail-trans-payment'>" +
                     "      <div id='list-detail-trans-payment'>" +
                     "          <div>" +
                     "              <div class='content-left'>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>ประจำงวดที่</div></div>" +
                     "              </div>" +
                     "              <div class='content-left'>" +
                     "                  <div class='form-input-style'><div class='form-input-content'><span id='period'></span></div></div>" +
                     "              </div>" +
                     "          </div>" +
                     "          <div class='clear'></div>";
            /*
            if (!String.IsNullOrEmpty(_calInterestYesNoDefault)) {
                _html += "      <div>" +
                         "          <div class='content-left'>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>คิด / ไม่คิดดอกเบี้ย</div></div>" +
                         "          </div>" +
                         "          <div class='content-left'>" +
                         "              <div class='form-input-style'><div class='form-input-content'><span>" + _calInterestYesNoDefault + "</span></div></div>" +
                         "          </div>" +
                         "      </div>" +
                         "      <div class='clear'></div>";
            }

            if (!String.IsNullOrEmpty(_overpaymentDateStartDefault) && !String.IsNullOrEmpty(_overpaymentDateEndDefault) &&
                !String.IsNullOrEmpty(_overpaymentYearDefault) && !String.IsNullOrEmpty(_overpaymentDayDefault) &&
                !String.IsNullOrEmpty(_overpaymentInterestDefault) && !String.IsNullOrEmpty(_overpaymentTotalInterestDefault)) {
                _html += "      <div id='overpayment'>" +
                         "          <div class='content-left'>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>ดอกเบี้ยจากการผิดนัดชำระ</div></div>" +
                         "          </div>" +
                         "          <div class='content-left'>" +
                         "              <div class='form-input-style'>" +
                         "                  <div class='form-input-content'>" +
                         "                      <div>ระยะเวลาผิดนัดชำระตั้งแต่วันที่ <span>" + _overpaymentDateStartDefault + "</span> ถึงวันที่ <span>" + _overpaymentDateEndDefault + "</span></div>" +
                         "                      <div class='form-input-content-line'>ระยะเวลาผิดนัดชำระ <span>" + (!_overpaymentYearDefault.Equals("0") ? _overpaymentYearDefault : "-") + "</span> ปี <span>" + (!_overpaymentDayDefault.Equals("0") ? _overpaymentDayDefault : "-") + "</span> วัน</div>" +
                         "                      <div class='form-input-content-line'>ดอกเบี้ยผิดนัดชำระอัตราร้อยละ <span>" + _overpaymentInterestDefault + "</span> บาท</div>" +
                         "                      <div class='form-input-content-line'>ยอดดอกเบี้ยจากการผิดนัดชำระ <span>" + _overpaymentTotalInterestDefault + "</span> บาท</div>" +                     
                         "                  </div>" +
                         "              </div>" +
                         "          </div>" +
                         "      </div>" +
                         "      <div class='clear'></div>";
            }

            if (!String.IsNullOrEmpty(_payRepayDateStartDefault) && !String.IsNullOrEmpty(_payRepayDateEndDefault) &&
                !String.IsNullOrEmpty(_payRepayYearDefault) && !String.IsNullOrEmpty(_payRepayDayDefault) &&
                !String.IsNullOrEmpty(_payRepayInterestDefault) && !String.IsNullOrEmpty(_payRepayTotalInterestDefault)) {
                _html += "      <div id='overpayment'>" +
                         "          <div class='content-left'>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>ดอกเบี้ยผ่อนชำระ</div></div>" +
                         "          </div>" +
                         "          <div class='content-left'>" +
                         "              <div class='form-input-style'>" +
                         "                  <div class='form-input-content'>" +
                         "                      <div>ระยะเวลาคิดดอกเบี้ยตั้งแต่วันที่ <span>" + _payRepayDateStartDefault + "</span> ถึงวันที่ <span>" + _payRepayDateEndDefault + "</span></div>" +
                         "                      <div class='form-input-content-line'>ระยะเวลาคิดดอกเบี้ย <span>" + (!_payRepayYearDefault.Equals("0") ? _payRepayYearDefault : "-") + "</span> ปี <span>" + (!_payRepayDayDefault.Equals("0") ? _payRepayDayDefault : "-") + "</span> วัน</div>" +
                         "                      <div class='form-input-content-line'>ดอกเบี้ยผ่อนชำระอัตราร้อยละ <span>" + _payRepayInterestDefault + "</span> บาท</div>" +
                         "                      <div class='form-input-content-line'>ยอดดอกเบี้ยผ่อนชำระ <span>" + _payRepayTotalInterestDefault + "</span> บาท</div>" +                     
                         "                  </div>" +
                         "              </div>" +
                         "          </div>" +
                         "      </div>" +
                         "      <div class='clear'></div>";
            }

            _html += "          <div>" +
                     "              <div class='content-left'>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>เงินต้นคงเหลือยกมา</div></div>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>ดอกเบี้ยครั้งนี้</div></div>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>ดอกเบี้ยค้างจ่ายยกมา</div></div>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>ยอดเงินคงเหลือที่ต้องชำระ</div></div>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินที่ได้รับชำระครั้งนี้</div></div>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>รับชำระเงินต้นครั้งนี้</div></div>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>รับชำระดอกเบี้ยครั้งนี้</div></div>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>เงินต้นคงเหลือยกไป</div></div>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>ดอกเบี้ยค้างจ่ายครั้งนี้</div></div>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>รวมยอดดอกเบี้ยค้างจ่ายยกไป</div></div>" +
                     "              </div>" +
                     "              <div class='content-left'>" +
                     "                  <div class='form-input-style'><div class='form-input-content'><span>" + _capitalDefault + "</span> บาท</div></div>" +
                     "                  <div class='form-input-style'><div class='form-input-content'><span>" + _interestDefault + "</span> บาท</div></div>" +
                     "                  <div class='form-input-style'><div class='form-input-content'><span>" + _totalAccruedInterestDefault + "</span> บาท</div></div>" +
                     "                  <div class='form-input-style'><div class='form-input-content'><span>" + _totalPaymentDefault + "</span> บาท</div></div>" +
                     "                  <div class='form-input-style'><div class='form-input-content'><span>" + _totalPayDefault + "</span> บาท</div></div>" +
                     "                  <div class='form-input-style'><div class='form-input-content'><span>" + _payCapitalDefault + "</span> บาท</div></div>" +
                     "                  <div class='form-input-style'><div class='form-input-content'><span>" + _payInterestDefault + "</span> บาท</div></div>" +
                     "                  <div class='form-input-style'><div class='form-input-content'><span>" + _remainCapitalDefault + "</span> บาท</div></div>" +
                     "                  <div class='form-input-style'><div class='form-input-content'><span>" + _accruedInterestDefault + "</span> บาท</div></div>" +
                     "                  <div class='form-input-style'><div class='form-input-content'><span>" + _remainAccruedInterestDefault + "</span> บาท</div></div>" +
                     "              </div>" +
                     "          </div>" +
                     "          <div class='clear'></div>" +                       
            */

            _html += "          <div>" +
                     "              <div class='content-left'>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินค่าปรับผิดสัญญา</div></div>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินต้นที่ต้องรับผิดชอบชดใช้</div></div>";

            if (_formatPayment.Equals("1")) {
                _html += "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินที่ชำระ</div></div>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินที่หักชำระดอกเบี้ยผิดนัด</div></div>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินที่หักชำระเงินต้น</div></div>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินส่วนที่ชำระเกิน</div></div>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินต้นคงเหลือ</div></div>";
            }

            if (_formatPayment.Equals("2")) {
                _html += "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินต้นคงเหลือที่ยกมา</div></div>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนดอกเบี้ยคงค้างที่ยกมา</div></div>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินที่ชำระในงวดนี้</div></div>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินที่หักชำระดอกเบี้ยผิดนัดก่อนผ่อนชำระ ( ถ้ามี )</div></div>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินที่หักชำระดอกเบี้ยผ่อนชำระในงวดนี้</div></div>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินที่หักชำระดอกเบี้ยผิดนัดผ่อนชำระ ( ถ้ามี )</div></div>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินที่หักชำระเงินต้นในงวดนี้</div></div>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนเงินต้นคงเหลือจากการหักชำระเงินงวดนี้</div></div>" +
                         "              <div class='form-label-discription-style'><div class='form-label-style'>จำนวนดอกเบี้ยคงค้าง ( ถ้ามี )</div></div>";
            }

            _html += "              </div>" +
                     "              <div class='content-left'>" +
                     "                  <div class='form-input-style'><div class='form-input-content'><span>" + _subtotalPenaltyDefault + "</span> บาท</div></div>" +
                     "                  <div class='form-input-style'><div class='form-input-content'><span>" + _totalPenaltyDefault + "</span> บาท</div></div>";

            if (_formatPayment.Equals("1")) {
                _html += "              <div class='form-input-style'><div class='form-input-content'><span>" + _totalPayDefault + "</span> บาท</div></div>" +
                         "              <div class='form-input-style'><div class='form-input-content'><span>" + _overpaymentTotalInterestDefault + "</span> บาท</div></div>" +
                         "              <div class='form-input-style'><div class='form-input-content'><span>" + _payCapitalDefault + "</span> บาท</div></div>" +
                         "              <div class='form-input-style'><div class='form-input-content'><span>" + _overpayDefault + "</span> บาท</div></div>" +
                         "              <div class='form-input-style'><div class='form-input-content'><span>" + _remainCapitalDefault + "</span> บาท</div></div>";

            }

            if (_formatPayment.Equals("2")) {
                _html += "              <div class='form-input-style'><div class='form-input-content'><span>" + _capitalDefault + "</span> บาท</div></div>" +
                         "              <div class='form-input-style'><div class='form-input-content'><span>" + _totalAccruedInterestDefault + "</span> บาท</div></div>" +
                         "              <div class='form-input-style'><div class='form-input-content'><span>" + _totalPayDefault + "</span> บาท</div></div>" +
                         "              <div class='form-input-style'><div class='form-input-content'><span>" + _overpaymentTotalInterestBeforeDefault + "</span> บาท</div></div>" +
                         "              <div class='form-input-style'><div class='form-input-content'><span>" + _payRepayTotalInterestDefault + "</span> บาท</div></div>" +
                         "              <div class='form-input-style'><div class='form-input-content'><span>" + _overpaymentTotalInterestDefault + "</span> บาท</div></div>" +
                         "              <div class='form-input-style'><div class='form-input-content'><span>" + _payCapitalDefault + "</span> บาท</div></div>" +
                         "              <div class='form-input-style'><div class='form-input-content'><span>" + _remainCapitalDefault + "</span> บาท</div></div>" +
                         "              <div class='form-input-style'><div class='form-input-content'><span>" + _remainAccruedInterestDefault + "</span> บาท</div></div>";
            }

            _html += "              </div>" +
                     "          </div>" +
                     "          <div class='clear'></div>" +
                     "          <div id='pay-channel" + _channelDefault + "'>" +
                     "              <div class='content-left'>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>จ่ายชำระด้วย</div></div>" +
                     "              </div>" +
                     "              <div class='content-left'>" +
                     "                  <div class='form-input-style'>" +
                     "                      <div class='form-input-content'>" +
                     "                          <div><span>" + eCPUtil._payChannel[int.Parse(_channelDefault) - 1] + "</span></div>";

            if (_channelDefault.Equals("2")) {
                _html += "                      <div class='form-input-content-line'>เลขที่เช็ค <span>" + _chequeNoDefault + "</span></div>" +
                         "                      <div class='form-input-content-line'>ธนาคาร <span>" + _chequeBankDefault + "</span></div>" +
                         "                      <div class='form-input-content-line'>สาขา <span>" + _chequeBankBranchDefault + "</span></div>" +
                         "                      <div class='form-input-content-line'>วันที่บนเช็ค <span>" + _chequeDateDefault + "</span></div>";
            }

            if (_channelDefault.Equals("3")) {
                _html += "                      <div class='form-input-content-line'>ธนาคาร <span>" + _cashBankDefault + "</span></div>" +
                         "                      <div class='form-input-content-line'>สาขา <span>" + _cashBankBranchDefault + "</span></div>" +
                         "                      <div class='form-input-content-line'>ชื่อบัญชี <span>" + _cashBankAccountDefault + "</span></div>" +
                         "                      <div class='form-input-content-line'>เลขที่บัญชี <span>" + _cashBankAccountNoDefault + "</span></div>" +
                         "                      <div class='form-input-content-line'>วันที่บนใบนำฝาก <span>" + _cashBankDateDefault + "</span></div>";
            }

            _html += "                      </div>" +
                     "                  </div>" +
                     "              </div>" +
                     "          </div>" +
                     "          <div class='clear'></div>" +
                     "          <div id='detail-receipt'>" +
                     "              <div class='content-left'>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>รายละเอียดใบเสร็จ</div></div>" +
                     "              </div>" +
                     "              <div class='content-left'>" +
                     "                  <div class='form-input-style'>" +
                     "                      <div class='form-input-content'>" +
                     "                          <div>เลขที่ใบเสร็จ <span>" + _receiptNoDefault + "</span></div>" +
                     "                          <div class='form-input-content-line'>เล่มที่ <span>" + _receiptBookNoDefault + "</span></div>" +
                     "                          <div class='form-input-content-line'>ลงวันที่ <span>" + _receiptDateDefault + "</span></div>" +
                     "                          <div class='form-input-content-line'>เลขที่ใบนำส่ง <span>" + _receiptSendNoDefault + "</span></div>" +
                     "                          <div class='form-input-content-line'>เข้ากองทุน <span>" + _receiptFundDefault + "</span></div>" +
                     "                          <div class='form-input-content-line'>สำเนาใบเสร็จรับเงิน <span>" + (!String.IsNullOrEmpty(_receiptCopyDefault) ? "<a class='text-underline' href='javascript:void(0)' onclick=DownloadReceiptCopy('" + _receiptCopyDefault + "')>ดาว์นโหลดสำเนาใบเสร็จรับเงิน</a>" : "-") + "</span></div>";

            if (!String.IsNullOrEmpty(_receiptCopyDefault)) {
                _html += "                      <form id='download-receiptcopy-form' action='FileProcess.aspx' method='POST' target='download-receiptcopy'>" +
                         "                          <input type='hidden' id='action' name='action' value='download' />" +
                         "                          <input type='hidden' id='file' name='file' value='' />" +
                         "                      </form>" +
                         "                      <iframe class='export-target' id='download-receiptcopy' name='download-receiptcopy'></iframe>";
            }

            _html += "                      </div>" +
                     "                  </div>" +
                     "              </div>" +
                     "          </div>" +
                     "          <div class='clear'></div>" +
                     "          <div id='lawyer'>" +
                     "              <div class='content-left'>" +
                     "                  <div class='form-label-discription-style'><div class='form-label-style'>นิติกรผู้รับผิดชอบ</div></div>" +
                     "              </div>" +
                     "              <div class='content-left'>" +
                     "                  <div class='form-input-style'><div class='form-input-content'>" + (!String.IsNullOrEmpty(_lawyerDefault) ? _lawyerDefault : "<span>-</span>") + "</div></div>" +
                     "              </div>" +
                     "          </div>" +
                     "          <div class='clear'></div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='button'>" +
                     "      <div class='button-style1 period-not-last'>" +
                     "          <ul>" +
                     "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                     "          </ul>" +
                     "      </div>" +
                     "      <div class='button-style1 period-last'>" +
                     "          <ul>" +
                     "              <li><a href='javascript:void(0)' onclick=doConfirmActionCPTransPayment('del','" + _data[0, 0] + "')>ลบ</a></li>" +
                     "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                     "          </ul>" +
                     "      </div>" +
                     "  </div>" +
                     "</div>";
        }

        return _html;
    }

    public static string ListTransPayment(string[,] data) {
        int i;
        string html = String.Empty;
        string highlight = String.Empty;
        string callFunc = String.Empty;
        double totalInterest = 0;
        double totalPayCapital = 0;
        double totalPayInterest = 0;
        double totalPay = 0;
        double totalPayment = 0;

        html += "<div class='table-content'>";

        for (i = 0; i < data.GetLength(0); i++) {
            highlight = (i % 2) == 0 ? "highlight1" : "highlight2";
            callFunc = "ShowDetailTransPayment('" + data[i, 1] + "','" + double.Parse(data[i, 0]).ToString("#,##0") + "')";
            totalInterest = (totalInterest + double.Parse(data[i, 5]));
            totalPayCapital = (totalPayCapital + double.Parse(data[i, 8]));
            totalPayInterest = (totalPayInterest + double.Parse(data[i, 9]));
            totalPay = (totalPay + double.Parse(data[i, 10]));
            totalPayment = (totalPayment + double.Parse(data[i, 7]));
            html += "<ul class='table-row-content " + highlight + " detail-trans-payment' id='detail-trans-payment" + data[i, 1] + "'>" +
                    "   <li id='table-content-trans-payment-col1' onclick=" + callFunc + "><div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-col2' onclick=" + callFunc + "><div>" + double.Parse(data[i, 4]).ToString("#,##0.00") + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-col3' onclick=" + callFunc + "><div>" + double.Parse(data[i, 5]).ToString("#,##0.00") + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-col4' onclick=" + callFunc + "><div>" + double.Parse(data[i, 8]).ToString("#,##0.00") + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-col5' onclick=" + callFunc + "><div>" + double.Parse(data[i, 9]).ToString("#,##0.00") + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-col6' onclick=" + callFunc + "><div>" + double.Parse(data[i, 7]).ToString("#,##0.00") + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-col7' onclick=" + callFunc + "><div>" + double.Parse(data[i, 11]).ToString("#,##0.00") + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-col8' onclick=" + callFunc + "><div>" + double.Parse(data[i, 6]).ToString("#,##0.00") + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-col9' onclick=" + callFunc + "><div>" + double.Parse(data[i, 12]).ToString("#,##0.00") + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-col10' onclick=" + callFunc + "><div>" + data[i, 3] + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-col11' onclick=" + callFunc + "><div>" + eCPUtil._payChannel[int.Parse(data[i, 14]) - 1] + "</div></li>" +
                    "</ul>";
        }

        if (data.GetLength(0) > 0) {
            html += "<ul class='table-row-content table-row-total' id='detail-trans-payment-total'>" +
                    "   <li id='table-content-trans-payment-total-col1'><div>รวม</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-total-col2'></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-total-col3'><div>" + totalInterest.ToString("#,##0.00") + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-total-col4'><div>" + totalPayCapital.ToString("#,##0.00") + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-total-col5'><div>" + totalPayInterest.ToString("#,##0.00") + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-total-col6'><div>" + totalPay.ToString("#,##0.00") + "</div></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-total-col7'></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-total-col8'></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-total-col9'></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-total-col10'></li>" +
                    "   <li class='table-col' id='table-content-trans-payment-total-col11'></li>" +
                    "</ul>";
        }
        
        html += "</div>";

        return html;
    }

    private static string DetailCpTransPayment(string[,] data) {
        string html = String.Empty;
        string cp2id = data[0, 1];
        string statusRepay = data[0, 6];
        string statusPayment = data[0, 7];
        string formatPayment = data[0, 8];
        string statusPaymentRecord = data[0, 33];
        string statusPaymentrecordLawyerDefault = data[0, 34];
        string statusPaymentrecordLawyerFullname = String.Empty;
        string statusPaymentrecordLawyerPhoneNumber = String.Empty;
        string statusPaymentrecordLawyerMobileNumber = String.Empty;
        string statusPaymentrecordLawyerEmail = String.Empty;
        string[,] data1, data2;
        int recordCount;

        string userid = eCPUtil.GetUserID();
        data1 = eCPDB.ListDetailCPTabUser(userid, "", "", "");
        statusPaymentrecordLawyerFullname = data1[0, 3];
        statusPaymentrecordLawyerPhoneNumber = data1[0, 6];
        statusPaymentrecordLawyerMobileNumber = data1[0, 7];
        statusPaymentrecordLawyerEmail = data1[0, 8];
        
        data2 = eCPDB.ListTransPayment(cp2id, "", "");
        recordCount = data2.GetLength(0);

        html += "<div class='form-content' id='detail-cp-trans-payment'>" +
                "   <input type='hidden' id='statuspayment-hidden' value='" + statusPayment + "' />" +
                "   <input type='hidden' id='statuspaymentrecord-hidden' value='" + statusPaymentRecord + "' />" +
                "   <input type='hidden' id='statuspaymentrecord-lawyer-hidden' value='" + (!String.IsNullOrEmpty(statusPaymentrecordLawyerDefault) ? statusPaymentrecordLawyerDefault : statusPaymentrecordLawyerFullname) + "' />" +
                "   <input type='hidden' id='statuspaymentrecord-lawyer-fullname-hidden' value='" + statusPaymentrecordLawyerFullname + "' />" +
                "   <input type='hidden' id='statuspaymentrecord-lawyer-phonenumber-hidden' value='" + statusPaymentrecordLawyerPhoneNumber + "' />" +
                "   <input type='hidden' id='statuspaymentrecord-lawyer-mobilenumber-hidden' value='" + statusPaymentrecordLawyerMobileNumber + "' />" +
                "   <input type='hidden' id='statuspaymentrecord-lawyer-email-hidden' value='" + statusPaymentrecordLawyerEmail + "' />" +
                    DetailPenaltyAndFormatPayment(data, true) +
                "   <div id='list-cp-trans-payment'>" +
                "       <div class='tab-line'></div>" +
                "       <div class='content-data-tab-content'>" +
                "           <div class='content-left'><div class='content-data-tab-content-msg'>ตารางการชำระหนี้" + "</div></div>" +
                "           <div class='content-right'><div class='content-data-tab-content-msg'>ค้นหาพบ " + recordCount + " รายการ</div></div>" +
                "       </div>" +
                "       <div class='clear'></div>" +
                "       <div class='tab-line'></div>" +
                "       <div class='box3'>" +
                "           <div class='table-head'>" +
                "               <ul>" +                            
                "                   <li id='table-head-trans-payment-col1'><div class='table-head-line1'>งวดที่</div></li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col2'><div class='table-head-line1'>เงินต้น</div><div>( บาท )</div></li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col3'><div class='table-head-line1'>ดอกเบี้ย</div><div>( บาท )</div></li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col4'><div class='table-head-line1'>เงินต้นรับชำระ</div><div>( บาท )</div></li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col5'><div class='table-head-line1'>ดอกเบี้ยรับชำระ</div><div>( บาท )</div></li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col6'><div class='table-head-line1'>ยอดเงินรับชำระ</div><div>( บาท )</div></li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col7'><div class='table-head-line1'>เงินต้นคงเหลือ</div><div>( บาท )</div></li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col8'><div class='table-head-line1'>ดอกเบี้ยคงเหลือ</div><div>( บาท )</div></li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col9'><div class='table-head-line1'>ดอกเบี้ยค้างจ่าย</div><div>( บาท )</div></li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col10'><div class='table-head-line1'>วันเดือนปี</div><div>ที่รับชำระหนี้</div></li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col11'><div class='table-head-line1'>จ่ายชำระด้วย</div></li>" +
                "               </ul>" +
                "           </div>" +
                "           <div class='clear'></div>" +                
                "       </div>" +
                "       <div id='box-list-trans-payment'><div id='list-trans-payment'>" + ListTransPayment(data2) + "</div></div>" +
                "   </div>" +
                "</div>";

        return html;
    }

    private static string DetailPenaltyAndFormatPayment(
        string[,] _data,
        bool _showStatusPaymentRecord
    ) {
        string _html = String.Empty;
        string _cp2id = _data[0, 1];
        string _totalPayScholarshipDefault = _data[0, 3];
        string _subtotalPenaltyDefault = _data[0, 4];
        string _totalPenaltyDefault = _data[0, 5];
        string _statusPayment = _data[0, 7];

        _html += "<div class='form-content' id='detail-cp-trans-payment'>" +
                 "  <div>" +
                 "      <div class='content-left' id='cal-contract-penalty-label'>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>ยอดเงินทุนการศึกษาที่ชดใช้</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>ยอดเงินค่าปรับผิดสัญญา</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>ยอดเงินต้นที่ต้องรับผิดชอบชดใช้</div></div>" +
                 "      </div>" +
                 "      <div class='content-left' id='cal-contract-penalty-input'>" +
                 "          <div class='form-input-style'><div class='form-input-content'><span>" + double.Parse(_totalPayScholarshipDefault).ToString("#,##0.00") + "</span> บาท</div></div>" +
                 "          <div class='form-input-style'><div class='form-input-content'><span>" + double.Parse(_subtotalPenaltyDefault).ToString("#,##0.00") + "</span> บาท</div></div>" +
                 "          <div class='form-input-style'><div class='form-input-content'><span>" + double.Parse(_totalPenaltyDefault).ToString("#,##0.00") + "</span> บาท</div></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>" +
                 "  <div>" +
                 "      <div class='content-left' id='format-payment-label'>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>รูปแบบการชำระหนี้</div></div>" +
                 "      </div>" +
                 "      <div class='content-left' id='format-payment-input'>" +
                 "          <div class='form-input-style'><div class='form-input-content'><span></span></div></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>" +
                 "  <div>" +
                 "      <div class='content-left' id='status-payment-label'>" +
                 "          <div class='form-label-discription-style" + (!_statusPayment.Equals("3") && _showStatusPaymentRecord.Equals(false) ? " clear-bottom" : String.Empty) + "'><div class='form-label-style'>สถานะการชำระหนี้</div></div>" +
                 "      </div>" +
                 "      <div class='content-left' id='status-payment-input'>" +
                 "          <div class='form-input-style" + (!_statusPayment.Equals("3") && _showStatusPaymentRecord.Equals(false) ? " clear-bottom" : String.Empty) + "'><div class='form-input-content'><span>" + eCPUtil._paymentStatus[int.Parse(_statusPayment) - 1] + "</span></div></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>";

        if (!_statusPayment.Equals("3") && _showStatusPaymentRecord.Equals(true)) {
            _html += "<div id='status-payment-record'>" +
                     "  <div class='content-left' id='status-payment-record-label'>" +
                     "      <div class='form-label-discription-style clear-bottom'>" +
                     "          <div class='form-label-style'>สถานะการบันทึกข้อมูลการชำระหนี้</div>" +
                     "          <div class='form-discription-style'>" +
                     "              <div class='form-discription-line1-style'>นิติกรผู้รับผิดชอบ คุณ<span id='status-payment-record-lawyer'></span></div>" +
                     "          </div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='content-left' id='status-payment-record-input'>" +
                     "      <div class='form-input-style clear-bottom'>" +
                     "          <div class='form-input-content'>" +
                     "              <div>" +
                     "                  <div class='content-left input'><input class='radio' type='radio' checked name='status-payment-record' value='" + eCPUtil._paymentRecordStatus[0, 1] + "' /></div>" +
                     "                  <div class='content-left label'>" + eCPUtil._paymentRecordStatus[0, 0] + " เนื่องจากอยู่ระหว่างการฟ้องร้องบังคับคดี</div>" +
                     "              </div>" +
                     "              <div class='clear'></div>" +
                     "              <div>" +
                     "                  <div class='content-left input'><input class='radio' type='radio' checked name='status-payment-record' value='" + eCPUtil._paymentRecordStatus[1, 1] + "' /></div>" +
                     "                  <div class='content-left label'>" + eCPUtil._paymentRecordStatus[1, 0] + "</div>" +
                     "              </div>" +
                     "              <div class='clear'></div>" +
                     "          </div>" +
                     "      </div>" +
                     "  </div>" +
                     "</div>" +
                     "<div class='clear'></div>";
        }

        if (_statusPayment.Equals("3")) {
            _html += "<div>" +
                     "  <div class='content-left' id='certificate-reimbursement-label'>" +
                     "      <div class='form-label-discription-style clear-bottom'><div class='form-label-style'>หนังสือรับรองการชดใช้เงิน</div></div>" +
                     "  </div>" +
                     "  <div class='content-left' id='certificate-reimbursement-input'>" +
                     "      <div class='form-input-style clear-bottom'>" +
                     "          <div class='form-input-content'>" +
                     "              <span><a class='text-underline' href='javascript:void(0)' onclick=PrintCertificateReimbursement('" + _cp2id + "')>พิมพ์หนังสือรับรองการชำระหนี้เรียบร้อย</a></span>" +
                     "          </div>" +
                     "      </div>" +
                     "  </div>" +
                     "</div>" +
                     "<div class='clear'></div>";
        }

        _html += "</div>" +
                 "<iframe class='export-target' id='export-target' name='export-target' src='#'></iframe>" +
                 "<form id='export-setvalue' method='post' target='export-target'>" +
                 "  <input id='export-send' name='export-send' value='' type='hidden' />" +
                 "  <input id='export-order' name='export-order' value='' type='hidden' />" +
                 "  <input id='export-type' name='export-type' value='' type='hidden' />" +
                 "</form>";

        return _html;
    }

    public static string AddDetailCPTransPayment(
        string _cp2id,
        string _action
    ) {
        string _html = String.Empty;
        string _statusRepay = String.Empty;
        string[,] _data1;
        string[,] _data2;

        _data1 = eCPDB.ListDetailPaymentOnCPTransRequireContract(_cp2id);

        if (_data1.GetLength(0) > 0) {
            _statusRepay = _data1[0, 6];

            _data2 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(_cp2id, _statusRepay);

            if (_data2.GetLength(0) > 0)
                _data1[0, 24] = _data2[0, 5];

            switch (_action) {
                case "detail":
                    _html = DetailCpTransPayment(_data1);
                    break;
                case "addfullrepay":
                    _html = AddCpTransPaymentFullRepay(_data1);
                    break;
                case "addpayrepay":
                    _html = AddCpTransPaymentPayRepay(_data1);
                    break;
            }
        }

        return _html;
    }

    public static string TabAddDetailCPTransPayment(string _cp2id) {
        string _html = String.Empty;
        string[,] _data;
                
        _data = eCPDB.ListDetailPaymentOnCPTransRequireContract(_cp2id);

        if (_data.GetLength(0) > 0) {
            string _statusPayment = _data[0, 7];
            string _formatPayment = _data[0, 8];
            string _studentIDDefault = _data[0, 9];
            string _titleNameDefault = _data[0, 10];
            string _firstNameDefault = _data[0, 11];
            string _lastNameDefault = _data[0, 12];
            string _facultyCodeDefault = _data[0, 16];
            string _facultyNameDefault = _data[0, 17];
            string _programCodeDefault = _data[0, 13];
            string _programNameDefault = _data[0, 14];
            string _groupNumDefault = _data[0, 18];
            string _dlevelDefault = _data[0, 20];
            string _pictureFileNameDefault = _data[0, 21];
            string _pictureFolderNameDefault = _data[0, 22];
            string _lawyerFullnameDefault = _data[0, 29];
            string _lawyerPhoneNumberDefault = _data[0, 30];
            string _lawyerMobileNumberDefault = _data[0, 31];
            string _lawyerEmailDefault = _data[0, 32];
            string _lawyerDefault = String.Empty;

            ArrayList _lawyerPhoneNumber = new ArrayList();

            if (!String.IsNullOrEmpty(_lawyerPhoneNumberDefault))
                _lawyerPhoneNumber.Add(_lawyerPhoneNumberDefault);

            if (!String.IsNullOrEmpty(_lawyerMobileNumberDefault))
                _lawyerPhoneNumber.Add(_lawyerMobileNumberDefault);

            if (!String.IsNullOrEmpty(_lawyerFullnameDefault) && (!String.IsNullOrEmpty(_lawyerPhoneNumberDefault) || !String.IsNullOrEmpty(_lawyerMobileNumberDefault) && !String.IsNullOrEmpty(_lawyerEmailDefault))) {
                _lawyerDefault += "คุณ<span>" + _lawyerFullnameDefault + "</span>" + (_lawyerPhoneNumber.Count > 0 ? (" ( <span>" + String.Join(", ", _lawyerPhoneNumber.ToArray()) + "</span> )") : String.Empty) +
                                  " อีเมล์ <span>" + _lawyerEmailDefault + "</span>";
            }

            _html += "<div class='form-content' id='adddetail-cp-trans-payment-head'>" +
                     "  <input type='hidden' id='cp2id' value='" + _cp2id + "'>" +
                     "  <input type='hidden' id='format-payment-hidden' value='" + _formatPayment + "'>" +
                     "  <input type='hidden' id='period-hidden' value=''>" +
                     "  <div id='profile-student'>" +
                     "      <div class='content-left' id='picture-student'><div><img src='Handler/eCPHandler.ashx?action=resize&file=" + eCPUtil.URL_PICTURE_STUDENT + _pictureFolderNameDefault + "/" + _pictureFileNameDefault + "&width=" + eCPUtil.WIDTH_PICTURE_STUDENT + "&height=" + eCPUtil.HEIGHT_PICTURE_STUDENT + "' /></div></div>" +
                     "      <div class='content-left' id='profile-student-label'>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>รหัสนักศึกษา</div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>ชื่อ - นามสกุล</div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>ระดับการศึกษา</div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>คณะ</div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>หลักสูตร</div></div>" +
                     "          <div class='form-label-discription-style clear-bottom'><div class='form-label-style'>นิติกรผู้รับผิดชอบ</div></div>" +
                     "      </div>" +
                     "      <div class='content-left' id='profile-student-input'>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _studentIDDefault + "&nbsp;" + _programCodeDefault.Substring(0, 4) + " / " + _programCodeDefault.Substring(4, 1) + "</span></div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _titleNameDefault + _firstNameDefault + " " + _lastNameDefault + "</span></div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _dlevelDefault + "</span></div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _facultyCodeDefault + " - " + _facultyNameDefault + "</span></div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _programCodeDefault + " - " + _programNameDefault + (!_groupNumDefault.Equals("0") ? " ( กลุ่ม " + _groupNumDefault + " )" : "") + "</span></div></div>" +
                     "          <div class='form-label-discription-style clear-bottom'><div class='form-label-style'>" + _lawyerDefault + "</div></div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='clear'></div>" +
                     "  <div class='box3'></div>" +
                     "  <div class='content-data-head'>" +
                     "      <div class='content-data-tabs content-data-subtabs' id='tabs-adddetail-cp-trans-payment'>" +
                     "          <div class='content-data-tabs-content'>" +
                     "              <ul>" +
                     "                  <li class='first-tab'><a class='active' id='link-tab1-adddetail-cp-trans-payment' alt='#tab1-adddetail-cp-trans-payment' href='javascript:void(0)'>รายละเอียด</a></li>" +
                     "                  <li id='tab2-adddetail-cp-trans-payment'><a id='link-tab2-adddetail-cp-trans-payment' alt='#tab2-adddetail-cp-trans-payment' href='javascript:void(0)'>ทำรายการ</a></li>" +
                     "                  <li id='tab3-adddetail-cp-trans-payment'><a id='link-tab3-adddetail-cp-trans-payment' alt='#tab3-adddetail-cp-trans-payment' href='javascript:void(0)'>การฟ้องคดี</a></li>" +
                     "              </ul>" +
                     "          </div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='content-data-tab-head'>" +
                     "      <div class='subtab-content' id='tab1-adddetail-cp-trans-payment-head'>" +
                     "          <div class='tab-line'></div>" +
                     "      </div>";

            if (!_statusPayment.Equals("3")) {
                _html += "  <div class='subtab-content' id='tab2-adddetail-cp-trans-payment-head'>" +
                         "      <div class='tab-line'></div>" +
                         "  </div>";
            }

            _html += "      <div class='subtab-content' id='tab3-adddetail-cp-trans-payment-head'>" +
                     "          <div class='tab-line'></div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='subtab-content' id='tab1-adddetail-cp-trans-payment-contents'></div>";

            if (!_statusPayment.Equals("3"))
                _html += "<div class='subtab-content' id='tab2-adddetail-cp-trans-payment-contents'></div>";
            
            _html += "  <div class='subtab-content' id='tab3-adddetail-cp-trans-payment-contents'></div>" +
                     "</div>" +
                     "<div id='adddetail-cp-trans-payment-content'>" +
                     "  <div class='subtab-content' id='tab1-adddetail-cp-trans-payment-content'>" +
                     "      <div class='adddetail-cp-trans-payment-content' id='detail-data-trans-payment'>" + DetailCpTransPayment(_data) + "</div>" +
                     "  </div>";

            if (!_statusPayment.Equals("3")) {
                _html += "<div class='subtab-content' id='tab2-adddetail-cp-trans-payment-content'>" +
                         "  <div class='adddetail-cp-trans-payment-content' id='add-data-trans-payment'></div>" +
                         "</div>";
            }

            _html += "  <div class='subtab-content' id='tab3-adddetail-cp-trans-payment-content'>" +
                     "      <div class='adddetail-cp-trans-payment-content' id='addupdate-data-trans-prosecution'></div>" +
                     "  </div>" +
                     "</div>";
        }

        return _html;
    }

    public static string SelectFormatPayment(string _cp2id) {
        string _html = String.Empty;

        _html += "<div class='form-content' id='select-format-payment'>" +
                 "  <div>" +
                 "      <input type='hidden' id='cp2id-hidden' value='" + _cp2id + "'>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='format-payment-label'>" +
                 "                  <div class='form-label-style'>รูปแบบการชำระหนี้</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกรูปแบบที่ต้องการชำระหนี้</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='format-payment-input'>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='format-payment-full-repay-input'><input class='radio' type='radio' name='format-payment' value='1' /></div>" +
                 "                      <div class='content-left' id='format-payment-full-repay-label'>" + eCPUtil._paymentFormat[0] + "</div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='format-payment-pay-repay-input'><input class='radio' type='radio' name='format-payment' value='2' /></div>" +
                 "                      <div class='content-left' id='format-payment-pay-repay-label'>" + eCPUtil._paymentFormat[1] + "</div>" +
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
                 "              <li><a href='javascript:void(0)' onclick='ValidateSelectFormatPayment()'>ตกลง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string ListPaymentOnCPTransRequireContract(HttpContext _c) {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string _pageHtml = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        string _trackingStatus = String.Empty;
        string _iconStatus = String.Empty;
        int _recordCount;
        int _i;
        int[] _resultPage = new int[2];
        int _currentPage;

        _recordCount = eCPDB.CountPaymentOnCPTransRequireContract(_c);

        if (_recordCount > 0) {
            _data = eCPDB.ListPaymentOnCPTransRequireContract(_c);

            HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
            _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
            int _pid = int.Parse(_eCPCookie["Pid"]);
            
            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++) {
                _groupNum = !_data[_i, 9].Equals("0") ? " ( กลุ่ม " + _data[_i, 9] + " )" : "";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";

                if (!_pid.Equals(20) && !_pid.Equals(6))
                    _callFunc = "ChkSelectFormatPayment('" + _data[_i, 1] + "','" + _data[_i, 15] + "','" + _data[_i, 16] + "')";

                _iconStatus = eCPUtil._iconPaymentStatus[int.Parse(_data[_i, 15]) - 1];
                _html += "<ul class='table-row-content " + _highlight + (String.IsNullOrEmpty(_callFunc) ? " noclick" : String.Empty) + "' id='trans-payment" + _data[_i, 1] + "'>" +
                         "  <li id='table-content-cp-trans-payment-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-trans-payment-col2' onclick=" + _callFunc + "><div>" + _data[_i, 3] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-trans-payment-col3' onclick=" + _callFunc + "><div>" + _data[_i, 4] + _data[_i, 5] + " " + _data[_i, 6] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-trans-payment-col4' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 7] + "</span>- " + _data[_i, 8] + _groupNum + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-trans-payment-col5' onclick=" + _callFunc + "><div>" + _data[_i, 22].Replace(",", "<br />") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-trans-payment-col6' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 19]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-trans-payment-col7' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 23]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-trans-payment-col8' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 24]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-trans-payment-col9' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 25]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-trans-payment-col10' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 26]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-trans-payment-col11' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 27]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-trans-payment-col12' onclick=" + _callFunc + ">" +
                         "      <div class='icon-status-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus + "'></li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "</ul>";
            }
            
            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "transpayment", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }

    public static string TabPaymentOnCPTransRequireContract() {
        string _html = String.Empty;
        string _title = "cp-trans-payment";
        int _pid;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _pid = int.Parse(_eCPCookie["Pid"]);

        if (_pid.Equals(20) || _pid.Equals(6))
            _title = "cp-report-debtor-contract-break-require-repay-payment";

        _html += "<div id='cp-trans-payment-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle(_title) +
                 "      <div class='content-data-tabs' id='tabs-cp-trans-payment'>" +
                 "          <div class='content-data-tabs-content'>" +
                 "              <ul>" +
                 "                  <li><a class='active' id='link-tab1-cp-trans-payment' alt='#tab1-cp-trans-payment' href='javascript:void(0)'>รายการแจ้งชำระหนี้</a></li>" +
                 "                  <li class='tab-hidden'><a id='link-tab2-cp-trans-payment' alt='#tab2-cp-trans-payment' href='javascript:void(0)'></a></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-content' id='tab1-cp-trans-payment-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'>" +
                 "                  <input type='hidden' id='search-trans-payment' value=''>" +
                 "                  <input type='hidden' id='paymentstatus-trans-payment-hidden' value=''>" +
                 "                  <input type='hidden' id='paymentstatus-trans-payment-text-hidden' value=''>" +
                 "                  <input type='hidden' id='paymentrecordstatus-trans-payment-hidden' value=''>" +
                 "                  <input type='hidden' id='paymentrecordstatus-trans-payment-text-hidden' value=''>" +
                 "                  <input type='hidden' id='id-name-trans-payment-hidden' value=''>" +
                 "                  <input type='hidden' id='faculty-trans-payment-hidden' value=''>" +
                 "                  <input type='hidden' id='program-trans-payment-hidden' value=''>" +
                 "                  <input type='hidden' id='date-start-trans-repay1-reply-hidden' value=''>" +
                 "                  <input type='hidden' id='date-end-trans-repay1-reply-hidden' value=''>" +
                 "                  <div class='button-style2'>" +
                 "                      <ul>" +
                 "                          <li><a href='javascript:void(0)' onclick=LoadForm(1,'searchcptranspayment',true,'','','')>ค้นหา</a></li>" +
                 "                          <li><a href='javascript:void(0)' onclick=PrintDebtorContractBreakRequireRepayPayment()>ส่งออก</a></li>" +
                 "                      </ul>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-cp-trans-payment'>ค้นหาพบ 0 รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='box-search-condition' id='search-trans-payment-condition'>" +
                 "              <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
                 "              <div class='box-search-condition-order search-trans-payment-condition-order' id='search-trans-payment-condition-order1'>" +
                 "                  <div class='box-search-condition-order-title'>สถานะการชำระหนี้</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-payment-condition-order1-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-trans-payment-condition-order' id='search-trans-payment-condition-order6'>" +
                 "                  <div class='box-search-condition-order-title'>สถานะการบันทึกข้อมูลการชำระหนี้</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-payment-condition-order6-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-trans-payment-condition-order' id='search-trans-payment-condition-order2'>" +
                 "                  <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-payment-condition-order2-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-trans-payment-condition-order' id='search-trans-payment-condition-order3'>" +
                 "                  <div class='box-search-condition-order-title'>คณะ</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-payment-condition-order3-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-trans-payment-condition-order' id='search-trans-payment-condition-order4'>" +
                 "                  <div class='box-search-condition-order-title'>หลักสูตร</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-payment-condition-order4-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-trans-payment-condition-order' id='search-trans-payment-condition-order5'>" +
                 "                  <div class='box-search-condition-order-title'>ช่วงวันที่รับเอกสารการทวงถามตอบกลับครั้งที่ 1</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-payment-condition-order5-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab2-cp-trans-payment-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "   </div>" +
                 "  <div class='tab-content' id='tab1-cp-trans-payment-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-cp-trans-payment-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-trans-payment-col2'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-trans-payment-col3'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-trans-payment-col4'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-trans-payment-col5'><div class='table-head-line1'>รับเอกสาร</div><div>การทวงถาม</div><div>คร้้งที่ 1, 2</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-trans-payment-col6'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่ต้องชดใช้</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-trans-payment-col7'><div class='table-head-line1'>รวมเงินต้น</div><div>รับชำระ</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-trans-payment-col8'><div class='table-head-line1'>รวมดอกเบี้ย</div><div>รับชำระ</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-trans-payment-col9'><div class='table-head-line1'>รวมเงิน</div><div>รับชำระ</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-trans-payment-col10'><div class='table-head-line1'>เงินต้น</div><div>คงเหลือ</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-trans-payment-col11'><div class='table-head-line1'>ดอกเบี้ย</div><div>ค้างชำระ</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-trans-payment-col12'><div class='table-head-line1'>สถานะ</div><div>การชำระหนี้</div><div><a href='javascript:void(0)' onclick=LoadForm(1,'detailpaymentstatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-trans-payment-contents'></div>" +
                 "</div>" +
                 "<div id='cp-trans-payment-content'>" +
                 "  <div class='tab-content' id='tab1-cp-trans-payment-content'>" +
                 "      <div class='box4' id='list-data-trans-payment'></div>" +
                 "      <div id='nav-page-trans-payment'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-trans-payment-content'>" +
                 "      <div class='box1' id='adddetail-data-trans-payment'></div>" +
                 "  </div>" +
                 "</div>" +
                 "<iframe class='export-target' id='export-target' name='export-target'></iframe>" +
                 "<form id='export-setvalue' method='post' target='export-target'>" +
                 "  <input id='export-send' name='export-send' value='' type='hidden' />" +
                 "  <input id='export-order' name='export-order' value='' type='hidden' />" +
                 "  <input id='export-type' name='export-type' value='' type='hidden' />" +
                 "</form>";

        return _html;
    }   
}
