<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMasterPage.Master" AutoEventWireup="true" CodeBehind="Medical.aspx.cs" Inherits="TRAVELMART.Medical.Medical" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>   
 
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td  style="width:100%;">
                   Medical Services
                </td> 
                <td style="white-space:nowrap;  ">
                  Port: &nbsp
                  <asp:DropDownList runat="server" ID="uoDropDownListPort" AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListPort_SelectedIndexChanged" />
                </td>                  
            </tr>
        </table>
</asp:Content>

 
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <style  type="text/css">
       .ListBox_Default {
       
           height: 100px !important;
           width: 100% !important;
       } 
       .nav {
        
            float: left;
            height: 1.8em;
            width:100%;
            text-align:center;
            font-weight: bold;
            text-decoration: none;
            border-width:0.1em;
            border-style: solid;
            border-color: #0288D8;
            border-radius: 1.5px;
            -moz-border-radius: 3.25px;
            -webkit-border-radius: 3.25px;
            line-height: 1.6em;
         } 
    </style>
    
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
                   
            <table width="100%"  >
                    <tr style=" height:100%;   ">
                        <td  style="  vertical-align:top; 
                              id="tdListbox" >
                                 <asp:ListBox ID="lstHotel" AutoPostBack="true" runat="server" 
                                        style="border-style:solid; border-width:1px; overflow:auto ;  "  
                                        OnSelectedIndexChanged="lstHotel_OnSelectedIndexChanged"
                                        ondblclick="lstHotel_DoubleClick"
                                    /> 
                          
                        </td>
                        <td  style="vertical-align:text-top; width:100%;  ">
                            <table style="width: 100%;  ">
                              <tr style="width: 100%; margin-top:-5px;  ">
                                    <td style="height:100%; width:70%; vertical-align:top;">
                                        <table style=" padding-top:-10px; width: 100%; margin-top:-5px; " border="0">
                                            <tr  style="display:none;">
                                                <td style=" width:25%;" >
                                                    <a href="#TabHotel" id="navHotel" class="nav" onclick="HighLightTabHeader(1)" >Hotel</a>
                                                </td>
                                                     
                                                <td  style= "width:25%;">
                                                    <a href="#Transportation" class="nav"  id="navTransportation"  onclick="HighLightTabHeader(2)" >Transportation</a>
                                                </td>
                                                <td  style=" width:25%;">
                                                    <a href="#PortAgent" class="nav" id="navPortAgent"  onclick="HighLightTabHeader(3)">Service Provider</a>
                                                </td>
                                                 <td  style=" width:25%;">
                                                    <a href="#MeetandGreet" class="nav" id="navMeetAndGreet"  onclick="HighLightTabHeader(4)">Meet and Greet</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                  <div id="TabHotel">
                                                        <table style="border: 1px;" id="TabHotelTable" width="100%">
                                                            <tr style=" display:none;">
                                                                <td colspan="1">
                                                                    Hotel :
                                                                </td>
                                                                
                                                                
                                                                <td colspan="3">
                                                                    <asp:DropDownList ID="uoDropDownListHotel" runat="server" 
                                                                    Width="83.8%"
                                                                        AutoPostBack="true" AppendDataBoundItems="true" onchange="SaveHotel()"
                                                                        />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="caption">
                                                                    Check In:
                                                                </td>
                                                                <td class="value">
                                                                    <asp:TextBox ID="uoTextBoxCheckinDate" runat="server" Text="" Width="80px" onchange="return OnDateChange('CheckIn');"></asp:TextBox>
                                                                    <cc1:TextBoxWatermarkExtender ID="uoTextBoxCheckinDate_Textboxwatermarkextender"
                                                                        runat="server" Enabled="True" TargetControlID="uoTextBoxCheckinDate" WatermarkCssClass="fieldWatermark"
                                                                        WatermarkText="MM/dd/yyyy">
                                                                    </cc1:TextBoxWatermarkExtender>
                                                                    <cc1:CalendarExtender ID="uoTextBoxCheckinDate_Calendarextender" runat="server" Enabled="True"
                                                                        TargetControlID="uoTextBoxCheckinDate" Format="MM/dd/yyyy">
                                                                    </cc1:CalendarExtender>
                                                                    <cc1:MaskedEditExtender ID="uoTextBoxCheckinDate_Maskededitextender" runat="server"
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxCheckinDate"
                                                                        UserDateFormat="MonthDayYear">
                                                                    </cc1:MaskedEditExtender>
                                                                    
                                                                </td>
                                                                <td class="caption" style="width: 60px;">
                                                                    Check Out:
                                                                </td>
                                                                <td class="value">
                                                                    <asp:TextBox ID="uoTextBoxCheckoutDate" runat="server" Text="" Width="80px" onchange="return OnDateChange('CheckOut');"></asp:TextBox>
                                                                    <cc1:TextBoxWatermarkExtender ID="uoTextBoxCheckoutDate_Textboxwatermarkextender"
                                                                        runat="server" Enabled="True" TargetControlID="uoTextBoxCheckoutDate" WatermarkCssClass="fieldWatermark"
                                                                        WatermarkText="MM/dd/yyyy">
                                                                    </cc1:TextBoxWatermarkExtender>
                                                                    <cc1:CalendarExtender ID="uoTextBoxCheckoutDate_Calendarextender" runat="server"
                                                                        Enabled="True" TargetControlID="uoTextBoxCheckoutDate" Format="MM/dd/yyyy">
                                                                    </cc1:CalendarExtender>
                                                                    <cc1:MaskedEditExtender ID="uoTextBoxCheckoutDate_Maskededitextender" runat="server"
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxCheckoutDate"
                                                                        UserDateFormat="MonthDayYear">
                                                                    </cc1:MaskedEditExtender>
                                                                   
                                                                    <asp:TextBox runat="server" ID="uoTxtBoxTimeIn" Visible="false" Width="80px" Text="00:00"></asp:TextBox>
                                                                    <cc1:TextBoxWatermarkExtender ID="uoTxtBoxTime_TextBoxWatermarkExtender" runat="server"
                                                                        Enabled="True" TargetControlID="uoTxtBoxTimeIn" WatermarkCssClass="fieldWatermark"
                                                                        WatermarkText="HH:mm">
                                                                    </cc1:TextBoxWatermarkExtender>
                                                                    <cc1:MaskedEditExtender ID="uoMaskedEditExtenderCheckInTime" runat="server" CultureAMPMPlaceholder=""
                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTxtBoxTimeIn"
                                                                        UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                                                                    </cc1:MaskedEditExtender>
                                                                    <asp:TextBox runat="server" ID="uoTxtBoxTimeOut" Visible="false" Width="80px" Text="00:00"></asp:TextBox>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" Enabled="True"
                                                                        TargetControlID="uoTxtBoxTimeOut" WatermarkCssClass="fieldWatermark" WatermarkText="HH:mm">
                                                                    </cc1:TextBoxWatermarkExtender>
                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTxtBoxTimeOut"
                                                                        UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                                                                    </cc1:MaskedEditExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="caption">
                                                                    Duration :
                                                                </td>
                                                                <td class="value">
                                                                    <asp:TextBox runat="server" ID="uoTextBoxDuration" onkeyup="SaveHotel()" Width="80px" Text="0"></asp:TextBox>
                                                                </td>
                                                                <td class="caption">
                                                                    Currency:
                                                                </td>
                                                                <td class="value">
                                                                    <asp:DropDownList ID="uoDropDownListCurrency" runat="server" CssClass="TextBoxInput" onchange="SaveHotel()"
                                                                        Width="200px">
                                                                    </asp:DropDownList>
                                                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoDropDownListCurrency"
                                                                        ErrorMessage="required" Font-Bold="true" ValidationGroup="Request" InitialValue="0" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="caption">
                                                                    Contracted Rate
                                                                    <br />
                                                                    Inclusive of Tax:
                                                                </td>
                                                                <td class="value">
                                                                    <asp:TextBox ID="uoTextContractedRate" ReadOnly="true" runat="server" Width="80px"
                                                                        Text="" onkeypress="return validate(event);" CssClass="ReadOnly"></asp:TextBox>
                                                                    &nbsp;
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoTextContractedRate"
                                                                        ErrorMessage="Amount is required." ValidationGroup="Validate">*</asp:RequiredFieldValidator>
                                                                   
                                                                    <asp:CheckBox ID="uoCheckContractBoxTaxInclusive" Visible="false" runat="server" />
                                                                </td>
                                                                <td class="caption" style="width: 80px; white-space:nowrap;">
                                                                    Meal Voucher :
                                                                </td>
                                                                <td class="value">
                                                                    <asp:TextBox ID="uoTextBoxMealVoucher" runat="server" Width="80px" Text=""  onkeyup="SaveHotel()"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            
                                                                <td class="caption" style="vertical-align: top">
                                                                    Comfirm Rate
                                                                    <br />
                                                                    Inclusive of Tax:
                                                                </td>
                                                                <td class="value">
                                                                    <asp:TextBox ID="uoTextBoxComfirmRate" runat="server" 
                                                                        onkeyup="SaveHotel()" Width="80px"></asp:TextBox>
                                                                </td>
                                                                <td class="caption" style="width: 80px; white-space:nowrap">
                                                                    Room Type :
                                                                </td>
                                                                <td class="value">
                                                                    <asp:DropDownList ID="uoDropDownListRoomeType" runat="server" CssClass="TextBoxInput" onchange="SelectRoomType(this)"
                                                                        Width="85px">
                                                                        <asp:ListItem Text="Single" Value="1" Selected="True" ></asp:ListItem>
                                                                        <asp:ListItem Text="Double" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>                             
                                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="caption" style="vertical-align: top">
                                                                    Meals:
                                                                </td>
                                                                <td class="value" colspan="3">
                                                                    <div>
                                                                        <asp:CheckBox ID="uoCheckboxBreakfast" runat="server" Text="Breakfast" onclick="SaveHotel()"/>
                                                                        &nbsp; &nbsp;
                                                                        <asp:CheckBox ID="uoCheckboxLunch" runat="server" Text="Lunch" onclick="SaveHotel()"/>
                                                                        &nbsp; &nbsp;
                                                                        <asp:CheckBox ID="uoCheckboxDinner" runat="server" Text="Dinner" onclick="SaveHotel()"/>
                                                                        &nbsp; &nbsp;
                                                                        <asp:CheckBox ID="uoCheckBoxLunchDinner" runat="server" Text="Additional Meals" onclick="SaveHotel()"/>
                                                                        &nbsp; &nbsp;
                                                                        <asp:CheckBox ID="uoCheckBoxIsWithShuttle" runat="server" Text=" With Shuttle" onclick="SaveHotel()"/>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:CheckBox ID="CheckBoxEmail" Text="Email :" runat="server" onclick="checkboxEnable(this)" />
                                                                </td>
                                                                <td valign="top" colspan="3">
                                                                    <asp:TextBox ID="uoTextBoxEmail"  runat="server"  Width="100%"  Height="83%" onkeyup="SaveHotel()"
                                                                        onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                                                                        ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                                                                        TextMode="MultiLine" Rows="2"></asp:TextBox>
                                                                </td>
                                                               
                                                            </tr>
                                                            
                                                            <tr>
                                                             <td valign="top" style="white-space:nowrap;">
                                                                   Comment : 
                                                                   <button id="uoButtonHotelComment" title="View all Comment"  style="height:15px; vertical-align:top;"  />
                                                                </td>
                                                                <td valign="top" colspan="3">
                                                                     <asp:TextBox ID="uoTextBoxComment" runat="server" Height="83%" TextMode="MultiLine" onkeyup="SaveHotel()"
                                                                           ToolTip="Click to select the request source" Rows="2" Width="100%" />
                                                                </td>
                                                            </tr>  
                                                             <tr runat="server" id="rowPortAgent" >
                                                             <td >
                                                                   Confirm by : 
                                                                </td>
                                                                <td valign="top" colspan="3">
                                                                     <asp:TextBox ID="uoTextBoxPortAgentConfirm" runat="server" Height="83%" TextMode="MultiLine" onkeyup="SaveHotel()"
                                                                           Rows="2" Width="100%" />
                                                                </td>
                                                            </tr>                                  
                                                        </table>
                                                    </div>
                                    
                                                
                                                </td>
                                            </tr>
                                            
                                        </table >           
                                    </td>
                                    <td style="height:100%; width:30%; vertical-align:top; 
                                         border-style:solid; border-width:1px;border-color:Gray;
                                          background-color:White; ">
                                        <asp:Panel ID="uoPanelRemark"  runat="server" 
                                                style=" height:100%; background-color:White;"   >
                                              
                                                <asp:ListView ID="uoListViewRemark" runat="server"  >
                                                    <LayoutTemplate>
                                                        <table border="0" id="uoListviewTableRemark" cellpadding="0" cellspacing="0"  
                                                               style="border-style:none; width:99.8%;" class="listViewTable">
                                                            <tr>
                                                               <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                                                            </tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr >
                                                            <td style="min-height:22px;height:22px; text-align:left; margin:5px; padding:8px; width:0.1%;">  
                                                                
                                                                <asp:HiddenField ID="uoHiddenFieldGridRemarkID" runat="server" Value='<%# Eval("RemarkID")%>'/>
                                                                <asp:HiddenField ID="uoHiddenFieldRemarkTRID" runat="server" Value='<%# Eval("TravelRequestID")%>'/>
                                                                <asp:HiddenField ID="uoHiddenFieldResourceID" runat="server" Value='<%# Eval("ReqResourceID")%>'/>
                                                                <asp:HiddenField ID="uoHiddenFieldTypeID" runat="server" Value='<%# Eval("RemarkTypeID")%>'/>
                                                                <asp:HiddenField ID="upHiddenFieldStatusID" runat="server" Value='<%# Eval("RemarkStatusID")%>'/>
                                                                <asp:HiddenField ID="uoHiddenFieldSumCall" runat="server" Value='<%# Eval("SummaryOfCall")%>'/>
                                                                <asp:HiddenField ID="uoHiddenFieldRequestorID" runat="server" Value='<%# Eval("RemarkRequestorID")%>'/>
                                                                <asp:HiddenField ID="uoHiddenFieldTransDate" runat="server" Value='<%# String.Format("{0:MM/dd/yyyy}", Eval("TransactionDate"))%>'/>
                                                                <asp:HiddenField ID="uoHiddenFieldTransTime" runat="server" Value='<%# String.Format("{0:HH:mm}", Eval("TransactionTime"))%>'/>
                                                                <asp:HiddenField ID="uoHiddenFieldPortCode" runat="server" Value='<%# Eval("PortCode")%>'/>
                                                               
                                                                <asp:Label runat="server" ID="lblUserName"   Text='<%# Eval("RemarkBy") %>'
                                                                    style="font-style:normal; font-weight:bold;"/>
                                                                <br/>
                                                                <div style="max-width:400px; table-layout:fixed; overflow:hidden; position:relative;">
                                                                     <asp:Label runat="server" ID="lblRemark" style="table-layout:fixed; overflow:hidden;"
                                                                            ToolTip='<%# Eval("Remark") %>' Text='<%# Eval("Remark") %>'/>
                                                                </div>                                                                   
                                                                <b>
                                                                <asp:Label runat="server" ID="Label6"  Text='<%# Eval("RecordLocator") %>'
                                                                        style="font-size:xx-small; color:#B0B0B0 ; font-style:oblique; "
                                                                        ToolTip='<%# Eval("Remark") %>' /></b>
                                                                     &nbsp;
                                                               <asp:Label runat="server" ID="Label4"  Text='<%# Eval("Resource") %>'
                                                                    style="font-size:xx-small; color:#C0C0C0" ToolTip='<%# Eval("Remark") %>'
                                                                    />
                                                                    &nbsp;
                                                                <asp:Label runat="server" ID="lblDateCreated"  Text='<%# Eval("RemarkDate") %>'
                                                                    style="font-size:xx-small; color:#C0C0C0;" ToolTip='<%# Eval("Remark") %>'
                                                                    /> &nbsp; &nbsp;
                                                                <asp:LinkButton ID="uoLinkButtonEditRemark" ToolTip="Edit Remark" Text="Edit"  
                                                                    runat="server" OnClientClick="EditRemarkmain(this)" 
                                                                    style="font-size:xx-small;"
                                                                    Visible='<%# Convert.ToBoolean(Eval("Visible").ToString() == "True" ? 1 : 0) %>'/>
                                                                    &nbsp;
                                                                <asp:LinkButton ID="uoLinkButtonDeleteRemark" ToolTip="Delete Remark" Text="Delete"  
                                                                    style="font-size:xx-small;"
                                                                    runat="server" OnClientClick="return DeleteRemarkmain(this)" Visible='<%# Convert.ToBoolean( Eval("Visible").ToString() == "True" ? 1 : 0) %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <table>
                                                             <tr> 
                                                                <td class="leftAligned">
                                                                    No Remark
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                        </asp:Panel>
                                    
                                    </td>
                              </tr>
                              <tr>
                                <td style="white-space:nowrap">
                                   <asp:Button runat="server" ID="uoButtonFinish" CssClass="SmallButton" Text="Finish" ValidationGroup="Request"  />
                                   <asp:FileUpload runat="server" ID="uoFileLoadExcel" 
                                                        CssClass="SmallButton" Text="Load Excel"
                                                        onchange="UploadFile(this);" EnableViewState="True" ViewStateMode="Enabled"  /> 

                               
                                 <%--  <div style="white-space:nowrap">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" > 
                                            <ContentTemplate> 
                                                <asp:FileUpload runat="server" ID="uoFileLoadExcel" 
                                                        CssClass="SmallButton" Text="Load Excel"
                                                        onchange="UploadFile(this);" EnableViewState="True" ViewStateMode="Enabled"  /> 
                                            </ContentTemplate> 
                                            <Triggers> 
                                                <asp:PostBackTrigger ControlID="uoButtonLoadExcel" /> 
                                            </Triggers> 
                                        </asp:UpdatePanel> 
                                       
                                    </div>--%>
                                    <%--<asp:Button runat="server" ID="uoButtonLoadExcel" CssClass="SmallButton" Text="Load Excel" 
                                                    Style="display: none" OnClick="Upload" />  --%>
                                </td>
                              </tr>
                              <tr>
                                <td style="width: 100%;  " colspan="2" >
                                
                                
                               
                                   <asp:Panel ID="uoPanelRequest" runat="server" ScrollBars="Auto" >
                                    <asp:ListView runat="server" ID="uoListviewRequest"> 
                                            <LayoutTemplate>
                                                 <table border="0" id="uoListviewTravelTable" cellpadding="0" 
                                                        cellspacing="0" class="listViewTable" >
                                                    <tr>
                                                        <th style="text-align: center; white-space: nowrap; width:20px;">
                                                        </th >
                                                         <th style="text-align: center; white-space: nowrap; width:80px;">
                                                            ID Number
                                                        </th >
                                                        <th style="text-align: center; white-space: nowrap; width:200px;">
                                                            Name
                                                        </th >
                                                        <th style="text-align: center; white-space: nowrap; ">
                                                            Gender
                                                        </th >
                                                        <th style="text-align: center; white-space: normal;">
                                                            Ship
                                                        </th >
                                                        <th style="text-align: center; white-space: normal; width:200px;">
                                                            PickUp Location and Phone
                                                        </th >
                                                        <th style="text-align: center; white-space: normal;">
                                                            PickupTime
                                                        </th >
                                                        <th style="text-align: center; white-space: normal;">
                                                            DropOffTime
                                                        </th >
                                                        <th style="text-align: center; white-space: normal;">
                                                            ApptTime
                                                        </th >
                                                        <th style="text-align: center; white-space: normal;">
                                                            Doctor or ApptType with Address	 
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap;">
                                                            Coordinator
                                                        </th> 
                                                        <th style="text-align: center; white-space:nowrap; width:75px;">
                                                            RCCLCaseManager
                                                        </th>  
                                                    </tr>        
                                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"/>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                   <td style="padding-left:3px; ">
                                                        <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBoxs" runat="server"/> 
                                                   </td>
                                                   <td class="leftAligned" style="white-space: normal; width:80px;" >
                                                        <asp:Label runat="server" ID="lblSeafarerID" 
                                                            Text='<%# Eval("SeafarerID") %>'/>
                                                    </td>
                                                   
                                                   <td class="leftAligned" style="white-space: normal; width:200px;" >
                                                        <asp:Label runat="server" ID="lblName"  width="200px"
                                                            Text='<%# Eval("Name") %>'/>
                                                    </td>
                                                   <td class="leftAligned">
                                                        <asp:Label runat="server" ID="lblGender" 
                                                            Text='<%# Eval("Gender") %>'/>
                                                    </td>
                                                   <td class="leftAligned">
                                                        <asp:Label runat="server" ID="lblNationality"  
                                                            Text='<%# Eval("Ship")%>'/>
                                                    </td>
                                                   <td class="leftAligned">
                                                        <asp:Label runat="server" ID="lblShip"  
                                                            Text='<%# Eval("Location")  %>'/>
                                                    </td>
                                                    <td class="leftAligned">
                                                        <asp:Label runat="server" ID="lblBrand"  
                                                            Text='<%# Eval("PickUpTime")  %>'/>
                                                    </td> 
                                                    
                                                    <td class="leftAligned">
                                                        <asp:Label runat="server" ID="lblPosition"  
                                                            Text='<%# Eval("DropOffTime")  %>'/>
                                                    </td> 
                                                    
                                                    <td class="leftAligned">
                                                        <asp:Label runat="server" ID="lblStatus"  
                                                            Text='<%# Eval("AppntTime")  %>'/>
                                                    </td> 
                                                    
                                                    <td class="leftAligned">
                                                        <asp:Label runat="server" ID="lblReasonCode"  
                                                            Text='<%# Eval("DoctorAppntTypeAdd")  %>'/>
                                                    </td> 
                                                    
                                                    <td class="leftAligned">
                                                        <asp:Label runat="server" ID="lblOnOffDate"  
                                                            Text='<%# Eval("Coordinator")  %>'/>
                                                    </td> 
                                                    <td class="leftAligned">
                                                        <asp:Label runat="server" ID="lblPort"  
                                                            Text='<%# Eval("RCCLCaseManager")  %>'/>
                                                    </td> 
                                                   <%-- <td class="leftAligned">
                                                        <asp:Label runat="server" ID="uoLabelLOE" 
                                                        Text='<%# Eval("LOEStatus") %>' />
                                                    </td>--%>
                                                     <td style="white-space: normal;  visibility:hidden; display:none;" >
                                                     
                                                     <%--   <asp:HiddenField runat="server" ID="uoHiddenFieldTravelRequestID" Value='<%# Eval("TravelRequetID") %>' />
                                                        <asp:HiddenField runat="server" ID="uoHiddenFieldTRIDBint" Value='<%# Eval("IDBigint") %>' />
                                                        <asp:HiddenField runat="server" ID="uoHiddenFieldSeqNo" Value='<%# Eval("SeqNo") %>' />
                                                        <asp:HiddenField runat="server" ID="uoHiddenFieldPortID" Value='<%# Eval("PortID") %>' />
                                                        <asp:HiddenField runat="server" ID="uoHiddenFieldRequestID" Value='<%# Eval("RequestID") %>' />
                                                        <asp:HiddenField runat="server" ID="uoHiddenFieldTransHotelID" Value='<%# Eval("TransHotelID") %>' />
                                                       --%> 

        <%--                                                <asp:HiddenField runat="server" ID="uoHiddenFieldLOEDate" Value='<%# String.Format("{0:dd-MMM-yyyy}", Eval("LOEDate"))%>' />
                                                        <asp:HiddenField runat="server" ID="uoHiddenFieldOfficer" Value='<%# Bind("LOEImmigrationOfficer") %>' />
                                                        <asp:HiddenField runat="server" ID="uoHiddenFieldPlace" Value='<%# Bind("LOEImmigrationPlace") %>' />
                                                        <asp:HiddenField runat="server" ID="uoHiddenFieldLOEReason" Value='<%# Bind("LOEReason") %>' />
        --%>
                                                    </td>
                                                  </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table  class="listViewTable" width="100px">
                                                    <tr>
                                                         <th style="text-align: center; white-space: nowrap; width:80px;">
                                                            ID Number
                                                        </th >
                                                        <th style="text-align: center; white-space: normal; width:200px;">
                                                            Name
                                                        </th >
                                                        <th style="text-align: center; white-space: nowrap; ">
                                                            Gender
                                                        </th >
                                                       <%-- <th style="text-align: center; white-space: nowrap;">
                                                            CrewMember Signature
                                                        </th >--%>
                                                         <th style="text-align: center; white-space: nowrap;">
                                                            Ship
                                                        </th >
                                                        <th style="text-align: center; white-space: normal; width:150px;">
                                                            PickUp Location and Phone
                                                        </th >
                                                        <th style="text-align: center; white-space: normal;">
                                                            Pickup Time
                                                        </th >
                                                        <th style="text-align: center; white-space: normal;">
                                                            Drop Off Time
                                                        </th >
                                                        <th style="text-align: center; white-space: normal;">
                                                            Appt. Time	
                                                        </th >
                                                        <th style="text-align: center; white-space: normal; width:150px">
                                                            Doctor or ApptType with Address	 
                                                        </th>
                                                        <th style="text-align: center; white-space: normal;">
                                                            Coordinator	
                                                        </th> 
                                                        <th style="text-align: center; white-space:normal; 
                                                            width:75px;">
                                                            RCCL Case Manager
                                                        </th>  
                                                    </tr>  
                                                    <tr>
                                                        <td colspan="1" class="leftAligned">
                                                            No Record
                                                        </td>
                                                        <td colspan="11" class="leftAligned">
                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                               </td>
                              </tr>  
                           </table>
                        </td>
                    </tr>
                  
            </table>
        
        </ContentTemplate>
         <Triggers> 
            <asp:PostBackTrigger ControlID="uoButtonLoadExcel" /> 
        </Triggers> 
    </asp:UpdatePanel>
    
  
    
    
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button runat="server" ID="uoButtonLoadExcel" 
                CssClass="SmallButton" Text="Load Excel" Style="display: none" OnClick="Upload"  />  
        </ContentTemplate>
    </asp:UpdatePanel>
      
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value=""/>
    <asp:HiddenField ID="uoHiddenFieldCityCode" runat="server" Value=""/>
    <asp:HiddenField ID="uoHiddenFieldContractStart" runat="server" Value=""/>
     <asp:HiddenField ID="uoHiddenFieldContractEnd" runat="server" Value=""/>
         
    <script type="text/javascript" language="javascript">

            $(document).ready(function() {
                TriggleJquery();
                HighLightTab();
            
            });


            function pageLoad(sender, args) {
                var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
                if (isAsyncPostback) {
                    TriggleJquery();
                    HighLightTab();
                }
            }

            function TriggleJquery() {
                var height = screen.height;
//                document.getElementById("divListBox").style.Height = height - 100 + "px";

                var h = (height - 400) + "px";
                
                $("[id$=divListBox]").height((height - 400) + "px");
                $("[id$=lstHotel]").height(h);
                console.log($("[id$=pnlListView]").height);
                var wd = $(window).width() * 0.50;
                var wh = $(window).height() * 0.50;
                
                console.log(wd);
                $("[id$=lstHotel]").width(330);
                $("[id$=tdListbox]").width(40);
//                $("#divRequest").width(wd);
//

//                $("#divRequest").height(wh - 110 + "px");
                $("[id$=uoPanelRequest]").width(wd);


                $("[id$=uoPanelRequest]").height(wh - 110 + "px");

                
                console.log($("#divRequest").width);
            
            }


            function HighLightTab(val) {
             
                $('.nav').css('background', '#ECDFC4');
                $('.nav').css('bordercolor', '#0288D8');
                $('.nav').css('color' , '#0288D8');
       
                $('#navHotel').css('background', '#0288D8');
                $('#navHotel').css('bordercolor', '#0288D8');
                $('#navHotel').css('color', '#ffffff');

 
            }


             

            function UploadFile(fileUpload) {
            
                if (fileUpload.value != '') {
                    document.getElementById("<%=uoButtonLoadExcel.ClientID %>").click();
                }
            }
            
     </script>    
      
</asp:Content>
 