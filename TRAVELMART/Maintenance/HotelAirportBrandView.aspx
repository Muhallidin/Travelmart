<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="True" CodeBehind="HotelAirportBrandView.aspx.cs" Inherits="TRAVELMART.Maintenance.HotelAirportBrandView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <div class="PageTitle">
        <table cellpadding="0" border="0" cellspacing="0" width="100%">
            <tr>
                <td align="left">
                    Hotel Branch: Airport-Brand Assignment
                </td>
                <td align="right">
                    <%--<a href="HotelAirportBrandPopup.aspx" id="aHotelAirportBrandPopup">--%>
                        <asp:Button ID="uoBtnHotelAdd" runat="server" Text="Add Hotel Branch" 
                        Font-Size="X-Small" onclick="uoBtnHotelAdd_Click" />
                    <%--</a>--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

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
            SetBranchResolution();
            ShowPopup();
        });

//        function tableSettings() {

//            $("#tableHotel tr").each(function() {
//                $(this).find("#TDEdit").each(function() {
//                    $(this).removeAttr("style");
//                });
//                $(this).find("#TDEditAssignment").each(function() {
//                    $(this).removeAttr("style");
//                });
//                $(this).find("#TdAdd").each(function() {
//                    $(this).removeAttr("style");
//                });
//            });
//        }

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetBranchResolution();
                ShowPopup();
            }
        }
        function ShowPopup() {            
            $("a#aHotelAirportBrandPopup").fancybox(
            {
                'width': '60%',
                'height': '80%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopup.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });

            $(".clsEditAssignment").fancybox(
            {
                'width': '80%',
                'height': '80%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopup.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });
        }
        function SetBranchResolution() {
            var ht = $(window).height()* 1.7;
            var ht2 = $(window).height() * 1.2;

            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.22;
                ht2 = ht2 * 0.46;
                wd = $(window).width();
            }
            else if (screen.height <= 720) {
                ht = ht * 0.28;
                ht2 = ht2 * 0.59;
            }
            else {
                ht = ht * 0.335;
                ht2 = ht2 * 0.62;
            }

            $("#Bv").height(ht);
            $("#Av").width(wd);
            $("#Bv").width(wd);
        }

        function OpenContract(vendorID, ContractID) {
            var URLString = "../ContractManagement/HotelContractView.aspx?vId=" + vendorID + "&cId=" + ContractID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
       

        function validate(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            if ((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) {
                return false;
            }
            else {
                return true;
            }
        }
        function validatePriority(branchIDChange, txtBoxID) {
            //            $("table.listViewTable input[type='text']").each(function(ev) {            
            var bReturn = true;
            var textboxToChange = $("#" + txtBoxID + "");
            var newValue = textboxToChange.val();
            $("#tableHotel tr").each(function(ev) {
                var branchID = $(this).find("[id$='uoHiddenFieldBranchID']").val();
                if (branchID != undefined) {
                    //alert(branchID);
                    var priority = $(this).find("input[id$='uoTextBoxPriority']");
                    if (branchIDChange != branchID) {
                        if (newValue == priority.val() && newValue != 0) {
                            var hotelName = $(this).find("[id$=uoLinkButtonContract]").text();
                            if (hotelName == "") {
                                hotelName = $(this).find("[id$=uoLabelVendor]").text();
                            }
                            var isConfirm = confirm("Cannot assign the same priority!\n" + hotelName + " will be reset.");
                            if (isConfirm) {
                                priority.val("0");
                                priority.attr('Orig', '0');
                                bReturn = true;
                            }
                            else {
                                var recentVal = textboxToChange.attr('Orig');
                                textboxToChange.val(recentVal);
                                bReturn = false;
                            }
                        }
                    }
                }
            });
            if (bReturn) {
                textboxToChange.attr('Orig', newValue);
            }
        }

        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;      
        }  
    </script>
<%--
    <div id="PG" style="overflow: auto; overflow-x: auto; overflow-y: auto;">--%>
        <table style="width: 100%" cellspacing="0" class="LeftClass">
            <tr>
                <td class="contentCaption">Region:</td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="250px" 
                        AppendDataBoundItems="True" AutoPostBack="True" 
                        onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                    <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListRegion" runat="server"
                        TargetControlID="uoDropDownListRegion" PromptText="Type to search" PromptPosition="Top"
                        IsSorted="true" PromptCssClass="dropdownSearch" />
                </td>
                <td class="contentCaption">Airport:</td>
                <td>
                      <asp:TextBox ID="uoTextBoxAirport" runat="server" CssClass="TextBoxInput" Width="250px" />
                      <asp:Label Text=" " runat="server" ID="ucLabelSpace"></asp:Label>
                    
                      <asp:DropDownList ID="uoDropDownListAirport" runat="server" Width="250px" 
                          AppendDataBoundItems="True" >
                      </asp:DropDownList>
                      <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListAirport" runat="server"
                        TargetControlID="uoDropDownListAirport" PromptText="Type to search" PromptPosition="Top"
                        IsSorted="true" PromptCssClass="dropdownSearch" />
                    <asp:Button ID="uoButtonFilterAirport" runat="server" Text="Filter Airport" 
                          CssClass="SmallButton" onclick="uoButtonFilterAirport_Click" />
                </td>
                
            </tr>
            <tr>
                <td class="contentCaption">Brand:</td>
                <td class="contentValue">
                     <asp:DropDownList ID="uoDropDownListBrand" runat="server" Width="250px" 
                         AppendDataBoundItems="True" >
                    </asp:DropDownList>
                </td>
                <td>Hotel Branch:
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxSearchParam" runat="server" CssClass="TextBoxInput" Width="249px" />
                    <asp:Button ID="uoButtonViewBranch" runat="server" Text="View Branch" CssClass="SmallButton"
                        OnClick="uoButtonViewBranch_Click" /> 
                    <asp:Button ID="uoButtonSavePriority" runat="server" Text="Save Priority" CssClass="SmallButton"
                        OnClick="uoButtonSavePriority_Click" />
                </td>
                
            </tr>
            <tr>
                <td class="contentCaption">Room Type:</td>
                <td class="contentValue">
                     <asp:DropDownList ID="uoDropDownListRoom" runat="server" Width="250px" 
                         AppendDataBoundItems="true"   >                       
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                
            </tr>
            </table>
       <br />  
       <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
            <asp:ListView runat="server" ID="uoListViewHeader" 
                OnItemCommand="uoListViewHeader_ItemCommand" 
                ondatabound="uoListViewHeader_DataBound" 
                onitemdatabound="uoListViewHeader_ItemDataBound" 
                onitemcreated="uoListViewHeader_ItemCreated" >
               <LayoutTemplate>
                </LayoutTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="listViewTable">                      
                         <tr>
                                <th  style="text-align: center; white-space: normal;width:300px">
                                    <asp:LinkButton ID="uoLinkButtonBranch" runat="server" CommandName="SortByHotelName" Text="Hotel Name" Width="300px">
                                    </asp:LinkButton>
                                </th>                               
                                <th  style="text-align: center; white-space: normal; width:240px">                                    
                                     <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByCountry" Text="Country" Width="240px"/>
                                </th>
                                <th  style="text-align: center; white-space: normal; width:150px">
                                     <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByCity" Text="City" Width="150px"/>
                                </th>
                                <th runat="server" id="PriorityTH" visible="false" style="width:60px ">
                                    <asp:LinkButton ID="uoLinkButtonPriority" runat="server" CommandName="SortByPriority" Width="60px"
                                        Text="Priority" ></asp:LinkButton>
                                </th>
                                <th  style="text-align: center; white-space: normal; width:170px">
                                     <asp:LinkButton ID="LinkButton7" runat="server" CommandName="SortByEmail" Text="Email" Width="170px"/>
                                </th>
                                <th style="width:70px" runat="server" id="THEditHotel" >                                    
                                     <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByEdit" Text="Edit Hotel" Width="70px"/>
                                </th>
                                 <th runat="server" id="THEditAssignment" style="width:90px">
                                     <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByEdit" Text="Edit Assignment" Width="90px"/>
                                </th>
                                <th style="width:85px" id="THContractList" runat="server">                                    
                                     <asp:LinkButton ID="LinkButton4" runat="server" CommandName="SortByContractList" Text="Contract" Width="85px"/>
                                </th>
                                 <th style="width:100px " runat="server" id="THAdd">                                    
                                     <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByContractAdd" Text="Add" Width="100px" />
                                </th>  
                                <th style="width:100px"></th>
                            </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
    </div>
     <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
            onscroll="divScrollL();">
                <asp:ListView runat="server" ID="uoListViewHotel" OnItemCommand="uoListViewHotel_ItemCommand"
                    OnItemDeleting="uoListViewHotel_ItemDeleting" 
                    OnItemDataBound="uoListViewHotel_ItemDataBound" 
                    ondatabound="uoListViewHotel_DataBound" 
                    onpagepropertieschanging="uoListViewHotel_PagePropertiesChanging">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" id="tableHotel">
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                           <%-- <td  class="leftAligned" style="white-space: normal;">
                                  <asp:Label runat="server" ID="Label1" Text="Test1" Width="145px"></asp:Label>
                            </td>
                            <td  class="leftAligned" style="white-space: normal;">
                                  <asp:Label runat="server" ID="Label2" Text="Test2" Width="95px"></asp:Label>
                            </td>
                            <td  class="leftAligned" style="white-space: normal;">
                                  <asp:Label runat="server" ID="Label3" Text="Test3" Width="95px"></asp:Label>
                            </td>
                            <td  class="leftAligned" style="white-space: normal;">
                                  <asp:Label runat="server" ID="Label4" Text="Test4" Width="45px"></asp:Label>
                            </td>
                            <td  class="leftAligned" style="white-space: normal;">
                                  <asp:Label runat="server" ID="Label5" Text="Test5" Width="45px"></asp:Label>
                            </td>
                            <td  class="leftAligned" style="white-space: normal;">
                                  <asp:Label runat="server" ID="Label6" Text="Test6" Width="45px"></asp:Label>
                            </td>--%>
                            <td class="leftAligned" style="width:305px">
                                <asp:HiddenField ID="uoHiddenFieldBrandAirportHoteID" runat="server" Value='<%# Eval("BrandAirHotelID") %>' />
                                <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value='<%# Eval("HotelID") %>' />
                                <asp:LinkButton runat="server" ID="uoLinkButtonContract" Text='<%# Eval("HotelName") %>'
                                    OnClientClick='<%# "return OpenContract(\"" + Eval("HotelID") + "\",\"" + Eval("colContractIdInt") + "\")" %>'
                                    Visible='<%# Eval("IsWithContract") %>'  Width="300px">
                                </asp:LinkButton>
                                <asp:Label ID="uoLabelVendor" runat="server" Text='<%# Eval("HotelName") %>' Width="300px"
                                    Visible='<%# !(Boolean)Eval("IsWithContract") %>' ></asp:Label>
                            </td>                         
                            <td class="leftAligned" style="width:250px">
                                <asp:Label runat="server" ID="uoLabelCountry" Text='<%# Eval("country")%>' Width="250px" ></asp:Label>
                            </td>
                            <td class="leftAligned"  style="width:200px">
                                <asp:Label runat="server" ID="Label1" Text='<%# Eval("city")%>' Width="155px"></asp:Label>
                            </td>
                            <td runat="server" id="ucTD" visible = '<%# Eval("IsPriorityVisible") %>' class="centerAligned" style="width:60px ">
                                <asp:TextBox ID="uoTextBoxPriority" runat="server" Width="60px" 
                                    Text='<%# Eval("Priority") %>' Orig='<%# Eval("Priority")%>' 
                                    onkeypress="return validate(event);" Font-Size="10px" ></asp:TextBox>
                            </td>
                             <td class="leftAligned"  style="width:180px;" >
                                <asp:Label runat="server" ID="Label2" Text='<%# Eval("Email")%>' Width="180px" ></asp:Label>
                            </td> 
                            <td style="width:80px"  id = "TDEdit" class='<%# uoHiddenFieldEditVisible.Value %>'> 
                                <asp:HyperLink runat="server" id="uoAEditHotel" href='<%# "HotelMaintenanceBranch.aspx?vmId=" + Eval("HotelID") + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
                                    Text="Edit" Width="80px"></asp:HyperLink>
                            </td>
                             <td style="width:100px" id = "TDEditAssignment" class='<%# uoHiddenFieldClassHideVisible.Value %>'>
                                <asp:HyperLink runat="server" id="HyperLink1" href='<%# "HotelAirportBrandPopup.aspx?vmId=" + Eval("HotelID") + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
                                    Text="Edit Assignment" Width="100px" CssClass="clsEditAssignment"></asp:HyperLink>
                            </td>
                            <td style="width:95px" id="TdContract" class='<%# uoHiddenFieldContractListVisible.Value %>'>
                                <asp:HyperLink runat="server" 
                                    id="A1" href='<%# "../ContractManagement/HotelContractListView.aspx?vmId=" + Eval("HotelID") + "&hvID=" + "&hbName=" + Eval("HotelName") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
                                    Text="Contract List" Width="95px"></asp:HyperLink>
                            </td>
                            <td id="TdAdd" style="width:70px " class='<%# uoHiddenFieldClassHideVisible.Value %>'  >
                                <asp:HyperLink runat="server" id="uoAddContract" href='<%# "../ContractManagement/HotelContractAdd.aspx?vmId=" + Eval("HotelID") + "&hvID=" + Eval("VendorID") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
                                    visible='<%# Eval("IsContractAddEditVisible") %>' Text="Add Contract" Width="70px" ></asp:HyperLink>
                            </td>                                                 
                            <td id="TdExtra" style="width:100px"></td>  
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%" >
                            <tr>                                
                                <td colspan="4" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <div align="left">
                    <asp:DataPager ID="uoListViewHotelPager" runat="server" PagedControlID="uoListViewHotel"
                        PageSize="20" OnPreRender="uoListViewHotelPager_PreRender">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        </Fields>
                    </asp:DataPager>
                </div>
             </div>   
            <asp:ObjectDataSource ID="uoObjectDataSourceHotel" runat="server" MaximumRowsParameterName="iMaxRow"
            SelectCountMethod="GetBranchCount" SelectMethod="GetBranch"
            StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.Maintenance.HotelAirportBrandView"
            OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" >
            <SelectParameters>
                <asp:ControlParameter Name="iRegionID" Type="Int32" ControlID="uoDropDownListRegion" />
                <asp:ControlParameter Name="iAirportID"  Type="Int32" ControlID="uoDropDownListAirport" />
                <asp:ControlParameter Name="sAirportName"  Type="String" ControlID="uoTextBoxAirport" />
                <asp:ControlParameter Name="iRoomTypeID"  Type="Int16" ControlID="uoDropDownListRoom" />
                <asp:ControlParameter Name="iBrandID"  Type="Int32" ControlID="uoDropDownListBrand" />
                <asp:ControlParameter Name="sBranchFilter"  Type="String" ControlID="uoTextBoxSearchParam" />
                <asp:ControlParameter Name="sOrderBy" Type="String" ControlID="uoHiddenFieldOrderBy" />                
                <%--<asp:ControlParameter Name="iLoadType"  Type="Int16" ControlID="uoHiddenFieldLoadType" />--%>
                <asp:Parameter Name="iLoadType"  Type="Int16" DefaultValue="1" />
                <asp:ControlParameter Name="sUserID"  Type="String" ControlID="uoHiddenFieldUser" />
            </SelectParameters>
        </asp:ObjectDataSource>                
    
   <%-- </div>--%>   
        <asp:HiddenField ID="uoHiddenFieldVendor" runat="server" Value="true" />
        <asp:HiddenField ID="uoHiddenFieldVendorClass" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldViewContract" runat="server" Value="true" />
        <asp:HiddenField ID="uoHiddenFieldViewContractClass" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldEditAddContract" runat="server" Value="true" />
        <asp:HiddenField ID="uoHiddenFieldEditAddContractClass" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldViewPriority" runat="server" Value="true" />
        <asp:HiddenField ID="uoHiddenFieldRegion" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldCountry" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldPopup" runat="server" Value="0" />
        
        <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />        
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldOrderBy" runat="server" Value="" />                
        <asp:HiddenField ID="uoHiddenFieldClassHideVisible" runat="server" Value="hideElement" />
        <asp:HiddenField ID="uoHiddenFieldEditVisible" runat="server" Value="hideElement" />
        <asp:HiddenField ID="uoHiddenFieldContractListVisible" runat="server" Value="hideElement" />
        
</asp:Content>
