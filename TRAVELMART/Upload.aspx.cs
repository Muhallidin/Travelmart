using System;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.UI.WebControls;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Text;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace TRAVELMART
{
    public partial class Upload : System.Web.UI.Page
    {
        /// <summary>
        /// ========================================================        
        /// Date Modified:  15/03/2012
        /// Modified By:    Muhallidin G Wali
        /// (description)   Replace Global Variable with Session          
        /// ========================================================  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label MasterLabel = (Label)Master.FindControl("uclabelStatus");
                MasterLabel.Visible = false;
                 Session["strPrevPage"] = Request.RawUrl;
            }
        }

        protected void uoButtonExtract_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string connString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + txtSourcePath.Text + ";" + "Extended Properties=Excel 8.0"; 
            //    //"Password=" + txtPWSource + ";";              
            //    OleDbConnection _SourceConn = new OleDbConnection(connString);            
            //    // _OleConn[1].ConnectionString = connString;            
            //    _SourceConn.Open();              _Sourcedt = new DataTable();             
            //    LoadCombo2(); 
            //}
            //catch (Exception ex)
            //{

            //}
        }

 
    }
}
