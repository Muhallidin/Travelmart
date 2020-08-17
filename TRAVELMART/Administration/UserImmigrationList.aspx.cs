using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TRAVELMART.Administration
{
    public partial class UserImmigrationList : System.Web.UI.Page
    {
        #region event
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   13/Nov/2015
        /// Description:    Page to reset Immigration Role with alternate email
        
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            if (!IsPostBack)
            {
                //Audit Trail
                string strLogDescription = "Immigration with Alternate Email Maintenance Viewed";
                string strFunction = "Page_Load";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                BindUsers();
            }
        }
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            BindUsers();
        }

        protected void uoButtonChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                string strNewPass = uoTextBoxNewPassword.Text;
                string sMsg = "";

                if (!strNewPass.Any(char.IsUpper))
                {
                    sMsg = "Password must have uppercase ";
                }
                if (!strNewPass.Any(char.IsLower))
                {
                    if (sMsg.Trim() == "")
                    {
                        sMsg = "Password must have lowercase letter. ";
                    }
                    else
                    {
                        sMsg = sMsg + " and lowercase letter. ";
                    }
                }
                else
                {
                    if (sMsg != "")
                    {
                        sMsg = sMsg + "letter. ";
                    }
                }

                if (strNewPass.Length < 9)
                {
                    if (sMsg.Trim() == "")
                    {
                        sMsg = "Password must have be at least 9 characters long. ";
                    }
                    else
                    {
                        sMsg = sMsg + " It must be at least 9 characters long. ";
                    }
                }

                if (sMsg != "")
                {
                    AlertMessage(sMsg);
                }
                else
                {
                    AddEditImmigrationOfficers();
                }
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }
        protected void uoUserListPager_PreRender(object sender, EventArgs e)
        {

        }
        #endregion

        #region function
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/04/2012
        /// Description: Initialize Session Values (user, role)
        /// </summary>
        protected void InitializeValues()
        {
            string UserRolePrimary = GlobalCode.Field2String(Session["UserRole"]);
            string Name = GlobalCode.Field2String(Session["UserName"]);
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Session["UserName"] = User.Identity.Name;
                Name = GlobalCode.Field2String(Session["UserName"]);
            }

            MembershipUser UserName = Membership.GetUser(Name);
            if (UserName == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }
            else
            {
                if (!UserName.IsOnline)
                {
                    Response.Redirect("~/Login.aspx", false);
                }
            }

            if (GlobalCode.Field2String(Session["UserRole"]) == "")
            {
                UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(Name);
                Session["UserRole"] = UserRolePrimary;
            }

            uoHiddenFieldUser.Value = Name;
            uoHiddenFieldRole.Value = UserRolePrimary;
            Session["strPrevPage"] = Request.RawUrl;

            ListView1.DataSource = null;
            ListView1.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   13/Nov/2015
        /// Description:    Bind Users from Immigration ROle
        /// </summary>        
        private void BindUsers()
        {
            string sUserameOrEmail = uoTextBoxSearch.Text.Trim();
            DateTime dtFrom = DateTime.Now;
            DateTime dtTo = dtFrom;

            List<UserList_LDAP> list = UserAccountBLL.GetUserList(dtFrom, dtTo, TravelMartVariable.RoleImmigration, true, true, sUserameOrEmail);
            if (list.Count > 0)
            {
                uoUserList.DataSource = list;
                uoUserList.DataBind();
            }
            else
            {
                uoUserList.DataSource = null;
                uoUserList.DataBind();
            }
        }
        /// Author:         Josephine Monteza
        /// Date Created:   03/Nov/2015
        /// Description:    Get all Immigartion Users with alternate email to reset pwd
        private void AddEditImmigrationOfficers()
        {
            DataTable dtUser = null;
            DataTable dt = null;

            string sMsg = "";
            try
            {
                CheckBox lChkSelect;
                Label lAlternateEmail;
                Label lUserName;                
                Label lLastName;
                Label lFirstName;
                //Label lMiddleName;
                Label lEmail;

                string sAlternateEmail;
                string sUserName;
                string sPassword;

                string sLastName;
                string sFirstName;
                //string sMiddleName;

                string sEmail;

                dt = new DataTable();
                DataColumn col = new DataColumn("UserName", typeof(string));
                dt.Columns.Add(col);
                DataRow row = dt.NewRow();

                if (uoUserList.Items.Count > 0)
                {
                    for (int i = 0; i < uoUserList.Items.Count; i++)
                    {
                        lChkSelect = (CheckBox)uoUserList.Items[i].FindControl("uoCheckBoxSelect");
                        if (lChkSelect.Checked)
                        {
                            lUserName = (Label)uoUserList.Items[i].FindControl("uoLblUName");
                            lAlternateEmail = (Label)uoUserList.Items[i].FindControl("uoLabelAltEmail");
                            lLastName = (Label)uoUserList.Items[i].FindControl("uoLabelLName");
                            lFirstName = (Label)uoUserList.Items[i].FindControl("uoLabelFName");
                            lEmail = (Label)uoUserList.Items[i].FindControl("uoLabelEmail");


                            sUserName = lUserName.Text;
                            sAlternateEmail = lAlternateEmail.Text;
                            sLastName = lLastName.Text;
                            sFirstName = lFirstName.Text;
                            // sMiddleName = "";
                            sEmail = lEmail.Text;

                            MembershipUser mUser = Membership.GetUser(sUserName);
                            if (mUser.IsLockedOut)
                            {
                                mUser.UnlockUser();
                            }

                            sPassword = mUser.ResetPassword();
                            mUser.ChangePassword(sPassword, uoTextBoxNewPassword.Text);
                            mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);
                            Membership.UpdateUser(mUser);

                            UserAccountBLL.LDAPImmigrationUpdate(sUserName, sEmail, sAlternateEmail, false);

                            //Add/modify from LDAP
                            sMsg = sMsg + "\n" + MUser.AddEditUserFromLDAP(sUserName, sFirstName, sLastName, sEmail, sPassword, "", sEmail);
                            MUser.ChangePasswordInLDAP(sUserName, uoTextBoxNewPassword.Text);

                            //remove this since they want Michael Jaworski to send the email manually to the users
                            //UserAccountBLL.EmailUserPassword(sUserName, sPassword, sAlternateEmail);    

                            row = dt.NewRow();
                            row[col] = sUserName;
                            dt.Rows.Add(row);
                        }
                    }
                }

                //extract the list instead
                dtUser = UserAccountBLL.GetImmigrationUsersToExtract(TravelMartVariable.RoleImmigration, true, uoTextBoxNewPassword.Text, dt);
                if (dtUser != null)
                {
                    if (dtUser.Rows.Count > 0)
                    {
                        CreateFile(dtUser);
                    }
                    else
                    {
                        sMsg = "No User Updated";
                    }
                }
                else
                {
                    sMsg = "No User Updated.";
                }

                AlertMessage("Information: " + sMsg);
            }
            catch (Exception ex)
            {                
                AlertMessage(ex.Message);
            }
            finally
            {
                if (dtUser != null)
                {
                    dtUser.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// ============================================
        /// Modified By:    Josephine  Gad
        /// CModified Date: 02/Sep/2013
        /// Description:    Change filename to Crew Admin Manifest + date of manifest + date extracted
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt)
        {
            try
            {
                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/Immigration/");
                string sDateNow = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string sDate = GlobalCode.Field2DateTime(sDateNow).ToString("MMMddyyy");
                string FileName = "ImmigrationUsers_" + sDate + "_" + sDateNow + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
                CreateExcel(dt, strFileName);
                OpenExcelFile(FileName, strFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Marco Abejar Gad
        /// Modified Date:  26/April/2013
        /// Description:    Added columns and include aie legs
        /// </summary>
        public static void CreateExcel(DataTable dtSource, string strFileName)
        {
            try
            {
                // Create XMLWriter
                using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
                {
                    int iColCount = (dtSource.Columns.Count);
                    //Format the output file for reading easier
                    xtwWriter.Formatting = Formatting.Indented;

                    // <?xml version="1.0"?>
                    xtwWriter.WriteStartDocument();

                    // <?mso-application progid="Excel.Sheet"?>
                    xtwWriter.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");

                    // <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet >"
                    xtwWriter.WriteStartElement("Workbook", "urn:schemas-microsoft-com:office:spreadsheet");

                    //Write definition of namespace
                    xtwWriter.WriteAttributeString("xmlns", "o", null, "urn:schemas-microsoft-com:office:office");
                    xtwWriter.WriteAttributeString("xmlns", "x", null, "urn:schemas-microsoft-com:office:excel");
                    xtwWriter.WriteAttributeString("xmlns", "ss", null, "urn:schemas-microsoft-com:office:spreadsheet");
                    xtwWriter.WriteAttributeString("xmlns", "html", null, "http://www.w3.org/TR/REC-html40");

                    // <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
                    xtwWriter.WriteStartElement("DocumentProperties", "urn:schemas-microsoft-com:office:office");

                    // Write document properties
                    xtwWriter.WriteElementString("Author", "Travelmart");
                    xtwWriter.WriteElementString("LastAuthor", Environment.UserName);
                    xtwWriter.WriteElementString("Created", DateTime.Now.ToString("u") + "Z");
                    xtwWriter.WriteElementString("Company", "RCCL");
                    xtwWriter.WriteElementString("Version", "1");

                    // </DocumentProperties>
                    xtwWriter.WriteEndElement();

                    // <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
                    xtwWriter.WriteStartElement("ExcelWorkbook", "urn:schemas-microsoft-com:office:excel");

                    // Write settings of workbook
                    xtwWriter.WriteElementString("WindowHeight", "13170");
                    xtwWriter.WriteElementString("WindowWidth", "17580");
                    xtwWriter.WriteElementString("WindowTopX", "120");
                    xtwWriter.WriteElementString("WindowTopY", "60");
                    xtwWriter.WriteElementString("ProtectStructure", "False");
                    xtwWriter.WriteElementString("ProtectWindows", "False");

                    // </ExcelWorkbook>
                    xtwWriter.WriteEndElement();

                    // <Styles>
                    xtwWriter.WriteStartElement("Styles");

                    // <Style ss:ID="Default" ss:Name="Normal">
                    xtwWriter.WriteStartElement("Style");
                    xtwWriter.WriteAttributeString("ss", "ID", null, "Default");
                    xtwWriter.WriteAttributeString("ss", "Name", null, "Normal");

                    // <Alignment ss:Vertical="Bottom"/>
                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement();

                    // Write null on the other properties
                    xtwWriter.WriteElementString("Borders", null);
                    xtwWriter.WriteElementString("Font", null);
                    xtwWriter.WriteElementString("Interior", null);
                    xtwWriter.WriteElementString("NumberFormat", null);
                    xtwWriter.WriteElementString("Protection", null);
                    // </Style>
                    xtwWriter.WriteEndElement();

                    //Style for header
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s62">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s62");
                    xtwWriter.WriteStartElement("Font");
                    // <Font ss:Bold="1"/>
                    xtwWriter.WriteAttributeString("ss", "Bold", null, "1");
                    //end of font
                    xtwWriter.WriteEndElement();
                    //End Style for header
                    xtwWriter.WriteEndElement();

                    //////////////////////////////////////////////////////////////////////
                    //Style for for group with border
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s63">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s63");

                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Left");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement(); //End of Alignment
                    xtwWriter.WriteStartElement("Borders");
                    xtwWriter.WriteStartElement("Border");
                    xtwWriter.WriteAttributeString("ss", "Position", null, "Top");
                    xtwWriter.WriteAttributeString("ss", "LineStyle", null, "Continuous");
                    xtwWriter.WriteAttributeString("ss", "Weight", null, "1");
                    xtwWriter.WriteEndElement(); //End of Borders
                    xtwWriter.WriteEndElement(); // End of Border

                    //End Style for group with border
                    xtwWriter.WriteEndElement();
                    ////////////////////////////////////////////////////////////////////////

                    //Style for total summary numbers
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s64">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s64");
                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Right");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement();
                    //End Style for header
                    xtwWriter.WriteEndElement();


                    //Style for Rows
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s64">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s65");
                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Left");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    // </Alignment>
                    xtwWriter.WriteEndElement();
                    // </Style>
                    xtwWriter.WriteEndElement();


                    // </Styles>
                    xtwWriter.WriteEndElement();

                    // <Worksheet ss:Name="xxx">
                    xtwWriter.WriteStartElement("Worksheet");
                    xtwWriter.WriteAttributeString("ss", "Name", null, "Immigration Role");

                    // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                    xtwWriter.WriteStartElement("Table");

                    int iRow = dtSource.Rows.Count + 15;

                    xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
                    xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

                    xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                    xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                    xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");


                    //Header
                    xtwWriter.WriteStartElement("Row");
                    xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");
                    int i = 1;
                    foreach (DataColumn Header in dtSource.Columns)
                    {
                        if (i <= iColCount)
                        {
                            xtwWriter.WriteStartElement("Cell");
                            // xxx
                            xtwWriter.WriteStartElement("Data");
                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                            // Write content of cell
                            xtwWriter.WriteValue(Header.ColumnName);
                            xtwWriter.WriteEndElement();
                            xtwWriter.WriteEndElement();
                        }
                        i++;
                    }
                    xtwWriter.WriteEndElement();


                    // Run through all rows of data source
                    foreach (DataRow row in dtSource.Rows)
                    {
                        // <Row>
                        xtwWriter.WriteStartElement("Row");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s65");
                        i = 1;

                        // Run through all cell of current rows
                        foreach (object cellValue in row.ItemArray)
                        {
                            if (dtSource.Columns[i - 1].Caption.ToUpper() != "ISVISIBLE")
                            {
                                //if (bGroup)
                                //{
                                if (i <= iColCount)
                                {

                                    // <Cell>
                                    xtwWriter.WriteStartElement("Cell");
                                    //Border
                                    xtwWriter.WriteAttributeString("ss", "StyleID", null, "s63");
                                    // <Data ss:Type="String">xxx</Data>
                                    xtwWriter.WriteStartElement("Data");

                                    if (dtSource.Columns[i - 1].Caption.ToUpper() == "E1ID" ||
                                        //dtSource.Columns[i - 1].Caption.ToUpper() == "HOTEL NITES" ||
                                        //dtSource.Columns[i - 1].Caption.ToUpper() == "ROOM TYPE" ||
                                        //dtSource.Columns[i - 1].Caption.ToUpper() == "MEAL ALLOWANCE" ||
                                        //dtSource.Columns[i - 1].Caption.ToUpper() == "FLIGHT NO." ||
                                        //dtSource.Columns[i - 1].Caption.ToUpper() == "DEPT TIME" ||
                                        //dtSource.Columns[i - 1].Caption.ToUpper() == "ARVL TIME" ||
                                       dtSource.Columns[i - 1].Caption.ToUpper() == "AIR SEQUENCE")
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                    }
                                    else if (dtSource.Columns[i - 1].Caption.ToUpper() == "COSTCENTER")
                                    {
                                        if (GlobalCode.Field2Int(cellValue) > 0)
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                        }
                                        else
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                        }
                                    }
                                    else
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                    }

                                    // Write content of cell
                                    xtwWriter.WriteValue(GlobalCode.Field2String(cellValue));

                                    // </Data>
                                    xtwWriter.WriteEndElement();

                                    // </Cell>
                                    xtwWriter.WriteEndElement();
                                }
                            }
                            i++;
                        }
                        // </Row>
                        xtwWriter.WriteEndElement();

                    }

                    // </Table>
                    xtwWriter.WriteEndElement();

                    // <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                    xtwWriter.WriteStartElement("WorksheetOptions", "urn:schemas-microsoft-com:office:excel");

                    // Write settings of page
                    xtwWriter.WriteStartElement("PageSetup");
                    xtwWriter.WriteStartElement("Header");
                    xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteStartElement("Footer");
                    xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteStartElement("PageMargins");
                    xtwWriter.WriteAttributeString("x", "Bottom", null, "0.984251969");
                    xtwWriter.WriteAttributeString("x", "Left", null, "0.78740157499999996");
                    xtwWriter.WriteAttributeString("x", "Right", null, "0.78740157499999996");
                    xtwWriter.WriteAttributeString("x", "Top", null, "0.984251969");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();

                    // <Selected/>
                    xtwWriter.WriteElementString("Selected", null);

                    // <Panes>
                    xtwWriter.WriteStartElement("Panes");

                    // <Pane>
                    xtwWriter.WriteStartElement("Pane");

                    // Write settings of active field
                    xtwWriter.WriteElementString("Number", "1");
                    xtwWriter.WriteElementString("ActiveRow", "1");
                    xtwWriter.WriteElementString("ActiveCol", "1");

                    // </Pane>
                    xtwWriter.WriteEndElement();

                    // </Panes>
                    xtwWriter.WriteEndElement();

                    // <ProtectObjects>False</ProtectObjects>
                    xtwWriter.WriteElementString("ProtectObjects", "False");

                    // <ProtectScenarios>False</ProtectScenarios>
                    xtwWriter.WriteElementString("ProtectScenarios", "False");

                    // </WorksheetOptions>
                    xtwWriter.WriteEndElement();

                    // </Worksheet>
                    xtwWriter.WriteEndElement();

                    // </Workbook>
                    xtwWriter.WriteEndElement();

                    // Write file on hard disk
                    xtwWriter.Flush();
                    xtwWriter.Close();

                    //FileInfo FileName = new FileInfo(strFileName);
                    //FileStream fs = new FileStream(FileName.FullName, FileMode.Create);                
                    //fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtSource != null)
                {
                    dtSource.Dispose();
                }
            }
        }
        public void OpenExcelFile(string strFileName, string filePath)
        {
            string strScript = "CloseModal('../Extract/Immigration/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoButtonChangePassword, this.GetType(), "CloseModal", strScript, true);
        }
        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", sScript, false);
        }
        #endregion

       
    }
}
