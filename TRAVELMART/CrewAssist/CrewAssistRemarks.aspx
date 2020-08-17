<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="True" ValidateRequest="false"
    CodeBehind="CrewAssistRemarks.aspx.cs" Inherits="TRAVELMART.Hotel.CrewAssistRemarks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">        
    <style type="text/css">
        .style2
        {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server" ID="header">
    <%--<div class="PageTitle">
        Hotel Booking Exception List</div>--%>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr class="PageTitle">
                <td align="left">
                   Crew Assist Remarks Report
                </td>                        
            </tr>
        </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <div id="PG" style="width: auto; height: auto; overflow: auto;">
      <table width="100%" cellpadding="0" cellspacing="0" style="width:100%; text-align:left">
            <tr>
                <td class="style2">
                    Filter:
                </td>
                <td class="style2">
                     <asp:DropDownList ID="uoDropDownListReportBy" runat="server" Width="200px">
                        <asp:ListItem Value="2" Text="By Employee ID"></asp:ListItem>
                        <asp:ListItem Value="1" Text="By Employee Name"></asp:ListItem>                        
                        <asp:ListItem Value="3" Text="By User"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style2">
                    <asp:Label runat="server" ID = "uoLabelFilterBy" Text="Search Employee ID:" Width="150px"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="uoTextBoxSearch" runat="server" Width="197px" onkeypress="return validate(event);"></asp:TextBox>
                    <asp:Button ID="uoButtonSearch" runat="server" Text="Search" 
                        CssClass="SmallButton" onclick="uoButtonSearch_Click" />
                </td>
                <td class="style2">
                    <asp:DropDownList ID="uoDropDownListFilter" runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 5%">
                    Year:
                </td>
                <td style="width: 20%">
                    <asp:DropDownList ID="uoDropDownListYear" runat="server" Width="200px" >
                    </asp:DropDownList>
                </td>
                <td  style="width: 5%">
                    Month:
                </td>
                <td style="width: 20%">
                    <asp:DropDownList ID="uoDropDownListMonth" runat="server" Width="200px">
                    </asp:DropDownList>
                      &nbsp;&nbsp;
                     <asp:CheckBox runat="server" ID="chIR" Checked="false" Text="Incident Report"/>
                </td>
                <td style="white-space:nowrap">
                     <asp:Button runat="server" ID="uoButtonView" CssClass="SmallButton" Text="View"
                     Visible="true" onclick="uoButtonView_Click" /> &nbsp;&nbsp;
                    <asp:Button runat="server" ID="uoBtnExportList" CssClass="SmallButton" Text="Export List"
                     Visible="true" onclick="uoBtnExportList_Click" />
                     &nbsp;&nbsp;
                   <%-- <asp:CheckBox  runat="server" ID="chAllIR" Checked="true" Text="All Remark"/>
                      &nbsp;&nbsp;--%>
                   
                </td>
            </tr>    
           <%-- <tr>
                <td colspan="5">
                    <asp:Label runat="server" ID="uoLabelRed" CssClass="RedNotification"
                        Text="Exception list is based from selected month and future signon or signoff date of the year" >
                    </asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td colspan="5">
                <br/>
                <br/>
                     <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;
                        position: relative;">
                        <asp:ListView runat="server" ID="ListView1" 
                             onitemcommand="ListView1_ItemCommand" >
                            <LayoutTemplate>
                            </LayoutTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                                    <tr>                        
                                        <th>
                                             <asp:LinkButton runat="server" ID="Label16" Text="Seafarer ID" Width="48px" CommandName="EmployeeID"></asp:LinkButton>                                            
                                        </th>
                                         <th>
                                           <asp:LinkButton runat="server" ID="uoLinkButtonIR" Text="Incident Report" Width="50px" CommandName="RequestSource"></asp:LinkButton> 
                                        </th>
                                         <th>
                                           <asp:LinkButton runat="server" ID="LinkButton1" Text="Source" Width="50px" CommandName="RequestSource"></asp:LinkButton> 
                                        </th>
                                        <th>
                                           <asp:LinkButton runat="server" ID="LinkButton3" Text="Type of Concern" Width="85px" CommandName="RequestHeader"></asp:LinkButton> 
                                        </th>
                                        <th>
                                           <asp:LinkButton runat="server" ID="Label2" Text="Remarks Type" Width="85px" CommandName="RequestType"></asp:LinkButton> 
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label3" Text="Summary" Width="180px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label4" Text="Remarks" Width="230px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="Label7" Text="Status" Width="50px" CommandName="Status"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="LinkButton2" Text="Requestor" Width="50px" CommandName="Requestor"></asp:LinkButton>
                                        </th>
                                        <th>
                                           <asp:LinkButton runat="server" ID="Label5" Text="Created Date" Width="80px"  CommandName="CreatedDate"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="Label6" Text="Created By" Width="100px" CommandName="CreatedBy"></asp:LinkButton>
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>                                        
                    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto; 
                     position: relative;"  onscroll="divScrollL();">
                    <asp:ListView runat="server" ID="uoListViewDashboard" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList" width="100%">
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>     
                            <tr>
                                 <td class="leftAligned" style="white-space: normal;">                                    
                                    <asp:Label runat="server" ID="Label16" Text='<%# Eval("SeafarerID")%>' Width="52px"> </asp:Label> 
                                 </td>
                                 
                                 <td class="leftAligned" style="white-space: normal;">                                    
                                    <asp:Label runat="server" ID="Label12" Text='<%#   Eval("IR").ToString() == "True" ? "Yes" : "No" %>' Width="52px"> </asp:Label> 
                                 </td>
                                 
                                 <td class="leftAligned" style="white-space: normal;">                                    
                                    <asp:Label runat="server" ID="Label8" Text='<%# Eval("Source")%>' Width="54px"> </asp:Label> 
                                 </td>
                                 <td class="leftAligned" style="white-space: normal;">  
                                   <asp:Label runat="server" ID="Label11" Text='<%# Eval("RequestHeader")%>' Width="87px"> </asp:Label> 
                                 </td>
                                 <td class="leftAligned" style="white-space: normal;">  
                                   <asp:Label runat="server" ID="Label1" Text='<%# Eval("RequestType")%>' Width="86px"> </asp:Label> 
                                 </td>
                                 <td class="leftAligned" style="white-space: normal;">  
                                    <asp:Label runat="server" ID="Label15" Text='<%# Eval("Summary")%>' Width="172px"> </asp:Label> 
                                 </td>
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label17" Text='<%# Eval("Remarks")%>' Width="220px"> </asp:Label> 
                                 </td>
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label18" Text='<%# Eval("RemarksStatus")%>' Width="53px"> </asp:Label> 
                                 </td>
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label9" Text='<%# Eval("Requestor")%>' Width="53px"> </asp:Label> 
                                 </td>
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label19"  Text='<%#String.Format("{0:dd-MMM-yyyy HHmm}", Eval("CreatedDate"))%>' Width="80px"> </asp:Label> 
                                 </td>
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label20" Text='<%# Eval("CreatedBy")%>' Width="103px"> </asp:Label> 
                                 </td>
                             </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <td colspan="20" class="leftAligned">
                                        <asp:Label runat="server" ID="Label10" Text="No Record" Width="1080px"> </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
                <div>
                    <asp:DataPager ID="uoListViewManifestPager" runat="server" PagedControlID="uoListViewDashboard"
                        PageSize="20" onprerender="uoListViewManifestPager_PreRender">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />                                            
                        </Fields>
                    </asp:DataPager>
                    <asp:ObjectDataSource ID="uoObjectDataSourceCrewAssist" runat="server" MaximumRowsParameterName="iMaxRow"
                        SelectCountMethod="GetCrewAssistRemarksCount" SelectMethod="GetCrewAssistRemarks" StartRowIndexParameterName="iStartRow"
                        TypeName="TRAVELMART.BLL.ReportBLL" OldValuesParameterFormatString="oldcount_{0}"
                        EnablePaging="True" OnSelecting="uoObjectDataSourceCrewAssist_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="iYear" Type="Int32" />
                            <asp:Parameter Name="iMonth" Type="Int32" />
                            <asp:Parameter Name="sCreatedBy" Type="String" />
                            <asp:Parameter Name="sUserID" Type="String" />
                            <asp:Parameter Name="iLoadType" Type="Int16" />
                            <asp:Parameter Name="iFilterBy" Type="Int16" />
                            <asp:Parameter Name="sFilterValue" Type="String" />
                            <asp:Parameter Name="sOrderBy" Type="String" />
                            <asp:Parameter Name="sIRBy" Type="Int16"  />
                            
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>                      
                </td>
            </tr>
        </table>        
    </div>
    
    <asp:HiddenField runat="server" ID="uoHiddenFieldUser" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldLoadType" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldOrderBy" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldFilterValue" Value="0" />
    
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetTRResolution();
            controlSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
                controlSettings();
            }
        }
        function SetTRResolution() {
            var ht = $(window).height();
            var ht2 = $(window).height();
            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.21;
                ht2 = ht2 * 0.46;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.40;
                ht2 = ht2 * 0.59;
            }
            else {
                ht = ht * 0.5;
                ht2 = ht2 * 0.7;
            }


            $("#Av").width(wd);
            $("#Bv").height(ht);
            $("#PG").height(ht2);
            $("#Bv").width(wd);

            $("#PG").width(wd);
        }
      
        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;

        }
        function controlSettings() {

            
            $("#<%=uoDropDownListReportBy.ClientID %>").change(function(e) {
                $("#<%=uoTextBoxSearch.ClientID %>").val('');

                var s = 'Search ';
                if ($(this).val() == 1) {
                    s = s + 'Employee Name:';
                    bindEmployee('1', '');
                }
                else if ($(this).val() == 2) {
                    s = s + 'Employee ID:';
                    bindEmployee('2', '');
                }
                else {
                    s = s + 'User Name:';



                    bindUsers($("#<%=uoHiddenFieldUser.ClientID %>").val());
                }
                $("#<%=uoLabelFilterBy.ClientID %>").text(s);

            });


            $("#<%=uoDropDownListFilter.ClientID %>").change(function(e) {
                $("#<%=uoHiddenFieldFilterValue.ClientID %>").val($(this).val());
            });


        }


//        function ReportType(val) {

//            if (val.id == "ctl00_NaviPlaceHolder_chAllIR") { 
//                if
//                document.getElementById("")
//            }
//        
//        }
        
        
        function bindUsers(sFilter) {
            var ddlUsers = $("#<%=uoDropDownListFilter.ClientID %>");

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../PageMethods.aspx/GetUserList",
                data: "{'sUserID': '" + sFilter + "', 'bIsForCrewAssistRemarks': 'true'}",
                dataType: "json",
                success: function(data) {
                    //if there is data
                    if (data.d.length > 0) {

                        //remove all the options in dropdown
                        $("#<%=uoDropDownListFilter.ClientID %>> option").remove();

                        //add option in dropdown
                        $("<option value='PROCESSING'>PROCESSING...</option>").appendTo(ddlUsers);
                        $("<option value='0'>--SELECT USERNAME--</option>").appendTo(ddlUsers);

                        for (var i = 0; i < data.d.length; i++) {
                            //add the data coming from the result
                            $("<option value='" + data.d[i].sUserName + "'>" + data.d[i].sUserName + "</option>").appendTo(ddlUsers);
                        }
                        $("#<%=uoDropDownListFilter.ClientID %>> option[value='PROCESSING']").remove();
                    }
                },
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }

        function bindEmployee(sFilterBy, sFilterValue) {
            var ddlUsers = $("#<%=uoDropDownListFilter.ClientID %>");

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../PageMethods.aspx/GetEmployeeList",
                data: "{'sFilterBy': '" + sFilterBy + "', 'sFilterValue': '" + sFilterValue + "'}",
                dataType: "json",
                success: function(data) {
                    //if there is data
                    if (data.d.length > 0) {

                        //remove all the options in dropdown
                        $("#<%=uoDropDownListFilter.ClientID %>> option").remove();

                        //add option in dropdown
                        $("<option value='PROCESSING'>PROCESSING...</option>").appendTo(ddlUsers);
                        $("<option value='0'>--SELECT EMPLOYEE--</option>").appendTo(ddlUsers);

                        for (var i = 0; i < data.d.length; i++) {
                            //add the data coming from the result
                            $("<option value='" + data.d[i].SFID + "'>" + data.d[i].Name + "</option>").appendTo(ddlUsers);
                        }
                        $("#<%=uoDropDownListFilter.ClientID %>> option[value='PROCESSING']").remove();
                    }
                },
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }
        
        function validate(key) {
            if ($("#<%=uoDropDownListReportBy.ClientID %>").val() == "2") {

                //getting key code of pressed key
                var keycode = (key.which) ? key.which : key.keyCode;
                if ((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) {
                    return false;
                }
                else {
                    return true;
                }
            }
            return true;
        }
        
    </script>
</asp:Content>
