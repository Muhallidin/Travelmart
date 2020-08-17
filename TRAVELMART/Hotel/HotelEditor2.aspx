<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" Async="true"  CodeBehind="HotelEditor2.aspx.cs" Inherits="TRAVELMART.Hotel.HotelEditor2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script> 
<script type="text/javascript">
    function closeWindow() {
        self.close();
    }

    window.onunload = function(ev) {
        window.opener.RefreshPageFromPopup();
        
    }

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

    function OpenContract(BranchId) {

        var URLString = "../ContractManagement/HotelContractView.aspx?vId=";
        URLString += BranchId + "&vmType=HO";
        
        var screenWidth = screen.availwidth;
        var screenHeight = screen.availheight;

        screenWidth = 800;
        screenHeight = 650;

        window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
        return false;
    }

    function validate(key) {
        //getting key code of pressed key
        var keycode = (key.which) ? key.which : key.keyCode;
                
        if (keycode >= 48 && keycode <= 57) {
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

    function validateDays() {
        //validation if no. of days is above the maximum value.
        var num = document.getElementById('<%=uoTxtBoxDays.ClientID %>').value;

        if (num > 100) {
            alert('Please enter no. of days in the range 1 to 100.');
            document.getElementById('<%=uoTxtBoxDays.ClientID %>').value = '1';
            document.getElementById('<%=uoTxtBoxDays.ClientID %>').focus();
            return false;
        }
        else if (num == 0) {
            alert('No. of days is not valid!');
            document.getElementById('<%=uoTxtBoxDays.ClientID %>').value = '1';
            document.getElementById('<%=uoTxtBoxDays.ClientID %>').focus();
            return false;
        }
        else {
            return true;
        }               
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
     <script type="text/javascript">
         function pageLoad(sender, args) {
             var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
             if (isAsyncPostback) {
                 pageSettings();
             }
         }

         $(document).ready(function() {
             pageSettings();
            
         });


       

         function pageSettings() {

             if ($("#<%=uoChkAccredited.ClientID %>").attr('checked')) {
                 $("#<%=uoLblAllocations.ClientID %>").show();
                 $("#<%=uoRBtnAllocations.ClientID %>").show();
                 $("#<%=uoBtnCheckRooms.ClientID %>").show();
             }
             else {
                 $("#<%=uoLblAllocations.ClientID %>").hide();
                 $("#<%=uoRBtnAllocations.ClientID %>").hide();
                 $("#<%=uoBtnCheckRooms.ClientID %>").hide();
             }  
            
             if ($("#<%=uoChkAdvanceSearch.ClientID %>").attr('checked')) {
                 $("#<%=uoTableAdvanceSearch.ClientID %>").show();
             }
             else {
                 $("#<%=uoTableAdvanceSearch.ClientID %>").hide();
             }             
             
             $("#<%=uoTxtBoxCheckIn.ClientID %>").change(function(ev) {
                 ValidateIfPastDate($(this).val());

             });

             $("#<%=uoTxtBoxTime.ClientID %>").change(function(ev) {
                 ValidateTime($(this).val());

             });

             $("#<%=uoTxtBoxDays.ClientID %>").change(function(ev) {
                 $("#<%=uoHiddenFieldDaysChanged.ClientID %>").val('true');

             });

             $("#<%=uoBtnUp.ClientID %>").click(function(ev) {
                 $("#<%=uoHiddenFieldDaysChanged.ClientID %>").val('true');
             });

             $("#<%=uoBtnDown.ClientID %>").click(function(ev) {
                 $("#<%=uoHiddenFieldDaysChanged.ClientID %>").val('true');
             });

             $("#<%=uoDropDownListRoomType.ClientID %>").change(function(ev) {
                 $("#<%=uoHiddenFieldRoomTypeChanged.ClientID %>").val('1');

             });


         }

         function SetVisibility() {
             if ($("#<%=uoChkAdvanceSearch.ClientID %>").attr('checked')) {
                 $("#<%=uoTableAdvanceSearch.ClientID %>").show();
             }
             else {
                 $("#<%=uoTableAdvanceSearch.ClientID %>").hide();
             }
         }

         function SetRoomAllocations() {
             if ($("#<%=uoChkAccredited.ClientID %>").attr('checked')) {
                 $("#<%=uoLblAllocations.ClientID %>").show();
                 $("#<%=uoRBtnAllocations.ClientID %>").show();
                 $("#<%=uoBtnCheckRooms.ClientID %>").show();
             }
             else {
                 $("#<%=uoLblAllocations.ClientID %>").hide();
                 $("#<%=uoRBtnAllocations.ClientID %>").hide();
                 $("#<%=uoBtnCheckRooms.ClientID %>").hide();
             }
         }

         function GetEvent() {
             var pDate = $("#<%=uoTxtBoxCheckIn.ClientID %>").val();
             var Branch = $("#<%=uoDropDownListBranch.ClientID %>").val();
             var City = $("#<%=uoHiddenFieldCity.ClientID %>").val();
             var sScript = '';
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "/PageMethods.aspx/GetEventCount",
                 data: "{'BranchId': '" + Branch + "', 'Date': '" + pDate + "'}",
                 dataType: "json",
                 success: function(data) {
                 if (data.d) {
                        $("#<%=uoLinkButtonEvent.ClientID %>").show();
                         $('a').show();
                        
                         sScript = 'return OpenEventsList(\'' + Branch + '\',\'' + City + '\',\'' + pDate + '\');';
                         $("#<%=uoLinkButtonEvent.ClientID %>").attr('onclick', sScript);
                         $("#<%=uoLinkButtonEvent.ClientID %>").text(data.d + ' Event/s.');
                     }
                     else {
                         $("#<%=uoLinkButtonEvent.ClientID %>").hide();
                         $('a').hide();
                        
                     }

                 }
                        ,
                 error: function(objXMLHttpRequest, textStatus, errorThrown) {
                     alert(errorThrown);
                 }
             });

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
                     $("#<%=uoTxtBoxCheckIn.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                     return false;
                 }
                 else {
                     $("#<%=uoHiddenFieldDateChanged.ClientID %>").val(1);
                     GetEvent();
                 }

             }
             else {
                 alert("Invalid date!");
                 $("#<%=uoTxtBoxCheckIn.ClientID %>").val(currentDate2);

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

         function ValidateTime(pTime) {
             try {
                 var timePat = /^(\d{1,2}):(\d{2})(:(\d{2}))?$/;

                 var matchArray = pTime.match(timePat);
                 if (matchArray == null) {
                     alert("Time is not in a valid format.");
                     return false;
                 }

                 var hour = matchArray[1];
                 var min = matchArray[2];

                 if (hour > 23) {
                     $("#<%=uoTxtBoxTime.ClientID %>").val('00:00');
                     alert("Invalid Time.");
                     return false;
                 }

                 if (min > 59) {
                     $("#<%=uoTxtBoxTime.ClientID %>").val('00:00');
                     alert("Invalid Time.");
                     return false;
                 }
                 return true;
             }
             catch (err) {
                 return false;
             }
         }



        function popobjed(url)
        {
            var newwindow;
            newwindow=window.open(url,'name','toolbar=0,scroll bars=1,location=0,statusbar=0,menubar=0,resizable= 0,width=500,height=520,left= 100,top = 100');
            if (window.focus) { newwindow.focus() }
            
        }        
        
    </script>
    
    <table >
        <tr class="PageTitle">
            <td class="LeftClass" colspan="4">Hotel and Property</td>
        </tr>
        <tr>
            <td colspan="4">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="LeftClass" colspan="4">
                <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Save" />
                <%--<asp:RangeValidator ID="uoRangeValidatorTxtBoxDays" Runat="Server"
                ControlToValidate="uoTxtBoxDays"
                Type="Integer"
                MinimumValue="0"
                MaximumValue="100"
                ErrorMessage="Please enter no. of days in the range 0 to 100"
                Display="Dynamic" 
                SetFocusOnError="True"
                ValidationGroup="Save"/>--%>
            </td>
        </tr>
        <%--<tr>
            <td class="LeftClass" style="width:100px; vertical-align:top"></td>
        </tr>--%>
        <tr>
            <td class="LeftClass" style="width:100px; vertical-align:top">
                Seafarer Name :
            </td>
            <td class="LeftClass" colspan="3">
                <asp:UpdatePanel runat="server" ID="uoPanelSeafarer" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Repeater runat="server" ID="uoRepeaterSFName">
                            <ItemTemplate>
                               <asp:Label runat="server" ID="uoLabel" Text='<%# Container.DataItem %>' BorderStyle="None"
                                    CssClass="ReadOnly" Font-Size="Medium"
                                    Width="70%"></asp:Label>
                                    <br />
                            </ItemTemplate>
                            <AlternatingItemTemplate >                                
                                <asp:Label runat="server" Text='<%# Container.DataItem %>' BorderStyle="None"
                                    Font-Size="Medium"
                                    Width="70%" BackColor="#FDFAE1"></asp:Label>
                                <br />
                            </AlternatingItemTemplate>                            
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>                
            </td>
        </tr>
        <tr>
            <td colspan="4"></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:UpdatePanel runat="server" ID="uoPanelAdvanceSearch" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:CheckBox runat="server" ID="uoChkAdvanceSearch" Text="Filter Hotel" onclick="return SetVisibility();" Visible="false"/>
                        <table runat="server" id="uoTableAdvanceSearch">
                            <%--<tr>
                                <td class="LeftClass" style="width:100px;">
                                    Region :
                                </td>
                                <td class="LeftClass">
                                    <asp:DropDownList runat="server" ID="uoDropDownListRegion" Width="300px"
                                        AppendDataBoundItems="true" AutoPostBack="true" 
                                        onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="LeftClass" style="width:100px;">
                                    Country :
                                </td>
                                <td class="LeftClass">
                                    <asp:DropDownList runat="server" ID="uoDropDownListCountry" Width="300px"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Selected="True" Text="--Select Country--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass" style="width:100px;">
                                    City Name :
                                </td>
                                <td class="LeftClass">
                                    <asp:TextBox runat="server" ID="uoTxtBoxCityName" Width="210px"></asp:TextBox> &nbsp;
                                    <asp:Button runat="server" ID="uoBtnSearchCity" Text="Search City"
                                        CssClass="SmallButton" onclick="uoBtnSearchCity_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass" style="width:100px;">
                                    City :
                                </td>
                                <td class="LeftClass">
                                    <asp:DropDownList runat="server" ID="uoDropDownListCity" Width="300px"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Selected="True" Text="--Select City--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="LeftClass" style="width:100px;">
                                    Hotel Name :
                                </td>
                                <td class="LeftClass">
                                    <asp:TextBox runat="server" ID="uoTxtBoxHotelName" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="LeftClass">
                                    <asp:CheckBox runat="server" ID="uoChkAccredited" Checked ="true" 
                                        Text="Is Accredited" onclick="return SetRoomAllocations();"
                                         
                                        />
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass" style="width:100px;">
                                    
                                </td>
                                <td class="LeftClass">
                                    <asp:Button runat="server" ID="uoButtonFilter" Text="Filter" 
                                        CssClass="SmallButton" onclick="uoButtonFilter_Click" /> &nbsp;
                                    <asp:Button runat="server" ID="uoButtonClear" Text="Clear Filter" 
                                        CssClass="SmallButton" Enabled="true" onclick="uoButtonClear_Click"/> &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
                <td class="LeftClass" style="width:100px;">
                    Region :
                </td>
                <td class="LeftClass">
                    <asp:DropDownList runat="server" ID="uoDropDownListRegion" Width="300px"
                        AppendDataBoundItems="true" AutoPostBack="true" 
                        onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
        </tr>
        <tr><td colspan="4"></td></tr>
        <tr>
            <td class="LeftClass">Hotel Branch :</td>
            <td class="LeftClass" style="width:400px;">
                <asp:UpdatePanel runat="server" ID="uoPanelBranch" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="uoDropDownListBranch" Width="300px" AutoPostBack="true"
                            AppendDataBoundItems="true" 
                            onselectedindexchanged="uoDropDownListBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvBranch" ValidationGroup="Save"
                            ErrorMessage="Hotel Branch required." InitialValue="0" 
                            ControlToValidate="uoDropDownListBranch">*</asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoButtonFilter" 
                            EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="LeftClass" style="width:100px;">
                Hotel Status :      
            </td>
            <td class="LeftClass" style="width:400px;">
                <asp:UpdatePanel runat="server" ID="uoPanelStatus" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="uoDropDownListStatus" Width="300px">
                            <asp:ListItem Selected="True" Text="UNUSED" Value="Unused"></asp:ListItem>
                            <asp:ListItem Text="CHECKED IN" Value="Checked In"></asp:ListItem>
                            <asp:ListItem Text="CHECKED OUT" Value="Checked Out"></asp:ListItem>
                            <asp:ListItem Text="CANCELLED" Value="Cancelled"></asp:ListItem>
                            <asp:ListItem Text="NO SHOW" Value="No Show"></asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>  
            </td>
        </tr>
       
        <tr>
            <td></td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoPanelContract" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button CssClass="SmallButton" ID="uoLinkButtonContract" Enabled="false" 
                            runat="server" Text="View Contract" Height="21px"></asp:Button>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">Room Type :</td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoPanelRoom" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="uoDropDownListRoomType" Width="300px">
                            <asp:ListItem Selected="True" Text="Single" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Double" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="LeftClass">Confirmation No. :</td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoPanelConfirmation" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox runat="server" ID="uoTxtBoxConfirmation" Width="300px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">Check In Date :</td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoPanelCheckIn" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox runat="server" ID="uoTxtBoxCheckIn" Width="300px" Height="22px"></asp:TextBox>
                        <cc1:textboxwatermarkextender ID="uoTxtBoxCheckIn_TextBoxWatermarkExtender" runat="server"
                            Enabled="True" TargetControlID="uoTxtBoxCheckIn" WatermarkCssClass="fieldWatermark"
                            WatermarkText="MM/dd/yyyy">
                        </cc1:textboxwatermarkextender>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                        <cc1:calendarextender ID="uoTxtBoxCheckIn_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="uoTxtBoxCheckIn" PopupButtonID="ImageButton1" Format="MM/dd/yyyy" 
                            OnClientDateSelectionChanged="CheckIndateChange">
                        </cc1:calendarextender >
                        <cc1:maskededitextender ID="uoTxtBoxCheckIn_MaskedEditExtender" runat="server"
                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                            CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTxtBoxCheckIn"
                            UserDateFormat="MonthDayYear">
                        </cc1:maskededitextender>
                        <asp:RequiredFieldValidator runat="server" ID="rfvCheckIn" ValidationGroup="Save"
                            ErrorMessage="Check In date required." ControlToValidate="uoTxtBoxCheckIn">*</asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="LeftClass">Time :</td>
            <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTxtBoxTime" Width="300px" Text ="00:00"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="uoTxtBoxTime_TextBoxWatermarkExtender"
                    runat="server" Enabled="True" TargetControlID="uoTxtBoxTime" WatermarkCssClass="fieldWatermark" WatermarkText="HH:mm">
                </cc1:TextBoxWatermarkExtender>
                <cc1:MaskedEditExtender ID="uoMaskedEditExtenderCheckInTime" runat="server" CultureAMPMPlaceholder=""
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                    Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTxtBoxTime"
                    UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                </cc1:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">No of Days :</td>
            <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTxtBoxDays" Text="1" Width="100px" 
                    Height="25px" MaxLength=3 onkeyup="validateDays();" 
                    onkeypress="return validate(event);" 
                   ></asp:TextBox>
                <cc1:NumericUpDownExtender ID="uoTxtBoxDays_NumericUpDownExtender" runat="server"
                    Enabled="True" Maximum="100" Minimum="1" RefValues="" ServiceDownMethod="" ServiceDownPath=""
                    ServiceUpMethod="" Tag="" TargetButtonDownID="uoBtnDown" TargetButtonUpID="uoBtnUp" TargetControlID="uoTxtBoxDays"
                    Width="50">
                </cc1:NumericUpDownExtender>                
                <table style="padding: inherit; margin: auto; border: 1px solid #000000; position: absolute; table-layout: fixed; width: 20px; z-index: auto; display: inline; background-color: #ECE9D8; border-collapse: separate;">
                    <tr>
                        <td>
                            
                        <asp:ImageButton ImageUrl="~/Images/btnUp.JPG" runat="server" ID="uoBtnUp" Height="8px" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ImageUrl="~/Images/btnDown.JPG" runat="server" ID="uoBtnDown" Height="8px" />
                        </td>
                    </tr>
                </table>                   
            </td>
            <td class="LeftClass">
                With Shuttle :
            </td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoPanelShuttle" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:CheckBox runat="server" ID="uoChkWithShuttle" Text="Transportation" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                Currency :
            </td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoPanelCurrency" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox runat="server" ID="uoTxtBoxCurrency" CssClass="ReadOnly"
                            Width="300px" ReadOnly="true"></asp:TextBox></ContentTemplate><Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </td>
            
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">   
                    <ContentTemplate>
                        <a runat="server" id="aEvent">
                            <asp:LinkButton ID="uoLinkButtonEvent" runat="server" Width="300px" 
                                ForeColor="Red" Text=""></asp:LinkButton>
                        </a>
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" 
                            EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="LeftClass" colspan="4">
                <asp:UpdatePanel runat="server" ID="uoPanelAllocationsTitle" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label runat="server" id="uoLblAllocations" Text="Room Allocations"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoChkAccredited" 
                            EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </td>
        </tr>
        <tr>
            <td></td>
            <td class="LeftClass" colspan ="3">
                <asp:UpdatePanel runat="server" ID="uoPanelAllocations" UpdateMode="Conditional">
                   <ContentTemplate>
                        <asp:RadioButtonList runat="server" ID="uoRBtnAllocations" 
                            RepeatDirection="Horizontal"
                            >
                            <asp:ListItem Selected="True" Text="Contract/Override" Value="0"></asp:ListItem>
                            <%--<asp:ListItem Text="Override" Value="1"></asp:ListItem>--%>
                            <asp:ListItem Text="Emergency" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvAllocations"
                            ControlToValidate="uoRBtnAllocations" ValidationGroup="Save"
                            ErrorMessage="Room Allocation Required.">*</asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoChkAccredited" 
                            EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td></td>
            <td class="LeftClass" colspan ="3">
               <asp:UpdatePanel runat="server" ID="uoPanelCheckAllocations" UpdateMode="Conditional">
                    <ContentTemplate>
                         <asp:Button runat="server" ID="uoBtnCheckRooms" Text="Check Room Blocks" Enabled="false"
                            CssClass="SmallButton" onclick="uoBtnCheckRooms_Click" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoChkAccredited" 
                            EventName="CheckedChanged" />
                    </Triggers>
               </asp:UpdatePanel>
            </td>
        </tr>
        <tr runat="server" class="Module">
            <td></td>
            <td colspan ="3" class="Module">
                <asp:UpdatePanel runat="server" ID="uoPanelRoomCount" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ListView runat="server" ID="uoListRoomBlocks">
                            <LayoutTemplate>
                                <table cellpadding="0" cellspacing="0" border="0" class="listViewTable">
                                    <tr>
                                        <th>Date</th>
                                        <th>Number of Seafarers</th>
                                        <th>Remaining Rooms</th>
                                        <th runat="server" id="EditTH"/>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr style='<%# Valid()%>'>
                                    <td><%#String.Format("{0:dd-MMM-yyy}", Eval("coldate")) %></td>
                                    <td><%#Eval("colNumOfPeople") %></td>
                                    <td><%#Eval("remaining") %></td>
                                    <td class='<%# (HideEdit()==""?"leftAligned":HideEdit()) %>'>
                                         <a id="HyperLink" style="color:blue;" onclick="popobjed('<%# "/HotelRoomOverrideEdit2.aspx?bID=" 
                                                    + Eval("BranchId") + "&rID=" + Eval("RoomTypeId") + "&dt=" 
                                                    + String.Format("{0:MM-dd-yyyy}", Eval("coldate")) + "&rc=" 
                                                    + String.Format("{0:#####}", Eval("ReservedOverride"))
                                                    + "&pFrom=HotelEditor"
                                                     %>');">
                                                    Add
                                                    </a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                       </asp:ListView>
                   </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoBtnCheckRooms" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="uoRBtnAllocations" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoChkAccredited" 
                            EventName="CheckedChanged" />
                    </Triggers>
               </asp:UpdatePanel>
           </td>
       </tr>
       <tr>
            <td class="LeftClass">Remarks :</td><td class="LeftClass" colspan="3">
                <asp:TextBox runat="server" ID="uoTxtBoxRemarks" Width="70%" Height="50px" 
                    TextMode="MultiLine" ></asp:TextBox></td></tr><tr>
            <td colspan="4"></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="3" class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoPanelSave" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button runat="server" ID="uoButtonSave" ValidationGroup="Save" Text="Save" 
                                onclick="uoButtonSave_Click" Enabled="false" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListBranch" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
    <asp:HiddenField runat="server" ID="uoHiddenFieldValid" Value="true" />
   <asp:HiddenField runat="server" ID="uoHiddenFieldDaysChanged" Value="false" />
   <asp:HiddenField runat="server" ID="uoHidenFieldDays" Value="1" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDateChanged" Value ="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRoomTypeChanged" Value ="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldCity" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRoomChecked" Value="false" />
    
     <asp:HiddenField runat="server" ID="uoHiddenFieldOverride" Value="0" />
    
</asp:Content>
