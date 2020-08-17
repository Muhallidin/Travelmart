<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true" CodeBehind="HotelLockedManifest.aspx.cs" Inherits="TRAVELMART.HotelLockedManifest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <div class="PageTitle">Hotel Locked Manifest Summary</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
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
            var ht2 = $(window).height();
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
                ht = ht * 0.45;
                ht2 = ht2 * 0.62;
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

        function CheckFromDate(d) {
            var txtFromDate = $("#<%=uoTextBoxFrom.ClientID %>");
            var txtToDate = $("#<%=uoTextBoxTo.ClientID %>");
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
            return true;
        }
        function CheckToDate(d) {
            var txtFromDate = $("#<%=uoTextBoxFrom.ClientID %>");
            var txtToDate = $("#<%=uoTextBoxTo.ClientID %>");
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
    <div>
        <table width="100%" class="LeftClass">
            <tr>
                <td class="contentCaption">
                    Region:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="300px" AppendDataBoundItems="true"
                        AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="contentCaption">
                    Port:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListPortPerRegion" runat="server" Width="300px" AppendDataBoundItems="true"
                        AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListPortPerRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
           
            <tr>
                <td class="contentCaption">
                    Hotel Branch:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListHotel" runat="server" Width="300px" AppendDataBoundItems="True">
                        <asp:ListItem Value="0">--SELECT HOTEL--</asp:ListItem>
                    </asp:DropDownList>
                    <cc1:listsearchextender ID="ListSearchExtender_uoDropDownListBranch" runat="server"
                        TargetControlID="uoDropDownListHotel" PromptText="Type to search" PromptPosition="Top"
                        IsSorted="true" PromptCssClass="dropdownSearch" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoDropDownListHotel"
                        Enabled="False" ErrorMessage="Required Hotel" InitialValue="0">*</asp:RequiredFieldValidator>
                </td>
                 <td class="contentCaption">
                    Manifest Type:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListHours" runat="server" Width="300px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="contentCaption">From:</td>
                <td class="contentValue">
                    <asp:TextBox ID="uoTextBoxFrom" runat="server" onchange="return CheckFromDate(this);" Width="115px"></asp:TextBox>
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
                </td>
                <td class="contentCaption">To:</td>
                <td class="contentValue">
                    <asp:TextBox ID="uoTextBoxTo" runat="server" onchange="return CheckToDate(this);" Width="115px"></asp:TextBox>
                    <cc1:textboxwatermarkextender ID="Textboxwatermarkextender1" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxTo" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:textboxwatermarkextender>
                    <cc1:calendarextender ID="Calendarextender1" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxTo"  Format="MM/dd/yyyy">
                    </cc1:calendarextender >
                    <cc1:maskededitextender ID="Maskededitextender1" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxTo"
                        UserDateFormat="MonthDayYear">
                    </cc1:maskededitextender>
                    &nbsp;
                    <asp:Button ID="uoButtonView" runat="server" Text="View" CssClass="SmallButton" 
                        onclick="uoButtonView_Click" />
                </td>
            </tr>
            
         </table>
    </div>
    <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
        <asp:ListView runat="server" ID="ListView1" >
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
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton2" runat="server" Width="130px" CommandName="SortByE1TRID">Hotel Name</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton4" runat="server" Width="40px" CommandName="SortByE1TRID">Manifest Date</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton1" runat="server" Width="30px" CommandName="SortByE1TRID">Manifest Hrs</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton3" runat="server" Width="80px" CommandName="SortByE1TRID">Manifest Type</asp:LinkButton>
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton Width="100px" ID="uoLinkButtonHeaderName" runat="server" CommandName="SortByName">Locked By</asp:LinkButton>
                        </th>
                        <th style="white-space: normal;">
                            <asp:LinkButton Width="40px" ID="uoLinkBtnHeaderE1ID" runat="server" CommandName="SortByE1">Locked Date</asp:LinkButton>
                        </th>
                        <%--<th style="text-align: center; white-space: normal; border:0; width:10px" >--%>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
        onscroll="divScrollL();">
        <asp:ListView runat="server" ID="uoListViewManifest" 
            onitemcommand="uoListViewManifest_ItemCommand" >
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td class="leftAligned" style="white-space:nowrap;">
                        <asp:HiddenField ID="uoHiddenFieldManifestTypeID" runat="server" Value='<%# Eval("ManifestTypeIDTinyint")%>' />
                        <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value='<%# Eval("BranchID")%>' />
                        <%--<asp:Label runat="server" ID="Label4" Text='<%# Eval("BranchName")%>' Width="100px"></asp:Label>--%>
                        <asp:LinkButton ID="uoLinkButtonBranch" Text='<%# Eval("BranchName")%>' Width="130px" runat="server" CommandName="Select"></asp:LinkButton>
                    </td>
                     <td class="leftAligned" style="white-space: nowrap;">
                       <asp:Label runat="server" ID="uoLabelDate" Width="40px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("ManifestDate"))%>'></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: nowrap;">
                        <asp:Label runat="server" ID="Label1" Text='<%# Eval("ManifestNameVarchar")%>' Width="30px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: nowrap;">
                       <asp:Label runat="server" ID="Label2" Text='<%# Eval("ManifestDescVarchar")%>' Width="80px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: nowrap;">
                       <asp:Label runat="server" ID="Label3" Text='<%# Eval("CreatedByVarchar")%>' Width="100px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: nowrap;">
                       <asp:Label runat="server" ID="uoLblOnOff" Width="40px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DateCreatedDateTime"))%>'></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table  width="100%">
                    <tr>
                        <th >
                            <asp:Label runat="server" ID="uoScroll" Width="100%"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td  class="LeftClass">
                            No Record
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <asp:ObjectDataSource ID="uoObjectDataSourceManifest" runat="server" MaximumRowsParameterName="MaxRow"
            SelectCountMethod="LoadLockedManifestSummaryCount" SelectMethod="LoadLockedManifestSummary"
            StartRowIndexParameterName="StartRow" TypeName="TRAVELMART.BLL.LockedManifestBLL"
            OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" 
            onselecting="uoObjectDataSourceManifest_Selecting" >
            <SelectParameters>
                <asp:Parameter Name="UserID" Type="String" />
                <asp:Parameter Name="iLoadType" Type="Int16" />
                <asp:Parameter Name="sDateFrom" Type="String" />
                <asp:Parameter Name="sDateTo" Type="String" />
                <asp:Parameter Name="iManifestTypeID" Type="Int16" />
                
                <asp:Parameter Name="iRegion" Type="Int32" />
                <asp:Parameter Name="iPort" Type="Int32" />
                <asp:Parameter Name="iBranch" Type="Int32" />
                <asp:Parameter Name="FromDefaultView" Type="Int16" DefaultValue="1" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <div align="left">
            <asp:DataPager ID="uoListViewManifestPager" runat="server" PagedControlID="uoListViewManifest"
                PageSize="50" onprerender="uoListViewManifestPager_PreRender">
                <Fields>
                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                </Fields>
            </asp:DataPager>
        </div>
        <asp:HiddenField runat="server" ID="uoHiddenFieldOrderBy" Value="SortByStatus" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldLoadType" Value="0" />
        
        <asp:HiddenField ID="uoHiddenFieldUserRole" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />   
        <asp:HiddenField ID="uoHiddenFieldDate" runat="server" Value="" />   
        <asp:HiddenField ID="uoHiddenFieldFromDefaultView" runat="server" Value="1" />   
    </div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SidePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
</asp:Content>
