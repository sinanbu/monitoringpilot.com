<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newjob.aspx.cs" Inherits="newjob" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot - New Job</title>

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
            height: 150px;
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
            }
            else {
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

<body onkeydown="return (event.keyCode!=8 || event.keyCode!=13 )">
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
                    <li id="mainmanu7" runat="server"><a href="mapizmitbay.html" target="_blank">Map</a></li>
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
                                    <button runat="server" id="Bdaricaships" style="border: 1px solid black;  cursor: pointer; background-color: #7DA3CD; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='daricaships.aspx'">Darıca Ships</button>&nbsp;
                                    <button runat="server" id="Byarimcaships" style="border: 1px solid black; cursor: pointer; background-color: #9B86B5; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='yarimcaships.aspx'">Yarımca Ships</button>



                                </td>
                                <td style="text-align: right;">
                                    <asp:LinkButton ID="LBonline" runat="server" ForeColor="#111" OnClick="LBonline_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                      <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="#111" OnClick="LBonlineoff_Click" runat="server"></asp:LinkButton>&nbsp;&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: center; color:red; font-size:15px;" colspan="2">
                <asp:Label ID="Lbltwice" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </td></tr>
                        </table>


                        <br />

                        <div class="clear"></div>
                        <asp:TextBox ID="TBimono" runat="server" MaxLength="7"  Placeholder="IMO No"  Width="90px" Height="22px"  AutoPostBack="true" OnTextChanged="TBimono_TextChanged"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" TargetControlID="TBimono" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>&nbsp;
                        <asp:TextBox ID="TBshipname" runat="server" MaxLength="30"  Placeholder="Ship Name"  Font-Size="Small" Width="185px" Height="22px" ></asp:TextBox>&nbsp;
                        <asp:Button ID="LBfindves" runat="server"  OnClick="LBfindves_Click" Text="Find Vessel" Width="90px" Height="24px"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" ></asp:Button>&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="ButtonANJshowpopup" runat="server" Style="background-color:#ffcccc; border:1px solid #111" Text="Add New Job" OnClick="ButtonANJshowpopup_Click"  Width="90px" Height="24px" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" ></asp:Button>

                        <p>&nbsp;</p> 

                        <asp:Label ID="LBnewjob" runat="server"  CssClass="tablobaslik4"   Text="LAST 20 RECORDS"></asp:Label>
                        <asp:Panel ID="panelcollapsein" Style="display: block; overflow: hidden;" runat="server">
                            <asp:Repeater ID="DLNewjob" runat="server" OnItemDataBound="DLNewjob_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="TDLNewjob" class="tablo4">
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
                                        <td class="tdis2 "  id="warngemitd">
                                                <asp:Button ID="Btngemiup"  BorderWidth="0"  runat="server"   OnClick="Btngemiup_Click" CssClass="butongemi3"  Text='<%#Eval("gemiadi")%>' CommandArgument='<%#Eval("id")%>' CommandName='<%#Eval("nedurumda")%>' ></asp:Button>
                                                <asp:Label ID="Labelgemiup"  runat="server"   Text='<%#Eval("gemiadi")%>' ></asp:Label>
                                        </td>
                                        <td class="tdis16 ">
                                            <asp:Label ID="notlar" runat="server" Text='<%#Eval("notlar").ToString().Length>33? (Eval("notlar") as string).Substring(0,34)+"." : Eval("notlar")%>' ToolTip='<%#Eval("notlar") %>'></asp:Label><asp:Label ID="notlar2" runat="server" Text='<%#Eval("notlar")%>' Visible="false"></asp:Label></td>

                                        <td class="tdis3 ">
                                            <asp:Label ID="Lbl3" runat="server"><%#Eval("kalkislimani") %> <%#Eval("kalkisrihtimi") as string == "0" || Eval("kalkisrihtimi") as string == ""  ? "" : "/" + Eval("kalkisrihtimi")   %> </asp:Label><asp:Label ID="Lblkalkislimanigiz" runat="server" Visible="false" Text='<%#Eval("kalkislimani")%>'></asp:Label></td>
                                        <td class="tdis4 ">
                                            <asp:Label ID="Lbl5" runat="server" Text='<%#Eval("yanasmalimani") %>'></asp:Label><asp:Label ID="Lbl5rih" runat="server"> <%#Eval("yanasmarihtimi") as string == "0" || Eval("yanasmarihtimi") as string == ""  ? "" : "/" + Eval("yanasmarihtimi")   %></asp:Label></td>
                                        <td class="tdis5">
                                            <asp:Label ID="demirkisa" runat="server" Text='<%#Eval("demiryeri") %>'></asp:Label></td>
                                        <td class="tdis6 ">
                                            <asp:Label ID="Lbl11" runat="server" Text='<%#Eval("bayrak").ToString().Length>10? (Eval("bayrak") as string).Substring(0,11)+"." : Eval("bayrak")%>'></asp:Label></td>
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
                                            <asp:Label ID="Lbl15" runat="server" Text='<%#Eval("tehlikeliyuk")%>'></asp:Label>  <asp:Label ID="Lbl15d" runat="server" Visible="false" Text='<%#Eval("pratikano")%>'></asp:Label> </td>
                                        <td class="tdis13 "><%#Eval("draft")%></td>
                                        <td class="tdis14 "><%#Eval("bilgi")%></td>
                                        <td class="tdis15 ">
                                            <asp:Label ID="Lbl4c" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(0,2)+"/ " : "" %>'></asp:Label>
                                            <asp:Label ID="Lbl4hour" runat="server" Text='<%#Eval("eta").ToString().Length>1? (Eval("eta") as string).Substring(11,5) : "" %>'></asp:Label></td>
                                        <td class="tdis17 ">
                                            <asp:Label ID="Lbl20c" runat="server" Text='<%#Eval("talepno").ToString().Length>6? (Eval("talepno") as string).Substring(0,7)+"." : Eval("talepno")%>' ToolTip='<%# "Req :" + Eval("talepno") + " / Prq :" + Eval("pratikano")%>'></asp:Label></td>
                                         <td class="tdis18 ">
                                            <asp:ImageButton ID="ImageButtonissilc" runat="server" CommandArgument='<%#Eval("id")%>' OnClick="ImageButtonissil_Click" ImageUrl="~/images/cancelsil.png" Width="16px" />
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







                        <asp:Button ID="buttonshowpopupisekle" runat="server" Style="display: none" Text="ANJ" />
                        <asp:Panel ID="Panelisekle" runat="server" Style="display: none;" CssClass="panelisekle">
                            <div style="margin-top: 10px; clear: both">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <div style="border: 1px solid white; font-weight: bold; height: 30px; text-align: center;"><strong>Adding New Job Screen</strong></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Ship IMO No :</td>
                                        <td>
                                            <asp:TextBox CssClass="textboxs" ID="TextBoxaddimo" runat="server" Text="" Visible="True" Width="105px" MaxLength="7" AutoPostBack="true" OnTextChanged="TextBoxaddimo_TextChanged"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="TextBoxaddimo" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Ship Name :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TextBoxisadd1" runat="server"  MaxLength="30" Text="" Visible="True" Width="197px"  Height="16px"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Departure Port :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLdepport" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="DDLdepport_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Departure Berth :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLdepberth" runat="server" Width="100px"></asp:DropDownList></td>

                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Anchorage Place :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLanchorplace" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="DDLanchorplace_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Destination Port :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLdestport" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="DDLdestport_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Destination Berth :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLdestberth" runat="server" Width="100px"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Eta :</td>
                                        <td>
                                            <asp:TextBox CssClass="textboxs" ID="TextBoxisadd4" runat="server" Text="" Visible="True" ClientIDMode="Static" Width="150px" MaxLength="16"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TextBoxisadd4" CultureName="tr-TR" ID="MaskedEditExtender2" runat="server" ErrorTooltipEnabled="true" MaskType="DateTime" DisplayMoney="Left" AcceptNegative="Left" Mask="99/99/9999 99:99"></asp:MaskedEditExtender>
                                        </td>
                                   </tr>
                                    <tr>
                                        <td class="panel-style1">Bow-Stern Thrstr :</td>
                                        <td>
                                            <asp:TextBox CssClass="textboxs" ID="TextBoxisadd7" runat="server" MaxLength="4" Text="" Visible="True" Width="65px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="TextBoxisadd7" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                            -
                                            <asp:TextBox CssClass="textboxs" ID="TextBoxisadd8" runat="server" Text="" MaxLength="4" Visible="True" Width="65px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="TextBoxisadd8" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                            Kw</td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Flag :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLflag" runat="server" Width="200px"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Grt :</td>
                                        <td>
                                            <asp:TextBox CssClass="textboxs" ID="TextBoxisadd10" runat="server" MaxLength="6" Text="" Visible="True" Width="65px" Height="16px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="TextBoxisadd10" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>&nbsp;&nbsp;&nbsp;
                                            Loa :&nbsp;<asp:TextBox CssClass="textboxs" ID="TextBoxisadd11" runat="server" Text="" MaxLength="3" Visible="True" Width="65px" Height="16px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="TextBoxisadd11" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                            m.

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Type :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLtip" runat="server" Width="120px"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                                <%--Tug :<asp:TextBox CssClass="textboxs" ID="TextBoxisadd11a" runat="server" Text="" MaxLength="1" Visible="True" Width="30px" Height="16px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" TargetControlID="TextBoxisadd11a" runat="server"  FilterType="Custom" ValidChars="012345"></asp:FilteredTextBoxExtender>--%>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td class="panel-style1">Dangerous&nbsp;Cargo :</td>
                                        <td>
                                            <asp:DropDownList ID="DDL12" runat="server" Width="50px" Height="19px">
                                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="panel-style1">Pratique :</td>
                                        <td>
                                           <asp:DropDownList ID="DDLpratika" runat="server" Width="50px" Height="19px"  OnSelectedIndexChanged="DDLpratika_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Value="0" Selected="True">No</asp:ListItem><asp:ListItem Value="1">Yes</asp:ListItem> </asp:DropDownList> 
                   <asp:TextBox ID="TBpratikano" Width="100px" runat="server" MaxLength="16"></asp:TextBox>
            
                                            </td>
                                    </tr>


                                    <tr>
                                        <td class="panel-style1">LÇB No/ Dest:</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TextBoxlcbno" runat="server" Text="" Visible="True" MaxLength="16" Width="100px" Height="16px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" TargetControlID="TextBoxlcbno" runat="server" ValidChars="0,1,2,3,4,5,6,7,8,9,0,.,-"></asp:FilteredTextBoxExtender>/&nbsp;
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TextBoxlcbdest" runat="server" Text="" Visible="True" MaxLength="20" Width="100px" Height="16px"></asp:TextBox>
                                            </td>
                                    </tr>

                                    <tr>
                                        <td class="panel-style1">LÇB ETS Date:</td>
                                         <td>
                                            <asp:TextBox CssClass="textboxs" ID="TextBoxlcbdate" runat="server" Text="" Visible="True" ClientIDMode="Static" Width="150px" MaxLength="16"></asp:TextBox><asp:MaskedEditExtender  ID="MaskedEditExtender25" TargetControlID="TextBoxlcbdate" CultureName="tr-TR" runat="server" ErrorTooltipEnabled="true" MaskType="DateTime" DisplayMoney="Left" AcceptNegative="Left" Mask="99/99/9999 99:99"></asp:MaskedEditExtender></td>
                                    </tr>




                                    <tr>
                                        <td class="panel-style1">Agency/Inv.:</td>
                                         <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TextBoxisadd13" runat="server" Text="" Visible="True" MaxLength="40" Width="100px" Height="16px"></asp:TextBox>/&nbsp;
                                             <asp:TextBox CssClass="ilkharfbuyuk" ID="TextBoxisadd14" runat="server" Text="" Visible="True" MaxLength="20" Width="100px" Height="16px"></asp:TextBox></td>
                                    </tr>


                                    <tr>
                                        <td class="panel-style1">Notes :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TextBoxisadd16" runat="server" Text="" Visible="True" MaxLength="200" Width="200px"  Rows="2" Wrap="true" TextMode="MultiLine" Height="35px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Req. No :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TextBoxisadd17" runat="server" Text="" Visible="True" MaxLength="20" Width="100px" Height="16px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1"></td>
                                        <td>
                                            <asp:Button ID="Yeniisekle" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" runat="server" Style="height: 30px; Width: 80px" Text="Save" CssClass="btn" OnClick="Yeniisekle_Click" />&nbsp;&nbsp; 
                                <asp:Button ID="Yeniisekleiptal" runat="server" Style="height: 30px; Width: 80px" Text="Cancel" CssClass="btn"  OnClick="Yeniisekleiptal_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:ModalPopupExtender
                            ID="ModalPopupyeniisekle" runat="server"
                            CancelControlID="Yeniisekleiptal"
                            TargetControlID="buttonshowpopupisekle"
                            PopupControlID="Panelisekle"
                            BackgroundCssClass="modalbodyarka">
                        </asp:ModalPopupExtender>


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
                                            <asp:Label Font-Size="Small" ID="TBJEimo" runat="server" Text="" Visible="True" Width="105px" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Ship Name :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEsn" runat="server" Visible="True" Width="197px" MaxLength="30" Height="16px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Departure Port :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLJEdepp" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="DDLJEdepp_SelectedIndexChanged"></asp:DropDownList></td>
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
                                            Loa :<asp:TextBox CssClass="textboxs" ID="TBJEloa" runat="server" Visible="True" MaxLength="3" Width="65px" Height="16px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="TBJEloa" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                        m.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Type :</td>
                                        <td>
                                            <asp:DropDownList ID="DDLJEtip" runat="server" Width="100px"></asp:DropDownList> &nbsp;&nbsp;
                                                Tug : <asp:Label ID="LblJEdrft" runat="server" Text="" Width="30px" Height="16px"></asp:Label> Tpp: <asp:Label ID="Labeltpp" runat="server" Text=""></asp:Label>
                                        </td>
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
                                                <asp:TextBox ID="TBJEpratikano" Width="100px" runat="server" MaxLength="16"></asp:TextBox>
            
                                            </td>
                                    </tr>



                                    
                                    <tr>
                                        <td class="panel-style1">LÇB No/ Dest:</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJElcbno" runat="server" Text="" Visible="True" MaxLength="16" Width="100px" Height="16px"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="TBJElcbno" runat="server" ValidChars="0,1,2,3,4,5,6,7,8,9,0,.,-"></asp:FilteredTextBoxExtender>/&nbsp;
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJElcbdest" runat="server" Text="" Visible="True" MaxLength="20" Width="100px" Height="16px"></asp:TextBox>
                                            </td>
                                    </tr>

                                    <tr>
                                        <td class="panel-style1">LÇB ETS Date:</td>
                                         <td>
                                            <asp:TextBox CssClass="textboxs" ID="TBJElcbdate" runat="server" Text="" Visible="True" ClientIDMode="Static" Width="150px" MaxLength="16"></asp:TextBox><asp:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="TBJElcbdate" CultureName="tr-TR" runat="server" ErrorTooltipEnabled="true" MaskType="DateTime" DisplayMoney="Left" AcceptNegative="Left" Mask="99/99/9999 99:99"></asp:MaskedEditExtender></td>
                                    </tr>

                                    

                                    <tr>
                                        <td class="panel-style1">Agency/Inv.:</td>
                                         <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEagency" runat="server" Text="" Visible="True" MaxLength="40" Width="100px" Height="16px"></asp:TextBox>/&nbsp;
                                             <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEinvoice" runat="server" Text="" Visible="True" MaxLength="20" Width="100px" Height="16px"></asp:TextBox></td>
                                    </tr>



                                    <tr>
                                        <td class="panel-style1">Notes :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEnotes" runat="server" Text="" MaxLength="200" Visible="True" Width="200px"  Rows="2" Wrap="true" TextMode="MultiLine" Height="35px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">Req. No :</td>
                                        <td>
                                            <asp:TextBox CssClass="ilkharfbuyuk" ID="TBJEreqno" runat="server" Text="" MaxLength="20" Visible="True" Width="100px" Height="16px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="panel-style1">
                                            <asp:Label Visible="false" ID="Lblnedurumda" runat="server"></asp:Label>
                                            <asp:Label Visible="false" ID="Lblnedurumdaopr" runat="server"></asp:Label></td>
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

                        <%--   modal message ok başlar    --%>
                        <asp:Button ID="buttonMessagePanelok" runat="server" Style="display: none;" />
                        <asp:Panel ID="panelMessagePanelok" Style="display: none;" CssClass="panelmessage" runat="server">
                            <div style="text-align: center; font-weight: bold;">
                                <br />
                            </div>
                            <asp:Button ID="Bacceptedok" runat="server" Style="height: 30px; Width: 80px" Text="OK" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" OnClick="Bacceptedok_Click" />&nbsp;&nbsp; 
                            <asp:Button ID="Bclosedok" runat="server" Style="height: 30px; Width: 80px"  Text="Close" />
                            <br /><br />
                            <asp:Literal ID="issilmes" runat="server"></asp:Literal><br /><br />
                            <asp:CheckBox ID="CBdeleteis" Checked="false" runat="server" />  &nbsp;<asp:Literal ID="Literal1" runat="server" Text="Delete but keep in System Database."></asp:Literal> &nbsp;<br />
                        </asp:Panel>
                        <asp:ModalPopupExtender ID="ModalPopupMessageok" runat="server"
                            TargetControlID="buttonMessagePanelok"
                            PopupControlID="panelMessagePanelok"
                            BackgroundCssClass="modalbodyarka"
                            CancelControlID="Bclosedok">
                        </asp:ModalPopupExtender>
                        <%-- modal message ok biter --%>

                        <div style="height: 40px; clear: both; float: left;">
                            <asp:Label runat="server" Text=" &nbsp;" ForeColor="Black" Height="40px"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </asp:Panel>
        </div>
    </form>
</body>
</html>
