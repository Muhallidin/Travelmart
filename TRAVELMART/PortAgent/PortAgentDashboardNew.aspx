<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true" CodeBehind="PortAgentDashboardNew.aspx.cs" 
Inherits="TRAVELMART.PortAgentDashboardNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" style="height:25px">
        <tr class="PageTitle">
            <td align="left" style="vertical-align:middle; white-space:nowrap; width: 150px;">
                <asp:Label ID="uoLabelTitle" runat="server" class="Title" Text="Vendor Dashboard"></asp:Label>
            </td>           
            <td align="right">
                Seaport :
                <asp:DropDownList runat="server" ID ="uoDropDownListSeaport" 
                    AppendDataBoundItems="true" Width="300px" AutoPostBack="true"
                    onselectedindexchanged="uoDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right" style="white-space: nowrap; width: 300px; padding-left:5px;">  
                Port Agent :             
                <asp:DropDownList runat="server" ID ="uoDropDownListPortAgent" 
                    AppendDataBoundItems="true" Width="300px" AutoPostBack="true"
                    onselectedindexchanged="uoDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>             
            <td align="right" style="white-space: nowrap; width: 80px; padding-left:5px;">
                Days : 
                <asp:DropDownList runat="server" ID ="uoDropDownListDays" 
                    AppendDataBoundItems="true" Width="80px" AutoPostBack="true"
                    onselectedindexchanged="uoDropDownList_SelectedIndexChanged">
                </asp:DropDownList>       
            </td>
        </tr>        
    </table>    
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    
    <div style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto; padding-top:5px ;display:none" >
        <asp:HyperLink ID="uoHyperLinkDashboard" runat="server">Non Turn Port Exception</asp:HyperLink>
    </div>
    
    <div id="uoDivDashboard" style="overflow:auto; overflow-x:auto; overflow-y:auto; width:760px ">
          <asp:ListView runat="server" ID="uoListViewHotelCount" OnItemDataBound="uoListViewDashboard_ItemDataBound" >
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="cssTblDashboard"  width="100%">
                        <tr style="background-color:Gray; border-bottom-style:solid;border-width:thin; ">
                            <th style="text-align: Left; white-space: normal; width:100px;   ">
                                Service Provider Name
                            </th >
                            <th style="text-align: Left; white-space: normal; width:100px; padding-left:6px;">
                                Hotel 
                            </th >
                            <th style="text-align: Left; white-space: normal; width:100px; padding-left:6px;">
                                Transportation
                            </th >
                        </tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <th style="text-align: Left; white-space: normal; width:100px;" >
                          <%--  <%--<asp:Label runat="server" ID="lblServiceProvider"><%# Eval("VendorName")%></asp:Label> --%>
                           
                        <%--  
                          href="../PortAgent/PortAgentHotelManifest.aspx?ufn=Muhallidin as Hotel Specialist&amp;dt=11/14/2016 12:00:00 AM&amp;PA=30&amp;st=4&amp;Dy=60&amp;LT=0&amp;PAName=BC Agency (AGP)" style="color:DarkGreen;"
                          
                         --%> 
                          
                          
                          
                            <%--<asp:LinkButton runat="server" ID="lbnServiceProvider" ForeColor="Black" 
                                href='<%# "../PortAgent/PortAgentConfirmCancelManifest.aspx?ufn=" + Request.QueryString["ufn"] + 
                                    "&dt=" + Eval("StartDate") + "&PA=" + Eval("PortAgentID") + "&st=4&Dy=" + Eval("Days") + "&LT=0&PAName=" + Eval("VendorName") %>' 
                                 style="text-decoration:none ; color:#1E90FF;" Text='<%# Eval("VendorName")%>'> 
                                 
                            </asp:LinkButton>--%>
                            <asp:LinkButton runat="server" ID="lbnServiceProvider" ForeColor="Black" 
                                href="#"  Text='<%# Eval("VendorName")%>' 
                                OnClientClick='<%# "return OpenContract(\""+ Eval("PortAgentID") + "\",\"" + Eval("ContractID") + "\");"  %>' 
                                 style="text-decoration:none ; color:#1E90FF;" >
                                
                                
                            </asp:LinkButton>
                            
                            
                            <%--OnClientClick = '<%# "return OpenContract(\""+ Eval("PortAgentID") + "\",\"" + Eval("ContractID") + "\");"  %>' Visible='<%# Convert.ToBoolean(Eval("IsWithContract")) %>' >
--%>                           <%-- <br />
                            <a href='#' style="text-decoration:none"><%# Eval("VendorName")%></a>--%>
                            
                            <asp:HiddenField ID="uoHiddenFieldPortAgentID" runat="server" value='<%# Eval("PortAgentID")%>'/>
                            <asp:HiddenField ID="uoHiddenFieldStartDate" runat="server" value='<%# Eval("StartDate")%>'/>
                            <asp:HiddenField ID="uoHiddenFieldPortID" runat="server" value='<%# Eval("PortID")%>'/>
                            <asp:HiddenField ID="uoHiddenFieldPortCode" runat="server" value='<%# Eval("PortCode")%>'/>
                            <asp:HiddenField ID="uoHiddenFieldPortName" runat="server" value='<%# Eval("PortName")%>'/>
                        </th> 
                        <th style="text-align: Left; white-space: normal; width:100px; padding:6px;">
                        
                         <asp:Label runat="server" style="text-align:left" ID="lblRequest" ><%# Eval("Request")%></asp:Label>
                            <asp:LinkButton runat="server" ID="lbnRequestHotel" ForeColor="Red" 
                                   href='<%# "../Hotel/HotelNonTurnPorts2.aspx?ufn=" +  Request.QueryString["ufn"] + 
                                        "&dt=" + Eval("StartDate") + "&PA=" + Eval("PortAgentID") + 
                                        "&st=1&Dy=" + Eval("Days") + "&LT=0&PAName=" + Eval("VendorName").ToString().Replace("&","%26") %>' 
                                   Text='<%# Eval("HotelRequestCount")%>'> 
                            </asp:LinkButton>
                             
                            
                            <br />
                         
                             <%-- 
                           PortAgent/PortAgentHotelManifest.aspx?pid=209&ufn=Muhallidin%20as%20Service%20Provider&dt=02/03/2017
                             <%-- <asp:LinkButton runat="server" ID="lbnConfirmedConfirmed" ForeColor="DarkGreen" 
                                    href='<%# "../Hotel/HotelNonTurnPorts2.aspx?ufn=" +  Request.QueryString["ufn"] + 
                                    "&dt=" + Eval("StartDate") + "&PA=" + Eval("PortAgentID") + 
                                    "&st=4&Dy=" + Eval("Days") + "&LT=0&PAName=" + Eval("VendorName").ToString().Replace("&","%26")   %>' 
                                    Text='<%# Eval("HotelConfirmedCount")%>'> --%>
                               <asp:Label runat="server" style="text-align:left" ID="lblConfirmed" ><%# Eval("Confirmed")%></asp:Label>        
                            <asp:LinkButton runat="server" ID="lbnConfirmedConfirmed" ForeColor="DarkGreen" 
                                    href='<%# "../Hotel/HotelNonTurnPorts2.aspx?ufn=" +  Request.QueryString["ufn"] + 
                                    "&dt=" + Eval("StartDate") + "&PA=" + Eval("PortAgentID") + "&st=4&Dy=" + Eval("Days") 
                                    + "&LT=0&PAName=" + Eval("VendorName").ToString().Replace("&","%26")   %>' 
                                    Text='<%# Eval("HotelConfirmedCount")%>'>  
                                    
                           
                            </asp:LinkButton>
                            <br />
                            <asp:Label runat="server" style="text-align:left" ID="lblPending" ><%# Eval("Pending")%></asp:Label>
                          
                            <asp:LinkButton runat="server" ID="lbnPendingHotel" ForeColor="Orange" 
                                    href='<%# "../Hotel/HotelNonTurnPorts2.aspx?ufn=" +  Request.QueryString["ufn"] + 
                                    "&dt=" + Eval("StartDate") + "&PA=" + Eval("PortAgentID") + "&st=3&Dy=" + Eval("Days") + 
                                    "&LT=0&PAName=" + Eval("VendorName").ToString().Replace("&","%26") %>' 
                                    Text='<%# Eval("HotelPendingCount")%>'> 
                                    
                            </asp:LinkButton>
                        </th>
                        <th style="text-align: Left; white-space: normal; width:100px; padding:6px;">
                            <asp:Label runat="server" style="text-align:left" ID="Label4" ><%# Eval("Request")%></asp:Label> 
                            <asp:LinkButton runat="server" ID="lbnRequestTrans" ForeColor="Red" 
                            
                                        href='<%# "../PortAgent/PortAgentApproveVehicleRequest.aspx?ufn=" +  Request.QueryString["ufn"] + 
                                            "&dt=" + Eval("StartDate") + "&PA=" + Eval("PortAgentID") + "&st=1&Dy=" + Eval("Days") + 
                                            "&LT=0&PAName=" + Eval("VendorName").ToString().Replace("&","%26") %>'
                            
                                Text='<%# Eval("VehicleRequestCount")%>' >
                            </asp:LinkButton>
                            <br />
                           
                            <asp:Label runat="server" style="text-align:left" ID="Label5" ><%# Eval("Confirmed")%></asp:Label>
                            <asp:LinkButton runat="server" ID="lbnConfirmedTrans" ForeColor="DarkGreen"
                                   href='<%# "../PortAgent/PortAgentApproveVehicleRequest.aspx?ufn=" +  Request.QueryString["ufn"] + 
                                            "&dt=" + Eval("StartDate") + "&PA=" + Eval("PortAgentID") + 
                                            "&st=4&Dy=" + Eval("Days") + "&LT=0&PAName=" + Eval("VendorName").ToString().Replace("&","%26") %>' 
                                   Text='<%# Eval("VehicleConfirmedCount")%>'>
                            </asp:LinkButton>
                            <br />
                        <%--    <asp:LinkButton runat="server" ID="LinkButton1" ForeColor="DarkGreen"
                                   href='<%# "../PortAgent/PortAgentVehicleManifest2.aspx?ufn=" +  Request.QueryString["ufn"] + 
                                            "&dt=" + Eval("StartDate") + "&PA=" + Eval("PortAgentID") + 
                                            "&st=4&Dy=" + Eval("Days") + "&LT=0&PAName=" + Eval("VendorName").ToString().Replace("&","%26") %>' 
                                   Text='<%# Eval("VehicleConfirmedCount")%>'>
                            </asp:LinkButton>--%>
                            
                            <%--  
                                href='<%# "../PortAgent/PortAgentApproveVehicleRequest.aspx?ufn=" +  Request.QueryString["ufn"] + 
                                "&dt=" + Eval("StartDate") + "&PA=" + Eval("PortAgentID") + "&st=3&Dy=" + Eval("Days") + 
                                "&LT=0&PAName=" + Eval("VendorName") %>' 
                            --%>
                    
                            <asp:Label runat="server" style="text-align:left" ID="Label6" ><%# Eval("Pending")%></asp:Label>
                            <asp:LinkButton runat="server" ID="lbnPendingTrans" ForeColor="Orange" 
                                     href='<%# "../PortAgent/PortAgentApproveVehicleRequest.aspx?ufn=" +  Request.QueryString["ufn"] + 
                                            "&dt=" + Eval("StartDate") + "&PA=" + Eval("PortAgentID") + "&st=2&Dy=" + Eval("Days") + 
                                            "&LT=0&PAName=" + Eval("VendorName").ToString().Replace("&","%26") %>'
                                    Text='<%# Eval("VehiclePendingCount")%>'>
                            </asp:LinkButton>
                            
                        </th>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="cssTblDashboard"  width="100%">
                         <tr style="background-color:Gray; border-bottom-style:solid;border-width:thin; ">
                            <th style="text-align: Left; white-space: normal; width:100px;   ">
                                Service Provider Name
                            </th >
                            <th style="text-align: Left; white-space: normal; width:100px; padding-left:6px;">
                                Hotel 
                            </th >
                            <th style="text-align: Left; white-space: normal; width:100px; padding-left:6px;">
                                Transportation
                            </th >
                        </tr>
                        <tr >
                         
                             <th colspan="3" style="text-align: Left; white-space: normal; width:100px; padding:6px;">
                                no record
                             </th>
                           
                        </tr> 
                    </table>
                </EmptyDataTemplate>
          </asp:ListView>
           
    </div>
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" value=''/>
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" value='Hotel Specialist'/>
    <asp:HiddenField ID="uoHiddenFieldPortCode" runat="server" value=''/>
    <asp:HiddenField ID="uoHiddenFieldRequestDate" runat="server" value=''/>
     <asp:HiddenField ID="uoHiddenFieldPortAgentID" runat="server" value='Hotel Specialist'/>
    <script type="text/javascript" language="javascript">
    
        Sys.Application.add_load(function() {
            SetDashboardResolution();
            HideDashboardLink();
        });

        $(document).ready(function() {            
            HideDashboardLink();
        });
        
        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                            
                HideDashboardLink();
                
            }
        }

        function GetURLFunction(url) {
            console.log(url);
            
        }

        function SetDashboardResolution() {
        
            var ht = $(window).height();  
            var wd = $(window).width() * 0.90;
            var screenH = screen.height;
            var percent = 0.55;

            if (screenH <= 600) {
                //alert('less 600');
                ht = ht * 0.35;
            }
            else if (screenH <= 720) {
                //alert('less 720')
                ht = ht * 0.49;
            }
            else {
                if (screenH = 768) {
                    percent = (540 - (screenH - ht)) / ht;
                }
                ht = ht * percent;
            }
            
            $("#uoDivDashboard").height(ht);
            $("#uoDivDashboard").width(wd * 0.50);

            console.log(document.getElementById("uoDivDashboard").offsetWidth);
            
            
        }

        function HideDashboardLink() {
        
            console.log(document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRole").value);
        
            if (document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRole").value != 'Hotel Specialist') {
                document.getElementById("ctl00_NaviPlaceHolder_uoHyperLinkDashboard").style.visibility = 'hidden';
            }
            
            console.log(document.getElementById("ctl00_NaviPlaceHolder_uoHyperLinkDashboard").style.visibility);
      
            
        }
        function OpenContract(branchID, contractID) {
            var URLString = "../ContractManagement/portAgentContractView.aspx?";
            URLString += "bId=" + branchID + "&cId=" + contractID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
        
    </script>
</asp:Content>
