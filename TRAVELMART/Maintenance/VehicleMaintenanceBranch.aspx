<%@ Page Language="C#" MasterPageFile="~/TravelMartMaster.Master" AutoEventWireup="true"
        CodeBehind="VehicleMaintenanceBranch.aspx.cs" Inherits="TRAVELMART.Maintenance.VehicleMaintenanceBranch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script src="../Menu/menu.js" type="text/javascript"></script>  --%>
    
    <style type="text/css">
        .style4
        {
            width: 147px;
        }
        .style6
        {
            width: 147px;
            height: 30px;
        }
         .style7
        {
            width: 147px;
            height: 30px;
            padding-top:2px;
            vertical-align:top;
        }
        
    </style>
        
    <script type="text/javascript" language="javascript">
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("a#<%=uoHyperLinkVehicleAdd.ClientID %>").fancybox(
                {
                    'width': '40%',
                    'height': '45%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupVehicle.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });

            $(".VehicleLink").fancybox(
                {
                    'width': '40%',
                    'height': '45%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupVehicle.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });

        });                       
    </script>
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftAligned">
        <div class="PageTitle">
            Vehicle Branch
        </div>
        <hr />
        <table width="120%" style="text-align: left">
            <tr>
                <td colspan="4">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ValidationGroup="Header" />
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Vehicle Brand :
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="uoDropDownListVendorName" runat="server" Width="447px" AutoPostBack="True" >
                    <%--OnSelectedIndexChanged="uoDropDownListVendorName_SelectedIndexChanged" >--%>
                    </asp:DropDownList>
                    &nbsp;<asp:RequiredFieldValidator ID="uorfvVendorName" runat="server" ControlToValidate="uoDropDownListVendorName"
                    ErrorMessage="Vendor required.">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style4">
                     Vahicle Branch :</td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxVendorBranchName" runat="server" Width="447px" ></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="uorfvVendorBranchName" runat="server" ControlToValidate="uoTextBoxVendorBranchName"
                    ErrorMessage="Branch name required.">*</asp:RequiredFieldValidator>
                </td>                            
            </tr>   
            <tr>
                <td class="style4">
                     Branch Code :</td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxBranchCode" runat="server" Width="300px" MaxLength="10"></asp:TextBox>
                </td>
            </tr>         
            <tr>
                <td class="style4">
                    Address :</td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxVendorAddress" runat="server" Width="305px" TextMode="MultiLine" CssClass=TextBoxInput></asp:TextBox>              
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Country :</td>
                <td colspan="3">
                    <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="447px" AutoPostBack="True" 
                    OnSelectedIndexChanged="uoDropDownListCountry_SelectedIndexChanged" >
                            </asp:DropDownList>
                                <cc1:listsearchextender ID="ListSearchExtender_uoDropDownListCountry" runat="server"
                                TargetControlID ="uoDropDownListCountry"
                                PromptText="Type to search"
                                PromptPosition="Top"
                                IsSorted="true"
                                PromptCssClass="dropdownSearch"/>
                                    &nbsp;<asp:RequiredFieldValidator ID="uorfvCountry" runat="server" ControlToValidate="uoDropDownListCountry"
                                    InitialValue="" ErrorMessage="Country required.">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    City :</td>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">   
                        <ContentTemplate>
                            <asp:DropDownList ID="uoDropDownListCity" runat="server" Width="447px" >
                            </asp:DropDownList>
                                <cc1:listsearchextender ID="ListSearchExtender_uoDropDownListCity" runat="server"
                                TargetControlID ="uoDropDownListCity"
                                PromptText="Type to search"
                                PromptPosition="Top"
                                IsSorted="true"
                                PromptCssClass="dropdownSearch"/> 
                                    &nbsp;<asp:RequiredFieldValidator ID="uorfvCity" runat="server" ControlToValidate="uoDropDownListCity"
                                    InitialValue="" ErrorMessage="City required.">*</asp:RequiredFieldValidator>
                        </ContentTemplate>
                            <Triggers>                            
                                <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" 
                                    EventName="SelectedIndexChanged" />
                        </Triggers>    
                    </asp:UpdatePanel>
                </td>                
            </tr> 
            <tr>
                <td class="caption">
                        City Filter:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxFilterCity" runat="server" Width="250px"  CssClass="TextBoxInput"  ></asp:TextBox>
                       
                        <asp:Button ID="uoButtonViewCity" runat="server" Text="Filter City" 
                            CssClass="SmallButton" onclick="uoButtonViewCity_Click"  />   
                </td>
            </tr>
            <tr>
                <td class="style4">
                Contact No :</td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxContactNo" runat="server" Width="300px"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="uoTextBoxContactNo_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""  
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="(999) 999-9999" MaskType="Number" 
                        TargetControlID="uoTextBoxContactNo">
                        </cc1:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Contact Person :</td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxContactPerson" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Franchise :</td>
                <td colspan="3">
                   <asp:CheckBox ID="uoCheckBoxFranchise" runat="server" Checked="false"/>
                </td>
            </tr>   
            <tr>
                <td class="style7" >
                    Type :</td>
                <td colspan="3">
                    <asp:RadioButtonList ID="uoRadioButtonListType" runat="server"/>
                </td>
            </tr>   
            <tr>
                <td colspan=4>
                </td>
            </tr>                                                 
            <tr>
                <td class="style6">
                    <asp:HiddenField ID="uoHiddenFieldBranchId" runat="server" />
                </td>
                <td>                                      
                    <asp:Button ID="uoButtonVehicleBranchSave" runat="server" Text="Save" Width="80" OnClick="uoButtonVehicleBranchSave_Click"/>                 
                </td>                                             
            </tr>            
        </table>                 
        <asp:Panel runat=server ID="uoPanel1" Visible=false>
            <%--<tr>--%>
            <%--<td style="padding-left: 3px; padding-right: 3px;" colspan="2">--%>
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 40%; text-align: left;" class="PageTitle">
                                <asp:Panel ID="uopanelvehiclehead" runat="server" Width="305px">
                                    Vehicle
                                    <asp:Label ID="uolabelVehicle" runat="server" Text="" Font-Size="Small" />
                                </asp:Panel>
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">                                
                                <a id="uoHyperLinkVehicleAdd" runat="server">
                                    <asp:Button ID="uoButtonVehicleAdd" runat="server" Text="Add Vehicle Type" Font-Size="X-Small" />
                                </a>
                            </td>
                        </tr>
                        <tr><td colspan="2"></td></tr>                         
                    </table>
                </div>                                    
            <%--</td>--%>
        <%--</tr>--%>
        
        <asp:Panel ID="uopanelvehicle" runat="server" CssClass="CollapsiblePanel">
        <table align=left width="50%">
        <tr>        
            <td class="Module" align="right">
                <asp:ListView runat="server" ID="uoListViewVehicle" OnItemCommand="uoListViewVehicle_ItemCommand"
                    OnItemDeleting="uoListViewVehicle_ItemDeleting">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <tr>
                                <th class="hideElement">ID</th>
                                <th class="hideElement">BranchID</th>
                                <th class="hideElement">TypeID</th>
                                <th width="20%">Type</th>
                                <th>Name</th>
                                <th width="20%">Capacity</th>
                                <th runat="server" style="width: 10%" id="EditTH" />
                                <th runat=server style="width: 15%"></th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>                          
                        <tr>
                            <td class="hideElement">
                                <asp:HiddenField runat="server" id="uoHiddenFieldVehicleIdBigint" value="ID"/>
                            </td>
                            <td class="hideElement">
                                <asp:HiddenField runat="server" id="uoHiddenFieldBranchIdInt" value="BranchID"/>
                            </td>
                            <td class="hideElement">
                                <asp:HiddenField runat="server" id="uoHiddenFieldVehicleTypeIdInt" value="TypeID"/>
                            </td>
                            <td class="leftAligned"><%# Eval("Type")%></td>
                            <td class="leftAligned"><%# Eval("Name")%></td>
                            <td class="leftAligned"><%# Eval("Capacity")%></td>
                            <td>
                                <a runat="server" class="VehicleLink" id="uoAEditVehicle" href='<%# "~/Maintenance/VehicleTypeBranch.aspx?vID=" + Eval("ID") + "&vmID=" + Eval("BranchID") %>'>
                                    Edit</a>
                            </td>
                             <td>
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                    CommandArgument='<%# Eval("ID") %>' CommandName="Delete">Delete</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="listViewTable">
                            <tr>                               
                                <th>Type</th>
                                <th>Name</th>
                                <th>Capacity</th>   
                            </tr>
                            <tr>
                                <td colspan="14" class="leftAligned">No Record</td>                                
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="uoListViewVehiclePager" runat="server" PagedControlID="uoListViewVehicle"
                    PageSize="20" OnPreRender="uoListViewVehiclePager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>   
        </table>     
        </asp:Panel>  
        </asp:Panel> 
        <cc1:collapsiblepanelextender ID="uocollapsibleVehicle" runat="server" TargetControlID="uopanelvehicle"
            ExpandControlID="uopanelvehiclehead" CollapseControlID="uopanelvehiclehead" TextLabelID="uolabelVehicle"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" 
            SuppressPostBack="true">
        </cc1:collapsiblepanelextender>
        <br />               
        </div>
        <asp:HiddenField ID="uoHiddenFieldPopupVehicle" runat="server" />     
</asp:Content>
