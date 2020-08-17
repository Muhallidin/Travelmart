<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master" AutoEventWireup="true"
    CodeBehind="PortAgentPriority.aspx.cs" Inherits="TRAVELMART.PortAgentPriority" %>

<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">
    <style type="text/css">
        .style1
        { 
            width: 100px;
            white-space:nowrap; 
            text-align:left;
            padding-left:5px;
        }  
          .style2
        {
            width: 400px;
            white-space:nowrap;
            text-align:left;
            padding-left:5px;
        }      
    </style>
    
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="PageTitle" >
            <td align="left" style="height: 25px; vertical-align:middle">
                 &nbsp;Service Provider : Brand - Airport Assignment
            </td>
            <td class="RightClass">
                    <asp:Button runat="server" id="uoButtonAdd" Text="Add" Width ="80px" CssClass="SmallButton" onclick="uoBtnPortAgentAdd_Click"/>
            </td>           
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
     <asp:HiddenField ID="uoHiddenFieldVendor" runat="server" Value="true" />
     <asp:HiddenField ID="uoHiddenFieldContractList" runat="server" Value="true" />
        <asp:HiddenField ID="uoHiddenFieldPriority" runat="server" Value="" /> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <table width="100%" style="text-align:left" >
       <tr>
            <td class="style1">
                Region:
            </td>
            <td class="style2">
                <asp:DropDownList runat="server" ID="uoDropDownListRegion" 
                    AppendDataBoundItems="true" Width="300px" AutoPostBack="true" 
                    onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="style1">
                Airport:
            </td>
            <td class="style2">
                <asp:DropDownList runat="server" ID="uoDropDownListAirport" AppendDataBoundItems="true" Width="400px"></asp:DropDownList>
            </td>       
       </tr>
       <tr>
            <td class="style1">
                Brand:
            </td>
            <td class="style2">
                <asp:DropDownList runat="server" ID="uoDropDownListBrand" AppendDataBoundItems="true" Width="300px"></asp:DropDownList>
            </td>
            <td class="style1">Service Provider Vendor Name:</td>
            <td class="style2">
                <asp:TextBox ID="uoTextBoxSearchParam" runat="server" Font-Size="7.5pt" Width="250px" Text=""/>                
                &nbsp;
                <asp:Button ID="uoButtonSearch" runat="server" Text="Search" Width="80px"
                                    Font-Size="X-Small" onclick="uoButtonSearch_Click" />                 
            </td>        
       </tr>
       <tr>
            <td colspan="6">
                 <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;" align="right">
                    <table width="100%" style="text-align:left">
                    <tr>
                        <td class="Module" align="right">
                            <asp:ListView runat="server" ID="uoListViewPortAgent" OnItemCommand="uoListViewPortAgent_ItemCommand"
                                OnItemDeleting="uoListViewPortAgent_ItemDeleting" 
                                onitemdatabound="uoListViewPortAgent_ItemDataBound" >
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                             <th style="text-align: center; white-space: normal;">
                                                    <asp:LinkButton ID="uoLinkButtonHeaderPortAgentName" runat="server"
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
                                             <th style="text-align: center; white-space: normal; width:60px ">
                                                Email Address
                                            </th> 
                                  <%--          <th style="text-align: center; white-space: normal; width:60px " visible="false" >
                                               Brand
                                            </th>--%>
                                            <%--<th runat="server" style="width:120px" id="PriorityTH"/>   --%>                             
                                            <th runat="server" style="width: 80px" id="EditTH" />         
                                            <th runat="server" style="width: 120px" id="EditAssignTH" visible="false" />         
                                            <th runat="server" style="width: 100px" id="ContractTH" />
                                            <th runat="server" style="width: 100px" id="AddContractTH" />
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="leftAligned">
			                                <asp:LinkButton runat="server" Width="300px" id="uoLinkButtonContract" Text='<%# Eval("PortAgentName") %>' OnClientClick = '<%# "return OpenContract(\""+ Eval("PortAgentID") + "\",\"" + Eval("ContractID") + "\");"  %>' Visible='<%# Convert.ToBoolean(Eval("IsWithContract")) %>' ></asp:LinkButton>
			                                <asp:Label runat="server" Width="300px" ID="uoLabelPortAgentName" Text='<%# Eval("PortAgentName") %>' Visible='<%# !Convert.ToBoolean(Eval("IsWithContract")) %>' ></asp:Label>
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
                                        <td class="leftAligned" style="width:60px; white-space:normal">
                                            <%# Eval("EmailTo")%>                        
                                        </td>  
                                    <%--    <td class="leftAligned" style="width:60px; white-space:normal" visible="false">
                                            <%# Eval("BrandCode")%>                        
                                        </td>--%>
                     <%--                   <td class="leftAligned" style="width:120px; white-space:normal;">
                                        
                                        </td> --%> 
                                        <td>
                                             <a runat="server"  id="uoAEditvehicle" href='<%# "~/Maintenance/PortAgentMaintenance.aspx?vmId=" + Eval("PortAgentID") + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]  %>'
                                            visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>'  >
                                                Edit</a>
                                        </td>     
              <%--                          <td visible="false">
                                            <a runat="server" class="clsEditAssignment"  id="uoAEditVehicleAssignment" href='<%# "~/Maintenance/PortAgentBrandPopup.aspx?pid="+ Eval("PortAgentID") + "&aid=1"%>' visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>'>Edit Assignment</a>
                                        </td>--%>
                                        <td>
                                             <a runat="server" style=' <%= (uoHiddenFieldVendor.Value == "true") %> ? " ": "display:none;" '
                                                id="A1" href='<%# "../ContractManagement/PortAgentContractListView.aspx?vmId=" + Eval("PortAgentID") + "&hbName=" + Eval("PortAgentName") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'>
                                                Contract List</a>
                                                
                                              
                                        </td>    
                                        <td>
                                            <a runat="server" id="uoAddContract" href='<%# "../ContractManagement/PortAgentContractAdd.aspx?vmId=" + Eval("PortAgentID") +  "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
                                                visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>'>Add Contract</a>
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
                             <asp:ObjectDataSource ID="uoObjectDataSourcePortAgent" runat="server" MaximumRowsParameterName="iMaxRow"
                                    SelectCountMethod="GetPortAgentListByAirportBrandCount" SelectMethod="GetPortAgentListByAirportBrand"
                                    StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.BLL.MaintenanceViewBLL"
                                    OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True"  >
                                    <SelectParameters>
                                        <asp:ControlParameter Name="sPortAgentVendor" Type="String" ControlID="uoTextBoxSearchParam" />
                                        <asp:ControlParameter Name="sOrder" Type="String" ControlID="uoHiddenFieldSortBy" />
                                        <asp:ControlParameter Name="iRegionID" Type="Int32" ControlID="uoDropDownListRegion" />
                                        <asp:ControlParameter Name="iAirportID" Type="Int32" ControlID="uoDropDownListAirport" />
                                        <asp:ControlParameter Name="iBrandID" Type="Int32" ControlID="uoDropDownListBrand" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftClass">
                            <asp:DataPager ID="uoListViewPortAgentPager" runat="server" PagedControlID="uoListViewPortAgent"
                                PageSize="20" OnPreRender="uoListViewPortAgentPager_PreRender">
                                <Fields>
                                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
                </div>
            </td>
       </tr>
    </table>
    <br />
    
    <div></div>
    <asp:HiddenField runat="server" ID="uoHiddenFieldUser"/>
     <asp:HiddenField runat="server" ID="uoHiddenFieldSortBy" Value=""/>
     <asp:HiddenField ID="uoHiddenFieldPopup" runat="server" Value="0" />

    
         <script type="text/javascript" language="javascript">
          Sys.Application.add_load(function() {
                controlSettings();
                SetBranchResolution();
            });

            function controlSettings() {
                $(".clsEditAssignment").fancybox(
            {
                'width': '80%',
                'height': '80%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopup.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });
            $("#uoPortAgentLink").fancybox(
            {
                'width': '80%',
                'height': '80%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopup.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });            
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
        function OpenContract(branchID, contractID) {
            var URLString = "../ContractManagement/portAgentContractView.aspx?";
            URLString += "bId=" + branchID + "&cId=" + contractID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
    </script>
    
</asp:Content>