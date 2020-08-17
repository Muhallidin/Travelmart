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
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace TRAVELMART.Maintenance
{
    public partial class HotelAirportBrandPopup : System.Web.UI.Page
    {
        #region Event
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   03/Jul/2014
        /// Description:    Assign Brand-Airport to this Hotel
        /// </summary>        
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Login.aspx", false);
                }

                uoHiddenFieldHotelID.Value = GlobalCode.Field2Int(Request.QueryString["vmid"]).ToString();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                BindPage();
            }
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            SaveBrandAirportHotel();
        }
        protected void uoButtonAdd_Click(object sender, EventArgs e)
        {
            AirportAdd();
        }

        protected void uoButtonRemove_Click(object sender, EventArgs e)
        {
            AirportRemove();
        }
        protected void uoButtonSearchAirport_Click(object sender, EventArgs e)
        {
            BindAirport();
        }
        #endregion

        #region Procedure
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   03/Jul/2014
        /// Description:    Load value of selected hotel
        /// </summary>
        private void BindPage()
        {
            MaintenanceViewBLL.GetHotelDetails(GlobalCode.Field2Int(uoHiddenFieldHotelID.Value));

            List<VendorHotelList> listHotel = new List<VendorHotelList>();
            List<BrandList> listBrand = new List<BrandList>();
            List<AirportDTO> listAirportNotAssigned = new List<AirportDTO>();
            List<AirportDTO> listAirportAssigned = new List<AirportDTO>();

            listHotel = (List<VendorHotelList>)Session["HotelAirportBrandPop_Hotel"];
            if (listHotel.Count > 0)
            {
                uoTextBoxHotelName.Text = listHotel[0].HotelName;
            }

            listBrand = (List<BrandList>)Session["HotelAirportBrandPop_Brand"];
            if (listBrand.Count > 0)
            {
                uoCheckBoxListBrand.DataSource = listBrand;
                uoCheckBoxListBrand.DataTextField = "BrandName";
                uoCheckBoxListBrand.DataValueField = "BrandID";
                uoCheckBoxListBrand.DataBind();

                for(int i=0; i< listBrand.Count; i++)
                {
                    uoCheckBoxListBrand.Items[i].Selected = listBrand[i].IsAssigned;
                }
            }

            listAirportNotAssigned = (List<AirportDTO>)Session["HotelAirportBrandPop_AirportNotAssigned"];
            uoListViewAirportNotAssigned.DataSource = listAirportNotAssigned;
            uoListViewAirportNotAssigned.DataBind();

            listAirportAssigned = (List<AirportDTO>)Session["HotelAirportBrandPop_AirportAssigned"];
            uoListViewAirportAssigned.DataSource = listAirportAssigned;
            uoListViewAirportAssigned.DataBind();
        }
        /// <summary>
        /// Date Created:   04/Jul/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Airport
        /// </summary>
        private void BindAirport()
        {
            List<AirportDTO> listAirport = new List<AirportDTO>();
            List<AirportDTO> listAirportAssigned = new List<AirportDTO>();

           
            listAirport = AirBLL.GetAirportByRegion("0", uoTextBoxAirport.Text.Trim());
            listAirportAssigned = GetAirportAssigned();
            
            listAirport.RemoveAll(a => listAirportAssigned.Exists(b => a.AirportIDString == b.AirportIDString));
            listAirport = listAirport.OrderBy(a => a.AirportNameString).ToList();

            Session["HotelAirportBrandPop_AirportNotAssigned"] = listAirport;

            uoListViewAirportNotAssigned.DataSource = listAirport;
            uoListViewAirportNotAssigned.DataBind();
        }
        /// <summary>         
        /// Author:         Josephine Gad
        /// Date Created:   04/Jul/2014
        /// Description:    Add to airport list
        /// </summary>
        private void AirportAdd()
        {
            List<AirportDTO> listAirportNOTAssigned = new List<AirportDTO>();
            List<AirportDTO> listAirportAssigned = new List<AirportDTO>();


            listAirportAssigned = GetAirportAssigned();
            listAirportNOTAssigned = GetAirportNotAssigned();                      

            AirportDTO item;
            HiddenField hAirport;
            CheckBox cSelect;
            Label lAirport;
            for (int i = 0; i < uoListViewAirportNotAssigned.Items.Count; i++)
            {                
                cSelect = (CheckBox)uoListViewAirportNotAssigned.Items[i].FindControl("uoCheckBoxSelect");

                if (cSelect.Checked)
                {
                    hAirport = (HiddenField)uoListViewAirportNotAssigned.Items[i].FindControl("uoHiddenFieldAirport");
                    lAirport = (Label)uoListViewAirportNotAssigned.Items[i].FindControl("uoLabelAirport");

                    item = new AirportDTO();
                    item.AirportIDString = hAirport.Value;
                    item.AirportNameString = lAirport.Text;

                    listAirportAssigned.Add(item);
                }
            }

            listAirportAssigned = listAirportAssigned.OrderBy(a => a.AirportNameString).ToList();

            listAirportNOTAssigned.RemoveAll(a => listAirportAssigned.Exists(b => a.AirportIDString == b.AirportIDString));
            listAirportNOTAssigned = listAirportNOTAssigned.OrderBy(a => a.AirportNameString).ToList();

            Session["HotelAirportBrandPop_AirportAssigned"] = listAirportAssigned;
            Session["HotelAirportBrandPop_AirportNotAssigned"] = listAirportNOTAssigned;

            uoListViewAirportAssigned.DataSource = listAirportAssigned;
            uoListViewAirportAssigned.DataBind();

            uoListViewAirportNotAssigned.DataSource = listAirportNOTAssigned;
            uoListViewAirportNotAssigned.DataBind();
        }
        /// <summary>         
        /// Author:         Josephine Gad
        /// Date Created:   04/Jul/2014
        /// Description:    remove from airport list
        /// </summary>
        private void AirportRemove()
        {
            List<AirportDTO> listAirportNOTAssigned = new List<AirportDTO>();
            List<AirportDTO> listAirportAssigned = new List<AirportDTO>();


            listAirportAssigned = GetAirportAssigned();
            listAirportNOTAssigned = GetAirportNotAssigned();

            AirportDTO item;
            HiddenField hAirport;
            CheckBox cSelect;
            Label lAirport;
            for (int i = 0; i < uoListViewAirportAssigned.Items.Count; i++)
            {
                cSelect = (CheckBox)uoListViewAirportAssigned.Items[i].FindControl("uoCheckBoxSelect");

                if (cSelect.Checked)
                {
                    hAirport = (HiddenField)uoListViewAirportAssigned.Items[i].FindControl("uoHiddenFieldAirport");
                    lAirport = (Label)uoListViewAirportAssigned.Items[i].FindControl("uoLabelAirport");

                    item = new AirportDTO();
                    item.AirportIDString = hAirport.Value;
                    item.AirportNameString = lAirport.Text;

                    listAirportNOTAssigned.Add(item);
                }
            }

            listAirportNOTAssigned = listAirportNOTAssigned.OrderBy(a => a.AirportNameString).ToList();

            listAirportAssigned.RemoveAll(a => listAirportNOTAssigned.Exists(b => a.AirportIDString == b.AirportIDString));
            listAirportAssigned = listAirportAssigned.OrderBy(a => a.AirportNameString).ToList();

            Session["HotelAirportBrandPop_AirportAssigned"] = listAirportAssigned;
            Session["HotelAirportBrandPop_AirportNotAssigned"] = listAirportNOTAssigned;

            uoListViewAirportAssigned.DataSource = listAirportAssigned;
            uoListViewAirportAssigned.DataBind();

            uoListViewAirportNotAssigned.DataSource = listAirportNOTAssigned;
            uoListViewAirportNotAssigned.DataBind();
        }
        /// <summary>         
        /// Author:         Josephine Gad
        /// Date Created:   04/Jul/2014
        /// Description:    Get Assigned Airport based from session or listview
        /// </summary>
        private List<AirportDTO> GetAirportAssigned()
        {
            List<AirportDTO> listAirportAssigned = new List<AirportDTO>();
            //List<AirportDTO> listAirport = new List<AirportDTO>();
            AirportDTO item;

            if (Session["HotelAirportBrandPop_AirportAssigned"] != null)
            {
                listAirportAssigned = (List<AirportDTO>)Session["HotelAirportBrandPop_AirportAssigned"];
            }
            else
            {
                HiddenField hAirport;
                Label lAirport;
                for (int i = 0; i < uoListViewAirportAssigned.Items.Count; i++)
                {
                    hAirport = (HiddenField)uoListViewAirportAssigned.Items[i].FindControl("uoHiddenFieldAirport");
                    lAirport = (Label)uoListViewAirportAssigned.Items[i].FindControl("uoLabelAirport");

                    item = new AirportDTO();
                    item.AirportIDString = hAirport.Value;
                    item.AirportNameString = lAirport.Text;

                    listAirportAssigned.Add(item);
                }
                Session["HotelAirportBrandPop_AirportAssigned"] = listAirportAssigned;
            }            
            return listAirportAssigned;           
        }
        /// <summary>         
        /// Author:         Josephine Gad
        /// Date Created:   04/Jul/2014
        /// Description:    Get Unassigned Airport based from session or listview
        /// </summary>
        private List<AirportDTO> GetAirportNotAssigned()
        {
            List<AirportDTO> listAirportNotAssigned = new List<AirportDTO>();
            AirportDTO item;

            if (Session["HotelAirportBrandPop_AirportNotAssigned"] != null)
            {
                listAirportNotAssigned = (List<AirportDTO>)Session["HotelAirportBrandPop_AirportNotAssigned"];
            }
            else
            {
                HiddenField hAirport;
                Label lAirport;
                for (int i = 0; i < uoListViewAirportNotAssigned.Items.Count; i++)
                {
                    hAirport = (HiddenField)uoListViewAirportAssigned.Items[i].FindControl("uoHiddenFieldAirport");
                    lAirport = (Label)uoListViewAirportAssigned.Items[i].FindControl("uoLabelAirport");

                    item = new AirportDTO();
                    item.AirportIDString = hAirport.Value;
                    item.AirportNameString = lAirport.Text;

                    listAirportNotAssigned.Add(item);
                }
                Session["HotelAirportBrandPop_AirportNotAssigned"] = listAirportNotAssigned;
            }
            return listAirportNotAssigned;
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   04/Jul/2014
        /// Description:    Insert/Delete hotel-brand-hotel matrix 
        /// </summary>
        private void SaveBrandAirportHotel()
        {
            DataTable dtBrand = null;
            DataTable dtAirport = null;
            try
            {
                List<AirportDTO> listAirportAssigned = new List<AirportDTO>();
                listAirportAssigned = GetAirportAssigned();
                var listAirport = (from a in listAirportAssigned
                                       select new
                                       {
                                           AirportIDString = GlobalCode.Field2Int(a.AirportIDString)
                                       }).ToList();


                dtBrand = new DataTable();
                DataColumn colBrandID = new DataColumn("colBrandID", typeof(Int32));
                dtBrand.Columns.Add(colBrandID);
                DataRow rowBrand;

                dtAirport = new DataTable();
                DataColumn colAirportID = new DataColumn("colAirportID", typeof(Int32));
                dtAirport.Columns.Add(colAirportID);

                for (int i = 0; i < uoCheckBoxListBrand.Items.Count; i++)
                {
                    if (uoCheckBoxListBrand.Items[i].Selected)
                    {
                        rowBrand = dtBrand.NewRow();
                        rowBrand["colBrandID"] = GlobalCode.Field2Int(uoCheckBoxListBrand.Items[i].Value);
                        dtBrand.Rows.Add(rowBrand);
                    }
                }
                dtAirport = getDataTable(listAirport);

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();
                MaintenanceViewBLL.BrandAirportHotelSave(GlobalCode.Field2Int(uoHiddenFieldHotelID.Value),
                    uoHiddenFieldUser.Value, "Save Hotel Brand Airport matrix", "SaveBrandAirportHotel",
                    Path.GetFileName(Request.Path),
                    CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, dtBrand, dtAirport);

                ClosePage("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtBrand != null)
                {
                    dtBrand.Dispose();
                }
                if (dtAirport != null)
                {
                    dtAirport.Dispose();
                }
            }
        }
        private DataTable getDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            return tb;
        }

        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                if (t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   04/Jul/2014
        /// Description:    close pop up page
        /// </summary>
        /// <param name="s"></param>
        private void ClosePage(string s)
        {
            string sScript = "<script language='JavaScript'>";

            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopup\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += " self.close(); ";

            sScript += "</script>";

            //ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
            ClientScript.RegisterClientScriptBlock(typeof(string), "scr", sScript);
        }
        #endregion                    
    }
}
