using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;

using System.Data;

namespace TRAVELMART.Vehicle
{
    public partial class VehicleAssignment : System.Web.UI.Page
    {
        VehicleManifestBLL BLL = new VehicleManifestBLL();
        //private AsyncTaskDelegate _dlgtAssign;
        // Create delegate. 
        //protected delegate void AsyncTaskDelegate(int vendorID);
        
        private AsyncTaskDelegate _dlgtAssign;
        private delegate void AsyncTaskDelegate();

        DriverGreeterVehHotelServProv _result = new DriverGreeterVehHotelServProv(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["vendorID"] != null) uoHiddenFieldVendorID.Value = Request.QueryString["vendorID"].ToString();
                if (Request.QueryString["cIds"] != null) uoHiddenFieldcIds.Value = Request.QueryString["cIds"].ToString();
                if (Request.QueryString["Date"] != null) uoHiddenFieldDate.Value = Request.QueryString["Date"].ToString();
                if (Request.QueryString["cmfid"] != null) uoHiddenFieldcmfid.Value = Request.QueryString["cmfid"].ToString();
                
                if (Request.QueryString["isVeh"] != null) uoHiddenFieldIsVehicle.Value = Request.QueryString["isVeh"].ToString();
                if (Request.QueryString["isGre"] != null) uoHiddenFieldIsGreeter.Value = Request.QueryString["isGre"].ToString();

                uoHiddenFieldsUserID.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);


                uoTextBoxPickupDate.Text = uoHiddenFieldDate.Value;
                PageAsyncTask task = new PageAsyncTask(OnBegin, OnEnd, null, "Async1", true);
                Page.RegisterAsyncTask(task);
            }
        }
          
        public void ExecuteAsyncTaskExceptions() { }
        public IAsyncResult OnBegin(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _dlgtAssign = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _dlgtAssign.BeginInvoke(cb, extraData);
            return result;
        }

        public void OnEnd(IAsyncResult ar)
        {
            DriverGreeterVehHotelServProv lst = new DriverGreeterVehHotelServProv();
            _dlgtAssign.EndInvoke(ar);
            lst = DriverGreeterVehHotelServProv(GlobalCode.Field2Int(uoHiddenFieldVendorID.Value), GlobalCode.Field2String(uoHiddenFieldcmfid.Value));


            uoDropDownListDriver.Items.Clear();
            uoDropDownListDriver.Items.Add(new ListItem("--Select Driver--", "0"));
            uoDropDownListDriver.DataSource = lst.Driver;
            uoDropDownListDriver.DataValueField = "ID";
            uoDropDownListDriver.DataTextField = "FullName";
            uoDropDownListDriver.DataBind();

            uoDropDownListGreeter.Items.Clear();
            uoDropDownListGreeter.Items.Add(new ListItem("--Select Greeter--", "0"));
            uoDropDownListGreeter.DataSource = lst.Greeter;
            uoDropDownListGreeter.DataValueField = "ID";
            uoDropDownListGreeter.DataTextField = "FullName";
            uoDropDownListGreeter.DataBind();

            uoListviewVehicle.DataSource = null;
            uoListviewVehicle.DataBind();

            uoListviewVehicle.DataSource = lst.VehHotelSerProv;
            uoListviewVehicle.DataBind();

            uoListViewManifestConfirm.DataSource = null;
            uoListViewManifestConfirm.DataBind();

            uoListViewManifestConfirm.DataSource = lst.VehicleManifestList;
            uoListViewManifestConfirm.DataBind();

            if (lst.VehicleManifestList.Count > 0 )
            {

                var l = lst.VehicleManifestList[0];

                uoTextBoxPickupDate.Text = GlobalCode.Field2String(l.PickupDate);
                uoTextBoxPickupTime.Text = GlobalCode.Field2DateTime(l.PickupTime.ToString()).Hour.ToString() + ":" + GlobalCode.Field2Time(l.PickupTime.ToString()).Minute.ToString();
                
                uoTextBoxPickupLocation.Text = GlobalCode.Field2String(l.PickupLocation);
                uoTextBoxDropupLocation.Text = GlobalCode.Field2String(l.DropOffLocation);

                
                
                
                uoTextBoxGreeterPickupDate.Text = GlobalCode.Field2String(l.PickupDate);
                uoTextBoxGreeterPickupTime.Text = GlobalCode.Field2DateTime(l.PickupTime.ToString()).Hour.ToString() + ":" + GlobalCode.Field2Time(l.PickupTime.ToString()).Minute.ToString();
                 
                uoTextBoxGreeterLocation.Text = GlobalCode.Field2String(l.PickupLocation);
                uoTextBoxGreeterDropLocation.Text = GlobalCode.Field2String(l.DropOffLocation);




            }
            


        }

        private DriverGreeterVehHotelServProv DriverGreeterVehHotelServProv(int vendorID, string ConfirmedManifestID)
        {
            return BLL.DriverGreeterVehHotelServProv(0, vendorID, ConfirmedManifestID);
        }



        protected void uoButtonSaveDriver_Click(object sender, EventArgs e) 
        {
            try
            {
                InsertDriverTransaction();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void InsertDriverTransaction()
        {
            try
            { 
                 VehicleManifestBLL BLL = new VehicleManifestBLL(); 
                DriverTransaction d = new DriverTransaction();
                 
                DataTable dtparam = DriverTransaction();
                DataTable gtparam = GreeterTransaction();

                DataRow dr;
                
                HiddenField ConfirmedManifestID;
                HiddenField DriverRequestID;
                HiddenField uodfDriverPickupLatitude;
                HiddenField uodfDriverPickupLongitude;
                HiddenField uodfDriverDropOffLatitude;
                HiddenField uodfDriverDropOffLongitude;
                HiddenField uodfDriverParkingLatitude;
                HiddenField uodfDriverParkingLongitude;
                                 

                Label Pickupdate;
                TextBox PickupTime;
                TextBox PickupLocation;
                TextBox DropOffLocation;

               
                foreach (ListViewDataItem list in uoListViewManifestConfirm.Items)
                {
                    
                    ConfirmedManifestID = (HiddenField)list.FindControl("uoHiddenFieldConfirmedManifestID");
                    DriverRequestID = (HiddenField)list.FindControl("uoHiddenFieldDriverRequestID");

                    uodfDriverPickupLatitude = (HiddenField)list.FindControl("uodfDriverPickupLatitude");
                    uodfDriverPickupLongitude = (HiddenField)list.FindControl("uodfDriverPickupLongitude");
                    uodfDriverDropOffLatitude = (HiddenField)list.FindControl("uodfDriverDropOffLatitude");
                    uodfDriverDropOffLongitude = (HiddenField)list.FindControl("uodfDriverDropOffLongitude");
                    uodfDriverParkingLatitude = (HiddenField)list.FindControl("uodfDriverParkingLatitude");
                    uodfDriverParkingLongitude = (HiddenField)list.FindControl("uodfDriverParkingLongitude");


                    Pickupdate = (Label)list.FindControl("lblPickupDate");
                    PickupTime = (TextBox)list.FindControl("txtPickupTime");
                    PickupLocation = (TextBox)list.FindControl("txtPickupLocation");
                    DropOffLocation = (TextBox)list.FindControl("txtDropOffLocation");

                    if (GlobalCode.Field2Int(uoDropDownListDriver.SelectedIndex) > 0)
                    { 
                        dr = dtparam.NewRow();
                        dr["DriverRequestID"] = GlobalCode.Field2Long(DriverRequestID.Value);
                        dr["UserID"] = uoDropDownListDriver.SelectedItem.Value;
                        dr["PickupDate"] = GlobalCode.Field2DateTime(Pickupdate.Text);
                        dr["PickupTime"] = GlobalCode.Field2DateTime(PickupTime.Text); 

                        dr["ParkingLocation"] = GlobalCode.Field2String(uoTextBoxPakingLocation.Text); 
                        dr["ParkingLatitude"] = GlobalCode.Field2Double(uodfDriverParkingLatitude.Value);
                        dr["ParkingLongitude"] = GlobalCode.Field2Double(uodfDriverParkingLongitude.Value); 
                        
                        dr["PickupLocation"] = GlobalCode.Field2String(PickupLocation.Text);
                        dr["PickupLatitude"] = GlobalCode.Field2Double(uodfDriverPickupLatitude.Value);
                        dr["PickupLongitude"] = GlobalCode.Field2Double(uodfDriverPickupLongitude.Value);
                        
                        dr["DropOffLocation"] = GlobalCode.Field2String(DropOffLocation.Text);
                        dr["DropOffLatitude"] = GlobalCode.Field2Double(uodfDriverDropOffLatitude.Value);
                        dr["DropOffLongitude"] = GlobalCode.Field2Double(uodfDriverParkingLongitude); 

                        dr["VehicleDetailID"] = GlobalCode.Field2Int(uoHiddenFieldVehicleHotelSPID.Value);
                        dr["ConfirmedManifestID"] = GlobalCode.Field2Long(ConfirmedManifestID.Value);      

                        dtparam.Rows.Add(dr);
                    }


                    if (GlobalCode.Field2Int(uoDropDownListGreeter.SelectedIndex) > 0)
                    { 
                        dr = gtparam.NewRow();
                        dr["GreeterRequestID"] = GlobalCode.Field2Long(DriverRequestID.Value);
                        dr["UserID"] = uoDropDownListGreeter.SelectedItem.Value;
                        dr["PickupDate"] = GlobalCode.Field2DateTime(Pickupdate.Text);
                        dr["PickupTime"] = GlobalCode.Field2DateTime(PickupTime.Text);
                        dr["PickupLocation"] = GlobalCode.Field2String(PickupLocation.Text);
                        dr["PickupLatitude"] = GlobalCode.Field2Double(uodfGreeterPickupLatitude.Value);
                        dr["PickupLongitude"] = GlobalCode.Field2Double(uodfGreeterPickupLongitude.Value);
                        dr["DropOffLocation"] = GlobalCode.Field2String(DropOffLocation.Text);
                        dr["DropOffLatitude"] = GlobalCode.Field2Double(uodfGreeterDropOffLatitude.Value);
                        dr["DropOffLongitude"] = GlobalCode.Field2Double(uodfGreterDropOffLongitude.Value);
                        dr["ConfirmedManifestID"] = GlobalCode.Field2Long(ConfirmedManifestID.Value);
                        gtparam.Rows.Add(dr);
                    }
                }

                BLL.SaveDriverTransaction(GlobalCode.Field2Int(uoHiddenFieldVendorID.Value),  uoHiddenFieldsUserID.Value, uoHiddenFieldcmfid.Value , dtparam, gtparam);


                OpenParentPage("Created successfully.");
            
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable DriverTransaction()
        { 
                
             DataTable table = new DataTable();
             table.Columns.Add("DriverRequestID",  Type.GetType("System.Int64"));
             table.Columns.Add("UserID", Type.GetType("System.String"));
             table.Columns.Add("PickupDate", Type.GetType("System.DateTime")); 
             table.Columns.Add("PickupTime", Type.GetType("System.DateTime")); 
             table.Columns.Add("ParkingLocation", Type.GetType("System.String")); 
             table.Columns.Add("ParkingLatitude", Type.GetType("System.Double")); 
             table.Columns.Add("ParkingLongitude", Type.GetType("System.Double"));
             table.Columns.Add("PickupLocation", Type.GetType("System.String")); 
             table.Columns.Add("PickupLatitude", Type.GetType("System.Double")); 
             table.Columns.Add("PickupLongitude", Type.GetType("System.Double")); 
             table.Columns.Add("DropOffLocation", Type.GetType("System.String")); 
             table.Columns.Add("DropOffLatitude", Type.GetType("System.Double")); 
             table.Columns.Add("DropOffLongitude", Type.GetType("System.Double"));
             table.Columns.Add("VehicleDetailID", Type.GetType("System.Int32"));   
             table.Columns.Add("ConfirmedManifestID", Type.GetType("System.Int64"));

             return table;
        }



        public DataTable GreeterTransaction()
        {

            DataTable table = new DataTable(); 

            table.Columns.Add("GreeterRequestID", Type.GetType("System.Int64"));
            table.Columns.Add("UserID", Type.GetType("System.String"));
            table.Columns.Add("PickupDate", Type.GetType("System.DateTime"));
            table.Columns.Add("PickupTime", Type.GetType("System.DateTime"));
            table.Columns.Add("PickupLocation", Type.GetType("System.String"));
            table.Columns.Add("PickupLatitude", Type.GetType("System.Double"));
            table.Columns.Add("PickupLongitude", Type.GetType("System.Double"));
            table.Columns.Add("DropOffLocation", Type.GetType("System.String"));
            table.Columns.Add("DropOffLatitude", Type.GetType("System.Double"));
            table.Columns.Add("DropOffLongitude", Type.GetType("System.Double")); 
            table.Columns.Add("ConfirmedManifestID", Type.GetType("System.Int64"));
            
            return table;
        }






       

        private void OpenParentPage(string s)
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_uoHiddenFieldServicePopup\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            if (s != "")
            {
                s = s.Replace("'", " ");
                sScript += " alert('" + s + "'); ";
            }
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(uoButtonSaveDriver, this.GetType(), "scr", sScript, false);

        }





        ///// <summary>
        ///// Author:             Marco Abejar
        ///// Date Created:       10/10/2013
        ///// Description:        Bind Driver
        ///// ------------------------------------
        ///// </summary>
        //private void BindDriver()
        //{
        //    uoDropDownListDriver.Items.Clear();
        //    uoDropDownListDriver.Items.Add(new ListItem("--SELECT DRIVER--", "0"));
        //    List<Driver> list = new List<Driver>();
        //    list = VehicleBLL.vehicleDriver(uoHiddenFieldVendorID.Value);
        //    uoDropDownListDriver.DataSource = list;
        //    uoDropDownListDriver.DataTextField = "DriverName";
        //    uoDropDownListDriver.DataValueField = "DriverId";
        //    uoDropDownListDriver.DataBind();
        //}
        ///// <summary>
        ///// Author:             Marco Abejar
        ///// Date Created:       10/10/2013
        ///// Description:        Bind Driver
        ///// ------------------------------------
        ///// </summary>
        //private void BindVehicleType()
        //{
        //    uoDropDownListDriver.Items.Clear();
        //    uoDropDownListDriver.Items.Add(new ListItem("--SELECT TYPE--", "0"));
        //    List<VehicleType> list = new List<VehicleType>();
        //    //list = VehicleBLL.vendorvehicleType();

        //    VendorMaintenanceBLL.VehicleVendorsVehicleTypeGet(0, GlobalCode.Field2Int(uoHiddenFieldVendorID.Value), false, 1);
        //    uoDropDownListDriver.DataSource = list;
        //    uoDropDownListDriver.DataValueField = "VehicleTypeID";
        //    uoDropDownListDriver.DataTextField = "VehicleTypeName";
        //    uoDropDownListDriver.DataBind();
        //}

        //protected void uoButtonAssign_Click(object sender, EventArgs e)
        //{
        //    VehicleBLL.vendorAssignVehicle(Request.QueryString["status"].ToString(), uoHiddenFieldCrewIDS.Value, uoDropDownListVehicle.SelectedValue, uoDropDownListDriver.SelectedValue, uoDropDownListVehiclePlate.SelectedValue, txtDispatchTime.Text, uoHiddenFieldUser.Value);
        //    OpenParentPage();
        //}


        //private void OpenParentPage()
        //{
        //    /// <summary>
        //    /// Date Created: 10/10/2013
        //    /// Created By: Marco Abejar
        //    /// (description) Close this page and update parent page            
        //    /// </summary>

        //    string sScript = "<script language='javascript'>";
        //    sScript += " window.parent.$(\"#ctl00_uoHiddenFieldServicePopup\").val(\"1\"); ";
        //    sScript += " parent.$.fancybox.close(); ";
        //    sScript += "</script>";

        //    ScriptManager.RegisterClientScriptBlock(uoButtonAssign, this.GetType(), "scr", sScript, false);
        //}
        //protected void uoDropDownListVehicle_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    uoDropDownListVehiclePlate.Items.Clear();
        //    uoDropDownListVehiclePlate.Items.Add(new ListItem("--SELECT PLATE--", "0"));
        //    List<VehiclePlate> list = new List<VehiclePlate>();
        //    list = VehicleBLL.vendorvehiclePlate(uoHiddenFieldVendorID.Value, uoDropDownListVehicle.SelectedValue);
        //    uoDropDownListVehiclePlate.DataSource = list;
        //    uoDropDownListVehiclePlate.DataValueField = "VehiclePlateID";
        //    uoDropDownListVehiclePlate.DataTextField = "VehiclePlateName";
        //    uoDropDownListVehiclePlate.DataBind();
        //}

        //protected void uoLinkButtonAddDriver_Click(object sender, EventArgs e)
        //{
        //    img1.Visible = false;
        //    lblTitle.Text = "ADD DRIVER";
        //    tblSaveVehicle.Visible = false;
        //    tblAddDriver.Visible = true;
        //    BindVehicleType();
        //    BindDriver();
        //}

        //protected void uoButtonSaveDriver_Click(object sender, EventArgs e)
        //{
        //    img1.Visible = true;
        //    lblTitle.Text = "ASSIGN VEHICLE";
        //    tblSaveVehicle.Visible = true;
        //    tblAddDriver.Visible = false;
        //    if ((uoFileUploadDriver.PostedFile != null) && (uoFileUploadDriver.PostedFile.ContentLength > 0))
        //    {
        //        if (!Directory.Exists(Server.MapPath("DriverPhoto")))
        //        {
        //            DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("DriverPhoto"));
        //        }
        //        string fn = System.IO.Path.GetExtension(uoFileUploadDriver.PostedFile.FileName);
        //        string SaveLocation = Server.MapPath("DriverPhoto") + "\\" + uoTextBoxDriverName.Text.Trim().Replace(" ","_") + fn;
        //        try
        //        {
        //            uoFileUploadDriver.PostedFile.SaveAs(SaveLocation);
        //            VehicleBLL.vendorAddDriver(uoHiddenFieldVendorID.Value, uoTextBoxDriverName.Text, uoTextBoxDriverName.Text.Trim().Replace(" ", "_") + fn, uoHiddenFieldUser.Value);
        //            BindVehicleType();
        //            BindDriver();

        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    else
        //    {
        //        string sScript = "<script language='JavaScript'>";
        //        sScript += "alert('Please select file to upload.');";
        //        sScript += "</script>";                
        //        ScriptManager.RegisterClientScriptBlock(uoButtonSaveDriver, this.GetType(), "scr", sScript, false);
        //    }
        //}

        //protected void uoButtonCancel_Click(object sender, EventArgs e)
        //{
        //    img1.Visible = true;
        //    lblTitle.Text = "ASSIGN VEHICLE";
        //    tblSaveVehicle.Visible = true;
        //    tblAddDriver.Visible = false;
        //}

        //protected void uoDropDownListDriver_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    List<Driver> list = new List<Driver>();
        //    list = VehicleBLL.vehicleDriver(uoHiddenFieldVendorID.Value);
        //    var driver = (from i in list.Where(i => i.DriverId == Convert.ToInt32(uoDropDownListDriver.SelectedValue))
        //                  select i).FirstOrDefault();


        //    if (File.Exists( Server.MapPath("DriverPhoto") + "\\" + driver.DriverImage.ToString()) && uoDropDownListDriver.SelectedIndex > 0)
        //        img1.Attributes["src"] = "~\\Vehicle\\DriverPhoto\\" + driver.DriverImage.ToString();
        //        //imgDriver.ImageUrl = Server.MapPath("DriverPhoto") + "\\" + driver.DriverImage.ToString();
        //    else
        //        img1.Attributes["src"] = "~\\Vehicle\\DriverPhoto\\default.jpg";
        //        //imgDriver.ImageUrl = Server.MapPath("DriverPhoto") + "\\default.jpg";
        //}


    }
}
