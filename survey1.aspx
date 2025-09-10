<%@ Page Language="C#" AutoEventWireup="true" CodeFile="survey1.aspx.cs" Inherits="survey1" EnableEventValidation="false" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html  xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot - ARGE</title>
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
width: 370px;
height: 300px;
border: 1px  groove #111;
border-radius: 8px;
background-color:lightsalmon;
text-align:left;
color:black;
font-size:14px;
}
.modalbodyarka
{
background-color: #333333;
filter: alpha(opacity:70);
opacity: 0.6;
z-index: 10000;
}
.tdsag {
text-align:left;
border-radius: 8px;
height:35px;
}

.tdorta {
text-align:center;
background-color:aliceblue;
border-radius: 8px;
}


.sagusttuslist {
	list-style-type: none;
}

.sagusttuslista {
    width:140px;
    height:26px;
    background-color: lightsalmon;
	float: left;
	padding: 12px;
	font-family:'Trebuchet MS';
	font-size: 24px;
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
           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
               <ContentTemplate>

<table  style="width:1340px; float:right;   color:white">
 <tr  style="height:22px;  border-color:red; ">
     <td style="text-align:left;">
                      <asp:Button ID="ButtonLiveScreen" style="height:20px; Width:110px; font-size:x-small;  text-align:center;" runat="server" Text="Live Screen" OnClick="ButtonLiveScreen_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"   /> &nbsp;&nbsp;&nbsp;
                             <asp:DropdownList ID="DDLarges"  runat="server"  AutoPostBack="true" OnSelectedIndexChanged="DDLarges_SelectedIndexChanged" Width="300px" Height="25px"  ></asp:DropdownList>
     </td>
        <td style="text-align:right">
                <asp:LinkButton ID="LBonline" runat="server"  ForeColor="#111" OnClick="LBonline_Click"  OnClientClick="this.disabled='true'; "  ></asp:LinkButton> &nbsp;&nbsp
                <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="#111"  OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "  ></asp:LinkButton>&nbsp;&nbsp;
 </td></tr></table>  



<table  style="width:100% ;"> <tr><td style="text-align:left; width:100% ; height:25px" colspan="2"></td></tr>
<tr><td style="text-align:left; width:100% ;" colspan="2"><span style="font-family:'Trebuchet MS'; color:#222; font-size:medium; margin:0; ">
    <b>8 SAAT DİNLENME SÜRESİ (R/8) AR-GE ANKETİ </b><br /><br /></span></td>
</tr>
    <tr>
    <td style="text-align:left; width:7% ;"><img src="images/Survey100.jpg" /></td> 
        <td style="text-align:left; vertical-align:top; width:93% ;"><span style="font-family:'Trebuchet MS'; vertical-align:top; color:#222; margin:0; ">
            İş sonrası maksimum 8 saatlik dinlenme süresinin son zamanlardaki iş yoğunluğuna göre yeterli, eksik veya fazla 
            olup olmadığı konusunda pilotların görüşü alınacaktır. Seçiminizi yapmak için
        aşağıdaki 'Ankete Katıl' butonuna tıklayınız.</br></br>
                      <ul>
  <li class="sagusttuslist"><asp:LinkButton ID="LBaddvote" OnClick="LBaddvote_Click" class="sagusttuslista" runat="server" >Ankete Katıl</asp:LinkButton></li>
</ul>
</br></br></td></tr>

<tr><td style="text-align:left; width:100% ; height:25px" colspan="2">
    <b>Sonuçlar</b></br>
    <b>Katılan : </b><asp:Label ID="sonuc1" runat="server"></asp:Label></br>
    <b>Bekleyen : </b><asp:Label ID="sonuc2" runat="server"></asp:Label></br>
    <b><asp:Label ID="sonuc3" runat="server"></asp:Label> : </b><asp:Label ID="sonuc6" runat="server"></asp:Label></br>
    <b><asp:Label ID="sonuc4" runat="server"></asp:Label> : </b><asp:Label ID="sonuc7" runat="server"></asp:Label></br>
    <b><asp:Label ID="sonuc5" runat="server"></asp:Label> : </b><asp:Label ID="sonuc8" runat="server"></asp:Label></br>
    </br>
    </td></tr>


</table>  

      

      <div class="clearup"> </div>
   <div  style="clear:both; text-align:left;"> 


                          <asp:GridView ID="GridView7" runat="server"  OnRowDataBound="GridView7_RowDataBound"  AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical"  OnSelectedIndexChanging="GridView7_SelectedIndexChanging"  Width="100%" AllowPaging="True" PageSize="100" >
                             <AlternatingRowStyle BackColor="White" />
                             <Columns >
                                 <asp:CommandField ButtonType="Image" HeaderText=""  ItemStyle-Width="24px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                
                                  <asp:TemplateField ControlStyle-Width="30px" HeaderText="No">
                                     <ItemTemplate>
                                         <asp:Label ID="siraod" runat="server" Text='<%# Container.DataItemIndex +1   %>'></asp:Label>
                                         <asp:Label ID="anketno" runat="server" Visible="false" Text='<%# Bind("anketno") %>'></asp:Label>
                                         <asp:Label ID="kapno" runat="server" Visible="false" Text='<%# Bind("kapno") %>'></asp:Label>
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

                                 <asp:TemplateField ControlStyle-Width="30px" HeaderText="Watch">
                                     <ItemTemplate>
                                         <asp:Label ID="lblvarid" runat="server" ><%#Eval("varid")%></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="200px" HeaderText="Vote">
                                     <ItemTemplate>
                                         <asp:Label ID="lbltel" runat="server" ><%#Eval("secenek")%></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Date">
                                     <ItemTemplate>
                                          <div style="text-align:left"><asp:Label ID="wizbasla" runat="server" Text='<%#Eval("sectarihi")%>'></asp:Label></div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="30px" HeaderText="X">
                                     <ItemTemplate>
                                          <div style="text-align:left"><asp:Label ID="wizbit" runat="server" Text='<%#Eval("onay")%>'></asp:Label></div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                             



                             </Columns>
                             
                             <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB" Font-Names="Arial"  Font-Size="12px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False"  />
                             <SelectedRowStyle BackColor="#c1cDe1"  ForeColor="#333333" />
                         </asp:GridView>
      


        <asp:Button id="buttonshowpopuppadd" runat="server" style="display:none;" />
        <asp:Panel ID="panellbadd"   CssClass="panellbadd" runat="server"> 

<table >                        
                     <tr>
                <td  class="tdorta" >
                 <p >ANKET KATILIM SAYFASI</p></td>
            </tr>          
            <tr>
                <td  class="tdsag" ><b>Anket No:</b> <asp:Label ID="TBpaddportno"  runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td  class="tdsag" ><div><b>Anket Adı:</b> <asp:Label ID="TBpaddportname" runat="server"   CssClass="kucukharf" ></asp:Label>
                    <asp:Label ID="Lsecbir" runat="server"   Visible="false" ></asp:Label>
                    <asp:Label ID="Lseciki" runat="server"   Visible="false" ></asp:Label>
                    <asp:Label ID="Lsecuc" runat="server"   Visible="false"  ></asp:Label>
                    <asp:Label ID="Laktif" runat="server"   Visible="false" ></asp:Label>
                </td>
            </tr>
                        <tr>
                <td  class="tdsag" ><div><b>Açıklama:</b> 
                    Aşağıdaki açılır listeden uygun dinlenme süresini seçerek OYLA tuşuna basınız. İsterseniz daha sonra seçiminizi tekrar düzenleyebilirsiniz.<br /> <br /> </td>
            </tr>
                <tr>
                <td  class="tdsag" ><div><b>Tercihler:</b>
                    <asp:DropdownList ID="TBpaddsec"  runat="server"  Width="240px" Height="25px"  >
                        

                    </asp:DropdownList><br /> <br /> 
                </td>
            </tr>
           

            <tr>
                <td  class="tdorta"  style="height:80px; text-align:center; font-size:20px; font-weight:bold; font-family:'Trebuchet MS'">
                       
                       <asp:Button ID="Buttonlbadd" runat="server" style="height:30px; Width:80px" Text="OYLA"  OnClick="Buttonlbadd_Click"  />&nbsp;&nbsp; 
                       <asp:Button ID="Buttonlbaddcancel" runat="server" style="height:30px; Width:80px" Text="iptal"   />         
                </td></tr>         
           </table>

        </asp:Panel>
        </div>
                  <br /> <br /> 
<asp:ModalPopupExtender 
            ID="ModalPopupExtenderlbadd" runat="server" 
            CancelControlID="Buttonlbaddcancel"
            TargetControlID="buttonshowpopuppadd" 
            PopupControlID="panellbadd"
            BackgroundCssClass="modalbodyarka" ></asp:ModalPopupExtender>

        <br />

                    <div style="height:150px; clear:both; float:left;">  <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label></div>
                 </ContentTemplate></asp:UpdatePanel>

           </asp:Panel>
  

    </div>

    </form>
</body>
</html>
