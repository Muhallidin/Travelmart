<%@ Page Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true" CodeBehind="PortNotExist.aspx.cs" Inherits="TRAVELMART.SystemAnalyst.PortNotExist" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="PageTitle">
            <td align="left">
               Port Not Exist
            </td>                        
            <td align="right">                
            Port: &nbsp;&nbsp;
            <asp:DropDownList ID="uoDropDownListPort" runat="server" Width="300px" AutoPostBack="true"
                AppendDataBoundItems="true" OnSelectedIndexChanged="uoDropDownListPort_SelectedIndexChanged">
            </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
 <script type="text/javascript" language="javascript">
     $(document).ready(function() {
         SetTMResolution();
     });
     function pageLoad(sender, args) {
         var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
         if (isAsyncPostback) {
             SetTMResolution();             
         }
     }
     function SetTMResolution() {
         var ht = $(window).height();
         var ht2 = $(window).height();

         var wd = $(window).width() * 0.90;
         var screenH = screen.height;
         var percent = 0.60;

         if (screen.height <= 600) {
             ht = ht * 0.44;
         }
         else if (screen.height <= 720) {
             ht = ht * 0.55;
         }
         else {
             if (screenH = 768) {
                 percent = (530 - (screenH - ht)) / ht;
             }
             ht = ht * percent;
         }

         $("#Av").width(wd);
         $("#Bv").height(ht);         
         $("#Bv").width(wd);

     }

     function divScrollL() {
         var Right = document.getElementById('Av');
         var Left = document.getElementById('Bv');
         Right.scrollLeft = Left.scrollLeft;
     }
 </script>
 
   <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">   
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
                        
                        <th class="hideElement">
                            <asp:CheckBox runat="server" ID="CheckBox1" Width="19px" CssClass="Checkbox" onclick="SetCheckBoxSettings(this);" />
                        </th>
                        
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableHotelCity" Text="Hotel City" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableHotelCity" runat="server" CommandName="SortByHotelName" Text="Hotel City" Width="53px"></asp:LinkButton>
                        </th>
                        
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableCheckin" Text="Checkin" Width="70px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableCheckin" runat="server" CommandName="SortByCheckin" Text="Checkin" Width="63px"></asp:LinkButton>
                        </th>
                         
                        <th style="text-align: center; ">
                            <%--<asp:Label runat="server" ID="uoLableCheckOut" Text="CheckOut" Width="70px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableCheckOut" runat="server" CommandName="SortByCheckOut" Text="CheckOut" Width="65px"></asp:LinkButton>
                        </th>
                        <%--<th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLableHotelNite" Text="Hotel Nite" Width="35px"> </asp:Label>
                        </th>--%>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableEmployee" Text="E1 ID" Width="55px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableEmployee" runat="server" CommandName="SortByE1ID" Text="E1 ID" Width="48px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableName" Text="Name" Width="200px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableName" runat="server" CommandName="SortByLName" Text="Last Name" Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableName" Text="Name" Width="200px"> </asp:Label>--%>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByFName" Text="First Name" Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableGender" Text="Gender" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableGender" runat="server" CommandName="SortByGender" Text="Gender" Width="44px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableSingleDouble" Text="Single Double" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableSingleDouble" runat="server" CommandName="SortBySingleDouble" Text="Single Double" Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableCouple" Text="Couple" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableCouple" runat="server" CommandName="SortByCouple" Text="Couple" Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableTitle" Text="Title" Width="150px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableTitle" runat="server" CommandName="SortByTitle" Text="Title" Width="195px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="Label2" Text="Stripe" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableStripe" runat="server" CommandName="SortByStripe" Text="Stripe" Width="43px"></asp:LinkButton>
                        </th>
                         <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="Label2" Text="Stripe" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByPort" Text="Seaport" Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableShip" Text="Ship" Width="150px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableShip" runat="server" CommandName="SortByShip" Text="Ship" Width="195px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableCostCenter" Text="Cost Center" Width="100px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableCostCenter" runat="server" CommandName="SortByCostCenter" Text="Cost Center" Width="195px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableNationality" Text="Nationality" Width="70px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableNationality" runat="server" CommandName="SortByNationality" Text="Nationality" Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableHotelRequest" Text="Hotel Request" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableHotelRequest" runat="server" CommandName="SortByHotelRequest" Text="Hotel Request" Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableRecLoc" Text="RecLoc" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableRecLoc" runat="server" CommandName="SortByRecLoc" Text="RecLoc" Width="54px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableRecLocID" Text="RecLocID" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableRecLocID" runat="server" CommandName="SortByRecLocID" Text="RecLocID" Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableAirSequence" Text="Air Sequence" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableAirSequence" runat="server" CommandName="SortByAirSequence" Text="Air Sequence" Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableDepartureCity" Text="Departure City" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableDepartureCity" runat="server" CommandName="SortByDepartureCity" Text="Departure City" Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableArrivalCity" Text="Arrival City" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableArrivalCity" runat="server" CommandName="SortByArrivalCity" Text="Arrival City" Width="43px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableArrivalDate" Text="Arrival Date" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableArrivalDate" runat="server" CommandName="SortByArrivalDate" Text="Arrival Date" Width="100px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableArrivalTime" Text="Arrival Time" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableArrivalTime" runat="server" CommandName="SortByArrivalTime" Text="Arrival Time" Width="54px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableCarrier" Text="Carrier" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableCarrier" runat="server" CommandName="SortByCarrier" Text="Carrier" Width="44px"></asp:LinkButton>
                            
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableFlightNo" Text="Flight No" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableFlightNo" runat="server" CommandName="SortByFlightNo" Text="Flight No" Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableVoucher" Text="Voucher" Width="50px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableVoucher" runat="server" CommandName="SortByVoucher" Text="Voucher" Width="45px"></asp:LinkButton>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLablePassportNo" Text="Passport No" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLablePassportNo" runat="server" CommandName="SortByPassportNo" Text="Passport No" Width="53px"/>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLablePassportExp" Text="Passport Exp" Width="80px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLablePassportExp" runat="server" CommandName="SortByPassportExp" Text="Passport Exp" Width="74px"/>
                            
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLablePassportIssued" Text="Passport Issued" Width="80px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLablePassportIssued" runat="server" CommandName="SortByPassportIssued" Text="Passport Issued" Width="73px"/>
                        </th>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableHotelBranch" Text="Hotel Branch" Width="60px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableHotelBranch" runat="server" CommandName="SortByHotelBranch" Text="Exception Remarks" Width="198px"/>
                        </th>
                      <%--  <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLableBooking" Text="Booking" Width="200px"> </asp:Label>
                        </th>--%>
                        <th style="text-align: center;">
                            <%--<asp:Label runat="server" ID="uoLableBookingRemark" Text="Booking Remark" Width="150px"> </asp:Label>--%>
                            <asp:LinkButton ID="uoLableBookingRemark" runat="server" CommandName="SortByBookingRemark" Text="Booking Remarks" Width="143px"/>
                        </th>
                         
                    </tr>
                    
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    
     <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
            onscroll="divScrollL();">
            
        <asp:ListView runat="server" ID="uoListViewNonTurnPort">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList" width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate> 
                 <%# DashboardChangeRowColor()%>
                                             
                 <td style="white-space: normal;" class="hideElement">
                     <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" Width="24px" runat="server" Enabled='<%# Eval("IsVisible") %>'/>
                </td>
                
                <td class="hideElement">                                
                    <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IDBigInt") %>' />
                    <asp:HiddenField runat="server" ID="hfSeqNo" Value='<%# Eval("AirSequence") %>' />
                    <asp:HiddenField runat="server" ID="hfReqId" Value='<%# Eval("TravelReqId") %>' />
                    <asp:HiddenField runat="server" ID="hfRoomType" Value='<%# Eval("RoomTypeId") %>' />
                    <asp:HiddenField runat="server" ID="hfPortId" Value='<%# Eval("PortId") %>' />
                    <asp:HiddenField runat="server" ID="uoLblStatus" Value='<%# Eval("SFStatus") %>' />
                    <asp:HiddenField runat="server" ID="uoLblHotelNite" Value='<%# Eval("HotelNite") %>' />
                    
                </td>
                
                <td class="leftAligned"  style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblHotelCity" Text='<%# Eval("HotelCity") %>' Width="60px" Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCheckin" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("Checkin"))%>' Width="70px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned">
                    <asp:Label runat="server" ID="uoLblCheckOut" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOut"))%>' Width="70px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
               <%-- <td class="leftAligned" style="white-space: normal;">
                    
                    <asp:Label runat="server" ID="uoLblHotelNite" Text='<%# Eval("HotelNite")%>' Width="35px"/> 
                </td>--%>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblSeafarerID" Text='<%# Eval("Employee")%>' Width="55px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "../SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("Employee") + "&recloc=&st=" + Eval("SFStatus") + "&ID=0&trID="+ Eval("TravelReqId") +"&manualReqID=0" + "&dt=" + Request.QueryString["dt"]%>'
                    runat="server" Width="105px" Text='<%# Eval("LastName")%>'  Visible='<%# Eval("IsVisible") %>'></asp:HyperLink>
                </td>
               <td class="leftAligned" style="white-space: normal;">
                     <asp:Label runat="server" ID="Label2" Text='<%# Eval("FirstName")%>' Width="105px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                     <asp:Label runat="server" ID="uoLblGender" Text='<%# Eval("Gender")%>' Width="50px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label1"  Text='<%# String.Format("{0:0.00}", Eval("SingleDouble"))%>' Width="50px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblCouple" Text='<%# Eval("Couple")%>' Width="48px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    
                    <asp:Label runat="server" ID="uoLblTitle" Text='<%# Eval("Title")%>' Width="200px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblStripe"  Text='<%# String.Format("{0:0.00}", Eval("Stripes"))%>' Width="50px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="Label3" Text='<%# Eval("PortName")%>' Width="50px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblShip" Text='<%# Eval("Ship")%>' Width="200px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblCostcenter" Text='<%# Eval("Costcenter")%>' Width="200px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblNatioality" Text='<%# Eval("Natioality")%>' Width="104px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblHotelRequest" Text='<%# Eval("HotelRequest")%>' Width="50px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblRecLoc" Text='<%# Eval("RecLoc")%>' Width="60px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblRecLocID" Text='<%# Eval("RecLocID")%>' Width="50px"  Visible='<%# Eval("IsVisible") %>'/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblAirSequence" Text='<%# Eval("AirSequence")%>' Width="50px"/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLbldeptCity" Text='<%# Eval("deptCity")%>' Width="50px"/> 
                </td>
                <td class="leftAligned"  style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblArvlCity" Text='<%# Eval("ArvlCity")%>' Width="50px"/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <%--<asp:Label runat="server"  ID="uoLblArvldate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("Arvldate"))%>' Width="60px"/> --%>
                    <asp:Label runat="server"  ID="uoLblArvldate" Text='<%# Eval("Arvldate")%>' Width="105px"/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblArvlTime" Text='<%#  Eval("ArvlTime")%>' Width="60px"/> 
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="50px"/>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblFlightNo" Text='<%# Eval("FlightNo")%>' Width="50px"/>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblVoucher" Text='<%# Eval("Voucher")%>' Width="50px"  Visible='<%# Eval("IsVisible") %>'/>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblPassportNo" Text='<%# Eval("PassportNo")%>' Width="60px"  Visible='<%# Eval("IsVisible") %>'/>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblPassportExp" Text='<%# Eval("PassportExp")%>' Width="80px"  Visible='<%# Eval("IsVisible") %>'/>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblPassportIssued" Text='<%# Eval("PassportIssued")%>' Width="80px"  Visible='<%# Eval("IsVisible") %>'/>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblHotelBranch" Text='<%# Eval("Booking")%>' Width="205px"  Visible='<%# Eval("IsVisible") %>'/>
                </td>
              <%--  <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblBooking" Text='<%# Eval("Booking")%>' Width="200px"/>
                </td>--%>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="uoLblBookingremark" Text='<%# Eval("BookingRemark")%>' Width="150px" Visible='<%# Eval("IsVisible") %>' />
                </td>
          <%--   <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server"  ID="Label1" Text='<%# Eval("IsVisible")%>' Width="150px"/>
                </td>--%>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <td colspan="20" class="leftAligned">
                            <asp:Label runat="server" ID="Label10" Text="No Record" > </asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>       
    </div>
    <asp:HiddenField runat="server" ID="uoHiddenFieldDate" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldUser" EnableViewState="true" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldSortBy" EnableViewState="true" Value="" />

</asp:Content>
