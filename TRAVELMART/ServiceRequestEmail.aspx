<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true"
    CodeBehind="ServiceRequestEmail.aspx.cs" Inherits="TRAVELMART.ServiceRequestEmail"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {            
            ControlSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {               
                ControlSettings();
            }
        }

        function ControlSettings() {
            $("#<%=uoButtonSend.ClientID %>").click(function() {
                var chkHotelReq = $("#<%=uoCheckBoxHotelRequest.ClientID %>");
                var chkVehicleReq = $("#<%=uoCheckBoxVehicleRequest.ClientID %>");
                var chkPortAgentReq = $("#<%=uoCheckBoxPortAgentRequest.ClientID %>");
                var chkMeetGreetReq = $("#<%=uoCheckBoxMeetGreetRequest.ClientID %>");

                var chkAll = $("#<%=uoCheckBoxCopyAll.ClientID %>");
                var chkCrewAssist = $("#<%=CheckBoxCopycrewassist.ClientID %>");
                var chkCrewHotels = $("#<%=CheckBoxCopycrewhotels.ClientID %>");
                var chkShip = $("#<%=CheckBoxCopyShip.ClientID %>");
                var chkScheduler = $("#<%=CheckBoxScheduler.ClientID %>");

                var txtEmailTo = $("#<%=uoTextBoxEmail.ClientID %>");
                var txtEmailToVehicle = $("#<%=uoTextBoxEmailVehicle.ClientID %>");
                var txtEmailToPortAgent = $("#<%=uoTextBoxEmailPortAgent.ClientID %>");
                var txtEmailToMeetGreet = $("#<%=uoTextBoxEmailMeetGreet.ClientID %>");
                
                var txtEmailOther = $("#<%=uoTextBoxEmailOther.ClientID %>");

                document.getElementById('<%=uoHiddenFieldEmailHotelVendor.ClientID %>').value = txtEmailTo.val();
                document.getElementById('<%=uoHiddenFieldEmailVehicle.ClientID %>').value = txtEmailToVehicle.val();
                document.getElementById('<%=uoHiddenFieldEmailPortAgent.ClientID %>').value = txtEmailToPortAgent.val();
                document.getElementById('<%=uoHiddenFieldEmailMeetGreet.ClientID %>').value = txtEmailToMeetGreet.val();
                document.getElementById('<%=uoHiddenFieldEmailOther.ClientID %>').value = txtEmailOther.val();
                
                var sMsg = "";
                
                if (chkHotelReq.attr('checked') ||
                    chkVehicleReq.attr('checked') ||
                    chkMeetGreetReq.attr('checked') ||
                    chkPortAgentReq.attr('checked')
                ) {

                    if (chkHotelReq.attr('checked') && txtEmailTo.val() == '') {
                        sMsg = sMsg + "No Hotel Request email recipient!";
                    }
                    if (chkVehicleReq.attr('checked') && txtEmailToVehicle.val() == '') {
                        sMsg = sMsg + "\nNo Vehicle Request email recipient!";
                    }
                    if (chkMeetGreetReq.attr('checked') && txtEmailToMeetGreet.val() == '') {
                        sMsg = sMsg + "\nNo Meet & Greet Request email recipient!";
                    }
                    if (chkPortAgentReq.attr('checked') && txtEmailToPortAgent.val() == '') {
                        sMsg = sMsg + "\nNo Port Agent Request email recipient!";
                    }
                    if (sMsg != "") {
                        alert(sMsg);
                        return false;
                    }
                }
                else {
                    alert('No email type to send!');
                    return false;
                }



                //                if (
                //                //                chkEmail.attr('checked') ||
                //                //                chkEmailVehicle.attr('checked') ||
                //                //                chkEmailMeetGreet.attr('checked') ||

                //                chkCrewAssist.attr('checked') ||
                //                chkCrewHotels.attr('checked') ||
                //                chkShip.attr('checked') ||
                //                chkScheduler.attr('checked')
                //            ) {
                //                    return true;
                //                }
                //                else {
                //                    alert('No recipient selected!');
                //                    return false;
                //                }
            });
        }
        
        function checkboxEnable(obj) {
            var chkAll = $("#<%=uoCheckBoxCopyAll.ClientID %>");
            var chkCrewAssist = $("#<%=CheckBoxCopycrewassist.ClientID %>");
            var chkCrewHotels = $("#<%=CheckBoxCopycrewhotels.ClientID %>");
            var chkShip = $("#<%=CheckBoxCopyShip.ClientID %>");
            var chkScheduler = $("#<%=CheckBoxScheduler.ClientID %>");

            var chkHotelReq = $("#<%=uoCheckBoxHotelRequest.ClientID %>");
            var chkVehicleReq = $("#<%=uoCheckBoxVehicleRequest.ClientID %>");
            var chkPortAgentReq = $("#<%=uoCheckBoxPortAgentRequest.ClientID %>");
            var chkMeetGreetReq = $("#<%=uoCheckBoxMeetGreetRequest.ClientID %>");

            var txtEmailTo = $("#<%=uoTextBoxEmail.ClientID %>");
            var txtEmailToVehicle = $("#<%=uoTextBoxEmailVehicle.ClientID %>");
            var txtEmailToPortAgent = $("#<%=uoTextBoxEmailPortAgent.ClientID %>");
            var txtEmailToMeetGreet = $("#<%=uoTextBoxEmailMeetGreet.ClientID %>");
            
            if (obj.id == chkAll.attr('id')) {
                chkCrewAssist.attr('checked', chkAll.attr('checked'));
                chkCrewHotels.attr('checked', chkAll.attr('checked'));
                chkShip.attr('checked', chkAll.attr('checked'));
                chkScheduler.attr('checked', chkAll.attr('checked'));
            }
            else 
            {
                if (obj.id == chkCrewAssist.attr('id') ||
                    obj.id == chkCrewHotels.attr('id') ||
                    obj.id == chkShip.attr('id') ||
                    obj.id == chkScheduler.attr('id')
                )
                    if (obj.checked == false) {
                    chkAll.attr('checked', false);
                }
            }

            if (obj.id == chkHotelReq.attr('id')) {
                if (obj.checked) {
                    
                    txtEmailTo.attr('readonly', false);
                    txtEmailTo.removeAttr('class');
                }
                else {
                    
                    txtEmailTo.attr('readonly', true);
                    txtEmailTo.attr('class', 'ReadOnly');
                }
            }

            if (obj.id == chkVehicleReq.attr('id')) {
                if (obj.checked) {

                    txtEmailToVehicle.attr('readonly', false);
                    txtEmailToVehicle.removeAttr('class');
                }
                else {

                    txtEmailToVehicle.attr('readonly', true);
                    txtEmailToVehicle.attr('class', 'ReadOnly');
                }
            }

            if (obj.id == chkPortAgentReq.attr('id')) {
                if (obj.checked) {

                    txtEmailToPortAgent.attr('readonly', false);
                    txtEmailToPortAgent.removeAttr('class');
                }
                else {

                    txtEmailToPortAgent.attr('readonly', true);
                    txtEmailToPortAgent.attr('class', 'ReadOnly');
                }
            }
            
            if (obj.id == chkMeetGreetReq.attr('id')) {
                if (obj.checked) {

                    txtEmailToMeetGreet.attr('readonly', false);
                    txtEmailToMeetGreet.removeAttr('class');
                }
                else {

                    txtEmailToMeetGreet.attr('readonly', true);
                    txtEmailToMeetGreet.attr('class', 'ReadOnly');
                }
            }
        }

        function testalert() {
            var m = 'test 1' + '\nTest2';

            alert(m);
        }
    </script>
    
    <div class="PageTitle">
        Service Request Email
    </div>
    <hr />
  
    <div>
        <table class="TableView" width="100%" >            
           <%-- <tr>
                <td class="caption" colspan="2">
                    <b>Hotel Request Email</b>
                </td>
            </tr>--%>
            <tr>
                <td class="caption" style="width: 100px">
                    E1 ID:
                </td>
                <td class="value" colspan="4">
                    <asp:TextBox runat="server" ID="uoTextBoxEmployeeID" ReadOnly="true" CssClass="ReadOnly" Width="200px"></asp:TextBox>
                </td>                
            </tr>    
            <tr>
                <td class ="caption" style="width: 100px">
                    Seafarer Name:
                </td>
                <td class="value" colspan="4">
                    <asp:TextBox runat="server" ID="uoTextBoxName" ReadOnly="true" CssClass="ReadOnly" Width="570px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="caption" style="width: 100px">
                    Email Type:
                </td>              
                <td class="value" colspan="3">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:CheckBox Width="150px" ID="uoCheckBoxHotelRequest" runat="server" onclick="checkboxEnable(this)" Text="Hotel Request" />                    
                            </td>
                            <td>
                                <asp:CheckBox Width="150px" ID="uoCheckBoxVehicleRequest" runat="server" onclick="checkboxEnable(this)" Text="Vehicle Request" />                    
                            </td>
                            <td>
                                <asp:CheckBox Width="150px" ID="uoCheckBoxPortAgentRequest" runat="server" onclick="checkboxEnable(this)" Text="Port Agent Request" />
                            </td>
                            <td>
                                <asp:CheckBox Width="150px" ID="uoCheckBoxMeetGreetRequest" runat="server" onclick="checkboxEnable(this)" Text="Meet & Greet Request" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="caption" style="width: 100px">
                    Email To:                      
                </td>
                <%--<td class="value" style="width: 79px"  >
                     <asp:CheckBox ID="uoCheckBoxEmail" runat="server" onclick="checkboxEnable(this)" Text="Hotel Vendor"  Width="136px"/> 
                </td>--%>
                <td class="caption">
                    Hotel Request:
                </td>
                <td class="value" >
                    <asp:TextBox runat="server" ID="uoTextBoxEmail" TextMode="MultiLine" Width="345px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>
                <td class="value" >
                    <asp:Label runat="server" ID="uoLabelHotelName" Text="Hotel Vendor"></asp:Label>
                </td>
            </tr>   
            <tr>
                <td class="caption" style="width: 100px">
                               
                </td>
                <%--<td class="value" style="width: 79px" >
                     <asp:CheckBox ID="uoCheckBoxEmailVehicle" runat="server" onclick="checkboxEnable(this)" Text="Vehicle Vendor"  Width="136px"/> 
                </td>--%>
                <td class="caption">
                    Vehicle Request:
                </td>
                <td class="value" >                 
                    <asp:TextBox runat="server" ID="uoTextBoxEmailVehicle" TextMode="MultiLine" Width="345px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>
                <td class="value">
                    <asp:Label runat="server" ID="uoLabelVehicleName" Text="Vehicle Vendor"></asp:Label>
                </td>
            </tr>
              <tr>
                <td class="caption" style="width: 100px">
                               
                </td>                
                <td class="caption">
                    Port Agent Request:
                </td>
                <td class="value" >
                    <asp:TextBox runat="server" ID="uoTextBoxEmailPortAgent" TextMode="MultiLine" Width="345px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>
                <td class="value" >
                    <asp:Label runat="server" ID="uoLabelPortAgentName" Text="Port Agent Vendor"></asp:Label>
                </td>
            </tr>   
            <tr>
                <td class="caption" style="width: 100px">
                               
                </td>
                <%--<td class="value" style="width: 79px">
                     <asp:CheckBox ID="uoCheckBoxEmailMeetGreet" runat="server" onclick="checkboxEnable(this)" Text="Meet & Greet Vendor" Width="136px" /> 
                </td>--%>
                <td class="caption">
                    Meet & Greet Request:
                </td>
                <td class="value" >
                    <asp:TextBox runat="server" ID="uoTextBoxEmailMeetGreet" TextMode="MultiLine" Width="345px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>
                <td class="value" >
                    <asp:Label runat="server" ID="uoLabelMeetGreet" Text="Meet & Greet Vendor"></asp:Label>
                </td>
            </tr>                                         
             <tr>
                <td colspan="4" class="caption">
                    CC Email:
                </td>
            </tr>  
            <tr>   
                <td class="caption" style="width: 100px">
                </td>            
                 <td  class="value"  colspan="3" style="white-space:nowrap">  
                    <table width="100%">
                        <tr>
                            <td style="width:150px">
                                <asp:CheckBox Width="100px" ID="uoCheckBoxCopyAll" runat="server" onclick="checkboxEnable(this)" Text="Copy All" />        
                            </td>
                            <td style="width:200px">
                                <asp:CheckBox Width="130px" ID="CheckBoxCopycrewassist" runat="server" onclick="checkboxEnable(this)" Text="Copy crew assist" />             
                            </td>
                            <td style="width:200px">
                                <asp:CheckBox Width="130px" ID="CheckBoxCopycrewhotels" Text="Copy crew hotels" runat="server" onclick="checkboxEnable(this)" />
                            </td>
                            <td style="width:200px">
                                <asp:CheckBox Width="130px" ID="CheckBoxCopyShip" Text="Copy Ship" runat="server" onclick="checkboxEnable(this)" />
                            </td>
                            <td style="width:200px">
                                <asp:CheckBox Width="130px" ID="CheckBoxScheduler" Text="Copy Scheduler" runat="server" onclick="checkboxEnable(this)" /> 
                            </td>        
                        </tr>
                    </table>                                                                                                      
                </td>
            </tr>
            <tr>                
                <td class="caption" style="width: 100px">
                    Other Email:
                </td>
                 <td class="value" colspan="3">
                    <%--<asp:TextBox ID="uoTextBoxEmailOther" style="margin-bottom:-5px; margin-left:14px;" runat="server" Width="400px"
                        TextMode="MultiLine" >
                    </asp:TextBox>--%> 
                    <asp:TextBox runat="server" ID="uoTextBoxEmailOther" TextMode="MultiLine" Width="570px"></asp:TextBox>
                 </td>
            </tr>
            <tr>   
                <td class="caption" style="width: 100px">
                </td>             
                <td style="text-align:left" colspan="3" >   
                    <br/><asp:Button runat="server" ID="uoButtonSend" Text="Email Hotel Request" 
                        CssClass="SmallButton" onclick="uoButtonSend_Click"/>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="uoHiddenFieldHotelRequestID" runat="server" value="0" />
    <asp:HiddenField ID="uoHiddenFieldVehicleRequestID" runat="server" value="0" />
    <asp:HiddenField ID="uoHiddenFieldMeetGreetRequestID" runat="server" value="0" />
    <asp:HiddenField ID="uoHiddenFieldPortAgentRequestID" runat="server" value="0" />
    
    <asp:HiddenField ID="uoHiddenFieldVehicleTransID" runat="server" value="0" />
    
    <asp:HiddenField ID="uoHiddenFieldTravelRequestID" runat="server" value="0" />
    <asp:HiddenField ID="uoHiddenFieldSeqNo" runat="server" value="0" />
    
    <asp:HiddenField ID="uoHiddenFieldHotelID" runat="server" value="0" />
    <asp:HiddenField ID="uoHiddenFieldVehicleID" runat="server" value="0" />    
    <asp:HiddenField ID="uoHiddenFieldPortAgentID" runat="server" value="0" />
    <asp:HiddenField ID="uoHiddenFieldMeetGreetID" runat="server" value="0" />
     
    <asp:HiddenField ID="uoHiddenFieldCrewAssist" runat="server" value="" />           
    <asp:HiddenField ID="uoHiddenFieldEmailHotel" runat="server" value="" />
    <asp:HiddenField ID="uoHiddenFieldCopyShip" runat="server" value="" />
    <asp:HiddenField ID="uoHiddenFieldScheduler" runat="server" value="" />
    
    <asp:HiddenField ID="uoHiddenFieldEmailHotelVendor" runat="server" value="" />
    <asp:HiddenField ID="uoHiddenFieldEmailVehicle" runat="server" value="" />
    <asp:HiddenField ID="uoHiddenFieldEmailMeetGreet" runat="server" value="" />
    <asp:HiddenField ID="uoHiddenFieldEmailPortAgent" runat="server" value="" />
    
    <asp:HiddenField ID="uoHiddenFieldEmailOther" runat="server" value="" />
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
