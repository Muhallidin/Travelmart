<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="PortAgentHotelEditor.aspx.cs" Inherits="TRAVELMART.PortAgentHotelEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script> 
    
    <style type="text/css">
        .style2
        {
            width: 279px;
            white-space: nowrap;
        }
        .style6
        {
            width: 200px;
            white-space: nowrap;            
        }
        .style7
        {
            width: 100px;
            white-space: nowrap;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="ViewTitlePadding">
       <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>
                    <asp:Label ID="uoLabelTitle" runat="server" Text="Confirm Hotel Request" CssClass="Title"></asp:Label>                    
                </td>
            </tr>
        </table>
    </div>
    <hr/> 
    <div>
        <table width="100%">
            <tr>
                <td class="style6" >
                    Service Provider Company:
                </td>
                <td  colspan="3">
                    <asp:TextBox runat="server" ID="uoTextBoxVendor" Width="784px" 
                        CssClass="ReadOnly" ReadOnly="true" ></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="style6" >
                    Checkin Date:
                </td>
                <td style="white-space:nowrap" class="style2">
                    <asp:TextBox runat="server" ID="uoTextBoxCheckInDate" Width="241px" 
                        ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>  
                <td class="style7" > Email To:</td>              
                <td style="white-space:nowrap">
                     <asp:TextBox runat="server" ID="uoTextBoxEmailAdd" Width="400px"
                     onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                     ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                     TextMode="MultiLine" Rows="2"></asp:TextBox>
                     &nbsp;
                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ValidationGroup="Email" 
                              ErrorMessage="Currency required." ControlToValidate="uoTextBoxEmailAdd">* </asp:RequiredFieldValidator> 
                </td>
            </tr>
            <tr>
                <td class="style6" >                   
                    Contracted Rate:                                   
                </td>
                <td class="style2" >
                                      
                      <asp:TextBox ID="uoTextBoxRateContract"  Width="240px" runat="server" 
                          ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>                            
                                      
                </td>
                 <td class="style7" > Email Cc: </td>              
                <td>
                     
                    <asp:TextBox runat="server" ID="uoTextBoxCopy" Width="400px"
                     onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                    ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                    TextMode="MultiLine" Rows="2"
                    ></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="style6" >
                    Currency:
                </td>
                <td class="style2" >
                    <asp:DropDownList runat="server" ID="uoDropDownListCurrency" Width="239px" AppendDataBoundItems="true"></asp:DropDownList>              
                    <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListRegion" runat="server"
                                TargetControlID ="uoDropDownListCurrency"
                                PromptText="Type to search"
                                PromptPosition="Top"
                                IsSorted="true"
                                PromptCssClass="dropdownSearch"/>  
                    &nbsp;
                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ValidationGroup="Email" 
                              ErrorMessage="Currency required." InitialValue="0" ControlToValidate="uoDropDownListCurrency">* </asp:RequiredFieldValidator> 
                </td>
                <td class="style7" >
                    Confirmed Rate Inclusive of Tax:
                </td>
                <td style="white-space:nowrap">
                    <%-- <asp:TextBox ID="uoTextBoxRateConfirmed"  Width="155px" runat="server" onkeypress="return validate(event);" ></asp:TextBox>     
                      &nbsp;
                       <asp:RequiredFieldValidator runat="server" ID="uoRequiredFieldValidatorRateConfirm" ValidationGroup="Email" 
                              ErrorMessage="Confirmed Rate required."  ControlToValidate="uoTextBoxRateConfirmed">* </asp:RequiredFieldValidator>   --%>  
                    <table>
                        <tr>
                             <td style="white-space:nowrap">
                                 <asp:TextBox ID="uoTextBoxRateConfirmed"  Width="150px" runat="server" onkeypress="return validate(event);"></asp:TextBox>     
                                  &nbsp;
                                   <asp:RequiredFieldValidator runat="server" ID="uoRequiredFieldValidatorRateConfirm" ValidationGroup="Email" 
                                          ErrorMessage="Confirmed Rate required."  ControlToValidate="uoTextBoxRateConfirmed">* </asp:RequiredFieldValidator>                                
                            </td>
                            <td>
                              <asp:Button  runat="server" id="uoButtonApplyTo" Text="Apply To" CssClass="SmallButton"/>
                            </td>
                            <td>
                                 <asp:DropDownList ID="uoDropDownListRoom" runat="server" Width="150px" 
                                    AppendDataBoundItems="true" >
                                </asp:DropDownList>   
                                             
                            </td>
                        </tr>
                    </table>                            
                </td>
            </tr>
            
             <tr>
                <td>Confirmation No.</td>
                <td class="style2">
                    <asp:TextBox ID="uoTextBoxConfirmation"  Width="240px" runat="server" ></asp:TextBox>      
                    &nbsp;
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ValidationGroup="Email" 
                              ErrorMessage="Confirmation No. required."  ControlToValidate="uoTextBoxConfirmation">* </asp:RequiredFieldValidator> 
                </td>
                <td class="style6" >
                     Hotel Name:
                </td>
                <td  style="white-space:nowrap">
                    <asp:TextBox ID="uoTextBoxHotelname"   Width="400px" runat="server" ></asp:TextBox>     
                      &nbsp;
                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ValidationGroup="Email" 
                              ErrorMessage="Hotel Name required."  ControlToValidate="uoTextBoxHotelname">* </asp:RequiredFieldValidator>     
                </td>                
            </tr>
             <tr>
                <td class="style6" >
                    Comment:
                </td>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="uoTextBoxComment" Width="850px" 
                        TextMode="MultiLine" Rows="2" ></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td>
                    Confirmed By:
                </td>
                <td  style="white-space:nowrap">
                    <asp:TextBox ID="uoTextBoxConfirmedBy"  Width="240px" runat="server"></asp:TextBox>
                    &nbsp;
                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ValidationGroup="Email" 
                              ErrorMessage="Confirmed by required."  ControlToValidate="uoTextBoxConfirmedBy">* </asp:RequiredFieldValidator>      
                </td>
                <td class="style6" >
                     <asp:Label runat ="server" ID="uoLabelSource" Text="Request Source:"></asp:Label>
                </td>
                <td  style="white-space:nowrap">                    
                    <asp:DropDownList ID="uoDropDownListRequestSource" runat="server" Width="400px" 
                        AppendDataBoundItems="true" >
                    </asp:DropDownList>                                                
                </td>  
            </tr>
            <tr>
                <td class="style6" >
                   
                </td>
                <td colspan="3">
                    <asp:Button runat="server" ID="uoButtonEmail" OnClientClick="return confimSave();"
                        Text="Confirm"  ValidationGroup="Email" Width="100px"
                        CssClass="SmallButton" onclick="uoButtonEmail_Click"/>                    
                </td>
            </tr>
            <tr>
                <td class="style6"></td>
                <td  colspan="3">
                    <asp:Label ID="Label5"  CssClass="RedNotification" runat="server" 
                    Text="Multiple emails should be separated by semicolon (i.e.  abc@rccl.com;xyz@rccl.com)"> </asp:Label> 
                </td>
            </tr>
        </table>
    </div>
    <%--=====================Start: Hotel Manifest==========================--%>
    <div id="Div2" class="PageSubTitle" style="text-decoration: underline;">
        <asp:Label ID="uoLabelSubTitle" runat="server" Text="Hotel Manifest"></asp:Label>
    </div>
    
        <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
            <asp:ListView runat="server" ID="uoListViewHotelHeader" OnItemCommand="uoListViewHotelHeader_ItemCommand">
            <LayoutTemplate>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="listViewTable">
                    <tr>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="RateContracted"
                                Text="Contracted Rate" Width="59px" />
                        </th>                                                                                         
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton7" runat="server" CommandName="RateConfirmed"
                                Text="Confirmed Rate" Width="63px" />
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
                                Text="First Name" Width="144px" />
                        </th>                        
                        
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblCheckInHeader" runat="server" CommandName="CheckIn"
                                Text="Check-In" Width="64px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblCheckOutHeader" runat="server" CommandName="CheckOut"
                                Text="Check-Out" Width="64px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblNitesHeader" runat="server" CommandName="HotelNites"
                                Text="Hotel Nites" Width="34px" />
                        </th>
                        
                      
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="RoomType"
                                Text="Room Type" Width="65px" />
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
                                Width="100px" />
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
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblATimeHeader" runat="server" CommandName="ArvlTime"
                                Text="Arvl Time" Width="53px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblCarrierHeader" runat="server" CommandName="Carrier"
                                Text="Carrier" Width="35px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblFlightNumHeader" runat="server" CommandName="FlightNo"
                                Text="Flight No." Width="44px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblVoucherHeader" runat="server" CommandName="Voucher"
                                Text="Voucher" Width="44px" />
                        </th>                     
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
        onscroll="divScrollL();">
        <asp:ListView runat="server" ID="uolistviewHotelInfo" >
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" id="uoTableHotel">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                 <%# OverflowChangeRowColor()%>
                <tr>   
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:HiddenField runat="server" ID="uoHiddenFieldListRecLocID" Value='<%# Eval("IdBigint") %>'/>
                        <asp:HiddenField runat="server" ID="uoHiddenFieldListTRID" Value='<%# Eval("TravelReqID") %>'/>
                        <asp:HiddenField runat="server" ID="uoHiddenFieldTransID" Value='<%# Eval("TransHotelID") %>'/>
                        <asp:Label runat="server" ID="Label4" Text='<%# String.Format("{0:0.00}", Eval("RateContracted")) %>' width="65px"></asp:Label>
                    </td>  
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:TextBox runat="server" ID="uoTextBoxRateConfirmedPerSeafarer" 
                            onkeypress="return validate(event);" CssClass="SmallText"
                            Text='<%# String.Format("{0:0.00}", Eval("RateConfirmed")) %>' Width="65px"></asp:TextBox>
                    </td> 
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("SeafarerIdInt")%>' Width="60px"></asp:Label>
                    </td>  
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLabelLast" Text='<%# Eval("LastName")%>' Width="150px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:LinkButton Width="150px" runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("SeafarerIdInt") + "&trID=" + Eval("TravelReqID") + "&st=" + Eval("CrewStatus") + "&recloc=" + Eval("RecordLocator") + "&manualReqID=0&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IdBigint") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("FirstName") %></asp:LinkButton>
                    </td>                       
                                                                                                                         
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCheckIn" Text='<%# String.Format("{0:dd-MMM-yyyy}",Eval("CheckIn"))%>'
                            Width="70px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCheckOut" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOut"))%>'
                            Width="70px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblNites" Text='<%# Eval("HotelNites")%>' Width="40px"></asp:Label>
                    </td>                           
                                                                         
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblRoom" Text='<%# Eval("RoomType")%>' Width="70px"></asp:Label>
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
                        <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator")%>' Width="60px"></asp:Label>
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
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblArrivalTime" Text='<%# Eval("ArvlTime")%>' Width="60px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="40px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblFN" Text='<%# Eval("FlightNo")%>' Width="50px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblVoucher" Text='<%# String.Format("{0:0.00}", Eval("Voucher")) %>'
                            Width="50px"></asp:Label>
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
    </div>    
    <%--=====================End: Hotel Manifest==========================--%>
    
    <%--=====================Start: Hotel Manifest Cancelled==========================--%>
    <div id="Div1" class="PageSubTitle" style="text-decoration: underline;">
        <asp:Label ID="Label2" runat="server" Text="Cancelled Hotel Manifest"></asp:Label>
    </div>
    
    <div id="DivCancelHeader" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
            <asp:ListView runat="server" ID="uoListViewCancelHeader" OnItemCommand="uoListViewHotelHeader_ItemCommand">
            <LayoutTemplate>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="listViewTable">
                    <tr>                                                                   
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
                                Text="First Name" Width="144px" />
                        </th>                        
                        
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblCheckInHeader" runat="server" CommandName="CheckIn"
                                Text="Check-In" Width="64px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblCheckOutHeader" runat="server" CommandName="CheckOut"
                                Text="Check-Out" Width="64px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblNitesHeader" runat="server" CommandName="HotelNites"
                                Text="Hotel Nites" Width="34px" />
                        </th>
                        
                      
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="RoomType"
                                Text="Room Type" Width="65px" />
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
                                Width="100px" />
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
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblATimeHeader" runat="server" CommandName="ArvlTime"
                                Text="Arvl Time" Width="53px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblCarrierHeader" runat="server" CommandName="Carrier"
                                Text="Carrier" Width="35px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblFlightNumHeader" runat="server" CommandName="FlightNo"
                                Text="Flight No." Width="44px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblVoucherHeader" runat="server" CommandName="Voucher"
                                Text="Voucher" Width="44px" />
                        </th>                     
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div id="DivCancelDetails" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
        onscroll="divScrollLCancel();">
        <asp:ListView runat="server" ID="uoListViewCancelDetails" >
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" id="uoTableHotelCancel">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                 <%# OverflowChangeRowColor()%>
                <tr>   
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("SeafarerIdInt")%>' Width="60px"></asp:Label>
                    </td>  
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLabelLast" Text='<%# Eval("LastName")%>' Width="150px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:LinkButton Width="150px" runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("SeafarerIdInt") + "&trID=" + Eval("TravelReqID") + "&st=" + Eval("CrewStatus") + "&recloc=" + Eval("RecordLocator") + "&manualReqID=0&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IdBigint") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("FirstName") %></asp:LinkButton>
                    </td>                       
                                                                                                                         
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCheckIn" Text='<%# String.Format("{0:dd-MMM-yyyy}",Eval("CheckIn"))%>'
                            Width="70px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCheckOut" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOut"))%>'
                            Width="70px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblNites" Text='<%# Eval("HotelNites")%>' Width="40px"></asp:Label>
                    </td>                           
                                                                         
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblRoom" Text='<%# Eval("RoomType")%>' Width="70px"></asp:Label>
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
                        <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator")%>' Width="60px"></asp:Label>
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
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblArrivalTime" Text='<%# Eval("ArvlTime")%>' Width="60px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="40px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblFN" Text='<%# Eval("FlightNo")%>' Width="50px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblVoucher" Text='<%# String.Format("{0:0.00}", Eval("Voucher")) %>'
                            Width="50px"></asp:Label>
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
    </div>  
    <%--=====================End: Hotel Manifest Cancelled==========================--%>
    
         <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
         <asp:HiddenField ID="uoHiddenFieldRole" runat="server" /> 
     
        <asp:HiddenField ID="uoHiddenFieldOtherFrom" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldOtherTo" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldIsEdit" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldCountAdd" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldCountDelete" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldOrder" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldPortID" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenfieldEndOfContract" runat="server" value="0"/>
        <asp:Button ID="btnAlertMe" runat="server" value="0" style="display:none" OnClientClick="checkContract()"/>
      
        
        
        
    <script type="text/javascript" language="javascript">
        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
                setupButtonSettings();
                HideGridview();
            }
        }

        $(document).ready(function() {
            SetTRResolution();
            setupParentPage();
            setupButtonSettings();
            HideGridview();
        });

        function setupParentPage() {
            window.parent.$("#ctl00_NaviPlaceHolder_uoHiddenFieldHotelConfirm").val('1');
        }

        function checkContract() {

            alert(document.getElementById('ctl00_ContentPlaceHolder1_uoHiddenfieldEndOfContract').value);

        }



        function HideGridview() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_uoLabelTitle').innerHTML == 'Hotel Request: Cancellation') {
                document.getElementById('ctl00_ContentPlaceHolder1_uoLabelSubTitle').style.display = 'None';
                document.getElementById('Av').style.display = 'None';
                document.getElementById('Bv').style.display = 'None';

                
                
                
            }
        
        
        }
        
        
        
        
        
        
        
        function SetTRResolution() {
            var ht = $(window).height() * .90;
            var ht2 = $(window).height() * .950;
            var wd = $(window).width() * 0.98;

            if (screen.height <= 600) {
                ht = ht * 0.20;
                ht2 = ht2 * 0.20;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.39;
                ht2 = ht2 * 0.39;
            }
            else {
                ht = ht * 0.50;
                ht2 = ht2 * 0.61;
            }

            $("#Av").width(wd);
            $("#Bv").height(ht);
            $("#Bv").width(wd);


            $("#DivCancelHeader").width(wd);
            $("#DivCancelDetails").height(ht);
            $("#DivCancelDetails").width(wd);
           
        }

        function ValidateDate(pDate) {
            try {
                var dt = Date.parse(pDate);
                return true;
            }
            catch (err) {
                return false;
            }
        }

        function validateEmail(field) {
            var regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
            return (regex.test(field)) ? true : false;
        }

        function validateMultipleEmailsSemiColonSeparated(email, seperator) {
            var value = email.value;
            if (value != '') {
                var result = value.split(seperator);
                for (var i = 0; i < result.length; i++) {
                    if (result[i] != '') {
                        if (!validateEmail(result[i])) {
                            email.focus();
                            alert('Please check, `' + result[i] + '` email address not valid!');
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;

        }
        function divScrollLCancel() {
            var Right = document.getElementById('DivCancelHeader');
            var Left = document.getElementById('DivCancelDetails');
            Right.scrollLeft = Left.scrollLeft;

        }
        function confimSave() {
            var sMsg = '';
            var iAdd = 0;
            var iDelete = 0;

            iAdd = $("#<%=uoHiddenFieldCountAdd.ClientID %>").val()
            iDelete = $("#<%=uoHiddenFieldCountDelete.ClientID %>").val()

            var sEmail = $("#<%=uoTextBoxEmailAdd.ClientID %>").val();
            var sCurrencyID = $("#<%=uoDropDownListCurrency.ClientID %>").val() ;
            var sRate = $("#<%=uoTextBoxRateConfirmed.ClientID %>").val();
            var sConfirmationNo = $("#<%=uoTextBoxConfirmation.ClientID %>").val();
            var sHotelName = $("#<%=uoTextBoxHotelname.ClientID %>").val();
            var sConfirmedBy = $("#<%=uoTextBoxConfirmedBy.ClientID %>").val();
            var sComment = $("#<%=uoTextBoxComment.ClientID %>").val();
            var sRole = $("#<%=uoHiddenFieldRole.ClientID %>").val();
            
            
            if ($.trim(sEmail) == "")
            {
                sMsg = 'Email recipient required!'
            }
            if (iAdd > 0) {
                if (sRole == 'Service Provider') {
                    if ($.trim(sCurrencyID) == "0") {
                        sMsg = sMsg + '\nCurrency required!'
                    }
                    if ($.trim(sRate) == "") {
                        sMsg = sMsg + '\nConfimed rate required!'
                    }
//                    if ($.trim(sConfirmationNo) == "") {
//                        sMsg = sMsg + '\nConfirmation No. required!'
//                    }
                    if ($.trim(sHotelName) == "") {
                        sMsg = sMsg + '\nHotel name required!'
                    }
                }
                else if (sRole == 'Hotel Specialist') {
                    if ($.trim(sCurrencyID) == "0") {
                        sMsg = sMsg + '\nCurrency required!'
                    }
                }
            }
            

            if ($.trim(sComment) == "") {
                sMsg = sMsg + '\nComment required!'
            }
            if ($.trim(sConfirmedBy) == "") {
                sMsg = sMsg + '\nConfirmed by required!'
            }
            
            if(sMsg != "")
            {
                alert(sMsg);
                return false;
            }
            
            if (iAdd > 0) {
                
                sMsg = 'There is/are ' + iAdd + ' record(s) to be confirmed. '
            }

            if (iDelete > 0) {
                sMsg = sMsg + '\nThere is/are ' + iDelete + ' record(s) to be cancelled. '
            }
            
            sMsg = sMsg + validateManifestAmount() + '\n\nDo you want to continue?';

            if (confirm(sMsg) == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function validate(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //if (((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) && !isNaN(keycode)) {
            if (keycode >= 48 && keycode <= 57 || keycode == 46) {
                return true;
            }
            else {
                return false;
            }
        }

        function setupButtonSettings() {                  
            $("#ctl00_ContentPlaceHolder1_uoButtonApplyTo").click(function() {

                var sRate = $("#ctl00_ContentPlaceHolder1_uoTextBoxRateConfirmed").val();
                var fRate = parseFloat(sRate);
                if (isNaN(fRate)) {
                    fRate = 0;
                }

                if (fRate > 0) {

                    getManifestAmount(fRate);
                }
                else {
                    var isContinue = confirm('Amount is 0, do you want to continue?');
                    if (isContinue) {
                        getManifestAmount(fRate);
                    }
                }
                return false;

            });           
        }

        function getManifestAmount(fAmount) {
            var sRoom = '';
            var fRoom = 0.0;


            sRoom = $("#ctl00_ContentPlaceHolder1_uoDropDownListRoom").val();
            fRoom = parseFloat(sRoom);
           
            $("#uoTableHotel tr").each(function(i, element) {
                //var $TDs = $(this).find('td');
                var txtRate = $('td:eq(1) input[name*="uoTextBoxRateConfirmedPerSeafarer"]', this);
                var lblRoom = $('td:eq(8) span[id*="uoLblRoom"]', this);

                var trimTDRoom = $.trim(lblRoom.html());
                var fTrimTDRoom = parseFloat(trimTDRoom);

                if (txtRate.val() != null) {
                   
                    if (fRoom == 0) {
                        txtRate.val(fAmount);
                    }
                    else if (fRoom == fTrimTDRoom) {
                        txtRate.val(fAmount);
                    }
                }
            });
        }
        function validateManifestAmount() {
            
            var sMSg = '\n\nThe following E1 ID has 0 amount.\n';
            var sTDMsg = '';
            $("#uoTableHotel tr").each(function(i, element) {
                var lblE1ID = $('td:eq(2) span[id*="uoLblSfID"]', this);

                var txtRate = $('td:eq(1) input[name*="uoTextBoxRateConfirmedPerSeafarer"]', this);
                var lblRoom = $('td:eq(8) span[id*="uoLblRoom"]', this);
                              
                if (txtRate.val() != null) {
                    if (txtRate.val() == 0) {
                        sTDMsg = sTDMsg + "\nE1 ID: " + $.trim(lblE1ID.html());
                    }
                }
            });
            
            if (sTDMsg == '') {
                return sTDMsg;
            }
            else {
                sMSg = sMSg + sTDMsg;
                return sMSg;
            }
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
