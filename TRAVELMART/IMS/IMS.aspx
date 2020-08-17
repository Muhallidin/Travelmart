<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMasterPage.Master" 
    AutoEventWireup="true" CodeBehind="IMS.aspx.cs" Inherits="TRAVELMART.IMS.IMS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>   

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
     <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
         <tr>
             <td class="PageTitle">
                
                <asp:Label runat="server" ID="lblIMSTitle" class="PageTitle">Invoice Management System</asp:Label>
                
                               
             </td>                
         </tr>
     </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    
    

<%--
  <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="uoPanelMaster" >
      <ContentTemplate>
      --%>
       <table width="100%"  >
            <tr style=" height:100%; width:30 ;vertical-align:text-top;   ">
                <td  style="vertical-align:text-top;" id="tdListbox" >
                    <div id="divListBox" style="width:350px; height:98%; background-color:Blue;"  >
                        <asp:ListBox ID="lstVendor" runat="server" style="padding:5px; width:350px; border-style:solid; border-width:1px;" 
                            ondblclick="lstVendor_DoubleClick()"/> 
                    </div>
                </td>
                <td  style=" vertical-align:top;width:100%; padding-left:5px ">
                    <table width="100%"  style="height:100%;  vertical-align:text-top;" >
          
                         <tr  style="vertical-align:top;">
                             <td style="white-space:nowrap; padding-right:20px; ">
                                 <fieldset style="padding-left:20px; padding-top:-20px;" >
                                    <legend  style="margin-left:-10px;">Invoice Date</legend>
                                     From : 
                                     <asp:TextBox ID="txtFromDate" runat="server" Text="" Width="80px" ></asp:TextBox>
                                     <cc1:TextBoxWatermarkExtender ID="txtFromDate_Textboxwatermarkextender"
                                         runat="server" Enabled="True" TargetControlID="txtFromDate" WatermarkCssClass="fieldWatermark"
                                         WatermarkText="MM/dd/yyyy">
                                     </cc1:TextBoxWatermarkExtender>
                                     <cc1:CalendarExtender ID="txtFromDate_Calendarextender" runat="server" Enabled="True"
                                         TargetControlID="txtFromDate" Format="MM/dd/yyyy">
                                     </cc1:CalendarExtender>
                                     <cc1:MaskedEditExtender ID="txtFromDate_Maskededitextender" runat="server"
                                         CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                         CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                         CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate"
                                         UserDateFormat="MonthDayYear">
                                     </cc1:MaskedEditExtender>
                                     <%-- 
                                          <asp:RequiredFieldValidator ID="uoRFVCheckIn" ControlToValidate="uoTextBoxCheckinDate"
                                               ValidationGroup="Request" runat="server" ErrorMessage="required" Font-Bold="true" />
                                     --%>
                                      &nbsp;&nbsp; &nbsp;&nbsp;
                                    To : 
                                     <asp:TextBox ID="txtToDate" runat="server" Text="" Width="80px" ></asp:TextBox>
                                     <cc1:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender"
                                         runat="server" Enabled="True" TargetControlID="txtToDate" WatermarkCssClass="fieldWatermark"
                                         WatermarkText="MM/dd/yyyy">
                                     </cc1:TextBoxWatermarkExtender>
                                     <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                         TargetControlID="txtToDate" Format="MM/dd/yyyy">
                                     </cc1:CalendarExtender>
                                     <cc1:MaskedEditExtender ID="txtToDate_MaskedEditExtender" runat="server"
                                         CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                         CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                         CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate"
                                         UserDateFormat="MonthDayYear">
                                     </cc1:MaskedEditExtender>
                                     <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="SmallButton"  OnClick="btnHidden_click" />
                                 </fieldset>
                             </td>
                             <td style="width:100%; white-space:nowrap; vertical-align:middle; padding-bottom:5px; ">
                                Invoice Status : <asp:DropDownList ID="cboStatus" runat="server" OnSelectedIndexChanged="cbostatus_SelectedIndexChanged" AutoPostBack="true"  />
                             </td>
                         </tr >
                         
                         <tr>
                         
                            <td  colspan="3" id="tdListView"   >
                              <div id='pnlListView' style="overflow:auto;" >   
                            
                                 <asp:ListView runat="server" ID="ListView1" ItemPlaceholderID="InvoicePlaceHolder">
                                        <LayoutTemplate>
                                            <table border="0" id="uoListviewInvoiceTable" cellpadding="0"  cellspacing="0" class="listViewTable">
                                                <tr>
                                                    
                                                    <th style="text-align: center; white-space: normal; width:10px;"></th>
                                                    <th style="text-align: center; white-space: normal; width:130px;">
                                                        Vendor Number
                                                    </th >
                                                    <th style="text-align: center; white-space: normal;">
                                                        Invoice Number
                                                    </th>
                                                    <th style="text-align: center; white-space: normal;">
                                                        Invoice Date
                                                    </th>
                                                    <th style="text-align: center; white-space: normal;">
                                                        Ship
                                                    </th>
                                                    <th style="text-align: center; white-space: normal;">
                                                        Port
                                                    </th>
                                                    <th style="text-align: center; white-space: normal;">
                                                        Unit Code
                                                    </th>  
                                                    <th style="text-align: center; white-space: normal;">
                                                        Status
                                                    </th> 
                                                    
                                                    <%--<th style="width:-1px;margin:-0px;"/>--%>
                                                    
                                                </tr>        
                                                <asp:PlaceHolder runat="server" ID="InvoicePlaceHolder"></asp:PlaceHolder>  
                                                            
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                          <tr>
                                            <td id='<%# Eval("InvoiceNumber") %>'>
                                                <asp:CheckBox CssClass="Checkbox" style="margin-left:0px;"  ID="uoCheckBoxVCost" runat="server"/> 
                                            </td>
                                            <td class="leftAligned" style="white-space:nowrap; display:none; width:100px;" >
                                                <asp:Label runat="server" ID="Label3" style="display:none;" />
                                            </td>
                                            <td class="leftAligned" style="white-space: nowrap; width:100px;" >
                                                <asp:Label runat="server" ID="lblVendorNumID" Text='<%# Eval("VendorNumID")%>' />
                                            </td>
                                            <td class="leftAligned" style="white-space: nowrap;" >
                                                <asp:Label runat="server" ID="lblInvoiceNumber"  Text='<%# Eval("InvoiceNumber") %>'/>
                                            </td>
                                            <td class="leftAligned" style="white-space: nowrap;" >
                                                <asp:Label runat="server" ID="lblInvoiceDate"   Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("InvoiceDate"))%>'/>
                                            </td>
                                            <td class="leftAligned">
                                                <asp:Label runat="server" ID="lblShip" Text='<%# Eval("Ship")%>'/>
                                            </td>
                                            <td class="leftAligned" style="white-space: normal;" >
                                                 <asp:Label runat="server" ID="lblPort"   Text='<%# Eval("Port")%>'/>
                                            </td>
                                            <td class="leftAligned" style="white-space: normal; width:30px;" >
                                                <asp:Label runat="server" ID="lblLabelHotelRate" Text='<%# Eval("BusinessUnitCode") %>'  />
                                            </td>
                                              
                                            <td style="white-space:nowrap; vertical-align:middle; padding-top:3px; 
                                                    border-right-width:thin; text-align:center; margin:-0px; width:-1px;">
                                          
                                               <img id="ImageButton" alt="Click the Status" title='<%# Eval("Exception") %>';
                                                    onclick="ShowInvoiceDetail('err<%# Eval("InvoiceNumber") %>')"
                                                    ondblclick="HideInvoiceDetail('err<%# Eval("InvoiceNumber") %>')"
                                                    src='<%# Eval("Image") %>'
                                                    style="size:20px; width:20px; height:20px;"/>
                                                    
                                               <asp:HiddenField runat="server" ID="uoInvoiceNumID" Value='<%# Eval("InvoiceNumID") %>' /> 
                                               <asp:HiddenField runat="server" ID="uoHiddenFieldException" Value='<%# Eval("ExceptionID") %>' /> 
                                            
                                            </td>
                                            
                                        </tr>
                                        
                                         
                                        <tr >
                                            <td></td>
                                            <td  colspan="7" >
                                             
                                                 <%--begin invoice detail--%>   
                                                 <div  id='detail<%# Eval("InvoiceNumber")%>' style="display:none;" >
                                                  
                                                      <asp:ListView runat="server" ID="ListViewDetail" DataSource='<%# Eval("InvoiceDetail") %>' ItemPlaceholderID="ExceptionDetailPlaceHolder" >
                                                            <LayoutTemplate>
                                                                
                                                                <table border="0" id="uoListviewInvoiceTable" cellpadding="0"  cellspacing="0" class="listViewTable">
                                                                    <tr>
                                                                       <%-- <th style="text-align: center; white-space: normal; width:30px;"></th>--%>
                                                                        <th style="text-align: center; white-space: normal; width:130px;">
                                                                            Expense Type
                                                                        </th >
                                                                        <th style="text-align: center; white-space: normal;">
                                                                            Quantiy
                                                                        </th>
                                                                        <th style="text-align: center; white-space: normal;">
                                                                            Unit Cost
                                                                        </th>
                                                                        <th style="text-align: center; white-space: normal;">
                                                                            Employee
                                                                        </th>
                                                                        <th style="text-align: center; white-space: normal;">
                                                                            Start Date
                                                                        </th>
                                                                        <th style="text-align: center; white-space: normal;">
                                                                            End Date
                                                                        </th>  
                                                                        <th style="text-align: center; white-space: normal;">
                                                                            Unit Type
                                                                        </th> 
                                                                        <th style="text-align: center; white-space: normal;">
                                                                            Trip No.
                                                                        </th> 
                                                                        <th style="text-align: center; white-space: normal;">
                                                                            Comment
                                                                        </th> 
                                                                        <th style="text-align: center; white-space: nowrap;">
                                                                            Total Cost
                                                                        </th> 
                                                                        <th runat="server" id="img" style="width:-1px;margin:-0px;"/>
                                                                    </tr>        
                                                                    <asp:PlaceHolder runat="server" ID="ExceptionDetailPlaceHolder"></asp:PlaceHolder>  
                                                                    <tr style="width:100% " >
                                                                        <td style="text-align: left; white-space: nowrap;" >
                                                                            <input type="button"  id="btnSendInvoice" value="Submit Invoice"  title="Submit Invoice" 
                                                                                class="SmallButton" onclick="SendInvoice(this)"                                                                                 
                                                                                />
                                                                        </td>
                                                                        <td colspan="8" style="text-align: right; white-space: nowrap; width:100%;">
                                                                            Total :
                                                                        </td>
                                                                        <td  colspan="1" style="text-align: center; white-space: nowrap; width:100%; ">
                                                                            <asp:Label runat="server" CssClass="lblTotal" ID="lblTotal" Text='<%# Eval("TotalCost")%>' />
                                                                        </td>
                                                                         <td   style="white-space:nowrap; vertical-align:middle; padding-top:3px; 
                                                                            border-right-width:thin; text-align:center; margin:-0px; ">
                                                                        
                                                                    
                                                                        </td>
                                                                    </tr>         
                                                                </table>
                                                            </LayoutTemplate>
                                                            
                                                            <ItemTemplate>
                                                                <tr id='<%# Eval("InvoiceDetailID")%>' style='<%# Eval("Backcolor")%>' >
                                                                    <%--<td id='<%# Eval("InvoiceNumber") %>'>
                                                                        <input type="checkbox" style="margin-left:0px;" 
                                                                        onclick="ExcludeInvoiceDetail('<%# Eval("InvoiceDetailID")%>')"
                                                                        id="img<%# Eval("InvoiceNumber")%>" /> 
                                                                    </td>--%>
                                                                    <td  style="text-align: right; white-space:nowrap; display:none; width:100px;" >
                                                                        <asp:Label runat="server" ID="Label3" style="display:none;" />
                                                                    </td>
                                                                    <td style="text-align: left; white-space: nowrap; width:130px;">
                                                                        <asp:Label runat="server" ID="lblExpenseTypeCode"   Text='<%# Eval("ExpenseTypeCode")%>'/>
                                                                    </td >
                                                                    <td style="text-align: center; white-space: normal;">
                                                                        <asp:Label runat="server" ID="lblQuantity"   Text='<%# Eval("Quantity")%>'/>
                                                                    </td>
                                                                    <td style="text-align: center; white-space: normal;">
                                                                        <asp:Label runat="server" ID="Label1"   Text='<%# Eval("UnitCost")%>'/>
                                                                    </td>
                                                                    <td style="text-align: center; white-space: normal;">
                                                                        <asp:Label runat="server" ID="Label2"   Text='<%# Eval("EmployeeNumber  ")%>'/>
                                                                    </td>
                                                                    <td style="text-align: center; white-space: normal;">
                                                                        <asp:Label runat="server" ID="lblCrewServiceStartDate"   Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CrewServiceStartDate"))%>'/>
                                                                    </td>
                                                                    <td style="text-align: center; white-space: normal;">
                                                                        <asp:Label runat="server" ID="Label4"   Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CrewServiceEndDate"))%>'/>
                                                                    </td>  
                                                                    <td style="text-align: center; white-space: normal;">
                                                                        <asp:Label runat="server" ID="Label5"   Text='<%# Eval("UnitofMeasureType")%>' style="text-align:left;"/>
                                                                    </td> 
                                                                    <td style="text-align: center; white-space: normal;">
                                                                        <asp:Label runat="server" ID="Label8"   Text='<%# Eval("TripNumber")%>'/>
                                                                    </td> 
                                                                    <td style="text-align:center; white-space:normal ;" id="<%# Eval("InvoiceDetailID")%>"
                                                                             class="expenseComment" >
                                                                        <asp:Label runat="server" ID="lblComment" Width="200px"  Text='<%# Eval("Comment")%>' Style="word-break: break-all;" />
                                                                    </td> 
                                                                    <td style="text-align: center; white-space: normal;">
                                                                        <asp:Label runat="server" ID="lblTotalCost"   Text='<%# Eval("TotalCost")%>'/>
                                                                        
                                                                    </td>
                                                                    <td   style="white-space:nowrap; vertical-align:middle; padding-top:3px; 
                                                                            border-right-width:thin; text-align:center; margin:-0px; width:-1px;">
                                                                            <%--onmouseover="ShowInvoiceDetail('<%# Eval("InvoiceNumber") %>')"
                                                                            onclick="HideInvoiceDetail('<%# Eval("InvoiceNumber") %>')"--%>
                                                                       <img id="img<%# Eval("InvoiceNumber")%>" alt="Click the Status"                                                                            
                                                                            onclick="ExcludeInvoiceDetail('<%# Eval("InvoiceDetailID")%>')"
                                                                            src="../Images/cut.ico"
                                                                            style="size:15px; width:15px; height:15px; display"/>
                                                                        <asp:HiddenField runat="server" ID="hiddentFieldInvDetailID" Value= '<%# Eval("InvoiceDetailID")%>'/> 
                                                                        <asp:HiddenField runat="server" ID="HiddenFieldException" Value= '<%# Eval("IsExcetion")%>'/> 
                                                                        <asp:HiddenField runat="server" ID="HiddenFieldExclude" Value= '<%# Eval("Exclude")%>'/> 
                                                                    </td>
                                                                    
                                                                     
                                                                </tr>
                                                                 
                                                            </ItemTemplate>
                                                            <InsertItemTemplate>
                                                                 <%--<table style="width:100% " >--%>
                                                                  <tr style="width:100% " >
                                                                        <td>
                                                                            <input type="button"   id="btnSendInvoice"  value="Submit Invoice" title="Submit Invoice" CssClass="SmallButton" />
                                                                        </td>
                                                                        <td colspan="10" style="text-align: right; white-space: nowrap; width:100%; padding-right:14px; ">
                                                                            <asp:Label runat="server" ID="lblTotal" Text='Total: 200.00' />
                                                                           
                                                                        </td>
                                                                         <td   style="white-space:nowrap; vertical-align:middle; padding-top:3px; 
                                                                            border-right-width:thin; text-align:center; margin:-0px; ">
                                                                        
                                                                    
                                                                        </td>
                                                                    </tr>
                                                            <%--</table>--%>
                                                            
                                                            
                                                            </InsertItemTemplate>
                                                            <EmptyItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td class="leftAligned" style="white-space:nowrap; display:none; width:100px;" >
                                                                            <asp:Label runat="server" ID="Label3" style="display:none;" />
                                                                        </td>
                                                                        <td style="text-align: center; white-space: normal; width:130px;">
                                                                                Expense Type
                                                                        </td >
                                                                        <td style="text-align: center; white-space: normal;">
                                                                            Quantiy
                                                                        </td>
                                                                        <td style="text-align: center; white-space: normal;">
                                                                            Unit Cost
                                                                        </td>
                                                                        <td style="text-align: center; white-space: normal;">
                                                                            Employee Number
                                                                        </td>
                                                                        <th style="text-align: center; white-space: normal;">
                                                                            Start Date
                                                                        </th>
                                                                        <td style="text-align: center; white-space: normal;">
                                                                            End Date
                                                                        </td>  
                                                                        <td style="text-align: center; white-space: normal;">
                                                                            Unit Type
                                                                        </td> 
                                                                        <td style="text-align: center; white-space: normal;">
                                                                            Trip No.
                                                                        </td> 
                                                                        <td style="text-align: center; white-space: normal;">
                                                                            Comment
                                                                        </td> 
                                                                        <td style="text-align: center; white-space: normal;">
                                                                            Total Cost
                                                                        </td> 
                                                                    </tr>
                                                                </table>
                                                            </EmptyItemTemplate>
                                                        </asp:ListView> 
                                                       
                                                 </div>
                                                 
                                                 <%--End invoice detail--%>   
                                            
                                                 <%-- begin treeview panel  --%>
                                                  <div  style="display:none" id='err<%# Eval("InvoiceNumber")%>'>
                                                        <asp:ListView runat="server" ItemPlaceholderID="placeHolderException" 
                                                            ID="LstException"  DataSource='<%# Eval("InvoiceException") %>'>
                                                            <LayoutTemplate>
                                                                 <table border="0" id="uoListviewExceptionTable" cellpadding="0"  cellspacing="0" class="listViewTable">
                                                                         <tr>
                                                                             <th style="text-align: center; white-space: normal;">
                                                                                 Status
                                                                             </th> 
                                                                             <th style="text-align: center; white-space: normal;">
                                                                                 Message
                                                                             </th> 
                                                                             <th style="text-align: center; white-space: normal; width:130px;">
                                                                                 Vendor Number
                                                                             </th >
                                                                             <th style="text-align: center; white-space: normal;">
                                                                                 Invoice Number
                                                                             </th>
                                                                             <th style="text-align: center; white-space: normal;">
                                                                                 Date Submitted
                                                                             </th>
                                                                             <%--<th style="width:-1px;margin:-0px;"/>--%>
                                                                         </tr>        
                                                                         <asp:PlaceHolder runat="server" ID="placeHolderException" />            
                                                                      </table>    
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                            <tr> 
                                                                    <td style="text-align: center; white-space: normal;">
                                                                        <asp:Label runat="server" ID="lblSuccess" Text='<%# Eval("Success")%>' />
                                                                    </td> 
                                                                    <td style="text-align: center; white-space: normal;">
                                                                        <asp:Label runat="server" ID="lblMessage" Text='<%# Eval("Message")%>' />
                                                                    </td> 
                                                                    <td style="text-align: center; white-space: normal; width:130px;">
                                                                       <asp:Label runat="server" ID="lblInvoiceNumber" Text='<%# Eval("InvoiceNumber")%>' />
                                                                    </td >
                                                                    <td style="text-align: center; white-space: normal;">
                                                                        <asp:Label runat="server" ID="lblVendorNumber" Text='<%# Eval("VendorNumber")%>' />
                                                                    </td>
                                                                    <td style="text-align: center; white-space: normal;">
                                                                        <asp:Label runat="server" ID="lblInvoiceDate" Text='<%# Eval("DateCreated")%>' />
                                                                    </td>
                                                            </tr>        
                                                            <tr>
                                                                <td colspan="5">
                                                                        <asp:ListView ID="lvExceptionDetail" runat="server" DataSource='<%# Eval("InvoiceExceptionDetail")%>' ItemPlaceholderID="ExceptionDetailPlaceHolder">
                                                                        <LayoutTemplate>
                                                                            <div style="padding-left: 30px;">
                                                                                <table border="1" cellpadding="0" cellspacing="0" width="100%" >
                                                                                    <tr>
                                                                                        <th style="text-align: left; white-space: nowrap;">
                                                                                            Owner &nbsp;&nbsp;
                                                                                        </th> 
                                                                                        <th style="text-align: left; white-space: normal;">
                                                                                            Exception Error
                                                                                        </th> 
                                                                                        <th style="text-align: center; white-space: normal;">
                                                                                            Status
                                                                                        </th> 
                                                                                    </tr> 
                                                                                    <asp:PlaceHolder runat="server" ID="ExceptionDetailPlaceHolder" />
                                                                                 </table>
                                                                            </div>
                                                                            <hr />
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="text-align: left; white-space: normal;">
                                                                                     <asp:Label ID="lblOwner" Text='<%# Eval("Owner") %>' runat="server" />
                                                                                </td>
                                                                                <td style="text-align: left; white-space: normal;">
                                                                                     <asp:Label ID="lblErrorMessage" Text='<%# Eval("ErrorMessage") %>' runat="server" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblType" Text='<%# Eval("Type") %>' runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView> 
                                                         
                                                  </div>
                                                  <%--end treeview panel--%>
                                            </td>
                                        
                                        
                                        </tr>
                                        
                                    </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table  class="listViewTable" >
                                               <tr>
                                                   <th style="text-align: center; white-space: normal; width:100px;">
                                                       Vendor Number
                                                   </th >
                                                   
                                                   <th style="text-align: center; white-space: normal; width:120px;">
                                                       Invoice Number
                                                   </th>
                                                   
                                                   <th style="text-align: center; white-space: normal;">
                                                       Invoice Date
                                                   </th>
                                                   
                                                   <th style="text-align: center; white-space: normal;">
                                                       Ship
                                                   </th>
                                                   
                                                   <th style="text-align: center; white-space: normal;">
                                                       Port
                                                   </th>
                                                   
                                                   <th style="text-align: center; white-space: normal;">
                                                       Unit Code
                                                   </th>  
                                                   <th style="text-align: center; white-space: normal;">
                                                       Status
                                                   </th> 
                                                </tr>  
                                                <tr>
                                                    <td colspan="8" class="leftAligned"  >
                                                        No Record
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                 
                              </div> 
                                 
                            </td>
                         </tr>
                         
                     </table>
                
                </td>
            </tr>
       </table>
      
      
        
<%--      </ContentTemplate>
  </asp:UpdatePanel>--%>
  
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value=""  />
    <asp:HiddenField ID="uoHiddenFieldPath" runat="server" Value=""  />
    <asp:TextBox ID="txtTotalCost" runat="server" style="position:absolute;display:none; "></asp:TextBox>
    <asp:Button ID="btnHidden" runat="server" Text="click me" style="display:none;" OnClick="btnHidden_click" />
    <asp:ListBox ID="lstSeaport" runat="server" style="padding:5px; border-style:solid; border-width:1px; position:absolute;display:none; "/> 
    <asp:HiddenField ID="uoHiddenFieldGen" runat="server" Value=""  />
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value=""  />
    <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value=""  />
    
    
    <div id="divComment" style="position:absolute; top:400px; background-color:White; 
            border-style:groove; left:700px; padding:2px 0px 2px 2px; text-align:right; display:none;">
           <asp:TextBox ID="txtComment" Rows="6" runat="server" TextMode="MultiLine" Height="60px" Width="300px" style="text-align:left"/>
           <br />

           <input type="button" id="btnComment" value="Add Comment" class="SmallButton" 
                  style="margin-right:3px;" onclick="AddComment(this)"/>
           <input type="button" id="btnCommentClose" value="Close" class="SmallButton" 
                  style="margin-right:3px;"/>       
                    
           <asp:HiddenField ID="uoHiddenFieldCommentID" runat="server" Value=""/>
           <asp:HiddenField ID="uoHiddenFieldComment" runat="server" Value=""/>
           
    </div>
    
     
    <script type="text/javascript" language="javascript">

        $(document).ready(function() {
            TriggleJquery();
        });


        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                TriggleJquery();
            }
        }

        function AddComment(val) {

            $("#divComment").css({ display: 'none' });
            console.log(document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldComment").value);
            var comID = document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldComment").value;

            var comment = document.getElementById("ctl00_NaviPlaceHolder_txtComment").value;
            
            
            console.log(document.getElementById("ctl00_NaviPlaceHolder_txtComment").value);
            console.log(document.getElementById(comID).innerText);
            
            document.getElementById(comID).innerText = comment;

            console.log(document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldCommentID").value);
            var InvoiceDetID = document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldCommentID").value;
            
            var hfInvoiceDetID = document.getElementById(InvoiceDetID).value;
            console.log(hfInvoiceDetID);
        
            var userID = document.getElementById('ctl00_NaviPlaceHolder_uoHiddenFieldUser').value;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../PageMethods.aspx/GetUpdateInvoiceDetail",
                data: "{'UpdateID': 1,'InvoiceDetailID': '" + InvoiceDetID + "','UpdateValue': '" + comment + "','UserID': '" + userID + "'}",
                dataType: "json",
                success: function(data) { }
            });


            document.getElementById("ctl00_NaviPlaceHolder_txtComment").value = '';


 
        }


        function TriggleJquery() {





            var height = screen.height;


            console.log(screen);
            $("[id$=lstVendor]").height((height - 370) + "px");
            document.getElementById("ctl00_NaviPlaceHolder_lstVendor").style.maxHeight = (height - 370) + "px";
            document.getElementById("divListBox").style.Height = (height - 370) + "px";
            
//            console.log($("[id$=pnlListView]").height);
//            console.log(document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRole").value);
//            console.log($("[id=tdListView]")[0].scrollWidth);
//            console.log($("tdListView").width() + ' test wali' );
//           
            
            var w = $("[id=tdListView]")[0].scrollWidth;
            $("[id$=pnlListView]").width(w + "px");
          
            
            if (document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRole").value == 'Hotel Vendor') {
                
                document.getElementById('tdListbox').style.display = "None";
                document.getElementById('tdListbox').style.width = "-1%";
                document.getElementById('divListBox').style.Height =  "-1%";
                document.getElementById('divListBox').style.display = "None";
                
           }
           else
           {
                
                document.getElementById('tdListbox').style.display = "Block";
                document.getElementById('tdListbox').style.width = "20%";
                document.getElementById('divListBox').style.Height = height - 100 + "px";
                $('#<%=this.lstVendor.ClientID%>').attr('size', $('#<%=this.lstVendor.ClientID%> option').length);
           }

           var objDiv = document.getElementById("divListBox");
           objDiv.scrollTop = objDiv.scrollHeight;
            

           $("[id$=lblTotalCost]").click(function() {
               $("[id$=uoHiddenFieldGen]").val(this.id);
               var parnt = $($(this).parents());
               var offset = $(this).offset();
               var w = $($(this).parents()).width();
               var h = $($(this).parents()).height();
               var xPos = offset.left - 11.5;
               var yPos = offset.top - 5;

               var tPos = $($(this).parents().offset().top);
               var lPos = $($(this).parents().offset().left);

               console.log(xPos);
               console.log(lPos);

               console.log(yPos);
               console.log(tPos);



               var excControl = this.id;
               console.log(document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRole").value);

               var ExcID = excControl.replace("lblTotalCost", "HiddenFieldException");

               if (document.getElementById(ExcID).value != "1" && document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRole").value != 'Hotel Vendor') {
                   $("[id$=txtTotalCost]").css({ top: tPos[0], left: lPos[0], position: 'absolute', zIndex: 5000, display: 'block', width: w, height: h });
                   $("[id$=txtTotalCost]").val(this.innerText);
                   $("[id$=txtTotalCost]").focus();
               }
           });

            $("[id$=txtComment]").keyup(function(e) {
                if (e.keyCode == '27') {
                    console.log(e.keyCode);
                    $("#divComment").css({ display: 'none' });
                }
            });
            

            $("[id$=txtTotalCost]").keyup(function(e) {
                if (e.keyCode == 13) {

                    var TotolCostID = $("[id$=uoHiddenFieldGen]").val();
                    var InvoiceDetID = TotolCostID.replace("lblTotalCost", "hiddentFieldInvDetailID");
                    var hfInvoiceDetID = document.getElementById(InvoiceDetID).value;
                    console.log(hfInvoiceDetID);
                    var Total = 0.0000;
                    Total = parseFloat($("[id$=txtTotalCost]").val());
                    console.log($(this).find("[id$=txtTotalCost]"));
                    document.getElementById($("[id$=uoHiddenFieldGen]").val()).innerText = Total.toFixed(4);
                    $("[id$=txtTotalCost]").css({ position: 'absolute', display: 'none' });

                    var userID = document.getElementById('ctl00_NaviPlaceHolder_uoHiddenFieldUser').value;
                    
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "../PageMethods.aspx/GetUpdateInvoiceDetail",
                        data: "{'UpdateID': 0,'InvoiceDetailID': '" + hfInvoiceDetID + "','UpdateValue': '" + Total.toFixed(4) + "','UserID': '" + userID + "'}",
                        dataType: "json",
                        success: function(data) {
                        
                        }
                    });

                    var parnt = $($(TotolCostID)).parents();
                    console.log(parnt);

                       
                    
                }
            }).focusout(function() {

                $(this).css({ position: 'absolute', display: 'none' });
            });



            $("[id$=lblPort]").click(function() {
                $("[id$=uoHiddenFieldGen]").val(this.id);

                var excID = this.id.replace("lblPort", "uoHiddenFieldException");

                var offset = $(this).offset();
                var w = $($(this).parents()).width();
                var xPos = offset.left - 3;
                var yPos = offset.top - 6;
                console.log(document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRole").value);
                if (document.getElementById(excID).value != "1" && document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRole").value != 'Hotel Vendor') { 
                    $("[id$=lstSeaport]").css({ top: yPos, left: xPos, position: 'absolute', zIndex: 5000, display: 'block', width: w + 5 });
                }
            });



            $(".expenseComment").click(function() {

                $("[id$=uoHiddenFieldCommentID]").val(this.id);


                var offset = $(this).offset();
                var w = $($(this).parents()).width();
                var xPos = offset.left;
                var yPos = offset.top;



                var excID = $(this).children()[0].id.replace("lblComment", "HiddenFieldException");
                
              
                if (document.getElementById(excID).value != "1" ) { //&& document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRole").value != 'Hotel Vendor') {
                    $("#divComment").css({ top: yPos, left: xPos, position: 'absolute', zIndex: 5000, display: 'block' });
                    $("[id$=txtComment]").focus();
                }
                $("[id$=txtComment]").val($(this).children()[0].innerText);
                $("[id$=uoHiddenFieldComment]").val($(this).children()[0].id);

            });
            $("#btnCommentClose").click(function() {
                $("#divComment").css({position: 'absolute', display: 'none' });
            });
            
            $("[id$=lstSeaport]").mouseleave(function() {
                $("[id$=lstSeaport]").css({ display: 'none', position: 'absolute' });
            }).click(function() {

                var r = confirm("Are you sure you want to update the selected port?");
                if (r == true) {
                    var userID = document.getElementById('ctl00_NaviPlaceHolder_uoHiddenFieldUser').value;
                    $("[id$=lstSeaport]").css({ display: 'none', position: 'absolute' });
                    $(this).find(":selected").each(function() {
                        var HiddenFieldGen = $("[id$=uoHiddenFieldGen]").val();
                        var inID = HiddenFieldGen.replace('lblPort', 'lblInvoiceNumber');
                        var invoiceNumber = document.getElementById(inID).innerText
                        console.log(inID);
                        var selectedPort = this.innerText;
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "../PageMethods.aspx/UpdateInvoiceHotelPort",
                            data: "{'InvoiceNumber': '" + invoiceNumber + "','PortNumber': '" + this.value + "','UserID': '" + userID + "'}",
                            dataType: "json",
                            success: function(data) {
                                console.log(data.d);
                                console.log($("[id$=uoHiddenFieldGen]").val());
                                console.log(selectedPort);
                                if (data.d == 1) {
                                    $("[id$=lstSeaport]").css({ display: 'none', position: 'absolute' });
                                    document.getElementById($("[id$=uoHiddenFieldGen]").val()).innerText = selectedPort;
                                }
                            }
                        });
                    });
                }
            });


            $("[id$=uoCheckBoxVCost]").change(function() {

                var num = this.id.replace("ctl00_NaviPlaceHolder_ListView1_ctrl", "").replace("_uoCheckBoxVCost", "");
                
                var VendorNumber = document.getElementById("ctl00_NaviPlaceHolder_ListView1_ctrl" + num + "_lblInvoiceNumber").innerHTML
                var imgheader = "ctl00_NaviPlaceHolder_ListView1_ctrl" + num + "_img";

                var detail = 'detail' + VendorNumber;
                var imge = 'img' + VendorNumber;

                if (this.checked)
                    document.getElementById(detail).style.display = 'block';
                else
                    document.getElementById(detail).style.display = 'none';


                var excval = this.id.replace("uoCheckBoxVCost", "uoHiddenFieldException");
                var btnInvoice = this.id.replace("uoCheckBoxVCost", "ListViewDetail_btnSendInvoice");
                
                SumTotal(detail, VendorNumber, excval);

                console.log(document.getElementById(excval).value);

                if (document.getElementById(excval).value == '1') {
                    $("[id$=" + imge + "]").each(function() {
                        this.style.visibility = 'hidden';
                    });
                }
                else {
                    console.log('test wali ' + imge );
                    $("[id$=" + imge + "]").each(function() {

                        this.style.visibility = 'visible';
                    });
                }
            });
        }


        function SumTotal(detail, VendorNumber, excval) {

            console.log('wali ' + ' ' + detail + ' ' + VendorNumber + ' ' + excval);
        
            $("#" + detail).each(function() {

              
                $(this).find(".SmallButton").attr('id', VendorNumber);
                console.log($(this).find(".SmallButton"));

                var IsPortAgent = false;
                IsPortAgent = $("[id$=uoHiddenFieldRole]").val() == 'Service Provider' ? true : false;

                if (document.getElementById(excval).value == '1' || IsPortAgent == true) {
                    $(this).find(".SmallButton").css("display", "none");
                }
                else {
                    $(this).find(".SmallButton").css("display", "inline");
                }

                var Total = 0.0000;

                console.log($(this).find("[id$=lblTotalCost]"));
                $($(this).find("[id$=lblTotalCost]")).each(function() {

                    var exc = this.id.replace("lblTotalCost", "HiddenFieldExclude")
                    console.log(exc);
                    console.log(document.getElementById(exc).value);
                    if (document.getElementById(exc).value == 'False') {
                        Total += parseFloat(this.innerHTML);
                    }

                    console.log(this.id);
                    console.log(this.innerHTML);
                    console.log(this.parentElement);
                    console.log(Total);
                });
                $(this).find(".lblTotal").html(Total.toFixed(4));
            }); 
        }

        function ShowInvoiceDetail(val) {

            console.log(val);
            document.getElementById(val).style.display = "100%";

            document.getElementById(val).style.display = 'block';
            console.log(document.getElementById(val).style.display);
        }
        
        function HideInvoiceDetail(val) {
            document.getElementById(val).style.display = 'none';

            console.log(document.getElementById(val).style.display);
        }

        function lstVendor_DoubleClick() {
            
            document.getElementById("<%= btnHidden.ClientID %>").click();

        }


        function ExcludeInvoiceDetail(val) {
        
            console.log(val);
            console.log(this);
                
            var backColor = document.getElementById(val).style.backgroundColor;

            if (document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRole").value != 'Hotel Vendor') {
                var exclude = "";
                if (document.getElementById(val).style.backgroundColor == 'red')
                    exclude = "include";
                else
                    exclude = "exclude";



                console.log(backColor);

                var sUser = $("#<%=uoHiddenFieldUser.ClientID %>").val();
                var sPath = $("#<%=uoHiddenFieldPath.ClientID %>").val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "../PageMethods.aspx/ExcludeInvoiceDetail",
                    data: "{'InvoiceDetailID': '" + val + "','sPath': '" + sPath + "', 'sUser':'" + sUser + "'}",
                    dataType: "json",
                    success: function(data) {
                        console.log(data);
                        HideExcludeInvoice(val, data.d)
                    }
                }); 
            } 
        }

        function HideExcludeInvoice(val, exclude) {
            if (exclude == 1)  
                document.getElementById(val).style.backgroundColor = "red";
            else
                document.getElementById(val).style.backgroundColor = "";
           
            
        }


        function SendInvoice(val) {

            console.log(val.getAttribute("ID"));
            var invoiceNumber = val.getAttribute("ID");
            
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "../PageMethods.aspx/GetInvoices",
                    data: "{'InvNumber': '" + invoiceNumber + "', 'InvDate': ''}",
                    dataType: "json",
                    success: function(data) {

                        console.log(data);
                        console.log(data.d.Invoice);
                        console.log(data.d.Token);
                        console.log(data.d.InvoiceStatus);


                        if (data.d.InvoiceStatus == '') {
                            var r = confirm("Are you sure you want to submit the invoice?");
                            if (r == true) {
                                SubmitSingleInvoice(data, invoiceNumber);
                                alert('Record has been submitted to IMS!'); 
                            }
                        }
                        else {
                            alert(data.d.InvoiceStatus + '!') ;
                        }


                    },
                    error: function(objXMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
           
        }
 
        function SubmitSingleInvoice(data, invoiceNumber) {
               var invoice = data.d.Invoice;
               var Token = data.d.Token;
               console.log(Token);
               console.log(invoice);
               
               var settings = {
                    "async": true,
                    "crossDomain": true,
                    "type": "POST",
                    "url": "https://uatrcclhrapi.brenock.com/api/invoice/Submit",
                    "method": "POST",
                    "data": invoice,
                    "content-type": "text/xml",
                    "headers": {
                        "accept": "application/json",
                        "content-type": "application/xml",
                        "authorization": "bearer " + Token,
                        "cache-control": "no-cache"
                  }
              }
            
            
              $.ajax(settings).done(function(response) {
                  SaveSubmittedInvoice(invoiceNumber, response.responseText);
              }).error(function(e) {
                  SaveErrorError(e.responseText, invoiceNumber);
              });


        }


        function SaveErrorError(val, invoiceNumber) {
        
            var userID = document.getElementById('ctl00_NaviPlaceHolder_uoHiddenFieldUser').value;
            console.log(invoiceNumber + ' ' + userID);
            console.log(val);

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../PageMethods.aspx/ErrorInvoices",
                data: "{'Content': '" + val + "', 'InvoiceNumber': '" + invoiceNumber + "', 'userID': '" + userID + "'}",
                dataType: "json",
                success: function(data) {
                    console.log(data);
                    alert('Error saved!');
                },
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }

            });
            
        }

        function SaveSubmittedInvoice(invoiceNumber, message) {
            var sUser = $("#<%=uoHiddenFieldUser.ClientID %>").val();
            var sPath = $("#<%=uoHiddenFieldPath.ClientID %>").val();

            var Desc = message;
            var Func = 'SaveSummittedInvoice';
            var FName = 'IMS.aspx';
            var TZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
            var GDATE = new Date().toISOString().split('T'); //.getTimezoneOffset();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../PageMethods.aspx/SaveSummittedInvoice",
                data: "{'VendorNumber': '0', 'InvoiceNumber': '" + invoiceNumber + "','sPath': '" + sPath + "', 'sUser':'" + sUser
                    + "', 'Desc':'" + Desc + "', 'Func':'" + Func + "', 'FName':'" + FName + "', 'TZone':'" + TZone
                    + "', 'GDATE':'" + GDATE + "'}",
                dataType: "json",
                success: function(data) {
                    console.log(data);
                    alert('Success!!!');
                },
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }
            
        function SaveAudit(sDescription, sFunction, sID) {
            if (sID != '') {
                var sUser = $("#<%=uoHiddenFieldUser.ClientID %>").val();
                var sPath = $("#<%=uoHiddenFieldPath.ClientID %>").val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "../PageMethods.aspx/LogAuditTrail",
                    data: "{'sID':'" + sID + "', 'strLogDescription': '" + sDescription + "', 'strFunction': '" + sFunction + "','sPath': '" + sPath + "', 'sUser':'" + sUser + "'}",
                    dataType: "json",
                    success: function(data) {
                        alert('Audit Trail saved!');
                    }
                });
            }
        }            
        
     </script>  
          
          
</asp:Content>
 