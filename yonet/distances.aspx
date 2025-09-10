<%@ Page Language="C#" AutoEventWireup="true" CodeFile="distances.aspx.cs" Inherits="yonet_distances"  MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>PMTR</title>

    <link href="css/all.css" rel="stylesheet" />

            <style type="text/css">

.sagusttuslist {
	list-style-type: none;
}

.sagusttuslista {
    background-color: #fcd9b5;
	float: right;
	padding: 8px;
	font-family: Verdana, Geneva, sans-serif;
	font-size: 12px;
	color: #000;
	text-decoration: none;
	transition: background-color 1s, color 1s;
	-o-transition: background-color 1s, color 1s;
	-moz-transition: background-color 1s, color 1s;
	-webkit-transition: background-color 1s, color 1s;
        border-top-right-radius: 10px;
    border-bottom-left-radius: 10px;
    -moz-border-radius-topright: 10px;
    -moz-border-radius-bottomleft: 10px;
    -webkit-border-top-right-radius: 10px;
    -webkit-border-bottom-left-radius: 10px;
}
.sagusttuslista:hover {
	background-color: #ff8300;
	text-decoration: none;
	color: #fff;
        border-top-right-radius: 10px;
    border-bottom-left-radius: 10px;
    -moz-border-radius-topright: 10px;
    -moz-border-radius-bottomleft: 10px;
    -webkit-border-top-right-radius: 10px;
    -webkit-border-bottom-left-radius: 10px;
}

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
      <li><span><span> <asp:LinkButton ID="LinkButton1" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton></span></span></li>
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
        <li id="ulnavlihover"><a >Distances</a></li></ul>
<asp:Literal ID="Litmenu4" Text="" runat="server">
      <h3>Flags</h3> <ul class="nav">        
        <li><a href="flags.aspx">Flags</a></li></ul></asp:Literal>
<asp:Literal ID="Litmenu5" Text="" runat="server">
      <h3>Ship Types</h3><ul class="nav">
        <li><a href="shiptypes.aspx">Ship Types</a></li></ul></asp:Literal>
<asp:Literal ID="Litmenu6" Text="" runat="server">
      <h3>Pilot Stations</h3><ul class="nav">
        <li><a href="pilotstations.aspx">Pilot Stations</a></li></ul></asp:Literal>

</div>


      <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
 
    <div id="center-column">
      <div class="top-bar">



        <h1><a href="yonetim.aspx">Settings</a> > Distances </h1>
          
                  <div class="breadcrumbs"></div>
      </div>
      <br />
      <div class="select-bar">
        <label>
            <asp:DropDownList ID="DropDownListports"  runat="server"   Height="21px" Width="154px" OnSelectedIndexChanged="DropDownListports_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
        </label>

      </div>


      <div class="table">

       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="747px" OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="id" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"  OnSelectedIndexChanging="GridView1_SelectedIndexChanging"  CellPadding="2" ForeColor="#333333"  GridLines="Vertical"  >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/arrowr.png"  HeaderText=""  SelectText="" ItemStyle-Width="30px"   ShowSelectButton="True" />


<asp:TemplateField HeaderText="Departure"  ControlStyle-Width="150px" >
                    <ItemTemplate><asp:Label ID="Litemportlimanadi" runat="server" Text='<%# Bind("kalkisliman") %>'></asp:Label></ItemTemplate>

                    <ControlStyle></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                </asp:TemplateField>

<asp:TemplateField HeaderText="Arrival"   ControlStyle-Width="150px">
                    <ItemTemplate><asp:Label ID="Litemportrihtimadi" runat="server" Text='<%# Bind("varisliman") %>'></asp:Label> </ItemTemplate>

                    <ControlStyle></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                </asp:TemplateField>

<asp:TemplateField HeaderText="Dist.(min)"   ControlStyle-Width="35px">
                    <EditItemTemplate><asp:TextBox ID="TBeditportlimanbolge" runat="server" Text='<%# Bind("arasure") %>' MaxLength="3" ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="TBeditportlimanbolge" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender> </EditItemTemplate>
                    <ItemTemplate> <asp:Label ID="Litemportlimanbolge" runat="server" Text='<%# Bind("arasure") %>'></asp:Label> </ItemTemplate>

                    <ControlStyle ></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                </asp:TemplateField>

<asp:TemplateField HeaderText="D.Area"  ControlStyle-Width="35px">
                    <ItemTemplate><asp:Label ID="Litemportzorluk" runat="server" Text='<%# Bind("kalkisbolge") %>'></asp:Label></ItemTemplate>

                    <ControlStyle ></ControlStyle>
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:TemplateField>

<asp:TemplateField HeaderText="A.Area."   ControlStyle-Width="35px">
                    <ItemTemplate><asp:Label ID="Litemportresp" runat="server" Text='<%# Bind("varisbolge") %>'></asp:Label></ItemTemplate>

                    <ControlStyle ></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                </asp:TemplateField>


<asp:CommandField  ItemStyle-Width="45px" ShowEditButton="True"  EditImageUrl="../images/edit.png"  EditText=""   UpdateText="" UpdateImageUrl="../images/save.png"  CancelImageUrl="../images/cancel.png"   CancelText=""  ButtonType="Image" >
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:CommandField>

                
            </Columns>

            <EditRowStyle BorderStyle="None" BackColor="#77bbdF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>


        <br />
            </ContentTemplate></asp:UpdatePanel>
                   


      </div>
    </div>
    
  </div>
  <div id="footer"></div>    <asp:LinkButton ID="LBmainpage" runat="server" Text="Pilot Monitor"  OnClick="LBmainpage_Click"></asp:LinkButton> <br />
        <br />

        <asp:LinkButton ID="LBguvcik" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton>
    
</div>





    
  
    </form>
</body>
</html>
