<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="HotelAirportAdd.aspx.cs" Inherits="TRAVELMART.Maintenance.HotelAirportAdd" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <div class="PageTitle">           
        Add Hotel Aiport Assigment</div>
    <hr/>
    <table class="TableEditor">
    <tr>
        <td class="caption">Airport:</td>
        <td class="value">
            <asp:TextBox ID="uoTextBoxAirport" runat="server" Width="310px" CssClass="ReadOnly" ReadOnly = "true"></asp:TextBox>                                          
        </td>        
        <td>
            &nbsp;</td>
    </tr>    
    <%--<tr>
        <td>&nbsp Hotel Category:</td>
        <td>
            <asp:DropDownList ID="uoDropDownListHotelCategory" runat="server" Width="305px">
                <asp:ListItem Value="">- Select a Hotel Category -</asp:ListItem>
                <asp:ListItem Value="Employee Shipboard">Employee Shipboard</asp:ListItem>
                <asp:ListItem Value="Employee Shoreside">Employee Shoreside</asp:ListItem>
                <asp:ListItem Value="Guests Hotels">Guests Hotels</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="uorfvHotelCategory" runat="server" 
                    ControlToValidate="uoDropDownListHotelCategory" 
                    ErrorMessage="Hotel category required." InitialValue="" ValidationGroup="Save">*</asp:RequiredFieldValidator>
        </td>
    </tr>--%>
    <tr>
        <td class="caption">Country:</td>
        <td class="value">
            <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="310px" AutoPostBack="true"
                onselectedindexchanged="uoDropDownListCountry_SelectedIndexChanged">                
            </asp:DropDownList>            
            <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListCountry" runat="server"
                    TargetControlID="uoDropDownListCountry" PromptText="Type to search" PromptPosition="Top"
                    IsSorted="true" PromptCssClass="dropdownSearch" />   
        </td>
        <td>
            <%--<asp:RequiredFieldValidator ID="uorfvCountry" runat="server" 
            ControlToValidate="uoDropDownListCountry" 
            ErrorMessage="Country is required." InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
        </td>        
    </tr>
    <tr>
        <td class="caption">Filter City:</td>
        <td class="value">
              <asp:TextBox ID="uoTextBoxCityName" runat="server" Width="265px"></asp:TextBox>                
              <asp:Button ID="uoButtonFilterCity" runat="server" Text="Filter" 
                  CssClass="SmallButton" onclick="uoButtonFilterCity_Click"/>
        </td>
    </tr>
    <tr>
        <td class="caption">City:</td>
        <td class="value">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>                                
                <asp:DropDownList ID="uoDropDownListCity" runat="server" Width="310px" AutoPostBack=true 
                    onselectedindexchanged="uoDropDownListCity_SelectedIndexChanged">                
                </asp:DropDownList>
                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListCity" runat="server"
                    TargetControlID="uoDropDownListCity" PromptText="Type to search" PromptPosition="Top"
                    IsSorted="true" PromptCssClass="dropdownSearch" />   
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoButtonFilterCity" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>                      
        </td>
        <td>
           <%-- <asp:RequiredFieldValidator ID="uorfvCity" runat="server" 
            ControlToValidate="uoDropDownListCity" 
            ErrorMessage="City is required." InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
        </td>        
    </tr>   
    <tr>
        <td class="caption">Hotel Branch:</td>
        <td class="value">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server"> 
                <ContentTemplate>
                    <asp:DropDownList ID="uoDropDownListHotel" runat="server" Width="310px" AutoPostBack=true >                
                    </asp:DropDownList>  
                    <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListHotel" runat="server"
                    TargetControlID="uoDropDownListHotel" PromptText="Type to search" PromptPosition="Top"
                    IsSorted="true" PromptCssClass="dropdownSearch" />   
                </ContentTemplate>                   
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoButtonFilterCity" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListCity" 
                        EventName="SelectedIndexChanged" />
                    <%--<asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" 
                        EventName="SelectedIndexChanged" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="uorfvHotelBranch" runat="server" 
                    ControlToValidate="uoDropDownListHotel" 
                    ErrorMessage="Hotel branch is required." InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
        </td>
    </tr> 
   <%-- <tr>
        <td class="caption">&nbsp; Vendor Accredited:</td>
        <td class="value">
            <asp:CheckBox ID="uoCheckBoxVendorAccredited" runat="server" Checked=true/>
        </td>
    </tr>    --%>   
    <tr>
        <td colspan="3"></td>
    </tr> 
    <tr>
        <td></td>
        <td>            
            <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80"
                onclick="uoButtonSave_Click" ValidationGroup="Save"/>                
        </td>
        <td></td>
    </tr> 
    <tr>
        <td></td>
        <td>            
            <asp:HiddenField ID="uoHiddenFieldRegionID" runat="server" /> 
            <asp:HiddenField ID="uoHiddenFieldAirportID" runat="server" />    
            <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
            <asp:HiddenField ID="uoHiddenFieldRoomType" runat="server" />
        </td>  
        <td></td>     
    </tr>    
</table>
</asp:Content>