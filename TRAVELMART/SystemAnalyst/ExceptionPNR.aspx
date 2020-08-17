<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true" CodeBehind="ExceptionPNR.aspx.cs" Inherits="TRAVELMART.ExceptionPNR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            width: 166px;
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
 <div class="PageTitle">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left">
                PNR Exception
            </td>
        </tr>
    </table>
</div>
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
         <div align="left">
             <table width="100%" class="LeftClass" id="uoTableSearch" runat="server">
                    <tr >
                        <td class="caption" style="width: 166px">
                            Date Range:
                        </td>
                        <td class="value">
                             <table>
                                <tr>
                                    <td>From:</td>
                                    <td>
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
                                    <td>To:</td>
                                    <td>
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
                                    </td>
                                </tr>
                             </table>
                        </td>
                        <td></td>
                    </tr>
                    <tr >
                        <td class="caption" style="width: 166px">
                            Record Locator:
                        </td>
                        <td class="value">
                             <asp:TextBox ID="uoTextBoxRecordLocator" runat="server" CssClass="TextBoxInput" Width="300px"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                     <tr >
                        <td class="style2">
                            Sequence Number:
                        </td>
                        <td class="contentValue">
                             <asp:TextBox ID="uoTextBoxSequenceNo" runat="server" CssClass="TextBoxInput" Width="300px"
                                    onkeypress="return validate(event);"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="uoButtonView" runat="server" Text="View" CssClass="SmallButton" 
                                onclick="uoButtonView_Click"/>
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
                                <asp:LinkButton ID="LinkButton2" runat="server" Width="55px" CommandName="SortByE1TRID">Rec Loc</asp:LinkButton>
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton1" runat="server" Width="55px" CommandName="SortByE1TRID">Seq. No.</asp:LinkButton>
                            </th>
                            <th style="white-space: normal;">
                                <asp:LinkButton Width="300px" ID="uoLinkBtnHeaderE1ID" runat="server" CommandName="SortByE1">Description</asp:LinkButton>
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton Width="100px" ID="uoLinkButtonHeaderName" runat="server" CommandName="SortByName">Date Created</asp:LinkButton>
                            </th>
                            <th style="text-align: center; white-space: normal; border:0">
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
        <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
            onscroll="divScrollL();">
            <asp:ListView runat="server" ID="uoListViewException" DataSourceID="uoObjectDataSourceTRException">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label1" Text='<%# Eval("RecordLocator")%>' Width="55px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                           <asp:Label runat="server" ID="Label2" Text='<%# Eval("SequenceNo")%>' Width="55px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label Width="300px" runat="server" ID="uoLblSFId" Text='<%# Eval("Description")%>'></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                           <asp:Label runat="server" ID="uoLblOnOff" Width="100px" Text='<%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("DateCreated"))%>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table  width="100%">
                        <tr>
                            <th colspan="4">
                                <asp:Label runat="server" ID="uoScroll" Width="100%"></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="4" class="LeftClass">
                                No Record
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <asp:ObjectDataSource ID="uoObjectDataSourceTRException" runat="server" MaximumRowsParameterName="MaxRow"
                SelectCountMethod="GetXMLExceptionCount" SelectMethod="GetXMLExceptionList"
                StartRowIndexParameterName="StartRow" TypeName="TRAVELMART.BLL.AnalystExceptionBLL"
                OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" 
                onselecting="uoObjectDataSourceTRException_Selecting" >
                <SelectParameters>
                    <asp:Parameter Name="sUserName" Type="String" />
                    <asp:Parameter Name="sRole" Type="String" />
                    <asp:Parameter Name="DateFrom" Type="DateTime" />
                    <asp:Parameter Name="DateTo" Type="DateTime" />
                    <asp:Parameter Name="sRecordLocator" Type="String" />
                    <asp:Parameter Name="iSequenceNo" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <div align="left">
                <asp:DataPager ID="uoListViewExceptionPager" runat="server" PagedControlID="uoListViewException"
                    PageSize="50" onprerender="uoListViewExceptionPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </div>
            <asp:HiddenField runat="server" ID="uoHiddenFieldOrderBy" Value="SortByStatus" />
            <asp:HiddenField runat="server" ID="uoHiddenFieldLoadType" Value="0" />
            
            <asp:HiddenField ID="uoHiddenFieldUserRole" runat="server" Value="" />
            <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
            <asp:HiddenField runat="server" ID="uoHiddenFieldDate" Value="0" />
        </div>
     </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SidePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
</asp:Content>
