using System;
using System.IO;

public partial class UploadFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string _action = Request.Form["action"];

        if (_action.Equals("preview"))
        {
            Stream _fs = Request.Files["file"].InputStream;
            BinaryReader _br = new BinaryReader(_fs);
            Byte[] _bytes = _br.ReadBytes((Int32)_fs.Length);

            Response.Write(Convert.ToBase64String(_bytes, 0, _bytes.Length));
        }
    }
}