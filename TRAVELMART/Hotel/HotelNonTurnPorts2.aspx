<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="HotelNonTurnPorts2.aspx.cs" Inherits="TRAVELMART.Hotel.HotelNonTurnPorts2"
    Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server" ID="header">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="PageTitle">
            <td align="left">
                Service Provider - <asp:Label ID="lblServiceProvider"  runat="server" Text="Hotel request"
                                    style="font-size:12pt; font-style:normal; font-weight:bold;"> 
                                    </asp:Label>
            </td>
            <td align="right" style="display:none">
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
         });
        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetExceptionResolution();
                
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


            $("#DivConfirmed").height(ht);
            $("#DivConfirmed").width(wd);

            $("#DivCancelled").height(ht);
            $("#DivCancelled").width(wd);

            document.getElementById("ctl00_uoTblDate").style.display = "none";

            var session = getQuerystringData("st");

            console.log(session);

            if (session == 4) {
                document.getElementById("uoButtonHotel").style.display = 'none';

                document.getElementById("DivCancelled").style.display = 'block';
                document.getElementById("DivCancelLabel").style.display = 'block';

            }
            else if (session == 1) {
                document.getElementById("uoButtonCancell").style.display = 'none';
                document.getElementById("DivConfirmed").style.display = 'block';
                document.getElementById("DivCancelled").style.display = 'block';


                document.getElementById("DivConfirmedLabel").style.display = 'block';
                document.getElementById("DivCancelLabel").style.display = 'block';
            }
            else {
                document.getElementById("DivConfirmed").style.display = 'none';
                document.getElementById("DivCancelled").style.display = 'none';

                document.getElementById("DivConfirmedLabel").style.display = 'none';
                document.getElementById("DivCancelLabel").style.display = 'none';
            
            }
        }
       

        function SetSettings(chk) {
            var status = chk.checked;
            $('input:checkbox[name *= uoSelectCheckBox]').attr('checked', status);
        }



        function getQuerystringData(name) {
            var data = {};
            var parameters = window.location.search.substring(1).split("&");
            for (var i = 0, j = parameters.length; i < j; i++) {
                var parameter = parameters[i].split("=");
                var parameterName = decodeURIComponent(parameter[0]);
                var parameterValue = typeof parameter[1] === "undefined" ? parameter[1] : decodeURIComponent(parameter[1]);
                var dataType = typeof data[parameterName];
                if (dataType === "undefined") {
                    data[parameterName] = parameterValue;
                } else if (dataType === "array") {
                    data[parameterName].push(parameterValue);
                } else {
                    data[parameterName] = [data[parameterName]];
                    data[parameterName].push(parameterValue);
                }
            }
            return typeof name === "string" ? data[name] : data;
        }


        function setAddHotel(val) {




            console.log( getQuerystringData("PA") + ' ' + 'Test PortAgetnID');
             
            var count = $('tr', '#uoTblExceptionList').size();

            var chck = $('.Checkbox > input[id*=uoSelectCheckBox]').is(":checked");
            
            if (chck == false) {
                alert("Please Seafarer that required services !!");
                return false; 
            }

            console.log(val);
            
            var mydata = new Array(4);
            var chdate = "";
            
            var canAdd = 0;
            var n = 0;

            for (var i = 0; i < count; i++) {
                var ElId = "ctl00_NaviPlaceHolder_uoListViewNonTurnPort_ctrl" + i;
                if (document.getElementById(ElId + "_uoSelectCheckBox").checked == true) {

                 
 
 
                    mydata[n] = new Array(1);
                    chdate = document.getElementById(ElId + "_uoLblCheckin").innerHTML;
                    mydata[n][1] = document.getElementById(ElId + "_hfIdBigint").value;
                    mydata[n][2] = document.getElementById(ElId + "_hfReqId").value;
                    mydata[n][3] = document.getElementById(ElId + "_hfSeqNo").value;
                    mydata[n][4] = document.getElementById(ElId + "_hfHotelTransID").value;

                    console.log(document.getElementById(ElId + "_hfReqId").value);
                    
                    n = n + 1;
                }
            }


            console.log(mydata);

            if (n > 0) {
                if (val.id == 'uoButtonHotel') {

//                    var href = "../Hotel/HotelEditor5.aspx?Action=Add&AddCancel=Add" + "&dt=" + chdate + "&qTravelReqID=" + mydata + "&pID=" + 0 + "&pRow=" + n ;
                    document.getElementById("uoNonTurnPageRoute").href = "../Hotel/HotelEditor5.aspx?Action=Add&AddCancel=Add" + "&dt=" + chdate + "&qTravelReqID=" + mydata + "&pID=" + 0 + "&pRow=" + n + "&paID=" + getQuerystringData("PA") + "&st=" + getQuerystringData("st");

                    document.getElementById("uoNonTurnPageRoute").click();


                }

                if (val.id == 'uoButtonCancell') {

                    //                    var href = "../Hotel/HotelEditor5.aspx?Action=Add&AddCancel=Add" + "&dt=" + chdate + "&qTravelReqID=" + mydata + "&pID=" + 0 + "&pRow=" + n ;
                    document.getElementById("uoNonTurnPageRoute").href = "../Hotel/HotelEditor5.aspx?Action=Cancel&AddCancel=Cancel" + "&dt=" + chdate + "&qTravelReqID=" + mydata + "&pID=" + 0 + "&pRow=" + n + "&paID=" + getQuerystringData("PA") + "&st=" + getQuerystringData("st");

                    document.getElementById("uoNonTurnPageRoute").click();


                }
                
                
                
                if (val.id == 'uoButtonVehicle') {
                    document.getElementById("uoNonTurnPageRoute").href = "../PortAgent/PortAgentVehicleEditor2.aspx?Action=Add&AddCancel=Add" + "&dt=" + chdate + "&qTravelReqID=" + mydata + "&pID=" + 0 + "&pRow=" + n + "&paID=" + getQuerystringData("PA") + "&st=" + getQuerystringData("st");
                    document.getElementById("uoNonTurnPageRoute").click();
                }
                
                
            }
            
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

        //function divScrollL() {
        //    var Right = document.getElementById('Av');
        //    var Left = document.getElementById('Bv');
        //    Right.scrollLeft = Left.scrollLeft;

        //}

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

        function backToPreviusPage() {

            window.history.back();
        }
        
    </script>
   <%-- <div height="10px">
    &nbsp;
    </div>--%>
    <div style="display:none" >
        <table align="center" width="90%" style="display:none" >
            <tr style="display:none" >
                <td style="width: auto; display:none" align="center">
                    <asp:HyperLink ID="uoHyperLinkDashboard" CssClass="RedBgNotification" runat="server"
                            NavigateUrl="#">Dashboard
                        </asp:HyperLink  >
                      |
                      
                         <asp:HyperLink ID="uoHyperLinkTop50" CssClass="RedBgNotification" runat="server"
                            NavigateUrl="#">Exception Top 50
                        </asp:HyperLink>
                        
                      |
                      
                         <asp:HyperLink ID="uoHyperLinkNonTurn" CssClass="SelectedSubMenuCss" runat="server"
                            NavigateUrl="#">Non-Turn Port
                        </asp:HyperLink>                     
                        
                </td>
            </tr>
        </table>
  
    </div>
    
    <table style="text-align: left" border="0" width="100%">       
        <tr>
            <td style="" >
                <asp:Button style="" runat="server" ID="uoBtnExportList" CssClass="SmallButton" Text="Export Manifest List"
                    OnClick="uoBtnExportList_Click" />
            </td>
            <td style="text-align: left; width:100%; ">
                  <%--<asp:Button style="" runat="server" ID="uoButtonApproveRequest" 
                    CssClass="SmallButton" Text="Approve Reques" OnClick="uoButtonApproveRequest_Click" />
                
                <asp:Button style="" runat="server" ID="uoButtonApproveRequest" 
                    CssClass="SmallButton" Text="Approve Request" OnClick="uoButtonApproveRequest_Click" />
                 --%>  
                 &nbsp; 
               <%-- 
                    <a id="uoLinkRequestHotel"  href="../Hotel/HotelEditor5.aspx?Action=Add&AddCancel=Add">
                       <asp:Button runat="server" ID="uoButtonHotel" Text="Request Hotel" CssClass="SmallButton" onClientClick="setAddHotel()"/>
                    </a>
                --%>
                <%--<asp:Button runat="server" ID="uoButtonHotel" Text="Request Hotel" CssClass="SmallButton" onClientClick="setAddHotel(this)"/>--%>
                <input id="uoButtonHotel" type="button" value="Approve" class="SmallButton" style=""  onclick="setAddHotel(this)"/>
                 &nbsp;
                  <input id="uoButtonCancell" type="button" value="Cancel" class="SmallButton" style="" onclick="setAddHotel(this)"/>
                 &nbsp;
                 
                 <%-- 
                    <a id="A1" href="../PortAgent/PortAgentVehicleEditor.aspx?Action=Add&AddCancel=Add">
                       <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="SmallButton"/>
                    </a>
                --%>
                <%--  
                   <input id="uoButtonVehicle" type="button" value="Request Vehicle" class="SmallButton" 
                        style="" onclick="setAddHotel(this)"/> 
              
                   <a id="uoLinkRequestVehicle" href="../PortAgent/PortAgentVehicleEditor.aspx?Action=Add&AddCancel=Add">
                      <asp:Button runat="server" ID="uoButtonVehicle" Text="Request Vehicle" CssClass="SmallButton"/>
                   </a>
                --%>
                
                  <input id="btnPrevious" type="button" value="Back previous page" class="SmallButton" style="" onclick="backToPreviusPage()"/>
                 &nbsp;
                <a id="uoNonTurnPageRoute" />
                
                
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
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <th class="hideElement">
                            requestInfo
                        </th>
                        <th >
                            <asp:CheckBox runat="server" ID="CheckBox1" Width="19px" CssClass="Checkbox" onclick="SetCheckBoxSettings(this);" />
                        </th>
                        <th style="text-align: center; display:none; ">
                            <%--<asp:Label runat="server" ID="uoLableHotelCity" Text="Hotel City" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableHotelCity" runat="server" CommandName="SortByHotelName"
                                Text="Hotel City" Width="53px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center; display:none;">                            
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByServiceRequest" Text="Service Requested"
                                Width="150px"></asp:LinkButton>
                        </th>
                         <th style="text-align: center; display:none; ">                            
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByServiceRequestDate" Text="Transpo Request Date"
                                Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableCheckin" Text="Checkin" Width="70px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableCheckin" runat="server" CommandName="SortByCheckin" Text="Checkin"
                                Width="63px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableCheckOut" Text="CheckOut" Width="70px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableCheckOut" runat="server" CommandName="SortByCheckOut"
                                Text="CheckOut" Width="65px"></asp:LinkButton>
                        </th>
                        <%--<th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLableHotelNite" Text="Hotel Nite" Width="35px"> </asp:Label>
                        </th>--%>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableEmployee" Text="E1 ID" Width="55px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableEmployee" runat="server" CommandName="SortByE1ID" Text="E1 ID"
                                Width="48px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableName" Text="Name" Width="200px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableName" runat="server" CommandName="SortByLName" Text="Last Name"
                                Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableName" Text="Name" Width="200px"> </asp:Label>--%>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByFName" Text="First Name"
                                Width="98px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableGender" Text="Gender" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableGender" runat="server" CommandName="SortByGender" Text="Gender"
                                Width="44px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableSingleDouble" Text="Single Double" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableSingleDouble" runat="server" CommandName="SortBySingleDouble"
                                Text="Single Double" Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center; display:none;">
                            <%--<asp:Label runat="server" ID="uoLableCouple" Text="Couple" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableCouple" runat="server" CommandName="SortByCouple" Text="Couple"
                                Width="40px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableTitle" Text="Title" Width="150px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableTitle" runat="server" CommandName="SortByTitle" Text="Title"
                                Width="195px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="Label2" Text="Stripe" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableStripe" runat="server" CommandName="SortByStripe" Text="Stripe"
                                Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableShip" Text="Ship" Width="150px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableShip" runat="server" CommandName="SortByShip" Text="Ship"
                                Width="195px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableCostCenter" Text="Cost Center" Width="100px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableCostCenter" runat="server" CommandName="SortByCostCenter"
                                Text="Cost Center" Width="195px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableNationality" Text="Nationality" Width="70px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableNationality" runat="server" CommandName="SortByNationality"
                                Text="Nationality" Width="98px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton33" Text="Birthday" Width="70px" runat="server" CommandName="SortByBirthday" />
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableHotelRequest" Text="Hotel Request" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableHotelRequest" runat="server" CommandName="SortByHotelRequest"
                                Text="Hotel Request" Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableRecLoc" Text="RecLoc" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableRecLoc" runat="server" CommandName="SortByRecLoc" Text="RecLoc"
                                Width="54px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center; display:none;  ">
                            <%--<asp:Label runat="server" ID="uoLableRecLocID" Text="RecLocID" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableRecLocID" runat="server" CommandName="SortByRecLocID"
                                Text="RecLocID" Width="50px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableAirSequence" Text="Air Sequence" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableAirSequence" runat="server" CommandName="SortByAirSequence"
                                Text="Air Sequence" Width="55px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableDepartureCity" Text="Departure City" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableDepartureCity" runat="server" CommandName="SortByDepartureCity"
                                Text="Departure City" Width="58px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableArrivalCity" Text="Arrival City" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableArrivalCity" runat="server" CommandName="SortByArrivalCity"
                                Text="Arrival City" Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableArrivalDate" Text="Arrival Date" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableArrivalDate" runat="server" CommandName="SortByArrivalDate"
                                Text="Arrival Date" Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableArrivalTime" Text="Arrival Time" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableArrivalTime" runat="server" CommandName="SortByArrivalTime"
                                Text="Arrival Time" Width="54px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableCarrier" Text="Carrier" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableCarrier" runat="server" CommandName="SortByCarrier" Text="Carrier"
                                Width="44px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableFlightNo" Text="Flight No" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableFlightNo" runat="server" CommandName="SortByFlightNo"
                                Text="Flight No" Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableVoucher" Text="Voucher" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableVoucher" runat="server" CommandName="SortByVoucher" Text="Voucher"
                                Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLablePassportNo" Text="Passport No" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLablePassportNo" runat="server" CommandName="SortByPassportNo"
                                Text="Passport No" Width="53px" />
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLablePassportExp" Text="Passport Exp" Width="80px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLablePassportExp" runat="server" CommandName="SortByPassportExp"
                                Text="Passport Exp" Width="74px" />
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLablePassportIssued" Text="Passport Issued" Width="80px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLablePassportIssued" runat="server" CommandName="SortByPassportIssued"
                                Text="Passport Issued" Width="73px" />
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableHotelBranch" Text="Hotel Branch" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableHotelBranch" runat="server" CommandName="SortByHotelBranch"
                                Text="Exception Remarks" Width="198px" />
                        </th>
                        <%--  <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLableBooking" Text="Booking" Width="200px"> </asp:Label>
                        </th>--%>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableBookingRemark" Text="Booking Remark" Width="150px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableBookingRemark" runat="server" CommandName="SortByBookingRemark"
                                Text="Booking Remarks" Width="143px" />
                        </th>
                    </tr>
                </table>
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
                <td style="white-space: normal;" >
                    <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" Width="24px" runat="server" Enabled='<%# Eval("IsVisible") %>' />
                </td>
                <td class="hideElement">
                    <asp:HiddenField runat="server" ID="hfHotelTransID" Value='<%# Eval("HotelTransID") %>' />
                    <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IDBigInt") %>' />
                    <asp:HiddenField runat="server" ID="hfSeqNo" Value='<%# Eval("AirSequence") %>' />
                    <asp:HiddenField runat="server" ID="hfReqId" Value='<%# Eval("TravelReqId") %>' />
                    <asp:HiddenField runat="server" ID="hfRoomType" Value='<%# Eval("RoomTypeId") %>' />
                    <asp:HiddenField runat="server" ID="hfPortId" Value='<%# Eval("PortId") %>' />
                    <asp:HiddenField runat="server" ID="uoLblStatus" Value='<%# Eval("SFStatus") %>' />
                    <asp:HiddenField runat="server" ID="uoLblHotelNite" Value='<%# Eval("HotelNite") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal; display:none;">
                    <asp:Label runat="server" ID="uoLblHotelCity" Text='<%# Eval("HotelCity") %>' Width="60px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal; display:none;">
                    <asp:Label runat="server" ID="Label6" Text='<%# Eval("ServiceRequested") %>' Width="153px"  />
                </td>
                <td class="leftAligned" style="white-space: normal;  display:none;">
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
                        Width="52px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal; display:none;">
                    <asp:Label runat="server" ID="uoLblCouple" Text='<%# Eval("Couple")%>' Width="49px"
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
                    <asp:Label runat="server" ID="Label7" Width="76px" Text='<%#String.Format("{0:dd-MMM-yyyy}", Eval("Birthday"))%>'
                        Visible='<%# Eval("IsVisible") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblHotelRequest" Text='<%# Eval("HotelRequest")%>'
                        Width="55px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecLoc")%>' Width="60px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal; display:none;">
                    <asp:Label runat="server" ID="uoLblRecLocID" Text='<%# Eval("RecLocID")%>' Width="55px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblAirSequence" Text='<%# Eval("AirSequence")%>'
                        Width="60px" />
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
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblFlightNo" Text='<%# Eval("FlightNo")%>' Width="50px" />
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
                    <asp:Label runat="server" ID="uoLblHotelBranch" Text='<%# Eval("Booking")%>' Width="205px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblBookingremark" Text='<%# Eval("BookingRemark")%>'
                        Width="150px" Visible='<%# Eval("IsVisible") %>' />
                </td>               
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
   </div>
        
        <%--===================================Begin Confirmed==================================================== --%>
         <br />
        
        <div id="DivConfirmed" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto; position: relative;display:none">
            <div id="DivConfirmedLabel" class="PageSubTitle" style="text-decoration: underline; display:none">
                Confirmed Manifest
            </div>
            <asp:ListView runat="server" ID="uoListViewConfirmedHeader">
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
                            <th style="display:none;">
                                <asp:CheckBox runat="server" ID="CheckBox1" Width="19px" CssClass="Checkbox" onclick="SetCheckBoxSettings(this);" />
                            </th>
                            <th style="text-align: center; display:none; ">
                                <asp:LinkButton ID="uoLableHotelCity" runat="server" CommandName="SortByHotelName"
                                    Text="Hotel City" Width="53px"></asp:LinkButton>
                            </th>
                            <th style="text-align: center; display:none;">                            
                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByServiceRequest" Text="Service Requested"
                                    Width="150px"></asp:LinkButton>
                            </th>
                             <th style="text-align: center; display:none; ">                            
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
                            <th style="text-align: center; display:none;">
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
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton33" Text="Birthday" Width="70px" runat="server" CommandName="SortByBirthday" />
                            </th>
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableHotelRequest" runat="server" CommandName="SortByHotelRequest"
                                    Text="Hotel Request" Width="45px"></asp:LinkButton>
                            </th>
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableRecLoc" runat="server" CommandName="SortByRecLoc" Text="RecLoc"
                                    Width="54px"></asp:LinkButton>
                            </th>
                            <th style="text-align: center; display:none;  ">
                                <asp:LinkButton ID="uoLableRecLocID" runat="server" CommandName="SortByRecLocID"
                                    Text="RecLocID" Width="50px"></asp:LinkButton>
                            </th>
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableAirSequence" runat="server" CommandName="SortByAirSequence"
                                    Text="Air Sequence" Width="55px"></asp:LinkButton>
                            </th>
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableDepartureCity" runat="server" CommandName="SortByDepartureCity"
                                    Text="Departure City" Width="58px"></asp:LinkButton>
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
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableCarrier" runat="server" CommandName="SortByCarrier" Text="Carrier"
                                    Width="44px"></asp:LinkButton>
                            </th>
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableFlightNo" runat="server" CommandName="SortByFlightNo"
                                    Text="Flight No" Width="45px"></asp:LinkButton>
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
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableHotelBranch" runat="server" CommandName="SortByHotelBranch"
                                    Text="Exception Remarks" Width="198px" />
                            </th> 
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableBookingRemark" runat="server" CommandName="SortByBookingRemark"
                                    Text="Booking Remarks" Width="143px" />
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <asp:ListView runat="server" ID="uoListViewConfirmed">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList"
                        width="100%">
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%# DashboardChangeRowColor()%>
                    <td style="white-space: normal; display:none; " >
                        <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" Width="24px" runat="server" Enabled='<%# Eval("IsVisible") %>' />
                    </td>
                    <td class="hideElement">
                        <asp:HiddenField runat="server" ID="hfHotelTransID" Value='<%# Eval("HotelTransID") %>' />
                        <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IDBigInt") %>' />
                        <asp:HiddenField runat="server" ID="hfSeqNo" Value='<%# Eval("AirSequence") %>' />
                        <asp:HiddenField runat="server" ID="hfReqId" Value='<%# Eval("TravelReqId") %>' />
                        <asp:HiddenField runat="server" ID="hfRoomType" Value='<%# Eval("RoomTypeId") %>' />
                        <asp:HiddenField runat="server" ID="hfPortId" Value='<%# Eval("PortId") %>' />
                        <asp:HiddenField runat="server" ID="uoLblStatus" Value='<%# Eval("SFStatus") %>' />
                        <asp:HiddenField runat="server" ID="uoLblHotelNite" Value='<%# Eval("HotelNite") %>' />
                    </td>
                    <td class="leftAligned" style="white-space: normal; display:none;">
                        <asp:Label runat="server" ID="uoLblHotelCity" Text='<%# Eval("HotelCity") %>' Width="60px"
                            Visible='<%# Eval("IsVisible") %>' />
                    </td>
                    <td class="leftAligned" style="white-space: normal; display:none;">
                        <asp:Label runat="server" ID="Label6" Text='<%# Eval("ServiceRequested") %>' Width="153px"  />
                    </td>
                    <td class="leftAligned" style="white-space: normal;  display:none;">
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
                            Width="52px" Visible='<%# Eval("IsVisible") %>' />
                    </td>
                    <td class="leftAligned" style="white-space: normal; display:none;">
                        <asp:Label runat="server" ID="uoLblCouple" Text='<%# Eval("Couple")%>' Width="49px"
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
                        <asp:Label runat="server" ID="Label7" Width="76px" Text='<%#String.Format("{0:dd-MMM-yyyy}", Eval("Birthday"))%>'
                            Visible='<%# Eval("IsVisible") %>'></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblHotelRequest" Text='<%# Eval("HotelRequest")%>'
                            Width="55px" Visible='<%# Eval("IsVisible") %>' />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecLoc")%>' Width="60px"
                            Visible='<%# Eval("IsVisible") %>' />
                    </td>
                    <td class="leftAligned" style="white-space: normal; display:none;">
                        <asp:Label runat="server" ID="uoLblRecLocID" Text='<%# Eval("RecLocID")%>' Width="55px"
                            Visible='<%# Eval("IsVisible") %>' />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblAirSequence" Text='<%# Eval("AirSequence")%>'
                            Width="60px" />
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
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="50px" />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblFlightNo" Text='<%# Eval("FlightNo")%>' Width="50px" />
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
                        <asp:Label runat="server" ID="uoLblHotelBranch" Text='<%# Eval("Booking")%>' Width="205px"
                            Visible='<%# Eval("IsVisible") %>' />
                    </td>                
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblBookingremark" Text='<%# Eval("BookingRemark")%>'
                            Width="150px" Visible='<%# Eval("IsVisible") %>' />
                    </td>               
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
        </div>
        
        <%--===================================End Confirmed==================================================== --%>
        <br />
         <div id="DivCancelLabel" class="PageSubTitle" style="text-decoration: underline; display:none">
            Cancelled Manifest
        </div>
        <div id="DivCancelled" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto; position: relative;display:none">
       
            <asp:ListView runat="server" ID="uoListViewCancelHeader">
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
                            <th  style="display:none;">
                                <asp:CheckBox runat="server" ID="CheckBox1" Width="19px" CssClass="Checkbox" onclick="SetCheckBoxSettings(this);" />
                            </th>
                            <th style="text-align: center; display:none; ">
                                <asp:LinkButton ID="uoLableHotelCity" runat="server" CommandName="SortByHotelName"
                                    Text="Hotel City" Width="53px"></asp:LinkButton>
                            </th>
                            <th style="text-align: center; display:none;">                            
                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByServiceRequest" Text="Service Requested"
                                    Width="150px"></asp:LinkButton>
                            </th>
                             <th style="text-align: center; display:none; ">                            
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
                            <th style="text-align: center; display:none;">
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
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton33" Text="Birthday" Width="70px" runat="server" CommandName="SortByBirthday" />
                            </th>
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableHotelRequest" runat="server" CommandName="SortByHotelRequest"
                                    Text="Hotel Request" Width="45px"></asp:LinkButton>
                            </th>
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableRecLoc" runat="server" CommandName="SortByRecLoc" Text="RecLoc"
                                    Width="54px"></asp:LinkButton>
                            </th>
                            <th style="text-align: center; display:none;  ">
                                <asp:LinkButton ID="uoLableRecLocID" runat="server" CommandName="SortByRecLocID"
                                    Text="RecLocID" Width="50px"></asp:LinkButton>
                            </th>
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableAirSequence" runat="server" CommandName="SortByAirSequence"
                                    Text="Air Sequence" Width="55px"></asp:LinkButton>
                            </th>
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableDepartureCity" runat="server" CommandName="SortByDepartureCity"
                                    Text="Departure City" Width="58px"></asp:LinkButton>
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
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableCarrier" runat="server" CommandName="SortByCarrier" Text="Carrier"
                                    Width="44px"></asp:LinkButton>
                            </th>
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableFlightNo" runat="server" CommandName="SortByFlightNo"
                                    Text="Flight No" Width="45px"></asp:LinkButton>
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
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableHotelBranch" runat="server" CommandName="SortByHotelBranch"
                                    Text="Exception Remarks" Width="198px" />
                            </th> 
                            <th style="text-align: center;">
                                <asp:LinkButton ID="uoLableBookingRemark" runat="server" CommandName="SortByBookingRemark"
                                    Text="Booking Remarks" Width="143px" />
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <asp:ListView runat="server" ID="uoListViewCancelledList">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList"
                        width="100%">
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%# DashboardChangeRowColor()%>
                    <td style="white-space: normal; display:none; " >
                        <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" Width="24px" runat="server" Enabled='<%# Eval("IsVisible") %>' />
                    </td>
                    <td class="hideElement">
                        <asp:HiddenField runat="server" ID="hfHotelTransID" Value='<%# Eval("HotelTransID") %>' />
                        <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IDBigInt") %>' />
                        <asp:HiddenField runat="server" ID="hfSeqNo" Value='<%# Eval("AirSequence") %>' />
                        <asp:HiddenField runat="server" ID="hfReqId" Value='<%# Eval("TravelReqId") %>' />
                        <asp:HiddenField runat="server" ID="hfRoomType" Value='<%# Eval("RoomTypeId") %>' />
                        <asp:HiddenField runat="server" ID="hfPortId" Value='<%# Eval("PortId") %>' />
                        <asp:HiddenField runat="server" ID="uoLblStatus" Value='<%# Eval("SFStatus") %>' />
                        <asp:HiddenField runat="server" ID="uoLblHotelNite" Value='<%# Eval("HotelNite") %>' />
                    </td>
                    <td class="leftAligned" style="white-space: normal; display:none;">
                        <asp:Label runat="server" ID="uoLblHotelCity" Text='<%# Eval("HotelCity") %>' Width="60px"
                            Visible='<%# Eval("IsVisible") %>' />
                    </td>
                    <td class="leftAligned" style="white-space: normal; display:none;">
                        <asp:Label runat="server" ID="Label6" Text='<%# Eval("ServiceRequested") %>' Width="153px"  />
                    </td>
                    <td class="leftAligned" style="white-space: normal;  display:none;">
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
                            Width="52px" Visible='<%# Eval("IsVisible") %>' />
                    </td>
                    <td class="leftAligned" style="white-space: normal; display:none;">
                        <asp:Label runat="server" ID="uoLblCouple" Text='<%# Eval("Couple")%>' Width="49px"
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
                        <asp:Label runat="server" ID="Label7" Width="76px" Text='<%#String.Format("{0:dd-MMM-yyyy}", Eval("Birthday"))%>'
                            Visible='<%# Eval("IsVisible") %>'></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblHotelRequest" Text='<%# Eval("HotelRequest")%>'
                            Width="55px" Visible='<%# Eval("IsVisible") %>' />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecLoc")%>' Width="60px"
                            Visible='<%# Eval("IsVisible") %>' />
                    </td>
                    <td class="leftAligned" style="white-space: normal; display:none;">
                        <asp:Label runat="server" ID="uoLblRecLocID" Text='<%# Eval("RecLocID")%>' Width="55px"
                            Visible='<%# Eval("IsVisible") %>' />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblAirSequence" Text='<%# Eval("AirSequence")%>'
                            Width="60px" />
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
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="50px" />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblFlightNo" Text='<%# Eval("FlightNo")%>' Width="50px" />
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
                        <asp:Label runat="server" ID="uoLblHotelBranch" Text='<%# Eval("Booking")%>' Width="205px"
                            Visible='<%# Eval("IsVisible") %>' />
                    </td>                
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblBookingremark" Text='<%# Eval("BookingRemark")%>'
                            Width="150px" Visible='<%# Eval("IsVisible") %>' />
                    </td>               
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
        </div>
        
        
        
        
        
        
       
        <asp:ListView runat="server" ID="uoListViewHeaderCancelleds" style="display:none; " Visible="false"  >
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
                                Width="100px"></asp:LinkButton>
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
                                Width="43px"></asp:LinkButton>
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
                                Text="Nationality" Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton33" Text="Birthday" Width="70px" runat="server" CommandName="SortByBirthday" />
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableHotelRequest" runat="server" CommandName="SortByHotelRequest"
                                Text="Hotel Request" Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableRecLoc" runat="server" CommandName="SortByRecLoc" Text="RecLoc"
                                Width="54px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableRecLocID" runat="server" CommandName="SortByRecLocID"
                                Text="RecLocID" Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableAirSequence" runat="server" CommandName="SortByAirSequence"
                                Text="Air Sequence" Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableDepartureCity" runat="server" CommandName="SortByDepartureCity"
                                Text="Departure City" Width="43px"></asp:LinkButton>
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
                        <th style="text-align: center;">
                            <asp:LinkButton ID="uoLableCarrier" runat="server" CommandName="SortByCarrier" Text="Carrier"
                                Width="44px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableFlightNo" Text="Flight No" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableFlightNo" runat="server" CommandName="SortByFlightNo"
                                Text="Flight No" Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableVoucher" Text="Voucher" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableVoucher" runat="server" CommandName="SortByVoucher" Text="Voucher"
                                Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLablePassportNo" Text="Passport No" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLablePassportNo" runat="server" CommandName="SortByPassportNo"
                                Text="Passport No" Width="53px" />
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLablePassportExp" Text="Passport Exp" Width="80px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLablePassportExp" runat="server" CommandName="SortByPassportExp"
                                Text="Passport Exp" Width="74px" />
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLablePassportIssued" Text="Passport Issued" Width="80px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLablePassportIssued" runat="server" CommandName="SortByPassportIssued"
                                Text="Passport Issued" Width="73px" />
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableHotelBranch" Text="Hotel Branch" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableHotelBranch" runat="server" CommandName="SortByHotelBranch"
                                Text="Exception Remarks" Width="198px" />
                        </th>
                        <%--  <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLableBooking" Text="Booking" Width="200px"> </asp:Label>
                        </th>--%>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableBookingRemark" Text="Booking Remark" Width="150px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableBookingRemark" runat="server" CommandName="SortByBookingRemark"
                                Text="Booking Remarks" Width="143px" />
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <asp:ListView runat="server" ID="uoListViewCancelled" style="display:none; " Visible="false" >
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
                    <asp:Label runat="server" ID="Label7" Width="76px" Text='<%#String.Format("{0:dd-MMM-yyyy}", Eval("Birthday"))%>'
                        Visible='<%# Eval("IsVisible") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblHotelRequest" Text='<%# Eval("HotelRequest")%>'
                        Width="50px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecLoc")%>' Width="60px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRecLocID" Text='<%# Eval("RecLocID")%>' Width="50px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblAirSequence" Text='<%# Eval("AirSequence")%>'
                        Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLbldeptCity" Text='<%# Eval("deptCity")%>' Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("ArvlCity")%>' Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <%--<asp:Label runat="server"  ID="uoLblArvldate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("Arvldate"))%>' Width="60px"/> --%>
                    <asp:Label runat="server" ID="uoLblArvldate" Text='<%# Eval("Arvldate")%>' Width="105px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblArvlTime" Text='<%#  Eval("ArvlTime")%>' Width="60px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="50px" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblFlightNo" Text='<%# Eval("FlightNo")%>' Width="50px" />
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
                    <asp:Label runat="server" ID="uoLblHotelBranch" Text='<%# Eval("Booking")%>' Width="205px"
                        Visible='<%# Eval("IsVisible") %>' />
                </td>
                <%--  <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblBooking" Text='<%# Eval("Booking")%>' Width="200px"/>
                </td>--%>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblBookingremark" Text='<%# Eval("BookingRemark")%>'
                        Width="150px" Visible='<%# Eval("IsVisible") %>' />
                </td>
                <%--   <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="Label1" Text='<%# Eval("IsVisible")%>' Width="150px"/>
                </td>--%>
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
    <asp:HiddenField runat="server" ID="uoHiddenFieldHotelConfirm" EnableViewState="true" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldVehicleConfirm" EnableViewState="true" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDateTime" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldPortAgentID" Value="0"/>
    
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
    <script language="javascript" type="text/javascript">
         Sys.Application.add_load(function() {
             ControlSettings();
         });


          

         document.getElementById("ctl00_uoTblDate").value = document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldDate").value;
         
         
         
         function ControlSettings() {
            $("a#uoLinkRequestHotel").fancybox(
            {
                'centerOnScroll': false,
                'width': '80%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                var a = $("#<%=uoHiddenFieldHotelConfirm.ClientID %>").val();

                    if (a == '2') {
                        $("#aspnetForm").submit();
                        alert('Email successfully sent!');
                    }
                    else {
                        $("#aspnetForm").submit();
                    }
                }
            });

            $("a#uoLinkRequestVehicle").fancybox(
            {
                'centerOnScroll': false,
                'width': '80%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldVehicleConfirm.ClientID %>").val();

                    if (a == '2') {
                        $("#aspnetForm").submit();
                        alert('Email successfully sent!');
                    }
                    else {
                        $("#aspnetForm").submit();
                    }
                }
            });



            $("a#uoNonTurnPageRoute").fancybox(
            {
                'centerOnScroll': false,
                'width': '65%',
                'height': '84%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldVehicleConfirm.ClientID %>").val();

                    if (a == '2') {
                        $("#aspnetForm").submit();
                        alert('Email successfully sent!');
                    }
                    else {
                        $("#aspnetForm").submit();
                    }
                }
            });
            
//            ctl00_uoTblDate
            
            
        }
         
    </script>
</asp:Content>
