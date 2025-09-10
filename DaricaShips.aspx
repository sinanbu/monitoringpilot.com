<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DaricaShips.aspx.cs" Inherits="DaricaShips" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot - Darıca Ships</title>

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

        .ilkharfbuyuk {
            text-transform: lowercase;
            direction: ltr;
            overflow: auto;
        }

        .panel-style1 {
            text-align: right;
            height: 20px;
            
        }

        .panel-style2 {
            text-align: right;
            height: 22px;
        }

        .panelpilotata {
            width: 430px;
            height: 330px;
            border: 1px groove #111;
            background-color: white;
            font-family: 'Trebuchet MS';
            text-align: left;
            font-size: 12px;
            color: black;
        }

        .panelisekle {
            width: 330px;
            height: 520px;
            border: 1px groove #111;
            background-color: white;
            font-family: 'Trebuchet MS';
            font-size: x-small;
            text-align: left;
            color: black;
        }

        .panelisedit {
            width: 330px;
            height: 480px;
            border: 1px groove #111;
            background-color: white;
            font-family: 'Trebuchet MS';
            font-size: x-small;
            text-align: left;
            color: black;
        }

        .panelmessage {
            width: 300px;
            height: 100px;
            border: 1px groove #111;
            background-color: white;
            margin: 15px;
            font-family: 'Trebuchet MS';
            font-size: x-small;
            text-align: center;
            color: black;
        }

        .modalbodyarka {
            background-color: #333;
            filter: alpha(opacity:70);
            opacity: 0.6;
            z-index: 10000;
        }
                .panelmessagejur {
            width: 300px;
            height: 250px;
            border: 1px groove #111;
            border-radius:4px;
            background-color: #00fffa;
            margin: 15px;
            font-family: 'Trebuchet MS';
            font-size:  12px;
            text-align: center;
            color: black;
        }

                
/* Tooltip container */
.tooltip {
  position: relative;
  display: inline-block;
  border-bottom: 1px dotted black; /* If you want dots under the hoverable text */
}

/* Tooltip text */
.tooltip .tooltiptext {
  visibility: hidden;
  width: 120px;
  background-color: black;
  color: #fff;
  text-align: center;
  padding: 5px 0;
  border-radius: 6px;
 
  /* Position the tooltip text - see examples below! */
  position: absolute;
  z-index: 1;
}

/* Show the tooltip text when you mouse over the tooltip container */
.tooltip:hover .tooltiptext {
  visibility: visible;
}

/*<div class="tooltip">Hover over me
  <span class="tooltiptext">Tooltip text</span>
</div>*/


.textboxs {

    -webkit-border-radius: 2px; 
    -moz-border-radius: 2px; 
    border-radius: 2px; 
    border: 1px solid #848484; 
    outline:0; 
    height:24px; 
    width: 150px; 
    font-size: 16px;

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

            ;}

    </script>

    <script type="text/javascript">
        function shouldCancelbackspace(e) {
            var key;
            if (e) {
                key = e.which ? e.which : e.keyCode;
                if (key == null || (key != 8 && key != 13)) { // return when the key is not backspace key.
                    return false;
                }
            } else {
                return false;
            }


            if (e.srcElement) { // in IE
                tag = e.srcElement.tagName.toUpperCase();
                type = e.srcElement.type;
                readOnly = e.srcElement.readOnly;
                if (type == null) { // Type is null means the mouse focus on a non-form field. disable backspace button
                    return true;
                } else {
                    type = e.srcElement.type.toUpperCase();
                }


            } else { // in FF
                tag = e.target.nodeName.toUpperCase();
                type = (e.target.type) ? e.target.type.toUpperCase() : "";
            }


            // we don't want to cancel the keypress (ever) if we are in an input/text area
            if (tag == 'INPUT' || type == 'TEXT' || type == 'TEXTAREA') {
                if (readOnly == true) // if the field has been dsabled, disbale the back space button
                    return true;
                if (((tag == 'INPUT' && type == 'RADIO') || (tag == 'INPUT' && type == 'CHECKBOX'))
                && (key == 8 || key == 13)) {
                    return true; // the mouse is on the radio button/checkbox, disbale the backspace button
                }
                return false;
            }


            // if we are not in one of the above things, then we want to cancel (true) if backspace
            return (key == 8 || key == 13);
        }


        // check the browser type
        function whichBrs() {
            var agt = navigator.userAgent.toLowerCase();
            if (agt.indexOf("opera") != -1) return 'Opera';
            if (agt.indexOf("staroffice") != -1) return 'Star Office';
            if (agt.indexOf("webtv") != -1) return 'WebTV';
            if (agt.indexOf("beonex") != -1) return 'Beonex';
            if (agt.indexOf("chimera") != -1) return 'Chimera';
            if (agt.indexOf("netpositive") != -1) return 'NetPositive';
            if (agt.indexOf("phoenix") != -1) return 'Phoenix';
            if (agt.indexOf("firefox") != -1) return 'Firefox';
            if (agt.indexOf("safari") != -1) return 'Safari';
            if (agt.indexOf("skipstone") != -1) return 'SkipStone';
            if (agt.indexOf("msie") != -1) return 'Internet Explorer';
            if (agt.indexOf("netscape") != -1) return 'Netscape';
            if (agt.indexOf("mozilla/5.0") != -1) return 'Mozilla';


            if (agt.indexOf('\/') != -1) {
                if (agt.substr(0, agt.indexOf('\/')) != 'mozilla') {
                    return navigator.userAgent.substr(0, agt.indexOf('\/'));
                } else
                    return 'Netscape';
            } else if (agt.indexOf(' ') != -1)
                return navigator.userAgent.substr(0, agt.indexOf(' '));
            else
                return navigator.userAgent;
        }


        // Global events (every key press)


        var browser = whichBrs();
        if (browser == 'Internet Explorer') {
            document.onkeydown = function () { return !shouldCancelbackspace(event); }
        } else if (browser == 'Firefox') {
            document.onkeypress = function (e) { return !shouldCancelbackspace(e); }
        }


        window.onload = SetScroll;

        function SetScroll() {
            var objDiv = document.getElementById("<%=panel2.ClientID%>");
            objDiv.scrollTop = objDiv.scrollHeight;
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

                                <asp:Timer ID="gerisayimtik" runat="server" OnTick="gerisayimtik_Tick" Interval="120000"></asp:Timer>
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
            <asp:Panel ID="panel2" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>



                        <table style="width: 1340px; height: 25px; color: white;">
                            <tr>
                                <td style="text-align: left;">
                                    <button runat="server" id="Bmain" style="border: 1px solid black; cursor: pointer; background-color: #FFB08E; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='main.aspx'">Live Screen</button>&nbsp;
                                    <asp:Button ID="ButtonRefresh" Style="border: 1px solid black; cursor: pointer; background-color: white; height: 25px; Width: 90px; font-size: 11px; text-align: center !important;" runat="server" Text="Refresh" ToolTip="Refresh" OnClick="ButtonRefresh_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" />




                                </td>
                                <td style="text-align: right;">
                                    <asp:LinkButton ID="LBonline" runat="server" ForeColor="#111" OnClick="LBonline_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                      <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="#111" OnClick="LBonlineoff_Click" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                      <button runat="server" id="ButtonANJ" style="border: 1px solid black; cursor: pointer; background-color: #ee1111; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='newjob.aspx'">Add New Job</button>&nbsp;&nbsp;
                                    <button runat="server" id="Byarimcaships" style="border: 1px solid black; cursor: pointer; background-color: #9B86B5; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='yarimcaships.aspx'">Yarımca Ships</button>
                                    &nbsp;&nbsp;&nbsp;</td>
                            </tr>
                        </table>


                        <br />

                        <div class="clear"></div>


                        <asp:Label  runat="server" CssClass="tablobaslik2" Text="INCOAMING VESSELS"></asp:Label>
                        <asp:Panel ID="panelcollapsein" Style="display: block; overflow: hidden;" runat="server">

                            <asp:Repeater ID="DLNewShipListc" runat="server" OnItemDataBound="DLNewShipListc_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="TDLNewShipListc" class="tablo2">
                                        <tr class="trbaslik2">
                                            <td class="tdis1">No</td>
                                            <td class="tdis2">Ship Name</td>
                                            <td class="tdis16" style="width:380px;">Berth Order</td>
                                            <td class="tdis4">Destination Place</td>
                                            <td class="tdis5">Anc</td>
                                            <td class="tdis6">Flag</td>
                                            <td class="tdis7">Typ</td>
                                            <td class="tdis8">Grt</td>
                                            <td class="tdis9">Agency</td>
                                            <td class="tdis10">Bow-St</td>
                                            <td class="tdis11">Loa</td>
                                            <td class="tdis12">DC</td>
                                            <td class="tdis13">Tug</td>
                                            <td class="tdis14">TPP</td>
                                            <td class="tdis15">Eta.Etd</td>
                                            <td class="tdis17">Req.No</td>
                                            <td class="tdis18">Process</td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <tr id="Row" runat="server" class="trsatir2 rowflash">
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec" runat="server" Font-Bold="true" ForeColor="Red" Style="text-decoration: none;" Text='<%# Container.ItemIndex +1 %>'></asp:Label>
                                            <asp:LinkButton ID="LBpatashiphisc" runat="server" Style="cursor: pointer; text-decoration: none;" Font-Bold="true" Visible="false"> <%#Eval("id")%> </asp:LinkButton></td>
                                        <td class="tdis2 "  id="warngemitd">
                                            <asp:Button ID="LBpata" OnClick="LBpata_Click"  runat="server" CssClass="butongemi2" Text='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' ></asp:Button>
                                            <asp:Label ID="Lblnedurumda" runat="server" Visible="false" Text='<%#Eval("nedurumda")%>'></asp:Label>
                                        </td>

                                        <td class ="tdis16 " style="width:380px;" id="warntd">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>58? (Eval("notlar") as string).Substring(0,59)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label></td>
                                        <td class="tdis4 ">
                                            <asp:Label ID="Lbl5c" runat="server" Text='<%#Eval("yanasmalimani") %>'></asp:Label><asp:Label ID="Lbllyb" runat="server" Text='<%#Eval("lybyol") %>' Visible="false"></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td> 
                                        <td class="tdis6 ">
                                            <asp:Label ID="Lbl11" runat="server" Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:Label></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>'  ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><asp:Label ID="Lblgrt" runat="server" Text='<%#Eval("grt") %>'  Font-Bold='<%#Convert.ToInt32(Eval("grt"))>5000? true : false%>'></asp:Label></td>
                                        <td class="tdis9 ">
                                            <asp:Label ID="acentefaturac" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label> </td>
                                        <td class="tdis10 "> <asp:Label ID="Lbl14d" runat="server" Text='<%#Eval("bowt")%>'></asp:Label>-<asp:Label ID="Lbl14e" runat="server" Text='<%#Eval("strnt")%>'></asp:Label><asp:Label ID="Lbl14f" runat="server" Visible="false" Text='<%#Eval("bowok")%>'></asp:Label></td>
                                        <td class="tdis11 ">
                                            <asp:Label ID="Lbl14c" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12 ">
                                            <asp:Label ID="Lbl15c" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label> <asp:Label ID="Lbl15d" runat="server" Visible="false" Text='<%#Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label> <asp:Label ID="Lbl4hour"    runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label><asp:Label ID="Lbl4full" Visible="false" runat="server" Text='<%#Eval("eta")%>'></asp:Label></td>
                                        <td id="tdlcb" class="tdis17 ">
                                            <asp:Button ID="Lbl20" runat="server" CssClass="butongemi2"  CommandName='<%#Eval("gemiadi")%>' Font-Underline="false"  OnClick="Lbl20_Click"  CommandArgument='<%#Eval("id")%>' Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano") + " / LÇB :" + Eval("lcbno") + " , " + Eval("lcbdest")+ " , " + Eval("lcbdate") %>'></asp:Button><asp:Label ID="Labellcbdate" Visible="false" runat="server" Text='<%#Eval("lcbdate")%>'></asp:Label></td>
                                        <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonJobEditc" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Width="16px" OnClick="ImageButtonJobEdit_Click" />
                                              <asp:Label Font-Size="8px" ID="Lblkaydeden" runat="server" Text='<%#Eval("kaydeden")%>'></asp:Label> </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
                        <p>&nbsp;</p>

                        <asp:Label  runat="server" CssClass="tablobaslik2" Text="CONTACTED VESSELS"></asp:Label>
                        <asp:Panel ID="panelcollapsevipC" Style="display: block; overflow: hidden;" runat="server">
                            <asp:Repeater ID="DLNewShipListvipC" runat="server" OnItemDataBound="DLNewShipListvipC_ItemDataBound" >
                                <HeaderTemplate>
                                    <table class="tablo2a">
                                        <tr class="trbaslik2">
                                            <td class="tdis1">No</td>
                                            <td class="tdis2">Ship Name</td>
                                            <td class="tdis16">Note</td>

                                            <td class="tdis3">Departure Place</td>
                                            <td class="tdis4">Destination Place</td>
                                            <td class="tdis5">Anc</td>
                                            <td class="tdis6">Flag</td>
                                            <td class="tdis7">Typ</td>
                                            <td class="tdis8">Grt</td>
                                            <td class="tdis9">Agency</td>
                                            <td class="tdis10">Bow-St</td>
                                            <td class="tdis11">Loa</td>
                                            <td class="tdis12">DC</td>
                                            <td class="tdis13">Tug</td>
                                            <td class="tdis14">TPP</td>
                                            <td class="tdis15">Eta.Etd</td>
                                            <td class="tdis17">Req.No</td>
                                            <td class="tdis18">Process</td>
                                        </tr>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <tr id="Row" runat="server" class="trsatir2 rowflash" ><%--style="background-color:#ffffd0"--%>
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec1" runat="server" Font-Bold="true" ForeColor="Red" Text='<%# Container.ItemIndex +1 %>'></asp:Label><asp:LinkButton ID="LBpatashiphis1c" runat="server" Style="cursor: pointer; text-decoration: none;" Font-Bold="true" Visible="false"> <%#Eval("id")%> </asp:LinkButton><asp:Label ID="LBindexns" runat="server" Text='<%# Container.ItemIndex %>' Visible="false"></asp:Label></td>
                                        <td class="tdis2 "   id="warngemitd">
                                            <asp:Button ID="LBpata" OnClick="LBpata_Click"  runat="server" CssClass="butongemi2" Text='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' ></asp:Button>
                                            <asp:Label ID="Lblnedurumdaopr" runat="server" Visible="false" Text='<%#Eval("nedurumdaopr")%>'></asp:Label>
                                            <asp:Label ID="Lblnedurumda" runat="server" Visible="false" Text='<%#Eval("nedurumda")%>'></asp:Label>
                                        </td>
                                        <td class="tdis16 " id="warntd">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>
                                        
                                        <td class="tdis3 ">
                                            <asp:Label ID="Lbl3" runat="server"><%#Eval("kalkislimani") %> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %> </asp:Label><asp:Label ID="Lblkalkislimanidemgiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                         <td class="tdis4 ">
                                            <asp:Label ID="Lbl5" runat="server" Text='<%#Eval("yanasmalimani") %>'></asp:Label><asp:Label ID="Lbllyb" runat="server" Text='<%#Eval("lybyol") %>' Visible="false"></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td> 
                                        <td class="tdis6 ">
                                            <asp:Label ID="Lbl11" runat="server" Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:Label></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><asp:Label ID="Lblgrt" runat="server" Text='<%#Eval("grt") %>'  Font-Bold='<%#Convert.ToInt32(Eval("grt"))>5000? true : false%>'></asp:Label></td>
                                        <td class="tdis9 ">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label> </td>
                                        <td class="tdis10 "> <asp:Label ID="Lbl14d" runat="server" Text='<%#Eval("bowt")%>'></asp:Label>-<asp:Label ID="Lbl14e" runat="server" Text='<%#Eval("strnt")%>'></asp:Label><asp:Label ID="Lbl14f" runat="server" Visible="false" Text='<%#Eval("bowok")%>'></asp:Label></td>
                                        <td class="tdis11 ">
                                            <asp:Label ID="Lbl14" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12 ">
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label> <asp:Label ID="Lbl15d" runat="server" Visible="false" Text='<%#Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label> <asp:Label ID="Lbl4hour"    runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4full" Visible="false" runat="server" Text='<%#Eval("eta")%>'></asp:Label>
                                            
                                        </td>

                                        <td id="tdlcb"  class="tdis17 ">
                                            <asp:Button ID="Lbl20" runat="server" CssClass="butongemi2"  CommandName='<%#Eval("gemiadi")%>' Font-Underline="false"  OnClick="Lbl20_Click"  CommandArgument='<%#Eval("id")%>' Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano") + " / LÇB :" + Eval("lcbno") + " , " + Eval("lcbdest")+ " , " + Eval("lcbdate") %>'></asp:Button><asp:Label ID="Labellcbdate" Visible="false" runat="server" Text='<%#Eval("lcbdate")%>'></asp:Label></td>
                                        <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonJobEdit1c" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Width="16px" OnClick="ImageButtonJobEdit_Click" />
                                            <asp:Label Font-Size="8px" ID="Lblkaydeden" runat="server" Text='<%#Eval("kaydeden")%>'></asp:Label>
                                        </td>

                                    </tr>

                                </ItemTemplate>

                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>


  



                        </asp:Panel>
                        <p>&nbsp;</p>

                        <asp:Label  runat="server" CssClass="tablobaslik2" Text="ESKİHİSAR ANCHORAGE"></asp:Label>
                        <asp:Panel ID="panelcollapsedar" Style="display: block; overflow: hidden;" runat="server">
                            <asp:Repeater ID="DLAnchoredShipList" runat="server" OnItemDataBound="DLAnchoredShipList_ItemDataBound">
                                <HeaderTemplate>
                                    <table class="tablo2">
                                        <tr class="trbaslik2">
                                            <td class="tdis1">No</td>
                                            <td class="tdis2">Ship Name</td>
                                            <td class="tdis16">Note</td>

                                            <td class="tdis3">Departure Place</td>
                                            <td class="tdis4">Destination Place</td>
                                            <td class="tdis5">Anc</td>
                                            <td class="tdis6">Flag</td>
                                            <td class="tdis7">Typ</td>
                                            <td class="tdis8">Grt</td>
                                            <td class="tdis9">Agency</td>
                                            <td class="tdis10">Bow-St</td>
                                            <td class="tdis11">Loa</td>
                                            <td class="tdis12">DC</td>
                                            <td class="tdis13">Tug</td>
                                            <td class="tdis14">TPP</td>
                                            <td class="tdis15">Eta.Etd</td>
                                            <td class="tdis17">Req.No</td>
                                            <td class="tdis18">Process</td>
                                        </tr>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <tr id="RowDLAnchoredShipList" runat="server" class="trsatir2 rowflash">
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec2" runat="server" Font-Bold="true" ForeColor="Red" Text='<%# Container.ItemIndex +1 %>'></asp:Label><asp:LinkButton ID="LBpatashiphis2" runat="server" Font-Bold="true" Style="cursor: pointer; text-decoration: none;" Visible="false"> <%#Eval("id")%> </asp:LinkButton></td>
                                       <td class="tdis2 "  id="warngemitd">
                                            <asp:Button ID="LBpata" OnClick="LBpata_Click" runat="server"  CssClass="butongemi2" Text='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' ></asp:Button></td>
                                        <td class="tdis16 " id="warntd">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>
                                        
                                        <td class="tdis3 ">
                                            <asp:Label ID="Lbl3" runat="server"><%#Eval("kalkislimani") %> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %> </asp:Label><asp:Label ID="Lblkalkislimanidemgiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                        <td class="tdis4 ">
                                            <asp:Label ID="Lbl5" runat="server" Text='<%#Eval("yanasmalimani") %>'></asp:Label><asp:Label ID="Lbllyb" runat="server" Text='<%#Eval("lybyol") %>' Visible="false"></asp:Label><asp:Label ID="Lbl5rih" runat="server"><%#Eval("yanasmarihtimi") as string == "0" || Eval("yanasmarihtimi") as string == ""  ? "" : "/" + Eval("yanasmarihtimi")   %></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td>                                         
                                        <td class="tdis6 ">
                                            <asp:Label ID="Lbl11" runat="server" Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:Label></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><asp:Label ID="Lblgrt" runat="server" Text='<%#Eval("grt") %>'  Font-Bold='<%#Convert.ToInt32(Eval("grt"))>5000? true : false%>'></asp:Label></td>
                                        <td class="tdis9 ">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label></td>
                                        <td class="tdis10 "> <asp:Label ID="Lbl14d" runat="server" Text='<%#Eval("bowt")%>'></asp:Label>-<asp:Label ID="Lbl14e" runat="server" Text='<%#Eval("strnt")%>'></asp:Label><asp:Label ID="Lbl14f" runat="server" Visible="false" Text='<%#Eval("bowok")%>'></asp:Label></td>
                                        <td class="tdis11 ">
                                            <asp:Label ID="Lbl14" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12 ">
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label> <asp:Label ID="Lbl15d" runat="server" Visible="false" Text='<%#Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label> <asp:Label ID="Lbl4hour"    runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4full" Visible="false" runat="server" Text='<%#Eval("eta")%>'></asp:Label>
                                        </td>
                                        <td  id="tdlcb" class="tdis17 ">
                                            <asp:Button ID="Lbl20" runat="server" CssClass="butongemi2"  CommandName='<%#Eval("gemiadi")%>' Font-Underline="false"  OnClick="Lbl20_Click"  CommandArgument='<%#Eval("id")%>' Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano") + " / LÇB :" + Eval("lcbno") + " , " + Eval("lcbdest")+ " , " + Eval("lcbdate") %>'></asp:Button><asp:Label ID="Labellcbdate" Visible="false" runat="server" Text='<%#Eval("lcbdate")%>'></asp:Label></td>
                                        <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonJobEdit2" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Width="16px" OnClick="ImageButtonJobEdit_Click" />
                                           <asp:Label Font-Size="8px" ID="Lblkaydeden" runat="server" Text='<%#Eval("kaydeden")%>'></asp:Label>
                                        </td>

                                    </tr>



                                </ItemTemplate>
                                <FooterTemplate></table></FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
                        <p>&nbsp;</p>

                        <asp:Label  runat="server" CssClass="tablobaslik2" Text="DİLOVASI PORTS"></asp:Label>
                        <asp:Panel ID="panelcollapsedil" Style="display: block; overflow: hidden;" runat="server">
                            <asp:Repeater ID="DLVIPort" runat="server" OnItemDataBound="DLVIPort_ItemDataBound">
                                <HeaderTemplate>
                                    <table class="tablo2">
                                        <tr class="trbaslik2">
                                            <td class="tdis1">No</td>
                                            <td class="tdis2">Ship Name</td>
                                            <td class="tdis16">Note</td>
                                            <td class="tdis3">Departure Place</td>
                                            <td class="tdis4">Destination Place</td>
                                            <td class="tdis5">Anc</td>
                                            <td class="tdis6">Flag</td>
                                            <td class="tdis7">Typ</td>
                                            <td class="tdis8">Grt</td>
                                            <td class="tdis9">Agency</td>
                                            <td class="tdis10">Bow-St</td>
                                            <td class="tdis11">Loa</td>
                                            <td class="tdis12">DC</td>
                                            <td class="tdis13">Tug</td>
                                            <td class="tdis14">TPP</td>
                                            <td class="tdis15">Eta.Etd</td>
                                            <td class="tdis17">Req.No</td>
                                            <td class="tdis18">Process</td>

                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="RowDLVIPort" runat="server" class="trsatir2 rowflash">
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec3" runat="server" ForeColor="Red" Font-Bold="true" Text='<%# Container.ItemIndex +1 %>'></asp:Label><asp:LinkButton ID="LBpatashiphis3" runat="server" Style="cursor: pointer; text-decoration: none;" Font-Bold="true" Visible="false"> <%#Eval("id")%> </asp:LinkButton></td>
                                        <td class="tdis2 "  id="warngemitd">
                                            <asp:Button ID="LBpata" OnClick="LBpata_Click" runat="server"  CssClass="butongemi2" Text='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' ></asp:Button></td>
                                        <td class="tdis16 " id="warntd">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>

                                        <td class="tdis3 ">
                                            <asp:Label ID="Lbl3" runat="server"><%#Eval("kalkislimani") %> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %> </asp:Label><asp:Label ID="Lblkalkislimanigiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                        <td class="tdis4 ">
                                            <asp:Label ID="Lbl5" runat="server" Text='<%#Eval("yanasmalimani") %>'></asp:Label><asp:Label ID="Lbllyb" runat="server" Text='<%#Eval("lybyol") %>' Visible="false"></asp:Label><asp:Label ID="Lbl5rih" runat="server"> <%#Eval("yanasmarihtimi") as string == "0" || Eval("yanasmarihtimi") as string == ""  ? "" : "/" + Eval("yanasmarihtimi")   %></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td> 
                                        <td class="tdis6 ">
                                            <asp:Label ID="Lbl11" runat="server" Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:Label></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><asp:Label ID="Lblgrt" runat="server" Text='<%#Eval("grt") %>'  Font-Bold='<%#Convert.ToInt32(Eval("grt"))>5000? true : false%>'></asp:Label></td>
                                        <td class="tdis9 ">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label> </td>
                                        <td class="tdis10 "> <asp:Label ID="Lbl14d" runat="server" Text='<%#Eval("bowt")%>'></asp:Label>-<asp:Label ID="Lbl14e" runat="server" Text='<%#Eval("strnt")%>'></asp:Label><asp:Label ID="Lbl14f" runat="server" Visible="false" Text='<%#Eval("bowok")%>'></asp:Label></td>
                                        <td class="tdis11 ">
                                            <asp:Label ID="Lbl14" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12 ">
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label> <asp:Label ID="Lbl15d" runat="server" Visible="false" Text='<%#Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label> <asp:Label ID="Lbl4hour"    runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4full" Visible="false" runat="server" Text='<%#Eval("eta")%>'></asp:Label>
                                        </td>
                                        <td  id="tdlcb" class="tdis17 ">
                                               <asp:Button ID="Lbl20" runat="server" CssClass="butongemi2"  CommandName='<%#Eval("gemiadi")%>' Font-Underline="false"  OnClick="Lbl20_Click"  CommandArgument='<%#Eval("id")%>' Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano") + " / LÇB :" + Eval("lcbno") + " , " + Eval("lcbdest")+ " , " + Eval("lcbdate") %>'></asp:Button><asp:Label ID="Labellcbdate" Visible="false" runat="server" Text='<%#Eval("lcbdate")%>'></asp:Label></td>
                                        <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonJobEdit3" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Width="16px" OnClick="ImageButtonJobEdit_Click" />
                                            <asp:Label Font-Size="8px" ID="Lblkaydeden" runat="server" Text='<%#Eval("kaydeden")%>'></asp:Label>
                                        </td>

                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></table></FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
                        <p>&nbsp;</p>

                        <asp:Label  runat="server" CssClass="tablobaslik2" Text="HEREKE PORTS"></asp:Label>
                        <asp:Panel ID="panelcollapseher" Style="display: block; overflow: hidden;" runat="server">
                            <asp:Repeater ID="DLher" runat="server" OnItemDataBound="DLher_ItemDataBound">
                                <HeaderTemplate>
                                    <table class="tablo2">
                                        <tr class="trbaslik2">
                                            <td class="tdis1">No</td>
                                            <td class="tdis2">Ship Name</td>
                                            <td class="tdis16">Note</td>
                                            
                                            <td class="tdis3">Departure Place</td>
                                            <td class="tdis4">Destination Place</td>
                                            <td class="tdis5">Anc</td>                                            
                                            <td class="tdis6">Flag</td>
                                            <td class="tdis7">Typ</td>
                                            <td class="tdis8">Grt</td>
                                            <td class="tdis9">Agency</td>
                                            <td class="tdis10">Bow-St</td>
                                            <td class="tdis11">Loa</td>
                                            <td class="tdis12">DC</td>
                                            <td class="tdis13">Tug</td>
                                            <td class="tdis14">TPP</td>
                                            <td class="tdis15">Eta.Etd</td>
                                            <td class="tdis17">Req.No</td>
                                            <td class="tdis18">Process</td>

                                        </tr>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <tr id="RowDLher" runat="server" class="trsatir2 rowflash">
                                        <td class="tdis1">
                                            <asp:Label ID="LBshipgec8" runat="server" ForeColor="Red" Font-Bold="true" Text='<%# Container.ItemIndex +1 %>'></asp:Label><asp:LinkButton ID="LBpatashiphis8" runat="server" Style="cursor: pointer; text-decoration: none;" Font-Bold="true" Visible="false"> <%#Eval("id")%> </asp:LinkButton></td>
                                        <td class="tdis2 "  id="warngemitd">
                                            <asp:Button ID="LBpata" OnClick="LBpata_Click" runat="server" CssClass="butongemi2" Text='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' ></asp:Button></td>
                                        <td class="tdis16" id="warntd">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>
                                        
                                        <td class="tdis3">
                                            <asp:Label ID="Lbl3" runat="server"><%#Eval("kalkislimani") %> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %> </asp:Label><asp:Label ID="Lblkalkislimanigiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                        <td class="tdis4">
                                            <asp:Label ID="Lbl5" runat="server" Text='<%#Eval("yanasmalimani") %>'></asp:Label><asp:Label ID="Lbllyb" runat="server" Text='<%#Eval("lybyol") %>' Visible="false"></asp:Label><asp:Label ID="Lbl5rih" runat="server"> <%#Eval("yanasmarihtimi") as string == "0" || Eval("yanasmarihtimi") as string == ""  ? "" : "/" + Eval("yanasmarihtimi")   %></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td>                                        
                                        <td class="tdis6">
                                            <asp:Label ID="Lbl11" runat="server" Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:Label></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><asp:Label ID="Lblgrt" runat="server" Text='<%#Eval("grt") %>'  Font-Bold='<%#Convert.ToInt32(Eval("grt"))>5000? true : false%>'></asp:Label></td>
                                        <td class="tdis9">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label> </td>
                                        <td class="tdis10 "> <asp:Label ID="Lbl14d" runat="server" Text='<%#Eval("bowt")%>'></asp:Label>-<asp:Label ID="Lbl14e" runat="server" Text='<%#Eval("strnt")%>'></asp:Label><asp:Label ID="Lbl14f" runat="server" Visible="false" Text='<%#Eval("bowok")%>'></asp:Label></td>
                                        <td class="tdis11">
                                            <asp:Label ID="Lbl14" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12">
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label> <asp:Label ID="Lbl15d" runat="server" Visible="false" Text='<%#Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis13"><%#Eval("draft")%></td>
                                        <td class="tdis14"><%#Eval("bilgi")%></td>
                                        <td class="tdis15">
                                            <asp:Label ID="Lbl4" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label> <asp:Label ID="Lbl4hour"    runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4full" Visible="false" runat="server" Text='<%#Eval("eta")%>'></asp:Label>
                                        </td>
                                        <td  id="tdlcb" class="tdis17">
                                               <asp:Button ID="Lbl20" runat="server" CssClass="butongemi2"  Font-Underline="false"  OnClick="Lbl20_Click" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano") + " / LÇB :" + Eval("lcbno") + " , " + Eval("lcbdest")+ " , " + Eval("lcbdate") %>'></asp:Button><asp:Label ID="Labellcbdate" Visible="false" runat="server" Text='<%#Eval("lcbdate")%>'></asp:Label></td>
                                        <td class="tdis18">
                                            <asp:ImageButton ID="ImageButtonJobEdit8" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Width="16px" OnClick="ImageButtonJobEdit_Click" />
                                           <asp:Label Font-Size="8px" ID="Lblkaydeden" runat="server" Text='<%#Eval("kaydeden")%>'></asp:Label>
                                        </td>

                                    </tr>


                                </ItemTemplate>
                                <FooterTemplate></table></FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
                        <p>&nbsp;</p>




                        <asp:Button ID="buttonshowpopuppata" runat="server" Style="display: none;" />
                        <asp:Panel ID="panelpilotata" Style="display: none;" CssClass="panelpilotata" runat="server">
                            <table class="panelpilotata">
                                <tr>
                                    <td colspan="2">
                                        <div style="border: 1px solid white; font-weight: bold; height: 40px; text-align: center">
                                            <br />
                                            <strong>PILOT NOMINATION</strong>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 50%">Ship Name : </td>
                                    <td>
                                        <div>
                                            <asp:Label ID="TBgemiadi" Width="150px" runat="server" Font-Size="16px" Font-Bold="true"  ForeColor="#ee1111"></asp:Label>
                                            <asp:Label ID="Lblgrt" runat="server" visible="false" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="text-align: right; width: 50%">Departure Place : </td>
                                    <td>
                                        <div>
                                            <asp:Label ID="Lblpatadep" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>


                                <tr>
                                    <td style="text-align: right; width: 50%">POB Time : </td>
                                    <td>
                                        <asp:TextBox ID="TBetadatetime" runat="server" Width="150px"></asp:TextBox><asp:MaskedEditExtender CultureName="tr-TR" ID="MaskedEditExtender1" runat="server" TargetControlID="TBetadatetime" ErrorTooltipEnabled="true" MaskType="DateTime" DisplayMoney="Left" AcceptNegative="Left" Mask="99/99/9999 99:99"></asp:MaskedEditExtender>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="text-align: right; width: 50%">Destination Place : </td>
                                    <td>
                                        <asp:DropDownList ID="DDLdesplace" runat="server" Height="21px" Width="154px" OnSelectedIndexChanged="DDLdesplace_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:DropDownList ID="DDLdesplaceno" runat="server" Height="21px" Width="100px" OnSelectedIndexChanged="DDLdesplaceno_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 50%">Nominated Pilot Name : </td>
                                    <td>
                                        <asp:DropDownList ID="DDLnompilot" runat="server" Height="21px" Width="154px" OnSelectedIndexChanged="DDLnompilot_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                                             <tr>
                                    <td style="text-align: right; width: 50%">Thrusters Active? </td>
                                    <td>
                                            <asp:DropDownList ID="DDLbowok" runat="server" Width="50px" Height="19px">
                                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                            </asp:DropDownList>
                                    </td>
                                </tr>

                                 <tr>
                                    <td style="text-align: right; width: 50%">Cancel POB : </td>
                                    <td><asp:CheckBox ID="CBdeletepob" Checked="false" OnCheckedChanged="CBdeletepob_CheckedChanged" AutoPostBack="true"  runat="server" /> 
                                    </td>
                                </tr>
                                <tr>
                                    <td class="panel-style1"></td>
                                    <td>
                                        <asp:Label ID="patauyari" runat="server" ForeColor="Red" Font-Bold="true" Text=""></asp:Label></td>
                                </tr>

                                <tr>
                                    <td colspan="2" style="height: 50px; text-align: center; font-size: 20px; font-weight: bold; font-family: 'Trebuchet MS'">

                                        <asp:Button ID="Buttonpilotata" runat="server" Style="height: 30px; Width: 80px;" Text="Nominate" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" OnClick="Buttonpilotata_Click" />&nbsp;&nbsp; 
                       <asp:Button ID="Buttonpilotatacancel" runat="server" Style="height: 30px; Width: 80px" Text="Cancel" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:ModalPopupExtender
                            ID="ModalPopupExtenderpilotata" runat="server"
                            TargetControlID="buttonshowpopuppata"
                            PopupControlID="panelpilotata"
                            BackgroundCssClass="modalbodyarka"
                            CancelControlID="Buttonpilotatacancel">
                        </asp:ModalPopupExtender>



                        <asp:Button ID="buttonshowpopuppatac" runat="server" Style="display: none;" />
                        <asp:Panel ID="panelpilotatac" Style="display: none;" CssClass="panelpilotata" runat="server">
                            <table class="panelpilotata">
                                <tr>
                                    <td colspan="2">
                                        <div style="border: 1px solid white; font-weight: bold; height: 30px; text-align: center">
                                            <br />
                                            <strong>CONTACTED VESSEL</strong>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 50%">Ship Name : </td>
                                    <td>
                                        <div>
                                            <asp:Label ID="TBgemiadic" Width="150px" runat="server"  Font-Size="16px" Font-Bold="true"  ForeColor="#ee1111"></asp:Label>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="text-align: right; width: 50%">Estimated POB Time : </td>
                                    <td>
                                        <asp:TextBox ID="TBetadatetimec" runat="server" Width="150px"></asp:TextBox><asp:MaskedEditExtender CultureName="tr-TR" ID="MaskedEditExtender3" runat="server" TargetControlID="TBetadatetimec" ErrorTooltipEnabled="true" MaskType="DateTime" DisplayMoney="Left" AcceptNegative="Left" Mask="99/99/9999 99:99"></asp:MaskedEditExtender>
                                    </td>
                                </tr>

    

                                <tr>
                                    <td class="panel-style1"></td>
                                    <td>
                                        <asp:Label ID="patauyaric" runat="server" ForeColor="Red" Font-Bold="true" Text=""></asp:Label>
                                        <asp:Label ID="Lblpatailkkayit" Visible="false" runat="server" Text='<%#Eval("kayitzamani")%>'></asp:Label>
                                    </td>
                                </tr>


                                <tr>
                                    <td style="text-align: right; width: 50%">Cancel Contact : </td>
                                    <td>&nbsp;&nbsp;
                                  <asp:CheckBox ID="CBdeleteis" Checked="false" AutoPostBack="true" OnCheckedChanged="CBdeleteis_CheckedChanged" runat="server" /> 
                                    </td>
                                </tr>



                                <tr>
                                    <td colspan="2" style="height: 60px; text-align: center; font-size: 20px; font-weight: bold; font-family: 'Trebuchet MS'">
                       <asp:Button ID="Buttonpilotatac" runat="server" Style="height: 30px; Width: 120px;" Text="Make Contacted" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" OnClick="Buttonpilotatac_Click" />&nbsp;&nbsp; 
                       <asp:Button ID="Buttonpilotatacancelc" runat="server" Style="height: 30px; Width: 80px" Text="Cancel" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:ModalPopupExtender ID="ModalPopupExtenderpilotatac" runat="server"
                            TargetControlID="buttonshowpopuppatac"
                            PopupControlID="panelpilotatac"
                            BackgroundCssClass="modalbodyarka"
                            CancelControlID="Buttonpilotatacancelc">
                        </asp:ModalPopupExtender>


                        <asp:Button ID="buttonshowpopupje" runat="server" Style="display: none;" />
                        <asp:Panel ID="panelisedit" Style="display: none;" CssClass="panelisedit" runat="server">
                            <div style="margin-top: 10px; clear: both">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <div style="border: 1px solid white; font-size:12px; font-weight: bold; height: 30px; text-align: center"><strong><u>Editing Job Record</u></strong></div>
                                            <asp:Label Visible="false" ID="LJEid" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Ship Name/IMO :</td>
                                        <td  style="width:100px;">
                                              <asp:Label  ID="LBJEsn" Font-Size="Medium" ForeColor="Red" runat="server"></asp:Label> / <asp:Label Font-Size="Medium"  ID="LBJEimo" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                              
                                    <tr>
                                        <td class="panel-style1">Departure Port :</td>
                                        <td>
                                            
                                            <asp:Label  ID="LblDDLJEdepp" runat="server"></asp:Label> / <asp:Label  ID="LblJEdepb" runat="server"></asp:Label>

                                        </td>
                                    </tr>
                           
                                    <tr>
                                        <td class="panel-style1">Anchorage Place :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLJEap" runat="server" Width="200px"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Destination Port :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLJEdp" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="DDLJEdp_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Destination Berth :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLJEdb" runat="server" Width="100px"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Eta :</td>
                                        <td>
                                            <asp:TextBox CssClass="textboxs" ID="TBJEetadt" runat="server" MaxLength="16" Visible="True" ClientIDMode="Static" Width="150px" Height="16px"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBJEetadt" CultureName="tr-TR" ID="MaskedEditExtender5" runat="server" ErrorTooltipEnabled="true" MaskType="DateTime" DisplayMoney="Left" AcceptNegative="Left" Mask="99/99/9999 99:99"></asp:MaskedEditExtender>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Bow-Stern Thrustr :</td>
                                        <td>
                                            <asp:TextBox CssClass="textboxs" ID="TBJEbt" runat="server" MaxLength="4" Visible="True" Width="65px"  Height="16px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="TBJEbt" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                            -
                                            <asp:TextBox CssClass="textboxs" ID="TBJEst" runat="server" MaxLength="4" Visible="True" Width="65px" Height="16px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" TargetControlID="TBJEst" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                            Kw</td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Flag :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLJEflag" runat="server" Width="200px"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Grt :</td>
                                        <td>
                                         <asp:TextBox CssClass="textboxs" ID="TBJEgrt" runat="server" MaxLength="6" Visible="True" Width="65px"  Height="16px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="TBJEgrt" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;
                                       Loa :    <asp:TextBox CssClass="textboxs" ID="TBJEloa" runat="server" Visible="True" MaxLength="3" Width="65px" Height="16px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="TBJEloa" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                        m.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Type :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLJEtip" runat="server" Width="100px"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Tug :</td>
                                        <td>
                                            <asp:Label ID="LblJEdrft" runat="server" Text="" ></asp:Label>
                                               &nbsp;&nbsp; Tpp: <asp:Label ID="Labeltpp" runat="server" Text=""></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Dangerous&nbsp;Cargo :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLJEdc" runat="server" Width="50px" Height="19px">
                                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>


                                     <tr>
                                        <td class="panel-style1">Pratique :</td>
                                        <td>
                                           <asp:DropDownList ID="DDLJEpratika" runat="server" Width="50px" Height="19px"  OnSelectedIndexChanged="DDLJEpratika_SelectedIndexChanged"  AutoPostBack="true">
                                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem><asp:ListItem Value="1">Yes</asp:ListItem> </asp:DropDownList> 
                                                <asp:TextBox ID="TBJEpratikano" Width="147px" runat="server" MaxLength="16"></asp:TextBox>
            
                                            </td>
                                    </tr>

                                    <tr>
                                        <td class="panel-style1">Agency/Invoice :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEagency" runat="server" Text="" MaxLength="40" Visible="True" Width="97px" Height="16px"></asp:TextBox>&nbsp;<asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEinvoice" runat="server" Text="" MaxLength="20" Visible="True" Width="97px" Height="16px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Req. No :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEreqno" runat="server" Text="" MaxLength="20" Visible="True" Width="100px" Height="16px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Notes :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEnotes" runat="server" Text="" MaxLength="200" Visible="True" Width="200px"  Rows="2" Wrap="true" TextMode="MultiLine" Height="35px"></asp:TextBox></td>

                                    </tr>
<%-- buna java.min.js de bağlanırsa fu lar hemen aynı anda göstertilir                                       
      <tr>
                                        <td class="panel-style1">LYB Load :</td>
                                        <td><asp:FileUpload ID="fuResim" runat="server" ClientIDMode="Static" onchange="$('#resim')[0].src = window.URL.createObjectURL(this.files[0])"  CssClass="form-control" />
                                            </td>
                                    </tr>

                                      <tr>
                                        <td class="panel-style1">Loaded File :</td>
                                        <td><asp:Label  Visible="true" ID="Lbllyb" runat="server"></asp:Label> <br />
                                            <image id="resim" width="10px" height="15px" alt="Seçtiğiniz resim burada görünecek "  />
                                            </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="panel-style1">
                                            <asp:Label Visible="false" ID="Lblnedurumda" runat="server"></asp:Label>
                                            <asp:Label Visible="false" ID="Lblnedurumdaopr" runat="server"></asp:Label>
                                            <asp:Label ID="Lblkayitzamani" Visible="false" runat="server" Text='<%#Eval("kayitzamani")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Button ID="Buttonisedit" BackColor="Green" BorderWidth="0"  OnClick="Buttonisedit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" runat="server" Style="height: 30px; Width: 80px" Text="Save" CssClass="btn" />&nbsp;&nbsp;&nbsp;&nbsp; 
                                <asp:Button ID="Buttoniseditcancel" BackColor="Red" BorderWidth="0"  runat="server" Style="height: 30px; Width: 80px" Text="Cancel" CssClass="btn" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                                
                        </asp:Panel>
                        <asp:ModalPopupExtender ID="ModalPopupExtenderjobedit" runat="server"
                            CancelControlID="Buttoniseditcancel"
                            TargetControlID="buttonshowpopupje"
                            PopupControlID="panelisedit"
                            BackgroundCssClass="modalbodyarka">
                        </asp:ModalPopupExtender>

                  
                                                        
<%-- modal live lcb note başlar --%>
                     <asp:Button id="buttonlcbnot" runat="server" style="display:none;" />
                     <asp:Panel ID="panellcb"   CssClass="panelmessagejur" runat="server"   style="display: none; ">  
                            <div style="margin-top: 10px; clear: both; font-weight:bold;">
                                <br /> <asp:Label ID="LBbasliklcb" runat="server" Font-Bold="true" ForeColor="Aqua" Style="text-decoration:none;" Font-Size="16px" Width="250px"  Height="20px" BackColor="Navy" Text="L.Ç.B. LOG"></asp:Label> <br /><br />
                                <table style=" Font-Size:14px" >

<tr>
        <td class="panel-style1">Ship Name :</td>
<td class="panel-style1"><asp:Label  ID="shipnamelcb" Font-Bold="true" runat="server"></asp:Label></td>
</tr>

<tr>
<td class="panel-style1">LÇB No :</td>
<td><asp:TextBox CssClass="textboxs" ID="TBlcbno" runat="server"  MaxLength="16" Visible="True" ClientIDMode="Static" ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="TBlcbno" runat="server" ValidChars="0,1,2,3,4,5,6,7,8,9,0,.,-"></asp:FilteredTextBoxExtender>
        <asp:Label ID="TBlcbnoilk" runat="server" Visible="false" Text="" ></asp:Label>
</td>
</tr>

<tr>
<td class="panel-style1">&nbsp;&nbsp; Destination :</td>
<td><asp:TextBox CssClass="textboxs" ID="TBlcbdest" runat="server"  MaxLength="20" Visible="True" ></asp:TextBox></td>
</tr>

<tr>
<td class="panel-style1">ETS Date :</td>
<td><asp:TextBox CssClass="textboxs" ID="TBlcbdate" runat="server"  MaxLength="16" Visible="True" ClientIDMode="Static" ></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBlcbdate" CultureName="tr-TR" ID="MEE2" runat="server" ErrorTooltipEnabled="true" MaskType="DateTime" DisplayMoney="Left" AcceptNegative="Left" Mask="99/99/9999 99:99"></asp:MaskedEditExtender></td>
</tr>
</table><br />
                            </div>
            <asp:Button ID="Bacceptedlcb" runat="server" style="height:30px; Width:80px" Text="OK"   OnClick="Bacceptedlcb_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" />&nbsp;&nbsp; 
            <asp:Button ID="Bclosedlcb" runat="server" style="height:30px; Width:80px"  Text="Close"  />  
<br /><br />
        </asp:Panel>
                     <asp:ModalPopupExtender ID="ModalPopuplcbnot" runat="server" 
                         CancelControlID="Bclosedlcb" 
                         TargetControlID="buttonlcbnot" 
                         PopupControlID="panellcb" 
                         BackgroundCssClass="modalbodyarka" >
                     </asp:ModalPopupExtender>
<%-- modal lcb biter --%>



                        <div style="height: 150px; clear: both; float: left;">
                            <asp:Label runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </asp:Panel>
        </div>
    </form>
</body>
</html>
