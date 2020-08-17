<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true"
    CodeBehind="HotelContractView.aspx.cs" Inherits="TRAVELMART.ContractManagement.HotelContractView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>

    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>

    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>

    <script src="../Menu/menu.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 145px;
        }
        .style2
        {
            width: 176px;
        }
        .style3
        {
            height: 26px;
        }
        .style4
        {
            width: 145px;
            height: 26px;
        }
        .style5
        {
            width: 176px;
            height: 26px;
        }
    </style>
    <style type="text/css">
        .style1
        {
            height: 24px;
        }
        .style6
        {
            height: 24px;
        }
        .style7
        {
            width: 145px;
            height: 24px;
        }
        .style8
        {
            width: 176px;
            height: 24px;
        }
        td
        {
            white-space: nowrap;
        }
    </style>

    <script language="javascript">
        $(document).ready(function() {
            SetOverflowVisibility();
        });

        function Navigate(xValue) {
            alert(xValue);
            window.open(xValue);
        }

        function SetOverflowVisibility() {
            var t = $("#<%= uoHiddenFieldRole.ClientID %>").val();           
            if (t == "Crew Assist") {
                $('#<%=notCrewAssit.ClientID %>').hide();
            }
        }      
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="leftAligned">
        <div class="PageTitle">
            Hotel Contract
        </div>
        <hr />                
        <table width="100%" style="text-align: left">
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Contract Status : 
                </td>
                <td colspan="3" style="text-align:left">
                    <asp:Label ID="ucLabelContractStatus" runat="server" Font-Size="14pt" ForeColor="Red"
                        Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="notCrewAssit">
                <td>
                    &nbsp;Contract Attachment :
                </td>
                <td colspan="3">
                    <asp:GridView ID="uoGridViewHotelContractAttachment" runat="server" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                        CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="500">
                        <Columns>
                            <asp:TemplateField HeaderText="Filename">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CommandArgument='<%# Eval("AttachmentId") %>'
                                        Text='<%# Eval("FileName") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <EmptyDataTemplate>
                            <table cellspacing="0" cellpadding="3" rules="cols" border="0" style="color: Black;
                                background-color: White; border-color: #999999; border-width: 0px; border-style: Solid;
                                width: 100%; border-collapse: collapse;">
                                <tr style="color: White; background-color: Black; font-weight: bold;">
                                    <th scope="col">
                                        Filename
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        No Contract Attachment
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    &nbsp;Vendor Code :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxVendorCode" runat="server" Width="500px" CssClass="TextBoxInputReadOnly"
                        ReadOnly="True" />
                    &nbsp;<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="uoTextBoxVendorCode"
                        ErrorMessage="Vendor code required." ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Hotel Branch Name :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxVendorName" runat="server" Width="500px" CssClass="TextBoxInputReadOnly"
                        ReadOnly="True" />
                    &nbsp;<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoTextBoxVendorName"
                        ErrorMessage="Vendor name required." ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td>
                  &nbsp;E-mail To:
                </td>
                <td colspan="2">
                  <asp:TextBox ID="uoTextBoxEmailTo" runat="server" Width="250px"
                        ReadOnly="True" CssClass="TextBoxInputReadOnly" />
                    &nbsp; &nbsp; &nbsp;
                    &nbsp;Telephone. No. :     
                    &nbsp;                     
                    <asp:TextBox ID="uoTextBoxTelNo" runat="server" Width="125px" ReadOnly="True" CssClass="TextBoxInputReadOnly" />
                </td>
                <%--<td colspan="2">
                </td>  --%>  
            
            </tr>
            <tr>
                <td>
                    &nbsp; Hotel Branch Address:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxHotelAddress" runat="server" Width="500px" CssClass="TextBoxInputReadOnly"
                        ReadOnly="True" />
                    &nbsp;
                </td>
            
            </tr>
            <tr>
                <td>
                    &nbsp;Contract Title :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uotextboxContractTitle" runat="server" Width="500px" CssClass="TextBoxInputReadOnly"
                        ReadOnly="True" />
                    &nbsp;<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uotextboxContractTitle"
                        ErrorMessage="Contract title required." ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Remarks :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uotextboxRemarks" runat="server" Width="496px" CssClass="TextBoxInputReadOnly"
                        TextMode="MultiLine" ReadOnly="True" />
                    <%--Width="447px"--%>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Hotel Time Zone :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxHotelTimeZone" runat="server" Width="500px" CssClass="TextBoxInputReadOnly"
                         ReadOnly="True" />
                    <%--Width="447px"--%>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Cancellation Terms No. of Hrs:
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxCancellationHours" runat="server"  CssClass="TextBoxInputReadOnly"
                         ReadOnly="True" />
                </td>
                <td>
                    &nbsp;Cut-off Time:
                </td>
                <td>
                     <asp:TextBox ID="uoTextBoxCutoffTime" runat="server"  CssClass="TextBoxInputReadOnly"
                         ReadOnly="True" />
                </td>
            </tr>
            
            
            <tr>
                <td>
                    &nbsp;Contract Start Date :
                </td>
                <%--<td style="width: 25%">--%>
                <td>
                    <asp:TextBox ID="uotextboxStartDate" runat="server" ReadOnly="True" CssClass="TextBoxInputReadOnly" />
                </td>
                <%--<td style="width: 20%">--%>
                <td >
                    &nbsp;Contract End Date :
                </td>
                <td class="style1">
                    <asp:TextBox ID="uotextboxEndDate" runat="server" ReadOnly="True" CssClass="TextBoxInputReadOnly" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;Vendor Personnel :
                </td>
                <td class="style4">
                    <asp:TextBox ID="uotextboxVendorRep" runat="server" Width="250px" ReadOnly="True"
                        CssClass="TextBoxInputReadOnly" />
                </td>
                <td class="style5">
                    &nbsp;RCCL Personnel :
                </td>
                <td class="style3">
                    <asp:TextBox ID="uotextboxRCCLRep" runat="server" ReadOnly="True" CssClass="TextBoxInputReadOnly" />
                </td>
            </tr>
            <tr>
                <td>
                    Vendor Personnel Contact #:
                </td>
                <td>
                    <asp:TextBox ID="uotextboxVendorRepContactNo" runat="server" Width="250px" ReadOnly="True" CssClass="TextBoxInputReadOnly"  />
                </td>
                 <td >
                    &nbsp;RCCL Personnel Contact #:
                </td>
                <td style="width:100%;">
                    <asp:TextBox ID="uotextboxRCCLRepContactNo" runat="server" Width="250px" ReadOnly="True" CssClass="TextBoxInputReadOnly"  />
                </td>
               
                
            </tr>
            <tr>
                <td>
                  Vendor Personnel Email Add:
                </td>
                <td>
                  <asp:TextBox ID="uotextboxVendorRepEmailAdd" runat="server" Width="250px" ReadOnly="True" CssClass="TextBoxInputReadOnly" />
                </td>
                <td>
                    &nbsp;RCCL Personnel Email Add:
                </td>
                <td>
                    <asp:TextBox ID="uotextboxRCCLRepEmailAdd" runat="server" Width="250px" ReadOnly="True" CssClass="TextBoxInputReadOnly"  />
                </td>
            
                
                
                
            </tr>
            <tr>
               
                
                <td>
                    Website :
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxWebsite" runat="server" Width="250px" ReadOnly="true" CssClass="TextBoxInputReadOnly" />
                </td>
                
                <td>
                  Fax No. :
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxFaxNumber" runat="server" Width="250px" ReadOnly="true"
                        CssClass="TextBoxInputReadOnly" />
                </td>
            </tr>
            <tr>
                <td>
                        E-mail Cc:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxEmailCc" runat="server" TextMode="MultiLine" Width="250px"
                        ReadOnly="True" CssClass="TextBoxInputReadOnly" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Meal Rate :
                </td>
                <td class="style1">
                    <asp:TextBox ID="uoTextBoxMeal" runat="server" ReadOnly="True" CssClass="TextBoxInputReadOnly"></asp:TextBox>
                </td>
                <td class="style2">
                    Meal Rate Inclusive of Tax :
                </td>
                <td>
                    <asp:CheckBox ID="uoCheckBoxMealTaxInclusive" runat="server" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Meal Rate Tax :
                </td>
                <td class="style1">
                    <asp:TextBox ID="uoTextBoxMealTax" runat="server" ReadOnly="True" CssClass="TextBoxInputReadOnly"></asp:TextBox>
                </td>
                <td class="style2">
                    Currency:
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxCurrency" runat="server" ReadOnly="True" CssClass="TextBoxInputReadOnly" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Default Single Room Rate:
                </td>
                <td>
                     <asp:TextBox ID="uoTextBoxDefaultSGLRate" runat="server" ReadOnly="True" CssClass="TextBoxInputReadOnly" Width="250px" Text="0.00"/>
                </td>
                <td>
                    Default Double Room Rate:
                </td>
                <td>
                     <asp:TextBox ID="uoTextBoxDefaultDBLRate" runat="server" ReadOnly="True" CssClass="TextBoxInputReadOnly" Width="250px" Text="0.00"/>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Inclusion :
                </td>
                <td class="style1">
                    <asp:CheckBox ID="uoCheckBoxBreakfast" runat="server" Text="Breakfast" Enabled="False" />
                </td>
                <td class="style2">
                    <asp:CheckBox ID="uoCheckBoxLunch" runat="server" Text="Lunch" Enabled="False" />
                </td>
                <%--<td>
                    &nbsp;</td>--%>
                <td class="style1">
                    <asp:CheckBox ID="uoCheckBoxDinner" runat="server" Text="Dinner" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:CheckBox ID="uoCheckBoxLunchDinner" runat="server" Text="Lunch or Dinner" Enabled="False" />
                </td>
                <td class="style2">
                    <asp:CheckBox ID="uoCheckBoxShuttle" runat="server" Enabled="False" Text="Shuttle" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <%--<tr>
                <td class="style6">
                    </td>
                <td class="style7">
                    
                </td>
                <td class="style8">
                    </td>
                <td class="style6">
                    </td>
            </tr>--%>
            <%--<tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style1">
                    <asp:CheckBox ID="uoCheckBoxShuttle" runat="server" 
                       Enabled="False" Text="Shuttle" />
                </td>
                <td class="style1">
                    </td>
                <td class="style1">
                    </td>
            </tr>--%>
            <tr>
                <td colspan="4">
                    <br />
                    <asp:HiddenField ID="uoHiddenFieldVendorID" runat="server" Value="0" />
                    <asp:HiddenField ID="uoHiddenFieldContractID" runat="server" Value="0" />
                </td>
            </tr>
        </table>
        <div class="PageTitle">
            <asp:Panel ID="uopanelroomhead" runat="server">
                Rooms
                <asp:Label ID="uolabelRoom" runat="server" Text="" Font-Size="Small" />
            </asp:Panel>
        </div>
        <asp:Panel ID="uopanelroom" runat="server" CssClass="CollapsiblePanel">
            <table width="100%">
                <caption>
                    <asp:GridView ID="uoGridViewDays" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        OnPageIndexChanging="uoGridViewDay_PageIndexChanging" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="DATE" HeaderText="Date">
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="colRoomNameVarchar" HeaderText="Room Type">
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="colNumberOfUnitsInt" HeaderText="No. of Rooms" />
                            <asp:BoundField DataField="colRatePerDayMoney" DataFormatString="{0:#,##0.00}" HeaderText="Room Rate" />
                            <asp:BoundField DataField="colMonInt" HeaderText="Mon" />
                            <asp:BoundField DataField="colTuesInt" HeaderText="Tues" />
                            <asp:BoundField DataField="colWedInt" HeaderText="Wed" />
                            <asp:BoundField DataField="colThursInt" HeaderText="Thurs" />
                            <asp:BoundField DataField="colFriInt" HeaderText="Fri" />
                            <asp:BoundField DataField="colSatInt" HeaderText="Sat" />
                            <asp:BoundField DataField="colSunInt" HeaderText="Sun" />
                        </Columns>
                        <HeaderStyle BackColor="#333333" ForeColor="White" />
                    </asp:GridView>
                    <br />
                    <%-- <b class="leftAligned">DETAILS:</b>--%>
                    <tr>
                        <td class="leftAligned" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </caption>
            </table>
        </asp:Panel>
        <cc1:CollapsiblePanelExtender ID="uocollapsibleRoom" runat="server" TargetControlID="uopanelRoom"
            ExpandControlID="uopanelroomhead" CollapseControlID="uopanelroomhead" TextLabelID="uolabelRoom"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" SuppressPostBack="true">
        </cc1:CollapsiblePanelExtender>
        <br />
        <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
    </div>
</asp:Content>
