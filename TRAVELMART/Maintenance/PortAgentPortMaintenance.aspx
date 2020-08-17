<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="PortAgentPortMaintenance.aspx.cs" Inherits="TRAVELMART.Maintenance.PortListMaintenance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: right;
            vertical-align: middle;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
      
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }

    </script>    
   
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
               $("a#<%=uoHyperLinkPortAgentSeaportAdd.ClientID %>").fancybox(
        {
            'width': '38%',
            'height': '60%',
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
            'onClosed': function() {
            var a = $("#<%=uoHiddenFieldPopupSeaPort.ClientID %>").val();
                if (a == '1')
                    $("#aspnetForm").submit();
            }
        });
        
           }                 
    </script>
    
    <table width="100%">
        <tr>
            <td colspan="2">
                <table width="100%" class="PageTitle">
                    <tr>
                        <td class="PageTitle">
                            Service Provider Seaport
                        </td>
                        <td class="RightClass">
                             <a id="uoHyperLinkPortAgentSeaportAdd" runat="server">
                                <asp:Button ID="uoBtnPortAgentSeaportAdd" runat="server" Text="Add Service Provider Seaport" Font-Size="X-Small" />
                            </a>    
                        </td>
                    </tr>
                  
                </table>
            </td>
        </tr>
       
       
        <tr><td colspan="2"></td></tr>
         <tr>
            <td class="LeftClass" style="width:15%;">&nbsp Service Provider Name: </td>
            <td class="LeftClass" style="font-weight:bold;">
                <asp:Label runat="server" ID="uoLabelPortName"></asp:Label>
            </td>
        </tr>
                    
        <tr>
            <td colspan="2">
                <table width="100%" runat="server" visible="false">
                     
                    <tr><td colspan="2"></td></tr>
                    <tr>
                        <td class="LeftClass" style="width:"25%;" >
                            Region:
                        </td>
                        <td class="LeftClass">
                           <%-- <asp:UpdatePanel ID="UpdatePanelRegion" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                                    <asp:DropDownList runat="server" ID="uoDropDownListRegion" Width="300px"
                                        AutoPostBack="true" AppendDataBoundItems="true" 
                                        ></asp:DropDownList>
                                   
                               <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftClass" style="width:"25%;">
                            Country: 
                        </td>
                        <td class="LeftClass">
                           <%-- <asp:UpdatePanel ID="UpdatePanelCountry" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                                    <asp:DropDownList runat="server" ID="uoDropDownListCountry" Width="300px"
                                    AutoPostBack="true" AppendDataBoundItems="true" 
                                        >
                                        <asp:ListItem Value="0">--Select Country--</asp:ListItem>
                            </asp:DropDownList>
                                  
                               <%-- </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListRegion" 
                                        EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>   --%>                             
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftClass">
                            City Name:
                        </td>
                        <td class="LeftClass">
                            <asp:TextBox runat="server" ID= "uoTextBoxCityName" Width="213px" Font-Size="X-Small"></asp:TextBox> &nbsp;
                            <asp:Button runat="server" ID="uoButtonSearch" Text="Search City" 
                                Font-Size="X-Small"  
                                />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:"25%;" class="LeftClass">
                            <div id="uoSeaportLabel">City: </div>
                        </td>
                        <td  class="LeftClass">
                            
                            <asp:DropDownList runat="server" ID="uoDropDownListLetter" Width= "35px"
                                AutoPostBack="true" 
                                >
                                <asp:ListItem Selected="True" Value="0">--</asp:ListItem>
                                </asp:DropDownList>                           &nbsp;
                            <asp:DropDownList runat="server" ID="uoDropDownListCity2" Width="258px" 
                                
                                AppendDataBoundItems="true" AutoPostBack ="true">
                                <asp:ListItem Selected="True" Value="0">--Select City--</asp:ListItem>
                            </asp:DropDownList>
                                       
                              
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td class="LeftClass">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
            <td align="center" colspan="2">
                <div id="uoPanelPortList">
                    <table width="100%">
                        
                        <tr>
                            <td colspan="2">
                                 <asp:UpdatePanel ID="UpdatePanelSeaportList" runat="server" 
                                        UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:ListView ID="uoSeaportList" runat="server" 
                                                onitemcommand="uoSeaportList_ItemCommand" 
                                                onitemdeleting="uoSeaportList_ItemDeleting" EnableViewState="true" 
                                                >
                                                <LayoutTemplate>
                                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                                        <tr>
                                                            
                                                            <th>
                                                                Seaport
                                                            </th>
                                                            <th>
                                                                Seaport Code
                                                            </th>
                                                            <th ID="DeleteTH" runat="server" style="width:10%">
                                                            </th>
                                                        </tr>
                                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="leftAligned">
                                                            <%# Eval("SeaportName")%>
                                                        </td>
                                                        <td class="leftAligned">
                                                            <%# Eval("SeaportCode") %>
                                                        </td>
                                                        
                                                        <td class="leftAligned">
                                                            <asp:LinkButton ID="uoLinkButtonDelete" runat="server" 
                                                                CommandArgument='<%# Eval("portAgentSeaportId")%>' CommandName="Delete" 
                                                                OnClientClick="return confirmDelete();" Text="Delete"
                                                                Visible='<%# (Convert.ToBoolean(uoBtnPortAgentSeaportAdd.Visible)) %>'></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                                        <tr>
                                                            <th>
                                                                Seaport
                                                            </th>
                                                            <th>Seaport Code</th>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                No Records</td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                          
                                            <asp:HiddenField ID="uoHiddenFieldPopupSeaPort" runat="server" Value="0" />
                                            <asp:HiddenField ID="uoHiddenFielPortAgentSeaportId" runat="server" Value="0" />
                                            <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="0" />
                                            <asp:HiddenField ID="uoHiddenFieldPortAgentId" runat="server" Value="0" />
                                            <asp:HiddenField ID="uoHiddenFieldCityId" runat="server" Value="0" />
                                        </ContentTemplate>
                                        <Triggers>
                                            
                                        <%--    <asp:AsyncPostBackTrigger ControlID="uoDropDownListCity2" 
                                                EventName="SelectedIndexChanged" />--%>
                                            
                                            <asp:AsyncPostBackTrigger ControlID="uoSeaportListPager" 
                                                EventName="PreRender" />
                                            
                                        </Triggers>
                                    </asp:UpdatePanel>
                            </td>
                        </tr>  
                        <tr>
                            <td colspan="2" class="RightClass">
                                <asp:DataPager ID="uoSeaportListPager" runat="server" 
                                    onprerender="uoSeaportListPager_PreRender" PagedControlID="uoSeaportList" 
                                    PageSize="10">
                                    <Fields>
                                        <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                                    </Fields>
                                </asp:DataPager>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                                    OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" 
                                    DeleteMethod="portListMaintenanceDelete" 
                                    SelectCountMethod="PortListMaintenanceSearchAgentSeaportCount" 
                                    SelectMethod="PortListMaintenanceSearchAgentSeaport" 
                                    TypeName="TRAVELMART.BLL.MasterfileBLL">
                                    <DeleteParameters>
                                        <asp:ControlParameter ControlID="uoHiddenFielPortAgentSeaportId" 
                                            Name="portAgentSeaportId" PropertyName="Value" />
                                        <asp:ControlParameter ControlID="uoHiddenFieldUser" Name="ModifiedBy" 
                                            PropertyName="Value" />
                                    </DeleteParameters>
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="uoHiddenFieldPortAgentId" Name="portAgentId" 
                                            PropertyName="Value" />
                                        <asp:ControlParameter ControlID="uoHiddenFieldCityId" Name="CityId" 
                                            PropertyName="Value" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </div>
               
                
            </td>
        </tr>
    </table>
</asp:Content>
