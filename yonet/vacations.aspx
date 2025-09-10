<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vacations.aspx.cs" Inherits="yonet_vacations" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>PMTR</title>

    <link href="css/all.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server"> 

   <div id="main">
       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

  <div id="header"> <a href="#" class="logo"><img src="img/logo.png" width="46" height="48" alt="" /></a>
<div id="header-right"><asp:Literal ID="Litpagebaslik" runat="server" ></asp:Literal></div>
    <ul id="top-navigation">
      <li><span><span><a href="yonetim.aspx">Settings</a></span></span></li>
      <li><span><span><a href="Statistics.aspx">Statistics</a></span></span></li>
      <li class="active" ><span><span>Vacations</span></span></li>
      <li><span><span><a href="onlineusers.aspx">Online Users</a></span></span></li>
      <li><span><span><a href="../main.aspx">Pilot Monitor</a></span></span></li>
      <li><span><span> <asp:LinkButton ID="LBguvcik" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton></span></span></li>

    </ul>
  </div>
  <div id="middle">

    <div id="left-column"> <!--  id="ulnavlihover" -->
      <h3>Users</h3>
      <ul class="nav">
          <asp:Literal ID="Literal1" Text="" runat="server"> 
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
        <li><a href="shiptypes.aspx">Ship Types</a></li></ul>

      <h3>Pilot Stations</h3><ul class="nav">
        <li><a href="pilotstations.aspx">Pilot Stations</a></li></ul>

</div>

    <div id="center-column">
      <div class="top-bar"> 
        <h1>Vacations</h1>    
      </div>
      <br />
      <div class="select-bar"> 
           <div id="left-column-full"> <!--  id="ulnavlihover" -->
      <ul class="navfull">
        <asp:LinkButton ID="LBistna" runat="server" OnClick="LBistna_Click">Name Listing</asp:LinkButton>
        <asp:LinkButton ID="LBistda" runat="server" OnClick="LBistda_Click">Date Listing</asp:LinkButton>
      </ul>

                </div>

      </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>


         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="747px"  OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="kapno"  OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"  OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="2" ForeColor="#333333"  GridLines="Vertical" PageSize="200" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>

<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/arrowr.png"  HeaderText=""  ItemStyle-Width="30px"  SelectText="" ShowSelectButton="True" />

<asp:TemplateField HeaderText="No" ControlStyle-Width="40px" >
                    <ItemTemplate><asp:Label ID="Litemportlimanno" runat="server" Text='<%# Bind("kapsirano") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>

<asp:TemplateField HeaderText="Pilot NameSurname"  ControlStyle-Width="290px" >
                    <ItemTemplate><asp:Label ID="Litemportlimanadi" runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>

<asp:TemplateField HeaderText="Winter Vac.Start"  ControlStyle-Width="140px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditstarti" runat="server" Text='<%# Bind("izinbasla") %>' MaxLength="10" ></asp:TextBox>
                         <asp:MaskedEditExtender  CultureName="tr-TR"  ID="MaskedEditExtender1" runat="server" TargetControlID="TBeditstarti" ErrorTooltipEnabled="true" MaskType="Date" DisplayMoney="Left"  AcceptNegative="Left" Mask="99/99/9999"></asp:MaskedEditExtender></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="LBstarti" runat="server" Text='<%# Bind("izinbasla") %>'></asp:Label></ItemTemplate>
          </asp:TemplateField>

                
<asp:TemplateField HeaderText="Summer Vac.Start"  ControlStyle-Width="140px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditfinishi" runat="server" Text='<%# Bind("izinbit") %>' MaxLength="10" ></asp:TextBox>
                      <asp:MaskedEditExtender  CultureName="tr-TR"  ID="MaskedEditExtender1i" runat="server" TargetControlID="TBeditfinishi" ErrorTooltipEnabled="true" MaskType="Date" DisplayMoney="Left"  AcceptNegative="Left" Mask="99/99/9999"></asp:MaskedEditExtender> </EditItemTemplate>
                    <ItemTemplate><asp:Label ID="LBfinishi" runat="server" Text='<%# Bind("izinbit") %>'></asp:Label></ItemTemplate>
          </asp:TemplateField>

<asp:CommandField  ItemStyle-Width="60px" ShowEditButton="True" HeaderText="Edit"  EditImageUrl="../images/edit.png"  EditText=""   UpdateText="" UpdateImageUrl="../images/save.png"  CancelImageUrl="../images/cancel.png"   CancelText=""  ButtonType="Image" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:CommandField>


            </Columns>

            <EditRowStyle BorderStyle="None" BackColor="#77bbdF" />
            <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="11px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            <RowStyle BackColor="#EFF3FB"  Font-Size="11px" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />

        </asp:GridView>

                 </ContentTemplate></asp:UpdatePanel>


    </div>
    
  </div>
  <div id="footer"></div>  
</div>





    
  
    </form>
</body>
</html>
