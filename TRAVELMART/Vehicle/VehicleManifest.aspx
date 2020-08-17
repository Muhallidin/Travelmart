<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    CodeBehind="VehicleManifest.aspx.cs" Inherits="TRAVELMART.Vehicle.VehicleManifest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
     <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="PageTitle">
            <td align="left">
                Vehicle Manifest
            </td>
            <td align="right">
                <div runat="server" id="uoDivRegionPort">
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
                
                    <asp:Label ID="Label37" runat="server" Text="Region:" Font-Size="14px"></asp:Label>&nbsp;
                    <asp:DropDownList ID="uoDropDownListRegion" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged"
                        Width="130px" CssClass="SmallText">
                    </asp:DropDownList>
                    <asp:Label ID="Label38" runat="server" Text="Port:" Font-Size="14px"></asp:Label>&nbsp;
                    <asp:DropDownList ID="uoDropDownListPort" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListPort_SelectedIndexChanged"
                        Width="150px" CssClass="SmallText">
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

        $(document).ready(function() {
            SetTMResolution();
            pageSettings();
            RouteSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTMResolution();
                pageSettings();
                RouteSettings();
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
        function pageSettings() {
            $(".clsAddSaveTranspo").fancybox(
            {
                'centerOnScroll': false,
                'width': '70%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldTransportation.ClientID %>").val();
                    if (a == '2') {
                        $("#aspnetForm").submit();
                        alert('Email successfully sent!');
                    }
                    else {
                        $("#aspnetForm").submit();
                    }
                }
            });

            $(".clsEditTranspo").fancybox(
            {
                'centerOnScroll': false,
                'width': '80%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldTransportation.ClientID %>").val();

                    if (a == '2') {
                        $("#aspnetForm").submit();
                        alert('Email successfully sent!');
                    }
                }
            });
        }
        
        function CheckALLEditTransport(obj) {
            $('input[name*="uoCheckBoxEditVehicle"]').attr('checked', obj.checked);
        }

        function CheckALLNeedTransport(obj) {
            $("#ucTableCrewAdmin tr").each(function(i, element) {
                var $TDs = $(this).find('td');
                var chk = $('td:eq(1) input[name*="uoCheckBoxNoVehicleNeed"]', this);

                var TransID = $('td:eq(1) input[name*="uoHiddenFieldTransID"]', this);

                if (chk.attr('checked') != undefined) {

                    if ((TransID.val() == '' || TransID.val() == '0') && chk.is(':visible')) {
                        chk.attr('checked', obj.checked);
                    }
                }
            });
        }
        function CheckUncheckNeedTransport() {

            var sAddCancel = '',
                sRecordLocID = '',
                sTReqID = '',
                sTransID = '';

            //            var sURL = $("a#linkTranspo").attr("href");
            //alert(sURL);

            $("#ucTableCrewAdmin tr").each(function(i, element) {
                var $TDs = $(this).find('td');
                var chk = $('td:eq(1) input[name*="uoCheckBoxNoVehicleNeed"]', this);
                //alert(chk.attr('checked'));

                var RecLocID = $('td:eq(1) input[name*="uoHiddenFieldListRecLocID"]', this);
                var TravelReqID = $('td:eq(1) input[name*="uoHiddenFieldListTRID"]', this);
                var TransID = $('td:eq(1) input[name*="uoHiddenFieldTransID"]', this);

                if (chk != null) {
                    if (chk.is(':visible')) {
                        if (chk.attr('checked')) {
                            if (sAddCancel == '') {
                                sAddCancel = 'Add';
                            }
                            else {
                                sAddCancel += ',Add';
                            }
                        }
                        else {
                            if (sAddCancel == '') {
                                sAddCancel = 'Cancel';
                            }
                            else {
                                sAddCancel += ',Cancel';
                            }
                        }

                        if (sRecordLocID == '') {
                            sRecordLocID = RecLocID.val();
                        }
                        else {
                            sRecordLocID += ',' + RecLocID.val();
                        }

                        if (sTReqID == '') {
                            sTReqID = TravelReqID.val();
                        }
                        else {
                            sTReqID += ',' + TravelReqID.val();
                        }

                        if (sTransID == '') {
                            sTransID = TransID.val();
                        }
                        else {
                            sTransID += ',' + TransID.val();
                        }
                    }
                }
            });

            var sURL = '../Vehicle/VehicleEditor2.aspx?AddCancel=' + sAddCancel + '&RecLoc=' + sRecordLocID + '&TReqID=' + sTReqID + '&TransID=' + sTransID;            
            $("a#linkTranspo").attr("href", sURL);
        }

        function CheckUncheckEditransport() {

            var sAddCancel = '',
                sRecordLocID = '',
                sTReqID = '',
                sTransID = '';


            $("#ucTableCrewAdmin tr").each(function(i, element) {
                var $TDs = $(this).find('td');
                var chk = $('td:eq(0) input[name*="uoCheckBoxEditVehicle"]', this);
                //alert(chk.attr('checked'));

                var RecLocID = $('td:eq(1) input[name*="uoHiddenFieldListRecLocID"]', this);
                var TravelReqID = $('td:eq(1) input[name*="uoHiddenFieldListTRID"]', this);
                var TransID = $('td:eq(1) input[name*="uoHiddenFieldTransID"]', this);
                
                if (chk != null) {
                    if (chk.is(':visible')) {
                        if (chk.attr('checked')) {

                            if (sAddCancel == '') {
                                sAddCancel = 'Edit';
                            }
                            else {
                                sAddCancel += ',Edit';
                            }


                            if (sRecordLocID == '') {
                                sRecordLocID = RecLocID.val();
                            }
                            else {
                                sRecordLocID += ',' + RecLocID.val();
                            }

                            if (sTReqID == '') {
                                sTReqID = TravelReqID.val();
                            }
                            else {
                                sTReqID += ',' + TravelReqID.val();
                            }

                            if (sTransID == '') {
                                sTransID = TransID.val();
                            }
                            else {
                                sTransID += ',' + TransID.val();
                            }
                        }
                    }
                }
            });

            var sURL = '../Vehicle/VehicleEditor2.aspx?AddCancel=' + sAddCancel + '&RecLoc=' + sRecordLocID + '&TReqID=' + sTReqID + '&TransID=' + sTransID;
            $("a#linkTranspoEdit").attr("href", sURL);
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
    <div>
      <table class="LeftClass" style="width:100%"  >
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
            <tr>
                <td class="LeftClass">Vehicle Company:</td>
                <td class="LeftClass">
                    <asp:DropDownList ID="uoDropDownListVehicle" runat="server" Width="300px" AppendDataBoundItems="True"
                        AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListVehicle_SelectedIndexChanged">
                        <asp:ListItem Value="0">--SELECT VENDOR--</asp:ListItem>
                    </asp:DropDownList>                                               
                </td>
                <td></td>
                <td colspan="2">
                    <table width="100%">
                    <tr>
                        <td class="LeftClass" style="width:100px">
                            <asp:Button ID="uoButtonExport"  runat="server" Text="Export" CssClass="SmallButton" Width="100px"
                                onclick="uoButtonExport_Click" />
                        </td>
                         <td class="LeftClass" style="width:100px">
                            <%--<asp:Button ID="uoButtonSave"  runat="server" Text="Save" CssClass="SmallButton" Width="100px"
                                 onclick="uoButtonSave_Click"/>--%>
                            <a href="../Vehicle/VehicleEditor2.aspx" class="clsAddSaveTranspo" id = 'linkTranspo'> 
                                <asp:Button ID="uoButtonSave" runat="server" Text="Save Transport" CssClass="SmallButton"
                                    OnClick="uoButtonSave_Click" />
                            </a>
                        </td>
                        <td class="LeftClass" style="width:100px">
                             <a href="../Vehicle/VehicleEditor2.aspx" class="clsEditTranspo" id = 'linkTranspoEdit'>
                                <asp:Button ID="uoButtonEdit" runat="server" Text="Edit Transport" CssClass="SmallButton"
                                    OnClick="uoButtonSave_Click" />
                            </a>
                        </td>
                        <td class="LeftClass" >
                            <div style="display:none">
                                <asp:Button ID="uoButtonConfirm"  runat="server" Text="Confirm" Visible="false"
                                CssClass="SmallButton" Width="100px" onclick="uoButtonConfirm_Click"/>
                            </div>
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
        </table>    
    </div>
    <br/>
    <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
        <asp:ListView runat="server" ID="uoListViewManifestHeader" 
            onitemcommand="uoListViewManifestHeader_ItemCommand">
            <LayoutTemplate>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="listViewTable">
                    <tr> 
                        <th style="text-align: center; white-space: normal;">
                            <asp:Label ID="Label13" runat="server" Text="Edit Transport" Width="70px"></asp:Label>
                            <asp:CheckBox ID="uoCheckBoxEdit" runat="server" OnClick="CheckALLEditTransport(this);" />
                        </th>  
                        <th style="text-align: center; white-space: normal;">
                            <asp:Label ID="Label5" runat="server" Text="Need Transport" Width="70px"></asp:Label>
                                <asp:CheckBox ID="uoCheckBoxNeedTransportALL" runat="server" OnClick="CheckALLNeedTransport(this);" CssClass="uoCheckBoxNeedTransportALL"/>
                        </th>                     
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLinkButtonRouteFrom" runat="server" CommandName="SortByRouteFrom"
                                Text="Route From" Width="100px" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByRouteTo"
                                Text="Route To" Width="100px" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByFromCity"
                                Text="From City" Width="150px" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton7" runat="server" CommandName="SortByToCity"
                                Text="To City" Width="150px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLinkButtonPickupDate" runat="server" CommandName="SortByPickupDate"
                                Text="Pickup Time" Width="64px" />
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
                                Text="Genders" Width="45px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByVehicleVendor"
                                Text="Vehicle Co." Width="145px" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton17" runat="server" CommandName="SortByVehicleVendor"
                                Text="Hotel Co." Width="145px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="SortByVehicleTypeName"
                                Text="Vehicle Type" Width="74px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="SortByCostCenter" Text="Cost Center"
                                Width="90px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="SortByNationality" Text="Nationality"
                                Width="90px" />
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
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByOnOffdate"
                                Text="OnOffDate" Width="100px" />
                        </th>        
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByOnOff"
                                Text="Status" Width="50px" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton10" runat="server" CommandName="SortByFlightNo"
                                Text="Flight No" Width="55px" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton11" runat="server" CommandName="SortByCarrier"
                                Text="Carrier" Width="55px" />
                        </th>  
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
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton15" runat="server" CommandName="SortByDateArr"
                                Text="Arr Date" Width="55px" />
                        </th>            
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblBookingRemarksHeader" runat="server" CommandName="SortByBookingRemarks"
                                Text="Booking Remarks" Width="150px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton18" runat="server" CommandName="SortByBookingRemarks"
                                Text="Passport No" Width="100px" />
                        </th>                                                                                                                      
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton19" runat="server" CommandName="SortByBookingRemarks"
                                Text="Passport Expiry" Width="100px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton20" runat="server" CommandName="SortByBookingRemarks"
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
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;" onscroll="divScrollLManifest();">
        <asp:ListView runat="server" ID="uoListViewManifestDetails" onpagepropertieschanging="uoListViewManifestDetails_PagePropertiesChanging"
            DataSourceID="uoObjectDataSourceVehicleManifest" 
            ondatabound="uoListViewManifestDetails_DataBound" >
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" id="ucTableCrewAdmin">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
               <%-- <%# HotelAddGroup()%>--%>
                <tr  class='<%# Convert.ToString(Eval("colIsVisibleBit")) == "True" ? "" : "PotentialColor" %>' >
                    <td class="centerAligned" style="white-space: normal;" >                                            
                        <%--<asp:Label Width="55px" ID="Label6" runat="server" Text="" Visible='<%# Convert.ToString(Eval("colVehicleVendorIDInt")) == "0"? true: false %>'></asp:Label>--%>
                        <%--<asp:CheckBox Width="55px" ID="uoCheckBoxShow" runat="server" Visible='<%# Convert.ToString(Eval("colVehicleVendorIDInt")) == "0"? false : true %>' Checked='<%# Convert.ToBoolean(Eval("colIsVisibleBit")) %>' />--%>                        
                        <asp:Label Width="70px" ID="Label11" runat="server" Text="" ></asp:Label>
                        <asp:CheckBox Width="70px"  ID="uoCheckBoxEditVehicle" runat="server" Checked="false"
                                            Visible='<%# Convert.ToBoolean(Eval("colIsVisibleBit"))%>' OnClick='CheckUncheckEditransport();' />
                    </td>
                    <td class="centerAligned" style="white-space: normal;" >
                        <asp:HiddenField ID="uoHiddenFieldListRecLocID" runat="server" Value ='<%# Eval("colIdBigint") %>' />                                            
                        <asp:HiddenField ID="uoHiddenFieldListTRID" runat="server" Value ='<%# Eval("colTravelReqIDInt") %>' />                                            
                        <asp:HiddenField ID="uoHiddenFieldTransID" runat="server" Value ='<%# Eval("TransVehicleID") %>'/>
                    
                         <asp:Label Width="70px" ID="uoLabelNoVehicleNeeded" runat="server" Text="" ></asp:Label>
                        <asp:CheckBox Width="70px" CssClass="chkNeedTransport" ID="uoCheckBoxNoVehicleNeed" runat="server" 
                        Visible='<%# Convert.ToString(Eval("colVehicleVendorIDInt")) == "0"? false : true %>' Checked='<%# Convert.ToBoolean(Eval("colIsVisibleBit")) %>'
                        OnClick='CheckUncheckNeedTransport();' />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label4" Text='<%# Eval("RouteFrom") %>' Width="105px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label8" Text='<%# Eval("RouteTo") %>' Width="107px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblDeptCity" Text='<%# Eval("colFromVarchar")%>' Width="155px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("colToVarchar")%>' Width="155px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCheckIn" Text='<%# String.Format("{0:hh:mm tt}",Eval("colPickUpTime"))%>' Width="70px"></asp:Label>
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
                        <asp:Label runat="server" ID="uoLblVehicleTypeName" Text='<%# Eval("VehicleVendorname")%>' Width="150px"></asp:Label>
                    </td>
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label22" Text='<%# Eval("HotelVendorName")%>' Width="150px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblVehicleType" Text='<%# Eval("VehicleTypeName")%>' Width="80px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCostCenter" Text='<%# Eval("CostCenter")%>'
                            Width="98px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label12" Text='<%# Eval("Nationality")%>'
                            Width="98px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblRank" Text='<%# Eval("RankName")%>' Width="221px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblVessel" Text='<%# Eval("VesselName")%>'
                            Width="170px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator")%>' Width="62px"></asp:Label>
                    </td>                    
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label10" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>' Width="105px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label1" Text='<%# Eval("colSFStatus")%>' Width="55px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label13" Text='<%# Eval("FlightNo")%>' Width="62px"></asp:Label>
                    </td>                        
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label14" Text='<%# Eval("Carrier")%>' Width="62px"></asp:Label>
                    </td>   
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
                        <asp:Label runat="server" ID="Label18" Text='<%# String.Format("{0:dd-MMM-yyyy hh:mm tt}", Eval("DateDep"))%>' Width="62px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label19"  Text='<%# String.Format("{0:dd-MMM-yyyy hh:mm tt}", Eval("DateArr"))%>' Width="62px"></asp:Label>
                    </td> 
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label3" Text='<%# Eval("BookingRemarks") %>' Width="150px"></asp:Label>
                    </td>
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label23" Text='<%# Eval("PassportNo")%>' Width="105px"></asp:Label>
                    </td> 
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label24" Text='<%# Eval("PassportExp")%>' Width="105px"></asp:Label>
                    </td>
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label25" Text='<%# Eval("PassportIssued")%>' Width="105px"></asp:Label>
                    </td> 
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label31" Text='<%# Eval("Birthday","{0:d-MMM-yyyy}")%>' Width="105px"></asp:Label> 
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
            <asp:DataPager ID="uoDataPagerManifest" runat="server" PagedControlID="uoListViewManifestDetails" PageSize="20">
                <Fields>
                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                </Fields>
            </asp:DataPager>
        </div>
    </div>
    <%--==================================New Manifest End================================================--%>    
    <%--==================================Confirm Manifest Start================================================--%>
    <br />
    <div runat="server" id="Div1" class="PageSubTitle" style="text-decoration: underline;">
        Confirmed Manifest
    </div>
    
      <div id="ucDivConfirmA" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
        <asp:ListView runat="server" ID="uoListViewHeaderConfirm" 
            onitemcommand="uoListViewHeaderConfirm_ItemCommand">
            <LayoutTemplate>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="listViewTable">
                    <tr>  
                        <th style="text-align: center; white-space: normal;">
                            <asp:Label ID="Label5" runat="server" Text="Need Transport" Width="50px"></asp:Label>
                        </th>    
                        <th style="text-align: center; white-space: normal;">
                            <asp:Label ID="Label7" runat="server" Text="Remarks" Width="50px"></asp:Label>
                        </th>             
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLinkButtonRouteFrom" runat="server" CommandName="SortByRouteFrom"
                                Text="Route From" Width="45px" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByRouteTo"
                                Text="Route To" Width="47px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByFromCity"
                                Text="From City" Width="150px" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton7" runat="server" CommandName="SortByToCity"
                                Text="To City" Width="150px" />
                        </th>    
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLinkButtonPickupDate" runat="server" CommandName="SortByPickupDate"
                                Text="Pickup Time" Width="64px" />
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
                            <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="SortByCostCenter" Text="Cost Center"
                                Width="90px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton8" runat="server" CommandName="SortByNationality" Text="Nationality"
                                Width="90px" />
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
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByOnOffdate"
                                Text="OnOffDate" Width="100px" />
                        </th>   
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByOnOff"
                                Text="Status" Width="50px" />
                        </th>  
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton10" runat="server" CommandName="SortByFlightNo"
                                Text="Flight No" Width="55px" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton11" runat="server" CommandName="SortByCarrier"
                                Text="Carrier" Width="55px" />
                        </th>  
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
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton15" runat="server" CommandName="SortByDateArr"
                                Text="Arr Date" Width="55px" />
                        </th>             
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblBookingRemarksHeader" runat="server" CommandName="SortByBookingRemarks"
                                Text="Booking Remarks" Width="150px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton22" runat="server" CommandName="SortByVehicleVendor"
                                Text="Passport No." Width="100px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton23" runat="server" CommandName="SortByVehicleVendor"
                                Text="Passport Expiry" Width="100px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton24" runat="server" CommandName="SortByVehicleVendor"
                                Text="Passport Issued" Width="100px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton30" runat="server" CommandName="SortByVehicleVendor"
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
    <div id="ucDivConfirmB" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;" onscroll="divScrollLManifestConfirm();">
        <asp:ListView runat="server" ID="uoListViewManifestConfirm" onpagepropertieschanging="uoListViewManifestConfirm_PagePropertiesChanging"
            DataSourceID="uoObjectDataSourceVehicleManifestConfirm" 
            ondatabound="uoListViewManifestConfirm_DataBound" >
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
               <%-- <%# HotelAddGroup()%>--%>
                <tr>
                    <td class="centerAligned" style="white-space: normal;" >
                        <asp:HiddenField ID="uoHiddenFieldTransID" runat="server" Value ='<%# Eval("TransVehicleID") %>'/>
                        <asp:Label Width="50px" ID="Label6" runat="server" Text="" Visible='<%# Convert.ToString(Eval("colVehicleVendorIDInt")) == "0"? true: false %>'></asp:Label>
                        <asp:CheckBox Width="51px" ID="uoCheckBoxShow" runat="server" Visible='<%# Convert.ToString(Eval("colVehicleVendorIDInt")) == "0"? false : true %>' Checked='<%# Convert.ToBoolean(Eval("colIsVisibleBit")) %>' />
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
                        <asp:Label runat="server" ID="uoLblDeptCity" Text='<%# Eval("colFromVarchar")%>' Width="155px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("colToVarchar")%>' Width="155px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCheckIn" Text='<%# String.Format("{0:hh:mm tt}",Eval("colPickUpTime"))%>' Width="70px"></asp:Label>
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
                        <asp:Label runat="server" ID="uoLblVehicleTypeName" Text='<%# Eval("VehicleVendorname")%>' Width="150px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label26" Text='<%# Eval("HotelVendorname")%>' Width="150px"></asp:Label>
                    </td>
                    
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblVehicleType" Text='<%# Eval("VehicleTypeName")%>' Width="80px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCostCenter" Text='<%# Eval("CostCenter")%>'
                            Width="98px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label20" Text='<%# Eval("Nationality")%>'
                            Width="98px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblRank" Text='<%# Eval("RankName")%>' Width="221px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblVessel" Text='<%# Eval("VesselName")%>'
                            Width="170px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator")%>' Width="62px"></asp:Label>
                    </td>
                    
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label10" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>' Width="105px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label1" Text='<%# Eval("colSFStatus")%>' Width="55px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label13" Text='<%# Eval("FlightNo")%>' Width="62px"></asp:Label>
                    </td>                        
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label14" Text='<%# Eval("Carrier")%>' Width="62px"></asp:Label>
                    </td>   
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
                        <asp:Label runat="server" ID="Label18" Text='<%# String.Format("{0:dd-MMM-yyyy hh:mm tt}", Eval("DateDep"))%>' Width="62px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label19"  Text='<%# String.Format("{0:dd-MMM-yyyy hh:mm tt}", Eval("DateArr"))%>' Width="62px"></asp:Label>
                    </td> 
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label3" Text='<%# Eval("BookingRemarks") %>' Width="150px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label27" Text='<%# Eval("PassportNo")%>' Width="105px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label28" Text='<%# Eval("PassportExp")%>' Width="105px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label29" Text='<%# Eval("PassportIssued")%>' Width="105px"></asp:Label>
                    </td>
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label32" Text='<%# Eval("Birthday","{0:d-MMM-yyyy}")%>' Width="105px"></asp:Label>
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
            <asp:DataPager ID="uoDataPagerManifestConfirm" runat="server" PagedControlID="uoListViewManifestConfirm" PageSize="20">
                <Fields>
                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                </Fields>
            </asp:DataPager>
        </div>
    </div>
    <%--==================================Confirm Manifest End================================================--%>
    
    <%--==================================Cancel Manifest Start================================================--%>
    <br />
    <div runat="server" id="Div2" class="PageSubTitle" style="text-decoration: underline;">
        Cancelled Manifest
    </div>
    
      <div id="ucDivCancelA" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
        <asp:ListView runat="server" ID="uoListViewHeaderCancel" 
            onitemcommand="uoListViewHeaderCancel_ItemCommand">
            <LayoutTemplate>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="listViewTable">
                    <tr>
                        <th style="text-align: center; white-space: normal;">
                            <asp:Label ID="Label5" runat="server" Text="Need Transport" Width="55px"></asp:Label>                                            
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLinkButtonRouteFrom" runat="server" CommandName="SortByRouteFrom"
                                Text="Route From" Width="45px" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByRouteTo"
                                Text="Route To" Width="47px" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByFromCity"
                                Text="From City" Width="150px" />
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton7" runat="server" CommandName="SortByToCity"
                                Text="To City" Width="150px" />
                        </th>    
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLinkButtonPickupDate" runat="server" CommandName="SortByPickupDate"
                                Text="Pickup Time" Width="64px" />
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
                            <asp:LinkButton ID="LinkButton25" runat="server" CommandName="SortByVehicleVendor"
                                Text="Hotel Co." Width="145px" />
                        </th>
                        
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="SortByVehicleTypeName"
                                Text="Vehicle Type" Width="74px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="SortByCostCenter" Text="Cost Center"
                                Width="90px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton9" runat="server" CommandName="SortByNationality" Text="Nationality"
                                Width="90px" />
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
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByOnOffdate"
                                Text="OnOffDate" Width="100px" />
                        </th>   
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByOnOff"
                                Text="Status" Width="50px" />
                        </th>               
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="uoLblBookingRemarksHeader" runat="server" CommandName="SortByBookingRemarks"
                                Text="Booking Remarks" Width="150px" />
                        </th> 
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton26" runat="server" CommandName="SortByBookingRemarks"
                                Text="Passport No." Width="100px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton27" runat="server" CommandName="SortByBookingRemarks"
                                Text="Passport Expiry" Width="100px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton28" runat="server" CommandName="SortByBookingRemarks"
                                Text="Passport Issued" Width="100px" />
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton31" runat="server" CommandName="SortByBookingRemarks"
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
    <div id="ucDivCancelB" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;" onscroll="divScrollLManifestCancel();">
        <asp:ListView runat="server" ID="uoListViewManifestCancel" 
            onpagepropertieschanging="uoListViewManifestCancel_PagePropertiesChanging"
            DataSourceID="uoObjectDataSourceVehicleManifestCancel" 
            ondatabound="uoListViewManifestCancel_DataBound" >
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
               <%-- <%# HotelAddGroup()%>--%>
                <tr>          
                    <td>
                       <asp:Label Width="60px" ID="Label6" runat="server" Text='<%# (Convert.ToString(Eval("colIsVisibleBit")) == "False" ? "No": "Yes") %>' ></asp:Label>
                        <%--<asp:HiddenField ID="uoHiddenFieldTransID" runat="server" Value ='<%# Eval("TransVehicleID") %>'/>
                        <asp:Label Width="50px" ID="Label6" runat="server" Text="" Visible='<%# Convert.ToString(Eval("colVehicleVendorIDInt")) == "0"? true: false %>'></asp:Label>
                        <asp:CheckBox Width="51px" ID="uoCheckBoxShow" runat="server" Visible='<%# Convert.ToString(Eval("colVehicleVendorIDInt")) == "0"? false : true %>' Checked='<%# Convert.ToBoolean(Eval("colIsVisibleBit")) %>' />      --%>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label4" Text='<%# Eval("RouteFrom") %>' Width="51px"></asp:Label>
                    
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label8" Text='<%# Eval("RouteTo") %>' Width="53px"></asp:Label>
                    </td>
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblDeptCity" Text='<%# Eval("colFromVarchar")%>' Width="155px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("colToVarchar")%>' Width="155px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCheckIn" Text='<%# String.Format("{0:hh:mm tt}",Eval("colPickUpTime"))%>' Width="70px"></asp:Label>
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
                        <asp:Label runat="server" ID="uoLblVehicleTypeName" Text='<%# Eval("VehicleVendorname")%>' Width="150px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label30" Text='<%# Eval("HotelVendorname")%>' Width="150px"></asp:Label>
                    </td>                    
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblVehicleType" Text='<%# Eval("VehicleTypeName")%>' Width="80px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblCostCenter" Text='<%# Eval("CostCenter")%>'
                            Width="98px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label21" Text='<%# Eval("Nationality")%>'
                            Width="98px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblRank" Text='<%# Eval("RankName")%>' Width="221px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblVessel" Text='<%# Eval("VesselName")%>'
                            Width="170px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator")%>' Width="62px"></asp:Label>
                    </td>
                   
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label10" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>' Width="105px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label1" Text='<%# Eval("colSFStatus")%>' Width="55px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label3" Text='<%# Eval("BookingRemarks") %>' Width="150px"></asp:Label>
                    </td> 
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label33" Text='<%# Eval("PassportNo") %>' Width="105px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label34" Text='<%# Eval("PassportExp") %>' Width="105px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label35" Text='<%# Eval("PassportIssued") %>' Width="105px"></asp:Label>
                    </td>
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label36" Text='<%# Eval("Birthday","{0:d-MMM-yyyy}") %>' Width="105px"></asp:Label>
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
            <asp:DataPager ID="uoDataPagerManifestCancel" runat="server" PagedControlID="uoListViewManifestCancel" PageSize="20">
                <Fields>
                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                </Fields>
            </asp:DataPager>
        </div>
    </div>
    <%--==================================Cancel Manifest End================================================--%>
     
      <asp:ObjectDataSource ID="uoObjectDataSourceVehicleManifest" runat="server" MaximumRowsParameterName="iMaxRow"
        SelectCountMethod="GetVehicleConfirmManifestCount" SelectMethod="GetVehicleConfirmManifestList"
        StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.Vehicle.VehicleManifest"
        OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" >
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
            
            <asp:ControlParameter Name="iBrandID" ControlID="uoHiddenFieldBrandID" />
            <asp:ControlParameter Name="iVesselID" ControlID="uoHiddenFieldVesselID" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="uoObjectDataSourceVehicleManifestConfirm" runat="server" MaximumRowsParameterName="iMaxRow"
        SelectCountMethod="GetVehicleConfirmManifestCount" SelectMethod="GetVehicleConfirmManifestList"
        StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.Vehicle.VehicleManifest"
        OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" >
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
            
            <asp:ControlParameter Name="iBrandID" ControlID="uoHiddenFieldBrandID" />
            <asp:ControlParameter Name="iVesselID" ControlID="uoHiddenFieldVesselID" />
           
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="uoObjectDataSourceVehicleManifestCancel" runat="server" MaximumRowsParameterName="iMaxRow"
        SelectCountMethod="GetVehicleConfirmManifestCount" SelectMethod="GetVehicleConfirmManifestList"
        StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.Vehicle.VehicleManifest"
        OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" >
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
            
            <asp:ControlParameter Name="iBrandID" ControlID="uoHiddenFieldBrandID" />
            <asp:ControlParameter Name="iVesselID" ControlID="uoHiddenFieldVesselID" />

        </SelectParameters>
    </asp:ObjectDataSource>
   
    </div>
    
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    
    <asp:HiddenField ID="uoHiddenFieldRegion" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldPort" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldVehicle" runat="server" Value="0" />
    
    <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldSortBy" runat="server" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldTransportation" Value = "0" />
    
       
    <asp:HiddenField ID="uoHiddenFieldRouteFrom" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldRouteTo" runat="server" Value="0"/>
    
    <asp:HiddenField ID="uoHiddenFieldCityFrom" runat="server" Value=""/>
    <asp:HiddenField ID="uoHiddenFieldCityTo" runat="server" Value=""/>
    <asp:HiddenField ID="uoHiddenFieldStatus" runat="server" Value=""/>
    
    <asp:HiddenField ID="uoHiddenFieldBrandID" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldVesselID" runat="server" Value="0"/>
    

</asp:Content>
