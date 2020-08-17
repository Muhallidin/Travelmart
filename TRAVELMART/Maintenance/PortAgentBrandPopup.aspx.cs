using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Data;
using System.Text.RegularExpressions;
namespace TRAVELMART
{
    public partial class PortAgentBrandPopup : System.Web.UI.Page
    {
        #region "Events"
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/10/2012
        /// Description:    Page for the removed records from Exception List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(GlobalCode.Field2String(Request.QueryString["pid"]) != "")
                {
                    uoHiddenFieldPortAgentIDInt.Value = GlobalCode.Field2String(Request.QueryString["pid"]);
                    //uoTestLabel.Text = GlobalCode.Field2String(Request.QueryString["pid"]);
                }

                if(GlobalCode.Field2String(Request.QueryString["aid"]) != "")
                {
                    uoHiddenFieldAirportID.Value = GlobalCode.Field2String(Request.QueryString["aid"]);
                }

                if (uoHiddenFieldPortAgentIDInt.Value == "0")
                {
                    uoDropDownListPortAgent.Visible = true;
                    uoTextBoxPortAgentName.Visible = false;
                    uoTextBoxAirport.Visible = false;
                }
                else
                {
                    Session["AirportList"] = null;
                    Session["AirportContainer"] = null;
                    BindPortAgentDetails();
                   GetAirportsOfVendor();
                    GetAirportsOFVendorSaved();
                    uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                    //uoCheckBoxListBrand.Attributes.Add("onclick", "return HandleOnCheck()");

                    uoDropDownListPortAgent.Visible = false;
                    uoTextBoxPortAgentName.Visible = true;
                    uoTextBoxAirport.Visible = true;
                }

            }
        }
 
        protected void ButtonAdd( object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Add Airport");
            AddAirport();
        }
        protected void ButtonRemove(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Remove Airport");
            RemoveAirport();
        }
        protected void ButtonSave(object sender,EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Save Changes");
            SaveAirports();
        }

        protected void airportSearch(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Search");
            SearchAirport();
        }


        #endregion

        #region "Functions"

        /// <summary>
        /// Date Created: 06/27/2014
        /// Created By:   Michael Brian C. Evangelista
        /// Description:  Add Airports for vendor brand
        /// </summary>
      
      private void SearchAirport(){
          DataTable dt = new DataTable();
          DataTable dt2 = new DataTable();
          

          string uoSearchFieldAirport = uoSearchAirportBox.Text;
          string serviceProvider = GlobalCode.Field2String(Request.QueryString["pid"]);
          dt = VendorMaintenanceBLL.GetServiceProviderAirportbyBrand(serviceProvider);
          dt2 = dt.Clone();
          if (dt.Rows.Count > 0)
          {
              for (var f = 0; f < dt.Rows.Count; f++)
              {
              string temp = dt.Rows[f]["colAirportFullName"].ToString();
              int tempColCount = temp.Length;
              string tempSearch = temp.ToLower();
              bool searchTerm = tempSearch.Substring(0, tempColCount).Contains(uoSearchFieldAirport.ToLower());

                 if(searchTerm)
                 {
                     dt2.ImportRow(dt.Rows[f]);
                 }

              }
              if (dt2.Rows.Count > 0)
              {
                  uoListViewAirport.DataSource = dt2;
                  uoListViewAirport.DataBind();
              }
              else
              {
                  uoListViewAirport.DataSource = dt;
                  uoListViewAirport.DataBind();              
              }
          }


      }
      private void GetAirportsOfVendor()
        {

            DataTable dt = new DataTable();
            

            string serviceProvider = GlobalCode.Field2String(Request.QueryString["pid"]);
            dt = VendorMaintenanceBLL.GetServiceProviderAirportbyBrand(serviceProvider);
            if (dt.Rows.Count > 0)
            {
                    uoListViewAirport.DataSource = dt;
                    uoListViewAirport.DataBind();   
            }
        }  

        private void AddAirport() {
            CheckBox uoCheckBoxSelect;
            HiddenField uoHiddenFieldAirport;
            Label uoLabelAirport;

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            string serviceProvider = GlobalCode.Field2String(Request.QueryString["pid"]);

            dt = VendorMaintenanceBLL.GetServiceProviderAirportbyBrand(serviceProvider);
            dt2 = VendorMaintenanceBLL.GetServiceProviderAirportbyVendor(serviceProvider);


            /// check if there is already a an existing datatable using session

            if (Session["AirportContainer"] != null)
            {
                dt3 = (DataTable) Session["AirportContainer"];
                dt = (DataTable) Session["AirportList"];
            }
            else
            {
            dt3 = dt2.Clone();
            dt3 = dt2.Copy();
            }

            foreach(ListViewItem item in uoListViewAirport.Items)
            {
         
                uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                if (uoCheckBoxSelect.Checked == true)
                {
                    uoHiddenFieldAirport = (HiddenField)item.FindControl("uoHiddenFieldAirport");
                    uoLabelAirport = (Label)item.FindControl("uoLabelAirport");

                   string  HiddenFieldAirport = GlobalCode.Field2String(uoHiddenFieldAirport.Value);
                  
                   for (var f = 0; f < dt.Rows.Count; f++ )
                   {
                       
                       if (dt.Rows[f]["colAirportIDInt"].ToString() == HiddenFieldAirport)
                       {   
                           dt3.ImportRow(dt.Rows[f]);
                           dt.Rows.RemoveAt(f);
                       }
                   }

                }
                
            }
            if (dt3.Rows.Count > 0)
            {

                dt3.DefaultView.Sort = "colAirportIDInt ASC";
                dt3 = dt3.DefaultView.ToTable();
                uoListViewAirportSaved.DataSource = dt3;
                uoListViewAirportSaved.DataBind();
                
                dt.DefaultView.Sort = "colAirportIDInt ASC";
                dt = dt.DefaultView.ToTable();
                uoListViewAirport.DataSource = dt;
                uoListViewAirport.DataBind();

                Session["AirportContainer"] = dt3;
                Session["AirportList"] = dt;
            }
        }   


        private void RemoveAirport() {

            CheckBox uoCheckBoxSelect;
            HiddenField uoHiddenFieldAirport;
            Label uoLabelAirport;

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            string serviceProvider = GlobalCode.Field2String(Request.QueryString["pid"]);

            dt = VendorMaintenanceBLL.GetServiceProviderAirportbyBrand(serviceProvider);
            dt2 = VendorMaintenanceBLL.GetServiceProviderAirportbyVendor(serviceProvider);

            /// check if there is already a an existing datatable using session

            if (Session["AirportContainer"] != null)
            {
                dt3 = (DataTable)Session["AirportContainer"];
                dt = (DataTable)Session["AirportList"];
            }
            else
            {
                dt3 = dt2.Clone();
                dt3 = dt2.Copy();
            }

            foreach (ListViewItem item in uoListViewAirportSaved.Items)
            {
                uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelectSaved");
                if (uoCheckBoxSelect.Checked == true)
                {
                    uoHiddenFieldAirport = (HiddenField)item.FindControl("uoHiddenFieldAirportSaved");
                    uoLabelAirport = (Label)item.FindControl("uoLabelAirportSaved");

                    string HiddenFieldAirport = GlobalCode.Field2String(uoHiddenFieldAirport.Value);

                    for (var f = 0; f < dt3.Rows.Count; f++)
                    {

                        if (dt3.Rows[f]["colAirportIDInt"].ToString() == HiddenFieldAirport)
                        {
                            dt.ImportRow(dt3.Rows[f]);
                            dt3.Rows.RemoveAt(f);
                        }
                    }                
                
                }


            }
            if (dt3.Rows.Count > 0)
            {

                dt3.DefaultView.Sort = "colAirportIDInt ASC";
                dt3 = dt3.DefaultView.ToTable();
                uoListViewAirportSaved.DataSource = dt3;
                uoListViewAirportSaved.DataBind();

                dt.DefaultView.Sort = "colAirportIDInt ASC";
                dt = dt.DefaultView.ToTable();
                uoListViewAirport.DataSource = dt;
                uoListViewAirport.DataBind();

                Session["AirportContainer"] = dt3;
                Session["AirportList"] = dt;
            }
        
        }  



        private void GetAirportsOFVendorSaved()
        {
            DataTable dt = new DataTable();

            string serviceProvider = GlobalCode.Field2String(Request.QueryString["pid"]);
            dt = VendorMaintenanceBLL.GetServiceProviderAirportbyVendor(serviceProvider);
            if (dt.Rows.Count > 0)
            {

                uoListViewAirportSaved.DataSource = dt;
                uoListViewAirportSaved.DataBind();
            }
            else
            {
                DateTime date = DateTime.Now;
                dt.Rows.Add(new Object[] { 1, 1, " ", " No Record", " ", date, " ", date, 1, 1, 1, "No Record" });
                uoListViewAirportSaved.DataSource = dt;
                uoListViewAirportSaved.DataBind();
            
            }

            //for brand checkboxes
            if(dt.Rows.Count > 0)
            {
                for (var f = 0; f < dt.Rows.Count; f++ )
                {


                   string teststringg =  dt.Rows[f]["colBrandIdInt"].ToString();
                   if (teststringg == "1")
                   {
                       ListItem listItem = this.uoCheckBoxListBrand.Items.FindByText("RCI");

                       if (listItem != null)
                       { 
                           listItem.Selected = true;
                       }
                   }
                   else if (teststringg == "2")
                   {
                       ListItem listItem = this.uoCheckBoxListBrand.Items.FindByText("AZA");

                       if (listItem != null)
                       {
                           listItem.Selected = true;
                       }                   
                   }
                   else if (teststringg == "3")
                   {
                       ListItem listItem = this.uoCheckBoxListBrand.Items.FindByText("CEL");

                       if (listItem != null)
                       {
                           listItem.Selected = true;
                       }
                   }
                   else if (teststringg == "4")
                   {
                       ListItem listItem = this.uoCheckBoxListBrand.Items.FindByText("PUL");

                       if (listItem != null)
                       {
                           listItem.Selected = true;
                       }
                   }
                }
            }


        }

        private void SaveAirports()
        {
            DataTable dt = new DataTable();
           
            dt.Columns.Add("airportID", typeof(int));
            DataRow rowAirport;
            HiddenField uoHiddenFieldAirport;

            DataTable dtBrand = null;
            dtBrand = new DataTable();
            DataColumn colBrandID = new DataColumn("colBrandID", typeof(Int32));
            dtBrand.Columns.Add(colBrandID);
            DataRow rowBrand;
            int portAgent = GlobalCode.Field2Int(Request.QueryString["pid"]);

            try { 
            

                foreach(ListViewItem item in uoListViewAirportSaved.Items)
                {
                    uoHiddenFieldAirport = (HiddenField)item.FindControl("uoHiddenFieldAirportSaved");
                    //uoCheckBoxListBrand = (CheckBoxList)item.FindControl("uoCheckBoxListBrand");
                    UserAirportList AirportItem = new UserAirportList();
                    
                    int airport = GlobalCode.Field2Int(uoHiddenFieldAirport.Value);
                    //get brand and user id of user
                    string stringger = Request.RawUrl.ToString();
                   
                    rowAirport = dt.NewRow();
                    rowAirport["airportID"] = airport;
                    dt.Rows.Add(rowAirport);
                    }
                
                for(int i = 0; i < uoCheckBoxListBrand.Items.Count; i++)
                {
                    if (uoCheckBoxListBrand.Items[i].Selected)
                    {
                        rowBrand = dtBrand.NewRow();
                        rowBrand["colBrandID"] = GlobalCode.Field2Int(uoCheckBoxListBrand.Items[i].Value);
                        dtBrand.Rows.Add(rowBrand);
                    }
                }
                MaintenanceViewBLL.BrandAirportVehicleSave(portAgent, dt, dtBrand, uoHiddenFieldUser.Value);
         
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally{
            
            }
        }

        private void BindPortAgentDetails()
        {
            VendorMaintenanceBLL.GetPortAgentAirportBrand(GlobalCode.Field2Int(uoHiddenFieldPortAgentIDInt.Value), 0);
            
            List<VendorPortAgentDetails> list = new List<VendorPortAgentDetails>();
            List<AirportDTO> listAirport = new List<AirportDTO>();
            uoTextBoxAirport.Text = "";
            uoTextBoxPortAgentName.Text = "";
            string[] sAirportArr = null;

            if(uoHiddenFieldAirportID.Value != "" && uoHiddenFieldAirportID.Value != "0")
            {
                sAirportArr = uoHiddenFieldAirportID.Value.Split("-".ToCharArray());
            }

                if (Session["PortAgentVendorDetails"] != null)
                {
                    list = (List<VendorPortAgentDetails>)Session["PortAgentVendorDetails"];
                    if (list.Count > 0)
                    {
                        uoTextBoxPortAgentName.Text = list[0].PortAgentName;
                         
                        if (Session["PortAgentAirport"] != null)
                        {
                            listAirport = (List<AirportDTO>)Session["PortAgentAirport"];
                            if (listAirport.Count > 0)
                            { 
                                foreach(AirportDTO item in listAirport)
                                {
                                    if (uoTextBoxAirport.Text != "")
                                    {
                                        uoTextBoxAirport.Text += ", ";
                                    }
                                    if (sAirportArr.Contains(item.AirportIDString))
                                    {
                                        //uoTextBoxAirport.Text += item.AirportCodeString;
                                    }
                                }
                            }
                        }
                        
                    }
                }
        }


        #endregion
    }
}
