<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster.Master" AutoEventWireup="true" CodeBehind="HotelAirportView_old.aspx.cs" Inherits="TRAVELMART.Maintenance.HotelAirportView_old" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
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
            filterSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                filterSettings();
            }
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
           
            var btnSavePrior = $("#<%=uoButtonSavePriority.ClientID %>");
           
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
                }
                if (ddlCountry.val() == "0") {
                    alert("Select Country.");
                    return false;
                }
                if (ddlAirport.val() == "0") {
                    alert("Select Airport.");
                    return false;
                }

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

            $("a#<%=uoHyperLinkAddHotel.ClientID %>").fancybox({
                'width': '40%',
                'height': '50%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupAdd.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });

            $("#<%=uoBtnHotelAdd.ClientID %>").click(function(ev) {
                var regionID = $("#<%=uoDropDownListRegion.ClientID %>").val();
                var countryID = $("#<%=uoDropDownListCountry.ClientID %>").val();
                var airportID = $("#<%=uoDropDownListAirport.ClientID %>").val();

                //var airportName = $("#<%=uoDropDownListAirport.ClientID %>").text();

                //var DropdownList = document.getElementById('<%=uoDropDownListAirport.ClientID %>');
                //var SelectedIndex = DropdownList.selectedIndex;
                //var SelectedValue = DropdownList.value;
                //var SelectedText = DropdownList.options[DropdownList.selectedIndex].text;

                //alert(SelectedText);
                if (airportID == "0") {
                    alert("Select Airport");
                    return false;
                }
                else {
                    $("a#<%=uoHyperLinkAddHotel.ClientID %>").attr("href", "HotelAirportAdd.aspx?aID=" + airportID + "&rID=" + regionID + "&cID=" + countryID); //+ "&rID=" + regionID  //+ "&aName=" + airportName)
                }
                return true;
            });
//            btnSavePrior.click(function(ev) {
//                $(SortByBranch).val(SortByBranch.val());
//                $(SortByPrior).val(SortByPrior.val());
//            });
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
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>             
    <table width="100%" id="uotableHotelVendorList" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;">
                <div class="PageTitle">
                    <table style="width: 100%;" cellspacing="0">
                        <tr>
                            <td class="LeftClass">
                                Airport and
                                Hotel Branch
                                Assignment</td>                            
                             <td class="RightClass">         
                              <a id="uoHyperLinkAddHotel" runat="server" href="HotelAirportAdd.aspx" >                       
                                <asp:Button ID="uoBtnHotelAdd" runat="server" Text="Add Hotel Branch" 
                                     Font-Size="X-Small"  />
                             </a>
                            </td>
                        </tr>
                    </table>
                </div>
                    <table style="width: 100%" cellspacing="0"  class="LeftClass" runat="server" id="uoTableAdvanceSearch">
                        <tr>                            
                            <td class="contentCaption">Region:</td>
                            <td class="contentValue" >
                               <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="250px"
                                    AppendDataBoundItems="True"  >
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListRegion" runat="server"
                                    TargetControlID="uoDropDownListRegion" PromptText="Type to search" PromptPosition="Top"
                                    IsSorted="true" PromptCssClass="dropdownSearch" />                            
                            </td>
                            <td></td> 
                            <td></td>                          
                        </tr>
                        <tr>                            
                            <td class="contentCaption">Country:</td>
                            <td class="contentValue" >
                               <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="250px"
                                    AppendDataBoundItems="True" >
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListCountry" runat="server"
                                    TargetControlID="uoDropDownListCountry" PromptText="Type to search" PromptPosition="Top"
                                    IsSorted="true" PromptCssClass="dropdownSearch" />                            
                            </td>
                            <td></td>                           
                            <td></td> 
                        </tr>
                        <tr>                            
                            <td class="contentCaption">Airport:</td>
                            <td class="contentValue" >
                               <asp:DropDownList ID="uoDropDownListAirport" runat="server" Width="250px" 
                                    AppendDataBoundItems="True" >
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListAirport" runat="server"
                                    TargetControlID="uoDropDownListAirport" PromptText="Type to search" PromptPosition="Top"
                                    IsSorted="true" PromptCssClass="dropdownSearch" />                            
                            </td>
                            <td class="LeftClass">
                                
                                <asp:TextBox ID="uoTextBoxSearchAirport" runat="server" CssClass="TextBoxInput" 
                                    Width="249px" />                                
                                <asp:Button ID="uoButtonFilterAirport" runat="server" Text="Filter Airport" 
                                    CssClass="SmallButton" onclick="uoButtonFilterAirport_Click" />                                 
                                <asp:Button ID="uoButtonViewBranch" runat="server" Text="View Branch" 
                                    CssClass="SmallButton" onclick="uoButtonViewBranch_Click" />
                            </td>  
                            <td class="RightClass">
                             <asp:Button ID="uoButtonSavePriority" runat="server" Text="Save Priority" Visible="false"
                                    CssClass="SmallButton" onclick="uoButtonSavePriority_Click" />
                             </td>                         
                        </tr>
                    </table>               
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">                                                               
                <asp:ListView runat="server" ID="uoHotelVendorList" OnItemCommand="uoHotelVendorList_ItemCommand"
                    OnItemDeleting="uoHotelVendorList_ItemDeleting" 
                    ondatabound="uoHotelVendorList_DataBound" 
                    onitemdatabound="uoHotelVendorList_ItemDataBound" >
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    Brand
                                </th>
                                <th>
                                    <asp:LinkButton ID="uoLinkButtonBranch" runat="server" CommandName="SortByBranchName" Text="Branch"></asp:LinkButton>
                                </th>                                
                                <th>
                                    Country
                                </th>
                                <th>
                                    City
                                </th> 
                                 <th runat="server" id="PriorityTH" style="display:none">
                                    <asp:LinkButton ID="uoLinkButtonPriority" runat="server" CommandName="SortByPriority" Text="Priority"></asp:LinkButton>
                                </th>                                                                 
                                <th runat="server" style="width: 12%" id="THDelete"> </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                                <%# Eval("COMPANY")%>
                            </td>
                            <td class="leftAligned">
                                <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value='<%# Eval("colBranchIDInt") %>'/>
                                <asp:LinkButton runat="server" id="uoLinkButtonContract" Text='<%# Eval("colVendorBranchNameVarchar") %>'            
                                OnClientClick = '<%# "return OpenContract(\"" + Eval("colBranchIDInt") + "\")" %>' Visible ='<%# Eval("IsWithContract") %>'>
                                </asp:LinkButton>
                                <asp:Label ID="uoLabelVendor" runat="server" Text='<%# Eval("colVendorBranchNameVarchar") %>' Visible ='<%# !(Boolean)Eval("IsWithContract") %>'></asp:Label>
                            </td>                           
                            <td class="leftAligned">
                                <%# Eval("country")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("city")%>
                            </td>
                             <%--<td class='<%# (uoHiddenFieldViewPriority.Value == "true"? "":"hideElement") %>'>
                                <asp:TextBox ID="uoTextBoxPriority" runat="server" Width="60px" CssClass="SmallTextCenter" Text='<%# Eval("Priority")%>' Orig='<%# Eval("Priority")%>' 
                                 visible='<%# (Convert.ToBoolean(uoHiddenFieldViewPriority.Value)) %>' onkeypress="return validate(event);"></asp:TextBox>
                            </td>--%>
                            <td runat="server" id="PriorityTR">
                                <asp:TextBox ID="uoTextBoxPriority" runat="server" Width="60px" CssClass="SmallTextCenter" Text='<%# Eval("Priority")%>' Orig='<%# Eval("Priority")%>' 
                                 onkeypress="return validate(event);"></asp:TextBox>
                            </td>
                            <td class = '<%# uoHiddenFieldVendorClass.Value  %>'>
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("colAirportIdInt") + "::" + Eval("colBranchIDInt") %>' OnClientClick="javascript: return confirmDelete();">Delete</asp:LinkButton>
                                <%--<a runat="server"  id="uoAEditHotel" href='<%# "~/Maintenance/HotelMaintenanceBranch.aspx?vmId=" + Eval("colBranchIDInt") + "&ufn=" + Request.QueryString["ufn"] %>'
                                    visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>'>
                                    Delete</a>--%>
                            </td>
                                                   
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
                <asp:ObjectDataSource ID="uoObjectDataSourceHotel" runat="server" 
                    onselecting="uoObjectDataSourceHotel_Selecting" 
                    MaximumRowsParameterName="MaxRow" SelectCountMethod="GetHotelVendorBranchListByUserCount" 
                    SelectMethod="GetHotelVendorBranchListByUserWithCount" StartRowIndexParameterName="StartRow" 
                    TypeName="TRAVELMART.BLL.MaintenanceViewBLL" 
                    OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" 
                    ondeleting="uoObjectDataSourceHotel_Deleting"
                    DeleteMethod="RemoveHotelInAirportNoUse">                
                <SelectParameters>
                    <asp:Parameter Name="strHotelName" Type="String"/>
                    <asp:Parameter Name="strUser" Type="String"/>
                    <asp:Parameter Name="Region" Type="String"/>
                    <asp:Parameter Name="Country" Type="String" />                    
                    <asp:Parameter Name="Airport" Type="String" />
                    <asp:Parameter Name="Port" Type="String" />
                    
                    <asp:Parameter Name="Hotel" Type="String" />
                    <asp:Parameter Name="UserRole" Type="String" />
                    <asp:Parameter Name="LoadType" Type="String" />   
                    <asp:Parameter Name="SortBy" Type ="String" />
                </SelectParameters>
                </asp:ObjectDataSource>                                                                       
            </td>
        </tr>      
        <tr>
            <td class="LeftClass">
                <asp:DataPager ID="uoHotelVendorListPager" runat="server" PagedControlID="uoHotelVendorList"
                    PageSize="20" OnPreRender="uoHotelVendorListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        <%-- <asp:NextPreviousPagerField ButtonType="Image"  ButtonCssClass="PagerClass" NextPageImageUrl="~/Images/next.jpg" PreviousPageImageUrl="~/Images/prev.jpg" />--%>
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>  
    </table> 
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />   
    <asp:HiddenField ID="uoHiddenFieldUserRole" runat="server" />   
    <asp:HiddenField ID="uoHiddenFieldRegion" runat="server" Value = "0" />   
    <asp:HiddenField ID="uoHiddenFieldCountry" runat="server"  Value="0"/>   
    <asp:HiddenField ID="uoHiddenFieldAirport" runat="server" Value="0"/>   
    <asp:HiddenField ID="uoHiddenFieldAirportName" runat="server" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldLoadType" Value = "0" />
    
    <asp:HiddenField ID="uoHiddenFieldVendor" runat="server" Value="true" />
    <asp:HiddenField Id="uoHiddenFieldVendorClass" runat="server" Value="" />
    
    <asp:HiddenField Id="uoHiddenFieldPopupAdd" runat="server" Value="" /> 
    <asp:HiddenField ID="uoHiddenFieldViewPriority" runat="server" Value="true" />   
    
    <asp:HiddenField ID="uoHiddenFieldSortBy" runat="server" Value="SortByBranchName" />    
        
    </ContentTemplate>        
    </asp:UpdatePanel>    
    
    <script type="text/javascript">
        function validatePriority(branchIDChange, txtBoxID) {            
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
</asp:Content>
