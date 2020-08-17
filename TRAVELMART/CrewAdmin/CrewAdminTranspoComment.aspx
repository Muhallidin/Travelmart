<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CrewAdminTranspoComment.aspx.cs" Inherits="TRAVELMART.CrewAdminTranspoComment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script> 
    <style type="text/css">
        .style1
        {
            width: 548px;
        }
        .style2
        {
            width: 107px;
        }
        .style3
        {
            width: 79px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                pageSettings();                
            }
        }

        $(document).ready(function() {
            pageSettings();
            setupParentPage();
        });

        function setupParentPage()
        {
            window.parent.$("#ctl00_NaviPlaceHolder_uoHiddenFieldTransportation").val('1');            
        }
        function pageSettings()
        {
            $("#<%=uoTextBoxPickupDate.ClientID %>").change(function(ev) {
                ValidateIfPastDate($(this).val());

            });

            $("#<%=uoTextBoxPickupTime.ClientID %>").change(function(ev) {
                ValidateTime($(this).val());

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
                    $("#<%=uoTextBoxPickupDate.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                    return false;
                }
                else {
                    $("#<%=uoHiddenFieldDateChanged.ClientID %>").val(1);
//                    GetEvent();
                }

            }
            else {
                alert("Invalid date!");
                $("#<%=uoTextBoxPickupDate.ClientID %>").val(currentDate2);

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
                    $("#<%=uoTextBoxPickupTime.ClientID %>").val('00:00');
                    alert("Invalid Time.");
                    return false;
                }

                if (min > 59) {
                    $("#<%=uoTextBoxPickupTime.ClientID %>").val('00:00');
                    alert("Invalid Time.");
                    return false;
                }
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
       
    </script>
    <div class="PageTitle">
             Add/Cancel Transportation
    </div>
    <hr/> 
    <div>
        <table width="100%">
            <tr>
                <td class="style2">
                    Vehicle Vendor:
                </td>
                <td class="style1">
                    <asp:TextBox runat="server" ID="uoTextBoxVehicleVendor" Width="500px" CssClass="ReadOnly" ReadOnly="true" ></asp:TextBox>
                </td>
                <td class="style3"></td>
                <td></td>
                
            </tr>
            <tr>
                <td class="style2">
                    Email To:
                </td>
                <td class="style1">
                    <asp:TextBox runat="server" ID="uoTextBoxEmailAdd" Width="500px"
                     onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                     ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                    ></asp:TextBox>
                </td>
                <td class="style3">
                    Pickup Date:
                </td>
                <td style="white-space:nowrap">
                    <asp:TextBox runat="server" ID="uoTextBoxPickupDate" Width="100px"></asp:TextBox>
                    <cc1:textboxwatermarkextender ID="uoTextBoxPickupDate_TextBoxWatermarkExtender" runat="server"
                            Enabled="True" TargetControlID="uoTextBoxPickupDate" WatermarkCssClass="fieldWatermark"
                            WatermarkText="MM/dd/yyyy">
                        </cc1:textboxwatermarkextender>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                        <cc1:calendarextender ID="uoTextBoxPickupDate_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="uoTextBoxPickupDate" PopupButtonID="ImageButton1" Format="MM/dd/yyyy" 
                            OnClientDateSelectionChanged="CheckIndateChange">
                        </cc1:calendarextender >
                        <cc1:maskededitextender ID="uoTextBoxPickupDate_MaskedEditExtender" runat="server"
                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                            CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxPickupDate"
                            UserDateFormat="MonthDayYear">
                        </cc1:maskededitextender>
                        <%--<asp:RequiredFieldValidator runat="server" ID="rfvCheckIn" ValidationGroup="Email"
                            ErrorMessage="Pickup date required." ControlToValidate="uoTextBoxPickupDate">*</asp:RequiredFieldValidator>--%>
                </td>                
            </tr>
            <tr>
                <td class="style2">
                    Email Cc:
                </td>
                <td class="style1">
                    <asp:TextBox runat="server" ID="uoTextBoxCopy" Width="500px"
                     onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                    ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                    ></asp:TextBox>
                </td>
                <td class="style3">
                    Pickup Time:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="uoTextBoxPickupTime" Width="100px"></asp:TextBox>
                     <cc1:textboxwatermarkextender ID="uoTextBoxPickupTime_TextBoxWatermarkExtender"
                    runat="server" Enabled="True" TargetControlID="uoTextBoxPickupTime" 
                        WatermarkCssClass="fieldWatermark" WatermarkText="HH:mm">
                    </cc1:textboxwatermarkextender>
                    <cc1:maskededitextender ID="uoMaskedEditExtenderCheckInTime" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxPickupTime"
                        UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                    </cc1:maskededitextender>
                </td>
            </tr>
             <tr>
                <td class="style2">
                    Comment:
                </td>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="uoTextBoxComment" Width="500px" TextMode="MultiLine" ></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="style2">
                   
                </td>
                <td colspan="3">
                    <asp:Button runat="server" ID="uoButtonEmail" Text="Book/Cancel Transportation & Email"  ValidationGroup="Email"
                        CssClass="SmallButton" onclick="uoButtonEmail_Click"/>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <asp:Label ID="Label5"  CssClass="RedNotification" runat="server" 
                    Text="Multiple emails should be separated by semicolon (i.e.  abc@rccl.com;xyz@rccl.com)"> 
                    </asp:Label> 
                </td>
            </tr>
        </table>
    </div>
    <div id="Div2" class="PageSubTitle" style="text-decoration: underline;">
        New Transportation Request
    </div>
    <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;
        position: relative;">
        <asp:ListView runat="server" ID="ListView1">
            <LayoutTemplate>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <th class="hideElement">
                            requestInfo
                        </th>                      
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="Label13" Text="E1 ID" Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablE1Hdr" Text="Last Name" Width="112px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablRecLocHdr" Text="First Name" Width="112px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablEmpIdHdr" Text="Route From" Width="70px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablNameHdr" Text="Route To" Width="70px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="Label1" Text="Rec Loc" Width="60px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablStatHdr" Text="Flight No." Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablOnOffHdr" Text="Dep" Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablGenderHdr" Text="Arr" Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablRankHdr" Text="Dep Date" Width="60px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablStripeHdr" Text="Arr Date" Width="55px"> </asp:Label>
                        </th>                        
                        <th>
                            <asp:Label runat="server" ID="Label11" Text="" Width="10px"> </asp:Label>
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;
        position: relative;" onscroll="divScrollL();">
        <asp:ListView runat="server" ID="uoListViewAdded">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList" width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <%# OverflowChangeRowColor()%>
                <td class="hideElement">                                
                    <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IDBigint") %>' />
                    <asp:HiddenField runat="server" ID="hfTransID" Value='<%# Eval("TransID") %>' />                                        
                </td>                
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label14" Text='<%# Eval("E1ID") %>' Width="50px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label16" Text='<%# Eval("LastName") %>' Width="100px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblE1TrId" Text='<%# Eval("FirstName") %>' Width="100px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRouteFr" Text='<%# Eval("RouteFrom") %>' Width="60px"></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRouteTo" Width="60px" Text='<%# Eval("RouteTo")%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label2" Width="50px" Text='<%# Eval("RecLoc") %>'></asp:Label>                  
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblSeafarerName" Width="50px" Text='<%# Eval("FlightNo") %>'></asp:Label>                  
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblStatus" Width="50px" Text='<%# Eval("Departure") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label8" Width="50px" Text='<%# Eval("Arrival") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label12" Width="50px" Text='<%# Eval("DepartureDate") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label18" Width="50px" Text='<%# Eval("ArrivalDate") %>'></asp:Label>
                </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                    <tr>
                        <td colspan="20" class="leftAligned">
                            <asp:Label runat="server" ID="Label10" Text="No Record" > </asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    </div>
    
    <br />
   <div id="Div1" class="PageSubTitle" style="text-decoration: underline;">
        Transportation To Cancel
    </div>
    
    <%--Cancelled Start--%>
    <div id="AvCancel" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;
        position: relative;">
        <asp:ListView runat="server" ID="ListView2">
            <LayoutTemplate>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <th class="hideElement">
                            requestInfo
                        </th>                        
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="Label13" Text="E1 ID" Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablE1Hdr" Text="Last Name" Width="112px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablRecLocHdr" Text="First Name" Width="112px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablEmpIdHdr" Text="Route From" Width="70px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablNameHdr" Text="Route To" Width="70px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="Label1" Text="Rec Loc" Width="60px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablStatHdr" Text="Flight No." Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablOnOffHdr" Text="Dep" Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablGenderHdr" Text="Arr" Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablRankHdr" Text="Dep Date" Width="60px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablStripeHdr" Text="Arr Date" Width="55px"> </asp:Label>
                        </th>                        
                        <th>
                            <asp:Label runat="server" ID="Label11" Text="" Width="10px"> </asp:Label>
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div id="BvCancel" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;
        position: relative;" onscroll="divScrollL();">
        <asp:ListView runat="server" ID="uoListViewCancel">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList" width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <%# OverflowChangeRowColor()%>
                <td class="hideElement">                                
                    <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IDBigint") %>' />
                    <asp:HiddenField runat="server" ID="hfTransID" Value='<%# Eval("TransID") %>' />                                        
                </td>                
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label14" Text='<%# Eval("E1ID") %>' Width="50px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label16" Text='<%# Eval("LastName") %>' Width="100px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblE1TrId" Text='<%# Eval("FirstName") %>' Width="100px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRouteFr" Text='<%# Eval("RouteFrom") %>' Width="60px"></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRouteTo" Width="60px" Text='<%# Eval("RouteTo")%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label2" Width="50px" Text='<%# Eval("RecLoc") %>'></asp:Label>                  
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblSeafarerName" Width="50px" Text='<%# Eval("FlightNo") %>'></asp:Label>                  
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblStatus" Width="50px" Text='<%# Eval("Departure") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label8" Width="50px" Text='<%# Eval("Arrival") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label12" Width="50px" Text='<%# Eval("DepartureDate") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label18" Width="50px" Text='<%# Eval("ArrivalDate") %>'></asp:Label>
                </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                    <tr>
                        <td colspan="20" class="leftAligned">
                            <asp:Label runat="server" ID="Label10" Text="No Record" > </asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldDateChanged" Value ="0" />
    </div>
    <%--Cancelled End--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
