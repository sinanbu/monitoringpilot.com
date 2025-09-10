<%@ Page Language="C#" AutoEventWireup="true" CodeFile="statisticsi.aspx.cs" Inherits="yonet_statisticsi" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>PMTR</title>

    <link href="css/all.css" rel="stylesheet" />


</head>

   

<body >
    <form id="form1" runat="server">
   <div id="main">
       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
  <div id="header"> <a href="#" class="logo"><img src="img/logo.png" width="46" height="48" alt="" /></a>
<div id="header-right"><asp:Literal ID="Litpagebaslik" runat="server" ></asp:Literal></div>
   <ul id="top-navigation">
      <li><span><span><a href="yonetim.aspx">Settings</a></span></span></li>
      <li  class="active" ><span><span>Statistics</span></span></li>
      <li><span><span><a href="vacations.aspx">Vacations</a></span></span></li>
      <li><span><span><a href="onlineusers.aspx">Online Users</a></span></span></li>
      <li><span><span><a href="../main.aspx">Pilot Monitor</a></span></span></li>
      <li><span><span> <asp:LinkButton ID="LBguvcik" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton></span></span></li>

    </ul>
  </div>
  <div id="middle">

    <div id="left-column-aralik"> <!--  id="ulnavlihover" -->
     
   
</div>

                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>

    <div id="center-column-full">       
      <div class="top-bar-full"> 
 <h1><a href="statistics.aspx">Statistics</a></h1>
        <div class="breadcrumbs"></div>
      </div>
      <br />
      <div class="select-bar"> </div>
 <div class="clear"> <!--  id="ulnavlihover" -->
      <ul class="navfull">
          
            <li class="liistatik1">  <asp:LinkButton ID="liistatik1" runat="server"  OnClick="liistatik1_Click">L3 Summary</asp:LinkButton></li>
<%--            <li class="liistatik1">  <asp:LinkButton ID="liistatik2" runat="server"  OnClick="liistatik2_Click">2M Summary</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik3" runat="server"  OnClick="liistatik3_Click">1Y Summary</asp:LinkButton></li>--%>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik4" runat="server"  OnClick="liistatik4_Click">Per Pilot</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik5" runat="server"  OnClick="liistatik5_Click">Per Ship</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik6" runat="server"  OnClick="liistatik6_Click">Per Terminal</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik7" runat="server"  OnClick="liistatik7_Click">One Day</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik8" runat="server"  OnClick="liistatik8_Click">One Month</asp:LinkButton></li>
            <li class="liistatik1a">  <asp:Label ID="liistatik9" runat="server"  >One Year</asp:Label></li>
</ul>  </div>

        <div class="clearup">
     
          <br />
              
            <%--arama radio no.9- One Year--%>
     <asp:TextBox ID="TextBox9" runat="server" MaxLength="4"    Font-Size="Small" Width="90px" Height="22px" ></asp:TextBox>
<asp:MaskedEditExtender  CultureName="tr-TR"  ID="MaskedEditExtender9" runat="server" TargetControlID="TextBox9" ErrorTooltipEnabled="true" MaskType="Number"  Mask="9999"></asp:MaskedEditExtender>


            <asp:Button ID="LBgetist3" runat="server" OnClick="LBgetist3_Click" Text="Get Data" Width="90px" Height="24px"></asp:Button>
 
     
                    <asp:Label ID="baslik9" runat="server"> <div  class="yazibigbold"><p></p>
                             <asp:Label ID="Lwoidyillik" runat="server" ></asp:Label>   </div></asp:Label>                   

                         <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanging="GridView9_SelectedIndexChanging"  Width="911px" AllowPaging="True" PageSize="50" OnPageIndexChanging="GridView9_PageIndexChanging">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-Width="24px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                
                                  <asp:TemplateField ControlStyle-Width="32px" HeaderText="No">
                                     <ItemTemplate>
                                         <asp:Label ID="sira" runat="server" Text='<%# Container.DataItemIndex +1   %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="130px" HeaderText="Pilot Name">
                                     <ItemTemplate>
                                         <asp:Label ID="pilotann" runat="server" Text='<%# Bind("pilotismi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                                 <asp:TemplateField ControlStyle-Width="125px" HeaderText="Ship Name">
                                     <ItemTemplate>
                                         <asp:Label ID="shipann" runat="server"  Text='<%# Bind("gemiadi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="60px" HeaderText="Type/Grt">
                                     <ItemTemplate>
                                         <asp:Label ID="tgrtann" runat="server" ><%#Eval("tipi")%>/<%#Eval("grt")%></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="125px" HeaderText="Departure">
                                     <ItemTemplate>
                                         <asp:Label ID="depann" runat="server" ><%#Eval("binisyeri") %> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="125px" HeaderText="Destination">
                                     <ItemTemplate>
                                         <asp:Label ID="destann" runat="server" ><%#Eval("inisyeri") %> <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="118px" HeaderText="Off Station ">
                                     <ItemTemplate>
                                         <asp:Label ID="sofann" runat="server" ><%#Eval("istasyoncikis") %> </asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="117px" HeaderText="On Station">
                                     <ItemTemplate>
                                         <asp:Label ID="sonann" runat="server" ><%#Eval("istasyongelis")%>  </asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                   <asp:TemplateField ControlStyle-Width="20px" HeaderText="X" >
                                     <ItemTemplate>
                                         <asp:Label  ID="cancelann" ToolTip='<%#Eval("manevraiptal") as string == "1"  ? "" : "Cancelled" %>' runat="server" ><%#Eval("manevraiptal") %></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                             </Columns>
                             <HeaderStyle BackColor="#507CD1" Font-Names="Trebuchet MS" Font-Size="11px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Font-Names="Trebuchet MS" Font-Size="11px" />
                             <SelectedRowStyle BackColor="#c1cDe1"  ForeColor="#333333" />
                         </asp:GridView>




                                 <%--<asp:TemplateField HeaderText="IByorulma"  ControlStyle-Width="450px" >
                    <ItemTemplate><div class="container">                           <div class="graph">
<div class="databar" style='width:<%#Eval("toplamissuresi")%>px;'> &nbsp;                               </div></div> </div>
</ItemTemplate>
    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Smaller" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Smaller" />
          </asp:TemplateField>--%>


                 </ContentTemplate></asp:UpdatePanel>

      </div>
       
    </div>
    
  </div>
  <div id="footer"></div>   




</div>





    
  
    </form>
</body>
</html>
