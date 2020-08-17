<%@ Page Title="Shipboard HR Strategy and Services" Language="C#" MasterPageFile="~/TravelmartMasterPage.Master" 
AutoEventWireup="true" CodeBehind="CrewAssist.aspx.cs" Inherits="TRAVELMART.CrewAssist" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>   
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td class="PageTitle">
                   Crew Assist Page
                </td>                
            </tr>
        </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <style type="text/css" >
        #navigation {
            margin: 0;
            padding: 0;
            list-style: none;
            overflow: hidden;
            border-bottom: 0.4em solid #0288D8;
            min-width:703px;
        }
        
        #navigation li {
            float: left;
            height: 2em;
            margin: 0 0.009em;
            background-color:Blue;
        }
      
        #navigation a:link, #navigation a:visited {
            float: left;
            height: 1.8em;
            width: 50%;
            padding: 0.2em 0.5em;
            
            background-color:Green;
             
            text-align:center;
            font-weight: bold;
            text-decoration: none;
            border-width: 0.1em 0.1em 0 0.1em;
            border-style: solid;
            border-color: #0288D8;
            border-radius: 3px;
            -moz-border-radius: 6px;
            -webkit-border-radius: 6px;
            line-height: 1.6em;
            position: relative;
            top: 0.3em;
        }
        
        .nav 
        {
            float: left;
            height: 1.8em;
            width:100%;
            text-align:center;
            font-weight: bold;
            text-decoration: none;
            border-width:0.1em;
            border-style: solid;
            border-color: #0288D8;
            border-radius: 1.5px;
            -moz-border-radius: 3.25px;
            -webkit-border-radius: 3.25px;
            line-height: 1.6em;
         }
        
        .ClassPanelCrewSchedule { max-height: 70px; height:70px; overflow: auto; }
        .ClassPanelAir { max-height: 90px; height:90px; overflow: auto; }
        .ClassPanelHotel { max-height: 90px; height:90px; overflow: auto; }
        .ClassPanelTransportation { max-height: 107px;  overflow: auto; }
        
        .centerdiv {
                    position:fixed;
                    top: 50%;
                    left: 50%;
                    width:30em;
                    height:12em;
                    margin-top: -9em;   /*set to a negative number 1/2 of your height*/
                    margin-left: -15em; /*set to a negative number 1/2 of your width*/
                    border: 1px solid #ccc;
                    background-color: #f3f3f3;
                    
        }
        
        .imagePos {
                    position:absolute;
                    right: -12px;
                    top: -8px;
                }   
      
        .RemarkDiv
        {
            position:absolute;
            /*top: 354px;
            left: 400.5px;*/
            border-color:#C0C0C0;
            border-style:solid;
            border-width:thin;
            width:31em;
            height:24em;
            margin-top: -9em; /*set to a negative number 1/2 of your height*/
            margin-left: -15em; /*set to a negative number 1/2 of your width*/
        }
        
       .PortTransDiv
        {
            position:absolute;
            /*top: 354px;
            left: 400.5px;*/
            border-style:solid;
            border-width:thin;
            border-color:#C0C0C0;
            width:32em;
            height:18.5em;
            margin-top: -9em; /*set to a negative number 1/2 of your height*/
            margin-left: -15em; /*set to a negative number 1/2 of your width*/
        }
      .PortAgentHotelDiv {
            position:absolute;
            /*top: 354px;
            left: 400.5px;*/
            border-style:solid;
            border-width:thin;
            border-color:#C0C0C0;
            width:36em;
            height:17em;
            margin-top: -9em; /*set to a negative number 1/2 of your height*/
            margin-left: -15em; /*set to a negative number 1/2 of your width*/
        }
  
        .DivCrewAssistComment
        {
            position:absolute;
            top: 354px;
            left: 400.5px;
            border-style:solid;
            border-width:thin;
            border-color:#C0C0C0;
            /*<%--width:36em; height:17em;--%>*/
            margin-top: -9em; /*set to a negative number 1/2 of your height*/
            margin-left: -15em; /*set to a negative number 1/2 of your width*/
        }
        
        .HotelRemarkDiv
        {
            position:absolute;
            border-color:#C0C0C0;
            border-style:solid;
            border-width:thin;
            width:31em;
            height:12.8em;
            margin-top: -9em; /*set to a negative number 1/2 of your height*/
            margin-left: -15em; /*set to a negative number 1/2 of your width*/
        }
  
    </style>
   
  <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="uoPanelMaster" >
                <ContentTemplate>
   
   
    <table id="ubTableMain" style="text-align:left; width:98%;  border-color: #0288D8;" class="LeftClass" runat="server"> 
            <tr >
                <td  style="width:60px; ">
                    Employee Id:
                </td>
                <td class="contentValue" >
                    <asp:TextBox ID="uoTextBoxEmployeeID" runat="server" 
                     onkeypress="getTextBoxID('1')" Width="100%" 
                    ></asp:TextBox>
                </td>
                
                <td  style="width:100px; text-align:right;">
                    Last Name:
                </td>
                
                <td class="contentValue" >
                    <asp:TextBox ID="uoTextBoxLastName" onkeypress="getTextBoxID('3')"
                         runat="server" Width="100%" ></asp:TextBox>
                </td>
                
                <td  style="width:100px; text-align:right; ">
                    First Name:
                </td>
                <td class="contentValue">
                    <asp:TextBox ID="uoTextBoxFirstName" onkeypress="getTextBoxID('2')"
                     runat="server" class="contentValue" Width="100%" ></asp:TextBox>
                </td>
                <td colspan="2" style="padding-left:5px; "  >
                    <asp:Button ID="uoButtonSearch" runat="server" 
                            OnClick="uoButtonSearch_click"
                            Text="Search" CssClass="SmallButton"  />
                </td>
            </tr>
    </table>
    
    
    <table style="text-align:left; width:98%;  border-color: #0288D8;" class="LeftClass" >
    
            <tr style="border-bottom-style:solid; border-right-style:solid; ">
                <td>
                    <asp:label runat="server" ID="lblCrewInformation" Text="Select Crew:"  Width="60px"/>
                </td>
              <td colspan="2">
                   <asp:DropDownList ID="uoDropDownListName" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="uoDropDownListName_SelectedIndexChanged" Width="100%">
                   </asp:DropDownList> 
              </td>
               
              <td align="right">
                    Remark: 
                    <button id="btnShowRemark2" style="height:15px; vertical-align:top;"  />
              </td>
              <td colspan="2" rowspan="5" style="width:90%; padding-left:10px;">
                      <table id="TableRemark" style="width:90%; height:100%; vertical-align:top; 
                                    border-style:solid; border-width:thin;  background-color:#FFFFFF;" >
                                       <tr style="height:100%;vertical-align:top;" >
                                            <td colspan="2" style="height:100%;">
                                            
                                                <asp:Panel ID="uoPanelRemark" ScrollBars="Vertical" runat="server" 
                                                        style="max-height:60px; height:60px; "   >
                                                    <asp:ListView ID="uoListViewRemark" runat="server" 
                                                           >
                                                        <LayoutTemplate>
                                                            <table border="0" id="uoListviewTableRemark"    cellpadding="0" cellspacing="0" 
                                                                    style="border-style:none; width:99.8%;"
                                                                   class="listViewTable">
                                                                <tr>
                                                                   <%-- <th style="width:200px; max-width:200px;" ></th>--%>
                                                                   <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                                                                </tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr >
                                                                <td style="min-height:22px;height:22px; text-align:left; margin:5px; padding:8px; width:0.1%;">  
                                                                        
                                                                    <asp:HiddenField ID="uoHiddenFieldGridRemarkID" runat="server" Value='<%# Eval("RemarkID")%>'/>
                                                                    <asp:HiddenField ID="uoHiddenFieldRemarkTRID" runat="server" Value='<%# Eval("TravelRequestID")%>'/>
                                                                    <asp:HiddenField ID="uoHiddenFieldResourceID" runat="server" Value='<%# Eval("ReqResourceID")%>'/>
                                                                    <asp:HiddenField ID="uoHiddenFieldTypeID" runat="server" Value='<%# Eval("RemarkTypeID")%>'/>
                                                                    <asp:HiddenField ID="upHiddenFieldStatusID" runat="server" Value='<%# Eval("RemarkStatusID")%>'/>
                                                                    <asp:HiddenField ID="uoHiddenFieldSumCall" runat="server" Value='<%# Eval("SummaryOfCall")%>'/>
                                                                    <asp:HiddenField ID="uoHiddenFieldRequestorID" runat="server" Value='<%# Eval("RemarkRequestorID")%>'/>
                                                                    <asp:HiddenField ID="uoHiddenFieldTransDate" runat="server" Value='<%# String.Format("{0:MM/dd/yyyy}", Eval("TransactionDate"))%>'/>
                                                                    <asp:HiddenField ID="uoHiddenFieldTransTime" runat="server" Value='<%# String.Format("{0:HH:mm}", Eval("TransactionTime"))%>'/>
                                                                    <asp:HiddenField ID="uoHiddenFieldPortCode" runat="server" Value='<%# Eval("PortCode")%>'/>
                                                                    <asp:CheckBox  ID="uoCheckBoxIR" runat="server" Checked='<%# Eval("IR")%>' style="display:none"/>
                                                                    
                                                                                                                                    
                                                                    <asp:Label runat="server" ID="lblUserName"   Text='<%# Eval("RemarkBy") %>'
                                                                        style="font-style:normal; font-weight:bold;"/>
                                                                    <br/>
                                                                    <div style="max-width:400px; table-layout:fixed; overflow:hidden; position:relative;">
                                                                         <asp:Label runat="server" ID="lblRemark" style="table-layout:fixed; overflow:hidden;"
                                                                                ToolTip='<%# Eval("Remark") %>' Text='<%# Eval("Remark") %>'/>
                                                                    </div>                                                                   
                                                                    <b>
                                                                    <asp:Label runat="server" ID="Label6"  Text='<%# Eval("RecordLocator") %>'
                                                                            style="font-size:xx-small; color:#B0B0B0 ; font-style:oblique; "
                                                                            ToolTip='<%# Eval("Remark") %>' /></b>
                                                                         &nbsp;
                                                                   <asp:Label runat="server" ID="Label4"  Text='<%# Eval("Resource") %>'
                                                                        style="font-size:xx-small; color:#C0C0C0" ToolTip='<%# Eval("Remark") %>'
                                                                        />
                                                                        &nbsp;
                                                                    <asp:Label runat="server" ID="lblDateCreated"  Text='<%# Eval("RemarkDate") %>'
                                                                        style="font-size:xx-small; color:#C0C0C0;" ToolTip='<%# Eval("Remark") %>'
                                                                        /> &nbsp; &nbsp;
                                                                    <asp:LinkButton ID="uoLinkButtonEditRemark" ToolTip="Edit Remark" Text="Edit"  
                                                                        runat="server" OnClientClick="EditRemarkmain(this)" 
                                                                        style="font-size:xx-small;"
                                                                        Visible='<%# Convert.ToBoolean(Eval("Visible").ToString() == "True" ? 1 : 0) %>'/>
                                                                        &nbsp;
                                                                    <asp:LinkButton ID="uoLinkButtonDeleteRemark" ToolTip="Delete Remark" Text="Delete" OnClick="ButtonDeleteRemark_click"
                                                                        style="font-size:xx-small;"
                                                                        runat="server" OnClientClick="return DeleteRemarkmain(this)" Visible='<%# Convert.ToBoolean( Eval("Visible").ToString() == "True" ? 1 : 0) %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <EmptyDataTemplate>
                                                            <table>
                                                                 <tr> 
                                                                    <td class="leftAligned">
                                                                        No Remark
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                                
                                            </td>
                                       </tr>
                                       <tr>
                                            <td style="width:90%">
                                                <asp:TextBox ID="txtRemark" style="border-color:#C0C0C0;" TextMode="MultiLine" Height="30px" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="padding-left:3px; vertical-align:top;">
                                                <asp:Button ID="btnSaveRemark" runat="server" Text="Save" OnClick="btnSaveRemark_click" OnClientClick="return checkRamarkTravelID(this)" />
                                            </td>
                                       </tr>
                                </table>
                </td>
               
            </tr>
            <tr >
                <td colspan="4">
                      <b> Crew Information </b>
                     <button id="btnShowRemark" style="height:15px; display:none; vertical-align:top;" />
                </td>
            </tr>
            <tr >
                <td >
                      Gender:
                </td>
                <td>
                   <asp:TextBox ID="uoTextBoxGender" runat="server" Width="200px" 
                                                ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>
                <td>
                    <asp:label runat="server" ID="lblBrand" Text="Brand:"  Width="60px"/> 
                    &nbsp;
                    <asp:TextBox ID="uoTextBoxBrand" runat="server" Width="200px" 
                                                ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>
                <td>
                    <asp:label runat="server" ID="lblRank" Text="Rank:" Width="60px"/>
                    &nbsp;
                    <asp:TextBox ID="uoTextBoxRank" runat="server" Width="200px" 
                                                ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>
               
            </tr>
            <tr >
                <td>
                    Nationality:
                     
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxNationality" runat="server" 
                                                Width="200px" ReadOnly="true" CssClass="ReadOnly">
                                                </asp:TextBox>
                </td>
                <td>
                    <asp:label runat="server" ID="lblShip" Text="Ship:" Width="60px"/>
                    &nbsp;
                    <asp:TextBox ID="uoTextBoxShip" runat="server" Width="200px" 
                                                ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>
                <td style="white-space:nowrap">
                    <asp:label runat="server" ID="lblStatus" Text="Status:" Width="60px"/>
                    &nbsp;
                    <asp:TextBox ID="uoTextBoxStatus" runat="server" Width="40px" CssClass="ReadOnly"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:label runat="server" ID="Label8" style="white-space:nowrap" Text="Reason Code:"/>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="uoTextBoxReasonCode" runat="server" Width="55px"   CssClass="ReadOnly"></asp:TextBox>
                    
                    <asp:TextBox ID="uoTextBoxCostCenter" runat="server" Width="200px" 
                                                Visible="false" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                                             
                </td>
               
            </tr>
          
           
        </table>   
       
     
    <hr style="margin-left:20px; margin-right:20px;"/>
    
    <table  id="Table1" runat="server" style="text-align:left; width:98.9%; border-color: #0288D8;" class="LeftClass" >
        <tr>
            <%--Left Pane--%>
            <td  style="vertical-align:top; padding-top:5px; text-align: left; border-color: #0288D8;">
                <div >
                    Port: &nbsp;
                    <asp:DropDownList ID="uoDropDownListPort" runat="server" Width="175px"
                    OnSelectedIndexChanged="uoDropDownListPort_SelectedIndexChanged" AutoPostBack="true"/> &nbsp;
                    Expense Type: &nbsp;
                    <asp:DropDownList ID="uoDropDownListExpenseType" runat="server" Width="175px"
                         OnSelectedIndexChanged="uoDropDownListExpenseType_OnSelectedIndexChanged" AutoPostBack="true"/>
                </div> 
                <div>
                
                    <table style=" width:100%;">
                        <tr>
                            <td style="white-space:nowrap; width:100%;  ">
                                <span  style="font-size:14px; padding-top:10px;">
                                    <b >Crew Schedule</b>
                                </span>
                            </td>
                            <td  style="white-space:nowrap;  " >
                                <asp:CheckBox ID="uoCheckBoxShow" Text= "Show Past date"  runat="server" AutoPostBack="true"  OnCheckedChanged="uoCheckBoxShow_click" />
                                 &nbsp;
                                 &nbsp;
                                <asp:CheckBox ID="uoCheckBoxMedical" Text= "Medical" runat="server" AutoPostBack="true"  />
                            </td>
                        </tr>
                    </table>
                    
                     <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" CssClass="ClassPanelCrewSchedule" Style="width:540px; min-height:100px" >
                        <asp:ListView runat="server" ID="uoListviewTravel" OnDataBound="uoListviewTravel_DataBound" >
                                <LayoutTemplate>
                                 <table border="0" id="uoListviewTravelTable" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                                          <tr>
                                            <th rowspan="2" style="text-align: center; white-space: normal; width:20px;"></th >
                                            <th rowspan="2" style="text-align: center; white-space: normal; width:100px;">
                                                Record Locator
                                            </th >
                                            <th rowspan="2" style="text-align: center; white-space: normal;">
                                                Ship
                                            </th >
                                            <th rowspan="2" style="text-align: center; white-space: normal;">
                                                Status
                                            </th>
                                            <th rowspan="2" style="text-align: center; white-space: normal; width:100px;">
                                                Sign On/Off Date
                                            </th>
                                            <th rowspan="2" style="text-align: center; white-space: normal;">
                                                Port
                                            </th> 
                                            <th rowspan="2" style="text-align: center; white-space:nowrap; width:75px;">
                                                Reason Code
                                            </th>   
                                            <th colspan="4" style="text-align: center; white-space:nowrap;">
                                                Status
                                            </th> 
                                            
                                        </tr>        
                                        <tr>
                                          
                                            <th style="text-align: center; white-space:nowrap; width:75px;">
                                                Immigration
                                            </th> 
                                            <th style="text-align: center; white-space:nowrap; width:75px;">
                                                Contract
                                            </th> 
                                            <th style="text-align: center; white-space:nowrap; width:75px;">
                                                Air
                                            </th> <th style="text-align: center; white-space:nowrap; width:75px;">
                                                Booking
                                            </th> 
                                            
                                        </tr>        
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                                </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                       <td style="padding-left:-10px">
                                            <asp:CheckBox CssClass="Checkbox" Width="20px" style="padding:2px;" 
                                             ID="uoSelectCheckBoxs" runat="server" 
                                             onclick="javascript:CheckTravelRequest(this)"/> 
                                               
                                       </td>
                                       <td class="leftAligned" style="white-space: normal; width:100px;" >
                                            <asp:Label runat="server" ID="lblRecordLocator" 
                                                Text='<%# Eval("RecordLocator") %>'/>
                                        </td>
                                       
                                       <td class="leftAligned" style="white-space: normal;" >
                                            <asp:Label runat="server" ID="lblVessel" 
                                                Text='<%# Eval("VesselCode") %>'/>
                                        </td>
                                       <td class="leftAligned">
                                            <asp:Label runat="server" ID="lblStatus" 
                                                Text='<%# Eval("Status") %>'/>
                                        </td>
                                       <td class="leftAligned">
                                            <asp:Label runat="server" ID="lblRequestDate"  
                                                Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("RequestDate"))%>'/>
                                        </td>
                                       <td class="leftAligned">
                                            <asp:Label runat="server" ID="lblPort"  
                                                Text='<%# Eval("PortCode")  %>'/>
                                        </td>
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ID="lblLabelReasoneCode"  
                                                Text='<%# Eval("ReasonCode")  %>'/>
                                        </td> 
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ID="uoLabelLOE" Text='<%# Eval("LOEStatus") %>' />
                                        </td>
                                        
                                        
                                        
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ForeColor="Red" ID="lblContractStatus" Text='<%# Eval("ContractStatus") %>' />
                                        </td>
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ForeColor="Red" ID="lblAirStatus" Text='<%# Eval("AirStatus") %>' />
                                        </td> 
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ForeColor="Red" ID="lblBookingStatus" Text='<%# Eval("BookingStatus") %>' />
                                        </td>
                                        
                                        <td style="white-space: normal; visibility:hidden; display:none;" >
                                           <asp:HiddenField runat="server" ID="hfTravelRequestID" Value='<%# Eval("TravelRequetID") %>' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldTRIDBint" Value='<%# Eval("IDBigint") %>' />
                                           <asp:HiddenField runat="server" ID="hfSeafarerID" Value='<%# Eval("SeafarerID") %>' />
                                           <asp:HiddenField runat="server" ID="hfVesselID" Value='<%# Eval("VesselID") %>' />
                                           <asp:HiddenField runat="server" ID="hfPortID" Value='<%# Eval("PortID") %>' />
                                           <asp:HiddenField runat="server" ID="hfCrewAssistRequestID" Value='<%# Eval("CrewAssistRequestID") %>' />
                                           <asp:HiddenField runat="server" ID="hfCrewAssistRequestNo" Value='<%# Eval("CrewAssistRequestNo") %>' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldTRTranspoReqID" Value='<%# Bind("ReqVehicleID") %>' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldTRMAGReqID" Value='<%# Bind("ReqMeetAndGreetID") %>' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldTRPortAgentID" Value='<%# Bind("ReqPortAgentID") %>' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldTRReqSafeguardID" Value='<%# Bind("ReqSafeguardID") %>' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldIsDeleted" Value='<%# Bind("IsDeleted") %>' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldLOEDate" Value='<%# String.Format("{0:dd-MMM-yyyy}", Eval("LOEDate"))%>' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldOfficer" Value='<%# Bind("LOEImmigrationOfficer") %>' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldPlace" Value='<%# Bind("LOEImmigrationPlace") %>' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldLOEReason" Value='<%# Bind("LOEReason") %>' />
                                        </td>
                                      </tr>
                                </ItemTemplate>
                                 <EmptyDataTemplate>
                                    <table  class="listViewTable">
                                        <tr>
                                            <th rowspan="2" style="text-align: center; white-space: normal; width:100px;">
                                                Record Locator
                                            </th >
                                            <th rowspan="2" style="text-align: center; white-space: normal;">
                                                Ship
                                            </th >
                                            <th rowspan="2" style="text-align: center; white-space: normal;">
                                                Status
                                            </th>
                                            <th rowspan="2" style="text-align: center; white-space: normal; width:100px;">
                                                Sign On/Off Date
                                            </th>
                                            <th rowspan="2" style="text-align: center; white-space: normal;">
                                                Port
                                            </th> 
                                            <th rowspan="2" style="text-align: center; white-space:nowrap; width:75px;">
                                                Reason Code
                                            </th>   
                                            <th colspan="4" style="text-align: center; white-space:nowrap;">
                                                Status
                                            </th> 
                                            
                                        </tr>        
                                        <tr>
                                          
                                            <th style="text-align: center; white-space:nowrap; width:75px;">
                                                Immigration
                                            </th> 
                                            <th style="text-align: center; white-space:nowrap; width:75px;">
                                                Contract
                                            </th> 
                                            <th style="text-align: center; white-space:nowrap; width:75px;">
                                                Air
                                            </th> <th style="text-align: center; white-space:nowrap; width:75px;">
                                                Booking
                                            </th> 
                                            
                                        </tr>        
                                        <tr>
                                            <td colspan="10" class="leftAligned">
                                                No Record
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                        </asp:ListView>
                    </asp:Panel>
                 </div>
                <div>
                    <span  style="font-size:14px; padding-top:10px; ">
                        <b>Air Detail</b>
                    </span>  
                    <br/> 
                    <asp:Panel ID="uoPanelAir" runat="server" ScrollBars="Auto" CssClass="ClassPanelAir" Width="540px" >
                        <asp:ListView runat="server" ID="uoListviewAir">
                                <LayoutTemplate>
                                 <table border="0" id="uoListviewAirTable" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                            <th style="text-align: center; white-space: normal;  width:20px;">
                                            </th >
                                            
                                            <th style="text-align: center; white-space: normal;">
                                                Seq #
                                            </th >
                                            <th style="text-align: center; white-space: normal;">
                                                Airline
                                            </th >
                                            <th style="text-align: center; white-space: normal;">
                                                Dept Date
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                Dept Time
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                Arr Date
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                Arr Time
                                            </th>
                                            <th style="text-align: center; white-space: normal; width:40px">
                                                Dept Code
                                            </th>    
                                            <th style="text-align: center; white-space: normal; width:40px">
                                                 Arr Code
                                            </th>   
                                            <th style="text-align: center; white-space: normal;">
                                                 Status
                                            </th>   
                                        </tr>        
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                       <td style="margin-left:-10px">
                                            <asp:CheckBox style="margin-left:-10px" CssClass="Checkbox" Width="10px" ID="uoSelectAirCheckBoxs" runat="server" 
                                             onclick="javascript:CheckTravelRequestAir(this);" 
                                               />
                                       </td>
                                       <td class="leftAligned" style="white-space: normal;" >
                                            <asp:Label runat="server" ID="lblSeqNo" Text='<%# Eval("SeqNo") %>'/>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;" >
                                            <asp:Label runat="server" ID="lblAirline" Text='<%# Eval("AirlineCode")%>' />
                                        </td>
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ID="lblDepartureDateTime"  Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DepartureDateTime"))%>'/>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;" >
                                            <asp:Label runat="server" ID="lblDeptTime"   Text='<%# String.Format("{0:HH:mm tt}", Eval("DepartureDateTime"))%>'/>
                                        </td>
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ID="lblArrivalDateTimeDate"  Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("ArrivalDateTime"))%>'/>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;" >
                                            <asp:Label runat="server" ID="lblArrTime"   Text='<%# String.Format("{0:HH:mm tt}", Eval("ArrivalDateTime"))%>'/>
                                        </td>
                                     
                                       <td class="leftAligned">
                                            <asp:Label runat="server" ID="lblDepartureAirportLocationCode" Text='<%# Eval("DepartureAirportLocationCode") %>'/></td>
                                       
                                       <td class="leftAligned">
                                            <asp:Label runat="server" ID="lblArrivalAirportLocationCode"  Text='<%# Bind("ArrivalAirportLocationCode")  %>'/>
                                        </td>
                                         <%--AutoPostBack="true"--%>
                                         <%--javascript:SelectHotelTransaction(this);--%>
                                        <td class="leftAligned">
                                            <asp:DropDownList runat="server" ID="uoDropDownListStatus" Width="69px" Style="margin:-3px; 
                                                padding:-3px; font-size:xx-small; "  onchange="javascript:AirChangeStatus(this);" 
                                                OnSelectedIndexChanged="uoDropDownListStatus_SelectedIndexChanged" AutoPostBack="true"
                                                Visible='<%# Eval("OrderNo").ToString() == "1" ? true : false %>'> 
                                                <asp:ListItem Text="Open" Value="1" Selected="True"/>
                                                <asp:ListItem Text="Checked-In" Value="2" />
                                                <asp:ListItem Text="Used" Value="3" />
                                                <asp:ListItem Text="Suspended" Value="4" />
                                            </asp:DropDownList>
                                            <asp:Label runat="server" ID="lblStatus"  Text='<%# Eval("Status")  %>' Visible='<%# Eval("OrderNo").ToString() == "1" ? false : true %> '/></td>
                                        </td>
                                        
                                        <td style="white-space: normal; visibility:hidden;" >
                                           <asp:HiddenField runat="server" ID="hfIDBigInt" Value='<%# Eval("IdBigint") %>' />
                                           <asp:HiddenField runat="server" ID="hfAirRequestID" Value='<%# Bind("HotelRequestID") %>' />
                                           
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldAirTranspoReqID" Value='<%# Bind("ReqVehicleID") %>' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldAirMAGReqID" Value='<%# Bind("ReqMeetAndGreetID") %>' />
                                           
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldAirPortAgentID" Value='<%# Bind("ReqPortAgentID") %>' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldAirFligthNo" Value='<%# Bind("FligthNo") %>' />
                                           
                                           
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldAHotelTranOtherID" Value='' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldStatus" Value='<%# Eval("Status")  %>' />
                                           <asp:HiddenField runat="server" ID="uoHiddenFieldStatusMessage" Value='<%# Eval("StatusMessage")  %>' />
                                           
                                        </td>
                                      </tr>
                                </ItemTemplate>
                                 <EmptyDataTemplate>
                                    <table  class="listViewTable">
                                        <tr>
                                            
                                            <th style="text-align: center; white-space: normal;">
                                                Seq #
                                            </th >
                                            <th style="text-align: center; white-space: normal;">
                                                Airline
                                            </th >
                                            <th style="text-align: center; white-space: normal;">
                                                Dept Date
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                Dept Time
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                Arr Date
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                Arr Time
                                            </th>
                                            <th style="text-align: center; white-space: normal; width:40px">
                                                Dept Code
                                            </th>    
                                            <th style="text-align: center; white-space: normal; width:40px">
                                                 Arr Code
                                            </th> 
                                            <th style="text-align: center; white-space: normal;">
                                                 Status
                                            </th>   
                                        </tr>  
                                        <tr>
                                            <td colspan="9" class="leftAligned">
                                                No Record
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                        </asp:ListView>
                    </asp:Panel> 
                
                </div> 
                <div id="divHotelBooking">
                    
                    
                      <table style=" width:100%;">
                        <tr>
                            <td style="white-space:nowrap; width:100%;  ">
                                <span  style="font-size:14px; padding-top:10px; ">
                                    <b>Hotel Booking</b>
                                </span>
                            </td>
                            <td >
                                 <asp:Button runat="server" ID="uoButtonAddHotel"  CssClass="SmallButton" 
                                        Text="Add New Hotel" OnClick="uoButtonAddHotel_Click" />
                            </td>
                        </tr>
                    </table>
                    
                    
                    
                    <asp:panel ID="uoPanelHotelBook" runat="server" ScrollBars="Auto" CssClass="ClassPanelHotel"  Width="540px">
                        <asp:ListView runat="server" ID="uoListViewHotelBook">
                            <LayoutTemplate>
                                <table border="0" id="uoListviewHotelBookTable" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr >
                                    <th style="text-align: center; white-space: normal;  width:30px;">
                                    </th >
                                    <th style="text-align: center; white-space: normal; width:130px;">
                                        Hotel
                                    </th >
                                    <th style="text-align: center; white-space: normal;">
                                        Check-in Date
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Check-Out Date
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Room Type
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Day
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Rate
                                    </th>  
                                    <th style="text-align: center; white-space: normal;">
                                        Status
                                    </th> 
                                    <th style="text-align: center; white-space: normal; width:40px;">
                                        Medical
                                    </th>     
                                    <th style="width:-1px;margin:-0px;"/>
                                        
                                </tr>        
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                            <%# GetRequestColor() %>
                                <td >
                                    <asp:CheckBox style="margin-left:1px" CssClass="Checkbox" Width="20px" ID="uoCheckBoxsHotelTransaction" runat="server" 
                                        onclick="javascript:SelectHotelTransaction(this);"  AutoPostBack="true"  
                                        OnCheckedChanged="uoCheckBoxsHotelTransaction_OnCheckedChanged"
                                    />
                               </td>
                                 <td class="leftAligned" style="white-space: normal; display:none; width:130px;" >
                                    <asp:Label runat="server" ID="Label3" style="display:none;" />
                                </td>
                                 <td class="leftAligned" style="white-space: normal; width:130px;" >
                                    <asp:Label runat="server" ID="lblHotelBook" Text='<%# Eval("Branch")%>' />
                                </td>
                                <td class="leftAligned" style="white-space: nowrap;" >
                                    <asp:Label runat="server" ID="lblCheckInDate"  Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckinDate"))%>'/>
                                </td>
                                <td class="leftAligned" style="white-space: nowrap;" >
                                    <asp:Label runat="server" ID="lblCheckOutDate"   Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckoutDate"))%>'/>
                                </td>
                                <td class="leftAligned">
                                    <asp:Label runat="server" ID="lblRoomTypeID"  Text='<%# Eval("RoomType")%>'/>
                                </td>
                                <td class="leftAligned" style="white-space: normal;" >
                                     <asp:Label runat="server" ID="lblHotelNite"   Text='<%# Eval("NoNitesInt")%>'/>
                                </td>
                                <td class="leftAligned" style="white-space: normal; width:30px;" >
                                    <asp:Label runat="server" ID="lblLabelHotelRate" ToolTip='<%# Eval("ConfirmRateMoney") %>'  Text='<%# String.Format("{0:#,##0.00}", Eval("ConfirmRateMoney"))%>' />
                                <%-- Text='<%# String.Format("{0:hh:mm:tt}", Eval("ConfirmRateMoney"))%>' --%>
                                </td>
                                 <td class="leftAligned" style="white-space: normal; width:70px;" >
                                    <asp:Label runat="server" ID="lblHStatus" ToolTip='<%# Eval("HotelStatus") %>'  Text='<%# Eval("HotelStatus") %>'/>
                                
                                </td>
                                <td  style="white-space: normal; width:40px; text-align:center;" >
                                    <asp:CheckBox  runat="server" ID="Label9" Enabled="false"   Checked='<%# Eval("IsMedical") %>'/>
                                
                                </td>
                                
                                <td style="white-space:nowrap; vertical-align:middle; padding-top:3px; border-right-width:thin; text-align:center; margin:-0px; width:-1px;">
                                 
                                   <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="Click to Cancel" 
                                    OnClientClick="return Confirmcancel(this)"
                                    ImageUrl="~/Images/Cancel.png"  style="size:20px; width:20px; height:20px;"/>
                                    <asp:LinkButton runat="server" ID="ouLinkButtonBranch"  style="font-size:small; font-weight:bold ; display:none;  "  class="Cancellation"/>
                                        
                                      
                                  <%-- <asp:LinkButton runat="server" ID="LinkButton1"  style="font-size:small; font-weight:bold" 
                                        visible='<%# Convert.ToBoolean(Eval("StatusID").ToString() != "4" ? 1 : 0) %>' 
                                        class="Cancellation">
                                        <asp:ImageButton ID="ImageButton3" runat="server" ToolTip="Click to Cancel" 
                                            Visible='<%# Convert.ToBoolean(Eval("StatusID").ToString() != "4" ? 1 : 0) %>'
                                            OnClientClick="return Confirmcancel(this)"
                                            ImageUrl="~/Images/Cancel.png"  style="size:20px; width:20px; height:20px;"/>
                                    </asp:LinkButton>     
                                 --%>
                                 
                                 
                                        
                                    <asp:ImageButton ID="uoImageButtonApproved" runat="server" OnClientClick="return Confirmcancel(this)" 
                                        OnClick="uoButtonCancelHotel_Click" ToolTip="Click to Approve" 
                                        ImageUrl="~/Images/Approved.png"  style="size:20px; width:20px; height:20px;"
                                        Visible='<%# Convert.ToBoolean(Eval("StatusID").ToString() == "2" ? 1 : 0) %>' />
                                       
                                   <asp:HiddenField runat="server" ID="uoHiddenFieldHotelID" Value='<%# Eval("TransHotelID") %>' />
                                   <asp:HiddenField runat="server" ID="uoHiddenFieldHIDBigint" Value='<%# Eval("IdBigint") %>' />
                                   <asp:HiddenField runat="server" ID="uoHiddenFieldSeqNo" Value='<%# Eval("SeqNo") %>' />
                                   <asp:HiddenField runat="server" ID="uoHiddenFieldHTravelRequestID" Value='<%# Eval("TravelReqID") %>' />
                                   <asp:HiddenField runat="server" ID="uoHiddenFieldHIsPortagent" Value='<%# Eval("IsPortAgent") %>' />
                                
                                
                                
                                   <asp:HiddenField runat="server" ID="uoHiddenFieldCancellationTermsInt" Value='<%# Eval("CancellationTermsInt") %>' />
                                   <asp:HiddenField runat="server" ID="uoHiddenFieldHotelTimeZoneID" Value='<%# Eval("HotelTimeZoneID") %>' />
                                   <asp:HiddenField runat="server" ID="uoHiddenFieldCutOffTime" Value='<%# Eval("CutOffTime") %>' />
                                   <asp:HiddenField runat="server" ID="uoHiddenFieldIsConfirm" Value='<%# Eval("IsConfirmed") %>' />

                                
                                </td>
                                
                              </tr>
                        </ItemTemplate>
                            <EmptyDataTemplate>
                            <table  class="listViewTable">
                               <tr>
                                   
                                    <th style="text-align: center; white-space: normal; width:130px;">
                                        Hotel
                                    </th >
                                    <th style="text-align: center; white-space: normal;">
                                        Check-in Date
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Check-Out Date
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Room Type
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        No Of Day
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Status
                                    </th>    
                                    <th style="text-align: center; white-space: normal;  width:40px;">
                                        Medical
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
                    </asp:panel>
                
                </div>
                <div>
                    <%--<span  style="font-size:14px; padding-top:10px;">
                        <b>Transportation Booking</b>
                    
                    </span>
                    <br />
                    --%>
                    
                      <table style=" width:100%;">
                        <tr>
                            <td style="white-space:nowrap; width:100%;  ">
                                <span  style="font-size:14px; padding-top:10px; ">
                                    <b>Transportation Booking</b>
                                </span>
                            </td>
                            <td >
                                 <asp:Button runat="server" ID="uoButtonAddTranspo"  style="display:none;"  CssClass="SmallButton" 
                                        Text="Add New Transpo" OnClick="uoButtonAddTranspo_Click" />
                            </td>
                        </tr>
                    </table>
                    
                    
                    
                    <asp:Panel ID="uoPanelTransportation" runat="server" CssClass="ClassPanelTransportation" Width="540px">
                        <asp:ListView runat="server" ID="uoListViewTransportation" 
                            OnItemDeleting="uoListViewTranportationBooking_ItemDeleting"
                            OnItemUpdating="uoListViewTranportationBooking_ItemUpdating"
                            OnItemCommand="uoListViewTranportationBooking_ItemCommand">
                            <LayoutTemplate>
                                <table border="0" id="uoListviewHotelBookTable" cellpadding="0" 
                                       cellspacing="0" class="listViewTable" >
                                    <tr>
                                         <th style="text-align: center; width:160px;">
                                            Transportation
                                        </th>
                                        <th style="text-align: center; width:120px;">
                                            From
                                        </th>
                                        <th style="text-align: center; width:120px;">
                                            To
                                        </th>
                                        <th style="text-align: center; width:70px;">
                                            Pick-up Date
                                        </th>
                                        <th style="text-align: center; width:70px; ">
                                            Pick-up Time
                                        </th>
                                        <th style="text-align: center; width:70px; ">
                                            Rate
                                        </th>
                                        <th style="text-align: center; width:80px; white-space:normal;">
                                            Status
                                        </th>
                                        <th style="text-align: center;white-space: nowrap; "/>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            
                            <ItemTemplate>
                               <%# GetRequestColor() %>
                                   <td class="leftAligned" style="white-space: normal; display:none; width:130px;" >
                                        <asp:Label runat="server" ID="Label3" style="display:none;"  Text='<%# Eval("VehicleVendor")%>'/>
                                    </td>
                                    <td class="leftAligned" style="white-space:normal;  width:160px;">
                                        <asp:Label runat="server" ID="lblListviewTransportation" 
                                                   Text='<%# Eval("VehicleVendor")%>'/>
                                                
                                    </td>
                                    <td class="leftAligned" style="white-space:normal; width:145px">
                                        <asp:Label runat="server" Width="145px" ID="lblFrom" Text='<%# Eval("RouteFrom")%>'  />
                                    </td>
                                    <td class="leftAligned" style="white-space:normal; width:145px">
                                        <asp:Label runat="server" Width="145px" ID="lblTo" Text='<%# Eval("RouteTo")%>' />
                                    </td>
                                    <td class="leftAligned" >
                                        <asp:Label runat="server" ID="lblPickupDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("PickUpDate"))%>' />
                                    </td>
                                    <td class="leftAligned" >
                                        <asp:Label runat="server" ID="lblPickupTime" Text='<%# String.Format("{0:hh:mm tt}", Eval("PickUpTime"))%>' />
                                    </td>
                                    
                                     <td class="leftAligned" >
                                        <asp:Label runat="server" ID="lblRate" Text='<%#  String.Format("{0:#,##0.00}", Eval("TranspoCost"))%>' />
                                    </td>
                                    
                                    <td class="leftAligned" style="white-space:nowrap; width:80px;" >
                                        <asp:Label runat="server" ID="lblStatus" style="text-align: center; width:80px;"
                                            Text='<%# Eval("VehicleStatusVarchar")%>' />
                                    </td> 
                               
                                    <td style="white-space:nowrap; vertical-align:middle; padding-top:3px;   text-align:left; margin:-0px; width:-1px;" >
                                        
                                        <asp:LinkButton runat="server" ID="ouLinkButtonTrans"  style="font-size:small; font-weight:bold" class="Cancellation">
                                            <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="Click to Cancel" OnClientClick="return ConfirmTranscancel(this)"
                                                ImageUrl="~/Images/Cancel.png"  style="size:20px; width:20px; height:20px;"/>
                                        </asp:LinkButton>      
                                                
                                    <%-- <asp:LinkButton runat="server" ID="LinkButton1"  style="font-size:small; font-weight:bold" 
                                        visible='<%# Convert.ToBoolean(Eval("StatusID").ToString() != "4" ? 1 : 0) %>' 
                                        class="Cancellation">
                                      
                                        <asp:ImageButton ID="ImageButton3" runat="server" ToolTip="Click to Cancel" 
                                            Visible='<%# Convert.ToBoolean(Eval("StatusID").ToString() != "4" ? 1 : 0) %>'
                                            OnClientClick="return ConfirmTranscancel(this)"
                                            ImageUrl="~/Images/Cancel.png"  style="size:20px; width:20px; height:20px;"/>
                                    </asp:LinkButton>      --%>
                                            
                                            
                                        <asp:ImageButton ID="uoImageButtonApproved" runat="server" 
                                            OnClientClick="return ConfirmTranscancel(this)" CommandName="Update" 
                                            ToolTip="Click to Approve" ImageUrl="~/Images/Approved.png" 
                                             style="size:20px; width:20px; height:20px;"
                                            Visible='<%# Convert.ToBoolean(Eval("StatusID").ToString() == "2" ? 1 : 0) %>' />
                                        
                                    </td>
                                    
                                    <td style="white-space: normal; visibility: hidden;">
                                        <asp:HiddenField runat="server" ID="uoHiddenFieldTranslID" Value='<%# Bind("ReqVehicleIDBigint") %>' />
                                        
                                        <asp:HiddenField runat="server" ID="uoHiddenFieldTransactionID" Value='<%# Bind("VehicleTransID") %>' />

                                        <asp:HiddenField runat="server" ID="uoHiddenFieldTIDBigint" Value='<%# Bind("IdBigint") %>' />
                                        <asp:HiddenField runat="server" ID="uoHiddenFieldTSeqNo" Value='<%# Bind("SeqNoInt") %>' />
                                        <asp:HiddenField runat="server" ID="uoHiddenFieldTTravelRequestID" Value='<%# Bind("TravelReqIDInt") %>' />
                                        <asp:HiddenField runat="server" ID="uoHiddenFieldIsPortAgent" Value='<%# Bind("IsPortAgent") %>' />
                                    
                                    </td>
                                </tr>
                                
                                
                                
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class="listViewTable">
                                    <tr>
                                      
                                        <th style="text-align: center; white-space: normal;">
                                            Transportation
                                        </th>
                                        <th style="text-align: center; white-space: normal; ">
                                            From
                                        </th>
                                        <th style="text-align: center; white-space: normal; ">
                                            To
                                        </th>
                                        <th style="text-align: center; white-space: normal;">
                                            Pick-up Date
                                        </th>
                                        <th style="text-align: center; white-space: normal;">
                                            Pick-up Time
                                        </th>
                                        <th style="text-align: center; width:70px; ">
                                            Rate
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="leftAligned">
                                            No Record
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </div>
            </td>
            <%--End Of Left Pane--%>
            
            <%--Right Pane--%>
            <td style="vertical-align: top; padding-left: 20px; text-align: left; border-color: #0288D8; width: 100%;">
                <table style=" padding-top:-10px; width: 95%; margin-top:-5px; " border="0">
                    <tr >
                    
                       <%-- <td style="width:100%; ">
                            <div style="width: 100%; float:right; ">
                                    <ul id="navigation" style="width:100%">
                                        <li><a href="#TabHotel" onclick="HighLightTabHeader(1)" >Hotel</a></li>
                                        <li><a href="#Transportation" onclick="HighLightTabHeader(2)" >Transportation</a></li>
                                        <li><a href="#PortAgent" onclick="HighLightTabHeader(3)">Service Provider</a></li>
                                        <li><a href="#Safeguard" onclick="HighLightTabHeader(6)" style="display:none; width:0px;">Safeguard</a></li>
                                        <li><a href="#MeetandGreet" onclick="HighLightTabHeader(3)" style="display:none; width:0px;">Meet and Greet</a></li>
                                        <li><a href="#Visa" onclick="HighLightTabHeader(4)" style="display:none; width:0px;">Visa</a></li>

                                    </ul>
                            </div>
                        </td>--%>
                        
                        <td style=" width:25%;" >
                            <a href="#TabHotel" id="navHotel" class="nav" onclick="HighLightTabHeader(1)" >Hotel</a>
                        </td>
                            
                        <td  style= "width:25%;">
                            <a href="#Transportation" class="nav"  id="navTransportation"  onclick="HighLightTabHeader(2)" >Transportation</a>
                        </td>
                        <td  style=" width:25%;">
                            <a href="#PortAgent" class="nav" id="navPortAgent"  onclick="HighLightTabHeader(3)">Service Provider</a>
                        </td>
                         <td  style=" width:25%;">
                            <a href="#MeetandGreet" class="nav" id="navMeetAndGreet"  onclick="HighLightTabHeader(4)">Meet and Greet</a>
                        </td>
                    </tr>
                     </table >
                      
                     <table >
                    <tr>
                        <td style="width:100%" colspan="3">
                            <div id="TabHotel">
                                <table style="border: 1px;" id="TabHotelTable" width="100%">
                                    <tr>
                                        <td colspan="1">
                                            Hotel :
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="uoDropDownListHotel" runat="server" 
                                            Width="83.8%" OnSelectedIndexChanged="uoDropDownListHotel_SelectedIndexChanged"
                                                AutoPostBack="true" AppendDataBoundItems="true" onchange="SaveHotel()"
                                                />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="1">
                                            Address :
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="uoLabelAdress" runat="server"  Font-Bold="true"></asp:Label>
                                            &nbsp; Telephone : &nbsp;<asp:Label ID="uoLabelTelephone" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="caption">
                                            Check In:
                                        </td>
                                        <td class="value">
                                            <asp:TextBox ID="uoTextBoxCheckinDate" runat="server" Text="" Width="80px" onchange="return OnDateChange('CheckIn');"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="uoTextBoxCheckinDate_Textboxwatermarkextender"
                                                runat="server" Enabled="True" TargetControlID="uoTextBoxCheckinDate" WatermarkCssClass="fieldWatermark"
                                                WatermarkText="MM/dd/yyyy">
                                            </cc1:TextBoxWatermarkExtender>
                                            <cc1:CalendarExtender ID="uoTextBoxCheckinDate_Calendarextender" runat="server" Enabled="True"
                                                TargetControlID="uoTextBoxCheckinDate" Format="MM/dd/yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:MaskedEditExtender ID="uoTextBoxCheckinDate_Maskededitextender" runat="server"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxCheckinDate"
                                                UserDateFormat="MonthDayYear">
                                            </cc1:MaskedEditExtender>
                                            <%-- <asp:RequiredFieldValidator ID="uoRFVCheckIn" ControlToValidate="uoTextBoxCheckinDate"
                                                    ValidationGroup="Request" runat="server" ErrorMessage="required" Font-Bold="true" />
                                                --%>
                                        </td>
                                        <td class="caption" style="width: 60px;">
                                            <%--   Time In:--%>
                                            Check Out:
                                        </td>
                                        <td class="value">
                                            <asp:TextBox ID="uoTextBoxCheckoutDate" runat="server" Text="" Width="80px" onchange="return OnDateChange('CheckOut');"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="uoTextBoxCheckoutDate_Textboxwatermarkextender"
                                                runat="server" Enabled="True" TargetControlID="uoTextBoxCheckoutDate" WatermarkCssClass="fieldWatermark"
                                                WatermarkText="MM/dd/yyyy">
                                            </cc1:TextBoxWatermarkExtender>
                                            <cc1:CalendarExtender ID="uoTextBoxCheckoutDate_Calendarextender" runat="server"
                                                Enabled="True" TargetControlID="uoTextBoxCheckoutDate" Format="MM/dd/yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:MaskedEditExtender ID="uoTextBoxCheckoutDate_Maskededitextender" runat="server"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxCheckoutDate"
                                                UserDateFormat="MonthDayYear">
                                            </cc1:MaskedEditExtender>
                                           
                                            <asp:TextBox runat="server" ID="uoTxtBoxTimeIn" Visible="false" Width="80px" Text="00:00"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="uoTxtBoxTime_TextBoxWatermarkExtender" runat="server"
                                                Enabled="True" TargetControlID="uoTxtBoxTimeIn" WatermarkCssClass="fieldWatermark"
                                                WatermarkText="HH:mm">
                                            </cc1:TextBoxWatermarkExtender>
                                            <cc1:MaskedEditExtender ID="uoMaskedEditExtenderCheckInTime" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTxtBoxTimeIn"
                                                UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                                            </cc1:MaskedEditExtender>
                                            <asp:TextBox runat="server" ID="uoTxtBoxTimeOut" Visible="false" Width="80px" Text="00:00"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" Enabled="True"
                                                TargetControlID="uoTxtBoxTimeOut" WatermarkCssClass="fieldWatermark" WatermarkText="HH:mm">
                                            </cc1:TextBoxWatermarkExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTxtBoxTimeOut"
                                                UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                                            </cc1:MaskedEditExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="caption">
                                            Duration :
                                        </td>
                                        <td class="value">
                                            <asp:TextBox runat="server" ID="uoTextBoxDuration" onkeyup="SaveHotel()" Width="80px" Text="0"></asp:TextBox>
                                        </td>
                                        <td class="caption">
                                            Currency:
                                        </td>
                                        <td class="value">
                                            <asp:DropDownList ID="uoDropDownListCurrency" runat="server" CssClass="TextBoxInput" onchange="SaveHotel()"
                                                Width="200px">
                                            </asp:DropDownList>
                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoDropDownListCurrency"
                                                ErrorMessage="required" Font-Bold="true" ValidationGroup="Request" InitialValue="0" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="caption">
                                            Contracted Rate
                                            <br />
                                            Inclusive of Tax:
                                        </td>
                                        <td class="value">
                                            <asp:TextBox ID="uoTextContractedRate" ReadOnly="true" runat="server" Width="80px"
                                                Text="" onkeypress="return validate(event);" CssClass="ReadOnly"></asp:TextBox>
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoTextContractedRate"
                                                ErrorMessage="Amount is required." ValidationGroup="Validate">*</asp:RequiredFieldValidator>
                                            <%--  <cc1:MaskedEditExtender ID="uoTextBoxEmergencyAmount_MaskedEditExtender" runat="server"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99,999.99" MaskType="Number" InputDirection="RightToLeft"
                                                    TargetControlID="uoTextContractedRate">
                                                </cc1:MaskedEditExtender>--%>
                                            <asp:CheckBox ID="uoCheckContractBoxTaxInclusive" Visible="false" runat="server" />
                                        </td>
                                        <td class="caption" style="width: 80px;">
                                            Meal Voucher :
                                        </td>
                                        <td class="value">
                                            <asp:TextBox ID="uoTextBoxMealVoucher" runat="server" Width="80px" Text=""  onkeyup="SaveHotel()"></asp:TextBox>
                                            <%-- <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99,999.99" MaskType="Number" InputDirection="RightToLeft"
                                                    TargetControlID="uoTextBoxMealVoucher">
                                                </cc1:MaskedEditExtender>--%>
                                              
                                        </td>
                                    </tr>
                                    <tr>
                                    
                                        <td class="caption" style="vertical-align: top">
                                            Comfirm Rate
                                            <br />
                                            Inclusive of Tax:
                                        </td>
                                        <td class="value">
                                            <asp:TextBox ID="uoTextBoxComfirmRate" runat="server" 
                                                onkeyup="SaveHotel()" Width="80px"></asp:TextBox>
                                        </td>
                                        <td class="caption" style="width: 80px; white-space:nowrap">
                                            Room Type :
                                        </td>
                                        <td class="value">
                                            <%--<asp:CheckBox ID="uoCheckBoxIsWithShuttle" runat="server"/> --%>
                                            <asp:DropDownList ID="uoDropDownListRoomeType" runat="server" CssClass="TextBoxInput" onchange="SelectRoomType(this)"
                                                Width="85px">
                                                <asp:ListItem Text="Single" Value="1" Selected="True" ></asp:ListItem>
                                                <asp:ListItem Text="Double" Value="2"></asp:ListItem>
                                            </asp:DropDownList> 
                                      <%--      &nbsp; <asp:Button ID="uoButtonCancelHotel" Width="60px" OnClientClick="return Confirmcancel(this)" OnClick="uoButtonCancelHotel_Click" runat="server" 
                                                        Text="Cancel" CssClass="SmallButton"  Visible="false"/>
                                            &nbsp; <asp:Button ID="uoButtonApprove" Width="60px"  OnClientClick="return Confirmcancel(this)"  OnClick="uoButtonCancelHotel_Click" 
                                                        runat="server" Text="Approve" CssClass="SmallButton" Visible="false"/>
                                    --%>                                        
                                    </td>
                                    </tr>
                                    <tr>
                                        <td class="caption" style="vertical-align: top">
                                            Meals:
                                        </td>
                                        <td class="value" colspan="3">
                                            <div>
                                                <asp:CheckBox ID="uoCheckboxBreakfast" runat="server" Text="Breakfast" onclick="SaveHotel()"/>
                                                &nbsp; &nbsp;
                                                <asp:CheckBox ID="uoCheckboxLunch" runat="server" Text="Lunch" onclick="SaveHotel()"/>
                                                &nbsp; &nbsp;
                                                <asp:CheckBox ID="uoCheckboxDinner" runat="server" Text="Dinner" onclick="SaveHotel()"/>
                                                &nbsp; &nbsp;
                                                <asp:CheckBox ID="uoCheckBoxLunchDinner" runat="server" Text="Additional Meals" onclick="SaveHotel()"/>
                                                &nbsp; &nbsp;
                                                <asp:CheckBox ID="uoCheckBoxIsWithShuttle" runat="server" Text=" With Shuttle" onclick="SaveHotel()"/>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:CheckBox ID="CheckBoxEmail" Text="Email :" runat="server" onclick="checkboxEnable(this)" />
                                        </td>
                                        <td valign="top" colspan="3">
                                            <asp:TextBox ID="uoTextBoxEmail"  runat="server"  Width="100%"  Height="83%" onkeyup="SaveHotel()"
                                                onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                                                ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                                                TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </td>
                                       
                                    </tr>
                                    
                                    <tr>
                                     <td valign="top" style="white-space:nowrap;">
                                           Comment : 
                                           <button id="uoButtonHotelComment" title="View all Comment"  style="height:15px; vertical-align:top;"  />
                                        </td>
                                        <td valign="top" colspan="3">
                                             <asp:TextBox ID="uoTextBoxComment" runat="server" Height="83%" TextMode="MultiLine" onkeyup="SaveHotel()"
                                                   ToolTip="Click to select the request source" Rows="2" Width="100%" />
                                        </td>
                                    </tr>  
                                     <tr runat="server" id="rowPortAgent" >
                                     <td >
                                           Confirm by : 
                                        </td>
                                        <td valign="top" colspan="3">
                                             <asp:TextBox ID="uoTextBoxPortAgentConfirm" runat="server" Height="83%" TextMode="MultiLine" onkeyup="SaveHotel()"
                                                   Rows="2" Width="100%" />
                                        </td>
                                    </tr>                                  
                                </table>
                            </div>
                            
                            <div id="TabTransportation" >
                                <table style="border: 1px;" id="TabTransportationTable">
                                    <tr>
                                        <td colspan="1" style="width:80px;">
                                            Transportation
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="uoDropDownListVehicleVendor" 
                                                OnSelectedIndexChanged="uoDropDownListVehicleVendor_OnSelectedIndexChanged"
                                                onchange="SaveTransportation(this)"
                                                runat="server" Width="449px" AutoPostBack="true" AppendDataBoundItems = "true"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="1" style="width:80px;">
                                            Address :
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="uoLabelVehicleAddress" runat="server" Font-Bold="true"/>
                                            &nbsp; Telephone : &nbsp;
                                            <asp:Label ID="uoLabelVehicleTelephone" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                
                                <table id="TabTransportationTable1">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td valign="top" colspan="4">
                                                        <asp:Panel ID="Panel5" runat="server"  ScrollBars="Auto" 
                                                            style="max-height:8em;height:8em; Width:665px; padding:0px 5px 0px 2px; border-style:solid; border-width:thin; border-color:#C0C0C0;" >
                                                            <asp:ListView runat="server" ID="uoListViewTranportationRoute" border="0" cellpadding="0" cellspacing="0" 
                                                                    class="listViewTable" InsertItemPosition="LastItem"  
                                                                    OnItemInserting="uoListViewTranportationRoute_ItemInserting"
                                                                    OnItemDeleting="uoListViewTranportationRoute_ItemDeleting"
                                                                    OnItemCommand="uoListViewTranportationRoute_ItemCommand">
                                                                <LayoutTemplate>
                                                                    <table border="0" id="uoListviewtTranspRoute" cellpadding="0"  cellspacing="0" class="listViewTable" >
                                                                        <tr>
                                                                            
                                                                            <th class="leftAligned" >
                                                                                <asp:Label runat="server" ID="uoLabelFrom" Width="110px" Text="From"></asp:Label>
                                                                            </th>
                                                                            
                                                                            <th style="text-align: center; white-space: normal;">
                                                                                <asp:Label runat="server" ID="uoLabelTO" Width="110px" Text="To"></asp:Label>
                                                                            </th>
                                                                            
                                                                            <th  style="text-align: center; white-space: normal;">
                                                                                <asp:Label runat="server" ID="uoLabelFromRoute" Width="100px" Text="From Route"></asp:Label>
                                                                            </th>
                                                                            
                                                                            <th  style="text-align: center; white-space: normal;">
                                                                                <asp:Label runat="server" ID="uoLabelToRoute" Width="100px" Text="To Route"></asp:Label>
                                                                            </th>
                                                                            
                                                                            <th  style="text-align: center; white-space: normal;">
                                                                                <asp:Label runat="server" ID="uoLabelCost" Width="30px" Text="Rate"></asp:Label>
                                                                            </th>
                                                                            
                                                                            <th  style="text-align: center; white-space: normal;">
                                                                                <asp:Label runat="server" ID="uoLabelPickUpDate" Width="70px" Text="PickUp Date"></asp:Label>
                                                                            </th>
                                                                            <th  style="text-align: center; white-space: normal;">
                                                                                <asp:Label runat="server" ID="uoLabelTime" Width="70px" Text="Time"></asp:Label>
                                                                            </th>
                                                                            <th  style="text-align: center; Width:20px;  white-space: normal;">
                                                                            </th>   
                                                                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                                                                        </tr>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        
                                                                        <td>
                                                                            <asp:HiddenField ID="uoHiddenFieldTransID" runat="server" Value='<%# Eval("VehicleTransID")%>'/>
                                                                            <asp:HiddenField ID="uoHiddenFieldReqTransID" runat="server" Value='<%# Eval("ReqVehicleIDBigint")%>'/>
                                                                            <asp:HiddenField ID="HiddenFieldRouteIDFromInt" runat="server"  Value='<%# Eval("RouteIDFromInt")%>'/>
                                                                                           
                                                                            <asp:Label runat="server" ID="uoLabelOrigin" Text='<%# Eval("FromVarchar")%>'></asp:Label>
                                                                        </td>
                                                                        <td class="leftAligned" >
                                                                            <asp:HiddenField ID="HiddenFieldRouteIDToInt" runat="server" Value='<%# Eval("RouteIDToInt")%>'/>
                                                                            <asp:Label runat="server" ID="uoLabelTO"  Text='<%# Eval("ToVarchar")%>'></asp:Label>
                                                                        </td>
                                                                        <td class="leftAligned" >
                                                                            <asp:Label runat="server" ID="uoLabelRouteFrom" Text='<%# Eval("RouteFrom")%>'></asp:Label>
                                                                        </td>
                                                                        <td class="leftAligned" > 
                                                                             <asp:Label runat="server" ID="uoLabelRouteTo" Text='<%# Eval("RouteTo")%>'></asp:Label>
                                                                        </td>
                                                                        <td class="leftAligned" > 
                                                                             <asp:Label runat="server" ID="uoLabelCost" Text='<%#  String.Format("{0:#,##0.00}", Eval("TranspoCost"))%>' ></asp:Label>
                                                                        </td>
                                                                        <td class="centerAligned" > 
                                                                         
                                                                             <asp:Label runat="server" ID="uoLabelPickUpDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("PickUpDate"))%>'></asp:Label>
                                                                        </td>
                                                                        <td class="centerAligned" > 
                                                                             <asp:Label runat="server" ID="uoLabelPickUpTime" Text='<%# String.Format("{0:hh:mm tt}", Eval("PickUpTime"))%>'></asp:Label>
                                                                        </td>
                                                                         <td style="white-space:normal;  Width:20px;  padding:3px 3px 3px 3px;">
                                                                            <asp:ImageButton ID="uoImageButtontDeleteRoute" runat="server"
                                                                                OnClientClick="return ConfirmTranscancel(this)" ToolTip="Click to Cancel" 
                                                                                CommandName="Delete" ImageUrl="~/Images/Cancel.png"  
                                                                                style="size:20px; width:20px; height:20px;"
                                                                                Visible='<%# Convert.ToBoolean((Eval("VehicleTransID").ToString() == "0" && Eval("ReqVehicleIDBigint").ToString() == "0") ?  1 : 0) %>'
                                                                                />

                                                                          
                                                                          
                                                                          
                                                                        </td> 
                                                                    </tr>
                                                                </ItemTemplate>
                                                             <%--   <EditItemTemplate>
                                                                     <tr>
                                                                       <tr>
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:DropDownList ID="uoDropDownListTFrom" runat="server" Width="120px" 
                                                                                    DataSourceID="uoObjectDataSourceRoute"
                                                                                    DataTextField="RoutName"
                                                                                    DataValueField="RoutId"
                                                                                    OnSelectedIndexChanged="uoDropDownListVehicleCost_SelectedIndexChanged"
                                                                                AppendDataBoundItems ="true"  onchange="SaveTransportationCombo(this)"/>
                                                                        </td>
                                                                             
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:DropDownList ID="uoDropDownListTTo" runat="server" Width="120px" 
                                                                                    AppendDataBoundItems ="true"
                                                                                    DataSourceID="uoObjectDataSourceRoute"
                                                                                    DataTextField="RoutName"
                                                                                    DataValueField="RoutId"
                                                                                    OnSelectedIndexChanged="uoDropDownListVehicleCost_SelectedIndexChanged"
                                                                                    onchange="SaveTransportationCombo(this)" />
                                                                        </td>
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox ID="uoTextBoxTFrom" Text="124"  width="95%" onkeyup="SaveTransportation(this)" runat="server"></asp:TextBox>
                                                                        </td>  
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox ID="uoTextBoxTTo" width="95%" onkeyup="SaveTransportation(this)" runat="server"></asp:TextBox>
                                                                        </td>    
                                                                        <td style="white-space:normal;width:70px; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox ID="uoTextBoxTPickupdate"  runat="server" Text="" width="95%"
                                                                             onkeyup="SaveTransportation(this)"></asp:TextBox>
                                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" Enabled="True" 
                                                                                TargetControlID="uoTextBoxTPickupdate" WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                                                                            </cc1:TextBoxWatermarkExtender>
                                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" 
                                                                                TargetControlID="uoTextBoxTPickupdate" 
                                                                                Format="MM/dd/yyyy">
                                                                            </cc1:CalendarExtender>
                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxTPickupdate"
                                                                                UserDateFormat="MonthDayYear">
                                                                            </cc1:MaskedEditExtender>
                                                                        </td>
                                                                        <td style="white-space:normal; width:60px; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox runat="server" ID="uoTextBoxTTime" width="95%" Text="00:00" onkeyup="SaveTransportation(this)"></asp:TextBox>
                                                                        </td>     
                                                                         <td style="white-space:normal;  Width:20px;  padding:0px 3px 0px 3px;">
                                                                            <asp:ImageButton ID="btnAddTranpoRoute" CommandName="Edit" ToolTip="Add Route" 
                                                                                runat="server" style="size:20px; width:20px; height:20px;" ImageUrl="~/Images/Add.ico" 
                                                                               />
                                                                        </td>          
                                                                    </tr>
                                                                </EditItemTemplate>--%>
                                                                <InsertItemTemplate>
                                                                    <tr>
                                                                       <tr>
                                                                       
                                                                        
                                                                                 
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:DropDownList ID="uoDropDownListTFrom" runat="server"  Width="110px"
                                                                                    DataSourceID="uoObjectDataSourceRoute"
                                                                                    DataTextField="RoutName"
                                                                                    DataValueField="RoutId"
                                                                                    AppendDataBoundItems ="true"  onchange="SaveTransportationCombo(this)"/>
                                                                        </td>
                                                                             
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:DropDownList ID="uoDropDownListTTo" runat="server" Width="110px" 
                                                                                    AppendDataBoundItems ="true"
                                                                                    DataSourceID="uoObjectDataSourceRoute"
                                                                                    DataTextField="RoutName"
                                                                                    DataValueField="RoutId"
                                                                                    onchange="SaveTransportationCombo(this)" 
                                                                                    />
                                                                        </td>
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox ID="uoTextBoxTFrom" width="95%" onkeyup="SaveTransportation(this)" runat="server"></asp:TextBox>
                                                                        </td>  
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox ID="uoTextBoxTTo" width="95%" onkeyup="SaveTransportation(this)" runat="server"></asp:TextBox>
                                                                        </td>    
                                                                        <td style="white-space:normal; width:30px; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox runat="server" ID="uoTextBoxCost" width="95%" 
                                                                                         style="text-align:right" Text="" onkeyup="SaveTransportation(this)"></asp:TextBox>
                                                                        </td> 
                                                                        <td style="white-space:normal;width:70px; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox ID="uoTextBoxTPickupdate"  runat="server" Text="" width="95%"
                                                                             onkeyup="SavePortAgent(this)"></asp:TextBox>
                                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" Enabled="True" 
                                                                                TargetControlID="uoTextBoxTPickupdate" WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                                                                            </cc1:TextBoxWatermarkExtender>
                                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" 
                                                                                TargetControlID="uoTextBoxTPickupdate" 
                                                                                Format="MM/dd/yyyy">
                                                                            </cc1:CalendarExtender>
                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxTPickupdate"
                                                                                UserDateFormat="MonthDayYear">
                                                                            </cc1:MaskedEditExtender>
                                                                        </td>
                                                                        <td style="white-space:normal; width:60px; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox runat="server" ID="uoTextBoxTTime" width="95%" Text="00:00" onkeyup="SaveTransportation(this)"></asp:TextBox>
                                                                            
                                                                            <cc1:TextBoxWatermarkExtender ID="uoTextBoxTTime_TextBoxWatermarkExtender" runat="server"
                                                                                Enabled="True" TargetControlID="uoTextBoxTTime" WatermarkCssClass="fieldWatermark"
                                                                                WatermarkText="HH:mm">
                                                                            </cc1:TextBoxWatermarkExtender>
                                                                            <cc1:MaskedEditExtender ID="uoMaskedEditExtenderTTime" runat="server" CultureAMPMPlaceholder=""
                                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxTTime"
                                                                                UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                                                                            </cc1:MaskedEditExtender>
                                                                        
                                                                        </td>     
                                                                         <td style="white-space:normal;  Width:20px;  padding:0px 3px 0px 3px;">
                                                                            <asp:ImageButton ID="btnAddTranpoRoute" CommandName="Insert" ToolTip="Add Route"
                                                                                runat="server" style="size:20px; width:20px; height:20px;" ImageUrl="~/Images/Add.ico" 
                                                                               />
                                                                        </td>    
                                                                              
                                                                    </tr>
                                                                </InsertItemTemplate>
                                                                <EmptyDataTemplate>
                                                                 <table border="0" id="uoListviewPATranspoCost" cellpadding="0"
                                                                        cellspacing="0" class="listViewTable" >
                                                                        <tr>
                                                                            
                                                                            <th class="leftAligned" >
                                                                                <asp:Label runat="server" ID="uoLabelFrom" Width="80px" Text="From"></asp:Label>
                                                                            </th>
                                                                            
                                                                            <th style="text-align: center; white-space: normal;">
                                                                                <asp:Label runat="server" ID="uoLabelTO" Width="40px" Text="To"></asp:Label>
                                                                            </th>
                                                                            
                                                                            <th  style="text-align: center; white-space: normal;">
                                                                                <asp:Label runat="server" ID="uoLabelFromRoute" Width="100px" Text="From Route"></asp:Label>
                                                                            </th>
                                                                            
                                                                            <th  style="text-align: center; white-space: normal;">
                                                                                <asp:Label runat="server" ID="uoLabelToRoute" Width="100px" Text="To Route"></asp:Label>
                                                                            </th>
                                                                            
                                                                            <th  style="text-align: center; Width:70px;  white-space: normal;">
                                                                                <asp:Label runat="server" ID="uoLabelPickUpDate" Width="80px" Text="PickUp Date"></asp:Label>
                                                                            </th>
                                                                            <th  style="text-align: center; Width:70px;  white-space: normal;">
                                                                                <asp:Label runat="server" ID="uoLabelTime" Width="80px" Text="Time"></asp:Label>
                                                                            </th>
                                                                            <th  style="text-align: center; Width:20px;  white-space: normal;">
                                                                            </th>
                                                                        </tr>
                                                                        
                                                                         <tr>
                                                                       <tr>
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:DropDownList ID="uoDropDownListTFrom" runat="server" Width="120px" OnSelectedIndexChanged="uoDropDownListRoute_SelectedIndexChanged"
                                                                                 DataSource='<%# GetDataRoute() %>' SelectedValue='<%# Bind("RoutId")%>'
                                                                                AppendDataBoundItems ="true"  onchange="SavePortAgent(this)"/>
                                                                        </td>
                                                                             
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:DropDownList ID="uoDropDownListTTo" runat="server" Width="120px" 
                                                                                    OnSelectedIndexChanged="uoDropDownListRoute_SelectedIndexChanged" AppendDataBoundItems ="true"
                                                                                     DataSource='<%# GetDataRoute() %>' SelectedValue='<%# Bind("RoutId")%>'
                                                                                    onchange="SavePortAgent(this)" />
                                                                        </td>
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox ID="uoTextBoxTFrom" Text="125"  width="95%" onkeyup="SavePortAgent(this)" runat="server"></asp:TextBox>
                                                                        </td>  
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox ID="uoTextBoxTTo" width="95%" onkeyup="SavePortAgent(this)" runat="server"></asp:TextBox>
                                                                        </td>    
                                                                        <td style="white-space:normal;width:70px; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox ID="uoTextBoxTPickupdate"  runat="server" Text="" width="95%"
                                                                             onkeyup="SavePortAgent(this)"></asp:TextBox>
                                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" Enabled="True" 
                                                                                TargetControlID="uoTextBoxTPickupdate" WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                                                                            </cc1:TextBoxWatermarkExtender>
                                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" 
                                                                                TargetControlID="uoTextBoxTPickupdate" 
                                                                                Format="MM/dd/yyyy">
                                                                            </cc1:CalendarExtender>
                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxTPickupdate"
                                                                                UserDateFormat="MonthDayYear">
                                                                            </cc1:MaskedEditExtender>
                                                                        </td>
                                                                        <td style="white-space:normal; width:60px; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox runat="server" ID="uoTextBoxTTime" width="95%" Text="00:00" onkeyup="SavePortAgent(this)"></asp:TextBox>
                                                                        </td>     
                                                                         <td style="white-space:normal;  Width:20px;  padding:0px 3px 0px 3px;">
                                                                            <asp:ImageButton ID="btnAddTranpoRoute" OnClick="btnAddTranpoRoute_click" runat="server" style="size:20px; width:20px; height:20px;" ImageUrl="~/Images/Add.ico" 
                                                                               />
                                                                        </td>          
                                                                    </tr>
                                                                        
                                                                    </table>
                                                                 
                                                                 </EmptyDataTemplate>
                                                            </asp:ListView>
                                                        
                                                            
                                                            <asp:ObjectDataSource ID="uoObjectDataSourceRoute" runat="server" SelectMethod="GetDataRoute"  TypeName="TRAVELMART.CrewAssist">
                                                            </asp:ObjectDataSource>
                                                        </asp:Panel>
                                                    </td>
                                                
                                                </tr>
                                                
                                             
                                                
                                              <tr style="display:none;">
                                                    <td style="width:80px;">
                                                        From:
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="uoDropDownListRouteFrom" runat="server" Width="120px" OnSelectedIndexChanged="uoDropDownListRoute_SelectedIndexChanged"
                                                            AutoPostBack="true"  AppendDataBoundItems ="true"  onchange="SaveTransportation(this)"/>
                                                       <%-- 
                                                        &nbsp; &nbsp;
                                                        <asp:TextBox ID="uoTextBoxRouteFrom" Width="210px"  onkeyup="SaveTransportation(this)" runat="server"></asp:TextBox>
--%>                                                   
                                                </td>
                                                </tr>
                                                
                                                
                                                <tr>
                                                    <td>
                                                        
                                                        <table style="width: 100%; border-color:Gray;">
                                                              
                                                            <tr >
                                                                <td valign="top" colspan="1">
                                                                    <asp:CheckBox ID="uoCheckBoxEmailTrans" Text="Email :" onclick="SaveTransportation(this)" runat="server" click="checkboxEnable(this)" />
                                                                </td>
                                                                <td style="vertical-align: top; width:100%;"  colspan="3">
                                                                    <asp:TextBox ID="uoTextBoxEmailTrans" 
                                                                        onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                                                                        ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                                                                        onchange="SaveTransportation(this)" Enabled="false" runat="server" Style="margin-top:-2px;"
                                                                        onkeyup="SaveTransportation(this)"
                                                                        TextMode="MultiLine" Rows="2" Width="95%" />
                                                                </td>
                                                                <td rowspan="3"  style="vertical-align:top;">
                                                                    <div id="TranspoCost" style="border-style:solid; border-width:thin; border-color:#C0C0C0; z-index: 100; visibility:visible;" >
                                                                        <asp:Panel ID="Panel4"  ScrollBars="Auto" runat="server" Width="250px" Height="120px" TabIndex="100" 
                                                                                          style="position:inherit"  Visible="true" BackColor="#ECDFC4" >
                                                                            <asp:ListView runat="server" ID="uoListViewTranspoCost" border="0"
                                                                                style="position:inherit"  cellpadding="0" cellspacing="0" class="listViewTable" >
                                                                                <LayoutTemplate>
                                                                                    <table border="0" id="uoListviewTranspoCost" cellpadding="0"  cellspacing="0" class="listViewTable" >
                                                                                        <tr>
                                                                                            <th class="leftAligned"  style="width:60px; "/>
                                                                                            
                                                                                            <th class="leftAligned" >
                                                                                                <asp:Label runat="server" ID="uoLabelVehicleTypeName" Width="80px" Text="Vehicle Type"></asp:Label>
                                                                                            </th>
                                                                                            
                                                                                            <th style="text-align: center; white-space: normal;">
                                                                                                <asp:Label runat="server" ID="uoLabelCost" Width="40px" Text="Rate"></asp:Label>
                                                                                            </th>
                                                                                            <th  style="text-align: center; white-space: normal;">
                                                                                                <asp:Label runat="server" ID="uoLabelCapacity" Width="100px" Text="Capacity"></asp:Label>
                                                                                            </th>
                                                                                            
                                                                                            <th  style="text-align: center; white-space: normal;">
                                                                                                <asp:Label runat="server" ID="uoLabelUnitOfMeasure" Width="148px" Text="Unit Of Measure"></asp:Label>
                                                                                            </th>
                                                                                            <th  style="text-align: center; white-space: normal;">     
                                                                                                <asp:Label runat="server" ID="uoLabelRoute" Width="100px" Text="Route"></asp:Label>
                                                                                            </th>
                                                                                            
                                                                                             <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                                                                                        </tr>
                                                                                    </table>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td >
                                                                                            <asp:CheckBox CssClass="Checkbox" style="margin-left:0px;" ID="uoCheckBoxVCost" runat="server" onclick="javascript:GetVehicleType(this);" />  
                                                                                        </td>
                                                                                        
                                                                                        <td class="leftAligned" >
                                                                                            <asp:HiddenField ID="uoHiddenFieldVCTravelRequestID" runat="server" Value='<%# Eval("TravelRequestID")%>' />
                                                                                            <asp:HiddenField ID="uoHiddenFieldIDBigInt" runat="server" Value='<%# Eval("IDBigInt")%>'/>
                                                                                            <asp:HiddenField ID="uoHiddenFieldVehicleTypeID" runat="server" Value='<%# Eval("VehicleTypeID")%>'/>
                                                                                            <asp:Label runat="server" ID="uoLabelVehicleType" Text='<%# Eval("VehicleTypeName")%>'
                                                                                                ToolTip=''
                                                                                            
                                                                                            ></asp:Label>
                                                                                        </td>
                                                                                        <td>  
                                                                                            <asp:Label runat="server" ID="uoLabelCost" Text='<%#  String.Format("{0:#,##0.00}", Eval("TranspoCost"))%>'></asp:Label>
                                                                                        </td>
                                                                                        <td class="leftAligned" >
                                                                                            <asp:Label runat="server" ID="uoLabelCapacity"  Text='<%# Eval("Capacity")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td class="leftAligned" >
                                                                                            <asp:Label runat="server" ID="uoLabelUnitOfMeasure" Text='<%# Eval("UnitOfMeasure")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td class="leftAligned" > 
                                                                                             <asp:Label runat="server" ID="uoLabelRoute" Text='<%# Eval("Route")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <EmptyDataTemplate>
                                                                                    <table cellpadding="0" cellspacing="0" class="listViewTable">
                                                                                        <tr>
                                                                                            <th class="leftAligned" >
                                                                                                <asp:Label runat="server" ID="uoLabelVehicleTypeName" Width="80px" Text="Vehicle Type"></asp:Label>
                                                                                            </th>
                                                                                            
                                                                                            <th  style="text-align: center; white-space: normal;">
                                                                                                <asp:Label runat="server" ID="uoLabelCost" Width="40px" Text="Rate"></asp:Label>
                                                                                            </th>
                                                                                            <th  style="text-align: center; white-space: normal;">
                                                                                                <asp:Label runat="server" ID="uoLabelCapacity" Width="100px" Text="Capacity"></asp:Label>
                                                                                            </th>
                                                                                            
                                                                                            <th  style="text-align: center; white-space: normal;">
                                                                                                <asp:Label runat="server" ID="uoLabelUnitOfMeasure" Width="148px" Text="Unit Of Measure"></asp:Label>
                                                                                            </th>
                                                                                            <th  style="text-align: center; white-space: normal;">     
                                                                                                <asp:Label runat="server" ID="uoLabelRoute" Width="100px" Text="Route"></asp:Label>
                                                                                            </th>
                                                                                        </tr>
                                                                                        <tr> 
                                                                                            <td class="leftAligned" colspan="5">
                                                                                                No Vehicle Type
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </EmptyDataTemplate>
                                                                            </asp:ListView>
                                                                       </asp:Panel>
                                                                       
                                                                    </div>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" style="width:80px;">
                                                                    Comment :
                                                                     <button id="uoButtonVehicleComment" title="View all Comment"  style="height:15px; vertical-align:top;"  />
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="uoTextBoxTransComment" runat="server" onkeyup="SaveTransportation(this)" 
                                                                        TextMode="MultiLine" Rows="2" Width="95%" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" style="width:80px;">
                                                                    Confirm By :
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="uoTextBoxTranpComfirmby" runat="server" onkeyup="SaveTransportation(this)" Width="95%" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </td>
                                   
                                    </tr>
                                
                                </table>
                                
                            </div>
                            <div id="TabMeetAndGreet">
                                <table>
                                    <tr>
                                        <td>
                                            Vendor :
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="uoDropDownListMAndGVendor" Width="449px" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="uoDropDownListMAndGVendor_SelectedIndexChanged" AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Address :
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="uoTextBoxMAndGAddress" Width="444px" runat="server">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Telephone :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="uoTextBoxMAndGTelephone" runat="server"> </asp:TextBox>
                                        </td>
                                        <td>
                                            Airport :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="uoDropDownListMAndGAirport" Width="190px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Service Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="uoTextBoxMAndGServiceDate" runat="server" Text="" Width="70px"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" Enabled="True"
                                                TargetControlID="uoTextBoxMAndGServiceDate" WatermarkCssClass="fieldWatermark"
                                                WatermarkText="MM/dd/yyyy">
                                            </cc1:TextBoxWatermarkExtender>
                                            <cc1:CalendarExtender ID="uoCalendarExtenderMAndGServiceDate" runat="server" Enabled="True"
                                                TargetControlID="uoTextBoxMAndGServiceDate" Format="MM/dd/yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:MaskedEditExtender ID="uoMaskedEditExtenderMAndGServiceDate" runat="server"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxMAndGServiceDate"
                                                UserDateFormat="MonthDayYear">
                                            </cc1:MaskedEditExtender>
                                             Time :
                                             <asp:TextBox runat="server" ID="uoTextBoxServiceTime" Width="40px" Text="00:00"></asp:TextBox>
                                            <%--<cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" Enabled="True"
                                                TargetControlID="uoTxtBoxTimeOut" WatermarkCssClass="fieldWatermark" WatermarkText="HH:mm:tt">
                                            </cc1:TextBoxWatermarkExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxServiceTime"
                                                UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                                            </cc1:MaskedEditExtender>--%>
                                        </td>
                                        <td>
                                           To :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="uoDropDownListMAGTo" Width="190px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Rate :
                                        </td>
                                        <td colspan="1">
                                            <asp:TextBox runat="server" ID="uoTextBoxMAndGRate" Width="70px" Text="00:00">
                                            </asp:TextBox>
                                        </td>
                                        <td>
                                            Flight No :
                                        </td>
                                        <td colspan="1">
                                            <asp:TextBox ID="uoTextBoxMAndGFligthInfo" runat="server" CssClass="ReadOnly" Width="50px"
                                               />
                                            &nbsp; Airline Code :
                                            <asp:TextBox runat="server" ID="uoTextBoxAirlineCode" Width="50px" CssClass="ReadOnly" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:CheckBox ID="uoCheckBoxMAGEmail" Text="Email :" runat="server" onclick="checkboxEnable(this)" />
                                        </td>
                                        <td valign="top" colspan="3">
                                            <asp:TextBox ID="uoTextBoxMAGEmail" Enabled="false" runat="server" Width="95%" Height="83%"
                                                 onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                                                ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                                                TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Comment :
                                        </td>
                                        <td valign="top" colspan="3">
                                            <asp:TextBox ID="uoTextBoxMAGComment" runat="server" Width="95%" Height="83%" TextMode="MultiLine"
                                                Rows="3" />
                                        </td>
                                    
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Confirm By :
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="uoTextBoxMAGConfirm" runat="server" Width="93%" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="TabVisa">
                                <table style="padding-top: 10px;">
                                    <tr>
                                        <td colspan="2">
                                            <b>Global Visa Entry Requirements. </b>
                                        </td>
                                    </tr>
                                    <tr style="padding-top: 5px;">
                                        <td>
                                            Employee Nationality :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="uoTextBoxVNationality" CssClass="ReadOnly" Height="20px" Width="195px"
                                                runat="server"></asp:TextBox>
                                        </td>
                                        <td style="margin-left: 20px">
                                            <asp:CheckBox ID="uoCheckBoxVISSCHENGEN" runat="server" Text="SCHENGEN VISA" />
                                        </td>
                                    </tr>
                                    <tr style="padding-top: 5px;">
                                        <td>
                                            Country Visiting :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="uoDropDownListVCountryVisiting" Width="200px" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListVCountryVisiting_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="margin-left: 20px">
                                            <asp:CheckBox ID="uoCheckBoxVISTRV" runat="server" Text="TRV VISA" />
                                        </td>
                                    </tr>
                                    <tr style="padding-top: 5px;">
                                        <td>
                                            Visa Requirements :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="uoTextBoxVRequired" CssClass="ReadOnly" Height="22px" Width="195px"
                                                runat="server"></asp:TextBox>
                                        </td>
                                        <td style="margin-left: 20px">
                                            <asp:CheckBox ID="uoCheckBoxVISC1D" runat="server" Text="C1/D VISA" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="TabPortagent">
                                <table>
                                    <tr>
                                        <td colspan="1">
                                            Service Provider :
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="uoDropDownListPortAgent" onchange="SavePortAgent(this)"
                                            OnSelectedIndexChanged="uoDropDownListPortAgent_SelectedIndexChanged"
                                                runat="server" Width="449px" AutoPostBack="true"  AppendDataBoundItems="true"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Address :
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="uoTextBoxPAAddress" Width="444px"  onkeyup="SavePortAgent(this)" runat="server">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Telephone :
                                        </td>
                                        <td colspan="4" >
                                            <asp:TextBox ID="uoTextBoxPATelephone" onkeyup="SavePortAgent(this)" runat="server"> </asp:TextBox>
                                            &nbsp;
                                            &nbsp;
                                             Port :&nbsp;
                                             <asp:TextBox ID="uoTextBoxPAPort" onkeyup="SavePortAgent(this)" runat="server" Width="65px" />
                                             &nbsp;&nbsp;
                                             Request Date: 
                                              <asp:TextBox ID="uoTextBoxPARequestDate" onkeyup="SavePortAgent(this)" runat="server" Text="" Width="80px"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" Enabled="True"
                                                TargetControlID="uoTextBoxPARequestDate" WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                                            </cc1:TextBoxWatermarkExtender>
                                            <cc1:CalendarExtender ID="CalendarExtender3"  runat="server" Enabled="True" TargetControlID="uoTextBoxPARequestDate"
                                                Format="MM/dd/yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxPARequestDate"
                                                UserDateFormat="MonthDayYear">
                                            </cc1:MaskedEditExtender>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td />
                                        <td style="padding-top: 1px;" colspan="3">
                                            <asp:Panel ID="Panel3" runat="server" GroupingText="Services" Font-Bold="true">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="uoCheckBoxPAMAG" onclick="SavePortAgent(this)" 
                                                                Font-Bold="false" Text="Meet and Greet" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="uoCheckBoxPALuggage" onclick="SavePortAgent(this)" 
                                                                Font-Bold="false" Text="Luggage" runat="server" />
                                                                
                                                            <asp:CheckBox ID="uoCheckBoxPATrans" onclick="SavePortAgent(this)" 
                                                                 Font-Bold="false" Text="Transportation" runat="server" style="display:none;" />
                                                        </td>
                                                        <td>
                                                        <asp:CheckBox ID="uoCheckBoxPASafeguard" onclick="SavePortAgent(this)" 
                                                                Font-Bold="false" Text="Safeguard" runat="server" />
                                                        
                                                            <asp:CheckBox ID="uoCheckBoxPAHotel" onclick="SavePortAgent(this)" 
                                                                Font-Bold="false" Text="Hotel" runat="server" style="display:none;" />
                                                                
                                                                
                                                        </td>
                                                        <td>
                                                             <asp:CheckBox ID="uoCheckBoxPAVisa" onclick="SavePortAgent(this)" 
                                                            Font-Bold="false" Text="Visa" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space:nowrap;">
                                                            <asp:CheckBox ID="uoCheckBoxPhoneCards" onclick="SavePortAgent(this)" 
                                                            Font-Bold="false" Text="Phone Cards" runat="server" />
                                                            <asp:TextBox ID="uoTextBoxPhonecards" onkeyup="SavePortAgent(this)" runat="server" Width="40px" />
                                                        </td>
                                                        <td style="white-space:nowrap;">
                                                           <asp:CheckBox ID="uoCheckBoxLaundry" onclick="SavePortAgent(this)" 
                                                            Font-Bold="false" Text="Laundry" runat="server" />
                                                            <asp:TextBox ID="uoTextBoxLaundry" onkeyup="SavePortAgent(this)" runat="server" Width="40px" />
                                                        </td>
                                                        <td style="white-space:nowrap;">
                                                           <asp:CheckBox ID="uoCheckBoxGiftCard" onclick="SavePortAgent(this)" 
                                                            Font-Bold="false" Text="Laundry" runat="server" />
                                                            <asp:TextBox ID="uoTextBoxGiftCard" onkeyup="SavePortAgent(this)" runat="server" Width="40px" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="uoCheckBoxPAOther" onclick="SavePortAgent(this)" Font-Bold="false" Text="Other" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:CheckBox ID="uoCheckBoxPAEmail" Text="Email :" runat="server" onclick="checkboxEnable(this)" />
                                        </td>
                                        <td valign="top" colspan="3">
                                            <asp:TextBox ID="uoTextBoxPAEmail" Enabled="false" onkeyup="SavePortAgent(this)" runat="server" Width="93%" Height="83%"
                                                onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                                                ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                                                TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Comment :
                                        </td>
                                        <td valign="top" colspan="3">
                                            <asp:TextBox ID="uoTextBoxPAComment" runat="server" onkeyup="SavePortAgent(this)" Width="93%" Height="83%" TextMode="MultiLine"
                                                Rows="3" />
                                        </td>
                                    
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Confirm By :
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="uoTextBoxPAConfirm" onkeyup="SavePortAgent(this)" runat="server" Width="90%" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="TabSafeguard">
                                <table>
                                    <tr>
                                        <td colspan="1">
                                            Safeguard Vendor :
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="uoDropDownListSafeguard" 
                                            OnSelectedIndexChanged="uoDropDownListSafeguard_SelectedIndexChanged"
                                                runat="server" Width="449px" AutoPostBack="true" AppendDataBoundItems="true"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Address :
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="uoTextBoxSAddress" Width="444px" runat="server">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Telephone :
                                        </td>
                                        <td colspan="1">
                                            <asp:TextBox ID="uoTextBoxSafeguardTelephone" runat="server"> </asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Request Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="uoTextBoxSafeguarDate" runat="server" Text="" Width="80px"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" Enabled="True"
                                                TargetControlID="uoTextBoxSafeguarDate" WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                                            </cc1:TextBoxWatermarkExtender>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="uoTextBoxSafeguarDate"
                                                Format="MM/dd/yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxSafeguarDate"
                                                UserDateFormat="MonthDayYear">
                                            </cc1:MaskedEditExtender>
                                        </td>
                                        <td>
                                            Time
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="uoTextBoxSafeguardTime" Width="45px" Text="00:00"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" Enabled="True"
                                                TargetControlID="uoTextBoxSafeguardTime" WatermarkCssClass="fieldWatermark" WatermarkText="HH:mm:tt">
                                            </cc1:TextBoxWatermarkExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxSafeguardTime"
                                                UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                                            </cc1:MaskedEditExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Service Rendered :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="uoDropDownListServiceRender" runat="server" OnSelectedIndexChanged="uoDropDownListServiceRender_SelectedIndexChanged"
                                                Width="150px" AutoPostBack="true" />
                                        </td>
                                        <td>
                                            Rate :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="uoTextBoxSafeguardRate" ReadOnly="true" CssClass="ReadOnly" Width="100px"
                                                runat="server">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:CheckBox ID="uoCheckBoxSafeguardEmail" Text="Email :" runat="server" onclick="checkboxEnable(this)" />
                                        </td>
                                        <td valign="top" colspan="3">
                                            <asp:TextBox ID="uoTextBoxSafeguardEmail" Enabled="false" runat="server" Width="93%"
                                                 onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                                                ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                                                Height="83%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Comment :
                                        </td>
                                        <td valign="top" colspan="3">
                                            <asp:TextBox ID="uoTextBoxSafeguardComment" runat="server" Width="93%" Height="83%"
                                                TextMode="MultiLine" Rows="3" />
                                        </td>
                                    
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Confirm By :
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="upTextBoxSComfirm" runat="server" Width="90%" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <span style="font-size: 14px; padding-top: 10px;"><b>CC Email </b>&nbsp; &nbsp;
                                <asp:CheckBox ID="uoCheckBoxCopyAll" runat="server" onclick="checkboxEnable(this)"
                                    Text="Copy All" />
                                &nbsp; </span>&nbsp;
                            <asp:CheckBox ID="CheckBoxCopycrewassist" runat="server" onclick="checkboxEnable(this)"
                                Text="Copy crew assist" />&nbsp; &nbsp;
                            <asp:CheckBox ID="CheckBoxCopycrewhotels" Text="Copy crew hotels" runat="server"
                                onclick="checkboxEnable(this)" />&nbsp; &nbsp;
                            <asp:CheckBox ID="CheckBoxCopyShip" Text="Copy Ship" runat="server" onclick="checkboxEnable(this)" />&nbsp;
                            &nbsp;
                            <asp:CheckBox ID="CheckBoxScheduler" Text="Copy Scheduler" runat="server" onclick="checkboxEnable(this)" />
                            <br />
                            <div style="padding-top: 5px;">
                                Other Email :
                                <asp:TextBox ID="uoTextBoxEmailOther" Style="margin-bottom: -5px; margin-left: 14px;"
                                    onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                                    ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                                    runat="server" Width="400px" TextMode="MultiLine" Rows="1" />
                            </div>
                         
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="width:100%; white-space:nowrap; padding-top:4px;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="uoButtonFinish" CssClass="SmallButton" Text="Finish"
                                        ValidationGroup="Request" OnClick="uoButtonFinish_click" OnClientClick="return ConfirmButtonFinish()"/>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label5"  CssClass="RedNotification" runat="server" 
                                        Text="Multiple emails should be separated by semicolon (i.e.  abc@rccl.com;xyz@rccl.com)"> 
                                        </asp:Label> 
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <%--End Of Right Pane--%>
        </tr>
        <%--ADD COMPANION/S and Add Button--%>
      <%--  OnCheckedChanged="uoCheckBoxAddCompanion_OnCheckedChanged"--%>
        <tr style="height: 2px; ">
            <td>
                <asp:CheckBox ID="uoCheckBoxAddCompanion" runat="server"
                 Text="Add Companion/s" 
                    AutoPostBack="True" />
            </td>
            <%-- <td style="padding-bottom: 2px; padding-left: 20px; height: 2px;">
                
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/positive.png" />
            </td>--%>
        </tr>
    </table>
    <table id="uoTableCompanion" runat="server" style="text-align: left; width: 100%"
        class="LeftClass">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            Last Name :
                        </td>
                        <td style="width: 100%">
                            <asp:TextBox ID="uoTextBoxCompLastname" Width="300px" MaxLength="200" runat="server" />
                            <asp:RequiredFieldValidator ID="uoRFVCompLastname" ControlToValidate="uoTextBoxCompLastname"
                                ValidationGroup="Companion" runat="server" ErrorMessage="required" Font-Bold="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            First Name :
                        </td>
                        <td style="width: 100%">
                            <asp:TextBox ID="uoTextBoxCompFirstname" Width="300px" MaxLength="200" runat="server" />
                            <asp:RequiredFieldValidator ID="uoRFVCompFirstname" ControlToValidate="uoTextBoxCompFirstname"
                                ValidationGroup="Companion" runat="server" ErrorMessage="required" Font-Bold="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Relationship :
                        </td>
                        <td style="width: 100%">
                            <asp:TextBox ID="uoTextBoxCompRelationship" Width="300px" MaxLength="200" runat="server" />
                            <asp:RequiredFieldValidator ID="uoRFVCompRelationship" ControlToValidate="uoTextBoxCompRelationship"
                                ValidationGroup="Companion" runat="server" ErrorMessage="required" Font-Bold="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Gender:
                        </td>
                        <td style="white-space:nowrap;">
                            <asp:DropDownList ID="uoDropDownListCompGender" runat="server" Width="150px" AppendDataBoundItems="True">
                                <asp:ListItem Value="0" Text="--SELECT GENDER--" />
                                <asp:ListItem Value="107" Text="MALE" />
                                <asp:ListItem Value="106" Text="FEMALE" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="uoRFVCompGender" ControlToValidate="uoDropDownListCompGender"
                                InitialValue="0" ValidationGroup="Companion" runat="server" ErrorMessage="required"
                                Font-Bold="true" BackColor="AliceBlue" /> 
                                
                            <asp:Button ID="uoButtonAddComp" runat="server" Text="Add Companion" CssClass="SmallButton"
                                OnClick="uoButtonAddComp_Click" ValidationGroup="Companion" />
                                
                        <%--    <asp:Button ID="uoButtonSendComp" runat="server" Text="Send" 
                                        CssClass="SmallButton"  OnClick="uoButtonSendComp_Click"  />    --%>
                              
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 100%; vertical-align: top; text-align: left">
                <asp:ListView runat="server" ID="uoListViewCompanionList" OnItemCommand="uoListViewCompanionList_ItemCommand" >
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTableClass" align="left">
                            <tr>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelLastname" Width="148px" Text="LASTNAME"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelFirstname" Width="150px" Text="FIRSTNAME"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelRelationShip" Width="150px" Text="RELATIONSHIP"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelGender" Width="148px" Text="GENDER"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label2" Width="67px" Text="REMOVE" />
                                </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="uoLabelLastname" Width="150px" Text='<%# Eval("LASTNAME") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="uoLabelFirstname" Width="150px" Text='<%# Eval("FIRSTNAME") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="uoLabelRelationship" Width="150px" Text='<%# Eval("RELATIONSHIP") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="uoLabelGender" Width="150px" Text='<%# Eval("GENDER") %>' />
                            </td>
                          
                            <td   >
                                
                                <asp:HiddenField ID="uoHiddenFieldGENDERID" runat="server" Value='<%# Eval("GENDERID") %>' />
                                <asp:HiddenField ID="uoHiddenFieldComReqID" runat="server" Value='<%# Eval("REQUESTID") %>' />
                                <asp:HiddenField ID="uoHiddenFieldComReqDetID" runat="server" Value='<%# Eval("DETAILID") %>' />
                                
                                <asp:HiddenField ID="uoHiddenFieldTRID" runat="server" Value='<%# Eval("TRAVELREQID") %>' />
                                <asp:HiddenField ID="uoHiddenFieldIDBIGNT" runat="server" Value='<%# Eval("IDBIGINT") %>' />
                                <asp:HiddenField ID="uoHiddenFieldSeqNo" runat="server" Value='<%# Eval("SEQNO") %>' />
                                <asp:HiddenField ID="uoHiddenFieldRecLoc" runat="server" Value='<%# Eval("RECORDLOCATOR") %>' />
                                
                                <asp:HiddenField ID="uoHiddenFieldPA" runat="server" Value='<%# Eval("IsPortAgent") %>' />
                                <asp:HiddenField ID="uoHiddenFieldComIsMedical" runat="server" Value='<%# Eval("IsMedical") %>' />
                            
                                <asp:LinkButton ID="uoLinkButtonRemove" Width="68px" runat="server" OnClientClick="return ConfirmDeleteCompanion()"
                                    CommandArgument='<%# Eval("DETAILID") %>' CommandName="remove" Text="Remove" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table <  cellpadding="0" cellspacing="0" class="listViewTableClass" align="left">
                            <tr>
                                 <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelLastname" Width="148px" Text="LASTNAME"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelFirstname" Width="150px" Text="FIRSTNAME"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelRelationShip" Width="150px" Text="RELATIONSHIP"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelGender" Width="148px" Text="GENDER"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label2" Width="67px" Text="REMOVE" />
                                </th>
                            </tr>
                            <tr>
                                <td colspan="6" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </td>
        </tr>
    </table>
    
  
    
    <div id="centerdiv" runat="server" class="centerdiv">
        <table style="width: 100%; text-align: left;" id="tbConfirm">
            <tr class="PageTitle">
                <td colspan="2">
                    Confirm
                    <asp:ImageButton ID="ImageButton1" runat="server" OnClick="Closed_Click" 
                        ImageUrl="~/FBox/fancybox/fancy_close.png"
                        ImageAlign="Right" CssClass="imagePos" />
                </td>
            </tr>
            <tr>
                <td style="width: 250px;">
                    <asp:Label ID="uoLabelContractRate" runat="server" Text="Was the contracted rate of $ 89 Confimed" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBoxYes" runat="server" Text="Yes" />&nbsp;
                    <asp:CheckBox ID="CheckBoxNo" runat="server" AutoPostBack="true" Text="No" OnCheckedChanged="CheckBoxNo_OnCheckedChanged" />&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text=" Name of the hotel agent who confimed	" />
                </td>
                <td>
                    <asp:TextBox ID="TextBoxWhoConfirm" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Confirmed rate
                </td>
                <td>
                    <asp:TextBox ID="TextBoxConfirmrate" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none; " >
                <td >
                    <asp:Label ID="Label1" runat="server" Text="Email Add: " />
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxEmailAdd" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Label ID="Label7"  CssClass="RedNotification" runat="server" 
                         Text="Click Send button to save the hotel request">  
                    </asp:Label>       
                    <asp:Button ID="ButtonSend" runat="server" Text="Send" OnClientClick="HiveDiv()"
                        OnClick="uoButtonSend_click" />
                </td>
            </tr>
        </table>
    </div>
    
    
  <%-- begin Remark Control--%>
    
    <div id="RemarkDiv" class="RemarkDiv" style="background-color:White; padding:5px 10px 5px 10px; ">
         <div class="PageTitle" >
           Remarks
        </div>
        <hr />
        <asp:Panel ID="Panel2" ScrollBars="Auto" runat="server"
             style="max-height:20em;height:20em; width:100%; 
                padding-bottom:2px; " >
            <asp:ListView ID="uoListViewRemarkPopup" runat="server" >
                   
                <LayoutTemplate>
                    <table border="0" id="uoListviewTableRemark" style="border-style:none; width:99%;"  
                             cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                     <tr >
                        <td style="min-height:22px;height:22px; text-align:left; 
                                   margin:5px; padding:8px; width:385px; max-width:390px;" 
                                >
                            <asp:HiddenField ID="uoHiddenFieldGridRemarkID" runat="server" Value='<%# Eval("RemarkID")%>'/>
                            <asp:HiddenField ID="uoHiddenFieldRemarkTRID" runat="server" Value='<%# Eval("TravelRequestID")%>'/>
                            <asp:HiddenField ID="uoHiddenFieldResourceID" runat="server" Value='<%# Eval("ReqResourceID")%>'/>
                            
                            <asp:HiddenField ID="uoHiddenFieldTypeID" runat="server" Value='<%# Eval("RemarkTypeID")%>'/>
                            <asp:HiddenField ID="upHiddenFieldStatusID" runat="server" Value='<%# Eval("RemarkStatusID")%>'/>
                            <asp:HiddenField ID="uoHiddenFieldSumCall" runat="server" Value='<%# Eval("SummaryOfCall")%>'/>
                            <asp:HiddenField ID="uoHiddenFieldRequestorID" runat="server" Value='<%# Eval("RemarkRequestorID")%>'/>
                            <asp:HiddenField ID="uoHiddenFieldTransDate" runat="server" Value='<%# String.Format("{0:MM/dd/yyyy}", Eval("TransactionDate"))%>'/>
                            <asp:HiddenField ID="uoHiddenFieldTransTime" runat="server" Value='<%# String.Format("{0:HH:mm}", Eval("TransactionTime"))%>'/>
                            <asp:HiddenField ID="uoHiddenFieldPortCode" runat="server" Value='<%# Eval("PortCode")%>'/>
                            <asp:CheckBox  ID="uoCheckBoxIR" runat="server" Checked='<%# Eval("IR")%>' style="display:none"/>
                                                                            
                            <asp:Label runat="server" ID="lblUserName"   Text='<%# Eval("RemarkBy") %>'
                                style="font-style:normal; font-size:small; font-weight:bold;"/>
                            <br/>
                            <asp:Label runat="server" ID="uoLabelRemark" Width="375px" 
                               Text='<%# Eval("Remark") %>' />
                            <br/><b>
                            <asp:Label runat="server" ID="Label6"  Text='<%# Eval("RecordLocator") %>'
                                style="font-size:xx-small; color:#B0B0B0; font-style:oblique; "
                            /></b>
                             &nbsp;
                           <asp:Label runat="server" ID="Label4"  Text='<%# Eval("Resource") %>'
                                style="font-size:xx-small; color:#C0C0C0"
                            />
                             &nbsp;
                            <asp:Label runat="server" ID="lblDateCreated"  Text='<%# Eval("RemarkDate") %>'
                                style="font-size:xx-small; color:#C0C0C0; white-space:nowrap; "
                            />
                             &nbsp; &nbsp;
                            <asp:LinkButton ID="uoLinkButtonEditRemark" Text="Edit"  
                                runat="server" OnClientClick="EditRemark(this)" ToolTip="Edit Remark"
                                style="font-size:xx-small;"
                                Visible='<%# Convert.ToBoolean(Eval("Visible").ToString() == "True" ? 1 : 0) %>'/>
                            &nbsp;
                            <asp:LinkButton ID="uoLinkButtonDeleteRemark" ToolTip="Delete Remark" Text="Delete" 
                                OnClick="ButtonDeleteRemark_click" style="font-size:xx-small;"
                                runat="server" OnClientClick="return DeleteRemark(this)" Visible='<%# Convert.ToBoolean( Eval("Visible").ToString() == "True" ? 1 : 0) %>' />
                          
                        </td>
                        
                    </tr>
                    
                    
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table style="border-style:none;"   cellpadding="0" cellspacing="0" class="listViewTable">
                         <tr  style="height:100%; vertical-align:top;"> 
                            <td class="leftAligned" colspan="3" style="height:100%; vertical-align:top;">
                                No Remark 
                            </td>
                        </tr>
                    </table>
                    
                </EmptyDataTemplate>
            </asp:ListView>
        </asp:Panel>
    
    </div>
    
    <div id="HotelRemarkDiv" class="HotelRemarkDiv" style="background-color:White; padding:5px 10px 5px 10px; ">
         <div class="PageTitle" >
           Comment
        </div>
        <hr />
        <asp:Panel ID="Panel7" ScrollBars="Auto" runat="server"
             style="max-height:9.08em;height:9.08em; width:100%; 
                padding-bottom:2px; " >
            <asp:ListView ID="uoHotelListViewRemark" runat="server" >
                   
                <LayoutTemplate>
                    <table border="0" id="uoListviewTableRemark" style="border-style:none; width:99%;"  
                             cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                     <tr >
                        <td style="min-height:22px;height:22px; text-align:left; 
                                    margin:5px; padding:8px; width:385px; 
                                     max-width:390px;"
                                >
                            <asp:HiddenField ID="uoHiddenFieldGridRemarkID" runat="server" Value='<%# Eval("RemarkID")%>'/>
                            <asp:HiddenField ID="uoHiddenFieldRemarkTRID" runat="server" Value='<%# Eval("TravelRequestID")%>'/>
                            <asp:HiddenField ID="uoHiddenFieldResourceID" runat="server" Value='<%# Eval("ReqResourceID")%>'/>
                
                            <asp:Label runat="server" ID="lblUserName"   Text='<%# Eval("RemarkBy") %>'
                                style="font-style:normal; font-size:small; font-weight:bold;"/>
                            <br/>
                            <asp:Label runat="server" ID="uoLabelRemark" Width="375px" 
                               Text='<%# Eval("Remark") %>' />
                            <br/>
                           <asp:Label runat="server" ID="Label4"  Text='<%# Eval("Resource") %>'
                                style="font-size:xx-small; color:#C0C0C0"
                            />
                             &nbsp;
                            <asp:Label runat="server" ID="lblDateCreated"  Text='<%# Eval("RemarkDate") %>'
                                style="font-size:xx-small; color:#C0C0C0; white-space:nowrap; "
                            />
                             &nbsp; &nbsp;
                           
                        </td>
                    </tr>
                    
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table style="border-style:none;"   cellpadding="0" cellspacing="0" class="listViewTable">
                         <tr  style="height:100%; vertical-align:top;"> 
                            <td class="leftAligned" colspan="3" style="height:100%; vertical-align:top;">
                                No Remark 
                            </td>
                        </tr>
                    </table>
                    
                </EmptyDataTemplate>
            </asp:ListView>
        </asp:Panel>
    
    </div>
    
    <div id="VehicleRemarkDiv" class="HotelRemarkDiv" style="background-color:White; padding:5px 10px 5px 10px; ">
         <div class="PageTitle" >
           Comment
        </div>
        <hr />
        <asp:Panel ID="Panel8" ScrollBars="Auto" runat="server"
             style="max-height:9.08em;height:9.08em; width:100%; 
                padding-bottom:2px; " >
            <asp:ListView ID="uoListViewVehicleRemark" runat="server" >
                   
                <LayoutTemplate>
                    <table border="0" id="uoListviewTableRemark" style="border-style:none; width:99%;"  
                             cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                     <tr >
                        <td style="min-height:22px;height:22px; text-align:left; 
                                    margin:5px; padding:8px; width:385px; 
                                     max-width:390px;"
                                >
                            <asp:HiddenField ID="uoHiddenFieldGridRemarkID" runat="server" Value='<%# Eval("RemarkID")%>'/>
                            <asp:HiddenField ID="uoHiddenFieldRemarkTRID" runat="server" Value='<%# Eval("TravelRequestID")%>'/>
                            <asp:HiddenField ID="uoHiddenFieldResourceID" runat="server" Value='<%# Eval("ReqResourceID")%>'/>
                
                            <asp:Label runat="server" ID="lblUserName"   Text='<%# Eval("RemarkBy") %>'
                                style="font-style:normal; font-size:small; font-weight:bold;"/>
                            <br/>
                            <asp:Label runat="server" ID="uoLabelRemark" Width="375px" 
                               Text='<%# Eval("Remark") %>' />
                            <br/>
                            
                           <asp:Label runat="server" ID="Label4"  Text='<%# Eval("Resource") %>'
                                style="font-size:xx-small; color:#C0C0C0"
                            />
                             &nbsp;
                            <asp:Label runat="server" ID="lblDateCreated"  Text='<%# Eval("RemarkDate") %>'
                                style="font-size:xx-small; color:#C0C0C0; white-space:nowrap; "
                            />
                             &nbsp; &nbsp;
                           
                        </td>
                    </tr>
                    
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table style="border-style:none;"   cellpadding="0" cellspacing="0" class="listViewTable">
                         <tr  style="height:100%; vertical-align:top;"> 
                            <td class="leftAligned" colspan="3" style="height:100%; vertical-align:top;">
                                No Remark 
                            </td>
                        </tr>
                    </table>
                    
                </EmptyDataTemplate>
            </asp:ListView>
        </asp:Panel>
    
    </div>
    
   <%-- End Remark Control--%> 
    
    <div id="DivCrewAssistComment"  class="DivCrewAssistComment" style="background-color:White;
         padding:5px 10px 5px 10px; display:none;">
         <div class="PageTitle"  style="overflow:hidden; white-space:nowrap; ">
           Request Source
        </div>
        <hr />
        <asp:RadioButtonList ID="uoRadioButtonListComment" style="margin-left:-10px; text-align:left;" runat="server">         
        
            <asp:ListItem Text="Via Email" Value="1"/>
            <asp:ListItem Text="Via Call" Value="2"/>
            <asp:ListItem Text="Via Live Chat" Value="3"/>
            <asp:ListItem Text="Via Queue" Value="4"/>
            
            <asp:ListItem Text="Hotel Exception" Value="5"/>
            <asp:ListItem Text="Flight Stats" Value="6"/>
            <asp:ListItem Text="HR Port Logistics" Value="7"/>
            
            
        </asp:RadioButtonList>
     </div>
     
     <div id="immigration"  class="DivCrewAssistComment" style="background-color:White;
          display:none; position:absolute; text-align:left;  padding:5px 10px 5px 10px;">
        
     </div>
    
    
    <div id="DivRemarkType"  class="DivRemarkType" style="background-color:White;position:absolute; width:368px; 
                             border-style:solid; border-width:thin; padding:5px 10px 5px 10px; display:none;">
       <table style="width:100% ">
            <tr>
                <td style="white-space:nowrap" ><asp:Label ID="lblRemarkType" runat="server" Text="Call" />&nbsp;Type:</td>
                <td style="width:100%; margin-left:-5px"><asp:DropDownList ID="cboRemarkType" Width="100%"  Visible="false" runat="server"/> 
                    <div id="DivRemcontainer" style="float: left;  width: 100%; background-color:White;">
                               <table>
                                <tr>
                                    <td style="width:100%; "><input type="text" id="treeViewSearchInput" runat="server" style="width:100%; " placeholder=" -- select --" />
                                        <input type="text" id="treeViewSearchInputID" runat="server" style="width:100%; display:none;" placeholder="ID" />
                                    </td>
                                    <td><input id="treeviewDropdownBtn" type="button" value="v"  /></td>
                                </tr>
                               </table>
                               
                                <%--<div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;--%>
                               
                               <div id="DivRemarktreeview" style="display: none; position:absolute; z-index:99; background-color:White;
                                   border-style:solid; border-width:thin;overflow: auto; overflow-x: hidden; max-height:400px; "  >
                                  
                                            <asp:TreeView runat="server" ID="Remarktreeview" LeafNodeStyle-CssClass="childnode" 
                                                ForeColor="Black" EnableClientScript="true" PopulateNodesFromClient="true">
                                                <HoverNodeStyle ForeColor="White" BackColor="#0288D8" />
                                             </asp:TreeView>
                                             <input id="btnCloseRemark" type="button" value="x" style="display:none"  class="SmallButton" />
                               </div>  
                         </div>
                
                
                </td>
            </tr>
            
          <%--  
            <tr>
                <td style="white-space:nowrap" ><asp:Label ID="Label10" runat="server" Text="Call" />&nbsp;Type:</td>
                <td style="width:100%">
                    
                
                
                </td>
            </tr>--%>
            
            
            
            <tr>
                <td style="white-space:nowrap" >Transaction Status:</td>
                <td style="width:100%"><asp:DropDownList ID="cboRemarkStatus" Width="100%"  runat="server"></asp:DropDownList></td>
            </tr>
             <tr>
                <td style="white-space:nowrap" >Requestor:</td>
                <td style="width:100%"><asp:DropDownList ID="cboRemarkRequestor" Width="100%"  runat="server"></asp:DropDownList></td>
            </tr>
             <tr>
                <td style="white-space:nowrap" >Trans Date:</td>
                <td style="width:100%; white-space:nowrap">
                    <asp:TextBox ID="uoTextBoxRemTransdate" runat="server" Text="" Width="80px"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderTransdate"
                        runat="server" Enabled="True" TargetControlID="uoTextBoxRemTransdate" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    
                    <cc1:CalendarExtender BehaviorID="CalendarExtenderTransdate" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxRemTransdate" Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    
                    <cc1:MaskedEditExtender ID="MaskedEditExtenderTransdate" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                        TargetControlID="uoTextBoxRemTransdate"
                        UserDateFormat="MonthDayYear">
                    </cc1:MaskedEditExtender>
                    
                    &nbsp; &nbsp;Trans Time:&nbsp;
                    <asp:TextBox runat="server" ID="uoTextBoxRemTransTime"  Width="80px"  ToolTip=" 24 hr format (e.g. 22:30)" ></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender BehaviorID="WatermarkExtenderRemTransTime" ID="TextBoxWatermarkExtender8" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxRemTransTime" WatermarkCssClass="fieldWatermark"
                        WatermarkText="hh:mm" >
                    </cc1:TextBoxWatermarkExtender>
                    
                    <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                        Enabled="True" Mask="99:99" MaskType="Time" BehaviorID="MaskedEditExtenderRemTransTime"  TargetControlID="uoTextBoxRemTransTime"
                        UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                     </cc1:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td  colspan="1" style="white-space:nowrap">Details Of Concerns</td>
                 <td  colspan="1" style="text-align:right"><asp:CheckBox ID="uoCheckBoxIR" runat="server" Text="Incident Report"  /></td>
            </tr>
            <tr>
                <td colspan="2" >
                   <asp:TextBox ID="txtSummaryCall" runat="server" style="border-color:#C0C0C0;" TextMode="MultiLine" Height="60px" Width="100%" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right">
                    <button type="button" id="btnRamarkTypeAdd" class="SmallButton">Add</button>
                    <button type="button" id="btnRemarkTypeClose" class="SmallButton">Close</button>
                </td>
            </tr>
       </table>
    </div>
    
     
  
        <asp:Button ID="uoButtonOverflow" runat="server" Style="display: none" OnClick="uoButtonOverflow_Click" />
        <asp:Button ID="btnClearControl" runat="server" Style="display: none" OnClick="btnClearControl_Click" />
        <asp:Button runat="server" ID="uoButtonLoadAir" Text="LoadAir" OnClick="uoButtonLoadAir_click"
            Style="display: none" />
        <asp:Button runat="server" ID="uoButtonChangePort" OnClick="uoButtonChangePort_click"
            Style="display: none" />
        <asp:Button runat="server" ID="uoButtonCopyEmail" OnClick="uoButtonCopyEmail_click"
            Style="display: none" />
        
        <asp:DropDownList ID="cboAirportList" runat="server" style="display:none; position:absolute; "/>
        
        <asp:Button ID="uoButtonFromTo" runat="server" Style="display: none" OnClick="uoButtonFromTo_Click" />
        <asp:HiddenField ID="uoHiddenFieldRank" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldVesselID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldCostCenterID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldGender" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldGenderID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldHotelRequestDetailID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldHotelRequestID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldTravelRequestID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
        <asp:HiddenField ID="HiddenFieldSearch" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldSaveType" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldSeqNo" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldIDBigint" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldBrandID" runat="server" Value="0" />
        <asp:HiddenField ID="HiddenFieldHideCenter" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldNoOfNites" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldVesselEmail" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldHotelEmail" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldCheckBox" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldCheckBoxLoaded" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldPortCode" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldCrewAssist" runat="server" Value="" />
        
        <asp:HiddenField ID="uoHiddenFieldEmailHotel" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldCopyShip" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldScheduler" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldRequestDate" runat="server" Value="" />

        <asp:HiddenField ID="uoHiddenFieldTransVendorID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldNationality" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldMeetAndGreetID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldFligthNo" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldCityCode" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldAirlineCode" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldDepartureTime" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldArrivalTime" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldDeptCode" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldArrCode" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldPortAgentID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldSGContractID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldSGContSerTypeID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldSafeguardRequestID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldTRSequenceNo" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldTransHotelOtherID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldTransTransapotationID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldTransMeetAndGreetID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldTransPortAgentID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldTransSafeguardID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldOverflowMessage" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldAirStatus" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldSaveConfirmation" runat="server" Value="" />
        
       
        <asp:HiddenField ID="uoHiddenFieldSaveConfirmationTrans" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldSaveConfirmationMAG" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldSaveConfirmationPA" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldSaveConfirmationSG" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldRecordLocator" runat="server" Value="" />

        
        <asp:HiddenField ID="uoHiddenFieldSaveHotel" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldSaveTrans" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldSaveMAG" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldSavePA" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldSaveSG" runat="server" Value="" />

        <asp:HiddenField ID="uoHiddenFieldRemarkID" runat="server" Value="" />

        <asp:HiddenField ID="uoHiddenFieldVTypeID" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldPAHContAmount" runat="server" Value="" />

        <asp:HiddenField ID="uoHiddenFieldPATRFrom" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldPATRTo" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldPATRFromRoute" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldPATRToRoute" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldPATRPickUpDate" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldPATRTime" runat="server" Value="" />

        <asp:HiddenField ID="uoHiddenFieldShowTable" runat="server" Value="" />

        <asp:HiddenField ID="uoHiddenFieldFromTo" runat="server" Value="" />
        
        <asp:HiddenField ID="uoHiddenFieldReqResourceFrom" runat="server" Value="" />
        
        <asp:HiddenField ID="uoHiddenFieldHSourceRequest" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldRSourceRequest" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldTSourceRequest" runat="server" Value="" />

        <asp:HiddenField ID="uoHiddenFieldAirportFromTo" runat="server" Value="" />

        <asp:HiddenField ID="uoHiddenFieldPAAirportHotel" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldPAHotelShip" runat="server" Value="" />
        
        <asp:HyperLink ID="uoHyperLinkCancelHotel" class="Cancellation" runat="server">
             <asp:Button runat="server" ID="uoButtonCancelHotel" Style="display: none" />
        </asp:HyperLink>
        
        <asp:Label ID="uoLabelUserID" runat="server" Text="" Style="display: none; position:absolute;" />
        
        <asp:HiddenField ID="uoHiddenFieldCrewHotelCancelPopup" runat="server" Value="" />
       
        <asp:HiddenField ID="uoHiddenFieldCancelHotel" runat="server" Value="0" />
        
        <asp:HyperLink ID="uoHyperLinkHotelConfirmation" runat="server" class="HotelConfirmation"  Text= "Test" style="display:none;">
        </asp:HyperLink>
       
       
        <asp:HiddenField ID="uoHiddenFieldHotelConfirmRate" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldHotelConfirmBy" runat="server" Value="" />
        <%--<asp:Button runat="server" ID="uoButtonApprovedHotel" Style="display: none" OnClick="uoButtonApprovedHotel_click" />--%>
       
        <asp:HiddenField ID="uoHiddenFieldFlightStatus" runat="server" Value="false" />
    
        <asp:HiddenField ID="uoHiddenFieldContractStart" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldContractEnd" runat="server" Value="" />
       
        </ContentTemplate>
    </asp:UpdatePanel> 
    
    <script type="text/javascript" language="javascript">

        $(document).ready(function() {

            KeyUpText();
            HighLightTab();
            HideDate();
            RemarkDiv();
            DropDown();
            InitializeRemarkTree();
        });
     

         function pageLoad(sender, args) {
             var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
             if (isAsyncPostback) {
                 KeyUpText();
                 HighLightTab();
                 HideDate();
                 RemarkDiv();
                 DropDown();
                 InitializeRemarkTree();
             }
         }
 
         function InitializeRemarkTree() {
             var ex = 0;
             $("#ctl00_NaviPlaceHolder_Remarktreeview > table > tbody > tr > td > a").each(function() {
                 $($(this)).addClass("expand");
                 var a = $(this).children('img');
                 ex = ex + 1;
                 $(a).attr('id', 'img' + ex);
                 $(a).attr('src', '../../Images/box_plus.png');
             });
             var dv = 0;
             $("#ctl00_NaviPlaceHolder_Remarktreeview > div").each(function() {
                dv = dv + 1;
                $($(this)).attr('id', 'dimg' + dv);  
            });



            $("#ctl00_NaviPlaceHolder_Remarktreeview > div > table > tbody > tr > td > a").each(function() {
                $($(this)).attr('onclick', 'OnTreeClick(this)');
            });  

             $("[id$=treeviewDropdownBtn]").click(function() {
                 document.getElementById("DivRemarktreeview").style.display = "block";
             });

             $("[id$=treeViewSearchInput]").click(function() {
                 document.getElementById("DivRemarktreeview").style.display = "block";
             });



             $("#btnCloseRemark").click(function() {
                 document.getElementById("DivRemarktreeview").style.display = "none";
             });



             $(".childnode").click(function() {
                 $('.childnode').each(function() {
                     $(this).css('backgroundColor', '');
                     $(this).css('color', '#000000');
                 });
                 $(this).css('backgroundColor', '#0288D8');
                 $(this).css('color', '#fff');
                 document.getElementById("DivRemarktreeview").style.display = "none";
                 return false;
             });

             $(".expand").click(function() {
                 var a = $(this).children('img');
                 var b = $(this).children('img');
                 var n = $(a)[0].id.replace('img', 'dimg');
                 if (document.getElementById(n).style.display == "none") {
                     document.getElementById(n).style.display = "block";
                     $(b).attr('src', '../../Images/box_minus.png');
                 }
                 else {
                     document.getElementById(n).style.display = "none";
                     $(b).attr('src', '../../Images/box_plus.png');
                 }
                 return false;
             });

             $(window).click(function(e) {
             console.log(e.target.id);
             if (e.target.id != "ctl00_NaviPlaceHolder_treeViewSearchInput" && e.target.id != "treeviewDropdownBtn")  
                document.getElementById("DivRemarktreeview").style.display = "none";
              });
         }

  

         function OnTreeClick(evt) {
             document.getElementById("ctl00_NaviPlaceHolder_treeViewSearchInput").value = "";
             document.getElementById("ctl00_NaviPlaceHolder_treeViewSearchInputID").value = ""; 
             var src = window.event != window.undefined ? window.event.srcElement : evt.target;
             var nodeClick = src.tagName.toLowerCase() == "a";
             if (nodeClick) {
                 var nodeText = src.innerText;
                 var nodeValue = GetNodeValue(src);
                 console.log("Text: " + nodeText + "," + "Value: " + nodeValue);
                 document.getElementById("ctl00_NaviPlaceHolder_treeViewSearchInput").value = nodeText;
                 document.getElementById("ctl00_NaviPlaceHolder_treeViewSearchInputID").value = nodeValue; 
                 
             }
             //return false; //uncomment this if you do not want postback on node click
         }
         function GetNodeValue(node) {
             var nodeValue = "";
             var nodePath = node.href.substring(node.href.indexOf(",") + 2, node.href.length - 2);
             var nodeValues = nodePath.split("\\");
             if (nodeValues.length > 1)
                 nodeValue = nodeValues[nodeValues.length - 1];
             else
                 nodeValue = nodeValues[0].substr(1);

             return nodeValue;
       }
 

 


         function HideDate() {
            //document.getElementById("ctl00_uoTblDate").style.display = "none";

             $("[id$=uoLabelLOE]").hover(function() {

                 var offset = $(this).offset();
                 var xPos = offset.left;
                 var yPos = offset.top;

                 var l0 = this.id.replace('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl', '');
                 var l1 = this.id.replace('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl', '');
                 var l2 = l1.replace('_uoLabelLOE', '');

                 $("#immigration").css({ top: yPos + 120, left: xPos + 240 });


                 var Status = this.innerText;
                 var Date = document.getElementById("ctl00_NaviPlaceHolder_uoListviewTravel_ctrl" + l2 + "_uoHiddenFieldLOEDate").value;
                 var Officer = document.getElementById("ctl00_NaviPlaceHolder_uoListviewTravel_ctrl" + l2 + "_uoHiddenFieldOfficer").value;
                 var Place = document.getElementById("ctl00_NaviPlaceHolder_uoListviewTravel_ctrl" + l2 + "_uoHiddenFieldPlace").value;
                 var LOEReason = document.getElementById("ctl00_NaviPlaceHolder_uoListviewTravel_ctrl" + l2 + "_uoHiddenFieldLOEReason").value;

                 var txt1 = "<fieldset><legend><b>Immagration Status</b></legend><table>";
                 var txt2 = "<tr><td>Status:</td><td>" + Status + "</td></tr>";
                 if (Status == 'Declined') {
                     txt2 = txt2 + "<tr><td>Reason:</td><td>" + LOEReason + "</td></tr>";
                 }
                 var txt3 = "<tr><td>Date:</td><td>" + Date + "</td></tr>";
                 var txt4 = "<tr><td>Officer:</td><td>" + Officer + "</td></tr>";
                 var txt5 = "<tr><td>Place:</td><td>" + Place + "</td></tr>";
                 var txt6 = "</table></fieldset >";
                 var immigration = txt1 + txt2 + txt3 + txt4 + txt5 + txt6;
                 $("#immigration").append(immigration);
                 $("#immigration").fadeIn();

             }).mouseleave(function() {
                 $("#immigration").hide(); ;
                 $("#immigration").empty();
             });
         }

         function DropDown() {
             var n = $('select#<%=uoDropDownListHotel.ClientID %> > option').length;
             var Hotel = document.getElementById('<%=uoDropDownListHotel.ClientID %>');
             if (n > 1) {
                 for (var i = 1; i < n ; i++) {
                     if (String(Hotel.options[i].text).substring(0, 2) == 'HT') {
                         Hotel.options[i].style.backgroundColor = '#00FF7F';
                     }
                     else {
                         Hotel.options[i].style.backgroundColor = '#7FFFD4';
                     }
                 }
             }
 
             var n = $('select#<%=uoDropDownListVehicleVendor.ClientID %> > option').length;
             var Hotel = document.getElementById('<%=uoDropDownListVehicleVendor.ClientID %>');
             if (n > 1) {
                 for (var i = 1; i < n; i++) {
                     if (String(Hotel.options[i].text).substring(0, 2) == 'VH') {
                         Hotel.options[i].style.backgroundColor = '#00FF7F';
                     }
                     else {
                         Hotel.options[i].style.backgroundColor = '#7FFFD4';
                     }
                 }
             }
         }
 
         function RemarkDiv() {

           $("#RemarkDiv").hide();
           $("#DivPortAgentHotel").hide();
           $("#DivPortAgentTrans").hide();
           $("#DivCrewAssistComment").hide();
           $("#HotelRemarkDiv").hide();
           $("#VehicleRemarkDiv").hide();

           $("#uoButtonHotelComment").mouseenter(function(event) {

               var vtop = event.pageY;
               var vleft = event.pageX;

               $("#HotelRemarkDiv").css({ top: vtop - 48, left: vleft + 185 });
               $("#HotelRemarkDiv").show();


           }).mouseleave(function() {
               $("#HotelRemarkDiv").hide();
           });



           $("#TranspoCost").mouseenter(function(event) {

           
           x = $("#TranspoCost").position();

               var vtop = x.top;
               var vleft = x.left - 420;

               document.getElementById("TranspoCost").style.position = "absolute";
 

               $("#TranspoCost").css({ top: vtop, left: vleft, width: 675 });
               $("#ctl00_NaviPlaceHolder_Panel4").css({ width: 675 });


           }).mouseleave(function() {
               document.getElementById("TranspoCost").style.position = "inherit";
               $("#TranspoCost").css({ width: 250 });
               $("#ctl00_NaviPlaceHolder_Panel4").css({ width: 250 });
           });

          
        


           $("#HotelRemarkDiv").mouseenter(function() {
                $("#HotelRemarkDiv").show();
           }).mouseleave(function() {
                $("#HotelRemarkDiv").hide();
           });


           $("#uoButtonVehicleComment").mouseenter(function(event) {

               var vtop = event.pageY;
               var vleft = event.pageX;

               $("#VehicleRemarkDiv").css({ top: vtop - 48, left: vleft + 185 });
               $("#VehicleRemarkDiv").show();


           }).mouseleave(function() {
                $("#VehicleRemarkDiv").hide();
           });


           $("#VehicleRemarkDiv").mouseenter(function() {
                $("#VehicleRemarkDiv").show();
           }).mouseleave(function() {
                $("#VehicleRemarkDiv").hide();
           });


        

           $(".Cancellation").fancybox({
               'width': '40%',
               'height': '45.5%',
               'autoScale': false,
               'transitionIn': 'fadeIn',
               'transitionOut': 'fadeOut',
               'type': 'iframe',
               'onClosed': function() {
                   var a = $("#<%=uoHiddenFieldCrewHotelCancelPopup.ClientID %>").val();
                   if (a == '1')
                       $("#aspnetForm").submit();
               }
           });


           $(".HotelConfirmation").fancybox({
               'width': '37%',
               'height': '32%',
               'autoScale': false,
               'transitionIn': 'fadeIn',
               'transitionOut': 'fadeOut',
               'type': 'iframe',
               'onStart': function() {
                   $("#<%=uoHiddenFieldCrewHotelCancelPopup.ClientID %>").val(0);
               },
               'onClosed': function() {
                   var a = $("#<%=uoHiddenFieldCrewHotelCancelPopup.ClientID %>").val();

                   if (a == '1') {
                      
                       $("#aspnetForm").submit();
                      
                   }
                   else {
                      
                       $("#aspnetForm").submit();
                   }
               }
           });

 
           $("#<%=uoTextBoxComment.ClientID %>").click(function(event) {

               var vtop = event.pageY;
               var vleft = event.pageX;
               $("#<%=uoHiddenFieldReqResourceFrom.ClientID %>").val("Hotel")
               $("#DivCrewAssistComment").css({ left: vleft + 200, top: vtop + 100 });
               $("#DivCrewAssistComment").fadeIn();
               clearRadioButtonList();

           }).blur(function(event) {
           
               var t = event.toString();
               if (t.replace('[', '').replace(']', '').replace('object', '').replace(' ', '') != 'Object') { 
                   if (($("#<%=uoHiddenFieldHSourceRequest.ClientID %>").val() == 0 || $("#<%=uoHiddenFieldHSourceRequest.ClientID %>").val() == '')
                        && t.replace('[', '').replace(']', '').replace('object', '').replace(' ', '') != 'FocusEvent') {
                       alert("Request source required... \nClick hotel comment textbox!");
                       var elementRef = document.getElementById('<%= uoRadioButtonListComment.ClientID %>');
                       var inputElementArray = elementRef.getElementsByTagName('input');
                       var inputElement = inputElementArray[0]
                       inputElement.focus();
                   }
               }
           });


           $("#<%=uoTextBoxTransComment.ClientID %>").click(function(event) {

               var vtop = event.pageY;
               var vleft = event.pageX;

               $("#<%=uoHiddenFieldReqResourceFrom.ClientID %>").val("Trans")
               $("#DivCrewAssistComment").css({ left: vleft + 200, top: vtop + 100 });
               $("#DivCrewAssistComment").fadeIn();

               clearRadioButtonList();

           }).blur(function(event) {
               var t = event.toString();
               if (t.replace('[', '').replace(']', '').replace('object', '').replace(' ', '') != 'Object') {
                   if (($("#<%=uoHiddenFieldTSourceRequest.ClientID %>").val() == 0 || $("#<%=uoHiddenFieldTSourceRequest.ClientID %>").val() == '')
                        && t.replace('[', '').replace(']', '').replace('object', '').replace(' ', '') != 'FocusEvent') {
                       alert("Request source required! \nClick transpo. comment textbox!!!");

                       var elementRef = document.getElementById('<%= uoRadioButtonListComment.ClientID %>');
                       var inputElementArray = elementRef.getElementsByTagName('input');
                       var inputElement = inputElementArray[0]
                       inputElement.focus();
                   }
               }
               
           });

           $("#<%=txtRemark.ClientID %>").click(function(event) {

               var vtop = event.pageY;
               var vleft = $("#<%=txtRemark.ClientID %>").offset().left; //event.pageX;


            

               $("#<%=uoHiddenFieldReqResourceFrom.ClientID %>").val("Remark");
               //               $("#DivCrewAssistComment").css({ left: vleft + 200, top: vtop + 100 });
               $("#DivCrewAssistComment").css({ left: vleft + 200, top: vtop + 100 });
               $("#DivCrewAssistComment").fadeIn();

               clearRadioButtonList();


           }).blur(function(event) {
               var t = event.toString();

               if (t.replace('[', '').replace(']', '').replace('object', '').replace(' ', '') != 'Object') {
                   if (t.replace('[', '').replace(']', '').replace('object', '').replace(' ', '') != 'FocusEvent') {
                       if ($("#<%=uoHiddenFieldRSourceRequest.ClientID %>").val() == 0 || $("#<%=uoHiddenFieldRSourceRequest.ClientID %>").val() == '') {

                           alert("Request source required... \nClick remark textbox!");

                           var elementRef = document.getElementById('<%= uoRadioButtonListComment.ClientID %>');
                           var inputElementArray = elementRef.getElementsByTagName('input');
                           var inputElement = inputElementArray[0]
                           inputElement.focus();
                       }
                   }
               }
           });

           function clearRadioButtonList() {
               
                var elementRef = document.getElementById('<%= uoRadioButtonListComment.ClientID %>');
                var inputElementArray = elementRef.getElementsByTagName('input');

                for (var i = 0; i < inputElementArray.length; i++) {
                    var inputElement = inputElementArray[i];
                    inputElement.checked = false;
                }
               
            }


           $("#DivCrewAssistComment").click(function() {
                $("#DivCrewAssistComment").fadeOut(); 
           });

           $("#RemarkDiv").mouseenter(function() {
               $("#RemarkDiv").show();
           }).mouseleave(function() {
               $("#RemarkDiv").hide();
           });

           $("#btnShowRemark2").mouseenter(function() {

               x = $("#<%=lblCrewInformation.ClientID %>").position();
               var vtop = x.top + 119.5;
               var vleft = x.left + 1002;

               $("#RemarkDiv").css({ top: vtop, left: vleft });
               $("#RemarkDiv").show();

           }).mouseleave(function() {
               $("#RemarkDiv").hide();
           });

           $("#RemarkDiv").mouseenter(function() {
               $("#RemarkDiv").show();
           }).mouseleave(function() {
               $("#RemarkDiv").hide();
           });

           $("#<%=uoRadioButtonListComment.ClientID%> input").change(function() {


               if ($("#<%=uoHiddenFieldReqResourceFrom.ClientID %>").val() == 'Hotel') {
                   $("#<%=uoHiddenFieldHSourceRequest.ClientID %>").val(this.value);
                   $("#<%=uoTextBoxComment.ClientID %>").focus();


               }
               else if ($("#<%=uoHiddenFieldReqResourceFrom.ClientID %>").val() == 'Trans') {
                   $("#<%=uoHiddenFieldTSourceRequest.ClientID %>").val(this.value);
                   $("#<%=uoTextBoxTransComment.ClientID %>").focus();
               }
               else if ($("#<%=uoHiddenFieldReqResourceFrom.ClientID %>").val() == 'Remark') {

                   $("#<%=uoHiddenFieldRSourceRequest.ClientID %>").val(this.value);
                   $("#<%=txtRemark.ClientID %>").focus();
                   var containerPos = $("#<%=txtRemark.ClientID %>").offset();

                   if (this.value == 1) {
                       document.getElementById('<%=lblRemarkType.ClientID %>').innerHTML = 'Email';
                   }
                   else if (this.value == 2) {
                       document.getElementById('<%=lblRemarkType.ClientID %>').innerHTML = 'Call';
                   }
                   else if (this.value == 3) {
                       document.getElementById('<%=lblRemarkType.ClientID %>').innerHTML = 'Live Chat';
                   }

                   var TransDateText = document.getElementById('<%=uoTextBoxRemTransdate.ClientID %>').value;
                   var TransTime = document.getElementById('<%=uoTextBoxRemTransTime.ClientID %>').value;

                   var mydate = new Date();
                   if (TransDateText == 'MM/dd/yyyy') {
                       var TransDateExtender = $find("CalendarExtenderTransdate");
                       TransDateExtender.set_selectedDate(mydate);
                   }

                   var TransTimeExtender = $find("WatermarkExtenderRemTransTime");

                   if (TransTime == 'hh:mm') {
                       var TransTimeExtender = $find("WatermarkExtenderRemTransTime");
                       
                       var myHours = mydate.getHours();
                       if (myHours < 10)
                           myHours = '0' + myHours;
                           
                       var myTime = mydate.getHours() + ':' + mydate.getMinutes();
                       if (myTime < 10)
                           myTime = '0' + myTime;
                       TransTimeExtender.set_Text(myTime);
                       
                   }

                   $("#DivRemarkType").css({ top: containerPos.top, left: containerPos.left, width: document.getElementById('TableRemark').offsetWidth - 28.5 });
                   $("#DivRemarkType").show();

               }
           });


           $("#btnRamarkTypeAdd").click(function() {

               if ($("#<%=txtSummaryCall.ClientID %>").val() == "") {
                   alert("Please enter summary of call for reporting purposes!!!");
               }
               else {

                   if ($("#<%=txtRemark.ClientID %>").val() == '') {
                       $("#<%=txtRemark.ClientID %>").val($("#<%=txtSummaryCall.ClientID %>").val());
                   }
//                   var length = document.getElementById('<%= txtRemark.ClientID %>').value.length; //$("#<%=txtRemark.ClientID %>").value.length;

//                   $("#<%=txtRemark.ClientID %>").setSelectionRange(length, length);

                   $("#<%=txtRemark.ClientID %>").focus();

                   $("#DivRemarkType").hide();
               }

           });
           $("#btnRemarkTypeClose").click(function() {

               $("#<%=txtRemark.ClientID %>").focus();
               
//               var length = $("#<%=txtRemark.ClientID %>").value.length;
//               
//               $("#<%=txtRemark.ClientID %>").setSelectionRange(length, length);
//               $("#<%=txtRemark.ClientID %>").focus();
               
               $("#DivRemarkType").hide();
             
           });

            $("#<%=uoCheckBoxPAHotel.ClientID %>").click(function() {

                $("#DivPortAgentTrans").hide();
                
            }).mouseenter(function() {
     
                $("#DivPortAgentTrans").hide();
                
            }).mouseleave(function() {
                $("#DivPortAgentTrans").hide();
            });

            $("#<%=uoCheckBoxPATrans.ClientID %>").click(function() {

                $("#DivPortAgentHotel").hide();

            }).mouseenter(function() {

                $("#DivPortAgentHotel").hide();
            }).mouseleave(function() {
                $("#DivPortAgentHotel").hide();
            });

            $("#btnHidePortAgentTrans").click(function() {
                $("#DivPortAgentTrans").hide();
            });

            $("#btnHidePortAgentHotel").click(function() {
                $("#DivPortAgentHotel").hide();
            });

        }

        function GetSetDate(val) {
            var getDate = val.split('/')
          
            var SetDate = new Date();
            SetDate.setFullYear(getDate[2], (getDate[0] - 1), getDate[1]);

            return SetDate;
        
        }

        function CheckHotelContractDate() {

            var x = GetSetDate(document.getElementById('<%= uoHiddenFieldContractStart.ClientID %>').value);
            var y = GetSetDate(document.getElementById('<%= uoHiddenFieldContractEnd.ClientID %>').value);
            
            var a = GetSetDate(document.getElementById('<%= uoTextBoxCheckinDate.ClientID %>').value);
            var b = GetSetDate(document.getElementById('<%= uoTextBoxCheckoutDate.ClientID %>').value);

            if (a == 'Invalid Date' && b == 'Invalid Date') {
                alert('Invalid checkin and checkout date!!! ');
                return false;
            }
            else if (b == 'Invalid Date') {
                alert('Invalid checkout date!!! ');
                return false;
            }
            else if (a == 'Invalid Date') {
                alert('Invalid checkin date!!! ');
                return false;
            }
            else if (y == 'Invalid Date') {
                alert('Contract not valid for requested date!!!');
                return false;
            }
            else if (x > a || b > y || y == 'Invalid Date') {
                alert('Contract not valid for requested date!!!\nvalid date between ' + document.getElementById('<%= uoHiddenFieldContractStart.ClientID %>').value + ' and ' + document.getElementById('<%= uoHiddenFieldContractEnd.ClientID %>').value);
                return false;
            } 
           
        }

        function ConfirmButtonFinish() {
            if (document.getElementById('<%= uoHiddenFieldSaveHotel.ClientID %>').value == "1") {
                if (CheckHotelContractDate() == false) {
                    return false;
                }
                if (document.getElementById('<%= uoTextBoxComment.ClientID %>').value == "") {
                    alert("Hotel: Comment Required \n and Request source...");
                    return false;
                }
                else if (document.getElementById('<%= uoTextBoxEmail.ClientID %>').value == '0' ) {
                    alert("Hotel email required!");
                    return false;
                }
                else if (document.getElementById('<%= uoHiddenFieldHSourceRequest.ClientID %>').value == '0' || document.getElementById('<%= uoHiddenFieldHSourceRequest.ClientID %>').value == '') {
                    alert("Hotel: Request source Required!!! \nClick Hotel. comment to select the request source...");
                    return false;
                }
                var Hotel = document.getElementById('<%=uoDropDownListHotel.ClientID %>');
                if (String(Hotel.options[Hotel.selectedIndex].text).substring(0, 2) == 'PA') {
                    if (document.getElementById('<%= uoTextBoxPortAgentConfirm.ClientID %>').value == "") {
                        alert("Confirm by required...");
                        return false;
                    }
                }
                else { 
                    document.getElementById("<%= uoHyperLinkHotelConfirmation.ClientID %>").href = "/CrewAssist/CrewAssistConfirmation.aspx?CRate=" + document.getElementById('<%= uoTextBoxComfirmRate.ClientID %>').value
                            + "&SfID=" + document.getElementById('<%= uoTextBoxEmployeeID.ClientID %>').value
                            + "&cDte=" + document.getElementById('<%= uoTextBoxCheckinDate.ClientID %>').value
                            + "&EDte=" + document.getElementById('<%= uoTextBoxCheckoutDate.ClientID %>').value 
                            + "&IDbint=" + document.getElementById('<%= uoHiddenFieldIDBigint.ClientID %>').value 
                            + "&sNo=" + document.getElementById('<%= uoHiddenFieldSeqNo.ClientID %>').value 
                            + "&trReqID=" + document.getElementById('<%= uoHiddenFieldTravelRequestID.ClientID %>').value
                            + "&bID=" + Hotel[Hotel.selectedIndex].value
                            + "&pCode=0"
                            + "&TrHID=" + document.getElementById('<%= uoHiddenFieldTransHotelOtherID.ClientID %>').value;
                    
                    document.getElementById("<%= uoHyperLinkHotelConfirmation.ClientID %>").setAttribute('class', 'HotelConfirmation');
                    document.getElementById("<%= uoHyperLinkHotelConfirmation.ClientID %>").click();
                    return false;
                
                }
            }

            if (document.getElementById('<%= uoHiddenFieldSaveTrans.ClientID %>').value == "1") {
                if (document.getElementById('<%= uoTextBoxTranpComfirmby.ClientID %>').value == "") {
                    alert("Required Transportation! \n Confirm By");
                    return false;
                }
                else if (document.getElementById('<%= uoTextBoxEmailTrans.ClientID %>').value == '0' ) {
                 
                 
                    alert("Transportation email required!");
                    return false;
                }
                else if (document.getElementById('<%= uoTextBoxTransComment.ClientID %>').value == "") {
                    alert("Transportation: Comment Required \n and Request source...");
                    return false;
                }
                
                else if (document.getElementById('<%= uoHiddenFieldTSourceRequest.ClientID %>').value == '0' || document.getElementById('<%= uoHiddenFieldTSourceRequest.ClientID %>').value == '') {
                    alert("Transportation: Request source Required!!! \nClick transpo. comment to select the request source...");
                    return;
                }
                

            }


            return true;
        }

        function EditRemark(obj) {
            var name1 = obj.id.replace("ctl00_NaviPlaceHolder_uoListViewRemarkPopup_ctrl", "");
            var counter = name1.replace("_uoLinkButtonEditRemark", "");
            $("#<%=uoHiddenFieldRemarkID.ClientID %>").val(0);
            $("#<%=txtRemark.ClientID %>").val("");
            var TypeID = 0;
            var StatusID = 0;
            var ReqID = 0;
            var SumCall = "";
            var TDate = "";
            var TTIme = "";
            for (var j = 0; j < $("[id$=uoLabelRemark]").length; j++) {
                if (counter == j) {
                    
                    $("#<%=uoHiddenFieldRemarkID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemarkPopup_ctrl' + j + '_uoHiddenFieldGridRemarkID').value);
                    document.getElementById('ctl00_NaviPlaceHolder_txtRemark').value = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemarkPopup_ctrl' + j + '_uoLabelRemark').innerHTML;
                    $("#<%=uoHiddenFieldTravelRequestID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemarkPopup_ctrl' + j + '_uoHiddenFieldRemarkTRID').value);
                    $("#<%=uoHiddenFieldRSourceRequest.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_uoHiddenFieldResourceID').value);

                    TypeID = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemarkPopup_ctrl' + j + '_uoHiddenFieldTypeID').value;
                    StatusID = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemarkPopup_ctrl' + j + '_upHiddenFieldStatusID').value;
                    SumCall = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemarkPopup_ctrl' + j + '_uoHiddenFieldSumCall').value;
                    ReqID = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemarkPopup_ctrl' + j + '_uoHiddenFieldRequestorID').value;

                    TDate = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemarkPopup_ctrl' + j + '_uoHiddenFieldTransDate').value;
                    TTIme = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemarkPopup_ctrl' + j + '_uoHiddenFieldTransTime').value;
                                                
                }
            }
          
            var rType = document.getElementById('ctl00_NaviPlaceHolder_cboRemarkType');
            var rStatus = document.getElementById('ctl00_NaviPlaceHolder_cboRemarkStatus');
            document.getElementById('ctl00_NaviPlaceHolder_txtSummaryCall').value = SumCall;
            var rReq = document.getElementById('ctl00_NaviPlaceHolder_cboRemarkRequestor');


            document.getElementById('ctl00_NaviPlaceHolder_uoTextBoxRemTransdate').value = TDate;
            document.getElementById('ctl00_NaviPlaceHolder_uoTextBoxRemTransTime').value = TTIme;
         
            var curdate = new Date(TDate);
            var TransDateExtender = $find("CalendarExtenderTransdate");
            TransDateExtender.set_selectedDate(curdate);
            
            var TransTimeExtender = $find("WatermarkExtenderRemTransTime");
            TransTimeExtender.set_Text(TTIme);
            
            GetSelectedValue(rType, TypeID);
            GetSelectedValue(rStatus, StatusID);
            GetSelectedValue(rReq, ReqID);
            $("#RemarkDiv").hide();
        }


        function EditRemarkmain(obj) {
            var name1 = obj.id.replace("ctl00_NaviPlaceHolder_uoListViewRemark_ctrl", "");
            var counter = name1.replace("_uoLinkButtonEditRemark", "");
            $("#<%=uoHiddenFieldRemarkID.ClientID %>").val(0);
            $("#<%=txtRemark.ClientID %>").val("");
            var TypeID = 0;
            var StatusID = 0;
            var SumCall = "";
            var ReqID = 0;
            var TDate = "";
            var TTIme = "";
            var IR = 0;
            
            for (var j = 0; j < $("[id$=lblRemark]").length; j++) {
                if (counter == j) {

                    $("#<%=uoHiddenFieldRemarkID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_uoHiddenFieldGridRemarkID').value);
                    document.getElementById('ctl00_NaviPlaceHolder_txtRemark').value = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_lblRemark').innerHTML;
                    $("#<%=uoHiddenFieldTravelRequestID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_uoHiddenFieldRemarkTRID').value);
                    $("#<%=uoHiddenFieldRSourceRequest.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_uoHiddenFieldResourceID').value);

                    TypeID = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_uoHiddenFieldTypeID').value;
                    StatusID = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_upHiddenFieldStatusID').value;
                    SumCall = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_uoHiddenFieldSumCall').value;
                    ReqID = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_uoHiddenFieldRequestorID').value;

                    TDate = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_uoHiddenFieldTransDate').value;
                    TTIme = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_uoHiddenFieldTransTime').value;
                    IR = document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_uoCheckBoxIR').checked;

                }
            }
             
            var rType = document.getElementById('ctl00_NaviPlaceHolder_cboRemarkType');
            var rStatus = document.getElementById('ctl00_NaviPlaceHolder_cboRemarkStatus');
            document.getElementById('ctl00_NaviPlaceHolder_txtSummaryCall').value = SumCall;
            var rReq = document.getElementById('ctl00_NaviPlaceHolder_cboRemarkRequestor');

            document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxRemTransTime").value = TTIme;
            document.getElementById('ctl00_NaviPlaceHolder_uoTextBoxRemTransdate').value = TDate;
            document.getElementById('ctl00_NaviPlaceHolder_uoCheckBoxIR').checked = IR;
            
            var curdate = new Date(TDate);
            var TransDateExtender = $find("CalendarExtenderTransdate");
            TransDateExtender.set_selectedDate(curdate);
            var TransTimeExtender = $find("WatermarkExtenderRemTransTime");
            TransTimeExtender.set_Text(TTIme);
            
            GetSelectedValue(rType, TypeID);
            GetSelectedValue(rStatus, StatusID);
            GetSelectedValue(rReq, ReqID);
            
            $("#RemarkDiv").hide();
        }
 
        function GetSelectedValue(selectObj, valueToSet) {
            for (var i = 0; i < selectObj.options.length; i++) {
                if (selectObj.options[i].value == valueToSet) {
                    selectObj.options[i].selected = true;
                    return;
                }
            }
        } 
        
        function DeleteRemarkmain(obj) {

            var name1 = obj.id.replace("ctl00_NaviPlaceHolder_uoListViewRemark_ctrl", "");
            var counter = name1.replace("_uoLinkButtonDeleteRemark", "");

            $("#<%=uoHiddenFieldRemarkID.ClientID %>").val(0);
            $("#<%=txtRemark.ClientID %>").val("");

            for (var j = 0; j < $("[id$=lblRemark]").length; j++) {

                if (counter == j) {
                    $("#<%=uoHiddenFieldRemarkID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_uoHiddenFieldGridRemarkID').value);
                    $("#<%=uoHiddenFieldTravelRequestID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemark_ctrl' + j + '_uoHiddenFieldRemarkTRID').value);
                }
            }
            $("#RemarkDiv").hide();
        }
        
        function DeleteRemark(obj) {
        
            var name1 = obj.id.replace("ctl00_NaviPlaceHolder_uoListViewRemarkPopup_ctrl", "");
            var counter = name1.replace("_uoLinkButtonDeleteRemark", "");

            var Cost = 0.00;

            $("#<%=uoHiddenFieldRemarkID.ClientID %>").val(0);
            $("#<%=txtRemark.ClientID %>").val("");

            for (var j = 0; j < $("[id$=uoLabelRemark]").length; j++) {
                if (counter == j) {
                    $("#<%=uoHiddenFieldRemarkID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemarkPopup_ctrl' + j + '_uoHiddenFieldGridRemarkID').value);
                    $("#<%=uoHiddenFieldTravelRequestID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewRemarkPopup_ctrl' + j + '_uoHiddenFieldRemarkTRID').value);
                }
            }
            $("#RemarkDiv").hide();
            
        }
        
        function GetVehicleType(obj) {
            var name1 = obj.id.replace("ctl00_NaviPlaceHolder_uoListViewTranspoCost_ctrl", "");
            var counter = name1.replace("_uoCheckBoxVCost", "");
            $("#<%=uoHiddenFieldVTypeID.ClientID %>").val(0);
            for (var j = 0; j < $("[id$=uoCheckBoxVCost]").length; j++) {
                if (counter == j) {
                    $("#<%=uoHiddenFieldVTypeID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewTranspoCost_ctrl' + j + '_uoHiddenFieldVehicleTypeID').value);
                    Cost = document.getElementById('ctl00_NaviPlaceHolder_uoListViewTranspoCost_ctrl' + j + '_uoLabelCost').innerHTML;
                }
                else {
                   document.getElementById('ctl00_NaviPlaceHolder_uoListViewTranspoCost_ctrl' + j + '_uoCheckBoxVCost').checked = false;
                }
            }
            
            if (obj.checked == true) {
                document.getElementById('ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl' + $("[id$=uoLabelOrigin]").length + '_uoTextBoxCost').value = Cost;
            }
            else {
                document.getElementById('ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl' + $("[id$=uoLabelOrigin]").length + '_uoTextBoxCost').value = "0.0";
            }
            
        } 


        function CheckTravelRequest(obj) {
                   
             var myName = obj.id
             var name1 = obj.id.replace("ctl00_NaviPlaceHolder_uoListviewTravel_ctrl", "");
             var counter = name1.replace("_uoSelectCheckBoxs", "");
             var Portname = "";
             var status= "";
             var TravelDate = "";

             var StartDate = "";
             var EndDate = "";

             $("#<%=uoHiddenFieldSeqNo.ClientID %>").val(0);
             $("#<%=uoHiddenFieldIDBigint.ClientID %>").val(0);
             $("#<%=uoHiddenFieldAirlineCode.ClientID %>").val("");

             $("#<%=uoHiddenFieldPortAgentID.ClientID %>").val(0);
             $("#<%=uoHiddenFieldHotelRequestID.ClientID %>").val(0);
             $("#<%=uoHiddenFieldTravelRequestID.ClientID %>").val(0);
             $("#<%=uoHiddenFieldTransVendorID.ClientID %>").val(0);
             $("#<%=uoHiddenFieldMeetAndGreetID.ClientID %>").val(0);
             $("#<%=uoHiddenFieldSafeguardRequestID.ClientID %>").val(0);
             $("#<%=uoHiddenFieldArrCode.ClientID %>").val("");

             $("#<%=uoHiddenFieldRecordLocator.ClientID %>").val("");
             
             var myDate = new Date();
             for (var j = 0; j < $("[id$=uoSelectCheckBoxs]").length; j++) {
                 if (counter != j) {
                 
                     document.getElementById('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl' + j + '_uoSelectCheckBoxs').checked = false;
                     
                 }
                 else {
                     Portname = document.getElementById('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl' + j + '_lblPort').innerHTML;
                     TravelDate = document.getElementById('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl' + j + '_lblRequestDate').innerHTML;
                     status = document.getElementById('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl' + j + '_lblStatus').innerHTML;
                     $("#<%=uoHiddenFieldTravelRequestID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl' + j + '_hfTravelRequestID').value);
                     $("#<%=uoHiddenFieldHotelRequestID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl' + j + '_hfCrewAssistRequestID').value);

                     $("#<%=uoHiddenFieldTransVendorID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl' + j + '_uoHiddenFieldTRTranspoReqID').value);
                     $("#<%=uoHiddenFieldMeetAndGreetID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl' + j + '_uoHiddenFieldTRMAGReqID').value);
                     $("#<%=uoHiddenFieldPortAgentID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl' + j + '_uoHiddenFieldTRPortAgentID').value);
                     $("#<%=uoHiddenFieldIDBigint.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl' + j + '_uoHiddenFieldTRIDBint').value);

                     $("#<%=uoHiddenFieldSafeguardRequestID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl' + j + '_uoHiddenFieldTRReqSafeguardID').value);

                     $("#<%=uoHiddenFieldRecordLocator.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl' + j + '_lblRecordLocator').innerHTML);
                 }
                 
             }

             myDate = TravelDate;
             
             $("#<%=uoHiddenFieldAirStatus.ClientID %>").val(status)
             $("#<%=uoHiddenFieldPortCode.ClientID %>").val(Portname);
             $("#<%=uoHiddenFieldRequestDate.ClientID %>").val(TravelDate);
                 
             $("#<%=uoHiddenFieldSaveType.ClientID %>").val("false");             
             $("#<%=uoHiddenFieldSeqNo.ClientID %>").val(0);

             $("#<%=uoHiddenFieldSaveHotel.ClientID %>").val("0");
             $("#<%=uoHiddenFieldSaveTrans.ClientID %>").val("0");

             document.getElementById("ctl00_NaviPlaceHolder_uoButtonLoadAir").click();
             
             if (obj.checked == false) {
                 $("#<%=btnClearControl.ClientID %>").click();
             }
         }

         function CheckTravelRequestAir(obj) {
           
             var myName = obj.id
             var name1 = obj.id.replace("ctl00_NaviPlaceHolder_uoListviewAir_ctrl", "");
             var counter = name1.replace("_uoSelectAirCheckBoxs", "");
             var Portname = "";
             var TravelDate = "";

             var Origin = "";
             var Destination = "";

             $("#<%=uoHiddenFieldSeqNo.ClientID %>").val(0);
             $("#<%=uoHiddenFieldIDBigint.ClientID %>").val(0);

             $("#<%=uoHiddenFieldPortAgentID.ClientID %>").val(0);
             $("#<%=uoHiddenFieldHotelRequestID.ClientID %>").val(0);
             $("#<%=uoHiddenFieldTransVendorID.ClientID %>").val(0);
             $("#<%=uoHiddenFieldMeetAndGreetID.ClientID %>").val(0);

          
             $("#<%=uoHiddenFieldDeptCode.ClientID %>").val("");
             $("#<%=uoHiddenFieldArrCode.ClientID %>").val("");
           
             
             for (var j = 0; j < $("[id$=uoSelectAirCheckBoxs]").length; j++) {
                 if (counter != j) {
                     document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_uoSelectAirCheckBoxs').checked = false;
                 }
                 else {
                 
                     Portname = document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_lblDepartureAirportLocationCode').innerHTML;
                     TravelDate = document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_lblArrivalDateTimeDate').innerHTML;
                     Origin = document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_lblArrivalAirportLocationCode').innerHTML;

                     $("#<%=uoHiddenFieldSeqNo.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_lblSeqNo').innerHTML);
                     $("#<%=uoHiddenFieldIDBigint.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_hfIDBigInt').value);
                     $("#<%=uoHiddenFieldHotelRequestID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_hfAirRequestID').value);
                     $("#<%=uoHiddenFieldTransVendorID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_uoHiddenFieldAirTranspoReqID').value);
                     $("#<%=uoHiddenFieldMeetAndGreetID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_uoHiddenFieldAirMAGReqID').value);
                     $("#<%=uoHiddenFieldFligthNo.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_uoHiddenFieldAirFligthNo').value);
                     $("#<%=uoHiddenFieldPortAgentID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_uoHiddenFieldAirPortAgentID').value);

                     $("#<%=uoHiddenFieldDepartureTime.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_lblDeptTime').innerHTML);
                     $("#<%=uoHiddenFieldArrivalTime.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_lblArrTime').innerHTML);
                     $("#<%=uoHiddenFieldDeptCode.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_lblDepartureAirportLocationCode').innerHTML);
                     $("#<%=uoHiddenFieldAirlineCode.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_lblAirline').innerHTML);

                 }
             }
             $("#<%=uoHiddenFieldPortCode.ClientID %>").val(Portname);
             $('input:checkbox[name *= uoSelectCheckBoxs]').attr('checked', false);
           
             $("#<%=uoHiddenFieldArrCode.ClientID %>").val(Origin);
             $("#<%=uoHiddenFieldRequestDate.ClientID %>").val(TravelDate);
             $("#<%=uoHiddenFieldSaveType.ClientID %>").val("true");
             $("#<%=uoButtonChangePort.ClientID %>").click();


             $("#<%=uoHiddenFieldSaveHotel.ClientID %>").val("0");
             $("#<%=uoHiddenFieldSaveTrans.ClientID %>").val("0");
             
             if (obj.checked == false){ 
                $("#<%=btnClearControl.ClientID %>").click();
            }
         }


         function SelectHotelTransaction(val) {


             $("#<%=uoHiddenFieldSeqNo.ClientID %>").val(0);
             $("#<%=uoHiddenFieldIDBigint.ClientID %>").val(0);

             $("#<%=uoHiddenFieldPortAgentID.ClientID %>").val(0);
             $("#<%=uoHiddenFieldHotelRequestID.ClientID %>").val(0);
             $("#<%=uoHiddenFieldTransVendorID.ClientID %>").val(0);
             $("#<%=uoHiddenFieldMeetAndGreetID.ClientID %>").val(0); 


             for (var j = 0; j < $("[id$=uoCheckBoxsHotelTransaction]").length; j++) {
                 if (val.id != 'ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + j + '_uoCheckBoxsHotelTransaction') {
                     document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + j + '_uoCheckBoxsHotelTransaction').checked = false;
                 }
                 else {

                     $("#<%=uoHiddenFieldSeqNo.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + j + '_uoHiddenFieldSeqNo').value);
                     $("#<%=uoHiddenFieldIDBigint.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + j + '_uoHiddenFieldHIDBigint').value);
                     $("#<%=uoHiddenFieldHotelRequestID.ClientID %>").val(document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + j + '_uoHiddenFieldHotelID').value);

                 }
             }
 
             val.AutoPostBack = val.checked;
             return val.checked;
             
         }

 
         function SetTMResolution() {
             var ht = $(window).height();
             var ht2 = $(window).height();

             var wd = $(window).width() * 0.3;

             if (screen.height <= 600) {
                 ht = ht * 0.22;
                 wd = $(window).width();
             }
             else if (screen.height <= 720) {
                 ht = ht * 0.28;
             }
             else {
                 ht = ht * 0.2;
             }
         }


         function getTextBoxID(obj) {

             var h = document.getElementById('ctl00_NaviPlaceHolder_HiddenFieldSearch');
             h.value = obj;

         }

         function FinishTransaction() {
             var seqNo = 0;

             for (var j = 0; j < $("[id$=uoSelectAirCheckBoxs]").length; j++) {
                 seqNo = document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_lblSeqNo').innerHTML;
                 if ($("#<%=uoHiddenFieldSeqNo.ClientID %>").val() == seqNo) {
                     document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_lblDepartureAirportLocationCode').innerHTML = $("#<%=uoHiddenFieldHotelRequestID.ClientID %>").val();
                     document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl' + j + '_hfAirRequestID').value = $("#<%=uoHiddenFieldHotelRequestID.ClientID %>").value;
                 }
             }
         }

         function HiveDiv() {
             $("#<%=HiddenFieldHideCenter.ClientID %>").val(0);
         }

         function KeyUpText() {
             $("#<%=uoTextBoxDuration.ClientID %>").keyup(function() {

                 var today1 = new Date($("#<%=uoTextBoxCheckinDate.ClientID %>").val());
                 if (today1 == "Invalid Date") {
                     var today = new Date();
                     var dd1 = today.getDate();
                     var mm1 = today.getMonth() + 1; //January is 0!

                     var yyyy1 = today.getFullYear();

                     if (dd1 < 10) {
                         dd1 = '0' + dd1;
                     }
                     if (mm1 < 10) {
                         mm1 = '0' + mm1;
                     }
                     $("#<%=uoTextBoxCheckinDate.ClientID %>").val(mm1 + '/' + dd1 + '/' + yyyy1);
                 }

                 var date1 = $("#<%=uoTextBoxCheckinDate.ClientID %>").val();
                 var newdate = new Date(date1);
                 var day1 = parseInt(document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxDuration").value);

                 var mydate = newdate.setDate(newdate.getDate() + day1);
                 var mynewdate = new Date(mydate)

                 var dd = mynewdate.getDate();
                 var mm = mynewdate.getMonth() + 1; //January is 0!
                 var yyyy = mynewdate.getFullYear();

                 if (dd < 10) {
                     dd = '0' + dd;
                 }
                 if (mm < 10) {
                     mm = '0' + mm;
                 }
                 if (document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxDuration").value != "") {
                     $("#<%=uoTextBoxCheckoutDate.ClientID %>").val(mm + '/' + dd + '/' + yyyy);
                 }

             });
         }

         function checkboxEnable(obj) {

             document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldCheckBox").value = obj.id;
             if (obj.id == "ctl00_NaviPlaceHolder_uoCheckBoxCopyAll") {
             
                 document.getElementById("ctl00_NaviPlaceHolder_CheckBoxScheduler").checked = obj.checked;
                 document.getElementById("ctl00_NaviPlaceHolder_CheckBoxCopyShip").checked = obj.checked;
                 document.getElementById("ctl00_NaviPlaceHolder_CheckBoxCopycrewassist").checked = obj.checked;
                 document.getElementById("ctl00_NaviPlaceHolder_CheckBoxCopycrewhotels").checked = obj.checked;

             }
             else if (obj.id == "ctl00_NaviPlaceHolder_CheckBoxEmail") {
                if (obj.checked == true) {
                     document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxEmail").disabled = false;
                 }
                 else {
                     document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxEmail").disabled = true;
                 }
                 SaveHotel(obj.id);
             }

             else if (obj.id == "ctl00_NaviPlaceHolder_uoCheckBoxEmailTrans") {
                 if (obj.checked == true) {
                     document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxEmailTrans").disabled = false;
                 }
                 else {
                     document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxEmailTrans").disabled = true;
                 }
                 SaveTransportation(obj.id);
             }



             else if (obj.id == "ctl00_NaviPlaceHolder_uoCheckBoxMAGEmail") {
                 if (obj.checked == true) {
                     document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxMAGEmail").disabled = false;
                 }
                 else {
                     document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxMAGEmail").disabled = true;
                 }
             }

             else if (obj.id == "ctl00_NaviPlaceHolder_uoCheckBoxPAEmail") {
                 if (obj.checked == true) {
                     document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxPAEmail").disabled = false;
                 }
                 else {
                     document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxPAEmail").disabled = true;
                 }
                 SavePortAgent(obj.id);
             }


             else if (obj.id == "ctl00_NaviPlaceHolder_uoCheckBoxSafeguardEmail") {
                 if (obj.checked == true) {
                     document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxSafeguardEmail").disabled = false;
                 }
                 else {
                     document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxSafeguardEmail").disabled = true;
                 }
             }
             
         }

         function HighLightTab() {
             
             var val = document.getElementById("ctl00_NaviPlaceHolder_uoDropDownListExpenseType").value;
             document.getElementById("ctl00_NaviPlaceHolder_uoButtonFinish").style.display = "block";
 

             if (val == 1) {


                 document.getElementById('navHotel').style.background = '#0288D8';
                 document.getElementById('navHotel').style.bordercolor = '#0288D8';
                 document.getElementById('navHotel').style.color = '#ffffff';

                 document.getElementById('navPortAgent').style.background = '#ECDFC4';
                 document.getElementById('navPortAgent').style.bordercolor = '#0288D8';
                 document.getElementById('navPortAgent').style.color = '#0288D8';

                 document.getElementById('navTransportation').style.background = '#ECDFC4';
                 document.getElementById('navTransportation').style.bordercolor = '#0288D8';
                 document.getElementById('navTransportation').style.color = '#0288D8';

                 document.getElementById('navMeetAndGreet').style.background = '#ECDFC4';
                 document.getElementById('navMeetAndGreet').style.bordercolor = '#0288D8';
                 document.getElementById('navMeetAndGreet').style.color = '#0288D8';



                 document.getElementById("TabHotelTable").style.display = "block";
                 document.getElementById("TabTransportation").style.display = "none";
                 document.getElementById("TabPortagent").style.display = "none";

                 document.getElementById("TabMeetAndGreet").style.display = "none";
                 document.getElementById("TabVisa").style.display = "none";
                 document.getElementById("TabSafeguard").style.display = "none";
                
//                 uoHiddenFieldHotelRequestID.Value
             }
             else if (val == 2) {

                 document.getElementById("TabHotelTable").style.display = "none";
                 document.getElementById("TabTransportation").style.display = "block";
                 document.getElementById("TabPortagent").style.display = "none";

                 document.getElementById("TabMeetAndGreet").style.display = "none";
                 document.getElementById("TabVisa").style.display = "none";
                 document.getElementById("TabSafeguard").style.display = "none";

                 document.getElementById('navTransportation').style.background = '#0288D8';
                 document.getElementById('navTransportation').style.bordercolor = '#0288D8';
                 document.getElementById('navTransportation').style.color = '#ffffff';

                 document.getElementById('navHotel').style.background = '#ECDFC4';
                 document.getElementById('navHotel').style.bordercolor = '#0288D8';
                 document.getElementById('navHotel').style.color = '#0288D8';

                 document.getElementById('navPortAgent').style.background = '#ECDFC4';
                 document.getElementById('navPortAgent').style.bordercolor = '#0288D8';
                 document.getElementById('navPortAgent').style.color = '#0288D8';

                 document.getElementById('navMeetAndGreet').style.background = '#ECDFC4';
                 document.getElementById('navMeetAndGreet').style.bordercolor = '#0288D8';
                 document.getElementById('navMeetAndGreet').style.color = '#0288D8';





             }
             else if (val == 3) {

                 document.getElementById("TabHotelTable").style.display = "none";
                 document.getElementById("TabTransportation").style.display = "none";
                 document.getElementById("TabPortagent").style.display = "block";

                 document.getElementById("TabMeetAndGreet").style.display = "none";
                 document.getElementById("TabVisa").style.display = "none";
                 document.getElementById("TabSafeguard").style.display = "none";


                 document.getElementById('navPortAgent').style.background = '#0288D8';
                 document.getElementById('navPortAgent').style.bordercolor = '#0288D8';
                 document.getElementById('navPortAgent').style.color = '#ffffff';

                 document.getElementById('navHotel').style.background = '#ECDFC4';
                 document.getElementById('navHotel').style.bordercolor = '#0288D8';
                 document.getElementById('navHotel').style.color = '#0288D8';

                 document.getElementById('navTransportation').style.background = '#ECDFC4';
                 document.getElementById('navTransportation').style.bordercolor = '#0288D8';
                 document.getElementById('navTransportation').style.color = '#0288D8';

                 document.getElementById('navMeetAndGreet').style.background = '#ECDFC4';
                 document.getElementById('navMeetAndGreet').style.bordercolor = '#0288D8';
                 document.getElementById('navMeetAndGreet').style.color = '#0288D8';


               
             }
             else if (val == 4) {

                 document.getElementById("TabHotelTable").style.display = "none";
                 document.getElementById("TabTransportation").style.display = "none";
                 document.getElementById("TabPortagent").style.display = "none";


                 document.getElementById("TabMeetAndGreet").style.display = "block";
                 document.getElementById("TabVisa").style.display = "none";
                 document.getElementById("TabSafeguard").style.display = "none";

                 document.getElementById('navMeetAndGreet').style.background = '#0288D8';
                 document.getElementById('navMeetAndGreet').style.bordercolor = '#0288D8';
                 document.getElementById('navMeetAndGreet').style.color = '#ffffff';

                 document.getElementById('navHotel').style.bordercolor = '#ECDFC4';
                 document.getElementById('navHotel').style.color = '#0288D8';
                 document.getElementById('navHotel').style.color = '#0288D8';

                 document.getElementById('navPortAgent').style.background = '#ECDFC4';
                 document.getElementById('navPortAgent').style.bordercolor = '#0288D8';
                 document.getElementById('navPortAgent').style.color = '#0288D8';

                 document.getElementById('navTransportation').style.bordercolor = '#ECDFC4';
                 document.getElementById('navTransportation').style.color = '#0288D8';
                 document.getElementById('navTransportation').style.color = '#0288D8';

//                 document.getElementById("ctl00_NaviPlaceHolder_uoButtonFinish").style.display = "none";
                 
             }
             else if (val == 5) {

                 document.getElementById("TabHotelTable").style.display = "none";
                 document.getElementById("TabTransportation").style.display = "none";
                 document.getElementById("TabPortagent").style.display = "block";
                 
                  document.getElementById("TabSafeguard").style.display = "none";
                 document.getElementById("TabMeetAndGreet").style.display = "none";
                 document.getElementById("TabVisa").style.display = "none";
                 
                

             }
             else if (val == 6) {

                 document.getElementById("TabHotelTable").style.display = "none";
                 document.getElementById("TabTransportation").style.display = "none";
                 document.getElementById("TabPortagent").style.display = "none";
                 
                 document.getElementById("TabMeetAndGreet").style.display = "none";
                 document.getElementById("TabVisa").style.display = "none";
                 document.getElementById("TabSafeguard").style.display = "block";

             }
         }


         function hexToR(h) { return parseInt((cutHex(h)).substring(0, 2), 16) }
         function hexToG(h) { return parseInt((cutHex(h)).substring(2, 4), 16) }
         function hexToB(h) { return parseInt((cutHex(h)).substring(4, 6), 16) }
         function cutHex(h) { return (h.charAt(0) == "#") ? h.substring(1, 7) : h }

         function HighLightTabHeader(val) {
 

             document.getElementById('ctl00_NaviPlaceHolder_uoDropDownListExpenseType').value = val;
             document.getElementById("ctl00_NaviPlaceHolder_uoButtonFinish").style.display = "block";
             
             if (val == 1) {


                 document.getElementById('navHotel').style.background = '#0288D8';
                 document.getElementById('navHotel').style.bordercolor = '#0288D8';
                 document.getElementById('navHotel').style.color = '#ffffff';

                 document.getElementById('navTransportation').style.background = '#ECDFC4';
                 document.getElementById('navTransportation').style.bordercolor = '#0288D8';
                 document.getElementById('navTransportation').style.color = '#0288D8';

                 document.getElementById('navPortAgent').style.background = '#ECDFC4';
                 document.getElementById('navPortAgent').style.bordercolor = '#0288D8';
                 document.getElementById('navPortAgent').style.color = '#0288D8';

                 document.getElementById('navMeetAndGreet').style.background = '#ECDFC4';
                 document.getElementById('navMeetAndGreet').style.bordercolor = '#0288D8';
                 document.getElementById('navMeetAndGreet').style.color = '#0288D8';


                 document.getElementById("TabHotelTable").style.display = "block";
                 document.getElementById("TabTransportation").style.display = "none";
                 document.getElementById("TabPortagent").style.display = "none";
                 
                 document.getElementById("TabMeetAndGreet").style.display = "none";
                 document.getElementById("TabVisa").style.display = "none";
                 document.getElementById("TabSafeguard").style.display = "none";
                 

             }
             else if (val == 2) {

                 document.getElementById("TabHotelTable").style.display = "none";
                 document.getElementById("TabTransportation").style.display = "block";
                 document.getElementById("TabPortagent").style.display = "none";
                 
                 document.getElementById("TabMeetAndGreet").style.display = "none";
                 document.getElementById("TabVisa").style.display = "none";
                 document.getElementById("TabSafeguard").style.display = "none";

                 document.getElementById('navTransportation').style.background = '#0288D8';
                 document.getElementById('navTransportation').style.bordercolor = '#0288D8';
                 document.getElementById('navTransportation').style.color = '#ffffff';

                 document.getElementById('navHotel').style.background = '#ECDFC4';
                 document.getElementById('navHotel').style.bordercolor = '#0288D8';
                 document.getElementById('navHotel').style.color = '#0288D8';

                 document.getElementById('navPortAgent').style.background = '#ECDFC4';
                 document.getElementById('navPortAgent').style.bordercolor = '#0288D8';
                 document.getElementById('navPortAgent').style.color = '#0288D8';

                 document.getElementById('navMeetAndGreet').style.background = '#ECDFC4';
                 document.getElementById('navMeetAndGreet').style.bordercolor = '#0288D8';
                 document.getElementById('navMeetAndGreet').style.color = '#0288D8';
                 
                 if (document.getElementById('ctl00_NaviPlaceHolder_uoCheckBoxIsWithShuttle').checked == true ){
                    alert("Hotel booking is with shuttle");
                 
                 }
                 

             }
             else if (val == 3) {

                 document.getElementById("TabHotelTable").style.display = "none";
                 document.getElementById("TabTransportation").style.display = "none";
                 document.getElementById("TabPortagent").style.display = "block";

                 document.getElementById("TabMeetAndGreet").style.display = "none";
                 document.getElementById("TabVisa").style.display = "none";
                 document.getElementById("TabSafeguard").style.display = "none";


                 document.getElementById('navPortAgent').style.background = '#0288D8';
                 document.getElementById('navPortAgent').style.bordercolor = '#0288D8';
                 document.getElementById('navPortAgent').style.color = '#ffffff';

                 document.getElementById('navHotel').style.background = '#ECDFC4';
                 document.getElementById('navHotel').style.bordercolor = '#0288D8';
                 document.getElementById('navHotel').style.color = '#0288D8';

                 document.getElementById('navTransportation').style.background = '#ECDFC4';
                 document.getElementById('navTransportation').style.bordercolor = '#0288D8';
                 document.getElementById('navTransportation').style.color = '#0288D8';

                 document.getElementById('navMeetAndGreet').style.background = '#ECDFC4';
                 document.getElementById('navMeetAndGreet').style.bordercolor = '#0288D8';
                 document.getElementById('navMeetAndGreet').style.color = '#0288D8';

 

             }
             else if (val == 4) {

                 document.getElementById("TabHotelTable").style.display = "none";
                 document.getElementById("TabTransportation").style.display = "none";
                 document.getElementById("TabPortagent").style.display = "none";

                 document.getElementById("TabMeetAndGreet").style.display = "block";
                 document.getElementById("TabVisa").style.display = "none";
                 document.getElementById("TabSafeguard").style.display = "none";


                 document.getElementById('navMeetAndGreet').style.background = '#0288D8';
                 document.getElementById('navMeetAndGreet').style.bordercolor = '#0288D8';
                 document.getElementById('navMeetAndGreet').style.color = '#ffffff';

                 document.getElementById('navHotel').style.background = '#ECDFC4';
                 document.getElementById('navHotel').style.bordercolor = '#0288D8';
                 document.getElementById('navHotel').style.color = '#0288D8';

                 document.getElementById('navPortAgent').style.background = '#ECDFC4';
                 document.getElementById('navPortAgent').style.bordercolor = '#0288D8';
                 document.getElementById('navPortAgent').style.color = '#0288D8';

                 document.getElementById('navTransportation').style.background = '#ECDFC4';
                 document.getElementById('navTransportation').style.bordercolor = '#0288D8';
                 document.getElementById('navTransportation').style.color = '#0288D8';
                 
                 
                 
                 
//                 document.getElementById("ctl00_NaviPlaceHolder_uoButtonFinish").style.display = "none";
                 
             }
             else if (val == 5) {

                 document.getElementById("TabHotelTable").style.display = "none";
                 document.getElementById("TabTransportation").style.display = "none";
                 document.getElementById("TabPortagent").style.display = "block";
                 
                 document.getElementById("TabMeetAndGreet").style.display = "none";
                 document.getElementById("TabVisa").style.display = "none";
                 document.getElementById("TabSafeguard").style.display = "none";

             }
             else if (val == 6) {

                 document.getElementById("TabHotelTable").style.display = "none";
                 document.getElementById("TabTransportation").style.display = "none";
                 document.getElementById("TabPortagent").style.display = "none";
                  
                  
                 document.getElementById("TabMeetAndGreet").style.display = "none";
                 document.getElementById("TabVisa").style.display = "none";
                
                 document.getElementById("TabSafeguard").style.display = "block";

             }
         }

         function OnDateChange(fromControl) {
             if (fromControl == 'CheckIn') {
                 ValidateIfPastDate($("#<%=uoTextBoxCheckinDate.ClientID %>").val(), fromControl);

             }
             else if (fromControl == 'CheckOut') {
                 ValidateIfPastDate($("#<%=uoTextBoxCheckoutDate.ClientID %>").val(), fromControl);
             }
             

         }

         function ValidateIfPastDate(pDate, fromControl) {
             if (ValidateDate(pDate)) {
                 var currentDate = new Date();
                 var currentDate2 = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate());

                 var selectedDate = Date.parse(pDate);

                             
                
                 if (selectedDate <= currentDate2) {
                 
                     alert("Past date is invalid!");
                     if (fromControl == 'CheckIn') {
                         $("#<%=uoTextBoxCheckinDate.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                     }
                     else if (fromControl == 'CheckOut') {
                         $("#<%=uoTextBoxCheckoutDate.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                     }
                     $("#<%=uoTextBoxDuration.ClientID %>").val(0);
                     $("#<%=uoHiddenFieldNoOfNites.ClientID %>").val(0);

                     return false;
                 }
                 else {
                     if (fromControl == 'CheckOut') {
                         var s_endDate = new Date($("#<%=uoTextBoxCheckinDate.ClientID %>").val());
                         if (($("#<%=uoTextBoxCheckoutDate.ClientID %>").val() <= $("#<%=uoTextBoxCheckinDate.ClientID %>").val()) || s_endDate == 'Invalid Date') {
                             var mydate = new Date(selectedDate);
                             var StartDate = new Date(mydate.setDate(mydate.getDate() - 1));
                             $("#<%=uoTextBoxCheckinDate.ClientID %>").val(StartDate.format('MM/dd/yyyy'))
                             return false;
                         }
                     }


                     if (fromControl == 'CheckIn') {

                         var c_endDate = new Date($("#<%=uoTextBoxCheckoutDate.ClientID %>").val());
                        

                         if (($("#<%=uoTextBoxCheckoutDate.ClientID %>").val() <= $("#<%=uoTextBoxCheckinDate.ClientID %>").val()) || c_endDate == 'Invalid Date') {
                             var mydate = new Date(selectedDate);
                             var endDate = new Date(mydate.setDate(mydate.getDate() + 1));
                             $("#<%=uoTextBoxCheckoutDate.ClientID %>").val(endDate.format('MM/dd/yyyy'))
                         }
                     }
                     var date1 = $("#<%=uoTextBoxCheckinDate.ClientID %>").val();
                     var date2 = $("#<%=uoTextBoxCheckoutDate.ClientID %>").val();
                     var diff = (Math.floor((Date.parse(date2) - Date.parse(date1)) / 86400000));
                     $("#<%=uoTextBoxDuration.ClientID %>").val(diff);
                     $("#<%=uoHiddenFieldNoOfNites.ClientID %>").val(diff);
                 }
             }
             else {
                 alert("Invalid date!");
                 if (fromControl == 'CheckIn') {
                     $("#<%=uoTextBoxCheckinDate.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                 }
                 else if (fromControl == 'CheckOut') {
                 
                     $("#<%=uoTextBoxCheckoutDate.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                 }
                 return false;
             }
             return true;
         }

         function ValidateDate(pDate) {
             try {
                 var dt = Date.parse(pDate);
                 return true;
             }
             catch (err) {
                 return false;
             }
         }

         function executeSearch() {
             $("#<%=uoButtonSearch.ClientID %>").click();
         }

         function OverflowCofirmation() {
             var meg = document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldOverflowMessage").value;
             document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldSaveConfirmation").value = "Overflow";
             var r = confirm(meg);
             if (r == true) {
                 $("#<%=uoButtonOverflow.ClientID %>").click();
             }
         
         }

         function TransportationCofirmation() {
             document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldSaveConfirmationTrans").value = "Transporation";
             var r = confirm("Are sure you want to send the transportation request ");
             if (r == true) {
                 $("#<%=uoButtonOverflow.ClientID %>").click();
             }
         }

         function MeetAndGreetCofirmation() {
             document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldSaveConfirmationMAG").value = "MeetAndGreet";
             var r = confirm("Are sure you want to send the Meet And Greet request ");
             if (r == true) {
                 $("#<%=uoButtonOverflow.ClientID %>").click();
             }
         }

         function PortAgentCofirmation() {
             document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldSaveConfirmationPA").value = "PortAgent";
             var r = confirm("Are sure you want to send the Port agent request ");
             if (r == true) {
                 $("#<%=uoButtonOverflow.ClientID %>").click();
             }
         }

         function PortAgentHotelCofirmation() {
             document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldSaveConfirmationPA").value = "PortAgentHotel";
             var r = confirm("Are sure you want to send the Port agent hotel request ");
             if (r == true) {
                 $("#<%=uoButtonOverflow.ClientID %>").click();
             }
         }
         
         
         function SafeguardCofirmation() {
             document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldSaveConfirmationSG").value = "Safeguard";
             var r = confirm("Are sure you want to send the Safeguard request ");
             if (r == true) {
                 $("#<%=uoButtonOverflow.ClientID %>").click();
             }
         }

         function SaveHotel() {
             document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldSaveHotel").value = "1";
         }

         function SelectRoomType(val) {
             document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldSaveHotel").value = "1";


             var hotel = document.getElementById("ctl00_NaviPlaceHolder_uoDropDownListHotel");
             var BID = hotel.options[hotel.selectedIndex].value;
             var BRaName = hotel.options[hotel.selectedIndex].innerHTML;
             
             var LT = 0;
             var RID = val.options[val.selectedIndex].value; ;
             var cdate = document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxCheckinDate").value;


             console.log((BRaName.split(" - "))[0]);


             if ((BRaName.split(" - "))[0] == "PA") {
                 LT = 0;
             }
             else {
                 LT = 1;
             }


             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "../PageMethods.aspx/GetVendorContractAmount",
                 data: "{'LT': '" + LT + "', 'BID': '" + BID + "', 'RID': '" + RID + "', 'cdate': '" + cdate + "'}",
                 dataType: "json",
                 success: function(data) {

                 console.log(data);
                     console.log(Number(data.d[0]).toFixed(2) + ' ' + data.d[1]);

                     document.getElementById("ctl00_NaviPlaceHolder_uoTextContractedRate").value = Number(data.d[0]).toFixed(2);
                     document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxComfirmRate").value = Number(data.d[1]).toFixed(2);


                 }
             });
             
             
//             
//             console.log(PageMethods.GetVendorContractAmount(LT, BID, RID, cdate, onSucess, onError));
//             
//             
//             function onSucess(result) {
//                 alert(result);
//             }
//             function onError(result) {
//                
//                 alert('Something wrong.');
//             }
//              
              
              
              
              
              
              
              
              
              
              
              
//             
//             $.ajax({
//                 type: "POST",
//                 contentType: "application/json; charset=utf-8",
//                 url: "~/GetVendorContractAmount",
//                 data: "{'LT': '" + 0 + "', 'BID': '" + 0 + "', 'RID': '" + 1 + "', 'cdate': '" + '08/05/2016' + "'}",
//                 dataType: "json",
//                 success: function(data) {
//                     if (data.d == 1) {
//                         document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_ouLinkButtonBranch').href = "/CrewAssist/CrewAssistCancelHotel.aspx?hN=" + Hotelname + "&hId=" + HotelTransID + "&hTID=" + HotelTransID + "&hIdb=" + IdBigInt + "&hSno=" + seqNo + "&eadd=" + BoxEmail + "&eIsV=0&Hty=" + selectedText;
//                         document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_ouLinkButtonBranch').click();
//                     }
//                     else if (data.d == 2) {
//                         document.getElementById("ctl00_NaviPlaceHolder_uoButtonLoadAir").click();
//                     }
//                     else {
//                         alert("Cancellation is not allowed as the contracted cancellation period of " + CancellationTermsInt + " hours has past.");
//                     }
//                 }
//             });
//             
             
             
             
         }

         function SaveTransportation(val) {
             document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldSaveTrans").value = "1";
         }


         function SaveTransportationCombo(val) {

             var num;

             var TReqID = document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldTravelRequestID").value;
             var IDBigint = document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldIDBigint").value;
             var RecLoc = "";
             var SeqNo = document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldSeqNo").value;
             var Stat = document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxStatus").value;


             var RequestDate = document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRequestDate").value;

             
             
             var str = val.id.replace("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl", "");
             for (i = 0; i < str.length; i++) {
                 if (String(str).substring(i, i + 1) == '_') break;
                 num = String(str).substring(0, i + 1)
             }
             var from = document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoDropDownListTFrom")
             var to = document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoDropDownListTTo")
            
             document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldFromTo").value = from.options[from.selectedIndex].value + '|' + to.options[to.selectedIndex].value;
             
             var portCode = document.getElementById('ctl00_NaviPlaceHolder_uoDropDownListPort');
             var port = portCode.options[portCode.selectedIndex].text.replace(" ","");
             var portText = "";
             
             for (i = 0; i < port.length; i++) {
                 if (String(port).substring(i, i + 1) == '-') break;
                 portText = String(port).substring(0, i + 1);
             }
             
             var HotelCode = document.getElementById('ctl00_NaviPlaceHolder_uoDropDownListHotel');
             var Hotel = HotelCode.options[HotelCode.selectedIndex].text.replace(" ", "");
             var HotelText = "";
           
             for (i = 0; i < Hotel.length; i++) {
                 if (String(Hotel).substring(i, i + 1) == '-') break;
                 HotelText = String(Hotel).substring(0, i + 1);
             }



                 if (from.id == val.id) { 
                     
                     document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTFrom").value = "";
                     if (from.options[from.selectedIndex].text == "Ship") {
                         document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTPickupdate").value = RequestDate;
                         document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTFrom").value = port.replace(portText, "").replace("-", "");
                         document.getElementById("ctl00_NaviPlaceHolder_uoButtonFromTo").click();
                     }

                     else if (from.options[from.selectedIndex].text == "Hotel") {
                     if (document.getElementById("ctl00_NaviPlaceHolder_uoCheckBoxIsWithShuttle").checked == true
                            && document.getElementById("ctl00_NaviPlaceHolder_uoDropDownListHotel").selectedIndex > 0
                            && String(HotelCode.options[HotelCode.selectedIndex].text).substring(0, 2) == 'HT') {
                             var r = confirm("The hotel has shuttle \n Do you want to continue?");
                             if (r == false)
                             {
                                 val.selectedIndex = 0;
                                 return false;
                             }
                         }
                        
                         if (Hotel.replace(HotelText, "").replace("-", "") != '-SelectHotel--') {
                             document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTFrom").value = Hotel.replace(HotelText, "").replace("-", "");
                         }
                         document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTPickupdate").value = RequestDate;

                          document.getElementById("ctl00_NaviPlaceHolder_uoButtonFromTo").click();
                     }

                     else if (from.options[from.selectedIndex].text == "Airport") {
                     
                         if (document.getElementById("ctl00_NaviPlaceHolder_uoCheckBoxIsWithShuttle").checked == false
                             && document.getElementById("ctl00_NaviPlaceHolder_uoDropDownListHotel").selectedIndex > 0
                             && String( HotelCode.options[HotelCode.selectedIndex].text).substring(0,2) == 'HT') {
                             var r = confirm("The hotel has shuttle \n Do you want to continue?");
                             if (r == false) {
                                 val.selectedIndex = 0;
                                 return false;
                             }
                         }

                         document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTFrom").value = GetAirportname(); ;
                         document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTPickupdate").value = RequestDate;

                         document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldAirportFromTo").value = 'AFrom-' + GetAirportname(); ;

                         document.getElementById("ctl00_NaviPlaceHolder_uoButtonFromTo").click();
                        
                     }
                     else {
                         document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTPickupdate").value = RequestDate;
                         document.getElementById("ctl00_NaviPlaceHolder_uoButtonFromTo").click();
                     }
                 }


                 if (to.id == val.id) {
                    document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTTo").value = "";


                    if (to.options[to.selectedIndex].text == "Ship") {

                        if (String(HotelCode.options[HotelCode.selectedIndex].text).substring(0, 2) == 'PA') { 
                            if (from.options[from.selectedIndex].text == 'Hotel') {
                                if (document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldPAHotelShip").value == 'False') {
                                    var r = confirm("No route Hotel to Ship to this Vendor\nDo want to continue?");
                                    if (r == false) {
                                        val.selectedIndex = 0;
                                        return false;
                                    }
                                }
                            }
                        }
                        
                        document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTTo").value = port.replace(portText, "").replace("-", "");
                        document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTPickupdate").value = RequestDate;
                        document.getElementById("ctl00_NaviPlaceHolder_uoButtonFromTo").click();
                         
                    }
                    else if (to.options[to.selectedIndex].text == "Hotel") {

                        if (String(HotelCode.options[HotelCode.selectedIndex].text).substring(0, 2) == 'PA') {
                            if (from.options[from.selectedIndex].text == 'Airport') {
                                if (document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldPAHotelShip").value == 'False') {
                                    var r = confirm("No route Airport to Hotel to this Vendor\nDo want to continue?");
                                    if (r == false) {
                                        val.selectedIndex = 0;
                                        return false;
                                    }
                                }
                                
                             }
                        }

                        if (Hotel.replace(HotelText, "").replace("-", "") != '-SelectHotel--') {
                            document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTTo").value = Hotel.replace(HotelText, "").replace("-", "");
                        }
                        document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTPickupdate").value = RequestDate;
                        document.getElementById("ctl00_NaviPlaceHolder_uoButtonFromTo").click();
                        
                    }

                    else if (to.options[to.selectedIndex].text == "Airport") {
                        document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTTo").value = GetAirportname();
                        document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTPickupdate").value = RequestDate;
                        document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldAirportFromTo").value = 'ATo-' + GetAirportname();
                        document.getElementById("ctl00_NaviPlaceHolder_uoButtonFromTo").click();
                        
                        
                    }
                    else {
                        document.getElementById("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl" + num + "_uoTextBoxTPickupdate").value = RequestDate;
                        document.getElementById("ctl00_NaviPlaceHolder_uoButtonFromTo").click();
                    }
                 }
             document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldSaveTrans").value = "1";
         }



         function GetAirportname() {


             var status = document.getElementById('ctl00_NaviPlaceHolder_uoTextBoxStatus').value;
             var ArrCode = document.getElementById('ctl00_NaviPlaceHolder_uoHiddenFieldArrCode').value;
             var PortCode = document.getElementById('ctl00_NaviPlaceHolder_uoHiddenFieldPortCode').value;

             var dropdown = document.getElementById('ctl00_NaviPlaceHolder_cboAirportList');

                
            
            var strCode = status == "ON" ? ArrCode == "" ? PortCode : ArrCode : PortCode;
            var AirportName = '';

            for (var i = 0; i < dropdown.length; i++) {
            
                //This alert is for the behavior of the output!
                
                dropdown.selectedIndex = i;
//                alert(dropdown.options[dropdown.selectedIndex].value);
                if (dropdown.options[dropdown.selectedIndex].value == strCode) {
                    AirportName = dropdown.options[dropdown.selectedIndex].innerText;
                }
            }
            return AirportName;
         
         }


      
         function SavePortAgent(val) {
             document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldSavePA").value = "1";
         }

         function checkdate(val) {
             if (Date.parse(val).toString() == 'NaN') {
                 return false;
             }
             else {
                 return true;
             }
         }

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

                 if (email.id == 'ctl00_NaviPlaceHolder_uoTextBoxEmail' &&
                     document.getElementById("ctl00_NaviPlaceHolder_CheckBoxEmail").checked == false) {
                     document.getElementById("ctl00_NaviPlaceHolder_CheckBoxEmail").checked = true;
                 }
                 else if (email.id == 'ctl00_NaviPlaceHolder_uoTextBoxEmailTrans' &&
                     document.getElementById("ctl00_NaviPlaceHolder_uoCheckBoxEmailTrans").checked == false) {
                     document.getElementById("ctl00_NaviPlaceHolder_uoCheckBoxEmailTrans").checked = true;
                 }
                 else if (email.id == 'ctl00_NaviPlaceHolder_uoTextBoxPAEmail' &&
                     document.getElementById("ctl00_NaviPlaceHolder_uoCheckBoxPAEmail").checked == false) {
                     document.getElementById("ctl00_NaviPlaceHolder_uoCheckBoxPAEmail").checked = true;
                 }
                 else if (email.id == 'ctl00_NaviPlaceHolder_uoTextBoxMAGEmail' &&
                      document.getElementById("ctl00_NaviPlaceHolder_uoCheckBoxMAGEmail").checked == false) {
                      document.getElementById("ctl00_NaviPlaceHolder_uoCheckBoxMAGEmail").checked = true;      
                 }
                 else if (email.id == 'ctl00_NaviPlaceHolder_uoTextBoxSafeguardEmail' &&
                       document.getElementById("ctl00_NaviPlaceHolder_uoCheckBoxSafeguardEmail").checked == false) {
                       document.getElementById("ctl00_NaviPlaceHolder_uoCheckBoxSafeguardEmail").checked = true;   
                 }
             }
             return true;
         }

         function checkRamarkTravelID(val) {
                
//             if (document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldTravelRequestID").value == "") {
//                 alert("Select Crew schedule... ");
//                 return false;
//             }
//             else

             if (document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxEmployeeID").value == "") {
                alert("Please enter employee ID... ");
                return false;
            }
            else if (document.getElementById("ctl00_NaviPlaceHolder_txtRemark").value == "") {
                 alert("Remark Required...");
                 return false;
             }
             else if (document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRSourceRequest").value == "0" || document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRSourceRequest").value == "") {
                 alert("Request source Required!!! \nClick Remark text box to select the request source...");
                 return false;
             }
             else {
                 
                 return true;
             }
         }

         function Confirmcancel(val) {

           

             var name = val.id.replace("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl", "");

             var selectedText = "";
             var ListHotel = document.getElementById('ctl00_NaviPlaceHolder_uoDropDownListHotel');


             if (name.substring(2, name.length) == 'ImageButton2') {
 
//                 var counter = name.replace('_ImageButton2', '');
//                 var PortAgent = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldHIsPortagent').value == "True" ? 'PA' : 'HT';
//                 if (selectedText == '') {
//                     selectedText = PortAgent;
//                 }
//                 
//                 var HotelTransID = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldHotelID').value;
//                 var Hotelname = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_lblHotelBook').innerHTML;
//                 var TravelRequestID = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldHTravelRequestID').value;
//                 var IdBigInt = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldHIDBigint').value;
//                 var seqNo = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldSeqNo').value;
//                 var BoxEmail = document.getElementById("<%= uoTextBoxEmail.ClientID %>").value;

//                 var r = confirm("Are you sure you want to cancel the hotel request?");
//                 if (r != true) {
//                     return false;
//                 }
//                 else {

//                      document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_ouLinkButtonBranch').href = "/CrewAssist/CrewAssistCancelHotel.aspx?hN=" + Hotelname + "&hId=" + HotelTransID + "&hTID=" + HotelTransID + "&hIdb=" + IdBigInt + "&hSno=" + seqNo + "&eadd=" + BoxEmail + "&eIsV=0&Hty=" + selectedText;
//                      document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_ouLinkButtonBranch').click();
//                 }


                var counter = name.replace('_ImageButton2', '');
                var HotelTransID = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldHotelID').value;
                var Hotelname = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_lblHotelBook').innerHTML;
                var TravelRequestID = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldHTravelRequestID').value;

                var IdBigInt = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldHIDBigint').value;
                var seqNo = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldSeqNo').value;
                var BoxEmail = document.getElementById("<%= uoTextBoxEmail.ClientID %>").value;

                var CancellationTermsInt = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldCancellationTermsInt').value;
                var HTimeZone = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldHotelTimeZoneID').value;
                var CutOffTime = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldCutOffTime').value;
                var IsConfirm = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldIsConfirm').value;


                var PortAgent = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_uoHiddenFieldHIsPortagent').value == "True" ? 'PA' : 'HT';

                if (selectedText == '') {
                    selectedText = PortAgent;
                }
                
                var currentDate = FormatCurrentDate(new Date());
                if (IsConfirm == "False") {
                    var r = confirm("Are you sure you want to cancel the hotel request?");
                    if (r != true) {
                        return false;
                    }
                }


                if (CancellationTermsInt == '0') {
                    document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_ouLinkButtonBranch').href = "/CrewAssist/CrewAssistCancelHotel.aspx?hN=" + Hotelname + "&hId=" + HotelTransID + "&hTID=" + HotelTransID + "&hIdb=" + IdBigInt + "&hSno=" + seqNo + "&eadd=" + BoxEmail + "&eIsV=0&Hty=" + selectedText;
                    document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_ouLinkButtonBranch').click();
                }
                else { 
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "../PageMethods.aspx/CheckCancellationCheckinDate",
                        data: "{'cTHrs': '" + CancellationTermsInt + "', 'cTZ': '" + HTimeZone + "', 'cCOT': '" + CutOffTime + "', 'cCUD': '" + currentDate + "', 'cIsC': '" + IsConfirm + "', 'cHID': '" + HotelTransID + "'}",
                        dataType: "json",
                        success: function(data) {
                            if (data.d == 1) {
                                document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_ouLinkButtonBranch').href = "/CrewAssist/CrewAssistCancelHotel.aspx?hN=" + Hotelname + "&hId=" + HotelTransID + "&hTID=" + HotelTransID + "&hIdb=" + IdBigInt + "&hSno=" + seqNo + "&eadd=" + BoxEmail + "&eIsV=0&Hty=" + selectedText;
                                document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_ouLinkButtonBranch').click();
                            }
                            else if (data.d == 2) {
                                document.getElementById("ctl00_NaviPlaceHolder_uoButtonLoadAir").click();
                            }
                            else {
                                alert("Cancellation is not allowed as the contracted cancellation period of " + CancellationTermsInt + " hours has past.");
                            }
                        }
                    });
                }

                
                
                
                
                

            }
            else {

            
                
                if ($("#<%=uoHiddenFieldHotelRequestID.ClientID %>").val() == '') {
                    return false;
                }
                 
                if (name.substring(2, name.length) == "uoImageButtonCancel") {

                    var r = confirm("Are you sure you want to cancel the request?");
                    if (r == true)
                        return true;
                    else
                        return false;

                }
                else if (name.substring(2, name.length) == "uoImageButtonApproved") {

              
                  var counter2 = name.replace('_uoImageButtonApproved', '');
                  var HotelTransID2 = document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter2 + '_uoHiddenFieldHotelID').value;
                  document.getElementById("<%= uoHiddenFieldHotelRequestID.ClientID %>").value = HotelTransID2;

                
                    var r = confirm("Are you sure you want to approve the request?");
                    if (r == true)
                        return true;
                    else
                        return false;
                }
                document.getElementById("<%= uoHiddenFieldCancelHotel.ClientID %>").value = '0';

            }
         }



         function FormatCurrentDate(val) {
             var d = new Date(val);
             var n = d.getMonth() + "/" + d.getDay() + "/" + d.getFullYear() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();
             return n;
         }





         function ConfirmTranscancel(val) {

             var name = val.id.replace("ctl00_NaviPlaceHolder_uoListViewTransportation_ctrl", "");

             var name2 = val.id.replace("ctl00_NaviPlaceHolder_uoListViewTranportationRoute_ctrl", "");

           
             var selectedText = "";
             var ListHotel = document.getElementById('ctl00_NaviPlaceHolder_uoDropDownListVehicleVendor');
 
             
            if (name.substring(2, name.length) == 'ImageButton2') {
                var counter = name.replace('_ImageButton2', '');
                 
                var HotelTransID = document.getElementById('ctl00_NaviPlaceHolder_uoListViewTransportation_ctrl' + counter + '_uoHiddenFieldTransactionID').value;
                var Hotelname = document.getElementById('ctl00_NaviPlaceHolder_uoListViewTransportation_ctrl' + counter + '_lblListviewTransportation').innerHTML;
                var TravelRequestID = document.getElementById('ctl00_NaviPlaceHolder_uoListViewTransportation_ctrl' + counter + '_uoHiddenFieldTTravelRequestID').value;

                var IdBigInt = document.getElementById('ctl00_NaviPlaceHolder_uoListViewTransportation_ctrl' + counter + '_uoHiddenFieldTIDBigint').value;
                var seqNo = document.getElementById('ctl00_NaviPlaceHolder_uoListViewTransportation_ctrl' + counter + '_uoHiddenFieldTSeqNo').value;
               
                var IsPortagent = document.getElementById('ctl00_NaviPlaceHolder_uoListViewTransportation_ctrl' + counter + '_uoHiddenFieldIsPortAgent').value ;//== 'True' ? 'PA' : 'VH';

                var BoxEmail = document.getElementById("<%= uoTextBoxEmailTrans.ClientID %>").value;
                

                document.getElementById('ctl00_NaviPlaceHolder_uoListViewTransportation_ctrl' + counter + '_ouLinkButtonTrans').href = "/CrewAssist/CrewAssistCancelHotel.aspx?hN=" + Hotelname + "&hId=" + HotelTransID + "&hTID=" + HotelTransID + "&hIdb=" + IdBigInt + "&hSno=" + seqNo + "&eadd=" + BoxEmail + "&eIsV=1&Vty=" + IsPortagent;
            }
            else { 
            
                if (name.substring(2, name.length) == "uoImageButtontCancel") {

                     var r = confirm("Are you sure you want to cancel the request?");
                     if (r == true)
                         return true;
                     else
                         return false;
                 }
                 else if (name.substring(2, name.length) == "uoImageButtonApproved") {

 
                 
                 
                     var r = confirm("Are you sure you want to approve the request?");
                     if (r == true)
                         return true;
                     else
                         return false;
                         
                 }
                 else if (name2.substring(2, name.length) == "uoImageButtontDeleteRoute") {
                     var r = confirm("Are you sure you want to delete the request?");
                     if (r == true)
                         return true;
                     else
                         return false;
                 }
                 else {
                     return false;
                 }
            }
        }


        function ConfirmDeleteCompanion() {

            var r = confirm("Are you sure you want to delete the selected companion?");
           
            if (r == true)
                return true;
            else
                return false;


        }
      
      
        
        function AirChangeStatus(val) {

            var Status = document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl0_uoHiddenFieldStatus').value;

            var res = document.getElementById('ctl00_NaviPlaceHolder_uoListviewAir_ctrl0_uoHiddenFieldStatusMessage').value.split(",");
            
          

            document.getElementById("<%= uoHiddenFieldFlightStatus.ClientID %>").value = 'false';
            if (val.options[val.selectedIndex].text == Status) {
                return;
            }


            var msg1 = res[0] == undefined ? '' : res[0];
            var msg2 = res[1] == undefined ? '' : res[1];
            var msg3 = res[2] == undefined ? '' : res[2];
            var msg = '';
            var msgVH = 'There is a hotel and vehicle request associated to this record. Any services will get cancelled.\n';
            var msgH = 'There is a hotel request associated to this record. Any services will get cancelled.\n';
            var msgV = 'There is a vehicle request associated to this record. Any services will get cancelled.\n';
       
            var Iscancel = false;

            if ($("[id$=uoCheckBoxsHotelTransaction]").length > 0 && $("[id$=lblListviewTransportation]").length > 0) {
                msg = msgVH;
            }
            else if ($("[id$=uoCheckBoxsHotelTransaction]").length > 0 && $("[id$=lblListviewTransportation]").length == 0) { 
                msg = msgH;
            }
            else if ($("[id$=uoCheckBoxsHotelTransaction]").length == 0 && $("[id$=lblListviewTransportation]").length > 0) { 
                msg = msgV;
            }

            if (val.value == 4 || (val.value == 1 && Status != 'Open'))
            {
                 Iscancel = true;
            }


            var r = confirm((Iscancel == true ? msg : '')  + ' ' + res + ' ' + val.options[val.selectedIndex].text + '?');
            if (r == true) {
                document.getElementById("<%= uoHiddenFieldFlightStatus.ClientID %>").value = 'true';
            }
            
        }
    
    </script>
     <%--<script type="text/javascript" language="javascript">
         $(document).ready(function() {
             showUpdate();
         });

         function showUpdate() {
             $("#anchorShowUpdate").fancybox().trigger('click');
             $("#anchorShowUpdate").fancybox(
                {
                    'centerOnScroll': false,
                    'width': '50%',
                    'height': '70%',
                    'scrolling': 'no',
                    'autoScale': false,
                    'autoSize': true,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe'
                });
         }
        </script>   
        <a id="anchorShowUpdate" href="../UnderMaintenance2.aspx">--%>
</asp:Content>
