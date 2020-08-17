<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="StripesRoomType.aspx.cs" Inherits="TRAVELMART.StripesRoomType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
     $(document).ready(function() {
         ShowPopup();
     });

     function confirmDelete() {
         if (confirm("Delete record?") == true)
             return true;
         else
             return false;
     }

     function pageLoad(sender, args) {
         var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
         if (isAsyncPostback) {
             ShowPopup();
         }
     }

     function ShowPopup() {

         $("a#<%=uoHyperLinkEventsAdd.ClientID %>").fancybox({
             'width': '50%',
             'height': '85%',
             'autoScale': false,
             'transitionIn': 'fadeIn',
             'transitionOut': 'fadeOut',
             'onComplete': function() {
                 $("#uoButtonSave.ClientID").click(function() {
                     $.fancybox.close();
                 });
             }
         });

         $("#<%=uoButtonSave.ClientID %>").click(function(ev) {

             $("#<%=uoHiddenFieldStripe.ClientID %>").val($("#<%=uoDropDownListStripes.ClientID %>").val());
             $("#<%=uoHiddenFieldRoomID.ClientID %>").val($("#<%=uoDropDownListRoom.ClientID %>").val());
             $("#<%=uoHiddenFieldEffectiveDate.ClientID %>").val($("#<%=uoTextBoxEffectiveDate.ClientID %>").val());
             $("#<%=uoHiddenFieldContractLength.ClientID %>").val($("#<%=uoTextBoxContractLength.ClientID %>").val());
             javascript: __doPostBack('ctl00$ContentPlaceHolder1$uoButtonSave', '');
         });
     }

     function validate(key) {

         //getting key code of pressed key
         var keycode = (key.which) ? key.which : key.keyCode;
         if ((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) {
             return false;
         }
         else {
             return true;
         }
         return true;
     }   
    </script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
<table width="100%">
    <tr class="PageTitle">
        <td class="LeftClass" style="width:100%;">           
            <table width="100%">
                <tr>
                    <td style="width:25%;"> Stripes Room Type</td>
                    <td class="RightClass">
                        <a id="uoHyperLinkEventsAdd" runat="server" href="#uoDivAddStripeRoomType">
                            <asp:Button Font-Size="X-Small" runat="server"
                                id="uoButtonAdd" Text="Add" onclick="uoButtonAdd_Click" />
                        </a>
                    </td>
                </tr>
            </table>
        </td>        
    </tr>
    <tr>
       
        <td></td>
    </tr>
    <tr>
        <td ></td>
        
    </tr>
    <tr>
        <td class="Module" >            
                    <asp:ListView runat="server" ID="uoStripesRoomList" 
                        onitemdeleting="uoStripesRoomList_ItemDeleting" 
                        onitemcommand="uoStripesRoomList_ItemCommand" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>Stripes</th>
                                    <th>Room Name</th>
                                    <th>Date Effective</th>
                                    <th>Contract Length</th>                                    
                                    <th runat="server" style="width: 10%" id="DeleteTH"></th>                                    
                                </tr>
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="uoHiddenFieldStripeRoomID" runat="server" Value='<%# Eval("StripeRoomIDInt") %>'/>                                    
                                      <%# Eval("Stripes")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("RoomName")%>
                                </td>
                                <td class="leftAligned">
                                    <%# String.Format("{0:dd-MMM-yyyy}", Eval("DateEffective"))%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("ContractLength")%>
                                </td>
                                <td >
                                    <asp:LinkButton runat="server" ID="uoHyperLinkDelete" CommandArgument='<%# Eval("StripeRoomIDInt") %>'
                                        CommandName = "Delete" Text="Delete" 
                                        OnClientClick="return confirmDelete();"></asp:LinkButton>
                                </td>                               
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table class="listViewTable">
                                <tr>                               
                                    <th>Stripes</th>
                                    <th>RoomName</th>
                                    <th>DateEffective</th>
                                    <th>Contract Length</th>
                                </tr>
                                <tr>
                                    <td colspan="4" class="leftAligned">No Record</td>                                
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
               
        </td>
    </tr>
    <tr>
        <td class="RightClass">
             <asp:DataPager ID="uoStripesRoomListPager" runat="server"
                PagedControlID="uoStripesRoomList" PageSize="20">
                <Fields>
                    <asp:NumericPagerField ButtonType="Link"
                        NumericButtonCssClass="PagerClass" />
                </Fields>    
            </asp:DataPager>   
        </td>
    </tr>
    </table>
    </ContentTemplate>
</asp:UpdatePanel> 
    <asp:HiddenField ID="uoHiddenFieldStripe" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldRoomID" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldEffectiveDate" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldContractLength" runat="server" />
    <table style="display:none">
    <tr>
        <td>        
        <div id = "uoDivAddStripeRoomType" style="width:550px">                
            <div class="PageTitle">
                Add Stripe Room Type             
            </div>
            <table id="uoTableAddStripeRoomType" class="TableEditor">           
                <tr>
                    <td  class="caption">   
                        Stripe:                 
                    </td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListStripes" runat="server" Width="305px">                        
                        </asp:DropDownList>                        
                    </td>
                    <td class="LeftClass">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Stripe Required" 
                        ControlToValidate = "uoDropDownListStripes" Text="*" ValidationGroup="SaveStripeRoom" InitialValue=""></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td  class="caption">                    
                        Room Type:
                    </td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListRoom" runat="server" Width="305px" >
                        </asp:DropDownList>
                    </td>
                    <td class="LeftClass">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Stripe Required" 
                        ControlToValidate = "uoDropDownListRoom" Text="*" ValidationGroup="SaveStripeRoom" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="caption">
                        Effective Date:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxEffectiveDate" runat="server" Text="" Width="300px"></asp:TextBox>
                       <%-- <cc1:CalendarExtender ID="uoTextBoxEffectiveDate_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="uoTextBoxEffectiveDate"  Format="MM/dd/yyyy">
                        </cc1:CalendarExtender>--%>
                        
                        <cc1:TextBoxWatermarkExtender ID="uoTextBoxEffectiveDate_TextBoxWatermarkExtender" runat="server"
                            Enabled="True" TargetControlID="uoTextBoxEffectiveDate" WatermarkCssClass="fieldWatermark"
                            WatermarkText="MM/dd/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                                                
                        <cc1:MaskedEditExtender ID="uoTextBoxEffectiveDate_MaskedEditExtender" runat="server"
                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                            CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxEffectiveDate"
                            UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>                                        
                    </td>
                    <td class="LeftClass">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoTextBoxEffectiveDate"
                        ErrorMessage="Effective Date Required." ValidationGroup="SaveStripeRoom">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="caption">
                        Contract Length:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxContractLength" runat="server" Text="" Width="300px" onkeypress="return validate(event);"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td  class="caption">                    
                    </td>
                    <td class="value">
                        <asp:Button ID="uoButtonSave" runat="server" Text="Save" CssClass="SmallButton" 
                            onclick="uoButtonSave_Click" ValidationGroup="SaveStripeRoom"/>
                    </td>
                </tr>
            </table>
        </div>
    <%--</div>--%>
    </td>
    </tr>
    </table>
</asp:Content>
