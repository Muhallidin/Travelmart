<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="Remarks.aspx.cs" Inherits="TRAVELMART.Remarks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" language="javascript">
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }               
    </script>
    <div class="PageTitle">
        Travel Request Remarks
    </div>
    <hr />
    
    <table width="100%" >
    <tr>
        <td class="caption">
             <asp:ListView runat="server" ID="uoListViewRemarks" 
                 onitemdeleting="uoListViewRemarks_ItemDeleting" 
                 onselectedindexchanging="uoListViewRemarks_SelectedIndexChanging"  >
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" >
                        <tr>
                            <th >
                                Remarks
                            </th>
                            <th>
                                Created By
                            </th>
                            <th>
                                Date Created 
                            </th>                            
                            <th runat="server" style="width: 5%" id="EditTh">
                            </th>
                            <th runat="server" style="width: 6%" id="DeleteTh">
                            </th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>                                    
                    <tr>
                        <td class="leftAligned">
                            <%# Eval("Remarks")%>
                            <asp:HiddenField runat="server" ID = "uoHiddenFieldRemarksID" Value='<%# Eval("RemarkIDBigInt") %>'/>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("CreatedBy")%>
                        </td>
                        <td class="leftAligned" >
                             <%# string.Format("{0:dd-MMM-yyyy HHmm}", Eval("CreatedDate"))%>
                        </td>
                        <td class="leftAligned" >
                            <asp:LinkButton ID="uoLinkButtonEdit" runat="server" Text="Edit" Visible='<%# IsVisible(Eval("CreatedBy"))%>' 
                            CommandName="Select"></asp:LinkButton>
                        </td>
                         <td class="leftAligned" >
                            <asp:LinkButton ID="uoLinkButtonDelete" runat="server" Text="Delete" Visible='<%# IsVisible(Eval("CreatedBy"))%>'
                            CommandName="Delete" OnClientClick="javascript:return confirmDelete()"></asp:LinkButton>
                         </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th >
                                Remarks
                            </th>
                            <th>
                                Created By
                            </th>
                            <th>
                                Date Created 
                            </th>       
                        </tr>
                        <tr>
                            <td colspan="3" class="leftAligned">
                                No Record
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
        </td>        
    </tr>
    </table>    
</asp:Content>
