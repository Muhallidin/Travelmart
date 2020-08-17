<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="HotelNonTurnPortsDynamic.aspx.cs" Inherits="TRAVELMART.Hotel.HotelNonTurnPortsDynamic"
    Async="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server" ID="header">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="PageTitle">
            <td align="left">
                Non Turn Port Manifest List
            </td>
            <td align="right">
                Seaport: &nbsp;&nbsp;
                <asp:DropDownList ID="uoDropDownListPort" runat="server" Width="300px" AutoPostBack="true"
                    AppendDataBoundItems="true" OnSelectedIndexChanged="uoDropDownListPort_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript">
        function CloseModal(strURL) {
            window.location = strURL;
        }

        $(document).ready(function() {
            SetExceptionResolution();
            ShowEmailPopup();
        });
        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetExceptionResolution();
                ShowEmailPopup();
            }
        }        
        function SetExceptionResolution() {
            var ht = $(window).height();
            var ht2 = $(window).height();

            var wd = $(window).width() * 0.90;
            var screenH = screen.height;
            var percent = 0.60;

            if (screen.height <= 600) {
                ht = ht * 0.44;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.55;
            }
            else {
                if (screenH = 768) {
                    percent = (550 - (screenH - ht)) / ht;
                }
                ht = ht * percent;
            }

            $("#Av").height(ht);
            $("#Av").width(wd);

        }

        function ShowEmailPopup() {
            $("a#<%=uoHyperLinkSendEmails.ClientID %>").fancybox(
            {
                'centerOnScroll': false,
                'width': '1000%',
                'height': '40%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut'            
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

        function SetSettings(chk) {
            var status = chk.checked;
            $('input:checkbox[name *= uoSelectCheckBox]').attr('checked', status);
        }

        function SetCheckBoxSettings(chk) {
            var status = chk.checked;
            var count = $('tr', '#uoTblExceptionList').size();
            for (var i = 0; i <= count; i++) {

                if (document.getElementById("ctl00_NaviPlaceHolder_uoListViewNonTurnPort_ctrl" + i + "_uoSelectCheckBox") != null) {
                    if (document.getElementById("ctl00_NaviPlaceHolder_uoListViewNonTurnPort_ctrl" + i + "_uoSelectCheckBox").disabled == false) {
                        document.getElementById("ctl00_NaviPlaceHolder_uoListViewNonTurnPort_ctrl" + i + "_uoSelectCheckBox").checked = status;
                    }
                }
            }
        }

        //        function divScrollL() {
        //            var Right = document.getElementById('Av');
        //            var Left = document.getElementById('Bv');
        //            Right.scrollLeft = Left.scrollLeft;

        //        }

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
    </script>

    <table style="text-align: left" border="0" width="100%">
        <tr>
            <td width="100px">
                <asp:Button runat="server" ID="uoBtnExportList" CssClass="SmallButton" Text="Export Non Turn Port Manifest List"
                    OnClick="uoBtnExportList_Click" />
            </td>
            <td style="text-align: left">
                <%--<asp:Button runat="server" ID="uoButtonEmail" CssClass="SmallButton" Text="Email" OnClick="uoButtonEmail_Click"/>    --%>
                <a id="uoHyperLinkSendEmails" runat="server" href="#uoDivEmail">Confirm & E-mail</a>
            </td>
        </tr>
    </table>
    
   <%-- <asp:GridView ID="uoGridViewNew" runat="server" AutoGenerateColumns="true" GridLines="None"                                          
        Width="750px" BorderStyle="None" CssClass="listViewTable" >  
        
    </asp:GridView>
    --%>
    <div id="Av" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;
        position: relative;">
        <asp:ListView runat="server" ID="ListView1">
            <LayoutTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">                    
                    <tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>                                    
                        <th class="hideElement">
                            requestInfo
                        </th>
                        <th class="hideElement">
                            <asp:CheckBox runat="server" ID="CheckBox1" Width="19px" CssClass="Checkbox" onclick="SetCheckBoxSettings(this);" />
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableHotelCity" runat="server" CommandName="SortByHotelName"
                                Text="Hotel City" Width="53px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">                            
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByServiceRequest" Text="Service Requested"
                                Width="150px"></asp:LinkButton>
                        </th>
                         <th style="text-align: center;">                            
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByServiceRequestDate" Text="Transpo Request Date"
                                Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableCheckin" runat="server" CommandName="SortByCheckin" Text="Checkin"
                                Width="63px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableCheckOut" runat="server" CommandName="SortByCheckOut"
                                Text="CheckOut" Width="65px"></asp:LinkButton>
                        </th>                        
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableEmployee" runat="server" CommandName="SortByE1ID" Text="E1 ID"
                                Width="48px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableName" runat="server" CommandName="SortByLName" Text="Last Name"
                                Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByFName" Text="First Name"
                                Width="98px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableGender" runat="server" CommandName="SortByGender" Text="Gender"
                                Width="44px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableSingleDouble" runat="server" CommandName="SortBySingleDouble"
                                Text="Single Double" Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableCouple" runat="server" CommandName="SortByCouple" Text="Couple"
                                Width="40px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableTitle" runat="server" CommandName="SortByTitle" Text="Title"
                                Width="195px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableStripe" runat="server" CommandName="SortByStripe" Text="Stripe"
                                Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableShip" runat="server" CommandName="SortByShip" Text="Ship"
                                Width="195px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableCostCenter" runat="server" CommandName="SortByCostCenter"
                                Text="Cost Center" Width="195px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableNationality" runat="server" CommandName="SortByNationality"
                                Text="Nationality" Width="98px"></asp:LinkButton>
                        </th>
                       
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableHotelRequest" runat="server" CommandName="SortByHotelRequest"
                                Text="Hotel Request" Width="47px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableRecLoc" runat="server" CommandName="SortByRecLoc" Text="RecLoc"
                                Width="54px"></asp:LinkButton>
                        </th>
                        
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableVoucher" runat="server" CommandName="SortByVoucher" Text="Voucher"
                                Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLablePassportNo" runat="server" CommandName="SortByPassportNo"
                                Text="Passport No" Width="53px" />
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLablePassportExp" runat="server" CommandName="SortByPassportExp"
                                Text="Passport Exp" Width="74px" />
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLablePassportIssued" runat="server" CommandName="SortByPassportIssued"
                                Text="Passport Issued" Width="73px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton33" Text="Birthday" Width="72px" runat="server" CommandName="SortByBirthday" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton7" Text="Hotel Branch" Width="75px" runat="server" CommandName="SortByHotelBranch" />
                        </th>
                        
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableHotelBranch" runat="server" CommandName="SortByHotelBranch"
                                Text="Exception Remarks" Width="198px" />
                        </th>
                        
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableBookingRemark" runat="server" CommandName="SortByBookingRemark"
                                Text="Booking Remarks" Width="143px" />
                        </th>
                        
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableAirSequence" runat="server" CommandName="SortByAirSequence"
                                Text="Air Sequence" Width="55px"></asp:LinkButton>
                        </th>
                         <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableCarrier" runat="server" CommandName="SortByCarrier" Text="Carrier"
                                Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableFlightNo" runat="server" CommandName="SortByFlightNo"
                                Text="Flight No" Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableDepartureCity" runat="server" CommandName="SortByDepartureCity"
                                Text="Departure City" Width="55px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableArrivalCity" runat="server" CommandName="SortByArrivalCity"
                                Text="Arrival City" Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableArrivalDate" runat="server" CommandName="SortByArrivalDate"
                                Text="Arrival Date" Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableArrivalTime" runat="server" CommandName="SortByArrivalTime"
                                Text="Arrival Time" Width="54px"></asp:LinkButton>
                        </th>                                               
                        
                        <%# BindHeaderAirNew()%>
            </ItemTemplate>
            <EmptyDataTemplate>                
            </EmptyDataTemplate>
        </asp:ListView>
        <%-- </div>
    
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;
        position: relative;" onscroll="divScrollL();">--%>
        <asp:ListView runat="server" ID="uoListViewNonTurnPort">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList"
                    width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <%# DashboardChangeRowColor()%>               
                <td style="white-space: normal;" class="hideElement">
                    <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" Width="24px" runat="server"
                        Enabled='<%# Eval("IsVisible") %>' />
                </td>
                <td class="hideElement">
                    <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IDBigInt") %>' />
                    <asp:HiddenField runat="server" ID="hfSeqNo" Value='<%# Eval("AirSequence") %>' />
                    <asp:HiddenField runat="server" ID="hfReqId" Value='<%# Eval("TravelReqId") %>' />
                    <asp:HiddenField runat="server" ID="hfRoomType" Value='<%# Eval("RoomTypeId") %>' />
                    <asp:HiddenField runat="server" ID="hfPortId" Value='<%# Eval("PortId") %>' />
                    <asp:HiddenField runat="server" ID="uoLblStatus" Value='<%# Eval("SFStatus") %>' />
                    <asp:HiddenField runat="server" ID="uoLblHotelNite" Value='<%# Eval("HotelNite") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblHotelCity" Text='<%# Eval("HotelCity") %>' Width="60px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label6" Text='<%# Eval("ServiceRequested") %>' Width="153px"  />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label8" Text='<%# Eval("ServiceRequestedDate")%>' Width="107px"  />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCheckin" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("Checkin"))%>'
                        Width="70px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned">
                    <asp:Label runat="server" ID="uoLblCheckOut" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOut"))%>'
                        Width="70px" Visible='<%# Eval("IsVisible") %>' />
                </td>                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblSeafarerID" Text='<%# Eval("Employee")%>' Width="55px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "../SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("Employee") + "&recloc=&st=" + Eval("SFStatus") + "&ID=0&trID="+ Eval("TravelReqId") +"&manualReqID=0" + "&dt=" + Request.QueryString["dt"]%>'
                        runat="server" Width="105px" Text='<%# Eval("LastName")%>' Visible='<%# Eval("IsVisible") %>'></asp:HyperLink>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label2" Text='<%# Eval("FirstName")%>' Width="105px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblGender" Text='<%# Eval("Gender")%>' Width="50px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label1" Text='<%# String.Format("{0:0.00}", Eval("SingleDouble"))%>'
                        Width="50px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCouple" Text='<%# Eval("Couple")%>' Width="48px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblTitle" Text='<%# Eval("Title")%>' Width="200px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblStripe" Text='<%# String.Format("{0:0.00}", Eval("Stripes"))%>'
                        Width="50px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblShip" Text='<%# Eval("Ship")%>' Width="200px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCostcenter" Text='<%# Eval("Costcenter")%>' Width="200px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblNatioality" Text='<%# Eval("Nationality")%>' Width="104px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
               
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblHotelRequest" Text='<%# Eval("HotelRequest")%>'
                        Width="55px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecLoc")%>' Width="60px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
               <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblVoucher" Text='<%# Eval("Voucher")%>' Width="50px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblPassportNo" Text='<%# Eval("PassportNo")%>' Width="60px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblPassportExp" Text='<%# Eval("PassportExp")%>'
                        Width="80px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblPassportIssued" Text='<%# Eval("PassportIssued")%>'
                        Width="80px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label7" Width="76px" Text='<%#String.Format("{0:dd-MMM-yyyy}", Eval("Birthday"))%>'
                        Visible='<%# Eval("IsVisible") %>'></asp:Label>
                </td>
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label9" Text='<%# Eval("HotelBranch")%>'
                        Width="80px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblHotelBranch" Text='<%# Eval("Booking")%>' Width="205px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblBookingremark" Text='<%# Eval("BookingRemark")%>'
                        Width="150px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblAirSequence" Text='<%# Eval("AirSequence")%>'
                        Width="60px" />
                </td>
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblFlightNo" Text='<%# Eval("FlightNo")%>' Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLbldeptCity" Text='<%# Eval("deptCity")%>' Width="61px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("ArvlCity")%>' Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblArvldate" Text='<%# Eval("Arvldate")%>' Width="105px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblArvlTime" Text='<%#  Eval("ArvlTime")%>' Width="60px" />
                </td>
                                              
                <%# BindDetailAirNew() %>              
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <td colspan="20" class="leftAligned">
                            <asp:Label runat="server" ID="Label10" Text="No Record"> </asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <%--===================================CONFIRMED==================================================== --%>
        <br />
        <div runat="server" id="PendingDiv" class="PageSubTitle" style="text-decoration: underline;">
            Confirmed Manifest
        </div>
        <%--<div id="BvCon" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;
        position: relative;">--%>
        <asp:ListView runat="server" ID="uoListViewHeaderConfirmed">
            <LayoutTemplate>
                 <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>                
                        <th class="hideElement">
                            requestInfo
                        </th>
                        <th class="hideElement">
                            <asp:CheckBox runat="server" ID="CheckBox1" Width="19px" CssClass="Checkbox" onclick="SetCheckBoxSettings(this);" />
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="SortByAddition" Text="Remarks"
                                Width="49px"></asp:LinkButton>
                        </th>
                       <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableHotelCity" runat="server" CommandName="SortByHotelName"
                                Text="Hotel City" Width="53px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">                            
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByServiceRequest" Text="Service Requested"
                                Width="150px"></asp:LinkButton>
                        </th>
                         <th style="text-align: center;">                            
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByServiceRequestDate" Text="Transpo Request Date"
                                Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableCheckin" runat="server" CommandName="SortByCheckin" Text="Checkin"
                                Width="63px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableCheckOut" runat="server" CommandName="SortByCheckOut"
                                Text="CheckOut" Width="65px"></asp:LinkButton>
                        </th>                        
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableEmployee" runat="server" CommandName="SortByE1ID" Text="E1 ID"
                                Width="48px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableName" runat="server" CommandName="SortByLName" Text="Last Name"
                                Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByFName" Text="First Name"
                                Width="98px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableGender" runat="server" CommandName="SortByGender" Text="Gender"
                                Width="44px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableSingleDouble" runat="server" CommandName="SortBySingleDouble"
                                Text="Single Double" Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableCouple" runat="server" CommandName="SortByCouple" Text="Couple"
                                Width="40px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableTitle" runat="server" CommandName="SortByTitle" Text="Title"
                                Width="195px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableStripe" runat="server" CommandName="SortByStripe" Text="Stripe"
                                Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableShip" runat="server" CommandName="SortByShip" Text="Ship"
                                Width="195px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableCostCenter" runat="server" CommandName="SortByCostCenter"
                                Text="Cost Center" Width="195px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableNationality" runat="server" CommandName="SortByNationality"
                                Text="Nationality" Width="98px"></asp:LinkButton>
                        </th>
                       
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableHotelRequest" runat="server" CommandName="SortByHotelRequest"
                                Text="Hotel Request" Width="47px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableRecLoc" runat="server" CommandName="SortByRecLoc" Text="RecLoc"
                                Width="54px"></asp:LinkButton>
                        </th>
                        
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableVoucher" runat="server" CommandName="SortByVoucher" Text="Voucher"
                                Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLablePassportNo" runat="server" CommandName="SortByPassportNo"
                                Text="Passport No" Width="53px" />
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLablePassportExp" runat="server" CommandName="SortByPassportExp"
                                Text="Passport Exp" Width="74px" />
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLablePassportIssued" runat="server" CommandName="SortByPassportIssued"
                                Text="Passport Issued" Width="73px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton33" Text="Birthday" Width="70px" runat="server" CommandName="SortByBirthday" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton7" Text="Hotel Branch" Width="75px" runat="server" CommandName="SortByHotelBranch" />
                        </th>
                        
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableHotelBranch" runat="server" CommandName="SortByHotelBranch"
                                Text="Exception Remarks" Width="198px" />
                        </th>
                        
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableBookingRemark" runat="server" CommandName="SortByBookingRemark"
                                Text="Booking Remarks" Width="143px" />
                        </th>
                        
                         <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableBookingRemark" Text="Booking Remark" Width="150px"> </asp:Label>--%>
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByConfirmedBy" Text="Confirmed By"
                                Width="143px" />
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableBookingRemark" Text="Booking Remark" Width="150px"> </asp:Label>--%>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByConfirmedDate"
                                Text="Date Confirmed" Width="150px" />
                        </th>
                        
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableAirSequence" runat="server" CommandName="SortByAirSequence"
                                Text="Air Sequence" Width="56px"></asp:LinkButton>
                        </th>
                         <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableCarrier" runat="server" CommandName="SortByCarrier" Text="Carrier"
                                Width="46px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableFlightNo" runat="server" CommandName="SortByFlightNo"
                                Text="Flight No" Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableDepartureCity" runat="server" CommandName="SortByDepartureCity"
                                Text="Departure City" Width="55px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableArrivalCity" runat="server" CommandName="SortByArrivalCity"
                                Text="Arrival City" Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableArrivalDate" runat="server" CommandName="SortByArrivalDate"
                                Text="Arrival Date" Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableArrivalTime" runat="server" CommandName="SortByArrivalTime"
                                Text="Arrival Time" Width="54px"></asp:LinkButton>
                        </th>                       
                        <%# BindHeaderAirConfirm()%>
                   
            </ItemTemplate>
            <EmptyDataTemplate>                
            </EmptyDataTemplate>
        </asp:ListView>
        <%--</div>
    
    <div id="AvCon" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;
        position: relative;" onscroll="divScrollL();">--%>
        <asp:ListView runat="server" ID="uoListViewConfirmed">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList"
                    width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <%# DashboardChangeRowColor()%>
                <td style="white-space: normal;" class="hideElement">
                    <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" Width="24px" runat="server"
                        Enabled='<%# Eval("IsVisible") %>' />
                </td>
                <td class="hideElement">
                    <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IDBigInt") %>' />
                    <asp:HiddenField runat="server" ID="hfSeqNo" Value='<%# Eval("AirSequence") %>' />
                    <asp:HiddenField runat="server" ID="hfReqId" Value='<%# Eval("TravelReqId") %>' />
                    <asp:HiddenField runat="server" ID="hfRoomType" Value='<%# Eval("RoomTypeId") %>' />
                    <asp:HiddenField runat="server" ID="hfPortId" Value='<%# Eval("PortId") %>' />
                    <asp:HiddenField runat="server" ID="uoLblStatus" Value='<%# Eval("SFStatus") %>' />
                    <asp:HiddenField runat="server" ID="uoLblHotelNite" Value='<%# Eval("HotelNite") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label5" Text='<%# Eval("Remarks") %>' Width="55px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblHotelCity" Text='<%# Eval("HotelCity") %>' Width="60px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label6" Text='<%# Eval("ServiceRequested") %>' Width="153px"  />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label8" Text='<%# Eval("ServiceRequestedDate")%>' Width="107px"  />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCheckin" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("Checkin"))%>'
                        Width="70px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned">
                    <asp:Label runat="server" ID="uoLblCheckOut" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOut"))%>'
                        Width="70px" Visible='<%# Eval("IsVisible") %>' />
                </td>                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblSeafarerID" Text='<%# Eval("Employee")%>' Width="55px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "../SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("Employee") + "&recloc=&st=" + Eval("SFStatus") + "&ID=0&trID="+ Eval("TravelReqId") +"&manualReqID=0" + "&dt=" + Request.QueryString["dt"]%>'
                        runat="server" Width="105px" Text='<%# Eval("LastName")%>' Visible='<%# Eval("IsVisible") %>'></asp:HyperLink>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label2" Text='<%# Eval("FirstName")%>' Width="105px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblGender" Text='<%# Eval("Gender")%>' Width="50px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label1" Text='<%# String.Format("{0:0.00}", Eval("SingleDouble"))%>'
                        Width="50px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCouple" Text='<%# Eval("Couple")%>' Width="48px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblTitle" Text='<%# Eval("Title")%>' Width="200px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblStripe" Text='<%# String.Format("{0:0.00}", Eval("Stripes"))%>'
                        Width="50px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblShip" Text='<%# Eval("Ship")%>' Width="200px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCostcenter" Text='<%# Eval("Costcenter")%>' Width="200px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblNatioality" Text='<%# Eval("Nationality")%>' Width="104px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
               
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblHotelRequest" Text='<%# Eval("HotelRequest")%>'
                        Width="55px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecLoc")%>' Width="60px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
               <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblVoucher" Text='<%# Eval("Voucher")%>' Width="50px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblPassportNo" Text='<%# Eval("PassportNo")%>' Width="60px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblPassportExp" Text='<%# Eval("PassportExp")%>'
                        Width="80px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblPassportIssued" Text='<%# Eval("PassportIssued")%>'
                        Width="80px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label7" Width="76px" Text='<%#String.Format("{0:dd-MMM-yyyy}", Eval("Birthday"))%>'
                        Visible='<%# Eval("IsVisible") %>'></asp:Label>
                </td>
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label9" Text='<%# Eval("HotelBranch")%>'
                        Width="80px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblHotelBranch" Text='<%# Eval("Booking")%>' Width="205px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblBookingremark" Text='<%# Eval("BookingRemark")%>'
                        Width="150px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("ConfirmedBy")%>' Width="150px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label4" Text='<%# String.Format("{0:dd-MMM-yyyy hhmm}", Eval("ConfirmedDate"))%>'
                        Width="155px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblAirSequence" Text='<%# Eval("AirSequence")%>'
                        Width="60px" />
                </td>
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblFlightNo" Text='<%# Eval("FlightNo")%>' Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLbldeptCity" Text='<%# Eval("deptCity")%>' Width="61px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("ArvlCity")%>' Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblArvldate" Text='<%# Eval("Arvldate")%>' Width="105px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblArvlTime" Text='<%#  Eval("ArvlTime")%>' Width="60px" />
                </td>
                                
                 <%# BindDetailAirConfirm() %>              
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <td colspan="20" class="leftAligned">
                            <asp:Label runat="server" ID="Label10" Text="No Record"> </asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <%--===================================CONFIRMED==================================================== --%>
        <%--===================================CANCELLED==================================================== --%>
        <br />
        <div runat="server" id="Div1" class="PageSubTitle" style="text-decoration: underline;">
            Cancelled Manifest
        </div>
        <asp:ListView runat="server" ID="uoListViewHeaderCancelled">
            <LayoutTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">                    
                    <tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>                                    
                    <th class="hideElement">
                        requestInfo
                    </th>
                    <th class="hideElement">
                        <asp:CheckBox runat="server" ID="CheckBox1" Width="19px" CssClass="Checkbox" onclick="SetCheckBoxSettings(this);" />
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableHotelCity" runat="server" CommandName="SortByHotelName"
                            Text="Hotel City" Width="53px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">                            
                        <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByServiceRequest" Text="Service Requested"
                            Width="150px"></asp:LinkButton>
                    </th>
                     <th style="text-align: center;">                            
                        <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByServiceRequestDate" Text="Transpo Request Date"
                            Width="100px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableCheckin" runat="server" CommandName="SortByCheckin" Text="Checkin"
                            Width="63px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableCheckOut" runat="server" CommandName="SortByCheckOut"
                            Text="CheckOut" Width="65px"></asp:LinkButton>
                    </th>                        
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableEmployee" runat="server" CommandName="SortByE1ID" Text="E1 ID"
                            Width="48px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableName" runat="server" CommandName="SortByLName" Text="Last Name"
                            Width="100px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByFName" Text="First Name"
                            Width="98px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableGender" runat="server" CommandName="SortByGender" Text="Gender"
                            Width="44px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableSingleDouble" runat="server" CommandName="SortBySingleDouble"
                            Text="Single Double" Width="45px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableCouple" runat="server" CommandName="SortByCouple" Text="Couple"
                            Width="40px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableTitle" runat="server" CommandName="SortByTitle" Text="Title"
                            Width="195px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableStripe" runat="server" CommandName="SortByStripe" Text="Stripe"
                            Width="43px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableShip" runat="server" CommandName="SortByShip" Text="Ship"
                            Width="195px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableCostCenter" runat="server" CommandName="SortByCostCenter"
                            Text="Cost Center" Width="195px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableNationality" runat="server" CommandName="SortByNationality"
                            Text="Nationality" Width="98px"></asp:LinkButton>
                    </th>
                   
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableHotelRequest" runat="server" CommandName="SortByHotelRequest"
                            Text="Hotel Request" Width="47px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableRecLoc" runat="server" CommandName="SortByRecLoc" Text="RecLoc"
                            Width="54px"></asp:LinkButton>
                    </th>
                    
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableVoucher" runat="server" CommandName="SortByVoucher" Text="Voucher"
                            Width="45px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLablePassportNo" runat="server" CommandName="SortByPassportNo"
                            Text="Passport No" Width="53px" />
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLablePassportExp" runat="server" CommandName="SortByPassportExp"
                            Text="Passport Exp" Width="74px" />
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLablePassportIssued" runat="server" CommandName="SortByPassportIssued"
                            Text="Passport Issued" Width="73px" />
                    </th>
                    <th style="text-align: center; white-space: normal;">
                        <asp:LinkButton ID="LinkButton33" Text="Birthday" Width="72px" runat="server" CommandName="SortByBirthday" />
                    </th>
                     <th style="text-align: center; white-space: normal;">
                        <asp:LinkButton ID="LinkButton7" Text="Hotel Branch" Width="75px" runat="server" CommandName="SortByHotelBranch" />
                    </th>
                    
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableHotelBranch" runat="server" CommandName="SortByHotelBranch"
                            Text="Exception Remarks" Width="198px" />
                    </th>
                    
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableBookingRemark" runat="server" CommandName="SortByBookingRemark"
                            Text="Booking Remarks" Width="143px" />
                    </th>
                    
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableAirSequence" runat="server" CommandName="SortByAirSequence"
                            Text="Air Sequence" Width="55px"></asp:LinkButton>
                    </th>
                     <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableCarrier" runat="server" CommandName="SortByCarrier" Text="Carrier"
                            Width="45px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableFlightNo" runat="server" CommandName="SortByFlightNo"
                            Text="Flight No" Width="43px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableDepartureCity" runat="server" CommandName="SortByDepartureCity"
                            Text="Departure City" Width="55px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableArrivalCity" runat="server" CommandName="SortByArrivalCity"
                            Text="Arrival City" Width="43px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableArrivalDate" runat="server" CommandName="SortByArrivalDate"
                            Text="Arrival Date" Width="100px"></asp:LinkButton>
                    </th>
                    <th style="text-align: center;">
                        <asp:LinkButton ID="uoLableArrivalTime" runat="server" CommandName="SortByArrivalTime"
                            Text="Arrival Time" Width="54px"></asp:LinkButton>
                    </th>                                               
                        
                    <%# BindHeaderAirCancel()%>
            </ItemTemplate>
            <EmptyDataTemplate>                
            </EmptyDataTemplate>
        </asp:ListView>
        <asp:ListView runat="server" ID="uoListViewCancelled">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList"
                    width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <%# DashboardChangeRowColor()%>               
                <td style="white-space: normal;" class="hideElement">
                    <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" Width="24px" runat="server"
                        Enabled='<%# Eval("IsVisible") %>' />
                </td>
                <td class="hideElement">
                    <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IDBigInt") %>' />
                    <asp:HiddenField runat="server" ID="hfSeqNo" Value='<%# Eval("AirSequence") %>' />
                    <asp:HiddenField runat="server" ID="hfReqId" Value='<%# Eval("TravelReqId") %>' />
                    <asp:HiddenField runat="server" ID="hfRoomType" Value='<%# Eval("RoomTypeId") %>' />
                    <asp:HiddenField runat="server" ID="hfPortId" Value='<%# Eval("PortId") %>' />
                    <asp:HiddenField runat="server" ID="uoLblStatus" Value='<%# Eval("SFStatus") %>' />
                    <asp:HiddenField runat="server" ID="uoLblHotelNite" Value='<%# Eval("HotelNite") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblHotelCity" Text='<%# Eval("HotelCity") %>' Width="60px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label6" Text='<%# Eval("ServiceRequested") %>' Width="153px"  />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label8" Text='<%# Eval("ServiceRequestedDate")%>' Width="107px"  />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCheckin" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("Checkin"))%>'
                        Width="70px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned">
                    <asp:Label runat="server" ID="uoLblCheckOut" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOut"))%>'
                        Width="70px" Visible='<%# Eval("IsVisible") %>' />
                </td>                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblSeafarerID" Text='<%# Eval("Employee")%>' Width="55px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "../SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("Employee") + "&recloc=&st=" + Eval("SFStatus") + "&ID=0&trID="+ Eval("TravelReqId") +"&manualReqID=0" + "&dt=" + Request.QueryString["dt"]%>'
                        runat="server" Width="105px" Text='<%# Eval("LastName")%>' Visible='<%# Eval("IsVisible") %>'></asp:HyperLink>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label2" Text='<%# Eval("FirstName")%>' Width="105px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblGender" Text='<%# Eval("Gender")%>' Width="50px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label1" Text='<%# String.Format("{0:0.00}", Eval("SingleDouble"))%>'
                        Width="50px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCouple" Text='<%# Eval("Couple")%>' Width="48px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblTitle" Text='<%# Eval("Title")%>' Width="200px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblStripe" Text='<%# String.Format("{0:0.00}", Eval("Stripes"))%>'
                        Width="50px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblShip" Text='<%# Eval("Ship")%>' Width="200px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCostcenter" Text='<%# Eval("Costcenter")%>' Width="200px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblNatioality" Text='<%# Eval("Nationality")%>' Width="104px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
               
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblHotelRequest" Text='<%# Eval("HotelRequest")%>'
                        Width="55px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecLoc")%>' Width="60px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
               <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblVoucher" Text='<%# Eval("Voucher")%>' Width="50px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblPassportNo" Text='<%# Eval("PassportNo")%>' Width="60px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblPassportExp" Text='<%# Eval("PassportExp")%>'
                        Width="80px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblPassportIssued" Text='<%# Eval("PassportIssued")%>'
                        Width="80px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label7" Width="76px" Text='<%#String.Format("{0:dd-MMM-yyyy}", Eval("Birthday"))%>'
                        Visible='<%# Eval("IsVisible") %>'></asp:Label>
                </td>
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label9" Text='<%# Eval("HotelBranch")%>'
                        Width="80px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblHotelBranch" Text='<%# Eval("Booking")%>' Width="205px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblBookingremark" Text='<%# Eval("BookingRemark")%>'
                        Width="150px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblAirSequence" Text='<%# Eval("AirSequence")%>'
                        Width="60px" />
                </td>
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblFlightNo" Text='<%# Eval("FlightNo")%>' Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLbldeptCity" Text='<%# Eval("deptCity")%>' Width="61px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("ArvlCity")%>' Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblArvldate" Text='<%# Eval("Arvldate")%>' Width="105px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblArvlTime" Text='<%#  Eval("ArvlTime")%>' Width="60px" />
                </td>
                                              
                <%# BindDetailAirCancel()%>              
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <td colspan="20" class="leftAligned">
                            <asp:Label runat="server" ID="Label10" Text="No Record"> </asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <%--===================================CANCELLED==================================================== --%>
    </div>
    <asp:HiddenField runat="server" ID="uoHiddenFieldUser" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldStartDate" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDateChanged" Value="1" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDaysChanged" Value="1" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRoomTypeChanged" Value="1" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHIddenFieldBranchId" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHIddenFieldSFCount" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRoomChecked" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDate" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDateTo" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRoomType" EnableViewState="true"
        Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldNoOfNites" EnableViewState="true"
        Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldSelectedRoom" EnableViewState="true"
        Value="1" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldSendEmail" EnableViewState="true"
        Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldSendSaveEmail" EnableViewState="true"
        Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldTo" EnableViewState="true" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldCc" EnableViewState="true" Value="" />
    
    <asp:HiddenField runat="server" ID="uoHiddenFieldNewColCount" EnableViewState="true" Value="43" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldConfirmColCount" EnableViewState="true" Value="46" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldCancelColCount" EnableViewState="true" Value="43" />
    
    <div style="display: none">
        <div id="uoDivEmail">
            <table cellpadding="0" width="500px">
                <tr class="PageTitle">
                    <td colspan="2">
                        Service Provider E-Mail
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
