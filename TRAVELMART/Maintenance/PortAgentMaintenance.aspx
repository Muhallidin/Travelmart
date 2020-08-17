<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master" AutoEventWireup="true"
    CodeBehind="PortAgentMaintenance.aspx.cs" Inherits="TRAVELMART.PortAgentMaintenance" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">  
    <div class="PageTitle">           
       Service Provider Vendor
    </div>
    <hr/>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
<table width="100%">
    <%--<tr>
        <td>&nbsp Code:</td>
        <td>
            <asp:TextBox ID="uoTextBoxVendorCode" runat="server" Width="300px"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="uoTextBoxVendorCode" ErrorMessage="Vendor code required." ValidationGroup="Save">*</asp:RequiredFieldValidator>                          
        </td>        
    </tr>    --%>
    <tr>
        <td class="LeftClass">Name:</td>
        <td class="LeftClass" >
            <asp:TextBox ID="uoTextBoxVendorName" runat="server" Width="300px" TextMode="MultiLine" CssClass="TextBoxInput"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoTextBoxVendorName" ErrorMessage="Vendor name required." ValidationGroup="Save">*</asp:RequiredFieldValidator></td>
        <td class="LeftClass">
           Vendor ID:
        </td>
        <td class="LeftClass">
            <asp:TextBox ID="uoTextBoxVendorID" runat="server" Width="300px"></asp:TextBox>
        </td>        
    </tr>     
    <tr>
        <td class="LeftClass">Address:</td>
        <td  class="LeftClass">
            <asp:TextBox ID="uoTextBoxVendorAddress" runat="server" Width="300px" TextMode=MultiLine CssClass=TextBoxInput></asp:TextBox> 
                <%--&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uoTextBoxVendorAddress" ErrorMessage="Vendor address required." ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>               
        </td>
        <td class="LeftClass">Website</td>
        <td class="LeftClass">
            <asp:TextBox ID="uoTextBoxWebsite" runat="server" Width="300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="LeftClass">
            Email To:
        </td>
        <td class="LeftClass">
            <asp:TextBox ID="uoTextBoxEmailTo" runat="server" Width="300px"
            ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
            TextMode="MultiLine" Rows="2"></asp:TextBox>
        </td>
        <td class="LeftClass">
            Email Cc:
        </td>
        <td class="LeftClass">
            <asp:TextBox ID="uoTextBoxEmailCc" runat="server" Width="300px"
            ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
            TextMode="MultiLine" Rows="2"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td></td>
        <td colspan="3" class="LeftClass">
            <asp:Label ID="Label5"  CssClass="RedNotification" runat="server" 
            Text="Multiple emails should be separated by semicolon (i.e.  abc@rccl.com;xyz@rccl.com)"> 
            </asp:Label> 
        </td>
    </tr>
    <tr>
        <td class="LeftClass">Country:</td>
        <td class="LeftClass">                       
            <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="305px" AutoPostBack="true" AppendDataBoundItems="true"
                onselectedindexchanged="uoDropDownListCountry_SelectedIndexChanged" >
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="uoDropDownListCountry" 
                    ErrorMessage="Country required." InitialValue="" ValidationGroup="Save">*</asp:RequiredFieldValidator>            
        </td>
        <td class="LeftClass">
            Contact Person:
        </td>
        <td class="LeftClass">
            <asp:TextBox ID="uoTextBoxContactPerson" runat="server" Width="300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="LeftClass">
             City Filter:
        </td>
        <td class="LeftClass">
            <asp:TextBox ID="uoTextBoxCity" runat="server" Width="300px"></asp:TextBox>
            &nbsp;
            <asp:Button ID="uoButtonFilterCity" runat="server" Text="Filter" CssClass="SmallButton" Width="50px"
                onclick="uoButtonFilterCity_Click" />
        </td>
        <td class="LeftClass"> Contact No:</td>
        <td class="LeftClass">
            <asp:TextBox ID="uoTextBoxContactNo" runat="server" Width="300px"></asp:TextBox>
<%--            <cc1:MaskedEditExtender ID="uoTextBoxContactNo_MaskedEditExtender" 
                runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                Mask="999999999999999" 
                TargetControlID="uoTextBoxContactNo">
            </cc1:MaskedEditExtender>--%>
        </td>
    </tr>
    <tr>
        <td class="LeftClass"></td>
        <td class="LeftClass">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <asp:DropDownList ID="uoDropDownListCity" runat="server" Width="305px" AppendDataBoundItems="true">                
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="uoDropDownListCity" 
                    ErrorMessage="City required." InitialValue="" ValidationGroup="Save">*</asp:RequiredFieldValidator>
            </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="LeftClass">Fax No.</td>
        <td class="LeftClass">
            <asp:TextBox ID="uoTextBoxFaxNo" runat="server" Width="300px"></asp:TextBox>
        </td>
    </tr>  
    <tr>
        <td colspan="4">
            <hr/>
        </td>
    </tr>  
    <tr>
        <td class="PageTitle" colspan="4">
            Assigned Airport
        </td>
    </tr>
    <tr>
        <td class="contentCaptionOrig">Airport Code Filter: </td>
        <td colspan="3" class="LeftClass">
              <asp:TextBox runat="server" ID="uoTextBoxAirportCode" Width="300px"></asp:TextBox>
              <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="uoTextBoxAirportCode" 
                    ErrorMessage="Airport Filter required." InitialValue="" ValidationGroup="FilterAirport">*</asp:RequiredFieldValidator>--%>
               &nbsp;&nbsp;&nbsp;
               <asp:Button ID="uoButtonAirportFilter" runat="server" Text="Filter" Width="50px" 
                CssClass="SmallButton" onclick="uoButtonAirportFilter_Click" />                               
        </td>        
    </tr>
    <tr>
        <td class="contentCaptionOrig">Airport: </td>
        <td colspan="3" class="LeftClass">                             
               <asp:DropDownList ID="uoDropDownListAirport" runat="server" Width="305px" AppendDataBoundItems="true"></asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="uoDropDownListAirport" 
                    ErrorMessage="Airport required." InitialValue="0" ValidationGroup="Add">*</asp:RequiredFieldValidator>
               &nbsp;
               <asp:Button ID="uoButtonPortAgentAirportAdd" runat="server" Text="Add" Width="50px" ValidationGroup="Add"
                CssClass="SmallButton" onclick="uoButtonPortAgentAirportAdd_Click" />
        </td>    
    </tr>
     <tr>
        <td></td>
        <td colspan="2">
             <asp:ListView runat="server" ID="uoListViewAirport"                  
                 onitemdeleting="uoListViewAirport_ItemDeleting">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="50%" >
                        <tr>
                            <th style="text-align: center; white-space: normal;">
                                Airport
                            </th>                                                                                      
                            <th runat="server" style="width: 5%" id="DeleteTH" />                                     
                        </tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="leftAligned" >                            
                            <asp:HiddenField ID="uoHiddenFieldAirportID" runat="server" Value='<%# Eval("AirportIDString") %>' />
                            <asp:Label ID="uoLabelAirportName" runat="server" Text='<%# Eval("AirportNameString")%>'></asp:Label>
                        </td>                        
                        <td style="width: 10%">
                            <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                        </td>                                         
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th>
                                Airport
                            </th>                                                                                                                       
                        </tr>
                        <tr>
                            <td class="leftAligned">
                                No Record
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>                
                <asp:HiddenField ID="uoHiddenFieldPortAgentIdInt" runat="server" />
                <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
                <asp:HiddenField ID="uoHiddenFieldURLFrom" runat="server" />
        </td>
        <td></td>
    </tr>
    <tr >
        <td colspan="4">
            <hr />
        </td>
    </tr>
    <tr  >
        <td class="PageTitle" colspan="4">
            &nbsp;Vehicle Type
        </td>
    </tr>
    <tr >
        <td class="contentCaptionOrig">Vendor Type: </td>
        <td colspan="3" class="LeftClass">
               <asp:DropDownList ID="uoDropDownListVehicleType" runat="server" Width="305px" AppendDataBoundItems="true"></asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="uoDropDownListVehicleType" 
                    ErrorMessage="Vehicle Type required." InitialValue="0" ValidationGroup="AddVehicleType">*</asp:RequiredFieldValidator>
               &nbsp;
               <asp:Button ID="uoButtonVehicleTypeAdd" runat="server" Text="Add" Width="50px" ValidationGroup="AddVehicleType"
                CssClass="SmallButton" onclick="uoButtonVehicleTypeAdd_Click" />
        </td>        
    </tr>
     <tr  >
        <td></td>
        <td colspan="3" class="LeftClass">
            <table style="width:300px; >
            <tr>
                <td>
                                
                 <asp:ListView runat="server" ID="uoListViewVehicleType"                  
                     onitemdeleting="uoListViewVehicleType_ItemDeleting">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable"  >
                            <tr>
                                <th style="text-align: center; white-space: normal;">
                                    Vehicle Type
                                </th>                                                                                      
                                <th runat="server" style="width: 5%" id="DeleteTH" />                                     
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned" >                            
                                <asp:HiddenField ID="uoHiddenFieldVehicleTypeID" runat="server" Value='<%# Eval("VehicleTypeID") %>' />
                                <asp:Label ID="uoLabelVehicleTypeName" runat="server" Text='<%# Eval("VehicleType")%>'></asp:Label>
                            </td>                        
                            <td style="width: 10%">
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                            </td>                                         
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    Vendor Type
                                </th>                                                                                                                       
                            </tr>
                            <tr>
                                <td class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>                                            
                </td>
                </tr>
            </table>
        </td>       
    </tr>
     <tr >
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <%--<tr  >
        <td class="PageTitle" colspan="4">
            &nbsp;Brand
        </td>
    </tr>  
    <tr>
        <td></td>
        <td>
            <asp:CheckBoxList ID="uoCheckBoxListBrand" runat="server">
            </asp:CheckBoxList>
        </td>        
    </tr>--%>
      <tr >
        <td colspan="2">
            <hr />
        </td>
    </tr>          
    <tr>       
        <td class="CenterClass" colspan="4">            
            <br />
            <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80"
                onclick="uoButtonSave_Click" ValidationGroup="Save"/>                
        &nbsp;            
            <asp:Button ID="uoButtonBack" runat="server" Text="Back" Width="80"
                onclick="uoButtonBack_Click" />                
            <br />
            <br />
        </td>
    </tr>        
</table>
</asp:Content>