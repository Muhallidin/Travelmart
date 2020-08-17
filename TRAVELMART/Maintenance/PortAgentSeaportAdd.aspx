<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="PortAgentSeaportAdd.aspx.cs" Inherits="TRAVELMART.Maintenance.PortAgentSeaportAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="PageTitle">
        City
    </div>
    <hr />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        CssClass="leftAligned" ValidationGroup="Save" />
    <table width="100%">
        <tr>
            <td>&nbsp Region: </td>
            <td>
                
                        <asp:DropDownList ID="uoDropDownListRegion" runat="server" 
                         Width="300px"  AutoPostBack="true" AppendDataBoundItems="true" onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged" 
                            ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="uoDropDownListRegion" ErrorMessage="Select Region." InitialValue="0"
                            ValidationGroup="Save">*
                        </asp:RequiredFieldValidator>
                  
            </td>
        </tr>
        <tr>
            <td>&nbsp Country: </td>
            <td>
                
                        <asp:DropDownList runat="server" ID="uoDropDownListCountry" AppendDataBoundItems="true"
                            Width="300px" AutoPostBack="true" 
                            onselectedindexchanged="uoDropDownListCountry_SelectedIndexChanged"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="uoDropDownListCountry" ErrorMessage="Select Country." InitialValue="0"
                            ValidationGroup="Save">*
                        </asp:RequiredFieldValidator>
                   
                                
            </td>
        </tr>
         <tr>
            <td class="LeftClass">
                &nbsp City Name:
            </td>
            <td class="LeftClass">
                <asp:TextBox runat="server" ID= "uoTextBoxCityName" Width="211px" Font-Size="X-Small"></asp:TextBox> &nbsp;
                <asp:Button runat="server" ID="uoButtonSearch" Text="Search City" 
                    Font-Size="X-Small" onclick="uoButtonSearch_Click" 
                    />
            </td>
         </tr>
        <tr>
            <td>&nbsp City: </td>
            <td>
                <asp:DropDownList runat="server" ID="uoDropDownListLetter"
                    Width="35px" AutoPostBack="true" AppendDataBoundItems="true" onselectedindexchanged="uoDropDownListLetter_SelectedIndexChanged"
                    ></asp:DropDownList> &nbsp;
               <asp:DropDownList ID="uoDropDownListCity2" runat="server"
                    Width="258px" AutoPostBack="true" AppendDataBoundItems="true" 
                    onselectedindexchanged="uoDropDownListCity2_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                    ControlToValidate="uoDropDownListCity2" ErrorMessage="Select Country." InitialValue="0"
                    ValidationGroup="Save">*
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>&nbsp Seaport: </td>
            <td>
                <asp:DropDownList runat="server" ID="uoDropDownListSeaport"
                    AppendDataBoundItems="true" Width="301px"
                    ></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                    ControlToValidate="uoDropDownListSeaport" ErrorMessage="Select Seaport." InitialValue="0"
                    ValidationGroup="Save">*
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="uoButtonSave" runat="server" Text="Save"
                    Width ="80px" ValidationGroup="Save" onclick="uoButtonSave_Click" />
            </td>
        </tr>
        </table>
</asp:Content>
