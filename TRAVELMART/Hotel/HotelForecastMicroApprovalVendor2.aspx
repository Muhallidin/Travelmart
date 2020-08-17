<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="True"
    CodeBehind="HotelForecastMicroApprovalVendor2.aspx.cs" Inherits="TRAVELMART.Hotel.HotelForecastMicroApprovalVendor2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">     
    <style type="text/css">
        .style2
        {
            width: 145px;
            vertical-align: middle;
        }
        .style3
        {
            width: 72px;
            vertical-align: middle;
        }
        .style5
        {
            width: 85px;
            vertical-align: middle;
        }
        .style6
        {
            width: 50px;
            vertical-align: middle;
        }
        .style7
        {
            width: 224px;
            vertical-align: middle;
        }
        .style8
        {
            width: 276px;
            vertical-align: middle;
        }
        .ManifestDate
        {
            white-space:nowrap;	
        }
    </style>
</asp:Content>
<asp:Content runat="server" ID="Conetent3" ContentPlaceHolderID="HeaderContent">
    
     <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
        <tr >
            <td align="left">
                Hotel Forecast Approval
            </td>
            <td align="right">
                <asp:Label ID="uoLabelHotelVendor" runat="server" Text="Hotel Vendor" CssClass="Title"></asp:Label>
               <%-- Region: &nbsp;&nbsp
                <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="200px" AutoPostBack="true"
                    OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp;Port: &nbsp;&nbsp;
                <asp:DropDownList ID="uoDropDownListPortPerRegion" runat="server" Width="200px" AutoPostBack="true"
                    AppendDataBoundItems="true" OnSelectedIndexChanged ="uoDropDownListPortPerRegion_SelectedIndexChanged">
                </asp:DropDownList>--%>
            </td>            
        </tr>
    </table>   
   
    <div id="PG" style="width: auto; height: auto; overflow: auto;">
        <table width="100%" class="LeftClass">
            <%--<tr>
                <td class="contentCaption">
                    <b>Single</b>
                </td>
                 <td class="style2">
                   Currency:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListCurrencySingle" runat="server" Width="300px"  AppendDataBoundItems="true">
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    Rate:
                </td>
                <td class="style7">
                    <asp:TextBox ID="uoTextBoxRateSingle" runat="server" Width="150px"></asp:TextBox>
                </td>
                 <td class="style8">
                    Is Tax Inclusive:
                </td>
                <td class="style6">
                    <asp:CheckBox ID="uoCheckBoxTaxInclusiveSingle" runat="server" />
                </td>
                 <td class="style5">
                   Room Rate Tax(%):
                </td>
                <td class="contentValue">
                    <asp:TextBox ID="uoTextBoxTaxSingle" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
              <tr>
                <td class="contentCaption">
                   <b>Double</b>
                </td>
                 <td class="style2">
                   Currency:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListCurrencyDouble" runat="server" Width="300px" AppendDataBoundItems = "true">
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    Rate:
                </td>
                <td class="style7">
                    <asp:TextBox ID="uoTextBoxRateDouble" runat="server" Width="150px"></asp:TextBox>
                </td>
                 <td class="style8">
                    Is Tax Inclusive:
                </td>
                <td class="style6">
                    <asp:CheckBox ID="uoCheckBoxTaxInclusiveDouble" runat="server" />
                </td>
                 <td class="style5">
                   Room Rate Tax(%):
                </td>
                <td class="contentValue">
                    <asp:TextBox ID="uoTextBoxTaxDouble" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td class="contentCaption">
                   
                </td>
                 <td class="style2">
                   No. of Days:
                </td>
                 <td class="contentValue" >
                   
               
                    <asp:DropDownList runat="server" ID ="uoDropDownListDays" OnSelectedIndexChanged="uoDropDownListDays_SelectedIndexChanged" 
                    AppendDataBoundItems="true" Width="200px" AutoPostBack="true" ></asp:DropDownList>
                    
                    <%--<asp:TextBox ID="uoTextBoxFrom" runat="server" onchange="return CheckFromDate(this);" Width="130px"></asp:TextBox>
                     <cc1:textboxwatermarkextender ID="uoTextBoxFrom_TextBoxWatermarkExtender" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxFrom" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:textboxwatermarkextender>
                    <cc1:calendarextender ID="uoTextBoxFrom_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxFrom"  Format="MM/dd/yyyy"  >
                    </cc1:calendarextender >
                    <cc1:maskededitextender ID="uoTextBoxFrom_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxFrom"
                        UserDateFormat="MonthDayYear">
                    </cc1:maskededitextender>
                    &nbsp
                    To: 
                     &nbsp;<asp:TextBox ID="uoTextBoxTo" runat="server" onchange="return CheckToDate(this);" Width="130px"></asp:TextBox><cc1:textboxwatermarkextender ID="Textboxwatermarkextender1" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxTo" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:textboxwatermarkextender>
                    <cc1:calendarextender ID="Calendarextender1" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxTo"  Format="MM/dd/yyyy">
                    </cc1:calendarextender >
                    <cc1:maskededitextender ID="Maskededitextender1" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxTo"
                        UserDateFormat="MonthDayYear">
                    </cc1:maskededitextender>--%>
               
                
                     <%--<asp:Button ID="uoButtonView" runat="server" CssClass="SmallButton" 
                             Text="View" onclick="uoButtonView_Click" Width="70px" />--%>
                   <%-- &nbsp;<asp:Button ID="uoBtnExportList" runat="server" CssClass="SmallButton" Text="Export"
                        OnClick="uoBtnExportList_Click" />--%>
                        &nbsp; 
                     <asp:Button ID="uoButtonSave" runat="server" CssClass="SmallButton" OnClientClick='return ValidateRows();'
                             Text="Submit"  Width="70px" onclick="uoButtonSave_Click" />
                </td>
                <td class="contentCaption">
                    Show All:
                </td>
                <td class="contentValue">
                    <asp:CheckBox runat="server" ID ="uoCheckBoxShowAll" Checked="true" Text="" 
                        AutoPostBack="true" oncheckedchanged="uoCheckBoxShowAll_CheckedChanged"/>
                </td>
                <td></td>
                <td></td>
                <td>
                    <asp:LinkButton runat="server" ID="uoLinkButtonViewContract">View Contract</asp:LinkButton>
                </td>
                
            </tr>
        </table>
        
        
        
        <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
           
                    
                
            <%--<asp:ListView runat="server" ID="uoListViewHeader" 
                ondatabound="uoListViewHeader_DataBound" >
                <LayoutTemplate>
                </LayoutTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <EmptyDataTemplate>--%>
                   <%-- <table class="listViewTable">--%>
                   
                 <asp:ListView runat="server" ID="uoListViewHeader"  >
                <LayoutTemplate>   
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%" >
                        <tr>
                            <%--<th style="text-align: center; white-space: normal;width:30px" rowspan="2" >
                                <asp:Label ID="uoLabelSelect" runat="server" Text="Select" Width="25px" /> <br />
                                <asp:CheckBox runat="server" ID = "uoCheckBoxSelectAll" Width="10px" OnClick="CheckUncheckAll(this);"/>                                            
                            </th>--%> 
                            <th style="text-align: center; white-space: normal; width:40px" rowspan="2">
                                <asp:Label runat="server" ID="uoLblTRHeader" Text="Date" Width="40px"></asp:Label>
                            </th>
                           <%-- <th style="text-align: center; white-space: normal;" colspan="2">
                                <asp:Label runat="server" ID="uoLblCheckInHeader" Text="Micro Confirmed Booking" Width="140px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;" colspan="2">
                                <asp:Label runat="server" ID="Label4" Text="Micro Overflow" Width="140px"></asp:Label>
                            </th>--%>
                            
                            <%--<th style="text-align: center; white-space: normal; width:80px" colspan="2" >
                                <asp:Label runat="server" ID="Label16" Text="Room Block Per Contract" Width="80px"></asp:Label>
                            </th>--%>
                            
                             <th style="text-align: center; white-space: normal; width:80px" colspan="2">
                                <asp:Label runat="server" ID="Label11" Text="Rooms Total Confirmed by Hotel" Width="80px"></asp:Label>
                            </th>
                            
                            
                            <th style="text-align: center; white-space: normal;  width:80px" colspan="2">
                                <asp:Label runat="server" ID="Label17" Text="Confirmed Rooms with Names in TM" Width="80px"></asp:Label>
                            </th>  
                            
                            
                            <th style="text-align: center; white-space: normal;  width:80px" colspan="2">
                                <asp:Label runat="server" ID="Label7" Text="Forecasted Rooms Needed" Width="80px"></asp:Label>
                            </th>
                                                                                                            
                            
                             <th style="text-align: center; white-space: normal; width:80px" colspan="2" >
                                <asp:Label runat="server" ID="Label1" Text="Addition" Width="80px"></asp:Label>
                            </th>
                            
                             <th style="text-align: center; white-space: normal; width:80px" colspan="2" >
                                <asp:Label runat="server" ID="Label33" Text="Room To Drop" Width="80px"></asp:Label>
                            </th>
                            
                            <th style="text-align: center; white-space: normal; width:70px" rowspan="2">
                                <asp:Label runat="server" ID="Label28" Text="Action" Width="70px"></asp:Label>
                            </th>
                            
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            
                             <%--<th style="text-align: center; white-space: normal; width:80px" colspan="2">
                                <asp:Label runat="server" ID="uoLabelCurrency" Text='Currency' Width="80px"></asp:Label>
                            </th>--%>
                            
                            
                            <th style="text-align: center; white-space: normal; width:35px" rowspan="2" >
                                <asp:Label runat="server" ID="Label23" Text="Tax(%)" Width="35px"></asp:Label>
                            </th>
                            
                            <th style="text-align: center; white-space: normal; width:35px" rowspan="2">
                                <asp:Label runat="server" ID="Label29" Text="Tax Inclusive" Width="35px"></asp:Label>
                            </th>
                            
                            
                            
                          <%--  <th style="text-align: center; white-space: normal;" rowspan="2">
                                <asp:Label runat="server" ID="Label123" Text="" Width="10px"></asp:Label>
                            </th>--%>
                        </tr>
                         <tr>
                           <%-- <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label9" Text="Double" Width="70px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label8" Text="Single" Width="70px"></asp:Label>
                            </th>
                            
                            <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label10" Text="Double" Width="70px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label11" Text="Single" Width="70px"></asp:Label>
                            </th>--%>
                            
                           <%-- <th style="text-align: center; white-space: normal; width:35px"  >
                                <asp:Label runat="server" ID="Label13" Text="Double" Width="35px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal; width:35px" >
                                <asp:Label runat="server" ID="Label14" Text="Single" Width="35px"></asp:Label>
                            </th>--%>
                            
                            <%--Rooms Total Confirmed by Hotel--%>
                             <th style="text-align: center; white-space: normal; width:35px" >
                                <asp:Label runat="server" ID="Label18" Text="Double" Width="35px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal; width:35px" >
                                <asp:Label runat="server" ID="Label19" Text="Single" Width="35px"></asp:Label>
                            </th>
                            
                             <%--Confirmed Rooms with Names in TM--%>
                            <th style="text-align: center; white-space: normal;  width:35px" >
                                <asp:Label runat="server" ID="Label3" Text="Double" Width="30px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;  width:35px" >
                                <asp:Label runat="server" ID="Label2" Text="Single" Width="35px"></asp:Label>
                            </th>
                            
                            
                            <%--Forecasted Rooms Needed--%>
                             <th style="text-align: center; white-space: normal;  width:35px" >
                                <asp:Label runat="server" ID="Label20" Text="Double" Width="35px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;  width:35px" >
                                <asp:Label runat="server" ID="Label21" Text="Single" Width="35px"></asp:Label>
                            </th>
                                                       
                            
                            <%--Addition--%>
                            <th style="text-align: center; white-space: normal; width:35px" >
                                <asp:Label runat="server" ID="Label26" Text="Double" Width="35px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal; width:35px" >
                                <asp:Label runat="server" ID="Label27" Text="Single" Width="35px"></asp:Label>
                            </th>
                            
                            <%--Room To Drop--%>
                            <th style="text-align: center; white-space: normal; width:35px" >
                                <asp:Label runat="server" ID="Label13" Text="Double" Width="35px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal; width:35px" >
                                <asp:Label runat="server" ID="Label14" Text="Single" Width="35px"></asp:Label>
                            </th>
                            
                             <%--Currency Room Rate--%>
                             <th style="text-align: center; white-space: normal; width:35px" >
                                <asp:Label runat="server" ID="Label16" Text="Double" Width="35px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal; width:35px" >
                                <asp:Label runat="server" ID="Label22" Text="Single" Width="35px"></asp:Label>
                            </th>                                                        
                            
                            <%--<th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label12" Text="" Width="25px"></asp:Label>
                            </th>--%>
                        </tr>
                    </table>
                <%--</EmptyDataTemplate>
            </asp:ListView>--%>
                
            </LayoutTemplate>
                <ItemTemplate>
                    <th style="text-align: center; white-space: normal; width:80px" colspan="2">
                        <asp:Label runat="server" ID="uoLabelCurrency" Text='<%# Eval("CurrencyName") %>' Width="80px"></asp:Label>
                    </th>
                </ItemTemplate>
                <EmptyDataTemplate></EmptyDataTemplate>
            </asp:ListView>
            
        </div>
        <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
            onscroll="divScrollL();">
            <asp:ListView runat="server" ID="uolistviewHotelInfo" DataSourceID="uoObjectDataSourceManifest"
                OnDataBound="uolistviewHotelInfo_DataBound" 
                onitemdatabound="uolistviewHotelInfo_ItemDataBound">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%" id="uoTableManifest">
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <%--<td class="centerAligned" style="white-space: normal; width:40px">
                            <asp:CheckBox runat="server" ID = "uoCheckBoxSelect"  Width="35px"/>
                        </td>--%>  
                        <td class="leftAligned" style="white-space: normal; width:45px">
                            <asp:Label runat="server" ID="uoLabelDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("colDate"))%>'
                                Width="50px" CssClass="ManifestDate"></asp:Label>
                        </td>
                        
                        <%--<td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblCouple" Text='<%# Eval("Confirmed_DBL")%>' Width="60px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label1" Text='<%# Eval("Confirmed_SGL")%>' Width="60px"></asp:Label>
                        </td>
                        
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label3" Text='<%# Eval("Overflow_DBL")%>' Width="60px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label5" Text='<%# Eval("Overflow_SGL")%>' Width="60px"></asp:Label>
                        </td>--%>
                        
                      <%--  <td class="rightAligned" style="white-space: normal; width:50px">
                            <asp:Label runat="server" ID="Label22" Text='<%# Eval("RoomBlock_DBL")%>' Width="40px"></asp:Label>
                        </td>
                        <td class="rightAligned" style="white-space: normal;  width:50px">
                            <asp:Label runat="server" ID="Label23" Text='<%# Eval("RoomBlock_SGL")%>' Width="40px"></asp:Label>
                        </td>--%>
                        
                        <%--Rooms Total Confirmed by Hotel--%>
                         <td class="rightAligned" style="white-space: normal; width:45px">
                            <asp:Label runat="server" ID="Label12" Text='<%# Eval("RoomBlock_DBL_Total")%>' Width="40px"></asp:Label>
                        </td>
                       
                        <td class="rightAligned" style="white-space: normal; width:45px">
                            <asp:Label runat="server" ID="Label15" Text='<%# Eval("RoomBlock_SGL_Total")%>' Width="40px"></asp:Label>
                        </td>
                        
                         <%--Confirmed Rooms with Names in TM--%>
                        <td class="rightAligned" style="white-space: normal;  width:45px">
                            <asp:Label runat="server" ID="Label24" Text='<%# Eval("TMBooked_DBL")%>' Width="45px"></asp:Label>
                        </td>
                        <td class="rightAligned" style="white-space: normal; width:45px">
                            <asp:Label runat="server" ID="Label25" Text='<%# Eval("TMBooked_SGL")%>' Width="45px"></asp:Label>
                        </td>
                        
                        <%--Forecasted Rooms Needed--%>
                         <td class="rightAligned" style="white-space: normal;  width:45px">
                            <%--<asp:Label runat="server" ID="Label4" Text='<%# Convert.ToString(Eval("Forecast_DBL_Old"))== "0"?"": Eval("Forecast_DBL_Old")%> '  Width="15px"  ToolTip="Old Value" ></asp:Label>
                             
                            <asp:Label runat="server" ID="Label6" Text='<%# Eval("Forecast_DBL")%>' Width="15px" ToolTip="Current Value"
                                ForeColor='<%# Convert.ToString(Eval("Forecast_DBL_Old"))== "0"?System.Drawing.Color.Black : System.Drawing.Color.Red %>'>
                            </asp:Label>--%>
                             <asp:Label runat="server" ID="Label6" Text='<%# Eval("Forecast_DBL")%>' Width="15px"
                                ForeColor='<%# Convert.ToString(Eval("Forecast_DBL_Old"))== "0"?System.Drawing.Color.Black : System.Drawing.Color.Red %>'>
                            </asp:Label>
                        </td>
                        <td class="rightAligned" style="white-space: normal;  width:45px">
                            <%--<asp:Label runat="server" ID="Label5" Text='<%# Convert.ToString(Eval("Forecast_SGL_Old"))== "0"?"": Eval("Forecast_SGL_Old")%> '  Width="15px"  ToolTip="Old Value" ></asp:Label>
                             
                            <asp:Label runat="server" ID="Label8" Text='<%# Eval("Forecast_SGL")%>' Width="15px" ToolTip="Current Value"
                                ForeColor='<%# Convert.ToString(Eval("Forecast_SGL_Old"))== "0"?System.Drawing.Color.Black : System.Drawing.Color.Red %>'>
                            </asp:Label>--%>
                            <asp:Label runat="server" ID="Label8" Text='<%# Eval("Forecast_SGL")%>' Width="15px"
                                ForeColor='<%# Convert.ToString(Eval("Forecast_SGL_Old"))== "0"?System.Drawing.Color.Black : System.Drawing.Color.Red %>'>
                            </asp:Label>
                        </td>
                        
                       
                        
                       <%--Addition--%>
                        <td class="rightAligned" style="white-space: nowrap; width:35px">
                            <asp:HiddenField ID="uoHiddenFieldToAddDBL" runat="server" Value='<%# Eval("ToBeAdded_DBL") %>' />
                            
                            <asp:TextBox runat="server" ID="uoTextBoxDBL" onkeypress='<%#  "return validate(event)" %>'
                            onchange ='<%#  "return validateNewValue(this, \""+  Eval("ToBeAdded_DBL") +"\")" %>'
                            CssClass = "ReadOnlySmallRight"
                            ReadOnly ="false"
                            Text='<%# Convert.ToBoolean(Eval("IsNeededHotelVisibleDBL")) == true? Eval("ToBeAdded_DBL"): "0" %>'  
                            Width="30px"  ></asp:TextBox>
                            
                            <asp:Label runat="server" ID="Label9" Text='<%# Convert.ToString(Eval("ToBeAdded_DBL_Suggested"))== "0"?"": Eval("ToBeAdded_DBL_Suggested")%> '  
                            Width="10px"  ToolTip="Suggested Value" ForeColor="Red"></asp:Label>
                             
                        </td>
                        <td class="rightAligned" style="white-space: nowrap; width:35px">
                          <asp:HiddenField ID="uoHiddenFieldToAddSGL" runat="server" Value='<%# Eval("ToBeAdded_SGL") %>' />
                            
                          <asp:TextBox runat="server" ID="uoTextBoxSGL" onkeypress='<%#  "return validate(event)" %>'
                            onchange ='<%#  "return validateNewValue(this, \""+  Eval("ToBeAdded_SGL") +"\")" %>'
                          CssClass = "ReadOnlySmallRight"
                          ReadOnly ="false"
                          Text='<%# Convert.ToBoolean(Eval("IsNeededHotelVisibleSGL")) == true? Eval("ToBeAdded_SGL"): "0" %>'  
                          Width="30px" ></asp:TextBox>
                          
                           <asp:Label runat="server" ID="Label10" Text='<%# Convert.ToString(Eval("ToBeAdded_SGL_Suggested"))== "0"?"": Eval("ToBeAdded_SGL_Suggested")%> '  
                            Width="10px"  ToolTip="Suggested Value" ForeColor="Red"></asp:Label>
                        </td>
                        
                         <%--Room To Drop--%>
                         <td class="rightAligned" style="white-space: nowrap; width:40px">
                            <asp:TextBox runat="server" ID="uoTextBoxRoomToDropDBL" onkeypress='<%#  "return validate(event)" %>'
                            CssClass = 'ReadOnlySmall'
                            ToolTip = '<%# Convert.ToString(Eval("RoomToDropColorDBL")) == "Red"? "More than 20%":""  %>'
                            Text='<%# Eval("RoomToDropDBL")%>'   
                            ReadOnly="true"                     
                            Width="35px" ></asp:TextBox>
                        </td>
                        <td class="rightAligned" style="white-space: nowrap; width:40px">
                            <asp:TextBox runat="server" ID="uoTextBoxRoomToDropSGL" onkeypress='<%#  "return validate(event)" %>'
                            CssClass = 'ReadOnlySmall'                    
                            ToolTip = '<%# Convert.ToString(Eval("RoomToDropColorSGL")) == "Red"? "More than 20%":""  %>'
                            Text='<%# Eval("RoomToDropSGL") %>'  
                            ReadOnly="true"
                            Width="35px" ></asp:TextBox>
                        </td>
                        
                         <td class="centerAligned" style="white-space: normal; width:80px">
                            <asp:Label runat="server" ID="uoLabelAction" Text='<%# Eval("MessageToVendor") %>' Visible='<%# Eval("IsRCCLApprovalVisible") %>'></asp:Label>
                            
                            <div style='<%# "display:" + (Eval("IsRCCLApprovalVisible").ToString() == "True"? "none":"display") %>'>
                                <asp:DropDownList ID="uoDropDownListAction" runat="server" Width="80px"  >
                                    <asp:ListItem Value="Action" Selected="True">--Select--</asp:ListItem>                                
                                    <asp:ListItem Value="Accept" >Accept</asp:ListItem>
                                    <asp:ListItem Value="Decline" >Decline</asp:ListItem>
                                    <asp:ListItem Value="Edit" >Edit</asp:ListItem>                                
                                </asp:DropDownList>
                            </div>
                        </td>    
                        
                        <%--Currency Room Rate--%>
                        <td class="rightAligned" style="white-space: nowrap; width:40px">
                            <asp:TextBox runat="server" ID="uoTextBoxRatePerDayMoneyDBL" onkeypress='<%#  "return validate(event)" %>'
                            CssClass = 'SmallTextRight'
                            Text='<%# Eval("RatePerDayMoneyDBL")%>'   
                            Width="35px" ></asp:TextBox>
                        </td>
                        
                        <td class="rightAligned" style="white-space: nowrap; width:40px">
                            <asp:TextBox runat="server" ID="uoTextBoxRatePerDayMoneySGL" onkeypress='<%#  "return validate(event)" %>'
                            CssClass = 'SmallTextRight'
                            Text='<%# Eval("RatePerDayMoneySGL")%>'   
                            Width="35px" ></asp:TextBox>
                        </td>
                        
                         <td class="rightAligned" style="white-space: nowrap; width:40px">
                            <asp:TextBox runat="server" ID="uoTextBoxRoomRateTaxPercentage" onkeypress='<%#  "return validate(event)" %>'
                            CssClass = 'SmallTextRight'
                            Text='<%# Eval("RoomRateTaxPercentage")%>'   
                            Width="35px" ></asp:TextBox>
                        </td>
                        
                         <td class="rightAligned" style="white-space: nowrap; width:40px">
                          <asp:CheckBox runat="server" ID="uoCheckBoxTaxInclusive" Checked='<%# Convert.ToBoolean(Eval("RoomRateIsTaxInclusive")) %>'/>
                        </td>
                        
                        <%--<td style="width:10px">
                             <asp:Label runat="server" ID="Label29" Text=""  Width="10px" ></asp:Label>
                        </td> --%>                                                       
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate> 
                    <table class="listViewTable">
                        <tr>
                            <td colspan="9" class="leftAligned">
                                No Record
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div align="left">
                <asp:DataPager ID="uolistviewHotelInfoPager" runat="server" PagedControlID="uolistviewHotelInfo"
                    PageSize="50">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
               
                <asp:ObjectDataSource ID="uoObjectDataSourceManifest" runat="server" MaximumRowsParameterName="MaxRow"
                    SelectCountMethod="GetForecastManifestCount" SelectMethod="GetForecastManifestList"
                    StartRowIndexParameterName="StartRow" TypeName="TRAVELMART.BLL.HotelForecastBLL" OldValuesParameterFormatString="oldcount_{0}"
                    EnablePaging="True" OnSelecting="uoObjectDataSourceManifest_Selecting">
                    <SelectParameters>
                        <asp:Parameter Name="sBranchName" Type="String" />
                        <asp:Parameter Name="sDateFrom" Type="String" />
                        <asp:Parameter Name="sDateTo" Type="String" />
                        <asp:Parameter Name="sVesselCode" Type="String" />
                        <asp:Parameter Name="sPortID" Type="String"  />
                        <asp:Parameter Name="sUser" Type="String" />
                        <asp:Parameter Name="sRole" Type="String" />
                        <asp:Parameter Name="bIsHotelVendorView" Type="Boolean" DefaultValue="True"/>
                        <asp:Parameter Name="LoadType" Type="String" />
                         <asp:Parameter Name="bShowAll" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldHotelID" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldContractID" runat="server" Value="0"/>
        
    <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldFrom" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldTo" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldNoOfDays" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldCurrency" runat="server" Value="0"/>
    
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetTMResolution();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTMResolution();
            }
        }

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
                ht = ht * 0.6;
                ht2 = ht2 * 0.75;
            }

            $("#Bv").height(ht);
            $("#PG").height(ht2);
            $("#Av").width(wd);
            $("#Bv").width(wd);
            $("#PG").width(wd);


            $("#uoTableManifest tr select").live('change', function(e) {
                //                alert($(this).closest('td').parent()[0].sectionRowIndex);
                var xTR = $(this).closest('td').parent()[0];

                //get the column index
                var xTDDBL = xTR.cells[7];
                var xTDSGL = xTR.cells[8];
                

                //-------column 7 elements-------
                //uoHiddenFieldToAddSGL 0
                //uoTextBoxSGL 1
                //Label10 2

                var xObj_Dbl = $(xTDDBL.children[1]);
                var xObj_Sgl = $(xTDSGL.children[1]);



                if ($(this).val() == 'Edit') {
                    xObj_Dbl.attr('class', 'SmallTextRight');
                    xObj_Sgl.attr('class', 'SmallTextRight');                    
                }
                else {
                    xObj_Dbl.attr('class', 'ReadOnlySmallRight');
                    xObj_Sgl.attr('class', 'ReadOnlySmallRight');                    
                }

            });


            $("#<%=uoLinkButtonViewContract.ClientID %>").click(function(ev) {
                var vendorID = $("#<%=uoHiddenFieldHotelID.ClientID %>").val();
                var contractID = $("#<%=uoHiddenFieldContractID.ClientID %>").val();
                OpenContract(vendorID, contractID);
            });

        }

        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;
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
        function validate(key) {
            //getting key code of pressed key

           //var a = $(this).val(String.fromCharCode(key.keyCode));
//            alert($(this).text);
           

            var keycode = (key.which) ? key.which : key.keyCode;

            if (keycode >= 48 && keycode <= 57) {

                return true;
            }
//            else if (keycode == 45) {

//                return true;
//            }
            else {
                return false;
            }

            ////getting key code of pressed key
            //var keycode = (key.which) ? key.which : key.keyCode;
            //if ((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) {
            //    return false;
            //}
            //else {
            //    return true;
            //}
        }
//        function validateNewValueTest(obj) {
//            alert(obj.value);
//        }
        function validateNewValue(key, old) {
            var xNew = key.value;

            var iNew = parseInt(xNew);
            var iOld = parseInt(old);


            if (iNew > iOld) {
                alert("Value should not exceed to " + old + " room(s).");
                key.value = old;
                return false;
            }
            else {
                return true;
            }
        }

        function ValidateRows() {

            var bIsCheck = false;
            $("#uoTableManifest tr").each(function(i, element) {
                var xCol = element.cells[11]; //column index
                var xDDL = $(xCol.children[0]);

                if (xDDL != null) {
                    if (xDDL.val() != 'Action') {
                        bIsCheck = true;
                    }
                }
            });
            if (bIsCheck == false) {
                alert("No action selected!");
            }
            return bIsCheck;
        }

        function OpenContract(vendorID, ContractID) {
            var URLString = "../ContractManagement/HotelContractView.aspx?vId=" + vendorID + "&cId=" + ContractID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
              

/*
        function CheckUncheckAll(obj) {
            $('input[name*="uoCheckBoxSelect"]').attr('checked', obj.checked);
        }

        function ValidateIfCheck(IsApproveButton) {
           
            var bIsCheck = false;
            $("#uoTableManifest tr").each(function(i, element) {
                var $TDs = $(this).find('td');
                var chk = $('td:eq(0) input[name*="uoCheckBoxSelect"]', this);

                if (chk != null) {
                    if (chk.attr('checked')) {
                        bIsCheck = true;
                    }
                }
            });
            if (bIsCheck == false) {
                alert("No selected record!");
            }
            return bIsCheck;
        }*/
    </script>      
</asp:Content>
