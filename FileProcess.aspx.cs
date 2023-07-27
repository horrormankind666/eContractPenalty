using System;
using System.IO;
using System.Text;
using System.Web.UI;

public partial class FileProcess: Page {
    protected void Page_Load(
        object sender,
        EventArgs e
    ) {
        string action = Request.Form["action"];

        if (!string.IsNullOrEmpty(action)) {
            if (action.Equals("preview")) {
                Stream fs = Request.Files["file"].InputStream;
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((int)fs.Length);

                Response.Write(Convert.ToBase64String(bytes, 0, bytes.Length));
            }

            if (action.Equals("download")) {
                string fName = Request.Form["filename"];
                string fEncode = Request.Form["file"];
                string fDecode = Encoding.UTF8.GetString(Convert.FromBase64String(fEncode));
                string[] fDecodeArray = (fDecode.Trim()).Split(';');
                string fContentType = ((fDecodeArray[0].Trim()).Split(':'))[1];
                string fBase64 = ((fDecodeArray[1].Trim()).Split(','))[1];
                byte[] bytes = Convert.FromBase64String(fBase64.Trim());
                MemoryStream ms = new MemoryStream(bytes);

                Response.AddHeader("Content-Disposition", "attachment; filename=" + fName + "." + eCPUtil.GetFileExtension(fContentType));
                Response.AddHeader("Content-Length", ms.Length.ToString());
                Response.ContentType = fContentType;
                Response.BinaryWrite(ms.ToArray());
                Response.Flush();
                Response.Close();
            }
        }
    }
}