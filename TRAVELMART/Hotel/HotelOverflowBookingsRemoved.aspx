<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="HotelOverflowBookingsRemoved.aspx.cs" Inherits="TRAVELMART.HotelOverflowBookingsRemoved" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
       
        $(document).ready(function() {
            SetExceptionResolution();
            settings();
        });

        function SetExceptionResolution() {
            var ht = $(window).height(); // * 0.54; //550;
            var wd = $(window).width();
                      
            if (screen.height <= 600) {
                ht = ht * 0.30;                              
            }
            else if (screen.height <= 720) {
                ht = ht * 0.45;                                
            }
            else {
                ht = ht * 0.75;                               
            }

            $("#Bv").height(ht);

            $("#Av").width(wd);
            $("#Bv").width(wd);


        }
        function settings() {
            $("#<%=uoButtonReturn.ClientID %>").click(function() {
                var atLeastOneIsChecked = $('input:checkbox[name *= uoSelectCheckBox]:checked').length > 0;
                if (atLeastOneIsChecked == true) {
                    return true;
                }
                else {
                    alert("No selected record to return!");
                    return false;
                }
            });
        }
    
        function SetSettings(chk) {
            var status = chk.checked;
            $('input:checkbox[name *= uoSelectCheckBox]').attr('checked', status);
        }
        
        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;

        }
    </script>

    <div class="PageTitle">
             Removed from Overflow List
    </div>
    <hr/> 
    <div>
        <asp:Button ID="uoButtonReturn" runat="server" Text="Return to Overflow List" 
            CssClass="SmallButton" onclick="uoButtonReturn_Click" />
    </div>
    <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;
        position: relative;">
        <asp:ListView runat="server" ID="ListView1">
            <LayoutTemplate>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <th class="hideElement">
                            requestInfo
                        </th>
                        <th>
                            <asp:CheckBox runat="server" ID="CheckBox1" Width="20px" CssClass="Checkbox" onclick="SetSettings(this);" />
                        </th> 
                        <th class="MouseTool" style="text-align: center;">
                            <asp:Label runat="server" ID="Label13" Text="Comments" Width="150px"> </asp:Label>
                        </th>
                         <th class="MouseTool" style="text-align: center;">
                            <asp:Label runat="server" ID="Label17" Text="Removed By" Width="100px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablE1Hdr" Text="E1 TRId" Width="35px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablRecLocHdr" Text="Record Locator" Width="60px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablEmpIdHdr" Text="Employee Id" Width="60px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablNameHdr" Text="Name" Width="200px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablStatHdr" Text="Status" Width="50px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablOnOffHdr" Text="On/Off Date" Width="70px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablGenderHdr" Text="Gender" Width="50px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablRankHdr" Text="Rank" Width="120px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablStripeHdr" Text="Stripe" Width="35px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablRoomHdr" Text="Room Type" Width="50px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablNatHdr" Text="Nationality" Width="120px"> </asp:Label>
                        </th>
                        <th class="MouseTool" style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablPort" Text="Port" ToolTip="Port Code based on Crew History"
                                Width="150px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLabelVessel" Text="Ship" Width="150px"> </asp:Label>
                        </th>
                        <th class="MouseTool" style="text-align: center;">
                            <asp:Label runat="server" ID="Label1" Text="Arvl/Dept Date" ToolTip="Arrival/Departure date based on PNR."
                                Width="70px"> </asp:Label>
                        </th>
                        <th class="MouseTool" style="text-align: center;">
                            <asp:Label runat="server" ID="Label2" Text="Arvl/Dept Time" ToolTip="Arrival/Departure time based on PNR."
                                Width="50px"> </asp:Label>
                        </th>
                        <th class="MouseTool" style="text-align: center;">
                            <asp:Label runat="server" ID="Label3" Text="Carrier" ToolTip="Airline Carrier based on PNR."
                                Width="45px"> </asp:Label>
                        </th>
                        <th class="MouseTool" style="text-align: center;">
                            <asp:Label runat="server" ID="Label4" Text="Flight No." ToolTip="Flight No. based on PNR."
                                Width="45px"> </asp:Label>
                        </th>
                        <th class="MouseTool" style="text-align: center;">
                            <asp:Label runat="server" ID="Label5" Text="From City" ToolTip="Airport Code Equivalent of Origin."
                                Width="45px"> </asp:Label>
                        </th>
                        <th class="MouseTool" style="text-align: center;">
                            <asp:Label runat="server" ID="Label6" Text="To City" ToolTip="Airport Code Equivalent of Destination."
                                Width="45px"> </asp:Label>
                        </th>
                        <th class="MouseTool" style="text-align: center;">
                            <asp:Label runat="server" ID="Label7" Text="Reason Code" ToolTip="Reason Code based on Crew History."
                                Width="60px"> </asp:Label>
                        </th>                       
                        <th class="MouseTool" style="text-align: center;">
                            <asp:Label runat="server" ID="Label15" Text=" Booking Remarks" Width="150px"> </asp:Label>
                        </th>
                        <th>
                            <asp:Label runat="server" ID="Label11" Text="" Width="20px"> </asp:Label>
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;
        position: relative;" onscroll="divScrollL();">
        <asp:ListView runat="server" ID="uoListViewOverflow">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList" width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <%# OverflowChangeRowColor()%>
                <td class="hideElement">                                
                    <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IdBigint") %>' />
                    <asp:HiddenField runat="server" ID="hfSeqNo" Value='<%# Eval("SeqNo") %>' />
                    
                    <asp:HiddenField runat="server" ID="hfReqId" Value='<%# Eval("TravelReqId") %>' />
                    <asp:HiddenField runat="server" ID="hfRoomType" Value='<%# Eval("RoomTypeId") %>' />
                    <asp:HiddenField runat="server" ID="hfPortId" Value='<%# Eval("PortId") %>' />
                    <asp:HiddenField runat="server" ID="hfExID" Value='<%# Eval("HotelOverflowID") %>' />
                </td>
                <td style="white-space: normal;">
                    <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" Width="24px" runat="server" />
                </td>
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label14" Text='<%# Eval("Comments") %>' Width="150px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label16" Text='<%# Eval("RemovedBy") %>' Width="100px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblE1TrId" Text='<%# Eval("E1TravelReqId") %>' Width="35px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator") %>' Width="60px"></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblSeafarerID" Width="60px" Text='<%# Eval("SeafarerId")%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblSeafarerName" Width="200px" Text='<%# Eval("SeafarerName") %>'></asp:Label>
                    <%--<asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "../SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("SeafarerId") + "&recloc=&st=" + Eval("SFStatus") + "&ID=0&trID="+ Eval("TravelReqId") +"&manualReqID=0" + "&dt=" + Request.QueryString["dt"]%>'
                        runat="server" Width="200px" Text='<%# Eval("SeafarerName")%>'></asp:HyperLink>--%>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblStatus" Width="50px" Text='<%# Eval("SFStatus") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblOnOff" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>'
                        Width="70px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLablGenderHdr" Text=' <%# Eval("Gender") %>' Width="50px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label8" Text='<%# Eval("RankName") %>' Width="120px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblStripe" Width="35px" Text='<%# String.Format("{0:0.00}", Eval("Stripes"))%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLablRoomHdr" Text='<%# Eval("RoomType") %>' Width="50px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLablNatHdr" Text='<%# Eval("Nationality") %>' Width="120px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLablPort" Text='<%# Eval("PortName")%>' Width="150px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label12" Text='<%# Eval("VesselName")%>' Width="150px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label1" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("ArrivalDepartureDatetime"))%>'
                        Width="70px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label2" Text='<%# String.Format("{0:HH:mm:ss}", Eval("ArrivalDepartureDatetime"))%>'
                        Width="50px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCarrier" Text=' <%# Eval("Carrier")%> ' Width="45px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblFlightNo" Text='<%# Eval("FlightNo")%>' Width="45px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label5" Text=' <%# Eval("FromCity")%>' Width="45px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label6" Text='<%# Eval("ToCity")%>' Width="45px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblReason" Text='<%# Eval("ReasonCode")%>' Width="60px"> </asp:Label>
                </td>                
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblBookingRemarks" Text=' <%# Eval("BookingRemarks") %>'
                        Width="150px"> </asp:Label>
                </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                    <tr>
                        <td colspan="20" class="leftAligned">
                            <asp:Label runat="server" ID="Label10" Text="No Record" > </asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
