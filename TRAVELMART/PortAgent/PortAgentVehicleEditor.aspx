<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="PortAgentVehicleEditor.aspx.cs" 
    Inherits="TRAVELMART.PortAgentVehicleEditor" %>
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
                    <asp:Label ID="uoLabelTitle" runat="server" Text="Confirm Vehicle Request" CssClass="Title"></asp:Label>                    
                </td>
            </tr>
        </table>
    </div>
    <hr/> 
    <div>
        <table width="100%" >
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
                    Pickup Date:
                </td>
                <td style="white-space:nowrap" class="style2">
                    <asp:TextBox runat="server" ID="uoTextBoxPickupDate" Width="241px" 
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
                    Pickup Time:
                </td>
                <td style="white-space:nowrap" class="style2">
                    <asp:TextBox runat="server" ID="uoTextBoxPickupTime" Width="80px" ></asp:TextBox>
                     <cc1:textboxwatermarkextender ID="uoTextBoxPickupTime_TextBoxWatermarkExtender"
                            runat="server" Enabled="True" TargetControlID="uoTextBoxPickupTime" 
                                WatermarkCssClass="fieldWatermark" WatermarkText="HH:mm">
                            </cc1:textboxwatermarkextender>
                            <%--<cc1:maskededitextender ID="uoMaskedEditExtenderCheckInTime" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxPickupTime"
                                UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                            </cc1:maskededitextender>--%>
                    &nbsp;
                    <asp:Label ID="Label4"  CssClass="RedNotification" runat="server" Text="24 hr format (e.g. 22:30)"> </asp:Label> 
                </td>  
                <td class="style7" > Email CC:</td>              
                <td style="white-space:nowrap">
                     <asp:TextBox runat="server" ID="uoTextBoxCopy" Width="400px"
                     onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                     ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                     TextMode="MultiLine" Rows="2"></asp:TextBox>
                    
                </td>
            </tr>
             <tr>
                <td class="style6" >
                    Driver:
                </td>
                <td class="style2" >
                       <asp:TextBox runat="server" ID="uoTextBoxDriver" Width="239px"></asp:TextBox>                     
                </td>
                <td  >
                   Plate No:
                </td>
                <td >
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" id="uoTextBoxPlateNo" Width="150px"></asp:TextBox>                                
                            </td>
                            <td>
                               Vehicle Type:
                            </td>
                            <td>
                                 <asp:DropDownList ID="uoDropDownListVehicleType" runat="server" Width="170px" 
                                    AppendDataBoundItems="true" >
                                </asp:DropDownList>   
                                             
                            </td>
                        </tr>
                    </table>                                        
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
                 <td class="style7" >  <asp:Label runat ="server" ID="uoLabelTranspoDetails" Text="Transportation Details:"></asp:Label></td>              
                <td>
                     <asp:TextBox runat="server" ID="uoTextBoxTranspoDetails" Width="399px"></asp:TextBox>                    
                </td>
            </tr>
            <tr>
                <td>Confirmation No.</td>
                <td class="style2">
                    <asp:TextBox ID="uoTextBoxConfirmation"  Width="235px" runat="server" ></asp:TextBox>      
                    &nbsp;
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ValidationGroup="Email" 
                              ErrorMessage="Confirmation No. required."  ControlToValidate="uoTextBoxConfirmation">* </asp:RequiredFieldValidator> 
                </td>
                <td class="style6" >
                     <asp:Label runat ="server" ID="uoLabelSource" Text="Request Source:"></asp:Label>
                </td>
                <td  style="white-space:nowrap">
                    <%--<asp:TextBox ID="uoTextBoxVehicleVendorName"   Width="400px" runat="server" ></asp:TextBox>     
                      &nbsp;
                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ValidationGroup="Email" 
                              ErrorMessage="Vehicle Vendor Name required."  ControlToValidate="uoTextBoxVehicleVendorName">* </asp:RequiredFieldValidator>--%>     
                                 <asp:DropDownList ID="uoDropDownListRequestSource" runat="server" Width="400px" 
                                    AppendDataBoundItems="true" >
                                </asp:DropDownList>   
                                             
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
                    Comfirmed Rate Inclusive of Tax:
                </td>
               <td>
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
                                 <asp:DropDownList ID="uoDropDownListRoute" runat="server" Width="170px" 
                                    AppendDataBoundItems="true" >
                                </asp:DropDownList>   
                                             
                            </td>
                        </tr>
                    </table> 
                </td> 
            </tr>
            
             
             <tr>
                <td class="style6" >
                    Comment:
                </td>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="uoTextBoxComment" Width="534px" 
                        TextMode="MultiLine" Rows="2" ></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td>
                    Confirmed By:
                </td>
                <td colspan="3" style="white-space:nowrap">
                    <asp:TextBox ID="uoTextBoxConfirmedBy"  Width="531px" runat="server"></asp:TextBox>
                    &nbsp;
                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ValidationGroup="Email" 
                              ErrorMessage="Confirmed by required."  ControlToValidate="uoTextBoxConfirmedBy">* </asp:RequiredFieldValidator>      
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
    <%--=====================Start: Vehicle Manifest==========================--%>
    <div id="Div2" class="PageSubTitle" style="text-decoration: underline;">
        <asp:Label ID="uoLabelSubTitle" runat="server" Text="Vehicle Manifest"></asp:Label>
    </div>
    
        <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
            <asp:ListView runat="server" ID="uoListViewVehicleHeader" OnItemCommand="uoListViewVehicleHeader_ItemCommand">
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
                            <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="VehicleType"
                                Text="Vehicle Type" Width="65px" />
                        </th>
                        
                        
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="CrewStatus"
                                Text="Status" Width="43px" />
                        </th>
                        
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton8" runat="server" CommandName="RouteFrom"
                                Text="Route From" Width="65px" />
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
                            <asp:LinkButton ID="uoLinkButtonRouteFromCity" runat="server" CommandName="RouteFromVarchar"
                                Text="From City" Width="50px" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLinkButtonRouteToCity" runat="server" CommandName="RouteToVarchar"
                                Text="To City" Width="50px" />
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
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="SeqNo"
                                Text="Seq No" Width="45px" />
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
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
        onscroll="divScrollL();">
        <asp:ListView runat="server" ID="uoListviewVehicleInfo" >
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" id="uoTableVehicle">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                 <%# OverflowChangeRowColor()%>
                <tr>   
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:HiddenField runat="server" ID="uoHiddenFieldListRecLocID" Value='<%# Eval("IdBigint") %>'/>
                        <asp:HiddenField runat="server" ID="uoHiddenFieldListTRID" Value='<%# Eval("TravelReqID") %>'/>
                        <asp:HiddenField runat="server" ID="uoHiddenFieldTransID" Value='<%# Eval("TransVehicleID") %>'/>
                        <asp:HiddenField runat="server" ID="uoHiddenFieldContractedRate" Value=''/>
                        <asp:Label runat="server" ID="uoLabelContractedRate" Text='<%# String.Format("{0:0.00}", Eval("RateContracted")) %>' width="65px"></asp:Label>
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
                        <asp:Label runat="server" ID="uoLblNites" Text='<%# Eval("VehicleTypeName")%>' Width="70px"></asp:Label>
                    </td>                           
                    
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblStatus" Text='<%# Eval("CrewStatus")%>' Width="50px"></asp:Label>
                    </td>                                                 
                    
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:HiddenField runat="server" ID="uoHiddenFieldRouteFromID" Value='<%# Eval("RouteFromID") %>'/>
                        <asp:Label runat="server" ID="uoLabelFrom" Text='<%# Eval("RouteFrom")%>' Width="70px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:HiddenField runat="server" ID="uoHiddenFieldRouteToID" Value='<%# Eval("RouteToID") %>'/>
                        <asp:Label runat="server" ID="uoLabelTo" Text='<%# Eval("RouteTo")%>' Width="70px"></asp:Label>
                    </td>
                   
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label9" Text='<%# Eval("RouteFromDisplay")%>' Width="200px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label10" Text='<%# Eval("RouteToDisplay")%>' Width="200px"></asp:Label>
                    </td> 
                    
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLabelRouteFromCity" Text='<%# Eval("CityFrom")%>' Width="55px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLabelRouteToCity" Text='<%# Eval("CityTo")%>' Width="55px"></asp:Label>
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
                        <asp:Label runat="server" ID="uoLabelSeqNo" Text='<%# Eval("SeqNo")%>' Width="50px"></asp:Label>
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
                        <asp:Label runat="server" ID="uoLblPNum" Text='<%# Eval("PassportNo")%>' Width="65px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblIssuedBy" Text='<%# Eval("PassportIssued")%>'
                            Width="65px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblExp" Text='<%# Eval("PassportExp")%>' Width="65px"></asp:Label>
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
    <%--=====================End: Vehicle Manifest==========================--%>
    
    <%--=====================Start: Vehicle Manifest Cancelled==========================--%>
    <div id="Div1" class="PageSubTitle" style="text-decoration: underline;">
        <asp:Label ID="Label2" runat="server" Text="Cancelled Vehicle Manifest"></asp:Label>
    </div>
    
    <div id="DivCancelHeader" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
            <asp:ListView runat="server" ID="uoListViewCancelHeader" OnItemCommand="uoListViewVehicleHeader_ItemCommand">
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
                            <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="VehicleType"
                                Text="Vehicle Type" Width="65px" />
                        </th>
                        
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton8" runat="server" CommandName="RouteFrom"
                                Text="Route From" Width="65px" />
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
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div id="DivCancelDetails" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
        onscroll="divScrollLCancel();">
        <asp:ListView runat="server" ID="uoListViewCancelDetails" >
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" id="uoTableVehicleCancel">
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
                        <asp:Label runat="server" ID="uoLblNites" Text='<%# Eval("VehicleTypeName")%>' Width="70px"></asp:Label>
                    </td>                           
                                                                         
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uolabelFrom" Text='<%# Eval("RouteFrom")%>' Width="70px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uolabelTo" Text='<%# Eval("RouteTo")%>' Width="70px"></asp:Label>
                    </td>
                   
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label9" Text='<%# Eval("RouteFromDisplay")%>' Width="200px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label10" Text='<%# Eval("RouteToDisplay")%>' Width="200px"></asp:Label>
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
                        <asp:Label runat="server" ID="Label5" Text='<%# String.Format("{0:0.00}", Eval("RateConfirmed")) %>' Width="65px"></asp:Label>
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
    <%--=====================End: Vehicle Manifest Cancelled==========================--%>
    
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldRole" runat="server"  />
        
        <asp:HiddenField ID="uoHiddenFieldOtherFrom" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldOtherTo" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldIsEdit" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldCountAdd" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldCountDelete" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldOrder" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldPortID" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldPortAgentID" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldContractID" runat="server" Value="0" />
              
    <script type="text/javascript" language="javascript">
        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
                setupButtonSettings();
                HideGridview()
            }
        }

        $(document).ready(function() {
            SetTRResolution();
            setupParentPage();
            setupButtonSettings();

            HideGridview()
        });


        function HideGridview() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_uoLabelTitle').innerHTML == 'Vehicle Request: Cancellation') {
                document.getElementById('ctl00_ContentPlaceHolder1_uoLabelSubTitle').style.display = 'None';
                document.getElementById('Av').style.display = 'None';
                document.getElementById('Bv').style.display = 'None';




            }


        }

    

        function setupParentPage() {
            window.parent.$("#ctl00_NaviPlaceHolder_uoHiddenFieldVehicleConfirm").val('1');
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


            $("#ctl00_ContentPlaceHolder1_uoDropDownListVehicleType").change(function(ev) {

            var iPortAgentID = $("#ctl00_ContentPlaceHolder1_uoHiddenFieldPortAgentID").val();
            var iVehicleTypeID = $(this).val();
                
                GetContractAmt(iPortAgentID, iVehicleTypeID);
            });
        }

        function getManifestAmount(fAmount) {
            var sApply = '';
            var sApplyText = '';
            var sFrom = '';
            var sTo = '';
            
            sApply = $("#ctl00_ContentPlaceHolder1_uoDropDownListRoute").val();

            if (sApply != '0') {
                sApplyText = $("#ctl00_ContentPlaceHolder1_uoDropDownListRoute option:selected").text();
                var split = sApplyText.split('-');

                sFrom = $.trim(split[0]);
                sTo = $.trim(split[1]);
            }


            $("#uoTableVehicle tr").each(function(i, element) {
                //var $TDs = $(this).find('td');
                var txtRate = $('td:eq(1) input[name*="uoTextBoxRateConfirmedPerSeafarer"]', this);
                var lblFrom = $('td:eq(7) span[id*="uoLabelFrom"]', this);
                var lblTo = $('td:eq(8) span[id*="uoLabelTo"]', this);

                var trimTDFrom = $.trim(lblFrom.html());
                var trimTDTo = $.trim(lblTo.html());


                if (txtRate.val() != null) {
                    if (sApply == '0') {
                        txtRate.val(fAmount);
                    }
                    else if ((trimTDFrom == sFrom) && (trimTDTo == sTo)) 
                    {                    
                        txtRate.val(fAmount);
                    }
                }
            });
        }
        function validateManifestAmount() {
            var sMSg = '\n\nThe following E1 ID has 0 amount.\n';            
            var iCount = 0;
            
            var sTDMsg = '';
            $("#uoTableVehicle tr").each(function(i, element) {

                iCount = iCount + 1;
                if (iCount <= 20) {
                    
                    var lblE1ID = $('td:eq(2) span[id*="uoLblSfID"]', this);                    

                    var txtRate = $('td:eq(1) input[name*="uoTextBoxRateConfirmedPerSeafarer"]', this);
                    var lblFrom = $('td:eq(7) span[id*="uoLabelFrom"]', this);
                    var lblTo = $('td:eq(8) span[id*="uoLabelTo"]', this);

                    var trimTDFrom = $.trim(lblFrom.html());
                    var trimTDTo = $.trim(lblTo.html());


                    if (txtRate.val() != null) {
                        if (txtRate.val() == 0) {
                            sTDMsg = sTDMsg + "\nE1 ID: " + $.trim(lblE1ID.html());
                        }
                    }

                    if (iCount == 20) {
                        sMSg = '\n\nThe following are the top 10 E1 ID with 0 amount.\n';
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
        function GetContractAmt(iPortAgentID, iVehicleTYpeID) {
            if (iVehicleTYpeID > 0) {
                var sPortAgentID = '0';
                var sVehicleTyepID = '0';

                sPortAgentID = iPortAgentID.toString();
                sVehicleTyepID = iVehicleTYpeID.toString();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/PageMethods.aspx/GetPortAgentVehicleContractAmt",
                    data: "{'sPortAgentID': '" + sPortAgentID + "', 'sVehicleTypeID': '" + sVehicleTyepID + "'}",
                    dataType: "json",
                    success: function(data) {

                        if (data.d.length > 0) {
                            $("#ctl00_ContentPlaceHolder1_uoHiddenFieldContractID").val(data.d[0].ContractID);

                            $("#uoTableVehicle tr").each(function(i, element) {
                                var hdnContractRate = $('td:eq(0) input[name*="uoHiddenFieldContractedRate"]', this);
                                var txtConrtactRate = $('td:eq(0) span[id*="uoLabelContractedRate"]', this);
                                var txtConfirmRate = $('td:eq(1) input[name*="uoTextBoxRateConfirmedPerSeafarer"]', this);
                                var lblFrom = $('td:eq(7) span[id*="uoLabelFrom"]', this);
                                var lblTo = $('td:eq(8) span[id*="uoLabelTo"]', this);
                                var lblFromCity = $('td:eq(11) span[id*="uoLabelRouteFromCity"]', this);
                                var lblToCity = $('td:eq(12) span[id*="uoLabelRouteToCity"]', this);

                                var trimTDFrom = $.trim(lblFrom.html());
                                var trimTDTo = $.trim(lblTo.html());

                                var trimTDFromCity = $.trim(lblFromCity.html());
                                var trimTDToCity = $.trim(lblToCity.html());

                                var isWithEqual = false;
                                for (var i = 0; i < data.d.length; i++) {
                                    if (trimTDFrom == data.d[i].RouteFrom &&
                                    trimTDTo == data.d[i].RouteTo &&
                                    trimTDFromCity == data.d[i].RouteFromCity &&
                                    trimTDToCity == data.d[i].RouteToCity) {

                                        txtConrtactRate.text(data.d[i].RateAmount);
                                        hdnContractRate.val(data.d[i].RateAmount);

                                        txtConfirmRate.val(data.d[i].RateAmount);
                                        isWithEqual = true;
                                        break;
                                    }
                                }
                                if (isWithEqual == false) {
                                    txtConrtactRate.text('0.00');
                                    txtConfirmRate.val('0.00');
                                    hdnContractRate.val('0.00');
                                }

                            });
                        }
                        else {
                            $("#ctl00_ContentPlaceHolder1_uoHiddenFieldContractID").val('0');
                            
                            $("#uoTableVehicle tr").each(function(i, element) {
                                var hdnContractRate = $('td:eq(0) input[name*="uoHiddenFieldContractedRate"]', this);
                                var txtConrtactRate = $('td:eq(0) span[id*="uoLabelContractedRate"]', this);
                                var txtConfirmRate = $('td:eq(1) input[name*="uoTextBoxRateConfirmedPerSeafarer"]', this);
                               
                                txtConrtactRate.text('0.00');
                                txtConfirmRate.val('0.00');
                                hdnContractRate.val('0.00');

                            });
                        }
                    }
                        ,
                    error: function(objXMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            }
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
