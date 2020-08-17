<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="PortAgentBrandPopup.aspx.cs" 
Inherits="TRAVELMART.PortAgentBrandPopup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script> 
<script type="text/javascript">
    var objChkd;
    function HandleOnCheck() {

        var chkLst = document.getElementById('CheckBoxList1');


        if (objChkd && objChkd.checked)
            objChkd.checked = false;

        objChkd = event.srcElement;


    }


</script>
<script>
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle">
             Service Provider: Brand Assignment
    </div>
    <hr/>     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="contentCaption"> Service Provider:</td>        
            <td class="contentValue">
                <asp:DropDownList runat="server" ID="uoDropDownListPortAgent" AppendDataBoundItems="true" Width="300px"></asp:DropDownList>
                <asp:TextBox runat="server" ID="uoTextBoxPortAgentName" Text="Service Provider Name" Font-Bold="true" CssClass="ReadOnly" 
                ReadOnly="true" Width="300px"></asp:TextBox>
            </td>
            <td class="contentCaption"> Assigned Airport: </td>
            <td class="contentValue">
                <asp:TextBox runat="server" ID="uoTextBoxAirport" Text="Airport"  CssClass="ReadOnly" 
                ReadOnly="true" Width="300px" TextMode="MultiLine" Rows="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="contentCaption">Brand:</td>
            <td colspan="4">
                <asp:CheckBoxList runat="server" ID="uoCheckBoxListBrand" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">RCI</asp:ListItem>
                    <asp:ListItem Value="2">AZA</asp:ListItem>
                    <asp:ListItem Value="3">CEL</asp:ListItem>
                    <asp:ListItem Value="4">PUL</asp:ListItem>                
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="contentCaption">Search Airport:</td>
            <td>                
                <asp:TextBox ID="uoSearchAirportBox" name="uoSearchAirportBox" runat="server" Width="200px" CssClass="SmallText"></asp:TextBox>
                <asp:Button ID="uoButtonSearchAirport" runat="server" Text="Search Airport" CssClass="SmallButton" 
                  OnClick="airportSearch"  />
</td>
            <td colspan="2">&nbsp;</td>
        </tr>
    </table>
    <div style="display:inline;width:100%;">
    <table valign=top style="width:35%;float:left">
       
            <asp:ListView runat="server" ID="uoListViewAirport">
            <LayoutTemplate>
                    <tr>
                <th style="font-weight: bold;font-size: 11px;color: #CCC;background-color: #333;text-align: left;border-color: #FFF;padding-top: 3px;padding-bottom: 3px;padding-left: 3px;padding-right: 3px;}"> </th>
                <th style="font-weight: bold;font-size: 11px;color: #CCC;background-color: #333;text-align: left;border-color: #FFF;padding-top: 3px;padding-bottom: 3px;padding-left: 3px;padding-right: 3px;}">Airport</th>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                        </tr>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                     <td class="leftAligned"><asp:CheckBox ID="uoCheckBoxSelect" runat="server" /></td>
                     <td class="leftAligned">
                           <asp:HiddenField ID="uoHiddenFieldAirport" runat="server" Value='<%# Eval("colAirportIDInt") %>' />
                           <asp:Label ID="uoLabelAirport" runat="server" Text='<%# Eval("colAirportFullName")%>' />                         
                     </td>                   
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
            <tr> <td colspan="2">No Record </td></tr>
            </EmptyDataTemplate>
            </asp:ListView>

    </table>
    <table valign=top style="width:20%;float:left;text-align:center;">
                <tbody style="display:block;">
                <tr style="display:block;width:100%;">
                    <td style="display:block;width:100%;">
                    <asp:Button ID="uoButtonAdd" runat="server" OnClick="ButtonAdd" CssClass="SmallButton" Text="Add >>"
                                 Width="65px" />
                    </td>
                </tr>
                <tr style="display:block;width:100%;">
                    <td style="display:block;width:100%;">
                    <asp:Button ID="uoButtonRemove" runat="server" CssClass="SmallButton" OnClick="ButtonRemove" Text="<< Remove"/>
                    </td>
                </tr>
                </tbody>
    </table>
    <table valign=top style="width:35%;float:left;">
        <asp:ListView runat="server" ID="uoListViewAirportSaved" EmptyDataText="No Data Found">
            <LayoutTemplate>
                <tr>
                  <th  style="font-weight: bold;font-size: 11px;color: #CCC;background-color: #333;text-align: left;border-color: #FFF;padding-top: 3px;padding-bottom: 3px;padding-left: 3px;padding-right: 3px;}">&nbsp;</th>
                  <th  style="font-weight: bold;font-size: 11px;color: #CCC;background-color: #333;text-align: left;border-color: #FFF;padding-top: 3px;padding-bottom: 3px;padding-left: 3px;padding-right: 3px;width: 100%;}">Airport</th>
                </tr>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td class="leftAligned">
                       <asp:CheckBox ID="uoCheckBoxSelectSaved" runat="server" />
                    </td>
                    <td class="leftAligned">
                       <asp:HiddenField ID="uoHiddenFieldAirportSaved" runat="server" Value='<%# Eval("colAirportIDInt") %>' />
                       <asp:Label ID="uoLabelAirportSaved" runat="server" Text='<%# Eval("colAirportFullName")%>' />                         
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
              <tr>
              <td colspan="2">No Record </td>
              </tr>  
            </EmptyDataTemplate>
        </asp:ListView>
            <tr>
            <td><asp:Button ID="SaveButton" runat="server" CssClass="SmallButton" OnClick="ButtonSave" Text="Save"/></td>
            </tr>
    </table>
    </div>
    <asp:HiddenField ID="uoHiddenFieldPortAgentIDInt" Value="0" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldAirportID" Value="0" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" Value="0" runat="server" />
</asp:Content>
