<%@ Page Language="C#" AutoEventWireup="true" CodeFile="watchsummary.aspx.cs" Inherits="watchsummary" EnableEventValidation="false" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html  xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot - WATCH SUMMARY</title>
 <link href="css/stil.css" rel="stylesheet" />
    <style type="text/css">
   
      
        .auto-style1 {
            width: 100%; 
                 }


        .highlightRow {
            color: #0000ee;
            border-spacing: 1px;
            background-color:  #FFE0BC;
            height: 22px;
        }

        .highlightRowclr{
            color: #111;
            border-spacing: 1px;
            background-color:  #fff;
            height: 22px;
        }


        #menutek {
            margin-top: -10px;
            margin-left: -108px;
            height: 20px;
            z-index: 20;
            position: absolute;
        }
            #menutek ul {
                list-style: none;
                width: 100px;
                height: 20px;
            }
                #menutek ul li {
                    float: left;
                    text-align: center;
                    margin: 0px;
                    padding: 0px;
                    background: #11a;
                    width: 100px;
                }
                    #menutek ul li a { /*ana menü özellikleri*/
                        padding: 5px;
                        display: block;
                        text-decoration: none;
                        color: #eee;
                        height: 30px;
                        background: #008;
                    }
                        #menutek ul li a:hover {
                            background: #00c;
                            color: #ccc;
                        }
                    #menutek ul li ul {
                        width: 100px;
                        display: none;
                    }
                        #menutek ul li ul li {
                            margin: 0px;
                            height: 30px;
                            width: 100px;
                        }
                    #menutek ul li:hover ul {
                        display: block;
                    }

        
.panellbadd
{
width: 980px;
min-height: 200px;
border: 1px  groove #111;
background-color:rgba(214, 214, 214, 1);
text-align:left;
vertical-align:top;
font-size:12px;
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

    <script type="text/javascript" src="js/jquery-1.11.2.js"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=DLDarica] td").bind("click", function () {
            var row = $(this).parent();
            $("[id*=DLDarica] tr").each(function () {
                if ($(this)[0] != row[0]) {
                    $("td", this).removeClass("selected_row");
                }
            });
            $("td", row).each(function () {
                if (!$(this).hasClass("selected_row")) {
                    $(this).addClass("selected_row");
                } else {
                    $(this).removeClass("selected_row");
                }
            });
        });
    });


   


</script>

</head>
<body onkeydown = "return (event.keyCode!=13)" >
    <form id="form1" runat="server" >

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

        <table style="width: 1340px;  color:#111; font-weight:bold;  height: 25px; background-image: url(images/boslukaltcizgi.png)">
            <tr style="height: 25px">
                <td style="text-align: right; width: 62%;">
                    <asp:Label ID="LblVarid" ForeColor="Red" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarid" runat="server" Text="" Visible="false"></asp:Literal>
                    <asp:Label ID="LblVarno" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarno" runat="server" Text="" Visible="false"></asp:Literal>
                    <asp:Label ID="LblVarbasla" runat="server" Text=""></asp:Label><asp:Literal ID="varbaslar" runat="server" Text="" Visible="false"></asp:Literal>
                    <asp:Label ID="LblVarbit" runat="server" Text=""></asp:Label>
                    <asp:Literal ID="varbiter" runat="server" Text="" Visible="false"></asp:Literal>

                </td>
                <td style="text-align: left; width: 38%;">
                    <asp:Panel ID="Panel1" runat="server">
                        <asp:UpdatePanel ID="UpdatePanelHeader" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:Timer ID="gerisayimtik" runat="server" OnTick="gerisayimtik_Tick" Interval="180000"></asp:Timer>
                                <asp:Label ID="LblReeltime" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </td>
                <td style="text-align: right; width: 10%;">

                    <div id="menutek">

                        <ul>
                            <li style="color:#fff; ">M E N U
                
                <ul>
                    <li id="mainmanu1" runat="server"><a href="watchsummary.aspx">Summary</a></li>
                    <li id="mainmanu2" runat="server"><a href="log.aspx">Daily Log</a></li>
                    <li id="mainmanu3" runat="server"><a href="joblist.aspx">Joblist</a></li>
                    <li id="mainmanu4" runat="server"><a href="oldjobs.aspx">Oldjobs</a></li>
                    <li id="mainmanu5" runat="server"><a href="pilot.aspx">Pilots</a></li>
                    <li id="mainmanu6" runat="server"><a href="portinfo.aspx">Port Info</a></li>
                    <li id="mainmanu7" runat="server"><a href="mapsection.aspx">Map</a></li>
                </ul>
                            </li>

                        </ul>
                    </div>

                </td>
            </tr>
        </table>

        <div style="width: 100%; text-align: left;">
 <asp:Panel ID="summariall" runat="server" >
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>

<table  style="width:1340px; float:right;   color:white">
 <tr  style="height:22px;  border-color:red; ">
     <td style="text-align:left;">
                      <asp:Button ID="ButtonLiveScreen" style="height:25px; Width:80px; font-size:x-small;  text-align:center;" runat="server" Text="Live Screen" OnClick="ButtonLiveScreen_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"   /> &nbsp;&nbsp;&nbsp;
     </td>
        <td style="text-align:right">
                <asp:LinkButton ID="LBonline" runat="server"  ForeColor="#111" OnClick="LBonline_Click"  OnClientClick="this.disabled='true'; "  ></asp:LinkButton> &nbsp;&nbsp
                <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="#111"  OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "  ></asp:LinkButton>&nbsp;&nbsp;
 </td></tr></table>  



<table  style="width:100% ;"> <tr><td style="text-align:left; width:100% ; height:25px"></td></tr>
<tr><td style="text-align:left; width:100% ;"><span style="font-family:'Trebuchet MS'; color:#222; font-size:small; margin:0; "><b>SUMMARIES AND SORTING OF WATCHES </b><br /><br /></span></td>
</tr></table>  


     


   
      <div class="clearup"> </div>
                <div  style="clear:both;">  
            <div  style="clear:both;"> 
          &nbsp;&nbsp;&nbsp;

        <div style="float:left;  margin-left:0px">
        <asp:Label ID="baslik1" runat="server">  <div  class="yazibigbold" style="text-align:left;"> <p></p>
                         Watch:<asp:Label ID="Lwid" runat="server" Text=""></asp:Label>
                         &nbsp;&nbsp;/&nbsp;&nbsp; Start: <asp:Label ID="Lwstart" runat="server" Text=""></asp:Label>
                                                              / Finish: <asp:Label ID="Lwfinish" runat="server" Text=""></asp:Label>
                         &nbsp;&nbsp;/&nbsp;&nbsp; Total Jobs: <asp:Label ID="Ljobs" runat="server"  ForeColor="Red"  Text=""></asp:Label>
                         &nbsp;&nbsp;/&nbsp;&nbsp; Total Work: <asp:Label ID="Lwork" runat="server" ForeColor="Red"  Text=""></asp:Label>
                         &nbsp;&nbsp;/&nbsp;&nbsp; OPAwt: <asp:Label ID="Lowa" runat="server" ForeColor="Red"  Text=""></asp:Label>
                     </div></asp:Label> 
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" PageSize="200" Width="960px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:CommandField ButtonType="Image" ItemStyle-HorizontalAlign="Center"  HeaderText="" ItemStyle-Width="31px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                <asp:TemplateField ControlStyle-Width="20px" ItemStyle-HorizontalAlign="center" HeaderText="No">
                                     <ItemTemplate >
                                         <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="180px"  ItemStyle-HorizontalAlign="Left"  HeaderText="Pilot Name">
                                     <ItemTemplate>
                                         <div style="text-align:left"><asp:LinkButton OnClick="kapadi_Click"  Font-Underline="false"  CommandName='<%#Eval("varno")%>'   CommandArgument='<%#Eval("kapno")%>'   ID="kapadi"  runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:LinkButton></div>
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


                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="One Work Time">
                                     <ItemTemplate>
                                         <asp:Label ID="owa" runat="server" Text='<%# Bind("owa") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="280px" HeaderText="Fatique">
                                     <ItemTemplate>
                                         <div class="container">
                                             <div class="graph">
                                                 <div class="databar" style='width:<%#Eval("Percentage")%>%;'></div>
                                             </div>
                                             <div class="datavalue">
                                                 <%#Eval("yorulma") %>
                                             </div>
                                         </div>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                             </Columns>
                             <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Font-Names="Arial"  Font-Size="12px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <SelectedRowStyle BackColor="#c1cDe1" ForeColor="#333333" />
                         </asp:GridView>
        </div>
 
        <div style="float:right; margin-right:0px">
        <br />
        <asp:GridView ID="GridView1d" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" PageSize="200" Width="350px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                <asp:TemplateField ControlStyle-Width="20px" ItemStyle-HorizontalAlign="center" HeaderText="No">
                                     <ItemTemplate >
                                         <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="180px" ItemStyle-HorizontalAlign="Left" HeaderText="Darıca Pilots Next Sorting">
                                     <ItemTemplate >
                                         <asp:Label ID="kapadi" runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="50px" HeaderText="Jobs">
                                     <ItemTemplate>
                                         <asp:Label ID="toj" runat="server" Text='<%# Bind("toplamissayisi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="60px" HeaderText="Fatique">
                                     <ItemTemplate>
                                            <asp:Label ID="yorulma" runat="server" Text='<%# Bind("yorulma") %>'></asp:Label>  
                                     </ItemTemplate>
                                 </asp:TemplateField>

                             </Columns>
                             <HeaderStyle BackColor="#6699FF" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Height="12px" Font-Names="Arial"  Font-Size="12px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                         </asp:GridView>
        <br />
        <asp:GridView ID="GridView1y" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" PageSize="200" Width="350px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                  <asp:TemplateField ControlStyle-Width="20px" ItemStyle-HorizontalAlign="center" HeaderText="No">
                                     <ItemTemplate >
                                         <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="180px" ItemStyle-HorizontalAlign="Left" HeaderText="Yarımca Pilots Next Sorting">
                                     <ItemTemplate >
                                         <asp:Label ID="kapadi" runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="50px" HeaderText="Jobs">
                                     <ItemTemplate>
                                         <asp:Label ID="toj" runat="server" Text='<%# Bind("toplamissayisi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="60px" HeaderText="Fatique">
                                     <ItemTemplate>
                                            <asp:Label ID="yorulma" runat="server" Text='<%# Bind("yorulma") %>'></asp:Label>  
                                     </ItemTemplate>
                                 </asp:TemplateField>


                             </Columns>
                             <HeaderStyle BackColor="#6699FF" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Height="12px" Font-Names="Arial"  Font-Size="12px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                         </asp:GridView>
        </div>     


         </div>   <div  style="clear:both;"> 
        <div style="float:left; margin-left:0px">
        <asp:Label ID="baslik2" runat="server"><div  class="yazibigbold" style="text-align:left;"><p>&nbsp;</p>
                             Watch.<asp:Label ID="Lwoid" runat="server" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; Start: <asp:Label ID="Lwstartonceki" runat="server" Text=""></asp:Label>
                                                                  / Finish: <asp:Label ID="Lwfinishonceki" runat="server" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; Total Jobs: <asp:Label ID="Lojobs" runat="server" ForeColor="Red" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; Total Work: <asp:Label ID="Lowork" runat="server" ForeColor="Red" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; OPAwt: <asp:Label ID="Lowao" runat="server" ForeColor="Red"  Text=""></asp:Label>
                         </div></asp:Label>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" PageSize="200" Width="960px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:CommandField ButtonType="Image"  ItemStyle-HorizontalAlign="Center"  HeaderText="" ItemStyle-Width="31px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                <asp:TemplateField ControlStyle-Width="20px" ItemStyle-HorizontalAlign="center" HeaderText="No">
                                     <ItemTemplate >
                                         <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="180px"  ItemStyle-HorizontalAlign="Left"  HeaderText="Pilot Name">
                                     <ItemTemplate>
                                         <asp:LinkButton OnClick="kapadi_Click"  Font-Underline="false"  CommandName='<%#Eval("varno")%>'   CommandArgument='<%#Eval("kapno")%>'   ID="kapadi"  runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:LinkButton>
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
                                 

                                  <asp:TemplateField ControlStyle-Width="100px" HeaderText="One Work Time">
                                     <ItemTemplate>
                                         <asp:Label ID="owao" runat="server" Text='<%# Bind("owa") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="280px" HeaderText="Fatique">
                                     <ItemTemplate>
                                         <div class="container">
                                             <div class="graph">
                                                 <div class="databar" style='width:<%#Eval("Percentage")%>%;'></div>
                                             </div>
                                             <div class="datavalue">
                                                 <%#Eval("yorulma") %>
                                             </div>
                                         </div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                             </Columns>
                             <EditRowStyle BorderStyle="None" BackColor="#77bbdF" />
                             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                             <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB" Font-Names="Arial"  Font-Size="12px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False"/>
                             <SelectedRowStyle BackColor="#c1cDe1" ForeColor="#333333" />
                         </asp:GridView>
        </div>
        
        <div style="float:right; margin-right:0px">
        <br /><br />
        <asp:GridView ID="GridView2d" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" PageSize="200" Width="350px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                <asp:TemplateField ControlStyle-Width="20px" ItemStyle-HorizontalAlign="center" HeaderText="No">
                                     <ItemTemplate >
                                         <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="180px" ItemStyle-HorizontalAlign="Left" HeaderText="Darıca Pilots Next Sorting">
                                     <ItemTemplate >
                                         <asp:Label ID="kapadi" runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="50px" HeaderText="Jobs">
                                     <ItemTemplate>
                                         <asp:Label ID="toj" runat="server" Text='<%# Bind("toplamissayisi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="60px" HeaderText="Fatique">
                                     <ItemTemplate>
                                            <asp:Label ID="yorulma" runat="server" Text='<%# Bind("yorulma") %>'></asp:Label>  
                                     </ItemTemplate>
                                 </asp:TemplateField>

                             </Columns>
                             <HeaderStyle BackColor="#6699FF" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Height="12px" Font-Names="Arial"  Font-Size="12px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                         </asp:GridView>
        <br />
        <asp:GridView ID="GridView2y" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" PageSize="200" Width="350px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                  <asp:TemplateField ControlStyle-Width="20px" ItemStyle-HorizontalAlign="center" HeaderText="No">
                                     <ItemTemplate >
                                         <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="180px" ItemStyle-HorizontalAlign="Left" HeaderText="Yarımca Pilots Next Sorting">
                                     <ItemTemplate >
                                         <asp:Label ID="kapadi" runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="50px" HeaderText="Jobs">
                                     <ItemTemplate>
                                         <asp:Label ID="toj" runat="server" Text='<%# Bind("toplamissayisi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="60px" HeaderText="Fatique">
                                     <ItemTemplate>
                                            <asp:Label ID="yorulma" runat="server" Text='<%# Bind("yorulma") %>'></asp:Label>  
                                     </ItemTemplate>
                                 </asp:TemplateField>


                             </Columns>
                             <HeaderStyle BackColor="#6699FF" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Height="12px" Font-Names="Arial"  Font-Size="12px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                         </asp:GridView>
        </div> 

                          
        </div>   <div  style="clear:both;">                         
        <div style="float:left; margin-left:0px">                   
        <asp:Label ID="baslik3" runat="server"><div  class="yazibigbold" style="text-align:left;"> <p>&nbsp;</p>
                             Watch.<asp:Label ID="Lwo2id" runat="server" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; Start: <asp:Label ID="Lwstartonceki2" runat="server" Text=""></asp:Label>
                                                                  / Finish: <asp:Label ID="Lwfinishonceki2" runat="server" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; Total Jobs: <asp:Label ID="Lo2jobs" runat="server" ForeColor="Red" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; Total Work: <asp:Label ID="Lo2work" runat="server" ForeColor="Red" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; OPAwt: <asp:Label ID="Lowao2" runat="server" ForeColor="Red"  Text=""></asp:Label>
                         </div></asp:Label>
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"  CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanging="GridView3_SelectedIndexChanging" PageSize="200" Width="960px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="31px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                <asp:TemplateField ControlStyle-Width="20px" ItemStyle-HorizontalAlign="center" HeaderText="No">
                                     <ItemTemplate >
                                         <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="180px"  ItemStyle-HorizontalAlign="Left"  HeaderText="Pilot Name">
                                     <ItemTemplate>
<asp:LinkButton OnClick="kapadi_Click"  CommandName='<%#Eval("varno")%>'  Font-Underline="false"  CommandArgument='<%#Eval("kapno")%>'   ID="kapadi"  runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:LinkButton>
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
                                 

                                  <asp:TemplateField ControlStyle-Width="100px" HeaderText="One Work Time">
                                     <ItemTemplate>
                                         <asp:Label ID="owao2" runat="server" Text='<%# Bind("owa") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                                 <asp:TemplateField ControlStyle-Width="280px" HeaderText="Fatique">
                                     <ItemTemplate>
                                         <div class="container">
                                             <div class="graph">
                                                 <div class="databar" style='width:<%#Eval("Percentage")%>%;'></div>
                                             </div>
                                             <div class="datavalue">
                                                 <%#Eval("yorulma") %>
                                             </div>
                                         </div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                             </Columns>
                             <EditRowStyle BorderStyle="None" BackColor="#77bbdF" />
                             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                             <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB" Font-Names="Arial"  Font-Size="12px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <SelectedRowStyle BackColor="#c1cDe1" ForeColor="#333333" />
                         </asp:GridView>
        </div>

        <div style="float:right; margin-right:0px">
        <br /><br />
        <asp:GridView ID="GridView3d" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" PageSize="200" Width="350px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                <asp:TemplateField ControlStyle-Width="20px" ItemStyle-HorizontalAlign="center" HeaderText="No">
                                     <ItemTemplate >
                                         <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="180px" ItemStyle-HorizontalAlign="Left" HeaderText="Darıca Pilots Next Sorting">
                                     <ItemTemplate >
                                         <asp:Label ID="kapadi" runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="50px" HeaderText="Jobs">
                                     <ItemTemplate>
                                         <asp:Label ID="toj" runat="server" Text='<%# Bind("toplamissayisi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="60px" HeaderText="Fatique">
                                     <ItemTemplate>
                                            <asp:Label ID="yorulma" runat="server" Text='<%# Bind("yorulma") %>'></asp:Label>  
                                     </ItemTemplate>
                                 </asp:TemplateField>

                             </Columns>
                             <HeaderStyle BackColor="#6699FF" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Height="12px" Font-Names="Arial"  Font-Size="12px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                         </asp:GridView>
        <br />
        <asp:GridView ID="GridView3y" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" PageSize="200" Width="350px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                  <asp:TemplateField ControlStyle-Width="20px" ItemStyle-HorizontalAlign="center" HeaderText="No">
                                     <ItemTemplate >
                                         <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="180px" ItemStyle-HorizontalAlign="Left" HeaderText="Yarımca Pilots Next Sorting">
                                     <ItemTemplate >
                                         <asp:Label ID="kapadi" runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="50px" HeaderText="Jobs">
                                     <ItemTemplate>
                                         <asp:Label ID="toj" runat="server" Text='<%# Bind("toplamissayisi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="60px" HeaderText="Fatique">
                                     <ItemTemplate>
                                            <asp:Label ID="yorulma" runat="server" Text='<%# Bind("yorulma") %>'></asp:Label>  
                                     </ItemTemplate>
                                 </asp:TemplateField>


                             </Columns>
                             <HeaderStyle BackColor="#6699FF" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Height="12px" Font-Names="Arial"  Font-Size="12px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                         </asp:GridView>
        </div> 



</div> </div>


                   
        <asp:Button  id="buttonshowpopuppe" runat="server" style="display:none;"/>
        <asp:Panel ID="paneleditpilot"  runat="server" CssClass="panellbadd" > 

<div style="border:1px solid white; background-color:orange; font-size:16px; font-weight:bold; height:30px ; text-align:center" ><br /><strong>
    <asp:label  ID="Lblpilotname" runat="server" Text="" ></asp:label> Watch Jobs.</strong></div><asp:Label Visible="false" ID="PilotEid" runat="server"></asp:Label>


<table style="width:980px">
                    <tr style=" height:22px; border-width:1px; border-style:solid;">
                        <td style="width:2%" >No</td>
                        <td  style="width:11%" >Ship Name</td>
                        <td  style="width:7%" >Flag</td>
                        <td  style="width:5%" >Grt</td>
                        <td  style="width:4%" >Type</td>
                        <td  style="width:10%" >Departure</td>
                        <td  style="width:10%" >Arrival</td>
                        <td  style="width:6%; text-align:center;" >Off-Station</td>
                        <td  style="width:6%; text-align:center;" >POB</td>
                        <td  style="width:6%; text-align:center;" >P.Off</td>
                        <td  style="width:6%; text-align:center;" >On-Station</td>
                        <td  style="width:12%; text-align:center; " >0... Rest Period ...6</td>
                        <td  style="width:1%; text-align:center;  border-width:1px; border-style:solid;" >hr</td>
                        <td  style="width:12%; text-align:center; " >0... Work Period ...6</td>

                    </tr>    
    <tr><td colspan="14"  style="background-color:black; height:2px;"></td></tr>

</table>
<asp:ListView ID="ListView1" runat="server"  >

                <itemtemplate>
                    <div> <table style="width:980px">
                    <tr style=" height:22px; border-width:1px; border-style:solid;">
                        <td style="width:2%" > <%# Container.DataItemIndex +1 %></td>
                        <td  style="width:11%" ><asp:label  ID="Label5" runat="server"  Font-Strikeout='<%#(Eval("manevraiptal").ToString())=="1"? true : false%>'  Text='<%#Eval("gemiadi")%>' ></asp:label><asp:label  ID="Labelpgkapno" runat="server" Text='<%#Eval("kapno")%>' Visible="false"></asp:label></td>
                        <td  style="width:7%" ><asp:label  ID="Label6" runat="server"  Text='<%#Eval("bayrak")%>' ></asp:label></td>
                        <td  style="width:5%" ><asp:label  ID="Label7" runat="server" Text='<%#Eval("grt")%>' ></asp:label></td>
                        <td  style="width:4%" ><asp:label  ID="Label15" runat="server" Text='<%#Eval("tip").ToString().Length>1? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ></asp:label></td>
                        <td  style="width:10%" ><asp:label  ID="Label8" runat="server" Text='<%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? Eval("binisyeri") :Eval("binisyeri")+ "/" + Eval("binisrihtim")   %>' ></asp:label></td>
                        <td  style="width:10%" ><asp:label  ID="Label9" runat="server" Text='<%#Eval("inisrihtim") as string == "0" || Eval("inisrihtim") as string == ""  ? Eval("inisyeri") :Eval("inisyeri")+ "/" + Eval("inisrihtim")  %>' ></asp:label></td>
                        <td  style="width:6%; text-align:center;" ><asp:label  ID="Label10" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? Eval("istasyoncikis").ToString().Substring(0,2)+"/"+Eval("istasyoncikis").ToString().Substring(11,5):""%>' ></asp:label></td>
                        <td  style="width:6%; text-align:center;" ><asp:label  ID="Label11" runat="server" Text='<%#Eval("pob").ToString().Length>0? Eval("pob").ToString().Substring(0,2)+"/"+Eval("pob").ToString().Substring(11,5):""%>' ></asp:label></td>
                        <td  style="width:6%; text-align:center;" ><asp:label  ID="Label12" runat="server" Text='<%#Eval("poff").ToString().Length>0? Eval("poff").ToString().Substring(0,2)+"/"+Eval("poff").ToString().Substring(11,5):""%>' ></asp:label></td>
                        <td  style="width:6%; text-align:center;" ><asp:label  ID="Label13" runat="server" Text='<%#Eval("istasyongelis").ToString().Length>0? Eval("istasyongelis").ToString().Substring(0,2)+"/"+Eval("istasyongelis").ToString().Substring(11,5):""%>' ></asp:label></td>

                        <td  style="width:12%; text-align:center; border-width:1px; border-style:solid;" >
                                                        <div class="containerr"><div class="graphr">
                                                 <div class="databarr1" style='width:<%#Convert.ToDouble(Eval("rangerest").ToString()==""?0:Eval("rangerest"))<6? Convert.ToInt32(Convert.ToDouble(Eval("rangerest").ToString()==""?0:Eval("rangerest"))*17):103%>%;'></div>
                                             </div>
                                             <div class="datavaluer"><%#Eval("rangerest") %></div>
                                         </div>

                        </td>
                        <td  style="width:1%;" ><asp:label  ID="Label3" runat="server" Text=" " ></asp:label></td>
                        <td  style="width:12%; text-align:center; border-width:1px; border-style:solid;" >
                            <div class="containerr"><div class="graphr">
                                                 <div class="databarr2" style='width:<%#Convert.ToDouble(Eval("rangework").ToString()==""?0:Eval("rangework"))<6? Convert.ToInt32(Convert.ToDouble(Eval("rangework").ToString()==""?0:Eval("rangework"))*17):103%>%;'></div>
                                             </div>
                                             <div class="datavaluer"><%#Eval("rangework") %></div>
                                         </div>
                            </td>

                    </tr>    </table> </div>
              </itemtemplate>

            </asp:ListView>



<div style="text-align:center; " class="clear"><br /><asp:Button ID="ButtonPilotEDTcancel" runat="server" style="height:30px; Width:120px" Text="Close"  CssClass="btn"/><br /><br /></div>
  </asp:Panel>
            <asp:ModalPopupExtender   
            ID="ModalPopupExtenderPilotEdit" runat="server"  
            CancelControlID="ButtonPilotEDTcancel" 
            TargetControlID="buttonshowpopuppe"  
            PopupControlID="paneleditpilot" 
            BackgroundCssClass="modalbodyarka" ></asp:ModalPopupExtender>


                    <div style="height:30px; clear:both; float:left;">  <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="30px"></asp:Label></div>

                 </ContentTemplate></asp:UpdatePanel>

           </asp:Panel>
  

    </div>

    </form>
</body>
</html>
