<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="True"
    CodeBehind="HotelForecastMicroApproval2.aspx.cs" Inherits="TRAVELMART.Hotel.HotelForecastMicroApproval2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">     
</asp:Content>
<asp:Content runat="server" ID="Conetent3" ContentPlaceHolderID="HeaderContent">
    <style type="text/css" >
       .rateDivCssHide 
       {
       	    display:none;
       }
       .rateDivCssShow  
       {
       	   /*
       	   position:absolute;
       	   z-index: 1000;
       	   background-color:White;
       	   text-align:left;
       	   display:block;  
       	   
       	        	   
       	   */
       	    z-index: 1000;
            position: absolute;
            top: 250px;
            right: 300px;
            
            border: 1px dashed #990000;
            background-color: White;
            text-align: left;
       }
       .rateTableCss   
       {
       	   border: 1px solid #d4d4d4;
       	   text-align:left;
       }
       table.rateTableCss td
        {
	        text-align:left;
        }
       
        .toApproveDivCssShow  
       {
       	    z-index: 1000;
            position: absolute;
            top: 250px;
            right: 180px;
            
            border: 1px dashed #990000;
            background-color: White;
            text-align: left;
       }
     </style>
     
     
     <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="PageTitle">
            <td align="left">
                Hotel Forecast Approval
            </td>
            <td align="right">
                Region: &nbsp;&nbsp
                <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="200px" AutoPostBack="true"
                    OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp;Port: &nbsp;&nbsp;
                <asp:DropDownList ID="uoDropDownListPortPerRegion" runat="server" Width="200px" AutoPostBack="true"
                    AppendDataBoundItems="true" OnSelectedIndexChanged ="uoDropDownListPortPerRegion_SelectedIndexChanged">
                </asp:DropDownList>
            </td>            
        </tr>
    </table>   
   
    <div id="PG" style="width: auto; height: auto; overflow: auto;">
        <table width="100%" class="LeftClass">
             <tr>
                <td class="contentCaption">
                    Hotel Branch:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListHotel" runat="server" Width="300px" AppendDataBoundItems="True">
                        <asp:ListItem Value="0">--SELECT HOTEL--</asp:ListItem>
                    </asp:DropDownList>
                    <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListBranch" runat="server"
                        TargetControlID="uoDropDownListHotel" PromptText="Type to search" PromptPosition="Top"
                        IsSorted="true" PromptCssClass="dropdownSearch" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoDropDownListHotel"
                        Enabled="False" ErrorMessage="Required Hotel" InitialValue="0">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID ="uoCheckBoxShowAll" Checked="true" Text="Show All" TextAlign="Right"/>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="contentCaption">
                    Date Range:
                </td>
                <td class="contentValue">
                    <asp:TextBox ID="uoTextBoxFrom" runat="server" onchange="return CheckFromDate(this);" Width="130px"></asp:TextBox>
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
                    </cc1:maskededitextender>
                </td>
                <td>
                     <asp:Button ID="uoButtonView" runat="server" CssClass="SmallButton" 
                             Text="View" onclick="uoButtonView_Click" Width="70px" />
                   <%-- &nbsp;<asp:Button ID="uoBtnExportList" runat="server" CssClass="SmallButton" Text="Export"
                        OnClick="uoBtnExportList_Click" />--%>
                        &nbsp; 
                     <asp:Button ID="uoButtonSave" runat="server" CssClass="SmallButton" OnClientClick='return ValidateIfCheck();'
                             Text="Save"  Width="70px" onclick="uoButtonSave_Click" />
                    
                     <asp:Button ID="uoButtonApprove" runat="server" CssClass="SmallButton" OnClientClick='return ValidateIfCheckApprove();'
                             Text="Approve"  Width="70px" onclick="uoButtonApprove_Click"  />
                </td>
                <td class="rightAligned">
                    <asp:LinkButton runat="server" ID="uoLinkButtonViewContract">View Contract</asp:LinkButton>
                </td>
            </tr>
        </table>
        
        
        
        <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
            <asp:ListView runat="server" ID="ListView1">
                <LayoutTemplate>
                </LayoutTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <EmptyDataTemplate>
                   <%-- <table class="listViewTable">--%>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%" >
                        <tr>
                            <th style="text-align: center; white-space: normal;width:35px" rowspan="2" >
                                <asp:Label ID="uoLabelSelect" runat="server" Text="Select" Width="25px" /> <br />
                                <asp:CheckBox runat="server" ID = "uoCheckBoxSelectAll" Width="10px" OnClick="CheckUncheckAll(this);"/>                                            
                            </th> 
                            <th style="text-align: center; white-space: normal; width:60px" rowspan="2">
                                <asp:Label runat="server" ID="uoLblTRHeader" Text="Date" Width="50px"></asp:Label>
                            </th>
                           <%-- <th style="text-align: center; white-space: normal;" colspan="2">
                                <asp:Label runat="server" ID="uoLblCheckInHeader" Text="Micro Confirmed Booking" Width="140px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;" colspan="2">
                                <asp:Label runat="server" ID="Label4" Text="Micro Overflow" Width="140px"></asp:Label>
                            </th>--%>
                            
                            <th style="text-align: center; white-space: normal; width:70px" colspan="2" >
                                <asp:Label runat="server" ID="Label16" Text="Room Block Per Contract" Width="70px"></asp:Label>
                            </th>
                            
                             <th style="text-align: center; white-space: normal; width:75px" colspan="2">
                                <asp:Label runat="server" ID="Label11" Text="Rooms Total Confirmed by Hotel" Width="75px"></asp:Label>
                            </th>
                            
                            
                            <th style="text-align: center; white-space: normal;  width:76px" colspan="2">
                                <asp:Label runat="server" ID="Label17" Text="Confirmed Rooms with Names in TM" Width="76px"></asp:Label>
                            </th>
                            
                            <th style="text-align: center; white-space: normal;  width:75px" colspan="2">
                                <asp:Label runat="server" ID="Label7" Text="Forecasted Rooms Needed" Width="75px"></asp:Label>
                            </th>
                                                   
                            <th style="text-align: center; white-space: normal;  width:80px"  colspan="2" >
                                <asp:Label runat="server" ID="Label1" Text="Forecast Adj" Width="80px"></asp:Label>
                            </th>
                            
                            <th style="text-align: center; white-space: normal; width:85px" colspan="2" >
                                <asp:Label runat="server" ID="Label10" Text="Needed Hotel" Width="85px"></asp:Label>
                            </th>
                            
                            <th style="text-align: center; white-space: normal; width:85px" colspan="2" >
                                <asp:Label runat="server" ID="Label33" Text="Room To Drop" Width="85px"></asp:Label>
                            </th>
                            
                            <th style="text-align: center; white-space: normal;  width:140px"  rowspan="2">
                                <asp:Label runat="server" ID="Label9" Text="Remarks" Width="140px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;  width:90px" colspan="2" >
                                <asp:Label runat="server" ID="Label30" Text="Still Needed" Width="90px"></asp:Label>
                            </th>
                            
                            <th style="text-align: center; white-space: normal;  width:45px" rowspan="2" >
                                <asp:Label runat="server" ID="Label39" Text="Approve" Width="45px"></asp:Label>
                            </th>
                            
                            <%-- <th style="text-align: center; white-space: normal; width:220px" colspan="2" >
                                <asp:Label runat="server" ID="Label1" Text="Addition/Cancelation" Width="100px"></asp:Label>
                            </th>--%>
                            
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
                            
                            <%--Room Block Per Contract--%>
                            <th style="text-align: center; white-space: normal; width:35px"  >
                                <asp:Label runat="server" ID="Label13" Text="Double" Width="35px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal; width:35px" >
                                <asp:Label runat="server" ID="Label14" Text="Single" Width="35px"></asp:Label>
                            </th>
                            
                            <%--Rooms Total Confirmed by Hotel--%>
                             <th style="text-align: center; white-space: normal; width:35px" >
                                <asp:Label runat="server" ID="Label18" Text="Double" Width="35px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal; width:35px" >
                                <asp:Label runat="server" ID="Label19" Text="Single" Width="35px"></asp:Label>
                            </th>
                            
                            <%--Forecasted from Micro--%>
                             <th style="text-align: center; white-space: normal;  width:33px" >
                                <asp:Label runat="server" ID="Label20" Text="Double" Width="33px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;  width:33px" >
                                <asp:Label runat="server" ID="Label21" Text="Single" Width="33px"></asp:Label>
                            </th>
                            
                           <%-- Confirmed Rooms with Names in TM--%>
                            <th style="text-align: center; white-space: normal;  width:33px" >
                                <asp:Label runat="server" ID="Label3" Text="Double" Width="33px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;  width:33px" >
                                <asp:Label runat="server" ID="Label2" Text="Single" Width="33px"></asp:Label>
                            </th>
                            
                            <%--Forecast Adj--%>
                            <th style="text-align: center; white-space: normal; width:42px" >
                                <asp:Label runat="server" ID="Label26" Text="Double" Width="42px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal; width:42px" >
                                <asp:Label runat="server" ID="Label27" Text="Single" Width="42px"></asp:Label>
                            </th>
                            
                           <%-- Needed Hotel--%>
                            <th style="text-align: center; white-space: normal; width:42px" >
                                <asp:Label runat="server" ID="Label28" Text="Double" Width="42px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal; width:42px" >
                                <asp:Label runat="server" ID="Label29" Text="Single" Width="42px"></asp:Label>
                            </th>
                            
                             <%-- Room To Drop--%>
                            <th style="text-align: center; white-space: normal; width:42px" >
                                <asp:Label runat="server" ID="Label34" Text="Double" Width="42px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal; width:42px" >
                                <asp:Label runat="server" ID="Label35" Text="Single" Width="42px"></asp:Label>
                            </th>
                            
                            <%--Still Needed--%>
                             <th style="text-align: center; white-space: normal; width:42px" >
                                <asp:Label runat="server" ID="Label31" Text="Double" Width="42px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal; width:42px" >
                                <asp:Label runat="server" ID="Label32" Text="Single" Width="42px"></asp:Label>
                            </th>
                            
                            
                            <%--<th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label12" Text="" Width="25px"></asp:Label>
                            </th>--%>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
        <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
            onscroll="divScrollL();">
            <asp:ListView runat="server" ID="uolistviewHotelInfo" DataSourceID="uoObjectDataSourceManifest"
                OnDataBound="uolistviewHotelInfo_DataBound">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%" id="uoTableManifest">
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <%--0 Select--%>
                        <td class="centerAligned" style="white-space: normal; width:40px">
                            <asp:CheckBox runat="server" ID = "uoCheckBoxSelect"  Width="40px"/>
                        </td>  
                        
                        <%--1 Date--%>
                        <td class="leftAligned" style="white-space: normal; width:65px">
                            <asp:Label runat="server" ID="uoLabelDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("colDate"))%>'
                                Width="65px"></asp:Label>
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
                        
                        <%--2,3 Room Block Per Contract--%>
                        <td class="rightAligned" style="white-space: normal; width:40px">
                            <asp:Label runat="server" ID="Label22" Text='<%# Eval("RoomBlock_DBL")%>' Width="40px"></asp:Label>
                        </td>
                        <td class="rightAligned" style="white-space: normal;  width:40px">
                            <asp:Label runat="server" ID="Label23" Text='<%# Eval("RoomBlock_SGL")%>' Width="40px"></asp:Label>
                        </td>
                        
                        <%--4,5 Rooms Total Confirmed by Hotel--%>
                         <td class="rightAligned" style="white-space: normal; width:40px">
                            <asp:Label runat="server" ID="Label12" Text='<%# Eval("RoomBlock_DBL_Total")%>' Width="40px"></asp:Label>
                        </td>
                       
                        <td class="rightAligned" style="white-space: normal; width:40px">
                            <asp:Label runat="server" ID="Label15" Text='<%# Eval("RoomBlock_SGL_Total")%>' Width="40px"></asp:Label>
                        </td>
                        
                        
                         <%--6,7 Confirmed Rooms with Names in TM--%>
                         <td class="rightAligned" style="white-space: normal;  width:40px">
                            <asp:Label runat="server" ID="Label24" Text='<%# Eval("TMBooked_DBL")%>' Width="40px"></asp:Label>
                        </td>
                        <td class="rightAligned" style="white-space: normal; width:40px">
                            <asp:Label runat="server" ID="Label25" Text='<%# Eval("TMBooked_SGL")%>' Width="40px"></asp:Label>
                        </td>
                        
                        
                        <%--8,9 Forecasted from Micro--%>
                         <td class="rightAligned" style="white-space: normal;  width:40px">
                            <%--<asp:Label runat="server" ID="Label4" Text='<%# Convert.ToString(Eval("Forecast_DBL_Old"))== "0"?"": Eval("Forecast_DBL_Old")%> '  
                            Width="15px"  ToolTip="Old Value" ></asp:Label>--%>
                             
                            <asp:Label runat="server" ID="Label6" Text='<%# Eval("Forecast_DBL")%>' Width="15px"
                                ForeColor='<%# Convert.ToString(Eval("Forecast_DBL_Old"))== "0"?System.Drawing.Color.Black : System.Drawing.Color.Red %>'>
                            </asp:Label>
                        </td>
                        <td class="rightAligned" style="white-space: normal;  width:40px">
                            <%--<asp:Label runat="server" ID="Label5" Text='<%# Convert.ToString(Eval("Forecast_SGL_Old"))== "0"?"": Eval("Forecast_SGL_Old")%> '  
                            Width="15px"  ToolTip="Old Value" ></asp:Label>--%>
                             
                            <asp:Label runat="server" ID="Label8" Text='<%# Eval("Forecast_SGL")%>' Width="15px"
                                ForeColor='<%# Convert.ToString(Eval("Forecast_SGL_Old"))== "0"?System.Drawing.Color.Black : System.Drawing.Color.Red %>'>
                            </asp:Label>
                        </td>
                        
                                               
                        <%--10,11 Forecast Adj--%>
                        <td class="rightAligned" style="white-space: normal;  width:40px">
                            <asp:TextBox runat="server" ID="uoTextBoxDBLAdj" onkeypress="return validate(event);" 
                            CssClass='<%# (Convert.ToBoolean(Eval("IsEnable"))== true? "SmallTextRight": "ReadOnlySmallRight") %>' 
                            Text='<%# Eval("Forecast_DBL_Adj") %>'
                            Width="40px" ReadOnly='<%# !Convert.ToBoolean(Eval("IsEnable")) %>' ></asp:TextBox>
                            
                        </td>
                        <td class="rightAligned" style="white-space: normal;  width:40px">
                            <asp:TextBox runat="server" ID="uoTextBoxSGLAdj" onkeypress="return validate(event);" 
                            CssClass='<%# (Convert.ToBoolean(Eval("IsEnable"))== true? "SmallTextRight": "ReadOnlySmallRight") %>' 
                            Text='<%# Eval("Forecast_SGL_Adj") %>'
                            Width="40px" ReadOnly='<%# !Convert.ToBoolean(Eval("IsEnable")) %>' ></asp:TextBox>
                        </td>
                        
                        <%--12 Needed Hotel--%>
                        <td class="rightAligned" style="white-space: nowrap; width:40px">
                            <asp:HiddenField ID="uoHiddenFieldToAddDBL" runat="server" Value='<%# Eval("ToBeAdded_DBL") %>' />
                            
                            <asp:TextBox runat="server" ID="uoTextBoxToAddDBL" onkeypress='<%#  "return validate(event)" %>'
                            style="cursor:pointer"
                            onchange ='<%#  "return validateNewValue(this, \""+  Eval("ToBeAdded_DBL") +"\")" %>'
                            CssClass = "ReadOnlySmallRight"
                            ReadOnly ="true"
                            Text='<%# Convert.ToBoolean(Eval("IsNeededHotelVisibleDBL")) == true? Eval("ToBeAdded_DBL"): "0" %>'                           
                            Width="35px" ></asp:TextBox>
                            
                                 <div id="ucDivRateDouble"  style="display:none" >
                                     <div class="rateDivCssShow">
                                        <table width="250px" class="rateTableCss"  align="left">
                                           <tr>
                                                <th colspan="2">Rate Details</th>
                                           </tr>
                                           <tr>
                                                <%--13,14--%>
                                                <td width="60px">
                                                    Currency:
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="Label38" Text='<%#  Eval("CurrencyName")%>' Width="42px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                 <%--15,16--%>
                                                <td width="60px">
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="Label4" Text='<%# String.Format("{0:0.00}", Eval("RatePerDayMoneyDBL"))%>' Width="42px"></asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                 <%--17,18--%>
                                                <td width="100px">
                                                    Tax:
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="Label5" Text='<%# String.Format("{0:0.00}", Eval("RoomRateTaxPercentage"))%>' Width="42px"></asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                 <%--19,20--%>
                                                <td width="100px">
                                                    Tax Inclusive:
                                                </td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="uoCheckBoxRate" Enabled="false"
                                                    Checked='<%# Convert.ToBoolean(Eval("RoomRateIsTaxInclusive")) %>'/>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                           
                            
                           <%-- <asp:Label runat="server" ID="Label9" Text='<%# Convert.ToString(Eval("ToBeAdded_DBL_Suggested"))== "0"?"": Eval("ToBeAdded_DBL_Suggested")%> '  
                            Width="10px"  ToolTip="Suggested Value" ForeColor="Red"></asp:Label>--%>
                             
                        </td>
                         <%--21--%>
                        <td class="rightAligned" style="white-space: nowrap; width:40px">
                          <asp:HiddenField ID="uoHiddenFieldToAddSGL" runat="server" Value='<%# Eval("ToBeAdded_SGL") %>' />
                            
                          <asp:TextBox runat="server" ID="uoTextBoxToAddSGL" onkeypress='<%#  "return validate(event)" %>'
                            style="cursor:pointer"
                            onchange ='<%#  "return validateNewValue(this, \""+  Eval("ToBeAdded_SGL") +"\")" %>'
                          CssClass = "ReadOnlySmallRight"
                          ReadOnly ="true"
                          Text='<%# Convert.ToBoolean(Eval("IsNeededHotelVisibleSGL")) == true? Eval("ToBeAdded_SGL"): "0" %>'   
                          Width="35px" ></asp:TextBox>
                          
                           <div id="ucDivRateSingle"  style="display:none" >
                                     <div class="rateDivCssShow">
                                        <table width="250px" class="rateTableCss"  align="left">
                                           <tr>
                                                <th colspan="2">Rate Details</th>
                                           </tr>
                                           <tr>
                                                 <%--22, 23--%>
                                                <td width="60px">
                                                    Currency:
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="Label40" Text='<%#  Eval("CurrencyName")%>' Width="42px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                 <%--24, 25--%>
                                                <td width="60px">
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="Label41" Text='<%# String.Format("{0:0.00}", Eval("RatePerDayMoneySGL"))%>' Width="42px"></asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                 <%--26, 27--%>
                                                <td width="100px">
                                                    Tax:
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="Label42" Text='<%# String.Format("{0:0.00}", Eval("RoomRateTaxPercentage"))%>' Width="42px"></asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                 <%--28, 29--%>
                                                <td width="100px">
                                                    Tax Inclusive:
                                                </td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="CheckBox1" Enabled="false"
                                                    Checked='<%# Convert.ToBoolean(Eval("RoomRateIsTaxInclusive")) %>'/>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                          
                           <%--<asp:Label runat="server" ID="Label10" Text='<%# Convert.ToString(Eval("ToBeAdded_SGL_Suggested"))== "0"?"": Eval("ToBeAdded_SGL_Suggested")%> '  
                            Width="10px"  ToolTip="Suggested Value" ForeColor="Red"></asp:Label>--%>
                        </td>
                        
                         <%--30, 31 Room To Drop--%>
                        <td class="rightAligned" style="white-space: nowrap; width:40px">
                            <asp:TextBox runat="server" ID="uoTextBoxRoomToDropDBL" onkeypress='<%#  "return validateNoMinus(event)" %>'
                            CssClass = '<%# Convert.ToString(Eval("RoomToDropColorDBL")) == "Red"? "SmallTextRightRed":"SmallTextRight"  %>'
                            ToolTip = '<%# Convert.ToString(Eval("RoomToDropColorDBL")) == "Red"? "More than 20%":""  %>'
                            Text='<%# Eval("RoomToDropDBL")%>'                           
                            Width="35px" ></asp:TextBox>
                            
                            <asp:Label runat="server" ID="Label36" Text='<%# Convert.ToBoolean(Eval("IsRoomToDropVisibleToVendorBDL"))== true?"*": "" %> '  
                            Width="10px"  ToolTip="Visible to Vendor"  ></asp:Label>
                             
                        </td>
                        <td class="rightAligned" style="white-space: nowrap; width:40px">
                            <asp:TextBox runat="server" ID="uoTextBoxRoomToDropSGL" onkeypress='<%#  "return validateNoMinus(event)" %>'
                            CssClass = '<%# Convert.ToString(Eval("RoomToDropColorSGL")) == "Red"? "SmallTextRightRed":"SmallTextRight"  %>'                    
                            ToolTip = '<%# Convert.ToString(Eval("RoomToDropColorSGL")) == "Red"? "More than 20%":""  %>'
                            Text='<%# Eval("RoomToDropSGL") %>'                           
                            Width="35px" ></asp:TextBox>
                            
                            <asp:Label runat="server" ID="Label37" Text='<%# Convert.ToBoolean(Eval("IsRoomToDropVisibleToVendorSGL"))== true?"*": "" %> '  
                            Width="10px"  ToolTip="Visible to Vendor" ForeColor="Red"></asp:Label>
                        </td>
                        
                        <%--32 Remarks--%>
                        <td class="leftAligned" style="white-space: normal;  width:150px">
                             <asp:TextBox runat="server" ID="uoTextBoxRemarks" 
                            CssClass='<%# (Convert.ToBoolean(Eval("IsEnable"))== true? "SmallText": "ReadOnlySmall") %>' 
                            Text='<%# Eval("Remarks") %>'
                            Width="150px" ReadOnly='<%# !Convert.ToBoolean(Eval("IsEnable")) %>' ></asp:TextBox>
                        </td>      
                           
                        <%--33, 34 Still Needed--%>
                        <td class="rightAligned" style="white-space: normal;  width:40px">
                            <asp:HiddenField ID="uoHiddenFieldAction" runat="server" Value='<%# Eval("ActionDone") %>' />
                            <asp:HyperLink runat="server" ID="ucHyperLinkActionDBL" Text='<%# Eval("ToBeAdded_DBL") %>' 
                             ForeColor="Red" CssClass="linkCursor" 
                             ToolTip='<%# Eval("ActionDone") %>'
                            Visible='<%# Eval("IsLinkToRequestVisibleDBL") %>'
                            NavigateUrl='<%# "#ucDivHotelToRequest"  %>'
                            OnClick='<%# "GetRoomToRequest(2, "+ Eval("ToBeAdded_DBL") + "," + Eval("ToBeAdded_SGL") +", \"" +  
                            String.Format("{0:dd-MMM-yyyy}", Eval("colDate")) + "\","+   Eval("colBranchIDInt") +");" %>' 
                            >
                            </asp:HyperLink>                            
                        </td>
                        
                        <td class="rightAligned" style="white-space: normal; width:40px">
                            <asp:HyperLink runat="server" ID="ucHyperLinkActionSGL" Text='<%# Eval("ToBeAdded_SGL") %>' 
                            ToolTip='<%# Eval("ActionDone") %>' ForeColor="Red" CssClass="linkCursor"
                            Visible='<%# Eval("IsLinkToRequestVisibleSGL") %>'
                            NavigateUrl="#ucDivHotelToRequest"
                            OnClick='<%# "GetRoomToRequest(1, "+ Eval("ToBeAdded_DBL") + "," + Eval("ToBeAdded_SGL") +",  \"" +  
                            String.Format("{0:dd-MMM-yyyy}", Eval("colDate")) + "\","+  Eval("colBranchIDInt") +");" %>' 
                            >
                             </asp:HyperLink>
                        </td>                                                                               
                        
                       <%--35 Need Approval--%>
                         <td class="centerAligned" style="white-space: normal; width:40px">
                            <asp:CheckBox runat="server" ID = "uoCheckBoxApprove"  Width="40px" Visible='<%# Convert.ToBoolean(Eval("IsRCCLApprovalVisible")) %>'/>
                                <div id="ucDivToApprove"  style="display:none" >
                                     <div class="toApproveDivCssShow">
                                         <table width="350px" class="rateTableCss"  align="left">
                                                <tr>
                                                    <th colspan="3" >
                                                        To Approve:
                                                    </th>
                                                </tr>
                                               <tr>
                                                    <td>
                                                        Currency:
                                                    </td>
                                                    <td colspan="2" style="white-space:nowrap"><asp:Label runat="server" ID="Label49" Text='<%#  Eval("CurrencyName")%>' Width="42px"></asp:Label> </td>
                                               </tr>
                                               <tr>
                                                    <td>Tax: </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label50" Text='<%# String.Format("{0:0.00}", Eval("RoomRateTaxPercentage"))%>' Width="42px"></asp:Label>
                                                    </td>
                                                    <td>
                                                         <asp:CheckBox runat="server" ID="CheckBox3" Enabled="false" Text="Tax Inclusive"
                                                        Checked='<%# Convert.ToBoolean(Eval("RoomRateIsTaxInclusive")) %>'/>
                                                    </td>
                                               </tr>
                                               <tr>
                                                    <td></td >                                                   
                                                    <td><b>Rate</b></td>
                                                    <td><b>Room</b></td>
                                                </tr>
                                                <tr>
                                                    <td><b>Single</b></td>                                              
                                                    <td width="100px">
                                                        <asp:Label runat="server" ID="Label48" Text='<%# String.Format("{0:0.00}", Eval("RatePerDayMoneySGL"))%>' Width="42px"></asp:Label>
                                                    </td>
                                                    <td width="100px">
                                                        <asp:Label runat="server" ID="uoLabelRoomApprovedSGL" Text='<%#  Eval("ApprovedSGL")%>' Width="42px"></asp:Label>
                                                    </td>                                                                                                        
                                                </tr>
                                                <tr>
                                                     <td><b>Double</b></td>                                                       
                                                    <td width="100px">
                                                        <asp:Label runat="server" ID="Label44" Text='<%# String.Format("{0:0.00}", Eval("RatePerDayMoneyDBL"))%>' Width="42px"></asp:Label>
                                                    </td>
                                                    <td width="100px">
                                                        <asp:Label runat="server" ID="uoLabelRoomApprovedDBL" Text='<%#  Eval("ApprovedDBL")%>' Width="42px"></asp:Label>
                                                    </td>                                                                                                        
                                                </tr>       
                                        </table>
                                    </div>
                                </div>
                        </td> 
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
                        <asp:Parameter Name="bIsHotelVendorView" Type="Boolean" DefaultValue="False"/>
                        <asp:Parameter Name="LoadType" Type="String" />
                        <asp:Parameter Name="bShowAll" Type="Boolean" />                        
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </div>
    
    <div style="display:none">
        <div id="ucDivHotelToRequest">
            <table cellpadding="0" width="500px" style="vertical-align:top" cellspacing="0">
                <tr class="PageTitle">
                    <td colspan="2">
                        Request Room to Hotel
                    </td>
                    <td class="RightClass">
                        <asp:Label runat="server" Text="Date Here" ID="uoLabelDateofRequest"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width:20px">
                        <asp:CheckBox runat="server" ID="uoCheckBoxDBL"/>
                    </td>
                    <td class="LeftClass"> 
                    <asp:Label runat="server" ID ="uoLabelDBL" Text = "0"></asp:Label>
                    Double Room:</td>
                    <td class="LeftClass">
                        <asp:DropDownList ID="uoDropDownListHotelToRequestDBL" runat="server" Width="255px" CssClass="SmallText">
                        </asp:DropDownList>
                    </td>
                   <%-- <td>
                        <asp:Button ID="uoButtonRequestDBL" runat="server" Text="Request" CssClass="SmallButton" Width="50px" />
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="3">
                       
                    </td>
                </tr>
                 <tr>
                    <td style="width:20px">
                        <asp:CheckBox runat="server" ID="uoCheckBoxSGL"/>
                    </td>
                    <td class="LeftClass"> 
                        <asp:Label runat="server" ID ="uoLabelSGL" Text = "0"></asp:Label>
                        Single Room:</td>
                    <td class="LeftClass">
                        <asp:DropDownList ID="uoDropDownListHotelToRequestSGL" runat="server" Width="255px" CssClass="SmallText">
                        </asp:DropDownList>
                    </td>
                   <%-- <td>
                        <asp:Button ID="Button1" runat="server" Text="Request" CssClass="SmallButton" Width="50px" />
                    </td>--%>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td class="LeftClass" >
                        <asp:Button ID="uoButtonRequest" runat="server" Text="Request" CssClass="SmallButton" Width="60px" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan= "3" class="LeftClass">
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldPopupRequestToOther" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldDateSelected" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldSave" runat="server" Value="0"/>
    
    <asp:HiddenField ID="uoHiddenFieldHotelSGL" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldHotelDBL" runat="server" Value="0"/>
           
    <asp:HiddenField ID="uoHiddenFieldRoomCountDBL" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldRoomCountSGL" runat="server" />
    
    <asp:HiddenField ID="uoHiddenFieldContractID" runat="server" Value="0"/> 
    <asp:HiddenField ID="uoHiddenFieldCurrencyName" runat="server" Value=""/>
       
        
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetTMResolution();
            ShowPopup();
            ControlSettings();
            showRate();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTMResolution();
                ShowPopup();
                ControlSettings();
                showRate();
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

        }

        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;
        }

        function CheckFromDate(d) {
            var txtFromDate = $("#<%=uoTextBoxFrom.ClientID %>");
            var txtToDate = $("#<%=uoTextBoxTo.ClientID %>");
            var dt = d.value;

            if (ValidateDate(dt)) {
                if (txtToDate.val() != '') {

                    var fromDate = Date.parse(dt);
                    var toDate = Date.parse(txtToDate.val());

                    if (fromDate > toDate) {
                        alert("Invalid date range!");
                        txtToDate.val('');
                        return false;
                    }
                }
            }
            else {
                alert("Invalid date!");
                txtFromDate.val('');
                return false;
            }
            $("#<%=uoHiddenFieldDate.ClientID %>").val(txtFromDate.val());

            return true;
        }
        function CheckToDate(d) {
            var txtFromDate = $("#<%=uoTextBoxFrom.ClientID %>");
            var txtToDate = $("#<%=uoTextBoxTo.ClientID %>");
            var dt = d.value;

            if (ValidateDate(dt)) {
                if (txtFromDate.val() != '') {

                    var toDate = Date.parse(dt);
                    var fromDate = Date.parse(txtFromDate.val());
                    if (fromDate > toDate) {
                        alert("Invalid date range!");
                        txtFromDate.val('');
                        return false;
                    }
                }
            }
            else {
                alert("Invalid date!");
                txtToDate.val('');
                return false;
            }
            return true;
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
            var keycode = (key.which) ? key.which : key.keyCode;

            if (keycode >= 48 && keycode <= 57) {
                return true;
            }
            else if (keycode == 45) {
                return true;
            }
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
        function validateNoMinus(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;

            if (keycode >= 48 && keycode <= 57) {
                return true;
            }
            else {
                return false;
            }         
        }

        function CheckUncheckAll(obj) {
            $('input[name*="uoCheckBoxSelect"]').attr('checked', obj.checked);
        }

        function ValidateIfCheck() {

            var iBranchID = $("#<%=uoDropDownListHotel.ClientID %>").val();
            var sDate = '';
            
            var bIsCheck = false;
            $("#uoTableManifest tr").each(function(i, element) {
                var $TDs = $(this).find('td');
                var chk = $('td:eq(0) input[name*="uoCheckBoxSelect"]', this);
                var lblDate = $('td:eq(1) span[id*="uoLabelDate"]', this);
                var sDateRow = $.trim(lblDate.html());

                if (chk != null) {
                    if (chk.attr('checked')) {
                        bIsCheck = true;
                        sDate = sDate + ',' + sDateRow;
                    }
                }
            });

       
            if (bIsCheck == false) {
                alert("No selected record!");
                return false;
            }

            
            
            ///////////////////
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../PageMethods.aspx/ValidateHotelForecast",
                data: "{'iBranchID': '" + iBranchID + "', 'sDate': '" + sDate + "'}",
                dataType: "json",
                success: function(data) {
                    //if there is data
                    if (data.d.length > 0) {

                        bIsCheck = false;
                        var sMsg = "Following Dates were already Edited/Declined";
                        sMsg = sMsg + "\nDo you want to continue?\n";



                        for (var i = 0; i < data.d.length; i++) {
                            sMsg = sMsg + "\n" + data.d[i].colDate;
                        }

                        var isContinue = confirm(sMsg);

                        if (isContinue) {
                            $("#<%=uoHiddenFieldSave.ClientID %>").val('1');
                            $("#aspnetForm").submit();
                        }
                    }
                },
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
            ///////////////////

           
            return bIsCheck;
        }

        function ValidateIfCheckApprove() {

            var iBranchID = $("#<%=uoDropDownListHotel.ClientID %>").val();
            var bIsCheck = false;

            $("#uoTableManifest tr").each(function(i, element) {
                var $TDs = $(this).find('td');
                var chkApprove = $('td:eq(35) input[name*="uoCheckBoxApprove"]', this);

                if (chkApprove != null) {

                    if (chkApprove.attr('checked')) {
                        bIsCheck = true;
                    }
                }
            });

            if (bIsCheck == false) {
                alert("No selected record to approve!");
                return false;
            }
            
            return bIsCheck;
        }
        
        function ShowPopup() {
            $(".linkCursor").fancybox(
            {
                'width': '68%',
                'height': '100%',
                'autoScale': true,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'titleShow': false,                
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupRequestToOther.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });
        }

//        function BindRoomToRequest() {
//                               
//            var sDBL = '1212';
//            $("#<%=uoLabelDBL.ClientID %>").html('000001');
//        }
        function GetRoomToRequest(roomType, roomCountBDL, roomCountSGL, dDate, branchID) {
            BindHotelToRequest(branchID);
           
            var ddlSGL = $("#<%=uoDropDownListHotelToRequestSGL.ClientID %>")
            var ddlDBL = $("#<%=uoDropDownListHotelToRequestDBL.ClientID %>")
            
            ddlSGL.val("0");
            ddlDBL.val("0");
        
            
            $("#<%=uoLabelDBL.ClientID %>").html(roomCountBDL);
            $("#<%=uoLabelSGL.ClientID %>").html(roomCountSGL);

            $("#<%=uoHiddenFieldRoomCountDBL.ClientID %>").val(roomCountBDL);
            $("#<%=uoHiddenFieldRoomCountSGL.ClientID %>").val(roomCountSGL);

            ddlSGL.removeAttr("disabled");
            ddlDBL.removeAttr("disabled");

            if (roomType == '1') {
                $("#<%=uoCheckBoxSGL.ClientID %>").attr('checked', true);
                $("#<%=uoCheckBoxDBL.ClientID %>").attr('checked', false);

               ddlDBL.attr("disabled", true);
            }
            else if (roomType == '2') {
                $("#<%=uoCheckBoxSGL.ClientID %>").attr('checked', false);
                $("#<%=uoCheckBoxDBL.ClientID %>").attr('checked', true);

                ddlSGL.attr("disabled", true);
            }
            $("#<%=uoHiddenFieldDateSelected.ClientID %>").val(dDate);
            $("#<%=uoLabelDateofRequest.ClientID %>").html(dDate);
        }        
        
        function ControlSettings() {
            var chkSGL = $("#<%=uoCheckBoxSGL.ClientID %>");
            var chkDBL = $("#<%=uoCheckBoxDBL.ClientID %>");
            
            var ddlSGL = $("#<%=uoDropDownListHotelToRequestSGL.ClientID %>");
            var ddlDBL = $("#<%=uoDropDownListHotelToRequestDBL.ClientID %>");

            var ddlHotel = $("#<%=uoDropDownListHotel.ClientID %>");

            chkSGL.change(function(ev) {              
                if (this.checked == true) {
                    ddlSGL.removeAttr("disabled");
                }
                else {
                    ddlSGL.attr("disabled", true);
                }
            });

            chkDBL.change(function(ev) {               
                if (this.checked == true) {
                    ddlDBL.removeAttr("disabled");
                }
                else {
                    ddlDBL.attr("disabled", true);
                }
            });

            $("#<%=uoButtonRequest.ClientID %>").click(function(ev) {

                var hiddenPopupRequest = $("#<%=uoHiddenFieldPopupRequestToOther.ClientID %>");
                if (!chkSGL.is(':checked') && !chkDBL.is(':checked')) {
                    alert('No Active Request!');
                    return false;
                }

                var sHotel = ddlHotel.children(':selected').text();
                var iHotelID = ddlHotel.val();
                var sDate = $("#<%=uoLabelDateofRequest.ClientID %>").html();


                if (chkSGL.is(':checked')) {
                    if (ddlSGL.val() == "0") {
                        alert('No Hotel selected for SGL room!');
                        return false;
                    }
                    if (iHotelID == ddlSGL.val()) {
                        var xConfirm = confirm('Hotel selected for SGL room is the same with the origin ' + sHotel + '!\nDo you want to continue?')
                        if (xConfirm) {
                            hiddenPopupRequest.val("0");
                        }
                        else {
                            hiddenPopupRequest.val("1");
                            return false;                        
                        }
                    }
                   

                    ///////////////////SGL ROOM
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "../PageMethods.aspx/ValidateHotelForecast",
                        data: "{'iBranchID': '" + ddlSGL.val() + "', 'sDate': '" + sDate + "'}",
                        dataType: "json",
                        success: function(data) {
                            //if there is data
                            if (data.d.length > 0) {

                                bIsCheck = false;
                                var sMsg = "(SGL) Following Dates were already Edited/Declined in " + ddlSGL.children(':selected').text();
                                sMsg = sMsg + "\nDo you want to continue?\n";



                                for (var i = 0; i < data.d.length; i++) {
                                    sMsg = sMsg + "\n" + data.d[i].colDate;
                                }

                                var isContinue = confirm(sMsg);

                                if (!isContinue) {
                                    hiddenPopupRequest.val("0");
                                }
                            }
                        },
                        error: function(objXMLHttpRequest, textStatus, errorThrown) {
                            alert(errorThrown);
                        }
                    });
                    ///////////////////
                }


                //////////////////////////////////////////////////////////////////////////////////////////////////////
                if (chkDBL.is(':checked')) {
                    if (ddlDBL.val() == "0") {
                        alert('No Hotel selected for DBL room!');
                        return false;
                    }
                    if (iHotelID == ddlDBL.val()) {

                        var xConfirm = confirm('Hotel selected for DBL room is the same with the origin ' + sHotel + '!\nDo you want to continue?')
                        if (xConfirm) {
                            hiddenPopupRequest.val("0");
                        }
                        else {
                            hiddenPopupRequest.val("1");
                            return false;
                        }
                    }
                    hiddenPopupRequest.val("1");

                    ///////////////////SGL ROOM
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "../PageMethods.aspx/ValidateHotelForecast",
                        data: "{'iBranchID': '" + ddlDBL.val() + "', 'sDate': '" + sDate + "'}",
                        dataType: "json",
                        success: function(data) {
                            //if there is data
                            if (data.d.length > 0) {

                                bIsCheck = false;
                                var sMsg = "(DBL) Following Dates were already Edited/Declined in " + ddlDBL.children(':selected').text();
                                sMsg = sMsg + "\nDo you want to continue?\n";



                                for (var i = 0; i < data.d.length; i++) {
                                    sMsg = sMsg + "\n" + data.d[i].colDate;
                                }

                                var isContinue = confirm(sMsg);

                                if (!isContinue) {
                                    hiddenPopupRequest.val("0");
                                }
                            }
                        },
                        error: function(objXMLHttpRequest, textStatus, errorThrown) {
                            alert(errorThrown);
                        }
                    });
                    ///////////////////
                }

                if (hiddenPopupRequest.val() == '1') {

                    $("#<%=uoHiddenFieldHotelSGL.ClientID %>").val(ddlSGL.val());
                    $("#<%=uoHiddenFieldHotelDBL.ClientID %>").val(ddlDBL.val());

                    $.fancybox.close();
                }
            });
        }

        function BindHotelToRequest(BranchID) {
            var ddlHotelSGL = $("#<%=uoDropDownListHotelToRequestSGL.ClientID %>");
            var ddlHotelDBL = $("#<%=uoDropDownListHotelToRequestDBL.ClientID %>");
            
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../PageMethods.aspx/GetHotelBranchByCityOfHotel",
                data: "{'sBranchID': '" + BranchID + "'}",
                dataType: "json",
                success: function(data) {

                    //remove all the options in dropdown
                    $("#<%=uoDropDownListHotelToRequestSGL.ClientID %>> option").remove();
                    $("#<%=uoDropDownListHotelToRequestDBL.ClientID %>> option").remove();


                    //add option in dropdown
                    $("<option value='PROCESSING'>PROCESSING...</option>").appendTo(ddlHotelSGL);
                    $("<option value='0'>--SELECT HOTEL--</option>").appendTo(ddlHotelSGL);

                    $("<option value='PROCESSING'>PROCESSING...</option>").appendTo(ddlHotelDBL);
                    $("<option value='0'>--SELECT HOTEL--</option>").appendTo(ddlHotelDBL);

                    for (var i = 0; i < data.d.length; i++) {
                        //add the data coming from the result
                        $("<option value='" + data.d[i].HotelIDString + "'>" + data.d[i].HotelNameString + "</option>").appendTo(ddlHotelSGL);
                        $("<option value='" + data.d[i].HotelIDString + "'>" + data.d[i].HotelNameString + "</option>").appendTo(ddlHotelDBL);
                        
                    }
                    $("#<%=uoDropDownListHotelToRequestSGL.ClientID %>> option[value='PROCESSING']").remove();
                    $("#<%=uoDropDownListHotelToRequestDBL.ClientID %>> option[value='PROCESSING']").remove();
                    
                }
                        ,
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
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
        function showRate() {
            $("#uoTableManifest tr").each(function(i, element) {
                var $TDs = $(this).find('td');
                var txtDBL = $('td:eq(12) input[name*="uoTextBoxToAddDBL"]', this);
                var txtSGL = $('td:eq(21) input[name*="uoTextBoxToAddSGL"]', this);
                var chkApprove = $('td:eq(35) input[name*="uoCheckBoxApprove"]', this);

                var dvRateDBL = $('td:eq(12) div[id*="ucDivRateDouble"]', this);
                var dvRateSGL = $('td:eq(21) div[id*="ucDivRateSingle"]', this);
                var dvToApprove = $('td:eq(35) div[id*="ucDivToApprove"]', this);

                txtDBL.click(function(e) {
                    dvRateDBL.show();
                });

                txtDBL.mouseleave(function(e) {
                    dvRateDBL.hide();
                });

                txtSGL.click(function(e) {
                    dvRateSGL.show();
                });

                txtSGL.mouseleave(function(e) {
                    dvRateSGL.hide();
                });


                chkApprove.hover(function(e) {
                    dvToApprove.show();
                });

                chkApprove.mouseleave(function(e) {
                    dvToApprove.hide();
                });

            });
        }
    </script>     
   
</asp:Content>