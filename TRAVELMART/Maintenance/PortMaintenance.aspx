<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true"
    CodeBehind="PortMaintenance.aspx.cs" Inherits="TRAVELMART.PortMaintenance" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>   
<script type="text/javascript">
    $(document).ready(function() {
        ShowPopup();
    });

    function pageLoad(sender, args) {
        var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
        if (isAsyncPostback) {
            ShowPopup();
        }
    }
    function ShowPopup() {
        var uoButtonAddAirport = $("#<%=uoButtonAddAirport.ClientID %>");
        var uoDropDownListAiport = $("#<%=uoDropDownListAiport.ClientID %>");

        uoButtonAddAirport.click(function(ev) {
            if (uoDropDownListAiport.val() == "0") {
                alert("Select Airport!");
                return false;
            }            
        });
    }
    function confirmDelete() {
        if (confirm("Delete record?") == true)
            return true;
        else
            return false;
    }
    function CheckPortCode() {
        var uoTextBoxPortCode = document.getElementById('<%= uoTextBoxPortCode.ClientID %>');
        var uoHiddenFieldPortID = document.getElementById('<%= uoHiddenFieldPortID.ClientID %>');
        PageMethods.CheckPortCode(uoTextBoxPortCode.value, uoHiddenFieldPortID.value, onSuccess, onFailure);
    }
    function onSuccess(result) {
        if (result == false) {
            alert('Port Code Already Exist');
            var uoTextBoxPortCode = document.getElementById('<%= uoTextBoxPortCode.ClientID %>');
            uoTextBoxPortCode.focus();
            uoTextBoxPortCode.select();
        }
    }

    function onFailure(error) {
        alert(error);

    } 
</script>

    <div class="PageTitle">
        Port
    </div>
    <hr />
    <table width="100%">
    <tr>
            <td>
                &nbsp Port Code:
            </td>
            <td>
                <asp:TextBox ID="uoTextBoxPortCode" runat="server" Width="300px" />
                 <asp:RequiredFieldValidator ID="uorfvPortCode" runat="server" ControlToValidate="uoTextBoxPortCode"
                    ErrorMessage="Port code required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp Port Name:
            </td>
            <td>
                <asp:TextBox ID="uoTextBoxPortName" runat="server" Width="300px" />
                 <asp:RequiredFieldValidator ID="uorfvPort" runat="server" ControlToValidate="uoTextBoxPortName"
                    ErrorMessage="Port name required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp; Country:
            </td>
            <td>
                <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="400px" 
                    AutoPostBack="true" onselectedindexchanged="uoDropDownListCountry_SelectedIndexChanged"/>
                <asp:RequiredFieldValidator ID="uorfvCountry" runat="server" ControlToValidate="uoDropDownListCountry"
                    ErrorMessage="Country required." InitialValue="" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListCountry" runat="server"
                    TargetControlID="uoDropDownListCountry" PromptText="Type to search" PromptPosition="Top"
                    IsSorted="true" PromptCssClass="dropdownSearch" />   
            </td>
        </tr>
        <tr>
            <td>&nbsp;Airport:</td>
            <td>
                <asp:DropDownList ID="uoDropDownListAiport" runat="server" Width="400px" />                
                <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server"
                    TargetControlID="uoDropDownListAiport" PromptText="Type to search" PromptPosition="Top"
                    IsSorted="true" PromptCssClass="dropdownSearch" />
                <asp:Button ID="uoButtonAddAirport" runat="server" Text="Add Airport" 
                    CssClass="SmallButton" onclick="uoButtonAddAirport_Click"/>        
            </td>        
        </tr>        
        <tr>
            <td></td>
            <td >              
                <asp:GridView ID="uoGridViewAirport" runat="server" AutoGenerateColumns="False" 
                CssClass="listViewTable" EmptyDataText="No data." 
                onselectedindexchanged="uoGridViewAirport_SelectedIndexChanged" 
                DataKeyNames="AirportID" Width="500px">
                    <Columns>
                        <asp:BoundField DataField="AirportSeaportID" HeaderText="AirportSeaportID" />
                        <asp:BoundField DataField="AirportID" HeaderText="AirportID" />
                        <asp:BoundField DataField="AirportCode" HeaderText="Airport Code" />
                        <asp:BoundField DataField="AirportName" HeaderText="Airport Name" ItemStyle-CssClass="leftAligned"/>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandName="Select" OnClientClick="javascript:return confirmDelete();">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td />
            <td>
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" ValidationGroup="Save" Width="80" OnClick="uoButtonSave_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldPortID" runat="server" Value="0" />    
</asp:Content>
