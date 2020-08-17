<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    CodeBehind="PortAgentRequest.aspx.cs" Inherits="TRAVELMART.PortAgentRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="PageTitle">
            <td align="left" style="height: 25px">
                <asp:Label ID="uoLabelTitle" runat="server" class="Title" Text="Service Provider Manifest"></asp:Label>
            </td>
            <%--<td align="right" style="white-space: nowrap">
                <asp:RadioButtonList ID="uoRadioButtonListPort" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="true" OnSelectedIndexChanged="uoRadioButtonListPort_SelectedIndexChanged">
                    <asp:ListItem Value="1">Airport</asp:ListItem>
                    <asp:ListItem Value="2">Seaport</asp:ListItem>
                </asp:RadioButtonList>
            </td>--%>
            <td align="right" style="width: 850px">
               <%-- <asp:Label ID="ucLabelPort" runat="server" class="Title" Text="Vendor"></asp:Label>&nbsp;
                <asp:Label ID="lblPortAgentVendor" runat="server" Text="" class="Title" />--%>
                 <asp:DropDownList runat="server" ID ="uoDropDownListPortAgent" 
                    AppendDataBoundItems="true" Width="350px" AutoPostBack="true" 
                    onselectedindexchanged="uoDropDownListPortAgent_SelectedIndexChanged">
                </asp:DropDownList>              
<%--                <asp:DropDownList runat="server" ID="uoDropDownListStatus" Width="250px" AutoPostBack="true" 
                AppendDataBoundItems="true"  onselectedindexchanged="uoDropDownListStatus_SelectedIndexChanged">
                </asp:DropDownList>--%>
                <asp:DropDownList runat="server" ID="uoDropDownListDays" Width="200px" AutoPostBack="true" 
                AppendDataBoundItems="true" onselectedindexchanged="uoDropDownListDays_SelectedIndexChanged">
                </asp:DropDownList>                    
               <%-- &nbsp;
                <asp:DropDownList ID="uoDropDownListPort" runat="server" Width="280px" AutoPostBack="true"
                    OnSelectedIndexChanged="uoDropDownListPort_SelectedIndexChanged" Visible="false">
                </asp:DropDownList>
                &nbsp;&nbsp;--%>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">      
        
        $(document).ready(function() {
            SetTRResolution();           
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();                
            }
        }
        function SetTRResolution() {
            var ht = $(window).height() * .9;
            var ht2 = $(window).height() * 1;
            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.20;
                ht2 = ht2 * 0.20;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.39;
                ht2 = ht2 * 0.39;
            }
            else {
                ht = ht * 0.6;
                ht2 = ht2 * 0.65;
            }        

            $("#AvSea").width(wd);
            $("#BvSea").height(ht);
            $("#BvSea").width(wd);

            $("#PG").height(ht2);
            $("#PG").width(wd);
        }
      
        function divScrollL(sSource) {

            var Right = document.getElementById('AvSea');
            var Left = document.getElementById('BvSea');

            Right.scrollLeft = Left.scrollLeft;
        }

        function confirmTag(e1ID, seafarerName) {
            var sMsg = "Tag " + e1ID + ": " + seafarerName + "? ";
            var x = confirm(sMsg);
            return x;
        }       
       
    </script>
    <div class="LeftClass">
        <asp:Button ID="uoButtonExport" runat="server" Text="Export Manifest" CssClass="SmallButton" 
        OnClick="uoButtonExport_Click" />
    </div>

    <div id="PG" style="width: auto; height: auto; overflow: auto;">               
        <div runat="server" id="uoDivSeaport">
            <div id="AvSea" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
                <asp:ListView runat="server" ID="ListViewHeaderSea" OnItemCommand="ListViewHeaderSea_ItemCommand">
                    <LayoutTemplate>
                    </LayoutTemplate>
                    <ItemTemplate>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>                               
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="linkHotel" Text="Hotel" Width="45px" runat="server" CommandName="colIsHotelBit" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="linkTransportation" Text="Transportation" Width="85px" runat="server"
                                        CommandName="colIsTransBit" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="linkMeetGreet" Text="Meet & Greet" Width="95px" runat="server"
                                        CommandName="colIsMAGBit" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="linkLagguage" Text="Luggage" Width="75px" runat="server" CommandName="colIsLuggageBit" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="linkSafeguard" Text="Safeguard" Width="75px" runat="server" CommandName="colIsSafeguardBit" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="linkVisa" Text="Visa" Width="45px" runat="server" CommandName="colIsVisaBit" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="linkOther" Text="Other" Width="43px" runat="server" CommandName="colIsOtherBit" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton6" Text="Last Name" runat="server" Width="110px" CommandName="colLastname" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton7" Text="First Name" runat="server" Width="130px" CommandName="colFirstname" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton8" Text="Employee ID" runat="server" Width="60px" CommandName="colSeafarerIdInt" />
                                </th>
                                <th style="white-space: normal;">
                                    <asp:LinkButton ID="LinkButton9" Text="Ship" runat="server" Width="182px" CommandName="Vessel" />
                                </th>
                                <th style="white-space: normal;">
                                    <asp:LinkButton ID="LinkButton26" Text="Status" runat="server" Width="58px" CommandName="colStatusVarchar" />
                                </th>
                                  <th style="white-space: normal;">
                                    <asp:LinkButton ID="LinkButton1" Text="ON/OFF Date" runat="server" Width="65px" CommandName="SignOnOffDate" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton11" Text="Title" runat="server" Width="220px" CommandName="Rank" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton12" Text="Gender" runat="server" Width="50px" CommandName="colGender" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton13" Text="Cost Center" runat="server" Width="130px"
                                        CommandName="colCostCenter" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton14" Text="Nationality" runat="server" Width="70px"
                                        CommandName="colNationality" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton33" Text="Birthday" Width="70px" runat="server" CommandName="colBirthday" />
                                </th>                                
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <div id="BvSea" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
                onscroll="divScrollL('Sea');">
                <asp:ListView runat="server" ID="uoListViewTRSea" OnItemCommand="uoListViewTR_ItemCommand">
                    <LayoutTemplate>
                        <table  cellpadding="0" cellspacing="0"  border="0" class="listViewTable">
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <%--<td class="leftAligned" style="white-space: normal;">
                                <asp:HiddenField ID="uoHiddenFieldStatus" runat="server" Value='<%# Eval("Status") %>' />
                                <asp:Label ID="Label26" runat="server" Width="46px" Text="Tagged" Visible='<%# IsTaggedLabelVisible(Eval("IsTaggedByUser"), Eval("IsFirstPartition"))%>'
                                    title='<%# "Tagged time is : " + Eval("TagDateTime") %>'></asp:Label>
                                <asp:LinkButton Width="46px" ID="uoLinkButtonTag" CssClass="PropertyLinkButtons BtnTag"
                                    CommandName="Tag" Visible='<%# IsTaggedLinkVisible(Eval("IsTaggedByUser"), Eval("IsFirstPartition"))%>'
                                    OnClientClick='<%# "javascript:return confirmTag("+ Eval("SfID") + ", \"" + Eval("Name") + "\");" %>'
                                    CommandArgument='<%# Eval("IDBigInt") + ":" + Eval("TravelRequestID") + ":" + Eval("AirportID") + ":" + Eval("SeaportID") %>'
                                    runat="server" Text="Tag"></asp:LinkButton>
                            </td>--%>
                            <td class="leftAligned">
                                    <input type="checkbox" style="white-space: normal; width: 45px" disabled="disabled"
                                    <%# IsServiceCheck(Eval("IsHotel"))%> />
                            </td>
                            <td class="leftAligned">                                
                                <input type="checkbox" style="white-space: normal; width: 83px" disabled="disabled"
                                   <%# IsServiceCheck(Eval("IsTransportation"))%> />
                            </td>
                            <td class="leftAligned">                                
                                <input type="checkbox" style="white-space: normal; width: 95px" disabled="disabled"
                                   <%# IsServiceCheck(Eval("IsMAG"))%> />
                            </td>
                            <td class="leftAligned">
                                <span style="width:70px">
                                <input type="checkbox" style="white-space: normal; width: 73px" disabled="disabled"
                                   <%# IsServiceCheck(Eval("IsLuggage"))%> />
                                </span>
                            </td>
                            <td class="leftAligned">                                
                                <input type="checkbox" style="white-space: normal; width: 74px" disabled="disabled"
                                   <%# IsServiceCheck(Eval("IsSafeguard"))%> />
                            </td>
                            <td class="leftAligned">                                
                                <input type="checkbox" style="white-space: normal; width: 44px" disabled="disabled"
                                   <%# IsServiceCheck(Eval("IsVisa"))%> />
                            </td>
                            <td class="leftAligned">                                
                                <input type="checkbox" style="white-space: normal; width: 43px" disabled="disabled"
                                  <%# IsServiceCheck(Eval("IsOther"))%> />
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label54" Width="115px" Text='<%# Eval("Lastname")%>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "../SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("SfID") + "&recloc=&st=" + Eval("Status") + "&ID=0&trID="+ Eval("TravelRequestID") +"&manualReqID=0" + "&dt=" + Request.QueryString["dt"]%>'
                                    runat="server" Width="136px" Text='<%# Eval("Firstname")%>'></asp:HyperLink>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" Width="66px" ID="ucLabelE1ID" Text='<%# Eval("SfID")%>'
                                    Visible='<%# Eval("IsFirstPartition") %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:Label runat="server" ID="uoLblVessel" Width="188px" Text='<%# Eval("Vessel")%>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:Label runat="server" ID="Label2" Width="63px" Text='<%# Eval("Status")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblOnOff" Width="73px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DateOnOff"))%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" Width="226px" ID="Label20" Text='<%# Eval("Rank")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label35" Width="56px" Text='<%# Eval("Gender")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label36" Width="136px" Text='<%# Eval("CostCenter")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label37" Width="76px" Text='<%# Eval("Nationality")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label7" Width="76px" Text='<%# Eval("Birthday")%>'></asp:Label>
                            </td>                           
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="" width="100%">
                            <tr>
                                <th colspan="30">
                                    <asp:Label runat="server" ID="uoScroll" Width="1820px"></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="30" class="LeftClass">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <div style="text-align: left">
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="uoListViewTRSea" OnPreRender="uoListViewPagerAir_PreRender"
                        PageSize="20">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="uoObjectDataSourceTR" runat="server" MaximumRowsParameterName="iMaxRow"
            SelectMethod="GetPortAgentRequest" StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.BLL.TravelRequestBLL"
            OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" OnSelecting="uoObjectDataSourceTR_Selecting">
            <SelectParameters>
                <asp:Parameter Name="LoadType" Type="Int16" />
                <asp:Parameter Name="sUser" Type="String" />
                <asp:Parameter Name="sRole" Type="String" />
                <asp:Parameter Name="sPortID" Type="String" />
                <asp:Parameter Name="sAirportID" Type="String" />
                <asp:Parameter Name="dDate" Type="DateTime" />
                <asp:Parameter Name="VesselID" Type="String" />
                <asp:Parameter Name="FilterByName" Type="String" />
                <asp:Parameter Name="SeafarerID" Type="String" />
                <asp:Parameter Name="NationalityID" Type="String" />
                <asp:Parameter Name="Gender" Type="String" />
                <asp:Parameter Name="RankID" Type="String" />
                <asp:Parameter Name="Status" Type="String" />
                <asp:Parameter Name="iViewType" Type="Int16" />
                <asp:Parameter Name="SortBy" Type="String" />
                <asp:Parameter Name="iVendorID" Type="Int32" />
                <asp:ControlParameter Name="iNoOfDay" ControlID="ctl00$HeaderContent$uoDropDownListDays" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField runat="server" ID="uoHiddenFieldDate" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldRole" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldIsAirport" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldIsSeaport" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldUser" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldSeaportID" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldAirpordID" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldLoadType" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldSortBy" Value="colLastname" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldTagTime" Value="" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldManifestStatus" Value="" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldOrder" Value="" />
        <%--<asp:HiddenField runat="server" ID="uoHiddenFieldVendorID" Value="" />--%>
        <asp:HiddenField ID="uoHiddenFieldPortAgentID" runat="server" />
    </div>
</asp:Content>
 