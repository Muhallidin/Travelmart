<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VehicleAssignment.aspx.cs"
    Inherits="TRAVELMART.Vehicle.VehicleAssignment" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../App_Themes/Default/Stylesheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Menu/jquery-1.3.1.min.js"></script>
    
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <asp:UpdatePanel>
            <ContentTemplate>
            
            
            
      
        
        <table style="width:800px; height:100%; text-align:left;" id="tableForm" >
            <tr>
                <td colspan="4" style="background: Black; color:White">
                   <b> Driver-Greeter Assignment</b>
                </td>
            </tr>
        
            <tr class="trDriver" style="display:none;" >
                <td style="width:94px">
                    Driver:
                </td>
                    
                <td class="leftAligned"  style="white-space:nowrap; width:300px" >
                     <asp:DropDownList ID="uoDropDownListDriver" runat="server" Width="280px" AppendDataBoundItems="True"
                        Visible="true" onchange="GetComboSelectedIndex(this)" >
                        <asp:ListItem Value="0">--SELECT DRIVER--</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; </td>
                <td colspan="2">
                    <input type="checkbox" id="chassignVehicle" onclick="IsGreeter(this)" /> &nbsp; <span>Assign Greeter</span>
                </td>
               
            </tr>
            <tr  class="trDriver"  style="display:none">
                <td style="width:100px;white-space:nowrap">
                    Pickup date:
                </td>
                <td style="width:300px; white-space:nowrap">
                    <asp:TextBox ID="uoTextBoxPickupDate" runat="server" Text="" Width="80px" Enabled="false" ></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="uoTextBoxPickupDate_Textboxwatermarkextender"
                        runat="server" Enabled="True" TargetControlID="uoTextBoxPickupDate" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <cc1:CalendarExtender ID="uoTextBoxPickupDate_Calendarextender" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxPickupDate" Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="uoTextBoxPickupDate_Maskededitextender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxPickupDate"
                        UserDateFormat="MonthDayYear">
                    </cc1:MaskedEditExtender>
                    
                </td>
                <td  style="width:100px;white-space:nowrap" >
                    Pickup Time
                </td>
                <td style="width:300px;white-space:nowrap">
                    <asp:TextBox runat="server" ID="uoTextBoxPickupTime" Width="80px" ></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="uoTxtBoxTime_TextBoxWatermarkExtender" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxPickupTime" WatermarkCssClass="fieldWatermark"
                        WatermarkText="HH:MM">
                    </cc1:TextBoxWatermarkExtender>
                    <cc1:MaskedEditExtender runat="server"
                        ID="uoMaskedEditExtenderCheckInTime"  
                        TargetControlID="uoTextBoxPickupTime"
                        Mask="99:99"
                        MessageValidatorTip="true"
                        MaskType="Time" 
                        CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" 
                        CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" 
                        CultureTimePlaceholder=""
                        Enabled="True"
                        UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                    </cc1:MaskedEditExtender>
                    
                </td>
            </tr>
             
            <tr  class="trDriver"  style="display:none">
                <td style="width:100px;white-space:nowrap">
                    Pickup Location:
                </td>
                <td style="width:300px">
                    <asp:TextBox ID="uoTextBoxPickupLocation" runat="server" value="" Width="280px"></asp:TextBox>
                </td>
                <td  style="width:100px;white-space:nowrap">
                    Drop-off Location
                </td>
                <td style="width:300px">
                    <asp:TextBox ID="uoTextBoxDropupLocation" runat="server" value="" Width="280px"></asp:TextBox>
                </td>
            </tr>
            <tr  style="display:none;">
                <td>
                    Parking Location:
                </td>
                <td colspan="3" style="width:280px">
                    <asp:TextBox ID="uoTextBoxPakingLocation" runat="server" value="" Width="300px"></asp:TextBox>
                </td>
            </tr>
            
               
               
            <tr class="trGreeter"  style="display:none">
                <td style="width:94px">
                        Greeter:
                    </td>
                        
                    <td colspan="3">
                         <asp:DropDownList ID="uoDropDownListGreeter" runat="server" Width="280px" AppendDataBoundItems="True"
                            Visible="true" >
                            <asp:ListItem Value="0">--SELECT GREETER--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
           
            
            
            
            <tr  class="trGreeter IsVehicle"  style="display:none">
                <td style="width:100px;white-space:nowrap" >
                    Pickup date
                </td>
                <td style="width:300px; white-space:nowrap">
                    <asp:TextBox ID="uoTextBoxGreeterPickupDate" runat="server" Text="" Width="80px" Enabled="false" ></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3"
                        runat="server" Enabled="True" TargetControlID="uoTextBoxGreeterPickupDate" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxGreeterPickupDate" Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxGreeterPickupDate"
                        UserDateFormat="MonthDayYear">
                    </cc1:MaskedEditExtender>
                    
                </td>
                <td  style="width:100px;white-space:nowrap">
                    Pickup Time
                </td>
                <td  style="width:300px;white-space:nowrap">
                    
                     <asp:TextBox runat="server" ID="uoTextBoxGreeterPickupTime" Width="80px" ></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxGreeterPickupTime" WatermarkCssClass="fieldWatermark"
                        WatermarkText="HH:MM">
                    </cc1:TextBoxWatermarkExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxGreeterPickupTime"
                        UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                    </cc1:MaskedEditExtender>
                </td>
            </tr>
             
           
            
             <tr class="trGreeter IsVehicle"  style="display:none">
                <td style=" width:100px;white-space:nowrap"> 
                   Pickup Location:
                </td>
                    
                <td  style=" width:300px;white-space:nowrap">
                    <asp:TextBox ID="uoTextBoxGreeterLocation" runat="server" value="" Width="300px"></asp:TextBox>
                </td>
            
            
                <td  style="width:100px;white-space:nowrap">
                    Drop-off Location
                </td>
                <td style="width:300px; white-space:nowrap"> 
                    <asp:TextBox ID="uoTextBoxGreeterDropLocation" runat="server" value="" Width="300px"></asp:TextBox>
                </td>
            </tr>
            
            
            
            
            <tr>
                <td colspan="4">
                    <asp:Button ID="uoButtonSaveDriver" runat="server" Text="Save" Font-Size="Smaller" style="display:none;"
                        ValidationGroup="add" OnClick="uoButtonSaveDriver_Click"  />
                    <input type="button" id="uoButtonSaveDriver1" class="SmallButton" value="Save"
                        onclick="checkSelectedVendor(this)" />
                        
                    <asp:Button ID="uoButtonCancel" runat="server" Text="Cancel"  CssClass="SmallButton"
                          />
                
               </td>
            </tr>
            <tr>
            
                <td colspan="4" id="RowVehicleType" style="width:100%">
                    <asp:ListView runat="server" ID="uoListviewVehicle" border="0"
                                            style="position:inherit"  cellpadding="0" cellspacing="0" class="listViewTable">
                          <LayoutTemplate>
                             <table border="0" cellpadding="0" cellspacing="0"  class="listViewTable">
                                <tr>
                                      <th style="text-align: center; white-space: normal;">
                                    <th style="text-align: center; white-space:  normal;">
                                       Vehicle Type
                                    </th >
                                    <th style="text-align: center; white-space: normal;">
                                        Vehicle Make
                                    </th >
                                    <th style="text-align: center; white-space: normal;">
                                        Capacity
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Flate Number
                                    </th>   
                                    <th style="text-align: center; white-space: normal;">
                                        Color
                                    </th>
                                  
                                    
                                </tr>        
                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                             </table>
                          </LayoutTemplate>
                          <ItemTemplate>
                             <tr>
                                    <td>
                                        <asp:CheckBox ID="chkboxSelectVehile"  runat="server"  onclick="SelectVehicleDetail(this)"  />
                                    </td>
                                   <td style="text-align: center; white-space:  normal;">
                                      <asp:HiddenField runat="server" ID="uoHiddenFieldVehicleHotelSPID" value='<%# Eval("VehicleHotelServProvID") %>'/>
                                      <asp:HiddenField runat="server" ID="uoHiddenFieldVehicleVendorID" value='<%# Eval("VendorID") %>'/>
                                      <asp:Label runat="server" ID="lblVehcleTypeID" Text='<%# Eval("VehicleType") %>'/>
                                   </td >
                                   <td style="text-align: center; white-space: normal;">
                                      <asp:HiddenField runat="server" ID="uoHiddenFieldMakeID" value='<%# Eval("VehicleMakeID") %>'/>
                                      <asp:Label runat="server" ID="lblVehicleMake" Text='<%# Eval("VehicleMakeName") %>'/>
                                   </td >
                                   <td style="text-align: center; white-space: normal;">
                                      <asp:Label runat="server" ID="lblCapacity" Text='<%# Eval("Capacity") %>'/>
                                   </td>
                                   <td style="text-align: center; white-space: normal;">
                                      <asp:Label runat="server" ID="lblFlateNumber" Text='<%# Eval("PlateNumber") %>'/>
                                   </td>
                                   <td style="text-align: center; white-space: normal;">
                                <%--      <asp:Label runat="server" ID="lblColor" Text='<%# Eval("VehicleColor") %>'/>--%>
                                      <asp:Label runat="server" ID="lblColorName" Text='<%# Eval("VehicleColorName") %>'/>
                                   </td>
                             </tr>   
                          </ItemTemplate>
                          <EmptyDataTemplate>
                           <table border="0" cellpadding="0" cellspacing="0"  class="listViewTable">
                                <tr>
                                    <th style="text-align: center; white-space:  normal;">
                                       Vehicle Type
                                    </th >
                                    <th style="text-align: center; white-space: normal;">
                                        Vehicle Make
                                    </th >
                                    <th style="text-align: center; white-space: normal;">
                                        Capacity
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Flate Number
                                    </th>   
                                    <th style="text-align: center; white-space: normal;">
                                        Color
                                    </th>
                                  
                                    
                                </tr>        
                                 <tr>
                                    <td colspan="5" style="text-align:left">
                                        No Vehicle Contract
                                    </td>
                                 </tr>      
                             </table>
                          
                          
                          
                          </EmptyDataTemplate>
                    </asp:ListView>
                </td>
            </tr>
            
        </table> 
           
           <asp:Panel runat="server" ID="pnlManifest" ScrollBars="Auto" style="min-height:170px; max-height:200px" >
                        
                  <asp:ListView runat="server" ID="uoListViewManifestConfirm"  >
                      <LayoutTemplate>
                           <table class="listViewTable">
                              <tr>
                                            
                                             <th class="leftAligned" style="white-space: normal;">
                                                    <asp:Label runat="server" ID="lblSeafarerID" Text="E1 ID" ></asp:Label>
                                                </th>
                                                <th class="leftAligned" style="white-space:nowrap;">
                                                    <asp:Label runat="server" ID="lblPickupDate" Text="Pickup Date"  ></asp:Label>
                                                </th>
                                                <th class="leftAligned" style="white-space: normal;">
                                                    <asp:Label runat="server" ID="lblPickupTime" Text="Pickup Time"
                                                        Width="70px"></asp:Label>
                                                </th>
                                                <th class="leftAligned" style="white-space: normal;">
                                                    <asp:Label runat="server" ID="lblPickupLocation" Text="Pickup Location"
                                                        Width="155px"></asp:Label>
                                                </th>
                                                <th class="leftAligned" style="white-space: normal;">
                                                    <asp:Label runat="server" ID="lbl" Text="DropOff Location"  Width="155px"></asp:Label>
                                                </th>
                                                  
                                                <th class="leftAligned" style="white-space: nowrap;">
                                                    <asp:Label runat="server" ID="lblRouteFrom" Text="Route From" Width="150px"></asp:Label>
                                                </th>
                                                 <th class="leftAligned" style="white-space: nowrap;">
                                                    <asp:Label runat="server" ID="Label1" Text="Route To" Width="150px"></asp:Label>
                                                </th>
                                                <th class="leftAligned" style="white-space: nowrap;">
                                                    <asp:Label runat="server" ID="lblFrom" Text="From" Width="150px"></asp:Label>
                                                </th>
                                                <th class="leftAligned" style="white-space: nowrap;">
                                                    <asp:Label runat="server" ID="lblTO" Text="To" Width="150px"></asp:Label>
                                                </th>
                                        </tr>
                              <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>     
                          </table>
                      </LayoutTemplate>
                      <ItemTemplate>
                          <tr>
                                           
                            <td class="leftAligned" style="white-space: normal;">
                               <asp:HiddenField ID="uoHiddenFieldConfirmedManifestID" runat="server" Value='<%# Convert.ToString(Eval("ConfirmManifestID")) %>' />
                               <asp:HiddenField ID="uoHiddenFieldDriverRequestID" runat="server" Value='<%# Convert.ToString(Eval("ConfirmManifestID")) %>' />
                               
                                <asp:HiddenField ID="uodfDriverPickupLatitude" runat="server" Value='<%# Convert.ToString(Eval("PickupLatitude")) %>' />
                                <asp:HiddenField ID="uodfDriverPickupLongitude" runat="server" Value='<%# Convert.ToString(Eval("PickupLongitude")) %>' />
                                <asp:HiddenField ID="uodfDriverDropOffLatitude" runat="server" Value='<%# Convert.ToString(Eval("DropOffLatitude")) %>' />
                                <asp:HiddenField ID="uodfDriverDropOffLongitude" runat="server" Value='<%# Convert.ToString(Eval("DropOffLongitude")) %>' />
                                <asp:HiddenField ID="uodfDriverParkingLatitude" runat="server" Value='<%# Convert.ToString(Eval("ParkingLatitude")) %>' />
                                <asp:HiddenField ID="uodfDriverParkingLongitude" runat="server" Value='<%# Convert.ToString(Eval("ParkingLongitude")) %>' />
                             
                            
                                <asp:Label runat="server" ID="lblSeafarerID" Text='<%# Eval("SeafarerIdInt") %>' Width="51px"></asp:Label>
                                 
                                
                            </td>
                            <td class="leftAligned" style="white-space: nowrap;">
                                <asp:Label runat="server" ID="lblPickupDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("PickupDate"))%>' Width="70px"></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: nowrap;">
                               <%-- <asp:Label runat="server" ID="lblPickupTime" Text='<%# String.Format("{0:hh:mm}",Eval("PickupTime"))%>'
                                    Width="70px"></asp:Label>--%>
                                    <asp:TextBox runat="server"    style="border:none; text-align:left; vertical-align:middle; font-size:small "  
                                     ID="txtPickupTime"  Text='<%# String.Format("{0:hh:mm}",Eval("PickupTime"))%>'
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td class="leftAligned" style="white-space: nowrap;">
                                <asp:TextBox runat="server" CssClass="PickupLocation"  style="border:none; text-align:left; vertical-align:middle" TextMode="MultiLine" 
                                     ID="txtPickupLocation" Text='<%# Eval("PickupLocation")%>'
                                    Width="155px"></asp:TextBox>
                            </td>
                            <td class="leftAligned" style="white-space: nowrap;">
                                <asp:TextBox runat="server" CssClass="dropLocation" style="border:none; text-align:left; vertical-align:middle" 
                                TextMode="MultiLine" ID="txtDropOffLocation" Text='<%# Eval("DropOffLocation")%>' Width="155px"></asp:TextBox>
                            </td>
                              
                            <td class="leftAligned" style="white-space: nowrap;">
                                <asp:Label runat="server" ID="lblRouteFrom" Text='<%# Eval("RouteFrom")%>' Width="150px"></asp:Label>
                            </td>
                             <td class="leftAligned" style="white-space: nowrap;">
                                <asp:Label runat="server" ID="Label1" Text='<%# Eval("RouteTo")%>' Width="150px"></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: nowrap;">
                                <asp:Label runat="server" ID="lblFrom" Text='<%# Eval("colFromVarchar")%>' Width="150px"></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: nowrap;">
                                <asp:Label runat="server" ID="lblTO" Text='<%# Eval("colToVarchar ")%>' Width="150px"></asp:Label>
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
            </asp:Panel>
        
        
        <asp:HiddenField  runat="server" ID="uoHiddenFieldVendorID"  Value=""/>
         <asp:HiddenField  runat="server" ID="uoHiddenFieldVehicleHotelSPID"  Value=""/>
        <asp:HiddenField  runat="server" ID="uoHiddenFieldcIds"  Value=""/>
        <asp:HiddenField  runat="server" ID="uoHiddenFieldDate"  Value=""/>
        <asp:HiddenField  runat="server" ID="uoHiddenFieldsUserID"  Value=""/>
        <asp:HiddenField  runat="server" ID="uoHiddenFieldcmfid"  Value=""/>
        <asp:HiddenField  runat="server" ID="uoHiddenFieldRole"  Value=""/>
        <asp:HiddenField  runat="server" ID="uoHiddenFieldIsVehicle"  Value=""/>
        <asp:HiddenField  runat="server" ID="uoHiddenFieldIsGreeter"  Value=""/>
        
        <asp:HiddenField  runat="server" ID="uodfDriverPickupLatitude"  Value=""/>
        <asp:HiddenField  runat="server" ID="uodfDriverPickupLongitude"  Value=""/>
        <asp:HiddenField  runat="server" ID="uodfDriverDropOffLatitude"  Value=""/>
        <asp:HiddenField  runat="server" ID="uodfDriverDropOffLongitude"  Value=""/>
        <asp:HiddenField  runat="server" ID="uodfDriverParkingLatitude"  Value=""/>
        <asp:HiddenField  runat="server" ID="uodfDriverParkingLongitude"  Value=""/>
        
        
        <asp:HiddenField  runat="server" ID="uodfGreeterPickupLatitude"  Value=""/>
        <asp:HiddenField  runat="server" ID="uodfGreeterPickupLongitude"  Value=""/>
        <asp:HiddenField  runat="server" ID="uodfGreeterDropOffLatitude"  Value=""/>
        <asp:HiddenField  runat="server" ID="uodfGreterDropOffLongitude"  Value=""/>
          </ContentTemplate>
    </asp:UpdatePanel> 
    </form>
    
    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            IsVehicle();
            ControlSize();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                ControlSize();

                IsVehicle();
            }
        }







        function ControlSize() {
         
            console.log($("#form1")[0].offsetWidth);
            var res = ($("#form1")[0].offsetWidth - 3);
            document.getElementById("pnlManifest").style.width = (res - 3) + "px";
            document.getElementById("tableForm").style.width = res  + "px";
            $("#fancybox-content").style.height = 400 + "px";
        }

        function IsVehicle() {
            if (document.getElementById("uoHiddenFieldIsVehicle").value == 1) {
                var checkboxes = new Array();
                checkboxes = document.getElementsByClassName('trDriver');
                for (var i = 0; i < checkboxes.length; i++) {

                    checkboxes[i].style.display = 'block';     
                }
            }

            if (document.getElementById("uoHiddenFieldIsGreeter").value == 1) {
                var g = new Array();
                g = document.getElementsByClassName('trGreeter');
                for (var i = 0; i < g.length; i++) {

                    g[i].style.display = 'block';
                }
            }


            if (document.getElementById("uoHiddenFieldIsVehicle").value == 0 && document.getElementById("uoHiddenFieldIsGreeter").value == 1) {
                document.getElementById('RowVehicleType').style.display = 'None';
            }
            else { 
                document.getElementById('RowVehicleType').style.display = 'block';
            }


            $('#uoTextBoxDropupLocation').change(function() {
                var droploc = $(this).val();
                $('#uoTextBoxGreeterDropLocation').val($(this).val());

                $('textarea[name$=txtDropOffLocation]').each(function() {
                    $(this).val(droploc);
                });

            });

            $('#uoTextBoxPickupLocation').change(function() {
                var pckloc = $(this).val();
               $('#uoTextBoxGreeterLocation').val($(this).val());
               $('textarea[name$=txtPickupLocation]').each(function() {
                   $(this).val(droploc);
               });
            });


            $('#uoTextBoxGreeterDropLocation').change(function() {
                var Gdroploc = $(this).val();

                $('textarea[name$=txtDropOffLocation]').each(function() {
                $(this).val(Gdroploc);
                });

            });

            $('#uoTextBoxGreeterLocation').change(function() {
                var Gpckloc = $(this).val();
                $('textarea[name$=txtPickupLocation]').each(function() {
                    $(this).val(Gpckloc);
                });
            });

            $('#uoTextBoxPickupTime').change(function() {
                var Dpcktime = $(this).val();
                console.log(Dpcktime);
                $('input[name$=txtPickupTime]').each(function() {
                    $(this).val(Dpcktime);
                });
            });

            $('#uoTextBoxGreeterPickupTime').change(function() {
                var Gpcktime = $(this).val();
                console.log(Gpcktime);
                $('input[name$=txtPickupTime]').each(function() {
                    $(this).val(Gpcktime);
                });
            });
 
        }


        function IsGreeter(val) {
 
 
                var g = new Array();
                g = document.getElementsByClassName('trGreeter');
                console.log(g);
               
                
                if (val.checked == true) {
                    var cbo = document.getElementById("uoDropDownListDriver");
                    document.getElementById("uoDropDownListGreeter").selectedIndex = -1;
                    document.getElementById("uoDropDownListGreeter").selectedIndex = cbo.selectedIndex;
                    var Greeter = document.getElementById("uoDropDownListGreeter");
                    console.log(document.getElementById("uoDropDownListGreeter").selectedIndex);
                    if (document.getElementById("uoDropDownListGreeter").selectedIndex == -1) {
                        document.getElementById("uoDropDownListGreeter").selectedIndex = 0;
                    }

                    if ($('#uoTextBoxGreeterDropLocation').val() == '') {
                        $('#uoTextBoxGreeterDropLocation').val($('#uoTextBoxDropupLocation').val());
                    }

                    if ($('#uoTextBoxGreeterLocation').val() == '') {
                        $('#uoTextBoxGreeterLocation').val($('#uoTextBoxPickupLocation').val());
                    }
                  
                     
                }
                else {
                    document.getElementById("uoDropDownListGreeter").selectedIndex = 0;

                }

                 for (var i = 0; i < g.length; i++) {
                     if (val.checked == true) {
                        
                         if (g[i].className == 'trGreeter IsVehicle') {
                             g[i].style.display = 'none';
                         }
                         else {
                             g[i].style.display = 'block';
                         }

//                         g[i].style.display = 'block';
                    }
                    else {
                        g[i].style.display = 'none';
                    }
                }
                
              
             
        }
        

        function SelectVehicleDetail(val) {
            var checkboxes = new Array();
            checkboxes = document.getElementsByTagName('input');
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].type == 'checkbox' && checkboxes[i].id != val.id) {
                    checkboxes[i].checked = false;
                }
            }
            var id = val.id.replace('chkboxSelectVehile', 'uoHiddenFieldVehicleHotelSPID');
            console.log(document.getElementById(id).value);
            console.log(val.checked);
            document.getElementById("uoHiddenFieldVehicleHotelSPID").value = "";
            
            if (val.checked == true) { 
              document.getElementById("uoHiddenFieldVehicleHotelSPID").value = document.getElementById(id).value
            }
          
        }

        function GetComboSelectedIndex(val) {

            document.getElementById("uoDropDownListGreeter").selectedIndex = 0;
            if (document.getElementById("chassignVehicle").checked == true) {

                console.log(val.selectedIndex);
            
                var value = val.options[val.selectedIndex].value;
                var text = val.options[val.selectedIndex].text;
                console.log(val.selectedIndex);
                var Greeter = document.getElementById("uoDropDownListGreeter");
                if (val.options[val.selectedIndex].value != Greeter.options[Greeter.selectedIndex].value) {
                    document.getElementById("uoDropDownListGreeter").selectedIndex = 0;
                }
            }

        }


        function checkSelectedVendor(val) {
        
        
        
            var cDriver =  document.getElementById("uoDropDownListDriver").selectedIndex;
            var dPupDate = document.getElementById("uoTextBoxPickupDate").value;
//            var dropDate = document.getElementById("uoTextBoxDropDate").value;
            var dPLocation = document.getElementById("uoTextBoxPickupLocation").value;
            var dDLocation = document.getElementById("uoTextBoxDropupLocation").value;

            var cGreeter = document.getElementById("uoDropDownListGreeter").selectedIndex;
            var gPupDate = document.getElementById("uoTextBoxGreeterPickupDate").value;
//            var gdropDate = document.getElementById("uoTextBoxGreeterDropDate").value;
            var gPLocation = document.getElementById("uoTextBoxGreeterLocation").value;
            var gDLocation = document.getElementById("uoTextBoxGreeterDropLocation").value;
            var VehicleHotelSPID = document.getElementById("uoHiddenFieldVehicleHotelSPID").value;

 

            var id = "";   
            $("[name$=chkboxSelectVehile]").each( function() {
                    console.log(this.checked);
                    if (this.checked == true) {
                        var id = this.id.replace('chkboxSelectVehile', 'uoHiddenFieldVehicleHotelSPID'); 
                        console.log(document.getElementById(id).value);
                        document.getElementById("uoHiddenFieldVehicleHotelSPID").value = document.getElementById(id).value
                    }
                });
 
            
            

            if (cGreeter <= 0 && cDriver <= 0) {
                alert("Required Driver or Greeter");
                return false;
            }
            if (cDriver > 0) {
                if (dPupDate == "") {
                    alert("Pickup date required");
                    return false;
                }
//                else if (dropDate == "") {
//                    alert("Drop-off date required");
//                    return false;
//                }
                else if (dPLocation == "") {
                    alert("Pickup location required");
                    return false;
                }
                else if (dDLocation == "") {
                    alert("Drop-off location required");
                    return false;
                }
                else if (VehicleHotelSPID == "" || VehicleHotelSPID == "0") {
                    alert("Vehicle type required");
                    return false;
                }
            }
            if (cGreeter > 0) {
                if (gPupDate == "") {
                    alert("Greeter pickup date required");
                    return false;
                }
//                else if (gdropDate == "") {
//                    alert("Greeter drop-off date required");
//                    return false;
//                }
                else if (gPLocation == "" ) {
                    alert("Greeter pickup location required");
                    return false;
                }
                else if (gDLocation == "" ) {
                    alert("Greeter drop-off location required");
                    return false;
                }
            }
            

            document.getElementById("uoButtonSaveDriver").click();
            
        }
        
        
        
    </script>
    
    
</body>
</html>
