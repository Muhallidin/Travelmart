<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true" CodeBehind="ExceptionActive.aspx.cs" Inherits="TRAVELMART.ExceptionActive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
     <div class="PageTitle">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    Cancelled E1 Travel Routing but Active PNR
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
            filterSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
                ShowPopup();
                filterSettings();
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
                ht = ht * 0.55;
                ht2 = ht2 * 0.65;
            }

            $("#Av").width(wd);
            $("#Bv").height(ht);
            $("#PG").height(ht2);
            $("#Bv").width(wd);

            $("#PG").width(wd);
        }
        
        function filterSettings() {

            $("#<%=uoDropDownListFilterBy.ClientID %>").change(function(ev) {
                if ($(this).val() != "1") {
                    $("#<%=uoTextBoxFilter.ClientID %>").val("");
                }
            });
        }
        function validateE1(key) {
            if ($("#<%=uoDropDownListFilterBy.ClientID %>").val() != "1") {

                //getting key code of pressed key
                var keycode = (key.which) ? key.which : key.keyCode;
                if ((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) {
                    return false;
                }
                else {
                    return true;
                }
            }
            return true;
        }
        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;
        }
    </script>
    
     <div id="PG" style="width: auto; height: auto; overflow: auto;">
         <div align="left">
             <table width="100%" class="LeftClass" id="uoTableSearch" runat="server">
                    <tr >
                        <td class="caption" style="width: 166px">
                            Filter By::
                        </td>
                        <td class="value" style="width: 306px">
                             <asp:DropDownList ID="uoDropDownListFilterBy" runat="server" Width="300px">
                                <asp:ListItem Value="1">SEAFARER NAME</asp:ListItem>
                                <asp:ListItem Value="2" Selected="True">EMPLOYEE ID</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                              <asp:TextBox ID="uoTextBoxFilter" runat="server" CssClass="TextBoxInput" Width="300px"
                                    onkeypress="return validateE1(event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr >
                        <td class="caption" style="width: 166px">
                            Record Locator:
                        </td>
                        <td class="value" style="width: 306px">
                             <asp:TextBox ID="uoTextBoxRecordLocator" runat="server" CssClass="TextBoxInput" Width="300px"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                     <tr >
                        <td class="caption">
                            Sequence Number:
                        </td>
                        <td class="value" style="width: 306px">
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
                                <asp:LinkButton ID="LinkButton1" runat="server" Width="40px" CommandName="SortByE1TRID">Seq. No.</asp:LinkButton>
                            </th>
                            <th style="white-space: normal;">
                                <asp:LinkButton Width="40px" ID="uoLinkBtnHeaderE1ID" runat="server" CommandName="SortByE1">E1 ID</asp:LinkButton>
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton Width="300px" ID="uoLinkButtonHeaderName" runat="server" CommandName="SortByName">Name</asp:LinkButton>
                            </th>
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton Width="20px" ID="LinkButton3" runat="server" CommandName="SortByName">Status</asp:LinkButton>
                            </th>
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton Width="100px" ID="LinkButton4" runat="server" CommandName="SortByName">Date</asp:LinkButton>
                            </th>
                            <th style="text-align: center; white-space: normal; border:0; width:10px" >
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
                           <asp:Label runat="server" ID="Label2" Text='<%# Eval("SequenceNo")%>' Width="40px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                           <asp:Label runat="server" ID="Label3" Text='<%# Eval("E1No")%>' Width="40px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label Width="300px" runat="server" ID="uoLblSFId" Text='<%# Eval("Name")%>'></asp:Label>
                        </td>
                         <td class="leftAligned" style="white-space: normal;">
                            <asp:Label Width="20px" runat="server" ID="Label4" Text='<%# Eval("Status")%>'></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                           <asp:Label runat="server" ID="uoLblOnOff" Width="100px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>'></asp:Label>
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
            <asp:ObjectDataSource ID="uoObjectDataSourceTRException" runat="server" MaximumRowsParameterName="MaxRow"
                SelectCountMethod="GetActiveExceptionCount" SelectMethod="GetActiveExceptionList"
                StartRowIndexParameterName="StartRow" TypeName="TRAVELMART.BLL.AnalystExceptionBLL"
                OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" 
                onselecting="uoObjectDataSourceTRException_Selecting" >
                <SelectParameters>
                    <asp:Parameter Name="iFilterBy" Type="Int16" />
                    <asp:Parameter Name="sFilter" Type="String" />
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
        </div>
     </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SidePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
</asp:Content>
