<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true" 
    CodeBehind="PortAgentVehicleManifest2.aspx.cs" 
    Inherits="TRAVELMART.PortAgentVehicleManifest2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" style="height:25px;">
        <tr class="PageTitle">
            <td align="left" style="vertical-align:middle">
                <asp:Label ID="Label7" runat="server" class="Title" Text="Service Provider Vehicle Manifest"></asp:Label>
            </td> 
            <td style="width:355px;text-align:right; display:none;" >
                <asp:DropDownList runat="server" ID ="uoDropDownListPortAgent" 
                    AppendDataBoundItems="true" Width="350px" AutoPostBack="true" 
                    onselectedindexchanged="uoDropDownListPortAgent_SelectedIndexChanged">
                </asp:DropDownList>                
            </td>             
            <td style="width:255px;text-align:right; display:none;">
                <asp:DropDownList runat="server" ID ="uoDropDownListStatus" 
                    AppendDataBoundItems="true" Width="250px" AutoPostBack="true" 
                    onselectedindexchanged="uoDropDownListStatus_SelectedIndexChanged">
                </asp:DropDownList>
                
            </td>
             <td align="right" style="white-space: nowrap; width: 160px; display:none;">
                <asp:DropDownList runat="server" ID ="uoDropDownListDays" 
                    AppendDataBoundItems="true" Width="160px" AutoPostBack="true" 
                    onselectedindexchanged="uoDropDownListDays_SelectedIndexChanged">
                </asp:DropDownList>       
            </td>            
        </tr>        
    </table>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">         
    <table width="100%" cellpadding="0" cellspacing="0" class="LeftClass">
        <tr >
            <%--<td style="width:355px;">              
                 <asp:DropDownList runat="server" ID ="uoDropDownListPortAgent" 
                    AppendDataBoundItems="true" Width="350px" AutoPostBack="true" 
                    onselectedindexchanged="uoDropDownListPortAgent_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width:255px;">
                <asp:DropDownList runat="server" ID ="uoDropDownListStatus" 
                    AppendDataBoundItems="true" Width="250px" AutoPostBack="true" 
                    onselectedindexchanged="uoDropDownListStatus_SelectedIndexChanged">
                </asp:DropDownList>
                
            </td>--%>
            <td >
                <asp:Button runat="server" ID="uoButtonExport" Text="Export" Width="100px" 
                    CssClass="SmallButton" onclick="uoButtonExport_Click"/>
                 <a id="linkConfirm" href="PortAgentVehicleEditor2.aspx" runat="server" style="display:none" >
                    <asp:Button runat="server" ID = "uoButtonConfirm" Text="Confirm" 
                    CssClass="SmallButton" Width="100px" OnClientClick="javascript: return false;"/>                    
                 </a>
                 
                 <a id="linkCancel" href="PortAgentVehicleEditor2.aspx" runat="server" >
                    <asp:Button runat="server" ID = "uoButtonCancelVendor" Text="Cancel"
                     CssClass="SmallButton" Width="100px" OnClientClick="javascript: return false;"/>
                 </a>
                 
                 <a id="linkEditAmount" href="PortAgentVehicleEditor2.aspx" runat="server" style="display:none" >
                    <asp:Button runat="server" ID = "uoButtonEdit" Text="Edit Amount"
                     CssClass="SmallButton" Width="100px" OnClientClick="javascript: return false;"/>                 
                 </a>
                 <a id="linkApprove" href="PortAgentVehicleEditor2.aspx" runat="server" style="display:none" >
                    <asp:Button runat="server" ID = "uoButtonApprove" Text="Approve"
                     CssClass="SmallButton" Width="100px" OnClientClick="javascript: return false;"/>
                </a>
                <a id="linkCancelRCCL" href="PortAgentVehicleEditor2.aspx" runat="server" style="display:none">
                 <asp:Button runat="server" ID = "uoButtonCancelRCCL" Text="Cancel"
                     CssClass="SmallButton" Width="100px" OnClientClick="javascript: return false;"/>
                </a>
            </td>   
            <td style ="white-space:nowrap; width:100%;padding-left:20px;  ">
                E1 ID: &nbsp;<asp:TextBox runat="server" ID="uoTextBoxSeafarerID" CssClass="SmallText" onkeypress="return validate(event);"></asp:TextBox>&nbsp;
                <asp:Button runat="server" ID="uoButtonSearch" Text="Search" Width="100px" 
                    CssClass="SmallButton" onclick="uoButtonSearch_Click"/>
            </td>          
        </tr>        
    </table>    
    <br />
    
   
   <%--===================================================================================================--%>
   <%--Hotel Service Manifest--%>
 
    <div id="uoDivHotelManifest" >       
        <table width="100%" cellpadding="0" cellspacing="0" class="LeftClass">
            <tr>
                <td>                 
                    <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
                        <asp:ListView runat="server" ID="uoListViewVehicleHeader" OnItemCommand="uoListViewVehicleHeader_ItemCommand">
                        <LayoutTemplate>
                        </LayoutTemplate>
                        <ItemTemplate>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table class="listViewTable">
                                <tr>     
                                    <th style="text-align: center; white-space: normal; display:none; ">
                                        <asp:Label ID="uoLabeApprove" runat="server" Text="Confirm" Width="50px" />
                                        <asp:CheckBox runat="server" ID = "uoCheckBoxApproveAll" Width="50px" OnClick="CheckUncheckConfirmTransportAll(this);"/>                                            
                                    </th>   
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:Label ID="Label8" runat="server" Text="Cancel" Width="50px" />
                                        <asp:CheckBox runat="server" ID = "uoCheckBoxCancelAll" Width="50px" OnClick="CheckUncheckCancelAll(this);"/>                                            
                                    </th> 
                                     <th runat="server" id="TagTH">
                                        <asp:Label runat="server" ID="Label7" Text="Tagged" Width="50px" ></asp:Label>
                                    </th> 
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton5" runat="server" CommandName="RequestStatus"
                                            Text="Request Status" Width="55px" />
                                    </th>                   
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLinkButtonE1" runat="server" CommandName="EmployeeId"
                                            Text="E1 ID" Width="55px" />
                                    </th>
                                     <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblLastNameHeader" runat="server" CommandName="LastName"
                                            Text="Last Name" Width="145px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblFirstNameHeader" runat="server" CommandName="FirstName"
                                            Text="First Name" Width="141px" />
                                    </th>                                    
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="ConfirmationNo"
                                            Text="Confirmation #" Width="95px" />
                                    </th>
                                    
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblCheckInHeader" runat="server" CommandName="PickupDate"
                                            Text="Pickup Date" Width="64px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblCheckOutHeader" runat="server" CommandName="PickupTime"
                                            Text="Pickup Time" Width="64px" />
                                    </th>
                                                                     
                                  
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="VehicleType"
                                            Text="Vehicle Type" Width="65px" />
                                    </th>
                                    
                                     <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton8" runat="server" CommandName="RouteFrom"
                                            Text="Route From" Width="68px" />
                                    </th>
                                     <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton9" runat="server" CommandName="RouteTo"
                                            Text="Route To" Width="65px" />
                                    </th>
                                   
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton10" runat="server" CommandName="RouteFromVarchar"
                                            Text="From" Width="190px" />
                                    </th>
                                     <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton11" runat="server" CommandName="RouteToVarchar"
                                            Text="To" Width="190px" />
                                    </th>
                                   
                                   
                                     <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblShipHeader" runat="server" CommandName="VesselName" Text="Ship"
                                            Width="164px" />
                                    </th>
                                    
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblTitleHeader" runat="server" CommandName="RankName" Text="Title"
                                            Width="215px" />
                                    </th>
                                  
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="CostCenter" Text="Cost Center"
                                            Width="105px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Nationality" Text="Nationality"
                                            Width="105px" />
                                    </th>
                                    
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblRLHeader" runat="server" CommandName="RecordLocator"
                                            Text="Record Locator" Width="55px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblDCityHeader" runat="server" CommandName="DeptCity"
                                            Text="Dept City" Width="45px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblACityHeader" runat="server" CommandName="ArvlCity"
                                            Text="Arvl City" Width="43px" />
                                    </th>
                                    
                                    
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="DeptDate" Text="Dept Date"
                                            Width="60px" />
                                    </th>
             
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="ArvlDate" Text="Arvl Date"
                                            Width="60px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblDTimeHeader" runat="server" CommandName="DeptTime"
                                            Text="Dept Time" Width="53px" />
                                    </th>
                                    <!--
                                    <th style="text-align:center; white-space:normal;">
                                           <asp:LinkButton ID="LinkButton16" runat="server" CommandName="ArvlCity"
                                            Text="Actual Departure Date" Width="43px" />                                    
                                    </th>                                    
                                    -->
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblATimeHeader" runat="server" CommandName="ArvlTime"
                                            Text="Arvl Time" Width="53px" />
                                    </th>
                                    <!--
                                     <th style="text-align: center; white-space: normal;">
                                          <asp:LinkButton ID="LinkButton12" runat="server" CommandName="ArvlCity"
                                            Text="Actual Arrival Date" Width="43px" />                                  
                                    </th>
                                    -->
                            
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblCarrierHeader" runat="server" CommandName="Carrier"
                                            Text="Carrier" Width="35px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblFlightNumHeader" runat="server" CommandName="FlightNo"
                                            Text="Flight No." Width="44px" />
                                    </th>
                                    <!--
                                    <th style="text-align: center; white-space: normal;">
                                          <asp:LinkButton ID="LinkButton13" runat="server" CommandName="ArvlCity"
                                            Text="Arrival Gate" Width="43px" />                                    
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                          <asp:LinkButton ID="LinkButton14" runat="server" CommandName="ArvlCity"
                                            Text="Flight Status" Width="43px" />                                    
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                          <asp:LinkButton ID="LinkButton15" runat="server" CommandName="ArvlCity"
                                            Text="Baggage Claim" Width="43px" />                                    
                                    </th>
                                   -->
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblPNumHeader" runat="server" CommandName="PassportNo"
                                            Text="Passport No." Width="57px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblIssuedHeader" runat="server" CommandName="PassportDateIssued"
                                            Text="Date Issued" Width="60px" />
                                    </th>                            
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblExpHeader" runat="server" CommandName="PassportExpiration"
                                            Text="Passport Expiration" Width="59px" />
                                    </th>  
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton6" runat="server" CommandName="RateContracted"
                                            Text="Contracted Rate" Width="59px" />
                                    </th>                                                                                         
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton7" runat="server" CommandName="RateConfirmed"
                                            Text="Confirmed Rate" Width="59px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:Label ID="Label9" runat="server"  Text="Comment" Width="205px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:Label ID="Label16" runat="server"  Text="Transportation Details" Width="205px" />
                                    </th>                                    
                                     <th style="text-align: center; white-space: normal;">
                                        <asp:Label ID="Label10" runat="server"  Text="Confirmed By" Width="105px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:Label ID="Label11" runat="server"  Text="Commented By" Width="105px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:Label runat="server" ID="Label2" Text="Remark Booking" Width="105px"></asp:Label>
                                    </th>
                                    
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:Label runat="server" ID="Label20" Text="" Width="10px"></asp:Label>
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
                <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
                    onscroll="divScrollL();">
                    <asp:ListView runat="server" ID="uolistviewVehicleInfo" 
                        OnDataBound="uolistviewVehicleInfo_DataBound" 
                        onitemcommand="uolistviewVehicleInfo_ItemCommand" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" id="uoTableHotel">
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <%# HotelAddGroup()%>
                            <tr>   
                                <td class="centerAligned" style="white-space: normal; display:none;">
                                    <asp:HiddenField runat="server" ID="uoHiddenFieldListRecLocID" Value='<%# Eval("IdBigint") %>'/>
                                    <asp:HiddenField runat="server" ID="uoHiddenFieldListTRID" Value='<%# Eval("TravelReqID") %>'/>
                                    <asp:HiddenField runat="server" ID="uoHiddenFieldTransID" Value='<%# Eval("TransVehicleID") %>'/>
                                    <asp:HiddenField runat="server" ID="uoHiddenFieldStatus" Value='<%# Eval("CrewStatus") %>'/>
                                    <asp:CheckBox runat="server" ID = "uoCheckBoxConfirm" Visible = '<%# Eval("IsConfirmVisible") %>' Width="50px"
                                        OnClick='CheckUncheckConfirmTransport();'/>
                                    <asp:Label runat="server" ID="Label6" Text=''  Visible = '<%# !Convert.ToBoolean(Eval("IsConfirmVisible")) %>' Width="50px"></asp:Label>
                                </td>  
                                <td class="centerAligned" style="white-space: normal;">                                    
                                    <asp:CheckBox runat="server" ID = "uoCheckBoxCancel" Visible = '<%# Eval("IsCancelVisible") %>' Width="50px"
                                        OnClick='CheckUncheckCancelTransport();'/>
                                    <asp:Label runat="server" ID="LabelCancel" Text=''  Visible = '<%# !Convert.ToBoolean(Eval("IsConfirmVisible")) %>' Width="50px"></asp:Label>
                                </td> 
                                
                                <td runat="server" id= "TagTD">
                                    <asp:Label ID="Label26" Width="50px" runat="server" Text="Tagged" Visible='<%# Convert.ToBoolean(Eval("IsTagged"))%>'></asp:Label>
                                    <asp:Label ID="Label18" Width="50px" runat="server" Text="" Visible='<%# ((!Convert.ToBoolean(Eval("IsTagged")) && !Convert.ToBoolean(Eval("IsVendor"))) == true? true: false)%>'></asp:Label>
                                    <asp:LinkButton Width="54px" ID="uoLnkBtnTag" CommandName="Tag" CssClass="BtnTag"  Visible='<%# ((!Convert.ToBoolean(Eval("IsTagged")) && Convert.ToBoolean(Eval("IsVendor"))) == true? true: false)%>'
                                        OnClientClick='<%# "javascript:return confirmTag("+ Eval("SeafarerIdInt") + ", \"" + Eval("FirstName") + "\");" %>'
                                        CommandArgument='<%# Eval("IdBigint") + ":" + Eval("TravelReqID") + ":" + Eval("PortAgentID") %>'
                                        runat="server" Text="Tag">                                
                                    </asp:LinkButton>
                                </td>
                                
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("RequestStatus")%>' Width="60px"></asp:Label>
                                </td>  
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("SeafarerIdInt")%>' Width="63px"></asp:Label>
                                </td>  
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelLast" Text='<%# Eval("LastName")%>' Width="150px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:LinkButton Width="150px" runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("SeafarerIdInt") + "&trID=" + Eval("TravelReqID") + "&st=" + Eval("CrewStatus") + "&recloc=" + Eval("RecordLocator") + "&manualReqID=0&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IdBigint") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("FirstName") %></asp:LinkButton>
                                </td>
                               <%-- <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoTextBoxHotelName" Text='<%# Eval("VehicleVendorName")%>' Width="150px"></asp:Label>
                                </td> --%>    
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoTextBoxConfirmationNo" Text='<%# Eval("ConfirmationNo")%>' Width="100px"></asp:Label>
                                </td>                                                                                                         
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblCheckIn" Text='<%# String.Format("{0:dd-MMM-yyyy}",Eval("PickupDate"))%>'
                                        Width="70px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblCheckOut" Text='<%# String.Format("{0:hh-mm}", Eval("PickupTime"))%>'
                                        Width="70px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblNites" Text='<%# Eval("VehicleTypeName")%>' Width="70px"></asp:Label>
                                </td>                           
                                                                                     
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblRoom" Text='<%# Eval("RouteFrom")%>' Width="74px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label8" Text='<%# Eval("RouteTo")%>' Width="70px"></asp:Label>
                                </td>
                               
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label9" Text='<%# Eval("RouteFromDisplay")%>' Width="198px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label10" Text='<%# Eval("RouteToDisplay")%>' Width="198px"></asp:Label>
                                </td>                             
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblVessel" Text='<%# Eval("VesselName")%>'
                                        Width="170px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblRank" Text='<%# Eval("RankName")%>' Width="220px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("CostCenter")%>' Width="110px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label15" Text='<%# Eval("Nationality")%>' Width="110px"></asp:Label>
                                </td>                       
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator")%>' Width="65px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblDeptCity" Text='<%# Eval("DeptCity")%>' Width="50px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("ArvlCity")%>' Width="50px"></asp:Label>
                                </td>
  
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblDepDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DeptDate"))%>'
                                        Width="67px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblArDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("ArvlDate"))%>'
                                        Width="67px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblDepartureTime" Text='<%# Eval("DeptTime")%>' Width="58px"></asp:Label>
                                </td>
                                <!--
                                <td class="leftAligned" style="white-space: normal;">
                                     <asp:Label runat="server" ID="Label23" Text='<%# Eval("ActualDepartureDate")%>' Width="50px"></asp:Label>
                                </td>    -->                            
                                
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblArrivalTime" Text='<%# Eval("ArvlTime")%>' Width="60px"></asp:Label>
                                </td>
                                <!--
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label19" Text='<%# Eval("ActualArrivalDate")%>' Width="50px"></asp:Label>
                                </td>     -->                                                                      
                                
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="40px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblFN" Text='<%# Eval("FlightNo")%>' Width="50px"></asp:Label>
                                </td>
                                <!--
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label20" Text='<%# Eval("ActualArrivalGate")%>' Width="50px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label21" Text='<%# Eval("ActualArrivalStatus")%>' Width="50px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label22" Text='<%# Eval("ActualArrivalBaggage")%>' Width="50px"></asp:Label>
                                </td> -->
                                                                                               
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblPNum" Text='<%# Eval("PassportNo")%>' Width="65px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblIssuedBy" Text='<%# Eval("PassportIssued")%>'
                                        Width="65px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblExp" Text='<%# Eval("PassportExp")%>' Width="65px"></asp:Label>
                                </td>  
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label4" Text='<%# String.Format("{0:0.00}", Eval("RateContracted")) %>' width="65px"></asp:Label>
                                </td>  
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label5" Text='<%# String.Format("{0:0.00}", Eval("RateConfirmed")) %>' Width="70px"></asp:Label>
                                </td>
                                
                                 <td class="leftAligned" style="white-space: normal;">                                    
                                    <a href="#uoDivVehicleRemarks" id="linkRemarks"
                                        onclick="<%# "getRemarks('"+ Eval("TravelReqID") +"','"+ Eval("RecordLocator") +"')" %>">
                                        <asp:Label runat="server" ID="Label12" Text='<%# Eval("Comment")%>'
                                        Width="205px"></asp:Label>
                                    </a>                                    
                                </td> 
                                 <td class="leftAligned" style="width:405px;white-space: normal;">                                
                                        <asp:Label runat="server" ID="Label17" Text='<%# Eval("TransportationDetails")%>'
                                        Width="205px"></asp:Label>
                                                                        
                                </td>                                                                                             
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label13" Text='<%# Eval("ConfirmedBy")%>'
                                        Width="105px"></asp:Label>
                                </td>                                                             
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label14" Text='<%# Eval("CommentBy")%>'
                                        Width="105px"></asp:Label>
                                </td>    
                                
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="lblRemark" Text='<%# Eval("Remark")%>'
                                        Width="105px"></asp:Label>
                                </td>          
                                <td style="text-align: center; white-space: normal;">
                                   <%-- <asp:Label runat="server" ID="LabelRamark" Text="" Width="10px"></asp:Label>--%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table class="listViewTable">
                                <tr>
                                    <td colspan="33" class="leftAligned">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <div align="left">
                        <asp:DataPager ID="uolistviewVehicleInfoPager" runat="server" PagedControlID="uolistviewVehicleInfo"
                            PageSize="20">
                            <Fields>
                                <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </div>                
            </td>
            </tr>
        </table>
    </div>
    
    <asp:ObjectDataSource ID="uoObjectDataSourceManifest" runat="server" MaximumRowsParameterName="iMaxRow"
            SelectCountMethod="GetVehicleConfirmManifestCount" SelectMethod="GetVehicleConfirmManifestList"
            StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.PortAgentVehicleManifest"
            OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True">
            <SelectParameters>
                <asp:Parameter Name="sType" Type="String" DefaultValue="New" />                
                 <asp:ControlParameter Name="iStatusID" ControlID="ctl00$HeaderContent$uoDropDownListStatus" />
                <asp:ControlParameter Name="iPortAgentID" ControlID="ctl00$HeaderContent$uoDropDownListPortAgent" />                
                <asp:ControlParameter Name="sDate" ControlID="uoHiddenFieldDate" />                
                <asp:ControlParameter Name="sUserID" ControlID="uoHiddenFieldUser" />
                <asp:ControlParameter Name="sRole" ControlID="uoHiddenFieldRole" />
                <asp:ControlParameter Name="sOrderBy" ControlID="uoHiddenFieldOrder" />
                <asp:ControlParameter Name="iLoadType" ControlID="uoHiddenFieldLoadType" />                                                                              
                <asp:ControlParameter Name="iNoOfDay" ControlID="ctl00$HeaderContent$uoDropDownListDays" />
                <asp:ControlParameter Name="iSFID" ControlID="uoTextBoxSeafarerID" />
            </SelectParameters>
    </asp:ObjectDataSource>
    
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />    
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />    
    
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldOrder" runat="server" Value="RequestStatus"/>
    <asp:HiddenField ID="uoHiddenFieldPortAgentID" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldVehicleConfirm" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldManifestStatus" runat="server" />
    
    <asp:HiddenField ID="uoHiddenFieldTRID" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldRecLoc" runat="server" />
    
    <asp:HiddenField ID="uoHiddenFieldParamTRID" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldParamRecLoc" runat="server" />
    
    
    <script type="text/javascript" language="javascript">
        Sys.Application.add_load(function() {
            SetTMResolution();
            ControlSettings();
        });
        
        function SetTMResolution() {
            var ht = $(window).height();
            var ht2 = $(window).height();

            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.22;
                ht2 = ht2 * 0.46;
                wd = $(window).width();
            }
            else if (screen.height <= 720) {
                ht = ht * 0.28;
                ht2 = ht2 * 0.59;
            }
            else {
                ht = ht * 0.50;
                ht2 = ht2 * 0.62;
            }

            $("#Bv").height(ht);
            $("#Av").width(wd);
            $("#Bv").width(wd);
        }

        function getRemarks(sTRid, sRecloc) {
            $("#ctl00_NaviPlaceHolder_uoHiddenFieldParamTRID").val(sTRid);
            $("#ctl00_NaviPlaceHolder_uoHiddenFieldParamRecLoc").val(sRecloc);
        
        }
        function getRemarksDiv(sTRid, sRecloc) {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../PageMethods.aspx/GetTransactionRemarks",
                data: "{'sTRid': '" + sTRid + "', 'sRecloc': '" + sRecloc + "', 'sExpenseType': '2'}",
                dataType: "json",
                success: function(data) {
                    $("#uoULRemarks").empty();

                    for (var i = 0; i < data.d.length; i++) {
                        var sContent = "<li class='RemarkDiv'><span style='font-style:normal; font-size:14px; font-weight:bold;'>" + data.d[i].RemarksBy + '</span>';
                        sContent = sContent + "<br/>" + data.d[i].Remarks;
                        sContent = sContent + "<br/><span style='font-size:small; color:#949098'>" + data.d[i].Resource + '</span>';
                        sContent = sContent + " &nbsp;<span style='font-size:small; color:#949098; white-space:nowrap; '>" + data.d[i].RemarksDate + "</span></li>";

                        $("#uoULRemarks").append(sContent);
                    }
                    return true;
                }
                    ,
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                    return false;
                }
            });                       
        }

        function ControlSettings() {
            $("a#linkRemarks").fancybox(
            {
                'centerOnScroll': false,
                'width': '50%',
                'height': '70%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'scrolling': 'auto',
                'onStart': function() {
                
                    var sTRID, sRecLoc;
                    sTRID = $("#ctl00_NaviPlaceHolder_uoHiddenFieldParamTRID").val();
                    sRecLoc = $("#ctl00_NaviPlaceHolder_uoHiddenFieldParamRecLoc").val();
                    
                    return getRemarksDiv(sTRID, sRecLoc);
                }
            });
            

            $("#<%=linkConfirm.ClientID %>").fancybox(
            {
                'centerOnScroll': false,
                'width': '80%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onStart': function() {
                    return ValidateIfCheck(true);
                },
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldVehicleConfirm.ClientID %>").val();

                    if (a == '2') {
                        $("#aspnetForm").submit();
                        alert('Email successfully sent!');
                    }
                    else {
                        $("#aspnetForm").submit();
                    }
                }
            });

            $("#<%=linkEditAmount.ClientID %>").fancybox(
            {
                'centerOnScroll': false,
                'width': '80%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onStart': function() {
                    return ValidateIfCheck(false);
                },
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldVehicleConfirm.ClientID %>").val();

                    if (a == '2') {
                        $("#aspnetForm").submit();
                        alert('Email successfully sent!');
                    }
                    else {
                        $("#aspnetForm").submit();
                    }
                }
            });


            $("#<%=linkApprove.ClientID %>").fancybox(
            {
                'centerOnScroll': false,
                'width': '80%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onStart': function() {
                    return ValidateIfCheck(false);
                },
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldVehicleConfirm.ClientID %>").val();

                    if (a == '2') {
                        $("#aspnetForm").submit();
                        alert('Email successfully sent!');
                    }
                    else {
                        $("#aspnetForm").submit();
                    }
                }
            });

            $("#ctl00_NaviPlaceHolder_linkCancel").fancybox(
            {
                'centerOnScroll': false,
                'width': '62.5%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onStart': function() {
                    return ValidateCancelIfCheck();
                },
                'onClosed': function() {
                    var a = $("#ctl00_NaviPlaceHolder_uoHiddenFieldVehicleConfirm").val();

                    if (a == '2') {
                        $("#aspnetForm").submit();
                        alert('Email successfully sent!');
                    }
                    else {
                        $("#aspnetForm").submit();
                    }
                }
            });

            $("#ctl00_NaviPlaceHolder_linkCancelRCCL").fancybox(
            {
                'centerOnScroll': false,
                'width': '61.5%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                 'onStart': function() {
                    return ValidateCancelIfCheck();
                },
                'onClosed': function() {
                    var a = $("#ctl00_NaviPlaceHolder_uoHiddenFieldVehicleConfirm").val();
                    
                    if (a == '2') {
                        $("#aspnetForm").submit();
                        alert('Email successfully sent!');
                    }
                    else {
                        $("#aspnetForm").submit();
                    }
                }
            });
        }

        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;
        }

        function CheckUncheckConfirmTransport() {

            var sAddCancel = '',
                sRecordLocID = '',
                sTReqID = '',
                sTransID = '',
                sRole = $("#<%=uoHiddenFieldRole.ClientID %>").val();

            $("#uoTableHotel tr").each(function(i, element) {
                var $TDs = $(this).find('td');
                var chk = $('td:eq(0) input[name*="uoCheckBoxConfirm"]', this);
                //alert(chk.attr('checked'));

                var sStatus = $('td:eq(2) span[id*="uoLabelStatus"]', this);
                var RecLocID = $('td:eq(0) input[name*="uoHiddenFieldListRecLocID"]', this);
                var TravelReqID = $('td:eq(0) input[name*="uoHiddenFieldListTRID"]', this);
                var TransID = $('td:eq(0) input[name*="uoHiddenFieldTransID"]', this);


                if (chk != null) {

                    if (chk.is(':visible')) {
                        if (chk.attr('checked')) {
                            if (sAddCancel == '') {
                                sAddCancel = 'Add';
                            }
                            else {
                                sAddCancel += ',Add';
                            }

                            if (sRecordLocID == '') {
                                sRecordLocID = RecLocID.val();
                            }
                            else {
                                sRecordLocID += ',' + RecLocID.val();
                            }

                            if (sTReqID == '') {
                                sTReqID = TravelReqID.val();
                            }
                            else {
                                sTReqID += ',' + TravelReqID.val();
                            }

                            if (sTransID == '') {
                                sTransID = TransID.val();
                            }
                            else {
                                sTransID += ',' + TransID.val();
                            }
                        }
                    }
                    else {

                        if (sStatus.html() == 'Cancelled' && sRole == 'Service Provider') {
                            if (sAddCancel == '') {
                                sAddCancel = 'Cancel';
                            }
                            else {
                                sAddCancel += ',Cancel';
                            }

                            if (sRecordLocID == '') {
                                sRecordLocID = RecLocID.val();
                            }
                            else {
                                sRecordLocID += ',' + RecLocID.val();
                            }

                            if (sTReqID == '') {
                                sTReqID = TravelReqID.val();
                            }
                            else {
                                sTReqID += ',' + TravelReqID.val();
                            }

                            if (sTransID == '') {
                                sTransID = TransID.val();
                            }
                            else {
                                sTransID += ',' + TransID.val();
                            }

                        }
                    }
                }
            });



            var sURL = 'PortAgentVehicleEditor2.aspx?AddCancel=' + sAddCancel + '&RecLoc=' + sRecordLocID + '&TReqID=' + sTReqID + '&TransID=' + sTransID;
            $("#<%=linkConfirm.ClientID %>").attr("href", sURL);
            $("#<%=linkEditAmount.ClientID %>").attr("href", sURL + '&Action=EditAmount');
            $("#<%=linkApprove.ClientID %>").attr("href", sURL + '&Action=Approve');

        }

        function CheckUncheckCancelTransport() {

            var sAddCancel = '',
                sRecordLocID = '',
                sTReqID = '',
                sdtValue = '',
                sTransID = '';
                var pRow = 0;
                var PortAgentID = 0;


                $("#uoTableHotel tr").each(function(i, element) {




                    var $TDs = $(this).find('td');
                    var chk = $('td:eq(1) input[name*="uoCheckBoxCancel"]', this);
                    //alert(chk.attr('checked'));

                    var RecLocID = $('td:eq(0) input[name*="uoHiddenFieldListRecLocID"]', this);
                    var TravelReqID = $('td:eq(0) input[name*="uoHiddenFieldListTRID"]', this);
                    var TransID = $('td:eq(0) input[name*="uoHiddenFieldTransID"]', this);
                    //                var seq = $('td:eq(0) input[name*="uoHiddenFieldTransID"]', this);

                    var opt = document.getElementById('ctl00_HeaderContent_uoDropDownListPortAgent');
                    var val = opt.options[opt.selectedIndex];


                    console.log(opt.value);
                    console.log(opt.text);
                    PortAgentID = opt.value;

                    console.log(PortAgentID);

                    if (chk != null) {
                        if (chk.is(':visible')) {
                            if (chk.attr('checked')) {

                                if (sAddCancel == '') {
                                    sAddCancel = 'Cancel';
                                }
                                else {
                                    sAddCancel += ',Cancel';
                                }

                                if (sRecordLocID == '') {
                                    sRecordLocID = RecLocID.val();
                                }
                                else {
                                    sRecordLocID += ',' + RecLocID.val();
                                }

                                if (sTReqID == '') {
                                    sTReqID = TravelReqID.val();
                                }
                                else {
                                    sTReqID += ',' + TravelReqID.val();
                                }

                                if (sTransID == '') {
                                    sTransID = TransID.val();
                                }
                                else {
                                    sTransID += ',' + TransID.val();
                                }


                                pRow += 1;



                            }


                        }
                    }
                });


            console.log(sAddCancel);

            var sURL = 'PortAgentVehicleEditor2.aspx?AddCancel=' + sAddCancel + '&RecLoc=' + sRecordLocID + '&TReqID=' + sTReqID + '&TransID=' + sTransID + '&pRow=' + pRow + '&pPAID=' + PortAgentID;
            
            $("#<%=linkCancel.ClientID %>").attr("href", sURL + '&Action=Cancel');
            $("#<%=linkCancelRCCL.ClientID %>").attr("href", sURL + '&Action=CancelByRCCL');

            console.log(sURL);
            
            
            
            
        }

        function CheckUncheckConfirmTransportAll(obj) {
            $('input[name*="uoCheckBoxConfirm"]').attr('checked', obj.checked);
            CheckUncheckConfirmTransport();
        }
        function CheckUncheckCancelAll(obj) {
            $('input[name*="uoCheckBoxCancel"]').attr('checked', obj.checked);
            CheckUncheckCancelTransport();
        }
        function ValidateIfCheck(IsConfirmButton) {

            var bIsCheck = false;
            var bIsDateTheSame = true;
            
            var dDate = '';
            $("#uoTableHotel tr").each(function(i, element) {
                var $TDs = $(this).find('td');
                var chk = $('td:eq(0) input[name*="uoCheckBoxConfirm"]', this);
                var labelDate = $('td:eq(7) span[id*="uoLblCheckIn"]', this);
                dDate2 = labelDate.html();

                if (chk != null) {
                    if (chk.attr('checked')) {

                        bIsCheck = true;                       

                        if (dDate == '') {
                            dDate = dDate2;
                            
                        }
                        else {
                            if (dDate != dDate2) {
                                bIsDateTheSame = false;
                            }
                        }
                    }
                }                
            });

            if (IsConfirmButton) {
                var sURL = $("#<%=linkConfirm.ClientID %>").attr("href");

                if (sURL != "") {
                    var split = sURL.split('=');
                    if (split[2] != undefined) {
                        var splitRecLoc = split[2].split('&');
                        if (splitRecLoc[0] != undefined) {
                            var sRecLoc = splitRecLoc[0];
                            if (sRecLoc != 'undefined') {
                                if (sRecLoc != '') {
                                    bIsCheck = true;
                                }
                            }
                        }
                    }
                }
            }

           
            if (!bIsCheck) {
                alert("No Selected Record!");
                return false;
            }

            var sRole = $("#<%=uoHiddenFieldRole.ClientID %>").val();

            if (!bIsDateTheSame && sRole == 'Service Provider') {
                alert("Pick-up date should be the same.");
                return false;
            }
           
            return bIsCheck;

        }
        function ValidateCancelIfCheck() {

            var bIsCheck = false;
            $("#uoTableHotel tr").each(function(i, element) {
                var $TDs = $(this).find('td');
                var chk = $('td:eq(1) input[name*="uoCheckBoxCancel"]', this);


                if (chk != null) {
                    if (chk.attr('checked')) {
                        bIsCheck = true;
                    }
                }
            });

            if (!bIsCheck) {
                alert("No selected record to cancel!");
                return false;
            }
            return bIsCheck;
        }
        function confirmTag(e1ID, seafarerName) {
            var sMsg = "Tag " + e1ID + ": " + seafarerName + "? ";
            var x = confirm(sMsg);
            return x;
        }
    </script>
    
   
    <div style="display:none">
    <div id="uoDivVehicleRemarks" style="display:inline">  
       
        <table cellpadding="0"; cellspacing="0"; style="width:500px">
            <tr>
                <td class="PageTitle">
                    Remarks
                </td>
            </tr>
            <tr>
                <td>
                      <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <ul id="uoULRemarks" style="list-style: none;padding:0;">
                                                
                    </ul>
                </td>
            </tr>
        </table>                    
    </div>
    </div>
    
</asp:Content>
