<%@ Page Language="C#" AutoEventWireup="true" CodeFile="statisticsc.aspx.cs" Inherits="yonet_statisticsc" %>

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
            <li class="liistatik1a">  <asp:label ID="liistatik3" runat="server" >1Y Summary</asp:label></li>--%>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik4" runat="server"  OnClick="liistatik4_Click">Per Pilot</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik5" runat="server"  OnClick="liistatik5_Click">Per Ship</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik6" runat="server"  OnClick="liistatik6_Click">Per Terminal</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik7" runat="server"  OnClick="liistatik7_Click">One Day</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik8" runat="server"  OnClick="liistatik8_Click">One Month</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik9" runat="server"  OnClick="liistatik9_Click">One Year</asp:LinkButton></li>
</ul>  </div>

        <div class="clearup">
     
  
      <br />

            <asp:DropDownList ID="DDLyilal" runat="server" Height="24px" Width="100px"  ></asp:DropDownList>
                   &nbsp;&nbsp;<asp:Button ID="LBgetist3" runat="server" OnClick="LBgetist3_Click" Text="Get Data" Width="90px" Height="24px"></asp:Button><p></p>
   <asp:Label ID="basliktop" runat="server">  <div  class="yazibigbold"> 
            Totals of  <asp:Label ID="baslikyilyaz" runat="server"></asp:Label>
                         &nbsp;&nbsp;  <br /> 
       Total Jobs: <asp:Label ID="basliktop1" runat="server"  ForeColor="Red"  Text=""></asp:Label>,&nbsp;&nbsp; 
       Total Work Hours: <asp:Label ID="basliktop2" runat="server" ForeColor="Red"  Text=""></asp:Label>,&nbsp;&nbsp; 
       One Work Time: <asp:Label ID="basliktop3" runat="server" ForeColor="Red"  Text=""></asp:Label><br /><br />
 </div></asp:Label>  
 <div  style="clear:both;">
       <div style="float:left;  margin-left:0px">
   <asp:Panel ID="paneltoppilot" Style="display: block;  overflow: hidden; font-weight:bold; font-size:12px" runat="server">

                            <asp:Repeater ID="Reptoppilot" runat="server"  OnItemDataBound="Reptoppilot_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="Tabletoppilot" style="width:260px; border-spacing:0; background-color:white; border:1px solid black">
                                         <tr >
                                    <td  style=" background-color:aqua;" colspan="3"> The Greatest Total Jobs / TOP 3 PILOT:</td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <tr style="border:1px solid black">
                                        <td  style="border:1px solid black"><%# Container.ItemIndex +1 %></td>
                                        <td  style="border:1px solid black"><asp:Label ID="padi" runat="server" Text='<%#Eval("kapno")%>'></asp:Label></td>
                                        <td  style="border:1px solid black"><asp:Label ID="totj" runat="server" Text='<%#Eval("say")%>'></asp:Label></td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
            </div>
             <div style="float:right;  margin-left:0px">
   <asp:Panel ID="paneltopwatch" Style="display: block;  float:left; overflow: hidden; font-weight:bold; font-size:12px" runat="server">

                            <asp:Repeater ID="Reptopwatch" runat="server"  OnItemDataBound="Reptopwatch_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="Tabletopw" style="width:260px; border-spacing:0; background-color:white; border:1px solid black">
                                    <tr >
                                    <td  style=" background-color:aquamarine;" colspan="3" >The Greatest WATCH Jobs: </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <tr style="border:1px solid black">
                                        <td  style="border:1px solid black"><asp:Label ID="wid" runat="server" ></asp:Label><asp:Label ID="padi" runat="server" Text='<%#Eval("varno")%>' Visible="false"></asp:Label></td>
                                        <td  style="border:1px solid black"><asp:Label ID="totj" runat="server" Text='<%#Eval("say")%>'></asp:Label></td>
                                        <td  style="border:1px solid black"><asp:Label ID="wdate" runat="server" ></asp:Label></td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
        </div>                 <p>&nbsp;</p>

 </div>


             <div class="clearup"> </div>
<p>&nbsp;</p><p>&nbsp;</p>
    
               <asp:Label ID="baslik1" runat="server">  <div  class="yazibigbold"> <p></p>
                         Watch:1.
                         &nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp; Jobs Count: <asp:Label ID="Ljobs" runat="server"  ForeColor="Red"  Text=""></asp:Label>
                         ,&nbsp;&nbsp; Total Work Hours: <asp:Label ID="Lwork" runat="server" ForeColor="Red"  Text=""></asp:Label>
                        ,&nbsp;&nbsp; Total Work Days: <asp:Label ID="Ldays" runat="server" ForeColor="Red"  Text=""></asp:Label>
                        ,&nbsp;&nbsp; Total GRT: <asp:Label ID="Lgrt" runat="server" ForeColor="Red"  Text=""></asp:Label>

                     </div></asp:Label>
                         <asp:GridView ID="GridView2a" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="kapno" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanging="GridView2a_SelectedIndexChanging" PageSize="200" Width="911px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-Width="31px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                 <asp:TemplateField ControlStyle-Width="200px" HeaderText="Pilot Name">
                                     <ItemTemplate>
                                         <asp:Label ID="kapadi" runat="server" Text='<%# Bind("pilotismi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Jobs Count">
                                     <ItemTemplate>
                                         <asp:Label ID="toj" runat="server" Text='<%# Bind("toplamissayisi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Work Hours">
                                     <ItemTemplate>
                                         <asp:Label ID="twh" runat="server" Text='<%# Bind("toplamissuresi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Work Days">
                                     <ItemTemplate>
                                         <asp:Label ID="trh" runat="server" Text='<%# Bind("toplamdinlenme") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="One Work Time">
                                     <ItemTemplate>
                                         <asp:Label ID="owa" runat="server" Text='<%# Bind("owa") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="280px" HeaderText="Total GRT">
                                     <ItemTemplate>
                                         <div class="container">
                                             <div class="graph">
                                                 <div class="databar" style='width:<%#Eval("Percentage")%>%;'>
                                                     &nbsp;</div>
                                             </div>
                                             <div class="datavalue">
                                                 <%#Eval("yorulma") %>
                                             </div>
                                         </div>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                             </Columns>
                             <HeaderStyle BackColor="#507CD1" Font-Names="Trebuchet MS" Font-Size="11px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Font-Names="Trebuchet MS" Font-Size="11px" />
                             <SelectedRowStyle BackColor="#c1cDe1"  ForeColor="#333333" />
                         </asp:GridView>

                    
                     <asp:Label ID="baslik2" runat="server"><div  class="yazibigbold"><p>&nbsp;</p>
                             Watch.2
                             &nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp; Jobs Count: <asp:Label ID="Ljobs2b" runat="server" ForeColor="Red" Text=""></asp:Label>
                             ,&nbsp;&nbsp; Work Hours: <asp:Label ID="Lwork2b" runat="server" ForeColor="Red" Text=""></asp:Label>
                        ,&nbsp;&nbsp; Total Work Days: <asp:Label ID="Ldays2" runat="server" ForeColor="Red"  Text=""></asp:Label>
                        ,&nbsp;&nbsp; Total GRT: <asp:Label ID="Lgrt2" runat="server" ForeColor="Red"  Text=""></asp:Label>

                         </div></asp:Label>
                         <asp:GridView ID="GridView2b" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="kapno" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanging="GridView2b_SelectedIndexChanging" PageSize="200" Width="911px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-Width="31px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                 <asp:TemplateField ControlStyle-Width="200px" HeaderText="Pilot Name">
                                     <ItemTemplate>
                                         <asp:Label ID="kapadio" runat="server" Text='<%# Bind("pilotismi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Jobs Count">
                                     <ItemTemplate>
                                         <asp:Label ID="tojo" runat="server" Text='<%# Bind("toplamissayisi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Work Hours">
                                     <ItemTemplate>
                                         <asp:Label ID="twho" runat="server" Text='<%# Bind("toplamissuresi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Work Days">
                                     <ItemTemplate>
                                         <asp:Label ID="trho" runat="server" Text='<%# Bind("toplamdinlenme") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="100px" HeaderText="One Work Time">
                                     <ItemTemplate>
                                         <asp:Label ID="owao" runat="server" Text='<%# Bind("owa") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="280px" HeaderText="Total GRT">
                                     <ItemTemplate>
                                         <div class="container">
                                             <div class="graph">
                                                 <div class="databar" style='width:<%#Eval("Percentage")%>%;'>
                                                     &nbsp;</div>
                                             </div>
                                             <div class="datavalue">
                                                 <%#Eval("yorulma") %>
                                             </div>
                                         </div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                             </Columns>
                             <HeaderStyle BackColor="#507CD1" Font-Names="Trebuchet MS" Font-Size="11px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Font-Names="Trebuchet MS" Font-Size="11px" />
                             <SelectedRowStyle BackColor="#c1cDe1"  ForeColor="#333333" />
                         </asp:GridView>
                
                   
                     <asp:Label ID="baslik3" runat="server"><div  class="yazibigbold"> <p>&nbsp;</p>
                             Watch.3
                             &nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp; Jobs Count: <asp:Label ID="Ljobs2c" runat="server" ForeColor="Red" Text=""></asp:Label>
                             ,&nbsp;&nbsp; Work Hours: <asp:Label ID="Lwork2c" runat="server" ForeColor="Red" Text=""></asp:Label>
                        ,&nbsp;&nbsp; Total Work Days: <asp:Label ID="Ldays3" runat="server" ForeColor="Red"  Text=""></asp:Label>
                        ,&nbsp;&nbsp; Total GRT: <asp:Label ID="Lgrt3" runat="server" ForeColor="Red"  Text=""></asp:Label>

                         </div></asp:Label>
                         <asp:GridView ID="GridView2c" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="kapno" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanging="GridView2c_SelectedIndexChanging" PageSize="200" Width="911px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-Width="31px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                 <asp:TemplateField ControlStyle-Width="200px" HeaderText="Pilot Name">
                                     <ItemTemplate>
                                         <asp:Label ID="kapadio2" runat="server" Text='<%# Bind("pilotismi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Jobs Count">
                                     <ItemTemplate>
                                         <asp:Label ID="tojo2" runat="server" Text='<%# Bind("toplamissayisi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Work Hours">
                                     <ItemTemplate>
                                         <asp:Label ID="twho2" runat="server" Text='<%# Bind("toplamissuresi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Work Days">
                                     <ItemTemplate>
                                         <asp:Label ID="trho2" runat="server" Text='<%# Bind("toplamdinlenme") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="100px" HeaderText="One Work Time">
                                     <ItemTemplate>
                                         <asp:Label ID="owao2" runat="server" Text='<%# Bind("owa") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                                 <asp:TemplateField ControlStyle-Width="280px" HeaderText="Total GRT">
                                     <ItemTemplate>
                                         <div class="container">
                                             <div class="graph">
                                                 <div class="databar" style='width:<%#Eval("Percentage")%>%;'>
                                                     &nbsp;</div>
                                             </div>
                                             <div class="datavalue">
                                                 <%#Eval("yorulma") %>
                                             </div>
                                         </div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                             </Columns>
                             <HeaderStyle BackColor="#507CD1" Font-Names="Trebuchet MS" Font-Size="11px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Font-Names="Trebuchet MS" Font-Size="11px" />
                             <SelectedRowStyle BackColor="#c1cDe1" ForeColor="#333333" />
                         </asp:GridView>


                 </ContentTemplate></asp:UpdatePanel>

      </div>
       
    </div>
    
  </div>
  <div id="footer"></div>   




</div>





    
  
    </form>
</body>
</html>
