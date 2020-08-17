<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartHotel.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="HotelManifest2.aspx.cs" Inherits="TRAVELMART.Hotel.HotelManifest2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function validate(key) {
            if ($("#<%=uoDropDownListFilter.ClientID %>").val() == "1") {

                //getting key code of pressed key
                var keycode = (key.which) ? key.which : key.keyCode;
                if ((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) {
                    return false;
                }
                else {
                    return true;
                }
            }
            return true;
        }  
        
        $(document).ready(function() {
            filterSettings();
            setSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                filterSettings();
            }
        }

        function SetVisibility() {
            if ($("#<%=uoChkBoxSearch.ClientID %>").attr('checked')) {
                $("#<%=uoTableSearch.ClientID %>").show();
            }
            else {
                $("#<%=uoTableSearch.ClientID %>").hide();
            }
        }
        function setSettings() {
            $("#<%=uoTableSearch.ClientID %>").hide();
//            $("#<%=uoChkBoxPDF.ClientID %>").attr("disabled", "disabled");
//            $("#<%=uoChkBoxSendDiff.ClientID %>").attr("disabled", "disabled");
//            $("#<%=uoBtnSendEmail.ClientID %>").attr("disabled", "disabled"); 
            $("#<%=uoHiddenBranch.ClientID %>").val($("#<%=uoDropDownListBranch.ClientID %>").val());

            if (($("#<%=uoDropDownListBranch.ClientID %>").val() == '0') ||
                ($("#<%=uoDropDownListManifest.ClientID %>").val() == '0')) {
                $("#<%=uoBtnView.ClientID %>").attr("disabled", "disabled");
                $("#<%=uoBtnSearch.ClientID %>").attr("disabled", "disabled");
                $("#<%=uoDropDownListCompare.ClientID %>").hide();
                $("#<%=uoBtnCompare.ClientID %>").hide();
                $("#<%=uoLblCompare.ClientID %>").hide();
                $("#<%=uoLblCompareHeader.ClientID %>").hide();
                $("#<%=uoDropDownListBranch.ClientID %>").val($("#<%=uoDropDownListBranch.ClientID %>").val());
                $("#<%=uoDropDownListManifest.ClientID %>").val($("#<%=uoDropDownListManifest.ClientID %>").val());
            }
            else {
                $("#<%=uoBtnView.ClientID %>").removeAttr("disabled");
                $("#<%=uoBtnSearch.ClientID %>").removeAttr("disabled");
                $("#<%=uoDropDownListCompare.ClientID %>").show();
                $("#<%=uoBtnCompare.ClientID %>").show();
                $("#<%=uoLblCompareHeader.ClientID %>").show();
                $("#<%=uoDropDownListBranch.ClientID %>").val($("#<%=uoDropDownListBranch.ClientID %>").val());
                $("#<%=uoDropDownListManifest.ClientID %>").val($("#<%=uoDropDownListManifest.ClientID %>").val());
            }
        }

        function filterSettings() {
            if ($("#<%=uoChkBoxSearch.ClientID %>").attr('checked')) {
                $("#<%=uoTableSearch.ClientID %>").show();
            }
            else {
                $("#<%=uoTableSearch.ClientID %>").hide();
            }

            $("#<%=uoDropDownListManifest.ClientID %>").change(function(ev) {
                if (($("#<%=uoDropDownListBranch.ClientID %>").val() == '0') ||
                ($("#<%=uoDropDownListManifest.ClientID %>").val() == '0')) {
                    $("#<%=uoBtnView.ClientID %>").attr("disabled", "disabled");
                    $("#<%=uoBtnSearch.ClientID %>").attr("disabled", "disabled");
                    $("#<%=uoDropDownListCompare.ClientID %>").hide();
                    $("#<%=uoBtnCompare.ClientID %>").hide();
                    $("#<%=uoLblCompare.ClientID %>").hide();
                    $("#<%=uoLblCompareHeader.ClientID %>").hide();
                    $("#<%=uoDropDownListBranch.ClientID %>").val($("#<%=uoDropDownListBranch.ClientID %>").val());
                    $("#<%=uoDropDownListManifest.ClientID %>").val($("#<%=uoDropDownListManifest.ClientID %>").val());
                }
                else {
                    $("#<%=uoBtnView.ClientID %>").removeAttr("disabled");
                    $("#<%=uoBtnSearch.ClientID %>").removeAttr("disabled");
                    $("#<%=uoDropDownListCompare.ClientID %>").show();
                    $("#<%=uoBtnCompare.ClientID %>").show();
                    $("#<%=uoLblCompareHeader.ClientID %>").show();
                    $("#<%=uoDropDownListBranch.ClientID %>").val($("#<%=uoDropDownListBranch.ClientID %>").val());
                    $("#<%=uoDropDownListManifest.ClientID %>").val($("#<%=uoDropDownListManifest.ClientID %>").val());
                }

            });

            $("#<%=uoDropDownListBranch.ClientID %>").change(function(ev) {
                if (($("#<%=uoDropDownListBranch.ClientID %>").val() == '0') ||
                ($("#<%=uoDropDownListManifest.ClientID %>").val() == '0')) {
                    $("#<%=uoBtnView.ClientID %>").attr("disabled", "disabled");
                    $("#<%=uoBtnSearch.ClientID %>").attr("disabled", "disabled");
                    $("#<%=uoDropDownListCompare.ClientID %>").hide();
                    $("#<%=uoBtnCompare.ClientID %>").hide();
                    $("#<%=uoLblCompare.ClientID %>").hide();
                    $("#<%=uoLblCompareHeader.ClientID %>").hide();
                    $("#<%=uoDropDownListBranch.ClientID %>").val($("#<%=uoDropDownListBranch.ClientID %>").val());
                    $("#<%=uoDropDownListManifest.ClientID %>").val($("#<%=uoDropDownListManifest.ClientID %>").val());
                }
                else {
                    $("#<%=uoBtnView.ClientID %>").removeAttr("disabled");
                    $("#<%=uoBtnSearch.ClientID %>").removeAttr("disabled");
                    $("#<%=uoBtnView.ClientID %>").disabled = false;
                    $("#<%=uoDropDownListCompare.ClientID %>").show();
                    $("#<%=uoBtnCompare.ClientID %>").show();
                    $("#<%=uoLblCompare.ClientID %>").show();
                    $("#<%=uoLblCompareHeader.ClientID %>").show();
                    $("#<%=uoDropDownListBranch.ClientID %>").val($("#<%=uoDropDownListBranch.ClientID %>").val());
                    $("#<%=uoDropDownListManifest.ClientID %>").val($("#<%=uoDropDownListManifest.ClientID %>").val());
                }

            });

            $("#<%=uoDropDownListCompare.ClientID %>").change(function(ev) {
                if ($("#<%=uoDropDownListCompare.ClientID %>").val() == '-1') {
                    $("#<%=uoBtnCompare.ClientID %>").attr("disabled", "disabled");
                    $("#<%=uoDropDownListCompare.ClientID %>").val($("#<%=uoDropDownListCompare.ClientID %>").val());
                }
                else {
                    $("#<%=uoBtnCompare.ClientID %>").removeAttr("disabled");
                    $("#<%=uoDropDownListCompare.ClientID %>").val($("#<%=uoDropDownListCompare.ClientID %>").val());
                }

            });

//            $("#<%=uoDropDownListFilter.ClientID %>").change(function(ev) {
//                $("#<%=uoTextBoxFilter.ClientID %>").val('');
//                if ($("#<%=uoDropDownListFilter.ClientID %>").val() != '0') {
//                    $("#<%=uoTextBoxFilter.ClientID %>").attr("maxlength", 8);
//                }
//                else {
//                    $("#<%=uoTextBoxFilter.ClientID %>").removeAttr("maxlength");
//                }

//            });
        }

        function SetValue() {
            $("#<%=uoHiddenBranch.ClientID %>").val($("#<%=uoDropDownListBranch.ClientID %>").val());


        }

        function SetManifestVal() {
            $("#<%=uoHiddenFieldManifest.ClientID %>").val($("#<%=uoDropDownListManifest.ClientID %>").val());
        }
    </script>
    <table width="100%">
        <tr>
            <td class="PageTitle" colspan="2">Hotel Locked Manifest</td>
        </tr>
        <tr>
            <td colspan= "2"></td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:100px;">Hotel Branch :</td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoPanelBranch" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="uoDropDownListBranch" 
                            Width="300px" AppendDataBoundItems="true" onchange="return SetValue();"
                            EnableViewState="true"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoBtnView" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">ManifestType :</td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoPanelManifestType" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="uoDropDownListManifest"
                            Width="300px" AppendDataBoundItems="true" onchange="return SetManifestVal();"></asp:DropDownList> &nbsp;
                        <asp:Button runat="server" ID="uoBtnView" Text="View" Width="100px"
                            CssClass="SmallButton" onclick="uoBtnView_Click"/>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                <asp:Label runat="server" ID="uoLblCompare" Text="Compare To :"></asp:Label>
            </td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoPanelCompareType" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="uoDropDownListCompare"
                            Width="300px" AppendDataBoundItems="true">
                            <asp:ListItem Selected="True" Text="--Select Compare Type--" Value="0"></asp:ListItem>
                        </asp:DropDownList> &nbsp;
                        <asp:Button runat="server" ID="uoBtnCompare" Text="Compare" Width="100px"
                            CssClass="SmallButton" Enabled="false" onclick="uoBtnCompare_Click" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoBtnView" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table width="100%">
        
        <tr>
            <td colspan="2" class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoPanelAdvanceSearch" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:CheckBox runat="server" ID="uoChkBoxSearch" Text="ViewAdvanceSearch" onclick="return SetVisibility();" />
                        <table runat="server" id="uoTableSearch" width="100%">
                            <tr>
                                <td class="LeftClass" style="width:100px;">
                                    Ship :
                                </td>
                                <td class="LeftClass">
                                    <asp:DropDownList runat="server" ID="uoDropDownListVessel"
                                        Width="300px" AppendDataBoundItems="true"></asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="LeftClass" style="width:100px;">
                                    Filter By :
                                </td>
                                <td class="LeftClass">
                                    <asp:DropDownList runat="server" ID="uoDropDownListFilter"
                                        Width="300px">
                                        <asp:ListItem Selected="True" Text="EMPLOYEE ID" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="SEAFARER NAME" Value="0"></asp:ListItem>                                        
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:TextBox runat="server" ID="uoTextBoxFilter" Width="200px" onkeypress="return validate(event);"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="LeftClass" style="width:100px;">
                                    Nationality :
                                </td>
                                <td class="LeftClass">
                                    <asp:DropDownList runat="server" ID="uoDropDownListNationality"
                                        Width="300px" AppendDataBoundItems="true">
                                    </asp:DropDownList>                                    
                                </td>                                
                            </tr>
                            <tr>
                                <td class="LeftClass" style="width:100px;">
                                    Gender :
                                </td>
                                <td class="LeftClass">
                                    <asp:DropDownList runat="server" ID="uoDropDownListGender"
                                        Width="300px">
                                        <asp:ListItem Selected="True" Text="--Select Gender--" Value=""></asp:ListItem>
                                         <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                        <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                    </asp:DropDownList>                                    
                                </td>                                
                            </tr>
                            <tr>
                                <td class="LeftClass" style="width:100px;">
                                    Rank :
                                </td>
                                <td class="LeftClass">
                                    <asp:DropDownList runat="server" ID="uoDropDownListRank"
                                        Width="300px" AppendDataBoundItems="true">
                                    </asp:DropDownList>                                    
                                </td>                                
                            </tr>
                            <tr>
                                <td class="LeftClass" style="width:100px;">
                                    Status :
                                </td>
                                <td class="LeftClass">
                                    <asp:DropDownList runat="server" ID="uoDropDownListStatus"
                                        Width="300px">
                                        <asp:ListItem Selected="True" Text="--Select Status--" Value=""></asp:ListItem>
                                        <asp:ListItem Text="ON" Value="ON"></asp:ListItem>
                                        <asp:ListItem Text="OFF" Value="OFF"></asp:ListItem>
                                    </asp:DropDownList>                                    
                                </td>                                
                            </tr>
                            <tr>
                                <td></td>
                                <td class="LeftClass">
                                    <asp:Button runat="server" ID="uoBtnSearch" Text="Search"
                                        CssClass="SmallButton" onclick="uoBtnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoBtnView" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td colspan="2" class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoPanelEmail" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button runat="server" ID="uoBtnSendEmail" Text="Send Email"
                            CssClass="SmallButton" onclick="uoBtnSendEmail_Click" />
                        &nbsp;
                        <asp:CheckBox runat="server" ID="uoChkBoxPDF" Text="Send as .pdf" />&nbsp;
                        <asp:CheckBox runat="server" ID="uoChkBoxSendDiff" Text="Send with the difference" /> &nbsp;
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoBtnView" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="uoBtnCompare" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="uoBtnSearch" EventName="Click" />                       
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="LeftClass">
            <td colspan="2" class="PageSubTitle">
                <asp:UpdatePanel runat="server" ID="uoPanelLockedHeader" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label runat="server" ID="uoLblManifestHeader"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoBtnView" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="LeftClass">
            <td colspan ="2">
                <asp:UpdatePanel runat="server" ID="uoPanelLocked" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ListView ID="uoListLockedManifest" runat="server">
                            <LayoutTemplate>
                                <table class="listViewTable" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>Couple</th>
                                        <th>Gender</th>
                                        <th>Nationality</th>
                                        <th>Cost Center</th>
                                        <th>Check-In</th>
                                        <th>Check-Out</th>
                                        <th>Name</th>
                                        <th>Employee ID</th>
                                        <th>Ship</th>
                                        <th>Hotel Request</th>
                                        <th>Single/Double</th>
                                        <th>Title</th>
                                        <th>Hotel City</th>
                                        <th>Hotel Nites</th>
                                        <th>From City</th>
                                        <th>To City</th>
                                        <th>A/L</th>
                                        <th>Record Locator</th>
                                        <th>Passport No.</th>
                                        <th>Issued By</th>
                                        <th>Passport Expiration</th>
                                        <th>Dept Date</th>
                                        <th>Arvl Date</th>
                                        <th>Dept City</th>
                                        <th>Arvl City</th>
                                        <th>Dept Time</th>
                                        <th>Arvl Time</th>
                                        <th>Carrier</th>
                                        <th>Flight No.</th>
                                        <th>Sign On/Off</th>
                                        <th>Voucher</th>
                                        <th>Travel Date</th>
                                        <th>Reason</th>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="leftAligned">
                                        <%# Eval("Couple")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("Gender")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("Nationality")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("CostCenter")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%--<%# Eval("CheckIn")%>--%>
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckIn"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%--<%# Eval("CheckOut")%>--%>
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOut"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "~/SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("EmployeeId") + "&recloc=" + Eval("RecordLocator") + "&st=" + Eval("Status") + "&ID=" + Eval("IdBigInt") + "&trID=" + Eval("TravelReqId ") + "&manualReqID=" + Eval("ManualReqId ") + "&dt=" + Request.QueryString["dt"]%>' runat="server"><%# Eval("Name")%></asp:HyperLink>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("EmployeeId")%>
                                    </td> 
                                    <td class="leftAligned">
                                        <%# Eval("Ship")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("HotelRequest")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("SingleDouble")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("Title")%>
                                    </td>
                                   
                                    <td class="leftAligned">
                                        <%--<%# Eval("HotelCity")%>--%>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("HotelCity") %>'></asp:Label>
                                        <%--<asp:Label ID="uoLabelWithEvent" runat="server" Text="*" Visible='<%# Eval("WithEvent") %>' ForeColor="red" Font-Bold="true" Font-Size=Large></asp:Label>--%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("HotelNights")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("FromCity")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("ToCity")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("AL")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("RecordLocator")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("PassportNo")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("IssuedBy")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("PassportExpiration")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("DeptDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:dd-MMM-yyyy}",Eval("ArvlDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("DeptCity")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("ArvlCity")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:HH:mm:ss}", Eval("DeptDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:HH:mm:ss}", Eval("ArvlDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("Carrier")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("FlightNo")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%--<%# Eval("SignOn")%>--%>
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%--<%# Eval("Voucher")%>--%>
                                        <%# String.Format("{0:0.00}", Eval("Voucher")) %>
                                    </td>
                                    <td class="leftAligned">
                                        <%--<%# Eval("TravelDate")%>--%>
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("TravelDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("Reason")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" class="listViewTable" cellpadding="0" cellspacing="0">
                                    <tr>
                                         <th>Couple</th>
                                        <th>Gender</th>
                                        <th>Nationality</th>
                                        <th>Cost Center</th>
                                        <th>Check-In</th>
                                        <th>Check-Out</th>
                                        <th>Name</th>
                                        <th>Employee ID</th>
                                        <th>Ship</th>
                                        <th>Hotel Request</th>
                                        <th>Single/Double</th>
                                        <th>Title</th>
                                        <th>Hotel City</th>
                                        <th>Hotel Nites</th>
                                        <th>From City</th>
                                        <th>To City</th>
                                        <th>A/L</th>
                                        <th>Record Locator</th>
                                        <th>Passport No.</th>
                                        <th>Issued By</th>
                                        <th>Passport Expiration</th>
                                        <th>Dept Date</th>
                                        <th>Arvl Date</th>
                                        <th>Dept City</th>
                                        <th>Arvl City</th>
                                        <th>Dept Time</th>
                                        <th>Arvl Time</th>
                                        <th>Carrier</th>
                                        <th>Flight No.</th>
                                        <th>Sign On/Off</th>
                                        <th>Voucher</th>
                                        <th>Travel Date</th>
                                        <th>Reason</th>
                                    </tr>
                                    <tr>
                                        <td colspan="33" class="leftAligned">No Record</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <asp:DataPager ID="uoListLockedManifestPager" runat="server" PagedControlID="uoListLockedManifest"
                            PageSize="25">
                            <Fields>
                                <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                            </Fields>
                        </asp:DataPager>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
                            OldValuesParameterFormatString="oldCount_{0}" 
                            SelectCountMethod="LoadLockedManifestPagingCount" 
                            SelectMethod="LoadLockedManifestPaging" TypeName="TRAVELMART.BLL.LockedManifestBLL">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="uoHiddenFieldUser" 
                                    ConvertEmptyStringToNull="False" DbType="String" Name="UserId" 
                                    PropertyName="Value" />
                                <asp:Parameter DefaultValue="1" Name="LoadType" DbType="Int16" />
                                <asp:ControlParameter ControlID="uoHiddenFieldDate" 
                                    ConvertEmptyStringToNull="False" DbType="Date" Name="Date" 
                                    PropertyName="Value" />
                                <asp:ControlParameter ControlID="uoDropDownListManifest" DbType="Int32" 
                                    DefaultValue="0" Name="ManifestType" PropertyName="SelectedValue" />
                                <asp:ControlParameter ControlID="uoDropDownListBranch" 
                                    ConvertEmptyStringToNull="False" DbType="Int32" DefaultValue="0" 
                                    Name="BranchId" PropertyName="SelectedValue" />
                                <asp:ControlParameter ControlID="uoHiddenFieldVessel" 
                                    ConvertEmptyStringToNull="False" DbType="Int32" DefaultValue="0" 
                                    Name="VesselId" PropertyName="Value" />
                                <asp:ControlParameter ControlID="uoHiddenFieldRank" DbType="String"
                                    ConvertEmptyStringToNull="False" Name="RankName" PropertyName="Value" />
                                <asp:ControlParameter ControlID="uoHiddenFieldName"  DbType="String"
                                    ConvertEmptyStringToNull="False" Name="sfName" PropertyName="Value" />
                                <asp:ControlParameter ControlID="uoHiddenFieldNationality"  DbType="String"
                                    ConvertEmptyStringToNull="False" Name="Nationality" PropertyName="Value" />
                                <asp:ControlParameter ControlID="uoHiddenFieldId" 
                                    ConvertEmptyStringToNull="False" Name="sfId" PropertyName="Value" DbType="Int64"/>   
                                <asp:ControlParameter ControlID="uoHiddenFieldGender" DbType="String"
                                    ConvertEmptyStringToNull="False" Name="Gender" PropertyName="Value" />  
                                <asp:ControlParameter ControlID="uoHiddenFieldStatus" DbType="String"
                                    ConvertEmptyStringToNull="False" Name="Status" PropertyName="Value" />       
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoBtnView" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="uoBtnSearch" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
         <tr class="LeftClass">
            <td colspan="2" class="PageSubTitle">
                <asp:UpdatePanel runat="server" ID="upPanelCompareHeader" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label runat="server" ID="uoLblCompareHeader"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoBtnCompare" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="LeftClass">
            <td colspan ="2">
                <asp:UpdatePanel runat="server" ID="uoPanelDifference" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ListView ID="uoDifferenceList" runat="server">
                            <LayoutTemplate>
                                <table class="listViewTable" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>Couple</th>
                                        <th>Gender</th>
                                        <th>Nationality</th>
                                        <th>Cost Center</th>
                                        <th>Check-In</th>
                                        <th>Check-Out</th>
                                        <th>Name</th>
                                        <th>Employee ID</th>
                                        <th>Ship</th>
                                        <th>Hotel Request</th>
                                        <th>Single/Double</th>
                                        <th>Title</th>
                                        <th>Hotel City</th>
                                        <th>Hotel Nites</th>
                                        <th>From City</th>
                                        <th>To City</th>
                                        <th>A/L</th>
                                        <th>Record Locator</th>
                                        <th>Passport No.</th>
                                        <th>Issued By</th>
                                        <th>Passport Expiration</th>
                                        <th>Dept Date</th>
                                        <th>Arvl Date</th>
                                        <th>Dept City</th>
                                        <th>Arvl City</th>
                                        <th>Dept Time</th>
                                        <th>Arvl Time</th>
                                        <th>Carrier</th>
                                        <th>Flight No.</th>
                                        <th>Sign On/Off</th>
                                        <th>Voucher</th>
                                        <th>Travel Date</th>
                                        <th>Reason</th>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr style='<%# SetValue()%>'>
                                    <td class="leftAligned">
                                        <%# Eval("Couple")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("Gender")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("Nationality")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("CostCenter")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%--<%# Eval("CheckIn")%>--%>
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckIn"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%--<%# Eval("CheckOut")%>--%>
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOut"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "~/SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("EmployeeId") + "&recloc=" + Eval("RecordLocator") + "&st=" + Eval("Status") + "&ID=" + Eval("IdBigInt") + "&trID=" + Eval("TravelReqId ") + "&manualReqID=" + Eval("ManualReqId ") + "&dt=" + Request.QueryString["dt"]%>' runat="server"><%# Eval("Name")%></asp:HyperLink>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("EmployeeId")%>
                                    </td> 
                                    <td class="leftAligned">
                                        <%# Eval("Ship")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("HotelRequest")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("SingleDouble")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("Title")%>
                                    </td>
                                   
                                    <td class="leftAligned">
                                        <%--<%# Eval("HotelCity")%>--%>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("HotelCity") %>'></asp:Label>
                                        <%--<asp:Label ID="uoLabelWithEvent" runat="server" Text="*" Visible='<%# Eval("WithEvent") %>' ForeColor="red" Font-Bold="true" Font-Size=Large></asp:Label>--%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("HotelNights")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("FromCity")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("ToCity")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("AL")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("RecordLocator")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("PassportNo")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("IssuedBy")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("PassportExpiration")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("DeptDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:dd-MMM-yyyy}",Eval("ArvlDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("DeptCity")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("ArvlCity")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:HH:mm:ss}", Eval("DeptDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:HH:mm:ss}", Eval("ArvlDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("Carrier")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("FlightNo")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%--<%# Eval("SignOn")%>--%>
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%--<%# Eval("Voucher")%>--%>
                                        <%# String.Format("{0:0.00}", Eval("Voucher")) %>
                                    </td>
                                    <td class="leftAligned">
                                        <%--<%# Eval("TravelDate")%>--%>
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("TravelDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("Reason")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" class="listViewTable" cellpadding="0" cellspacing="0">
                                    <tr>
                                         <th>Couple</th>
                                        <th>Gender</th>
                                        <th>Nationality</th>
                                        <th>Cost Center</th>
                                        <th>Check-In</th>
                                        <th>Check-Out</th>
                                        <th>Name</th>
                                        <th>Employee ID</th>
                                        <th>Ship</th>
                                        <th>Hotel Request</th>
                                        <th>Single/Double</th>
                                        <th>Title</th>
                                        <th>Hotel City</th>
                                        <th>Hotel Nites</th>
                                        <th>From City</th>
                                        <th>To City</th>
                                        <th>A/L</th>
                                        <th>Record Locator</th>
                                        <th>Passport No.</th>
                                        <th>Issued By</th>
                                        <th>Passport Expiration</th>
                                        <th>Dept Date</th>
                                        <th>Arvl Date</th>
                                        <th>Dept City</th>
                                        <th>Arvl City</th>
                                        <th>Dept Time</th>
                                        <th>Arvl Time</th>
                                        <th>Carrier</th>
                                        <th>Flight No.</th>
                                        <th>Sign On/Off</th>
                                        <th>Voucher</th>
                                        <th>Travel Date</th>
                                        <th>Reason</th>
                                    </tr>
                                    <tr>
                                        <td colspan="32" class="LeftClass">No Records.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <asp:DataPager ID="uoDifferenceListPager" runat="server" PagedControlID="uoDifferenceList"
                            PageSize="25">
                            <Fields>
                                <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                            </Fields>
                        </asp:DataPager>
                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" EnablePaging="True" 
                            OldValuesParameterFormatString="oldCount_{0}" 
                            SelectCountMethod="GetDifferenceClassCount" 
                            SelectMethod="GetDifferenceClass" TypeName="TRAVELMART.Hotel.HotelManifest2">
                           <SelectParameters>
                           </SelectParameters>
                           
                           
                        </asp:ObjectDataSource>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoBtnCompare" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="uoHiddenFieldUser" EnableViewState="true" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldDate" EnableViewState="true" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenBranch" EnableViewState="true" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldName" EnableViewState="true" Value=""/>
    <asp:HiddenField runat="server" ID="uoHiddenFieldId" EnableViewState="true" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldVessel" EnableViewState="true" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRank" EnableViewState="true" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldNationality" EnableViewState="true" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldGender" EnableViewState="true" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldStatus" EnableViewState="true" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldManifest" EnableViewState="true" Value="0" />
     
</asp:Content>
