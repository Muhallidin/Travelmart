 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TravelMartMaster2.Master"
    CodeBehind="VehicleManifestByVendor.aspx.cs" Inherits="TRAVELMART.Vehicle.VehicleManifestByVendor"
    Trace="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" style="height:25px">
        <tr class="PageTitle">
            <td align="left">
                Vehicle Manifest
            </td>
            <td align="right">
                 <asp:Label ID="Label5" runat="server" Text="Brand:" Font-Size="14px"></asp:Label>&nbsp;
                    <asp:DropDownList ID="uoDropDownListBrand" runat="server" Width="150px" 
                        AutoPostBack="true" CssClass="SmallText" AppendDataBoundItems="true"
                        onselectedindexchanged="uoDropDownListBrand_SelectedIndexChanged">
                    </asp:DropDownList>                    
                    &nbsp;&nbsp
                    <asp:Label ID="Label6" runat="server" Text="Vessel:" Font-Size="14px"></asp:Label>&nbsp;
                    <asp:DropDownList ID="uoDropDownListVessel" runat="server" Width="150px" 
                        AutoPostBack="true" CssClass="SmallText" AppendDataBoundItems="true"
                        onselectedindexchanged="uoDropDownListVessel_SelectedIndexChanged">
                    </asp:DropDownList>                    
                    &nbsp;&nbsp
                <asp:Label ID="Label42" runat="server" Text="Vehicle Vendor:" Font-Size="14px"></asp:Label>&nbsp;
                <asp:DropDownList runat="server" ID="uoDropDownListVehicle" Width="300px" AppendDataBoundItems="true"
                    CssClass="SmallText" AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListVehicle_SelectedIndexChanged"></asp:DropDownList>
                <%--<div runat="server" id="uoDivRegionPort" style="visibility: hidden">
                    Region: &nbsp;&nbsp;
                    <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="200px" AppendDataBoundItems="true"
                        AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp; Port: &nbsp;&nbsp;
                    <asp:DropDownList ID="uoDropDownListPort" runat="server" Width="200px" AppendDataBoundItems="true"
                        AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListPort_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>--%>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
              
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetTMResolution();
            RouteSettings();
            ShowPopupPotentialSignOFFVehicle();
            SetConfirmedButton();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTMResolution();
                RouteSettings();
                ShowPopupPotentialSignOFFVehicle();
                SetConfirmedButton();       
            }
        }


        function SetConfirmedButton() {


            if (document.getElementById('ctl00_NaviPlaceHolder_uoHiddenFieldRole').value == "Driver") {
                document.getElementById("ctl00_NaviPlaceHolder_uoButtonExport").style.display = "none";
                document.getElementById("ctl00_NaviPlaceHolder_uoButtonConfirm").style.display = "none";
                return;
            }
            if (document.getElementById('ctl00_NaviPlaceHolder_uoHiddenFieldRole').value == "Greeter") {
                document.getElementById("ctl00_NaviPlaceHolder_uoButtonExport").style.display = "none";
                document.getElementById("ctl00_NaviPlaceHolder_uoButtonConfirm").style.display = "none";
                return;
            }
            else { 
            
                console.log($("#ConfirmedTable > tbody > tr").length);
                console.log($("#uoButtonAssignConfirm"));
                
                if ($("#ConfirmedTable > tbody > tr").length > 0) {
                    $("#uoButtonAssignConfirm")[0].style.display = "block";
                    $("#uoButtonAssignConfirmMeetAndGreet")[0].style.display = "block";
                } 
                else {
                    $("#uoButtonAssignConfirm")[0].style.display = "none";
                    $("#uoButtonAssignConfirmMeetAndGreet")[0].style.display = "none";
                }
            
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
                ht2 = ht2 * 0.65;
            }
            else {
                ht = ht * 0.45;
                ht2 = ht2 * 0.62;
            }
            $("#Bv").height(ht);
            //            $("#PG").height(ht2);
            $("#Av").width(wd);
            $("#Bv").width(wd);
            //            $("#PG").width(wd);

            $("#ucDivConfirmB").height(ht);
            $("#ucDivConfirmA").width(wd);
            $("#ucDivConfirmB").width(wd);

            $("#ucDivCancelB").height(ht);
            $("#ucDivCancelA").width(wd);
            $("#ucDivCancelB").width(wd);
        }

        function divScrollLManifest() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;
        }

        function divScrollLManifestConfirm() {
            var Right = document.getElementById('ucDivConfirmA');
            var Left = document.getElementById('ucDivConfirmB');
            Right.scrollLeft = Left.scrollLeft;
        }

        function divScrollLManifestCancel() {
            var Right = document.getElementById('ucDivCancelA');
            var Left = document.getElementById('ucDivCancelB');
            Right.scrollLeft = Left.scrollLeft;
        }
        function goAssign() {
            var BigInt = "";
            var AllBigInt = "";
            var VendorID = "";
            var sUserName = "";
            var dDate = "";


            for (var j = 0; j < $("[id$=uoCheckBoxShowVehicle0]").length; j++) {
                //alert(j);
                if (document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestDetails_ctrl" + j + "_uoCheckBoxShowVehicle0").checked == true) {
                    BigInt = document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestDetails_ctrl" + j + "_uoHiddenFieldUnAssignedIDs").value;
                    AllBigInt += BigInt + ",";
                }
            }

            // alert(AllBigInt);
            // $("#<%=uoHiddenFieldCrewIDs.ClientID %>").val(AllBigInt);
            VendorID = $("#<%=uoHiddenFieldVehicle.ClientID %>").val();
            sUserName = $("#<%=uoHiddenFieldUser.ClientID %>").val();
            dDate = $("#<%=uoHiddenFieldDate.ClientID %>").val();
            $("#<%=uoHyperLinkAssignVehicle.ClientID %>").attr('href', "/Vehicle/VehicleAssignment.aspx?status=new&vendorID=" + VendorID + "&cIds=" + AllBigInt + "&dDate=" + dDate + "&sUserID=" + sUserName);
        }

        function goAssignConfirm() {
            var BigInt = "";
            var AllBigInt = "";
            var VendorID = "";
            var sUserName = "";
            var dDate = "";
            var cfm = "";
            for (var j = 0; j < $("[id$=uoCheckBoxShowVehicle1]").length; j++) {
                
                //alert(j);
                if (document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestConfirm_ctrl" + j + "_uoCheckBoxShowVehicle1").checked == true) {
                    
                    BigInt = document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestConfirm_ctrl" + j + "_uoHiddenFieldConfirmIDs").value;
                    AllBigInt += BigInt + ",";
                    
                    cfm +=  document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestConfirm_ctrl" + j + "_uoHiddenFieldConfirmedManifestID").value + ",";
                    
                }
            }


            if (cfm == "") {
                alert("Please select confirmed manifest!!");
                return false;
            }
            
            
            console.log(cfm.substring(0, cfm.length - 1));
            //alert(AllBigInt);
            $("#<%=uoHiddenFieldCrewIDs.ClientID %>").val(AllBigInt);
            VendorID = $("#<%=uoHiddenFieldVehicle.ClientID %>").val();
            sUserName = $("#<%=uoHiddenFieldUser.ClientID %>").val();
            dDate = $("#<%=uoHiddenFieldDate.ClientID %>").val();
            $("#<%=uoHyperLinkAssignVehicleConfirm.ClientID %>").attr('href', "/Vehicle/VehicleAssignment.aspx?status=confirmed&vendorID=" + VendorID + "&cIds=" + AllBigInt + "&dDate=" + dDate + "&sUserID=" + sUserName + "&cmfid=" + cfm.substring(0, cfm.length - 1) + "&isVeh=1&isGre=0");
            $("#<%=uoHyperLinkAssignVehicleConfirm.ClientID %>").click();
        }


        function goAssignConfirmMeetAndGreet() {
            var BigInt = "";
            var AllBigInt = "";
            var VendorID = "";
            var sUserName = "";
            var dDate = "";
            var cfm = "";
            for (var j = 0; j < $("[id$=uoCheckBoxShowVehicle1]").length; j++) {

                //alert(j);
                if (document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestConfirm_ctrl" + j + "_uoCheckBoxShowVehicle1").checked == true) {

                    BigInt = document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestConfirm_ctrl" + j + "_uoHiddenFieldConfirmIDs").value;
                    AllBigInt += BigInt + ",";

                    cfm += document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestConfirm_ctrl" + j + "_uoHiddenFieldConfirmedManifestID").value + ",";

                }
            }



            if (cfm == "") {
                alert("Please select confirmed manifest!!");
                return false;
            }
            
            console.log(cfm.substring(0, cfm.length - 1));
            //alert(AllBigInt);
            $("#<%=uoHiddenFieldCrewIDs.ClientID %>").val(AllBigInt);
            VendorID = $("#<%=uoHiddenFieldVehicle.ClientID %>").val();
            sUserName = $("#<%=uoHiddenFieldUser.ClientID %>").val();
            dDate = $("#<%=uoHiddenFieldDate.ClientID %>").val();
            $("#<%=uoHyperLinkAssignVehicleConfirmAndGreet.ClientID %>").attr('href', "/Vehicle/VehicleAssignment.aspx?status=confirmed&vendorID=" + VendorID + "&cIds=" + AllBigInt + "&dDate=" + dDate + "&sUserID=" + sUserName + "&cmfid=" + cfm.substring(0, cfm.length - 1) + "&isVeh=0&isGre=1");
            $("#<%=uoHyperLinkAssignVehicleConfirmAndGreet.ClientID %>").click();
        }


        function CheckVehicleUnAssign(obj) {
            var name = obj.id.replace("_uoCheckBoxShowVehicle0", "");
            var counter = name.replace("ctl00_NaviPlaceHolder_uoListViewManifestDetails_ctrl", "");

            if (document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestDetails_ctrl" + counter + "_uoCheckBoxShowVehicle0").checked == true) {
                document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestDetails_ctrl" + counter + "_uoCheckBoxShow").checked = true;
            }
            else {
                document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestDetails_ctrl" + counter + "_uoCheckBoxShow").checked = false;
            }

        }

        function CheckVehicleConfirm(obj) {
            var name = obj.id.replace("_uoCheckBoxShowVehicle1", "");
            var counter = name.replace("ctl00_NaviPlaceHolder_uoListViewManifestConfirm_ctrl", "");
            if (document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestConfirm_ctrl" + counter + "_uoCheckBoxShowVehicle1").checked == true) {
                document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestConfirm_ctrl" + counter + "_uoCheckBoxShow").checked = true;
            }
            else {
                document.getElementById("ctl00_NaviPlaceHolder_uoListViewManifestConfirm_ctrl" + counter + "_uoCheckBoxShow").checked = false;
            }
        }

        function SetSettings(chk) {
            var obj = chk.id;
            var name = chk.id.substring(0, chk.id.length - 11);
            var ctr = chk.id.substring(chk.id.length, chk.id.length - 1);
            name = name.replace("ctl00_NaviPlaceHolder_uoListViewManifestHeader", "ctl00_NaviPlaceHolder_uoListViewManifestDetails");
            //alert(name + ":" + ctr);
            if (chk.checked == true) {
                for (var j = 0; j < $("[id$=uoCheckBoxShowVehicle" + ctr + "]").length; j++) {
                    (document.getElementById(name + j + "_uoCheckBoxShowVehicle" + ctr).checked = true)
                }
            }
            else {
                for (var j = 0; j < $("[id$=uoCheckBoxShowVehicle" + ctr + "]").length; j++) {
                    (document.getElementById(name + j + "_uoCheckBoxShowVehicle" + ctr).checked = false)
                }
            }
            goAssign();
            //alert($("#<%=uoHiddenFieldCrewIDs.ClientID %>").val());
        }

        function SetSettingsConfirm(chk) {
            var obj = chk.id;
            var name = chk.id.substring(0, chk.id.length - 11);
            var ctr = chk.id.substring(chk.id.length, chk.id.length - 1);
            name = name.replace("ctl00_NaviPlaceHolder_uoListViewHeaderConfirm", "ctl00_NaviPlaceHolder_uoListViewManifestConfirm");
            //alert(name + ":" + ctr);
            if (chk.checked == true) {
                for (var j = 0; j < $("[id$=uoCheckBoxShowVehicle" + ctr + "]").length; j++) {
                    (document.getElementById(name + j + "_uoCheckBoxShowVehicle" + ctr).checked = true)
                }
            }
            else {
                for (var j = 0; j < $("[id$=uoCheckBoxShowVehicle" + ctr + "]").length; j++) {
                    (document.getElementById(name + j + "_uoCheckBoxShowVehicle" + ctr).checked = false)
                }
            }
//            goAssignConfirm();
            //alert($("#<%=uoHiddenFieldCrewIDs.ClientID %>").val());
        }

        function RouteSettings() {
            $("#<%=uoDropDownListRouteFrom.ClientID %>").change(function(e) {                
                var iRoute = $(this).val();

                $("#<%=uoHiddenFieldRouteFrom.ClientID %>").val(iRoute);

                if (iRoute == 1) {
                    GetCity(1, 0);
                }
                if (iRoute == 2 || iRoute == 3) {
                    GetCity(1, 1);
                }
            });

            $("#<%=uoDropDownListRouteTo.ClientID %>").change(function(e) {                
                var iRoute = $(this).val();
                
                $("#<%=uoHiddenFieldRouteTo.ClientID %>").val(iRoute);
                
                if (iRoute == 1) {
                    GetCity(0, 0);
                }
                if (iRoute == 2 || iRoute == 3) {
                    GetCity(0, 1);
                }
            });
            

            $("#<%=uoDropDownListCityFrom.ClientID %>").change(function(e) {
                var iCityFrom = $(this).val();
                $("#<%=uoHiddenFieldCityFrom.ClientID %>").val(iCityFrom);
            });
            
            $("#<%=uoDropDownListCityTo.ClientID %>").change(function(e) {
                var iCityTo = $(this).val();
                $("#<%=uoHiddenFieldCityTo.ClientID %>").val(iCityTo);
            });
        }

        function GetCity(IsFromCity, IsAirport) {
            var iVendorID = $("#<%=uoHiddenFieldVehicle.ClientID %>").val();
            if (IsFromCity == 1) {
                var ddlFromCity = $("#<%=uoDropDownListCityFrom.ClientID %>");
                if (IsAirport == 1) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/PageMethods.aspx/GetVehicleVendorAirportList",
                        data: "{'iVehicleVendor': '" + iVendorID  + "', 'iLoadType': '0'}",
                        dataType: "json",
                        success: function(data) {

                            //remove all the options in dropdown
                            $("#<%=uoDropDownListCityFrom.ClientID %>> option").remove();

                            //add option in dropdown
                            $("<option value='PROCESSING'>PROCESSING...</option>").appendTo(ddlFromCity);
                            $("<option value='0'>--SELECT FROM CITY--</option>").appendTo(ddlFromCity);

                            for (var i = 0; i < data.d.length; i++) {
                                //add the data coming from the result
                                $("<option value='" + data.d[i].AirportCodeString + "'>" + data.d[i].AirportNameString + "</option>").appendTo(ddlFromCity);
                            }
                            $("#<%=uoDropDownListCityFrom.ClientID %>> option[value='PROCESSING']").remove();
                        },
                        error: function(objXMLHttpRequest, textStatus, errorThrown) {
                            alert(errorThrown);
                        }
                    });
                }
                else { //select seaport                    
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/PageMethods.aspx/GetVehicleVendorSeaportList",
                        data: "{'iVehicleVendor': '" + iVendorID + "', 'iLoadType': '0'}",
                        dataType: "json",
                        success: function(data) {

                            //remove all the options in dropdown
                            $("#<%=uoDropDownListCityFrom.ClientID %>> option").remove();

                            //add option in dropdown
                            $("<option value='PROCESSING'>PROCESSING...</option>").appendTo(ddlFromCity);
                            $("<option value='0'>--SELECT FROM CITY--</option>").appendTo(ddlFromCity);

                            for (var i = 0; i < data.d.length; i++) {
                                //add the data coming from the result
                                $("<option value='" + data.d[i].SeaportCodeString + "'>" + data.d[i].SeaportNameString + "</option>").appendTo(ddlFromCity);
                            }
                            $("#<%=uoDropDownListCityFrom.ClientID %>> option[value='PROCESSING']").remove();
                        },
                        error: function(objXMLHttpRequest, textStatus, errorThrown) {
                            alert('error encountered');
                        }
                    });
                }
            }
            else //  if (IsFromCity == 0) To city settings
            {
                var ddlToCity = $("#<%=uoDropDownListCityTo.ClientID %>");
                if (IsAirport == 1) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/PageMethods.aspx/GetVehicleVendorAirportList",
                        data: "{'iVehicleVendor': '" + iVendorID + "', 'iLoadType': '0'}",
                        dataType: "json",
                        success: function(data) {

                            //remove all the options in dropdown
                            $("#<%=uoDropDownListCityTo.ClientID %>> option").remove();

                            //add option in dropdown
                            $("<option value='PROCESSING'>PROCESSING...</option>").appendTo(ddlToCity);
                            $("<option value='0'>--SELECT FROM CITY--</option>").appendTo(ddlToCity);

                            for (var i = 0; i < data.d.length; i++) {
                                //add the data coming from the result
                                $("<option value='" + data.d[i].AirportCodeString + "'>" + data.d[i].AirportNameString + "</option>").appendTo(ddlToCity);
                            }
                            $("#<%=uoDropDownListCityTo.ClientID %>> option[value='PROCESSING']").remove();
                        },
                        error: function(objXMLHttpRequest, textStatus, errorThrown) {
                            alert(errorThrown);
                        }
                    });
                }
                else { //select seaport                    
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/PageMethods.aspx/GetVehicleVendorSeaportList",
                        data: "{'iVehicleVendor': '" + iVendorID + "', 'iLoadType': '0'}",
                        dataType: "json",
                        success: function(data) {

                            //remove all the options in dropdown
                            $("#<%=uoDropDownListCityTo.ClientID %>> option").remove();

                            //add option in dropdown
                            $("<option value='PROCESSING'>PROCESSING...</option>").appendTo(ddlToCity);
                            $("<option value='0'>--SELECT TO CITY--</option>").appendTo(ddlToCity);

                            for (var i = 0; i < data.d.length; i++) {
                                //add the data coming from the result
                                $("<option value='" + data.d[i].SeaportCodeString + "'>" + data.d[i].SeaportNameString + "</option>").appendTo(ddlToCity);
                            }
                            $("#<%=uoDropDownListCityTo.ClientID %>> option[value='PROCESSING']").remove();
                        },
                        error: function(objXMLHttpRequest, textStatus, errorThrown) {
                            alert('error encountered');
                        }
                    });
                }
            }
        }

        function GOPotentialSignOFF() {
            var BigInt = "";
            var AllBigInt = "";
            var VendorID = "";
            var sUserName = "";
            var dDate = "";
            var sVName = "";
            VendorID = $("#<%=uoHiddenFieldVehicle.ClientID %>").val();
            sUserName = $("#<%=uoHiddenFieldUser.ClientID %>").val();
            dDate = $("#<%=uoHiddenFieldDate.ClientID %>").val();

//            dDate = $("#<%=uoHiddenFieldDate.ClientID %>").val();
           sVName = document.getElementById("ctl00_HeaderContent_uoLabelVendorName").innerHTML;
           $("#<%=uoHyperLinkPotentialSignOff.ClientID %>").attr('href', "/Vehicle/PotentialSignOff.aspx?VID=" + VendorID + "&dDate=" + dDate + "&sUserID=" + sUserName + "&sVN=" + sVName);
        }

        function ShowPopupPotentialSignOFFVehicle() {

            $(".PotentialPopUpVehiclePage").fancybox(
        {
//            'width': '97%',
            //            'height': '97%',

            'width': '250px',
            'height': '80%',
            
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
            'onClosed': function() {
                var a = $("#<%=uoHiddenFieldServicePopup.ClientID %>").val();
                if (a == '1')
                    $("#aspnetForm").submit();
            }
        });
        }


        function GetStatus(val) {

            $("#<%=uoHiddenFieldStatus.ClientID %>").val(val);

            switch (val) {
                case 'ON':
                    document.getElementById('ctl00_NaviPlaceHolder_uoRadioButtonAll').checked = false;
                    document.getElementById('ctl00_NaviPlaceHolder_uoRadioButtonOff').checked = false;
                    break;
                case 'OFF':
                    document.getElementById('ctl00_NaviPlaceHolder_uoRadioButtonAll').checked = false;
                    document.getElementById('ctl00_NaviPlaceHolder_uoRadioButtonOn').checked = false;
                    break;
                default:
                    document.getElementById('ctl00_NaviPlaceHolder_uoRadioButtonOff').checked = false;
                    document.getElementById('ctl00_NaviPlaceHolder_uoRadioButtonOn').checked = false;
            }
        }             
    </script>

    <div id="PG" style="width: auto; height: auto; overflow: auto;">
        <%--==================================New MAnifest Start================================================--%>
        <%-- <div runat="server" id="PendingDiv" class="PageSubTitle" style="text-decoration: underline;">
            Confirmed Manifest
    </div>--%>
        <div style="padding-top:-50px;">
            <table width="100%" >
                
                <tr>                   
                    <td class="LeftClass" >
                        <table style="width: 100%"  > 
                            <tr>
                                <td  class="LeftClass"  style="width:100px;">
                                    Filter Status:
                                </td>
                                <td  class="LeftClass" colspan="3">
                                    <asp:RadioButton runat="server" id="uoRadioButtonAll" Checked="true" Text="ALL" onclick="GetStatus('')"></asp:RadioButton>
                                    &nbsp;&nbsp;
                                    <asp:RadioButton runat="server" id="uoRadioButtonOn" Text="ON" onclick="GetStatus('ON')"></asp:RadioButton>
                                    &nbsp;&nbsp;
                                    <asp:RadioButton runat="server" id="uoRadioButtonOff" Text="OFF" onclick="GetStatus('OFF')"></asp:RadioButton>
                                </td>
                            </tr>
                        
                            <tr>
                                <td style="width: 100px" class="LeftClass" >Route From: </td>
                                <td style="width: 100px" class="LeftClass" >
                                    <asp:DropDownList runat="server" ID="uoDropDownListRouteFrom" Width="300px" AppendDataBoundItems="true"></asp:DropDownList>
                                </td>
                                <td style="width: 100px" class="LeftClass" >Route To: </td>
                                <td style="width: 100px" class="LeftClass" >
                                    <asp:DropDownList runat="server" ID="uoDropDownListRouteTo" Width="300px" AppendDataBoundItems="true"></asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                           <%-- <tr>
                                <td style="width: 100px" class="LeftClass" >Filter by Code: </td>
                                <td style="width: 100px" class="LeftClass" >
                                    <asp:TextBox runat="server" ID="uoTextBoxFilterCityFrom" Width="190px"></asp:TextBox>                                    
                                </td>
                                <td style="width: 100px" class="LeftClass" >Filter By Code: </td>
                                <td style="width: 100px" class="LeftClass" >
                                    <asp:TextBox runat="server" ID="uoTextBoxFilterCityTo" Width="190px"></asp:TextBox>
                                </td>
                                <td class="LeftClass">
                                    <asp:Button runat="server" ID="uoButtonFilter" Text ="Filter City" Width="70px" 
                                        CssClass="SmallButton" onclick="uoButtonFilter_Click"/>
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="width: 100px" class="LeftClass" >City From: </td>
                                <td style="width: 100px" class="LeftClass" >
                                    <asp:DropDownList runat="server" ID="uoDropDownListCityFrom" Width="300px" AppendDataBoundItems="true"></asp:DropDownList>
                                </td>
                                <td style="width: 100px" class="LeftClass" >City To: </td>
                                <td style="width: 100px" class="LeftClass" >
                                    <asp:DropDownList runat="server" ID="uoDropDownListCityTo" Width="300px" AppendDataBoundItems="true"></asp:DropDownList>
                                </td>
                                <td class="LeftClass">
                                    <asp:Button runat="server" ID="uoButtonSearch" Text ="Search" Width="70px" 
                                        CssClass="SmallButton" onclick="uoButtonSearch_Click"/>
                                </td>
                                <td class="RightClass" style="text-align:right" >                                
                                    <table style="width: 100%; text-align:right; " >
                                        <tr>
                                            <td>
                                                ON: &nbsp;
                                                <asp:Label ID="uoLabelONCount" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                                OFF: &nbsp;
                                                <asp:Label ID="uoLabelOFFCount" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </td>                                        
                                            <td>
                                                Potential OFF: &nbsp;
                                                <asp:Label ID="uoLabelOFFCountPotential" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>                                    
                                </td>
                            </tr>                            
                        </table>                        
                    </td>
                </tr>
                <%--<tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>--%>
                <tr>                                                         
                    <td style="padding-left:33px;" class="LeftClass" colspan="5">
                        <asp:Button ID="uoButtonExport" runat="server" Text="Export" CssClass="SmallButton"
                            Width="100px" OnClick="uoButtonExport_Click" />                            
                        
                        <a runat="server" id="uoHyperLinkAssignVehicle" class="PopUpVehiclePage" onclick="javascript:goAssign()" style="display:none">
                            <asp:Button ID="uoButtonAssign" runat="server" Text="Assign Vehicle" CssClass="SmallButton"
                                Width="110px" />
                        </a>
                        
                        <asp:Button ID="uoButtonConfirm" runat="server" Text="Confirm" CssClass="SmallButton"
                            Width="100px" OnClick="uoButtonConfirm_Click" />
                        
                         <a runat="server" visible="false" id="uoHyperLinkPotentialSignOff" class="PotentialPopUpVehiclePage" onclick="javascript:GOPotentialSignOFF()">
                            <asp:Button ID="uoButtonPotentialSignOFF" runat="server" Text="Potential Sign OFF" CssClass="SmallButton" />    
                        </a>
                        <%--<asp:panel runat="server" >
                        </asp:panel>--%>
                                              
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
            <asp:ListView runat="server" ID="uoListViewManifestHeader" OnItemCommand="uoListViewManifestHeader_ItemCommand">
                <LayoutTemplate>
                </LayoutTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="listViewTable">
                        <tr>
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label ID="Label23" runat="server" Text="Confirm" Width="50px"></asp:Label>
                                <asp:CheckBox ID="uoCheckBoxAllConfirm" runat="server" OnClick="TagSelectConfirmALL(this);" />
                            </th> 
                            <th style="text-align: center; white-space: normal; " >
                                <asp:Label ID="Label5" runat="server" Text="Assign" Width="50px" Visible="false" ></asp:Label>
                                <br />
                                <asp:CheckBox runat="server" ID="uoCheckBoxAssignAll" Width="24px" CssClass="Checkbox" onclick="SetSettings(this);" Visible="false" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label ID="Label39" runat="server" Text="Tag" Width="50px" ></asp:Label>
                            </th>
                             <th style="text-align: center; white-space: normal; display:none">
                                <asp:Label ID="Label38" runat="server" Text="Tag" Width="50px" ></asp:Label>
                                <br />
                                <asp:CheckBox runat="server" ID="uoCheckBoxTagAll" Width="24px"   />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLinkButtonRouteFrom" runat="server" CommandName="SortByRouteFrom"
                                    Text="Route From" Width="95px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByRouteTo" Text="Route To"
                                    Width="97px" />
                            </th>                            
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLinkButtonPickupDate" runat="server" CommandName="SortByPickupDate"
                                    Text="Pickup Time" Width="64px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByFromCity" Text="From City"
                                    Width="150px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton7" runat="server" CommandName="SortByToCity" Text="To City"
                                    Width="150px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByOnOffdate" Text="OnOffDate"
                                    Width="100px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByStatus" Text="Status"
                                    Width="45px" />
                            </th>
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
                                    Text="Employee ID" Width="70px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblGenderHeader" runat="server" CommandName="SortByGender"
                                    Text="Gender" Width="45px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByVehicleVendor"
                                    Text="Vehicle Co." Width="145px" />
                            </th>          
                           <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton21" runat="server" CommandName="SortByVehicleVendor"
                                    Text="Hotel Co." Width="145px" />
                            </th>           
           
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="SortByVehicleTypeName"
                                    Text="Vehicle Type" Width="74px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="SortByVehiclePlateNumber"
                                    Text="Vehicle Plate" Width="74px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton8" runat="server" CommandName="SortByVehicleDriver"
                                    Text="Driver" Width="145px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton9" runat="server" CommandName="SortByDispatchTime"
                                    Text="Dispatch Time" Width="74px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="SortByCostCenter"
                                    Text="Cost Center" Width="90px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton19" runat="server" CommandName="SortByNationality"
                                    Text="Nationality" Width="90px" />
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
                                <asp:LinkButton ID="uoLblRLHeader" runat="server" CommandName="SortByRecordLocator"
                                    Text="Record Locator" Width="55px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton10" runat="server" CommandName="SortByFlightNo"
                                    Text="Flight No" Width="55px" />
                            </th>
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton11" runat="server" CommandName="SortByCarrier"
                                    Text="Carrier" Width="55px" />
                            </th> 
                           
                            <%-- <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton36" runat="server" CommandName="SortByDateArr"
                                    Text="Arrival Gate" Width="55px" />
                            </th> 
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton37" runat="server" CommandName="SortByDateArr"
                                    Text="Flight Status" Width="55px" />
                            </th>                             
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton38" runat="server" CommandName="SortByDateArr"
                                    Text="Baggage Claim" Width="55px" />
                            </th>                               
                             --%>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton16" runat="server" CommandName="SortBySeqNo"
                                    Text="Seq No" Width="55px" />
                            </th>                          
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton12" runat="server" CommandName="SortByDeparture"
                                    Text="Dep Airport" Width="55px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton13" runat="server" CommandName="SortByArrival"
                                    Text="Arr Airport" Width="55px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton14" runat="server" CommandName="SortByDateDep"
                                    Text="Dep Date" Width="55px" />
                            </th>
                            <!--
                            <th style="text-align:center; white-space:normal;">
                                <asp:LinkButton ID="LinkButton43" runat="server" CommandName="SortByDateDep"
                                    Text="Actual Departure Date" Width="150px" />                            
                            </th>
                                    -->                    
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton15" runat="server" CommandName="SortByDateArr"
                                    Text="Arr Date" Width="55px" />
                            </th>
                            <!--
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton35" runat="server" CommandName="SortByDateArr"
                                    Text="Actual Arrival Date" Width="150px" />
                            </th>
                            -->
                          
                            
                            
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblBookingRemarksHeader" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Booking Remarks" Width="150px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton22" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Passport No" Width="100px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton23" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Passport Exp" Width="100px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton24" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Passport Issued" Width="100px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton25" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Birthday" Width="105px" />
                            </th>                                                                                    
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label2" Text="" Width="25px"></asp:Label>
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
        <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
            onscroll="divScrollLManifest();">
            <asp:ListView runat="server" ID="uoListViewManifestDetails" OnPagePropertiesChanging="uoListViewManifestDetails_PagePropertiesChanging"
                DataSourceID="uoObjectDataSourceVehicleManifest" OnDataBound="uoListViewManifestDetails_DataBound">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" id="NewTable">
                        <tr runat="server" id="itemPlaceholder">
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%-- <%# HotelAddGroup()%>--%>
                    <tr  class='<%# Convert.ToString(Eval("colIsVisibleBit")) == "True" ? "" : "PotentialColor" %>'>
                         <td class="centerAligned" style="white-space: normal; ">
                            <asp:Label ID="Label43" Text="" runat="server" Width="50px" Visible='<%# !Convert.ToBoolean(Eval("colIsVisibleBit")) %>'/>                           
                            <asp:CheckBox runat="server" ID="uoCheckBoxConfirm" Width="50px"  
                            Visible='<%# Eval("colIsVisibleBit") %>'
                            Checked = '<%# Eval("IsToConfirm") %>'
                            OnClick='<%# "TagSelectConfirm(this, "+ Eval("colTravelReqIDInt") +", "+ Eval("colIdBigint") +");" %>' />  
                        </td>
                        <td class="centerAligned" style="white-space: normal;">
                         
                            <asp:HiddenField ID="uoHiddenFieldUnAssignedIDs" runat="server" Value='<%# Convert.ToString(Eval("colIdBigint")) %>' />
                            <asp:HiddenField ID="uoHiddenFieldTransID" runat="server" Value='<%# Eval("TransVehicleID") %>' />
                            <asp:Label Width="0px" ID="Labeldummy1" runat="server" Text="" />
                            <asp:Label Width="0px" ID="Label6" runat="server" Text="" Visible='<%# Convert.ToString(Eval("colVehicleVendorIDInt")) == "0"? true: false %>'></asp:Label>
                            <div style="display:none">
                                <asp:CheckBox Width="0px" ID="uoCheckBoxShowVehicle0" runat="server" OnClick="javascript:CheckVehicleUnAssign(this)" 
                                    Visible='<%#   Convert.ToString(Eval("colIsVisibleBit")) == "True" ? true: false %>' />
                            </div>
                            <asp:CheckBox Width="0px" ID="uoCheckBoxShow" runat="server" Checked='<%# Convert.ToBoolean(Eval("colIsVisibleBit")) %>'
                                Style="visibility: hidden" />
                        </td>
                         <td class="centerAligned" style="white-space: normal; display:none">
                            <asp:CheckBox runat="server" ID="uoCheckBoxTag" Width="50px"   />
                        </td>
                        <td class="centerAligned" style="white-space:normal;" >
                        <%--added Sept 12 2014 --%>    
                        
                        <asp:LinkButton ID="uoCheckBoxTagtoVehicle" Text="Tag" Width="50px" Visible='<%# Convert.ToString(Eval("colIsVisibleBit")) == "True" ? (Convert.ToInt32(Eval("TaggedActive")) == 0 ? true : false) : false  %>' runat="server" OnClick='<%# "TagChanged(this, "+ Eval("colIdBigint") +", "+ Eval("colTravelReqIDInt") +", \""+ Eval("RecordLocator") +"\", "+ Eval("SeafarerIdInt") +", "+ Eval("colVehicleVendorIDInt") +", \""+ Eval("colSFStatus")+ "\" , "+ Eval("colVehicleVendorIDInt")+")" %>' />
                        
                        <asp:Label Text="Tagged" runat="server" Width="50px" Visible='<%# Convert.ToString(Eval("colIsVisibleBit")) == "True" ? (Convert.ToInt32(Eval("TaggedActive")) == 1 ? true : false) : false %>'/>
                        <asp:Label ID="Label44" Text="" runat="server" Width="50px" Visible='<%# !Convert.ToBoolean(Eval("colIsVisibleBit")) %>'/>  
                        </td>
                        <td class="leftAligned" style="white-space: normal;" > 
                            <asp:Label runat="server" ID="Label4" Text='<%# Eval("RouteFrom") %>' Width="100px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label8" Text='<%# Eval("RouteTo") %>' Width="100px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblCheckIn" Text='<%# String.Format("{0:hh:mm}",Eval("colPickUpTime"))%>'
                                Width="70px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblDeptCity" Text='<%# Eval("colFromVarchar")%>'
                                Width="155px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("colToVarchar")%>' Width="155px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label10" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>'
                                Width="105px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label1" Text='<%# Eval("colSFStatus")%>' Width="55px"></asp:Label>
                        </td>                        
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLabelLast" Text='<%# Eval("LastName")%>' Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:LinkButton Width="150px" runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("SeafarerIdInt") + "&trID=" + Eval("colTravelReqIDInt") + "&st=" + Eval("colSFStatus") + "&recloc=" + Eval("RecordLocator") + "&manualReqID=" + Eval("colRequestIDInt") + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("colIdBigint") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("FirstName") %></asp:LinkButton>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("SeafarerIdInt")%>' Width="78px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblGender" Text='<%# Eval("Gender")%>' Width="50px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblVehicleTypeName" Text='<%# Eval("VehicleVendorname")%>'
                                Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label23" Text='<%# Eval("HotelVendorname")%>'
                                Width="150px"></asp:Label>
                        </td>                        
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblVehicleType" Text='<%# Eval("VehicleTypeName")%>'
                                Width="80px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label7" Text='<%# Eval("VehiclePlateNoVarchar")%>'
                                Width="80px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label9" Text='<%# Eval("DriverName")%>' Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label12" Text='<%# Eval("VehicleDispatchTime")%>' Width="80px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblCostCenter" Text='<%# Eval("CostCenter")%>' Width="98px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label22" Text='<%# Eval("Nationality")%>' Width="98px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblRank" Text='<%# Eval("RankName")%>' Width="221px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblVessel" Text='<%# Eval("VesselName")%>' Width="170px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator")%>' Width="62px"></asp:Label>
                        </td> 
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label13" Text='<%# Eval("FlightNo")%>' Width="62px"></asp:Label>
                        </td>                        
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label14" Text='<%# Eval("Carrier")%>' Width="62px"></asp:Label>
                        </td>
                        <!--
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label46" Text='<%# Eval("ActualArrivalGate") %>' Width="56px"></asp:Label>
                        </td>
                                                <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label47" Text='<%# Eval("ActualArrivalStatus") %>' Width="56px"></asp:Label>
                        </td>
                                                <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label48" Text='<%# Eval("ActualArrivalBaggage") %>' Width="56px"></asp:Label>
                        </td>   -->                     
                           
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label15" Text='<%# Eval("SeqNo")%>' Width="62px"></asp:Label>
                        </td>                                              
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label16" Text='<%# Eval("Departure")%>' Width="62px"></asp:Label>
                        </td>                             
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label17" Text='<%# Eval("Arrival")%>' Width="62px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label18" Text='<%# String.Format("{0:dd-MMM-yyyy hh:mm}", Eval("DateDep"))%>' Width="62px"></asp:Label>
                        </td>
                    <!--    <td class="leftAligned" style="white-space:normal;">
                            <asp:Label runat="server" ID="Label53" Text='<%# Eval("ActualDepartureDate") %>' Width="150px"></asp:Label>                       
                        </td>   -->                     
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label19"  Text='<%# String.Format("{0:dd-MMM-yyyy hh:mm}", Eval("DateArr"))%>' Width="62px"></asp:Label>
                        </td> 
                        <!--
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label45" Text='<%# Eval("ActualArrivalDate") %>' Width="150px"></asp:Label>
                        </td>   -->
                               
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label3" Text='<%# Eval("BookingRemarks") %>' Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label24" Text='<%# Eval("PassportNo") %>' Width="105px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label25" Text='<%# Eval("PassportExp") %>' Width="105px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label26" Text='<%# Eval("PassportIssued") %>' Width="105px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label27" Text='<%# Eval("Birthday","{0:d-MMM-yyyy}") %>' Width="105px"></asp:Label>
                        </td>                                                                                                
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="listViewTable">
                        <tr>
                            <td colspan="35" class="leftAligned">
                                No Record
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div align="left">
                <asp:DataPager ID="uoDataPagerManifest" runat="server" PagedControlID="uoListViewManifestDetails"
                    PageSize="30" onprerender="uoDataPagerManifest_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </div>
        </div>
        <%--==================================New Manifest End================================================--%>
        <%--==================================Confirm Manifest Start================================================--%>
        <br />
        <div runat="server" id="Div1" class="PageSubTitle" style="text-decoration: none;">
             <table>
                <tr>
                    <td> Confirmed Manifest</td>
                    <td>
                        <a runat="server" id="uoHyperLinkAssignVehicleConfirm"
                            class="PopUpVehiclePage" style="display:none">
                        </a>
                        <input type="button" id="uoButtonAssignConfirm" onclick="javascript:goAssignConfirm()" 
                            value="Assign Vehicle" class="SmallButton" style="width:110px; display:none" />
                    </td>
                    <td>
                        <a runat="server" id="uoHyperLinkAssignVehicleConfirmAndGreet"
                            class="PopUpVehiclePage" style="display:none;">
                        </a>
                        <input type="button" id="uoButtonAssignConfirmMeetAndGreet" onclick="javascript:goAssignConfirmMeetAndGreet()" 
                            value="Assign Meet and Greet" class="SmallButton" style ="width:150px; display:none" />
                    </td>
                </tr>
             </table>
            
        </div>
        <div id="ucDivConfirmA" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
            <asp:ListView runat="server" ID="uoListViewHeaderConfirm" OnItemCommand="uoListViewHeaderConfirm_ItemCommand">
                <LayoutTemplate>
                </LayoutTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="listViewTable">
                        <tr>
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label ID="Label5" runat="server" Text="Assign" Width="50px"></asp:Label>
                                <asp:CheckBox runat="server" ID="CheckBox1" Width="24px" CssClass="Checkbox" onclick="SetSettingsConfirm(this);" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label ID="Label7" runat="server" Text="Tag" Width="50px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label ID="Label41" runat="server" Text="Remarks" Width="50px"></asp:Label>
                            </th>                            
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLinkButtonRouteFrom" runat="server" CommandName="SortByRouteFrom"
                                    Text="Route From" Width="45px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByRouteTo" Text="Route To"
                                    Width="47px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLinkButtonPickupDate" runat="server" CommandName="SortByPickupDate"
                                    Text="Pickup Time" Width="64px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByFromCity" Text="From City"
                                    Width="150px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton7" runat="server" CommandName="SortByToCity" Text="To City"
                                    Width="150px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByOnOffdate" Text="OnOffDate"
                                    Width="100px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByStatus" Text="Status"
                                    Width="50px" />
                            </th>
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
                                    Text="Employee ID" Width="70px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblGenderHeader" runat="server" CommandName="SortByGender"
                                    Text="Gender" Width="45px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByVehicleVendor"
                                    Text="Vehicle Co." Width="145px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton20" runat="server" CommandName="SortByVehicleVendor"
                                    Text="Hotel Co." Width="145px" />
                            </th>
                            
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="SortByVehicleTypeName"
                                    Text="Vehicle Type" Width="74px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="SortByVehiclePlateNumber"
                                    Text="Vehicle Plate" Width="74px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton8" runat="server" CommandName="SortByVehicleDriver"
                                    Text="Driver" Width="145px" />
                            </th>
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton35" runat="server" CommandName="SortByVehicleGreeter"
                                    Text="Greeter" Width="145px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="SortByCostCenter"
                                    Text="Cost Center" Width="90px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton18" runat="server" CommandName="SortByNationality"
                                    Text="Nationality" Width="90px" />
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
                                <asp:LinkButton ID="uoLblRLHeader" runat="server" CommandName="SortByRecordLocator"
                                    Text="Record Locator" Width="55px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton10" runat="server" CommandName="SortByFlightNo"
                                    Text="Flight No" Width="55px" />
                            </th>
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton11" runat="server" CommandName="SortByCarrier"
                                    Text="Carrier" Width="55px" />
                            </th> 
                            <!--
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton40" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Arrival Gate" Width="150px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton41" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Flight Status" Width="150px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton42" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Baggage Claim" Width="150px" />
                            </th>                              
                            -->
                              
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton16" runat="server" CommandName="SortBySeqNo"
                                    Text="Seq No" Width="55px" />
                            </th>                                                                               
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton12" runat="server" CommandName="SortByDeparture"
                                    Text="Dep Airport" Width="55px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton13" runat="server" CommandName="SortByArrival"
                                    Text="Arr Airport" Width="55px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton14" runat="server" CommandName="SortByDateDep"
                                    Text="Dep Date" Width="55px" />
                            </th>
                            <!--
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton44" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Actual Departure Date" Width="150px" />
                            </th>        -->                    
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton15" runat="server" CommandName="SortByDateArr"
                                    Text="Arr Date" Width="55px" />
                            </th>
                            <!--
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton39" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Actual Arrival Date" Width="150px" />
                            </th> -->
                                                                                                             
                            
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblBookingRemarksHeader" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Booking Remarks" Width="150px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton26" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Passport No" Width="100px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton27" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Passport Exp" Width="100px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton28" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Passport Issued" Width="100px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton29" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Birthday" Width="105px" />
                            </th>
                                                                                                                
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label2" Text="" Width="25px"></asp:Label>
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
        <div id="ucDivConfirmB" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
            onscroll="divScrollLManifestConfirm();">
            <asp:ListView runat="server" ID="uoListViewManifestConfirm" OnPagePropertiesChanging="uoListViewManifestConfirm_PagePropertiesChanging"
                DataSourceID="uoObjectDataSourceVehicleManifestConfirm" OnDataBound="uoListViewManifestConfirm_DataBound">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" id="ConfirmedTable">
                        <tr runat="server" id="itemPlaceholder">
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%-- <%# HotelAddGroup()%>--%>
                    <tr>
                        <td class="centerAligned" style="white-space: normal;">
                           <asp:HiddenField ID="uoHiddenFieldConfirmedManifestID" runat="server" Value='<%# Convert.ToString(Eval("ConfirmManifestID")) %>' />
                            <asp:HiddenField ID="uoHiddenFieldConfirmIDs" runat="server" Value='<%# Convert.ToString(Eval("colIdBigint")) %>' />
                            <asp:HiddenField ID="uoHiddenFieldTransID" runat="server" Value='<%# Eval("TransVehicleID") %>' />
                            <asp:Label Height="8px" ID="Labeldummy2" runat="server" Text="" />
                            <asp:Label Width="50px" ID="Label6" runat="server" Text="" Visible='<%# Convert.ToString(Eval("colVehicleVendorIDInt")) == "0"? true: false %>'></asp:Label>
                            <asp:CheckBox Width="51px" ID="uoCheckBoxShowVehicle1" runat="server" OnClick="javascript:CheckVehicleConfirm(this)" />
                            <asp:CheckBox Width="51px" ID="uoCheckBoxShow" runat="server" Checked='<%# Convert.ToBoolean(Eval("colIsVisibleBit")) %>'
                                Style="visibility: hidden" />
                        </td>
                        <td class="centerAligned" style="whitespace:normal;" >
                            
 
                        <asp:LinkButton ID="uoCheckBoxTagtoVehicle" Text="Tag" Width="50px" Visible='<%# Convert.ToInt32(Eval("TaggedActive")) == 0 ? true : false %>'  OnClick='<%# "TagChanged(this, "+ Eval("colIdBigint") +", "+ Eval("colTravelReqIDInt") +", \""+ Eval("RecordLocator") +"\", "+ Eval("SeafarerIdInt") +", "+ Eval("colVehicleVendorIDInt") +", \""+ Eval("colSFStatus")+ "\" , "+ Eval("colVehicleVendorIDInt")+")" %>' runat="server" />
                        
                        <asp:Label ID="Label40" Text="Tagged" runat="server" Width="50px" Visible='<%# Convert.ToInt32(Eval("TaggedActive")) == 1 ? true : false %>'/>
                          </td>                        
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label9" Text='<%# Eval("Remarks") %>' Width="51px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label4" Text='<%# Eval("RouteFrom") %>' Width="51px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label8" Text='<%# Eval("RouteTo") %>' Width="53px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblCheckIn" Text='<%# String.Format("{0:hh:mm}",Eval("colPickUpTime"))%>'
                                Width="70px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblDeptCity" Text='<%# Eval("colFromVarchar")%>'
                                Width="155px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("colToVarchar")%>' Width="155px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label10" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>'
                                Width="105px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label1" Text='<%# Eval("colSFStatus")%>' Width="55px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLabelLast" Text='<%# Eval("LastName")%>' Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:LinkButton Width="150px" runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("SeafarerIdInt") + "&trID=" + Eval("colTravelReqIDInt") + "&st=" + Eval("colSFStatus") + "&recloc=" + Eval("RecordLocator") + "&manualReqID=" + Eval("colRequestIDInt") + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("colIdBigint") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("FirstName") %></asp:LinkButton>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("SeafarerIdInt")%>' Width="78px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblGender" Text='<%# Eval("Gender")%>' Width="50px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblVehicleTypeName" Text='<%# Eval("VehicleVendorname")%>'
                                Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label28" Text='<%# Eval("HotelVendorname")%>'
                                Width="150px"></asp:Label>
                        </td>
                        
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblVehicleType" Text='<%# Eval("VehicleTypeName")%>'
                                Width="80px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label7" Text='<%# Eval("VehiclePlateNoVarchar")%>'
                                Width="80px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label11" Text='<%# Eval("DriverName")%>' Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label45" Text='<%# Eval("GreeterName")%>' Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblCostCenter" Text='<%# Eval("CostCenter")%>' Width="98px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label21" Text='<%# Eval("Nationality")%>' Width="98px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblRank" Text='<%# Eval("RankName")%>' Width="221px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblVessel" Text='<%# Eval("VesselName")%>' Width="170px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator")%>' Width="62px"></asp:Label>
                        </td> 
                         <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label13" Text='<%# Eval("FlightNo")%>' Width="62px"></asp:Label>
                        </td>                        
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label14" Text='<%# Eval("Carrier")%>' Width="62px"></asp:Label>
                        </td>
                        <!--
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label50" Text='<%# Eval("ActualArrivalGate") %>' Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label51" Text='<%# Eval("ActualArrivalStatus") %>' Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label52" Text='<%# Eval("ActualArrivalBaggage") %>' Width="150px"></asp:Label>
                        </td>      -->            
                                                     
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label15" Text='<%# Eval("SeqNo")%>' Width="62px"></asp:Label>
                        </td> 
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label16" Text='<%# Eval("Departure")%>' Width="62px"></asp:Label>
                        </td>                             
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label17" Text='<%# Eval("Arrival")%>' Width="62px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label18" Text='<%# String.Format("{0:dd-MMM-yyyy hh:mm}", Eval("DateDep"))%>' Width="62px"></asp:Label>
                        </td>
                        <!--
                         <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label54" Text='<%# Eval("ActualDepartureDate") %>' Width="150px"></asp:Label>
                        </td>   -->                      
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label19"  Text='<%# String.Format("{0:dd-MMM-yyyy hh:mm}", Eval("DateArr"))%>' Width="62px"></asp:Label>
                        </td>                             
                              <!--      
                         <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label49" Text='<%# Eval("ActualArrivalDate") %>' Width="150px"></asp:Label>
                        </td>   -->
                       
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label3" Text='<%# Eval("BookingRemarks") %>' Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label29" Text='<%# Eval("PassportNo") %>' Width="105px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label30" Text='<%# Eval("PassportExp") %>' Width="105px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label31" Text='<%# Eval("PassportIssued") %>' Width="105px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label32" Text='<%# Eval("Birthday","{0:d-MMM-yyyy}") %>' Width="105px"></asp:Label>
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
                <asp:DataPager ID="uoDataPagerManifestConfirm" runat="server" PagedControlID="uoListViewManifestConfirm"
                    PageSize="30">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </div>
        </div>
        <%--==================================Confirm Manifest End================================================--%>
        <%--==================================Cancel Manifest Start================================================--%>
        <br />
        <div runat="server" id="Div2" class="PageSubTitle" style="text-decoration: none;">
            Cancelled Manifest
        </div>
        <div id="ucDivCancelA" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
            <asp:ListView runat="server" ID="uoListViewHeaderCancel" OnItemCommand="uoListViewHeaderCancel_ItemCommand">
                <LayoutTemplate>
                </LayoutTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="listViewTable">
                        <tr>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLinkButtonRouteFrom" runat="server" CommandName="SortByRouteFrom"
                                    Text="Route From" Width="45px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByRouteTo" Text="Route To"
                                    Width="47px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLinkButtonPickupDate" runat="server" CommandName="SortByPickupDate"
                                    Text="Pickup Time" Width="64px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByFromCity" Text="From City"
                                    Width="150px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton7" runat="server" CommandName="SortByToCity" Text="To City"
                                    Width="150px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByOnOffdate" Text="OnOffDate"
                                    Width="100px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByOnOff" Text="Status"
                                    Width="50px" />
                            </th>
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
                                    Text="Employee ID" Width="70px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblGenderHeader" runat="server" CommandName="SortByGender"
                                    Text="Gender" Width="45px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByVehicleVendor"
                                    Text="Vehicle Co." Width="145px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton30" runat="server" CommandName="SortByVehicleVendor"
                                    Text="Hotel Co." Width="145px" />
                            </th>                            
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="SortByVehicleTypeName"
                                    Text="Vehicle Type" Width="74px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="SortByCostCenter"
                                    Text="Cost Center" Width="90px" />
                            </th>
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton17" runat="server" CommandName="SortByNationality"
                                    Text="Nationality" Width="90px" />
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
                                <asp:LinkButton ID="uoLblRLHeader" runat="server" CommandName="SortByRecordLocator"
                                    Text="Record Locator" Width="55px" />
                            </th>                            
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblBookingRemarksHeader" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Booking Remarks" Width="150px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton31" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Passport No" Width="100px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton32" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Passport Exp" Width="100px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton33" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Passport Issued" Width="100px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton34" runat="server" CommandName="SortByBookingRemarks"
                                    Text="Birthday" Width="105px" />
                            </th>                                                                                                                
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label2" Text="" Width="25px"></asp:Label>
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
        <div id="ucDivCancelB" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
            onscroll="divScrollLManifestCancel();">
            <asp:ListView runat="server" ID="uoListViewManifestCancel" OnPagePropertiesChanging="uoListViewManifestCancel_PagePropertiesChanging"
                DataSourceID="uoObjectDataSourceVehicleManifestCancel" OnDataBound="uoListViewManifestCancel_DataBound">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" id="CancelledTable">
                        <tr runat="server" id="itemPlaceholder">
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%-- <%# HotelAddGroup()%>--%>
                    <tr>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label4" Text='<%# Eval("RouteFrom") %>' Width="51px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label8" Text='<%# Eval("RouteTo") %>' Width="53px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblCheckIn" Text='<%# String.Format("{0:hh:mm}",Eval("colPickUpTime"))%>'
                                Width="70px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblDeptCity" Text='<%# Eval("colFromVarchar")%>'
                                Width="155px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("colToVarchar")%>' Width="155px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label10" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>'
                                Width="105px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label1" Text='<%# Eval("colSFStatus")%>' Width="55px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLabelLast" Text='<%# Eval("LastName")%>' Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:LinkButton Width="150px" runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("SeafarerIdInt") + "&trID=" + Eval("colTravelReqIDInt") + "&st=" + Eval("colSFStatus") + "&recloc=" + Eval("RecordLocator") + "&manualReqID=" + Eval("colRequestIDInt") + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("colIdBigint") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("FirstName") %></asp:LinkButton>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("SeafarerIdInt")%>' Width="78px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblGender" Text='<%# Eval("Gender")%>' Width="50px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblVehicleTypeName" Text='<%# Eval("VehicleVendorname")%>'
                                Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label33" Text='<%# Eval("HotelVendorname")%>'
                                Width="150px"></asp:Label>
                        </td>                        
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblVehicleType" Text='<%# Eval("VehicleTypeName")%>'
                                Width="80px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblCostCenter" Text='<%# Eval("CostCenter")%>' Width="98px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label20" Text='<%# Eval("Nationality")%>' Width="98px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblRank" Text='<%# Eval("RankName")%>' Width="221px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblVessel" Text='<%# Eval("VesselName")%>' Width="170px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator")%>' Width="62px"></asp:Label>
                        </td>                        
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label3" Text='<%# Eval("BookingRemarks") %>' Width="150px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label34" Text='<%# Eval("PassportNo") %>' Width="105px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label35" Text='<%# Eval("PassportExp") %>' Width="105px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label36" Text='<%# Eval("PassportIssued") %>' Width="105px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label37" Text='<%# Eval("Birthday","{0:d-MMM-yyyy}") %>' Width="155px"></asp:Label>
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
                <asp:DataPager ID="uoDataPagerManifestCancel" runat="server" PagedControlID="uoListViewManifestCancel"
                    PageSize="30">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </div>
        </div>
        <%--==================================Cancel Manifest End================================================--%>
        <asp:ObjectDataSource ID="uoObjectDataSourceVehicleManifest" runat="server" MaximumRowsParameterName="iMaxRow"
            SelectCountMethod="GetVehicleConfirmManifestCount" SelectMethod="GetVehicleConfirmManifestList"
            StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.Vehicle.VehicleManifestByVendor"
            OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True">
            <SelectParameters>
                <asp:ControlParameter Name="dDate" ControlID="uoHiddenFieldDate" />
                <asp:ControlParameter Name="sUserID" ControlID="uoHiddenFieldUser" />
                <asp:ControlParameter Name="iRegionID" ControlID="uoHiddenFieldRegion" />
                <asp:ControlParameter Name="iPortID" ControlID="uoHiddenFieldPort" />
                <asp:ControlParameter Name="iVehicleID" ControlID="uoHiddenFieldVehicle" />
                <asp:ControlParameter Name="iLoadType" ControlID="uoHiddenFieldLoadType" />
                <asp:ControlParameter Name="sOrderBy" ControlID="uoHiddenFieldSortBy" />
                <asp:Parameter Name="ListType" Type="String" DefaultValue="New" />
                
                <asp:ControlParameter Name="iRouteFrom" ControlID="uoHiddenFieldRouteFrom" />
                <asp:ControlParameter Name="iRouteTo" ControlID="uoHiddenFieldRouteTo" />
                
                <asp:ControlParameter Name="sCityFrom" ControlID="uoHiddenFieldCityFrom" />
                <asp:ControlParameter Name="sCityTo" ControlID="uoHiddenFieldCityTo" />
                <asp:ControlParameter Name="sStatus" ControlID="uoHiddenFieldStatus" />
                <asp:ControlParameter Name="sRole" ControlID="uoHiddenFieldRoleVehicleVendor" />
                
                <asp:ControlParameter Name="iBrandID" ControlID="uoHiddenFieldBrandID" />
                <asp:ControlParameter Name="iVesselID" ControlID="uoHiddenFieldVesselID" />            
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="uoObjectDataSourceVehicleManifestConfirm" runat="server"
            MaximumRowsParameterName="iMaxRow" SelectCountMethod="GetVehicleConfirmManifestCount"
            SelectMethod="GetVehicleConfirmManifestList" StartRowIndexParameterName="iStartRow"
            TypeName="TRAVELMART.Vehicle.VehicleManifestByVendor" OldValuesParameterFormatString="oldcount_{0}"
            EnablePaging="True">
            <SelectParameters>
                <asp:ControlParameter Name="dDate" ControlID="uoHiddenFieldDate" />
                <asp:ControlParameter Name="sUserID" ControlID="uoHiddenFieldUser" />
                <asp:ControlParameter Name="iRegionID" ControlID="uoHiddenFieldRegion" />
                <asp:ControlParameter Name="iPortID" ControlID="uoHiddenFieldPort" />
                <asp:ControlParameter Name="iVehicleID" ControlID="uoHiddenFieldVehicle" />
                <asp:ControlParameter Name="iLoadType" ControlID="uoHiddenFieldLoadType" />
                <asp:ControlParameter Name="sOrderBy" ControlID="uoHiddenFieldSortBy" />
                <asp:Parameter Name="ListType" Type="String" DefaultValue="Confirm" />
                
                <asp:ControlParameter Name="iRouteFrom" ControlID="uoHiddenFieldRouteFrom" />
                <asp:ControlParameter Name="iRouteTo" ControlID="uoHiddenFieldRouteTo" />
                
                <asp:ControlParameter Name="sCityFrom" ControlID="uoHiddenFieldCityFrom" />
                <asp:ControlParameter Name="sCityTo" ControlID="uoHiddenFieldCityTo" />
                <asp:ControlParameter Name="sStatus" ControlID="uoHiddenFieldStatus" />
                <asp:ControlParameter Name="sRole" ControlID="uoHiddenFieldRoleVehicleVendor" />
                
                <asp:ControlParameter Name="iBrandID" ControlID="uoHiddenFieldBrandID" />
                <asp:ControlParameter Name="iVesselID" ControlID="uoHiddenFieldVesselID" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="uoObjectDataSourceVehicleManifestCancel" runat="server"
            MaximumRowsParameterName="iMaxRow" SelectCountMethod="GetVehicleConfirmManifestCount"
            SelectMethod="GetVehicleConfirmManifestList" StartRowIndexParameterName="iStartRow"
            TypeName="TRAVELMART.Vehicle.VehicleManifestByVendor" OldValuesParameterFormatString="oldcount_{0}"
            EnablePaging="True">
            <SelectParameters>
                <asp:ControlParameter Name="dDate" ControlID="uoHiddenFieldDate" />
                <asp:ControlParameter Name="sUserID" ControlID="uoHiddenFieldUser" />
                <asp:ControlParameter Name="iRegionID" ControlID="uoHiddenFieldRegion" />
                <asp:ControlParameter Name="iPortID" ControlID="uoHiddenFieldPort" />
                <asp:ControlParameter Name="iVehicleID" ControlID="uoHiddenFieldVehicle" />
                <asp:ControlParameter Name="iLoadType" ControlID="uoHiddenFieldLoadType" />
                <asp:ControlParameter Name="sOrderBy" ControlID="uoHiddenFieldSortBy" />
                <asp:Parameter Name="ListType" Type="String" DefaultValue="Cancel" />
                
                <asp:ControlParameter Name="iRouteFrom" ControlID="uoHiddenFieldRouteFrom" />
                <asp:ControlParameter Name="iRouteTo" ControlID="uoHiddenFieldRouteTo" />
                
                <asp:ControlParameter Name="sCityFrom" ControlID="uoHiddenFieldCityFrom" />
                <asp:ControlParameter Name="sCityTo" ControlID="uoHiddenFieldCityTo" />
                <asp:ControlParameter Name="sStatus" ControlID="uoHiddenFieldStatus" />
                <asp:ControlParameter Name="sRole" ControlID="uoHiddenFieldRoleVehicleVendor" />
                
                <asp:ControlParameter Name="iBrandID" ControlID="uoHiddenFieldBrandID" />
                <asp:ControlParameter Name="iVesselID" ControlID="uoHiddenFieldVesselID" />
               
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldRoleVehicleVendor" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldRegion" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldPort" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldVehicle" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldSortBy" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldCrewIDs" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldServicePopup" runat="server" />
    
    <asp:HiddenField ID="uoHiddenFieldRouteFrom" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldRouteTo" runat="server" Value="0"/>
    
    <asp:HiddenField ID="uoHiddenFieldPortAgentVendor" runat="server" Value=""/>
    <asp:HiddenField ID="uoHiddenFieldCityFrom" runat="server" Value=""/>
    <asp:HiddenField ID="uoHiddenFieldCityTo" runat="server" Value=""/>
     <asp:HiddenField ID="uoHiddenFieldStatus" runat="server" Value=""/>
     <asp:HiddenField ID="uoHiddenFieldTag" runat="server" Value="" />

    
    <asp:HiddenField ID="uoHiddenFieldBrandID" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldVesselID" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldSelectConfirm" runat="server" Value="0"/>

    <script type="text/javascript">
        $(document).ready(function() {
 
        });
    </script>
    <script type="text/javascript">
        function TagChanged(IsChk,IDBigInt, TravelReq, RecordLoc, SeafarerId, Vehiclevendor,colSFStatus,userId) {
            var sDescription = 'Tag Seafarer To Vehicle';
            var sFunction = 'TagChanged';
            var get_id = IsChk.id;
            var onoff = 1;
            var idno = get_id.split();
            /*add all the data by getting the same row id to the hidden field*/
            var sUserName = $("#ctl00_NaviPlaceHolder_uoHiddenFieldUser").val();
            
            var r = confirm("Are you sure you want to tag Record # "+SeafarerId+" ?");
            if (r == true) {
            
                 $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/PageMethods.aspx/TagtoVehicle",
                data: "{'colIDBigint': '" + IDBigInt + "', 'colTravelReqIDInt': '" + TravelReq +
                "', 'colRecordLocatorVarchar': '" + RecordLoc + "', 'colSeafarerIdInt': '" + 
                SeafarerId + "', 'colOnOff': '" + colSFStatus + "', 'colVehicleVendorIDInt': '" + 
                Vehiclevendor + "', 'colPortAgentVendorIDInt': '0', 'colSFStatus': '" + 
                onoff + "', 'UserId': '" + sUserName + 
                "', 'sDescription': '" + sDescription +
                "', 'sFunction': '" + sFunction +
                "', 'sFileName': 'VehicleManifestByVendor.aspx'}",
                dataType: "json",
                success: function(data) {
                } ,
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                }
                });                
            }            
             $("#aspnetForm").submit();
        }
        $(document).ready(function() {
            PageSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                PageSettings(); 
            }
        }
        function PageSettings() {
            $("#<%=uoButtonConfirm.ClientID %>").click(function() {

                var checked = $("input[id*=uoCheckBoxConfirm]:checked").length;
                if (checked > 0) {
                    return true;
                }
                else {
                    alert("No selected record to confirm!");
                    return false;
                }
            })
        }
        function TagSelectConfirmALL(IsSelected) {

            var sUserName = $("#<%= uoHiddenFieldUser.ClientID %>").val();
            $("#<%= uoHiddenFieldLoadType.ClientID %>").val("1");

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/PageMethods.aspx/SaveVehicleManifestToConfirmAll",
                data: "{'UserID': '" + sUserName + "', 'IsSelected': " + IsSelected.checked + "}",
                dataType: "json",
                success: function(data) {
                    $("#<%=uoHiddenFieldSelectConfirm.ClientID %>").val('1');
                    //                    $("#aspnetForm").submit();
                    $('input[id*="uoCheckBoxConfirm"]').attr('checked', IsSelected.checked);
                },
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }
        function TagSelectConfirm(IsSelected, iTravelRequestID, iIDBigint) {

            var sUserName = $("#<%= uoHiddenFieldUser.ClientID %>").val();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/PageMethods.aspx/SaveVehicleManifestToConfirm",
                data: "{'UserID': '" + sUserName + "', 'IsSelected': " + IsSelected.checked +
                    ", 'iTravelReqID': " + iTravelRequestID + ", 'iIDBigint': " + iIDBigint + "}",
                dataType: "json",
                success: function(data) {
                }
                        ,
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }
    </script>

</asp:Content>
