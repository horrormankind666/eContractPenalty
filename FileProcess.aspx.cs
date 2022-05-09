using System;
using System.IO;
using System.Text;
using System.Web.UI;

public partial class FileProcess: Page {
    protected void Page_Load(
        object sender,
        EventArgs e
    ) {
        string _action = Request.Form["action"];

        if (!String.IsNullOrEmpty(_action)) {
            if (_action.Equals("preview")) {
                Stream _fs = Request.Files["file"].InputStream;
                BinaryReader _br = new BinaryReader(_fs);
                Byte[] _bytes = _br.ReadBytes((Int32)_fs.Length);

                Response.Write(Convert.ToBase64String(_bytes, 0, _bytes.Length));
            }

            if (_action.Equals("download")) {
                string _fName = Request.Form["filename"];
                string _fEncode = Request.Form["file"];
                string _fDecode = Encoding.UTF8.GetString(Convert.FromBase64String(_fEncode));
                string[] _fDecodeArray = (_fDecode.Trim()).Split(';');
                string _fContentType = ((_fDecodeArray[0].Trim()).Split(':'))[1];
                string _fBase64 = ((_fDecodeArray[1].Trim()).Split(','))[1];
                byte[] _bytes = Convert.FromBase64String(_fBase64.Trim());
                MemoryStream _ms = new MemoryStream(_bytes);

                Response.AddHeader("Content-Disposition", "attachment; filename=" + _fName + "." + eCPUtil.GetFileExtension(_fContentType));
                Response.AddHeader("Content-Length", _ms.Length.ToString());
                Response.ContentType = _fContentType;
                Response.BinaryWrite(_ms.ToArray());
                Response.Flush();
                Response.Close();
            }
        }
    }
}