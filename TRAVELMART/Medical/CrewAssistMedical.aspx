<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMasterPage.Master" AutoEventWireup="true" CodeBehind="CrewAssistMedical.aspx.cs" Inherits="TRAVELMART.CrewAssistMedical" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>   


    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td class="PageTitle">
                   Medical Request Page
                </td>                
            </tr>
        </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server" >

    <style type="text/css" >
    
        .InfoWidth {
            width:95%;
            background-color: #CDDDE2;
            border-style: solid;
            border-width: 1px;
            border-color: #9EC0DE;
        }
        table, tr
        {
            border-style:none;
            border-color:#0288D8;
            
        }
         /*   width:100%; */
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
            color: #0288D8;  
            -moz-border-radius: 3.25px;
            -webkit-border-radius: 3.25px;
            line-height: 1.6em;
         }
        .ClassPanelCrewSchedule { max-height: 70px; height:70px; overflow: auto; }
        .ClassPanelAir { max-height: 90px; height:90px; overflow: auto; }
        .ClassPanelHotel { max-height: 90px; height:90px; overflow: auto; }
        .ClassPanelTransportation { max-height: 107px;  overflow: auto; }
        
        
        .DivCrewAssistComment
        {
            position:absolute;
            top: 354px;
            left: 400.5px;
            border-style:solid;
            border-width:thin;
            border-color:#C0C0C0;
            margin-top: -9em; /*set to a negative number 1/2 of your height*/
            margin-left: -15em; /*set to a negative number 1/2 of your width*/
        }
       
        .PrimaryHotelComfirmation
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding: 10px;
            width: 500px;
            height: 150px;
        }
        
        .ComfirmBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        
    </style>
    <asp:UpdatePanel runat="server" UpdateMode="Always" ID="uoPanelMaster" >
        <ContentTemplate>
         
             <table id="ubTableMain" style="text-align:left; width:98%;" class="LeftClass" runat="server"> 
                <tr>
                    <td>Employee ID</td>
                    <td style="white-space:nowrap" ><asp:TextBox ID="txtEmployeeID" Width="72%"  runat="server" />
                     &nbsp;
                        <asp:Button ID="uoButtonSearch" runat="server" Text="Search" CssClass="SmallButton" OnClick="uoButtonSearch_click"  />
                        </td>
                    <td>Name</td>
                    <td colspan="3" style="white-space:nowrap;" >
                        <asp:TextBox ID="txtName" runat="server" Width="97.5%"  ToolTip="Last Name/First Name/ Middle Name "/>
                       <%-- <br/>
                        <p style="font-size:xx-small" >Last name &nbsp;&nbsp; First name&nbsp;&nbsp;&nbsp; MI </p>--%>
                    </td>
                    <td colspan="3" >
                        <button id="btnShowRemark2" style="height:15px; vertical-align:top;"  />
                    </td>
                    <td rowspan="3" style="width:30%" >
                        <table id="TableRemark" style="width:100%; height:100%; vertical-align:top; border-style:solid; border-width:thin; background-color:#FFFFFF;" >
                           <tr style="height:100%;vertical-align:top;" >
                              <td colspan="2" style="height:100%;">
                                  <asp:Panel ID="uoPanelRemark" ScrollBars="Vertical" runat="server"  style="max-height:60px; height:60px; "   >
                                      <asp:ListView ID="uoListViewRemark" runat="server" >
                                                <LayoutTemplate>
                                                    <table border="0" id="uoListviewTableRemark"    cellpadding="0" cellspacing="0" 
                                                            style="border-style:none; width:99.8%;"
                                                           class="listViewTable">
                                                        <tr>
                                                           <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                                                        </tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr >
                                                        <td style="min-height:22px;height:22px; text-align:left; margin:5px; padding:8px; width:0.1%;" >
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
                                                                        runat="server" OnClientClick="return DeleteRemarkmain(this)" style="font-size:xx-small;" 
                                                                        Visible='<%# Convert.ToBoolean( Eval("Visible").ToString() == "True" ? 1 : 0) %>' />
                                                               
                                                    
                                                      <%--  
                                                            <asp:HiddenField ID="uoHiddenFieldGridRemarkID" runat="server" Value='<%# Eval("RemarkID")%>'/>
                                                            <asp:HiddenField ID="uoHiddenFieldRemarkTRID" runat="server" Value='<%# Eval("TravelRequestID")%>'/>
                                                            <asp:HiddenField ID="uoHiddenFieldResourceID" runat="server" Value='<%# Eval("ReqResourceID")%>'/>
                                                             
                                                            <asp:Label runat="server" ID="lblUserName"   Text='<%# Eval("RemarkBy") %>'
                                                                style="font-style:normal; font-weight:bold;"/>
                                                            <br/>
                                                            <div  style="max-width:300px; table-layout:fixed; overflow:hidden; position:relative;">
                                                                   <asp:Label runat="server" ID="lblRemark" style="table-layout:fixed; 
                                                                       overflow:hidden;" ToolTip='<%# Eval("Remark") %>'
                                                                    Text='<%# Eval("Remark") %>'/>
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
                                                                
                                                            <asp:LinkButton ID="uoLinkButtonDeleteRemark" ToolTip="Delete Remark" Text="Delete"
                                                                style="font-size:xx-small;" OnClick="ButtonDeleteRemark_click"
                                                                runat="server" OnClientClick="return DeleteRemarkmain(this)" 
                                                                Visible='<%# Convert.ToBoolean( Eval("Visible").ToString() == "True" ? 1 : 0) %>' />--%>
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
                                   <asp:TextBox ID="txtRemark" CssClass="TextArea" style="border-color:#C0C0C0;"  TextMode="MultiLine" Height="30px" 
                                   Width="100%" runat="server"></asp:TextBox>
                               </td>
                               <td style="padding-left:3px; vertical-align:top;">
                                   <asp:Button ID="btnSaveRemark" runat="server" Text="Save" OnClick="btnSaveRemark_click" />
                               </td>
                           </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>Gender</td>
                    <td><asp:TextBox ID="txtGender" runat="server" CssClass="InfoWidth" /></td>
                    <td>Brand</td>
                    <td><asp:TextBox ID="txtBrand" runat="server" CssClass="InfoWidth" /></td>
                    <td>Rank</td>
                    <td >
                        <asp:TextBox ID="txtRank" runat="server" CssClass="InfoWidth" /> 
                    </td>
                </tr>
                <tr>
                    <td>Nationality</td>
                    <td><asp:TextBox ID="txtNationality" runat="server" CssClass="InfoWidth" /></td>
                    <td>Ship</td>
                    <td><asp:TextBox ID="txtShip" runat="server" CssClass="InfoWidth" /></td>
                    <td>Status</td>
                    <td style="white-space:nowrap" >
                        <asp:TextBox ID="txtStatus" runat="server" Width="50px" CssClass="InfoWidth"/> 
                         &nbsp;
                         Reason Code
                         &nbsp;
                         <asp:TextBox ID="txtReasonCode" runat="server"  Width="70px"  CssClass="InfoWidth"/> 
                    </td>
                </tr>
             </table>
             <hr />
             <table  class="LeftClass" style="width:100%;" >
                <tr >
                     <%--Left Pane--%>
                    <td  style="vertical-align:top; padding-top:5px; text-align:left; border-color: #0288D8;  ">
                        <div >
                            Seaport: &nbsp;
                            <asp:DropDownList ID="uoDropDownListPort" runat="server" Width="80%"
                             AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListPort_SelectedIndexChanged"/>
                             <%-- &nbsp;
                            Expense Type: &nbsp;--%>
                            <asp:DropDownList ID="uoDropDownListExpenseType" style="display:none;" runat="server" Width="175px"
                                  AutoPostBack="true"/>
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
                                        <asp:CheckBox ID="uoCheckBoxShow" Text= "Show Past date"  
                                            runat="server" AutoPostBack="true" style="display:none"/>
                                         &nbsp;
                                         &nbsp;
                                        <asp:CheckBox ID="uoCheckBoxMedical" Text= "Medical" 
                                            runat="server" AutoPostBack="true"  style="display:none"/>
                                    </td>
                                </tr>
                            </table>
                            
                             <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" CssClass="ClassPanelCrewSchedule" Width="540px" >
                                <asp:ListView runat="server" ID="uoListviewTravel" >
                                        <LayoutTemplate>
                                         <table border="0" id="uoListviewTravelTable" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                                                <tr>
                                                    <th style="text-align: center; white-space: normal; width:20px;">
                                                    </th >
                                                    <th style="text-align: center; white-space: normal; width:100px;">
                                                        Record Locator
                                                    </th >
                                                    <th style="text-align: center; white-space: normal;">
                                                        Ship
                                                    </th >
                                                    <th style="text-align: center; white-space: normal;">
                                                        Status
                                                    </th>
                                                    <th style="text-align: center; white-space: normal; width:100px; ">
                                                        Sign On/Off Date
                                                    </th>
                                                    <th style="text-align: center; white-space: normal;">
                                                        Port
                                                    </th> 
                                                    <th style="text-align: center; white-space:nowrap; width:75px; ">
                                                        Reason Code
                                                    </th>   
                                                     <th style="text-align: center; white-space:nowrap; width:75px; ">
                                                        Immigration Status
                                                    </th>  
                                                </tr>        
                                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                                        </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                               <td style="padding-left:-10px">
                                                    <asp:CheckBox CssClass="Checkbox" Width="10px" style="margin-left:-10px;" 
                                                     ID="uoSelectCheckBoxs" runat="server" OnCheckedChanged="uoSelectCheckBoxs_CheckedChanged" 
                                                      AutoPostBack="true" />
                                               </td>
                                               <td class="leftAligned" style="white-space: normal;  width:100px;" >
                                                    <asp:Label runat="server" ID="lblRecordLocator" 
                                                        Text='<%# Eval("RecordLocator") %>'/>
                                                </td>
                                               
                                               <td class="leftAligned" style="white-space: normal;" >
                                                    <asp:Label runat="server" ID="lblVessel" 
                                                        Text='<%# Eval("VesselCode") %>'/>
                                                </td>
                                               <td class="leftAligned">
                                                    <asp:Label runat="server" ID="lblStatus" 
                                                        Text='<%# Eval("Status") %>'/></td>
                                               <td class="leftAligned">
                                                    <asp:Label runat="server" ID="lblRequestDate"  
                                                        Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("SignOnOffDate"))%>'/>
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
                                                 <td style="white-space: normal; visibility:hidden; display:none;" >
                                                 
                                                    <asp:HiddenField runat="server" ID="hfTravelRequestID" Value='<%# Eval("TravelRequestID") %>' />
                                                    <asp:HiddenField runat="server" ID="uoHiddenFieldHIDBigint" Value='<%# Eval("IDBigint") %>' />
                                                    <asp:HiddenField runat="server" ID="uoHiddenFieldSeqNo" Value='<%# Eval("SeqNo") %>' />
                                                    
                                                    <asp:HiddenField runat="server" ID="hfVesselID" Value='<%# Eval("VesselID") %>' />
                                                    <asp:HiddenField runat="server" ID="hfPortID" Value='<%# Eval("PortID") %>' />
                                                   
                                                    

                                                    <asp:HiddenField runat="server" ID="uoHiddenFieldLOEDate" Value='<%# String.Format("{0:dd-MMM-yyyy}", Eval("LOEDate"))%>' />
                                                    <asp:HiddenField runat="server" ID="uoHiddenFieldOfficer" Value='<%# Bind("LOEImmigrationOfficer") %>' />
                                                    <asp:HiddenField runat="server" ID="uoHiddenFieldPlace" Value='<%# Bind("LOEImmigrationPlace") %>' />
                                                    <asp:HiddenField runat="server" ID="uoHiddenFieldLOEReason" Value='<%# Bind("LOEReason") %>' />
                                                    
                                                    
                                                    
                                                    <asp:HiddenField runat="server" ID="uoHiddenFieldLongShip" Value='<%# Bind("Vessel") %>' />
                                                    <asp:HiddenField runat="server" ID="uoHiddenFieldRank" Value='<%# Bind("Rank") %>' />
                                                    <asp:HiddenField runat="server" ID="uoHiddenFieldBrand" Value='<%# Bind("Brand") %>' />

                                                </td>
                                              </tr>
                                        </ItemTemplate>
                                         <EmptyDataTemplate>
                                            <table  class="listViewTable">
                                                <tr>
                                                    <th style="text-align: center;  white-space: normal; width:100px;" >
                                                        Record Locator
                                                    </th >
                                                    <th style="text-align: center; white-space: normal;">
                                                        Ship
                                                    </th >
                                                    <th style="text-align: center; white-space: normal;">
                                                        Status
                                                    </th>
                                                    <th style="text-align: center; white-space: normal; width:100px; ">
                                                        Sign On/Off Date
                                                    </th>
                                                    <th style="text-align: center; white-space: normal;">
                                                        Port
                                                    </th>    
                                                    <th style="text-align: center; white-space:nowrap; width:75px; ">
                                                        Reason Code
                                                    </th> 
                                                    <th style="text-align: center; white-space:nowrap; width:75px; ">
                                                        Immigration Status
                                                    
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
                                                       />
                                               </td>
                                               <td class="leftAligned" style="white-space: normal;" >
                                                    <asp:Label runat="server" ID="lblSeqNo" Text='<%# Eval("SeqNo") %>'/>
                                                </td>
                                               <td class="leftAligned" style="white-space: normal;" >
                                                    <asp:Label runat="server" ID="lblAirline" Text='<%# Eval("AirlineCode")%>' />
                                                </td>
                                               <td class="leftAligned" style="white-space: nowrap;" >
                                                    <asp:Label runat="server" ID="lblDepartureDateTime"  Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DepartureDateTime"))%>'/>
                                                </td>
                                               <td class="leftAligned" style="white-space: nowrap;" >
                                                    <asp:Label runat="server" ID="lblDeptTime"   Text='<%# String.Format("{0:HH:mm tt}", Eval("DepartureDateTime"))%>'/>
                                                </td>
                                               <td class="leftAligned" style="white-space: nowrap;" >
                                                    <asp:Label runat="server" ID="lblArrivalDateTimeDate"  Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("ArrivalDateTime"))%>'/>
                                                </td>
                                               <td class="leftAligned" style="white-space: nowrap;" >
                                                    <asp:Label runat="server" ID="lblArrTime"   Text='<%# String.Format("{0:HH:mm tt}", Eval("ArrivalDateTime"))%>'/>
                                                </td>
                                               <td class="leftAligned">
                                                    <asp:Label runat="server" ID="lblDepartureAirportLocationCode" Text='<%# Eval("DepartureAirportLocationCode") %>'/>
                                                </td>
                                               <td class="leftAligned">
                                                    <asp:Label runat="server" ID="lblArrivalAirportLocationCode"  Text='<%# Bind("ArrivalAirportLocationCode")  %>'/>
                                                </td>
                                               <td class="leftAligned">
                                                    <asp:DropDownList runat="server" ID="uoDropDownListStatus" Width="69px" Style="margin:-3px; 
                                                        padding:-3px; font-size:xx-small;"   onchange="javascript:AirChangeStatus(this);" 
                                                        OnSelectedIndexChanged="uoDropDownListStatus_SelectedIndexChanged" AutoPostBack="true"
                                                        Visible='<%# Eval("OrderNo").ToString() == "1" ? true : false %>'> 
                                                        <asp:ListItem Text="Open" Value="1" Selected="True"/>
                                                        <asp:ListItem Text="Checked-In" Value="2" />
                                                        <asp:ListItem Text="Used" Value="3" />
                                                        <asp:ListItem Text="Suspended" Value="4" />
                                                    </asp:DropDownList>
                                                    <asp:Label runat="server" ID="lblStatus"  Text='<%# Eval("Status")  %>' Visible='<%# Eval("OrderNo").ToString() == "1" ? false : true %> '/>
                                                </td>
                                               <td style="white-space: normal; visibility:hidden;" >
                                                   <asp:HiddenField runat="server" ID="hfIDBigInt" Value='<%# Eval("IdBigint") %>' />
                                       
                                                   <asp:HiddenField runat="server" ID="uoHiddenFieldAirFligthNo" Value='<%# Bind("FligthNo") %>' />
                                                   
                                                    <asp:HiddenField runat="server" ID="uoHiddenFieldTravelReqID" Value='<%# Bind("TravelReqId") %>' />
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
                                         <asp:Button runat="server" ID="uoButtonAddHotel" 
                                          CssClass="SmallButton" Text="Add Hotel" style="display:none"/>
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
                                                AutoPostBack="true"  
                                            />
                                       </td>
                                         <td class="leftAligned" style="white-space: normal; display:none; width:130px;" >
                                            <asp:Label runat="server" ID="Label3" style="display:none;" />
                                        </td>
                                         <td class="leftAligned" style="white-space: normal; width:130px;" >
                                            <asp:Label runat="server" ID="lblHotelBook" Text='<%# Eval("VendorName")%>' />
                                        </td>
                                        <td class="leftAligned" style="white-space: nowrap;" >
                                            <asp:Label runat="server" ID="lblCheckInDate"  Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("TimeSpanStartDate"))%>'/>
                                        </td>
                                        <td class="leftAligned" style="white-space: nowrap;" >
                                            <asp:Label runat="server" ID="lblCheckOutDate"   Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("TimeSpanEndDate"))%>'/>
                                        </td>
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ID="lblRoomTypeID"  Text='<%# Eval("RoomType")%>'/>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;" >
                                             <asp:Label runat="server" ID="lblHotelNite"   Text='<%# Eval("TimeSpanDuration")%>'/>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal; width:30px;" >
                                            <asp:Label runat="server" ID="lblLabelHotelRate" ToolTip='<%# Eval("ConfirmRateMoney") %>'  Text='<%# String.Format("{0:#,##0.00}", Eval("ConfirmRateMoney"))%>' />
                                        </td>
                                         <td class="leftAligned" style="white-space: normal; width:70px;" >
                                            <asp:Label runat="server" ID="lblHStatus" ToolTip='<%# Eval("HotelStatus") %>'  Text='<%# Eval("HotelStatus") %>'/>
                                        
                                        </td>
                                        <td  style="white-space: normal; width:40px; text-align:center;" >
                                            <asp:CheckBox  runat="server" ID="Label9" Enabled="false"   Checked='<%# Eval("IsMedical") %>'/>
                                        
                                        </td>
                                        
                                        <td style="white-space:nowrap; vertical-align:middle; padding-top:3px; border-right-width:thin; text-align:center; margin:-0px; width:-1px;">
                                         
                                           <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="Click to Cancel" OnClientClick="return Confirmcancel(this)"
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
                                                 ToolTip="Click to Approve" 
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
                                           
                                           <asp:HiddenField runat="server" ID="hoAddress" Value='<%# Eval("Address") %>' />
                                           <asp:HiddenField runat="server" ID="hoTelephone" Value='<%# Eval("ContactNo") %>' />
                                           
                                           <asp:HiddenField runat="server" ID="hoContractRate" Value='<%# String.Format("{0:#,##0.00}", Eval("ContractedRateMoney"))%>'/>
                                           <asp:HiddenField runat="server" ID="hoConfirmRate" Value='<%# String.Format("{0:#,##0.00}", Eval("ConfirmRateMoney"))%>'/>
                                           <asp:HiddenField runat="server" ID="uoMealVoucher" Value='<%# String.Format("{0:#,##0.00}", Eval("VoucherAmount"))%>'/>
                                           
                                           <asp:HiddenField runat="server" ID="hoIsBreakfast" Value='<%# Eval("Breakfast") %>' />
                                           <asp:HiddenField runat="server" ID="hoIsLunch" Value='<%# Eval("Lunch") %>' />
                                           <asp:HiddenField runat="server" ID="hoIsDinner" Value='<%# Eval("Dinner") %>' />
                                           <asp:HiddenField runat="server" ID="hoIsAddMeal" Value='<%# Eval("LunchOrDinner") %>' />
                                           <asp:HiddenField runat="server" ID="hoIsWithShuttle" Value='<%# Eval("WithShuttle") %>' />
                                           <asp:HiddenField runat="server" ID="hoEmail" Value='<%# Eval("EmailTo") %>' />
                                           <asp:HiddenField runat="server" ID="hoComment" Value='<%# Eval("Comment") %>' />
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
                           
                              <table style=" width:100%;">
                                <tr>
                                    <td style="white-space:nowrap; width:100%;  ">
                                        <span  style="font-size:14px; padding-top:10px; ">
                                            <b>Transportation Booking</b>
                                        </span>
                                    </td>
                                    <td >
                                         <asp:Button runat="server" ID="uoButtonAddTranspo"  style="display:none;"  CssClass="SmallButton" 
                                                Text="Add New Transpo"  />
                                    </td>
                                </tr>
                            </table>
                            
                            
                            
                            <asp:Panel ID="uoPanelTransportation" runat="server" CssClass="ClassPanelTransportation" Width="540px">
                                <asp:ListView runat="server" ID="uoListViewTransportation" >
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
                                                <asp:Label runat="server" ID="lblRate" Text='<%#  String.Format("{0:#,##0.00}", Eval("ConfirmRateMoney"))%>' />
                                            </td>
                                            
                                            <td class="leftAligned" style="white-space:nowrap; width:80px;" >
                                                <asp:Label runat="server" ID="VehicleStatus" style="text-align: center; width:80px;"
                                                    Text='<%# Eval("VehicleStatus")%>' />
                                            </td> 
                                       
                                            <td style="white-space:nowrap; vertical-align:middle; padding-top:3px;  
                                                 text-align:left; margin:-0px; width:-1px;" >
                                                
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
                                               <%-- <asp:HiddenField runat="server" ID="uoHiddenFieldTranslID" Value='<%# Bind("ReqVehicleIDBigint") %>' />
                                               --%> 
                                                <asp:HiddenField runat="server" ID="uoHiddenFieldTransactionID" Value='<%# Bind("TransVehicleID") %>' />

                                                <asp:HiddenField runat="server" ID="uoHiddenFieldTIDBigint" Value='<%# Bind("IdBigint") %>' />
                                                <asp:HiddenField runat="server" ID="uoHiddenFieldTSeqNo" Value='<%# Bind("SeqNo") %>' />
                                                <asp:HiddenField runat="server" ID="uoHiddenFieldTTravelRequestID" Value='<%# Bind("TravelReqID") %>' />
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
                    <td style="width:100% ; vertical-align:top;   " >
                         <table style=" padding-top:-10px; width: 100%; margin-top:-5px; vertical-align:text-top;  " border="0">
                            <tr style="width:100%;">
                            
                                <td style=" width:50%;" >
                                    <a href="#TabHotel" id="navHotel"   class="nav">Hotel</a>
                                </td>
                                <td  style= "width:50%;">
                                    <a href="#Transportation" id="navTransportation" class="nav">Transportation</a>
                                   <a  style=" width:0%; display:none;" href="#PortAgent"  id="navPortAgent" class="nav">Service Provider</a>

                                  <a style=" width:0%; display:none;" href="#MeetandGreet" id="navMeetAndGreet" class="nav">Meet and Greet</a>

                                
                                </td>
                              <%--  <td  style=" width:0%; display:none;">
                                                                 </td>
                                <td  style=" width:0%;  display:none;">
                                                                  </td>--%>
                                
                            </tr>
                            
                            
                            <tr>
                                <td style="white-space:nowrap;" colspan="2" >
                                
                                    <div id="TabHotel" class="TabClass">
                                        <table style="border: 1px;" id="TabHotelTable" width="100%">
                                            <tr>
                                                <td colspan="1">
                                                    Hotel :
                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="uoDropDownListHotel" runat="server" Width="83.8%" 
                                                        AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="uoDropDownListHotel_SelectedIndexChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="1">
                                                    Address :
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="uoLabelAdress" runat="server"  Font-Bold="true"></asp:Label>
                                                    &nbsp; Telephone : &nbsp;<asp:Label ID="uoLabelTelephone" runat="server" Font-Bold="true"></asp:Label>
                                                </td>
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
                                                    <%--  <asp:RequiredFieldValidator ID="uoRFVCheckOut" ControlToValidate="uoTextBoxCheckoutDate"
                                                            ValidationGroup="Request" runat="server" ErrorMessage="required" Font-Bold="true" />
                                                     --%>
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
                                                    <asp:TextBox ID="uoTextContractedRate"  runat="server" Width="80px"
                                                      CssClass="ReadOnly"></asp:TextBox>
                                                    &nbsp;
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoTextContractedRate"
                                                        ErrorMessage="Amount is required." ValidationGroup="Validate">*</asp:RequiredFieldValidator>--%>
                                                    <%--  
                                                        <cc1:MaskedEditExtender ID="uoTextBoxEmergencyAmount_MaskedEditExtender" runat="server"
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                            CultureTimePlaceholder="" Enabled="True" Mask="99,999.99" MaskType="Number" InputDirection="RightToLeft"
                                                            TargetControlID="uoTextContractedRate">
                                                        </cc1:MaskedEditExtender>
                                                    --%>
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
                                                    <asp:TextBox ID="uoTextBoxComfirmRate" runat="server"  Width="80px"></asp:TextBox>
                                                </td>
                                                <td class="caption" style="width: 80px; white-space:nowrap">
                                                    Room Type :
                                                </td>
                                                <td class="value">
                                                    <asp:DropDownList ID="uoDropDownListRoomeType" runat="server" CssClass="TextBoxInput" onchange="SelectRoomType(this)"
                                                        Width="85px">
                                                        <asp:ListItem Text="Single" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Double" Value="2"></asp:ListItem>
                                                    </asp:DropDownList> 
                                                                                      
                                            </td>
                                            </tr>
                                            <tr>
                                                <td class="caption" style="vertical-align: top">
                                                    Meals:
                                                </td>
                                                <td class="value" colspan="3">
                                                    <div >
                                                        <asp:CheckBox ID="uoCheckboxBreakfast" runat="server" Text="Breakfast" />
                                                        &nbsp; &nbsp;
                                                        <asp:CheckBox ID="uoCheckboxLunch" runat="server" Text="Lunch"  />
                                                        &nbsp; &nbsp;
                                                        <asp:CheckBox ID="uoCheckboxDinner" runat="server" Text="Dinner"  />
                                                        &nbsp; &nbsp;
                                                        <asp:CheckBox ID="uoCheckBoxLunchDinner" runat="server" Text="Additional Meals" />
                                                        &nbsp; &nbsp;
                                                        <asp:CheckBox ID="uoCheckBoxIsWithShuttle" runat="server" Text=" With Shuttle"  />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <asp:CheckBox ID="CheckBoxEmail" Text="Email :" runat="server" onclick="checkboxEnable(this)" />
                                                </td>
                                                <td valign="top" colspan="3">
                                                    <asp:TextBox ID="uoTextBoxEmail"  runat="server"  Width="98.5%"  Height="83%" onkeyup="SaveHotel()"
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
                                                     <asp:TextBox ID="uoTextBoxComment" runat="server" CssClass="TextArea"  Height="88%" TextMode="MultiLine" onkeyup="SaveHotel()"
                                                           ToolTip="Click to select the request source" Rows="2" Width="98.5%" />
                                                </td>
                                            </tr>  
                                             <tr runat="server" id="rowPortAgent" >
                                             <td >
                                                   Confirm by : 
                                                </td>
                                                <td valign="top" colspan="3">
                                                     <asp:TextBox ID="uoTextBoxPortAgentConfirm" runat="server" Height="83%" TextMode="MultiLine" onkeyup="SaveHotel()"
                                                           Rows="2" Width="98.5%" />
                                                </td>
                                            </tr>                                  
                                        </table>
                                    </div>
                                    <div id="TabTransportation" class="TabClass" style="display:none; ">
                                            <table style="border: 1px; width:98%; " id="TabTransportationTable" >
                                                <tr>
                                                    <td colspan="1" style="width:80px;">
                                                        Transportation
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="uoDropDownListVehicleVendor" runat="server" Width="449px" 
                                                            AutoPostBack="true" AppendDataBoundItems = "true" OnSelectedIndexChanged="uoDropDownListVehicleVendor_OnSelectedIndexChanged"/>
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
                                                <tr>
                                                    <td colspan="4" >
                                                        <asp:Panel ID="Panel5" runat="server" Width="625px" ScrollBars="Auto" style="max-height:8em;height:8em;
                                                            padding:0px 5px 0px 2px; border-style:solid; border-width:thin; border-color:#C0C0C0;" >
                                                            <asp:ListView runat="server" ID="uoListViewTranportationRoute" border="0" cellpadding="0" cellspacing="0" 
                                                                    class="listViewTable" InsertItemPosition="LastItem"  
                                                                    OnItemInserting="uoListViewTranportationRoute_ItemInserting"
                                                                    OnItemDeleting="uoListViewTranportationRoute_ItemDeleting"
                                                                  >
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
                                                                            <asp:HiddenField ID="uoHiddenFieldTransID" runat="server" Value='<%# Eval("TransVehicleID")%>'/>
                                                                            <%--<asp:HiddenField ID="uoHiddenFieldReqTransID" runat="server" Value='<%# Eval("ReqVehicleIDBigint")%>'/>--%>
                                                                            <asp:HiddenField ID="HiddenFieldRouteIDFromInt" runat="server"  Value='<%# Eval("RouteIDFrom")%>'/>
                                                                                           
                                                                            <asp:Label runat="server" ID="uoLabelOrigin" Text='<%# Eval("From")%>'></asp:Label>
                                                                        </td>
                                                                        <td class="leftAligned" >
                                                                            <asp:HiddenField ID="HiddenFieldRouteIDToInt" runat="server" Value='<%# Eval("RouteIDTo")%>'/>
                                                                            <asp:Label runat="server" ID="uoLabelTO"  Text='<%# Eval("To")%>'></asp:Label>
                                                                        </td>
                                                                        <td class="leftAligned" >
                                                                            <asp:Label runat="server" ID="uoLabelRouteFrom" Text='<%# Eval("RouteFrom")%>'></asp:Label>
                                                                        </td>
                                                                        <td class="leftAligned" > 
                                                                             <asp:Label runat="server" ID="uoLabelRouteTo" Text='<%# Eval("RouteTo")%>'></asp:Label>
                                                                        </td>
                                                                        <td class="leftAligned" > 
                                                                             <asp:Label runat="server" ID="uoLabelCost" Text='<%#  String.Format("{0:#,##0.00}", Eval("ConfirmRateMoney"))%>' ></asp:Label>
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
                                                                                Visible='<%# Convert.ToBoolean((Eval("StatusID").ToString() == "1") ?  1 : 0) %>'
                                                                                />
                                                                        </td> 
                                                                    </tr>
                                                                </ItemTemplate>
                                                             
                                                                <InsertItemTemplate>
                                                                    <tr>
                                                                       <tr>
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:DropDownList ID="uoDropDownListTFrom" runat="server"  Width="110px"  onchange="GetSelectedTranspoRoute(this);" >
                                                                                    <asp:ListItem Text="--Select Route--" Value="0"/> 
                                                                                    <asp:ListItem Text="Ship" Value="1"/> 
                                                                                    <asp:ListItem Text="Hotel" Value="2"/> 
                                                                                    <asp:ListItem Text="Airport" Value="3"/> 
                                                                                    <asp:ListItem Text="Other" Value="4"/> 
                                                                                    <asp:ListItem Text="Office" Value="5"/> 
                                                                            </asp:DropDownList>
                                                                                   
                                                                                   
                                                                        </td>
                                                                             
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:DropDownList ID="uoDropDownListTTo" runat="server" Width="110px" >
                                                                                    <asp:ListItem Text="--Select Route--" Value="0"/> 
                                                                                    <asp:ListItem Text="Ship" Value="1"/> 
                                                                                    <asp:ListItem Text="Hotel" Value="2"/> 
                                                                                    <asp:ListItem Text="Airport" Value="3"/> 
                                                                                    <asp:ListItem Text="Other" Value="4"/> 
                                                                                    <asp:ListItem Text="Office" Value="5"/> 
                                                                                </asp:DropDownList>
                                                                           
                                                                                   
                                                                        </td>
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox ID="uoTextBoxTFrom" width="95%"  runat="server"></asp:TextBox>
                                                                        </td>  
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox ID="uoTextBoxTTo" width="95%"   runat="server"></asp:TextBox>
                                                                        </td>    
                                                                        <td style="white-space:normal; width:30px; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox runat="server" ID="uoTextBoxCost" width="95%" 
                                                                                         style="text-align:right" Text=""  ></asp:TextBox>
                                                                        </td> 
                                                                        <td style="white-space:normal;width:70px; padding:0px 1px 0px 1px;">
                                                                            <asp:TextBox ID="uoTextBoxTPickupdate"  runat="server" Text='<%# System.DateTime.Now %>' width="95%"/>
                                                                             
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
                                                                            <asp:TextBox runat="server" ID="uoTextBoxTTime" width="95%" Text="00:00" ></asp:TextBox>
                                                                            
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
                                                                 <table border="0" id="uoListviewPATranspoCost" cellpadding="0" cellspacing="0" class="listViewTable" >
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
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                           <%-- <asp:DropDownList ID="uoDropDownListTFrom" runat="server" Width="120px"  
                                                                               SelectedValue='<%# Bind("RoutId")%>'
                                                                                AppendDataBoundItems ="true"  />--%>
                                                                        </td>
                                                                             
                                                                        <td style="white-space:nowrap; padding:0px 1px 0px 1px;">
                                                                          <%--  <asp:DropDownList ID="uoDropDownListTTo" runat="server" Width="120px" 
                                                                                    AppendDataBoundItems ="true"
                                                                                    SelectedValue='<%# Bind("RoutId")%>'
                                                                                    onchange="SavePortAgent(this)" />--%>
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
                                                                            <asp:ImageButton ID="btnAddTranpoRoute" runat="server" style="size:20px; width:20px; height:20px;" ImageUrl="~/Images/Add.ico" 
                                                                               />
                                                                        </td>          
                                                                    </tr> 
                                                                    </table>
                                                                 
                                                                 </EmptyDataTemplate>
                                                            </asp:ListView>
                                                            <asp:ObjectDataSource ID="uoObjectDataSourceRoute" runat="server"   TypeName="TRAVELMART.CrewAssist"></asp:ObjectDataSource>
                                                                
                                                        </asp:Panel>
                                                    
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5"   >
                                                        <table style="width: 100%; border-color:Gray;">
                                                            <tr >
                                                                <td valign="top" colspan="1">
                                                                    <asp:CheckBox ID="uoCheckBoxEmailTrans" Text="Email :" onclick="SaveTransportation(this)" runat="server" click="checkboxEnable(this)" />
                                                                </td>
                                                                <td style="vertical-align: top; "  colspan="3">
                                                                    <asp:TextBox ID="uoTextBoxEmailTrans" 
                                                                        onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                                                                        ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                                                                        onchange="SaveTransportation(this)" Enabled="false" runat="server" Style="margin-top:-2px;"
                                                                        onkeyup="SaveTransportation(this)"
                                                                        TextMode="MultiLine" Rows="2"   Width="98%" />
                                                                </td>
                                                                <td rowspan="3"  style="vertical-align:top; width:45%; background-color:Gray;  ">
                                                                    <div id="TranspoCost" style="border-style:solid; width:100%;   border-width:thin; border-color:#C0C0C0; z-index: 100;" >
                                                                        <asp:Panel ID="Panel4"  ScrollBars="Auto" runat="server" Width="284px" Height="120px" TabIndex="100" 
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
                                                                                            <asp:CheckBox CssClass="Checkbox" style="margin-left:0px;" ID="uoCheckBoxVCost" runat="server" 
                                                                                            onclick="javascript:GetVehicleType(this);" />  
                                                                                        </td>
                                                                                        
                                                                                        <td class="leftAligned" >
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
                                                                     <button id="uoButtonVehicleComment" title="View all Comment"  style="height:15px; vertical-align:top;" />
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="uoTextBoxTransComment" runat="server" onkeyup="SaveTransportation(this)" 
                                                                       CssClass="TextArea" TextMode="MultiLine" Rows="2"  Width="98%"/>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" style="width:80px;">
                                                                    Confirm By :
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="uoTextBoxTranpComfirmby" runat="server" onkeyup="SaveTransportation(this)"  Width="98%"/>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    
                                                    
                                                    </td>
                                                    
                                                </tr>
                                            </table>
                                            
                                             
                                            
                                      </div>
                                     
                                    <div id="TabPortagent"  class="TabClass" style="display:none;">
                                        <table>
                                            <tr>
                                                <td colspan="1">
                                                    Service Provider :
                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="uoDropDownListPortAgent" onchange="SavePortAgent(this)"
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
                                                                    <asp:CheckBox ID="uoCheckBoxPAMAG" onclick="SavePortAgent(this)" Font-Bold="false" Text="Meet and Greet" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="uoCheckBoxPATrans" onclick="SavePortAgent(this)" Font-Bold="false" Text="Transportation" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="uoCheckBoxPAHotel" onclick="SavePortAgent(this)" Font-Bold="false" Text="Hotel" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="uoCheckBoxPALuggage" onclick="SavePortAgent(this)" Font-Bold="false" Text="Luggage" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="uoCheckBoxPASafeguard" onclick="SavePortAgent(this)" Font-Bold="false" Text="Safeguard" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="uoCheckBoxPAVisa" onclick="SavePortAgent(this)" Font-Bold="false" Text="Visa" runat="server" />
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
                                                    <asp:TextBox ID="uoTextBoxPAComment" CssClass="TextArea"  runat="server" onkeyup="SavePortAgent(this)" Width="93%" Height="83%" TextMode="MultiLine"
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
                                     
                                    <div id="TabMeetAndGreet" class="TabClass" style="display:none;  " >
                                        <table>
                                            <tr>
                                                <td>
                                                    Vendor :
                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="uoDropDownListMAndGVendor" Width="449px" runat="server" AutoPostBack="true"
                                                       AppendDataBoundItems="true">
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
                                                    <asp:TextBox ID="uoTextBoxMAGComment" CssClass="TextArea"  runat="server" Width="95%" Height="83%" TextMode="MultiLine"
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
                                     
                                </td>
                            </tr>
                            <tr>
                                <td style="white-space:nowrap;" colspan="2" >
                                    <span style="font-size: 14px; padding-top: 10px;"><b>CC Email </b>&nbsp; &nbsp;
                                    <asp:CheckBox ID="uoCheckBoxCopyAll" runat="server" Text="Copy All" onclick="checkboxEnable(this)"/>
                                        &nbsp; </span>&nbsp;
                                    <asp:CheckBox ID="CheckBoxCopycrewassist" runat="server" 
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
                                            runat="server" TextMode="MultiLine" Rows="1" Width="85%"  />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="white-space:nowrap;" colspan="2" >
                                
                                    <asp:Button runat="server" ID="uoButtonFinish" CssClass="SmallButton" Text="Finish"
                                        OnClientClick="return ProcessCrewAssistRequest();" OnClick="ButtonSend_OnClick"/>
                                         &nbsp;
                                        
                                         &nbsp;
                                        <asp:Label ID="Label5"  CssClass="RedNotification" runat="server" 
                                        Text="Multiple emails should be separated by semicolon (i.e.  abc@rccl.com;xyz@rccl.com)"> 
                                        </asp:Label> 
                                </td>
                            </tr>
                            
                        </table >
                    
                    </td>
                </tr>
                <tr style="height: 2px;display:none ">
                    <td>
                        <asp:CheckBox ID="uoCheckBoxAddCompanion" runat="server" Text="Add Companion/s" />
                    </td>
                </tr>
             
             </table>
            <div id="DivCompanion" >
             <table id="uoTableCompanion" style="text-align: left; width: 100%" class="LeftClass">
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
                                            ValidationGroup="Companion" />
                                            
                                
                                          
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 100%; vertical-align: top; text-align: left">
                            <asp:ListView runat="server" ID="uoListViewCompanionList"  >
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
            </div>         
            <div id="immigration"  style="background-color:White;
                  display:none; position:absolute; text-align:left;  padding:5px 10px 5px 10px;">
            </div>
              
             <div id="RemarkDiv" class="RemarkDiv" style="background-color:White; position:absolute;display:none;  padding:5px 10px 5px 10px; ">
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
                                                margin:5px; padding:8px; width:325px; 
                                                 max-width:330px;"
                                            >
                                        <asp:HiddenField ID="uoHiddenFieldGridRemarkID" runat="server" Value='<%# Eval("RemarkID")%>'/>
                                        <asp:HiddenField ID="uoHiddenFieldRemarkTRID" runat="server" Value='<%# Eval("TravelRequestID")%>'/>
                                        <asp:HiddenField ID="uoHiddenFieldResourceID" runat="server" Value='<%# Eval("ReqResourceID")%>'/>
                            
                                        <asp:Label runat="server" ID="lblUserName"   Text='<%# Eval("RemarkBy") %>'
                                            style="font-style:normal; font-size:small; font-weight:bold;"/>
                                        <br/>
                                        <asp:Label runat="server" ID="uoLabelRemark" Width="315px" 
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
            
            <div id="DivCrewAssistComment" class="DivCrewAssistComment"  
                    style="background-color:White; padding:5px 10px 5px 10px; display:none;">
                 <div class="PageTitle"  style="overflow:hidden; white-space:nowrap; ">
                   Request Source
                </div>
                <hr />
                <asp:RadioButtonList ID="uoRadioButtonListComment" CssClass="ResourceOption" style="margin-left:-10px; text-align:left;" runat="server">         
                    <asp:ListItem  Text="Via Email" Value="1"/>
                    <asp:ListItem  Text="Via Call" Value="2"/>
                    <asp:ListItem  Text="Via Live Chat" Value="3"/>
                </asp:RadioButtonList>
            </div>
            
          
                 
            <cc1:ModalPopupExtender ID="mp1" runat="server" 
                        PopupControlID="Panl1" TargetControlID="btnPrimaryHotel"
                        CancelControlID="ButtonClose" BackgroundCssClass="ComfirmBackground" 
                        OkControlID="ButtonSend"   BehaviorID="mp1Behavior"
                        
                        OnCancelScript="HotelComfirmationCancel()" 
                        OnOkScript="HotelComfirmationOK()"
                        
                        >
            </cc1:ModalPopupExtender>
                
                
                
            <asp:Panel ID="Panl1" runat="server" CssClass="PrimaryHotelComfirmation" align="center" style = "display:none">
                    <table style="width: 100%;">
                        <tr class="PageTitle">
                            <td colspan="2">Confirm</td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                       <tr>
                            <td style="width: 250px; white-space:nowrap;">
                                <asp:label  ID="uoLabelContractRate"  runat="server" Text="Was the contracted rate of $0.00 Confimed"/>
                            </td>
                            <td >
                               <asp:CheckBox ID="CheckBoxYes" onclick='ConfirmCheckRate(this)' Checked="false" runat="server" Text="Yes" />&nbsp;
                               <asp:CheckBox ID="CheckBoxNo" onclick='ConfirmCheckRate(this)' Checked="false" runat="server" Text="No" />&nbsp;
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="white-space:nowrap;">
                                <asp:label ID="Label2" runat="server" Text=" Name of the hotel agent who confimed	"/>
                            </td>
                            <td style="width:98%;">
                                <asp:TextBox ID="txtWhoConfirm" runat="server" style="width:98%;"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="white-space:nowrap;">
                               Confirmed rate  
                            </td>
                            <td >
                                 <asp:TextBox ID="txtConfirmrate" runat="server" Enabled="false" style="width:98%;"></asp:TextBox>
                            </td>
                        </tr>
                     
                        <tr>
                           <td  colspan="2"  align="right">
                                <asp:Button ID="ButtonSend" runat="server" Text="Send" OnClientClick="SaveHotelBooking()"  />
                                <asp:Button ID="ButtonClose" runat="server" Text="Cancel"  />
                            </td>
                        </tr>
                        
              </table>
            
            </asp:Panel>
            
            
             <div id="DivRemarkType"  class="DivRemarkType" style="background-color:White;position:absolute;  
                             border-style:solid; border-width:thin;  padding:5px 10px 5px 10px; display:none;">
               <table style="width:100%;">
                    <tr>
                        <td style="white-space:nowrap;" ><asp:Label ID="lblRemarkType" runat="server" Text="Call" />&nbsp;Type:</td>
                        <td style="width:100%"><asp:DropDownList ID="cboRemarkType" Width="100%"  runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap" >Trans Status:</td>
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
                            
                            <cc1:CalendarExtender ID="CalendarExtender1" BehaviorID="CalendarExtenderTransdate" runat="server"
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
                            <asp:TextBox runat="server" ID="uoTextBoxRemTransTime"  Width="70px"  ToolTip=" 24 hr format (e.g. 22:30)" ></asp:TextBox>
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
                        <td  colspan="2" >Summary Of Calls.</td>
                    </tr>
                     <tr>
                        <td colspan="2" >
                            <asp:TextBox ID="txtSummaryCall" runat="server" style="border-color:#C0C0C0;" TextMode="MultiLine" Height="60px" Width="98%" />
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
            
            
            
        
            <asp:HiddenField ID="uoHiddenFieldUser" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldHotelTransID" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldFlightStatus" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldExpendType" runat="server" value="0"/>
            
            <asp:HiddenField ID="uoHiddenFieldTravelRequestID" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldIDBigint" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldSeqNo" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldHSourceRequest" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldCityCode" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldPortCode" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldContractStart" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldContractEnd" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldContractID" runat="server" value=""/>
            
            
            <asp:HiddenField ID="uoHiddenFieldHotelRequestID" runat="server" value=""/>
            
            <asp:HiddenField ID="uoHiddenFieldCrewHotelCancelPopup" runat="server" value="0"/>
            <%--<asp:HyperLink ID="uoHiddenFieldFinishHotel" runat="server" value="0" class="HotelConfirmation" />--%>
            <asp:Button ID="btnPrimaryHotel" runat="server" Text="Send" style="display:none;"  />
         
            <asp:HiddenField ID="uoHiddenFieldWhoConfirm" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldConfirmRate" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldNoOfNites" runat="server" value=""/>
          
            <asp:HiddenField ID="uoHiddenFieldRemarkID" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldRSourceRequest" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldArrCode" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldRecordLocator" runat="server" value=""/>
            <asp:HiddenField ID="uoHiddenFieldVTypeID" runat="server" value=""/>
        </ContentTemplate>
       
    </asp:UpdatePanel>
                             
            <asp:Button ID="uoButtonPrimaryHotel" runat="server"  TabIndex="-1"  Text="Send" style="display:none;" OnClick="ButtonSend_OnClick"   />
            <asp:Button ID="uoButtonLoadHotel" runat="server"  TabIndex="-1"  Text="Load Hotel" style="display:none;" OnClick="uoButtonLoadHotel_OnClick"   />
       
           
     
    <script type="text/javascript" language="javascript">

        $(document).ready(function() {

            HideDate();
            ShowRemarkLis();
            navigate();
            HotelComfirmationCancel();
            HotelComfirmationOK();
            KeyUpText();

        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                HideDate();
                ShowRemarkLis();
                navigate();
                HotelComfirmationCancel();
                HotelComfirmationOK();
                KeyUpText()
            }
        }


        function SaveHotelBooking() {
            document.getElementById('ctl00_NaviPlaceHolder_uoButtonPrimaryHotel').click();
        }
 
        function HotelComfirmationCancel() {
 
            document.getElementById('<%=txtWhoConfirm.ClientID%>').value = "";
            document.getElementById('<%=txtConfirmrate.ClientID%>').value = "";
            document.getElementById('<%=CheckBoxNo.ClientID%>').checked = false;
            document.getElementById('<%=CheckBoxYes.ClientID%>').checked = false;
            document.getElementById("<%= txtConfirmrate.ClientID %>").disabled = true;
        }

        function HotelComfirmationOK() { 
        
            document.getElementById('<%=txtWhoConfirm.ClientID%>').value = "";
            document.getElementById('<%=txtConfirmrate.ClientID%>').value = "";
            document.getElementById('<%=CheckBoxNo.ClientID%>').checked = false;
            document.getElementById('<%=CheckBoxYes.ClientID%>').checked = false;
            document.getElementById("<%= txtConfirmrate.ClientID %>").disabled = true;
 
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




        function HideDate() {



            $("[id$=uoLabelLOE]").hover(function() {

                var offset = $(this).offset();
                var xPos = offset.left;
                var yPos = offset.top;

                var l1 = this.id.replace('ctl00_NaviPlaceHolder_uoListviewTravel_ctrl', '');
                var l2 = l1.replace('_uoLabelLOE', '');

                $("#immigration").css({ top: yPos, left: xPos });

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
                $("#immigration").hide();
                $("#immigration").empty();
            });

        }
       
        function ShowRemarkLis() {

            $("#btnShowRemark2").mouseover(function() {

                var offset = $(this).offset();
                var xPos = offset.left;
                var yPos = offset.top;

                $("#RemarkDiv").css({ top: yPos, left: xPos });
                $("#RemarkDiv").show();

            });

            $("#RemarkDiv").mouseleave(function() {
                $("#RemarkDiv").hide();
            });


            $(".TextArea").click(function(event) {

                var vtop = event.pageY;
                var vleft = event.pageX;
                if (this.id == 'ctl00_NaviPlaceHolder_txtRemark') {
                    $(".DivCrewAssistComment").css({ top: $("#<%=txtRemark.ClientID %>").offset().top + 120, left: $("#<%=txtRemark.ClientID %>").offset().left + 200 });
                    ShowRemarkPopUp();
                }
                else {

                    $(".DivCrewAssistComment").css({ top: vtop + 110, left: vleft + 200 });

                }
                $('input:radiobutton[type*=radio]').attr('checked', false);
                $(".DivCrewAssistComment").show();
                $('div[class=DivCrewAssistComment]').attr('id', this.id);

            });



            $("#<%=uoRadioButtonListComment.ClientID%> input").change(function() {
                document.getElementById('ctl00_NaviPlaceHolder_uoHiddenFieldRSourceRequest').value = this.value;
            });


            function ShowRemarkPopUp() { 
            
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

                            var myMinute = mydate.getMinutes();
                            if (myMinute < 10)
                                myMinute = '0' + myMinute;

                            var myTime = myMinute + ':' + myMinute;
                            TransTimeExtender.set_Text(myTime);

                        }

                        var containerPos = $("#<%=txtRemark.ClientID %>").offset();

                        $("#DivRemarkType").css({ top: containerPos.top, left: containerPos.left, width: document.getElementById('TableRemark').offsetWidth - 28.5 });
                        $("#DivRemarkType").show();
            
            
            }





            $(".ResourceOption").click(function() {
                if ($('div[class=DivCrewAssistComment]').attr('id') == 'ctl00_NaviPlaceHolder_txtRemark') {
                    ShowRemarkPopUp();
                }
                else if ($('div[class=DivCrewAssistComment]').attr('id') == 'ctl00_NaviPlaceHolder_uoTextBoxComment') {
                    document.getElementById('ctl00_NaviPlaceHolder_uoTextBoxComment').focus();
                }
                else if ($('div[class=DivCrewAssistComment]').attr('id') == 'ctl00_NaviPlaceHolder_uoTextBoxTransComment') {
                    document.getElementById('ctl00_NaviPlaceHolder_uoTextBoxTransComment').focus();
                }
                else if ($('div[class=DivCrewAssistComment]').attr('id') == 'ctl00_NaviPlaceHolder_uoTextBoxPAComment') {
                    document.getElementById('ctl00_NaviPlaceHolder_uoTextBoxPAComment').focus();
                }
                else if ($('div[class=DivCrewAssistComment]').attr('id') == 'ctl00_NaviPlaceHolder_uoTextBoxMAGComment') {
                    document.getElementById('ctl00_NaviPlaceHolder_uoTextBoxMAGComment').focus();
                }

                $(".DivCrewAssistComment").fadeOut();
                $('div[class=DivCrewAssistComment]').attr('id', 'DivCrewAssistComment');

            });

            $("#btnRamarkTypeAdd").click(function() {
                if ($("#<%=txtSummaryCall.ClientID %>").val() == "") {
                    alert("Please enter summary of call for reporting purposes!!!");
                }
                else {
                    $("#<%=txtRemark.ClientID %>").val($("#<%=txtSummaryCall.ClientID %>").val());
                    $("#<%=txtRemark.ClientID %>").focus();
                    $("#DivRemarkType").hide();
                }
            });

            $("#btnRemarkTypeClose").click(function() {
            
            
                $("#<%=txtRemark.ClientID %>").focus();
                $("#DivRemarkType").hide();
            });

        }

        function clickConfirm() {

            var r = confirm('Do you want to proceed with the booking?');
            
            
            
            
            if (r != true) {
                return false;
            }
            return true;
        }

        function ConfirmCheckRate(val) {

            if (val.id == 'ctl00_NaviPlaceHolder_CheckBoxYes') {
                val.checked == true ? document.getElementById("<%= txtConfirmrate.ClientID %>").disabled = false : document.getElementById("<%= txtConfirmrate.ClientID %>").disabled = true;
                document.getElementById("<%= CheckBoxNo.ClientID %>").checked = false;

            }
            else if (val.id == 'ctl00_NaviPlaceHolder_CheckBoxNo') {
                document.getElementById("<%= txtConfirmrate.ClientID %>").disabled = true;
                document.getElementById("<%= CheckBoxYes.ClientID %>").checked = false;
            }
            else {
                document.getElementById("<%= txtConfirmrate.ClientID %>").disabled = true;
            }






        }




        function navigate() {

            $(".nav").click(function() {

                $(".TabClass").hide();
                $(".nav").css("background-color", "#ECDFC4");
                $(".nav").css("color", "#0288D8");

                if (this.id == "navHotel") {
                    $("#TabHotel").show();
                    $("#navHotel").css("background-color", "#0288D8");
                    $("#navHotel").css("color", "white");
                    $("[id$=uoHiddenFieldExpendType]").val(0);
                }
                if (this.id == "navTransportation") {
                    $("#TabTransportation").show();
                    $("#navTransportation").css("background-color", "#0288D8");
                    $("#navTransportation").css("color", "white");
                    $("[id$=uoHiddenFieldExpendType]").val(1);
                }
                if (this.id == "navPortAgent") {
                    $("#TabPortagent").show();
                    $("#navPortAgent").css("background-color", "#0288D8");
                    $("#navPortAgent").css("color", "white");
                    $("[id$=uoHiddenFieldExpendType]").val(2);
                }
                if (this.id == "navMeetAndGreet") {
                    $("#TabMeetAndGreet").show();
                    $("#navMeetAndGreet").css("background-color", "#0288D8");
                    $("#navMeetAndGreet").css("color", "white");
                    $("[id$=uoHiddenFieldExpendType]").val(3);
                }

            });


            $(function() {

                $(".TabClass").hide();
                if ($("[id$=uoHiddenFieldExpendType]").val() == "1") {
                    $("#TabTransportation").show();
                    $("#navTransportation").css("background-color", "#0288D8");
                    $("#navTransportation").css("color", "white");
                }
                else if ($("[id$=uoHiddenFieldExpendType]").val() == "2") {
                    $("#TabPortagent").show();
                    $("#navPortAgent").css("background-color", "#0288D8");
                    $("#navPortAgent").css("color", "white");
                }
                else if ($("[id$=uoHiddenFieldExpendType]").val() == "3") {
                    $("#TabMeetAndGreet").show();
                    $("#navMeetAndGreet").css("background-color", "#0288D8");
                    $("#navMeetAndGreet").css("color", "white");
                }
                else {
                    $("#TabHotel").show();
                    $("#navHotel").css("background-color", "#0288D8");
                    $("#navHotel").css("color", "white");
                }

                if ($("[name*=uoCheckBoxAddCompanion]").is(":checked")) {
                    $("#DivCompanion").show();
                }
                else {
                    $("#DivCompanion").hide();
                }


                document.getElementById("<%= txtConfirmrate.ClientID %>").value = "";
                document.getElementById("<%= txtWhoConfirm.ClientID %>").value = "";



            });

            $("[name*=uoCheckBoxAddCompanion]").click(function() {
                if ($(this).is(':checked') == true)
                    $("#DivCompanion").show();
                else
                    $("#DivCompanion").hide();
            });


            $("[name*=uoCheckBoxsHotelTransaction]").click(function() {

                var myCheck = this.checked;

                $("[name*=uoCheckBoxsHotelTransaction]").attr('checked', false)







                this.checked = true;
                if (myCheck == true) {

                    var Num = this.id.replace("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl", "");
                    var rowNum = String(Num).substring(0, 1);

                    console.log(GetDate(document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_lblCheckOutDate").innerHTML));

                    document.getElementById("<%= uoTextBoxCheckinDate.ClientID %>").value = GetDate(document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_lblCheckInDate").innerHTML);
                    document.getElementById("<%= uoTextBoxCheckoutDate.ClientID %>").value = GetDate(document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_lblCheckOutDate").innerHTML);

                    document.getElementById("<%= uoTextBoxDuration.ClientID %>").value = document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_lblHotelNite").innerHTML;

                    
                    document.getElementById("<%= uoTextBoxEmail.ClientID %>").value = document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_hoEmail").value;
                    document.getElementById("<%= uoTextBoxComment.ClientID %>").value = document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_hoComment").value;

                    document.getElementById("<%= uoCheckboxBreakfast.ClientID %>").checked = document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_hoIsBreakfast").value == 'True' ? true : false;
                    document.getElementById("<%= uoCheckboxLunch.ClientID %>").checked = document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_hoIsLunch").value == 'True' ? true : false;
                    document.getElementById("<%= uoCheckboxDinner.ClientID %>").checked = document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_hoIsDinner").value == 'True' ? true : false;
                    document.getElementById("<%= uoCheckBoxLunchDinner.ClientID %>").checked = document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_hoIsAddMeal").value == 'True' ? true : false;
                    document.getElementById("<%= uoCheckBoxIsWithShuttle.ClientID %>").checked = document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_hoIsWithShuttle").value == 'True' ? true : false;
                    
                    
                    document.getElementById("<%= uoTextContractedRate.ClientID %>").value =  document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_hoContractRate").value;
                    document.getElementById("<%= uoTextBoxComfirmRate.ClientID %>").value = document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_hoConfirmRate").value;
                    document.getElementById("<%= uoTextBoxMealVoucher.ClientID %>").value = document.getElementById("ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl" + rowNum + "_uoMealVoucher").value;
                }

            });

        }



        function GetSelectedTranspoRoute(val) {
            //get label controls to set value/text


            var SelectedValue = val.options[e.selectedIndex].value;



            if (val.id == 'uoDropDownListTFrom') {

                switch (val.selectedValue) {
                    case 1:

                        break;
                    case 2:
 

                }


            }

            if (val.id == 'uoDropDownListTTo') {

            }


            //                
            //                
            //                var lblSelectedText = document.getElementById("lblSelectedText");
            //                var lblSelectedValue = document.getElementById("lblSelectedValue");
            //                //get selected value and check if subject is selected else show alert box
            //                var SelectedValue = e.options[e.selectedIndex].value;
            //                if (SelectedValue > 0) {
            //                    //get selected text and set to label
            //                    var SelectedText = e.options[e.selectedIndex].text;
            //                    lblSelectedText.innerHTML = SelectedText;

            //                    //set selected value to label
            //                    lblSelectedValue.innerHTML = SelectedValue;
            //                } else {
            //                    //reset label values and show alert
            //                    lblSelectedText.innerHTML = "";
            //                    lblSelectedValue.innerHTML = "";
            //                    alert("Please select valid subject.");
            //                }
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

            if (val.value == 4 || (val.value == 1 && Status != 'Open')) {
                Iscancel = true;
            }

            var r = confirm((Iscancel == true ? msg : '') + ' ' + res + ' ' + val.options[val.selectedIndex].text + '?');
            if (r == true) {
                document.getElementById("<%= uoHiddenFieldFlightStatus.ClientID %>").value = 'true';
            }
        }

        function ProcessCrewAssistRequest() {


            if ($("[id$=uoHiddenFieldExpendType]").val() == '0') {


                var hotelName = document.getElementById("<%=uoDropDownListHotel.ClientID%>").options[document.getElementById("<%=uoDropDownListHotel.ClientID%>").selectedIndex].text
                if (hotelName.substring(0, 2) == 'HT') {

                    var val = clickConfirm();
                    if (val == true) {

                        $get('<%=txtConfirmrate.ClientID%>').value = document.getElementById('ctl00_NaviPlaceHolder_uoTextBoxComfirmRate').value;
                        $get('<%=txtWhoConfirm.ClientID%>').value = '';
                        return true;

                    }
                    return false;
                }
                if (hotelName.substring(0, 2) == 'PA') {
                    var val = clickConfirm();
                    if (val == true) {
                        document.getElementById('ctl00_NaviPlaceHolder_uoButtonPrimaryHotel').click();
                    }
                }
                else {
                    alert("Please Select Hotel vendor!!!");
                    return false;
                }
            }
            else {
                document.getElementById('ctl00_NaviPlaceHolder_uoButtonPrimaryHotel').click();
                
            }
            
            
            
            
            
        
        }


        function GetDate(val) {

            var mydate = val.split("-");
            var month = 1;
            switch (mydate[1]) {
                case 'Jan': month = 01;
                    break;
                case 'Feb': month = 02;
                    break;
                case 'Mar': month = 03;
                    break;
                case 'Apr': month = 04;
                    break;
                case 'May': month = 05;
                    break;
                case 'Jun': month = 06;
                    break;
                case 'Jul': month = 07;
                    break;
                case 'Aug': month = 08;
                    break;
                case 'Sep': month = 09;
                    break;
                case 'Oct': month = 10;
                    break;
                case 'Nov': month = 11;
                    break;
                case 'Dec': month = 12;
                    break;
            }

            return month + '/' + mydate[0] + '/' + mydate[2];

        }



        function checkboxEnable(obj) {

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
            }

            else if (obj.id == "ctl00_NaviPlaceHolder_uoCheckBoxEmailTrans") {
                if (obj.checked == true) {
                    document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxEmailTrans").disabled = false;
                }
                else {
                    document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxEmailTrans").disabled = true;
                }
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

                }
            }

            var rType = document.getElementById('ctl00_NaviPlaceHolder_cboRemarkType');
            var rStatus = document.getElementById('ctl00_NaviPlaceHolder_cboRemarkStatus');
            document.getElementById('ctl00_NaviPlaceHolder_txtSummaryCall').value = SumCall;
            var rReq = document.getElementById('ctl00_NaviPlaceHolder_cboRemarkRequestor');

            document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxRemTransTime").value = TTIme;
            document.getElementById('ctl00_NaviPlaceHolder_uoTextBoxRemTransdate').value = TDate;

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


        function OnDateChange(fromControl) {
            if (fromControl == 'CheckIn') {
                ValidateIfPastDate($("#<%=uoTextBoxCheckinDate.ClientID %>").val(), fromControl);

            }
            else if (fromControl == 'CheckOut') {
                ValidateIfPastDate($("#<%=uoTextBoxCheckoutDate.ClientID %>").val(), fromControl);
            }


//            var hotel = document.getElementById("ctl00_NaviPlaceHolder_uoDropDownListHotel");
//            var BID = hotel.options[hotel.selectedIndex].value;
//            var BName = hotel.options[hotel.selectedIndex].text;
//            var LT = 0;
//            console.log(BName);
//            if (BName.substring(0, 2) == 'HT') {
//                LT = 1;
//            }
//            else {
//                LT = 0;
//            }


//            var RID = val.options[val.selectedIndex].value; ;


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



        function SelectRoomType(val) {

            var hotel = document.getElementById("ctl00_NaviPlaceHolder_uoDropDownListHotel");
            var BID = hotel.options[hotel.selectedIndex].value;
            var BName = hotel.options[hotel.selectedIndex].text;
            var LT = 0;
            console.log(BName);
            if (BName.substring(0, 2) == 'HT') {
                LT = 1;
            }
            else {
                LT = 0;
            }
           
            var RID = val.options[val.selectedIndex].value; ;
            var cdate = document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxCheckinDate").value;

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../PageMethods.aspx/GetVendorContractAmount",
                data: "{'LT': '" + LT + "', 'BID': '" + BID + "', 'RID': '" + RID + "', 'cdate': '" + cdate + "'}",
                dataType: "json",
                success: function(data) {
                    document.getElementById("ctl00_NaviPlaceHolder_uoTextContractedRate").value = Number(data.d[0]).toFixed(2);
                    document.getElementById("ctl00_NaviPlaceHolder_uoTextBoxComfirmRate").value = Number(data.d[1]).toFixed(2);
                }
            });
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
      
    </script>
    
    
</asp:Content>
<%--
<asp:Content ID="Content4" ContentPlaceHolderID="SidePlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">
</asp:Content>
--%>