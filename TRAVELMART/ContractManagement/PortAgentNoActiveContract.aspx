<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="PortAgentNoActiveContract.aspx.cs" Inherits="TRAVELMART.ContractManagement.PortAgentNoContract" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript" language="javascript">
            
            function OpenContract(portAgentId, ContractID) {
                var URLString = "../ContractManagement/PortAgentContractView.aspx?pId=" + portAgentId + "&cId=" + ContractID;           

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle">Service Provider With No Active Contract List</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>                                                    
            <asp:ListView runat="server" ID="uoPortAgentContractList" O 
                onitemcommand="uoPortAgentContractList_ItemCommand">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>       
                            <th>
                                <asp:LinkButton ID="uoLinkPortAgentName" runat="server" CommandName="SortByAgentName"
                                    CommandArgument="colPortAgentName">
                                Service Provider Name
                                </asp:LinkButton>
                            </th>                        
                            <th>
                                <asp:LinkButton ID="uoLinkPortCompany" runat="server" CommandName="SortByCompanyName"
                                    CommandArgument="colCompanyNameVarchar">
                                Port Company
                                </asp:LinkButton>
                            </th> 
                            <th>Region</th> 
                            <th>Country</th>                           
                            <th runat="server" style="width: 10%;" id="AddContractTH"></th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="leftAligned">
                            <%# Eval("name") %>                                                                                  
                        </td>
                         <td class="leftAligned">
                            <%# Eval("colCompanyNameVarchar")%>
                        </td>
                        <td class="leftAligned">                           
                             <%# Eval("colMapNameVarchar")%>
                        </td>  
                        <td class="leftAligned">                           
                             <%# Eval("colCountryNameVarchar")%>
                        </td>                         
                         <td>
                            <a runat="server" id="uoAddContract" href='<%# "~/ContractManagement/PortAgentContractAdd.aspx?pId=" + Eval("colPortAgentIdInt") + "&pCId=" + Eval("colPortAgentCompanyIdInt") + "&cId=0" +  "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'>
                                Add Contract</a>
                        </td>
                        <%--<td>
                            <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                CommandArgument='<%#Eval("PORTID") %>' CommandName="Delete">Delete</asp:LinkButton>
                        </td>--%>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th>Service Provider Name</th>
                            <th>Port Company</th>
                            <th>Region</th> 
                            <th>Country</th>   
                            <th></th>                                 
                        </tr>
                        <tr>
                            <td colspan="5" class="leftAligned">
                                No Record
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <asp:DataPager ID="uoPortAgentContractListPager" runat="server" 
                PagedControlID="uoPortAgentContractList" PageSize="20" onprerender="uoPortAgentContractListPager_PreRender"
                >
                <Fields>
                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                </Fields>
            </asp:DataPager>
            <asp:HiddenField ID="uoHiddenFieldSortParam" runat="server" Value="colPortAgentName" />
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
                OldValuesParameterFormatString="oldCount_{0}" 
                SelectCountMethod="PortAgentNoActiveContractListCount" 
                SelectMethod="PortAgentNoActiveContractList" 
                TypeName="TRAVELMART.BLL.ContractBLL">
                <SelectParameters>
                    <asp:ControlParameter ControlID="uoHiddenFieldSortParam" Name="sortParam" 
                        PropertyName="Value" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="uoPortAgentContractListPager" 
                EventName="PreRender" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
