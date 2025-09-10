<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log.aspx.cs" Inherits="log" EnableEventValidation="false" %>

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
            color: #222;
            border-spacing: 1px;
            background-color: #ffffd0;
            height: 22px;
        }

        .highlightRowclr {
            color: #4F81BD;
            border-spacing: 1px;
            background-color: #fff;
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

.tdis1 { width: 20px;}
.tdis2 { width: 130px;}
.tdis3 { width: 150px;}
.tdis4 { width: 50px;}
.tdis5 { width: 120px;}
.tdis6 { width: 120px;}
.tdis7 { width: 100px;}
.tdis8 { width: 70px;}
.tdis9 { width: 145px;}
.tdis10 { width: 45px;}
.tdis11 { width:280px;}
.tdis12 { width: 70px;}
.tdis13 { width: 30px;}
.tdis14 { width: 10px;}

      .panelpilotgec {
            width: 1000px;
            height: auto;
            border: 1px groove #111;
            background-color: white;
            margin-left: 5px;
            margin-right: 5px;
            margin-top: auto;
            font-family: 'Trebuchet MS';
            font-size: small;
            text-align: left;
        }

        .panelmessage {
            width: 300px;
            height: 230px;
            border: 1px groove #111;
            background-color: white;
            margin: 15px;
            font-family: 'Trebuchet MS';
            font-size: small;
            text-align: center;
            color: black;
        }
        .paneltus {
            width: 300px;
            height: 20px;
            border: 1px groove #111;
            background-color: white;
            margin: 15px;
            font-family: 'Trebuchet MS';
            font-size: 15px;
            text-align: center;
            color: red;
        }
        .paneltusnoborder {
            width: 300px;
            height: 20px;
            background-color: white;
            margin: 15px;
            font-family: 'Trebuchet MS';
            font-size: 14px;
            text-align: center;
            color: red;
        }


                .panelmessagejur {
            width: 270px;
            height: 450px;
            border: 1px groove #111;
            background-color: white;
            margin: 15px;
            margin-left:15px;
            font-family: 'Trebuchet MS';
            font-size: small;
            text-align: left;
            color: black;
        }

        .panelmessagevarch {
            width: 390px;
            height: 300px;
            border: 1px groove #111;
            background-color: white;
            margin: 10px;
            font-family: 'Trebuchet MS';
            font-size: 11px;
            text-align: center;
            border-radius: 10px;
            color: black;
        }

        .panelmessagecd {
            width: 300px;
            height: 160px;
            border: 1px groove #111;
            background-color: white;
            margin: 15px;
            font-family: 'Trebuchet MS';
            font-size: small;
            text-align: center;
            color: black;
        }

        .paneltakviye {
            width: 450px;
            height: 430px;
            border: 1px groove #111;
            background-color: white;
            margin: 15px;
            font-family: 'Trebuchet MS';
            font-size: small;
            text-align: center;
            color: black;
        }

        .panelizinli {
            width: 500px;
            height: 400px;
            border: 1px groove #111;
            background-color: white;
            margin: 15px;
            font-family: 'Trebuchet MS';
            font-size: small;
            text-align: center;
            color: black;
        }

        .modalbodyarka {
            background-color: #333333;
            filter: alpha(opacity:70);
            opacity: 0.6;
            z-index: 10000;
            color: black;
        }
                .panel-style1 {
            text-align: right;
            height: 20px;
            
        }
        #TBjurnot {
            resize:none;
        }
        #TBjurnoty{
            resize:none;
        }

                .modalbodyarka {
            background-color: #000000;
            filter: alpha(opacity:30);
            opacity: 0.8;
            z-index: 10000;
            color: black;
        }
    </style>

   
    <script type="text/javascript" src="js/jquery-1.11.2.js"></script>
    <script type="text/javascript">
        function select() {
            $(".rowflash").click(function() {
                $(this).addClass("highlightRow").siblings().removeClass("highlightRow").removeClass("highlightRowclr");
            })

            $(".rowflash").dblclick(function() {
                $(this).addClass("highlightRowclr");
            })

            $(".rowflash").hover(function () {
                $(this).css("color", "#00a");
                }, function () {
                    $(this).css("color", "#222");
            })

            ;
        }

    </script>




</head>
<body onkeydown="return (event.keyCode!=13)">
    <form id="form1" runat="server">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnableScriptGlobalization="true" runat="server"></asp:ToolkitScriptManager>


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
                    <li id="mainmanu7" runat="server"><a href="mapsection.aspx">Map</a></li>
                </ul>
                            </li>

                        </ul>
                    </div>

                </td>
            </tr>
        </table>

      <div style="width: 100%; text-align: left;">
            <asp:Panel ID="summariall" runat="server" DefaultButton="LBgetist3">
           
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                <table style="width: 1340px; float: right; color: white">
                    <tr style="height: 22px; border-color: red;">
                        <td style="text-align: left;">
                                    <button runat="server" id="Bmain" style="border: 1px solid black; cursor: pointer; background-color: #FFB08E; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='main.aspx'">Live Screen</button>&nbsp;
                                    <button runat="server" id="Bdaricaships" style="border: 1px solid black; cursor: pointer; background-color: #7DA3CD; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='daricaships.aspx'">Darıca Ships</button>&nbsp;
                                    <button runat="server" id="Byarimcaships" style="border: 1px solid black; cursor: pointer; background-color: #9B86B5; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='yarimcaships.aspx'">Yarımca Ships</button>


                        </td>
                        <td style="text-align: right">
                            <asp:LinkButton ID="LBonline" runat="server" ForeColor="#111" OnClick="LBonline_Click" OnClientClick="this.disabled='true'; "></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="#111" OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>

    <p>&nbsp;</p> 
                        <div class="clear"></div>
                     
                            <asp:TextBox ID="TextBox7" runat="server"  MaxLength="10" Font-Size="Small" Width="90px" Height="22px"></asp:TextBox><asp:MaskedEditExtender CultureName="tr-TR" ID="MaskedEditExtender7" runat="server" TargetControlID="TextBox7" ErrorTooltipEnabled="true" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" Mask="99/99/9999"></asp:MaskedEditExtender>&nbsp;
                            <asp:Button ID="LBgetist3" runat="server" OnClick="LBgetist3_Click" Text="Show Jobs" Width="90px" Height="24px" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"></asp:Button><br />
                        <asp:Calendar ID="Calendar1" Visible="false" runat="server"></asp:Calendar>

                        <asp:CalendarExtender ID="Date_CalendarExtender" runat="server"  
  Enabled="True" Format="dd.MM.yyyy" TargetControlID="TextBox7">
  </asp:CalendarExtender>

                        <p>&nbsp;</p> 

                        <asp:Label ID="LBnewjob" runat="server"  CssClass="tablobaslik4"  Style="cursor: default"  Text="DAILY LOG"></asp:Label>
                <p>&nbsp;</p>

    <tr  style="height:22px"><td  style="text-align:left">
    <asp:Label ID="LBLjdaily" runat="server"  Font-Size="12px" ForeColor="#660066" Text=""></asp:Label>
    </td></tr>

                       <p>&nbsp;</p> 
                        <asp:Label ID="Lwoidgunluk"  Style="color:#111; font-size:14px;"  runat="server" ></asp:Label>  

                            
                           
                            

                        <asp:Panel ID="panelcollapsein" Style="display: block; overflow: hidden;" runat="server">


                         <asp:Repeater ID="Replog" runat="server" OnItemDataBound="Replog_ItemDataBound"  >

                                <HeaderTemplate>
                                    <table id="TDLNewShipListc" class="tablo4">
                                        <tr class="trbaslik4">
                                            <td class="tdis1">No</td>
                                            <td class="tdis2">Pilot Name</td>
                                            <td class="tdis3">Ship Name</td>
                                            <td class="tdis4">Grt</td>
                                            <td class="tdis5">Departure Place</td>
                                            <td class="tdis6">Destination Place</td>
                                            <td class="tdis7">POB-Poff</td>
                                            <td class="tdis8">WorkHour</td>
                                            <td class="tdis9">Tugs</td>
                                            <td class="tdis10">M.Boat</td>
                                            <td class="tdis11">Jur.log</td>
                                            <td class="tdis12">Agency</td>
                                            <td class="tdis13">Opr.</td>
                                            <td class="tdis14">X</td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <tr id="Row" runat="server" class="trsatir4 rowflash">
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec" runat="server" Font-Bold="true" ForeColor="Red" Style="text-decoration: none;" Text='<%# Container.ItemIndex +1 %>'></asp:Label></td>
                                        <td class="tdis2"><%#Eval("pilotismi")%></td>
                                        <td class="tdis3 ">
<asp:LinkButton ID="logedit"  CommandArgument='<%#Eval("id")%>' runat="server"  Text='<%#Eval("gemiadi")%>' OnClick="logedit_Click"/> 
<asp:Label ID="logeditL" runat="server" Text='<%# Eval("gemiadi")%>' Visible="false"/>                                            </td>
                                        <td class="tdis4 "><%#Eval("grt")%></td>
                                        <td class="tdis5 ">
                                                <asp:Label ID="depod" runat="server"><%#Eval("binisyeri") %> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %></asp:Label></td>
                                        <td class="tdis6">
                                                <asp:Label ID="destod" runat="server"><%#Eval("inisyeri") %> <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %></asp:Label></td>
                                        <td class="tdis7 "><%#(Eval("pob") as string).Substring(0,2)+"/"+(Eval("pob") as string).Substring(11,5) +"-"+ (Eval("Poff") as string).Substring(11,5) %></td>

                                        <td class="tdis8"><%#Eval("calsaat1")  as string == null || Eval("calsaat1")  as string == ""  ? "" :  (Eval("calsaat1")+"-"+Eval("calsaat2")) %> <%#Eval("calsaat3")  as string == null || Eval("calsaat3")  as string == ""  ? "" : "/" + (Eval("calsaat3")+"-"+Eval("calsaat4")) %></td>

                                        <td class="tdis9"><%#Eval("rom1")%></td>
                                        <td class="tdis10 "><%#Eval("mboat")%> </td>

                                        <td class="tdis11 "><asp:Label ID="jurnot" runat="server" Text='<%# Eval("jnot") == null ? "": (Eval("jnot").ToString().Length>40) ? (Eval("jnot") as string).Substring(0,41)+"." : Eval("jnot")%>' ToolTip='<%#Eval("jnot") %>'></asp:Label></td>
                                       
                                        <td class="tdis12 ">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%# Eval("acente") == null ? "": (Eval("acente").ToString()==""? "" :  (Eval("acente").ToString().Length>7? (Eval("acente") as string).Substring(0,8)+"." : Eval("acente")))%>' ToolTip='<%#Eval("acente") %>'></asp:Label></td>
                                        <td class="tdis13 ">
                                            <asp:Label ID="Lbl20c"       runat="server" Text='<%#Eval("oper") == null ? "": (Eval("oper").ToString().Length>6? (Eval("oper") as string).Substring(0,7)+"." : Eval("oper"))%>'  ToolTip='<%# Eval("oper") %>'></asp:Label></td>
                                        <td class="tdis14 ">
                                            <asp:Label ToolTip='<%#Eval("manevraiptal") as string == "1"  ? "" : "Cancelled" %>' ID="cancelod" runat="server"><%#Eval("manevraiptal") %></asp:Label></td>
                                    </tr>   


                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>

    

                                </asp:Panel>
                        <p>&nbsp;</p>


                              
<%-- modal live jurnal note başlar --%>
 <asp:Button id="buttonjurnot" runat="server" style="display:none;" />
        <asp:Panel ID="paneljurnal"   CssClass="panelmessagejur" runat="server"   Style="display: none; ">  

                          <div style="margin-top: 2px; clear: both; align-content:center;">
                                  <table>
                                      <tr>
                                        <td colspan="2">
                                            <div style="border: 1px solid blue; background-color:dodgerblue; font-size:16px; font-weight: bold; height: 20px; text-align: center"><strong>E - JURNAL</strong></div>
                                            <asp:Label Visible="false" ID="LJEid" runat="server"></asp:Label></td>
                                    </tr>
                                       <tr>
                                        <td colspan="2"  class="paneltus"  >
                                           
                                                <strong><asp:Label ID="LBJEsn" runat="server"></asp:Label></strong>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEagency" runat="server" MaxLength="30" Visible="False" Width="160px" Height="16px"></asp:TextBox>
                                       
                                            </td>
                                    </tr>

                              
                                    <tr>
                                        <td class="panel-style1">1.Tug- 2.Tug :</td>
                                        <td style="width:160px;">
                                            <asp:DropDownList ID="DDLJEap" runat="server" Width="74px" OnSelectedIndexChanged="DDLJEap_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
                                            <asp:DropDownList ID="DDLJEdp" runat="server" Width="74px" ></asp:DropDownList>
                                        </td>
                                    </tr>
                                                                       <tr>
                                        <td class="panel-style1">Dp.Work Hr :</td>
                                        <td>
                                            <asp:TextBox CssClass="textboxs" ID="TBJEetadt" runat="server" MaxLength="5" Visible="True" ClientIDMode="Static" Width="70px" Height="16px"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBJEetadt" CultureName="tr-TR" ID="MaskedEditExtender5" runat="server" ErrorTooltipEnabled="true" MaskType="Time" DisplayMoney="Left" AcceptNegative="Left" Mask="99:99" MessageValidatorTip="true"></asp:MaskedEditExtender>
                                            <asp:TextBox CssClass="textboxs" ID="TBJEgrt" runat="server" MaxLength="5" Visible="True" ClientIDMode="Static" Width="70px" Height="16px"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBJEgrt" CultureName="tr-TR" ID="MaskedEditExtender2" runat="server" ErrorTooltipEnabled="true" MaskType="Time" DisplayMoney="Left" AcceptNegative="Left" Mask="99:99" MessageValidatorTip="true"></asp:MaskedEditExtender>                                               
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="panel-style1">3.Tug- 4.Tug :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLJEdb" runat="server" Width="74px" OnSelectedIndexChanged="DDLJEdb_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:DropDownList ID="DDLJEflag" runat="server" Width="74px" ></asp:DropDownList>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td class="panel-style1">Ar.Work Hr :</td>
                                        <td>
                                            <asp:TextBox CssClass="textboxs" ID="TBJEarhr1" runat="server" MaxLength="5" Visible="True" ClientIDMode="Static" Width="70px" Height="16px"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBJEarhr1" CultureName="tr-TR" ID="MaskedEditExtender4" runat="server" ErrorTooltipEnabled="true" MaskType="Time" DisplayMoney="Left" AcceptNegative="Left" Mask="99:99" MessageValidatorTip="true"></asp:MaskedEditExtender>
                                            <asp:TextBox CssClass="textboxs" ID="TBJEarhr2" runat="server" MaxLength="5" Visible="True" ClientIDMode="Static" Width="70px" Height="16px"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBJEarhr2" CultureName="tr-TR" ID="MaskedEditExtender6" runat="server" ErrorTooltipEnabled="true" MaskType="Time" DisplayMoney="Left" AcceptNegative="Left" Mask="99:99" MessageValidatorTip="true"></asp:MaskedEditExtender>                                               
                                        </td>
                                    </tr>
                                    

 
                                        <tr>
                                        <td class="panel-style1">Extra Tug :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLJEextra" runat="server" Width="74px" ></asp:DropDownList>

                                            <asp:Label  ID="TBJEloa" runat="server" MaxLength="30" Visible="False" Width="160px"  Height="16px"></asp:Label>
                                        </td>
                                    </tr>
                                                                                 
                                    <tr>
                                        <td class="panel-style1">Moor.Boat :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLJEtip" runat="server" Width="74px"></asp:DropDownList>
                                        </td>
                                    </tr>         

                                                                       
                                    <tr>
                                        <td  colspan="2"  ><br/>Last Notes :</td>
                                         </tr> <tr>
                                        <td colspan="2" >
                                        <asp:Textbox ID="TBjurnot" Enabled="false"  runat="server"  CssClass="ilkharfbuyuk" TextMode="MultiLine" MaxLength="2500"  Visible="True" Height="120px" Width="260px" BackColor="#FFFFE3"  Rows="5" Wrap="true"></asp:Textbox></td>
                                    </tr>

                                      <tr>
                                        <td colspan="2" >
                                            <asp:TextBox ID="TBjurnoty"  placeholder="Write New Note..."  CssClass="ilkharfbuyuk" MaxLength="500" runat="server" Text="" Visible="True" Width="260px" BackColor="#FFFFE3"  Rows="2" Wrap="true" TextMode="MultiLine" Height="40px"></asp:TextBox></td>
                                    </tr>
     
                                    <tr>
                                        
                                        <td colspan="2"  class="paneltusnoborder"> </br>
                                            <asp:Button ID="Bacceptedjur"  OnClick="Bacceptedjur_Click"  runat="server" style="height:30px; Width:80px" Text="Save" BackColor="Green" BorderWidth="1"   CssClass="btn"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" />&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                 
                                            <asp:Button ID="Bclosedjur" runat="server" style="height:30px; Width:80px"  Text="Close" BackColor="Yellow" BorderWidth="1"  CssClass="btn"  />  
                                        </td>
                                    </tr>
                                </table>
                            </div>

            <br /><br /> 
        </asp:Panel>
     <asp:ModalPopupExtender ID="ModalPopupjurnot" runat="server" 
         TargetControlID="buttonjurnot" 
         PopupControlID="paneljurnal" 
         BackgroundCssClass="modalbodyarka" 
         CancelControlID="Bclosedjur" ></asp:ModalPopupExtender>
<%-- modal jurnal biter --%>





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
