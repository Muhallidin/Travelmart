<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="MeetAndGreetMaintenance.aspx.cs" Inherits="TRAVELMART.Maintenance.MeetAndGreetMaintenance" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <div class="PageTitle">           
        Meet & Greet Vendor
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
        <td class="LeftClass" colspan="3">
            <asp:TextBox ID="uoTextBoxVendorName" runat="server" Width="600px"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoTextBoxVendorName" ErrorMessage="Vendor name required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
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
            <asp:TextBox ID="uoTextBoxEmailTo" runat="server" Width="300px"></asp:TextBox>
        </td>
        <td class="LeftClass">
            Email Cc:
        </td>
        <td class="LeftClass">
            <asp:TextBox ID="uoTextBoxEmailCc" runat="server" Width="300px"></asp:TextBox>
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
            <cc1:MaskedEditExtender ID="uoTextBoxContactNo_MaskedEditExtender" 
                runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                Mask="(999) 999-9999" MaskType="Number" 
                TargetControlID="uoTextBoxContactNo">
            </cc1:MaskedEditExtender>
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
        <td class="contentCaptionOrig">Meet and Greet Airport: </td>
        <td colspan="3" class="LeftClass">
               <asp:DropDownList ID="uoDropDownListVehicleType" runat="server" Width="305px" AppendDataBoundItems="true"></asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="uoDropDownListVehicleType" 
                    ErrorMessage="Airport required." InitialValue="0" ValidationGroup="Add">*</asp:RequiredFieldValidator>
               &nbsp;
               <asp:Button ID="uoButtonVehicleTypeAdd" runat="server" Text="Add" Width="50px" ValidationGroup="Add"
                CssClass="SmallButton" onclick="uoButtonVehicleTypeAdd_Click" />
        </td>        
    </tr>
     <tr>
        <td></td>
        <td colspan="2">
             <asp:ListView runat="server" ID="uoListViewVehicleType"                  
                 onitemdeleting="uoListViewVehicleType_ItemDeleting">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="50%" >
                        <tr>
                            <th style="text-align: center; white-space: normal;">
                               Meet and Greet Airport
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
                               Meet and Greet Airport
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
          <%--   <asp:ObjectDataSource ID="uoObjectDataSourceVehicle" runat="server" MaximumRowsParameterName="iMaxRow"
                    SelectCountMethod="GetVehicleVendorTypeList" SelectMethod="VehicleVendorsGet"
                    StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.BLL.MaintenanceViewBLL"
                    OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" OnSelecting="uoObjectDataSourceVehicle_Selecting" >
                    <SelectParameters>
                        <asp:Parameter Name="sVehicleVendorName" Type="String"/>
                        <asp:ControlParameter Name="sOrderyBy" Type="String" ControlID="uoHiddenFieldSortBy" />                            
                    </SelectParameters>
                </asp:ObjectDataSource>
--%>
                <asp:HiddenField ID="uoHiddenFieldVehicleVendorIdInt" runat="server" />
                
        </td>
        <td></td>
    </tr>      
    <tr>
        <td class="LeftClass"></td>
        <td class="LeftClass" colspan="3">            
            <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80"
                onclick="uoButtonSave_Click" ValidationGroup="Save"/>                
        </td>
    </tr>        
</table>
</asp:Content>

