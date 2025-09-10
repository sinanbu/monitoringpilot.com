<%@ Page Language="C#" AutoEventWireup="true" CodeFile="joblist.aspx.cs" Inherits="joblist" EnableEventValidation="false" MaintainScrollPositionOnPostback="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot - JOBS LIST</title>

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
            height: 350px;
            border: 1px groove #111;
            background-color: white;
            font-family: 'Trebuchet MS';
            text-align: left;
            font-size: x-small;
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
            height: 520px;
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
            background-color: #333333;
            filter: alpha(opacity:70);
            opacity: 0.6;
            z-index: 10000;
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

        <table style="width: 1340px; height: 25px; color: #111; font-weight: bold; background-image: url(images/boslukaltcizgi.png)">
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
                            <li style="color: #fff;">M E N U
                
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

              <div id="lybload" runat="server">
                    <asp:TextBox CssClass="ilkharfbuyuk" ID="TBgemilyb" runat="server" Text="" MaxLength="30" Visible="True" Width="160px" Height="20px"></asp:TextBox>&nbsp;&nbsp;
<asp:FileUpload ID="fuResim"  BackColor="#ffff88" ForeColor="Black" runat="server" BorderWidth="1px" BorderColor="DarkGreen" Height="20px"/>
<asp:Button ID="BlybEkle" runat="server" OnClick="BlybEkle_Click"  Text="LYB  YÜKLE"  style="height: 22px; color:firebrick" OnClientClick="SyncStatus(); return false;"/>&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="Blybsil" runat="server"  OnClick="Blybsil_Click"  Text="LYB  SİL"  style="height: 22px; color:firebrick" OnClientClick="SyncStatus(); return false;"/>
                  </div>

                      <div id="orload" runat="server" style="margin-top:10px; ">
                    <asp:TextBox CssClass="ilkharfbuyuk" ID="TBgemior" runat="server" Text="" MaxLength="30" Visible="True" Width="160px" Height="20px"></asp:TextBox>&nbsp;&nbsp;
<asp:FileUpload ID="fuResimor"  BackColor="#ffff88" ForeColor="Black" runat="server" BorderWidth="1px" BorderColor="DarkGreen" Height="20px"/>
<asp:Button ID="BorEkle" runat="server"  OnClick="BorEkle_Click"  Text="ORD YÜKLE"  style="height: 22px;  color:forestgreen" OnClientClick="SyncStatus(); return false;"/>&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="Borsil" runat="server"   OnClick="Borsil_Click"  Text="ORD SİL"  style="height: 22px; color:forestgreen;" OnClientClick="SyncStatus(); return false;"/>
                  </div>
               

        <div style="width: 100%; text-align: left;">
            <asp:Panel ID="panel2" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>



                        <table style="width: 1340px; height: 25px; color: white;">
                            <tr>
                                <td style="text-align: left;">
                                    <button runat="server" id="Bmain" style="border: 1px solid black; cursor: pointer; background-color: #FFB08E; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='main.aspx'">Live Screen</button>&nbsp;
                                    <button runat="server" id="Bdaricaships" style="border: 1px solid black; cursor: pointer; background-color: #7DA3CD; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='daricaships.aspx'">Darıca Ships</button>&nbsp;
<%--                                    <button runat="server" id="Byalovaships" style="border: 1px solid black; cursor: pointer; background-color: #9FF3A1; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='yalovaships.aspx'">Yalova Ships</button>&nbsp;--%>
                                    <button runat="server" id="Byarimcaships" style="border: 1px solid black; cursor: pointer; background-color: #9B86B5; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='yarimcaships.aspx'">Yarımca Ships</button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="text-align: right;">
                                    <asp:LinkButton ID="LBonline" runat="server" ForeColor="#111" OnClick="LBonline_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                      <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="#111" OnClick="LBonlineoff_Click" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                      <asp:Button ID="ButtonANJshowpopup" Style="border: 1px solid black; background-color: red; height: 25px; Width: 80px; font-size: 11px;" runat="server" Text="Add New Job" OnClick="ButtonANJshowpopup_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" />&nbsp;&nbsp;&nbsp;&nbsp; </td>
                            </tr>
                        </table>


                        <br />

                        <div class="clear"></div>

                        <asp:Label runat="server" ID="Label0" Style="cursor: default" CssClass="tablobaslik4" Text="INCOAMING VESSELS"></asp:Label>
                        <asp:Panel ID="panelcollapsein" Style="display: block; overflow: hidden;" runat="server">

                            <asp:Repeater ID="DLNewShipListc" runat="server" OnItemDataBound="DLNewShipListc_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="TDLNewShipListc" class="tablo4">
                                        <tr class="trbaslik4">
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

                                    <tr id="Row" runat="server" class="trsatir4 rowflash">
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec" runat="server" Font-Bold="true" ForeColor="Red" Style="text-decoration: none;" Text='<%# Container.ItemIndex +1 %>'></asp:Label></td>
                                        <td class="tdis2">
                                            <asp:LinkButton ID="LBLgemiadi1c" runat="server" Style="text-decoration: none;" ForeColor="#444444" Font-Bold="true" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' OnClick="LBLgemiadi1c_Click" Text='<%#Eval("gemiadi")%>'></asp:LinkButton></td>
                                        <td class="tdis16 ">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>

                                        <td class="tdis3 ">
                                            <asp:Label ID="Lbl11" runat="server"  Text='<%#Eval("kalkislimani") %>'></asp:Label><asp:Label ID="Label4" runat="server"> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %></asp:Label> <asp:Label ID="Lblkalkislimanigiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                        <td class="tdis4 ">
                                            <asp:LinkButton ID="Lbl5" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl5_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("yanasmalimani") %>'></asp:LinkButton><asp:Label ID="Lbl5rih" runat="server"> <%#Eval("yanasmarihtimi") as string == "0" || Eval("yanasmarihtimi") as string == ""  ? "" : "/" + Eval("yanasmarihtimi")   %></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td>
                                        <td class="tdis6 ">
                                            <asp:LinkButton ID="Lbl3" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl3_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:LinkButton></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><%#Eval("grt")%></td>
                                        <td class="tdis9 ">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label>
                                        </td>
                                        <td class="tdis10 "><%#Eval("bowt")%>-<%#Eval("strnt")%></td>
                                        <td class="tdis11 ">
                                            <asp:Label ID="Lbl14" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12 ">
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label></td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4c" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4hour" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label></td>
                                        <td class="tdis17 ">
                                            <asp:Label ID="Lbl20c" runat="server" Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonJobEdit1" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Width="16px" OnClick="ImageButtonJobEdit_Click" />
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

                        <asp:Label runat="server" ID="Label1" Style="cursor: default" CssClass="tablobaslik4" Text="CONTACTED VESSELS"></asp:Label>
                        <asp:Panel ID="panel3" Style="display: block; overflow: hidden;" runat="server">

                            <asp:Repeater ID="DLNewShipListvipC" runat="server" OnItemDataBound="DLNewShipListvipC_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="TDLNewShipListc" class="tablo4">
                                        <tr class="trbaslik4">
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

                                    <tr id="Row" runat="server" class="trsatir4 rowflash">
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec" runat="server" Font-Bold="true" ForeColor="Red" Style="text-decoration: none;" Text='<%# Container.ItemIndex +1 %>'></asp:Label></td>
                                        <td class="tdis2">
                                            <asp:LinkButton ID="LBLgemiadi1c" runat="server" Style="text-decoration: none;" ForeColor="#444444" Font-Bold="true" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' OnClick="LBLgemiadi1c_Click" Text='<%#Eval("gemiadi")%>'></asp:LinkButton></td>
                                        <td class="tdis16 ">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>

                                        <td class="tdis3 ">
                                            <asp:Label ID="Lbl11" runat="server"  Text='<%#Eval("kalkislimani") %>'></asp:Label><asp:Label ID="Label4" runat="server"> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %></asp:Label> <asp:Label ID="Lblkalkislimanigiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                        <td class="tdis4 ">
                                            <asp:LinkButton ID="Lbl5" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl5_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("yanasmalimani") %>'></asp:LinkButton><asp:Label ID="Lbl5rih" runat="server"> <%#Eval("yanasmarihtimi") as string == "0" || Eval("yanasmarihtimi") as string == ""  ? "" : "/" + Eval("yanasmarihtimi")   %></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td>
                                        <td class="tdis6 ">
                                            <asp:LinkButton ID="Lbl3" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl3_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:LinkButton></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><%#Eval("grt")%></td>
                                        <td class="tdis9 ">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label>
                                        </td>
                                        <td class="tdis10 "><%#Eval("bowt")%>-<%#Eval("strnt")%></td>
                                        <td class="tdis11 ">
                                            <asp:Label ID="Lbl14" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12 ">
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label></td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4c" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4hour" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label></td>
                                        <td class="tdis17 ">
                                            <asp:Label ID="Lbl20c" runat="server" Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonJobEdit1" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Width="16px" OnClick="ImageButtonJobEdit_Click" />
                                            <asp:Label ID="Lblkaydeden" Font-Size="8px"  runat="server" Text='<%#Eval("kaydeden")%>'></asp:Label>

                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
                        <p>&nbsp;</p>

                        <asp:Label runat="server" ID="Label9" Style="cursor: default" CssClass="tablobaslik4" Text="MANEUVERING VESSELS"></asp:Label>
                        <asp:Panel ID="panel11" Style="display: block; overflow: hidden;" runat="server">

                            <asp:Repeater ID="DLNewShipManeuvering" runat="server"  OnItemDataBound="DLNewShipManeuvering_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="TDLNewShipManeuvering" class="tablo4">
                                        <tr class="trbaslik4">
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

                                    <tr id="Row" runat="server" class="trsatir4 rowflash">
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec" runat="server" Font-Bold="true" ForeColor="Red" Style="text-decoration: none;" Text='<%# Container.ItemIndex +1 %>'></asp:Label></td>
                                        <td class="tdis2">
                                            <asp:LinkButton ID="LBLgemiadi1c" runat="server" Style="text-decoration: none;" ForeColor="#444444" Font-Bold="true" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' OnClick="LBLgemiadi1c_Click" Text='<%#Eval("gemiadi")%>'></asp:LinkButton></td>
                                        <td class="tdis16 ">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>

                                        <td class="tdis3 ">
                                            <asp:Label ID="Lbl11" runat="server"  Text='<%#Eval("kalkislimani") %>'></asp:Label><asp:Label ID="Label4" runat="server"> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %></asp:Label> <asp:Label ID="Lblkalkislimanigiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                        <td class="tdis4 ">
                                            <asp:LinkButton ID="Lbl5" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl5_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("yanasmalimani") %>'></asp:LinkButton><asp:Label ID="Lbl5rih" runat="server"> <%#Eval("yanasmarihtimi") as string == "0" || Eval("yanasmarihtimi") as string == ""  ? "" : "/" + Eval("yanasmarihtimi")   %></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td>
                                        <td class="tdis6 ">
                                            <asp:LinkButton ID="Lbl3" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl3_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:LinkButton></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><%#Eval("grt")%></td>
                                        <td class="tdis9 ">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label>
                                        </td>
                                        <td class="tdis10 "><%#Eval("bowt")%>-<%#Eval("strnt")%></td>
                                        <td class="tdis11 ">
                                            <asp:Label ID="Lbl14" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12 ">
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label></td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4c" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4hour" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label></td>
                                        <td class="tdis17 ">
                                            <asp:Label ID="Lbl20c" runat="server" Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonJobEdit1" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Visible="false" Width="16px" OnClick="ImageButtonJobEdit_Click" />
                                            <asp:Label ID="Lblkaydeden" Font-Size="8px"  runat="server" Text='<%#Eval("kaydeden")%>'></asp:Label>

                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
                        <p>&nbsp;</p>

                        <asp:Label runat="server" ID="Label2" Style="cursor: default" CssClass="tablobaslik4" Text="ESKİHİSAR-YALOVA ANCHORAGE"></asp:Label>
                        <asp:Panel ID="panel4" Style="display: block; overflow: hidden;" runat="server">

                            <asp:Repeater ID="DLAnchoredShipList" runat="server" OnItemDataBound="DLAnchoredShipList_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="TDLNewShipListc" class="tablo4">
                                        <tr class="trbaslik4">
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

                                    <tr id="Row" runat="server" class="trsatir4 rowflash">
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec" runat="server" Font-Bold="true" ForeColor="Red" Style="text-decoration: none;" Text='<%# Container.ItemIndex +1 %>'></asp:Label></td>
                                        <td class="tdis2">
                                            <asp:LinkButton ID="LBLgemiadi1c" runat="server" Style="text-decoration: none;" ForeColor="#444444" Font-Bold="true" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' OnClick="LBLgemiadi1c_Click" Text='<%#Eval("gemiadi")%>'></asp:LinkButton></td>
                                        <td class="tdis16 ">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>

                                        <td class="tdis3 ">
                                            <asp:Label ID="Lbl11" runat="server"  Text='<%#Eval("kalkislimani") %>'></asp:Label><asp:Label ID="Label4" runat="server"> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %></asp:Label> <asp:Label ID="Lblkalkislimanigiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                        <td class="tdis4 ">
                                            <asp:LinkButton ID="Lbl5" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl5_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("yanasmalimani") %>'></asp:LinkButton><asp:Label ID="Lbl5rih" runat="server"> <%#Eval("yanasmarihtimi") as string == "0" || Eval("yanasmarihtimi") as string == ""  ? "" : "/" + Eval("yanasmarihtimi")   %></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td>
                                        <td class="tdis6 ">
                                            <asp:LinkButton ID="Lbl3" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl3_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:LinkButton></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><%#Eval("grt")%></td>
                                        <td class="tdis9 ">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label>
                                        </td>
                                        <td class="tdis10 "><%#Eval("bowt")%>-<%#Eval("strnt")%></td>
                                        <td class="tdis11 ">
                                            <asp:Label ID="Lbl14" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12 ">
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label></td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4c" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4hour" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label></td>
                                        <td class="tdis17 ">
                                            <asp:Label ID="Lbl20c" runat="server" Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonJobEdit1" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Width="16px" OnClick="ImageButtonJobEdit_Click" />
                                            <asp:Label ID="Lblkaydeden"  Font-Size="8px" runat="server" Text='<%#Eval("kaydeden")%>'></asp:Label>                                        
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
                        <p>&nbsp;</p>

                        <asp:Label runat="server" ID="Label3" Style="cursor: default" CssClass="tablobaslik4" Text="DİLOVASI PORTS"></asp:Label>
                        <asp:Panel ID="panel5" Style="display: block; overflow: hidden;" runat="server">

                            <asp:Repeater ID="DLVIPort" runat="server" OnItemDataBound="DLVIPort_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="TDLNewShipListc" class="tablo4">
                                        <tr class="trbaslik4">
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

                                    <tr id="Row" runat="server" class="trsatir4 rowflash">
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec" runat="server" Font-Bold="true" ForeColor="Red" Style="text-decoration: none;" Text='<%# Container.ItemIndex +1 %>'></asp:Label></td>
                                        <td class="tdis2">
                                            <asp:LinkButton ID="LBLgemiadi1c" runat="server" Style="text-decoration: none;" ForeColor="#444444" Font-Bold="true" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' OnClick="LBLgemiadi1c_Click" Text='<%#Eval("gemiadi")%>'></asp:LinkButton></td>
                                        <td class="tdis16 ">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>

                                        <td class="tdis3 ">
                                            <asp:Label ID="Lbl11" runat="server"  Text='<%#Eval("kalkislimani") %>'></asp:Label><asp:Label ID="Label4" runat="server"> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %></asp:Label> <asp:Label ID="Lblkalkislimanigiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                        <td class="tdis4 ">
                                            <asp:LinkButton ID="Lbl5" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl5_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("yanasmalimani") %>'></asp:LinkButton><asp:Label ID="Lbl5rih" runat="server"> <%#Eval("yanasmarihtimi") as string == "0" || Eval("yanasmarihtimi") as string == ""  ? "" : "/" + Eval("yanasmarihtimi")   %></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td>
                                        <td class="tdis6 ">
                                            <asp:LinkButton ID="Lbl3" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl3_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:LinkButton></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><%#Eval("grt")%></td>
                                        <td class="tdis9 ">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label>
                                        </td>
                                        <td class="tdis10 "><%#Eval("bowt")%>-<%#Eval("strnt")%></td>
                                        <td class="tdis11 ">
                                            <asp:Label ID="Lbl14" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12 ">
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label></td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4c" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4hour" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label></td>
                                        <td class="tdis17 ">
                                            <asp:Label ID="Lbl20c" runat="server" Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonJobEdit1" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Width="16px" OnClick="ImageButtonJobEdit_Click" />
                                            <asp:Label ID="Lblkaydeden"  Font-Size="8px" runat="server" Text='<%#Eval("kaydeden")%>'></asp:Label>
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
                        <p>&nbsp;</p>



                        <asp:Label runat="server" ID="Label5" Style="cursor: default" CssClass="tablobaslik4" Text="HEREKE PORTS"></asp:Label>
                        <asp:Panel ID="panel7" Style="display: block; overflow: hidden;" runat="server">

                            <asp:Repeater ID="DLher" runat="server" OnItemDataBound="DLher_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="TDLNewShipListc" class="tablo4">
                                        <tr class="trbaslik4">
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

                                    <tr id="Row" runat="server" class="trsatir4 rowflash">
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec" runat="server" Font-Bold="true" ForeColor="Red" Style="text-decoration: none;" Text='<%# Container.ItemIndex +1 %>'></asp:Label></td>
                                        <td class="tdis2">
                                            <asp:LinkButton ID="LBLgemiadi1c" runat="server" Style="text-decoration: none;" ForeColor="#444444" Font-Bold="true" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' OnClick="LBLgemiadi1c_Click" Text='<%#Eval("gemiadi")%>'></asp:LinkButton></td>
                                        <td class="tdis16 ">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>

                                        <td class="tdis3 ">
                                            <asp:Label ID="Lbl11" runat="server"  Text='<%#Eval("kalkislimani") %>'></asp:Label><asp:Label ID="Label4" runat="server"> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %></asp:Label> <asp:Label ID="Lblkalkislimanigiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                        <td class="tdis4 ">
                                            <asp:LinkButton ID="Lbl5" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl5_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("yanasmalimani") %>'></asp:LinkButton><asp:Label ID="Lbl5rih" runat="server"> <%#Eval("yanasmarihtimi") as string == "0" || Eval("yanasmarihtimi") as string == ""  ? "" : "/" + Eval("yanasmarihtimi")   %></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td>
                                        <td class="tdis6 ">
                                            <asp:LinkButton ID="Lbl3" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl3_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:LinkButton></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><%#Eval("grt")%></td>
                                        <td class="tdis9 ">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label>
                                        </td>
                                        <td class="tdis10 "><%#Eval("bowt")%>-<%#Eval("strnt")%></td>
                                        <td class="tdis11 ">
                                            <asp:Label ID="Lbl14" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12 ">
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label></td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4c" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4hour" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label></td>
                                        <td class="tdis17 ">
                                            <asp:Label ID="Lbl20c" runat="server" Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonJobEdit1" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Width="16px" OnClick="ImageButtonJobEdit_Click" />
                                            <asp:Label ID="Lblkaydeden" Font-Size="8px"  runat="server" Text='<%#Eval("kaydeden")%>'></asp:Label>
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
                        <p>&nbsp;</p>

                        <asp:Label runat="server" ID="Label6" Style="cursor: default" CssClass="tablobaslik4" Text="YARIMCA-HEREKE-İZMİT ANCHORAGE"></asp:Label>
                        <asp:Panel ID="panel8" Style="display: block; overflow: hidden;" runat="server">

                            <asp:Repeater ID="DLyaranc" runat="server" OnItemDataBound="DLyaranc_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="TDLNewShipListc" class="tablo4">
                                        <tr class="trbaslik4">
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

                                    <tr id="Row" runat="server" class="trsatir4 rowflash">
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec" runat="server" Font-Bold="true" ForeColor="Red" Style="text-decoration: none;" Text='<%# Container.ItemIndex +1 %>'></asp:Label></td>
                                        <td class="tdis2">
                                            <asp:LinkButton ID="LBLgemiadi1c" runat="server" Style="text-decoration: none;" ForeColor="#444444" Font-Bold="true" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' OnClick="LBLgemiadi1c_Click" Text='<%#Eval("gemiadi")%>'></asp:LinkButton></td>
                                        <td class="tdis16 ">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>

                                        <td class="tdis3 ">


                                            <asp:Label ID="Lbl11" runat="server"  Text='<%#Eval("kalkislimani") %>'></asp:Label><asp:Label ID="Label4" runat="server"> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %></asp:Label> <asp:Label ID="Lblkalkislimanigiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                        <td class="tdis4 ">
                                            <asp:LinkButton ID="Lbl5" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl5_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("yanasmalimani") %>'></asp:LinkButton><asp:Label ID="Lbl5rih" runat="server"> <%#Eval("yanasmarihtimi") as string == "0" || Eval("yanasmarihtimi") as string == ""  ? "" : "/" + Eval("yanasmarihtimi")   %></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td>
                                        <td class="tdis6 ">


                                            <asp:LinkButton ID="Lbl3" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl3_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:LinkButton></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><%#Eval("grt")%></td>
                                        <td class="tdis9 ">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label>
                                        </td>
                                        <td class="tdis10 "><%#Eval("bowt")%>-<%#Eval("strnt")%></td>
                                        <td class="tdis11 ">
                                            <asp:Label ID="Lbl14" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12 ">
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label></td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4c" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4hour" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label></td>
                                        <td class="tdis17 ">
                                            <asp:Label ID="Lbl20c" runat="server" Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonJobEdit1" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Width="16px" OnClick="ImageButtonJobEdit_Click" />
                                            <asp:Label ID="Lblkaydeden" Font-Size="8px"  runat="server" Text='<%#Eval("kaydeden")%>'></asp:Label>
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
                        <p>&nbsp;</p>

                        <asp:Label runat="server" ID="Label7" Style="cursor: default" CssClass="tablobaslik4" Text="YARIMCA PORTS"></asp:Label>
                        <asp:Panel ID="panel9" Style="display: block; overflow: hidden;" runat="server">

                            <asp:Repeater ID="DLyarport" runat="server" OnItemDataBound="DLyarport_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="TDLNewShipListc" class="tablo4">
                                        <tr class="trbaslik4">
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

                                    <tr id="Row" runat="server" class="trsatir4 rowflash">
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec" runat="server" Font-Bold="true" ForeColor="Red" Style="text-decoration: none;" Text='<%# Container.ItemIndex +1 %>'></asp:Label></td>
                                        <td class="tdis2">
                                            <asp:LinkButton ID="LBLgemiadi1c" runat="server" Style="text-decoration: none;" ForeColor="#444444" Font-Bold="true" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' OnClick="LBLgemiadi1c_Click" Text='<%#Eval("gemiadi")%>'></asp:LinkButton></td>
                                        <td class="tdis16 ">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>

                                        <td class="tdis3 ">
                                            <asp:Label ID="Label11" runat="server"  Text='<%#Eval("kalkislimani") %>'></asp:Label><asp:Label ID="Label4" runat="server"> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %></asp:Label> <asp:Label ID="Lblkalkislimanigiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                        <td class="tdis4 ">
                                            <asp:LinkButton ID="Lbl5" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl5_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("yanasmalimani") %>'></asp:LinkButton><asp:Label ID="Lbl5rih" runat="server"> <%#Eval("yanasmarihtimi") as string == "0" || Eval("yanasmarihtimi") as string == ""  ? "" : "/" + Eval("yanasmarihtimi")   %></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td>
                                        <td class="tdis6 ">
                                            <asp:LinkButton ID="Lbl3" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl3_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:LinkButton></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><%#Eval("grt")%></td>
                                        <td class="tdis9 ">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label>
                                        </td>
                                        <td class="tdis10 "><%#Eval("bowt")%>-<%#Eval("strnt")%></td>
                                        <td class="tdis11 ">
                                            <asp:Label ID="Lbl14" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12 ">
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label></td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4c" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4hour" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label></td>
                                        <td class="tdis17 ">
                                            <asp:Label ID="Lbl20c" runat="server" Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonJobEdit1" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Width="16px" OnClick="ImageButtonJobEdit_Click" />
                                            <asp:Label ID="Lblkaydeden" Font-Size="8px"  runat="server" Text='<%#Eval("kaydeden")%>'></asp:Label>
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
                        <p>&nbsp;</p>

                        <asp:Label runat="server" ID="Label8" Style="cursor: default" CssClass="tablobaslik4" Text="KOSBAŞ SHIPYARD"></asp:Label>
                        <asp:Panel ID="panel10" Style="display: block; overflow: hidden;" runat="server">

                            <asp:Repeater ID="DLVIPortKosb" runat="server" OnItemDataBound="DLVIPortKosb_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="TDLNewShipListc" class="tablo4">
                                        <tr class="trbaslik4">
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

                                    <tr id="Row" runat="server" class="trsatir4 rowflash">
                                        <td class="tdis1 ">
                                            <asp:Label ID="LBshipgec" runat="server" Font-Bold="true" ForeColor="Red" Style="text-decoration: none;" Text='<%# Container.ItemIndex +1 %>'></asp:Label></td>
                                        <td class="tdis2">
                                            <asp:LinkButton ID="LBLgemiadi1c" runat="server" Style="text-decoration: none;" ForeColor="#444444" Font-Bold="true" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' OnClick="LBLgemiadi1c_Click" Text='<%#Eval("gemiadi")%>'></asp:LinkButton></td>
                                        <td class="tdis16 ">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>

                                        <td class="tdis3 ">
                                            <asp:Label ID="Label11" runat="server"  Text='<%#Eval("kalkislimani") %>'></asp:Label><asp:Label ID="Label4" runat="server"> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %></asp:Label> <asp:Label ID="Lblkalkislimanigiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                        <td class="tdis4 ">
                                            <asp:LinkButton ID="Lbl5" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl5_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("yanasmalimani") %>'></asp:LinkButton><asp:Label ID="Lbl5rih" runat="server"> <%#Eval("yanasmarihtimi") as string == "0" || Eval("yanasmarihtimi") as string == ""  ? "" : "/" + Eval("yanasmarihtimi")   %></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td>
                                        <td class="tdis6 ">
                                            <asp:LinkButton ID="Lbl3" runat="server" ForeColor="#444" Style="text-decoration: none;"  OnClick="Lbl3_Click" CommandArgument='<%#Eval("id")%>' Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:LinkButton></td>
                                        <td class="tdis7">
                                            <asp:Label ID="Lbl7" runat="server" Text='<%#Eval("tip").ToString().Length>2? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>' ToolTip='<%#Eval("tip") %>'></asp:Label></td>
                                        <td class="tdis8"><%#Eval("grt")%></td>
                                        <td class="tdis9 ">
                                            <asp:Label ID="acentefatura" runat="server" Text='<%#Eval("acente").ToString().Length>11? (Eval("acente") as string).Substring(0,12)+"." : Eval("acente")%>' ToolTip='<%#Eval("fatura") %>'></asp:Label>
                                        </td>
                                        <td class="tdis10 "><%#Eval("bowt")%>-<%#Eval("strnt")%></td>
                                        <td class="tdis11 ">
                                            <asp:Label ID="Lbl14" runat="server" Text='<%#Eval("loa")%>'></asp:Label></td>
                                        <td class="tdis12 ">
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label></td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4c" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4hour" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label></td>
                                        <td class="tdis17 ">
                                            <asp:Label ID="Lbl20c" runat="server" Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>'  ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano")%>'></asp:Label></td>
                                        <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonJobEdit1" CommandName='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' runat="server" ImageUrl="~/images/edit.png" Width="16px" OnClick="ImageButtonJobEdit_Click" />
                                            <asp:Label ID="Lblkaydeden"  Font-Size="8px" runat="server" Text='<%#Eval("kaydeden")%>'></asp:Label>
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>

                        </asp:Panel>
                        <p>&nbsp;</p>



                        <asp:Button ID="buttonshowpopupje" runat="server" Style="display: none;" />
                        <asp:Panel ID="panelisedit" Style="display: none;" CssClass="panelisedit" runat="server">
                            <div style="margin-top: 10px; clear: both">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <div style="border: 1px solid white; font-weight: bold; height: 30px; text-align: center"><strong>Editing Job Record</strong></div>
                                            <asp:Label Visible="false" ID="LJEid" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Ship IMO No :</td>
                                        <td>
                                            <asp:TextBox CssClass="textboxs" ID="TBJEimo" runat="server" Text="" Visible="True" Enabled="false" Width="100px" MaxLength="7"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="TBJEimo" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Ship Name :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEsn" runat="server" Visible="True" Width="200px" MaxLength="30"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Departure Port :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLJEdepp" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="DDLJEdepp_SelectedIndexChanged"></asp:DropDownList><asp:Label Visible="false" ID="LblDDLJEdepp" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Departure Berth :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLJEdepb" runat="server" Width="100px"></asp:DropDownList></td>

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
                                            <asp:TextBox CssClass="textboxs" ID="TBJEetadt" runat="server" MaxLength="16" Visible="True" ClientIDMode="Static" Width="150px"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBJEetadt" CultureName="tr-TR" ID="MaskedEditExtender5" runat="server" ErrorTooltipEnabled="true" MaskType="DateTime" DisplayMoney="Left" AcceptNegative="Left" Mask="99/99/9999 99:99"></asp:MaskedEditExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Bow-Stern Thrustr :</td>
                                        <td>
                                            <asp:TextBox CssClass="textboxs" ID="TBJEbt" runat="server" MaxLength="4" Visible="True" Width="65px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="TBJEbt" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                            -
                                            <asp:TextBox CssClass="textboxs" ID="TBJEst" runat="server" MaxLength="4" Visible="True" Width="65px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" TargetControlID="TBJEst" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
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
                                            <asp:TextBox CssClass="textboxs" ID="TBJEgrt" runat="server" MaxLength="6" Visible="True" Width="65px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="TBJEgrt" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;
                                        Loa :<asp:TextBox CssClass="textboxs" ID="TBJEloa" runat="server" Visible="True" MaxLength="3" Width="65px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="TBJEloa" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                            m.</td>
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
                                           
                                            &nbsp;&nbsp; Tpp:
                                            <asp:Label ID="Labeltpp" runat="server" Text=""></asp:Label></td>
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
                                        <td class="panel-style1">Agency :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEagency" runat="server" Text="" MaxLength="40" Visible="True" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Invoice :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEinvoice" runat="server" Text="" MaxLength="20" Visible="True" Width="200px"></asp:TextBox></td>
                                        <%--                        </tr ><tr ><td class="panel-style1">Infos :</td><td><asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEinfo" runat="server" Text=""  MaxLength="20" Visible="True" Width="200px"></asp:TextBox></td>--%>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Notes :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEnotes" runat="server" Text="" MaxLength="200" Visible="True" Width="200px" TextMode="MultiLine"></asp:TextBox></td>
                                        <asp:Label Visible="false" ID="TBJEnotesorg" runat="server"></asp:Label>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Req. No :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEreqno" runat="server" Text="" MaxLength="20" Visible="True" Width="100px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">
                                            <asp:Label Visible="false" ID="Lblnedurumda" runat="server"></asp:Label>
                                            <asp:Label Visible="false" ID="Lblnedurumdaopr" runat="server"></asp:Label>
                                            <asp:Label ID="Lblkayitzamani" Visible="false" runat="server" Text='<%#Eval("kayitzamani")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Button ID="Buttonisedit" OnClick="Buttonisedit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" runat="server" Style="height: 30px; Width: 80px" Text="Save" CssClass="btn" />&nbsp;&nbsp; 
                                <asp:Button ID="Buttoniseditcancel" runat="server" Style="height: 30px; Width: 80px" Text="Cancel" CssClass="btn" />
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


                        <div style="height: 150px; clear: both; float: left;">
                            <asp:Label runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label></div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </asp:Panel>
        </div>
    </form>
<%--    bu kod geri veya f5 tuşunda formun tekrar gönderilmemesi için--%>
    <script>
if ( window.history.replaceState ) {
  window.history.replaceState( null, null, window.location.href );
}
</script>
</body>
</html>
