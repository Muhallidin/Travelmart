<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="HotelExceptionBooking.aspx.cs" Inherits="TRAVELMART.Hotel.HotelExceptionBooking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .HotelGroup
        {
            font-size: larger;
            font-weight: bold;
            text-decoration: underline;
            color: #82ABB8;
        }
        .Details
        {
            font-size: small;
            text-align: right;
            vertical-align: bottom;
            display: inline;
        }
        .Padding
        {
            padding-left: 1px;
        }
        .Header
        {
            font-size: medium;
            font-weight: bold;
            text-decoration: underline;
            text-align: center;
        }
        .Bullets
        {
            height: auto;
            text-align: left;
            list-style-type: circle;
            font-size: small;
            vertical-align: middle;
            display: block;
        }
        .Panel
        {
            display: none;
        }
        .alignRight
        {
            text-align: right;
            padding-right: 5px;
        }
        .MouseTool
        {
            cursor: pointer;
        }
        .listViewTable2
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: #333333;
            border-collapse: collapse;
            border: 1px solid;
            border-color: #CCCCCC;
            text-align: center;
            white-space: normal;
        }               
    </style>    
       
</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server" ID="header">
    <%--<div class="PageTitle">
        Hotel Booking Exception List</div>--%>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr class="PageTitle">
                <td align="left">
                   Hotel Booking Exception List
                </td>                        
                <td align="right">                
                Region: &nbsp;&nbsp
			     <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="200px" AutoPostBack="true" 
			           >
                 </asp:DropDownList>
                 &nbsp;&nbsp;Port: &nbsp;&nbsp;
                <asp:DropDownList ID="uoDropDownListPortPerRegion" runat="server" Width="200px" AutoPostBack="true"
                    >
                </asp:DropDownList>
                </td>
            </tr>
        </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidePlaceHolder" runat="server">
     <table >
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="uoUpdateExport" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button runat="server" ID="uoBtnExportList" CssClass="SmallButton" Text="Export Exception List"
                             Visible="true" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <a href="HotelExceptionBookingsRemoved.aspx" id="aBtnViewRemoved">
                    <asp:Button runat="server" ID="uoButtonViewRemoved" CssClass="SmallButton" 
                     Text="View Removed Records"  Visible="true" />
                </a>
            </td>
        </tr>
    </table>
    <table  >
        <tr style="text-align:left; vertical-align:top;  ">
            <td style="text-align:left; vertical-align:top; ">
               <div id="DivLeft" style="overflow: auto; overflow-x: hidden; width: 100%;   overflow-y: hidden;
                     position: relative; background:red;"  >
                       <asp:ListView runat="server" ID="ListViewExceptionHeader" >
                            <LayoutTemplate/>
                            <ItemTemplate/>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th class="hideElement">
                                            requestInfo
                                        </th>
                                        <th>
                                            <asp:CheckBox runat="server" ID="CheckBox1" Width="20px" CssClass="Checkbox" onclick="SetSettings(this);" />
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label16" Text="Remove" Width="50px"> </asp:Label>
                                        </th>
                                        <th style="text-align: center;">
                                            <asp:Label runat="server" ID="uoLablE1Hdr" Text="E1 TRId" Width="35px"> </asp:Label>
                                        </th>
                                        <th style="text-align: center;">
                                             <asp:LinkButton ID="LinkButton17" runat="server" CommandName="SortByRecLoc" 
                                            Text="Record Locator" Width="60px"></asp:LinkButton>
                                        </th>
                                        <th style="text-align: center;">
                                             <asp:LinkButton ID="uoLinkButtonEmpID" runat="server" CommandName="SortByEmpID" 
                                            Width="60px" Text="Employee Id"></asp:LinkButton>
                                        </th>
                                        <th style="text-align: center;">
                                             <asp:LinkButton ID="uoLinkButtonName" runat="server" CommandName="SortByName" 
                                            Width="200px" Text="Name"></asp:LinkButton>
                                        </th>
                                        <th style="text-align: center;">
                                            <asp:LinkButton ID="uoLinkButtonStatus" runat="server" CommandName="SortByStatus" 
                                            Width="50px" Text="Status"></asp:LinkButton>
                                        </th>
                                         <th class="MouseTool" style="text-align: center;">
                                             <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByExRemarks" 
                                            Width="250px" Text="Exception Remarks"></asp:LinkButton>
                                        </th>
                                        <th style="text-align: center;">
                                            <asp:LinkButton ID="uoLinkButtonOnOffDate" runat="server" CommandName="SortByOnOffDate" 
                                            Width="70px" Text="On/Off Date"></asp:LinkButton>
                                        </th>
                                        <th style="text-align: center;">
                                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByGender" 
                                            Width="50px" Text="Gender"></asp:LinkButton>
                                        </th>
                                        <th style="text-align: center;">
                                             <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByRank" 
                                            Width="120px" Text="Rank"></asp:LinkButton>
                                        </th>
                                        <th style="text-align: center;">
                                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="SortByStripe" 
                                            Width="35px" Text="Stripe"></asp:LinkButton>
                                        </th>
                                        <th style="text-align: center;">
                                             <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByRoomType" 
                                            Width="50px" Text="Room Type"></asp:LinkButton>
                                        </th>
                                        <th style="text-align: center;">
                                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByNationality" 
                                            Width="120px" Text="Nationality"></asp:LinkButton>
                                        </th>
                                        <th class="MouseTool" style="text-align: center;">
                                            <%--<asp:Label runat="server" ID="uoLablPort" Text="Port" ToolTip="Port Code based on Crew History"
                                                Width="150px"> </asp:Label>--%>
                                            <asp:LinkButton ID="LinkButton7" runat="server" CommandName="SortByPort" ToolTip="Port Code based on Crew History"
                                            Width="150px" Text="Port"></asp:LinkButton>
                                        </th>
                                        <th style="text-align: center;">
                                            <asp:LinkButton ID="LinkButton8" runat="server" CommandName="SortByShip" 
                                            Width="150px" Text="Ship"></asp:LinkButton>
                                        </th>
                                        <th class="MouseTool" style="text-align: center;">
                                            <%--<asp:Label runat="server" ID="Label1" Text="Arvl/Dept Date" ToolTip="Arrival/Departure date based on PNR."
                                                Width="70px"> </asp:Label>--%>
                                            <asp:LinkButton ID="LinkButton9" runat="server" CommandName="SortByArrDepDate" 
                                            Width="70px" Text="Arvl/Dept Date"></asp:LinkButton>
                                        </th>
                                        <th class="MouseTool" style="text-align: center;">
                                            <%--<asp:Label runat="server" ID="Label2" Text="Arvl/Dept Time" ToolTip="Arrival/Departure time based on PNR."
                                                Width="50px"> </asp:Label>--%>
                                                <asp:LinkButton ID="LinkButton10" runat="server" CommandName="SortByArrDepTime" 
                                                Width="50px" Text="Arvl/Dept Time"></asp:LinkButton>
                                        </th>
                                        <th class="MouseTool" style="text-align: center;">
                                            <%--<asp:Label runat="server" ID="Label3" Text="Carrier" ToolTip="Airline Carrier based on PNR."
                                                Width="45px"> </asp:Label>--%>
                                                <asp:LinkButton ID="LinkButton12" runat="server" CommandName="SortByCarrier" 
                                                Width="45px" Text="Carrier"></asp:LinkButton>
                                        </th>
                                        <th class="MouseTool" style="text-align: center;">
                                           <%-- <asp:Label runat="server" ID="Label4" Text="Flight No." ToolTip="Flight No. based on PNR."
                                                Width="45px"> </asp:Label>--%>
                                                <asp:LinkButton ID="LinkButton11" runat="server" CommandName="SortByFlightNo" 
                                                Width="45px" Text="Flight No."></asp:LinkButton>
                                        </th>
                                        <th class="MouseTool" style="text-align: center;">
                                            <%--<asp:Label runat="server" ID="Label5" Text="From City" ToolTip="Airport Code Equivalent of Origin."
                                                Width="45px"> </asp:Label>--%>
                                                <asp:LinkButton ID="LinkButton13" runat="server" CommandName="SortByFromCity" 
                                                Width="45px" Text="From City"></asp:LinkButton>
                                        </th>
                                        <th class="MouseTool" style="text-align: center;">
                                            <%--<asp:Label runat="server" ID="Label6" Text="To City" ToolTip="Airport Code Equivalent of Destination."
                                                Width="45px"> </asp:Label>--%>
                                                 <asp:LinkButton ID="LinkButton14" runat="server" CommandName="SortByToCity" 
                                                Width="45px" Text="To City"></asp:LinkButton>
                                        </th>
                                        <th class="MouseTool" style="text-align: center;">
                                            <%--<asp:Label runat="server" ID="Label7" Text="Reason Code" ToolTip="Reason Code based on Crew History."
                                                Width="60px"> </asp:Label>--%>
                                                <asp:LinkButton ID="LinkButton15" runat="server" CommandName="SortByRasonCode" 
                                                Width="60px" Text="Reason Code"></asp:LinkButton>
                                        </th>
                                       
                                        <th class="MouseTool" style="text-align: center;">
                                            <asp:LinkButton ID="LinkButton16" runat="server" CommandName="SortByRemarks" 
                                            Width="150px" Text="Booking Remarks"></asp:LinkButton>
                                        </th>
                                        
                                        <th>
                                            <asp:Label runat="server" ID="Label11" Text="" Width="20px"> </asp:Label>
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        </div>
                       <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto; position: relative;"  onscroll="divScrollL();">
                        <asp:ListView runat="server" ID="uoListViewExceptionList" 
                             >
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList">
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <%# OverflowChangeRowColor()%>
                                <td class="hideElement">                                
                                    <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IdBigint") %>' />
                                    <asp:HiddenField runat="server" ID="hfSeqNo" Value='<%# Eval("SeqNo") %>' />
                                    
                                    <asp:HiddenField runat="server" ID="hfReqId" Value='<%# Eval("TravelReqId") %>' />
                                    <asp:HiddenField runat="server" ID="hfRoomType" Value='<%# Eval("RoomTypeId") %>' />
                                    <asp:HiddenField runat="server" ID="hfPortId" Value='<%# Eval("PortId") %>' />
                                    <asp:HiddenField runat="server" ID="uoHiddenFieldHotelCity" Value='<%# Eval("HotelCity") %>' />
                                    <asp:HiddenField runat="server" ID="uoHiddenFieldIsByPort" Value='<%# Eval("IsByPort").ToString() %>' />
                                    <asp:HiddenField runat="server" ID="hfHotelOverflowID" Value='<%# Eval("ExceptionIdBigInt") %>' />
                                </td>
                                <td style="white-space: normal;">
                                    <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" Width="24px" runat="server" />
                                </td>
                                <td style="white-space: normal;">                   
                                    <a href="#ucDivComment" id="ucLinkRemove" onclick='<%# "javascript:GetExceptionDetails(\""+ 
                                        Eval("SeafarerId")  + "\", \"" + Eval("SeafarerName") + "\", \"" +
                                        Eval("RecordLocator") + "\", \"" +
                                        Eval("ExceptionIdBigInt") +"\");" %>' >
                                        <%--<asp:LinkButton ID="uoLinkButtonRemove"  CommandName="Remove" runat="server" Text="Remove" Width="58px" ></asp:LinkButton>--%>
                                        <asp:Label ID="Label3" Width="58" runat="server" Text="Remove"></asp:Label>
                                    </a>
                                </td>
                                    
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblE1TrId" Text='<%# Eval("E1TravelReqId") %>' Width="35px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator") %>' Width="68px"></asp:Label></td><td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblSeafarerID" Width="64px" Text='<%# Eval("SeafarerId")%>'></asp:Label></td><td class="leftAligned" style="white-space: normal;">
                                    <%--<asp:Label runat="server" ID="uoLblSeafarerName" Width="150px" Text='<%# Eval("SeafarerName") %>'></asp:Label>--%>
                                    <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "../SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("SeafarerId") + "&recloc=&st=" + Eval("SFStatus") + "&ID=0&trID="+ Eval("TravelReqId") +"&manualReqID=0" + "&dt=" + Request.QueryString["dt"]%>'
                                        runat="server" Width="205px" Text='<%# Eval("SeafarerName")%>'></asp:HyperLink></td><td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblStatus" Width="58px" Text='<%# Eval("SFStatus") %>'></asp:Label></td>
                                    <td class="leftAligned" style="white-space: normal;">
                                        <asp:Label runat="server" ID="uoLblException" Text=' <%# Eval("ExceptionRemarks") %>'
                                        Width="255px"> </asp:Label>
                                    </td>
                                    <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblOnOff" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>'
                                        Width="75px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLablGenderHdr" Text=' <%# Eval("Gender") %>' Width="55px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label8" Text='<%# Eval("RankName") %>' Width="128px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblStripe" Width="40px" Text='<%# String.Format("{0:0.00}", Eval("Stripes"))%>'></asp:Label></td><td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLablRoomHdr" Text='<%# Eval("RoomType") %>' Width="55px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLablNatHdr" Text='<%# Eval("Nationality") %>' Width="128px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLablPort" Text='<%# Eval("PortName")%>' Width="155px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label12" Text='<%# Eval("VesselName")%>' Width="158px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label1" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("ArrivalDepartureDatetime"))%>'
                                        Width="75px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label2" Text='<%# String.Format("{0:HH:mm:ss}", Eval("ArrivalDepartureDatetime"))%>'
                                        Width="55px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblCarrier" Text=' <%# Eval("Carrier")%> ' Width="50px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblFlightNo" Text='<%# Eval("FlightNo")%>' Width="50px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label5" Text=' <%# Eval("FromCity")%>' Width="53px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label6" Text='<%# Eval("ToCity")%>' Width="50px"> </asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblReason" Text='<%# Eval("ReasonCode")%>' Width="68px"> </asp:Label>
                                </td>
                              
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblBookingRemarks" Text=' <%# Eval("BookingRemarks") %>'
                                        Width="155px"> </asp:Label>
                                </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <td colspan="20" class="leftAligned">
                                            <asp:Label runat="server" ID="Label10" Text="No Record" Width="1910px"> </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                    <asp:HiddenField runat="server" ID="uoHiddenFieldUser" Value="" />
                    <asp:HiddenField runat="server" ID="uoHiddenFieldStartDate" Value="" />      
            
        </td>
        <td style="border-left: solid 1px gray; padding-left: 1px; padding-right: 1px;"/>
        <td>
         <%--<asp:Panel runat="server" ID="uoPanelHotels" Width="100%"   >
                    
            </asp:Panel>--%>
            
            
              <div style="overflow: auto; overflow-x: auto; overflow-y: auto; 
        text-align: left; padding-top: 3px; width:100%;  position:relative;" id="HotelDiv" >
        <asp:Panel runat="server" ID="uoPanelHotels" Width="100%"   >
            <asp:Repeater runat="server" ID="uoRepeaterHotels"  >
                <ItemTemplate>
                    <table width="100%" runat="server" id="uoPanelBranchNameHeader">
                        <tr>
                            <td align="left" style="width: 75%">
                                <asp:LinkButton runat="server" ID="uoLinkBranch" CommandName="Select" Text='<%# Eval("BranchName") %>'
                                    CssClass="HotelGroup" CommandArgument='<%# Eval("BranchId")%>'></asp:LinkButton></td><td align="right" style="padding-right: 5px;">
                                <asp:ImageButton runat="server" ID="uoBtnContract" ImageUrl="~/Images/contract.jpg"
                                    Visible='<%# SetContractVisibility() %>' Height="15px" Width="15px" OnClientClick='<%# "return OpenContract(\"" + Eval("BranchId") + "\",\"" + Eval("ContractId") + "\")" %>'
                                    CssClass="rightAligned" ToolTip="View Contract" />
                                <asp:ImageButton runat="server" ID="uoBtnEvent" ImageUrl="~/Images/calendar1.png"
                                    Visible='<%# SetEventVisibility() %>' Height="15px" Width="15px" OnClientClick='<%# "return OpenEventsList(\"" + Eval("BranchId") + "\",\"" + Eval("CityId") + "\",\"" + Eval("colDate") + "\")" %>'
                                    CssClass="rightAligned" ToolTip="View Event" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel runat="server" ID="uoPanelBranchName" BorderColor="#ECDFC4" Width="100%" 
                        BorderStyle="Solid" >
                        <cc1:CollapsiblePanelExtender ID="cpe" runat="server" TargetControlID="uoPanelBranchName"
                            ExpandControlID="uoLinkBranch" CollapseControlID="uoLinkBranch" TextLabelID="uoLinkBranch"
                            Collapsed="true" ExpandDirection="Vertical">
                        </cc1:CollapsiblePanelExtender>
                        <table width="100%" runat="server" id="uoTblDetails" >
                            <tr id="Tr1" visible='<%# !Convert.ToBoolean(Eval("isAccredited")) %>' runat="server">
                                <td colspan="2">
                                    <asp:ListView runat="server" ID="uoRoomList">
                                        <LayoutTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                                <tr>
                                                    <th>
                                                        <asp:Label runat="server" ID="uoLblDateHdr" Text="Date" Width="62px"></asp:Label></th><th style="white-space: normal;">
                                                        <asp:Label runat="server" ID="uoLblCabinHdr" Text="Cabin" Width="40px"></asp:Label></th><th style="white-space: normal;">
                                                        <asp:Label runat="server" ID="uoLblCrewHdr" Text="Booked Room" Width="40px"></asp:Label></th><th colspan="6" style="white-space: normal;">
                                                        <asp:Label runat="server" ID="uoLblAvailableHdr" Text="Available Rooms" Width="75px"></asp:Label></th></tr><tr>
                                                    <th>
                                                    </th>
                                                    <th>
                                                    </th>
                                                    <th>
                                                    </th>
                                                    
                                                    <th style="white-space: normal;">
                                                        <asp:Label runat="server" ID="Label13" Text="C" Width="25px" ToolTip="Contract"></asp:Label></th><th style="white-space: normal;">
                                                        <asp:Label runat="server" ID="LblOverride" Text="O" Width="25px" ToolTip="Override"></asp:Label></th><th>                                                           
                                                        <asp:Label runat="server" ID="Label1" Text="R" Width="25px" ToolTip="Remaining Room"></asp:Label></th><th>                                                 
                                                        <asp:Label runat="server" ID="LblOverrideEdit" Text="" Width="25px" ToolTip="Add Override Room Blocks"></asp:Label></th><th style="white-space: normal;">
                                                        <asp:Label runat="server" ID="LblEmergency" Text="E" Width="25px" ToolTip="Emergency"></asp:Label></th><th>
                                                        <asp:Label runat="server" ID="LblEmergencyEdit" Text="" Width="25px" ToolTip="Add Emergency Room Blocks"></asp:Label></th></tr><asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="uoLblDate" Width="62px" Text='<%# setDate() %>'></asp:Label>
                                                    </td>
                                                    <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="uoLblRoom" Width="40px" Text='<%#Eval("Room") %>'>
                                                    </asp:Label></td>
                                                    <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="uoLblCrew" Width="40px" Text='<%#Eval("TotalBooking") %>'></asp:Label>
                                                    </td>
                                                    <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="uoLblContractBlocks" Width="25px" Text='<%#Eval("ContractBlocks").ToString().Replace(".0", "") %>'></asp:Label>
                                                    </td>
                                                    <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="uoLblOverrideBlocks" Width="25px" Text='<%# Eval("OverrideBlocks").ToString().Replace(".0", "") %>'></asp:Label>
                                                    </td>
                                                     <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="Label3" Width="25px" Text='<%# Eval("RemainingRooms").ToString().Replace(".0", "") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                    <asp:HyperLink ID="uoHyperLinkEditOverride" CssClass="Link" runat="server" Width="25px"
                                                        NavigateUrl='<%# "~/HotelRoomOverrideEdit2.aspx?bID=" + Eval("BranchId") + "&rID=" + Eval("RoomTypeId") + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "&rc=" + String.Format("{0:#####}", Eval("ReservedOverride")) %>'>Add</asp:HyperLink>
                                                    </td>
                                                    <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="Label14" Width="25px" Text='<%#Eval("EmergencyBlocks").ToString().Replace(".0", "") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                    <asp:HyperLink CssClass="Link" ID="uoHyperLinkEditEmergency" runat="server" NavigateUrl='<%# "~/HotelRoomEmergencyEdit2.aspx?bID=" + Eval("BranchId") + "&rID=" + Eval("RoomTypeId") + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) %>'>Add</asp:HyperLink></td></tr></ItemTemplate></asp:ListView></td></tr>
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr id="Tr2" visible='<%# !Convert.ToBoolean(Eval("isAccredited")) %>' runat="server">
                                <td valign="middle">
                                    <asp:Label ID="uoAllocationHdr" runat="server" Text="Room Allocations : "></asp:Label></td><td class="LeftClass">
                                    <asp:RadioButtonList runat="server" ID="uoRBtnAllocations" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Text="Contract / Override" Value="0"></asp:ListItem>
                                        <%--<asp:ListItem Text="Override" Value="1"></asp:ListItem>--%>
                                        <asp:ListItem Text="Emergency" Value="2"></asp:ListItem></asp:RadioButtonList><asp:RequiredFieldValidator runat="server" ID="rfvAllocations" ControlToValidate="uoRBtnAllocations"
                                        ValidationGroup="Save" ErrorMessage="Room Allocation Required.">*</asp:RequiredFieldValidator></td></tr><tr>
                                <td class="LeftClass">
                                    Hotel Status :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:DropDownList runat="server" ID="uoDropDownListStatus" Width="200px">
                                        <asp:ListItem Selected="True" Text="UNUSED" Value="Unused"></asp:ListItem><asp:ListItem Text="CHECKED IN" Value="Checked In"></asp:ListItem><asp:ListItem Text="CHECKED OUT" Value="Checked Out"></asp:ListItem><asp:ListItem Text="CANCELLED" Value="Cancelled"></asp:ListItem><asp:ListItem Text="NO SHOW" Value="No Show"></asp:ListItem></asp:DropDownList></td></tr><tr>
                                <td class="LeftClass">
                                    Room Type :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:DropDownList runat="server" ID="uoDropDownListRoomType" Width="200px" onchange="return getRoomType(this);">
                                        <asp:ListItem Selected="True" Text="Single" Value="1"></asp:ListItem><asp:ListItem Text="Double" Value="2"></asp:ListItem></asp:DropDownList><asp:RequiredFieldValidator runat="server" ID="rfvRoom" ValidationGroup="Save" ControlToValidate="uoDropDownListRoomType">*</asp:RequiredFieldValidator></td></tr><tr>
                                <td class="LeftClass">
                                    Check-In Date :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:TextBox runat="server" ID="uoTxtBoxCheckIn" Height="22px" Width="65px" onchange="return OnDateChange(this);"></asp:TextBox>
                                    <cc1:TextBoxWatermarkExtender ID="uoTxtBoxCheckIn_TextBoxWatermarkExtender" runat="server"
                                        Enabled="True" TargetControlID="uoTxtBoxCheckIn" WatermarkCssClass="fieldWatermark"
                                        WatermarkText="MM/dd/yyyy">
                                    </cc1:TextBoxWatermarkExtender>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <cc1:CalendarExtender ID="uoTxtBoxCheckIn_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="uoTxtBoxCheckIn" PopupButtonID="ImageButton1" Format="MM/dd/yyyy">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="uoTxtBoxCheckIn_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTxtBoxCheckIn"
                                        UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvCheckIn" ValidationGroup="Save"
                                        ErrorMessage="Check In date required." ControlToValidate="uoTxtBoxCheckIn">*</asp:RequiredFieldValidator>
                                    <asp:Button runat="server" ID="uoBtnCheckRooms" Text="Check Rooms" Width="100px"
                                        CssClass="SmallButton" Visible='<%# !Convert.ToBoolean(Eval("isAccredited")) %>' />
                                </td>
                            </tr>
                            <tr id="Tr3" visible='<%# !Convert.ToBoolean(Eval("isAccredited")) %>' runat="server">
                                <td>
                                </td>
                                <td class="LeftClass">
                                    <span class="RedNotification">Click Check Rooms button
                                        <br />
                                        to validate available rooms.</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass">
                                    No. of Nites :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:TextBox runat="server" ID="uoTxtBoxDays" Text="1" Width="90px" MaxLength="3"
                                        onkeyup="validateDays();" onkeypress="return validate(event);" onchange="GetNoOfNites(this)"></asp:TextBox><cc1:NumericUpDownExtender ID="uoTxtBoxDays_NumericUpDownExtender" runat="server"
                                        Enabled="True" Maximum="100" Minimum="1" RefValues="" ServiceDownMethod="" ServiceDownPath=""
                                        ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="uoTxtBoxDays"
                                        Width="90">
                                    </cc1:NumericUpDownExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass">
                                    Time :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:TextBox runat="server" ID="uoTxtBoxTime" Width="65px" Text="00:00"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="uoTxtBoxTime_TextBoxWatermarkExtender" runat="server"
                                        Enabled="True" TargetControlID="uoTxtBoxTime" WatermarkCssClass="fieldWatermark"
                                        WatermarkText="HH:mm">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:MaskedEditExtender ID="uoMaskedEditExtenderCheckInTime" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTxtBoxTime" UserDateFormat="None"
                                        UserTimeFormat="TwentyFourHour">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass">
                                    With Shuttle :
                                </td>
                                <td class="LeftClass">
                                    <asp:CheckBox runat="server" ID="uoCheckBoxShuttle" Text="Transportation" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass">
                                    Remarks :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:TextBox runat="server" ID="uoTxtBoxRemarks" Width="200px" Height="50px" TextMode="MultiLine"></asp:TextBox></td></tr><tr>
                                <td class="LeftClass">
                                    Confirmation No. :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:TextBox runat="server" ID="uoTxtBoxConfirmation" Width="200px"></asp:TextBox></td></tr><tr>
                                <td>
                                </td>
                                <td class="rightAligned">
                                    <asp:Button runat="server" ID="uoButtonSave" ValidationGroup="Save" Text="Save" CssClass="SmallButton"
                                        OnClientClick="return Save();"  />
                                    &nbsp;
                                    <asp:Button runat="server" ID="uoButtonCancel" Text="Cancel" CssClass="SmallButton"
                                         />
                                </td>
                            </tr>
                            <tr  style=" height:8px"></tr>
                        </table>
                        
                    </asp:Panel>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>
    </div>
            
        </td>
    </tr>
    
    
    </table>
   
        
    <asp:HiddenField runat="server" ID="uoHiddenFieldBranch" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldPopEditor" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDateChanged" Value="1" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDaysChanged" Value="1" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRoomTypeChanged" Value="1" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHIddenFieldBranchId" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHIddenFieldSFCount" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRoomChecked" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldPopupHotel" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDate" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDateTo" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRoomType" EnableViewState="true" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldNoOfNites" EnableViewState="true" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldSelectedRoom" EnableViewState="true" Value="1" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRemoveFromList" EnableViewState="true" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldSortBy" EnableViewState="true" Value="" />
    
    <asp:HiddenField runat="server" ID="uoHiddenFieldE1ID" EnableViewState="true" Value="0" />    
    <asp:HiddenField runat="server" ID="uoHiddenFieldName" EnableViewState="true" Value="" />        
    <asp:HiddenField runat="server" ID="uoHiddenFieldRecLoc" EnableViewState="true" Value="" />    
    <asp:HiddenField runat="server" ID="uoHiddenFieldExceptionID" EnableViewState="true" Value="0" />   
        
        
        
        <script language="javascript" type="text/javascript">
            function CloseModal(strURL) {
                window.location = strURL;
            }
            Sys.Application.add_load(function() {
                SetExceptionResolution();
    //            ShowRemoveComment();
            });

            $("#masterDiv").style.display = "hidden";
            
            function SetExceptionResolution() {
                var ht = $(window).height(); // * 0.54; //550;
                var wd = $(window).width() * 0.62;
                var ht2 = $(window).height(); // * 0.60; //550;
                var ht3 = $(window).height(); // * 0.60;
                var wd2 = $(window).width() - (wd + 45);
                if (screen.height <= 600) {
                    ht = ht * 0.30;
                    ht2 = ht2 * 0.40;
                    ht3 = ht3 * 0.40;
                    wd2 = $(window).width() - wd;

                }
                else if (screen.height <= 720) {
                    ht = ht * 0.45;
                    ht2 = ht2 * 0.50;
                    ht3 = ht3 * 0.50;
                }
                else {
                    ht = ht * 0.50;
                    ht2 = ht2 * 0.58;
                }
                $("#<%= uoPanelHotels.ClientID %>").height($(window).height() - 210);
                $("#DivLeft").width(wd);
                $("#Bv").width(wd);
                
                $("#HotelDiv").width(wd2);
                document.getElementById("masterDiv").style.border="0px";

            }
            
            function divScrollL() {
                var Right = document.getElementById('DivLeft');
                var Left = document.getElementById('Bv');
                Right.scrollLeft = Left.scrollLeft;
            }
    
    </script>
    
    
    
    
           
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server" >
    
</asp:Content>
