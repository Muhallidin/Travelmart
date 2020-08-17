<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true" CodeBehind="ServiceRequestView.aspx.cs" Inherits="TRAVELMART.ServiceRequestView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
     <div class="ViewTitlePadding">
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>
                    Service Request List
                </td>                
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <script type="text/javascript" language="javascript">
        function CloseModal(strURL) {
            window.location = strURL;
        }
        
        $(document).ready(function() {
            SetTRResolution();
            ControlSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
                ControlSettings();
            }
        }
        function SetTRResolution() {
            var ht = $(window).height() * 0.95;
            var ht2 = $(window).height() * 0.9;
            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.20;
                ht2 = ht2 * 0.20;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.39;
                ht2 = ht2 * 0.39;
            }
            else {
                ht = ht * 0.47;
                ht2 = ht2 * 0.61;
            }
            $("#Av").width(wd);
            $("#Bv").height(ht);
            $("#Bv").width(wd);
            $("#PG").height(ht2);
            $("#PG").width(wd);
        }

        function divScrollLManifest() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;
        }
        
        function ControlSettings() {
            $(".LinkSendEmail").fancybox({
            'centerOnScroll': false,
            'width': '70%',
            'height': '90%',
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopUp.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });
        }
        function validate(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;

            if (keycode >= 48 && keycode <= 57) {
                return true;
            }
            else {
                return false;
            }

            ////getting key code of pressed key
            //var keycode = (key.which) ? key.which : key.keyCode;
            //if ((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) {
            //    return false;
            //}
            //else {
            //    return true;
            //}
        }
    </script>
    <div>
        <table width="100%" class="LeftClass">
            <tr>               
                <td class="caption">
                    Filter Type:
                </td>
                <td class="value">
                    <%--<asp:CheckBox ID="uoCheckBoxViewAll" runat="server" Checked="false"/>--%>
                    <asp:DropDownList ID="uoDropDownListFilterType" runat="server" Width="300px">
                        <asp:ListItem Value="0" Text="View By Date Created/Modified"> </asp:ListItem>                        
                        <asp:ListItem Value="1" Text="View By Sign On/Off Date"> </asp:ListItem>
                        <asp:ListItem Value="2" Text="View All Regardless of Date"> </asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="caption">Employee ID:</td>
                <td class="value">
                    <asp:TextBox ID="uoTextBoxEmployeeID" runat="server" Width="190px" onkeypress="return validate(event);" ></asp:TextBox>
                </td>
                <td class="caption">Crew Assist:</td>
                <td class="value">
                    <asp:DropDownList ID="uoDropDownListCrewAssist" runat="server" Width="250px" AppendDataBoundItems="true"></asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="caption">
                      Service Request Filters:
                </td>
                <td class="value">                  
                    <asp:DropDownList ID="uoDropDownListRequest" runat="server" Width="300px">
                        <asp:ListItem Value="0" Text="View All Service Request"> </asp:ListItem>
                        <asp:ListItem Value="1" Text="View with Hotel Request"> </asp:ListItem>
                        <asp:ListItem Value="2" Text="View with Vehicle Request"> </asp:ListItem>
                        <asp:ListItem Value="3" Text="View with Meet & Greet Request"> </asp:ListItem>
                        <asp:ListItem Value="4" Text="View with Port Agent Request"> </asp:ListItem>
                    </asp:DropDownList>
                </td> 
                <td class="caption">
                    Active:
                </td>                                    
                <td class="value" >                    
                    <asp:DropDownList ID="uoDropDownListActive" runat="server" Width="200px">
                        <asp:ListItem Value="0" Text="View All"> </asp:ListItem>
                        <asp:ListItem Value="1" Text="View Active"> </asp:ListItem>
                        <asp:ListItem Value="2" Text="View Inactive"> </asp:ListItem>
                    </asp:DropDownList>
                </td>    
                <td class="caption">
                    Booked:
                </td>                                 
                <td class="value" >                    
                    <asp:DropDownList ID="uoDropDownListBooked" runat="server" Width="250px">
                        <asp:ListItem Value="0" Text="View All"> </asp:ListItem>
                        <asp:ListItem Value="1" Text="View Booked"> </asp:ListItem>
                        <asp:ListItem Value="2" Text="View Pending"> </asp:ListItem>
                    </asp:DropDownList>
                </td> 
                <td class="LeftClass">
                    <asp:Button ID="uoButtonView" runat="server" Text="View" CssClass="SmallButton" Width="100px"
                        onclick="uoButtonView_Click" />
                </td>  
            </tr>
            <tr>
                <td></td>                
                <td>
                    <asp:Button ID="uoButtonExport" runat="server" Text="Export" 
                        CssClass="SmallButton" Width="100px" onclick="uoButtonExport_Click" />
                    &nbsp
                    <asp:Button ID="uoButtonSave" runat="server" Text="Save" CssClass="SmallButton" Width="100px"
                    onclick="uoButtonSave_Click" />
                </td>
                <td colspan="5"></td>
            </tr>
        </table>
    </div>
    
    <div id="PG" style="width: auto; height: auto; overflow: auto;">
        <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
            <asp:ListView runat="server" ID="uoListViewServiceRequestHeader" 
                onitemcommand="uoListViewServiceRequestHeader_ItemCommand" >
                <LayoutTemplate>
                </LayoutTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="listViewTable">
                        <tr>  
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblEmpIDHeader" runat="server" CommandName="SortByEmployeeID"
                                    Text="Employee ID" Width="65px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblLastNameHeader" runat="server" CommandName="SortByLastName"
                                    Text="Last Name" Width="135px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="uoLblFirstNameHeader" runat="server" CommandName="SortByFirstName"
                                    Text="First Name" Width="125px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByOnOffdate"
                                    Text="OnOffDate" Width="83px" />
                            </th>
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton12" runat="server" CommandName="SortByVessel"
                                    Text="Vessel" Width="196px" />
                            </th>   
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByStatus"
                                    Text="Status" Width="42px" />
                            </th>                              
                            
                            <%--Hotel--%>                    
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="SortByIsWithHotel"
                                    Text="With Hotel Req" Width="65px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton8" runat="server" CommandName="SortByIsHotelActive"
                                    Text="Active Hotel Req" Width="63px" />
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByIsHotelBook"
                                    Text="Hotel Req Booked" Width="63px" />
                            </th>
                            
                             <th style="text-align: center; white-space: normal;">
                                 <asp:Label runat="server" ID="uoLabel" Text="Resend Email" Width="60px"></asp:Label>
                             </th>
                            <%--Vehicle--%>
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByIsWithVehicle"
                                    Text="With Vehicle Req" Width="65px" />
                            </th>                            
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByIsVehicleActive"
                                    Text="Active Vehicle Req" Width="63px" />
                            </th>                                                        
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton7" runat="server" CommandName="SortByIsVehicleBook"
                                    Text="Vehicle Req Booked" Width="63px" />
                            </th>
                            
                             <%--Service Provider--%>
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton13" runat="server" CommandName="SortByIsWithPortAgent"
                                    Text="With Port Agent Req" Width="70px" />
                            </th>                            
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton14" runat="server" CommandName="SortByIsPortAgentActive"
                                    Text="Active Port Agent Req" Width="70px" />
                            </th>                                                        
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton15" runat="server" CommandName="SortByIsPortAgentBook"
                                    Text="Port Agent Req Booked" Width="70px" />
                            </th>
                            
                            <%--Meet & Greet--%>
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton9" runat="server" CommandName="SortByIsWithMeetGreet"
                                    Text="With Meet&Greet Req" Width="70px" />
                            </th>                            
                            <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton10" runat="server" CommandName="SortByIsMeetGreetActive"
                                    Text="Active Meet&Greet Req" Width="70px" />
                            </th>                                                        
                             <th style="text-align: center; white-space: normal;">
                                <asp:LinkButton ID="LinkButton11" runat="server" CommandName="SortByIsMeetGreetBook"
                                    Text="Meet&Greet Req Booked" Width="70px" />
                            </th>
                                   
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label ID="uoTextBoxCreatedByHotel" runat="server"  Text="Hotel Requested By" Width="150px" />
                            </th>
                            
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label ID="TextBox1" runat="server"  Text="Vehicle Requested By" Width="150px" />
                            </th>
                            
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label ID="TextBox2" runat="server"  Text="Service Provider Requested By" Width="150px" />
                            </th>
                                         
                                                                            
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label2" Text="" Width="8px"></asp:Label>
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
        <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;" onscroll="divScrollLManifest();">
            <asp:ListView runat="server" ID="uoListViewServiceRequest" onpagepropertieschanging="uoListViewServiceRequest_PagePropertiesChanging"
                DataSourceID="uoObjectDataSourceServiceRequest" 
                ondatabound="uoListViewServiceRequest_DataBound" 
                onitemcommand="uoListViewServiceRequest_ItemCommand" >
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                   <%-- <%# HotelAddGroup()%>--%>
                    <tr>   
                         <td class="leftAligned" style="white-space: normal;">
                            <asp:HiddenField ID="uoHiddenFielReqIdHotel" runat="server" Value='<%# Eval("HotelRequestIDBigint") %>'/>
                            <asp:HiddenField ID="uoHiddenFielReqIdVehicle" runat="server" Value='<%# Eval("VehicleRequestIDBigint") %>' />
                            <asp:HiddenField ID="uoHiddenFielReqIdMeetGreet" runat="server" Value='<%# Eval("MeetGreetRequestIDBigint") %>'/>
                            
                            <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("SeafarerIdInt")%>' Width="70px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <%--<asp:Label runat="server" ID="uoLabelLast" Text='<%# Eval("LastName")%>' Width="138px"></asp:Label>--%>
                            <asp:HyperLink Width="140px" ID="uoHyperLinkName" NavigateUrl='<%# "CrewAssist/CrewAssist.aspx?sfId=" + Eval("SeafarerIdInt") + "&hrID=" + Eval("HotelRequestIDBigint") + "&trID=" + Eval("TravelReqIDInt") + "&App=2" + "&dt=" + Eval("SignOnOffDate") + "&as=" + Eval("AirSeqNo") + "&trp=" + Eval("VehicleRequestIDBigint") + "&magp=" + Eval("MeetGreetRequestIDBigint")  %>'
                                runat="server"> <%# Eval("LastName")%></asp:HyperLink>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <%--<asp:LinkButton Width="135px" runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("SeafarerIdInt") + "&trID=" + Eval("TravelReqIDInt") + "&st=" + Eval("SFStatus") + "&recloc=" +  "&manualReqID=" + Eval("RequestIDInt") + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IDBigInt") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("FirstName") %></asp:LinkButton>--%>
                            <asp:Label runat="server" ID="uoLabelFirst" Text='<%# Eval("FirstName")%>' Width="133px"></asp:Label>
                        </td>
                         <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label10" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("SignOnOffDate"))%>' Width="90px"></asp:Label>
                        </td>
                         <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label9" Text='<%# Eval("VesselName")%>' Width="200px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label1" Text='<%# Eval("SFStatus")%>' Width="48px"></asp:Label>
                        </td>
                        
                         <%--Hotel--%>                     
                        <td class="centerAligned" style="white-space: normal;">                            
                              <asp:CheckBox ID="uoCheckBoxWithHotelRequest" Checked='<%# Eval("IsWithHotelRequest") %>' runat="server" Width="70px" Enabled="false" />
                        </td>
                        <td class="centerAligned" style="white-space: normal;">
                            <asp:CheckBox ID="uoCheckBoxHotelActive"  Visible='<%# Eval("IsWithHotelRequest") %>' Enabled='<%# !Convert.ToBoolean(Eval("IsHotelRequestBook")) %>' Checked='<%# Eval("IsHotelRequestActive") %>' runat="server" Width="68px" />
                            <asp:Label runat="server" ID="Label5" Visible='<%# !Convert.ToBoolean(Eval("IsWithHotelRequest")) %>' Width="68px"></asp:Label>
                        </td>
                        <td class="centerAligned" style="white-space: normal;">
                            <asp:CheckBox ID="CheckBox2"  Visible='<%# Eval("IsWithHotelRequest") %>' Checked='<%# Eval("IsHotelRequestBook") %>' runat="server" Width="68px"  Enabled="false" />
                            <asp:Label runat="server" ID="Label4" Visible='<%# !Convert.ToBoolean(Eval("IsWithHotelRequest")) %>' Width="68px"></asp:Label>
                        </td>
                        <td class="centerAligned" style="white-space: normal;">
                            <asp:HyperLink Visible='<%# Eval("IsEmailVisible") %>'  CssClass="LinkSendEmail" ID="uoHyperLinkEmailHotel" runat="server" Width="60px" 
                            NavigateUrl='<%# "ServiceRequestEmail.aspx?sfId=" + Eval("SeafarerIdInt") + "&hrID=" + Eval("HotelRequestIDBigint") + "&trID=" + Eval("TravelReqIDInt") + "&App=2" + "&dt=" + Eval("SignOnOffDate") + "&as=" + Eval("AirSeqNo") + "&trp=" + Eval("VehicleRequestIDBigint") + "&name=" + Eval("LastName") + ", " + Eval("FirstName") + "&MAid=" + Eval("MeetGreetRequestIDBigint") + "&PAid=" + Eval("PortAgentRequestIDBigint") %>' 
                            Text="Email"></asp:HyperLink>
                            <asp:Label runat="server" ID="Label6" Visible='<%# !Convert.ToBoolean(Eval("IsEmailVisible")) %>' Width="60px"></asp:Label>
                        </td>
                        
                         <%--Vehicle--%>                                                                       
                        <td class="centerAligned" style="white-space: normal;">                            
                            <asp:CheckBox ID="uoCheckBoxWithVehicleRequest" Checked='<%# Eval("IsWithVehicleRequest") %>' runat="server" Width="70px" Enabled="false" />
                        </td>                                                
                       <td class="centerAligned" style="white-space: normal;">
                            <asp:CheckBox ID="uoCheckBoxVehicleActive" Visible='<%# Eval("IsWithVehicleRequest") %>' Enabled='<%# !Convert.ToBoolean(Eval("IsVehicleRequestBook")) %>' Checked='<%# Eval("IsVehicleRequestActive") %>' runat="server" Width="70px" />
                            <asp:Label runat="server" ID="Label3" Visible='<%# !Convert.ToBoolean(Eval("IsWithVehicleRequest")) %>' Width="70px"></asp:Label>
                        </td>                                            
                        <td class="centerAligned" style="white-space: normal;">
                            <asp:CheckBox ID="CheckBox1" Visible='<%# Eval("IsWithVehicleRequest") %>' Checked='<%# Eval("IsVehicleRequestBook") %>' runat="server" Width="70px"  Enabled="false" />
                            <asp:Label runat="server" ID="Label11" Visible='<%# !Convert.ToBoolean(Eval("IsWithVehicleRequest")) %>' Width="70px"></asp:Label>
                        </td>
                       
                        <%--Port Agent--%>                                                                       
                        <td class="centerAligned" style="white-space: normal;">                            
                            <asp:CheckBox ID="CheckBox4" Checked='<%# Eval("IsWithPortAgentRequest") %>' runat="server" Width="75px" Enabled="false"  />
                        </td>                                                
                       <td class="centerAligned" style="white-space: normal;">
                            <asp:CheckBox ID="CheckBox6" Visible='<%# Eval("IsWithPortAgentRequest") %>' Enabled='<%# !Convert.ToBoolean(Eval("IsPortAgentRequestBook")) %>' Checked='<%# Eval("IsPortAgentRequestActive") %>' runat="server" Width="77px" />
                            <asp:Label runat="server" ID="Label12" Visible='<%# !Convert.ToBoolean(Eval("IsWithPortAgentRequest")) %>' Width="77px"></asp:Label>
                        </td>                                            
                        <td class="centerAligned" style="white-space: normal;">
                            <asp:CheckBox ID="CheckBox7" Visible='<%# Eval("IsWithPortAgentRequest") %>' Checked='<%# Eval("IsPortAgentRequestBook") %>' runat="server" Width="75px"  Enabled="false" />
                            <asp:Label runat="server" ID="Label13" Visible='<%# !Convert.ToBoolean(Eval("IsWithPortAgentRequest")) %>' Width="75px"></asp:Label>
                        </td>        
                        
                         <%--Meet & Greet--%>                                                                       
                        <td class="centerAligned" style="white-space: normal;">                            
                            <asp:CheckBox ID="CheckBox3" Checked='<%# Eval("IsWithMeetGreetRequest") %>' runat="server" Width="75px" Enabled="false"  />
                        </td>                                                
                       <td class="centerAligned" style="white-space: normal;">
                            <asp:CheckBox ID="uoCheckBoxMeetGreetActive" Visible='<%# Eval("IsWithMeetGreetRequest") %>' Enabled='<%# !Convert.ToBoolean(Eval("IsMeetGreetRequestBook")) %>' Checked='<%# Eval("IsMeetGreetRequestActive") %>' runat="server" Width="77px" />
                            <asp:Label runat="server" ID="Label7" Visible='<%# !Convert.ToBoolean(Eval("IsWithMeetGreetRequest")) %>' Width="77px"></asp:Label>
                        </td>                                            
                        <td class="centerAligned" style="white-space: normal;">
                            <asp:CheckBox ID="CheckBox5" Visible='<%# Eval("IsWithMeetGreetRequest") %>' Checked='<%# Eval("IsMeetGreetRequestBook") %>' runat="server" Width="75px"  Enabled="false" />
                            <asp:Label runat="server" ID="Label8" Visible='<%# !Convert.ToBoolean(Eval("IsWithMeetGreetRequest")) %>' Width="75px"></asp:Label>
                        </td>  
                        
                        <td style="text-align: center; white-space: normal;">
                            <asp:Label ID="uoTextBoxCreatedByHotel" runat="server"  Text='<%# Eval("HotelRequestCreatedBy") %>' Width="150px" />
                        </td>                        
                        <td style="text-align: center; white-space: normal;">
                            <asp:Label ID="TextBox1" runat="server"  Text='<%# Eval("VehicleRequestCreatedBy") %>' Width="150px" />
                        </td>                        
                        <td style="text-align: center; white-space: normal;">
                            <asp:Label ID="TextBox2" runat="server"  Text='<%# Eval("PortAgentRequestCreatedBy") %>' Width="150px" />
                        </td>                               
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="listViewTable">
                        <tr>
                            <td colspan="33" class="leftAligned">
                                No Record
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div align="left">
                <asp:DataPager ID="uoDataPagerManifest" runat="server" PagedControlID="uoListViewServiceRequest" PageSize="20">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </div>
             <asp:ObjectDataSource ID="uoObjectDataSourceServiceRequest" runat="server" MaximumRowsParameterName="iMaxRow"
            SelectCountMethod="GetServiceRequestCount" SelectMethod="GetServiceRequestList"
            StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.ServiceRequestView"
            OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" >
            <SelectParameters>
                <asp:ControlParameter Name="dDate" ControlID="uoHiddenFieldDate" />
                <asp:ControlParameter Name="sUser" ControlID="uoHiddenFieldUser" />
                <asp:ControlParameter Name="iLoad" ControlID="uoHiddenFieldLoadType" />
                <asp:ControlParameter Name="sOrderBy" ControlID="uoHiddenFieldSortBy" />   
                <asp:ControlParameter Name="iViewFilter" ControlID="uoDropDownListRequest" />    
                <asp:ControlParameter Name="iViewActive" ControlID="uoDropDownListActive" />    
                <asp:ControlParameter Name="iViewBooked" ControlID="uoDropDownListBooked" />  
                <asp:ControlParameter Name="iFilterType" ControlID="uoDropDownListFilterType" />    
                <asp:ControlParameter Name="iEmployeeID" ControlID="uoHiddenFieldEmployeeID" />  
                <asp:ControlParameter Name="sCrewAssistUser" ControlID="uoDropDownListCrewAssist" />  
            </SelectParameters>
        </asp:ObjectDataSource>
        </div>
    </div>
          
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldSortBy" runat="server" Value=""/>
    <asp:HiddenField ID="uoHiddenFieldPopUp" runat="server" Value="0"/>        
    <asp:HiddenField ID="uoHiddenFieldEmployeeID" runat="server" Value="0"/>        
    
    <%-- <div style="display:none">
        <div id="uoDivEmail" style="width:300px">
            <div class="PageTitle">
                 Service Request Email
            </div>  
            <div>
                <table width="300px">
                    <tr>
                        <td>
                            Hotel Request
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Vehicle Request
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>--%>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SidePlaceHolder" runat="server">   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
</asp:Content>
