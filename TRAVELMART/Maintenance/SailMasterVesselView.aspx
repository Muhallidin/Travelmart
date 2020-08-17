<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="SailMasterVesselView.aspx.cs" Inherits="TRAVELMART.SailMasterView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }
        
        
    </script>
    
    <table width="100%">
        <tr>
            <td class="PageTitle">
                Ship
            </td>
        </tr>
        <tr>
            <td class="RightClass">
                <span>Ship: </span>
                <asp:TextBox runat="server" ID="uoTextBoxSearch" Font-Size="X-Small" Width="200px">
                </asp:TextBox>
                <asp:Button runat="server" ID="uoButtonSearch" Text="Search" Font-Size="X-Small" />
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                <asp:Button runat="server" ID="uoButtonViewAll" Text="View All" 
                    CssClass="SmallButton" onclick="uoButtonViewAll_Click" />
            </td>
        </tr>
        <tr>
            <td class="Module">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ListView runat="server" ID="uoVesselList"
                            EnableViewState="true">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th class="LeftClass">Ship Name</th>
                                        <th class="LeftClass">Ship Code</th>
                                    </tr>
                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>    
                            <ItemTemplate>
                                <tr>
                                    <td class="leftAligned">
                                        <a runat="server" id="a1" href='<%# "SailMasterView.aspx?ufn=" + Request.QueryString["ufn"] + "&vId=" + Eval("colVesselIdInt") + "&vName=" + Eval("colVesselNameVarchar") + "&dt=" + Request.QueryString["dt"] %>' >
                                        <%#Eval("colVesselNameVarchar") %></a>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("colVesselCodeVarchar") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th class="LeftClass">Ship Name</th>
                                        <th class="LeftClass">Ship Code</th>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="LeftClass">No Record</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <asp:DataPager ID="uoVesselListPager" runat="server" PagedControlID="uoVesselList" PageSize="20"
                            OnPreRender="uoVesselListPager_PreRender">
                            <Fields>
                                <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                            </Fields>
                        </asp:DataPager>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoVesselListPager" EventName="PreRender" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:HiddenField runat="server" ID="uoHiddenFieldViewAll" Value="0" />
                <asp:HiddenField runat="server" ID="uoHiddenFieldDateFrom" Value="0" />
                <asp:HiddenField runat="server" ID="uoHiddenFieldDateTo" Value="0" />
            </td>
        </tr>
        <tr>
            <td class="RightClass">
                 
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
                     OldValuesParameterFormatString="oldcount_{0}" 
                     SelectCountMethod="SailMasterMaintenanceVesselSearchCount" 
                     SelectMethod="SailMasterMaintenanceVessel" 
                     TypeName="TRAVELMART.BLL.MasterfileBLL" 
                     onselecting="ObjectDataSource1_Selecting">
                    <UpdateParameters>
                        <asp:ControlParameter ControlID="uoTextBoxSearch" Name="pVesselName" 
                            PropertyName="Text" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="uoTextBoxSearch" DefaultValue=" " 
                            Name="pVesselName" PropertyName="Text" />
                        <asp:ControlParameter ControlID="uoHiddenFieldViewAll" DbType="Boolean" 
                            DefaultValue="" Name="viewAll" PropertyName="Value" />
                    </SelectParameters>
                 </asp:ObjectDataSource>
            </td>
        </tr>
        
    </table>
</asp:Content>
