<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="True"
    CodeBehind="HotelForecastMicroApproval.aspx.cs" Inherits="TRAVELMART.Hotel.HotelForecastMicroApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">     
</asp:Content>
<asp:Content runat="server" ID="Conetent3" ContentPlaceHolderID="HeaderContent">
    
     <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="PageTitle">
            <td align="left">
                Hotel Forecast Approval
            </td>
            <td align="right">
                Region: &nbsp;&nbsp
                <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="200px" AutoPostBack="true"
                    OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp;Port: &nbsp;&nbsp;
                <asp:DropDownList ID="uoDropDownListPortPerRegion" runat="server" Width="200px" AutoPostBack="true"
                    AppendDataBoundItems="true" OnSelectedIndexChanged ="uoDropDownListPortPerRegion_SelectedIndexChanged">
                </asp:DropDownList>
            </td>            
        </tr>
    </table>   
   
    <div id="PG" style="width: auto; height: auto; overflow: auto;">
        <table width="100%" class="LeftClass">
             <tr>
                <td class="contentCaption">
                    Hotel Branch:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListHotel" runat="server" Width="300px" AppendDataBoundItems="True">
                        <asp:ListItem Value="0">--SELECT HOTEL--</asp:ListItem>
                    </asp:DropDownList>
                    <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListBranch" runat="server"
                        TargetControlID="uoDropDownListHotel" PromptText="Type to search" PromptPosition="Top"
                        IsSorted="true" PromptCssClass="dropdownSearch" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoDropDownListHotel"
                        Enabled="False" ErrorMessage="Required Hotel" InitialValue="0">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td class="contentCaption">
                    Date Range:
                </td>
                <td class="contentValue">
                    <asp:TextBox ID="uoTextBoxFrom" runat="server" onchange="return CheckFromDate(this);" Width="130px"></asp:TextBox>
                     <cc1:textboxwatermarkextender ID="uoTextBoxFrom_TextBoxWatermarkExtender" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxFrom" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:textboxwatermarkextender>
                    <cc1:calendarextender ID="uoTextBoxFrom_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxFrom"  Format="MM/dd/yyyy"  >
                    </cc1:calendarextender >
                    <cc1:maskededitextender ID="uoTextBoxFrom_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxFrom"
                        UserDateFormat="MonthDayYear">
                    </cc1:maskededitextender>
                    &nbsp
                    To: 
                     &nbsp;<asp:TextBox ID="uoTextBoxTo" runat="server" onchange="return CheckToDate(this);" Width="130px"></asp:TextBox><cc1:textboxwatermarkextender ID="Textboxwatermarkextender1" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxTo" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:textboxwatermarkextender>
                    <cc1:calendarextender ID="Calendarextender1" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxTo"  Format="MM/dd/yyyy">
                    </cc1:calendarextender >
                    <cc1:maskededitextender ID="Maskededitextender1" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxTo"
                        UserDateFormat="MonthDayYear">
                    </cc1:maskededitextender>
                </td>
                <td>
                     <asp:Button ID="uoButtonView" runat="server" CssClass="SmallButton" 
                             Text="View" onclick="uoButtonView_Click" Width="70px" />
                   <%-- &nbsp;<asp:Button ID="uoBtnExportList" runat="server" CssClass="SmallButton" Text="Export"
                        OnClick="uoBtnExportList_Click" />--%>
                        &nbsp; 
                     <asp:Button ID="uoButtonSave" runat="server" CssClass="SmallButton" OnClientClick='return ValidateIfCheck();'
                             Text="Save"  Width="70px" onclick="uoButtonSave_Click" />
                </td>
            </tr>
        </table>
        
        
        
        <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
            <asp:ListView runat="server" ID="ListView1">
                <LayoutTemplate>
                </LayoutTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="listViewTable">
                        <tr>
                            <th style="text-align: center; white-space: normal;width:55px" rowspan="2" >
                                <asp:Label ID="uoLabelSelect" runat="server" Text="Select" Width="50px" />
                                <asp:CheckBox runat="server" ID = "uoCheckBoxSelectAll" Width="50px" OnClick="CheckUncheckAll(this);"/>                                            
                            </th> 
                            <th style="text-align: center; white-space: normal; width:105px" rowspan="2">
                                <asp:Label runat="server" ID="uoLblTRHeader" Text="Date" Width="65px"></asp:Label>
                            </th>
                           <%-- <th style="text-align: center; white-space: normal;" colspan="2">
                                <asp:Label runat="server" ID="uoLblCheckInHeader" Text="Micro Confirmed Booking" Width="140px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;" colspan="2">
                                <asp:Label runat="server" ID="Label4" Text="Micro Overflow" Width="140px"></asp:Label>
                            </th>--%>
                            <th style="text-align: center; white-space: normal;" colspan="2">
                                <asp:Label runat="server" ID="Label7" Text="Forecast" Width="100px"></asp:Label>
                            </th>
                            
                            
                            <th style="text-align: center; white-space: normal;" colspan="2">
                                <asp:Label runat="server" ID="Label16" Text="Room Blocked" Width="100px"></asp:Label>
                            </th>
                             <th style="text-align: center; white-space: normal;" colspan="2">
                                <asp:Label runat="server" ID="Label17" Text="Booked" Width="100px"></asp:Label>
                            </th>
                            
                             <th style="text-align: center; white-space: normal; width:220px" colspan="2" >
                                <asp:Label runat="server" ID="Label1" Text="To be Added" Width="100px"></asp:Label>
                            </th>
                            
                            <%--<th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label2" Text="" Width="25px"></asp:Label>
                            </th>--%>
                        </tr>
                         <tr>
                           <%-- <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label9" Text="Double" Width="70px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label8" Text="Single" Width="70px"></asp:Label>
                            </th>
                            
                            <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label10" Text="Double" Width="70px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label11" Text="Single" Width="70px"></asp:Label>
                            </th>--%>
                            
                            <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label13" Text="Double" Width="50px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label14" Text="Single" Width="50px"></asp:Label>
                            </th>
                            
                             <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label18" Text="Double" Width="50px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label19" Text="Single" Width="50px"></asp:Label>
                            </th>
                            
                             <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label20" Text="Double" Width="50px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;" >
                                <asp:Label runat="server" ID="Label21" Text="Single" Width="50px"></asp:Label>
                            </th>
                            
                            <th style="text-align: center; white-space: normal; width:110px" >
                                <asp:Label runat="server" ID="Label3" Text="Double" Width="100px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal; width:110px" >
                                <asp:Label runat="server" ID="Label2" Text="Single" Width="100px"></asp:Label>
                            </th>
                            
                           <%-- <th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label12" Text="" Width="25px"></asp:Label>
                            </th>--%>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
        <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
            onscroll="divScrollL();">
            <asp:ListView runat="server" ID="uolistviewHotelInfo" DataSourceID="uoObjectDataSourceManifest"
                OnDataBound="uolistviewHotelInfo_DataBound">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%" id="uoTableManifest">
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="centerAligned" style="white-space: normal; width:55px">
                            <asp:CheckBox runat="server" ID = "uoCheckBoxSelect"  Width="50px"/>
                        </td>  
                        <td class="leftAligned" style="white-space: normal; width:105px">
                            <asp:Label runat="server" ID="uoLabelDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("colDate"))%>'
                                Width="65px"></asp:Label>
                        </td>
                        
                        <%--<td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="uoLblCouple" Text='<%# Eval("Confirmed_DBL")%>' Width="60px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label1" Text='<%# Eval("Confirmed_SGL")%>' Width="60px"></asp:Label>
                        </td>
                        
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label3" Text='<%# Eval("Overflow_DBL")%>' Width="60px"></asp:Label>
                        </td>
                        <td class="leftAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label5" Text='<%# Eval("Overflow_SGL")%>' Width="60px"></asp:Label>
                        </td>--%>
                        
                        <td class="rightAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label4" Text='<%# Convert.ToString(Eval("Forecast_DBL_Old"))== "0"?"": Eval("Forecast_DBL_Old")%> '  Width="18px"  ToolTip="Old Value" ></asp:Label>
                             
                            <asp:Label runat="server" ID="Label6" Text='<%# Eval("Forecast_DBL")%>' Width="18px" ToolTip="Current Value"
                                ForeColor='<%# Convert.ToString(Eval("Forecast_DBL_Old"))== "0"?System.Drawing.Color.Black : System.Drawing.Color.Red %>'>
                            </asp:Label>
                        </td>
                        <td class="rightAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label5" Text='<%# Convert.ToString(Eval("Forecast_SGL_Old"))== "0"?"": Eval("Forecast_SGL_Old")%> '  Width="18px"  ToolTip="Old Value" ></asp:Label>
                             
                            <asp:Label runat="server" ID="Label8" Text='<%# Eval("Forecast_SGL")%>' Width="18px" ToolTip="Current Value"
                                ForeColor='<%# Convert.ToString(Eval("Forecast_SGL_Old"))== "0"?System.Drawing.Color.Black : System.Drawing.Color.Red %>'>
                            </asp:Label>
                        </td>
                        
                         <td class="rightAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label22" Text='<%# Eval("RoomBlock_DBL")%>' Width="40px"></asp:Label>
                        </td>
                        <td class="rightAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label23" Text='<%# Eval("RoomBlock_SGL")%>' Width="40px"></asp:Label>
                        </td>
                        
                         <td class="rightAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label24" Text='<%# Eval("TMBooked_DBL")%>' Width="40px"></asp:Label>
                        </td>
                        <td class="rightAligned" style="white-space: normal;">
                            <asp:Label runat="server" ID="Label25" Text='<%# Eval("TMBooked_SGL")%>' Width="40px"></asp:Label>
                        </td>
                        
                        <td class="rightAligned" style="white-space: nowrap; width:100px">
                            <asp:HiddenField ID="uoHiddenFieldToAddDBL" runat="server" Value='<%# Eval("ToBeAdded_DBL") %>' />
                            
                            <asp:TextBox runat="server" ID="uoTextBoxDBL" onkeypress="return validate(event);" 
                            CssClass='<%# (Convert.ToBoolean(Eval("IsEnable"))== true? "SmallTextRight": "ReadOnlySmallRight") %>' 
                            Text='<%# Eval("ToBeAdded_DBL") %>'
                            Width="70px" ReadOnly='<%# !Convert.ToBoolean(Eval("IsEnable")) %>' ></asp:TextBox>
                            
                            <asp:Label runat="server" ID="Label9" Text='<%# Convert.ToString(Eval("ToBeAdded_DBL_Suggested"))== "0"?"": Eval("ToBeAdded_DBL_Suggested")%> '  
                            Width="25px"  ToolTip="Suggested Value" ForeColor="Red"></asp:Label>
                             
                        </td>
                        <td class="rightAligned" style="white-space: nowrap; width:100px">
                          <asp:HiddenField ID="uoHiddenFieldToAddSGL" runat="server" Value='<%# Eval("ToBeAdded_SGL") %>' />
                            
                          <asp:TextBox runat="server" ID="uoTextBoxSGL" onkeypress="return validate(event);" 
                          CssClass='<%# (Convert.ToBoolean(Eval("IsEnable"))== true? "SmallTextRight": "ReadOnlySmallRight") %>' 
                          Text='<%# Eval("ToBeAdded_SGL") %>'
                          Width="70px" ReadOnly='<%# !Convert.ToBoolean(Eval("IsEnable")) %>'></asp:TextBox>
                          
                           <asp:Label runat="server" ID="Label10" Text='<%# Convert.ToString(Eval("ToBeAdded_SGL_Suggested"))== "0"?"": Eval("ToBeAdded_SGL_Suggested")%> '  
                            Width="25px"  ToolTip="Suggested Value" ForeColor="Red"></asp:Label>
                        </td>
                        
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate> 
                    <table class="listViewTable">
                        <tr>
                            <td colspan="9" class="leftAligned">
                                No Record
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div align="left">
                <asp:DataPager ID="uolistviewHotelInfoPager" runat="server" PagedControlID="uolistviewHotelInfo"
                    PageSize="50">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
               
                <asp:ObjectDataSource ID="uoObjectDataSourceManifest" runat="server" MaximumRowsParameterName="MaxRow"
                    SelectCountMethod="GetForecastManifestCount" SelectMethod="GetForecastManifestList"
                    StartRowIndexParameterName="StartRow" TypeName="TRAVELMART.BLL.HotelForecastBLL" OldValuesParameterFormatString="oldcount_{0}"
                    EnablePaging="True" OnSelecting="uoObjectDataSourceManifest_Selecting">
                    <SelectParameters>
                        <asp:Parameter Name="sBranchName" Type="String" />
                        <asp:Parameter Name="sDateFrom" Type="String" />
                        <asp:Parameter Name="sDateTo" Type="String" />
                        <asp:Parameter Name="sVesselCode" Type="String" />
                        <asp:Parameter Name="sPortID" Type="String"  />
                        <asp:Parameter Name="sUser" Type="String" />
                        <asp:Parameter Name="sRole" Type="String" />
                        <asp:Parameter Name="LoadType" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0"/>
        
        
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetTMResolution();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTMResolution();
            }
        }

        function SetTMResolution() {
            var ht = $(window).height();
            var ht2 = $(window).height();

            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.22;
                ht2 = ht2 * 0.46;
                wd = $(window).width();
            }
            else if (screen.height <= 720) {
                ht = ht * 0.28;
                ht2 = ht2 * 0.59;
            }
            else {
                ht = ht * 0.6;
                ht2 = ht2 * 0.75;
            }

            $("#Bv").height(ht);
            $("#PG").height(ht2);
            $("#Av").width(wd);
            $("#Bv").width(wd);
            $("#PG").width(wd);

        }

        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;
        }

        function CheckFromDate(d) {
            var txtFromDate = $("#<%=uoTextBoxFrom.ClientID %>");
            var txtToDate = $("#<%=uoTextBoxTo.ClientID %>");
            var dt = d.value;

            if (ValidateDate(dt)) {
                if (txtToDate.val() != '') {

                    var fromDate = Date.parse(dt);
                    var toDate = Date.parse(txtToDate.val());

                    if (fromDate > toDate) {
                        alert("Invalid date range!");
                        txtToDate.val('');
                        return false;
                    }
                }
            }
            else {
                alert("Invalid date!");
                txtFromDate.val('');
                return false;
            }
            $("#<%=uoHiddenFieldDate.ClientID %>").val(txtFromDate.val());

            return true;
        }
        function CheckToDate(d) {
            var txtFromDate = $("#<%=uoTextBoxFrom.ClientID %>");
            var txtToDate = $("#<%=uoTextBoxTo.ClientID %>");
            var dt = d.value;

            if (ValidateDate(dt)) {
                if (txtFromDate.val() != '') {

                    var toDate = Date.parse(dt);
                    var fromDate = Date.parse(txtFromDate.val());
                    if (fromDate > toDate) {
                        alert("Invalid date range!");
                        txtFromDate.val('');
                        return false;
                    }
                }
            }
            else {
                alert("Invalid date!");
                txtToDate.val('');
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
        function validate(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;

            if (keycode >= 48 && keycode <= 57) {
                return true;
            }
            else if (keycode == 45) {
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

        function CheckUncheckAll(obj) {
            $('input[name*="uoCheckBoxSelect"]').attr('checked', obj.checked);
        }

        function ValidateIfCheck(IsApproveButton) {

            var bIsCheck = false;
            $("#uoTableManifest tr").each(function(i, element) {
                var $TDs = $(this).find('td');
                var chk = $('td:eq(0) input[name*="uoCheckBoxSelect"]', this);

                if (chk != null) {
                    if (chk.attr('checked')) {
                        bIsCheck = true;
                    }
                }
            });
            if (bIsCheck == false) {
                alert("No selected record!");
            }
            return bIsCheck;
        }
    </script>      
</asp:Content>
