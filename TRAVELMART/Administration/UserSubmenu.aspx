<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="UserSubmenu.aspx.cs" Inherits="TRAVELMART.UserSubmenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
    //************************** Treeview Parent-Child check behaviour ****************************//  

   function OnTreeClick(evt)
   {
        var src = window.event != window.undefined ? window.event.srcElement : evt.target;
        var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
        if(isChkBoxClick)
        {
            var parentTable = GetParentByTagName("table", src);
            var nxtSibling = parentTable.nextSibling;
            if(nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
            {
                if(nxtSibling.tagName.toLowerCase() == "div") //if node has children
                {
                    //check or uncheck children at all levels
                    CheckUncheckChildren(parentTable.nextSibling, src.checked);
                }
            }
            //check or uncheck parents at all levels
            CheckUncheckParents(src, src.checked);
        }
   } 

   function CheckUncheckChildren(childContainer, check)
   {
      var childChkBoxes = childContainer.getElementsByTagName("input");
      var childChkBoxCount = childChkBoxes.length;
      for(var i = 0; i<childChkBoxCount; i++)
      {
        childChkBoxes[i].checked = check;
      }
   }

   function CheckUncheckParents(srcChild, check)
   {
       var parentDiv = GetParentByTagName("div", srcChild);
       var parentNodeTable = parentDiv.previousSibling;
       
       if(parentNodeTable)
        {
            var checkUncheckSwitch;
            
            if(check) //checkbox checked
            {
                var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                if(isAllSiblingsChecked)
                    checkUncheckSwitch = true;
                else    
                    return; //do not need to check parent if any child is not checked
            }
            else //checkbox unchecked
            {
                checkUncheckSwitch = false;
            }
            
            var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
            if(inpElemsInParentTable.length > 0)
            {
                var parentNodeChkBox = inpElemsInParentTable[0]; 
                parentNodeChkBox.checked = checkUncheckSwitch; 
                //do the same recursively
                CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
            }
        }
   }

   function AreAllSiblingsChecked(chkBox)
   {
     var parentDiv = GetParentByTagName("div", chkBox);
     var childCount = parentDiv.childNodes.length;
     for(var i=0; i<childCount; i++)
     {
        if(parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
        {
            if(parentDiv.childNodes[i].tagName.toLowerCase() =="table")
            {
               var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
              //if any of sibling nodes are not checked, return false
              if(!prevChkBox.checked) 
              {
                return false;
              } 
            }
        }
     }
     return true;
   }

   //utility function to get the container of an element by tagname
   function GetParentByTagName(parentTagName, childElementObj)
   {
      var parent = childElementObj.parentNode;
      while(parent.tagName.toLowerCase() != parentTagName.toLowerCase())
      {
         parent = parent.parentNode;
      }
    return parent;    
   }
   </script>

    <div class="PageTitle">
        <asp:Label ID="ucLabelTitle" runat="server" Font-Bold="True"></asp:Label>
    </div>
    <hr/>
    <table width="100%">
        <tr>
            <td class="LeftClass">
                
            </td>
            <td class="RightClass">
                
            </td>
        </tr>
        <tr>
            <td >
                <asp:TreeView ID="uoTreeViewSubmenu" runat="server" Onclick=OnTreeClick(event)>
                </asp:TreeView>
                <div runat="server" id = "uoDivMsg" visible ="false" style="color:Red">No Submenu</div>
                <%--<asp:GridView ID="uoGridViewSubMenu" runat="server" CssClass="listViewTable" 
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="colMenuIDInt"/>
                        <asp:TemplateField HeaderText = "Select">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="uoCheckBoxSelect"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="colDisplayNameVarchar" HeaderText="Submenu"/>  
                    </Columns>
                </asp:GridView>--%>
            </td>
            <td style="vertical-align:top" class="RightClass"> 
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" 
                    onclick="uoButtonSave_Click" CssClass="SmallButton"/>
            </td>
        </tr>      
    </table>
</asp:Content>
