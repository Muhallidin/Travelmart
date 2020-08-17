<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="HotelOverflowBooking2.aspx.cs" Inherits="TRAVELMART.Hotel.HotelOverflowBooking2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   
    <script type="text/javascript" language="javascript">

        function RefreshPageFromPopup() {
            if ($("#<%=uoHiddenFieldPopEditor.ClientID %>").val() == 1) {
                $("#aspnetForm").submit();
            }
    }
    
     function OpenRequestEditor(sfID, sfName,  Stripe, Status, RecLoc, TrId, mReqId) {
         var screenWidth = screen.availwidth;
         var screenHeight = screen.availheight;
         //           var sfId2 = $(
         screenWidth = 1060;
         screenHeight = 600;

         window.open('HotelEditor2.aspx?sfID=' + sfID + '&sfName=' + sfName + '&Stripe=' 
            + Stripe + '&Status=' + Status + '&RecLoc=' + RecLoc + '&trId=' + TrId + '&mReqId=' + mReqId, 'Hotel_Vehicle_Request', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
         return false;
     }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script type="text/javascript">
        
        $(document).ready(function() {
            filterSettings();
        });

        function filterSettings() {

            $("#ctl00_ContentPlaceHolder1_uoOverflowList_uoCheckBoxCheckAll").click(function(ev) {
                var status = $(this).attr('checked');
                $('input:checkbox[name *= uoSelectCheckBox]').attr('checked', status);

            });


            $("#<%=uoButtonApprove.ClientID %>").click(function(ev) {
                var listOfCheckbox = $('input:checkbox[name*=uoSelectCheckBox]:checked');

                var i = 0;
                $(listOfCheckbox).each(function() {
                    i++;
                });


                if (i == 0) {
                    alert("No selected request!");
                    return false;
                }
                else {
                    return true;
                }
            });

        }
    </script>
    <div class="PageTitle" style="width:100%;">
        Hotel Booking Exception List
    </div>
    <div class="RightClass" style="width:100%;">       
        <asp:Button runat="server" ID="uoButtonApprove" CssClass="SmallButton"
            Text="Approve" onclick="uoButtonApprove_Click" />
    </div>
    <div class="LeftClass" runat="server" id="uoDivList" >
        <asp:UpdatePanel runat="server" ID="uoPanelOverflow" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:ListView runat="server" ID="uoOverflowList">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th class="hideElement">requestInfo</th>
                            <th>E1 Travel Request Id</th>
                            <th>E1 Id</th>
                            <th>Name</th>
                            <th>Status</th>
                            <th>Gender</th>
                            <th>Nationality</th>
                            <th>Rank</th>
                            <th>Stripe</th>
                            <th>Room Type</th>
                            <th>Record Locator</th>
                            <th>On/Off Date</th>
                            <th>Port</th>
                            <th>Arvl/Dept Date</th>
                            <th>Arvl/Dept Time</th>
                            <th>Flight No.</th>
                            <th>Carrier</th>
                            <th>From City</th>
                            <th>To City</th>
                            <th>Reason Code</th>
                            <th>Exception Remarks</th>
                            <th>
                                <asp:CheckBox runat="server" ID="uoCheckBoxCheckAll"  />
                            </th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr style='<%# ValidateData()%>'>
                        <td class="hideElement">
                            <asp:HiddenField runat="server" ID="hfReqId" Value='<%# Eval("TravelReqId") %>' />
                            <asp:HiddenField runat="server" ID="hfRoomType" Value='<%# Eval("RoomTypeId") %>' />
                            <asp:HiddenField runat="server" ID="hfPortId" Value='<%# Eval("PortId") %>' />
                        </td>
                        <td class="leftAligned">
                            <%# Eval("E1TravelReqId") %>
                        </td>
                        <td class="leftAligned">
                            <asp:Label runat="server" ID="uoLblSeafarerID" Text='<%# Eval("SeafarerId")%>'></asp:Label>
                        </td>
                        <td class="leftAligned">
                            <asp:Label runat="server" ID="uoLblSeafarerName" Text='<%# Eval("SeafarerName") %>'></asp:Label>
                        </td>
                        <td class="leftAligned">
                            <asp:Label runat="server" ID="uoLblStatus" Text='<%# Eval("SFStatus") %>'></asp:Label>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("Gender") %>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("Nationality") %>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("RankName") %>
                        </td>
                        <td class="leftAligned">
                            <asp:Label runat="server" ID="uoLblStripe" Text='<%# String.Format("{0:0.00}", Eval("Stripes"))%>'></asp:Label>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("RoomType") %>
                        </td>
                        <td class="leftAligned">
                            <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator") %>'></asp:Label>
                        </td>
                        <td class="leftAligned">
                            <%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("PortName")%>
                        </td>
                        <td class="leftAligned">
                            <%# String.Format("{0:dd-MMM-yyyy}", Eval("ArrivalDepartureDatetime"))%>
                        </td>
                        <td class="leftAligned">
                            <%# String.Format("{0:HH:mm:ss}", Eval("ArrivalDepartureDatetime"))%>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("FlightNo")%>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("Carrier")%>
                        </td>               
                        <td class="leftAligned">
                            <%# Eval("FromCity")%>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("ToCity")%>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("ReasonCode")%>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("ExceptionRemarks")%>
                        </td>
                        <td>
                            <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" runat="server"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th class="hideElement">requestInfo</th>
                            <th>E1 Travel Request Id</th>
                            <th>E1 Id</th>
                            <th>Name</th>
                            <th>Status</th>
                            <th>Gender</th>
                            <th>Nationality</th>
                            <th>Rank</th>
                            <th>Stripe</th>
                            <th>Room Type</th>
                            <th>Record Locator</th>
                            <th>On/Off Date</th>
                            <th>Port</th>
                            <th>Arvl/Dept Date</th>
                            <th>Arvl/Dept Time</th>
                            <th>Flight No.</th>
                            <th>Carrier</th>
                            <th>From City</th>
                            <th>To City</th>
                            <th>Reason Code</th>
                        </tr>
                        <tr>
                            <td colspan="20" class="LeftClass">No Records</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <asp:DataPager runat="server" ID="uoOverflowListPager" PagedControlID="uoOverflowList"
                PageSize="20">
                <Fields>
                    <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                </Fields>
            </asp:DataPager>  
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
                    MaximumRowsParameterName="MaximumRows" 
                    OldValuesParameterFormatString="oldCount_{0}" 
                   StartRowIndexParameterName="startRowIndex"
                   SelectCountMethod="LoadOverflowBookingTableCount"
                   SelectMethod="LoadOverflowBookingTable"
                   TypeName="TRAVELMART.BLL.OverFlowBookingBLL">
                 <SelectParameters>
                    <asp:ControlParameter ControlID="uoHiddenFieldStartDate" 
                        ConvertEmptyStringToNull="False" DbType="DateTime" Name="Date" 
                        PropertyName="Value" />
                    <asp:ControlParameter ControlID="uoHiddenFieldUser" 
                        ConvertEmptyStringToNull="False" DbType="String" Name="UserId" 
                        PropertyName="Value" />
                    <asp:Parameter DefaultValue="1" Name="Loadtype" DbType="Int16" />
                     <asp:ControlParameter ControlID="uoHiddenFieldFilter" 
                        ConvertEmptyStringToNull="False" DbType="Int16" Name="FilterBy" 
                        PropertyName="Value" />
                    
                </SelectParameters>
            </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>
    <asp:HiddenField runat="server" ID="uoHiddenFieldUser" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldStartDate" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldFilter" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldPopupHotel" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldSFId"  />
     <asp:HiddenField runat="server" ID="uoHiddenFieldPopEditor" Value="0" />
</asp:Content>
