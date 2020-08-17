using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;
using System.Data.SqlClient;
using System.Configuration; 

namespace TRAVELMART.ContractManagement
{
    public partial class Attachment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            byte[] imageBytes = new byte[FileUpload1.PostedFile.InputStream.Length + 1];
            FileUpload1.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);

            ContractBLL.InsertAttachHotelContract(FileUpload1.FileName, imageBytes, imageBytes.Length, FileUpload1.PostedFile.ContentType);
        }
    }
}
