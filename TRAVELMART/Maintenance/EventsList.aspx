<%@ Page Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="EventsList.aspx.cs" Inherits="TRAVELMART.Maintenance.EventsList" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script src="../Menu/menu.js" type="text/javascript"></script>  
    <%--<script type="text/javascript">
        $(document).ready(function() {            
            $(".EventsLink").fancybox(
            {
                'width': '80%',
                'height': '90%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupEvents.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });
        });           
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle">
        &nbsp;Events List
    </div>
    <hr/>    
    <table style="width: 483px">
        <%--<tr>
        <td>--%>
        <asp:ListView runat="server" ID="uoListViewEvents">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <tr>
                                <th>Events Name</th>
                                <th>Hotel</th>
                                <th>Date From</th>
                                <th>Date To</th>
                                <th>Remarks</th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>                          
                        <tr>                            
                            <td class="leftAligned"><%# Eval("colEventNameVarchar")%></td>
                            <td class="leftAligned"><%# Eval("colVendorBranchNameVarchar")%></td>
                            <td class="leftAligned"><%# Eval("colEventDateFromDate")%></td>
                            <td class="leftAligned"><%# Eval("colEventDateToDate")%></td>
                            <td class="leftAligned"><%# Eval("colRemarksText")%></td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="listViewTable">
                            <tr>                               
                                <th>Events Name</th>
                                <th>Hotel</th>
                                <th>Date From</th>
                                <th>Date To</th>
                                <th>Remarks</th>
                            </tr>
                            <tr>
                                <td colspan="14" class="leftAligned">No Record</td>                                
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
        <%--</td>
        </tr>--%>
        <%--<tr>
        <td class="RightClass">--%>
            <asp:DataPager ID="uoListViewEventsPager" runat="server" PagedControlID="uoListViewEvents"
            PageSize="20" OnPreRender="uoListViewEventsPager_PreRender">
                <Fields>
                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                </Fields>
                    </asp:DataPager>
        <%--</td>
        </tr>--%>        
    </table>    
    <asp:HiddenField ID="uoHiddenFieldBranch" runat="server" Value="0"/>            
    <asp:HiddenField ID="uoHiddenFieldCity" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldOnOffDate" runat="server" Value="0"/>
</asp:Content>
