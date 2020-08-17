<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImmigrationCompany.aspx.cs" Inherits="TRAVELMART.Maintenance.ImmigrationCompany" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  <script src="http://code.jquery.com/jquery-1.9.1.js"></script> 
   <%--<script type="text/javascript" src="~/jquery-1.9.1.js"></script>--%>
  
   
    <link rel="stylesheet" type="text/css" href="../../App_Themes/Default/Stylesheet.css" media="screen" />
    <link href="../../App_Themes/Default/LinkMenu.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/Default/LinkMenuServices.css" rel="stylesheet" type="text/css" />
    

      
    <title>Add Immigration Company</title>
    
     

</head>
<body>
    <form id="form1" runat="server">
   <%--     
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
      <div style="padding:10px;" > 
        <table style=" width:100%;  ">
            <tr>
                <td> Company</td>
                <td colspan="3" >
                    <asp:TextBox ID="txtCompany"  Width="99.2%" runat="server"></asp:TextBox>
                </td>
                 
            </tr>
            <tr>
                 <td>Email Add</td>
                <td>
                    <asp:TextBox ID="txtEmailAdd" Width="98%" runat="server"></asp:TextBox>
                </td>
                <td style="padding-left:10px" >Phone</td>
                <td>
                    <asp:TextBox ID="txtPhoneNo"  Width="98%" runat="server"></asp:TextBox>
                </td>
                
            </tr>
               
            
           
             <tr>
                
                <td>Address</td>
                <td colspan="3" >
                    <asp:TextBox ID="txtAddress"  Width="99.2%" runat="server"></asp:TextBox>
                </td>
            
            
            </tr>
             <tr>
                <td> Country</td>
                <td>
                    <asp:DropDownList ID="cboCountry" Width="100%" runat="server" AppendDataBoundItems="true"
                    OnSelectedIndexChanged="cboCountry_SelectedIndexChanged" AutoPostBack="true" />
                </td>
                <td style="padding-left:10px" >City </td>
                <td>
                    <asp:DropDownList ID="cboCity"  Width="100%" runat="server"  AppendDataBoundItems="true"/>
                     
                </td>
            
            
            </tr>
            <tr>
                <td colspan="4" style="text-align:right; vertical-align:middle;   " >
                    <asp:Button ID="btnSave" CssClass="SmallButton"  runat="server" Text="Save" OnClick="btnSave_click" />
                  <%--  <asp:Button ID="btnClear" CssClass="SmallButton"  runat="server" Text="Clear" />
                    <asp:Button ID="btnClose" CssClass="SmallButton"  runat="server" Text="Close" />--%>
                </td>
            </tr>
            
            <tr>
                <td colspan="5" >
                      <asp:Panel runat="server" ScrollBars="Vertical" Width="700px"  Height="300px">
                           
                            <asp:ListView runat="server" ID="uoListviewImmigrationCompany"  
                              OnItemCommand="uoListviewImmigrationCompany_ItemCommand"
                              OnItemDeleting="uoListviewImmigrationCompany_ItemDeleting"
                              OnItemEditing="uoListviewImmigrationCompany_ItemEditing">
                              
                                <LayoutTemplate>
                                 <table border="0" id="uoListviewTravelTable" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                                        <tr>
                                             
                                            <th style="text-align: center; white-space: normal; width:100px;">
                                                Company Name
                                            </th >
                                            <th style="text-align: center; white-space: normal;">
                                                Email
                                            </th >
                                            
                                            <th style="text-align: center; white-space: normal; width:100px; ">
                                                Contact
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                Address
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                Country
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                City
                                            </th> 
                                             <th style="text-align: center; white-space: normal;">
                                            </th> 
                                        </tr>        
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>              
                                </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                       <td class="leftAligned" style="white-space: normal;  width:100px;" >
                                            <asp:Label runat="server" ID="lblCompanyName"  Text='<%# Eval("Company") %>'/>
                                        </td>
                                       
                                       <td class="leftAligned" style="white-space: normal;" >
                                            <asp:Label runat="server" ID="lblEmailAdd" 
                                                Text='<%# Eval("EmailAdd") %>'/>
                                        </td>
                                       <td class="leftAligned">
                                            <asp:Label runat="server" ID="lblContact" 
                                                Text='<%# Eval("Contact") %>'/></td>
                                       <td class="leftAligned">
                                            <asp:Label runat="server" ID="lblAddress"  
                                                Text='<%#  Eval("Address")%>'/>
                                        </td>
                                       <td class="leftAligned">
                                            <asp:Label runat="server" ID="lblCountry"  
                                                Text='<%# Eval("Country")  %>'/>
                                        </td>
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ID="lblCity"  
                                                Text='<%# Eval("City")  %>'/>
                                        </td> 
                                       
                                        
                                        
                                        <td style="width:50px; white-space:nowrap;">
                                           <%-- <a runat="server" href="#"   id="uoEditUser" onclick="EditCompany(this)"> Edit</a>
                                              &nbsp;  
                                            <a runat="server"  href="#" commandname="delete" id="A2"> delete</a>
                                       --%>
                                            <asp:LinkButton  runat="server" id="A1" CommandName="edit" Text="Edit"/>
                                              &nbsp;  
                                            <asp:LinkButton  runat="server" id="A3" CommandName="delete" Text="delete" OnClientClick="DeleteCompany(this)"  /> 
                                       
                                       
                                       
                                        </td>
                                        
                                        
                                         <td style="white-space: normal; visibility:hidden; display:none;" >
                                         
                                            <asp:HiddenField runat="server"  ID="HFImmigrationCompanyID"  Value='<%# Eval("ImmigrationCompanyID") %>' />
                                            <asp:HiddenField runat="server" ID="HFCountryID" Value='<%# Eval("CountryID") %>' />
                                            <asp:HiddenField runat="server" ID="HFCityID" Value='<%# Eval("CityID") %>' />

                                        </td>
                                      </tr>
                                </ItemTemplate>
                                 <EmptyDataTemplate>
                                    <table border="0" id="uoListviewTravelTable" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                                        <tr>
                                             
                                            <th style="text-align: center; white-space: normal;" >
                                                Company Name
                                            </th >
                                            <th style="text-align: center; white-space: normal;">
                                                Email
                                            </th >
                                            
                                            <th style="text-align: center; white-space: normal;" >
                                                Contact
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                Address
                                            </th>
                                             <th style="text-align: center; white-space: normal;">
                                                Country
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                City
                                            </th> 
                                        </tr>        
                                        <tr > 
                                            <td class="leftAligned" colspan="6" >No Record</td>
                                        </tr>
                                </table>
                                </EmptyDataTemplate>
                        </asp:ListView>
                      
                      
                      </asp:Panel>  
                    
                        
                         
                </td>
            </tr>
        </table>
         <asp:HiddenField runat="server"  ID="uoHiddenFieldImmigrationCompanyID"  Value='<%# Eval("ImmigrationCompanyID") %>' />
    </div>
    
<%--    </asp:UpdatePanel>
       --%>
    
       
    </form>
    
    
      <script type="text/javascript">

          function EditCompany(val) {

              var name = val.id.replace("uoListviewImmigrationCompany_ctrl", "");
              var counter = name.replace('_uoEditUser', '');

              var Company = document.getElementById('uoListviewImmigrationCompany_ctrl' + counter + '_lblCompanyName').innerHTML;
              var CompanyID = document.getElementById('uoListviewImmigrationCompany_ctrl' + counter + '_HFImmigrationCompanyID').value;
              var EmailAdd = document.getElementById('uoListviewImmigrationCompany_ctrl' + counter + '_lblEmailAdd').innerHTML;
              var Contact = document.getElementById('uoListviewImmigrationCompany_ctrl' + counter + '_lblContact').innerHTML;
              var Address = document.getElementById('uoListviewImmigrationCompany_ctrl' + counter + '_lblAddress').innerHTML;
              var countryID = document.getElementById('uoListviewImmigrationCompany_ctrl' + counter + '_HFCountryID').value;
              var CityID = document.getElementById('uoListviewImmigrationCompany_ctrl' + counter + '_HFCityID').value;

              document.getElementById('txtCompany').value = Company;
              document.getElementById('txtAddress').value = Address;
              document.getElementById('txtEmailAdd').value = EmailAdd;
              document.getElementById('txtPhoneNo').value = Contact;
              
              document.getElementById('uoHiddenFieldImmigrationCompanyID').value = CompanyID;
              
              GetSelectedCountry(countryID);

              GetSelectedCity(CityID, countryID);


          }

          function DeleteCompany(val) {

              val.split("uoListviewImmigrationCompany$ctrl3$A3")


              var name = val.id.replace("uoListviewImmigrationCompany_ctrl", "");
              var counter = name.replace('_uoEditUser', '');

              var CompanyID = document.getElementById('uoListviewImmigrationCompany_ctrl' + counter + '_HFImmigrationCompanyID').value;
          

              document.getElementById('uoHiddenFieldImmigrationCompanyID').value = CompanyID;

           


          }

          function GetSelectedCountry(val) {

              var dropdown = document.getElementById('cboCountry');
              for (var i = 0; i < dropdown.length; i++) {
                  dropdown.selectedIndex = i;
                  if (dropdown.options[dropdown.selectedIndex].value == val) {
                      AirportName = dropdown.options[dropdown.selectedIndex].innerText;
                      return;
                  }
              }
          }


          function GetSelectedCity(val, CountryID) {
              var h;
              h = document.getElementById('cboCity');
              h.options.length = 0;
              var listItem;
              listItem = new Option("--Select City--", 0, false, false);
              
              h.options[i] = listItem;


              $.ajax({
                  type: "POST",
                  contentType: "application/json; charset=utf-8",
                  url: "ImmigrationCompany.aspx/GetCityList",
                  data: "{'CountryID': '" + CountryID + "'}",
                  dataType: "json",
                  success: function(data) {
                  alert(data.d.length + ' ' + val);
                      if (data.d.length > 0) {
                         

                          for (var i = 0; i <= data.d.length; i++) {


                              if (data.d[i].CityId == val) {
                                  listItem = new Option(data.d[i].CityName, data.d[i].CityId, true, true);
                              }
                              else {
                                  listItem = new Option(data.d[i].CityName, data.d[i].CityId, false, false);
                              }
                              h.options[i] = listItem;

                          }
                      }

                  }
              });

            
              var dropdown = document.getElementById('cboCity');

              for (var i = 0; i < dropdown.length; i++) {
                  dropdown.selectedIndex = i;
                  if (dropdown.options[dropdown.selectedIndex].value == val) {
                      return;
                  }
              }
          }
            
             
            
            
        </script>
</body>



</html>
