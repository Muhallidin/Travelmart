<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true"
    CodeBehind="SuperUser.aspx.cs" Inherits="TRAVELMART.SuperUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }

        function confirmApprove() {
            if (confirm("Approve record?") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("a#<%=uoHyperLinkAirAdd.ClientID %>").fancybox({
                'width': '40%',
                'height': '85%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupAir.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }

            });

            $(".AirLink").fancybox(
            {
                'width': '40%',
                'height': '85%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupAir.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });
            $("a#<%=uoHyperLinkVehicleAdd.ClientID %>").fancybox(
            {
                'width': '45%',
                'height': '110%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupVehicle.ClientID %>").val();
                    var b = $("#<%=uoHiddenFieldPopupVehiclePending.ClientID %>").val();
                    if (a == '1' || b == '1') {
                        $("#aspnetForm").submit();
                    }
                }
            });
            $(".VehicleLink").fancybox(
            {
                'width': '45%',
                'height': '110%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupVehicle.ClientID %>").val();
                    var b = $("#<%=uoHiddenFieldPopupVehiclePending.ClientID %>").val();
                    if (a == '1' || b == '1') {
                        $("#aspnetForm").submit();
                    }
                }
            });
            $(".VehicleLinkPending").fancybox(
            {
                'width': '45%',
                'height': '110%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupVehiclePending.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });
            $("a#<%=uoHyperLinkHotelAdd.ClientID %>").fancybox(
            {
                'width': '87%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupHotel.ClientID %>").val();
                    var b = $("#<%=uoHiddenFieldPopupHotelPending.ClientID %>").val();
                    if (a == '1' || b == '1') {
                        $("#aspnetForm").submit();
                    }
                }
            });
            $(".HotelLink").fancybox(
            {
                'width': '87%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupHotel.ClientID %>").val();
                    var b = $("#<%=uoHiddenFieldPopupHotelPending.ClientID %>").val();
                    if (a == '1' || b == '1') {
                        $("#aspnetForm").submit();
                    }
                }
            });
            $(".HotelLinkPending").fancybox(
            {
                'width': '80%',
                'height': '95%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupHotelPending.ClientID %>").val();
                    if (a == '1') {
                        $("#aspnetForm").submit();
                    }
                }
            });
            $(".PortLink").fancybox
            ({
                'width': '40%',
                'height': '63%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupPort.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });

            $("a#<%=uoHyperLinkReimbursementAdd.ClientID %>").fancybox({
                'width': '55%',
                'height': '65%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupReimbursement.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }

            });

            $(".ReimbursementLink").fancybox(
            {
                'width': '55%',
                'height': '65%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupReimbursement.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });

            $(".ReimbursementLinkView").fancybox(
            {
                'width': '55%',
                'height': '60%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe'
            });
        });
           
    </script>
    
    <script type="text/javascript" language="javascript">
        function OpenContract(vendorID, VendorType) {

            var URLString = "../ContractManagement/";
            if (VendorType == 'HO') {
                URLString += "HotelContractView.aspx?vId=";
            }
            else {
                URLString += "VehicleContractView.aspx?bId=";
            }
            URLString += vendorID + "&vmType=" + VendorType;            
            
            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
    </script>

    <script type="text/javascript" language="javascript">
        function OpenEventsList(branchID, cityID, OnOffDate) {
            var URLString = "../Maintenance/EventsList.aspx?bId=";
            URLString += branchID;
            URLString += "&cityId=" + cityID;
            URLString += "&Date=" + OnOffDate;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 600;
            screenHeight = 400;

            window.open(URLString, 'Events_List', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
    </script>

    <table width="100%">
        <tr>
            <td colspan="6">
                <div class="PageTitle">
                    Seafarer Information</div>
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width: 85px; padding-left: 5px;">
                E1 ID :</td>
            <td class="LeftClass" style="width: 250px; font-weight: bold">
                <asp:Label ID="uclabelE1ID" runat="server" />
            </td>
            <td class="LeftClass" style="width: 100px">
                Onsigning :
            </td>
            <td class="LeftClass" style="width: 200px; font-weight: bold">
                <asp:Label ID="ucLabelSignOn" runat="server" />
            </td>
            <td class="LeftClass" style="width: 100px">
                Offsigning :
            </td>
            <td class="LeftClass" style="width: 200px; font-weight: bold">
                <asp:Label ID="ucLabelSignOff" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="padding-left: 5px">
                Name :
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelName" runat="server" />
            </td>
            <td class="LeftClass">
                Brand :&nbsp;
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelBrand" runat="server" />
            </td>
            <td class="LeftClass">
                Civil Status :
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelCivilStatus" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="padding-left: 5px">
                Rank :
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelRank" runat="server" />
            </td>
            <td class="LeftClass">
                Ship :
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelVessel" runat="server" />
            </td>
            <td class="LeftClass">
                Status:</td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelStatus" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="padding-left: 5px">
                Nationality :
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelNationality" runat="server" />
            </td>
            <td class="LeftClass">
                Gender :</td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelGender" runat="server" />
            </td>
            <td class="LeftClass">
                <asp:Label ID="ucLabelRecordLocator" runat="server" Text="Record Locator:"></asp:Label>
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="ucLabelrecloc" runat="server" />
            </td>
        </tr>
         
        </table>
    <table width="100%" cellspacing="0" border="0">
        <tr>                        
             <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 30%; text-align: left;" class="PageTitle">
                                Air Travel Information
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                <a id="uoHyperLinkAirAdd" runat="server" href="~/Air/AirEditor.aspx" >
                                    <asp:Button ID="uoButtonAirAdd" runat="server" Text="Add" CssClass="SmallButton" Visible="false" />
                                </a>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="Module">               
                <asp:ListView runat="server" ID="uoListViewAirTravelInfo">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <%--<th>Seq No.</th>--%>
                                <th>City Pair</th>                           
                                <th>Departure Date/Time</th>
                                <th>Arrival Date/Time</th>
                                <th>Airline</th> 
                                <th>Flight No.</th>                                                     
                                <th>Status</th> 
                                <th runat="server" id = "EditTH"></th>                                                                                
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>                    
                    <ItemTemplate>
                        <tr class='<%# (InactiveRow( Eval("IsCurrent") )) %>'>                      
                            <%--<td ><%# Eval("colSeqNoInt")%></td>--%>
                            <td class="leftAligned"><%# Eval("citypair")%></td>
                            <td class="leftAligned"><%# string.Format("{0:dd-MMM-yyyy HHmm}", Eval("departure"))%></td>
                            <td class="leftAligned"><%# string.Format("{0:dd-MMM-yyyy HHmm}", Eval("arrival"))%></td>
                            <td class="leftAligned"><%# Eval("colMarketingAirlineCodeVarchar")%></td>
                            <td class="leftAligned"><%# Eval("flightNo")%></td>
                            <td class="leftAligned"><%# Eval("status")%></td>
                            <td class='<%# (HideAir()==""?"leftAligned":HideAir()) %>'  >
                               <asp:HyperLink runat="server" Enabled='<%# !InactiveControl( Eval("IsCurrent"),"","") %>' CssClass='<%# (InactiveControl( Eval("IsCurrent"),"","")== true?"":"AirLink") %>'  id="uoHyperLinkAirEdit" NavigateUrl='<%# "~/Air/AirEditor.aspx?sfId=" + Eval("sfID")  +  "&st=" + Request.QueryString["st"] + "&recloc=" + Request.QueryString["recloc"] + "&ID=" + Eval("colIdBigint") + "&SN=" + Eval("colSeqNoInt") %>'>
                                    Edit</asp:HyperLink>                                 
                            </td> 
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                           <%-- <th>Seq No.</th>--%>
                            <th>City Pair</th>                           
                            <th>Departure Date/Time</th>
                            <th>Arrival Date/Time</th>
                            <th>Airline</th> 
                            <th>Flight No.</th>                                                     
                            <th>Status</th>                               
                        </tr>
                        <tr>
                            <td colspan="6" class="leftAligned">
                                No Record
                            </td>
                        </tr>                        
                        </table>                    
                    </EmptyDataTemplate>
                </asp:ListView>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 30%; text-align: left;" class="PageTitle">
                                Vehicle Information
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                <a id="uoHyperLinkVehicleAdd" runat="server" href="~/Vehicle/VehicleEditor.aspx">
                                    <asp:Button ID="uoButtonVehicleAdd" runat="server" Text="Add" Font-Size="X-Small" />
                                </a>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="Module">
            <asp:ListView runat="server" ID="uoListviewVehicleTravelInfo"
                    onitemcommand="uoListviewVehicleTravelInfo_ItemCommand" 
                    onitemdeleting="uoListviewVehicleTravelInfo_ItemDeleting">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                            <%--<th>Seq No.</th>--%>
                            <th>Company</th>                           
                            <th>Vehicle Type</th>
                            <th>Pick-up Point</th>
                            <th>Pick-up Date/Time</th> 
                            <th>Destination</th>                                                     
                            <th>Status</th>                            
                            <th runat="server" id = "EditTH"></th> 
                            <th runat="server" id = "DeleteTH"></th> 
                          <%--   <%# end  If %>     --%>                                            
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class='<%# (InactiveRow( Eval("IsCurrent") )) %>'>                        
                           <%-- <td><%# Eval("colSeqNoInt")%></td>--%>
                            <td class="leftAligned">
                                <%# Eval("company")%> 
                            </td>                     
                             <td class="leftAligned">
                                <%# Eval("vehicletype")%>
                            </td>
                              <td class="leftAligned">
                                <%# Eval("pickuppoint")%>
                            </td>
                            <td class="leftAligned">
                              <%# String.Format(DateFormat(Eval("IsDateTime")),Eval("pickupdatetime"))%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("destination")%>
                            </td> 
                             <td class="leftAligned">
                                <%# Eval("status")%>
                            </td>                             
                            <td class='<%# (HideVehicle()==""?"leftAligned":HideVehicle()) %>'  >
                               <asp:HyperLink runat="server" Enabled='<%# !InactiveControl(Eval("IsCurrent"), Eval("colBranchIDInt"),"VE") %>' CssClass='<%# (InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"),"VE")== true?"":"VehicleLink") %>'   id="uoHyperLinkVehicleEdit" NavigateUrl='<%# "~/Vehicle/VehicleEditor.aspx?vID="+ Eval("colTransVehicleIDBigInt") +"&sfId=" + Eval("sfID") + "&st=" + Request.QueryString["st"] + "&recloc=" + Request.QueryString["recloc"] + "&ID=" + Eval("colIdBigint") + "&SN=" + Eval("colSeqNoInt") + "&manualReqID=" + Request.QueryString["manualReqID"] + "&trID=" + Request.QueryString["trID"] %>'>
                                    Edit</asp:HyperLink>  
                            </td> 
                            <td class='<%# (HideVehicle()==""?"leftAligned":HideVehicle()) %>' >
                               <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick='<%# (InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"),"VE")== true? "return false":"return confirmDelete();") %>'  
                                    CommandArgument='<%#Eval("colIdBigint") + ";" + Eval("colSeqNoInt") + ";" + Eval("colTransVehicleIDBigInt") %>' CommandName="Delete" Enabled='<%# !InactiveControl(Eval("IsCurrent"), Eval("colBranchIDInt"),"VE") %>'>Delete</asp:LinkButton>
                           </td>                             
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>  
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                          <%--  <th>Seq No.</th>--%>
                            <th style ="width:180px" >Name</th>   
                            <th>Company</th>
                            <th>Vehicle Type</th>
                            <th>Pick-up Point</th>
                            <th>Pick-up Date/Time</th> 
                            <th>Destination</th>                                                     
                            <th>Status</th>  
                              
                        </tr>
                        <tr>
                            <td colspan="7" class="leftAligned">
                                No Record
                            </td>
                        </tr>                        
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>               
            </td>
        </tr>
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 30%; text-align: left;" class="PageTitle">
                                Hotel Information
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                <a id="uoHyperLinkHotelAdd" runat="server">
                                    <asp:Button ID="uoBtnAdd" runat="server" Text="Add" Font-Size="X-Small" />
                                </a>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="Module">
               <asp:ListView runat="server" ID="uolistviewHotelInfo" 
                    onitemcommand="uolistviewHotelInfo_ItemCommand" 
                    onitemdeleting="uolistviewHotelInfo_ItemDeleting">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                           <%-- <th>Seq No.</th>--%>
                            <th>Company</th>
                            <th>Check-in Date/Time</th>                           
                            <th>Room Type</th>
                            <th>No. of Days</th>                         
                            <th>Status</th>  
                            <th runat="server" id = "EditTH"></th> 
                            <th runat="server" id = "DeleteTH"></th>                                                   
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class='<%# (InactiveRow( Eval("IsCurrent") )) %>'>
                           <%-- <td><%# Eval("colSeqNoInt")%></td>--%>
                            <td class="leftAligned">
                                <%# Eval("company")%>                               
                                <asp:Label ID="uoLabelEvents" runat="server" Text="*" Font-Bold="true" Font-Size="Large" ForeColor="Red" Visible='<%# IsEventExists(Eval("colBranchIDInt")) %>'></asp:Label>
                            </td>                          
                            <td class="leftAligned">
                                <%# String.Format(DateFormat(Eval("IsDateTime")),Eval("checkindatetime"))%>
                            </td>                            
                             <td class="leftAligned">
                                <%# Eval("roomtype")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("nodaysnight")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("status")%>
                            </td> 
                            <td class='<%# (HideHotel()==""?"leftAligned":HideHotel()) %>'>
                               <asp:HyperLink runat="server"  Enabled='<%# !InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"),"HO") %>'  CssClass='<%# (InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"), "HO")== true?"":"HotelLink") %>' id="uoAEditHotel" NavigateUrl='<%# "~/Hotel/HotelEditor.aspx?hID=" + Eval("colTransHotelIDBigInt") + "&sfId=" + Eval("sfID") + "&st=" + Request.QueryString["st"] + "&recloc=" + Request.QueryString["recloc"] + "&ID=" + Eval("colIdBigint") + "&SN=" + Eval("colSeqNoInt") + "&trID=" + Request.QueryString["trID"] + "&manualReqID=" + Request.QueryString["manualReqID"] + "&Date=" + NotifyEvent() %>'>                               
                                    Edit</asp:HyperLink>
                            </td> 
                            <td class='<%# (HideHotel()==""?"leftAligned":HideHotel()) %>'>
                               <asp:LinkButton   Enabled='<%# !InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"),"HO") %>' ID="uoLinkButtonDelete" runat="server" OnClientClick='<%# (InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"),"HO")== true? "return false":"return confirmDelete();") %>'  
                                    CommandArgument='<%#Eval("colIdBigint") + ";" + Eval("colSeqNoInt") + ";" + Eval("colTransHotelIDBigInt") %>' CommandName="Delete">Delete</asp:LinkButton>
                            </td> 
                            
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>  
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                           <%-- <th>Seq No.</th>--%>
                            <th style ="width:180px" >Name</th>   
                            <th>Company</th>
                            <th>Check-in Date/Time</th>
                            <th>Room Type</th>
                            <th>No. of Days</th>                            
                            <th>Status</th>                                                             
                        </tr>
                        <tr>
                            <td colspan="6" class="leftAligned">
                                No Record
                            </td>
                        </tr>                        
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-left: 3px; padding-right: 3px">
                <div class="PageTitle">
                    Port Information</div>
            </td>
        </tr>
        <tr>
            <td colspan="2">               
                <asp:GridView ID="uogridviewPortInfo" runat="server" AutoGenerateColumns="False"
                    CssClass="listViewTable" OnRowCommand="uogridviewPortInfo_RowCommand" OnRowDeleting="uogridviewPortInfo_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="port" HeaderText="Port" ItemStyle-CssClass="leftAligned">
                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vessel" HeaderText="Ship" ItemStyle-CssClass="leftAligned">
                            <ItemStyle Width="25%" HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="porttransdate" HeaderText="Arrival Date/Time" DataFormatString="{0:dd-MMM-yyyy HHmm}"
                            HtmlEncode="false" ItemStyle-CssClass="leftAligned">
                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="costcenter" HeaderText="Cost Center" ItemStyle-CssClass="leftAligned">
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-CssClass="leftAligned">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink CssClass="PortLink" ID="uoHyperLinkPortUpdate" runat="server" NavigateUrl='<%# "~/PortAgent/PortEditor.aspx?sfId=" + Eval("sfID") + "&st=" + Request.QueryString["st"] + "&recloc=" + Request.QueryString["recloc"] + "&pID=" + Eval("colPortAgentTransIdbigint") + "&trID=" + Request.QueryString["trID"] + "&manualReqID=" + Request.QueryString["manualReqID"]%>'>Update</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                    CommandArgument='<%#Eval("colPortAgentTransIdbigint") %>' CommandName="Delete">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr><td colspan="2"></td></tr>
       
        <tr>
          <%-- <td>Reimbursement</td>
           <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                <a id="uoHyperLinkReimbursementAdd" runat="server" >
                    <asp:Button ID="uoBtnAddReimbursement" runat="server" Text="Add" Font-Size="X-Small"  />
                </a>  
           </td>--%>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 30%; text-align: left;" class="PageTitle">
                                Reimbursement
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                <a id="uoHyperLinkReimbursementAdd" runat="server">
                                    <asp:Button ID="uoBtnAddReimbursement" runat="server" Text="Add" Font-Size="X-Small" />
                                </a>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>        
        <tr>
            <td class="Module" colspan="2">
                <asp:UpdatePanel runat="server" ID="uoUpdatePanelReimbursement" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ListView runat="server" ID="uoReimbursementList" 
                            onitemcommand="uoReimbursementList_ItemCommand">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>Reimbursement Name</th>
                                        <th>Amount</th>
                                        <th>Currency</th>
                                        <th runat="server" id="uoEditTh" style="width: 10%" ></th>
                                        <th runat="server" id="uoDeleteTH" style="width: 10%" ></th>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="leftAligned" visible='<%# (!Convert.ToBoolean(ViewState["ReimbursementEditable"])) %>'>
                                        <a runat="server" class="ReimbursementLinkView" id="A1" href='<%# "~/Finance/ReimbursementView.aspx?rId=" + Eval("colReimbursementIdInt") + "&sfId=" + 
                                            Request.QueryString["sfId"] + "&mReqId=" + Request.QueryString["manualReqID"] + 
                                            "&tReqId=" + Request.QueryString["trID"] + "&cId=" + ViewState["CurrencyId"] + 
                                            "&cName=" + ViewState["CurrencyName"] %>'
                                            >
                                            <%# Eval("colReimbursementNameVarchar")%>
                                        </a>
                                    </td>
                                   
                                    <td class="leftAligned">
                                        <%# String.Format("{0:0.00}", Eval("colAmountMoney"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("colCurrencyName") %>
                                    </td>
                                    <td class="leftAligned" visible='<%# (!Convert.ToBoolean(ViewState["ReimbursementEditable"])) %>'>
                                        <a runat="server" class="ReimbursementLink" id="uoAEditRegion" href='<%# "~/Finance/ReimbursementAdd.aspx?rId=" + Eval("colReimbursementIdInt") + "&sfId=" + 
                                            Request.QueryString["sfId"] + "&mReqId=" + Request.QueryString["manualReqID"] + 
                                            "&tReqId=" + Request.QueryString["trID"] + "&cId=" + ViewState["CurrencyId"] + 
                                            "&cName=" + ViewState["CurrencyName"] %>'
                                            Visible='<%# (Convert.ToBoolean(uoHyperLinkReimbursementAdd.Visible)) %>'>
                                            Edit
                                        </a>
                                    </td>
                                   <td class="leftAligned" visible='<%# (!Convert.ToBoolean(ViewState["ReimbursementEditable"])) %>'>
                                       
                                        <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandArgument='<%# Eval("colReimbursementIdInt") %>' 
                                            Text="Delete" CommandName="Delete"
                                            OnClientClick="return confirmDelete();"
                                            Visible='<%# (Convert.ToBoolean(uoHyperLinkReimbursementAdd.Visible)) %>'>
                                        </asp:LinkButton>
                                   </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr >
                                        <th>Reimbursement Name</th>
                                        <th>Amount</th>
                                        <th>Currency</th>
                                    </tr>
                                    <tr>
                                        <td class="leftAligned" colspan="5">
                                            No Record
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                         <asp:DataPager ID="uoReimbursementListPager" runat="server" PagedControlID="uoReimbursementList"
                            PageSize="10" onprerender="uoReimbursementListPager_PreRender">
                            <Fields>
                                <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                                <%-- <asp:NextPreviousPagerField ButtonType="Image"  ButtonCssClass="PagerClass" NextPageImageUrl="~/Images/next.jpg" PreviousPageImageUrl="~/Images/prev.jpg" />--%>
                            </Fields>
                        </asp:DataPager>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoReimbursementListPager" 
                            EventName="PreRender" />
                    </Triggers>
                </asp:UpdatePanel>                
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-left: 3px; padding-right: 3px">
                <div class="PageTitle">
                    Other</div>
            </td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
            <td class="Module" colspan="2">
                <asp:UpdatePanel runat="server" ID="uoUpdatePanelOtherInfo" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ListView runat="server" ID="uoOtherList">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>Users</th>
                                        <th>Date</th>
                                        <th>Barcode Scanned</th>
                                    </tr>
                                     <asp:PlaceHolder runat="server" ID="itemPlaceholder" ></asp:PlaceHolder>
                                </table>                               
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="leftAligned">
                                       <%# Eval("colUserRoleVarchar") %>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("colProcessDatetime"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("isScanned")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr >
                                        <th>Users</th>
                                        <th>Date</th>
                                        <th>Barcode Scanned</th>
                                    </tr>
                                    <tr><td colspan="3" class="leftAligned">No Record</td></tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <%--<td class="PageTitle" colspan="2">Pending Vehicle Booking</td>--%>
            <td colspan="2" style="padding-left: 3px; padding-right: 3px">                
                <table class="PageTitle" width="100%">
                    <tr>
                        <td>Pending Vehicle Booking</td>
                        <td align="right">                                               
                            <asp:Image ID="uoImageVehiclePending" runat="server" ImageUrl="~/Images/box_up.png" />
                            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender_VehiclePending" runat="Server"
                            TargetControlID="uoPanelVehiclePending"
                            CollapsedSize="0"                                        
                            ExpandControlID="uoImageVehiclePending"
                            CollapseControlID="uoImageVehiclePending"
                            AutoCollapse="False"
                            AutoExpand="False"                                        
                            ImageControlID="uoImageVehiclePending"
                            ExpandedImage="~/images/box_up.png"
                            CollapsedImage="~/images/box_down.png"
                            ExpandDirection="Vertical" />  
                        </td>
                    </tr>
                </table>                              
            </td>
        </tr>        
        <tr>
            <td colspan="2" class="Module">
            <asp:Panel ID="uoPanelVehiclePending" runat="server">
            <asp:ListView runat="server" ID="uoListviewVehicleTravelPending"
                    onitemcommand="uoListviewVehicleTravelPending_ItemCommand" 
                    onitemdeleting="uoListviewVehicleTravelPending_ItemDeleting">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                            <%--<th>Seq No.</th>--%>
                            <th>Company</th>                           
                            <th>Vehicle Type</th>
                            <th>Pick-up Point</th>
                            <th>Pick-up Date/Time</th> 
                            <th>Destination</th>                                                     
                            <th>Status</th>    
                            <th runat="server" id = "EditTH"></th> 
                            <th runat="server" id = "DeleteTH"></th> 
                            <th runat="server" id = "ApproveTH"></th>                           
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr >                        
                           <%-- <td><%# Eval("colSeqNoInt")%></td>--%>
                            <td class="leftAligned">
                                <%# Eval("company")%> 
                            </td>                     
                             <td class="leftAligned">
                                <%# Eval("vehicletype")%>
                            </td>
                              <td class="leftAligned">
                                <%# Eval("pickuppoint")%>
                            </td>
                            <td class="leftAligned">
                              <%# String.Format(DateFormat(Eval("IsDateTime")),Eval("pickupdatetime"))%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("destination")%>
                            </td> 
                             <td class="leftAligned">
                                <%# Eval("status")%>
                            </td>                             
                            <td class='<%# (HideVehicle()==""?"leftAligned":HideVehicle()) %>'  >
                               <asp:HyperLink Enabled='<%# !InactiveControl( bool.Parse("true"), Eval("colBranchIDInt"),"VE") %>' 
                               CssClass='<%# (InactiveControl( bool.Parse("true"), Eval("colBranchIDInt"),"VE")== true?"":"VehicleLinkPending") %>'                                
                               runat="server" id="uoHyperLinkVehicleEdit" NavigateUrl='<%# "~/Vehicle/VehicleEditor.aspx?pendingID="+ Eval("VehiclePendingID") +"&vID="+ Eval("colTransVehicleIDBigInt") +"&sfId=" + Request.QueryString["sfId"] + "&st=" + Request.QueryString["st"] + "&recloc=" + Request.QueryString["recloc"] + "&ID=" + Eval("colIdBigint") + "&SN=" + Eval("colSeqNoInt") + "&manualReqID=" + Request.QueryString["manualReqID"] + "&trID=" + Request.QueryString["trID"] %>'>
                                    Edit</asp:HyperLink>  
                            </td> 
                            <td class='<%# (HideVehicle()==""?"leftAligned":HideVehicle()) %>' >
                               <asp:LinkButton ID="uoLinkButtonDelete" runat="server" 
                               Enabled='<%# !InactiveControl( bool.Parse("true"), Eval("colBranchIDInt"),"VE") %>' 
                               OnClientClick='<%# (InactiveControl( bool.Parse("True"), Eval("colBranchIDInt"),"VE")== true? "return false":"return confirmDelete();") %>'  
                               CommandArgument='<%#Eval("VehiclePendingID") %>' CommandName="Delete" >Delete</asp:LinkButton>
                           </td>
                           <td class='<%# (HideVehiclePending()==""?"leftAligned":HideVehiclePending()) %>'>
                               <asp:LinkButton ID="uoLinkButtonApprove" runat="server" OnClientClick='return confirmApprove();' 
                                    CommandArgument='<%#Eval("VehiclePendingID") %>' CommandName="Approve">Approve</asp:LinkButton>
                           </td>                             
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>  
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                          <%--  <th>Seq No.</th>--%>
                            <th style ="width:180px" >Name</th>   
                            <th>Company</th>
                            <th>Vehicle Type</th>
                            <th>Pick-up Point</th>
                            <th>Pick-up Date/Time</th> 
                            <th>Destination</th>                                                     
                            <th>Status</th>  
                              
                        </tr>
                        <tr>
                            <td colspan="7" class="leftAligned">
                                No Record
                            </td>
                        </tr>                        
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>  
                </asp:Panel>             
            </td>
        </tr>
          <%--<tr>
            <td colspan="2" style="padding-left: 3px; padding-right: 3px">                
                <table class="PageTitle" width="100%">
                    <tr>
                        <td>Pending Hotel Booking</td>
                        <td align="right">                                               
                            <asp:Image ID="uoImageHotelPending" runat="server" ImageUrl="~/Images/box_up.png" />
                            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender_HotelPending" runat="Server"
                            TargetControlID="uoPanelHotelPending"
                            CollapsedSize="0"                                        
                            ExpandControlID="uoImageHotelPending"
                            CollapseControlID="uoImageHotelPending"
                            AutoCollapse="False"
                            AutoExpand="False"                                        
                            ImageControlID="uoImageHotelPending"
                            ExpandedImage="~/images/box_up.png"
                            CollapsedImage="~/images/box_down.png"
                            ExpandDirection="Vertical" />  
                        </td>
                    </tr>
                </table>                              
            </td>
        </tr>--%>
        <%--<tr>
            <td colspan="2" class="Module">
            <asp:Panel ID="uoPanelHotelPending" runat="server">
               <asp:ListView runat="server" ID="uolistviewHotelPending" 
                    onitemcommand="uolistviewHotelPending_ItemCommand" 
                    onitemdeleting="uolistviewHotelPending_ItemDeleting">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>                           
                            <th>Company</th>
                            <th>Check-in Date/Time</th>                           
                            <th>Room Type</th>
                            <th>No. of Days</th>                         
                            <th>Status</th>  
                            <th runat="server" id = "EditTH"></th> 
                            <th runat="server" id = "DeleteTH"></th>   
                            <th runat="server" id = "ApproveTH"></th>                                                 
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr >                           
                            <td class="leftAligned">
                                <%# Eval("company")%>                               
                                <asp:Label ID="uoLabelEvents" runat="server" Text="*" Font-Bold="true" Font-Size="Large" ForeColor="Red" Visible='<%# IsEventExists(Eval("colBranchIDInt")) %>'></asp:Label>
                            </td>                          
                            <td class="leftAligned">
                                <%# String.Format(DateFormat(Eval("IsDateTime")),Eval("checkindatetime"))%>
                            </td>                            
                             <td class="leftAligned">
                                <%# Eval("roomtype")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("nodaysnight")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("status")%>
                            </td> 
                            <td class='<%# (HideHotel()==""?"leftAligned":HideHotel()) %>'>
                               <asp:HyperLink runat="server" Enabled='<%# !InactiveControl( bool.Parse("True"), Eval("colBranchIDInt"),"HO") %>'  
                               CssClass='<%# (InactiveControl( bool.Parse("True"), Eval("colBranchIDInt"), "HO")== true?"":"HotelLinkPending") %>' 
                               id="uoAEditHotel" NavigateUrl='<%# "~/Hotel/HotelEditor.aspx?pendingID="+ Eval("HotelPendingID") + "&hID=" + Eval("colTransHotelIDBigInt") + "&sfId=" + Request.QueryString["sfId"] + "&st=" + Request.QueryString["st"] + "&recloc=" + Request.QueryString["recloc"] + "&ID=" + Eval("colIdBigint") + "&SN=" + Eval("colSeqNoInt") + "&Date=" + NotifyEvent() %>'>
                                    Edit</asp:HyperLink>
                            </td> 
                            <td class='<%# (HideHotel()==""?"leftAligned":HideHotel()) %>'>
                               <asp:LinkButton    Enabled='<%# !InactiveControl( bool.Parse("True"), Eval("colBranchIDInt"),"HO") %>' ID="uoLinkButtonDelete" runat="server" 
                               OnClientClick='<%# (InactiveControl( bool.Parse("True"), Eval("colBranchIDInt"),"HO")== true? "return false":"return confirmDelete();") %>'  
                                    CommandArgument='<%#Eval("HotelPendingID") %>' CommandName="Delete">Delete</asp:LinkButton>
                            </td> 
                            <td class='<%# (HideHotelPending()==""?"leftAligned":HideHotelPending()) %>'>
                               <asp:LinkButton ID="uoLinkButtonApprove" runat="server" OnClientClick='return confirmApprove();' 
                                    CommandArgument='<%#Eval("HotelPendingID") %>' CommandName="Approve">Approve</asp:LinkButton>
                           </td>   
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>  
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>                           
                            <th style ="width:180px" >Name</th>   
                            <th>Company</th>
                            <th>Check-in Date/Time</th>
                            <th>Room Type</th>
                            <th>No. of Days</th>                            
                            <th>Status</th>                                                             
                        </tr>
                        <tr>
                            <td colspan="6" class="leftAligned">
                                No Record
                            </td>
                        </tr>                        
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                </asp:Panel>
            </td>
        </tr>--%>      
        <tr>
            <td align="right" >
                <asp:HiddenField ID="uoHdRecLoc" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right"  colspan="6" style="padding-right:3px;">
                <asp:Button ID="uobuttonBack" runat="server" Text="Back" OnClick="uobuttonBack_Click"
                     Font-Size="X-Small" />
            </td>
        </tr>
        <tr>
            <td align="right" >
                <asp:HiddenField ID="uoHdSfStatus" runat="server" />
                <asp:HiddenField ID="uoHiddenFieldPopupPort" runat="server" Value="0" />
                <asp:HiddenField ID="uoHiddenFieldPopupHotel" runat="server" Value="0" />
                <asp:HiddenField ID="uoHiddenFieldPopupHotelPending" runat="server" Value="0" />
                
                <asp:HiddenField ID="uoHiddenFieldPopupVehicle" runat="server" Value="0" />
                <asp:HiddenField ID="uoHiddenFieldPopupVehiclePending" runat="server" Value="0" />
                
                <asp:HiddenField ID="uoHiddenFieldPopupAir" runat="server" Value="0" />   
                <asp:HiddenField ID="uoHiddenFieldPopupReimbursement" runat="server" />
                <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
                <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />  
                <asp:HiddenField ID="uoHiddenFieldRoleBranchID" runat="server" Value="0" /> 
                <asp:HiddenField ID="uoHiddenFieldReimbursementId" runat="server" />
                
                <asp:HiddenField ID="uoHiddenFieldDateOn" runat="server" />
                <asp:HiddenField ID="uoHiddenFieldDateOff" runat="server" />
                <asp:HiddenField ID="uoHiddenFieldIsExistInTR" runat="server" Value="1" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:ObjectDataSource ID="ObjectDataSourceReimbursement" runat="server" 
                    EnablePaging="True" OldValuesParameterFormatString="oldCount_{0}" 
                    SelectCountMethod="SelectReimbursementListbySeafarerCount" 
                    SelectMethod="SelectReimbursementListbySeafarer" 
                    TypeName="TRAVELMART.BLL.FinanceBLL" 
                    DeleteMethod="DeleteSeafarerReimbursement">
                    <DeleteParameters>
                        <asp:ControlParameter ControlID="uoHiddenFieldReimbursementId" 
                            Name="ReimbursementId" PropertyName="Value" />
                        <asp:ControlParameter ControlID="uoHiddenFieldUser" Name="UserId" 
                            PropertyName="Value" />
                    </DeleteParameters>
                    <SelectParameters>
                        <asp:QueryStringParameter Name="SeafarerId" QueryStringField="sfId" />
                        <asp:QueryStringParameter Name="mReqId" QueryStringField="manualReqID" />
                        <asp:QueryStringParameter Name="tReqId" QueryStringField="trID" />
                        <asp:ControlParameter ControlID="uoHiddenFieldIsExistInTR" ConvertEmptyStringToNull="false"
                                DbType="String" Name="E1TRId" PropertyName="Value" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
   
</asp:Content>
