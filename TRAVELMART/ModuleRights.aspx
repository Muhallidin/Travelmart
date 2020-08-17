<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMart.Master" AutoEventWireup="true" CodeBehind="ModuleRights.aspx.cs" Inherits="TRAVELMART.ModuleRights" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ViewTitlePadding"  >        
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>Module Rights</td>                    
     
            </tr>
        </table>    
    </div> 
    <table width="100%">
            <tr>
            <td>
<%--                <div class="">
                   
                </div>--%>
            </td>
        </tr>
            <tr>
            <td style="text-align: left">
                <asp:GridView ID="uoGridViewModule" runat="server" AutoGenerateColumns="False" 
                    Width="535px">
                    <Columns>
                        <asp:BoundField HeaderText="Module Name" />
                        <asp:CheckBoxField HeaderText="View" />
                        <asp:CheckBoxField HeaderText="Add" />
                        <asp:CheckBoxField HeaderText="Edit" />
                        <asp:CheckBoxField HeaderText="Delete" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
            <tr>
            <td style="text-align: left">
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" />
                </td>
        </tr>
    </table>
</asp:Content>
