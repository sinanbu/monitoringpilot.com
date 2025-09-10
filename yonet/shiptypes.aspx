<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shiptypes.aspx.cs" Inherits="yonet_shiptypes"  MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>PMTR</title>

    <link href="css/all.css" rel="stylesheet" />

        <style type="text/css">

    

</style>

</head>

<body>
    <form id="form1" runat="server">
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

   <div id="main">
  <div id="header"> <a href="#" class="logo"><img src="img/logo.png" width="46" height="48" alt="" /></a>
<div id="header-right"><asp:Literal ID="Litpagebaslik" runat="server" ></asp:Literal></div>
    <ul id="top-navigation">
      <li class="active" ><span><span>Settings</span></span></li>
      <li><span><span><a href="Statistics.aspx">Statistics</a></span></span></li>
      <li><span><span><a href="vacations.aspx">Vacations</a></span></span></li>
      <li><span><span><a href="onlineusers.aspx">Online Users</a></span></span></li>
      <li><span><span><a href="../main.aspx">Pilot Monitor</a></span></span></li>
      <li><span><span> <asp:LinkButton ID="LBguvcik" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton></span></span></li>
    </ul>
  </div>
  <div id="middle">

    <div id="left-column"> 
      <h3>Users</h3>
      <ul class="nav">                <asp:Literal ID="Litmenu1" Text="" runat="server"> 
                <li><a href="pilots.aspx">User List</a></li></asp:Literal>
      </ul>

      <h3>Places</h3>
      <ul class="nav">
        <li><a href="portsandberths.aspx">Ports and Berths</a></li>
        <li><a href="anchorageplaces.aspx">Anchorage Places</a></li>

      </ul>

      <h3>Distances</h3><ul class="nav">
        <li><a href="distances.aspx">Distances</a></li></ul>

      <h3>Flags</h3> <ul class="nav">        
        <li><a href="flags.aspx">Flags</a></li></ul>

      <h3>Ship Types</h3><ul class="nav">
        <li id="ulnavlihover"><a>Ship Types</a></li></ul>

      <h3>Pilot Stations</h3><ul class="nav">
        <li><a href="pilotstations.aspx">Pilot Stations</a></li></ul>

</div>


       <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
    <div id="center-column">
      <div class="top-bar"> 
         
          <ul>
  <li class="sagusttuslist"><asp:LinkButton ID="LBaddnewportberth" class="sagusttuslista" runat="server" OnClick="LBaddnewportberth_Click">Add New Ship Type</asp:LinkButton></li>
</ul>

        <h1><a href="yonetim.aspx">Settings</a> > Ship Types </h1>
          
                  <div class="breadcrumbs"></div>
      </div>
      <br />
      <div class="select-bar">
        <label>        </label>
        <label>        </label>
      </div>


      <div class="table">
        

       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="747px"  OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"  OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="2" ForeColor="#333333"  GridLines="Vertical"  >
            <AlternatingRowStyle BackColor="White" />
            <Columns>

<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/arrowr.png"   ItemStyle-Width="47px"  SelectText="" ShowSelectButton="True" />

<asp:TemplateField HeaderText="id" ControlStyle-Width="60px" >
                    <ItemTemplate><asp:Label ID="Litemportlimanno" runat="server" Text='<%# Bind("id") %>'></asp:Label></ItemTemplate>

                    <ControlStyle  ></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Trebuchet MS" Font-Size="Small" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
            </asp:TemplateField>

<asp:TemplateField HeaderText="Shiptype"  ControlStyle-Width="200px" >
                    <EditItemTemplate> <asp:TextBox ID="TBeditportlimanadi" runat="server" CssClass="kucukharf"  MaxLength="20" Text='<%# Bind("tip") %>'></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportlimanadi" runat="server" Text='<%# Bind("tip") %>'></asp:Label></ItemTemplate>

                     <ControlStyle></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
            </asp:TemplateField>

<asp:TemplateField HeaderText="Speed"  ControlStyle-Width="60px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditporthiz" runat="server" Text='<%# Bind("hiz") %>' MaxLength="2" ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" TargetControlID="TBeditporthiz" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemporthiz" runat="server" Text='<%# Bind("hiz") %>'></asp:Label></ItemTemplate>

                    <ControlStyle  ></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
          </asp:TemplateField>

<asp:CommandField  ItemStyle-Width="60px" ShowEditButton="True" HeaderText=""  EditImageUrl="../images/edit.png"  EditText=""   UpdateText="" UpdateImageUrl="../images/save.png"  CancelImageUrl="../images/cancel.png"   CancelText=""  ButtonType="Image" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:CommandField>

<asp:CommandField ItemStyle-Width="60px" ShowDeleteButton="True"  HeaderText=""   DeleteImageUrl="../images/cancelsil.png" DeleteText=""    ButtonType="Image">
            <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:CommandField>

            </Columns>

            <EditRowStyle BorderStyle="None" BackColor="#77bbdF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    

        <asp:Button id="buttonshowpopuppadd" runat="server" style="display:none;" />
        <asp:Panel ID="panellbadd"   CssClass="panellbadd" runat="server"> 

<table  class="panellbadd">                        
                     <tr>
                <td colspan="2"  class="tdorta"  >
                 <p >Add New Ship Type </p></td>
            </tr>          
            <tr>
                <td >ID : </td>
                <td  class="tdsag" ><div>
                    <asp:TextBox ID="TBpaddportno" Width="30px" runat="server" MaxLength="2" Enabled="false"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="TBpaddportno" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender></div>
                </td>
            </tr>
            <tr>
                <td >Shiptype: </td>
                <td  class="tdsag" >
                    <asp:TextBox ID="TBpaddportname" Width="150px" runat="server"  MaxLength="20"  CssClass="kucukharf" ></asp:TextBox>
                </td>
            </tr>
                        <tr>
                <td >Speed : </td>
                <td  class="tdsag" >
                    <asp:TextBox ID="TBpaddhiz" Width="30px" runat="server" MaxLength="2"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="TBpaddhiz" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2"  class="tdorta"  style="height:50px; text-align:center; font-size:20px; font-weight:bold; font-family:'Trebuchet MS'">
                       
                       <asp:Button ID="Buttonlbadd" runat="server" style="height:30px; Width:80px" Text="Save"  OnClick="Buttonlbadd_Click"  />&nbsp;&nbsp; 
                       <asp:Button ID="Buttonlbaddcancel" runat="server" style="height:30px; Width:80px" Text="Cancel"   />         
                </td></tr>         
           </table>

        </asp:Panel>

            <asp:ModalPopupExtender 
            ID="ModalPopupExtenderlbadd" runat="server" 
            CancelControlID="Buttonlbaddcancel"
            TargetControlID="buttonshowpopuppadd" 
            PopupControlID="panellbadd"
            BackgroundCssClass="modalbodyarka" ></asp:ModalPopupExtender>

        <br />


<%-- modal onay message baþlar  
    onay için bu panele kes yapýþtýr. Baccepted  butonuna onclick olayý yaz.  --%>
 <asp:Button id="buttononayMessagePanel" runat="server" style="display:none;" /> 
        <asp:Panel ID="panelonayMessagePanel"    CssClass="panelmessage" runat="server" >  
    <div style="text-align:center; font-weight:bold;">        
<br /> Are you sure to delete the record? <br /><br /></div>
            <asp:Button ID="Baccepted" CommandName="0" runat="server" style="height:30px; Width:80px" Text="Yes"  OnClick="Baccepted_Click" />&nbsp;&nbsp; 
            <asp:Button ID="Bclosed" runat="server" style="height:30px; Width:80px" Text="Cancel"  />  
<br /><br />
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopuponayMessage" runat="server" TargetControlID="buttononayMessagePanel" PopupControlID="panelonayMessagePanel" BackgroundCssClass="modalbodyarka" CancelControlID="Bclosed" ></asp:ModalPopupExtender>
<%-- modal onay message biter --%>



            </ContentTemplate></asp:UpdatePanel>

      <!--
        <div class="select"> <strong>Other Pages: </strong>
          <select>
            <option>1</option>
          </select>
        </div>
      -->

      </div>
    </div>
    
  </div>
  <div id="footer"></div>    
    
</div>





    
  
    </form>
</body>
</html>
