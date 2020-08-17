<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    CodeBehind="OkToBrazil.aspx.cs" Inherits="TRAVELMART.OkToBrazil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 143px;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" ID="ContentHeader" runat="server">
    <div class="PageTitle">
        <table width="100%">
            <tr>
                <td align="left">
                    OK to Board in Brazil
                </td>
                <%--<td align="right">
                    Region: &nbsp;&nbsp
                    <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="200px" AutoPostBack="true"
                        OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;Port: &nbsp;&nbsp;
                    <asp:DropDownList ID="uoDropDownListPortPerRegion" runat="server" Width="200px" AutoPostBack="true"
                        AppendDataBoundItems="true" OnSelectedIndexChanged="uoDropDownListPortPerRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>--%>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <%--ContentPlaceHolder1--%>

    <script type="text/javascript" language="javascript">
        function CloseModal(strURL) {
            window.location = strURL;
        }

        $(document).ready(function() {
            SetTRResolution();
            ShowPopup();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
                ShowPopup();
            }
        }

        function SetTRResolution() {
            var ht = $(window).height();
            var ht2 = $(window).height() ;
            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.21;
                ht2 = ht2 * 0.46;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.40;
                ht2 = ht2 * 0.59;
            }
            else {
                ht = ht * 0.5;
                ht2 = ht2 * 0.65;
            }


            $("#Av").width(wd);
            $("#Bv").height(ht);
            $("#PG").height(ht2);
            $("#Bv").width(wd);

            $("#PG").width(wd);
        }


        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;

        }  
    </script>

    <%--<div class="PageTitle">
            No Travel Request List
    </div>--%>
    <div id="PG" style="width: auto; height: auto; overflow: auto;">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <div align="left">
                    <%--<table width="100%" class="LeftClass" onload="opener.location.reload();">
                        <tr>
                            <td class="style1">
                                <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>--%>
                   <%-- <table width="100%" class="LeftClass" id="uoTableAdvanceSearch" runat="server">
                        <tr id="uoTRVessel" runat="server">
                            <td class="contentCaption">
                                Ship:
                            </td>
                            <td class="contentValue">
                                <asp:DropDownList ID="uoDropDownListVessel" runat="server" Width="300px" AppendDataBoundItems="True">
                                    <asp:ListItem>--SELECT SHIP--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="contentCaption">
                                Filter By:
                            </td>
                            <td class="contentValue">
                                <asp:DropDownList ID="uoDropDownListFilterBy" runat="server" Width="300px">
                                    <asp:ListItem Value="1">SEAFARER NAME</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">EMPLOYEE ID</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="uoTextBoxFilter" runat="server" CssClass="TextBoxInput" Width="300px"
                                    onkeypress="return validate(event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="contentCaption">
                                Nationality:
                            </td>
                            <td class="contentValue">
                                <asp:DropDownList ID="uoDropDownListNationality" runat="server" Width="300px" AppendDataBoundItems="True">
                                    <asp:ListItem>--SELECT NATIONALITY--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="contentCaption">
                                Gender:
                            </td>
                            <td class="contentValue">
                                <asp:DropDownList ID="uoDropDownListGender" runat="server" Width="300px" AppendDataBoundItems="True">
                                    <asp:ListItem>--SELECT GENDER--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="contentCaption">
                                Rank:
                            </td>
                            <td class="contentValue">
                                <asp:DropDownList ID="uoDropDownListRank" runat="server" Width="300px" AppendDataBoundItems="True">
                                    <asp:ListItem>--SELECT RANK--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="contentCaption">
                                Status:
                            </td>
                            <td class="contentValue">
                                <asp:DropDownList ID="uoDropDownListStatus" runat="server" Width="300px">
                                    <asp:ListItem Value="0">--SELECT STATUS--</asp:ListItem>
                                    <asp:ListItem>ON</asp:ListItem>
                                    <asp:ListItem>OFF</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="contentCaption">
                                &nbsp;
                            </td>
                            <td class="contentValue">
                                <asp:Button ID="uoButtonView" runat="server" CssClass="SmallButton" OnClick="uoButtonView_Click"
                                    Text="View" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>--%>
                    <%--<asp:Panel runat="server" ID="uoPanelNavi" ScrollBars="Auto" Height="100%" >--%>
                    <table width="100%" class="LeftClass" style="vertical-align: top">
                        <tr>
                            <td class="LeftClass">
                                <asp:Button runat="server" ID="uoBtnSaveToExcel" Text="Export List" CssClass="SmallButton"
                                    OnClick="uoBtnSaveToExcel_Click" />
                                &nbsp;
                                <asp:CheckBox runat="server" ID="uoChkBoxPDF" Text="Save as .PDF" CssClass="SmallButton"
                                    Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
                    <asp:ListView runat="server" ID="ListView1" OnItemCommand="ListView1_ItemCommand">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table class="listViewTable" width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <th style="white-space: normal;">
                                        <asp:LinkButton Width="54px" ID="uoLinkBtnHeaderE1ID" runat="server" CommandName="SortByE1">E1 ID</asp:LinkButton>
                                    </th>
                                    <th style="white-space: normal;">
                                        <asp:LinkButton Width="54px" ID="LinkButton7" runat="server" CommandName="SortByRecLoc">Rec Loc</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="110px" ID="uoLinkButtonHeaderName" runat="server" CommandName="SortByLastname">Last Name</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="110px" ID="LinkButton4" runat="server" CommandName="SortByFirstname">First Name</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="94px" ID="uoLinkButtonHeaderOnOff" runat="server" CommandName="SortByOnOff">On/Off Date</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="80px" ID="uoLinkButtonHeaderCrew" runat="server" CommandName="SortByStatus">Crew Status</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="75px" ID="uoLinkButtonHeaderReasonCode" runat="server" CommandName="SortByReason">Reason Code</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="150px" ID="uoLinkButtonHeaderPort" runat="server" CommandName="SortByPort">Port</asp:LinkButton>
                                    </th>                                   
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="180px" ID="uoLinkButtonHeaderVessel" runat="server" CommandName="SortByShip">Ship</asp:LinkButton>
                                    </th>                                   
                                    <th style="text-align: center; white-space: normal;">
                                        <%--<asp:Label runat="server" ID="uoLblNationalityHeader" Text="Nationality" Width="200px"></asp:Label>--%>
                                        <asp:LinkButton Width="200px" ID="uoLinkButtonHeaderNationality" runat="server" CommandName="SortByNationality">Nationality</asp:LinkButton>
                                    </th>
                                    <%--<th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton Width="150px" ID="uoLinkButtonHeaderArrivalDepartureDate" runat="server" CommandName="SortByArrivalDepartureDate">Arrival/Departure Date</asp:LinkButton>
                                </th>--%>
                                    <%--<th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton33" Text="Birthday" Width="70px" runat="server" CommandName="SortByBirthday" />
                                    </th>--%>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="94px" ID="uoLinkButtonHeaderDepartureDate" runat="server"
                                            CommandName="SortByDepartureDate">Dept Date</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="60px" ID="LinkButton2" runat="server" CommandName="SortByDepartureDate">Dept Time</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="94px" ID="uoLinkButtonHeaderArrivalDate" runat="server" CommandName="SortByArrivalDate">Arvl Date</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="60px" ID="LinkButton3" runat="server" CommandName="SortByArrivalDate">Arvl Time</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="60px" ID="uoLinkButtonHeaderAirline" runat="server" CommandName="SortByAirline">Airline</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="60px" ID="uoLinkButtonHeaderFlightNo" runat="server" CommandName="SortByFlightNo">Flight No</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <%--<asp:Label runat="server" ID="uoLabelOriginHeader" Text="Origin" Width="55px"></asp:Label>--%>
                                        <asp:LinkButton Width="55px" ID="uoLinkButtonHeaderOrigin" runat="server" CommandName="SortByOrigin">Origin Airport</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <%--<asp:Label runat="server" ID="uoLabelDestinationHeader" Text="Destination" Width="75px"></asp:Label>--%>
                                        <asp:LinkButton Width="75px" ID="uoLinkButtonHeaderDestination" runat="server" CommandName="SortByDestination">Destination Airport</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="94px" ID="uoLinkButtonPassport" runat="server" CommandName="SortByPassport">Passport No.</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="94px" ID="uoLinkButtonPassportIssued" runat="server" CommandName="SortByPassIssued">Date Issued</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="94px" ID="uoLinkButtonPassportExp" runat="server" CommandName="SortByPassExp">Passport Expiration</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="94px" ID="LinkButton1" runat="server" CommandName="SortByPassport">Seamans Book No.</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="94px" ID="LinkButton5" runat="server" CommandName="SortByPassIssued">Date Issued</asp:LinkButton>
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton Width="94px" ID="LinkButton6" runat="server" CommandName="SortByPassExp">Expiration</asp:LinkButton>
                                    </th>
                                                                        
                                    <th>
                                        <asp:Label runat="server" ID="Label11" Text="" Width="1000px">
                                        </asp:Label>
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
                <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;" onscroll="divScrollL();">
                    <asp:ListView runat="server" ID="uoListViewOKTB" DataSourceID="uoObjectDataSourceOKTB">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <%--<table class="listViewTable" width="100%">--%>
                            <tr>     
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label Width="60px" runat="server" ID="uoLblSFId" Text='<%# Eval("SeafarerID")%>'></asp:Label>
                                </td>                          
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label Width="60px" runat="server" ID="Label10" Text='<%# Eval("RecLoc")%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label5" Text='<%# Eval("LastName") %>' Width="116px"></asp:Label>
                                </td>
                                <td class="leftAligned">
                                    <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "../SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("SeafarerID") + "&recloc=&st=" + Eval("Status") + "&ID=0&trID="+ Eval("TRID") +"&manualReqID=0" + "&dt=" + Request.QueryString["dt"]%>'
                                        runat="server" Width="116px"><%# Eval("FirstName")%></asp:HyperLink>
                                    <%--<asp:Image ID="Image1" ImageUrl="~/Images/Hotel.png" runat="server" ToolTip="Hotel Request"
                                        Visible='<%# bool.Parse(Eval("HotelRequest").ToString()) %>' />--%>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblOnOff" Width="100px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" Width="86px" ID="uoLblStatus" Text='<%# Eval("Status")%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblReason" Width="81px" Text='<%# Eval("ReasonCode")%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <%--<asp:Label runat="server" ID="uoLblPort" Width="156px" Text='<%# String.Format("{0}-{1}", Eval("PortCode"), Eval("Port")) %>'></asp:Label>--%>
                                    <asp:Label runat="server" ID="uoLblPort" Width="156px" Text='<%# String.Format("{0}-{1}",Eval("PortCode"),Eval("PortName"))%>'></asp:Label>
                                </td>
                                <td class="leftAligned">
                                    <asp:Label runat="server" ID="uoLblVessel" Width="182px" Text='<%# String.Format("{0}-{1}",Eval("VesselCode"),Eval("VesselName"))%>'></asp:Label>&nbsp;
                                   <%-- <asp:Image ID="uoImageSailMaster" ImageUrl="~/Images/Vessel.png" runat="server" ToolTip="With Sail Master"
                                        Visible='<%# bool.Parse(Eval("IsWithSail").ToString()) %>' />
                                    <asp:Label runat="server" ID="uoSpace" Width="18px" Text='' Visible='<%# !bool.Parse(Eval("IsWithSail").ToString()) %>'></asp:Label>--%>
                                </td>                              
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblNationality" Width="206px" Text='<%# Eval("Nationality")%>'></asp:Label>
                                </td>                               
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelDepartureDate" Width="100px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DepartureDatetime"))%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label2" Width="66px" Text='<%#  String.Format("{0:HHmm}", Eval("DepartureDatetime"))%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelArrivalDate" Width="100px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("ArrivalDatetime"))%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label3" Width="66px" Text='<%#  String.Format("{0:HHmm}", Eval("ArrivalDatetime"))%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" Width="66px" ID="uoLabelAirline" Text='<%# Eval("Airline")%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label Width="66px" runat="server" ID="uoLabelFlightNo" Text='<%# Eval("FlightNo")%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelOrigin" Width="61px" Text='<%# Eval("DepartureAirport")%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelDestination" Width="81px" Text='<%# Eval("ArrivalAirport")%>'></asp:Label>
                                </td>
                                
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label6" Width="100px" Text='<%# Eval("PassportNo")%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label4" Width="100px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("PassportIssuedDate"))%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label7" Width="100px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("PassportExpiredDate"))%>'></asp:Label>
                                </td>   
                                
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label1" Width="100px" Text='<%# Eval("SeamansBook")%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label8" Width="100px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("SeaBookIssuedDate"))%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label9" Width="100px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("SeaBookExpiredDate"))%>'></asp:Label>
                                </td>                            
                            </tr>
                            <%-- </table>--%>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table class="" width="100%">
                                <tr>
                                    <th colspan="14">
                                        <asp:Label runat="server" ID="uoScroll" Width="1820px"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="14" class="LeftClass">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                  
                    <asp:ObjectDataSource ID="uoObjectDataSourceOKTB" runat="server" MaximumRowsParameterName="iMaxRow"
                        SelectCountMethod="GetOKToBrazilCount" SelectMethod="GetOKToBrazilList"
                        StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.BLL.PortAgentBLL"
                        OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" OnSelecting="uoObjectDataSourceOKTB_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="sUser" Type="String" />
                            <asp:Parameter Name="dDate" Type="DateTime" />
                            <asp:Parameter Name="sOrderBy" Type="String" />                            
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField runat="server" ID="uoHiddenFieldOrderBy" Value="SortByStatus" />
                    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
                    <asp:HiddenField runat="server" ID="uoHiddenFieldDate" Value="0" />
                </div>
                
                  <div>                        
                    <table cellspacing="0" width="100%">
                        <tr>
                            <td class="LeftClass">
                                <asp:DataPager ID="uoListViewManifestPager" runat="server" PagedControlID="uoListViewOKTB"
                                    PageSize="30">
                                    <Fields>
                                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />                                            
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>                        
                    </table>                        
                </div>
            <%--</ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
