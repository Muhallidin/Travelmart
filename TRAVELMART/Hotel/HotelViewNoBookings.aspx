<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true"
    CodeBehind="HotelViewNoBookings.aspx.cs" Inherits="TRAVELMART.Hotel.HotelViewNoBookings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript">
        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance();
            if (isAsyncPostback) {
                filterSettings();
            }
        }

        $(document).ready(function() {
            filterSettings();
        });

        function filterSettings() {
            var role = $("#<%=uoHiddenFieldRole.ClientID %>").val();
            $("#ctl00_ContentPlaceHolder1_uoSeafarerList_uoCheckBoxCheckAll").click(function(ev) {
                var status = $(this).attr('checked');
                $('input:checkbox[name] *= uoSelectCheckBox').attr('checked', status);
            });

            if ($("#<%=uoCheckBoxAdvancedSearch.ClientID %>").attr('checked')) {
                $("#<%=uoTableAdvancedSearch.ClientID %>").show();
            }
            else {
                $("#<%=uoTableAdvancedSearch.ClientID %>").hide();
            }

            $("#<%=uoCheckBoxAdvancedSearch.ClientID %>").click(function() {
                if ($(this).attr('checked')) {
                    $("#<%=uoTableAdvancedSearch.ClientID %>").fadeIn();
                }
                else {
                    $("#<%=uoTableAdvancedSearch.ClientID %>").fadeOut();
                }
            });
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function SetTRResolution() {
            var ht = $(window).height();

            if (screen.height <= 600) {
                ht = ht * 0.20;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.39;
            }
            else {
                ht = ht * 0.44;
            }

            $("#Bv").height(ht);

        }

        $(document).ready(function() {
            SetTRResolution();

        });

        function pageLoad(sender, args) {

            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
            }
        }
        
    </script>

    <div class="PageTitle">
        New Hotel Bookings</div>
    <br />
    <div class="LeftClass" id="Bv" style="overflow: auto; width: 100%; overflow-x: auto;
        overflow-y: auto; position: relative; width:100%;">
        <asp:UpdatePanel runat="server" ID="uoPanelHotel">
            <ContentTemplate>
                <asp:ListView runat="server" ID="uoHotelList">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th class="hideElement">
                                    Req ID
                                </th>
                                <th>
                                    Couple
                                </th>
                                <th>
                                    Gender
                                </th>
                                <th>
                                    Nationality
                                </th>
                                <th>
                                    Cost Center
                                </th>
                                <th>
                                    Check-In
                                </th>
                                <th>
                                    Check-Out
                                </th>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Employee ID
                                </th>
                                <th>
                                    Ship
                                </th>
                                <th>
                                    Hotel Request
                                </th>
                                <th>
                                    Single/Double
                                </th>
                                <th>
                                    Title
                                </th>
                                <th>
                                    Hotel City
                                </th>
                                <th>
                                    Hotel Nites
                                </th>
                                <th>
                                    From City
                                </th>
                                <th>
                                    To City
                                </th>
                                <th>
                                    Record Locator
                                </th>
                                <th>
                                    Carrier
                                </th>
                                <th>
                                    Dept Date
                                </th>
                                <th>
                                    Arvl Date
                                </th>
                                <th>
                                    Dept Time
                                </th>
                                <th>
                                    Arvl Time
                                </th>
                                <th>
                                    Flight No.
                                </th>
                                <th>
                                    Sign ON/OFF
                                </th>
                                <th>
                                    Voucher
                                </th>
                                <th>
                                    Travel Date
                                </th>
                                <th>
                                    Reason
                                </th>
                                <th>
                                    Stripe
                                </th>
                                <th>
                                    <asp:CheckBox runat="server" ID="uoCheckBoxCheckAll" />
                                </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="hideElement">
                                <asp:HiddenField runat="server" ID="uoHiddenFieldColIdBigInt" Value='<%# Eval("HotelPendingID") %>' />
                                <asp:HiddenField runat="server" ID="hfVendor" Value='<%# Eval("VendorId") %>' />
                                <asp:HiddenField runat="server" ID="hfBranch" Value='<%# Eval("BranchId") %>' />
                                <asp:HiddenField runat="server" ID="hfRoom" Value='<%# Eval("RoomTypeId") %>' />
                                <asp:HiddenField runat="server" ID="hfCountry" Value='<%# Eval("CountryID") %>' />
                                <asp:HiddenField runat="server" ID="hfCity" Value='<%# Eval("CityId") %>' />
                                <asp:HiddenField runat="server" ID="hfTRID" Value='<%# Eval("TravelRequestId") %>' />
                                <asp:HiddenField runat="server" ID="hfMRID" Value='<%# Eval("ManualRequestId") %>' />
                                <asp:HiddenField runat="server" ID="hfPort" Value='<%# Eval("PortId") %>' />
                                <asp:HiddenField runat="server" ID="hfVessel" Value='<%# Eval("VesselId") %>' />
                                <asp:HiddenField runat="server" ID="hfContractID" Value='<%# Eval("ContractId") %>' />
                                <asp:HiddenField runat="server" ID="hfDatasource" Value='<%# Eval("DataSource") %>' />
                                <asp:HiddenField runat="server" ID="hfHotelStatus" Value='<%# Eval("HotelStatus") %>' />
                                <asp:HiddenField runat="server" ID="hfSFStatus" Value='<%# Eval("Status") %>' />
                                <asp:HiddenField runat="server" ID="hfHotelName" Value='<%# Eval("HotelName") %>' />
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Couple") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Gender") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Nationality")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("CostCenter")%>
                            </td>
                            <td class="leftAligned">
                                <asp:Label ID="uoLblCheckIn" runat="server" Text='<%# String.Format("{0:dd-MMM-yyyy HH:mm:ss}", Eval("CheckInDate")) %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:Label ID="uoLblCheckOut" runat="server" Text='<%# String.Format("{0:dd-MMM-yyyy HH:mm:ss}", Eval("CheckOutDate")) %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:LinkButton runat="server" ID="uoLinkSeafarer" PostBackUrl='<%# "~/SuperUserView.aspx?sfId=" + Eval("E1ID") +  "&trID=" + Eval("TravelRequestId") + "&st=" + Eval("Status") + "&ufn=" + Request.QueryString["ufn"] + "&manualReqID=" + Eval("ManualRequestID")  + "&dt=" + Request.QueryString["dt"]%>'
                                    Text='<%# Eval("Name") %>'></asp:LinkButton>
                            </td>
                            <td class="leftAligned">
                                <asp:Label ID="uoLblE1ID" runat="server" Text='<%# Eval("E1ID") %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Vessel")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("HotelRequest")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("RoomName")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Rank")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("HotelCity")%>
                            </td>
                            <td class="leftAligned">
                                <asp:Label ID="uoLblDuration" runat="server" Text='<%# Eval("HotelNites") %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("FromCity")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("ToCity")%>
                            </td>
                            <td class="leftAligned">
                                <asp:Label ID="uoLblRecLoc" runat="server" Text='<%# Eval("RecordLocator") %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Carrier")%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("DeptDate"))%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("ArvlDate"))%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:HH:MM:SS}", Eval("DeptTime"))%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:HH:MM:SS}", Eval("ArvlTime"))%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("FlightNo")%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>
                            </td>
                            <td class="leftAligned">
                                <asp:Label runat="server" ID="uoLblVoucher" Text='<%# Eval("Voucher")%>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("TravelDate")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("ReasonCode")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Stripe") %>
                            </td>
                            <td>
                                <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th class="hideElement">
                                    Req ID
                                </th>
                                <th>
                                    Couple
                                </th>
                                <th>
                                    Gender
                                </th>
                                <th>
                                    Nationality
                                </th>
                                <th>
                                    Cost Center
                                </th>
                                <th>
                                    Check-In
                                </th>
                                <th>
                                    Check-Out
                                </th>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Employee ID
                                </th>
                                <th>
                                    Ship
                                </th>
                                <th>
                                    Hotel Request
                                </th>
                                <th>
                                    Single/Double
                                </th>
                                <th>
                                    Title
                                </th>
                                <th>
                                    Hotel City
                                </th>
                                <th>
                                    Hotel Nites
                                </th>
                                <th>
                                    From City
                                </th>
                                <th>
                                    To City
                                </th>
                                <th>
                                    Record Locator
                                </th>
                                <th>
                                    Carrier
                                </th>
                                <th>
                                    Dept Date
                                </th>
                                <th>
                                    Arvl Date
                                </th>
                                <th>
                                    Dept Time
                                </th>
                                <th>
                                    Arvl Time
                                </th>
                                <th>
                                    Flight No.
                                </th>
                                <th>
                                    Sign ON/OFF
                                </th>
                                <th>
                                    Voucher
                                </th>
                                <th>
                                    Travel Date
                                </th>
                                <th>
                                    Reason
                                </th>
                                <th>
                                    Stripe
                                </th>
                            </tr>
                            <tr>
                                <td colspan="11" class="leftAligned">
                                    No Pending Hotel Bookings.
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager runat="server" ID="uoHotelListPager" PagedControlID="uoHotelList"
                    PageSize="10" OnPreRender="uoHotelListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
                <asp:HiddenField runat="server" ID="uoHiddenFieldRole" />
                <asp:HiddenField runat="server" ID="uoHiddenFieldUser" />
                <asp:HiddenField runat="server" ID="uoHiddenFieldStartDate" />
                <asp:HiddenField runat="server" ID="uoHiddenFieldEndDate" />
                <asp:HiddenField runat="server" ID="uoHiddenFieldSeafarer" Value="" />
                <asp:HiddenField runat="server" ID="uoHiddenFieldSeafarerId" Value="" />
                <asp:HiddenField runat="server" ID="uoHiddenFieldHotelId" Value="0" />
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" OldValuesParameterFormatString="oldCount_{0}"
                    TypeName="TRAVELMART.BLL.AutomaticBookingBLL" SelectCountMethod="SelectAutomaticBookingCount"
                    SelectMethod="SelectAutomaticBooking">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="uoHiddenFieldStartDate" ConvertEmptyStringToNull="False"
                            DbType="Date" Name="StartDate" PropertyName="Value" />
                        <asp:ControlParameter ControlID="uoHiddenFieldEndDate" ConvertEmptyStringToNull="False"
                            DbType="Date" Name="EndDate" PropertyName="Value" />
                        <asp:ControlParameter ControlID="uoHiddenFieldUser" ConvertEmptyStringToNull="False"
                            DbType="String" Name="userId" PropertyName="Value" />
                        <asp:ControlParameter ControlID="uoHiddenFieldSeafarer" ConvertEmptyStringToNull="False"
                            DbType="String" Name="sfName" PropertyName="Value" />
                        <asp:ControlParameter ControlID="uoHiddenFieldSeafarerId" ConvertEmptyStringToNull="False"
                            DbType="String" Name="e1Id" PropertyName="Value" />
                        <asp:ControlParameter ControlID="uoHiddenFieldHotelId" ConvertEmptyStringToNull="False"
                            DbType="Int32" Name="HotelId" PropertyName="Value" DefaultValue="0" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
