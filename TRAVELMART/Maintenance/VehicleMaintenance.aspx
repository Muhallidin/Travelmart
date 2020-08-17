<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster2.Master" AutoEventWireup="true" CodeBehind="VehicleMaintenance.aspx.cs" Inherits="TRAVELMART.Maintenance.VehicleMaintenance" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">  

   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
     <ContentTemplate>

    <div class="PageTitle VehicleVendor">           
        Vehicle Vendor
    </div>
    <hr class="VehicleVendor"/>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <table width="100%">
        <%--<tr>
                <td>&nbsp Code:</td>
                <td>
                    <asp:TextBox ID="uoTextBoxVendorCode" runat="server" Width="300px"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="uoTextBoxVendorCode" ErrorMessage="Vendor code required." ValidationGroup="Save">*</asp:RequiredFieldValidator>                          
                </td>        
            </tr>    
        --%>
        <tr class="VehicleVendor">
            <td class="LeftClass" style="width:400px">Name:</td>
            <td class="LeftClass" >
                <asp:TextBox ID="uoTextBoxVendorName" runat="server" Width="300px" TextMode="MultiLine" CssClass="TextBoxInput"
                    Height="35px"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoTextBoxVendorName" ErrorMessage="Vendor name required." ValidationGroup="Save">*</asp:RequiredFieldValidator></td>        
            <td class="LeftClass">
               Vendor ID:
            </td>
            <td class="LeftClass" colspan="3">
                <asp:TextBox ID="uoTextBoxVendorID" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>     
        <tr class="VehicleVendor">
            <td class="LeftClass" style="width:400px">Address:</td>
            <td  class="LeftClass">
                <asp:TextBox ID="uoTextBoxVendorAddress" runat="server" Width="300px" TextMode="MultiLine" CssClass="TextBoxInput"></asp:TextBox> 
                    <%--&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uoTextBoxVendorAddress" ErrorMessage="Vendor address required." ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>               
            </td>
            <td class="LeftClass">Website:</td>
            <td class="LeftClass" colspan="1">
                <asp:TextBox ID="uoTextBoxWebsite" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr class="VehicleVendor">
            <td class="LeftClass" style="width:400px">
                Email To:
            </td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxEmailTo" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td class="LeftClass">
                Email Cc:
            </td>
            <td class="LeftClass" colspan="1">
                <asp:TextBox ID="uoTextBoxEmailCc" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr class="VehicleVendor">
            <td class="LeftClass" style="width:400px">Country:</td>
            <td class="LeftClass">                       
                <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="305px" AutoPostBack="true" AppendDataBoundItems="true"
                    onselectedindexchanged="uoDropDownListCountry_SelectedIndexChanged" >
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="uoDropDownListCountry" 
                        ErrorMessage="Country required." InitialValue="" ValidationGroup="Save">*</asp:RequiredFieldValidator>            
            </td>
            <td class="LeftClass">
                Contact Person:
            </td>
            <td class="LeftClass" colspan="1">
                <asp:TextBox ID="uoTextBoxContactPerson" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr class="VehicleVendor">
            <td class="LeftClass" style="width:400px">
                 City Filter:
            </td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxCity" runat="server" Width="300px"></asp:TextBox>
                &nbsp;
                <asp:Button ID="uoButtonFilterCity" runat="server" Text="Filter" CssClass="SmallButton" Width="50px"
                    onclick="uoButtonFilterCity_Click" />
            </td>
            <td class="LeftClass"> Contact No:</td>
            <td class="LeftClass" colspan="1">
                <asp:TextBox ID="uoTextBoxContactNo" runat="server" Width="300px"></asp:TextBox>
    <%--            <cc1:MaskedEditExtender ID="uoTextBoxContactNo_MaskedEditExtender" 
                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="(999) 999-9999" MaskType="Number" 
                    TargetControlID="uoTextBoxContactNo">
                </cc1:MaskedEditExtender>--%>
            </td>
        </tr>
        <tr class="VehicleVendor">
            <td class="LeftClass" style="width:400px"></td>
            <td class="LeftClass">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <asp:DropDownList ID="uoDropDownListCity" runat="server" Width="305px" AppendDataBoundItems="true">                
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="uoDropDownListCity" 
                        ErrorMessage="City required." InitialValue="" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="LeftClass">Fax No.</td>
            <td class="LeftClass" colspan="1">
                <asp:TextBox ID="uoTextBoxFaxNo" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>  
        <tr class="VehicleVendor">
            <td colspan="4">
                <hr/>
            </td>
        </tr>  
        <tr class="VehicleVendor">
            <td class="PageTitle" colspan="4">
                Vendor Vehicle Type
            </td>
        </tr>
        <tr class="VehicleVendor">
            <td class="contentCaptionOrig" style="width:400px">Vendor Type: </td>
            <td colspan="3" class="LeftClass">
                   <asp:DropDownList ID="uoDropDownListVehicleType" runat="server" Width="305px" AppendDataBoundItems="true"></asp:DropDownList>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="uoDropDownListVehicleType" 
                        ErrorMessage="Vehicle Type required." InitialValue="0" ValidationGroup="Add">*</asp:RequiredFieldValidator>
                   &nbsp;
                   <asp:Button ID="uoButtonVehicleTypeAdd" runat="server" Text="Add" Width="50px" ValidationGroup="Add"
                    CssClass="SmallButton" onclick="uoButtonVehicleTypeAdd_Click" />
            </td>        
        </tr>
        <tr class="VehicleVendor">
            <td></td>
            <td colspan="3" class="LeftClass">
                <table style="width:300px">
                <tr>
                    <td>
                                    
                     <asp:ListView runat="server" ID="uoListViewVehicleType"                  
                         onitemdeleting="uoListViewVehicleType_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable"  >
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Vehicle Type
                                    </th>                                                                                      
                                    <th runat="server" style="width: 5%" id="DeleteTH" />                                     
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned" >                            
                                    <asp:HiddenField ID="uoHiddenFieldVehicleTypeID" runat="server" Value='<%# Eval("VehicleTypeID") %>' />
                                    <asp:Label ID="uoLabelVehicleTypeName" runat="server" Text='<%# Eval("VehicleType")%>'></asp:Label>
                                </td>                        
                                <td style="width: 10%">
                                    <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                                </td>                                         
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Vendor Type
                                    </th>                                                                                                                       
                                </tr>
                                <tr>
                                    <td class="leftAligned">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>                
                  <%--   <asp:ObjectDataSource ID="uoObjectDataSourceVehicle" runat="server" MaximumRowsParameterName="iMaxRow"
                            SelectCountMethod="GetVehicleVendorTypeList" SelectMethod="VehicleVendorsGet"
                            StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.BLL.MaintenanceViewBLL"
                            OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" OnSelecting="uoObjectDataSourceVehicle_Selecting" >
                            <SelectParameters>
                                <asp:Parameter Name="sVehicleVendorName" Type="String"/>
                                <asp:ControlParameter Name="sOrderyBy" Type="String" ControlID="uoHiddenFieldSortBy" />                            
                            </SelectParameters>
                        </asp:ObjectDataSource>
        --%>                    
                        </td>
                    </tr>
                </table>
            </td>       
        </tr>      
        <tr class="VehicleVendor">
            <td colspan="4">
                <hr/>
            </td>
        </tr>  
        <tr>
            <td class="PageTitle" colspan="4" id="headerVehicleDetail">
                Vehicle Details
            </td>
        </tr>
         <tr>
            <td class="LeftClass"style="width:25%"> Vendor Type: </td>        
            <td class="LeftClass"  >
                   <asp:DropDownList ID="uoDropDownListVendorTypePlate" runat="server" Width="305px" 
                        AppendDataBoundItems="true" onchange="GetSelectedMakeType(this)" ></asp:DropDownList>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="uoDropDownListVendorTypePlate" 
                        ErrorMessage="Vehicle Type required." InitialValue="0" 
                        ValidationGroup="AddPlateNo">*</asp:RequiredFieldValidator>               
            </td>  
               
            <td class="LeftClass">
                vehicle Brand
            </td>   
            <td  class="LeftClass" style="white-space:nowrap; width:100%">
                   <asp:DropDownList ID="uoDropDownListVehicleBrand" runat="server" Width="305px" 
                        AppendDataBoundItems="true" onchange="GetSelectedBrand(this)" >
                   </asp:DropDownList>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidatorVehicleBrand" runat="server" 
                        ControlToValidate="uoDropDownListVehicleBrand" 
                        ErrorMessage="Vehicle Brand" InitialValue="0" 
                        ValidationGroup="AddPlateNo">*</asp:RequiredFieldValidator>               
        
            
            
            </td>   
        </tr>
        <tr>
            <td class="LeftClass" style="width:25%">
               Plate No:
            </td>
            <td  class="LeftClass" style="white-space:nowrap">
                 <asp:TextBox ID="uoTextBoxPlateNo" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                     ControlToValidate="uoTextBoxPlateNo" 
                     ErrorMessage="Vehicle Type required." 
                     InitialValue="" ValidationGroup="AddPlateNo">*</asp:RequiredFieldValidator> 
               
            </td>
            <td  class="LeftClass" >Vehicle Make: </td>        
            <td  class="LeftClass" style="white-space:nowrap; width:100%" >
                <asp:DropDownList ID="uoDropDownListVehicleMaker" runat="server" Width="305px" onchange="GetSelectedBrandType(this)"
                     AppendDataBoundItems="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ControlToValidate="uoDropDownListVehicleMaker" 
                        ErrorMessage="Vehicle Maker required." 
                        InitialValue="0" 
                        ValidationGroup="AddPlateNo">*</asp:RequiredFieldValidator>     
            </td>  
        </tr>
        <tr>
            <td class="LeftClass" >  Vehicle Color       </td>
            <td  class="LeftClass" style="white-space:nowrap">
                <table>
                    <tr>
                        <td>
                             <div>
                                   <input type="button" id="btnColor" value="Select Color" class="SmallButton"  name="color"/>
                                   <div id="divColor" style="display: none; position:absolute; z-index:99; background-color:White;
                                             border-style:solid; border-width:thin;overflow: auto; overflow-x: hidden; max-height:400px;" >
                                            <asp:ListView ID="uoListViewColor" runat="server" >
                                                <LayoutTemplate>
                                                    <table border="0" id="uoListviewTableColor" style="border-style:none; width:99%;"  
                                                             cellpadding="0" cellspacing="0" class="listViewTable">
                                                        <tr>
                                                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                                                        </tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr  >
                                                         <%# GetCellVehicleColor() %>
                                                         <td>
                                                         
                                                         </td>
                                                        <td style="min-height:22px;height:22px; text-align:left; 
                                                                    margin:5px; padding:8px; width:150px; 
                                                                     max-width:150px;"
                                                                >
                                                            <label id='<%# string.Concat(Eval("ColorCode"), "_", Eval("ColorName"))%>' title='<%# Eval("ColorName") %>' 
                                                                style="font-style:normal; font-size:small;" onclick ="GetColorCode(this)" ><%# Eval("ColorName") %> </Label>
                                                            <asp:HiddenField runat="server" id="uoHiddenFieldColorCode" Value='<%# Eval("ColorCode") %>' />
                                                            <asp:HiddenField runat="server" id="uoHiddenFieldColorName" Value='<%# Eval("ColorName") %>' />
                                                      
                                                        </td>
                                                      
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <table style="border-style:none;"   cellpadding="0" cellspacing="0" class="listViewTable">
                                                         <tr  style="height:100%; vertical-align:top;"> 
                                                            <td class="leftAligned" colspan="3" style="height:100%; vertical-align:top;">
                                                                No Color 
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                            
                                        </div>
                                        <label id="lblColor" style="font-weight:bold;border-width:thin" />
                                </div>
                        </td>
                        <td>
                        <asp:Button ID="uoButtonVehicleTypePlateAdd" runat="server" 
                                        Text="Add" Width="50px" ValidationGroup="AddPlateNo"
                                        CssClass="SmallButton" onclick="uoButtonVehicleTypePlateAdd_Click"   />
                        
                        </td>
                    </tr>
                
                </table>
           
            
               
            </td>
        
        </tr>
        
         <tr>
            <td></td>
            <td colspan="5" class="LeftClass">
                <table style="width:300px">
                 <tr>
                    <td>
                     
                             <asp:ListView runat="server" ID="uoListViewPlateNo"
                                 onitemdeleting="uoListViewPlateNo_ItemDeleting">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable"  >
                                        <tr>
                                            <th style="text-align: center; white-space: normal;">
                                                Vehicle Type
                                            </th>  
                                             <th style="text-align: center; white-space: normal;">
                                                Vehicle Brand
                                            </th>  
                                            <th style="text-align: center; white-space: normal;">
                                                Vehicle Make
                                            </th>                                                                                     
                                            <th style="text-align: center; white-space: normal;">
                                                Plate No.
                                            </th> 
                                           
                                             <th style="text-align: center; white-space: normal;">
                                                Color
                                            </th> 
                                             <th style="text-align: center; white-space: normal;">
                                                Image
                                            </th> 
                                            <th runat="server" style="width: 5%" id="DeleteTH" />                                     
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="leftAligned"  >                            
                                            <asp:HiddenField ID="uoHiddenFieldVehicleTypeID" runat="server" Value='<%# Eval("VehicleTypeID") %>' />
                                            <asp:HiddenField ID="uoHiddenFieldGridVehicleDetailID" runat="server" Value='<%# Eval("VehicleDetailID")%>'/>
                                            <asp:Label ID="uoLabelVehicleTypeName" runat="server" Text='<%# Eval("VehicleTypeName")%>'></asp:Label>
                                            
                                        </td>   
                                         <td class="leftAligned" >                            
                                           <asp:HiddenField ID="uoHiddenFieldVehicleBrandID" runat="server" Value='<%# Eval("VehicleBrandID") %>' />
                                            <asp:Label ID="lblVehicleBrandName" runat="server" Text='<%# Eval("VehicleBrandName")%>'></asp:Label>
                                        </td>
                                        
                                        <td class="leftAligned" >                            
                                           <asp:HiddenField ID="uoHiddenFileVehicleMakeID" runat="server" Value='<%# Eval("VehicleMakeID") %>' />
                                            <asp:Label ID="lblVehicleMake" runat="server" Text='<%# Eval("VehicleMakeName")%>'></asp:Label>
                                        </td>
                                        <td class="leftAligned" >                            
                                                <asp:Label ID="uoLabelPlateNo" runat="server" Text='<%# Eval("VehiclePlateName")%>' >
                                                </asp:Label>
                                            
                                            
                                        </td>  
                                         <%# GetCellColor()%> 
                                        
                                        <td>
                                            
                                              <div>
                                                 <a class="LinkAddPhoto" href="#" id="<%# Eval("VehicleDetailID")%>" onclick="ShowImage(this)">Add/Edit</a>
                                                 <div class="driverPhoto" id="div<%# Eval("VehicleDetailID")%>" 
                                                    style="display: none; position:absolute; z-index:99; background-color:White;
                                                     border-style:solid; border-width:thin;overflow: auto; overflow-x: hidden;" >
                                                     <table>
                                                        <tr>
                                                          <td style="width:150px; height:150px; padding:2px">
                                                              <img class="imgVehicleImage" src="../Vehicle/DriverPhoto/default.JPG" style="width:304; height:150px;" alt="No Vehicle Photo" id="imgVehicleImage<%# Eval("VehicleDetailID")%>" title="Click to add vehicle photo"  />
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <td class="labelAligned" style="width:100%; text-align:right" id="<%# Eval("VehicleDetailID")%>">
                                                              <input  id="Photo<%# Eval("VehicleDetailID")%>" style="display:none" class="SmallButton" type="file" onchange="readURLPhoto(this);">
                                                              <input type="button" class="SmallButton" id="btnSavePhoto<%# Eval("VehicleDetailID")%>" value="Choose File" onclick="LoadVehiclePhoto(this)" />
                                                              <%--<asp:Button runat="server" CssClass="SmallButton" ID="btnSavePhoto" Text="Save Photo"  OnClick="btnSavePhoto_LoadImage"></asp:Button>--%>
                                                              
                                                              <input type="button" class="SmallButton" id="Button1" value="Save Photo"  onclick="Saveimage()"/>
                                                              <input type="button" class="SmallButton" id="btnClosePhoto" value="Close"  onclick="ClosePhoto()"/>
                                                          </td>
                                                        </tr>
                                                        <tr style="display:none">
                                                            <td>
                                                                <asp:FileUpload ID="uoFileUploadFile" runat="server" onchange="javascript:PreviewImage(this);" Visible="true" />
                                                            </td>
                                                        </tr>
                                                     </table>   
                                                
                                                </div>
                                            </div>
                                            
                                            <asp:HiddenField ID="uoHiddenFieldColor" runat="server" Value='<%# Eval("VehicleColor") %>' />
                                            <asp:HiddenField ID="uoHiddenFieldColorName" runat="server" Value='<%# Eval("VehicleColorName") %>' />
                                        
                                        </td>
                                                          
                                        <td style="width: 10%">
                                            <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                                        </td>                                         
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                            <th style="text-align: center; white-space: normal;">
                                                Vehicle Type
                                            </th>  
                                             <th style="text-align: center; white-space: normal;">
                                                Vehicle Brand
                                            </th>  
                                            <th style="text-align: center; white-space: normal;">
                                                Vehicle Make
                                            </th>                                                                                     
                                            <th style="text-align: center; white-space: normal;">
                                                Plate No.
                                            </th> 
                                             <th style="text-align: center; white-space: normal;">
                                                Color
                                            </th> 
                                        </tr>
                                        <tr>
                                            <td class="leftAligned" colspan="5">
                                                No Record
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>   
                      
                    </td>
                 </tr>
                </table>
            </td>       
        </tr> 
        <tr>
            <td colspan="4">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="LeftClass"></td>
            <td class="LeftClass" colspan="3" style="vertical-align:top">     
               <div id="divGridPlateNo" style="vertical-align:top">       
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="100px"
                    onclick="uoButtonSave_Click" ValidationGroup="Save"/>  &nbsp&nbsp
                <asp:Button ID="uoButtonBack" runat="server" Text="Back" Width="100px"
                    onclick="uoButtonBack_Click" />
                 </div>                         
            </td>
        </tr>        
    </table>

        <asp:HiddenField ID="uoHiddenFieldVehicleVendorIdInt" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldURLFrom" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldColorCode" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldColorName" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldimageID" runat="server" />
        <asp:HiddenField ID="uoHiddenFieldVehicleDetailID" runat="server" />
        
        <asp:FileUpload ID="uoFileUploadFile" runat="server" style="display:none;" onchange="readURLVehiclePhoto(this)"/>  
        <asp:Button runat="server" CssClass="SmallButton"  style="display:none;"  ID="btnSavePhoto" Text="Save Photo"  OnClick="btnSavePhoto_LoadImage"></asp:Button>

        <script src="../Script/mgwtimezone.js" type="text/javascript"></script>
        
        
        
    </ContentTemplate>
    <Triggers >
        <asp:PostBackTrigger ControlID="btnSavePhoto"/>     
        <asp:PostBackTrigger ControlID="uoButtonSave"/>          
        <asp:PostBackTrigger ControlID="uoButtonBack"/>          
    </Triggers>
</asp:UpdatePanel>
        
<script type="text/javascript">

    $(document).ready(function() {
        ShowColorList();
        HideMakerList();
        CheckImage();
        HideVehicleVendor();
    });

      function pageLoad(sender, args) {
          var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
          if (isAsyncPostback) {
              ShowColorList();
              HideMakerList();
              CheckImage();
              HideVehicleVendor();
          }
      }

      function ShowColorList()
      {
         $("#btnColor").click(function() {
             document.getElementById("divColor").style.display = "block";
         });
     }


     function CheckImage() {
         var url = "", n;
         $('.LinkAddPhoto').each(function() {
             
             if (this.id == 0) {
                 this.style.display = "none";
             }
         });
     }
     
     function HideVehicleVendor() {
         if (document.getElementById("ctl00_uoHiddenFieldRole").value == "Vehicle Vendor") { 
             $('.VehicleVendor').each(function() {
                this.style.display = "none";
            });
            var screenHeight = screen.availHeight;
            console.log(screen);
            document.getElementById("divGridPlateNo").style.height = screenHeight - 500 + "px";
            var vHeader = $('#headerVehicleDetail');
            $('#headerVehicleDetail')[0].innerText = vHeader[0].innerText + ' - ' + $("#<%=uoTextBoxVendorName.ClientID %>").val();
         }
     }      

     function HideMakerList() {

         var cbo = document.getElementById("ctl00_HeaderContent_uoDropDownListVehicleBrand");


         var VPlate = document.getElementById("ctl00_HeaderContent_uoDropDownListVendorTypePlate");
         var VBrand = document.getElementById("ctl00_HeaderContent_uoDropDownListVehicleBrand");

         var vpValue = VPlate.options[VPlate.selectedIndex].value
         var vbValue = VBrand.options[VBrand.selectedIndex].value
         var res;
         
         if (VBrand.selectedIndex == 0 && VPlate.selectedIndex == 0) {
             $('[id*=uoDropDownListVehicleMaker] option').each(function() {
                 if (this.value != 0) {
                    this.style.display = "none";
                 }
                 else { 
                    this.style.display = "block";
                 }
             });

         }
         else if (VBrand.selectedIndex != 0 && VPlate.selectedIndex == 0) {

              $('[id*=uoDropDownListVehicleMaker] option').each(function() {
              
                 res = this.value.split('-');
                 if (this.value != 0) {
                     this.style.display = "none";
                     if (res[0] == VBrand.options[VBrand.selectedIndex].value) {
                         this.style.display = "block";
                     }
                     else {
                         if (this.value != 0) this.style.display = "none";
                     }
                     
                 }
                 else {
                     this.style.display = "block";
                 }
             });
         
         }
         else if (VBrand.selectedIndex != 0 && VPlate.selectedIndex != 0) {

             $('[id*=uoDropDownListVehicleMaker] option').each(function() {
                 this.style.display = "none";
                 res = this.value.split('-');
                 if (this.value != 0) {
                     if (res[0] == VBrand.options[VBrand.selectedIndex].value && res[2] == VPlate.options[VPlate.selectedIndex].value) {
                         this.style.display = "block";
                     }
                     else {
                         if (this.value != 0) this.style.display = "none";
                     }
                 }
                 else {
                     this.style.display = "block";
                 }
             });

         }


         else if (VBrand.selectedIndex == 0 && VPlate.selectedIndex != 0) {

             $('[id*=uoDropDownListVehicleMaker] option').each(function() {
                 this.style.display = "none";
                 res = this.value.split('-');
                 if (this.value != 0) {

                     if (res[0] == VBrand.options[VBrand.selectedIndex].value && res[2] == VPlate.options[VPlate.selectedIndex].value) {
                         this.style.display = "block";
                     }
                     else {
                         if (this.value != 0) this.style.display = "none";
                     }

                 }
                 else {
                     this.style.display = "block";
                 }
             });

         }
         
     }

     function GetSelectedBrand(val) {
         console.log(val.options[val.selectedIndex].value);
         var selecetedType = $('[id*=uoDropDownListVendorTypePlate]')[0].value;
         document.getElementById("ctl00_HeaderContent_uoDropDownListVehicleMaker").selectedIndex = 0;
         var res;
         if (val.selectedIndex == 0) {

             $('[id*=uoDropDownListVehicleMaker] option').each(function() {
                 this.style.display = "block";
                 if (selecetedType > 0) {
                     res = this.value.split('-');
                     if (res[2] != selecetedType) this.style.display = "none";
                 }
                 else { 
                    if (this.value != 0) this.style.display = "none";
                 }
             });
         }
         else {
           
             $('[id*=uoDropDownListVehicleMaker] option').each(function() {
                 console.log(this.value);
                 this.style.display = "block";
                 res = this.value.split('-');
                 
                 if (res[0] == val.options[val.selectedIndex].value) {
                     this.style.display = "block";
                     if (selecetedType > 0 && res[2] != selecetedType) {
                         this.style.display = "none";
                     }
                 }
                 else {
                     if (this.value != 0) this.style.display = "none";
                 }
             });
             console.log(val.selectedIndex);
         }
     }

     function GetSelectedMakeType(val) {

         console.log(val.options[val.selectedIndex].value);
         document.getElementById("ctl00_HeaderContent_uoDropDownListVehicleMaker").selectedIndex = 0;
         document.getElementById("ctl00_HeaderContent_uoDropDownListVehicleBrand").selectedIndex = 0;
         if (val.selectedIndex == 0) {
          
             $('[id*=uoDropDownListVehicleMaker] option').each(function() {
                this.style.display = "block";
                if (this.value != 0) this.style.display = "none";
            });
          
         }
         else {
             var res;
             $('[id*=uoDropDownListVehicleMaker] option').each(function() {
                 console.log(this.value);
                 this.style.display = "block";
                 res = this.value.split('-');
                 if (res[2] == val.options[val.selectedIndex].value) {
                     this.style.display = "block";
                 }
                 else {
                     if (this.value != 0) this.style.display = "none";
                 }
             });
             console.log(val.selectedIndex);
         }
     }

     function GetSelectedBrandType(val) {
         var res = val.value.split('-');

         document.getElementById("ctl00_HeaderContent_uoDropDownListVehicleBrand").selectedIndex = res[0];
         console.log(res);
        
     }
      
     function GetColorCode(val)
     {
         console.log(val.id);

         var res = val.id.split('_');

         console.log(res);
         $("#<%=uoHiddenFieldColorCode.ClientID %>").val(res[0]);
         $("#<%=uoHiddenFieldColorName.ClientID %>").val(res[1]);

        document.getElementById("divColor").style.display = "none";
        document.getElementById("lblColor").innerHTML = res[1];

    }



    function checkImageExists(imageUrl, callBack) {
        var imageData = new Image();
        imageData.onload = function() {
            callBack(true);
        };
        imageData.onerror = function() {
            callBack(false);
        };
        imageData.src = imageUrl;
    }


    function ShowImage(val) {

        $("#<%=uoHiddenFieldVehicleDetailID.ClientID %>").val(0);
        $("#<%=uoHiddenFieldVehicleDetailID.ClientID %>").val(val.id);
        $('.driverPhoto').each(function() {
            if (this.id != "div" + val.id) {
                this.style.display = "none";
            }
        });

        var PhotoURL = '<%= ConfigurationManager.AppSettings["MediaURL"] %>';
        var Token = '<%= ConfigurationManager.AppSettings["MediaToken"] %>';

        var url = PhotoURL + '/vendors/transportation/vehicle/' + val.id + '?at=' + Token;

        console.log(url);

        //var image = $('img#imgVehicleImage' + val.id);
        //checkImageExists(url, function(existsImage) {
        //    if (existsImage == true) {
        //        $('img#imgVehicleImage' + val.id).attr('src', url).width(304).height(150); 
        //    }
        //    else {
        //        $('img#imgVehicleImage' + val.id).attr('src', "").width(304).height(150);
        //    }
        //});

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../PageMethods.aspx/GetPhoto",
            data: "{'URL': '" + url + "'}",
            dataType: "json",
            success: function(data) {
                if (data.d.Image != null) {
                    var res = "data:image/*;base64," + data.d.Image;
                    $('img#imgVehicleImage' + val.id).attr('src', res).width(304).height(150);
                }
                else {
                    alert("Ooops! no image found!");

                }
            }
        }); 

        if (document.getElementById("div" + val.id).style.display == "none") {
            document.getElementById("div" + val.id).style.display = "block";
        }
        else {
            document.getElementById("div" + val.id).style.display = "none";
        }

    }

    function ClosePhoto() {
        $('.driverPhoto').each(function() {
            this.style.display = "none";
        });
    }




    function readURLVehiclePhoto(input) {
       
        var id = input.id;
        console.log(input.files[0]);

        var imgID = document.getElementById("ctl00_HeaderContent_uoHiddenFieldimageID").value;
        console.log(imgID);
        
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function(e) {
                $("img#imgVehicleImage" + imgID).attr('src', e.target.result).width(304).height(150);
            };
            reader.readAsDataURL(input.files[0]);
        }
   
    }






    function readURLPhoto(input) {
        var id = input.id;
        console.log(input.files[0]);

 
        
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function(e) {
            $("img#imgVehicleImage" + id.replace("Photo", "")).attr('src', e.target.result).width(304).height(150);
            };
           reader.readAsDataURL(input.files[0]);
       }

        //var settings = {
        //    "async": true,
        //    "crossDomain": true,
        //    "url": "http://10.80.0.49:7001/vendors/transportation/vehicle/2?at=8c193d19e210e3f5705f95c21cd60f614db1bc26",
        //    "method": "GET",
        //    "headers": {
        //        "cache-control": "no-cache",
        //        "postman-token": "9f730703-5f23-17e1-1240-c9f51a6a244e"
        //    }
        //}
        //$.ajax(settings).done(function(response) {
        //    console.log(response);
        //});

   }


   function Saveimage() {

       $("#<%=btnSavePhoto.ClientID %>").click();
   
   } 

   function LoadVehiclePhoto(input) {
       var id = input.id;
       document.getElementById("ctl00_HeaderContent_uoHiddenFieldimageID").value = "";
       document.getElementById("ctl00_HeaderContent_uoHiddenFieldimageID").value = id.replace("btnSavePhoto", "");
       console.log(document.getElementById("ctl00_HeaderContent_uoHiddenFieldimageID").value);
       
        
       $("#<%=uoFileUploadFile.ClientID %>").click();





      









       //var settings = {
       //    "async": true,
       //    "crossDomain": true,
       //    "url": "http://10.80.0.49:7001/vendors/transportation/vehicle/2?at=8c193d19e210e3f5705f95c21cd60f614db1bc26",
       //    "method": "GET",
       //    "headers": {
       //        "cache-control": "no-cache",
       //        "postman-token": "9f730703-5f23-17e1-1240-c9f51a6a244e"
       //    }
       //}
       //$.ajax(settings).done(function(response) {
       //    console.log(response);
       //});

   }

    function PreviewImage(ImagePath) {

        if (ImagePath) {
            if (ImagePath.files && ImagePath.files[0]) {
                var Filerdr = new FileReader();
                Filerdr.onload = function(e) {
                    $('#imgVehicleImage').attr('src', e.target.result);

                    $("img#imgVehicleImage" + id.replace("Photo", "")).attr('src', e.target.result).width(304).height(150);
                }
                Filerdr.readAsDataURL(ImagePath.files[0]);
            }
        }
    }


    function SaveImageTest() {





        var vehicleDetailID = $("#<%=uoHiddenFieldVehicleDetailID.ClientID %>").val();


        //stop submit the form, we will post it manually.
        event.preventDefault();

        // Get form
        var form = $('#Photo' + vehicleDetailID)[0];

        // Create an FormData object
        var data = new FormData(form);

        // If you want to add an extra field for the FormData
        data.append("CustomField", "This is some extra data, testing");

        form.append("VehicleID", vehicleDetailID);
        form.append("Asset", form);

        var settings = {
            "async": true,
            "crossDomain": true,
            "url": "testURL",
            "method": "POST",
            "type": "POST",
            "headers": {
                "X-RCCL-AccessToken": "13"
            },
            "processData": false,
            "contentType": false,
            "mimeType": "multipart/form-data",
            "data": form
        }

        $.ajax(settings).done(function(response) {
            console.log(response);
        });




        //stop submit the form, we will post it manually.
        event.preventDefault();

        // Get form
        var form = $('#fileUploadForm')[0];

        // Create an FormData object
        var data = new FormData(form);

        // If you want to add an extra field for the FormData
        data.append("CustomField", "This is some extra data, testing");

        // disabled the submit button
        $("#btnSubmit").prop("disabled", true);

             $.ajax({
                 type: "POST",
                 enctype: 'multipart/form-data',
                 url: "/api/upload/multi",
                 data: data,
                 processData: false,
                 contentType: false,
                 cache: false,
                 timeout: 600000,
                 success: function(data) {

                     $("#result").text(data);
                     console.log("SUCCESS : ", data);
                     $("#btnSubmit").prop("disabled", false);

                 },
                 error: function(e) {

                     $("#result").text(e.responseText);
                     console.log("ERROR : ", e);
                     $("#btnSubmit").prop("disabled", false);

                 }
             });

    
    }
    
    
    
     function SaveImage() {
          
        var vehicleDetailID = $("#<%=uoHiddenFieldVehicleDetailID.ClientID %>").val();

        var file = $('#Photo' + vehicleDetailID)[0];
        
        var savePhotoURL = '<%= ConfigurationManager.AppSettings["TM-API"] %>';
        savePhotoURL = savePhotoURL + "/AddEditPhoto";
        var form = new FormData();
        
        form.append("Photo", file.files[0]);
        form.append("IDName", "VehicleID");
        form.append("IDValue", vehicleDetailID);
        form.append("EntityType", "vehicle");


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
            } 
        }

        $.ajax(settings).done(function(response) {

            if (response.Success == false) {
                alert("Error: " + response.Message);

            }
            else {

                var res = JSON.parse(response);
                alert("Result: " + res.Message);

                self['location']['replace'](self.location['href']);
                location.reload();
                location['reload']();
                window.location.reload();
                window['location'].reload();
                window.location['reload']();
                window['location']['reload']();
                self.location.reload();
                self['location'].reload();
                self.location['reload']();
                self['location']['reload']();

            }

        });
        
        $('.driverPhoto').each(function() {
            this.style.display = "none";
        });

    }
    
</script>
        
    </asp:Content>

