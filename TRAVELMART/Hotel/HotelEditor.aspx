<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="True"
    CodeBehind="HotelEditor.aspx.cs" Inherits="TRAVELMART.HotelEditor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <link href="../Css/TaskStylesheet.css" rel="stylesheet" type="text/css" />--%>   
    <style type="text/css">
        .style1
        {
            width: 200px;
        }
        .style2
        {
            width: 200px;
            color:Red;
        }
    </style>       
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script> 
        
     <script type="text/javascript">
         $(document).ready(function() {
             pageSettings();
         });

         function pageLoad(sender, args) {
             var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
             if (isAsyncPostback) {
                 pageSettings();
             }
         }

         function pageSettings() {
             $("#<%=uoTextBoxCheckInDate.ClientID %>").change(function(ev) {
                 ValidateIfPastDate($(this).val());
             });

             $("#<%=uoButtonValidate.ClientID %>").click(function(ev) {
                 if (!ValidateIfPastDate($("#<%=uoTextBoxCheckInDate.ClientID %>").val())) {
                     $("#<%=uoTextBoxCheckInDate.ClientID %>").val("");
                     return false;
                 }
             });

             $("#<%=uoButtonSave.ClientID %>").click(function(ev) {
                 var sMsg="";

                 if ($("#<%=uoHiddenFieldIsValidated.ClientID %>").val() != "1") {
                     sMsg = "Click Check Room Blocks button before saving!";
                     alert(sMsg);
                     return false;
                 }
                 if ($("#<%=uoHiddenFieldBranchID.ClientID %>").val() != $("#<%=uoDropDownListBranch.ClientID %>").val()) {
                     sMsg = "Hotel branch has been changed.\nClick Check Room Blocks button before saving!";
                     alert(sMsg);
                     return false;
                 }
                 if ($("#<%=uoHiddenFieldRoomTypeID.ClientID %>").val() != $("#<%=uoDropDownListRoomType.ClientID %>").val()) {
                     sMsg = "Room type has been changed.\nClick Check Room Blocks button before saving!";
                     alert(sMsg);
                     return false;
                 }
                 if ($("#<%=uoHiddenFieldCheckinDate.ClientID %>").val() != $("#<%=uoTextBoxCheckInDate.ClientID %>").val()) {
                     sMsg = "Checkin Date has been changed.\nClick Check Room Blocks button before saving!";
                     alert(sMsg);
                     return false;
                 }
                 if ($("#<%=uoHiddenFieldDuration.ClientID %>").val() != $("#<%=uoTextBoxNoOfdays.ClientID %>").val()) {
                     sMsg = "Duration has been changed.\nClick Check Room Blocks button before saving!";
                     alert(sMsg);
                     return false;
                 }
                 
                 var sMsg2 = "";
                 //Loop in uoGridViewDate row, not including the header
                 $("#<%=uoGridViewDate.ClientID %> tr").not(':first').each(function(ev) {
                     //Finds all inputs with an id attribute that contains 'uoTextBoxRate'
                     var txtAmt = $(this).find("input[id*='uoTextBoxRate']");
                     var chkTax = $(this).find("input[id*='uoCheckBoxTax']");
                     var txtTax = $(this).find("input[id*='uoTextBoxTaxPercent']");
                     if (txtAmt != null) {
                         var nAmt = txtAmt.val();
                         var nTax = txtTax.val();
                         if (nAmt == 0) {
                             //alert("Amount should not be 0!");
                             //return false;
                             sMsg2 = "\nAmount should not be 0!";
                         }
                     }
                 });
                 sMsg += sMsg2;
                 if (sMsg == "") {
                     return true;
                 }
                 else {
                     alert(sMsg);
                     return false;
                 }
             });
         }

         function validate(key) {

             //getting key code of pressed key
             var keycode = (key.which) ? key.which : key.keyCode;
             if ((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) {
                 return false;
             }
             else {
                 return true;
             }
             return true;
         }

         function CheckIndateChange(sender, args) {
             ValidateIfPastDate(sender._selectedDate.format(sender._format));
         }

         function ValidateIfPastDate(pDate) {
             if (ValidateDate(pDate)) {

                 var currentDate = new Date();
                 var currentDate2 = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate());

                 var selectedDate = Date.parse(pDate);
                                  
                 if (selectedDate < currentDate2) {
                     alert("Past date is invalid!");
                     $("#<%=uoTextBoxCheckInDate.ClientID %>").val("");                    
                     return false;
                 }                 
             }
             else {
                 alert("Invalid date!");
                 $("#<%=uoTextBoxCheckInDate.ClientID %>").val("");                 
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
    </script>
    
    <script type="text/javascript">                                    
        function GetAmount() {
            var Days = $("#<%=uoTextBoxNoOfdays.ClientID %>");
            var hStripe = $("#<%=uoHiddenFieldStripe.ClientID %>");
            
            var Branch = $("#<%=uoDropDownListBranch.ClientID %>");
            var txtAmount = $("#<%=uoTextBoxVoucher.ClientID %>");
            //                if (ViewCountry.val() == "1") {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/PageMethods.aspx/GetVoucherAmount",
                data: "{'sStripe': '" + hStripe.val() + "', 'BranchId': '" + Branch.val() + "', 'Days': '" + Days.val() + "'}",
                dataType: "json",
                success: function(data) {

                    document.getElementById('<%=uoTextBoxVoucher.ClientID %>').value = data.d;
                    document.getElementById('<%=uoHiddenFieldVoucherAmount.ClientID %>').value = data.d;
                    return false;
                }
                        ,
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
            
        }

        function OpenContract(vendorID) 
        {
            window.parent.OpenContract(vendorID, 'HO');            
            return false;
        }       
    </script>
    
    <script type="text/javascript">
        function OpenEventsList(branchID, cityID, OnOffDate) {
            window.parent.OpenEventsList(branchID, cityID, OnOffDate);
            return false;
        }         
    </script>  
        
    <%--<script type="text/javascript" language="javascript">
        function OpenEventsList(branchID, cityID, OnOffDate) {
            var URLString = "../Maintenance/EventsList.aspx?bId=";
            URLString += branchID;
            URLString += "&cityId=" + cityID;
            URLString += "&Date=" + OnOffDate;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Events_List', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
    </script>--%>
    
    <div class="PageTitle">
        Hotel and Property
    </div>
    <hr />    
    <table width="100%">
        <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" BackColor="Beige"/>
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap" class="style1">
                Seafarer Name:
            </td>
            <td  colspan="4">
                <asp:TextBox ID="uoTextBoxName" runat="server" Width="780px" ReadOnly="True" 
                    CssClass="ReadOnly"></asp:TextBox>
            </td>           
        </tr>
        <tr>
            <td class="style1" >
                Category:</td>
            <td  style="width:400px">
                <asp:DropDownList ID="uoDropDownListCategory" runat="server" Width="305px" 
                    AutoPostBack="True" 
                    onselectedindexchanged="uoDropDownListCategory_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="1">Accredited</asp:ListItem>
                    <asp:ListItem Value="2">Non Accredited</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width:100px">
                  Meals:
            </td>
            <td style="width:150px" rowspan="4">
                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                    <ContentTemplate>
                        <table  style="vertical-align:top">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="uoCheckBoxBreakfast" runat="server" Text="Breakfast"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="uoCheckBoxLunchDinner" runat="server" Text="Lunch/Dinner" 
                                    oncheckedchanged="uoCheckBoxLunchDinner_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="uoCheckBoxLunch" runat="server" Text="Lunch" 
                                    oncheckedchanged="uoCheckBoxLunch_CheckedChanged"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="uoCheckBoxDinner" runat="server" Text="Dinner" 
                                    oncheckedchanged="uoCheckBoxDinner_CheckedChanged"/>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListHotel" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCity" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListRoomType" />
                    </Triggers>
                </asp:UpdatePanel>                            
            </td>          
            <td style="width:400px">
                &nbsp;</td>   
        </tr>
        <tr>
            <td class="style1" >&nbsp;Hotel:
            </td>
            <td style=" white-space:nowrap">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                                
                <asp:DropDownList ID="uoDropDownListHotel" runat="server" Width="305px" AutoPostBack="True"
                    OnSelectedIndexChanged="uoDropDownListHotel_SelectedIndexChanged" 
                        AppendDataBoundItems="True">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uoDropDownListHotel"
                    ErrorMessage="Hotel Required." InitialValue="0">*</asp:RequiredFieldValidator>            
                
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCategory" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td></td>
             <%--<td style="white-space:nowrap" >
               <asp:CheckBox ID="uoCheckBoxLunchDinner" runat="server" Text="Lunch/Dinner" 
                    oncheckedchanged="uoCheckBoxLunchDinner_CheckedChanged" />
            </td>--%>
            <td>
                &nbsp;</td>          
        </tr>
        <tr>
        <td class="style1">&nbsp;Country:</td>
        <td >
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                    
            <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="305px" 
           AutoPostBack="True" 
                onselectedindexchanged="uoDropDownListCountry_SelectedIndexChanged" 
                    AppendDataBoundItems="True">
            </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="uoDropDownListCountry"
                ErrorMessage="Country required." InitialValue="0">*</asp:RequiredFieldValidator>
            
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListCategory" 
                        EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListHotel" 
                        EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>            
        <td></td>
        <%--<td >
            <asp:CheckBox ID="uoCheckBoxLunch" runat="server" Text="Lunch" 
                oncheckedchanged="uoCheckBoxLunch_CheckedChanged"/>
        </td>  --%>
         <td>
             &nbsp;</td>  
    </tr>
    <tr>
        <td class="style1">&nbsp;City:</td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                        
            <asp:DropDownList ID="uoDropDownListCity" runat="server" Width="305px" 
             AutoPostBack="True" AppendDataBoundItems="true"
                onselectedindexchanged="uoDropDownListCity_SelectedIndexChanged">
            </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoDropDownListCity"
                ErrorMessage="City required." InitialValue="0">*</asp:RequiredFieldValidator>
                
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListCategory" 
                        EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListHotel" 
                        EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" 
                        EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>   
        <td></td>
        <%--<td>
                <asp:CheckBox ID="uoCheckBoxDinner" runat="server" Text="Dinner" 
                    oncheckedchanged="uoCheckBoxDinner_CheckedChanged"/>
        </td>--%>
         <td>
             &nbsp;</td>         
    </tr>   
        <tr>
            <td class="style1" >&nbsp;Branch:
            </td>
            <td style=" white-space:nowrap" >
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                
                <asp:DropDownList ID="uoDropDownListBranch" runat="server" Width="305px"                     
                    AutoPostBack="True" 
                    onselectedindexchanged="uoDropDownListBranch_SelectedIndexChanged" 
                        AppendDataBoundItems="True">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoDropDownListBranch"
                    ErrorMessage="Branch Required." InitialValue="0">*</asp:RequiredFieldValidator>                
                    <asp:LinkButton ID="uoLinkButtonContract" runat="server">Contract</asp:LinkButton>                
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCategory" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListHotel" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCity" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td> 
            <td style="white-space:nowrap" >
                 &nbsp;Hotel Status:
            </td>    
            <td  colspan="2">
                <asp:DropDownList ID="uoDropDownListHotelStatus" runat="server" Width="324px" 
                    AutoPostBack="True" 
                    onselectedindexchanged="uoDropDownListHotelStatus_SelectedIndexChanged">
                    <asp:ListItem Value="Unused">Unused</asp:ListItem>
                    <asp:ListItem Value="Checked In">Checked In</asp:ListItem>
                    <asp:ListItem Value="Checked Out">Checked Out</asp:ListItem>
                    <asp:ListItem Value="No Show">No Show</asp:ListItem>
                    <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                </asp:DropDownList>
            </td>         
        </tr>
        <tr>
            <td class="style1" >&nbsp;Room Type:
            </td>
            <td  >
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                <ContentTemplate>
                
                <asp:DropDownList ID="uoDropDownListRoomType" runat="server" Width="305px" 
                    onselectedindexchanged="uoDropDownListRoomType_SelectedIndexChanged" 
                    AppendDataBoundItems="True" AutoPostBack="True">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_uoDropDownListRoomType" runat="server" ControlToValidate="uoDropDownListRoomType"
                    ErrorMessage="Room Type Required." InitialValue="0">*</asp:RequiredFieldValidator>
                
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCategory" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListHotel" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCity" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" 
                            EventName="SelectedIndexChanged" />                        
                    </Triggers>
                </asp:UpdatePanel>
            </td>     
            <td></td>      
            <td colspan="2">                  
                                &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                  
                <asp:CheckBox ID="uoCheckBoxCrewBill" runat="server" Text="Charge to crew" Visible="False" ToolTip="Set charges for hotel status cancelled."/>
            </td>  
        </tr>
        <tr>
            <td class="style1" >&nbsp;Checkin Date:
            </td>
            <td  >
                <asp:TextBox ID="uoTextBoxCheckInDate" runat="server" Text="" Width="300px"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="uoTextBoxPickUpDate_TextBoxWatermarkExtender" runat="server"
                    Enabled="True" TargetControlID="uoTextBoxCheckInDate" WatermarkCssClass="fieldWatermark"
                    WatermarkText="MM/dd/yyyy">
                </cc1:TextBoxWatermarkExtender>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="uoTextBoxPickUpDate_CalendarExtender" runat="server" Enabled="True"
                    TargetControlID="uoTextBoxCheckInDate" PopupButtonID="ImageButton1" Format="MM/dd/yyyy" 
                    OnClientDateSelectionChanged="CheckIndateChange">
                </cc1:CalendarExtender >
                <cc1:MaskedEditExtender ID="uoTextBoxPickUpDate_MaskedEditExtender" runat="server"
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxCheckInDate"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <%--<asp:CalendarExtender ID="uoTextBoxCheckInDate_CalendarExtender" runat="server" Enabled="True"
                    PopupButtonID="ucImageButtonCheckinDate" TargetControlID="uoTextBoxCheckInDate"
                    Format="MM/dd/yyyy">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                    Enabled="True" MaskType="Date" TargetControlID="uoTextBoxCheckInDate" Mask="99/99/9999"
                    UserDateFormat="MonthDayYear">
                </asp:MaskedEditExtender>--%>            
                <%--<asp:ImageButton ID="ucImageButtonCheckinDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />--%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoTextBoxCheckInDate"
                    ErrorMessage="Check In Date Required.">*</asp:RequiredFieldValidator>
            </td>
            <td >
                 &nbsp;Time:
            </td>
            <td  colspan="2">
                <asp:TextBox ID="uoTextBoxCheckInTime" runat="server" Text="" Width="324px"></asp:TextBox>
                <%--<asp:CalendarExtender ID="uoCalendarExtenderCheckInTime" runat="server" Enabled="True"                    
                    TargetControlID="uoTextBoxCheckInDate" Format="HH:mm">
                </asp:CalendarExtender>--%>
                <cc1:TextBoxWatermarkExtender ID="uoTextBoxCheckInTime_TextBoxWatermarkExtender"
                    runat="server" Enabled="True" TargetControlID="uoTextBoxCheckInTime" WatermarkCssClass="fieldWatermark" WatermarkText="HH:mm">
                </cc1:TextBoxWatermarkExtender>
                <cc1:MaskedEditExtender ID="uoMaskedEditExtenderCheckInTime" runat="server" CultureAMPMPlaceholder=""
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                    Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxCheckInTime"
                    UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                </cc1:MaskedEditExtender>
                <%--<asp:MaskedEditExtender ID="uoMaskedEditExtenderCheckInTime" runat="server" CultureAMPMPlaceholder=""
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                    Enabled="True" MaskType="Time" TargetControlID="uoTextBoxCheckInTime" Mask="99:99"
                    UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                </asp:MaskedEditExtender>--%>
            </td> 
        </tr>
        <tr>
            <td class="style1" >&nbsp;No. of Days:</td>
            <td >
                <asp:TextBox ID="uoTextBoxNoOfdays" runat="server" Width="300px" 
                Text="1" onchange="return GetAmount();"></asp:TextBox>
                <asp:NumericUpDownExtender ID="uoTextBoxNoOfdays_NumericUpDownExtender" runat="server"
                    Enabled="True" Maximum="100" Minimum="1" RefValues="" ServiceDownMethod="" ServiceDownPath=""
                    ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="uoTextBoxNoOfdays"
                    Width="50">
                </asp:NumericUpDownExtender>
            </td> 
            <td >
                &nbsp;With Shuttle:
            </td>   
             <td   colspan="2">             
                 <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>                    
                        <asp:CheckBox ID="uoCheckBoxShuttle" runat="server" Text="Transportation"/>
                    </ContentTemplate>
                     <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" 
                             EventName="SelectedIndexChanged" />
                     </Triggers>
                 </asp:UpdatePanel>
            </td>          
        </tr>
        <tr>
            <td class="style1" >&nbsp;Currency:&nbsp;</td>
            <td >
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>

                <asp:TextBox ID="uoTextBoxCurrency" runat="server" Width="300px" 
                    CssClass="ReadOnly" ReadOnly="True"></asp:TextBox>
      
            </ContentTemplate> 
            <Triggers >
                <asp:AsyncPostBackTrigger ControlID = "uoDropDownListCountry" EventName="SelectedIndexChanged" />                           
                <asp:AsyncPostBackTrigger ControlID = "uoDropDownListHotel" />
                <asp:AsyncPostBackTrigger ControlID = "uoDropDownListBranch" />
                <asp:AsyncPostBackTrigger ControlID = "uoDropDownListCity" />
                <asp:AsyncPostBackTrigger ControlID = "uoDropDownListRoomType" />
            </Triggers>
            </asp:UpdatePanel> 
            </td>             
            <td >                
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">   
                    <ContentTemplate>
                        <asp:Label ID="uoLabelRoomAmount" runat="server" ForeColor=Green Visible=false>&nbsp;Room Amount:&nbsp;</asp:Label>                        
                    </ContentTemplate>
                        <%--<Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoButtonSave" 
                            EventName="Click" />                                            
                        </Triggers>--%>
                </asp:UpdatePanel>                    
            <%--&nbsp;Event:&nbsp;--%></td>
            <td >
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">   
                    <ContentTemplate>                    
                        <asp:TextBox ID="uoTextBoxRoomAmount" runat="server" Width="300px" Visible=false></asp:TextBox>       
                        <cc1:MaskedEditExtender ID="uoTextBoxRoomAmount_MaskedEditExtender" 
                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="999,999,999.99" MaskType="Number" TargetControlID="uoTextBoxRoomAmount" 
                                        InputDirection="RightToLeft">
                        </cc1:MaskedEditExtender>                     
                    </ContentTemplate>
                        <%--<Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoButtonSave" 
                            EventName="Click" />                                            
                        </Triggers>--%>
                </asp:UpdatePanel>
            </td>          
        </tr>
        <tr>
            <td valign="top" class="style1">&nbsp;Voucher Amount:&nbsp;</td>
            <td colspan="1">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                <ContentTemplate >
                
                <asp:TextBox ID="uoTextBoxVoucher" runat="server" Width="300px" 
                    CssClass="ReadOnly" ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers >
                        <asp:AsyncPostBackTrigger ControlID = "uoDropDownListCountry" />
                        <asp:AsyncPostBackTrigger ControlID = "uoDropDownListCity" />
                        <asp:AsyncPostBackTrigger ControlID = "uoDropDownListHotel" />
                        <asp:AsyncPostBackTrigger ControlID = "uoDropDownListBranch" />
                        <asp:AsyncPostBackTrigger ControlID = "uoDropDownListRoomType" />
                    </Triggers>
                    </asp:UpdatePanel>
            </td> 
            <td colspan=1></td>  
            <td>
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">   
                    <ContentTemplate>
                        <asp:CheckBox ID="uoCheckBoxTax" runat="server" Visible="false" AutoPostBack="true"
                        Text="With Tax" oncheckedchanged="uoCheckBoxTax_CheckedChanged" />
                    </ContentTemplate>
                        <%--<Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoCheckBoxTax" 
                            EventName="CheckedChanged" />
                        </Triggers>--%>
                </asp:UpdatePanel>
            </td>                   
        </tr>
        <tr>
            <td>Confirmation No.:</td>
            <td>
                <asp:TextBox ID="uoTextBoxConfirmation" runat="server" Width="300px"></asp:TextBox>                
            </td>
            <td >                
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">   
                    <ContentTemplate>
                        <asp:Label ID="uoLabelTaxAmount" runat="server" ForeColor=Green Visible=false>&nbsp;Tax(%):&nbsp;</asp:Label>                        
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoCheckBoxTax" 
                            EventName="CheckedChanged" />                                            
                        </Triggers>
                </asp:UpdatePanel>                    
            <%--&nbsp;Event:&nbsp;--%></td>
            <td >
                <asp:UpdatePanel ID="UpdatePanel13" runat="server">   
                    <ContentTemplate>                    
                        <asp:TextBox ID="uoTextBoxTaxAmount" runat="server" Width="300px" Visible="false" AutoPostBack="true"></asp:TextBox>       
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" 
                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="99.99" MaskType="Number" TargetControlID="uoTextBoxTaxAmount" 
                                        InputDirection="RightToLeft">
                        </cc1:MaskedEditExtender>                     
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoCheckBoxTax" 
                            EventName="CheckedChanged" />                                            
                        </Triggers>
                </asp:UpdatePanel>
            </td>            
        </tr>      
        <tr>
            <td class="style1"></td>
            <td class="style2">
                <asp:Button ID="uoButtonValidate" runat="server" Text="Check Room Blocks" CssClass="SmallButton" 
                    onclick="uoButtonValidate_Click" />
            </td>
            <td class="style2">                
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">   
                    <ContentTemplate>
                        <asp:Label ID="uoLabel" runat="server" Visible=false ForeColor=Red>&nbsp;Event:&nbsp;</asp:Label> 
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" 
                            EventName="SelectedIndexChanged" />                                            
                        </Triggers>
                </asp:UpdatePanel>                    
            <%--&nbsp;Event:&nbsp;--%></td> 
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">   
                    <ContentTemplate>
                        <asp:LinkButton ID="uoLinkButtonEvent" runat="server" Width="300px" ForeColor=Red Visible=false> </asp:LinkButton>
                        <%--<asp:Label ID="uoLabelEvent" runat="server" Width="300px" ForeColor=Red Visible=false></asp:Label>--%>
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" 
                            EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>  
        <tr>
            <td valign="top" class="style1">&nbsp;</td>
            <td  colspan="4">
                <asp:GridView ID="uoGridViewDate" runat="server" CssClass="listViewTable" 
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="colDetailsIDInt" HeaderText="DetailsIDInt"/>
                        <asp:BoundField DataField="colContractIDInt" HeaderText="ContractIDInt"/>
                                                
                        <asp:BoundField DataField="colDate" HeaderText="Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}"/>                        
                        <asp:TemplateField HeaderText = "From Contract">
                            <ItemTemplate>
                                 <asp:CheckBox ID="uoCheckBoxFromContract" runat="server" Checked='<%# Eval("colIsFromContract") %>' Enabled = "false"/>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText = "Room Rate" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="uoTextBoxRate" runat="server" Text='<%# String.Format("{0:0.00}", Eval("colRoomRate"))%>' 
                                CssClass ="SmallText" onkeypress="return validate(event);"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "With Tax">
                            <ItemTemplate>
                                <asp:CheckBox ID="uoCheckBoxTax" runat="server" Checked='<%# Eval("colIsWithTax") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText = "Tax (%)" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                 <asp:TextBox ID="uoTextBoxTaxPercent" runat="server" Text ='<%# Eval("colTaxPercent") %>' 
                                 CssClass ="SmallText" onkeypress="return validate(event);"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>               
        </tr>
        <tr>
            <td class="style1" valign="top" >&nbsp;Remarks:</td>
            <td colspan="3" >
                <asp:TextBox ID="uoTextBoxRemarks" runat="server" TextMode="MultiLine" 
                    Width="780px" Height="50px" CssClass="TextBoxInput"></asp:TextBox>
            </td>           
        </tr>        
        <tr>
            <td class="style1" >&nbsp;</td>
            <td colspan="3" >
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" OnClick="uoButtonSave_Click"
                    Width="70px" />
            </td>           
        </tr>        
    </table>
    <asp:HiddenField ID="uoHiddenFieldSfID" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldRankType" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldStripe" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldHotelBookingID" runat="server" />  
            <asp:HiddenField ID="uoHiddenFieldTravelRequestID" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldManualRequestID" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldRecordLocator" runat="server" Value="0"/>            
            <asp:HiddenField ID="uoHiddenFieldStatus" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldOnOffDate" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldPort" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldPortCountry" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldPortCity" runat="server" Value="0"/>
            
            <asp:HiddenField ID="uoHiddenFieldPendingId" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldAction" runat="server" />
            <asp:HiddenField ID="uoHiddenFieldCreatedBy" runat="server" />
            <asp:HiddenField ID="uoHiddenFieldCreatedDate" runat="server" />
            <asp:HiddenField ID="uoHiddenFieldCurrencyID" runat="server" Value="0"/> 
            <asp:HiddenField ID="uoHiddenFieldContractId" runat="server" Value="0"/> 
            <asp:HiddenField ID="uoHiddenFieldVoucherAmount" runat="server" Value="0.00" />
            <asp:HiddenField ID="uoHiddenFieldGroupNo" runat="server"  />
            <asp:HiddenField ID="uoHiddenFieldIsValidated" runat="server"  Value="0" />
            <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server"   />
            <asp:HiddenField ID="uoHiddenFieldRoomTypeID" runat="server"   />
            <asp:HiddenField ID="uoHiddenFieldCheckinDate" runat="server"   />
            <asp:HiddenField ID="uoHiddenFieldDuration" runat="server"   />
            
            <asp:HiddenField ID="uoHiddenFieldUser" runat="server"  />
        </asp:Content>
