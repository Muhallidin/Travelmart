<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="SuperUserView.aspx.cs" Inherits="TRAVELMART.SuperUserView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function OpenRequestEditor(sfID, sfName, Stripe, Status, RecLoc, TrId, mReqId, idBgint, seqNo) {
            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;
            //           var sfId2 = $(
            screenWidth = 1060;
            screenHeight = 600;

            
            window.open('Hotel/HotelEditor2.aspx?sfID=' + sfID + '&sfName=' + sfName + '&Stripe='
            + Stripe + '&Status=' + Status + '&RecLoc=' + RecLoc + '&trId=' + TrId + '&idBgint=' + idBgint + '&seqNo=' + seqNo
            + '&mReqId=' + mReqId, 'Hotel_Vehicle_Request', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');

                return false;
        }
        function OpenEmergencyRequestEditor(sfID, TrId, mReqId,App) {
            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;
            //           var sfId2 = $(
            screenWidth = 1160;
            screenHeight = 600;

            window.open('HotelRequest.aspx?sfId=' + sfID + '&trID=' + TrId + '&hrID=' + mReqId + '&App=' + App, 'Emergency_Hotel_Request', 'top=10, left=100, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
          
            return false;
        }

        function OpenRequestEditor2(sfID, sfName, Stripe, Status, RecLoc, TrId, mReqId, HotelTransId, idBgint, seqNo) {
            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;
            //           var sfId2 = $(
            screenWidth = 1060;
            screenHeight = 600;

            window.open('Hotel/HotelEditor2.aspx?sfID=' + sfID + '&sfName=' + sfName + '&Stripe='
            + Stripe + '&Status=' + Status + '&RecLoc=' + RecLoc + '&trId=' + TrId + '&idBgint=' + idBgint + '&seqNo=' + seqNo
            + '&mReqId=' + mReqId + '&HotelTransId=' + HotelTransId, 'Hotel_Vehicle_Request', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }

        function SetTRResolution() {
            var ht = $(window).height();
            var wd = $(window).width() * 0.945;
            if (screen.height <= 600) {
                ht = ht * 0.33;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.50;
            }
            else {
                ht = ht * 0.525;
            }

            $("#Bv").height(ht);
            $("#Bv").width(wd);
        }        
        function pageLoad(sender, args) {

            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();                
            }
        }
        $(document).ready(function() {
            SetTRResolution();
        });


//        function Confirm() {
//            var confirm_value = document.createElement("INPUT");
//            confirm_value.type = "hidden";
//            confirm_value.name = "confirm_value";
//            if (confirm("Do you want to save data?")) {
//                confirm_value.value = "Yes";
//            } else {
//                confirm_value.value = "No";
//            }
//            document.forms[0].appendChild(confirm_value);
//        }
          
        
    </script>

</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">
    <asp:HiddenField ID="HiddenFieldPriority" runat="server" value="false" /> 
    <div class="PageTitle">
        Crew Information</div>
    <table width="95%" >
        <tr>
            <td class="LeftClass" style="width: 85px; padding-left: 5px;">
                E1 ID :
            </td>
            <td class="LeftClass" style="width: 250px; font-weight: bold">
                <asp:Label ID="uclabelE1ID" runat="server" />
            </td>
            <td class="LeftClass" style="width: 100px">
                Brand :&nbsp;
            </td>
            <td class="LeftClass" style="width: 250px; font-weight: bold">
                <asp:Label ID="uclabelBrand" runat="server" />
            </td>
            <td class="LeftClass" style="width: 100px">
                Status:                
            </td>
            <td class="LeftClass" style="width: 100px; font-weight: bold">
                <asp:Label ID="uclabelStatus" runat="server" />
            </td>
            <td rowspan="5" style="width:300px">
                <table style="text-align:left; vertical-align:top; border-style:solid; border-color:Gray; border-width:thin" runat="server" id="ucTableRemarks">
                    <tr>
                        <td>User:</td>
                        <td> <asp:Label ID="uoLabelRemarksBy" runat="server" /></td>
                        <td style="text-align:right">
                            <asp:HyperLink runat="server" ID ="uoHyperLinkRemarks" Text ="View All" CssClass="ViewRemarks"></asp:HyperLink> 
                        </td>
                    </tr>
                    <tr>
                        <td>Date:</td>
                        <td> <asp:Label ID="uoLabelRemarksDate" runat="server" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top">Remarks:
                             
                            <asp:Button ID="uoButtonNew" runat="server" Text="New" CssClass="SmallButton" 
                                onclick="uoButtonNew_Click"  Width="50px"/> <br/>
                            <asp:Button ID="uoButtonEdit" runat="server" Text="Edit" 
                                CssClass="SmallButton"  Width="50px" onclick="uoButtonEdit_Click"/>
                           
                            <asp:Button ID="uoButtonSave" runat="server" Text="Save" 
                                CssClass="SmallButton" Width="50px" Visible="false" 
                                onclick="uoButtonSave_Click"/><br/>
                            <asp:Button ID="uoButtonCancel" runat="server" Text="Cancel" Visible="false"
                                     CssClass="SmallButton" onclick="uoButtonCancel_Click"  Width="50px"/>
                             
                        </td>
                        <td style="vertical-align:top" colspan="2">                            
                            <asp:TextBox ID="uoTextBoxRemarks" runat="server"  TextMode="MultiLine" Width="250px" 
                                Height="50px" ReadOnly="true" CssClass="ReadOnly" />
                        </td>                        
                    </tr>
                   <%-- <tr>
                        <td colspan="2" style="text-align:right">
                            
                            
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="padding-left: 5px">
                Name :
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelName" runat="server" />
            </td>
            <td class="LeftClass">
                Ship :
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelVessel" runat="server" />
            </td>
            <td class="LeftClass">
                Signing ON Date:
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <%--<asp:Label ID="uclabelCivilStatus" runat="server" />--%>
                <asp:Label ID="ucLabelSignOn" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="padding-left: 5px">
                Nationality :
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelNationality" runat="server" />
            </td>
            <td class="LeftClass">
                Rank :
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelRank" runat="server" />
            </td>
            <td class="LeftClass">
                Signing OFF Date:
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="ucLabelSignOff" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="padding-left: 5px">
                Gender :
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelGender" runat="server" />
            </td>
            <td class="LeftClass">
                Reason Code:
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <asp:Label ID="uclabelReason" runat="server" /> 
            </td>
            <td class="LeftClass">
                Orig/Dest:
            </td>
            <td class="LeftClass" style="font-weight: bold">
                <%--<asp:Label ID="ucLabelPort" runat="server" />--%>
                <%--<asp:Label ID="ucLabelOrigin" runat="server" /> / <asp:Label ID="ucLabelDestination" runat="server" />--%>
                <asp:Label ID="ucLabelOriginDestination" runat="server" />
            </td>            
        </tr>
        <tr>
            <td class="LeftClass" style="padding-left: 5px">
               
            </td>
            <td class="LeftClass" style="font-weight: bold">
                
            </td>
            <td class="LeftClass">
               
            </td>
            <td class="LeftClass" style="font-weight: bold">
                
            </td>
            <td class="LeftClass">
               
            </td>
            <td class="LeftClass" style="font-weight: bold">
                
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">
        function RefreshPageFromPopup() {
            if ($("#<%=uoHiddenFieldPopEditor.ClientID %>").val() == 1) {
                $("#aspnetForm").submit();
            }
        }
        
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }

        function confirmApprove() {
            if (confirm("Approve record?") == true)
                return true;
            else
                return false;
        }

        function confirmCancel() {
            if (confirm("Are you sure you want to cancel the hotel?") == true)
                return true;
            else
                return false;
        }

       
    </script>

    <script type="text/javascript">
        function pageLoad(sender, args) {

            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                PageSettings();
            }
        }

        $(document).ready(function() {
            PageSettings();

        });

        function PageSettings() {
             $("a#<%=uoHyperLinkAirAdd.ClientID %>").fancybox({
                 'width': '40%',
                 'height': '85%',
                 'autoScale': false,
                 'transitionIn': 'fadeIn',
                 'transitionOut': 'fadeOut',
                 'type': 'iframe',
                 'onClosed': function() {
                     var a = $("#<%=uoHiddenFieldPopupAir.ClientID %>").val();
                     if (a == '1')
                         $("#aspnetForm").submit();
                 }

             });

             $(".AirLink").fancybox(
            {
                'width': '40%',
                'height': '85%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupAir.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });
             $("a#<%=uoHyperLinkVehicleAdd.ClientID %>").fancybox(
            {
                'width': '45%',
                'height': '110%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupVehicle.ClientID %>").val();
                    var b = $("#<%=uoHiddenFieldPopupVehiclePending.ClientID %>").val();
                    if (a == '1' || b == '1') {
                        $("#aspnetForm").submit();
                    }
                }
            });
             $(".VehicleLink").fancybox(
            {
                'width': '45%',
                'height': '110%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupVehicle.ClientID %>").val();
                    var b = $("#<%=uoHiddenFieldPopupVehiclePending.ClientID %>").val();
                    if (a == '1' || b == '1') {
                        $("#aspnetForm").submit();
                    }
                }
            });
             $(".VehicleLinkPending").fancybox(
            {
                'width': '45%',
                'height': '110%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupVehiclePending.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });

             $(".HotelLink").fancybox(
            {
                'width': '87%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupHotel.ClientID %>").val();
                    var b = $("#<%=uoHiddenFieldPopupHotelPending.ClientID %>").val();
                    if (a == '1' || b == '1') {
                        $("#aspnetForm").submit();
                    }
                }
            });
             $(".HotelLinkPending").fancybox(
            {
                'width': '80%',
                'height': '95%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupHotelPending.ClientID %>").val();
                    if (a == '1') {
                        $("#aspnetForm").submit();
                    }
                }
            });
             $(".PortLink").fancybox
            ({
                'width': '40%',
                'height': '63%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupPort.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });

             $("a#<%=uoHyperLinkReimbursementAdd.ClientID %>").fancybox({
                 'width': '60%',
                 'height': '65%',
                 'autoScale': false,
                 'transitionIn': 'fadeIn',
                 'transitionOut': 'fadeOut',
                 'type': 'iframe',
                 'onClosed': function() {
                     var a = $("#<%=uoHiddenFieldPopupReimbursement.ClientID %>").val();
                     if (a == '1')
                         $("#aspnetForm").submit();
                 }

             });

             $(".ReimbursementLink").fancybox(
            {
                'width': '60%',
                'height': '65%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupReimbursement.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });

             $(".ReimbursementLinkView").fancybox(
            {
                'width': '55%',
                'height': '60%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe'
            });

            $(".ViewRemarks").fancybox(
            {
                'width': '50%',
                'height': '65%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupRemarks.ClientID %>").val();                   
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });

            $("#<%=uoBtnAdd.ClientID %>").click(function(ev) {
                var isWithRecord = false;

                $('table[id$=uoTableAir]').each(function() {
                    if ($('input:checkbox[name *= uoCheckBoxSelect]') != null) {
                        isWithRecord = true;
                    }
                })

                var isCheck = false;
                if (isWithRecord) {
                    $('table[id$=uoTableAir] tr').each(function(i, tr) {
                        if (i > 0) {
                            var v = $(this).find('input:checkbox').attr('checked');
                            if (v) {
                                isCheck = true;
                            }
                        }
                    })

                    if (isCheck == false) {
                        alert('No Air segment selected!');
                        return false;
                    }
                }
            });
        }  
    </script>

    <script type="text/javascript" language="javascript">
        function OpenContract(vendorID, VendorType) {

            var URLString = "../ContractManagement/";
            if (VendorType == 'HO') {
                URLString += "HotelContractView.aspx?vId=";
            }
            else {
                URLString += "VehicleContractView.aspx?bId=";
            }
            URLString += vendorID + "&vmType=" + VendorType;


            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
    </script>

    <script type="text/javascript" language="javascript">
        function OpenEventsList(branchID, cityID, OnOffDate) {
            var URLString = "../Maintenance/EventsList.aspx?bId=";
            URLString += branchID;
            URLString += "&cityId=" + cityID;
            URLString += "&Date=" + OnOffDate;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 600;
            screenHeight = 400;

            window.open(URLString, 'Events_List', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
    </script>

    <div id="Bv" style="overflow: auto; overflow-x: auto; overflow-y: auto;">
        <table width="95%" cellspacing="0" border="0">
            <tr>
                <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                    <div>
                        <table style="width: 100%" cellspacing="0">
                            <tr>
                                <td style="width: 30%; text-align: left;" class="PageTitle">
                                    Air Travel Information
                                </td>
                                <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                    <a id="uoHyperLinkAirAdd" runat="server" href="~/Air/AirEditor.aspx">
                                        <asp:Button ID="uoButtonAirAdd" runat="server" Text="Add" CssClass="SmallButton"
                                            Visible="false" />
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="Module">
                    <asp:UpdatePanel runat="server" ID="uoPanelAir" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ListView runat="server" ID="uoListViewAirTravelInfo">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" id = "uoTableAir">
                                        <tr>
                                            <%--<th>Seq No.</th>--%>
                                            <th>
                                                Rec Loc
                                            </th>
                                            <th>
                                                Seq. No.
                                            </th>
                                            <th>
                                                City Pair
                                            </th>
                                           
                                            <th>
                                            Est. Connection Time
                                                </th>
                                             <th>
                                             
                                             
                                                Departure Date/Time
                                            </th>
                                            
                                         <th>
                                                Actual Departure Time
                                            </th>
                                            
                                            
                                            <th>
                                                Arrival Date/Time
                                            </th>
                                            
                                           <th>
                                                Actual Arrival Time
                                            </th> 
                                                                                      
                                            <th>
                                                Airline
                                            </th>
                                            <th>
                                                Flight No.
                                            </th>
                                            <th>
                                                Status
                                            </th> 
                                      <th>
                                            Actual Flight Status
                                            </th>
                                            
                                            <th>
                                                Arrival Gate
                                            </th>
                                            <th>
                                            Seat Number
                                            </th>
                                            <th>
                                            Baggage Claim
                                            </th> 
                                          
                                            <th style="width: 100px;">
                                                Action Code
                                            </th>                                              
                                          
                                            <th style="width: 150px;">
                                                E-ticket
                                            </th>
                                            </th>
                                            <th runat="server" style="width: 20px;"  id="SelectTH">
                                                Select
                                            </th>
                                              <th runat="server" id="EditTH"/>
                                            
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class='<%# (InactiveRow( Eval("IsCurrent") )) %>'>
                                        <td>
                                           <asp:Label ID="uoLabelRecLoc" runat="server" Text='<%# Eval("recloc")%>'></asp:Label> 
                                        </td>
                                         <td>
                                            <asp:Label ID="uoLabelSeqNo" runat="server" Text='<%# Eval("colSeqNoInt")%>'></asp:Label>     
                                             <asp:HiddenField ID="uoHiddenFieldIDBigint" Value ='<%# Eval("colIdBigint") %>' runat="server" />                                       
                                        </td>
                                        <td class="leftAligned">
                                            <%# Eval("citypair")%>
                                        </td>
                                        
                                         <td bgcolor="#ed1c24" >
                                           <%# string.Compare(Eval("SegmentOrder").ToString(), "1") == 0 ? "" : "" %>
                                           <%# string.Compare(Eval("SegmentOrder").ToString(), "1") != 0 ? Eval("datediffs")+" Hours" : ""%>
                                        </td>
                                        <td class="leftAligned">
                                            <%# string.Format("{0:dd-MMM-yyyy HHmm}", Eval("departure"))%>
                                        </td>
                                        
                                    <td class="leftAligned">
                                           <%# Eval("actDateD")%>
                                        </td>   
                                        
                                                                          
                                        <td class="leftAligned">
                                            <%# string.Format("{0:dd-MMM-yyyy HHmm}", Eval("arrival"))%>
                                        </td>
                                        
                                    
                                        <td class="leftAligned">
                                         <%# Eval("actDateT")%>
                                        </td>
                                       
                                                                               
                                        <td class="leftAligned">
                                            <%# Eval("colMarketingAirlineCodeVarchar")%>
                                        </td>
                                        
                                        <td class="leftAligned">
                                            <%# Eval("flightNo")%>
                                        </td>
                                       <td class="leftAligned">
                                            <%# Eval("status")%>
                                         </td> 
                                                                         
                                        <td class="leftAligned"> 
                                         <%# Eval("FlightStatus")%>
                                        </td>
                                          
                                        <td class="leftAligned">
                                         <%# Eval("FlightArrGate")%>
                                        </td>
                                        <td>
                                            <%# Eval("Number")%>
                                        </td>
                                        <td class="leftAligned">
                                         <%# Eval("FlightBaggageClaim") %>
                                        </td>
                                             
                                         <td class="leftAligned">
                                            <%# Eval("ActionCode")%>
                                            <%# string.Compare(Eval("ActionCode").ToString(), "HK") == 0 ? "- Confirmed" : "" %>
                                            <%# string.Compare(Eval("ActionCode").ToString(), "GK") == 0 ? "- Passive Segment" : ""%>
                                            <%# string.Compare(Eval("ActionCode").ToString(), "HX") == 0 ? "- Holding Confirmed and now cancelled" : ""%>
                                            <%# string.Compare(Eval("ActionCode").ToString(), "KK") == 0 ? "- Carrier confirming request" : ""%> 
                                        </td>
                                      
                                   
                                          <td class="leftAligned">
                                            <%# Eval("ETicket")%>
                                        </td>
                                        <td style="vertical-align:middle; text-align:center;  " visible='<%# (Convert.ToBoolean(HiddenFieldPriority.Value)) %>'>
                                            <asp:CheckBox ID="uoCheckBoxSelect" runat="server" />
                                        </td>
                                        
                                         <td class='<%# (HideAir()==""?"leftAligned":HideAir()) %>'>
                                            <asp:HyperLink runat="server" Enabled='<%# !InactiveControl( Eval("IsCurrent"), "","") %>'
                                                CssClass='<%# (InactiveControl( Eval("IsCurrent"),"","")== true?"":"AirLink") %>'
                                                ID="uoHyperLinkAirEdit" NavigateUrl='<%# "~/Air/AirEditor.aspx?sfId=" + Eval("sfID") + "&aId=" + Eval("colTransAirIDBigInt") +  "&st=" + Request.QueryString["st"] + "&recloc=" + Request.QueryString["recloc"] + "&ID=" + Eval("colIdBigint") + "&SN=" + Eval("colSeqNoInt") %>'>
                                    Edit</asp:HyperLink>
                                        
                                        
                                        
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                            <th>
                                                Rec Loc
                                            </th>
                                            <th>
                                                City Pair
                                            </th>
                                            <th>
                                                Departure Date/Time
                                            </th>
                                            <th>
                                                Arrival Date/Time
                                            </th>
                                            <th>
                                                Airline
                                            </th>
                                            <th>
                                                Flight No.
                                            </th>
                                            <th>
                                                Status
                                            </th>
                                            <th >
                                                Action Code
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="7" class="leftAligned">
                                                No Record
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoListViewAirTravelInfo" 
                                EventName="DataBinding" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                    <div>
                        <table style="width: 100%" cellspacing="0">
                            <tr>
                                <td style="width: 30%; text-align: left;" class="PageTitle">
                                    Vehicle Information
                                </td>
                                <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                    <a id="uoHyperLinkVehicleAdd" runat="server" href="~/Vehicle/VehicleEditor.aspx">
                                        <asp:Button ID="uoButtonVehicleAdd" runat="server" Text="Add" Font-Size="X-Small" />
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="Module">
                    <asp:ListView runat="server" ID="uoListviewVehicleTravelInfo" OnItemCommand="uoListviewVehicleTravelInfo_ItemCommand"
                        OnItemDeleting="uoListviewVehicleTravelInfo_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Rec Loc
                                    </th>
                                    <th>
                                        Company
                                    </th>
                                    <th>
                                        Vehicle Type
                                    </th>
                                    <th>
                                        Pick-up Point
                                    </th>
                                    <th>
                                        Pick-up Date/Time
                                    </th>
                                    <th>
                                        Destination
                                    </th>
                                    <th>
                                        Tagged
                                    </th>
                                    <th style="display:none">
                                        Status
                                    </th>                                    
                                    <%--<%#  if (hideshow()) %>--%>
                                    <th runat="server" id="EditTH">
                                    </th>
                                    <th runat="server" id="DeleteTH">
                                    </th>
                                    <%--   <%# end  If %>     --%>
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class='<%# (InactiveRow( Eval("IsCurrent") )) %>'>
                                <td>
                                    <%# Eval("RecLoc")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("company")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("vehicletype")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("pickuppoint")%>
                                </td>
                                <td class="leftAligned">
                                    <%# String.Format(DateFormat(Eval("IsDateTime")),Eval("pickupdatetime"))%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("destination")%>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat='server' enabled="false" Checked='<%# Eval("IsTagged")%>'/>   
                                </td>
                                <td class="leftAligned" style="display:none">
                                    <%# Eval("status")%>
                                </td>                                
                                <td class='<%# (HideVehicle()==""?"leftAligned":HideVehicle()) %>'>
                                    <asp:HyperLink runat="server" Enabled='<%# !InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"),"VE") %>'
                                        CssClass='<%# (InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"),"VE")== true?"":"VehicleLink") %>'
                                        ID="uoHyperLinkVehicleEdit" NavigateUrl='<%# "~/Vehicle/VehicleEditor.aspx?vID="+ Eval("colTransVehicleIDBigInt") +"&sfId=" + Eval("sfID") + "&st=" + Request.QueryString["st"] + "&recloc=" + Request.QueryString["recloc"] + "&ID=" + Eval("colIdBigint") + "&SN=" + Eval("colSeqNoInt")  + "&manualReqID=" + Request.QueryString["manualReqID"] + "&trID=" + Request.QueryString["trID"] %>'>
                                    Edit</asp:HyperLink>
                                </td>
                                <td class='<%# (HideVehicle()==""?"leftAligned":HideVehicle()) %>'>
                                    <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick='<%# (InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"),"VE")== true? "return false":"return confirmDelete();") %>'
                                        CommandArgument='<%#Eval("colIdBigint") + ";" + Eval("colSeqNoInt") + ";" + Eval("colTransVehicleIDBigInt") %>'
                                        CommandName="Delete" Enabled='<%# !InactiveControl( Eval("IsCurrent"),  Eval("colBranchIDInt"), "VE") %>'>Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Rec Loc
                                    </th>
                                    <th style="width: 180px">
                                        Name
                                    </th>
                                    <th>
                                        Company
                                    </th>
                                    <th>
                                        Vehicle Type
                                    </th>
                                    <th>
                                        Pick-up Point
                                    </th>
                                    <th>
                                        Pick-up Date/Time
                                    </th>
                                    <th>
                                        Destination
                                    </th>
                                    <th>
                                        Status
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="8" class="leftAligned">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                    <div>
                        <table style="width: 100%" cellspacing="0">
                            <tr>
                                <td style="width: 30%; text-align: left;" class="PageTitle">
                                    Hotel Information
                                </td>
                                <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                    <asp:Button ID="uoBtnAdd" runat="server" Text="Add" Font-Size="X-Small" OnClick="uoBtnAdd_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="Module">
                    <asp:ListView runat="server" ID="uolistviewHotelInfo" OnItemCommand="uolistviewHotelInfo_ItemCommand"
                        OnItemDeleting="uolistviewHotelInfo_ItemDeleting" OnItemEditing="uolistviewHotelInfo_ItemEditing">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                     
                                    <th>
                                        Rec Loc
                                    </th>
                                    <th>
                                        Company
                                    </th>
                                    <th>
                                        Check-in Date/Time
                                    </th>
                                    <th>
                                        Room Type
                                    </th>
                                    <th>
                                        No. of Days
                                    </th>
                                    <th style="display:none">
                                        Status
                                    </th>
                                    <th>
                                    Voucher
                                    </th>
                                    <th>
                                     Shuttle
                                    </th>
                                    <th>
                                     Tagged
                                    </th>
                                    <th runat="server" id="EditTH">
                                    </th>
                                   <%-- <th runat="server" id="CancelTH">
                                    </th>--%>
                                   <%-- <th runat="server" id="DeleteTH">
                                    </th>--%>
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class='<%# (InactiveRow( Eval("IsCurrent") )) %>'>
                            
                                <td>
                                    <%# Eval("RecLoc")%>
                                    <asp:HiddenField ID="hfIdBigint" runat="server" Value='<%# Eval("colIdBigint") %>' />
                                    <asp:HiddenField ID="hfSeqNo" runat="server" Value='<%# Eval("colSeqNoInt") %>'/>
                                </td>
                                
                                <td class="leftAligned">
                                    <%# Eval("company")%>
                                    <asp:Label ID="uoLabelEvents" runat="server" Text="*" Font-Bold="true" Font-Size="Large"
                                        ForeColor="Red" Visible='<%# IsEventExists(Eval("colBranchIDInt")) %>'></asp:Label>
                                </td>
                                <td class="leftAligned">
                                    <%# String.Format(DateFormat(Eval("IsDateTime")),Eval("checkindatetime"))%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("roomtype")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("nodaysnight")%>
                                </td>
                                <td class="leftAligned" style="display:none">
                                    <%# Eval("status")%>
                                </td>
                                <td>
                                <asp:CheckBox runat="server" enabled="false" Checked='<%# (Convert.ToDecimal(Eval("colVoucherAmountMoney")) > 0 ? true : false)  %>' />
                                <%--<%# (String.IsNullOrEmpty(Eval("colVoucherAmountMoney").ToString()) ? "False" : "True")%>--%>
                                <%--<%# Eval("colVoucherAmountMoney", "{0:#,##0.00}")%>--%>
                                </td>
                                <td>
                                    <asp:CheckBox runat='server' enabled="false" Checked='<%# Eval("colWithShuttleBit")%>'/>                                
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat='server' enabled="false" Checked='<%# Eval("IsTagged")%>'/>   
                                </td>
                                <td  style="text-align:center"  class='<%# (HideHotel()==""?"leftAligned":HideHotel()) %>'>
                                    <asp:LinkButton Enabled='<%# !InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"), "HO") %>'
                                        ID="uoLinkButtonEdit" runat="server" CommandArgument='<%#Eval("colIdBigint") + ";" + Eval("colSeqNoInt") + ";" + Eval("colTransHotelIDBigInt") %>'
                                        CommandName="Edit">Edit</asp:LinkButton>
                                    <%--<asp:HyperLink runat="server"  Enabled='<%# !InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"), "HO") %>'  CssClass='<%# (InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"), "HO")== true?"":"HotelLink") %>' id="uoAEditHotel" NavigateUrl='<%# "~/Hotel/HotelEditor.aspx?hID=" + Eval("colTransHotelIDBigInt") + "&sfId=" + Eval("sfID") + "&st=" + Request.QueryString["st"] + "&recloc=" + Request.QueryString["recloc"] + "&ID=" + Eval("colIdBigint") + "&SN=" + Eval("colSeqNoInt") + "&trID=" + Request.QueryString["trID"] + "&manualReqID=" + Request.QueryString["manualReqID"] + "&Date=" + NotifyEvent() %>'>                                                           
                                    Edit</asp:HyperLink>--%>
                                </td>
                                
                                <%-- <td style="text-align:center" class='<%# (HideCancelHotel()==""?"leftAligned":HideCancelHotel()) %>'>
                                    <asp:LinkButton Enabled='<%# !InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"), "HO") %>'
                                        OnClientClick='<%# (InactiveControl( Eval("IsCurrent"), Eval("colBranchIDInt"),"HO")== true? "return false":"return confirmCancel();") %>'
                                        ID="uoLinkButtonCancel" runat="server" CommandArgument='<%#Eval("colIdBigint") + ";" + Eval("colSeqNoInt") + ";" + Eval("colTransHotelIDBigInt") %>'
                                        CommandName="Delete">Cancel</asp:LinkButton>
                                    
                                </td>--%>
                                
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Rec Loc
                                    </th>
                                    <th style="width: 180px">
                                        Name
                                    </th>
                                    <th>
                                        Company
                                    </th>
                                    <th>
                                        Check-in Date/Time
                                    </th>
                                    <th>
                                        Room Type
                                    </th>
                                    <th>
                                        No. of Days
                                    </th>
                                    <th>
                                        Status
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="7" class="leftAligned">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-left: 3px; padding-right: 3px">
                    <div class="PageTitle">
                        Port Information</div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="uogridviewPortInfo" runat="server" AutoGenerateColumns="False"
                        CssClass="listViewTable" OnRowCommand="uogridviewPortInfo_RowCommand" OnRowDeleting="uogridviewPortInfo_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="port" HeaderText="Port" ItemStyle-CssClass="leftAligned">
                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vessel" HeaderText="Ship" ItemStyle-CssClass="leftAligned">
                                <ItemStyle Width="25%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="porttransdate" HeaderText="Arrival Date/Time" DataFormatString="{0:dd-MMM-yyyy HHmm}"
                                HtmlEncode="false" ItemStyle-CssClass="leftAligned">
                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="costcenter" HeaderText="Cost Center" ItemStyle-CssClass="leftAligned">
                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-CssClass="leftAligned">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%--<asp:HyperLink CssClass="PortLink" ID="uoHyperLinkPortUpdate" runat="server" NavigateUrl='<%# "~/PortAgent/PortEditor.aspx?sfId=" + Eval("sfID") + "&st=" + Request.QueryString["st"] + "&recloc=" + Request.QueryString["recloc"] + "&pID=" + Eval("colPortAgentTransIdbigint") %>'>Update</asp:HyperLink>--%>
                                    <%--<asp:HyperLink CssClass="PortLink" ID="uoHyperLinkPortUpdate" runat="server" NavigateUrl='<%# "~/PortAgent/PortEditor.aspx?sfId=" + Eval("sfID") + "&st=" + Request.QueryString["st"]  + "&pID=" + Eval("colPortAgentTransIdbigint") + "&trID=" + Request.QueryString["trID"] + "&manualReqID=" + Request.QueryString["manualReqID"]%>'>Update</asp:HyperLink>--%>
                                    <asp:HyperLink CssClass="PortLink" ID="uoHyperLinkPortUpdate" runat="server" NavigateUrl='<%# "~/PortAgent/PortEditor.aspx?sfId=" + Eval("sfID") + "&st=" + Request.QueryString["st"] + "&recloc=" + Request.QueryString["recloc"] + "&pID=" + Eval("colPortAgentTransIdbigint") + "&trID=" + Request.QueryString["trID"] + "&manualReqID=" + Request.QueryString["manualReqID"]%>'>Update</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                        CommandArgument='<%#Eval("colPortAgentTransIdbigint") %>' CommandName="Delete">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr style="display:none;">
                <%--<td>Reimbursement</td>
           <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                <a id="uoHyperLinkReimbursementAdd" runat="server" >
                    <asp:Button ID="uoBtnAddReimbursement" runat="server" Text="Add" Font-Size="X-Small"  />
                </a> 
           </td>--%>
                <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                    <div>
                        <table style="width: 100%" cellspacing="0">
                            <tr>
                                <td style="width: 30%; text-align: left;" class="PageTitle">
                                    Reimbursement
                                </td>
                                <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                    <a id="uoHyperLinkReimbursementAdd" runat="server">
                                        <asp:Button ID="uoBtnAddReimbursement" runat="server" Text="Add" Font-Size="X-Small" />
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="display:none;">
                <td colspan="2">
                </td>
            </tr>
            <tr style="display:none;">
                <td class="Module" colspan="2">
                    <asp:UpdatePanel runat="server" ID="uoUpdatePanelReimbursement" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ListView runat="server" ID="uoReimbursementList" OnItemCommand="uoReimbursementList_ItemCommand">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                            <th>
                                                Reimbursement
                                            </th>
                                            <th>
                                                Amount
                                            </th>
                                            <th>
                                                Currency
                                            </th>
                                            <th runat="server" id="uoEditTh" style="width: 10%">
                                            </th>
                                            <th runat="server" id="uoDeleteTH" style="width: 10%">
                                            </th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="leftAligned" visible='<%# (!Convert.ToBoolean(ViewState["ReimbursementEditable"])) %>'>
                                            <a runat="server" class="ReimbursementLinkView" id="A1" href='<%# "~/Finance/ReimbursementView.aspx?rId=" + Eval("colReimbursementIdInt") + "&sfId=" + 
                                            Request.QueryString["sfId"] + "&mReqId=" + Request.QueryString["manualReqID"] + 
                                            "&tReqId=" + Request.QueryString["trID"] + "&cId=" + ViewState["CurrencyId"] + 
                                            "&cName=" + ViewState["CurrencyName"]%>'>
                                                <%# Eval("colReimbursementNameVarchar") %>
                                            </a>
                                        </td>
                                        <td class="leftAligned">
                                            <%# String.Format("{0:0.00}", Eval("colAmountMoney"))%>
                                        </td>
                                        <td class="leftAligned">
                                            <%# Eval("colCurrencyName") %>
                                        </td>
                                        <td class="leftAligned" visible='<%# (!Convert.ToBoolean(ViewState["ReimbursementEditable"])) %>'>
                                            <a runat="server" class="ReimbursementLink" id="uoAEditRegion" href='<%# "~/Finance/ReimbursementAdd.aspx?rId=" + Eval("colReimbursementIdInt") + "&sfId=" + 
                                            Request.QueryString["sfId"] + "&mReqId=" + Request.QueryString["manualReqID"] + 
                                            "&tReqId=" + Request.QueryString["trID"] + "&cId=" + ViewState["CurrencyId"] + 
                                            "&cName=" + ViewState["CurrencyName"]%>' visible='<%# (Convert.ToBoolean(uoHyperLinkReimbursementAdd.Visible)) %>'>
                                                Edit </a>
                                        </td>
                                        <td class="leftAligned" visible='<%# (!Convert.ToBoolean(ViewState["ReimbursementEditable"])) %>'>
                                            <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandArgument='<%# Eval("colReimbursementIdInt") %>'
                                                Text="Delete" CommandName="Delete" OnClientClick="return confirmDelete();" Visible='<%# (Convert.ToBoolean(uoHyperLinkReimbursementAdd.Visible)) %>'>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                            <th>
                                                Reimbursement Name
                                            </th>
                                            <th>
                                                Amount
                                            </th>
                                            <th>
                                                Currency
                                            </th>
                                        </tr>
                                        <tr>
                                            <td class="leftAligned" colspan="5">
                                                No Record
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <asp:DataPager ID="uoReimbursementListPager" runat="server" PagedControlID="uoReimbursementList"
                                PageSize="10" OnPreRender="uoReimbursementListPager_PreRender">
                                <Fields>
                                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                                    <%-- <asp:NextPreviousPagerField ButtonType="Image"  ButtonCssClass="PagerClass" NextPageImageUrl="~/Images/next.jpg" PreviousPageImageUrl="~/Images/prev.jpg" />--%>
                                </Fields>
                            </asp:DataPager>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoReimbursementListPager" EventName="PreRender" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-left: 3px; padding-right: 3px">
                    <div class="PageTitle">
                        Other</div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td class="Module" colspan="2">
                    <asp:UpdatePanel runat="server" ID="uoUpdatePanelOtherInfo" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ListView runat="server" ID="uoOtherList">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                            <th>
                                                Users
                                            </th>
                                            <th>
                                                Date
                                            </th>
                                            <th>
                                                Barcode Scanned
                                            </th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="leftAligned">
                                            <%# Eval("colUserRoleVarchar") %>
                                        </td>
                                        <td class="leftAligned">
                                            <%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("colProcessDatetime"))%>
                                        </td>
                                        <td class="leftAligned">
                                            <%# Eval("isScanned")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                            <th>
                                                Users
                                            </th>
                                            <th>
                                                Date
                                            </th>
                                            <th>
                                                Barcode Scanned
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="leftAligned">
                                                No Record
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <%--<tr>
                <td colspan="2" style="padding-left: 3px; padding-right: 3px">
                    <table class="PageTitle" width="100%">
                        <tr>
                            <td>
                                Pending Vehicle Booking
                            </td>
                            <td align="right">
                                <asp:Image ID="uoImageVehiclePending" runat="server" ImageUrl="~/Images/box_up.png" />
                                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender_VehiclePending" runat="Server"
                                    TargetControlID="uoPanelVehiclePending" CollapsedSize="0" ExpandControlID="uoImageVehiclePending"
                                    CollapseControlID="uoImageVehiclePending" AutoCollapse="False" AutoExpand="False"
                                    ImageControlID="uoImageVehiclePending" ExpandedImage="~/images/box_up.png" CollapsedImage="~/images/box_down.png"
                                    ExpandDirection="Vertical" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="Module">
                    <asp:Panel ID="uoPanelVehiclePending" runat="server">
                        <asp:ListView runat="server" ID="uoListviewVehicleTravelPending" OnItemCommand="uoListviewVehicleTravelPending_ItemCommand"
                            OnItemDeleting="uoListviewVehicleTravelPending_ItemDeleting">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>                                      
                                        <th>
                                            Company
                                        </th>
                                        <th>
                                            Vehicle Type
                                        </th>
                                        <th>
                                            Pick-up Point
                                        </th>
                                        <th>
                                            Pick-up Date/Time
                                        </th>
                                        <th>
                                            Destination
                                        </th>
                                        <th>
                                            Status
                                        </th>
                                        <th runat="server" id="EditTH">
                                        </th>
                                        <th runat="server" id="DeleteTH">
                                        </th>
                                        <th runat="server" id="ApproveTH">
                                        </th>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="leftAligned">
                                        <%# Eval("company")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("vehicletype")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("pickuppoint")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format(DateFormat(Eval("IsDateTime")),Eval("pickupdatetime"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("destination")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("status")%>
                                    </td>
                                    <td class='<%# (HideVehicle()==""?"leftAligned":HideVehicle()) %>'>
                                        <asp:HyperLink Enabled='<%# !InactiveControl( bool.Parse("true"), Eval("colBranchIDInt"),"VE") %>'
                                            CssClass='<%# (InactiveControl( bool.Parse("true"), Eval("colBranchIDInt"),"VE")== true?"":"VehicleLinkPending") %>'
                                            runat="server" ID="uoHyperLinkVehicleEdit" NavigateUrl='<%# "~/Vehicle/VehicleEditor.aspx?pendingID="+ Eval("VehiclePendingID") +"&vID="+ Eval("colTransVehicleIDBigInt") +"&sfId=" + Request.QueryString["sfId"] + "&st=" + Request.QueryString["st"] + "&recloc=" + Request.QueryString["recloc"] + "&ID=" + Eval("colIdBigint") + "&SN=" + Eval("colSeqNoInt") + "&manualReqID=" + Request.QueryString["manualReqID"] + "&trID=" + Request.QueryString["trID"] %>'>
                                    Edit</asp:HyperLink>
                                    </td>
                                    <td class='<%# (HideVehicle()==""?"leftAligned":HideVehicle()) %>'>
                                        <asp:LinkButton ID="uoLinkButtonDelete" runat="server" Enabled='<%# !InactiveControl( bool.Parse("true"), Eval("colBranchIDInt"),"VE") %>'
                                            OnClientClick='<%# (InactiveControl( bool.Parse("True"), Eval("colBranchIDInt"),"VE")== true? "return false":"return confirmDelete();") %>'
                                            CommandArgument='<%#Eval("VehiclePendingID") %>' CommandName="Delete">Delete</asp:LinkButton>
                                    </td>
                                    <td class='<%# (HideVehiclePending()==""?"leftAligned":HideVehiclePending()) %>'>
                                        <asp:LinkButton ID="uoLinkButtonApprove" runat="server" OnClientClick='return confirmApprove();'
                                            CommandArgument='<%#Eval("VehiclePendingID") %>' CommandName="Approve">Approve</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>                                        
                                        <th style="width: 180px">
                                            Name
                                        </th>
                                        <th>
                                            Company
                                        </th>
                                        <th>
                                            Vehicle Type
                                        </th>
                                        <th>
                                            Pick-up Point
                                        </th>
                                        <th>
                                            Pick-up Date/Time
                                        </th>
                                        <th>
                                            Destination
                                        </th>
                                        <th>
                                            Status
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="7" class="leftAligned">
                                            No Record
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </td>
            </tr>
           --%>
            <tr>
                <td align="right">
                    <asp:HiddenField ID="uoHdRecLoc" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="6" style="padding-right: 3px;">
                    <asp:Button ID="uobuttonBack" runat="server" Text="Previous Page" OnClick="uobuttonBack_Click"
                        Font-Size="X-Small" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:HiddenField ID="uoHdSfStatus" runat="server" />
                    <asp:HiddenField ID="uoHiddenFieldPopupPort" runat="server" Value="0" />
                    <asp:HiddenField ID="uoHiddenFieldPopupHotel" runat="server" Value="0" />
                    <asp:HiddenField ID="uoHiddenFieldPopupHotelPending" runat="server" Value="0" />
                    <asp:HiddenField runat="server" ID="uoHiddenFieldPopEditor" Value="0" />
                    <asp:HiddenField ID="uoHiddenFieldPopupVehicle" runat="server" Value="0" />
                    <asp:HiddenField ID="uoHiddenFieldPopupVehiclePending" runat="server" Value="0" />
                    <asp:HiddenField ID="uoHiddenFieldPopupAir" runat="server" Value="0" />
                    <asp:HiddenField ID="uoHiddenFieldPopupReimbursement" runat="server" />
                    
                    <asp:HiddenField ID="uoHiddenFieldRoleBranchID" runat="server" Value="0" />
                    <asp:HiddenField ID="uoHiddenFieldReimbursementId" runat="server" />
                    <asp:HiddenField ID="uoHiddenFieldDateOn" runat="server" />
                    <asp:HiddenField ID="uoHiddenFieldDateOff" runat="server" />
                    <asp:HiddenField ID="uoHiddenFieldIsExistInTR" runat="server" Value="1" />
                    <asp:HiddenField ID="uoHiddenPrevPage" runat="server" EnableViewState="true" />
                    <asp:HiddenField ID="uoHiddenFieldLatestRemarksID" runat="server" EnableViewState="true" /> 
                    <asp:HiddenField ID="uoHiddenFieldPopupRemarks" runat="server" Value="0" /> 
                    
                    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />
                    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
                    <asp:HiddenField ID="uoHiddenFieldTRID" runat="server" Value="" />
                    <asp:HiddenField ID="uoHiddenFieldIDBigint" runat="server" Value="" />
                    
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ObjectDataSource ID="ObjectDataSourceReimbursement" runat="server" EnablePaging="True"
                        OldValuesParameterFormatString="oldCount_{0}" SelectCountMethod="SelectReimbursementListbySeafarerCount"
                        SelectMethod="SelectReimbursementListbySeafarer" TypeName="TRAVELMART.BLL.FinanceBLL"
                        DeleteMethod="DeleteSeafarerReimbursement">
                        <DeleteParameters>
                            <asp:ControlParameter ControlID="uoHiddenFieldReimbursementId" Name="ReimbursementId"
                                PropertyName="Value" />
                            <asp:ControlParameter ControlID="uoHiddenFieldUser" Name="UserId" PropertyName="Value" />
                        </DeleteParameters>
                        <SelectParameters>
                            <asp:QueryStringParameter Name="SeafarerId" QueryStringField="sfId" />
                            <asp:QueryStringParameter Name="mReqId" QueryStringField="manualReqID" />
                            <asp:QueryStringParameter Name="tReqId" QueryStringField="trID" />
                            <asp:ControlParameter ControlID="uoHiddenFieldIsExistInTR" ConvertEmptyStringToNull="false"
                                DbType="String" Name="E1TRId" PropertyName="Value" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
   <script type="text/javascript" language="javascript">
       $(document).ready(function() {

           var a = $("#ctl00_HeaderContent_HiddenFieldPriority").val();
           if (a == "false") {

               $("#uoTableAir td:nth-child(12)").hide();
           }
           else {
               $("#uoTableAir td:nth-child(12)").show();
           }
       });
   </script>
    
    <%--<script type="text/javascript" language="javascript">
      
            $(".deleteRow").each(function() {
                    var btn = $(this).find(".deleteCommand");
                    
                    $(this).find(".deleteButton").click(function(e) {
                        e.preventDefault();
                        var msg = $(this).attr("title")
         
                        confirm("Are you sure you want to " + msg + "?", function() {
                            deleteRow(btn);
                        });
                    });
                });
            });
         
            function deleteRow(args) {
                $(args).click();
                alert("The row would of been deleted.");
                return false;
            }
         
            function confirm(message, callback) {
                $('#confirm').modal({
                    close:false, 
                    overlayId:'confirmModalOverlay',
                    containerId:'confirmModalContainer', 
                    onShow: function (dialog) {
                        dialog.data.find('.message').append(message);
         
                        // if the user clicks "yes"
                        dialog.data.find('.yes').click(function () {
                            // call the callback
                            if ($.isFunction(callback)) {
                                callback.apply();
                            }
                            // close the dialog
                            $.modal.close();
                        });
                    }
                });
             }
    </script>--%>
     
</asp:Content>
