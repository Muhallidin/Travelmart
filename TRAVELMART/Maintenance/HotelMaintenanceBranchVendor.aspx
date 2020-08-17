<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster2.Master" AutoEventWireup="True" 
CodeBehind="HotelMaintenanceBranchVendor.aspx.cs" Inherits="TRAVELMART.HotelMaintenanceBranchVendor" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    
    <script src="../Script/nicEdit.js" type="text/javascript"></script>
    
    <script type="text/javascript">         
        bkLib.onDomLoaded(function() {

        new nicEditor({ buttonList: ['bold', 'italic', 'underline', 'strikeThrough', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'indent', 'outdent', 'removeformat'] }).panelInstance('ctl00_NaviPlaceHolder_uoTextBoxInstructionOn');
        new nicEditor({ buttonList: ['bold', 'italic', 'underline', 'strikeThrough', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'indent', 'outdent', 'removeformat'] }).panelInstance('ctl00_NaviPlaceHolder_uoTextBoxInstructionOff');
        });
    </script>

   <div class="PageTitle">
        <asp:Label ID="lblHotelName" runat="server" Text="HotelName" CssClass="Title"></asp:Label>
   </div>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
  
<script type="text/javascript" language="javascript">


    $(document).ready(function() {
        $("#<%=btnSaveImageLogo.ClientID %>").hide();
        $("#<%=btnSaveImageShuttle.ClientID %>").hide();

        PreviewImage('logo');
        PreviewImage('shuttle');
        PageSettings();
        //checkFirstVisit();
        
    });

    function checkFirstVisit() {
        if (document.cookie.indexOf('IsFirstVisit') == -1) {
            // cookie doesn't exist, create it now
            document.cookie = 'IsFirstVisit=1';

           
        }
        else {
            //Not 1st load so do nothing...
        }
    }
   
       function PageSettings() {
//           $("#<%=btnSaveImageLogo.ClientID %>").click(function(ev) {
//               ev.preventDefault();
//               SaveImage('logo');
//           });

//           $("#<%=btnSaveImageShuttle.ClientID %>").click(function(ev) {
//               ev.preventDefault();
//               SaveImage('shuttle');
//           });
       }

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
               sApiUrl = sApiUrl + "/HT-" + $("#<%=uoHiddenFieldBranchID.ClientID %>").val() + "/hotel";
           }
           else {
               sApiUrl = sApiUrl + "/HT-" + $("#<%=uoHiddenFieldBranchID.ClientID %>").val() + "/shuttle";
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

       function SaveImage(sFrom) {

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
    
    </script>
    
    <table>
        <tr>
            <td>
                <%--<table width="100%">
                    <tr>
                        <td class="PageTitle">
                            Photo
                        </td>
                    </tr>
                </table>--%>
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <table style="text-align: left; width: 90%;" align="center">
                    <tr>
                        <td class="contentCaption" style="vertical-align: top; width: 10%; white-space:nowrap">
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
                        <td class="contentCaption" style="vertical-align: top; width: 10%; white-space:nowrap">
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
                        <asp:PostBackTrigger ControlID="btnSaveImageShuttle"/>
                        <asp:PostBackTrigger ControlID="btnSaveImageLogo"/>                        
                    </Triggers>
                </asp:UpdatePanel>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanelHotelBranch" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td class="PageTitle">
                                Information
                            </td>
                        </tr>
                     </table>
                     
                    <table style="text-align: left; width: 90%;" align="center">
                        <tr>
                            <td class="contentCaption" style="vertical-align: top; width: 10%; white-space:nowrap">
                                &nbsp; Signing ON Instructions:
                            </td>
                            <td class="contentValue">                                                    
                                 <asp:TextBox ID="uoTextBoxInstructionOn" runat="server"  Width="500px" TextMode="MultiLine"  Rows="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="contentCaption" style="vertical-align: top; width: 10%; white-space:nowrap">
                                &nbsp; Signing OFF Instructions:
                            </td>
                            <td class="contentValue">
                                 <asp:TextBox ID="uoTextBoxInstructionOff" runat="server"  Width="500px" TextMode="MultiLine" Rows="10" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center" colspan="2">
                                <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80" OnClick="uoButtonSave_Click"/>
                                <asp:Button ID="uoButtonBack" runat="server" Text="Back" Width="80" OnClick="uoButtonBack_Click"/>
                            </td>
                        </tr>
                    </table>
                 </ContentTemplate>
                 <Triggers >
                    <asp:PostBackTrigger ControlID="uoButtonSave"/>
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
       
    </table>
   
       
    <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    
        
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="SidePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
     
</asp:Content>
--%>