<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="HotelMaintenanceBranchView.aspx.cs" Inherits="TRAVELMART.Maintenance.HotelMaintenanceBranchView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <div class="PageTitle">
        <table cellpadding="0" border="0" cellspacing="0" width="100%">
            <tr>
                <td align="left">
                    Hotel Branch
                </td>
                <td align="right">
                    <asp:Button ID="uoBtnHotelAdd" runat="server" Text="Add Hotel Branch" Font-Size="X-Small"
                        OnClick="uoBtnHotelAdd_Click" />
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
            filterSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetBranchResolution();
                filterSettings();
            }
        }
        function SetBranchResolution() {
            var ht = $(window).height(); //550;
            var ht2 = $(window).height(); //550;
            var wd = $(window).width() *0.90;
            if (screen.height <= 600) {
                ht = ht * 0.47;
                ht2 = ht2 * 0.47;
            }
            else if (screen.height <= 720) {
            ht = ht * 0.52;
            ht2 = ht2 * 0.52;
            }
            else {
                ht = ht * 0.55;
                ht2 = ht2 * 0.68;
            }

            $("#Bv").width(wd);
            $("#Bv").height(ht);

            $("#PG").width(wd);
            $("#PG").height(ht2);
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

        function filterSettings() {

            var ddlRegion = $("#<%=uoDropDownListRegion.ClientID %>");
            var ddlCountry = $("#<%=uoDropDownListCountry.ClientID %>");
            var ddlAirport = $("#<%=uoDropDownListAirport.ClientID %>");
            var txtAirport = $("#<%=uoTextBoxSearchAirport.ClientID %>");
            var btnAirportFilter = $("#<%=uoButtonFilterAirport.ClientID %>");
            var btnViewBranch = $("#<%=uoButtonViewBranch.ClientID %>");

            var SortByBranch = $("#<%=uoHiddenFieldSortByBranch.ClientID %>");
            var SortByPrior = $("#<%=uoHiddenFieldSortByPriority.ClientID %>");

            if ($("#<%=uoCheckBoxAdvanceSearch.ClientID %>").attr('checked')) {
                $("#<%=uoTableAdvanceSearch.ClientID %>").show();
            }
            else {
                $("#<%=uoTableAdvanceSearch.ClientID %>").hide();
            }

            $("#<%=uoCheckBoxAdvanceSearch.ClientID %>").click(function() {
                if ($(this).attr('checked')) {
                    $("#<%=uoTableAdvanceSearch.ClientID %>").fadeIn();
                }
                else {
                    $("#<%=uoTableAdvanceSearch.ClientID %>").fadeOut();
                    $("#<%=uoDropDownListRegion.ClientID %>").val('0');
                    $("#<%=uoDropDownListCountry.ClientID %>").val('0');
                    $("#<%=uoDropDownListAirport.ClientID %>").val('0');
                }
            });

//            btnSavePrior.click(function(ev) {
//                $(SortByBranch).val(SortByBranch.val());
//                $(SortByPrior).val(SortByPrior.val());
//            });

            //Bind country, city
            ddlRegion.change(function(ev) {
                GetCountry($(this).val(), '');
                ClearAirport();
            });

            //Bind Airport
            ddlCountry.change(function(ev) {
                GetAirport($(this).val(), ddlRegion.val(), txtAirport.val());
            });

            btnAirportFilter.click(function(ev) {
                if (ddlRegion.val() != "0" && ddlCountry.val() != "0") {
                    GetAirport(ddlCountry.val(), ddlRegion.val(), txtAirport.val());
                }
                return false;
            });

            btnViewBranch.click(function(ev) {
                if (ddlRegion.val() == "0") {
                    alert("Select Region.");
                    return false;

                    if (ddlCountry.val() == "0") {
                        alert("Select Country.");
                        return false;
                    }
                    if (ddlAirport.val() == "0") {
                        alert("Select Airport.");
                        return false;
                    }
                }
                else
                { }


                $("#<%=uoHiddenFieldRegion.ClientID %>").val(ddlRegion.val());
                $("#<%=uoHiddenFieldCountry.ClientID %>").val(ddlCountry.val());
                $("#<%=uoHiddenFieldAirport.ClientID %>").val(ddlAirport.val());
                $("#<%=uoHiddenFieldAirportName.ClientID %>").val(txtAirport.val());
                return true;
            });

            function GetCountry(RegionID, CountryName) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/PageMethods.aspx/GetCountry",
                    data: "{'RegionID': '" + RegionID + "', 'CountryName': '" + CountryName + "'}",
                    dataType: "json",
                    success: function(data) {

                        //remove all the options in dropdown
                        $("#<%=uoDropDownListCountry.ClientID %>> option").remove();

                        //add option in dropdown
                        $("<option value='PROCESSING'>PROCESSING...</option>").appendTo(ddlCountry);
                        $("<option value='0'>--SELECT COUNTRY--</option>").appendTo(ddlCountry);

                        for (var i = 0; i < data.d.length; i++) {
                            //add the data coming from the result
                            $("<option value='" + data.d[i].CountryIDString + "'>" + data.d[i].CountryNameString + "</option>").appendTo(ddlCountry);
                        }
                        $("#<%=uoDropDownListCountry.ClientID %>> option[value='PROCESSING']").remove();
                    }
                        ,
                    error: function(objXMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            }
            function GetAirport(countryID, regionID, airportName) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/PageMethods.aspx/GetAirport",
                    data: "{'CountryID': '" + countryID + "', 'RegionID': '" + regionID + "', 'AirportName': '" + airportName + "'}",
                    dataType: "json",
                    success: function(data) {

                        //remove all the options in dropdown
                        $("#<%=uoDropDownListAirport.ClientID %>> option").remove();

                        //add option in dropdown
                        $("<option value='PROCESSING'>PROCESSING...</option>").appendTo(ddlAirport);
                        $("<option value='0'>--SELECT AIRPORT--</option>").appendTo(ddlAirport);

                        for (var i = 0; i < data.d.length; i++) {
                            //add the data coming from the result
                            $("<option value='" + data.d[i].AirportIDString + "'>" + data.d[i].AirportNameString + "</option>").appendTo(ddlAirport);
                        }
                        $("#<%=uoDropDownListAirport.ClientID %>> option[value='PROCESSING']").remove();
                    },
                    error: function(objXMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            }
            function ClearAirport() {
                //remove all the options in dropdown
                $("#<%=uoDropDownListAirport.ClientID %>> option").remove();
                $("<option value='0'>--SELECT AIRPORT--</option>").appendTo(ddlAirport);
            }
            function ClearCountry() {
                //remove all the options in dropdown
                $("#<%=uoDropDownListCountry.ClientID %>> option").remove();
                $("<option value='0'>--SELECT COUNTRY--</option>").appendTo(ddlCountry);
            }
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
            $("table.listViewTable tr").each(function(ev) {
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
    </script>

    <div id="PG" style="overflow: auto; overflow-x: auto; overflow-y: auto;">
    <div>
        <table style="width: 100%" cellspacing="0" class="LeftClass">
            <tr>
                <td class="contentCaption">
                    Hotel Branch:
                </td>
                <td class="contentValue">
                    <asp:TextBox ID="uoTextBoxSearchParam" runat="server" CssClass="TextBoxInput" Width="249px" />
                    &nbsp;
                    <asp:Button ID="uoButtonSearch" runat="server" Text="Search" CssClass="SmallButton"
                        OnClick="uoButtonSearch_Click" />
                </td>
                
            </tr>
        </table>
    </div>
    <div>
        <table width="100%" class="LeftClass" onload="opener.location.reload();">
            <tr>
                <td class="contentValue" style="width: 150px">
                    <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server" />
                </td>
               <%-- <td class="RedNotification" >
                    View hotel branch by airport to update priority.
                </td>--%>
            </tr>
        </table>
        <table style="width: 100%" cellspacing="0" class="LeftClass" runat="server" id="uoTableAdvanceSearch">
            <tr>
                <td class="contentCaption">
                    Region:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="250px" AppendDataBoundItems="True">
                    </asp:DropDownList>
                    <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListRegion" runat="server"
                        TargetControlID="uoDropDownListRegion" PromptText="Type to search" PromptPosition="Top"
                        IsSorted="true" PromptCssClass="dropdownSearch" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="contentCaption">
                    Country:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="250px" AppendDataBoundItems="True">
                    </asp:DropDownList>
                    <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListCountry" runat="server"
                        TargetControlID="uoDropDownListCountry" PromptText="Type to search" PromptPosition="Top"
                        IsSorted="true" PromptCssClass="dropdownSearch" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="contentCaption">
                    Airport:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListAirport" runat="server" Width="250px" AppendDataBoundItems="True">
                    </asp:DropDownList>
                    <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListAirport" runat="server"
                        TargetControlID="uoDropDownListAirport" PromptText="Type to search" PromptPosition="Top"
                        IsSorted="true" PromptCssClass="dropdownSearch" />
                </td>
                <td class="LeftClass">
                    <asp:TextBox ID="uoTextBoxSearchAirport" runat="server" CssClass="TextBoxInput" Width="249px" />
                    <asp:Button ID="uoButtonFilterAirport" runat="server" Text="Filter Airport" CssClass="SmallButton" />
                    <asp:Button ID="uoButtonViewBranch" runat="server" Text="View Branch" CssClass="SmallButton"
                        OnClick="uoButtonViewBranch_Click" />
                </td>
                <td class="RightClass">
                   <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="uoButtonSavePriority" runat="server" Text="Save Priority" Visible="false"
                                CssClass="SmallButton" OnClick="uoButtonSavePriority_Click" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoButtonViewBranch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="uoButtonSearch" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                </td>
            </tr>
        </table>
    </div>
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;" align="right">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ListView runat="server" ID="uoHotelVendorList" OnItemCommand="uoHotelVendorList_ItemCommand"
                    OnItemDeleting="uoHotelVendorList_ItemDeleting" OnItemDataBound="uoHotelVendorList_ItemDataBound">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <tr>
                                <th>
                                    <asp:LinkButton ID="uoLinkButtonBranch" runat="server" CommandName="SortByBranchName">Branch</asp:LinkButton>
                                </th>
                                <th>                                    
                                     <asp:LinkButton ID="LinkButton1" runat="server" CommandName="colRegionNameVarchar" Text="Region"/>
                                </th>
                                <th>
                                    
                                     <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Country" Text="Country"/>
                                </th>
                                <th>
                                    
                                     <asp:LinkButton ID="LinkButton3" runat="server" CommandName="City" Text="City"/>
                                </th>
                                <th runat="server" id="PriorityTH" visible="false">
                                    <asp:LinkButton ID="uoLinkButtonPriority" runat="server" CommandName="SortByPriority"
                                        Text="Priority"></asp:LinkButton>
                                </th>
                                <th runat="server" style="width: 5%" id="EditTH">
                                </th>
                                <th runat="server" style="width: 10%" id="ContractListTH">
                                </th>
                                <th runat="server" style="width: 10%" id="ContractTH">
                                </th>                                
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                                <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value='<%# Eval("colBranchIDInt") %>' />
                                <asp:LinkButton runat="server" ID="uoLinkButtonContract" Text='<%# Eval("colVendorBranchNameVarchar") %>'
                                    OnClientClick='<%# "return OpenContract(\"" + Eval("colBranchIDInt") + "\",\"" + Eval("colContractIdInt") + "\")" %>'
                                    Visible='<%# Eval("IsWithContract") %>'>
                                </asp:LinkButton>
                                <asp:Label ID="uoLabelVendor" runat="server" Text='<%# Eval("colVendorBranchNameVarchar") %>'
                                    Visible='<%# !(Boolean)Eval("IsWithContract") %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colRegionNameVarchar")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("country")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("city")%>
                            </td>
                            <td class='<%# (uoHiddenFieldViewPriority.Value == "true"? "":"hideElement") %>'>
                                <asp:TextBox ID="uoTextBoxPriority" runat="server" Width="60px" CssClass="ReadOnly" ReadOnly="true" 
                                    Text='<%# Eval("Priority")%>' Orig='<%# Eval("Priority")%>' Visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>'
                                    onkeypress="return validate(event);"></asp:TextBox>
                            </td>
                            <td class='<%# uoHiddenFieldVendorClass.Value  %>'>
                                <a runat="server" id="uoAEditHotel" href='<%# "HotelMaintenanceBranch.aspx?vmId=" + Eval("colBranchIDInt") + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
                                    visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>'>Edit</a>
                            </td>
                            <td class='<%# uoHiddenFieldViewContractClass.Value  %>'>
                                <a runat="server" visible='<%# (Convert.ToBoolean(uoHiddenFieldViewContract.Value)) %>'
                                    id="A1" href='<%# "../ContractManagement/HotelContractListView.aspx?vmId=" + Eval("colBranchIDInt") + "&hvID=" + Eval("hvID") + "&hbName=" + Eval("colVendorBranchNameVarchar") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'>
                                    Contract List</a>
                            </td>
                            <td class='<%# uoHiddenFieldEditAddContractClass.Value  %>'>
                                <a runat="server" id="uoAddContract" href='<%# "../ContractManagement/HotelContractAdd.aspx?vmId=" + Eval("colBranchIDInt") + "&hvID=" + Eval("hvID") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
                                    visible='<%# (Convert.ToBoolean(uoHiddenFieldEditAddContract.Value)) %>'>Add Contract</a>
                            </td>
                            <%-- <td>
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                    CommandArgument='<%#Eval("hvID") %>' CommandName="Delete" >Delete</asp:LinkButton>
                            </td>--%>                            
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    Branch
                                </th>
                                <th>
                                    Region
                                </th>
                                <th>
                                    Country
                                </th>
                                <th>
                                    City
                                </th>
                            </tr>
                            <tr>
                                <td colspan="4" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <div align="left">
                    <asp:DataPager ID="uoHotelVendorListPager" runat="server" PagedControlID="uoHotelVendorList"
                        PageSize="20" OnPreRender="uoHotelVendorListPager_PreRender">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                            <%-- <asp:NextPreviousPagerField ButtonType="Image"  ButtonCssClass="PagerClass" NextPageImageUrl="~/Images/next.jpg" PreviousPageImageUrl="~/Images/prev.jpg" />--%>
                        </Fields>
                    </asp:DataPager>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="uoButtonViewBranch" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="uoButtonSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div>
                <asp:HiddenField ID="uoHiddenFieldVendor" runat="server" Value="true" />
                <asp:HiddenField ID="uoHiddenFieldVendorClass" runat="server" Value="" />
                <asp:HiddenField ID="uoHiddenFieldViewContract" runat="server" Value="true" />
                <asp:HiddenField ID="uoHiddenFieldViewContractClass" runat="server" Value="" />
                <asp:HiddenField ID="uoHiddenFieldEditAddContract" runat="server" Value="true" />
                <asp:HiddenField ID="uoHiddenFieldEditAddContractClass" runat="server" Value="" />
                <asp:HiddenField ID="uoHiddenFieldViewPriority" runat="server" Value="true" />
                <asp:HiddenField ID="uoHiddenFieldRegion" runat="server" Value="" />
                <asp:HiddenField ID="uoHiddenFieldCountry" runat="server" Value="" />
                <asp:HiddenField ID="uoHiddenFieldAirport" runat="server" Value="" />
                <asp:HiddenField ID="uoHiddenFieldAirportName" runat="server" Value="" />
                <asp:HiddenField ID="uoHiddenFieldSortByBranch" runat="server" Value="1" />
                <asp:HiddenField ID="uoHiddenFieldSortByPriority" runat="server" Value="2" />
                <asp:HiddenField ID="uoHiddenFieldOrderBy" runat="server" Value="" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="uoButtonViewBranch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="uoButtonSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </div>
</asp:Content>
