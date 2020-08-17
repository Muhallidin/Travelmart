<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="True" ValidateRequest="false"
    CodeBehind="CrewAssistRemarksReportByDate.aspx.cs" Inherits="TRAVELMART.Hotel.CrewAssistRemarksReportByDate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">        
    <style type="text/css">
        .style2
        {
            height: 13px;
        }
        .style3
        {
            height: 13px;
            width: 13%;
        }
        .style4
        {
            width: 13%;
        }
        .style5
        {
            height: 13px;
            width: 21%;
        }
        .style6
        {
            width: 21%;
        }
        .style7
        {
            height: 13px;
            width: 10%;
        }
        .style8
        {
            width: 10%;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server" ID="header">
    <%--<div class="PageTitle">
        Hotel Booking Exception List</div>--%>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr class="PageTitle">
                <td align="left">
                   Crew Assist Remarks Report
                </td>                        
            </tr>
        </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <div id="PG" style="width: auto; height: auto; overflow: auto;">
      <table width="100%" cellpadding="0" cellspacing="0" style="width:100%; text-align:left" class="TableView">
            <tr>
                <td class="caption">
                    By Date range:
                </td>
                <td class="value">
                     <asp:CheckBox ID="uoCheckBoxByDateRange" runat="server" Checked="false" OnClick="DateRangeCheck(this.checked);"/>
                </td>
                <td class="caption">
                    Source Type:</td>
                <td class="value">
                    <asp:DropDownList ID="uoDropDownListSourceType" runat="server" Width="200px">
                    </asp:DropDownList>
                    </td>
                <td class="caption">
                    </td>
            </tr>
             <tr id="ucTRYearMonth" runat="server">
                <td class="caption">
                    Year:
                </td>
                <td class="value">
                    <asp:DropDownList ID="uoDropDownListYear" runat="server" Width="200px" >
                    </asp:DropDownList>
                </td>
                <td class="caption">
                    Month:
                </td>
                <td class="value">
                    <asp:DropDownList ID="uoDropDownListMonth" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td class="caption">
                </td>
            </tr>
            <tr id="ucTRDate" runat="server">
                <td class="caption">
                    Date From:
                </td>
                <td class="value">
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
                </td>
                <td class="caption">
                    Date To:
                </td>
                <td class="value">
                    <asp:TextBox ID="uoTextBoxTo" runat="server" onchange="return CheckToDate(this);" Width="120px"></asp:TextBox>
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
                <td class="caption">
                </td>
            </tr>  
           
              <tr>
                <td class="caption">
                </td>
                <td class="value">
                     <asp:Button runat="server" ID="uoButtonView" CssClass="SmallButton" Text="View"
                     Visible="true" onclick="uoButtonView_Click" /> 
                    &nbsp;&nbsp; 
                    <asp:Button runat="server" ID="uoBtnExportList" CssClass="SmallButton" Text="Export List"
                     Visible="true" onclick="uoBtnExportList_Click" />
                </td>
                <td class="caption">
                    &nbsp;</td>
                <td class="value">
                    &nbsp;</td>
                <td class="caption">
                     &nbsp;&nbsp;
                    </td>
            </tr>  
           <%-- <tr>
                <td colspan="5">
                    <asp:Label runat="server" ID="uoLabelRed" CssClass="RedNotification"
                        Text="Exception list is based from selected month and future signon or signoff date of the year" >
                    </asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td colspan="5">
                <br/>
                <br/>
                     <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;
                        position: relative;">
                        <asp:ListView runat="server" ID="ListView1" 
                             onitemcommand="ListView1_ItemCommand" >
                            <LayoutTemplate>
                            </LayoutTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>                        
                                        <th>
                                             <asp:LinkButton runat="server" ID="Label16" Text="Type" Width="300px" CommandName="Remarks Type"></asp:LinkButton>                                            
                                        </th>
                                        <th>
                                           <asp:LinkButton runat="server" ID="Label2" Text="Count" Width="85px" CommandName="Count"></asp:LinkButton> 
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>                                        
                    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto; 
                     position: relative;"  onscroll="divScrollL();">
                    <asp:ListView runat="server" ID="uoListViewDashboard" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList">
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>     
                            <tr>
                                 <td class="leftAligned" style="white-space: normal;">                                    
                                    <asp:Label runat="server" ID="Label16" Text='<%# Eval("RequestType")%>' Width="305px"> </asp:Label> 
                                 </td>
                                  <td class="leftAligned" style="white-space: normal;">
                                   <asp:Label runat="server" ID="Label1" Text='<%# Eval("iCount")%>' Width="95px"> </asp:Label> 
                                 </td>
                             </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <td colspan="2" class="leftAligned">
                                        <asp:Label runat="server" ID="Label10" Text="No Record" Width="1080px"> </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
                <div>
                     <asp:DataPager ID="uoListViewManifestPager" runat="server" PagedControlID="uoListViewDashboard"
                        PageSize="20" onprerender="uoListViewManifestPager_PreRender">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />                                            
                        </Fields>
                    </asp:DataPager>
                    <asp:ObjectDataSource ID="uoObjectDataSourceCrewAssist" runat="server" MaximumRowsParameterName="iMaxRow"
                        SelectCountMethod="GetCrewAssistRemarksCount" SelectMethod="GetCrewAssistRemarks" StartRowIndexParameterName="iStartRow"
                        TypeName="TRAVELMART.BLL.ReportBLL" OldValuesParameterFormatString="oldcount_{0}"
                        EnablePaging="True" OnSelecting="uoObjectDataSourceCrewAssist_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="iYear" Type="Int32" />
                            <asp:Parameter Name="iMonth" Type="Int32" />
                            <asp:Parameter Name="sCreatedBy" Type="String" />
                            <asp:Parameter Name="sUserID" Type="String" />
                            <asp:Parameter Name="iLoadType" Type="Int16" />
                            <asp:Parameter Name="iFilterBy" Type="Int16" />
                            <asp:Parameter Name="sFilterValue" Type="String" />
                            <asp:Parameter Name="sOrderBy" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>                      
                </td>
            </tr>
        </table>        
    </div>
    
    <asp:HiddenField runat="server" ID="uoHiddenFieldUser" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldLoadType" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldOrderBy" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldFilterValue" Value="0" />
    
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetTRResolution();
            ControlSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
                ControlSettings();
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
                ht = ht * 0.5;
                ht2 = ht2 * 0.7;
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

        function ControlSettings() {
            var IsCheck = $("#<%=uoCheckBoxByDateRange.ClientID %>").attr("checked");
            DateRangeCheck(IsCheck);
        }

        function DateRangeCheck(IsDateRange) {
            if (IsDateRange) {
                $("#<%=ucTRYearMonth.ClientID %>").hide();
                $("#<%=ucTRDate.ClientID %>").show();
            }
            else {
                $("#<%=ucTRYearMonth.ClientID %>").show();
                $("#<%=ucTRDate.ClientID %>").hide();
            }
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
</asp:Content>
