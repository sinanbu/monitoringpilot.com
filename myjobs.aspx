<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myjobs.aspx.cs" Inherits="myjobs" EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot - Daily Log</title>
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

                        /* Data Pager Styles */
    tr.pager-row td
    {
        border-top:solid 2px #bbd9ee;
    }
    .pager
    {
        font-family:arial,sans-serif;
        text-align:center;
        padding:6px;
        font-size:18px;
    }
    .pager span.command,
    .pager span.current,
    .pager a.command,
    tr.pager-row td a
    {
        color:#5a90ce;
        padding:0px 5px;
        text-decoration:none;
        border:none;
    }
    .pager a.command:hover,
    tr.pager-row td a:hover
    {
	    border:solid 2px #333;
        background-color:#faff00;
        color:#000;
        padding:2px 4px;
        text-decoration:none;
    }
    .pager span.current,
    tr.pager-row td span
    {
	    border:solid 2px #3c3c3c;
        background-color:#3e3e3e;
        color:#fff;
        font-weight:bold;
        padding:0px 6px;
    }
    tr.pager-row td
    {
	    border-top:none;
	    text-align:center;
    }
     tr.pager-row table
    {
	    height:35px;
	    margin:0 auto 0 auto;
    }


    </style>

    <script type="text/javascript" src="js/jquery-1.11.2.js"></script>
 <script type="text/javascript">
     function select() {
         $(".rowflash").click(function () {
             $(this).addClass("highlightRow").siblings().removeClass("highlightRow").removeClass("highlightRowclr");
         })

         $(".rowflash").dblclick(function () {
             $(this).addClass("highlightRowclr");
         })

         //$(".rowflash").hover(function () {
         //    $(this).css("color", "#222");
         //    }, function () {
         //        $(this).css("color", "#4F81BD");
         //})

         ;
     }

    </script>




</head>
<body onkeydown="return (event.keyCode!=13)">
    <form id="form1" runat="server">

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

                                <asp:Timer ID="gerisayimtik" runat="server" OnTick="gerisayimtik_Tick" Interval="30000"></asp:Timer>
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
                    <li id="mainmanu7" runat="server"><a href="mapizmitbay.html" target="_blank">Map</a></li>
                </ul>
                            </li>

                        </ul>
                    </div>

                </td>
            </tr>
        </table>

      <div style="width: 100%; text-align: left;">
            <asp:Panel ID="summariall" runat="server" >
           
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                <table style="width: 1340px; float: right; color: white">
                    <tr style="height: 22px; border-color: red;">
                        <td style="text-align: left;">
                                    <button runat="server" id="Bmain" style="border: 1px solid black; cursor: pointer; background-color: #FFB08E; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='main.aspx'">Live Screen</button>&nbsp;</td>
                        <td style="text-align: right">
                            <asp:LinkButton ID="LBonline" runat="server" ForeColor="#111" OnClick="LBonline_Click" OnClientClick="this.disabled='true'; "></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="LBmyjobs" runat="server"  ForeColor="#111"   Text="My Jobs"  Enabled="false"></asp:LinkButton> &nbsp;&nbsp
                            <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="#111" OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>

    <p>&nbsp;</p> 
                        <div class="clear"></div>

                        <p>&nbsp;</p> 

                        <asp:Label ID="LBnewjob" runat="server"  CssClass="tablobaslik4"  Style="cursor: default"  Text="MY JOBS HISTORY"></asp:Label>

                       <p>&nbsp;</p> <asp:Label ID="Lwoidgunluk"  Style="color:#111; font-size:14px;"  runat="server" ></asp:Label>  

                            
                           
                            <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#222" GridLines="Vertical" OnSelectedIndexChanging="GridView7_SelectedIndexChanging" Width="100%" AllowPaging="True" PageSize="100" OnPageIndexChanging="GridView7_PageIndexChanging">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-Width="24px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />

                                    <asp:TemplateField ControlStyle-Width="40px" HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label ID="siraod" runat="server" Text='<%# Container.DataItemIndex +1   %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="200px" HeaderText="Pilot Name">
                                        <ItemTemplate>
                                            <div style="text-align: left">
                                                <asp:Label ID="pilotod" runat="server" Text='<%# Bind("pilotismi") %>'></asp:Label></div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="200px" HeaderText="Ship Name">
                                        <ItemTemplate>
                                            <div style="text-align: left">
                                                <asp:Label ID="shipod" runat="server" Text='<%# Bind("gemiadi") %>'></asp:Label></div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="100px" HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="tgrtodtipi" runat="server"><%#Eval("tip")%></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="50px" HeaderText="Grt">
                                        <ItemTemplate>
                                            <asp:Label ID="tgrtod" runat="server"><%#Eval("grt")%></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="200px" HeaderText="Departure">
                                        <ItemTemplate>
                                            <div style="text-align: left">
                                                <asp:Label ID="depod" runat="server"><%#Eval("binisyeri") %> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %></asp:Label></div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="200px" HeaderText="Destination">
                                        <ItemTemplate>
                                            <div style="text-align: left">
                                                <asp:Label ID="destod" runat="server"><%#Eval("inisyeri") %> <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %></asp:Label></div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="125px" HeaderText="Pilot OnBoard">
                                        <ItemTemplate>
                                            <asp:Label ID="sofod" runat="server"><%#Eval("pob") %> </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="125px" HeaderText="Pilot Off">
                                        <ItemTemplate>
                                            <asp:Label ID="sonod" runat="server"><%#Eval("poff")%>  </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="30px" HeaderText="X">
                                        <ItemTemplate>
                                            <asp:Label ToolTip='<%#Eval("manevraiptal") as string == "1"  ? "" : "Cancelled" %>' ID="cancelod" runat="server"><%#Eval("manevraiptal") %></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>

                                <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="12px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <SelectedRowStyle BackColor="#c1cDe1" ForeColor="#333333" />
                                <PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="20" NextPageText=">>" PreviousPageText="<<" />
                                <PagerStyle CssClass="pager-row" />
                            </asp:GridView>



                        </div>
                        <div style="height: 150px; clear: both; float: left;">
                            <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label></div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </asp:Panel>


        </div>

    </form>
</body>
</html>
