<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    CodeBehind="HotelRequestView.aspx.cs" Inherits="TRAVELMART.HotelRequestView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <div class="PageTitle">
        Hotel Request View</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetTRResolution();
            ShowPopup();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
                ShowPopup();
            }
        }
        function SetTRResolution() {
            var ht = $(window).height(); //550;
            var wd = $(window).width() * 0.88;
            if (screen.height <= 600) {
                ht = ht * 0.35;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.49;
            }
            else {
                ht = ht * 0.55;
            }

            $("#Bv").height(ht);
            $("#Bv").width(wd);
            $("#Av").width(wd);
            $("#Bv1").height(ht);
            $("#Bv1").width(wd);
            $("#Av1").width(wd);

        }

        function ShowPopup() {
            $(".ViewRequest").fancybox(
            {
                'centerOnScroll': false,
                'width': '100%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupHotel.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });
        }

        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;

        }
        function divScrollL1() {
            var Right = document.getElementById('Av1');
            var Left = document.getElementById('Bv1');
            Right.scrollLeft = Left.scrollLeft;

        }  
    </script>

    <%--Pending Request--%>
    <div id="Av" style="overflow-x: hidden; width: 100%; overflow-y: hidden;">
        <asp:ListView runat="server" ID="uoListViewHead" OnItemCommand="uoListViewHead_ItemCommand">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="listViewTable" width="100%">
                    <tr>
                       <%-- <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton1" runat="server" Width="100px" CommandName="RequestNo">Request No.</asp:LinkButton>
                        </th>--%>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton2" runat="server" Width="144px" CommandName="Lastname">Last Name</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton3" runat="server" Width="144px" CommandName="Firstname">First Name</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton4" runat="server" Width="194px" CommandName="Rank">Position</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton7" runat="server" Width="55px" CommandName="Gender">Gender</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton10" runat="server" Width="194px" CommandName="Hotel">Hotel</asp:LinkButton>
                        </th>
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton1" runat="server" Width="194px" CommandName="Transportation">Transportation</asp:LinkButton>
                        </th>
                        
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton8" runat="server" Width="194px" CommandName="MeetAndGreet">Meet And Greet</asp:LinkButton>
                        
                        </th>
                        
                         <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton5" runat="server" Width="164px" CommandName="CostCenter">Cost Center</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton6" runat="server" Width="160px" CommandName="Vessel">Ship</asp:LinkButton>
                        </th>
                       <%-- <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton8" runat="server" Width="70px" CommandName="CheckIn">Check In</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton9" runat="server" Width="70px" CommandName="CheckOut">Check Out</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton11" runat="server" Width="40px" CommandName="Nites">Nites</asp:LinkButton>
                        </th>--%>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
        onscroll="divScrollL();">
        <asp:ListView runat="server" ID="uoListViewHotelRequest" DataSourceID="uoObjectDataSourceHotelRequestView"
            OnItemCommand="uoListViewHead_ItemCommand">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <%--<td style="white-space: normal;">
                    <asp:HyperLink Width="110px" ID="uoHyperLinkAddRequest" CssClass="ViewRequest" NavigateUrl='<%# "HotelRequest.aspx?sfId=" + Eval("SfID") + "&hrID=" + Eval("REQUESTID") + "&trID=0&App=2"%>'
                            runat="server"><%# Eval("REQUESTNO")%></asp:HyperLink>
                      
                    </td>--%>
                  <%--  <asp:HyperLink Width="110px" ID="uoHyperLinkAddRequest" 
                     NavigateUrl='<%# "CrewAssist/CrewAssist.aspx?sfId=" + Eval("SfID") + "&hrID=" + Eval("REQUESTID") + "&trID=" + Eval("colTravelReqIDInt") + "&App=2" + "&dt=" + Eval("CHECKIN") + "&as=" + Eval("colSeqNoInt") + "&trp=" + Eval("TransportationID")   %>'
                            runat="server"><%# Eval("REQUESTNO")%></asp:HyperLink>
                      
                    </td>
                   
                    
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label3" Text=' <%# Eval("LASTNAME")%>' Width="150px" />
                    </td>--%>
                    
                    <td style="white-space: normal;">
                        <asp:HyperLink Width="154.5px" ID="uoHyperLinkName" 
                         NavigateUrl='<%# "CrewAssist/CrewAssist.aspx?sfId=" + Eval("SfID") + "&hrID=" + Eval("REQUESTID") + "&trID=" + Eval("colTravelReqIDInt") + "&App=2" + "&dt=" + Eval("CHECKIN") + "&as=" + Eval("colSeqNoInt") + "&trp=" + Eval("TransportationID") + "&magp=" + Eval("MeetAndGreetID")   %>'
                                runat="server"> <%# Eval("LASTNAME")%></asp:HyperLink>
                      
                    </td>
                    
                    
                    <td class="leftAligned" style="white-space: normal;">
                          <asp:Label runat="server" ID="Label1" Text=' <%# Eval("FIRSTNAME")%>' Width="150px" />
                    </td>
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label4" Text=' <%# Eval("RANK")%>' Width="200px" />
                    </td>
                   
                    <td style="white-space: normal;">
                        <asp:Label runat="server" ID="Label7" Text=' <%# Eval("GENDER")%>' Width="65px" />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label10" Text=' <%# Eval("HOTEL")%>' Width="201px" />
                    </td>
                    
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label2" Text=' <%# Eval("TRANSPORTATION")%>' Width="201px" />
                    </td>
                   
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label3" Text=' <%# Eval("MeetAndGreet")%>' Width="201px" />
                    </td>
                    
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label5" Text=' <%# Eval("COSTCENTER")%>' Width="170px" />
                    </td>
                    <td style="white-space: normal;">
                        <asp:Label runat="server" ID="Label6" Text=' <%# Eval("VESSEL")%>' Width="170px" />
                    </td> 
                    
                    
                    <%--<td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label8" Text=' <%# String.Format("{0:dd-MMM-yyyy}", Eval("CHECKIN"))%>'
                            Width="75px" />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label9" Text=' <%# String.Format("{0:dd-MMM-yyyy}",Eval("CHECKOUT"))%>'
                            Width="77px" />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label12" Text=' <%# Eval("NITES")%>' Width="43px" />
                    </td>--%>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table>
                    <tr>
                        <td colspan="12">
                            <asp:Label runat="server" ID="uoLblScroll" Width="1630px"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left">
                        <td colspan="12" class="leftAligned">
                            No Record
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <div align="left">
            <asp:DataPager ID="uoListViewHotelRequestPager" runat="server" PagedControlID="uoListViewHotelRequest"
                PageSize="20">
                <Fields>
                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                </Fields>
            </asp:DataPager>
            <asp:ObjectDataSource ID="uoObjectDataSourceHotelRequestView" runat="server" SelectMethod="GetHotelRequestList"
                TypeName="TRAVELMART.BLL.SeafarerBLL" OnSelecting="uoObjectDataSourceHotelRequestView_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="SortBy" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
   <%--Booked Request--%>
   <%-- 
    <div id="Av1" style="overflow-x: hidden; width: 100%; overflow-y: hidden;">
        <asp:ListView runat="server" ID="uoListViewHeadBooked" OnItemCommand="uoListViewHeadBooked_ItemCommand">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="listViewTable" width="100%">
                    <tr>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton1" runat="server" Width="100px" CommandName="RequestNo">Reason No.</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton2" runat="server" Width="144px" CommandName="Lastname">Last Name</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton3" runat="server" Width="144px" CommandName="Firstname">First Name</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton4" runat="server" Width="144px" CommandName="Rank">Position</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton5" runat="server" Width="144px" CommandName="CostCenter">Cost Center</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton6" runat="server" Width="140px" CommandName="Vessel">Ship</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton7" runat="server" Width="55px" CommandName="Gender">Gender</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton10" runat="server" Width="144px" CommandName="Hotel">Hotel</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton8" runat="server" Width="70px" CommandName="CheckIn">Check In</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton9" runat="server" Width="70px" CommandName="CheckOut">Check Out</asp:LinkButton>
                        </th>
                        <th style="text-align: center; white-space: normal;">
                            <asp:LinkButton ID="LinkButton11" runat="server" Width="40px" CommandName="Nites">Nites</asp:LinkButton>
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div id="Bv1" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
        onscroll="divScrollL1();">
        <asp:ListView runat="server" ID="uoListViewHeadBookedList" DataSourceID="uoObjectDataSourceBookedHotelRequestView"
            OnItemCommand="uoListViewHeadBooked_ItemCommand">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td style="white-space: normal;">
                        <asp:Label runat="server" ID="Label1" Text=' <%# Eval("REQUESTNO")%>' Width="110px" />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label3" Text=' <%# Eval("LASTNAME")%>' Width="150px" />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:HyperLink Width="150px" ID="uoHyperLinkAddRequest" CssClass="ViewRequest" NavigateUrl='<%# "HotelRequest.aspx?sfId=" + Eval("SfID") + "&trID=" + Eval("SfID") + "&e1TR=" + Eval("SfID")%>'
                            runat="server"><%# Eval("FIRSTNAME")%></asp:HyperLink>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label4" Text=' <%# Eval("RANK")%>' Width="150px" />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label5" Text=' <%# Eval("COSTCENTER")%>' Width="150px" />
                    </td>
                    <td style="white-space: normal;">
                        <asp:Label runat="server" ID="Label6" Text=' <%# Eval("VESSEL")%>' Width="150px" />
                    </td>
                    <td style="white-space: normal;">
                        <asp:Label runat="server" ID="Label7" Text=' <%# Eval("GENDER")%>' Width="65px" />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label10" Text=' <%# Eval("HOTEL")%>' Width="151px" />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label8" Text=' <%# String.Format("{0:dd-MMM-yyyy}", Eval("CHECKIN"))%>'
                            Width="75px" />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label9" Text=' <%# String.Format("{0:dd-MMM-yyyy}",Eval("CHECKOUT"))%>'
                            Width="77px" />
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label12" Text=' <%# Eval("NITES")%>' Width="43px" />
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table>
                    <tr>
                        <td colspan="12">
                            <asp:Label runat="server" ID="uoLblScroll" Width="1630px"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left">
                        <td colspan="12" class="leftAligned">
                            No Record
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <div align="left">
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="uoListViewHeadBookedList"
                PageSize="20">
                <Fields>
                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                </Fields>
            </asp:DataPager>
            <asp:ObjectDataSource ID="uoObjectDataSourceBookedHotelRequestView" runat="server"
                SelectMethod="GetBookedHotelRequestList" TypeName="TRAVELMART.BLL.SeafarerBLL"
                OnSelecting="uoObjectDataSourceHotelBookedRequestView_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="SortBy" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>--%>
    <asp:HiddenField runat="server" ID="uoHiddenFieldSortBy" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldPopupHotel" />
    <asp:HiddenField runat="server" ID="uoHiddenFIeldHasChanges" />
</asp:Content>
