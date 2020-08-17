<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master" AutoEventWireup="true" 
CodeBehind="PortAgentContractApproval.aspx.cs" Inherits="TRAVELMART.ContractManagement.PortAgentContractApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" language="javascript">
        function confirmApprove() {
            if (confirm("Confirm approved?") == true)
                return true;
            else
                return false;
        }

        function confirmReplace() {
            if (confirm("There is a current live contract, if you continue it will replace the current live contract.") == true)
                return true;
            else
                return false;
        }

        function OpenContract(portAgentId, contractID) {
            var URLString = "../ContractManagement/portAgentContractView.aspx?";
            URLString += "cId=" + contractID + "&pId=" + portAgentId;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeaderContent" runat="server">
    <div class="ViewTitlePadding">
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td align="left">
                    Service Provider Contract For Approval List
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <table style="border:0; width:100%" cellpadding="0" cellspacing="0" class="LeftClass">
    <tr >
        <td style="width: 100px"  >Service Provider:</td>
        <td style="width: 300px" class="RightClass">
            <asp:TextBox runat="server" ID="uoTextBoxPortAgent" Width="300px" CssClass="TextBoxInput"></asp:TextBox>
        </td>
        <td >&nbsp;
            <asp:Button runat="server" ID="uoButtonSearch" Text="Search" Width="80px" 
                CssClass="SmallButton" onclick="uoButtonSearch_Click"/>
        </td>
    </tr>
</table>
<br />
<div class="Module">
    <asp:ListView runat="server" ID="uoContractApprovalList"
        onitemcommand="uoContractApprovalList_ItemCommand" 
        onselectedindexchanging="uoContractApprovalList_SelectedIndexChanging">
        <LayoutTemplate>
            <table cellpadding="0" cellspacing="0" border="0" class="listViewTable">
                <tr>
                    <th>
                        <asp:LinkButton ID="uoLinkContractTitle" runat="server" CommandName="SortByCTitle"
                            CommandArgument="ContractName">
                        Contract Title
                        </asp:LinkButton>
                    </th>
                    <th>
                        <asp:LinkButton ID="uoLinkPortAgent" runat="server" CommandName="SortByPortAgent"
                           CommandArgument="PortAgentName">
                        Service Provider
                        </asp:LinkButton>
                    </th>
                    <th>Country</th>
                    <th>City</th>
                    <th>Status</th>
                    <th>Date Created</th>
                    <th runat="server" style="width: 12%" id="ContractTH" /> </th> 
                </tr>
                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="leftAligned">
                 <asp:LinkButton runat="server" id="uoLinkButtonContract" Text='<%# Eval("ContractName")%>'
                    OnClientClick = '<%# "return OpenContract(\"" + Eval("portAgentId") + "\", \""+ Eval("colContractIdInt") +"\")" %>'>
                    </asp:LinkButton>                               
                </td>
                
                <td class="leftAligned">
                    <%# Eval("PortAgentName")%>
                </td>
                <td class="leftAligned">
                    <%# Eval("COUNTRY")%>
                </td>
                <td class="leftAligned">
                    <%# Eval("CITy")%>
                </td>
                <td class="leftAligned">
                    <%# Eval("colContractStatusVarchar") %>
                </td>
                <td class="leftAligned">
                    <%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("DateCreated"))%>
                </td>
                
                <td class="leftAligned">
                    <asp:LinkButton runat="server" ID="uoHyperLinkApprove"  OnClientClick="return confirmApprove();"
                        CommandArgument='<%# Eval("colContractIdInt") %>'
                        CommandName="Select">
                        Approve Contract
                    </asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                <tr>
                    <th>Contract Title</th>
                    <th>Service Provider</th>
                    <th>Region</th>
                    <th>Country</th>
                    <th>Status</th>
                    <th></th>
                </tr>
                <tr>
                    <td colspan="6">
                        No record
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
    <div style="text-align:left">
        <asp:DataPager ID="uoContractApprovalListPager" runat="server"
            PagedControlID="uoContractApprovalList"
            PageSize="20" onprerender="uoContractApprovalListPager_PreRender">
                <Fields>
                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                </Fields>    
        </asp:DataPager>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
            OldValuesParameterFormatString="oldCount_{0}" 
            SelectCountMethod="PortAgentContractApprovalListCount" 
            SelectMethod="PortAgentContractApprovalList" 
            TypeName="TRAVELMART.BLL.ContractBLL" >        
        <SelectParameters>
            <asp:ControlParameter ControlID="uoHiddenFieldSortParam" Name="sortParam" 
                PropertyName="Value" />
            <asp:ControlParameter ControlID="uoTextBoxPortAgent" Name="sPortName" 
                PropertyName="Text" />
        </SelectParameters>
        </asp:ObjectDataSource>

    <asp:HiddenField ID="uoHiddenFieldUserId" runat="server" Value="0" />
    <asp:HiddenField ID ="uoHiddenFieldSortParam" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldreturnValue" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldContractId" runat="server" Value="0" />
</div>


</asp:Content>
