<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlightDetailPDF.aspx.cs" Inherits="TRAVELMART.FlightDetailPDF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div  style="width:100%">
       FLIGHT DETAILS
       <br />
       <asp:Label ID="lblDestination" runat="server" Text="17 JAN 2014> 17 JAN 2014 TRIP TO LOUIS ARMSTRONG NEW ORLEANS INTERNATIONAL AIRPORT("></asp:Label>
       <hr />
       <br />
       <table  style="width:100%">
           <tr>
               <td>
                    PREPARED FOR: 
                    <br /><br />
                    <asp:Label ID="lblName" runat="server" text="SAMLERO, JOHNASIS SALALIMA"/>  
                    <br /><br/>    
                    RESERVATION CODE:<asp:Label ID="lblReservationCode" runat="server" text="LXZZCE"/>  
                    <br /><br />    
               </td>     
               <td  style="width:1px; vertical-align:top; padding-top:20px;">
                    <asp:Image ID="imgLogo" style="width:157px; height:45px;" runat="server"  ImageUrl="Images/SHRSS.gif" /> 
               </td>
               <td style="width:280px; padding-left:10px; vertical-align:top; padding-top:20px;">
                  Royal Caribbean Cruises, Ltd.
                  <br/>  
                  Email: CrewAssist@rccl.com
                  <br/>  
                  Phone: 1.877.414.2739
               </td>
           </tr>
       </table> 
       
       <div  style="width:100%">
       
           <asp:ListView ID="lstviewAirDetail" runat="server" style="width:100%">
                <LayoutTemplate>
                     <table border="0" cellpadding="1" style="width:100%">
                        <tr id="itemPlaceholder" runat="server"></tr>
                     </table>
                </LayoutTemplate>
                <ItemTemplate>
                      <tr style="width:100%"> 
                          <td colspan="4">
                               <hr /> 
                          </td>
                      </tr>
                      <tr>
                          <td colspan="4">
                                DEPARTURE : 
                                &nbsp;<asp:Label runat="server" ID="lblDeparture" Text='<%# Eval("TravelDate") %>' Width="100px"/>
                                &nbsp;Please verify your flight prior to departure
                          </td>
                      </tr>
                      <tr>
                      
                          <td>
                                <asp:Label runat="server" ID="lblCarrier" Text='<%# Eval("Carrier") %>' Width="100px"/>
                                <br />
                                <asp:Label runat="server" ID="lblFlightNo" Text='<%# Eval("FlightNo") %>' Width="100px"/>
                          </td>
                          <td>
                                <asp:Label runat="server" ID="lblDepartureCode" Text='<%# Eval("DepartureCode") %>' Width="100px"/>
                                <br />
                                <asp:Label runat="server" ID="lblDeparturePort" Text='<%# Eval("DeparturePort") %>' Width="100px"/>
                          </td>
                          <td>
                                <asp:Label runat="server" ID="lblArrivalCode" Text='<%# Eval("ArrivalCode") %>' Width="100px"/>
                                <br />
                                <asp:Label runat="server" ID="lblArrivalPort" Text='<%# Eval("ArrivalPort") %>' Width="100px"/>
                          </td>
                          <td>
                                Aircraft:
                                <br />
                                <asp:Label runat="server" ID="lblAircraft" Text='<%# Eval("Aircraft") %>' Width="100px"/>

                          </td>
                      </tr>
                      
                      <tr>
                          <td>
                                Duration:
                                <br />
                                <asp:Label runat="server" ID="lblDuration" Text='<%# Eval("Duration") %>' Width="100px"/>
                          </td>
                          <td>
                                Departing At: 
                                <br />
                                <asp:Label runat="server" ID="lblDepartureDateTime" Text='<%# Eval("DepartureDateTime") %>' Width="100px"/>
                          </td>
                          <td>
                                Arriving At: 
                                <br />
                                <asp:Label runat="server" ID="lblArrivalDateTime" Text='<%# Eval("ArrivalDateTime") %>' Width="100px"/>
                          </td>
                          <td>
                                Distance (in Miles):
                                <br />
                                <asp:Label runat="server" ID="lblMileFlown" Text='<%# Eval("MileFlown") %>' Width="100px"/>
                          </td>
                      </tr>
                      <tr>
                        <td colspan="4">
                            <table  style="width:100%">
                                <tr>
                                    <td>Passenger Name</td>
                                    <td>Seats</td>
                                    <td>Class</td>
                                    <td>Status</td>
                                    <td>Record Locator</td>
                                    <td>Meals</td>
                                </tr>
                                <tr>
                                    <td><asp:Label runat="server" ID="lblPassengerName" Text='<%# Eval("PassengerName") %>' Width="100px"/></td>
                                    <td><asp:Label runat="server" ID="lblSeat" Text='<%# Eval("Seat") %>' Width="100px"/></td>
                                    <td><asp:Label runat="server" ID="lblClass" Text='<%# Eval("Class") %>' Width="100px"/></td>
                                    <td><asp:Label runat="server" ID="lblStatus" Text='<%# Eval("Status") %>' Width="100px"/></td>
                                    <td><asp:Label runat="server" ID="lblRecordLocator" Text='<%# Eval("RecordLocator") %>' Width="100px"/></td>
                                    <td><asp:Label runat="server" ID="lblMeals" Text='<%# Eval("Meals") %>' Width="100px"/></td>
                                </tr>
                            </table>
                        
                        </td>
                      </tr>
                </ItemTemplate>
           </asp:ListView>
           <hr />
           <table  style="width:100%">
                <tr>
                    <td colspan="2">HOTEL INFORMATION</td>
                </tr>
                <tr>
                    <td  style="white-space: nowrap;"> 
                        Check In Date:&nbsp;
                        <asp:Label runat="server" ID="lblCheckInDate" Width="100px"/>
                    </td>
                    <td style="width:100%">
                        Hotel:&nbsp;
                        <asp:Label runat="server" ID="lblHotel" Width="100px"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Address:&nbsp;
                        <asp:Label runat="server" ID="lblHotelAddress" Width="100px"/>
                    </td>
                </tr>
                <tr>
                    <td style="white-space:nowrap;">
                        No. Of Days:&nbsp;
                        <asp:Label runat="server" ID="lblNoOfDay" Width="100px"/>
                    </td>
                    <td style="width:100%">
                        Room Type:&nbsp;
                        <asp:Label runat="server" ID="lblRoomType" Width="100px"/>
                    </td>
                </tr>
                
                <tr>
                    <td  style="white-space: nowrap;">
                       Contact Person:&nbsp;
                        <asp:Label runat="server" ID="lblContactPerson" Width="100px"/>
                    </td>
                    <td style="width:100%">
                        Room Type:&nbsp;
                        <asp:Label runat="server" ID="lblContactNumber" Width="100px"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Email:&nbsp;
                        <asp:Label runat="server" ID="lblEmail" Width="100px"/>
                    </td>
                    
                </tr>
                
                
           </table>
           
           <div style="background-color:#BCC6CC; border-style:solid; border-width:thin; border-color:#98AFC7;  
                      padding:10px 0 10px 8px; width:100%" >
               This crew member's travel details will be available 35 days before he/she is scheduled to travel. 
               <br />
               Please remind crew members to check all their travel details again 72 hours before their travel date
           </div>
           
       </div>
    </div>
    
        <asp:HiddenField ID="uoHiddenFieldSeafaredID" runat="server" />
    
    
    </form>
    
</body>
</html>
