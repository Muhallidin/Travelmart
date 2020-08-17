<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true" 
    CodeBehind="HotelOverflowBooking3.aspx.cs" Inherits="TRAVELMART.Hotel.HotelOverflowBooking3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .HotelGroup
        {
            font-size: larger;
            font-weight: bold;
            text-decoration: underline;
            color: #82ABB8;
        }
        .Details
        {
            font-size: small;
            text-align: right;
            vertical-align: bottom;
            display: inline;
        }
        .Padding
        {
            padding-left: 10px;
        }
        .Header
        {
            font-size: medium;
            font-weight: bold;
            text-decoration: underline;
            text-align: center;
        }
        .Bullets
        {
            height: auto;
            text-align: left;
            list-style-type: circle;
            font-size: small;
            vertical-align: middle;
            display: block;
        }
        
        
        .DivHotelNight
        {
            position:absolute;
            top: 354px;
            left: 400.5px;
            border-style:solid;
            border-width:thin;
            border-color:#C0C0C0;
            /*<%--width:36em; height:17em;--%>*/
            margin-top: -9em; /*set to a negative number 1/2 of your height*/
            margin-left: -15em; /*set to a negative number 1/2 of your width*/
        }
        
        
    </style>
</asp:Content>
<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <%--<div class="PageTitle">
        Hotel Overflow Bookings</div>--%>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="PageTitle">
            <td align="left">
                Hotel Overflow Bookings
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
                <asp:Label ID="Label7" runat="server" Text="Room Type:" Font-Size="14px"></asp:Label>&nbsp;
                <asp:DropDownList ID="uoDropDownListRoomType" runat="server" Width="100px" 
                    AutoPostBack="true" CssClass="SmallText"
                    onselectedindexchanged="DropDownListRoomType_SelectedIndexChanged" >
                    <asp:ListItem Selected="True" Value="0">--Select Room--</asp:ListItem>
                    <asp:ListItem Value="2">Double</asp:ListItem>
                    <asp:ListItem Value="1">Single</asp:ListItem>
                </asp:DropDownList>
            
                &nbsp;&nbsp;                
                <asp:Label ID="Label8" runat="server" Text="Region:" Font-Size="14px"></asp:Label> &nbsp;
                <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="130px" AutoPostBack="true"
                    OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged" CssClass="SmallText">
                </asp:DropDownList>
                &nbsp;&nbsp;
                
                <asp:Label ID="Label9" runat="server" Text="Port:" Font-Size="14px"></asp:Label> &nbsp;
                <asp:DropDownList ID="uoDropDownListPortPerRegion" runat="server" Width="150px" AutoPostBack="true"
                    AppendDataBoundItems="true" OnSelectedIndexChanged="uoDropDownListPortPerRegion_SelectedIndexChanged"
                    CssClass="SmallText">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidePlaceHolder" runat="server">

    <script type="text/javascript">

        function CloseModal(strURL) {
            window.location = strURL;
        }



        function SetSettings(chk) {
            var status = chk.checked;
            $('input:checkbox[name *= uoSelectCheckBox]').attr('checked', status);
        }
        
        function SetSettingsRemove(chk) {
            var status = chk.checked;
            $('input:checkbox[name *= uoCheckBoxRemove]').attr('checked', status);
        }

        function SetOverflowResolution() {
            var ht = $(window).height(); // * 0.54; //550;
            var wd = $(window).width() * 0.60;
            var ht2 = $(window).height(); // * 0.60; //550;
            var ht3 = $(window).height(); // * 0.60;
            var wd2 = $(window).width() - (wd + 45); // * 0.30;

            var screenH = screen.height;
            var percent = 0.55;

            if (screen.height <= 600) {
                ht = ht * 0.30;
                ht2 = ht2 * 0.40;
                ht3 = ht3 * 0.40;
                wd2 = $(window).width() * 0.30;

            }
            else if (screen.height <= 720) {
                ht = ht * 0.45;
                ht2 = ht2 * 0.50;
                ht3 = ht3 * 0.50;
            }
            else {

                if (screenH = 768) {
                    percent = (510 - (screenH - ht)) / ht;
                }
                ht = ht * percent;
                ht2 = ht2 * 0.65;

            }

            $("#Bv").height(ht);
            $("#<%= uoPanelHotels.ClientID %>").height(ht2);

            $("#Av").width(wd);
            $("#Bv").width(wd);
            //$("#<%= uoPanelHotels.ClientID %>").width(wd2);
            $("#HotelDiv").width(wd2);
        }

      

        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;

        }  
                     
    </script>
   
    
    <table>
        <tr>
            <td>
                <asp:Button runat="server" ID="uoBtnExportList" CssClass="SmallButton" Text="Export Overflow List"
                Width = "120px" Visible="true" OnClick="uoBtnExportList_Click" />
            </td>
            <td>
                <a href="#ucDivComment" id="aShowComment" onclick="javascript:clickShowComment();"> 
                    <asp:Button runat="server" ID="uoButtonRemove" CssClass="SmallButton" Text="Remove Overflow"
                     Width = "120px"  Visible="true" />
                 </a>
            </td>
             <td>
                <a href="HotelOverflowBookingsRemoved.aspx" id="aBtnViewRemoved" > 
                    <asp:Button runat="server" ID="uoButtonViewRemoved" CssClass="SmallButton" Text="View Removed Records"
                     Width = "140px"  Visible="true" />
                 </a>
            </td>
        </tr>
    </table>
    
    
                
    <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;
        position: relative;">
        <asp:ListView runat="server" ID="ListView1" OnItemCommand="ListView1_ItemCommand">
            <LayoutTemplate>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                    <tr>
                        <th style="width:50px; text-align:center">
                            <asp:Label runat ="server" ID="Label15" Text="Book" Width="45px"></asp:Label>
                            &nbsp;
                            <asp:CheckBox runat="server" ID="uoCheckBoxCheckAll" onclick="SetSettings(this);"
                                Style="width: 50px" />
                        </th>
                        <th style="width:50px; text-align:center">
                            <asp:Label runat ="server" ID="Label4" Text="Remove" Width="45px"></asp:Label>
                            &nbsp;
                            <asp:CheckBox runat="server" ID="uoCheckBoxRemoveAll" onclick="SetSettingsRemove(this);"
                                Style="width: 50px" />
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHotelName" runat="server" CommandName="SortByHotelName"
                                Text="Hotel Name" Width="199px"></asp:LinkButton>
                        </th>
                        <th>
                            <%--<asp:Label runat="server" ID="uoLabelHotelCity" Width="50px" Text="Hotel City"></asp:Label>--%>
                            
                            <label id></label>
                            
                            <asp:LinkButton ID="uoLabelHotelCity" runat="server" CommandName="SortByHotelCity"
                                Text="Hotel City" Width="43px"></asp:LinkButton>
                        </th>
                        
                        <th>
                            <asp:Label runat="server" ID="lblHotelNight" Width="50px" Text="Hotel Night"></asp:Label>
                        </th>
                        
                        
                        <th>
                            <asp:Label runat="server" ID="uoLblE1TRHeader" Width="50px" Text="E1 TRId"></asp:Label>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton17" runat="server" CommandName="SortByRecLoc" Text="Record Locator"
                                Width="100px"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonEmpID" runat="server" CommandName="SortByEmpID" Width="80px"
                                Text="Employee Id"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonName" runat="server" CommandName="SortByName" Width="205px"
                                Text="Name"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonStatus" runat="server" CommandName="SortByStatus"
                                Width="60px" Text="Status"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonOnOffDate" runat="server" CommandName="SortByOnOffDate"
                                Width="80px" Text="On/Off Date"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByCheckDate" Width="80px"
                                Text="Check-In Date"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByGender" Width="60px"
                                Text="Gender"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByRank" Width="200px"
                                Text="Rank"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="SortByStripe" Width="50px"
                                Text="Stripe"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByRoomType" Width="70px"
                                Text="Room Type"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByNationality" Width="200px"
                                Text="Nationality"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton7" runat="server" CommandName="SortByPort" Width="150px"
                                Text="Port"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton8" runat="server" CommandName="SortByShip" Width="150px"
                                Text="Ship"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton9" runat="server" CommandName="SortByArrDepDate" Width="80px"
                                Text="Arvl/Dept Date"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton10" runat="server" CommandName="SortByArrDepTime" Width="80px"
                                Text="Arvl/Dept Time"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton11" runat="server" CommandName="SortByFlightNo" Width="60px"
                                Text="Flight No."></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton12" runat="server" CommandName="SortByCarrier" Width="60px"
                                Text="Carrier"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton13" runat="server" CommandName="SortByFromCity" Width="60px"
                                Text="From City"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton14" runat="server" CommandName="SortByToCity" Width="60px"
                                Text="To City"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton15" runat="server" CommandName="SortByReasonCode" Width="80px"
                                Text="Reason Code"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton16" runat="server" CommandName="SortByRemarks" Width="120px"
                                Text="Booking Remarks"></asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton18" runat="server" CommandName="SortByRemarks" Width="120px"
                                Text="Created Date"></asp:LinkButton>                         
                        </th>
                        <th>
                            <asp:Label runat="server" ID="Label11" Text="" Width="10px">
                            </asp:Label>
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div id="Bv" style="overflow: auto; overflow-x: auto; overflow-y: auto; position: relative;"
        onscroll="divScrollL();">
        <asp:ListView runat="server" ID="uoOverflowList">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" id="uoTblOverflowList">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <%# OverflowChangeRowColor()%>
                <%-- <td class="hideElement">
                                                           
                </td>--%>
              <%--  <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label4" Width="205px" Text='<%# Eval("HotelOverflowID") %>'></asp:Label>
                </td>--%>
                <td style="white-space: normal; width:50px">
                    <asp:CheckBox CssClass="Checkbox" Width="50px" ID="uoSelectCheckBox" runat="server" />
                    <asp:HiddenField runat="server" ID="hfReqId" Value='<%# Eval("TravelReqId") %>' />
                    <asp:HiddenField runat="server" ID="hfRoomType" Value='<%# Eval("RoomTypeId") %>' />
                    <asp:HiddenField runat="server" ID="hfPortId" Value='<%# Eval("PortId") %>' />
                    <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IDBigint") %>' />
                    <asp:HiddenField runat="server" ID="hfSeqNo" Value='<%# Eval("SeqNo") %>' />
                   <asp:HiddenField runat="server" ID="hfHotelOverflowID" Value='<%# Eval("HotelOverflowID") %>' />
                </td>
                <td style="white-space: normal;width:50px">
                    <asp:CheckBox CssClass="Checkbox" Width="48px" ID="uoCheckBoxRemove" runat="server" />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLabelHotelName" Width="205px" Text='<%# Eval("HotelName") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLabelHotelCity" Width="50px" Text='<%# Eval("HotelCity") %>'></asp:Label>
                    <asp:HiddenField runat="server" ID="uoHiddenFieldIsByPort" Value='<%# Eval("IsByPort").ToString() %>' />
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="hotelNites" Width="50px" Text='<%# Eval("HotelNights") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblE1TR" Width="50px" Text='<%# Eval("E1TravelReqId") %>'></asp:Label>
                </td>
                
                
                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRecLoc" Width="108px" Text='<%# Eval("RecordLocator") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblSeafarerID" Width="85px" Text='<%# Eval("SeafarerId")%>'></asp:Label>
                </td>
                <td class="leftAligned">
                    <%--<asp:Label runat="server" ID="uoLblSeafarerName" Width="205px" Text='<%# Eval("SeafarerName") %>'></asp:Label>--%>
                    <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "../SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("SeafarerId") + "&recloc=&st=" + Eval("SFStatus") + "&ID=0&trID="+ Eval("TravelReqId") +"&manualReqID=0" + "&dt=" + Request.QueryString["dt"]%>'
                        runat="server" Width="210px" Text='<%# Eval("SeafarerName")%>'></asp:HyperLink>
                    <%--<asp:Image ID="imgHotelRequest" Width="15px" ImageUrl="~/Images/Hotel.png" runat="server"
                        ToolTip="Hotel Request" Visible='<%# bool.Parse(Eval("HotelRequest").ToString()) %>' />--%>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblStatus" Width="68px" Text='<%# Eval("SFStatus") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblOnOff" Width="88px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCheckIn" Width="85px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckInDate"))%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblGender" Width="65px" Text='<%# Eval("Gender") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRank" Width="208px" Text='<%# Eval("RankName") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblStripe" Width="55px" Text='<%# String.Format("{0:0.00}", Eval("Stripes"))%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRoomType" Width="75px" Text='<%# Eval("RoomType") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblNationality" Width="205px" Text='<%# Eval("Nationality") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblPortName" Width="155px" Text='<%# Eval("PortName") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLabelVesselName" Width="155px" Text='<%# Eval("VesselName") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblADDate" Width="88px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("ArrivalDepartureDatetime"))%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblADTime" Width="88px" Text='<%# String.Format("{0:HH:mm:ss}", Eval("ArrivalDepartureDatetime"))%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblFNo" Width="65px" Text='<%# Eval("FlightNo")%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCarrier" Width="65px" Text='<%# Eval("Carrier")%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblFromCity" Width="68px" Text='<%# Eval("FromCity")%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblToCity" Width="68px" Text='<%# Eval("ToCity")%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblReasonCode" Width="85px" Text='<%# Eval("ReasonCode")%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label2" Width="125px" Text='<%# Eval("BookingRemarks")%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label12" Width="125px" Text='<%# String.Format("{0:dd-MMM-yyyy HH:mm:ss}", Eval("Datecreated"))%>'></asp:Label>
                </td>                
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                    <tr>
                        <td colspan="20" class="leftAligned">
                            <asp:Label runat="server" ID="Label10" Text="No Records" Width="1500px">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
   
        
    <div id="DivHotelNight" class="DivHotelNight" style="background-color:White;padding:5px 10px 5px 10px; display:none;" >
    
        <div class="PageTitle"  style="overflow:hidden; white-space:nowrap; ">
           Select Hotel Night
        </div>
        <hr />
        <asp:CheckBoxList id="chkCheckList" runat="server" class="DivRoomList"/>
        
        <%--
            <select multiple>
              <option value="volvo">Volvo</option>
              <option value="saab">Saab</option>
              <option value="opel">Opel</option>
              <option value="audi">Audi</option>
            </select>
        --%>
        
    </div>
        
    <asp:HiddenField runat="server" ID="uoHiddenFieldUser" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldStartDate" Value="" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

    <script type="text/javascript">
 
        function InitHotelNite() {

            $("#ctl00_SidePlaceHolder_ListView1_ctrl0_lblHotelNight").hover(function(event) {
//                $('input:checkbox[name *= chkCheckList]').attr('checked', false);
                var vtop = event.pageY;
                var vleft = event.pageX;
                $("#DivHotelNight").css({ left: vleft + 200, top: vtop + 100 });
                $("#DivHotelNight").fadeIn();
            });

            $("#DivHotelNight").click(function() {
                $("#DivHotelNight").fadeOut();
            });

            $("[name*=chkCheckList]").click(function() {
                $('input:checkbox[name *= chkCheckList]').attr('checked', false);
                SetSettingHotelNite($(this).parent('td').children()[1].innerHTML);
                $(this).parent('td').children()[0].checked = true;
            });
            
        }

        


        function SetSettingHotelNite(val) {
            var HotelNight = val.split(" ")[0];
            var nite = 0;
            for (var j = 0; j < $("[id$=uoSelectCheckBox]").length; j++) {
                nite = document.getElementById('ctl00_SidePlaceHolder_uoOverflowList_ctrl' + j + '_hotelNites').innerHTML;
                if (HotelNight == nite) {
                    document.getElementById('ctl00_SidePlaceHolder_uoOverflowList_ctrl' + j + '_uoSelectCheckBox').checked = true;
                }
                else {
                    document.getElementById('ctl00_SidePlaceHolder_uoOverflowList_ctrl' + j + '_uoSelectCheckBox').checked = false;
                }
            }
            $("[name*=uoTxtBoxDays]").attr('value', HotelNight);
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

        function ValidateIfPastDate(pDate, pControl) {
            if (ValidateDate(pDate)) {

                var currentDate = new Date();
                var currentDate2 = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate());

                var selectedDate = Date.parse(pDate);

                if (selectedDate < currentDate2) {
                    alert("Past date is invalid!");
                    pControl.value = currentDate2.format('MM/dd/yyyy');
                    return false;
                }
            }
            else {
                alert("Invalid date!");

                return false;
            }
            return true;
        }

        function OnDateChange(CheckIn) {            
            ValidateIfPastDate(CheckIn.value, CheckIn);
        }

        $(document).ready(function() {
            SetOverflowResolution();
            filterSettings();
            ShowPopup();
            InitHotelNite();
        });



        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetOverflowResolution();
                filterSettings();
                ShowPopup();
                InitHotelNite();
            }
        }

        function ShowPopup() {
            $(".HotelLink").fancybox(
            {
                'centerOnScroll': false,
                'width': '35%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupHotel.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });

            $(".Link").fancybox(
            {
                'centerOnScroll': false,
                'width': '35%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupHotel.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });

            $("#aBtnViewRemoved").fancybox(
            {
                'centerOnScroll': false,
                'width': '75%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe'
            });
        }
                      

        function Save() {
            var listOfCheckbox = $('input:checkbox[name*=uoSelectCheckBox]:checked');

            var i = 0;
            $(listOfCheckbox).each(function() {
                i++;
            });


            if (i == 0) {
                alert("No selected request!");
                return false;
            }
            else {

                var uoDropDownListRoomType = $("#<%= uoHiddenFieldSelectedRoom.ClientID%>");
                var uoHiddenFieldRoomType = $("#<%= uoHiddenFieldRoomType.ClientID%>");
                uoHiddenFieldRoomType.val("");

                var IsSingleRoom = false;
                var IsDoubleRoom = false;

                var uoSelectCheckBox;
                var hfRoomType;
                var iNoOfDays;
                $("#uoTblDetails tr").each(function(ev) {

                    iNoOfDays = $(this).find("[id$=uoTxtBoxDays]").val();
                });

                $("#uoTblExceptionList tr").each(function(ev) {
                    uoSelectCheckBox = $(this).find("[id$=uoSelectCheckBox]").attr("checked");
                    hfRoomType = $(this).find("[id$=hfRoomType]").val();

                    if (uoSelectCheckBox) {
                        if (hfRoomType == "1") {
                            IsSingleRoom = true;
                        }
                        if (hfRoomType == "2") {
                            IsDoubleRoom = true;
                        }
                    }
                });

                if (IsSingleRoom && !IsDoubleRoom) {
                    uoHiddenFieldRoomType.val("1");
                }
                else if (!IsSingleRoom && IsDoubleRoom) {
                    uoHiddenFieldRoomType.val("2");
                }
                else if (IsSingleRoom && IsDoubleRoom) {
                    uoHiddenFieldRoomType.val("3");
                }
                else {
                    uoHiddenFieldRoomType.val("");
                }

                var sMsg = "";

                if (uoHiddenFieldRoomType.val() == "1") {
                    if (uoDropDownListRoomType.val() == "2") {
                        if (sMsg != "") {
                            sMsg += "\n";
                        }
                        sMsg += "Seafarer’s room accommodation is supposed to be Single Cabin.";
                    }
                }
                if (uoHiddenFieldRoomType.val() == "2") {
                    if (uoDropDownListRoomType.val() == "1") {
                        if (sMsg != "") {
                            sMsg += "\n";
                        }
                        sMsg += "Seafarer’s room accommodation is supposed to be Double Cabin.";
                    }
                }
                if (uoHiddenFieldRoomType.val() == "3") {
                    if (uoDropDownListRoomType.val() == "1") {
                        if (sMsg != "") {
                            sMsg += "\n";
                        }
                        sMsg += "There are some seafarers whose room accommodation is supposed to be Double Cabin.";
                    }
                }
                if (uoHiddenFieldRoomType.val() == "3") {
                    if (uoDropDownListRoomType.val() == "2") {
                        if (sMsg != "") {
                            sMsg += "\n";
                        }
                        sMsg += "There are some seafarers whose room accommodation is supposed to be Single Cabin.";
                    }
                }


                if (iNoOfDays > 1) {
                    if (sMsg != "") {
                        sMsg += "\n";
                    }
                    sMsg += "Number of days is more than 1.";
                }
                if (sMsg += "") {
                    sMsg += "\n\nDo you want to continue?";
                    var x = confirm(sMsg);
                    if (x) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                return true;
            }
        }
        function filterSettings() {

            $("#aShowComment").fancybox(
                {
                    'centerOnScroll': false,
                    'width': '50%',
                    'height': '40%',
                    'autoScale': true,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut'
                });
        }

        function clickShowComment() {
            
            if ($('input:checkbox[name *= uoCheckBoxRemove]:checked').length > 0) {
                GetOverflowToRemove();
                return true;
            }
            else {
                alert("No selected record to remove!");
                return false;
            }
        }
    
        function GetOverflowToRemove() {

            var sOverID = '';
            $("#uoTblOverflowList tr").each(function(i, element) {
                var $TDs = $(this).find('td');
                var chk = $('td:eq(1) input[name*="uoCheckBoxRemove"]', this);

                var OverflowID = $('td:eq(0) input[name*="hfHotelOverflowID"]', this);
               

                if (chk.attr('checked') != undefined) {

                    if (chk.attr('checked')) {
                        //alert(OverflowID.val());
                        sOverID = sOverID + ';' + OverflowID.val();
                    }
                }
            });

            $("#<%=uoHiddenFieldOverflowToRemove.ClientID %>").val(sOverID);            
        }

        function ConfirmRemove() {
            var isRemove = 'true';

            var sComment = $("#<%=uoTextBoxComment.ClientID %>").val();
            var sOverflowId = $("#<%=uoHiddenFieldOverflowToRemove.ClientID %>").val();
             

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../PageMethods.aspx/OverflowAddRemoveFromList",
                data: "{'sOverflowId': '" + sOverflowId + "', 'sIsRemove': '" + isRemove + "', 'sComment': '" + sComment + "', 'sPage':'HotelOverflowBooking3.aspx'}",
                dataType: "json",
                success: function(data) {
                    alert("Overflow List successfully updated!");
                    $("#<%= uoHiddenFieldRemoveFromList.ClientID %>").val("1");
                    $("#aspnetForm").submit();
                },
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
            return true;
        }            
              

        function OpenContract(branchID, contractID) {
            var URLString = "../ContractManagement/HotelContractView.aspx?vId=" + branchID;
            URLString += "&cId=" + contractID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }

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

        function getRoomType(RoomType) {
            $("#<%=uoHiddenFieldSelectedRoom.ClientID %>").val(RoomType);
        }

        function GetNoOfNites(nites) {
            //var d = nites.getAttribute('value');
            var newval = $(nites).val();
            $("#<%=uoHiddenFieldNoOfNites.ClientID %>").val(newval);
            //alert("hello:" + newval);
        }

        function RefreshPageFromPopupViewRemove() {
            $.fancybox.close();
            var a = $("#<%=uoHiddenFieldRemoveFromList.ClientID %>").val();
            if (a == '1') {
                $("#<%=uoButtonViewRemoved.ClientID %>").click();
            }
            $("#<%=uoHiddenFieldRemoveFromList.ClientID %>").val("0");
        }

    </script>

    <div style="overflow: auto; overflow-x: auto; overflow-y: auto; text-align: left;
        position: relative;" id="HotelDiv">
        <asp:Panel runat="server" ID="uoPanelHotels" Width="100%">
            <asp:Repeater runat="server" ID="uoRepeaterHotels" OnItemCommand="uoRepeaterHotels_ItemCommand">
                <ItemTemplate>
                    <table width="100%" runat="server" id="uoPanelBranchNameHeader">
                        <tr>
                            <td align="left" style="width: 75%">
                                <asp:LinkButton runat="server" ID="uoLinkBranch" CommandName="Select" Text='<%# Eval("BranchName") %>'
                                    CssClass="HotelGroup" CommandArgument='<%# Eval("BranchId")%>'></asp:LinkButton>
                            </td>
                            <td align="right" style="padding-right: 5px;">
                                <asp:ImageButton runat="server" ID="uoBtnContract" ImageUrl="~/Images/contract.jpg"
                                    Visible='<%# SetContractVisibility() %>' Height="15px" Width="15px" OnClientClick='<%# "return OpenContract(\"" + Eval("BranchId") + "\",\"" + Eval("ContractId") + "\")" %>'
                                    CssClass="rightAligned" ToolTip="View Contract" />
                                <asp:ImageButton runat="server" ID="uoBtnEvent" ImageUrl="~/Images/calendar1.png"
                                    Visible='<%# SetEventVisibility() %>' Height="15px" Width="15px" OnClientClick='<%# "return OpenEventsList(\"" + Eval("BranchId") + "\",\"" + Eval("CityId") + "\",\"" + Eval("colDate") + "\")" %>'
                                    CssClass="rightAligned" ToolTip="View Event" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel runat="server" ID="uoPanelBranchName" BorderColor="#ECDFC4" Width="100%"
                        BorderStyle="Solid">
                        <cc1:CollapsiblePanelExtender ID="cpe" runat="server" TargetControlID="uoPanelBranchName"
                            ExpandControlID="uoLinkBranch" CollapseControlID="uoLinkBranch" TextLabelID="uoLinkBranch"
                            Collapsed="true" ExpandDirection="Vertical">
                        </cc1:CollapsiblePanelExtender>
                        <table width="100%" runat="server" id="uoTblDetails">
                            <tr id="Tr1" visible='<%# !Convert.ToBoolean(Eval("isAccredited")) %>' runat="server">
                                <td colspan="2">
                                    <asp:ListView runat="server" ID="uoRoomList">
                                        <LayoutTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                                <tr>
                                                    <th>
                                                        <asp:Label runat="server" ID="uoLblDateHdr" Text="Date" Width="62px"></asp:Label>
                                                    </th>
                                                    <th style="white-space: normal;">
                                                        <asp:Label runat="server" ID="uoLblCabinHdr" Text="Cabin" Width="40px"></asp:Label>
                                                    </th>
                                                    <th style="white-space: normal;">
                                                        <asp:Label runat="server" ID="uoLblCrewHdr" Text="Booked Room" Width="40px"></asp:Label>
                                                    </th>
                                                    <th colspan="6" style="white-space: normal;">
                                                        <asp:Label runat="server" ID="uoLblAvailableHdr" Text="Available Rooms" Width="75px"></asp:Label>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <th>
                                                    </th>
                                                    <th>
                                                    </th>
                                                    <th>
                                                    </th>
                                                    <th style="white-space: normal;">
                                                        <asp:Label runat="server" ID="Label13" Text="C" Width="25px" ToolTip="Contract"></asp:Label>
                                                    </th>
                                                    <th style="white-space: normal;">
                                                        <asp:Label runat="server" ID="LblOverride" Text="O" Width="25px" ToolTip="Override"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label runat="server" ID="Label1" Text="R" Width="25px" ToolTip="Remaining Room"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label runat="server" ID="LblOverrideEdit" Text="" Width="25px" ToolTip="Add Override Room Blocks"></asp:Label>
                                                    </th>
                                                    <th style="white-space: normal;">
                                                        <asp:Label runat="server" ID="LblEmergency" Text="E" Width="25px" ToolTip="Emergency"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label runat="server" ID="LblEmergencyEdit" Text="" Width="25px" ToolTip="Add Emergency Room Blocks"></asp:Label>
                                                    </th>
                                                </tr>
                                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="uoLblDate" Width="62px" Text='<%# setDate() %>'></asp:Label>
                                                </td>
                                                <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="uoLblRoom" Width="40px" Text='<%#Eval("Room") %>'></asp:Label>
                                                </td>
                                                <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="uoLblCrew" Width="40px" Text='<%#Eval("TotalBooking") %>'></asp:Label>
                                                </td>
                                                <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="uoLblContractBlocks" Width="25px" Text='<%#Eval("ContractBlocks").ToString().Replace(".0", "") %>'></asp:Label>
                                                </td>
                                                <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="uoLblOverrideBlocks" Width="25px" Text='<%# Eval("OverrideBlocks").ToString().Replace(".0", "") %>'></asp:Label>
                                                </td>
                                                <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="Label3" Width="25px" Text='<%# Eval("RemainingRooms").ToString().Replace(".0", "") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="uoHyperLinkEditOverride" CssClass="Link" runat="server" Width="25px"
                                                        NavigateUrl='<%# "~/HotelRoomOverrideEdit2.aspx?bID=" + Eval("BranchId") + "&rID=" + Eval("RoomTypeId") + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "&rc=" + String.Format("{0:#####}", Eval("ReservedOverride")) %>'>Add</asp:HyperLink>
                                                </td>
                                                <td style="white-space: normal;">
                                                    <asp:Label runat="server" ID="Label14" Width="25px" Text='<%#Eval("EmergencyBlocks").ToString().Replace(".0", "") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink CssClass="Link" ID="uoHyperLinkEditEmergency" runat="server" NavigateUrl='<%# "~/HotelRoomEmergencyEdit2.aspx?bID=" + Eval("BranchId") + "&rID=" + Eval("RoomTypeId") + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) %>'>Add</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr id="Tr2" visible='<%# !Convert.ToBoolean(Eval("isAccredited")) %>' runat="server">
                                <td valign="middle">
                                    <asp:Label ID="uoAllocationHdr" runat="server" Text="Room Allocations : "></asp:Label>
                                </td>
                                <td class="LeftClass">
                                    <asp:RadioButtonList runat="server" ID="uoRBtnAllocations" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Text="Contract / Override" Value="0"></asp:ListItem>
                                      <%--  <asp:ListItem Text="Override" Value="1"></asp:ListItem>--%>
                                        <asp:ListItem Text="Emergency" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvAllocations" ControlToValidate="uoRBtnAllocations"
                                        ValidationGroup="Save" ErrorMessage="Room Allocation Required.">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass">
                                    Hotel Status :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:DropDownList runat="server" ID="uoDropDownListStatus" Width="200px">
                                        <asp:ListItem Selected="True" Text="UNUSED" Value="Unused"></asp:ListItem>
                                        <asp:ListItem Text="CHECKED IN" Value="Checked In"></asp:ListItem>
                                        <asp:ListItem Text="CHECKED OUT" Value="Checked Out"></asp:ListItem>
                                        <asp:ListItem Text="CANCELLED" Value="Cancelled"></asp:ListItem>
                                        <asp:ListItem Text="NO SHOW" Value="No Show"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass">
                                    Room Type :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:DropDownList runat="server" ID="uoDropDownListRoomType" Width="200px" onchange="return getRoomType(this);">
                                        <asp:ListItem Selected="True" Text="Single" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Double" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvRoom" ValidationGroup="Save" ControlToValidate="uoDropDownListRoomType">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass">
                                    Check-In Date :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:TextBox runat="server" ID="uoTxtBoxCheckIn" Height="22px" Width="65px" onchange="return OnDateChange(this);"></asp:TextBox>
                                    <cc1:TextBoxWatermarkExtender ID="uoTxtBoxCheckIn_TextBoxWatermarkExtender" runat="server"
                                        Enabled="True" TargetControlID="uoTxtBoxCheckIn" WatermarkCssClass="fieldWatermark"
                                        WatermarkText="MM/dd/yyyy">
                                    </cc1:TextBoxWatermarkExtender>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <cc1:CalendarExtender ID="uoTxtBoxCheckIn_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="uoTxtBoxCheckIn" PopupButtonID="ImageButton1" Format="MM/dd/yyyy">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="uoTxtBoxCheckIn_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTxtBoxCheckIn"
                                        UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvCheckIn" ValidationGroup="Save"
                                        ErrorMessage="Check In date required." ControlToValidate="uoTxtBoxCheckIn">*</asp:RequiredFieldValidator>
                                    <asp:Button runat="server" ID="uoBtnCheckRooms" Text="Check Rooms" Width="100px"
                                        CssClass="SmallButton" OnClick="uoBtnCheckRooms_Click" Visible='<%# !Convert.ToBoolean(Eval("isAccredited")) %>' />
                                </td>
                            </tr>
                            <tr id="Tr3" visible='<%# !Convert.ToBoolean(Eval("isAccredited")) %>' runat="server">
                                <td>
                                </td>
                                <td class="LeftClass">
                                    <span class="RedNotification">Click Check Rooms button
                                        <br />
                                        to validate available rooms.</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass">
                                    No. of Nights :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:TextBox runat="server" name="uoTxtBoxDays" ID="uoTxtBoxDays" Text="1" Width="90px"
                                        MaxLength="3" onkeyup="validateDays();" onkeypress="return validate(event);"
                                        onchange="GetNoOfNites(this);">
                                    </asp:TextBox>
                                    <cc1:NumericUpDownExtender ID="uoTxtBoxDays_NumericUpDownExtender" runat="server"
                                        Enabled="True" Maximum="100" Minimum="1" RefValues="" ServiceDownMethod="" ServiceDownPath=""
                                        ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="uoTxtBoxDays"
                                        Width="90">
                                    </cc1:NumericUpDownExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass">
                                    Time :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:TextBox runat="server" ID="uoTxtBoxTime" Width="65px" Text="00:00"></asp:TextBox>
                                    <cc1:TextBoxWatermarkExtender ID="uoTxtBoxTime_TextBoxWatermarkExtender" runat="server"
                                        Enabled="True" TargetControlID="uoTxtBoxTime" WatermarkCssClass="fieldWatermark"
                                        WatermarkText="HH:mm">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:MaskedEditExtender ID="uoMaskedEditExtenderCheckInTime" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTxtBoxTime" UserDateFormat="None"
                                        UserTimeFormat="TwentyFourHour">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass">
                                    With Shuttle :
                                </td>
                                <td class="LeftClass">
                                    <asp:CheckBox runat="server" ID="uoCheckBoxShuttle" Text="Transportation" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass">
                                    Remarks :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:TextBox runat="server" ID="uoTxtBoxRemarks" Width="200px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftClass">
                                    Confirmation No. :
                                </td>
                                <td class="LeftClass" style="padding-left: 4px;">
                                    <asp:TextBox runat="server" ID="uoTxtBoxConfirmation" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td class="rightAligned">
                                    <asp:Button runat="server" ID="uoButtonSave" ValidationGroup="Save" Text="Save" CssClass="SmallButton"
                                        OnClientClick="return Save();" OnClick="uoButtonSave_Click" />
                                    &nbsp;
                                    <asp:Button runat="server" ID="uoButtonCancel" Text="Cancel" CssClass="SmallButton"
                                        OnClick="uoButtonCancel_Click" />
                                </td>
                            </tr>
                            <tr style="height: 8px">
                            </tr>
                        </table>
                    </asp:Panel>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>
    </div>
    <asp:HiddenField runat="server" ID="uoHiddenFieldBranch" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldPopEditor" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDateChanged" Value="1" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDaysChanged" Value="1" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRoomTypeChanged" Value="1" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHIddenFieldBranchId" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHIddenFieldSFCount" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRoomChecked" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldPopupHotel" Value="0" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDate" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRoomType" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDateTo" EnableViewState="true" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldSelectedRoom" EnableViewState="true"
        Value="1" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldNoOfNites" EnableViewState="true"
        Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldSortBy" EnableViewState="true" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldOverflowToRemove" Value = "" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRemoveFromList" Value = "" />
    
   <div  style="display:none">
    <div id="ucDivComment">
       <table border="0" cellpadding="0" width="350px">
        <tr class="PageTitle">
          <td >
            Comment
          </td>  
        </tr>       
        <tr>           
            <td >
                <asp:TextBox ID="uoTextBoxComment" runat="server" CssClass="TextBoxInput" TextMode="MultiLine"
                 Width="380px" Height="150px">
                </asp:TextBox></td>
        </tr>        
        <tr>
            <td colspan="4">
            <asp:Button ID="uoButtonSubmitComment" runat="server" Text="Submit" CssClass="SmallButton"  
            OnClientClick="javascript:return ConfirmRemove();" />
            </td>
        </tr>
       </table>              
        </div>
     </div>
</asp:Content>
