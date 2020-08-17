<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PotentialSignOFF.aspx.cs" Inherits="TRAVELMART.Vehicle.PotentialSignOFF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
     <link href="../App_Themes/Default/Stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
         
          <table>
             <tr>
                <td style="background-color: Gray; text-align: left; font-size:large; font-weight:bold;
                     color: White;">
                     <asp:Label ID="lblTitle" runat="server" Text="Potential Sign OFF" ForeColor="White" />
                     &nbsp;&nbsp;
                     <asp:Button ID="uoButtonExport"  runat="server" Text="Export" CssClass="SmallButton" Width="100px" OnClick="uoButtonExport_Click" />   
                </td>
             </tr>
             <tr>
                <td>
                        <asp:panel ID="Panel1" runat="server" ScrollBars="Auto">
                        <asp:ListView runat="server" ID="uoListViewManifestDetails"  >
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" id="NewTable">
                                        <tr>
                                          
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLinkButtonRouteFrom" runat="server" CommandName="SortByRouteFrom"
                                                    Text="Route From" Width="150px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByRouteTo" Text="Route To"
                                                    Width="150px" />
                                            </th>  
                                             <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByOnOffdate" Text="Pickup Date"
                                                    Width="100px" />
                                            </th>                          
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLinkButtonPickupDate" runat="server" CommandName="SortByPickupDate"
                                                    Text="Pickup Time" Width="64px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByFromCity" Text="From City"
                                                    Width="50px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="LinkButton7" runat="server" CommandName="SortByToCity" Text="To City"
                                                    Width="50px" />
                                            </th>
                                           
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByOnOff" Text="Status"
                                                    Width="45px" />
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
                                                    Text="Employee ID" Width="70px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLblGenderHeader" runat="server" CommandName="SortByGender"
                                                    Text="Gender" Width="45px" />
                                            </th>
                                          
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="SortByCostCenter"
                                                    Text="Cost Center" Width="90px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLblTitleHeader" runat="server" CommandName="SortByTitle" Text="Title"
                                                    Width="215px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLblShipHeader" runat="server" CommandName="SortByShip" Text="Ship"
                                                    Width="164px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLblRLHeader" runat="server" CommandName="SortByRecordLocator"
                                                    Text="Record Locator" Width="55px" />
                                            </th>
                                            
                                        
                                            <th style="text-align: center; white-space: normal; display:none;">
                                                <asp:Label runat="server" ID="Label2" Text="" Width="25px"></asp:Label>
                                            </th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder> 
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                       
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="Label4" Text='<%# Eval("RouteFrom") %>' Width="150px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="Label8" Text='<%# Eval("RouteTo") %>' Width="150px"></asp:Label>
                                        </td>
                                         <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="Label10" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("colPickUpDate"))%>'
                                                Width="105px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblCheckIn" Text='<%# String.Format("{0:hh:mm}",Eval("colPickUpTime"))%>'
                                                Width="70px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblDeptCity" Text='<%# Eval("colFromVarchar")%>'
                                                Width="55px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("colToVarchar")%>' Width="55px"></asp:Label>
                                        </td>
                                       
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="Label1" Text='<%# Eval("colSFStatus")%>' Width="55px"></asp:Label>
                                        </td>                        
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLabelLast" Text='<%# Eval("LastName")%>' Width="150px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:LinkButton Width="150px" runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("SeafarerIdInt") + "&trID=" + Eval("colTravelReqIDInt") + "&st=" + Eval("colSFStatus") + "&recloc=" + Eval("RecordLocator") + "&manualReqID=" + Eval("colRequestIDInt") + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("colIdBigint") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("FirstName") %></asp:LinkButton>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("SeafarerIdInt")%>' Width="78px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblGender" Text='<%# Eval("Gender")%>' Width="50px"></asp:Label>
                                        </td>
                                      
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblCostCenter" Text='<%# Eval("CostCenter")%>' Width="98px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblRank" Text='<%# Eval("RankName")%>' Width="221px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblVessel" Text='<%# Eval("VesselName")%>' Width="170px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator")%>' Width="62px"></asp:Label>
                                        </td>                        
                                      
                                         <td class="centerAligned" style="white-space: normal; display:none;">
                                            <asp:HiddenField ID="uoHiddenFieldUnAssignedIDs" runat="server" Value='<%# Convert.ToString(Eval("colIdBigint")) %>' />
                                            <asp:HiddenField ID="uoHiddenFieldTransID" runat="server" Value='<%# Eval("TransVehicleID") %>' />
                                            <asp:Label Height="8px" ID="Labeldummy1" runat="server" Text="" />
                                            <asp:Label Width="50px" ID="Label6" runat="server" Text="" Visible='<%# Convert.ToString(Eval("colVehicleVendorIDInt")) == "0"? true: false %>'></asp:Label>
                                        </td>
                                        
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table class="listViewTable">
                                     <tr>
                                            <%--<th style="text-align: center; white-space: normal;">
                                                <asp:Label ID="Label5" runat="server" Text="" Width="50px"></asp:Label>
                                                <br />
                                                <asp:CheckBox runat="server" ID="CheckBox0" Width="24px" CssClass="Checkbox" onclick="SetSettings(this);" />
                                            </th>--%>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLinkButtonRouteFrom" runat="server" CommandName="SortByRouteFrom"
                                                    Text="Route From" Width="150px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="SortByRouteTo" Text="Route To"
                                                    Width="150px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByOnOffdate" Text="Pickup Date"
                                                    Width="100px" />
                                            </th>                            
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLinkButtonPickupDate" runat="server" CommandName="SortByPickupDate"
                                                    Text="Pickup Time" Width="64px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="LinkButton6" runat="server" CommandName="SortByFromCity" Text="From City"
                                                    Width="50px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="LinkButton7" runat="server" CommandName="SortByToCity" Text="To City"
                                                    Width="50px" />
                                            </th>
                                            
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByOnOff" Text="Status"
                                                    Width="45px" />
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
                                                    Text="Employee ID" Width="70px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLblGenderHeader" runat="server" CommandName="SortByGender"
                                                    Text="Gender" Width="45px" />
                                            </th>
                                           
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="SortByCostCenter"
                                                    Text="Cost Center" Width="90px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLblTitleHeader" runat="server" CommandName="SortByTitle" Text="Title"
                                                    Width="215px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLblShipHeader" runat="server" CommandName="SortByShip" Text="Ship"
                                                    Width="164px" />
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <asp:LinkButton ID="uoLblRLHeader" runat="server" CommandName="SortByRecordLocator"
                                                    Text="Record Locator" Width="55px" />
                                            </th>
                                           
                                        </tr>
                                        <tr>
                                            <td colspan="35" class="leftAligned">
                                                No Record
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <div align="left">
                                <asp:DataPager ID="uoDataPagerManifest" runat="server" PagedControlID="uoListViewManifestDetails"
                                    PageSize="20">
                                    <Fields>
                                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                                    </Fields>
                                </asp:DataPager>
                            </div>
                    </asp:panel>
                
                </td>
             </tr>
             
             </table> 
       
       
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldVendorID" runat="server" />   
        <asp:HiddenField ID="uoHiddenFieldVendor" runat="server" />       
            
    </div>
        

    </form>
</body>
</html>
