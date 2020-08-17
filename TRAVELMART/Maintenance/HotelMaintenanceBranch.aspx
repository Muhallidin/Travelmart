<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster2.Master"
    AutoEventWireup="true" CodeBehind="HotelMaintenanceBranch.aspx.cs" Inherits="TRAVELMART.Maintenance.HotelMaintenanceBranch" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">
   
  
 
    <script src="../Script/nicEdit.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        bkLib.onDomLoaded(function() {

        new nicEditor({ buttonList: ['bold', 'italic', 'underline', 'strikeThrough', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'indent', 'outdent', 'removeformat'] }).panelInstance('ctl00_NaviPlaceHolder_uoTextBoxInstructionOn');
        new nicEditor({ buttonList: ['bold', 'italic', 'underline', 'strikeThrough', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'indent', 'outdent', 'removeformat'] }).panelInstance('ctl00_NaviPlaceHolder_uoTextBoxInstructionOff');
        });
    </script>

    <script type="text/ecmascript" language="javascript">
        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetResolution();                
            }
        }

        $(document).ready(function() {
            SetResolution();
        });

        function SetResolution() {
            var ht = $(window).height(); //550;
            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.45;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.52;
            }
            else {
                ht = ht * 0.65;
            }

            $("#Bv").height(ht);
            $("#Bv").width(wd);
            
        }

       
    </script>
    
    <div class="PageTitle">
        Hotel Branch
    </div>
    <hr />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">
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

        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
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
            }
            return true;
        }                       
    </script>

    <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    
        <ContentTemplate>
  --%>
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;"  align="center">      
        <asp:UpdatePanel ID="UpdatePanelHotelBranch" runat="server">
            <ContentTemplate>
                <%--<table style="text-align: left; width: 100%;" class="TableView" align="left">--%>
                <table style="text-align: left; width: 100%;" >
                    <tr>
                        <td colspan="4" class="contentCaption">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Save"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="width: 10%;">
                            IMS Vendor ID:&nbsp;
                        </td>
                        <td class="contentValue" style="width: 40%;" >
                            <asp:TextBox ID="uoTextBoxBranchCode" runat="server" Width="300px" MaxLength="10" CssClass="ReadOnly" ReadOnly="true"></asp:TextBox>
                            <br />
                            <asp:DropDownList ID="uoDropDownListIMSVendor" runat="server" Width="505px" AppendDataBoundItems="true" >
                            </asp:DropDownList> &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="uoDropDownListIMSVendor"
                                ErrorMessage="IMS VendorID required" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>                           
                        </td>
                        <td class="contentCaption" style="width: 10%;">
                        </td>
                        <td class="contentValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            Hotel Brand:
                        </td>
                        <td class="contentValue" >
                            <asp:DropDownList ID="uoDropDownListVendor" runat="server" OnSelectedIndexChanged="uoDropDownListVendor_SelectedIndexChanged"
                                Width="505px">
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListVendor" runat="server"
                                TargetControlID="uoDropDownListVendor" PromptText="Type to search" PromptPosition="Top"
                                IsSorted="true" PromptCssClass="dropdownSearch" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoDropDownListVendor"
                                ErrorMessage="Vendor required" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="contentCaption">
                        </td>
                        <td class="contentValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            Hotel Branch:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox ID="uoTextBoxVendorName" runat="server" Width="500px" style="text-transform:capitalize;"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="uorfvVendorName" runat="server" ControlToValidate="uoTextBoxVendorName"
                                ErrorMessage="Vendor Branch Name required." ValidationGroup="Save">*</asp:RequiredFieldValidator></td>
                        <td class="contentCaption">
                        </td>
                        <td class="contentValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="width: 10%;">
                            E-mail To:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox onblur="validateMultipleEmailsSemiColonSeparated(this,';');" ID="uoTextBoxEmailTo"
                                CssClass="TextBoxInput" runat="server" Width="500px" MaxLength="500" TextMode="MultiLine"
                                ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."></asp:TextBox>
                        </td>
                        <td class="contentCaption" rowspan="3" style="vertical-align: top; padding-left: 5px;"
                            align="left">
                            Address:
                        </td>
                        <td class="contentValue" rowspan="3" style="vertical-align: top;" align="left">
                            <asp:TextBox ID="uoTextBoxVendorAddress" runat="server" Width="305px" TextMode="MultiLine"
                                CssClass="TextBoxInput" Height="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="width: 10%;">
                            E-mail Cc:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox onblur="validateMultipleEmailsSemiColonSeparated(this,';');" ID="uoTextBoxEmailCc"
                                CssClass="TextBoxInput" runat="server" Width="500px" MaxLength="500" TextMode="MultiLine"
                                ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="width: 10%;">
                            Country:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="305px" AutoPostBack="true"
                                OnSelectedIndexChanged="uoDropDownListCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListCountry" runat="server"
                                TargetControlID="uoDropDownListCountry" PromptText="Type to search" PromptPosition="Top"
                                IsSorted="true" PromptCssClass="dropdownSearch" />
                            <asp:RequiredFieldValidator ID="uorfvCountry" runat="server" ControlToValidate="uoDropDownListCountry"
                                ErrorMessage="Country required." InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="width: 10%;">
                            City:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListCity" runat="server" Width="305px" OnSelectedIndexChanged="uoDropDownListCity_SelectedIndexChanged"
                                AppendDataBoundItems="True">
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListCity" runat="server"
                                TargetControlID="uoDropDownListCity" PromptText="Type to search" PromptPosition="Top"
                                IsSorted="true" PromptCssClass="dropdownSearch" />
                            <asp:RequiredFieldValidator ID="uorfvCity" runat="server" ControlToValidate="uoDropDownListCity"
                                ErrorMessage="City required." InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="contentCaption" style="vertical-align: top; padding-left: 5px;" align="left">
                            City Filter:
                        </td>
                        <td class="contentValue" style="vertical-align: top;" align="left">
                            <asp:TextBox ID="uoTextBoxFilterCity" runat="server" Width="230px" CssClass="TextBoxInput"></asp:TextBox>
                            <asp:Button ID="uoButtonViewCity" runat="server" Text="Filter City" CssClass="SmallButton"
                                OnClick="uoButtonViewCity_Click" />
                        </td>
                    </tr>                    
                    <tr>
                        <td class="contentCaption" style="width: 10%;">
                            Contact No:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox ID="uoTextBoxContactNo" runat="server" Width="300px"></asp:TextBox>
                           <%-- <cc1:MaskedEditExtender ID="uoTextBoxContactNo_MaskedEditExtender" runat="server"
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                CultureTimePlaceholder="" Enabled="True" Mask="(999) 999-9999" MaskType="Number"
                                TargetControlID="uoTextBoxContactNo">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                        <td class="contentCaption" style="vertical-align: top; padding-left: 5px;" align="left">
                            Fax No.:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox ID="uoTextBoxFaxNo" runat="server" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    <td class="contentCaption" style="width: 10%;">
                            Website&nbsp;:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox ID="uoTextBoxWebsite" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td class="contentCaption" style="vertical-align: top; padding-left: 5px;" align="left">
                           <%-- Is Franchise:--%>
                        </td>
                        <td class="contentValue" style="vertical-align: top;" align="left">
                            <asp:CheckBox ID="uoCheckBoxIsFranchise" runat="server" Checked="false" Visible="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="width: 10%;">
                            Contact Person:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox ID="uoTextBoxPerson" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td class="contentCaption" style="vertical-align: top; padding-left: 5px;" align="left">
                            <%--With shuttle:--%>
                        </td>
                        <td class="contentValue" style="vertical-align: top;" align="left">
                           <%-- <asp:CheckBox ID="uoCheckBoxShuttle" runat="server" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption">
                        </td>                       
                        <td class="caption">
                        </td>
                        <td class="value">
                            <asp:CheckBox ID="uoCheckBoxOfficer" runat="server" Text="Officer" Visible="false" />
                            <asp:CheckBox ID="uoCheckBoxRating" runat="server" Text="Ratings" Visible="false" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="contentCaption">
                            Status:
                        </td>
                        <td class="contentValue">
                            <asp:CheckBox ID="uoCheckBoxON" runat="server" Text="On" />
                            <asp:CheckBox ID="uoCheckBoxOff" runat="server" Text="Off" />
                        </td>
                        <td class="caption">
                            &nbsp;
                        </td>
                        <td class="value">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
               
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table width="100%" >
                    <tr>
                        <td class="PageTitle">
                            Hotel Branch Room Type
                        </td>
                    </tr>
                </table>                
                <table style="text-align: left; width: 90%;" align="center" >
                    <tr>
                        <td colspan="2" class="contentCaption">
                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Room" />
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="width: 10%;">
                            Room Type:&nbsp;
                        </td>
                        <td class="contentValue" >
                            <asp:DropDownList ID="uoDropDownListRoom" runat="server" Width="305px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoDropDownListRoom"
                                ErrorMessage="Required Room" InitialValue="0" ValidationGroup="Room">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                        </td>
                        <td class="contentValue" >
                            <asp:Button ID="uoButtonAddRoom" runat="server" OnClick="uoButtonAddRoom_Click" CssClass="SmallButton"
                                Text="Add" ValidationGroup="Room" Width="47px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                        </td>
                        <td class="contentValue">
                            <asp:GridView ID="uoGridViewRoom" runat="server" AutoGenerateColumns="False" CssClass="listViewTable"
                                EmptyDataText="No data." OnSelectedIndexChanged="uoGridViewRoom_SelectedIndexChanged"
                                DataKeyNames="colRoomNameVarchar" Width="310px">
                                <Columns>
                                    <asp:BoundField DataField="colRoomTypeID" HeaderText="Room ID" />
                                    <asp:BoundField DataField="colRoomNameVarchar" HeaderText="Room Type" />
                                    <%--<asp:ButtonField CommandName="Select" Text="Delete" />--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandName="Select" OnClientClick="javascript:return confirmDelete();">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td class="PageTitle">
                            Hotel Branch Stripes
                        </td>
                    </tr>
                </table>
                <table style="text-align: left; width: 90%;" align="center">
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;" colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Rank" />
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            Department:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListDepartment" runat="server" Width="305px" AutoPostBack="True"
                                OnSelectedIndexChanged="uoDropDownListDepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="uoDropDownListDepartment"
                                ErrorMessage="Department Required" InitialValue="0" ValidationGroup="Stripes">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            Stripes:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListStripes" runat="server" Width="305px">
                                <asp:ListItem Value="0">--Select Stripes--</asp:ListItem>
                                <%--   <asp:ListItem Value="1">Stripe 1</asp:ListItem>
                           <asp:ListItem Value="1.5">Stripe 1.5</asp:ListItem>
                           <asp:ListItem Value="2">Stripe 2</asp:ListItem>
                           <asp:ListItem Value="2.5">Stripe 2.5</asp:ListItem>
                           <asp:ListItem Value="3">Stripe 3</asp:ListItem>
                           <asp:ListItem Value="3.5">Stripe 3.5</asp:ListItem>
                           <asp:ListItem Value="4">Stripe 4</asp:ListItem>
                           <asp:ListItem Value="4.5">Stripe 4.5</asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uoDropDownListStripes"
                                ErrorMessage="Required Stripes" InitialValue="" ValidationGroup="Stripes">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            &nbsp;
                        </td>
                        <td class="contentValue">
                            <asp:Button ID="uoButtonSaveDeptStripe" runat="server" CssClass="SmallButton" Text="Add"
                                ValidationGroup="Stripes" Width="47px" OnClick="uoButtonSaveDeptStripe_Click" />
                            <asp:GridView ID="uoGridViewDepartment" runat="server" AutoGenerateColumns="False"
                                CssClass="listViewTable" EmptyDataText="No data." OnSelectedIndexChanged="uoGridViewDepartment_SelectedIndexChanged"
                                Width="310px">
                                <Columns>
                                    <asp:BoundField DataField="BranchDeptStripeID" HeaderText="DeptStripeID" />
                                    <asp:BoundField DataField="DeptID" HeaderText="DeptID" />
                                    <asp:BoundField DataField="DeptName" HeaderText="Department" />
                                    <asp:BoundField DataField="Stripes" HeaderText="Stripes" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandName="Select" OnClientClick="javascript:return confirmDelete();">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StripeName" HeaderText="StripeName" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td class="PageTitle">
                            Hotel Branch Rank Exception
                        </td>
                    </tr>
                </table>
                <table style="text-align: left; width: 90%;" align="center">
                    <tr>
                        <td colspan="2" class="contentCaption" style="vertical-align: top; width: 10%;">
                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Rank" />
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            Stripes:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListStripesRanks" runat="server" Width="305px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            Filter:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox ID="uoTextBoxFilterRank" runat="server" Width="230px"></asp:TextBox>
                            <asp:Button ID="uoButtonFilterRank" runat="server" Text="Filter Rank" CssClass="SmallButton"
                                OnClick="uoButtonFilterRank_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            Ranks:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListRanks" runat="server" Width="305px" AppendDataBoundItems="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="uoDropDownListRanks"
                                ErrorMessage="Required Rank" InitialValue="0" ValidationGroup="Ranks">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            &nbsp;
                        </td>
                        <td class="contentValue">
                            <asp:Button ID="uoButtonSaveRanks" runat="server" CssClass="SmallButton" Text="Add"
                                ValidationGroup="Ranks" Width="47px" OnClick="uoButtonSaveRanks_Click" />
                            <asp:GridView ID="uoGridViewRanks" runat="server" AutoGenerateColumns="False" CssClass="listViewTable"
                                DataKeyNames="RankID" EmptyDataText="No data." Width="310px" OnSelectedIndexChanged="uoGridViewRanks_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="BranchRankExceptID" HeaderText="BranchRankExceptID" />
                                    <asp:BoundField DataField="RankID" HeaderText="RankID" />
                                    <asp:BoundField DataField="RankName" HeaderText="Ranks" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandName="Select" OnClientClick="javascript:return confirmDelete();">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td class="PageTitle">
                            Hotel Branch Voucher
                        </td>
                    </tr>
                </table>
                <table style="text-align: left; width: 90%;" align="center">
                    <tr>
                        <td colspan="2" class="contentCaption" style="vertical-align: top; width: 10%;">
                            <asp:ValidationSummary ID="ValidationSummaryVoucher" runat="server" ValidationGroup="voucher" />
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            &nbsp; Stripe/s:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox ID="uoTextBoxStripes" runat="server" onkeypress="return validate(event);"></asp:TextBox>
                            <%--<cc1:MaskedEditExtender ID="uoTextBoxStripes_MaskedEditExtender" runat="server" 
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                InputDirection="RightToLeft" Mask="9.9" MaskType="Number" 
                                TargetControlID="uoTextBoxStripes">
                            </cc1:MaskedEditExtender>--%>
                            <asp:RequiredFieldValidator ID="uorfvStripe" runat="server" ControlToValidate="uoTextBoxStripes"
                                ErrorMessage="Stripe/s required." ValidationGroup="voucher">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            &nbsp; Voucher Amount:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox ID="uoTextBoxAmount" runat="server" onkeypress="return validate(event);"></asp:TextBox>
                            <%--<cc1:MaskedEditExtender ID="uoTextBoxAmount_MaskedEditExtender" runat="server" 
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="99" MaskType="Number" TargetControlID="uoTextBoxAmount">
                    </cc1:MaskedEditExtender>--%>
                            <asp:RequiredFieldValidator ID="uorfvAmount" runat="server" ControlToValidate="uoTextBoxAmount"
                                ErrorMessage="Voucher Amount required." ValidationGroup="voucher">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            &nbsp; Day No.:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox ID="uoTextBoxDayNo" runat="server" onkeypress="return validate(event);"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoTextBoxDayNo"
                                ErrorMessage="Day No. required." ValidationGroup="voucher">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            &nbsp;
                        </td>
                        <td class="contentValue">
                            <asp:Button ID="uoButtonVoucher" runat="server" CssClass="SmallButton" OnClick="uoButtonVoucher_Click"
                                Text="Add" ValidationGroup="voucher" Width="47px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            &nbsp;
                        </td>
                        <td class="contentValue">
                            <asp:GridView ID="uoGridViewVoucher" runat="server" AutoGenerateColumns="False" CssClass="listViewTable"
                                DataKeyNames="Stripe" EmptyDataText="No data." OnSelectedIndexChanged="uoGridViewVoucher_SelectedIndexChanged"
                                Width="310px">
                                <Columns>
                                    <asp:BoundField DataField="VoucherID" HeaderText="ID"/>
                                    <asp:BoundField DataField="Stripe" HeaderText="Stripe/s" DataFormatString="{0:#.##}" />
                                    <asp:BoundField DataField="DayNo" HeaderText="Day No." />
                                    <asp:BoundField DataField="Amount" DataFormatString="{0:N}" HeaderText="Amount (US$)" />
                                    <%--<asp:ButtonField CommandName="Select" Text="Delete" />--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandName="Select" OnClientClick="javascript:return confirmDelete();">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td class="PageTitle">
                            Photo
                        </td>
                    </tr>
                </table>
                <table style="text-align: left; width: 90%;" align="center">
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            &nbsp; Hotel Logo:
                        </td>
                        <td class="contentValue" style="vertical-align: top; width: 40%;">  
                             <img alt="Profile Picture" src="../Vehicle/DriverPhoto/default.JPG" ID="imgAvatarLogo" runat="server" style="height:150px; width:150px" visible="true"/> 
                             <br />                                                  
                            <asp:FileUpload ID="FileUploadImageLogo" runat="server" onchange="javascript:PreviewImage('logo',this);" Visible="true" />
                            <br /><br />
                             <asp:Button ID="btnSaveImageLogo" runat="server" Text="Save Logo" 
                                 ToolTip="Save Image" Width="85px" onclick="btnSaveImageLogo_Click" />
                        </td>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            &nbsp; Shuttle Photo:
                        </td>
                        <td class="contentValue" style="vertical-align: top; width: 40%;">  
                             <img alt="Profile Picture" src="../Vehicle/DriverPhoto/default.JPG" ID="imgAvatarShuttle" runat="server" style="height:150px; width:150px" visible="true"/> 
                             <br />                                                  
                            <asp:FileUpload ID="FileUploadImageShuttle" runat="server" onchange="javascript:PreviewImage('shuttle',this);" Visible="true" />
                            <br /><br />
                             <asp:Button ID="btnSaveImageShuttle" runat="server" Text="Save Shuttle" 
                                 ToolTip="Save Image" Width="100px" onclick="btnSaveImageShuttle_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers >
                <asp:PostBackTrigger ControlID="btnSaveImageLogo"/>
                <asp:PostBackTrigger ControlID="btnSaveImageShuttle"/>
            </Triggers>
        </asp:UpdatePanel>
        
         <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td class="PageTitle">
                            Other Information
                        </td>
                    </tr>
                </table>
                <table style="text-align: left; width: 90%;" align="center">
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            &nbsp; Signing ON Instructions:
                        </td>
                        <td class="contentValue">                                                    
                             <asp:TextBox ID="uoTextBoxInstructionOn" runat="server"  Width="500px" TextMode="MultiLine"  Rows="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%;">
                            &nbsp; Signing OFF Instructions:
                        </td>
                        <td class="contentValue">
                             <asp:TextBox ID="uoTextBoxInstructionOff" runat="server"  Width="500px" TextMode="MultiLine" Rows="10" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers >
                <asp:PostBackTrigger ControlID="uoButtonSave"/>
            </Triggers>
        </asp:UpdatePanel>
        
        <hr />
        <table width="100%" align="center">
            <tr>
                <td style="width:10%">
                </td>
                <td  style="text-align:left">
                    <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80" OnClick="uoButtonSave_Click"
                        ValidationGroup="Save" />
                    <asp:Button ID="uoButtonBack" runat="server" Text="Back" Width="80" OnClick="uoButtonBack_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldVendorIdInt" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldNew" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldShuttleID" runat="server" Value="0" />
        
    </div>
   

<script type="text/javascript" language="javascript">

    $(document).ready(function() {

        $("#<%=btnSaveImageLogo.ClientID %>").hide();
        $("#<%=btnSaveImageShuttle.ClientID %>").hide();

        PreviewImage('logo');
        PreviewImage('shuttle');
        PageSettings();
    });

    function PreviewImage(sType, ImagePath) {

        var sSrc = "../Vehicle/DriverPhoto/default.JPG";      

        if (ImagePath) {
            if (ImagePath.files && ImagePath.files[0]) {
                var Filerdr = new FileReader();
                Filerdr.onload = function(e) {
                    if (sType == 'logo') {
                        $('#<%=imgAvatarLogo.ClientID %>').attr('src', e.target.result);
                        $("#<%=btnSaveImageLogo.ClientID %>").show();

                    }
                    else {
                        $('#<%=imgAvatarShuttle.ClientID %>').attr('src', e.target.result);
                        $("#<%=btnSaveImageShuttle.ClientID %>").show();
                    }
                }
                Filerdr.readAsDataURL(ImagePath.files[0]);

            }
            else {
                //No file selected
                if (sType == 'logo') {
                    $('#<%=imgAvatarLogo.ClientID %>').attr('src', sSrc);
                    $("#<%=btnSaveImageLogo.ClientID %>").hide();
                }
                else {
                    $('#<%=imgAvatarShuttle.ClientID %>').attr('src', sSrc);
                    $("#<%=btnSaveImageShuttle.ClientID %>").hide();
                }
            }
        }
        else {

            var MediaURL = '<%= ConfigurationManager.AppSettings["MediaURL"] %>';
            var MediaToken = '<%= ConfigurationManager.AppSettings["MediaToken"] %>';
            var sApiurl = MediaURL + '/vendors/transportation/driver';

            var sApiurl = '';

            if (sType == 'logo') {
                sApiurl = MediaURL + '/vendors/hotel/logo';
            }
            else {
                sApiurl = MediaURL + '/vendors/hotel/shuttle-service';
            }
            sApiurl = sApiurl + "/HT-" + $("#<%=uoHiddenFieldBranchID.ClientID %>").val() + "?at=" + MediaToken;

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../PageMethods.aspx/GetPhoto",
                data: "{'URL': '" + sApiurl + "'}",
                dataType: "json",
                success: function(data) {
                    if (data.d.Image != null) {
                        var result = "data:image/*;base64," + data.d.Image;
                        
                        if (sType == 'logo') {
                            $('#<%=imgAvatarLogo.ClientID %>').attr('src', result);
                        }
                        else {
                            $('#<%=imgAvatarShuttle.ClientID %>').attr('src', result);
                        }
                    }
                    else {
                        console.log("Ooops! no image found!");

                    }
                }
            });
        }
    }
    
    function PreviewImageTMAPI(sType, ImagePath) {
        var sSrc = "../Vehicle/DriverPhoto/default.JPG";

        var MediaURL = '<%= ConfigurationManager.AppSettings["TM-API"] %>';
        var sApiUrl = MediaURL + '/getPhoto';

        if (sType == 'logo') {
            sApiUrl = sApiUrl + "/" + $("#<%=uoHiddenFieldBranchID.ClientID %>").val() + "/hotel";
        }
        else {
            sApiUrl = sApiUrl + "/" + $("#<%=uoHiddenFieldBranchID.ClientID %>").val() + "/shuttle";
        }
       

        if (ImagePath) {

            if (ImagePath.files && ImagePath.files[0]) {
                var Filerdr = new FileReader();
                Filerdr.onload = function(e) {
                    if (sType == 'logo') {
                        $('#<%=imgAvatarLogo.ClientID %>').attr('src', e.target.result);
                        $("#<%=btnSaveImageLogo.ClientID %>").show();

                    }
                    else {
                        $('#<%=imgAvatarShuttle.ClientID %>').attr('src', e.target.result);
                        $("#<%=btnSaveImageShuttle.ClientID %>").show();
                    }
                }
                Filerdr.readAsDataURL(ImagePath.files[0]);
            }
            else {
                //No file selected
                if (sType == 'logo') {
                    $('#<%=imgAvatarLogo.ClientID %>').attr('src', sSrc);
                    $("#<%=btnSaveImageLogo.ClientID %>").hide();
                }
                else {
                    $('#<%=imgAvatarShuttle.ClientID %>').attr('src', sSrc);
                    $("#<%=btnSaveImageShuttle.ClientID %>").hide();
                }
            }
        }

        else {
            // alert('no value');
            IsImageExists(sApiUrl, function(existsImage) {
                if (existsImage == true) {
                    if (sType == 'logo') {
                        $('#<%=imgAvatarLogo.ClientID %>').attr('src', sApiUrl);
                    }
                    else {
                        $('#<%=imgAvatarShuttle.ClientID %>').attr('src', sApiUrl);
                    }
                    console.log("image exist");
                }
                else {
                    if (sType == 'logo') {
                        $('#<%=imgAvatarLogo.ClientID %>').attr('src', sSrc);
                    }
                    else {
                        $('#<%=imgAvatarShuttle.ClientID %>').attr('src', sSrc);
                    }
                    console.log("image not exist");
                }
            });
        }
    }

    function IsImageExists(imageUrl, callBack) {
        var imageData = new Image();
        imageData.onload = function() {
            callBack(true);
        };
        imageData.onerror = function() {
            callBack(false);
        };
        imageData.src = imageUrl;
    }

    function createCORSRequest(method, url) {
        var xhr = new XMLHttpRequest();
        if ("withCredentials" in xhr) {
            xhr.open(method, url, true);
        } else if (typeof XDomainRequest != "undefined") {
            xhr = new XDomainRequest();
            xhr.open(method, url);
        } else {
            xhr = null;
        }
        return xhr;
    }


    function PageSettings() {
//        $("#<%=btnSaveImageLogo.ClientID %>").click(function(ev) {
//            ev.preventDefault();
//            SaveImage('logo');
//        });

//        $("#<%=btnSaveImageShuttle.ClientID %>").click(function(ev) {
//            ev.preventDefault();
//            SaveImage('shuttle');
//        });
    }
    
    
    
    function SaveImageOld() {

        var sBID = $("#<%=uoHiddenFieldBranchID.ClientID %>").val();
        var files = $("#<%=FileUploadImageLogo.ClientID %>").get(0).files;

        var savePhotoURL = '<%= ConfigurationManager.AppSettings["TM-API"] %>';
        savePhotoURL = savePhotoURL + "/AddEditPhoto"

        if (files.length > 0) {
            fileName = files[0].name;
        }

        var form = new FormData();
        form.append("Photo", files[0]);
        form.append("IDName", "HotelID");
        form.append("IDValue", sBID);
        form.append("EntityType", "hotel");

        var request = createCORSRequest('post', savePhotoURL);
        if (request) {
            $.ajax({
                async: false,
                crossDomain: true,
                url: savePhotoURL,
                type: 'POST',

                processData: false,  //lets you prevent jQuery from automatically transforming the data into a query string
                contentType: false,
                data: form,
                mimeType: 'multipart/form-data',
                success: function(d) {
                    alert("Photo successfully saved.");
                },

                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(error);
                    console.log('With error here:');
                    console.log(objXMLHttpRequest + '-' + textStatus + '-' + errorThrown);
                }
            });
        }
    }

    function SaveImage(sFrom) {
        alert('test');

        var sBID = $("#<%=uoHiddenFieldBranchID.ClientID %>").val();
        var files = null;

        if (sFrom == 'logo') {
            files = $("#<%=FileUploadImageLogo.ClientID %>").get(0).files;
        }
        else {
            files = $("#<%=FileUploadImageShuttle.ClientID %>").get(0).files;

        }


        var savePhotoURL = '<%= ConfigurationManager.AppSettings["TM-API"] %>';
        savePhotoURL = savePhotoURL + "/AddEditPhoto"

        if (files.length > 0) {
            fileName = files[0].name;
        }

        var form = new FormData();
        form.append("Photo", files[0]);

        if (sFrom == 'logo') {
            form.append("IDName", "HotelID");
            form.append("IDValue", sBID);
            form.append("EntityType", "hotel");
        }
        else {
            form.append("IDName", "HotelID");
            form.append("IDValue", sBID);
            form.append("EntityType", "shuttle");
        }


        var request = createCORSRequest('post', savePhotoURL);
        if (request) {
            request.onload = function() {

                $.ajax({
                    // "method": "POST",
                    type: 'POST',
                    //                    async: true,
                    crossDomain: true,
                    url: savePhotoURL,
                    method: 'POST',
                    processData: false,
                    contentType: false,
                    mimeType: 'multipart/form-data',
                    data: form,
                    cache: false,
                    success: function(d) {
                        var res = JSON.parse(d);
                        alert("Result: " + res.Message);
                        //alert('Photo successfully saved.');
                    },
                    error: function(objXMLHttpRequest, textStatus, errorThrown) {
                        alert('Error encountered. Check the file size or contact the System Administrator.');
                       // alert(errorThrown);
                    }
                });


            };
            //            request.beforeSend = function(xhr) {
            //                xhr.setRequestHeader('X-Requested-With', { toString: function() { return ''; } });
            //            };
            request.send();

        }
    } 
    
    //working but boundary is not
    function AddImageByCORS() {

        var sBID = $("#<%=uoHiddenFieldBranchID.ClientID %>").val();
        var files = $("#<%=FileUploadImageLogo.ClientID %>").get(0).files;

        var savePhotoURL = '<%= ConfigurationManager.AppSettings["TM-API"] %>';
        savePhotoURL = savePhotoURL + "/AddEditPhoto"

        if (files.length > 0) {
            fileName = files[0].name;
        }

        var form = new FormData();
        form.append("Photo", files[0]);
        form.append("IDName", "HotelID");
        form.append("IDValue", sBID);
        form.append("EntityType", "hotel");

//        console.log('below is the file:');
//        console.log(files[0]);

        var request = createCORSRequest('post', savePhotoURL);
        if (request) {
            request.onload = function() {

                var settings = {
                    // "method": "POST",
                    "type": "POST",
                    "async": true,
                    "crossDomain": true,
                    "url": savePhotoURL,
                    "method": "POST",
                    "processData": false,
                    "contentType": "multipart/form-data; boundary=" + form.boundary,
                    "mimeType": "multipart/form-data",
                    "data": form,
                    "cache": false,
                    "success": function(d) {
                        alert("Photo successfully saved.");
                    },
                    "error": function(objXMLHttpRequest, textStatus, errorThrown) {
                        alert('There is an error!');
                        alert(errorThrown);
                    }
                }

                $.ajax(settings).done(function(response) {

                    if (response.Success == false) {
                        alert("Error: " + response.Message);
                    }
                    else {
                        alert(response.Message);

                    }
                    console.log(response);
                });
            };
            request.beforeSend = function(xhr) {
                xhr.setRequestHeader('X-Requested-With', { toString: function() { return ''; } });
            };
            request.send();

        }
    }

    function SaveImageLogo() {

        var sBID = $("#<%=uoHiddenFieldBranchID.ClientID %>").val();
        var files = $("#<%=FileUploadImageLogo.ClientID %>").get(0).files;
        var fileName = "";

        //alert(sBID);


        var savePhotoURL = '<%= ConfigurationManager.AppSettings["TM-API"] %>';
        savePhotoURL = savePhotoURL + "/AddEditPhoto"

        if (files.length > 0) {
            fileName = files[0].name;
        }

        var form = new FormData();
        form.append("Photo", files[0]);
        form.append("IDName", "HotelID");
        form.append("IDValue", sBID);
        form.append("EntityType", "hotel");

        var settings = {
            "type": "POST",
            "async": true,
            "crossDomain": true,
            "url": savePhotoURL,
            "method": "POST",
            "processData": false,
            "contentType": false,
            "mimeType": "multipart/form-data",
            "data": form,
            "error": function(objXMLHttpRequest, textStatus, errorThrown) {
                //                alert('There is an error!');
                //                alert(errorThrown);
            }
        }

        $.ajax(settings).done(function(response) {

            if (response.Success == false) {
                alert("Error: " + response.Message);
            }
            else {
                alert(response.Message);

            }
            console.log(response);
        });
    }
    

  
</script>
</asp:Content>
