<%@ Page Language="C#" AutoEventWireup="true" CodeFile="offlineusers.aspx.cs" Inherits="yonet_offlineusers" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>MPTR OfflineUsers</title>

    <style type="text/css">

            .panellbadd2
{
width: 700px;
height: 500px;
border: 1px  groove #111;
background-color:rgba(214, 214, 214, 1);
text-align:left;
vertical-align:top;
font-size:11px;
color:black;
line-height:10px;
}
.modalbodyarka
{
background-color: #333333;
filter: alpha(opacity:70);
opacity: 0.6;
z-index: 10000;
}

    </style>


    <link href="css/all.css" rel="stylesheet" />

</head>

<body>
    <form id="form1" runat="server"> 

   <div id="main">
       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

  <div id="header"> <a href="#" class="logo"><img src="img/logo.png" width="46" height="48" alt="" /></a>
<div id="header-right">MPTR Admin Page</div>
    <ul id="top-navigation">
      <li><span><span><a href="yonetim.aspx">Settings</a></span></span></li>
      <li><span><span><a href="Statistics.aspx">Statistics</a></span></span></li>
      <li><span><span><a href="vacations.aspx">Vacations</a></span></span></li>
      <li class="active" ><span><span>Online Users</span></span></li>
      <li><span><span><a href="../main.aspx">Pilot Monitor</a></span></span></li>
      <li><span><span> <asp:LinkButton ID="LBguvcik" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton></span></span></li>

    </ul>
  </div>
  <div id="middle">

    <div id="left-column"> 
      <h3>Users</h3>
      <ul class="nav" >
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
        <h1><a href="onlineusers.aspx" >Offline Users</a></h1>    
        <h1><a href="onofflineiplist.aspx">User IP List</a></h1> 
      </div>
      <br />
      <div class="select-bar"> </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>


         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="747px"   DataKeyNames="id"   OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="2" ForeColor="#333333"  GridLines="Vertical" PageSize="200" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>

<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/arrowr.png"  HeaderText=""  ItemStyle-Width="30px"  SelectText="" ShowSelectButton="True" />

<asp:TemplateField HeaderText="KapNo" ControlStyle-Width="40px" >
                    <ItemTemplate><asp:Label ID="Litemportlimanno" runat="server" Text='<%# Eval("kapno") %>'></asp:Label></ItemTemplate>

            </asp:TemplateField>

<asp:TemplateField HeaderText="Name Surname"  ControlStyle-Width="290px" >
                    <ItemTemplate><asp:LinkButton  OnClick="kapadi_Click" Font-Underline="false"   CommandArgument='<%#Eval("kapno")%>' ID="Litemportlimanadi"  runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:LinkButton>
                        </ItemTemplate>
            </asp:TemplateField>

<asp:TemplateField HeaderText="PC-IP"  ControlStyle-Width="160px" >
                    <ItemTemplate><asp:Label ID="LBstarti" runat="server" Text='<%# Bind("pcip") %>'></asp:Label></ItemTemplate>
          </asp:TemplateField>

                
<asp:TemplateField HeaderText="Login Time"  ControlStyle-Width="180px" >
                    <ItemTemplate><asp:Label ID="LBfinishi" runat="server" Text='<%# Bind("login") %>'></asp:Label></ItemTemplate>
          </asp:TemplateField>


            </Columns>

            <EditRowStyle BorderStyle="None" BackColor="#77bbdF" />
            <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="11px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            <RowStyle BackColor="#EFF3FB"  Font-Size="11px" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />

        </asp:GridView>




        <asp:Button  id="buttonshowpopuppe" runat="server" style="display:none;"/>
        <asp:Panel ID="paneleditpilot"  runat="server" CssClass="panellbadd2" ScrollBars="Vertical"> 

<div style="border:1px solid white; background-color:orange; font-size:16px; font-weight:bold; height:30px ; text-align:left" >
    <strong> <asp:Button ID="ButtonPilotEDTcancel" runat="server" style="height:30px; Width:30px" Text="X"  CssClass="btn"/>
        <asp:label  ID="Lblpilotname" runat="server" Text="" ></asp:label></strong></div>
            <asp:Label Visible="false" ID="PilotEid" runat="server"></asp:Label>
           
                      <table style="width:100%">
                    <tr style="border-bottom: 1px solid gray; border-left:1px solid aliceblue; height:22px; text-align:left; font-weight:bold; ">
                        <td style="width:3% "> No</td>
                        <td  style="width:47% "><asp:Label ID="Lkapadi" runat="server" Text='User Name/ Password'></asp:Label></td>
                        <td  style="width:20% "><asp:label  ID="Lpcip" runat="server" Text='PC IP' ></asp:label></td>
                        <td  style="width:20% "><asp:label  ID="Llogin" runat="server" Text='Login Time' ></asp:label></td>
                        <td  style="width:10% "><asp:label  ID="Llogof" runat="server" Text='Logof Time' ></asp:label></td>
                    </tr>    </table>

<asp:ListView ID="ListView1" runat="server"  >
    
                <itemtemplate >
                    <div> <table style="width:100%">
                    <tr style="border-bottom: 1px solid gray; border-left:1px solid aliceblue; height:22px; text-align:left">
                        <td  style="width:3% "> <%# Container.DataItemIndex +1 %></td>
                        <td  style="width:47% "><asp:Label ID="Lkapadi" runat="server" Text='<%#Eval("kapadisoyadi") %>'></asp:Label></td>
                        <td  style="width:20% "><asp:label  ID="Lpcip" runat="server" Text='<%#Eval("pcip")%>' ></asp:label></td>
                        <td  style="width:20% "><asp:label  ID="Llogin" runat="server" Text='<%#Eval("login")%>' ></asp:label></td>
                        <td  style="width:10% "><asp:label  ID="Llogof" runat="server" Text='<%#Eval("logof")%>' ></asp:label></td>
                    </tr>    </table> </div>
              </itemtemplate>

            </asp:ListView>



<div style="text-align:center; " class="clear"><br />
   <br /><br /></div>
  </asp:Panel>
            <asp:ModalPopupExtender   
            ID="ModalPopupExtenderPilotEdit" runat="server"  
            CancelControlID="ButtonPilotEDTcancel" 
            TargetControlID="buttonshowpopuppe"  
            PopupControlID="paneleditpilot" 
            BackgroundCssClass="modalbodyarka" ></asp:ModalPopupExtender>

                 </ContentTemplate></asp:UpdatePanel>


    </div>
    
  </div>
  <div id="footer"></div>  
</div>





    
  
    </form>
</body>
</html>
