<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateUserAccount.aspx.cs"
    Inherits="TRAVELMART.CreateUserAccount" %>
    <%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Travel Mart - Login</title>
    
    <script type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }

        function validate(key) {

            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //if (((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) && !isNaN(keycode)) {
            if (keycode >= 48 && keycode <= 57) {
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
            //return true;
        }
    </script>

    <link href="~/App_Themes/Default/Stylesheet.css" rel="stylesheet" type="text/css" />

    
    <script src="../Script/jquery-3.2.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 200px;
            white-space:nowrap;
            text-align:right;
            vertical-align:top;
        }
        .style2
        {
            width: 400px;
            text-align:left;             
        }
        .style3
        {
            width: 81px;
            white-space: nowrap;
            text-align: right;
            vertical-align: top;
        }
        .style4
        {
            width: 254px;
            text-align: left;
        }
        .style5
        {
            width: 254px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function() {

            var valName = document.getElementById("<%=uoRequiredFieldValidatorContactNo.ClientID%>");

            EnableRequiredField(valName);
            $("#<%=btnTSaveImage.ClientID %>").hide();
            PreviewImage();

//            $("#<%=btnTSaveImage.ClientID %>").click(function(e) {
//                e.preventDefault();
//            });
        });


        function checkAlternateEmail(val) {

            //            val.checked = true ? document.getElementById('<%= txtAlternateEmail.ClientID %>').disabled = true : document.getElementById('<%= txtAlternateEmail.ClientID %>').disabled = false;
            if (val.checked == true)
                document.getElementById('<%= txtAlternateEmail.ClientID %>').disabled = false;
            else
                document.getElementById('<%= txtAlternateEmail.ClientID %>').disabled = true;

        }

        function CheckRole(chk, sRole) {
            var bChk = chk.checked;

            var valName = document.getElementById("<%=uoRequiredFieldValidatorContactNo.ClientID%>");

            //            ValidatorEnable(valName, false);

            EnableRequiredField(valName);

        }

        function EnableRequiredField(validatorID) {

            ValidatorEnable(validatorID, false);

            $("#<%=uoGridViewRole.ClientID %> tr").each(function(e) {

                var checkBox = $(this).find("input[id*='uoCheckBoxRole']");
                var lblRole = $(this).find('span[id*="uoLabelRoleName"]').text();

                if (checkBox.prop("checked")) {
                    //alert(lblRole);
                    if (lblRole == 'Driver' || lblRole == 'Greeter') {
                        ValidatorEnable(validatorID, true);
                    }
                }
            });
        }

        function CheckOtherIsCheckedByGVID(spanChk, id) {

            var IsChecked = spanChk.checked;
            if (IsChecked) {
                document.getElementById('<%= uoTextBoxPrimaryRole.ClientID %>').value = id;
                //                spanChk.parentElement.parentElement.style.backgroundColor = '#228b22';
                //                spanChk.parentElement.parentElement.style.color = 'white';

            }
            //alert(id);
            var CurrentRdbID = spanChk.id;
            var Chk = spanChk;

            Parent = document.getElementById('<%= uoGridViewRole.ClientID %>');

            var items = Parent.getElementsByTagName('input');

            for (i = 0; i < items.length; i++) {
                if (items[i].id != CurrentRdbID && items[i].type == "radio") {
                    if (items[i].checked) {
                        items[i].checked = false;
                        items[i].parentElement.parentElement.style.backgroundColor = 'white';
                        items[i].parentElement.parentElement.style.color = 'black';
                    }
                }
            }
        }
        function openVessel(uid) {
            var URLString = "UserVessel.aspx?uId=" + uid;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'User_Vessel', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
        function openAirport(uid) {
            var URLString = "UserAirport.aspx?uId=" + uid;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'User_Airport', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }

        function openSeaport(uid) {
            var URLString = "UserSeaport.aspx?uId=" + uid;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'User_Airport', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }

        function openPortAgent(uid) {
            var URLString = "UserPortAgent.aspx?uId=" + uid;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'User_PortAgent', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }


        function openVehicleVendor(uid) {

            var URLString = "UserVehicle.aspx?uId=" + uid;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'User_Vehicle', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
        function openDriver(uid) {
            var sUser = document.getElementById('uotextboxUName').value;

            if (uid.trim() == '') {
                if (sUser.trim() == '') {
                    alert('User name is empty!');
                } else {

                    uid = sUser.trim();
                    openDriverWithID(uid);
                    return false;
                }
            }
            else {

                openDriverWithID(uid);
                return false;
            }
        }
        function openDriverWithID(uid) {
            var URLString = "UserDriver.aspx?uId=" + uid;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'User_Driver', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no,location=no');
            return false;
        }
        function openGreeter(uid) {
            var sUser = document.getElementById('uotextboxUName').value;

            if (uid.trim() == '') {
                if (sUser.trim() == '') {
                    alert('User name is empty!');
                } else {

                    uid = sUser.trim();
                    openGreeterWithID(uid);
                    return false;
                }
            }
            else {

                openGreeterWithID(uid);
                return false;
            }
        }
        function openGreeterWithID(uid) {
            var URLString = "UserGreeter.aspx?uId=" + uid;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            var w = window.open(URLString, 'User_Greeter', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no,location=no');
            return false;
        }

        //        $(".Cancellation").fancybox({
        //            'width': '40%',
        //            'height': '45.5%',
        //            'autoScale': false,
        //            'transitionIn': 'fadeIn',
        //            'transitionOut': 'fadeOut',
        //            'type': 'iframe',
        //            'onClosed': function() {
        //                var a = $("#<%=uoHiddenFieldImmigrationPopup.ClientID %>").val();
        //                if (a == '1')
        //                    $("#aspnetForm").submit();
        //            }
        //        });


        function AddImmigrationCompany(val) {

            var URLString = '/Maintenance/ImmigrationCompany.aspx?uID=' + document.getElementById("uotextboxUName").value;


            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 640;
            screenHeight = 400;

            window.open(URLString, 'Immigration_Company', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;


            //            document.getElementById('ctl00_NaviPlaceHolder_uoListViewHotelBook_ctrl' + counter + '_ouLinkButtonBranch').href = "/CrewAssist/CrewAssistCancelHotel.aspx?hN=" + Hotelname + "&hId=" + HotelTransID + "&hTID=" + HotelTransID + "&hIdb=" + IdBigInt + "&hSno=" + seqNo + "&eadd=" + BoxEmail + "&eIsV=0&Hty=" + selectedText;
            //            val.href = "/Maintenance/ImmigrationCompany.aspx";
        }
        
        
    </script>
</head>
<body style="background-image: url('../Images/gradient.jpg'); background-repeat: repeat-x;
    font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #333;">   
    
    <%--<div style="text-align: left; margin: auto;">--%>
       <%-- <br />--%>
        <%--<asp:Panel ID="Panel3" runat="server" Style="margin: auto;">--%>
            <form id="form1" runat="server">
             <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            
            <%--<div style="text-align: left; margin: auto;">--%>
                <asp:Panel ID="Panel2" runat="server" Style="color: #D8000C; margin: auto; margin-bottom: 10px; width: 90%"
                    Visible="false">
                    <img src="../Images/exclamation.png" />&nbsp;&nbsp;<asp:Label ID="LabelLoginErrorDetails"
                        runat="server" Text=""></asp:Label>
                </asp:Panel>
                <%--<div>--%>
                   <%-- <asp:Panel ID="Panel1" runat="server" Style="background-color: White; text-align:left" 
                        BorderStyle="None" >--%>
                       <%-- <div style="padding: 5px; margin-bottom: 5px; text-align: left;">--%>
                            <table style="width:100%" >
                                <tr>
                                    <td colspan="4">
                                        <div class="PageTitle">
                                            <asp:Label ID="lblTitle" runat="server" Text="Create User" Font-Bold="true"></asp:Label>
                                        </div>
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:ValidationSummary ValidationGroup="Create" ID="ValidationSummary1" runat="server" BackColor="Beige"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style1">
                                        <asp:Label ID="Label1" runat="server" Text="User Photo:" Visible="true"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <img alt="Profile Picture" src="../Vehicle/DriverPhoto/default.JPG" ID="imgAvatar" runat="server" style="height:150px; width:150px" visible="true"/> 
                                         <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="true" >
                                            <ContentTemplate>                                                
                                                 <img alt="Profile Picture" src="../Vehicle/DriverPhoto/default.JPG" ID="imgAvatar" runat="server" style="height:150px; width:150px"/> 
                                                 
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="uobuttonSave" />
                                            </Triggers>
                                        </asp:UpdatePanel>--%>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:FileUpload ID="FileUploadImage" runat="server" onchange="javascript:PreviewImage(this);" Visible="true" />
                                        <br /><br />
                                        <asp:Button ID="btnTSaveImage" runat="server" Text="Save Image" 
                                            ToolTip="Save Image" Width="85px" onclick="btnTSaveImage_Click" />
                                         <%--<asp:Button ID="btnTSaveImage" runat="server" Text="Save Image" ToolTip="Save Image" Width="85px" OnClientClick="SaveImage();"/>--%>
                                        <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" Visible="true">
                                            <ContentTemplate>
                                                <asp:FileUpload ID="FileUploadImage" runat="server" onchange="javascript:PreviewImage(this);"  />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="uobuttonSave" />
                                            </Triggers>
                                        </asp:UpdatePanel>--%>
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="btnTSaveImageTest" runat="server" Text="Save Image"  Width="77px" OnClientClick="AddImage();" Visible="true"/>--%>
                                       
                                    </td>
                                    <td></td>
                                    
                                </tr>
                                <tr>
                                    <td align="right" class="style1">
                                        <asp:Label ID="uclabelLName" runat="server" Text="Last Name:" />
                                    </td>
                                    <td align="left" class="style4">
                                        <asp:TextBox ID="uotextboxLName" runat="server" Width="290px" TabIndex="1" />
                                    </td>
                                     <td align="right" class="style3">
                                        <asp:Label ID="uclabelUName" runat="server" Text="User Name:" />
                                    </td>
                                    <td align="left" class="style2">
                                        <asp:TextBox ID="uotextboxUName" runat="server" Width="290px" Text="" 
                                            TabIndex="5"/>&nbsp;<asp:RequiredFieldValidator
                                            ID="uorfvUname" runat="server" ControlToValidate="uotextboxUName" ErrorMessage="User Name is required."
                                            ToolTip="User Name is required." ValidationGroup="Create">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td align="right" class="style1">
                                        <asp:Label ID="uclabelFName" runat="server" Text="First Name:" />
                                    </td>
                                    <td align="left" class="style4">
                                        <asp:TextBox ID="uotextboxFName" runat="server" Width="290px" TabIndex="2" />
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator1" runat="server" ControlToValidate="uotextboxFName" ErrorMessage="First name is required."
                                            ToolTip="First name is require." ValidationGroup="Create">*</asp:RequiredFieldValidator>
                                    </td>    
                                    
                                    <%--Row for Number of Days--%>                                
                                    
                                    <td align="right" class="style3">
                                        <asp:Label ID="uclabelPWD" runat="server" Text="Password:" />
                                    </td>

                                    
                                    <td align="left" class="style2">
                                        <%--<cc1:maskededitextender ID="uotextboxNoOfDays_MaskedEditExtender" 
                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="999" MaskType="Number" TargetControlID="uotextboxNoOfDays">
                                        </cc1:maskededitextender>--%><asp:TextBox ID="uotextboxPWD" runat="server" Width="290px" TextMode="Password" 
                                            Text="" TabIndex="6"/>
                                        <asp:RequiredFieldValidator
                                            ID="uorfvPWD" runat="server" ControlToValidate="uotextboxPWD" ErrorMessage="Password is required."
                                            ToolTip="Password is required." ValidationGroup="Create">*</asp:RequiredFieldValidator>
                                    </td>
                                    
                                                 
                                </tr>
                                
                                <tr>
                                    <td align="right" class="style1">
                                        <asp:Label ID="uclabel" runat="server" Text="Email Address:" />
                                    </td>
                                    <td align="left" class="style4">
                                        <asp:TextBox ID="uotextboxEmail" runat="server" Width="290px" TabIndex="3" />&nbsp;
                                        <asp:RequiredFieldValidator
                                            ID="uorfvEmail" runat="server" ControlToValidate="uotextboxEmail" ErrorMessage="Email is required."
                                            ToolTip="Email is required." ValidationGroup="Create">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="uoregexvalEmail" ControlToValidate="uotextboxEmail"
                                            runat="server" ErrorMessage="Invalid Email" ToolTip="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="Create">*</asp:RegularExpressionValidator>
                                    </td>                                    
                                    
                                    <%--<td align="right" class="style1"/>
                                    <td align="left" class="style2"/>--%>
                                    
                                    <td align="right" class="style3">
                                        <asp:Label ID="uclabelPWD1" runat="server" Text="Confirm Password:" />
                                    </td>
                                    <%--<td align="left" class="style2">
                                        <asp:TextBox ID="uotextboxPWD1" runat="server" Width="290px" TextMode="Password" />
                                        &nbsp;<asp:RequiredFieldValidator
                                            ID="uorfvPWD1" runat="server" ControlToValidate="uotextboxPWD1" ErrorMessage="Please re-type password."
                                            ToolTip="Please re-type password." ValidationGroup="Create">*</asp:RequiredFieldValidator>
                                    </td>--%>
                                    <td align="left" class="style2">
                                        <asp:TextBox ID="uotextboxPWD1" runat="server" Width="290px" 
                                            TextMode="Password" TabIndex="7" />
                                        <asp:RequiredFieldValidator
                                            ID="uorfvPWD1" runat="server" ControlToValidate="uotextboxPWD1" ErrorMessage="Please re-type password."
                                            ToolTip="Please re-type password." ValidationGroup="Create">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td class="RightClass">
                                            Use Alternate Email 
                                         <asp:CheckBox runat="server"   ID="chkAlternateEmail" onclick="checkAlternateEmail(this)"/>
                                        
                                            
                                    </td>
                                    <td colspan="1" class="style5" >
                                     <asp:TextBox runat="server" ID="txtAlternateEmail" Width="290px" Enabled="false"  />   
                                    
                                    
                                    </td>
                                     <td align="right" class="style3">
                                         &nbsp;
                                    </td>
                                    
                                    <td align="left" class="style2">
                                        <%--<span class="RedNotification" runat="server" id = "uoSpanPwd">Password must be at least 8 characters long and a combination of Alpha-Numeric and Special Characters. </span>--%>
                                        <asp:Label class="RedNotification" runat ="server" ID="ucLabelPassword" Text="Password must be at least 8 characters long and a combination of Alpha-Numeric and Special Characters."></asp:Label>
                                    </td>  
                                    
                                </tr>
                                <tr valign="top">        
                                    <td align="right" class="style1">
                                        <asp:Label ID="Label2" runat="server" Text="No. of Days to Display:" />
                                    </td>
                                    <td align="left"  class="style4">
                                      
                                        <asp:TextBox ID="uotextboxNoOfDays" runat="server" Width="100px" onkeypress="return validate(event);" 
                                            TextMode="SingleLine" TabIndex="4"  />
                                       
                                    </td>                                    
                                    
                                
                                    <td align="right" valign="top" class="style3" rowspan="3">
                                        <%--<asp:Label ID="Label1" runat="server" Text="Profile Picture:" style='display:none' />--%>
                                    </td>  
                                    <td rowspan="3">
                                   
                                    </td>                                                    
                                </tr> 
                                 <tr>
                                    <td align="right" class="style1" valign="top">
                                          <asp:Label ID="Label3" runat="server" Text="Contact No.:" />
                                    </td>
                                    <td align="left"  class="style4" valign="top">
                                         <asp:TextBox ID="uoTextBoxContactNo" runat="server" Width="290px" />
                                         <asp:RequiredFieldValidator
                                            ID="uoRequiredFieldValidatorContactNo" runat="server" ControlToValidate="uoTextBoxContactNo" ErrorMessage="Contact no. is required."
                                            ToolTip="Contact no. is required." ValidationGroup="Create">*</asp:RequiredFieldValidator>
                                    </td>
                                    
                                 </tr>                            
                                
                                <tr>
                                    <td align="right" valign="top" class="style1">
                                        <asp:Label ID="uclabelRole" runat="server" Text="User Role:" />
                                    </td>                                    
                                    <td align="left" valign="top"  colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanelRole" runat="server">
                                        <ContentTemplate>                                    
                                        <%--<asp:CheckBoxList ID="uoCheckBoxListRole" runat="server" AutoPostBack="true" 
                                                onselectedindexchanged="uoCheckBoxListRole_SelectedIndexChanged">
                                        </asp:CheckBoxList>--%>
                                            <asp:GridView ID="uoGridViewRole" runat="server" AutoGenerateColumns="False" GridLines="None"                                          
                                                Width="750px" BorderStyle="None" TabIndex="8" >                                            
                                                <Columns>   
                                                <asp:BoundField DataField="RoleId" HeaderText="Value" ItemStyle-Width="10px" HeaderStyle-Width="10px">
                                                    <HeaderStyle Width="10px" />
                                                    <ItemStyle Width="10px" />
                                                    </asp:BoundField>
                                               <asp:TemplateField  ItemStyle-Width="30px" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" HeaderText = "">
                                                <ItemTemplate >
                                                    <asp:CheckBox ID="uoCheckBoxRole" runat="server"  Width="30px" Enabled='<%# Convert.ToBoolean(Eval("IsWithAccess")) %>'
                                                        Checked = <%# Eval("IsSelected") %> AutoPostBack="true" onclick=<%#"javascript:CheckRole(this, '"+ Eval("RoleName") + "');" %>
                                                        oncheckedchanged="uoCheckBoxRole_CheckedChanged"/>                                                        
                                                </ItemTemplate>                                                    
                                                   <HeaderStyle Width="30px" />
                                                   <ItemStyle HorizontalAlign="Left" Width="30px" />
                                                </asp:TemplateField>
                                               <%-- <asp:BoundField DataField="rolename" HeaderText="" ItemStyle-Width="150px" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left">                                             
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Width="150px" />
                                                    </asp:BoundField>--%>
                                                <asp:TemplateField  ItemStyle-Width="150px" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left" HeaderText = "">
                                                 <ItemTemplate >
                                                    <asp:Label ID="uoLabelRoleName" runat="server" Text = '<%# Eval("rolename") %>' Width="200px" ></asp:Label>
                                                     <HeaderStyle Width="150px" />
                                                     <ItemStyle HorizontalAlign="Left" Width="30px" />                                                       
                                                </ItemTemplate>               
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="" ItemStyle-Width="20px" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                                    <ItemTemplate >
                                                        <%--<asp:RadioButton ID="uoRadioButtonPrimary" onclick="javascript:CheckOtherIsCheckedByGVID(this,<%# Eval("RoleId") %>);" Width="5px" runat="server" Checked=<%# Eval("IsPrimary") %>/>--%>
                                                        <asp:RadioButton ID="uoRadioButtonPrimary" onclick=<%# "javascript:CheckOtherIsCheckedByGVID(this, '" + Eval("RoleId") + "');" %> Width="5px" runat="server" Checked=<%# Eval("IsPrimary") %>/>
                                                    </ItemTemplate>                                                    
                                                      <HeaderStyle Width="20px" />
                                                      <ItemStyle HorizontalAlign="Left" Width="20px" Wrap="False" />
                                                </asp:TemplateField>    
                                                <asp:TemplateField HeaderText="" ItemStyle-Width="305px" HeaderStyle-Width="310px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                                    <ItemTemplate >
                                                        <asp:HiddenField ID="uoHiddenFieldIsWithAccess" runat="server" Value=<%# Eval("IsWithAccess").ToString() %>/>
                                                        <asp:HiddenField ID="uoHiddenFieldBranch" runat="server" Value=<%# Eval("BranchID") %>/>
                                                        &nbsp;
                                                        <asp:LinkButton ID="uoLinkButtonVessel" runat="server" Text="Vessel"></asp:LinkButton>
                                                        &nbsp;
                                                        <asp:LinkButton ID="uoLinkButtonPortAgent" runat="server" Text="Service Provider" OnClientClick=<%# "openPortAgent('" + uotextboxUName.Text  + "')" %> ></asp:LinkButton>
                                                        &nbsp;                                                        
                                                        <asp:DropDownList ID="uoDropDownListBranch" runat="server" Width="400px" AppendDataBoundItems="true">
                                                        </asp:DropDownList>
                                                         &nbsp;  
                                                        <asp:Button ID="uoButtonAddCompany" runat="server" Text="..." ToolTip="Add Immigration Company" 
                                                            CssClass="SmallButton"  OnClientClick="return AddImmigrationCompany(this)"/>
                                                    </ItemTemplate>                                                    
                                                    <HeaderStyle Width="310px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="305px" Wrap="False" />
                                                </asp:TemplateField>                                                                                           
                                                <asp:TemplateField HeaderText="" ItemStyle-Width="305px" HeaderStyle-Width="310px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                                    <ItemTemplate >
                                                         <asp:RequiredFieldValidator
                                                        ID="uoRequiredFieldValidatorBranch" runat="server" ControlToValidate="uoDropDownListBranch" ErrorMessage="Select Branch"
                                                        ToolTip="Required" ValidationGroup="Create" InitialValue="0">*</asp:RequiredFieldValidator>
                                                    </ItemTemplate>                                                    
                                                    <HeaderStyle Width="310px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="305px" Wrap="False" />
                                                </asp:TemplateField>
                                               
                                                </Columns>                                               
                                            </asp:GridView>
                                                                                                                                     
                                       <%-- <asp:DropDownList ID="uodropdownlistRole" runat="server" Width="295px" DataTextField="role"
                                            DataValueField="rid" AppendDataBoundItems="true" AutoPostBack="true"
                                            onselectedindexchanged="uodropdownlistRole_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="--Select Role--" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="uorfvRole" runat="server" ControlToValidate="uodropdownlistRole"
                                            ErrorMessage="Role is required." ToolTip="Email is required." ValidationGroup="Create"
                                            InitialValue="0">*</asp:RequiredFieldValidator>--%>
                                            <asp:TextBox ID="uoTextBoxPrimaryRole" runat="server" Text="" CssClass="hideElement" ></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="uoRequiredFieldValidatorPrimary" runat="server" ControlToValidate="uoTextBoxPrimaryRole"
                                               ValidationGroup="Create"  ErrorMessage="Select Primary Role">&nbsp</asp:RequiredFieldValidator>  
                                            </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </td>                                                                       
                                </tr>
                                <tr>
                                     <td align="right" runat="server" id="uoTDLabelBranch" visible="false" 
                                         class="style1">
                                        Branch Name:
                                    </td>
                                    <td align="left" runat="server" id="uoTDBranch" visible="false" class="style4">
                                        <asp:DropDownList ID="uoDropDownListBranch" Width="295px"
                                            AppendDataBoundItems="true" runat="server">
                                            <asp:ListItem Value="0">--SELECT BRANCH--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uoDropDownListBranch"
                                            ErrorMessage="Branch is required." ToolTip="Branch is required." ValidationGroup="Create"
                                            InitialValue="0">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="style3"></td>
                                    <td class="style2"></td>
                                </tr>
                                <tr>
                                    <td class="style1"></td>                                    
                                    <td colspan="3"  class="style2" >
                                        <asp:HiddenField runat="server" Value="false" ID="uoHiddenfieldIsActive" />
                                        <asp:HiddenField runat="server" Value="0" ID="uoHiddenFieldOldUserName" />                                        
                                    </td>
                                    
                                </tr>
                                 <tr>
                                 <td class="style1" />
                                    <td align="right" class="style2" colspan="3">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="uobuttonSave" runat="server" Text="Save" ValidationGroup="Create"
                                                    Width="77px" OnClick="uobuttonSave_Click" TabIndex="10" OnClientClick="javascript:return SaveButton();" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                  
                                    
                                    &nbsp;&nbsp;&nbsp;<asp:HiddenField ID="uohiddenuid"
                                        runat="server" />
                                        
                                       <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value=""/>
                                    </td>
                                </tr>
                            </table>                           
                       <%-- </div>--%>
                    <%--</asp:Panel>--%>
               <%-- </div>--%>
           <%-- </div>--%>
           
           
                <asp:HiddenField ID="uoHiddenFieldImmigrationPopup" runat="server" Value=""  />
                <asp:HiddenField ID="uoHiddenFieldIsLDAPOn" runat="server" Value="0"  />
                <asp:HiddenField ID="uoHiddenFieldEmailOld" runat="server" Value=""  />
                
            </form>
       <%-- </asp:Panel>--%>
   <%-- </div>--%>
</body>
<script type="text/javascript">
    //    Sys.Application.add_load(function() {
    //        AddImage();
    //    });

    function AddImage() {
        alert('333');
        var sUser = document.getElementById('uotextboxUName').value;
        var MediaURL = '<%= ConfigurationManager.AppSettings["MediaURL"] %>';
        var MediaToken = '<%= ConfigurationManager.AppSettings["MediaToken"] %>';
        var sApiurl = MediaURL + '/vendors/transportation/driver';

        alert(sApiurl);

        var xhr = new XMLHttpRequest();
        xhr.open('post', 'http://10.80.0.49:7000/vendors/transportation/driver', true);
        xhr.onload = function() {  //instead of onreadystatechange
            AddImage2();
        };
        xhr.send(null);
    }

    function AddImage2() {
        var sUser = document.getElementById('uotextboxUName').text();
        alert(sUser);
        //        var MediaURL = '<%= ConfigurationManager.AppSettings["MediaURL"] %>';
        //        var MediaToken = '<%= ConfigurationManager.AppSettings["MediaToken"] %>';
        //        var sApiurl = MediaURL + '/vendors/transportation/driver';
        //        var sHeader = '<%= ConfigurationManager.AppSettings["MediaHeader"] %>';
        //
        var sURL = 'http://localhost:62118/api/UserPhoto';

        var files = $("#<%=FileUploadImage.ClientID %>").get(0).files;
        var data = new FormData();

        data.append('Username', sUser); // Add the uploaded image content to the form data collection

        if (files.length > 0) {
            data.append('Userphot', files[0].name);
        }




        $.ajax({
            async: false,
            crossDomain: true,
            url: sApiurl,
            type: 'POST',

            processData: false,  //lets you prevent jQuery from automatically transforming the data into a query string
            contentType: false,
            data: data,
            mimeType: 'multipart/form-data',
            success: function(d) {
                alert("Saved Successfully");
            },

            error: function(objXMLHttpRequest, textStatus, errorThrown) {
                alert(error);

                console.log(objXMLHttpRequest + '-' + textStatus + '-' + errorThrown);
            }
        });


        alert('Done!');
        return false;

    }

    function PreviewImage(ImagePath) {
       
        var sSrc = "../Vehicle/DriverPhoto/default.JPG";

        var MediaURL = '<%= ConfigurationManager.AppSettings["MediaURL"] %>';
        var MediaToken = '<%= ConfigurationManager.AppSettings["MediaToken"] %>';
        var sApiurl = MediaURL + '/vendors/transportation/driver';

        var sApiurl = MediaURL + '/vendors/transportation/driver';
        sApiurl = sApiurl + "/" + $("#<%=uotextboxUName.ClientID %>").val() + "?at=" + MediaToken;

        if (ImagePath) {
            //            alert('with ImagePath');
            if (ImagePath.files && ImagePath.files[0]) {
                var Filerdr = new FileReader();
                Filerdr.onload = function(e) {
                    $('#<%=imgAvatar.ClientID %>').attr('src', e.target.result);
                }

                $("#<%=btnTSaveImage.ClientID %>").show();
                Filerdr.readAsDataURL(ImagePath.files[0]);
            }
            else {
                //No file selected
                $("#<%=btnTSaveImage.ClientID %>").hide();
                $('#<%=imgAvatar.ClientID %>').attr('src', sSrc);
            }
        }
        else {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../PageMethods.aspx/GetPhoto",
                data: "{'URL': '" + sApiurl + "'}",
                dataType: "json",
                success: function(data) {
                    if (data.d.Image != null) {
                        var result = "data:image/*;base64," + data.d.Image;
                        $('#<%=imgAvatar.ClientID %>').attr('src', result);
                        //                    $('#<%=imgAvatar.ClientID %>').attr('src', res).width(304).height(150);
                    }
                    else {
                        console.log("Ooops! no image found!");

                    }
                }
            });
        }
    }

    function PreviewImageFromPanda(ImagePath) {
        var sSrc = "../Vehicle/DriverPhoto/default.JPG";

        var MediaURL = '<%= ConfigurationManager.AppSettings["MediaURL"] %>';
        var MediaToken = '<%= ConfigurationManager.AppSettings["MediaToken"] %>';
        var sApiurl = MediaURL + '/vendors/transportation/driver';

        var sApiurl = MediaURL + '/vendors/transportation/driver';
        sApiurl = sApiurl + "/" + $("#<%=uotextboxUName.ClientID %>").val() + "?at=" + MediaToken;


        if (ImagePath) {
            //            alert('with ImagePath');
            if (ImagePath.files && ImagePath.files[0]) {
                var Filerdr = new FileReader();
                Filerdr.onload = function(e) {
                    $('#<%=imgAvatar.ClientID %>').attr('src', e.target.result);
                }

                $("#<%=btnTSaveImage.ClientID %>").show();
                Filerdr.readAsDataURL(ImagePath.files[0]);
            }
            else {
                //No file selected
                $("#<%=btnTSaveImage.ClientID %>").hide();
                $('#<%=imgAvatar.ClientID %>').attr('src', sSrc);
            }
        }
        else {

            IsImageExists(sApiurl, function(existsImage) {
                if (existsImage == true) {
                    $('#<%=imgAvatar.ClientID %>').attr('src', sApiurl);
                    console.log("image exist");
                }
                else {
                    $('#<%=imgAvatar.ClientID %>').attr('src', sSrc);
                    console.log("image not exist");
                }
            });

        }
        //        else if (IsImageExists(sApiurl)) {
        //        $('#<%=imgAvatar.ClientID %>').attr('src', sApiurl);
        //           
        //        }
        //        else {
        //            alert('no avatar');
        //            $("#<%=btnTSaveImage.ClientID %>").hide();
        //            var sSrc = "../Vehicle/DriverPhoto/default.JPG";
        //            $('#<%=imgAvatar.ClientID %>').attr('src', sSrc);
        //        }
    }


    function PreviewImageFromTMAPI(ImagePath) {
        var sSrc = "../Vehicle/DriverPhoto/default.JPG";

        var MediaURL = '<%= ConfigurationManager.AppSettings["TM-API"] %>';

        var sApiurl = MediaURL + '/getPhoto';
        sApiurl = sApiurl + "/" + $("#<%=uotextboxUName.ClientID %>").val() + "/user";


        if (ImagePath) {
            //            alert('with ImagePath');
            if (ImagePath.files && ImagePath.files[0]) {
                var Filerdr = new FileReader();
                Filerdr.onload = function(e) {
                    $('#<%=imgAvatar.ClientID %>').attr('src', e.target.result);
                }

                $("#<%=btnTSaveImage.ClientID %>").show();
                Filerdr.readAsDataURL(ImagePath.files[0]);
            }
            else {
                //No file selected
                $("#<%=btnTSaveImage.ClientID %>").hide();
                $('#<%=imgAvatar.ClientID %>').attr('src', sSrc);
            }
        }
        else {

            IsImageExists(sApiurl, function(existsImage) {
                if (existsImage == true) {
                    $('#<%=imgAvatar.ClientID %>').attr('src', sApiurl);
                    console.log("image exist");
                }
                else {
                    $('#<%=imgAvatar.ClientID %>').attr('src', sSrc);
                    console.log("image not exist");
                }
            });

        }
        //        else if (IsImageExists(sApiurl)) {
        //        $('#<%=imgAvatar.ClientID %>').attr('src', sApiurl);
        //           
        //        }
        //        else {
        //            alert('no avatar');
        //            $("#<%=btnTSaveImage.ClientID %>").hide();
        //            var sSrc = "../Vehicle/DriverPhoto/default.JPG";
        //            $('#<%=imgAvatar.ClientID %>').attr('src', sSrc);
        //        }
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


    function getImage(ImagePath) {
        var request = createCORSRequest('get', ImagePath);
        alert('hi');
        if (request) {
            alert('request');
            request.onload = function() {

                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": ImagePath,
                    "method": "GET",
                    "success": function(d) {

                        $('#<%=imgAvatar.ClientID %>').attr('src', d);


                    },
                    "error": function(objXMLHttpRequest, textStatus, errorThrown) {
                        alert('with error');
                        return errorThrown
                    }
                }

                $.ajax(settings).done(function(response) {

                    alert('oK!');

                })

                .fail(function(xhr, textStatus, errorThrown) {
                    alert('Fail daw');
                    alert(textStatus);
                });
            }
        }
        request.send();
    }

    function checkImageExists(ImagePath) {
        var request = createCORSRequest('get', ImagePath);

        if (request) {
            request.onload = function() {

                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": ImagePath,
                    "method": "GET",
                    "success": function(d) {
                        $('#<%=imgAvatar.ClientID %>').attr('src', d);

                        alert("Saved Successfully");
                    },
                    "error": function(objXMLHttpRequest, textStatus, errorThrown) {
                        alert('with error');
                        return false
                    }
                }

                $.ajax(settings).done(function(response) {

                    alert('oK!');
                    return true;
                })

                .fail(function(xhr, textStatus, errorThrown) {
                    alert(xhr.responseText);
                    alert(textStatus);
                });
            }
        }
        request.send();
        return true;
        //        try {
        //            $('#<%=imgAvatar.ClientID %>').attr('src', ImagePath);
        //            return true;
        //        }
        //        catch (err) {
        //            alert(err);
        //            return false;
        //        }
    }

    function TestCORS() {
        alert('22');
        var xhr = new XMLHttpRequest();
        xhr.open('post', 'http://localhost:62118/api/photo', true);
        xhr.onload = function() {  //instead of onreadystatechange
            TestCORS2();
        };
        xhr.send(null);
    }

    function TestCORS2() {

        var files = $("#<%=FileUploadImage.ClientID %>").get(0).files;
        var fileName = "123";
        if (files.length > 0) {
            fileName = files[0].name;
        }


        var settings = {
            "async": true,
            "crossDomain": true,
            "url": "http://localhost:62118/api/photo",
            "method": "POST",
            "headers": {
                "content-type": "application/x-www-form-urlencoded",
                "cache-control": "no-cache"
            },
            "data": fileName
        }

        $.ajax(settings).done(function(response) {
            console.log(response);
            alert('123');

        });
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

    function AddImageByCORS() {
        var sURL = 'http://localhost:62118/api/UserPhoto';
        alert('555');

        var files = $("#<%=FileUploadImage.ClientID %>").get(0).files;
        var fileName = "123";
        if (files.length > 0) {
            fileName = files[0].name;
        }


        var sUsername = $("#<%=uotextboxUName.ClientID %>").text();

        //var data = "{'Username': '" + sUsername + "', 'Asset': " + files + " }";

        var form = new FormData();
        form.append("Userphoto", fileName);
        form.append("Username", sUsername);


        var request = createCORSRequest('post', sURL);
        if (request) {
            request.onload = function() {

                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": sURL,
                    "method": "POST",
                    "headers": {
                        "cache-control": "no-cache"
                    },
                    "processData": false,
                    "contentType": false,
                    "mimeType": "multipart/form-data",
                    "data": form
                }

                $.ajax(settings).done(function(response) {
                    alert('123');
                    console.log(response);


                });
            };
            request.send();

        }
    }

    function AddImageByCORS2() {
        var sUsername = $("#<%=uotextboxUName.ClientID %>").val();
        var files = $("#<%=FileUploadImage.ClientID %>").get(0).files;
        var fileName = "";

        var savePhotoURL = '<%= ConfigurationManager.AppSettings["TM-API"] %>';
        savePhotoURL = savePhotoURL + "/AddEditPhoto"

        if (files.length > 0) {
            fileName = files[0].name;
        }

        var form = new FormData();
        form.append("Photo", files[0]);
        form.append("IDName", "DriverID");
        form.append("IDValue", sUsername);
        form.append("EntityType", "user");


        var request = createCORSRequest('post', savePhotoURL);
        if (request) {
            request.onload = function() {

                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": savePhotoURL,
                    "method": "POST",
                    "headers": {
                        "cache-control": "no-cache"
                    },
                    "processData": false,
                    "contentType": false,
                    "mimeType": "multipart/form-data",
                    "data": form
                }

                $.ajax(settings).done(function(response) {
                    alert('Image successfully saved!');
                    console.log(response);
                })
                .fail(function(xhr, textStatus, errorThrown) {
                    alert(xhr.responseText);
                    alert(textStatus);
                })
            };
            request.send();

        }
    }

    function SaveImageOld() {

        var sUsername = $("#<%=uotextboxUName.ClientID %>").val();
        var files = $("#<%=FileUploadImage.ClientID %>").get(0).files;
        var fileName = "";

        var savePhotoURL = '<%= ConfigurationManager.AppSettings["TM-API"] %>';
        savePhotoURL = savePhotoURL + "/AddEditPhoto"

        console.log('savePhotoURL: ' + savePhotoURL);

        if (files.length > 0) {
            fileName = files[0].name;
        }

        var form = new FormData();
        form.append("Photo", files[0]);
        form.append("IDName", "DriverID");
        form.append("IDValue", sUsername);
        form.append("EntityType", "user");

        $.ajax({
            async: true,
            url: savePhotoURL,
            method: "POST",
            processData: false,
            contentType: false,
            mimeType: "multipart/form-data",
            data: form,
            error: function(objXMLHttpRequest, textStatus, errorThrown) {
                //alert('Error encountered. Check the file size or contact the System Administrator.');
                // alert("Oopss error found");
                //alert(objXMLHttpRequest);
                //alert(errorThrown);
                console.log('1: ' + objXMLHttpRequest);
                console.log('2: ' + textStatus);

                console.log('3: ' + errorThrown);
            },
            success: function(response) {
                //                var res = JSON.parse(response);

                //                alert("Result: " + res.Message);
                //                console.log('This is the result: ' + response);

                if (response.Success == false) {
                    alert("Error: " + response.Message);

                }
                else {

                    var res = JSON.parse(response);
                    alert("Result: " + res.Message);

                }
            }
        });


    }

    function SaveImage() {

        var sUsername = $("#<%=uotextboxUName.ClientID %>").val();
        var files = $("#<%=FileUploadImage.ClientID %>").get(0).files;
        var fileName = "";

        var savePhotoURL = '<%= ConfigurationManager.AppSettings["TM-API"] %>';
        savePhotoURL = savePhotoURL + "/AddEditPhoto"

        if (files.length > 0) {
            fileName = files[0].name;
        }

        var form = new FormData();
        form.append("Photo", files[0]);
        form.append("IDName", "DriverID");
        form.append("IDValue", sUsername);
        form.append("EntityType", "user");

        console.log('savePhotoURL: ' + savePhotoURL);

        var settings = {
            "async": true,
            "crossDomain": true,
            "url": savePhotoURL,
            "method": "POST",
            "type": "POST",
            "processData": false,
            "contentType": false,
            "mimeType": "multipart/form-data",
            "data": form,
            "error": function(objXMLHttpRequest, textStatus, errorThrown) {
                alert('Error encountered. Check the file size or contact the System Administrator.');
                console.log('Error: ' + errorThrown);
            }
        }

        $.ajax(settings).done(function(response) {
            console.log('Success');
            if (response.Success == false) {
                alert("Error: " + response.Message);

            }
            else {

                var res = JSON.parse(response);
                alert("Result: " + res.Message);

            }
        });
    }

    function SaveButton() {
        if (Page_ClientValidate()) {

            var sLogName = document.getElementById('uoHiddenFieldUser').value;
            var sUserToCheck = document.getElementById('uotextboxUName').value;
            var toSubmit = true;

            //loop in gridview
            var IsDriverGreeter = false;
            var sRoleToShow = '';
            var sRoleToCheck = '';

            $("#<%=uoGridViewRole.ClientID %> tr").each(function(e) {

                var checkBox = $(this).find("input[id*='uoCheckBoxRole']");
                var lblRole = $(this).find('span[id*="uoLabelRoleName"]').text();

                if (checkBox.prop("checked")) {
                    if (lblRole == 'Driver' || lblRole == 'Greeter') {

                        if (lblRole == 'Driver') {
                            if (sRoleToShow == '') {
                                sRoleToShow = 'Driver';
                                sRoleToCheck = 'Driver';
                            }
                            else {
                                sRoleToShow = sRoleToShow + '/Driver';
                                sRoleToCheck = sRoleToCheck + ',Driver'
                            }
                        }
                        else if (lblRole == 'Greeter') {
                            if (sRoleToShow == '') {
                                sRoleToShow = 'Greeter';
                                sRoleToCheck = 'Greeter';
                            }
                            else {
                                sRoleToShow = sRoleToShow + '/Greeter';
                                sRoleToCheck = sRoleToCheck + ',Greeter'
                            }
                        }
                        IsDriverGreeter = true;
                    }
                }
            });

            if (IsDriverGreeter == true) {
                toSubmit = false;

                //Check if Driver or Greeter has Vendor assignment
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "../PageMethods.aspx/IsUserWithVendor",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{'sLogName' :'" + sLogName + "', 'sUsername' :'" + sUserToCheck + "', 'sRoleToCheck': '" + sRoleToCheck + "'}",
                    success: function(data) {
                        //console.log('output is:');
                        //console.log(data.d);
                        toSubmit = data.d;

                    },

                    error: function(objXMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                        console.log(objXMLHttpRequest + '-' + textStatus + '-' + errorThrown);
                    }
                });
            }

            console.log(toSubmit);
            if (toSubmit == true) {
                return true;
            }
            else {

                if (IsDriverGreeter) {
                    sRoleToShow = 'No assigned Vendor for ' + sRoleToShow + ' Role!';
                    alert(sRoleToShow);
                    return false;
                }
                else {
                    return true;
                }
            }
        }
    }
    
  
</script>
</html>