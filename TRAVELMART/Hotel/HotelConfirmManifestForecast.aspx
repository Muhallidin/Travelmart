<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    CodeBehind="HotelConfirmManifestForecast.aspx.cs" Inherits="TRAVELMART.Hotel.HotelConfirmManifestForecast" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="PageTitle">
            <td align="left">
                Confirm Manifest Forecast
            </td>
            <td align="right">
                <div runat="server" id="uoDivRegionPort">
                    Region: &nbsp;&nbsp;
                    <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="200px" AppendDataBoundItems="true"
                        AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp; Port: &nbsp;&nbsp;
                    <asp:DropDownList ID="uoDropDownListPort" runat="server" Width="200px" AppendDataBoundItems="true"
                        AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListPort_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">
        function CloseModal(strURL) {
            window.location = strURL;
        }

        function confirmEmail() {
            if (confirm("Email record?") == true)
                return true;
            else
                return false;
        }
        function confirmLock() {
            if ($("#<%=uoDropDownListHotel.ClientID %>").val() == "0") {
                alert("Select Hotel Branch");
                return false;
            }
            confirmEmail();
        }

    </script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetTMResolution();
            ShowPopup();
            ShowSearchPopup();
            ShowEmailPopup();
            SetClientTime();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTMResolution();
                ShowPopup();
                ShowSearchPopup();
                ShowEmailPopup();
                SetClientTime();
            }
        }

        function SetClientTime() {
            $(".BtnTag").click(function() {
                var d = new Date();
                //$("#<%=uoHiddenFieldTagTime.ClientID %>").val(d.format("yyyy-mm-dd hh:mm:ss"));
                $("#<%=uoHiddenFieldTagTime.ClientID %>").val(d.getMonth() + 1 + "/" + d.getDate() + "/" + d.getFullYear() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds());
            });
        }

        function SetTMResolution() {
            var ht = $(window).height() * 1.3;
            var ht2 = $(window).height() ;

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
                ht = ht * 0.335;
                ht2 = ht2 * 0.62  ;                
            }
          
            $("#ConfirmedB").height(ht)
            $("#ConfirmedA").width(wd);
            $("#ConfirmedB").width(wd);

            $("#CancelledB").height(ht)
            $("#CancelledA").width(wd);
            $("#CancelledB").width(wd);
        }
       
        function divScrollLConfirm() {
            var Right = document.getElementById('ConfirmedA');
            var Left = document.getElementById('ConfirmedB');
            Right.scrollLeft = Left.scrollLeft;
        }
        function divScrollLCancel() {
            var Right = document.getElementById('CancelledA');
            var Left = document.getElementById('CancelledB');
            Right.scrollLeft = Left.scrollLeft;
        }        

        function ShowEmailPopup() {           
            $(".Confirmation").fancybox(
            {
                'centerOnScroll': false,
                'width': '30%',
                'height': '27%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupConfirmation.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });
        }

        function SendEmail(send, saveSend) {
            $.fancybox.close();

            if (send == 1) {
                $("#<%=uoHiddenFieldSendEmail.ClientID %>").val("1");
            }
            if (saveSend == 1) {
                $("#<%=uoHiddenFieldSendSaveEmail.ClientID %>").val("1");
            }

            var txtTo = $("#<%=uoTextBoxTo.ClientID %>");
            var txtCc = $("#<%=uoTextBoxCc.ClientID %>");


            $("#<%=uoHiddenFieldTo.ClientID %>").val(txtTo.val());
            $("#<%=uoHiddenFieldCc.ClientID %>").val(txtCc.val());

            $("#aspnetForm").submit();
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

        function confirmSaveAndSend() {
            if (confirm("Saving will completely change your recipient and/or carbon copy list. Are you sure?") == true) {
                SendEmail(0, 1);
            }
            else {
                return false;
            }
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
        function confirmTag(e1ID, seafarerName) {
            var sMsg = "Tag " + e1ID + ": " + seafarerName + "? ";
            var x = confirm(sMsg);
            return x;
        }
        function CheckFromDate(d) {
            var txtFromDate = $("#<%=uoTextBoxFrom.ClientID %>");
            var txtToDate = $("#<%=uoTextBoxToDate.ClientID %>");
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
            $("#<%=uoHiddenFieldDateTo.ClientID %>").val(txtToDate.val()); 
            return true;
        }
        function CheckToDate(d) {
            var txtFromDate = $("#<%=uoTextBoxFrom.ClientID %>");
            var txtToDate = $("#<%=uoTextBoxToDate.ClientID %>");
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
            $("#<%=uoHiddenFieldDate.ClientID %>").val(txtFromDate.val());
            $("#<%=uoHiddenFieldDateTo.ClientID %>").val(txtToDate.val()); 
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

    <div id="PG" style="width: auto; height: auto; overflow: auto;">
        <%--<asp:UpdatePanel runat="server" ID="uoUpdatePanel">
            <ContentTemplate>--%>
        <div runat="server" id="uoDivHotelSpecialist">
            <table width="100%" class="LeftClass">
                <tr>
                    <td class="contentCaption">
                        Hotel Branch:
                    </td>
                    <td class="contentValue">
                        <asp:DropDownList ID="uoDropDownListHotel" runat="server" Width="300px" AppendDataBoundItems="True"
                            OnSelectedIndexChanged="uoDropDownListHotel_SelectedIndexChanged">
                            <asp:ListItem Value="0">--SELECT HOTEL--</asp:ListItem>
                        </asp:DropDownList>
                        <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListBranch" runat="server"
                            TargetControlID="uoDropDownListHotel" PromptText="Type to search" PromptPosition="Top"
                            IsSorted="true" PromptCssClass="dropdownSearch" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoDropDownListHotel"
                            Enabled="False" ErrorMessage="Required Hotel" InitialValue="0">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                        <td class="contentCaption">
                            Date Range:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox ID="uoTextBoxFrom" runat="server" onchange="return CheckFromDate(this);" Width="120px"></asp:TextBox>
                             <cc1:textboxwatermarkextender ID="uoTextBoxFrom_TextBoxWatermarkExtender" runat="server"
                                Enabled="True" TargetControlID="uoTextBoxFrom" WatermarkCssClass="fieldWatermark"
                                WatermarkText="MM/dd/yyyy">
                            </cc1:textboxwatermarkextender>
                            <cc1:calendarextender ID="uoTextBoxFrom_CalendarExtender" runat="server" Enabled="True"
                                TargetControlID="uoTextBoxFrom"  Format="MM/dd/yyyy">
                            </cc1:calendarextender >
                            <cc1:maskededitextender ID="uoTextBoxFrom_MaskedEditExtender" runat="server"
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxFrom"
                                UserDateFormat="MonthDayYear">
                            </cc1:maskededitextender>
                            &nbsp
                            To: 
                             &nbsp;<asp:TextBox ID="uoTextBoxToDate" runat="server" onchange="return CheckToDate(this);" Width="120px"></asp:TextBox>
                            <cc1:textboxwatermarkextender ID="Textboxwatermarkextender1" runat="server"
                                Enabled="True" TargetControlID="uoTextBoxToDate" WatermarkCssClass="fieldWatermark"
                                WatermarkText="MM/dd/yyyy">
                            </cc1:textboxwatermarkextender>
                            <cc1:calendarextender ID="Calendarextender1" runat="server" Enabled="True"
                                TargetControlID="uoTextBoxToDate"  Format="MM/dd/yyyy">
                            </cc1:calendarextender >
                            <cc1:maskededitextender ID="Maskededitextender1" runat="server"
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxToDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:maskededitextender>
                        </td>
                        <td >
                            <asp:Button ID="uoBtnView" runat="server" CssClass="SmallButton" Text="View" Width="100px" OnClick="uoBtnView_Click" />
                        </td>                       
                    </tr>
            </table>
        </div>        
        <table width="100%" class="LeftClass">
            <tr>
                <td class="contentCaption">
                    <asp:Button ID="uoBtnExportList" runat="server" CssClass="SmallButton" Text="Export Confirmed Manifest"
                        OnClick="uoBtnExportList_Click" />
                </td>
                <td class="LeftClass">                    
                   <%-- <a id="uoHyperLinkSendEmails" runat="server" href="#uoDivEmail">Confirm & E-mail</a>
                    <asp:Button ID="uoButtonConfirmByVendor" CssClass="SmallButton" runat="server" Text="Confirm Manifest"
                        OnClick="uoButtonConfirmByVendor_Click" />--%>
                </td>
            </tr>
        </table>
        <span style="font-weight: bold; font-size: larger;">            
        <%--===================================CONFIRMED==================================================== --%>
        <br />
        <div runat="server" id="PendingDiv" class="PageSubTitle" style="text-decoration: underline;">
            Confirmed Manifest
        </div>
        <div id="ConfirmedA" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
            <asp:ListView runat="server" ID="uoListViewHeaderConfirmed" 
                OnItemCommand="uoListViewHeaderConfirmed_ItemCommand" >
                <LayoutTemplate>
                </LayoutTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="listViewTable">
                        <tr>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblRemarksHeader" runat="server" CommandName="SortByRemarks"
                                    Text="Remarks" Width="45px" />
                            </th>
                            <th runat="server" id="TagTH">
                                <asp:Label runat="server" ID="Label7" Text="Tagged" Width="50px" ></asp:Label>
                            </th>
                            <th>
                                <asp:LinkButton Width="90px" ID="uoLinkButtonConfirmation" runat="server" CommandName="SortByConfirmation"
                                    Text="Confirmation #" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCityHeader" runat="server" CommandName="SortByHotelCity"
                                    Text="Hotel City" Width="43px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCheckInHeader" runat="server" CommandName="SortByCheckIn"
                                    Text="Check-In" Width="64px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCheckOutHeader" runat="server" CommandName="SortByCheckOut"
                                    Text="Check-Out" Width="64px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblNitesHeader" runat="server" CommandName="SortByHotelNites"
                                    Text="Hotel Nights" Width="34px" />
                            </th>
                            <%-- <th style="text-align: center; white-space: normal;" runat="server" >
                                    <asp:LinkButton ID="uoLblReasonHeader" runat="server" CommandName="SortByReason" Text="Reason" Width="44px"  />
                                </th> --%>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblLastNameHeader" runat="server" CommandName="SortByLastName"
                                    Text="Last Name" Width="145px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblFirstNameHeader" runat="server" CommandName="SortByFirstName"
                                    Text="First Name" Width="144px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblEmpIDHeader" runat="server" CommandName="SortByEmployeeID"
                                    Text="Employee ID" Width="54px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblGenderHeader" runat="server" CommandName="SortByGender"
                                    Text="Gender" Width="45px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="SortBySingleDouble"
                                    Text="Single/Double" Width="74px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCoupleHeader" runat="server" CommandName="SortByCouple"
                                    Text="Couple" Width="39px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblTitleHeader" runat="server" CommandName="SortByTitle" Text="Title"
                                    Width="215px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblShipHeader" runat="server" CommandName="SortByShip" Text="Ship"
                                    Width="164px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="SortByCost" Text="Cost Center"
                                    Width="164px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblNationalityHeader" runat="server" CommandName="SortByNationality"
                                    Text="Nationality" Width="195px" />
                            </th>
                            <%--<th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLblHRHeader" runat="server" CommandName="SortByHotelRequest" Text="Hotel Request" Width="52px" />
                                </th>--%>                           
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblRLHeader" runat="server" CommandName="SortByRecordLocator"
                                    Text="Record Locator" Width="55px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblDCityHeader" runat="server" CommandName="SortByDeptCity"
                                    Text="Dept City" Width="45px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblACityHeader" runat="server" CommandName="SortByArvlCity"
                                    Text="Arvl City" Width="43px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByDeptDate" Text="Dept Date"
                                    Width="60px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByArvlDate" Text="Arvl Date"
                                    Width="60px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblDTimeHeader" runat="server" CommandName="SortByDeptTime"
                                    Text="Dept Time" Width="53px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblATimeHeader" runat="server" CommandName="SortByArvlTime"
                                    Text="Arvl Time" Width="53px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCarrierHeader" runat="server" CommandName="SortByCarrier"
                                    Text="Carrier" Width="35px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblFlightNumHeader" runat="server" CommandName="SortByFlightNo"
                                    Text="Flight No." Width="44px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblVoucherHeader" runat="server" CommandName="SortByVoucher"
                                    Text="Voucher" Width="44px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblPNumHeader" runat="server" CommandName="SortByPassportNo"
                                    Text="Passport No." Width="57px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblIssuedHeader" runat="server" CommandName="SortByDateIssued"
                                    Text="Date Issued" Width="60px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblExpHeader" runat="server" CommandName="SortByPassportExpiration"
                                    Text="Passport Expiration" Width="59px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton33" Text="Birthday" Width="70px" runat="server" CommandName="SortByBirthday" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblBookingRemarksHeader" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Booking Remarks" Width="150px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByConfirmedBy" Text="Confirmed By"
                                    Width="80px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="SortByConfirmedDate"
                                    Text="Confirmed Date" Width="120px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label2" Text="" Width="25px"></asp:Label>
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
        <div id="ConfirmedB" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
            onscroll="divScrollLConfirm();">
            <asp:ListView runat="server" ID="uoListViewManifestConfirmed" DataSourceID="uoObjectDataSourceConfirmed"
                OnItemCommand="uoListViewManifestConfirmed_ItemCommand" 
                onpagepropertieschanging="uoListViewManifestConfirmed_PagePropertiesChanging" >
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%# HotelAddGroup()%>
                    <tr>
                        <td class="leftAligned" style="white-space: normal;">
                             <asp:HiddenField ID="uoHiddenFieldStatus" runat="server" Value='<%# Eval("Status") %>'/>
                            <asp:Label runat="server" ID="Label4" Text='<%# Eval("Remarks")%>' Width="50px"></asp:Label>
                           <%-- <asp:Label runat="server" ID="Label4" Text='<%# Convert.ToString(Eval("IsTagLinkVisible")) %>' Width="50px"></asp:Label>--%>
                        </td>
                        <td runat="server" id= "TagTD" Visible='<%# Convert.ToBoolean(Eval("IsHotelVendor")) %>'>
                            <asp:Label ID="Label26" Width="50px" runat="server" Text="Tagged" Visible='<%# Convert.ToBoolean(Eval("IsTaggedByUser")) %>'></asp:Label>
                            <asp:LinkButton Width="54px" ID="uoLnkBtnTag" CommandName="Tag" CssClass="BtnTag"  Visible='<%# Convert.ToBoolean(Eval("IsTagLinkVisible")) %>'
                                OnClientClick='<%# "javascript:return confirmTag("+ Eval("EmployeeID") + ", \"" + Eval("FirstName") + "\");" %>'
                                CommandArgument='<%# Eval("IDBigInt") + ":" + Eval("TravelReqIdInt") + ":" + uoDropDownListHotel.SelectedValue %>'
                                runat="server" Text="Tag"> </asp:LinkButton>
                        </td>
                        <td class="centerAligned">
                            <div>
                                <asp:HyperLink ID="uoHyperLinkConfirm" runat="server" class="Confirmation" Visible='<%# Convert.ToBoolean(Eval("IsHotelVendor")) %>'                               
                                    Width="95px" NavigateUrl='<%# "AddConfirmation.aspx?BigID=" + Eval("IDBigInt") + " &TravelID=" + Eval("TravelReqIdInt") + " &BranchID=" + uoDropDownListHotel.SelectedValue + " &tType=" + Eval("ConfirmationNo") %>'
                                    Text=' <%# Convert.ToString(Eval("ConfirmationNo")).Length > 0 ? Eval("ConfirmationNo") : "Add"%>  '> </asp:HyperLink>
                                <asp:Label ID="ucLabelConfirmation" Width="95px" runat="server" Text='<%# Eval("ConfirmationNo") %>'
                                    Visible='<%# !Convert.ToBoolean(Eval("IsHotelVendor")) %>'></asp:Label>
                            </div>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label ID="Label1" runat="server" Width="50px" Text='<%# Eval("HotelCity") %>'></asp:Label>
                            <asp:Label ID="uoLabelWithEvent" runat="server" Text="*" Visible='<%# Eval("WithEvent") %>'
                                ForeColor="red" Font-Bold="true" Font-Size="Large"></asp:Label>
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
                            <asp:Label runat="server" ID="uoLblNites" Text='<%# Eval("HotelNights")%>' Width="40px"></asp:Label>
                        </td>
                        <%--<td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblReason" Text='<%# Eval("ReasonCode")%>' Width="50px"  ></asp:Label>                                        
                            </td>--%>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLabelLast" Text='<%# Eval("LastName")%>' Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:LinkButton Width="150px" runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("EmployeeId") + "&trID=" + Eval("TravelReqIdInt") + "&st=" + Eval("Status") + "&recloc=" + Eval("RecLoc") + "&manualReqID=" + Eval("RequestID") + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IDBigInt") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("FirstName") %></asp:LinkButton>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("EmployeeId")%>' Width="63px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblGender" Text='<%# Eval("Gender")%>' Width="50px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblRoom" Text='<%# Eval("SingleDouble")%>' Width="80px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblCouple" Text='<%# Eval("Couple")%>' Width="45px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblRank" Text='<%# Eval("Title")%>' Width="220px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblVessel" Text='<%# Eval("ShipCode") + " - " + Eval("ShipName")%>'
                                Width="170px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblCostCenter" Text='<%# Eval("CostCenterCode") + " - " + Eval("CostCenter")%>'
                                Width="170px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblNationality" Text='<%# Eval("Nationality")%>'
                                Width="200px"></asp:Label>
                        </td>
                        <%-- <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblHRequest" Text='<%# Eval("HotelRequest")%>' Width="60px"  ></asp:Label>
                            </td>--%>
                        
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecLoc")%>' Width="62px"></asp:Label>
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
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblPNum" Text='<%# Eval("PassportNo")%>' Width="65px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblIssuedBy" Text='<%# Eval("PasportDateIssued")%>'
                                Width="65px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblExp" Text='<%# Eval("PassportExpiration")%>' Width="65px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label7" Width="76px" Text='<%#String.Format("{0:dd-MMM-yyyy}", Eval("Birthday"))%>'></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label3" Text='<%# Eval("BookingRemarks") %>' Width="155px"></asp:Label>
                        </td>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label5" Text='<%# Eval("ConfirmedBy") %>' Width="87px"></asp:Label>
                        </td>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label6" Text='<%# Eval("ConfirmedDate") %>' Width="120px"></asp:Label>
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
                <asp:DataPager ID="uoDataPagerConfirmed" runat="server" PagedControlID="uoListViewManifestConfirmed"
                    PageSize="20">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </div>
        </div>
       <%--===================================CONFIRMED==================================================== --%>
       <%--===================================CANCELLED==================================================== --%>
        <br />
        <div runat="server" id="Div1" class="PageSubTitle" style="text-decoration: underline;">
            Cancelled Manifest
        </div>
        <div id="CancelledA" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
            <asp:ListView runat="server" ID="uoListViewHeaderCancelled" OnItemCommand="uoListViewHeaderConfirmed_ItemCommand">
                <LayoutTemplate>
                </LayoutTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="listViewTable">
                        <tr>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCityHeader" runat="server" CommandName="SortByHotelCity"
                                    Text="Hotel City" Width="43px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCheckInHeader" runat="server" CommandName="SortByCheckIn"
                                    Text="Check-In" Width="64px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCheckOutHeader" runat="server" CommandName="SortByCheckOut"
                                    Text="Check-Out" Width="64px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblNitesHeader" runat="server" CommandName="SortByHotelNites"
                                    Text="Hotel Nights" Width="34px" />
                            </th>
                            <%-- <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblReasonHeader" runat="server" CommandName="SortByReason" Text="Reason" Width="44px"/>
                        </th> --%>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblLastNameHeader" runat="server" CommandName="SortByLastName"
                                    Text="Last Name" Width="145px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblFirstNameHeader" runat="server" CommandName="SortByFirstName"
                                    Text="First Name" Width="144px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblEmpIDHeader" runat="server" CommandName="SortByEmployeeID"
                                    Text="Employee ID" Width="54px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblGenderHeader" runat="server" CommandName="SortByGender"
                                    Text="Gender" Width="45px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="SortBySingleDouble"
                                    Text="Single/Double" Width="74px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCoupleHeader" runat="server" CommandName="SortByCouple"
                                    Text="Couple" Width="39px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblTitleHeader" runat="server" CommandName="SortByTitle" Text="Title"
                                    Width="215px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblShipHeader" runat="server" CommandName="SortByShip" Text="Ship"
                                    Width="164px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="SortByCost" Text="Cost Center"
                                    Width="164px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblNationalityHeader" runat="server" CommandName="SortByNationality"
                                    Text="Nationality" Width="195px" />
                            </th>
                            <%-- <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblHRHeader" runat="server" CommandName="SortByHotelRequest" Text="Hotel Request" Width="52px"/>
                        </th>--%>
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton33" Text="Birthday" Width="70px" runat="server" CommandName="SortByBirthday" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblRLHeader" runat="server" CommandName="SortByRecordLocator"
                                    Text="Record Locator" Width="55px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblDCityHeader" runat="server" CommandName="SortByDeptCity"
                                    Text="Dept City" Width="45px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblACityHeader" runat="server" CommandName="SortByArvlCity"
                                    Text="Arvl City" Width="43px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByDeptDate" Text="Dept Date"
                                    Width="60px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByArvlDate" Text="Arvl Date"
                                    Width="60px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblDTimeHeader" runat="server" CommandName="SortByDeptTime"
                                    Text="Dept Time" Width="53px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblATimeHeader" runat="server" CommandName="SortByArvlTime"
                                    Text="Arvl Time" Width="53px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCarrierHeader" runat="server" CommandName="SortByCarrier"
                                    Text="Carrier" Width="35px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblFlightNumHeader" runat="server" CommandName="SortByFlightNo"
                                    Text="Flight No." Width="44px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblVoucherHeader" runat="server" CommandName="SortByVoucher"
                                    Text="Voucher" Width="44px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblPNumHeader" runat="server" CommandName="SortByPassportNo"
                                    Text="Passport No." Width="57px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblIssuedHeader" runat="server" CommandName="SortByDateIssued"
                                    Text="Date Issued" Width="60px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblExpHeader" runat="server" CommandName="SortByPassportExpiration"
                                    Text="Passport Expiration" Width="59px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblBookingRemarksHeader" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Booking Remarks" Width="150px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label2" Text="" Width="25px"></asp:Label>
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
        <div id="CancelledB" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
            onscroll="divScrollLCancel();">
            <asp:ListView runat="server" ID="uoListViewCancelled" DataSourceID="uoObjectDataSourceCancelled"
                OnDataBound="uoListViewCancelled_DataBound"
                onpagepropertieschanging="uoListViewCancelled_PagePropertiesChanging">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%# HotelAddGroup()%>
                    <tr>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label ID="Label1" runat="server" Width="50px" Text='<%# Eval("HotelCity") %>'></asp:Label>
                            <asp:Label ID="uoLabelWithEvent" runat="server" Text="*" Visible='<%# Eval("WithEvent") %>'
                                ForeColor="red" Font-Bold="true" Font-Size="Large"></asp:Label>
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
                            <asp:Label runat="server" ID="uoLblNites" Text='<%# Eval("HotelNights")%>' Width="40px"></asp:Label>
                        </td>
                        <%--  <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblReason" Text='<%# Eval("ReasonCode")%>' Width="50px"></asp:Label>                                        
                    </td>--%>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLabelLast" Text='<%# Eval("LastName")%>' Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:LinkButton Width="150px" runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("EmployeeId") + "&trID=" + Eval("TravelReqIdInt") + "&st=" + Eval("Status") + "&recloc=" + Eval("RecLoc") + "&manualReqID=" + Eval("RequestID") + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IDBigInt") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("FirstName") %></asp:LinkButton>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("EmployeeId")%>' Width="60px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblGender" Text='<%# Eval("Gender")%>' Width="50px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblRoom" Text='<%# Eval("SingleDouble")%>' Width="80px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblCouple" Text='<%# Eval("Couple")%>' Width="45px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblRank" Text='<%# Eval("Title")%>' Width="220px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblVessel" Text='<%# Eval("ShipCode") + " - " + Eval("ShipName")%>'
                                Width="170px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblCostCenter" Text='<%# Eval("CostCenterCode") + " - " + Eval("CostCenter")%>'
                                Width="170px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblNationality" Text='<%# Eval("Nationality")%>'
                                Width="200px"></asp:Label>
                        </td>
                        <%-- <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblHRequest" Text='<%# Eval("HotelRequest")%>' Width="60px"></asp:Label>
                    </td>--%>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label7" Width="76px" Text='<%#String.Format("{0:dd-MMM-yyyy}", Eval("Birthday"))%>'></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecLoc")%>' Width="60px"></asp:Label>
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
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblPNum" Text='<%# Eval("PassportNo")%>' Width="65px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblIssuedBy" Text='<%# Eval("PasportDateIssued")%>'
                                Width="65px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblExp" Text='<%# Eval("PassportExpiration")%>' Width="65px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label3" Text='<%# Eval("BookingRemarks") %>' Width="150px"></asp:Label>
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
                <asp:DataPager ID="uoDataPagerCancelled" runat="server" PagedControlID="uoListViewCancelled"
                    PageSize="20">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </div>
        </div>
        <%--===================================CANCELLED==================================================== --%>
        
           <asp:ObjectDataSource ID="uoObjectDataSourceConfirmed" runat="server" MaximumRowsParameterName="MaxRow"
            SelectCountMethod="GetHotelConfirmManifestCount" SelectMethod="GetHotelConfirmManifestList"
            StartRowIndexParameterName="StartRow" TypeName="TRAVELMART.Hotel.HotelConfirmManifestForecast"
            OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True">
            <SelectParameters>
                <asp:ControlParameter Name="DateFromString" ControlID="uoHiddenFieldDate" />
                <asp:ControlParameter Name="DateToString" ControlID="uoHiddenFieldDateTo" />
                <asp:ControlParameter Name="strUser" ControlID="uoHiddenFieldUser" />
                <asp:Parameter Name="DateFilter" Type="String" DefaultValue="1" />
                <asp:Parameter Name="ByNameOrID" Type="String" DefaultValue="2" />
                <asp:Parameter Name="filterNameOrID" Type="String" DefaultValue="" />
                <asp:Parameter Name="Nationality" Type="String" DefaultValue="0" />
                <asp:Parameter Name="Gender" Type="String" DefaultValue="0" />
                <asp:Parameter Name="Rank" Type="String" DefaultValue="0" />
                <asp:Parameter Name="Status" Type="String" DefaultValue="0" />
                <asp:ControlParameter Name="Region" Type="String" ControlID="uoHiddenFieldRegion" />
                <asp:Parameter Name="Country" Type="String" DefaultValue="0" />
                <asp:Parameter Name="City" Type="String" DefaultValue="0" />
                <asp:Parameter Name="Vessel" Type="String" DefaultValue="0" />
                <asp:ControlParameter Name="Port" ControlID="uoHiddenFieldPort" />
                <asp:ControlParameter Name="Hotel" ControlID="uoDropDownListHotel" />                
                <asp:ControlParameter Name="UserRole" ControlID="uoHiddenFieldRole" />
                <asp:ControlParameter Name="LoadType" ControlID="uoHiddenFieldLoadType" />
                <asp:ControlParameter Name="SortBy" ControlID="uoHiddenFieldSortBy" />
                <asp:Parameter Name="ListType" Type="String" DefaultValue="Confirm" />
            </SelectParameters>
        </asp:ObjectDataSource>
       
       <asp:ObjectDataSource ID="uoObjectDataSourceCancelled" runat="server" MaximumRowsParameterName="MaxRow"
            SelectCountMethod="GetHotelConfirmManifestCount" SelectMethod="GetHotelConfirmManifestList"
            StartRowIndexParameterName="StartRow" TypeName="TRAVELMART.Hotel.HotelConfirmManifestForecast"
            OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True">
            <SelectParameters>
                <asp:ControlParameter Name="DateFromString" ControlID="uoHiddenFieldDate" />
                <asp:ControlParameter Name="DateToString" ControlID="uoHiddenFieldDateTo" />
                <asp:ControlParameter Name="strUser" ControlID="uoHiddenFieldUser" />
                <asp:Parameter Name="DateFilter" Type="String" DefaultValue="1" />
                <asp:Parameter Name="ByNameOrID" Type="String" DefaultValue="2" />
                <asp:Parameter Name="filterNameOrID" Type="String" DefaultValue="" />
                <asp:Parameter Name="Nationality" Type="String" DefaultValue="0" />
                <asp:Parameter Name="Gender" Type="String" DefaultValue="0" />
                <asp:Parameter Name="Rank" Type="String" DefaultValue="0" />
                <asp:Parameter Name="Status" Type="String" DefaultValue="0" />
                <asp:ControlParameter Name="Region" Type="String" ControlID="uoHiddenFieldRegion" />
                <asp:Parameter Name="Country" Type="String" DefaultValue="0" />
                <asp:Parameter Name="City" Type="String" DefaultValue="0" />
                <asp:Parameter Name="Vessel" Type="String" DefaultValue="0" />
                <asp:ControlParameter Name="Port" ControlID="uoHiddenFieldPort" />
                <asp:ControlParameter Name="Hotel" ControlID="uoDropDownListHotel" />                
                <asp:ControlParameter Name="UserRole" ControlID="uoHiddenFieldRole" />
                <asp:ControlParameter Name="LoadType" ControlID="uoHiddenFieldLoadType" />
                <asp:ControlParameter Name="SortBy" ControlID="uoHiddenFieldSortBy" />
                <asp:Parameter Name="ListType" Type="String" DefaultValue="Cancel" />
            </SelectParameters>
        </asp:ObjectDataSource>
        
        <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldDateTo" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldRegion" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldPort" runat="server" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldSortBy" EnableViewState="true" Value="" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldSendEmail" EnableViewState="true"
            Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldSendSaveEmail" EnableViewState="true"
            Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldTo" EnableViewState="true" Value="" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldCc" EnableViewState="true" Value="" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldHotelName" Value="" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldPopupConfirmation" Value="1" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldConfirmation" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldIsVendor" Value="false" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldTagTime" Value="" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldIsTagLinkVisible" Value="false" />
    </div>
    <div style="display: none">
        <div id="uoDivEmail">
            <table cellpadding="0" width="500px">
                <tr class="PageTitle">
                    <td colspan="2">
                        Hotel E-Mail
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="LeftClass" style="width: 15%;">
                        &nbsp To:
                    </td>
                    <td class="LeftClass">
                        <asp:TextBox onblur="validateMultipleEmailsSemiColonSeparated(this,';');" ID="uoTextBoxTo"
                            runat="server" Width="80%" MaxLength="500" Height="50px" TextMode="MultiLine"
                            ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="LeftClass" style="width: 15%;">
                        &nbsp Cc:
                    </td>
                    <td class="LeftClass">
                        <asp:TextBox onblur="validateMultipleEmailsSemiColonSeparated(this,';');" ID="uoTextBoxCc"
                            runat="server" Width="80%" MaxLength="500" Height="50px" TextMode="MultiLine"
                            ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="uoButtonSend" runat="server" Text="Send" CssClass="SmallButton" OnClientClick="javascript:SendEmail(1,0);" />
                        &nbsp;
                        <asp:Button ID="uoButtonSaveAndSend" runat="server" Text="Save & Send" CssClass="SmallButton"
                            OnClientClick="return confirmSaveAndSend();" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
