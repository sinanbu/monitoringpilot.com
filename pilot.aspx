<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pilot.aspx.cs" Inherits="pilot" EnableEventValidation="false" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html  xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot - PILOT INFO</title>
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
                      <asp:Button ID="ButtonLiveScreen" style="height:20px; Width:110px; font-size:x-small;  text-align:center;" runat="server" Text="Live Screen" OnClick="ButtonLiveScreen_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"   /> &nbsp;&nbsp;&nbsp;</td>
        <td style="text-align:right">
                <asp:LinkButton ID="LBonline" runat="server"  ForeColor="#111" OnClick="LBonline_Click"  OnClientClick="this.disabled='true'; "  ></asp:LinkButton> &nbsp;&nbsp
                <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="#111"  OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "  ></asp:LinkButton>&nbsp;&nbsp;
 </td></tr></table>  



<table  style="width:100% ;"> <tr><td style="text-align:left; width:100% ; height:25px"></td></tr>
<tr><td style="text-align:left; width:100% ;"><span style="font-family:'Trebuchet MS'; color:#222; font-size:small; margin:0; "><b>PILOT INFORMATIONS AND VACATIONS </b><br /><br /></span></td>
</tr></table>  

      

      <div class="clearup"> </div>
   <div  style="clear:both; text-align:left;"> 


                          <asp:GridView ID="GridView7" runat="server"   OnRowDataBound="GridView7_RowDataBound"   AutoGenerateColumns="False" CellPadding="2" DataKeyNames="kapno" ForeColor="#333333" GridLines="Vertical"  OnSelectedIndexChanging="GridView7_SelectedIndexChanging"  Width="100%" AllowPaging="True" PageSize="100" >
                             <AlternatingRowStyle BackColor="White" />
                             <Columns >
                                 <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-Width="24px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                
                                  <asp:TemplateField ControlStyle-Width="30px" HeaderText="No">
                                     <ItemTemplate>
                                         <asp:Label ID="siraod" runat="server" Text='<%# Container.DataItemIndex +1   %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="150px" HeaderText="Pilot Name">
                                     <ItemTemplate>
                                          <div style="text-align:left"><asp:Label ID="pilotod" runat="server" Text='<%# Bind("kapadi") %>'></asp:Label></div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="150px" HeaderText="SurName">
                                     <ItemTemplate>
                                          <div style="text-align:left"><asp:Label ID="shipod" runat="server"  Text='<%# Bind("kapsoyadi") %>'></asp:Label></div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Gsm No">
                                     <ItemTemplate>
                                         <asp:Label ID="lbltel" runat="server" ><%#Eval("tel1")%></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="150px" HeaderText="E-mail">
                                     <ItemTemplate>
                                         <asp:Label ID="lblposta" runat="server" ><%#Eval("eposta")%></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="30px" HeaderText="Watch">
                                     <ItemTemplate>
                                         <asp:Label ID="lblvarid" runat="server" ><%#Eval("varid")%></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="W.Vacation Start">
                                     <ItemTemplate>
                                          <div style="text-align:left"><asp:Label ID="wizbasla" runat="server" Text='<%#Eval("izinbasla")%>'></asp:Label></div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="100px" HeaderText="First Job Day">
                                     <ItemTemplate>
                                          <div style="text-align:left"><asp:Label ID="wizbit" runat="server" Text='<%#Eval("izinbasla2")%>'></asp:Label></div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="100px" HeaderText="S.Vacation Start">
                                     <ItemTemplate>
                                         <asp:Label ID="sizbasla" runat="server" Text='<%#Eval("izinbit")%>'> </asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="100px" HeaderText="First Job Day">
                                     <ItemTemplate>
                                         <asp:Label ID="sizbit" runat="server" Text='<%#Eval("izinbit2")%>'> </asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>



                             </Columns>
                             
                             <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB" Font-Names="Arial"  Font-Size="12px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False"  />
                             <SelectedRowStyle BackColor="#c1cDe1"  ForeColor="#333333" />
                         </asp:GridView>



       </div>
                    <div style="height:150px; clear:both; float:left;">  <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label></div>
                 </ContentTemplate></asp:UpdatePanel>

           </asp:Panel>
  

    </div>

    </form>
</body>
</html>
