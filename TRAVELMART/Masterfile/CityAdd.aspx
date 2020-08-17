<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CityAdd.aspx.cs" Inherits="TRAVELMART.CityAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table width="100%">
        <tr class="PageTitle">
            <td colspan="2">City</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                CssClass="leftAligned" ValidationGroup="Save" />
            </td>
        </tr>
        <%--<tr>
            <td class="LeftClass" style="width:20%;">&nbsp Region: </td>
            <td class="LeftClass">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="uoTextBoxRegion" runat="server" Width="300px" Visible="false" Enabled="false"></asp:TextBox>
                        <asp:DropDownList ID="uoDropDownListRegion" runat="server" 
                         Width="91%" Visible="false" AutoPostBack="true" AppendDataBoundItems="true" 
                            onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="uoDropDownListRegion" ErrorMessage="Select Region." InitialValue="0"
                            ValidationGroup="Save">*
                        </asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>--%>
        <tr>
            <td class="LeftClass" width=20%>&nbsp Country: </td>
            <td class="LeftClass">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID ="uoTextBoxCountry" runat="server" Width="300px"
                            Visible="false" Enabled="false"></asp:TextBox>
                        <asp:DropDownList runat="server" ID="uoDropDownListCountry" AppendDataBoundItems="true"
                            Width="91%" Visible="false"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="uoDropDownListCountry" ErrorMessage="Select Country." InitialValue="0"
                            ValidationGroup="Save">*
                        </asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <%--<Triggers>                    
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListRegion" 
                            EventName="SelectedIndexChanged" />                    
                    </Triggers>--%>
                </asp:UpdatePanel>
                                
            </td>
        </tr>
        <tr>
            <td class="LeftClass" width=20%>&nbsp City Name: </td>
            <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxCity"
                    Width="90%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="uorfvCityName" runat="server"
                    ControlToValidate="uoTextBoxCity" ErrorMessage="City Name is required."
                    ValidationGroup="Save">*
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="LeftClass" width=20%>&nbsp City Code: </td>
            <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxCityCode"
                    Width="90%" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvCCode"
                    ControlToValidate="uoTextBoxCityCode" ErrorMessage="City Code is required."
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
