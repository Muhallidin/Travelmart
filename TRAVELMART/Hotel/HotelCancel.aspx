<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelCancel.aspx.cs" Inherits="TRAVELMART.Hotel.HotelCancel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../App_Themes/Default/Stylesheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script> 
    <style type="text/css">
        
        .style6
        {
            width: 200px;
            white-space: nowrap;            
        }
                
    </style>
    
    
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <div class="ViewTitlePadding">
           <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
                <tr>
                    <td>
                        <asp:Label ID="uoLabelTitle" runat="server" Text="Cancel Hotel Booking" CssClass="Title"></asp:Label>                    
                    </td>
                </tr>
            </table>
        </div>
        <hr/> 
        
        <table>
            
            <tr>
                <td> Hotel<span lang="en-us">:</span> </td>
                <td colspan="3">
                    <asp:Label runat="server" ID="uoLabelHotel" Text="HotelName"></asp:Label>
                </td>
            </tr>
            <tr>
                <%--<td> Cancel Date: </td>
                <td >
                    <asp:TextBox runat="server" ID="uoTextBoxCancelDate" Width="100px"></asp:TextBox>
                    <cc1:textboxwatermarkextender ID="uoTextBoxPickupDate_TextBoxWatermarkExtender" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxCancelDate" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:textboxwatermarkextender>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                    <cc1:calendarextender ID="uoTextBoxCancelDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxCancelDate" PopupButtonID="ImageButton1" Format="MM/dd/yyyy" 
                        >
                    </cc1:calendarextender >
                    <cc1:maskededitextender ID="uoTextBoxCancelDate_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                        TargetControlID="uoTextBoxCancelDate"
                        UserDateFormat="MonthDayYear">
                    </cc1:maskededitextender>    
              
                    
                </td>--%>
                <td> Email To: </td>
                <td colspan="3" style=" ">
                    <asp:TextBox ID="txtEmailTo" runat="server" 
                        onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                        ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                        Width="500px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
               <%-- <td class="style6" >
                    Pickup Time:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="uoTextBoxCancelTime" Width="100px"/>
                    <cc1:textboxwatermarkextender ID="uoTextBoxCancelTime_TextBoxWatermarkExtender"
                        runat="server" Enabled="True" TargetControlID="uoTextBoxCancelTime" 
                        WatermarkCssClass="fieldWatermark" WatermarkText="HH:mm">
                    </cc1:textboxwatermarkextender>
                    <cc1:maskededitextender ID="uoMaskedEditExtenderCheckInTime" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxCancelTime"
                        UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                    </cc1:maskededitextender>
                </td>--%>
                <td> Email CC: </td>
                <td colspan="3">
                    <asp:TextBox ID="txtEmailCC" runat="server"  Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6" >
                    Comment:
                </td>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="uoTextBoxComment" Width="500px" 
                                 TextMode="MultiLine" Rows="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Confirmed By:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxConfirmedBy" Width="500px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6" ></td>
                <td colspan="3" style="width:100%;">
                    <asp:Button ID="btnSaveCancelHotel" runat="server" Text="Cancel Hotel Booking & Email" CssClass="SmallButton" OnClientClick="return ValidateFields();"
                        OnClick="btnSaveCancelHotel_Click"/>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    <%--<asp:CheckBox ID="uoCheckBoxEmail" runat="server" />Send Email--%>
                </td>
                <td colspan="3" >
                    <asp:Label ID="Label5" CssClass="RedNotification" runat="server" 
                               Text="Multiple emails should be separated by semicolon (i.e. abc@rccl.com;xyz@rccl.com)"> 
                    </asp:Label> 
                </td>
            </tr>
            
        </table>
        <br />
        <asp:Panel ID="Panel1" runat="server"  Width="100%"  ScrollBars="Both" >
            <asp:ListView runat="server" ID="uoListViewHotelCancel" >
                        <LayoutTemplate>
                            <table class="listViewTable" >
                                <tr>
                                   
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblRLHeader" runat="server" CommandName="SortByRecordLocator"
                                            Text="Record Locator" Width="55px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblLastNameHeader" runat="server" CommandName="SortByLastName"
                                            Text="Last Name" Width="145px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblFirstNameHeader" runat="server" CommandName="SortByFirstName"
                                            Text="First Name" Width="144px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblEmpIDHeader" runat="server" CommandName="SortByEmployeeID"
                                            Text="Employee ID" Width="54px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblGenderHeader" runat="server" CommandName="SortByGender"
                                            Text="Gender" Width="45px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="SortBySingleDouble"
                                            Text="Single/Double" Width="74px" />
                                    </th>
                                  
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblTitleHeader" runat="server" CommandName="SortByTitle" 
                                            Text="Title" Width="215px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblShipHeader" runat="server" CommandName="SortByShip" 
                                            Text="Ship" Width="164px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="SortByCost" 
                                            Text="Cost Center" Width="164px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblNationalityHeader" runat="server" CommandName="SortByNationality"
                                            Text="Nationality" Width="195px" />
                                    </th>
                                   
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblDCityHeader" runat="server" CommandName="SortByDeptCity"
                                            Text="Dept City" Width="45px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblACityHeader" runat="server" CommandName="SortByArvlCity"
                                            Text="Arvl City" Width="43px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByDeptDate" 
                                            Text="Dept Date" Width="60px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByArvlDate" 
                                            Text="Arvl Date" Width="60px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblDTimeHeader" runat="server" CommandName="SortByDeptTime"
                                            Text="Dept Time" Width="53px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblATimeHeader" runat="server" CommandName="SortByArvlTime"
                                            Text="Arvl Time" Width="53px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblCarrierHeader" runat="server" CommandName="SortByCarrier"
                                            Text="Carrier" Width="35px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblFlightNumHeader" runat="server" CommandName="SortByFlightNo"
                                            Text="Flight No." Width="44px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblVoucherHeader" runat="server" CommandName="SortByVoucher"
                                            Text="Voucher" Width="44px" />
                                    </th>
                                   
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                              
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecLoc")%>' Width="62px"></asp:Label>
                                </td>
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelLast" Text='<%# Eval("LastName")%>' Width="150px"></asp:Label>
                                 </td>
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:LinkButton Width="150px" runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("EmployeeId") + "&trID=" + Eval("TravelReqIdInt") + "&st=" + Eval("Status") + "&recloc=" + Eval("RecLoc") + "&manualReqID=" + Eval("RequestID") + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IDBigInt") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("FirstName") %></asp:LinkButton>
                                 </td>
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("EmployeeId")%>' Width="63px"></asp:Label>
                                 </td>
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblGender" Text='<%# Eval("Gender")%>' Width="50px"></asp:Label>
                                 </td>
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblRoom" Text='<%# Eval("SingleDouble")%>' Width="80px"></asp:Label>
                                 </td>
                               
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblRank" Text='<%# Eval("Title")%>' Width="220px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblVessel" Text='<%# Eval("ShipCode") + " - " + Eval("ShipName")%>'
                                        Width="170px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblCostCenter" Text='<%# Eval("CostCenterCode") + " - " + Eval("CostCenter")%>'
                                        Width="170px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblNationality" Text='<%# Eval("Nationality")%>'
                                        Width="200px"></asp:Label>
                                </td>
                                
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblDeptCity" Text='<%# Eval("DeptCity")%>' Width="50px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("ArvlCity")%>' Width="50px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblDepDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DeptDate"))%>'
                                        Width="67px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblArDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("ArvlDate"))%>'
                                        Width="67px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblDepartureTime" Text='<%# Eval("DeptTime")%>' Width="58px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblArrivalTime" Text='<%# Eval("ArvlTime")%>' Width="60px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="40px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblFN" Text='<%# Eval("FlightNo")%>' Width="50px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblVoucher" Text='<%# String.Format("{0:0.00}", Eval("Voucher")) %>'
                                        Width="50px"></asp:Label>
                                </td>
                                
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                       
                            <table class="listViewTable">
                                <tr>
                                    
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblRLHeader" runat="server" CommandName="SortByRecordLocator"
                                            Text="Record Locator" Width="55px" />
                                    </th>
                                    
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblLastNameHeader" runat="server" CommandName="SortByLastName"
                                            Text="Last Name" Width="145px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblFirstNameHeader" runat="server" CommandName="SortByFirstName"
                                            Text="First Name" Width="144px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblEmpIDHeader" runat="server" CommandName="SortByEmployeeID"
                                            Text="Employee ID" Width="54px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblGenderHeader" runat="server" CommandName="SortByGender"
                                            Text="Gender" Width="45px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="SortBySingleDouble"
                                            Text="Single/Double" Width="74px" />
                                    </th>
                                  
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblTitleHeader" runat="server" CommandName="SortByTitle" 
                                            Text="Title" Width="215px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblShipHeader" runat="server" CommandName="SortByShip" 
                                            Text="Ship" Width="164px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="SortByCost" 
                                            Text="Cost Center" Width="164px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblNationalityHeader" runat="server" CommandName="SortByNationality"
                                            Text="Nationality" Width="195px" />
                                    </th>
                                 
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblDCityHeader" runat="server" CommandName="SortByDeptCity"
                                            Text="Dept City" Width="45px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblACityHeader" runat="server" CommandName="SortByArvlCity"
                                            Text="Arvl City" Width="43px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByDeptDate" 
                                            Text="Dept Date" Width="60px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByArvlDate" 
                                            Text="Arvl Date" Width="60px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblDTimeHeader" runat="server" CommandName="SortByDeptTime"
                                            Text="Dept Time" Width="53px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblATimeHeader" runat="server" CommandName="SortByArvlTime"
                                            Text="Arvl Time" Width="53px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblCarrierHeader" runat="server" CommandName="SortByCarrier"
                                            Text="Carrier" Width="35px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblFlightNumHeader" runat="server" CommandName="SortByFlightNo"
                                            Text="Flight No." Width="44px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblVoucherHeader" runat="server" CommandName="SortByVoucher"
                                            Text="Voucher" Width="44px" />
                                    </th>
                                   
                                </tr>
                                <tr>
                                    <td colspan="20" class="leftAligned">
                                        <asp:Label runat="server" ID="Label10" Text="No Record" > </asp:Label>
                                    </td>
                                </tr>
                        </table>
                        </EmptyDataTemplate>
                      </asp:ListView> 
        </asp:Panel>
        <asp:HiddenField ID="uoHiddenFieldTravelReq" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
        
        <script type="text/javascript" language="javascript">
            function validateEmail(field) {
                var regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
                return (regex.test(field)) ? true : false;
            }

            function validateMultipleEmailsSemiColonSeparated(email, seperator) {
                var value = email.value;
                if (value != '') {
                    var result = value.split(seperator);
                    for (var i = 0; i < result.length; i++) {
                        if (result[i] != '') {
                            if (!validateEmail(result[i])) {
                                email.focus();
                                alert('Please check, `' + result[i] + '` email address not valid!');
                                return false;
                            }
                        }
                    }
                }
                return true;
            }

            function ValidateFields() {
                var sEmail = $("#<%=txtEmailTo.ClientID %>").val();
                var sComment = $("#<%=uoTextBoxComment.ClientID %>").val();
                
                var sMsg = "";
                if ($.trim(sEmail) == "") {
                    sMsg = "Email recipient required!";                   
                }
                if ($.trim(sComment) == "") {
                    sMsg = sMsg +"\nComment required!";
                }
                if (sMsg != "") {
                    alert(sMsg);
                    return false;
                }
                return true;                
            }
        
        </script>                
    </form>
</body>
</html>
                
