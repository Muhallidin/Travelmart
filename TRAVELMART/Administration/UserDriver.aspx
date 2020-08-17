<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="UserDriver.aspx.cs" Inherits="TRAVELMART.UserDriver" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>
        Driver
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="PageTitle">
             Driver Vehicle Vendor
    </div>
    <hr/>  
    
    <table class="LeftClass" >
        <tr>
            <td class="contentCaption">Search Vehicle Vendor:</td>
            <td class="contentValue" style="white-space:nowrap">
                 <asp:TextBox ID="uoTextBoxVehicleSearch" runat="server" Text="" Width="300px" CssClass="TextBoxInput"></asp:TextBox>
            </td>
        </tr>       
        <tr>
            <td></td>
            <td>
                <asp:Button ID="uoButtonSearch" runat="server" Text="Search" CssClass="SmallButton" onclick="uoButtonSearch_Click"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top" colspan="2" style="width: 400px">
                <asp:ListView runat="server" ID="uoListViewVehicle" 
                    DataSourceID="uoObjectDataSourceVehicleToAdd" 
                    onpagepropertieschanging="uoListViewVehicle_PagePropertiesChanging">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" width="500px">
                            <tr>
                                <th>
                                    
                                </th>
                                <th>
                                    Vehicle Vendor
                                </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                                <asp:CheckBox ID="uoCheckBoxSelect" runat="server" />
                            </td>
                            <td class="leftAligned">
                                <asp:HiddenField ID="uoHiddenFieldVehicleVendor" runat="server" Value='<%# Eval("VehicleID")%>' />
                                <asp:Label ID="uoLabelVehicle" runat="server" Text='<%# Eval("VehicleName")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="500px">
                            <tr>
                                <th>
                                    Vehicle Vendor
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
                <div class="LeftClass">
                    <asp:DataPager ID="uoPagerVehicle" runat="server" 
                        PagedControlID="uoListViewVehicle" PageSize="10" 
                        onprerender="uoPagerVehicle_PreRender"  >
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </td>
           <td style="vertical-align: top"  align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="uoButtonAdd" runat="server" CssClass="SmallButton" Text="Add >>"
                                ValidationGroup="Seaport" Width="70px" OnClick="uoButtonAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="uoButtonDelete" runat="server" CssClass="SmallButton" Text="<< Remove"
                                ValidationGroup="Seaport" Width="70px" OnClick="uoButtonDelete_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 400px; vertical-align:top">
                   <asp:ListView runat="server" ID="uoListViewVehicleSaved" 
                       DataSourceID="uoObjectDataSourceVehicleAdded" 
                       onpagepropertieschanging="uoListViewVehicleSaved_PagePropertiesChanging">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" width="500px">
                            <tr>
                                <th>
                                    
                                </th>
                                <th>
                                    Vehicle Vendor
                                </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                                <asp:CheckBox ID="uoCheckBoxSelect" runat="server" />
                            </td>
                            <td class="leftAligned">
                                <asp:HiddenField ID="uoHiddenFieldVehicleVendor" runat="server" Value='<%# Eval("VehicleID") %>' />
                                <asp:Label ID="uoLabelVehicle" runat="server" Text='<%# Eval("VehicleName")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="500px">
                            <tr>
                                <th>
                                    Vehicle Vendor
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
                <div class="LeftClass">
                    <asp:DataPager ID="uoPagerVehicleSaved" runat="server" 
                        PagedControlID="uoListViewVehicleSaved" PageSize="10" 
                        onprerender="uoPagerVehicleSaved_PreRender" >
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        </Fields>
                    </asp:DataPager>
                </div>
                <br/>
               <%-- <asp:Button ID="uoButtonSave" runat="server" Text="Save" 
                       onclick="uoButtonSave_Click" />--%>
            </td>
        </tr>
    </table>
     <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value=""/> 
     <asp:HiddenField ID="uoHiddenFieldUserName" runat="server" Value=""/>
     <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0" />
     <asp:HiddenField ID="uoHiddenFieldSortBy" runat="server" Value="Name" />
     
     <asp:HiddenField ID="uoHiddenFieldToLoad" runat="server" Value="" />
     
      <asp:ObjectDataSource ID="uoObjectDataSourceVehicleToAdd" runat="server" MaximumRowsParameterName="iMaxRow"
            SelectCountMethod="GetDriverVendorCount" SelectMethod="GetDriverVendorList"
            StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.UserDriver"
            OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" 
        onselecting="uoObjectDataSourceVehicleToAdd_Selecting" 
        onselected="uoObjectDataSourceVehicleToAdd_Selected">
            <SelectParameters>
                <asp:ControlParameter Name="sLoginUser" ControlID="uoHiddenFieldUserName"/>
                <asp:ControlParameter Name="sUserID" ControlID="uoHiddenFieldUser" />
                <asp:ControlParameter Name="sVendorToFind" ControlID="uoTextBoxVehicleSearch" />
                <asp:Parameter Name="bIsToBeAdded" Type="Boolean" DefaultValue="true" />
                <asp:Parameter Name="sVendorType" Type="String" DefaultValue="Vehicle" />
                
                <asp:ControlParameter Name="iLoadType" ControlID="uoHiddenFieldLoadType" />
                <asp:ControlParameter Name="sOrderBy" ControlID="uoHiddenFieldSortBy" />
                <asp:ControlParameter Name="sToLoad" ControlID="uoHiddenFieldToLoad" />
            </SelectParameters>            
        </asp:ObjectDataSource>
        
         <asp:ObjectDataSource ID="uoObjectDataSourceVehicleAdded" runat="server" MaximumRowsParameterName="iMaxRow"
            SelectCountMethod="GetDriverVendorCount" SelectMethod="GetDriverVehicleVendorAddedList"
            StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.UserDriver"
            OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" 
        onselected="uoObjectDataSourceVehicleAdded_Selected" 
        onselecting="uoObjectDataSourceVehicleAdded_Selecting">
            <SelectParameters>
                <asp:ControlParameter Name="sLoginUser" ControlID="uoHiddenFieldUserName"/>
                <asp:ControlParameter Name="sUserID" ControlID="uoHiddenFieldUser" />
               <%-- <asp:ControlParameter Name="sVendorToFind" ControlID="uoTextBoxVehicleSearch" />--%>
                <asp:Parameter Name="sVendorToFind" Type="String" DefaultValue="" />
                <asp:Parameter Name="bIsToBeAdded" Type="Boolean" DefaultValue="false" />
                <asp:Parameter Name="sVendorType" Type="String" DefaultValue="Vehicle" />
                
                <asp:ControlParameter Name="iLoadType" ControlID="uoHiddenFieldLoadType" />
                <asp:ControlParameter Name="sOrderBy" ControlID="uoHiddenFieldSortBy" />
                <asp:ControlParameter Name="sToLoad" ControlID="uoHiddenFieldToLoad" />
            </SelectParameters>
        </asp:ObjectDataSource>
        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
