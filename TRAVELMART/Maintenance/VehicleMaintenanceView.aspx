<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master" AutoEventWireup="true"
    CodeBehind="VehicleMaintenanceView.aspx.cs" Inherits="TRAVELMART.VehicleMaintenanceView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript" language="javascript">
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        Sys.Application.add_load(function() {
            SetBranchResolution();
            HideFilterHeader();
        });

        $(document).ready(function() {
            HideFilterHeader();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                HideFilterHeader();
            }
        }


        function SetBranchResolution() {
            var ht = $(window).height(); //550;
            var ht2 = $(window).height(); //550;
            var wd = $(window).width() * 0.90;
            if (screen.height <= 600) {
                ht = ht * 0.47;
                ht2 = ht2 * 0.47;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.52;
                ht2 = ht2 * 0.52;
            }
            else {
                ht = ht * 0.55;
                ht2 = ht2 * 0.68;
            }

            $("#Bv").width(wd);
            $("#Bv").height(ht);

            $("#PG").width(wd);
            $("#PG").height(ht2);
        }

        function OpenContract(vendorID, ContractID) {
            var URLString = "../ContractManagement/VehicleContractView.aspx?bId=" + vendorID + "&cId=" + ContractID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }


        function HideFilterHeader() {

            console.log(document.getElementById("ctl00_uoHiddenFieldRole").value) ;
            var role = document.getElementById("ctl00_uoHiddenFieldRole").value;
            $(".FilterControl").css('display', '');
            document.getElementById("ctl00_HeaderContent_uoBtnVehicleAdd").style.display = "block";
            if (role == "Vehicle Vendor") {
                $(".FilterControl").css('display', 'none');
                document.getElementById("ctl00_HeaderContent_uoBtnVehicleAdd").style.display = "none";
            }
          document.getElementById("tdBtnVehicleAdd").style.textAlign = 'right';
            
        }
        
    </script>

    <div id="PG" style="overflow: auto; overflow-x: auto; overflow-y: auto;">
    <table width="100%" id="uotableVehicleVendorList" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr style="width:100%">
                            <td style="width: 40%; text-align: left;" class="PageTitle" colspan="3">
                                <asp:Panel ID="uopanelvehiclehead" runat="server">
                                    Vehicle Vendor List
                                    <asp:Label ID="uolabelVehicle" runat="server" Text="" Font-Size="Small" />
                                </asp:Panel>
                            </td>
                            <td id="tdBtnVehicleAdd" style=" padding-right: 3px;  background-color: #82ABB8; text-align:right; "  colspan="1">   
                                
                                <table style="width:100%" >
                                    <tr style="width:100%" > 
                                        <td  style=" padding-right: 3px; width:100%">
                                            <asp:Button ID="uoBtnVehicleAdd" runat="server" Text="Add" Font-Size="X-Small" Width="100px"  
                                                OnClick="uoBtnVehicleAdd_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr><td colspan="2"></td></tr>
                        
                        <tr class="FilterControl">
                            <td class="contentCaptionOrig" style="width:100px">
                                Region:
                            </td>
                            <td class="contentValueOrig">
                                <asp:DropDownList ID="uoDropDownListRegion" runat="server"  Width="300px" AppendDataBoundItems="true"
                                    AutoPostBack="true" onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged"
                                    >
                                </asp:DropDownList >
                            </td>
                            <td class="contentCaptionOrig" style="width:100px">
                                Seaport:
                            </td>
                            <td class="contentValueOrig">
                                <asp:DropDownList ID="uoDropDownListSeaport" runat="server" Width="300px" AppendDataBoundItems="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        
                         <tr  class="FilterControl">
                            <td class="contentCaptionOrig" style="width:130px">
                                Brand:
                            </td>
                            <td class="contentValueOrig">
                                <asp:DropDownList ID="uoDropDownListBrand" runat="server"  Width="300px" AppendDataBoundItems="true">
                                </asp:DropDownList>
                            </td>
                            <td class="contentCaptionOrig" style="width:130px">
                               Vehicle Vendor Name:
                            </td>
                            <td class="contentValueOrig" style="width:400px; white-space:nowrap">
                               <asp:TextBox ID="uoTextBoxSearchParam" runat="server" Font-Size="7.5pt" 
                                    Width="300px" Text="" /> &nbsp;
                               <asp:Button ID="uoButtonSearch" runat="server" Text="Search" Width="70px"
                                    Font-Size="X-Small" onclick="uoButtonSearch_Click" />
                            </td>
                        </tr>                        
                    </table>
                </div>
            </td>
        </tr>
        </table>
        
        <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;" align="right">
        <table width="100%" style="text-align:left">
        <tr>
            <td class="Module" align="right">
                <asp:ListView runat="server" ID="uoVehicleVendorList" OnItemCommand="uoVehicleVendorList_ItemCommand"
                    OnItemDeleting="uoVehicleVendorList_ItemDeleting" ondatabound="uoVehicleVendorList_DataBound" 
                     
                    >
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                 <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLinkButtonHeaderVendorName" runat="server"
                                            CommandName="SortByName">Vendor Name</asp:LinkButton>
                                </th>                            
                                 <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton  ID="LinkButton1" runat="server"
                                            CommandName="SortByCOuntry">Country</asp:LinkButton>
                                </th>                            
                                <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton2" runat="server"
                                            CommandName="SortByCity">City</asp:LinkButton>                                
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    Contact No.
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    Fax No.
                                </th>
                                 <th style="text-align: center; white-space: normal;  width:300px">
                                    Email Address
                                </th>                                
                                <th runat="server" style="width: 150px" id="EditTH" >  Edit</th>
                                <th runat="server" style="width: 170px" id="ContractTH" >Contract</th>
                                <th runat="server" style="width: 170px" id="AddContractTH" > Add Contract</th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">                                
                                <asp:LinkButton runat="server" ID="uoLinkButtonContract" Text='<%# Eval("VendorName") %>'                                    
                                    OnClientClick='<%# "return OpenContract(\"" + Eval("VehicleID") + "\",\"" + Eval("colContractIdInt") + "\")" %>'
                                    Visible='<%# Eval("IsWithContract") %>'>                                
                                </asp:LinkButton>
                                <asp:Label ID="uoLabelVendor" runat="server" Text='<%# Eval("VendorName") %>'
                                    Visible='<%# !(Boolean)Eval("IsWithContract") %>'></asp:Label>
                                    
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Country")%>
                            </td>                           
                            <td class="leftAligned">
                                <%# Eval("City")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("ContactNo")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("FaxNo")%>
                            </td>
                            <td class="leftAligned" style="width:300px; white-space:normal">
                                <%# Eval("EmailTo")%>                        
                            </td>  
                           <%--  visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>'--%>
                            <td class ='<%# Eval("CssEditContractVisible") %>'>
                                 <a runat="server"  id="uoAEditvehicle" href='<%# "~/Maintenance/VehicleMaintenance.aspx?vmId=" + Eval("VehicleID") + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]  %>'
                                       
                                    >Edit</a>
                            </td>     
                             <td class ='<%# Eval("CssContractListVisible") %>'  >
                                 <a runat="server" visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value))  %>'
                                    id="A1" href='<%# "../ContractManagement/VehicleContractListView.aspx?vmId=" + Eval("VehicleID") + "&hbName=" + Eval("VendorName") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'>
                                    Contract List</a>
                            </td>    
                             <td class ='<%# Eval("CssAddContractVisible") %>'>
                               <%--  visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>'--%>
                                <a class ='<%# Eval("CssAddContractVisible") %>' runat="server" id="uoAddContract" href='<%# "../ContractManagement/VehicleContractAdd.aspx?vmId=" + Eval("VehicleID") +  "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
                                
                                    >Add Contract</a>
                            </td>                 
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    Vendor Name
                                </th>                                
                                <th>
                                    Country
                                </th>
                                <th>
                                    City
                                </th>
                                <th>
                                    Contact No.
                                </th>
                                <th>
                                    Fax No.
                                </th>
                                 <th>
                                    Email Address
                                </th>                                                                
                            </tr>
                            <tr>
                                <td colspan="6" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>                
                 <asp:ObjectDataSource ID="uoObjectDataSourceVehicle" runat="server" MaximumRowsParameterName="iMaxRow"
                        SelectCountMethod="VehicleVendorsGetCount" SelectMethod="VehicleVendorsGet"
                        StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.BLL.MaintenanceViewBLL"
                        OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" OnSelecting="uoObjectDataSourceVehicle_Selecting" >
                        <SelectParameters>
                            <asp:ControlParameter Name="iLoadType" Type="Int16" ControlID="uoHiddenFieldLoadType" />
                            <asp:ControlParameter Name="iRegionID" Type="Int32" ControlID="uoDropDownListRegion" />
                            <asp:ControlParameter Name="iSeaportID" Type="Int32" ControlID="uoDropDownListSeaport" />
                            <asp:ControlParameter Name="iBrandID" Type="Int16" ControlID="uoDropDownListBrand" />
                            
                            <asp:Parameter Name="sVehicleVendorName" Type="String"/>
                            <asp:ControlParameter Name="sOrderyBy" Type="String" ControlID="uoHiddenFieldSortBy" />
                            <asp:ControlParameter Name="sUserID" Type="String" ControlID="uoHiddenFieldUser" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                <asp:DataPager ID="uoVehicleVendorListPager" runat="server" PagedControlID="uoVehicleVendorList"
                    PageSize="20" OnPreRender="uoVehicleVendorListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
    </table>
    </div>
    <asp:HiddenField ID="uoHiddenFieldPopupVehicle" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldVendor" runat="server" Value="true" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server"  />
    <asp:HiddenField ID="uoHiddenFieldShowEditContractAddCol" runat="server" Value="true" />    
    
    <asp:HiddenField ID="uoHiddenFieldSortBy" runat="server" Value="SortByName" />
    <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0" />
    
    </div>
</asp:Content>
