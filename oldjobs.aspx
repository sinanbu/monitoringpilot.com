<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oldjobs.aspx.cs" Inherits="oldjobs" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

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
            z-index: 200;
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
            height: 290px;
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

<body onkeydown="return (event.keyCode!=8 || event.keyCode!=13 )"> <%--// kod 8 tuşunu enteri aktif tapmak için ekle--%>
    <form id="form1" runat="server">

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnableScriptGlobalization="true" runat="server"></asp:ToolkitScriptManager>

        <table style="width: 1340px; color:#111; font-weight:bold;  height: 25px; background-image: url(images/boslukaltcizgi.png)">
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
                            <li  id="menumenu" runat="server" style="color:#fff; ">M E N U
                
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
                                    <button runat="server" id="Bdaricaships" style="border: 1px solid black; cursor: pointer; background-color: #7DA3CD; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='daricaships.aspx'">Darıca Ships</button>&nbsp;
                                    <button runat="server" id="Byarimcaships" style="border: 1px solid black; cursor: pointer; background-color: #9B86B5; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='yarimcaships.aspx'">Yarımca Ships</button>

                                </td>
                                <td style="text-align: right;">
                                    <asp:LinkButton ID="LBonline" runat="server" ForeColor="#111" OnClick="LBonline_Click"></asp:LinkButton> &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="#111" OnClick="LBonlineoff_Click" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp; </td>
                            </tr>
                        </table>


                        <br />

                        <div class="clear"></div>
                        <asp:TextBox ID="TBshipname" runat="server" MaxLength="30"  Placeholder="Ship Name"  Font-Size="Small" Width="187px" Height="22px" ></asp:TextBox>  &nbsp;&nbsp;&nbsp;
                                <asp:DropDownList ID="DDLPilots"   runat="server" Height="24px" Width="200px"   ></asp:DropDownList> &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="LBgetist3" runat="server"  OnClick="LBgetist3_Click" Text="Find Jobs" Width="80px" Height="24px"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" ></asp:Button>&nbsp;&nbsp;&nbsp;

                        <p>&nbsp;</p> 



                        <asp:Label ID="LBnewjob" runat="server"  CssClass="tablobaslik4"  Style="cursor: default"  Text="OLD JOB RECORDS"></asp:Label>
                        <asp:Panel ID="panelcollapsein" Style="display: block; overflow: hidden;" runat="server">
                       <p>&nbsp;</p> <asp:Label ID="Lwoidgunluk"  Style="color:#111; font-size:14px;"  runat="server" ></asp:Label>  

                         <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#222" GridLines="Vertical" OnSelectedIndexChanging="GridView7_SelectedIndexChanging"  Width="100%" AllowPaging="True" PageSize="100" OnPageIndexChanging="GridView7_PageIndexChanging">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns >
                                 <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-Width="24px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                
                                  <asp:TemplateField ControlStyle-Width="40px"  HeaderStyle-HorizontalAlign="Left" HeaderText="No">
                                     <ItemTemplate>
                                         <asp:Label ID="siraod" runat="server" Text='<%# Container.DataItemIndex +1   %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="140px" HeaderStyle-HorizontalAlign="Left"  HeaderText="Ship Name">
                                     <ItemTemplate>
                                           <div style="text-align:left"><asp:Label ID="shipod" runat="server"  Text='<%# Bind("gemiadi") %>'></asp:Label></div>
                                         
                                     </ItemTemplate>
                                 </asp:TemplateField>

                               <asp:TemplateField ControlStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" HeaderText="Grt">
                                     <ItemTemplate>
                                         <asp:Label ID="tgrtod" runat="server" ><%#Eval("grt")%></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="140px" HeaderStyle-HorizontalAlign="Left"  HeaderText="Pilot Name">
                                     <ItemTemplate>
                                         <div style="text-align:left"><asp:Label ID="pilotod" runat="server" Text='<%# Bind("pilotismi") %>'></asp:Label></div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="110px" HeaderStyle-HorizontalAlign="Left"  HeaderText="Departure">
                                     <ItemTemplate>
                                          <div style="text-align:left"><asp:Label ID="depod" runat="server" ><%#Eval("binisyeri") %> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %></asp:Label></div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="110px" HeaderStyle-HorizontalAlign="Left"  HeaderText="Destination">
                                     <ItemTemplate>
                                          <div style="text-align:left"><asp:Label ID="destod" runat="server" ><%#Eval("inisyeri") %> <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %></asp:Label></div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"  HeaderText="Pilot OnBoard">
                                     <ItemTemplate>
                                         <asp:Label ID="sofod" runat="server" ><%#Eval("pob") %> </asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"  HeaderText="Pilot Off">
                                     <ItemTemplate>
                                         <asp:Label ID="sonod" runat="server" ><%#Eval("poff")%>  </asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 
                                  <asp:TemplateField ControlStyle-Width="90px" HeaderStyle-HorizontalAlign="Left"  HeaderText="Req.No">
                                     <ItemTemplate>
                                         <asp:Label ID="talepno" runat="server" ><%#Eval("talepno")%>  </asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 
                                  <asp:TemplateField ControlStyle-Width="260px" HeaderStyle-HorizontalAlign="Left"  HeaderText="Notes">
                                     <ItemTemplate>
                                         <asp:Label ID="notlar" runat="server" ><%#Eval("notlar")%>  </asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                                   <asp:TemplateField ControlStyle-Width="30px" HeaderStyle-HorizontalAlign="Left"  HeaderText="X" >
                                     <ItemTemplate>
                                         <asp:Label ToolTip='<%#Eval("manevraiptal") as string == "1"  ? "" : "Cancelled" %>' ID="cancelod" runat="server" ><%#Eval("manevraiptal") %></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                             </Columns>
                             
                             <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB" Font-Names="Arial"  Font-Size="12px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False"  />
                             <SelectedRowStyle BackColor="#c1cDe1"  ForeColor="#222" />
                         </asp:GridView>

                        </asp:Panel>
                        <p>&nbsp;</p>


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
