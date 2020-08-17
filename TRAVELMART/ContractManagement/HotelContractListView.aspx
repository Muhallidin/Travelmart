<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="HotelContractListView.aspx.cs" Inherits="TRAVELMART.ContractManagement.HotelContractListView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .thVal
        {
            font-weight: bold;
            font-size: 13px;
            color: #CCC;
            background-color: #333;
            text-align: center;
            border-color: #FFF;
            border-style: solid;
            border-width: 1px;
            padding: 3px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">
    <div>
        <table style="width: 100%" cellspacing="0">
            <tr>
                <td style="width: 30%; text-align: left;" class="PageTitle">
                    <asp:Panel ID="uopanelhotelhead" runat="server" Width="364px">
                        <%--Hotel Contract List --%>
                        <asp:Label ID="ucLabelHotel" runat="server" Font-Bold="True" Font-Size="12pt" Font-Names="Calibri"></asp:Label>
                        <asp:Label ID="uolabelHotel" runat="server" Text="" Font-Size="Small" />
                    </asp:Panel>
                </td>
                <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                    <a id="uoHyperLinkHotelAdd" runat="server">
                        <asp:Button ID="uoBtnHotelAdd" runat="server" Text="Add" Font-Size="X-Small" OnClick="uoBtnHotelAdd_Click1"
                            Visible="False" />
                    </a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">
        function OpenContract(vendorID, contractID) {
            var URLString = "../ContractManagement/HotelContractView.aspx?";
            URLString += "vId=" + vendorID + "&cId=" + contractID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }


        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;
        }  
    </script>

    <script type="text/javascript" language="javascript">
        function confirmDelete(IsActive) {
            if (IsActive == 'True') {
                if (confirm("Confirm inactive?") == true)
                    return true;
                else
                    return false;
            }
            return false;
        }
        function SetResolution() {
            var ht = $(window).height(); //550;
            var ht2 = $(window).height();
            var wd = $(window).width() * 0.90;
            if (screen.height <= 600) {
                ht = ht * 0.45;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.52;
            }
            else {
                ht = ht * 0.55;
                ht2 = ht2 * 0.62;
            }

            $("#Bv").height(ht);
            $("#Bv").width(wd);
            $("#Av").width(wd);

        }

        function isVisible() {  
            if ($("#<%=uoHiddenFieldAmendContract.ClientID %>").val() == 'true') {
                return true;
            }
            else {
                false;
            }
        }

        function pageLoad(sender, args) {

            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetResolution();
            }
        }

        $(document).ready(function() {
            SetResolution();
        });
    </script>

    <div class="LeftClass" style="padding-left: 10px; padding-right: 10px;">
        <span>Group By :</span>
        <asp:DropDownList ID="upDropDownListSort" runat="server" Width="200px" AutoPostBack="True"
            OnSelectedIndexChanged="upDropDownListSort_SelectedIndexChanged">
            <asp:ListItem Value="colIsActiveBit">Active</asp:ListItem>
            <asp:ListItem Value="colContractNameVarchar">Contract Title</asp:ListItem>
            <asp:ListItem Value="colContractDateStartedDate">Contract Date Start</asp:ListItem>
            <asp:ListItem Value="colContractDateEndDate">Contract Date End</asp:ListItem>
            <asp:ListItem Value="colContractStatusVarchar">Status</asp:ListItem>
        </asp:DropDownList>
    </div>
    <br />
            
   <%-- <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden; text-align:left">       
        <asp:ListView runat="server" ID="ListView1" 
            onitemcreated="ListView1_ItemCreated">
            <LayoutTemplate>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <th class="thVal" style="width: 395px">
                            <asp:Label runat="server" ID="uoLblTitleHdr" Text="Contract Title" Width="395px"></asp:Label>
                        </th>
                        <th class="thVal" style="width: 105px">
                            <asp:Label runat="server" ID="uoLblDateStartHdr" Text="Contract Date Start" Width="105px"></asp:Label>
                        </th>
                        <th class="thVal" style="width: 100px">
                            <asp:Label runat="server" ID="uoLblDateEndHdr" Text="Contract Date End" Width="100px"></asp:Label>
                        </th>
                        <th class="thVal" style="width: 100px">
                            <asp:Label runat="server" ID="uoLblStatus" Text="Status" Width="100px"></asp:Label>
                        </th>
                        <th class="thVal" style="width: 59px">
                            <asp:Label runat="server" ID="uoLblActive" Text="Active" Width="59px"></asp:Label>
                        </th>
                        <th id="AmendTh" class="thVal" runat="server" style="width: 85px">
                            <asp:Label runat="server" ID="Amend" Text="Amend" 
                                Width="85px">
                            </asp:Label>
                        </th>                                      
                        <th id="DeleteTh" class="thVal" runat="server" style="width: 86px">
                            <asp:Label runat="server" ID="Delete" Text="Inactivate" Width="86px">
                            </asp:Label>
                        </th>
                          <th id="Th2" class="thVal" style="width: 86px">
                            <asp:Label runat="server" ID="Label2" Text="Date Created" Width="86px">
                            </asp:Label>
                        </th>
                        <td runat="server" style="width: 12%" id="Th1">
                            <asp:Label runat="server" ID="Label1" Text="" Width="20px">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>--%>
        
       <%-- <asp:ListView runat="server" ID="ListView2">
            <LayoutTemplate>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <th class="thVal">
                            <asp:Label runat="server" ID="uoLblTitleHdr" Text="Contract Title" Width="397px"></asp:Label>
                        </th>
                        <th class="thVal">
                            <asp:Label runat="server" ID="uoLblDateStartHdr" Text="Contract Date Start" Width="180px"></asp:Label>
                        </th>
                        <th class="thVal">
                            <asp:Label runat="server" ID="uoLblDateEndHdr" Text="Contract Date End" Width="179px"></asp:Label>
                        </th>
                        <th class="thVal">
                            <asp:Label runat="server" ID="uoLblStatus" Text="Status" Width="100px"></asp:Label>
                        </th>
                        <th class="thVal">
                            <asp:Label runat="server" ID="uoLblActive" Text="Active" Width="59px"></asp:Label>
                        </th>
                        <td runat="server" style="width: 12%" id="Th1">
                            <asp:Label runat="server" ID="Label1" Text="" Width="20px">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
   
        </div>--%>
        <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto; text-align:left " onscroll="divScrollL();" >
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                   <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td >
                                <asp:ListView runat="server" ID="uoHotelContractList" OnItemCommand="uoHotelContractList_ItemCommand"
                                    OnItemDeleting="uoHotelContractList_ItemDeleting" 
                                    OnSelectedIndexChanging="uoHotelContractList_SelectedIndexChanging" 
                                    ondatabound="uoHotelContractList_DataBound">
                                    <LayoutTemplate>
                                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTableClass" width="100%">
                                            <tr>
                                                <th class="thVal" style="width: 395px">
                                                    <asp:Label runat="server" ID="uoLblTitleHdr" Text="Contract Title" Width="395px"></asp:Label>
                                                </th>
                                                <th class="thVal" style="width: 105px">
                                                    <asp:Label runat="server" ID="uoLblDateStartHdr" Text="Contract Date Start" Width="105px"></asp:Label>
                                                </th>
                                                <th class="thVal" style="width: 100px">
                                                    <asp:Label runat="server" ID="uoLblDateEndHdr" Text="Contract Date End" Width="100px"></asp:Label>
                                                </th>
                                                <th class="thVal" style="width: 100px">
                                                    <asp:Label runat="server" ID="uoLblStatus" Text="Status" Width="100px"></asp:Label>
                                                </th>
                                                <th class="thVal" style="width: 59px">
                                                    <asp:Label runat="server" ID="uoLblActive" Text="Active" Width="59px"></asp:Label>
                                                </th>
                                                <th id="AmendTh" class="thVal" runat="server" style="width: 85px">
                                                    <asp:Label runat="server" ID="Amend" Text="Amend" 
                                                        Width="85px">
                                                    </asp:Label>
                                                </th>                                      
                                                <th id="DeleteTh" class="thVal" runat="server" style="width: 86px">
                                                    <asp:Label runat="server" ID="Delete" Text="Inactivate" Width="86px">
                                                    </asp:Label>
                                                </th>
                                                  <th id="Th2" class="thVal" style="width: 100px">
                                                    <asp:Label runat="server" ID="Label2" Text="Date Created" Width="100px">
                                                    </asp:Label>
                                                </th>                                               
                                            </tr>
                                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <%# ContractAddGroup()%>
                                        <tr>
                                            <td class="leftAligned" style="width:397px">
                                                <asp:LinkButton runat="server" ID="uoLinkButtonContract" Text='<%# Eval("colContractNameVarchar")%>'
                                                    Width="397px" OnClientClick='<%# "return OpenContract(\"" + Eval("colBranchIDInt") + "\", \""+ Eval("colContractIdInt") +"\")" %>'>
                                                </asp:LinkButton>
                                            </td>
                                            <%--                        <td class="leftAligned">                                
                               <%# Eval("colVendorBranchNameVarchar")%>
                            </td>--%>
                                            <td class="leftAligned" style="width:105px">
                                                <asp:Label runat="server" ID="uoLblStartDate" Text='<%# Eval("colContractDateStartedDate")%>'
                                                    Width="105px"></asp:Label>
                                            </td>
                                            <td class="leftAligned" style="width:105px">
                                                <asp:Label runat="server" ID="uoLblEndDate" Text='<%# Eval("colContractDateEndDate")%>'
                                                    Width="105px"></asp:Label>
                                            </td>
                                            <td class="leftAligned" style="width:101px">
                                                <asp:Label runat="server" ID="uoLblStatus" Text='<%# Eval("colContractStatusVarchar")%>'
                                                    Width="101px"></asp:Label>
                                            </td>
                                            <td class="leftAligned" style="width:60px">
                                                <asp:Label runat="server" ID="uoLblActive" Text='<%# Eval("colIsActiveBit")%>' Width="60px"></asp:Label>
                                            </td>
                                            <%--                            <td>  class='<%# (HideVehicle()==""?"leftAligned":HideVehicle()) %>'  
                                <a runat="server" id="uoAmend" href='<%# "~/ContractManagement/HotelContractAdd.aspx?vmId=" + Eval("colBranchIDInt") + "&hvID=" + Eval("hvID") + "&st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&ufn=" + Request.QueryString["ufn"] %>'>
                                    Amend Contract</a>
                            </td>--%>
                                            <td class='<%# uoHiddenFieldAmendContractClassTD.Value  %>' style="width:90px">
                                                <asp:HyperLink runat="server" ID="uoHyperLinkAmendContract" Width="90px"
                                                     NavigateUrl='<%# "HotelContractAdd.aspx?vmId=" + Eval("colBranchIDInt") + "&hvID=" + Eval("colVendorIDInt") + "&cID=" + Eval("colContractIdInt") + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
                                                    Visible='<%# (Convert.ToBoolean(uoHiddenFieldAmendContract.Value)) %>'>
                                    Amend Contract</asp:HyperLink>
                                            </td>
                                            <td class='<%# uoHiddenFieldInactiveContractClassTD.Value %>' style="width:90px">
                                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" Width="90px"
                                                    Enabled='<%# !InactiveControl( Eval("IsCurrent")) %>'
                                                    OnClientClick='<%# "return confirmDelete(\"" + Eval("IsCurrent").ToString() + "\");" %>'
                                                    CommandArgument='<%#Eval("colContractIdInt") %>' CommandName="Select" 
                                                    Visible='<% # Convert.ToBoolean(uoHiddenFieldInactiveContract.Value) %>'
                                                    >Inactive Contract</asp:LinkButton>
                                            </td>
                                            <td style="width:100px">
                                                <asp:Label runat="server" ID="Label3" Text='<%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("colDateCreatedDate"))%>'  Width="100px"></asp:Label>                                                
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                            <tr>
                                                <td colspan="6" class="leftAligned">
                                                    No Record
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:DataPager ID="uoHotelContractListPager" runat="server" PagedControlID="uoHotelContractList"
                                    PageSize="20" OnPreRender="uoHotelContractList_PreRender">
                                    <Fields>
                                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                                        <%-- <asp:NextPreviousPagerField ButtonType="Image"  ButtonCssClass="PagerClass" NextPageImageUrl="~/Images/next.jpg" PreviousPageImageUrl="~/Images/prev.jpg" />--%>
                                    </Fields>
                                </asp:DataPager>
                                <asp:HiddenField ID="uoHiddenFieldVendor" runat="server" Value="true" />
                                <asp:HiddenField ID="uoHiddenFieldAmendContractClass" runat="server" Value="" />
                                <asp:HiddenField ID="uoHiddenFieldAmendContractClassTD" runat="server" Value="" />
                                <asp:HiddenField ID="uoHiddenFieldAmendContract" runat="server" Value="true" />
                                <asp:HiddenField ID="uoHiddenFieldInactiveContractClass" runat="server" Value="" />
                                <asp:HiddenField ID="uoHiddenFieldInactiveContractClassTD" runat="server" Value="" />
                                <asp:HiddenField ID="uoHiddenFieldInactiveContract" runat="server" Value="true" />
                                <asp:HiddenField ID="uoHiddenFieldAlign" runat="server" Value="true" />
                            </td>
                        </tr>
                    </table>
                    <%--<table style="width: 100%" cellspacing="0">
                    <tr>
                        <td align="left">
                        </td>
                        <td class="RightClass">
                            <span>Hotel:</span>
                            <asp:TextBox ID="uoTextBoxHotel" runat="server" Width="200" CssClass="TextBoxInput"
                                Visible="False"></asp:TextBox>
                            <asp:Button ID="uoButtonSearch" runat="server" Text="Search" CssClass="SmallButton"
                                OnClick="uoButtonSearch_Click" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Module" align="right">
                            
                        </td>
                    </tr>
                </table>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
</asp:Content>
