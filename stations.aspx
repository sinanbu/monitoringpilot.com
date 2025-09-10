<%@ Page Language="C#" AutoEventWireup="true" CodeFile="stations.aspx.cs" Inherits="stations" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html  xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot - LIVE SCREEN</title>
 <link href="css/stil.css" rel="stylesheet" />
        
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }


        #menutek {
            margin-top: -10px;
            margin-left: 21px;
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

            .modalbodyarka {
            background-color: #000000;
            filter: alpha(opacity:30);
            opacity: 0.8;
            z-index: 10000;
            color: black;
        }
            .linkbutonclear {
            color: black;
            text-decoration:none;
        }

    </style>

    <script type="text/javascript" src="js/jquery-1.11.2.js"></script>
<script type="text/javascript">

    window.onload = SetScroll;

    function SetScroll() {
        var objDiv = document.getElementById("<%=UpdatePanelCanliekran.ClientID%>");
        objDiv.scrollTop = objDiv.scrollHeight;
    }



</script>

</head>
<body onkeydown = "return (event.keyCode!=13)" >
    <form id="form1" runat="server" >

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

           <table  style="width:1340px; height:25px;  color:#111; font-weight:bold; z-index:10; background-image:url(images/boslukaltcizgi.png)"><tr  style="height:25px">
            <td style="text-align:right; width:56%; float:left; "> 
<asp:Label ID="LblVarid" ForeColor="Red" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarid" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarno" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarno" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarbasla" runat="server" Text=""></asp:Label><asp:Literal ID="varbaslar" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarbit" runat="server" Text=""></asp:Label> <asp:Literal ID="varbiter" runat="server" Text="" Visible="false"></asp:Literal> 
                <asp:Literal ID="Litlastup" runat="server" Text="" Visible="false"></asp:Literal> 
            </td><td  style="text-align:left;   width:32%; float:left; "><div  style="position:absolute; margin-top:-4px;">  <asp:Panel ID="Panel1" runat="server">
<asp:UpdatePanel ID="UpdatePanelHeader" runat="server" UpdateMode="Always" ><ContentTemplate>
                
                    <asp:Timer ID="gerisayimtik" runat="server" OnTick="gerisayimtik_Tick"  Interval="120000"></asp:Timer>
<asp:Label ID="LblReeltime" runat="server" Text=""></asp:Label> / JobCounter: <asp:Label ID="LblCounter" ForeColor="Red"  Font-Size="14px"  runat="server" Text=""></asp:Label>&nbsp;
    <asp:Label  ID="Lblindicator1" ForeColor="black"  Height="16px" Font-Size="12px"  runat="server" ></asp:Label><asp:Label  ID="Lblindicator2" ForeColor="black"  Height="16px"  Font-Size="12px"  runat="server"  ></asp:Label><asp:Label  ID="Lblindicator3" ForeColor="black"   Height="16px"  Font-Size="12px"  runat="server"   ></asp:Label><asp:Label  ID="Lblindicator4" ForeColor="black"    Height="16px"  Font-Size="12px"  runat="server"   ></asp:Label><asp:Label  ID="Lblindicator5" ForeColor="black"    Height="16px"  Font-Size="12px"  runat="server"   ></asp:Label><asp:Label  ID="Lblindicator6" ForeColor="black"    Height="16px" Font-Size="12px"  runat="server"   ></asp:Label><asp:Label  ID="Lblindicator7" ForeColor="black"    Height="16px" Font-Size="10px"  runat="server"   ></asp:Label>

 </ContentTemplate></asp:UpdatePanel></asp:Panel> </div>
                </td><td  style="text-align:right;  width:10%;">

               <div id="menutek">
            
        <ul>
            <li  style="color:#fff; ">M E N U
                
                <ul>
                    <li id="mainmanu1" runat="server"><a href="watchsummary.aspx">Summary</a></li>
                    <li id="mainmanu2" runat="server"><a href="log.aspx">Daily Log</a></li>
                    <li id="mainmanu3" runat="server"><a href="joblist.aspx">Joblist</a></li>
                    <li id="mainmanu4" runat="server"><a href="oldjobs.aspx">Oldjobs</a></li>
                    <li id="mainmanu5" runat="server"><a href="pilot.aspx">Pilots</a></li>
                    <li id="mainmanu6" runat="server"><a href="portinfo.aspx">Port Info</a></li>
                    <%-- <li id="mainmanu8" runat="server"><a href="shipsonmap.aspx" >Ships In Port</a></li>--%>
                   <li id="mainmanu7" runat="server"><a href="mapsection.aspx">Map</a></li>
                </ul>
            </li>
        
        </ul>
    </div> 



 
</td></tr>
</table>


    <div  style="width:100%;  z-index:10;  text-align:left;">
     <asp:Panel ID="Panel2" runat="server">
 <asp:UpdatePanel ID="UpdatePanelCanliekran" runat="server" UpdateMode="Always" ><ContentTemplate>



<table  style="width:1340px; float:right;  color:white">
 <tr  style="height:22px;  border-color:red; ">
     <td style="text-align:left; width:30%;">

</td>

     <td style="text-align:center;  width:40%;">
                <asp:LinkButton ID="LBonline" runat="server"  ForeColor="#111" OnClick="LBonline_Click" ></asp:LinkButton> &nbsp;&nbsp
                <asp:LinkButton ID="LBmyjobs" runat="server"  ForeColor="#111"  OnClick="LBmyjobs_Click"  Text="My Jobs" Visible="false"></asp:LinkButton> &nbsp;&nbsp
                <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="#111"  OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "  ></asp:LinkButton>&nbsp;&nbsp;
                         <asp:LinkButton ID="LBSurvey" Text="AR-GE" Font-Bold="true" ForeColor="#000"   OnClick="LBSurvey_Click" runat="server"   ></asp:LinkButton>&nbsp;&nbsp;
  </td>  <td style="text-align:right;  width:30%;">


 </td></tr></table>  


    <table  style="width:100%; float:left; color:white">
<tr  style="height:22px; "><td  style="text-align:left; font-size:12px; color:#1111ee;"><b>OnLeave(Changing/AddedPilot) :</b> <asp:Label ID="Lblizinliler" runat="server" Text=""></asp:Label>
    <asp:Label ID="Litizinler" runat="server" Text=""></asp:Label></td></tr>

<tr  style="height:22px"><td  style="text-align:left">
    <asp:Label ID="LBDuyuru" runat="server"  style="font-size:12px; color:#1111ee;"><b>Notice :</b></asp:Label> 
    <asp:Label ID="LblDuyuru" runat="server"  Font-Size="12px" ForeColor="#660066"  Text=""></asp:Label>
    </td></tr></table>  
                    


<table  style="width:100% ;">
<tr><td style="text-align:left; width:100% ;"><span class="tablobaslik1" style="cursor:default;"><b><asp:LinkButton runat="server"  style="text-decoration:none"  Text="DARICA" ID="siptus"  OnClick="siptus_Click"></asp:LinkButton> </b>&nbsp;/&nbsp;
    <asp:Label ID="LBonlined" runat="server"></asp:Label></span><asp:label  ID="LblNosayd" runat="server" Visible="false" ></asp:label></td>
</tr> </table>  

        <asp:DataList ID="DLDarica"  runat="server" Width="1340px"  CellPadding="2" HorizontalAlign="Left" OnItemDataBound="DLDarica_ItemDataBound" OnItemCommand="DLDarica_ItemCommand">

           <HeaderTemplate>
                                    <table  class="tablo1">
                                        <tr class="trbaslik1" >
                        <td style="width:2% ">No</td>
                        <td style="width:12% ">Pilot Name</td>
                        <td style="width:2% ">Jb</td>
                        <td style="width:3% ">W.Hr</td>
                        <td style="width:4% ">Fatig.</td>
                      <%--  <td style="width:3%; ">R/24</td>--%>
                        <td style="width:3%; ">R/8</td>
                        <td style="width:14% ">Ship Name</td>
                        <td style="width:7% ">Flag</td>
                        <td style="width:4% ">Grt</td>
                        <td style="width:3% ">Type</td>
                        <td style="width:3% "><asp:Label ID="Label3" runat="server"  Text="IMDG" ToolTip="Dangerous Cargo"></asp:Label></td>
                        <td style="width:3% "><asp:Label ID="Label23" runat="server"  Text="TBP-T" ToolTip="Total Bollard Pull"></asp:Label></td>
                        <td style="width:10% ">Departure</td>
                        <td style="width:10% ">Arrival</td>
                        <td style="text-align:center; width:5% ">OffStation</td>
                        <td style="text-align:center; width:5% ">POB</td>
                        <td style="text-align:center; width:5% ">POff</td>
                        <td style="text-align:center; width:5% ">OnStation</td>
</tr></table>
                    
            </HeaderTemplate>

            <ItemTemplate>
                <table  class="tablo1">
                        <tr  class="trsatir1">
                        <td  style="width:2% "><asp:label  ID="LblNo" runat="server" ForeColor="Black" Text='<%# Container.ItemIndex +1 %>' ></asp:label></td>
                        <td  style="width:12% " >
                            <asp:label  ID="lblDurum" runat="server" Text='<%#Eval("durum")%>'  Visible="false"></asp:label>
                            <asp:label ID="LBpgecmis" runat="server"  style="cursor:pointer; text-decoration:none;"  Font-Italic='<%#(Eval("kidem").ToString())=="5"? true : false%>' Font-Bold='<%#Convert.ToInt32((Eval("kidem")))>2? true : false%>'><%#Eval("degismeciadisoyadi")%></asp:label>
                            <asp:label  ID="LblKapno" runat="server" Text='<%#Eval("kapno")%>' Visible="False"></asp:label>
                            <asp:label  ID="Lblkidem" runat="server" Text='<%#Eval("kidem")%>' Visible="False"></asp:label>
                        </td>
                         <td  style=" width:2% ; "><%#Eval("toplamissayisi")%></td>
                        <td  style=" width:3% ; "><%#Eval("toplamissuresi")%></td>
                        <td  style=" width:4% ; "><asp:label ID="Lblyorulma" runat="server"  Text='<%#Eval("yorulma")%>' ToolTip=""></asp:label><asp:label ID="Lbllastday" runat="server"  Visible="false" Text='<%#Eval("lastday")%>'></asp:label></td>
                        
                        <td  style=" width:3% ;   "><%#Eval("lastseven")%></td>
                        <td  style=" width:14% ; "><asp:LinkButton ID="Lblgemiadi" Style="text-decoration: none;" CommandName="gemilybd" runat="server" Text='<%#Eval("gemiadi")%>'  ToolTip=""></asp:LinkButton><asp:label ID="Lblimono" runat="server" Visible="false" Text='<%#Eval("imono")%>' ></asp:label></td>
                        <td  style=" width:7% ; "><asp:LinkButton ID="Lblbayrak" Style="text-decoration: none;" CommandName="gemiord" runat="server" Text='<%#Eval("bayrak").ToString().Length>9? (Eval("bayrak") as string).Substring(0,10)+"." : Eval("bayrak")%>'  ToolTip=""></asp:LinkButton></td>
                        <td  style=" width:4% ; "><%#Eval("grt")%></td>
                        <td  style=" width:3% ; "><%#Eval("tip").ToString().Length>1? (Eval("tip") as string).Substring(0,3) : Eval("tip")%></td>
                                                    <td  style=" width:3% ; "><asp:label ID="Lbldc" runat="server"></asp:label></td>
                                                    <td  style=" width:3% ; "><asp:label ID="Lbltbp" runat="server"></asp:label></td>


                        <td  style=" width:10% ; "><asp:LinkButton ID="binisport" runat="server" Text='<%# Bind("binisyeri") %>'  CommandName="linkleb" CssClass="tablobaslik1"></asp:LinkButton> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %>   </td>
                        <td  style=" width:10% ; "><asp:LinkButton ID="inisport" runat="server" Text='<%# Bind("inisyeri") %>'  CommandName="linklei" CssClass="tablobaslik1"></asp:LinkButton> <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %><asp:label ID="Lblinisyerigiz" runat="server"  Visible="false" Text='<%#Eval("inisyeri")%>'></asp:label></td>
                        <td  style="text-align:center; width:5%  ">
                                <asp:label  ID="LblIstasyoncikis" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>' ></asp:label>
                             <asp:label  ID="LblIstasyoncikiseta" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>'  ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td  style="text-align:center; width:5%  ">
                                        <asp:label  ID="LblPob" runat="server"   Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>' ></asp:label>
                                        <asp:label  ID="LblPobeta" runat="server" Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td  style="text-align:center; width:5%  ">
                               <asp:label  ID="LblPoff" runat="server"  Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>' ></asp:label>
                            <asp:label  ID="LblPoffeta" runat="server" Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td  style="text-align:center; width:5%  ">
                                <asp:label  ID="LblIstasyongelis" runat="server"  Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>' ></asp:label>
                             <asp:label  ID="LblIstasyongeliseta" runat="server" Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>'  ForeColor="#aaaaaa"></asp:label>
                        </td>     </tr></table>                                                        


                  
                <div class="clear">
        <asp:Panel ID="panelpilotgec"  runat="server"  >  
<asp:ListView ID="ListView1" runat="server"  >

                <itemtemplate>
                    <div> <table  class="tablo1a" >
                    <tr style="border-bottom: 1px solid gray; border-left:1px solid aliceblue; height:22px">
                        <td style="width:2% "><asp:label  ID="Label5x" runat="server" Text='<%# Container.DataItemIndex +1 %>' ></asp:label><asp:label  ID="Labelpgkapno" runat="server" Text='<%#Eval("kapno")%>' Visible="false"></asp:label></td>
                      <td  style="width:13%; text-align:center; border-width:1px; border-style:solid;" >
                                                        <div class="containerrm"><div class="graphrm">
                                                 <div class="databarr1m" style='width:<%#Convert.ToDouble(Eval("rangerest").ToString()==""?0:Eval("rangerest"))<8? Convert.ToInt32(Convert.ToDouble(Eval("rangerest").ToString()==""?0:Eval("rangerest"))*13):103%>%;'></div>
                                             </div>

                                             <div class="datavaluerm"><%#Eval("rangerest") %></div>
                                         </div>

                        </td>
                        <td  style="width:13%; text-align:center; border-width:1px; border-style:solid;" >
                            <div class="containerrm"><div class="graphrm">
                                                 <div class="databarr2m" style='width:<%#Convert.ToDouble(Eval("rangework").ToString()==""?0:Eval("rangework"))<8? Convert.ToInt32(Convert.ToDouble(Eval("rangework").ToString()==""?0:Eval("rangework"))*13):103%>%;'></div>
                                             </div>
                                             <div class="datavaluerm"><%#Eval("rangework") %></div>
                                         </div>
                            </td>
                        <td  style="width:12%" ><asp:label  ID="Label5" runat="server" Text='<%#Eval("gemiadi")%>'  Font-Strikeout='<%#(Eval("manevraiptal").ToString())=="1"? true : false%>' ></asp:label></td>
                        <td  style="width:7%" ><asp:label  ID="Label6" runat="server" Text='<%#Eval("bayrak")%>' ></asp:label></td>
                        <td  style="width:4% ;" ><asp:label  ID="Label7" runat="server" Text='<%#Eval("grt")%>' ></asp:label></td>
                        <td  style="width:3%;" ><asp:label  ID="Label15" runat="server" Text='<%#Eval("tip").ToString().Length>1? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>'  ToolTip='<%#Eval("tip") %>'></asp:label></td>
                        <td  style="width:3%;" ><asp:label  ID="Lbldcy" runat="server"> </asp:label></td>
                        <td  style="width:3%;" ><asp:label  ID="Lbltbpy" runat="server"> </asp:label></td>
                        <td  style="width:10%" ><asp:label  ID="Label8" runat="server" Text='<%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? Eval("binisyeri") :Eval("binisyeri")+ "/" + Eval("binisrihtim")   %>' ></asp:label></td>
                        <td  style="width:10%" ><asp:label  ID="Label9" runat="server" Text='<%#Eval("inisrihtim") as string == "0" || Eval("inisrihtim") as string == ""  ? Eval("inisyeri") :Eval("inisyeri")+ "/" + Eval("inisrihtim")  %>' ></asp:label></td>
                        <td  style="width:5%; text-align:center;" ><asp:label  ID="Label10" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? Eval("istasyoncikis").ToString().Substring(0,2)+"/"+Eval("istasyoncikis").ToString().Substring(11,5):""%>' ></asp:label></td>
                        <td  style="width:5%; text-align:center;" ><asp:label  ID="Label11" runat="server" Text='<%#Eval("pob").ToString().Length>0? Eval("pob").ToString().Substring(0,2)+"/"+Eval("pob").ToString().Substring(11,5):""%>' ></asp:label></td>
                        <td  style="width:5%; text-align:center;" ><asp:label  ID="Label12" runat="server" Text='<%#Eval("poff").ToString().Length>0? Eval("poff").ToString().Substring(0,2)+"/"+Eval("poff").ToString().Substring(11,5):""%>' ></asp:label></td>
                        <td  style="width:5%; text-align:center;" ><asp:label  ID="Label13" runat="server" Text='<%#Eval("istasyongelis").ToString().Length>0? Eval("istasyongelis").ToString().Substring(0,2)+"/"+Eval("istasyongelis").ToString().Substring(11,5):""%>' ></asp:label></td>

                    </tr>    </table> </div>
              </itemtemplate>

            </asp:ListView>
            <br /><br />
        </asp:Panel>
      <asp:CollapsiblePanelExtender runat="server" TargetControlID="panelpilotgec" ID="collapsepilotgec" Enabled="true" CollapseControlID="LBpgecmis" ExpandControlID="LBpgecmis" CollapsedText="Show Jobs.." ExpandedText="Hide Jobs.." SuppressPostBack="True" Collapsed="True">
      </asp:CollapsiblePanelExtender>    </div>
            
        
     
            
            </ItemTemplate>    
        </asp:DataList>




 <table  style="width:100%">
<tr><td style="text-align:left; width:100% "><br /><span  class="tablobaslik0"  style="cursor:default;"><b><asp:LinkButton runat="server" style="text-decoration:none" Text="YARIMCA" ID="sipyar"  OnClick="sipyar_Click"></asp:LinkButton></b>&nbsp;/&nbsp;
    <asp:Label ID="LBonliney" runat="server"></asp:Label></span><asp:label  ID="LblNosayy" runat="server" Visible="false" ></asp:label></td>
</tr> </table>
            
        <asp:DataList ID="DLDaricay" runat="server" Width="1340px"  CellPadding="2" HorizontalAlign="Left" OnItemDataBound="DLDaricay_ItemDataBound" OnItemCommand="DLDaricay_ItemCommand">

          <HeaderTemplate>
                <table  class="tablo0">
                     <tr  class="trbaslik0">
                        <td style="width:2% ">No</td>
                        <td style="width:12% ">Pilot Name</td>
                        <td style="width:2% ">Jb</td>
                        <td style="width:3% ">W.Hr</td>
                        <td style="width:4% ">Fatig.</td>
                        <%--<td style="width:3%; ">R/24</td>--%>
                        <td style="width:3%; ">R/8</td>
                        <td style="width:14% ">Ship Name</td>
                        <td style="width:7% ">Flag</td>
                        <td style="width:4% ">Grt</td>
                        <td style="width:3% ">Type</td>
                        <td style="width:3% "><asp:Label ID="Label3" runat="server"  Text="IMDG" ToolTip="Dangerous Cargo"></asp:Label></td>
                        <td style="width:3% "><asp:Label ID="Label23" runat="server"  Text="TBP-T" ToolTip="Total Bollard Pull"></asp:Label></td>
                        <td style="width:10% ">Departure</td>
                        <td style="width:10% ">Arrival</td>
                        <td style="text-align:center; width:5% ">OffStation</td>
                        <td style="text-align:center; width:5% ">POB</td>
                        <td style="text-align:center; width:5% ">POff</td>
                        <td style="text-align:center; width:5% ">OnStation</td>

                    </tr></table>
            </HeaderTemplate>
   
            <ItemTemplate>
                <table  class="tablo1" >
                       <tr  class="trsatir0">
                        <td  style="width:2% "><asp:label  ID="LblNoy" runat="server" ForeColor="Black" Text='<%# Container.ItemIndex +1 %>' ></asp:label></td>
                        <td  style="width:12% ">
                            <asp:label  ID="lblDurumy" runat="server" Text='<%#Eval("durum")%>' Visible="false" ></asp:label>
                            <asp:label ID="LBpgecmisy" runat="server"  style="cursor:pointer; text-decoration:none; "   Font-Italic='<%#(Eval("kidem").ToString())=="5"? true : false%>' Font-Bold='<%#Convert.ToInt32((Eval("kidem")))>2? true : false%>'><%#Eval("degismeciadisoyadi")%></asp:label>
                            <asp:label  ID="LblKapnoy" runat="server" Text='<%#Eval("kapno")%>' Visible="False"></asp:label>
                            <asp:label  ID="Lblkidemy"  runat="server" Text='<%#Eval("kidem")%>' Visible="False"></asp:label>
                        </td>
                        <td  style=" width:2% ; "><%#Eval("toplamissayisi")%></td>
                        <td  style=" width:3% ; "><%#Eval("toplamissuresi")%></td>
                        <td  style=" width:4% ; "><asp:label ID="Lblyorulmay" runat="server"  Text='<%#Eval("yorulma")%>'></asp:label><asp:label ID="Lbllastdayy" runat="server"   Visible="false"  Text='<%#Eval("lastday")%>'></asp:label></td>
                       
                        <td  style=" width:3% ;   "><%#Eval("lastseven")%></td>
                        <td  style=" width:14%  ; "><asp:LinkButton ID="Lblgemiadiy" Style="text-decoration: none;" CommandName="gemilybd"  runat="server" Text='<%#Eval("gemiadi")%>'></asp:LinkButton><asp:label ID="Lblimonoy" runat="server" Visible="false" Text='<%#Eval("imono")%>' ></asp:label></td>
                        <td  style=" width:7% ; "><asp:LinkButton ID="Lblbayraky" Style="text-decoration: none;" CommandName="gemiord" runat="server" Text='<%#Eval("bayrak").ToString().Length>9? (Eval("bayrak") as string).Substring(0,10)+"." : Eval("bayrak")%>'  ToolTip=""></asp:LinkButton></td>
                        <td  style=" width:4%  ;"><%#Eval("grt")%></td>
                        <td  style=" width:3%  ; "><%#Eval("tip").ToString().Length>1? (Eval("tip") as string).Substring(0,3) : Eval("tip")%></td>
                                                    <td  style=" width:3% ; "><asp:label ID="Lbldcy" runat="server"></asp:label></td>
                                                    <td  style=" width:3% ; "><asp:label ID="Lbltbpy" runat="server"></asp:label></td>
                        <td   style=" width:10%  ; "><asp:LinkButton ID="binisport" runat="server" Text='<%# Bind("binisyeri") %>'  CommandName="linkleb" CssClass="tablobaslik0"></asp:LinkButton> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %>   </td>
                        <td   style=" width:10%  ; "><asp:LinkButton ID="inisport" runat="server" Text='<%# Bind("inisyeri") %>'  CommandName="linklei" CssClass="tablobaslik0"></asp:LinkButton> <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %><asp:label ID="Lblinisyerigizy" runat="server"  Visible="false" Text='<%#Eval("inisyeri")%>'></asp:label></td>
                        <td   style="text-align:center; width:5% ">
                                <asp:label  ID="LblIstasyoncikisy" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>' ></asp:label>
                             <asp:label  ID="LblIstasyoncikisetay" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td   style="text-align:center; width:5% ">
                             <asp:label  ID="LblPoby" runat="server"   Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>' ></asp:label>
                             <asp:label  ID="LblPobetay" runat="server" Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td   style="text-align:center; width:5% ">
                               <asp:label  ID="LblPoffy" runat="server"  Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>' ></asp:label>
                            <asp:label  ID="LblPoffetay" runat="server" Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td   style="text-align:center; width:5% ">
                                <asp:label  ID="LblIstasyongelisy"   runat="server" Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>' ></asp:label>
                             <asp:label  ID="LblIstasyongelisetay" runat="server" Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>'  ForeColor="#aaaaaa"></asp:label>
                        </td>                                                           


                    </tr></table>
    
                  
                <div class="clear">
        <asp:Panel ID="panelpilotgecy"  runat="server"  >  
<asp:ListView ID="ListView1y" runat="server" >

                <itemtemplate>
                    <div> <table  class="tablo1a" >
                    <tr style="border-bottom: 1px solid gray; border-left:1px solid aliceblue; height:22px">
                        <td style="width:2% "><asp:label  ID="Label5x" runat="server" Text='<%# Container.DataItemIndex +1 %>' ></asp:label><asp:label  ID="Labelpgkapnoy" runat="server" Text='<%#Eval("kapno")%>' Visible="false"></asp:label></td>
                      <td  style="width:13%; text-align:center; border-width:1px; border-style:solid;" >
                                                        <div class="containerrm"><div class="graphrm">
                                                 <div class="databarr1m" style='width:<%#Convert.ToDouble(Eval("rangerest").ToString()==""?0:Eval("rangerest"))<8? Convert.ToInt32(Convert.ToDouble(Eval("rangerest").ToString()==""?0:Eval("rangerest"))*13):103%>%;'></div>
                                             </div>
                                             <div class="datavaluerm"><%#Eval("rangerest") %></div>
                                         </div>

                        </td>
                        <td  style="width:13%; text-align:center; border-width:1px; border-style:solid;" >
                            <div class="containerrm"><div class="graphrm">
                                                 <div class="databarr2m" style='width:<%#Convert.ToDouble(Eval("rangework").ToString()==""?0:Eval("rangework"))<8? Convert.ToInt32(Convert.ToDouble(Eval("rangework").ToString()==""?0:Eval("rangework"))*13):103%>%;'></div>
                                             </div>
                                             <div class="datavaluerm"><%#Eval("rangework") %></div>
                                         </div>
                            </td>
                        <td  style="width:12%" ><asp:label  ID="Label5" runat="server" Text='<%#Eval("gemiadi")%>'  Font-Strikeout='<%#(Eval("manevraiptal").ToString())=="1"? true : false%>' ></asp:label></td>
                        <td  style="width:7%" ><asp:label  ID="Label6" runat="server" Text='<%#Eval("bayrak")%>' ></asp:label></td>
                        <td  style="width:4% ;" ><asp:label  ID="Label7" runat="server" Text='<%#Eval("grt")%>' ></asp:label></td>
                        <td  style="width:3%;" ><asp:label  ID="Label15" runat="server" Text='<%#Eval("tip").ToString().Length>1? (Eval("tip") as string).Substring(0,3) : Eval("tip")%>'  ToolTip='<%#Eval("tip") %>'></asp:label></td>
                        <td  style="width:3%;" ><asp:label  ID="Label4" runat="server"> </asp:label></td>
                        <td  style="width:3%;" ><asp:label  ID="Label14" runat="server"> </asp:label></td>
                        <td  style="width:10%" ><asp:label  ID="Label8" runat="server" Text='<%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? Eval("binisyeri") :Eval("binisyeri")+ "/" + Eval("binisrihtim")   %>' ></asp:label></td>
                        <td  style="width:10%" ><asp:label  ID="Label9" runat="server" Text='<%#Eval("inisrihtim") as string == "0" || Eval("inisrihtim") as string == ""  ? Eval("inisyeri") :Eval("inisyeri")+ "/" + Eval("inisrihtim")  %>' ></asp:label></td>
                        <td  style="width:5%; text-align:center;" ><asp:label  ID="Label10" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? Eval("istasyoncikis").ToString().Substring(0,2)+"/"+Eval("istasyoncikis").ToString().Substring(11,5):""%>' ></asp:label></td>
                        <td  style="width:5%; text-align:center;" ><asp:label  ID="Label11" runat="server" Text='<%#Eval("pob").ToString().Length>0? Eval("pob").ToString().Substring(0,2)+"/"+Eval("pob").ToString().Substring(11,5):""%>' ></asp:label></td>
                        <td  style="width:5%; text-align:center;" ><asp:label  ID="Label12" runat="server" Text='<%#Eval("poff").ToString().Length>0? Eval("poff").ToString().Substring(0,2)+"/"+Eval("poff").ToString().Substring(11,5):""%>' ></asp:label></td>
                        <td  style="width:5%; text-align:center;" ><asp:label  ID="Label13" runat="server" Text='<%#Eval("istasyongelis").ToString().Length>0? Eval("istasyongelis").ToString().Substring(0,2)+"/"+Eval("istasyongelis").ToString().Substring(11,5):""%>' ></asp:label></td>

                    </tr>    </table> </div>
              </itemtemplate>

            </asp:ListView>
            <br /><br />
        </asp:Panel>
      <asp:CollapsiblePanelExtender runat="server" TargetControlID="panelpilotgecy" ID="collapsepilotgecy" Enabled="true" CollapseControlID="LBpgecmisy" ExpandControlID="LBpgecmisy" CollapsedText="Show Jobs.." ExpandedText="Hide Jobs.." SuppressPostBack="True" Collapsed="True">
      </asp:CollapsiblePanelExtender>    </div>
            


            </ItemTemplate> <FooterTemplate></FooterTemplate>
        
        </asp:DataList>






 <div style="height:150px; clear:both; font-size:15px;  Color:#000000; float:left;">  <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="20px"></asp:Label>
</br>
</br><span style="height:60px; text-align:center; font-size:20px; color:yellow; background-color:black; font-weight:bold; font-family:'Trebuchet MS'">GÜNCELLEME :</span>

</br><b>** Fatigue hesaplamalarında manevra tipleri zorluk katsayıları kaldırılmıştır. Demirleme,kalkış,yanaşma,kalkıştan yanaşma,çift demirle yanaşma, herbirinin fatigue katsayısı eşittir.</b> 

<%--     <strong><asp:Label ID="Label3" runat="server"  BackColor=""  Text="GÜNCELLEME:" ForeColor="Black" Height="20px"></asp:Label></strong>
</br><b></b>  
     
     </br>

<table  class="trsatir2" style="text-align:center"><tr>
    <td style="background-color:#ffb3fd; height:40px; width:150px; text-align:center">% 0-20 </br> Efortless Working</td>
    <td style="background-color:#77ff6d; width:150px">% 20-25 </br> Easy Working</td>
    <td style="background-color:#ffff00; width:150px">% 25-30 </br> Normal Working</td>
    <td style="background-color:#ff7700; width:150px">% 30-35 </br> Hard Working</td>
    <td style="background-color:#ff0000; width:150px">% 35-40 </br> Overload Working</td>
    <td style="background-color:#505050; width:150px">% 40-... </br> Risky Working</td>
       </tr></table>    
     </br>
     
     Sistemin geliştirilmesi, varsa eksikliklerinin giderilmesi ve pilotların en uygun şekilde çalışmasının sağlanması hedeflenmektedir.  Bu amaçla; 
     sistem üzerinden gerektiğinde pilotların görüşünün alınabileceği AR-GE sayfası oluşturulmuştur. Yeni ArGe Anketi eklendiğinde, sistem girişten sonra otomatik olarak arge sayfasını
     açacak ve giriş yapan pilotun ankete katılması istenecektir. Eski/Yeni bütün ArGe sonuçlarına sayfanın üst orta kısımdaki AR-GE linkine tıklanarak ulaşılabilinecektir.
</br>Safiport yanaşma veya kalkış manevraları, fatique hesaplamasında demirleme manevrasına eşdeğerdir.

    
</br><span style="height:60px; text-align:center; font-size:20px; color:yellow; background-color:black; font-weight:bold; font-family:'Trebuchet MS'">GÜNCELLEME :</span>
</br>
    <span style="height:30px; font-family:'Trebuchet MS'">Yapılacak manevra için gemiye ait Liman başkanlığı ordinosu, Tehlikeli Yük bilgisi (IMDG) ve Romorkör Toplam Çekme Kuvveti (TBP) bilgilerine sistem üzerinden ulaşabilirsiniz.</span> 
     </br>
<span style="height:30px; font-size:14px; font-weight:bold; font-family:'Trebuchet MS'">Ordino :</span>Ekranda Geminin bayrak ismine tıklayarak görüntülenebilir.</br>
<span style="height:30px; font-size:14px; font-weight:bold; font-family:'Trebuchet MS'">IMDG: </span>Gemide tehlikeli yük olup olmadığı, Sütun olarak eklenmiştir.</br>
<span style="height:30px; font-size:14px; font-weight:bold; font-family:'Trebuchet MS'">TBP-T:  </span>TBP:Total Bollard Pull, Sütun olarak eklenmiştir. Manevra için gerekli olan minimum toplam römorkör çekme gücüdür. 
     T:(-) Tireden sonraki rakam minimum Römorkör sayısını gösterir. Bu değerler gemi defect'siz halde iken varsa indirim hakkı otomatik olarak sistem tarafından düşülerek hesaplanır.     
     (Bu hesap, gözcülerin sisteme yüklediği gemiye ait grt,loa,baş iter,kıç iter,tehlikeli yük bilgileri ile otomatik yapılmaktadır. Bilgilerde hata payı olabileceği unutulmamalıdır.)</br>
           --%>
     </br>
    
     </br>


     


        <asp:Button id="buttonshowpopuppadd" runat="server" style="display:none;" />
        <asp:Panel ID="panellbadd"   CssClass="panellbadd" runat="server"> 

<table  class="panellbadd">                        
                     <tr>
                <td class="tdorta"  >
          <div style="width:747px; height:561px; border:1px solid gray" >
              <asp:Image ID="Image1" runat="server"  />

          </div></td>
            </tr>      
    

            <tr>
                <td  class="tdorta"  style="height:50px; text-align:center; font-size:20px; font-weight:bold; font-family:'Trebuchet MS'">
                       <asp:Button ID="Buttonlbaddnotes" runat="server" style="height:40px; Width:120px" Text="Notes"  Font-Bold="true" BackColor="Yellow" OnClick="Buttonlbaddnotes_Click" />   
                       <asp:Button ID="Buttonlbaddcancel" runat="server" style="height:40px; Width:120px" Text="Cancel"  Font-Bold="true" BackColor="Yellow" OnClick="Buttonlbaddcancel_Click" />         
                </td></tr>         
           </table>

        </asp:Panel>
           
        <asp:ModalPopupExtender 
            ID="MPEdrawing" runat="server" 
            TargetControlID="buttonshowpopuppadd" 
            PopupControlID="panellbadd"
            BackgroundCssClass="modalbodyarka" ></asp:ModalPopupExtender>

        <br />


             <asp:Button id="buttonshowpopuppadd2" runat="server" style="display:none;" />
        <asp:Panel ID="panellbadd2"   CssClass="panellbadd" runat="server"> 

<table  class="panellbadd">                        
                     <tr>
                <td class="tdorta"  >
          <div style="width:747px; height:561px; border:1px solid gray; background-color:white;" >
<%--        Portinfo dök--%>

              <table > 
               <tr><td>
              <asp:Label ID="Lblnote0" runat="server" ></asp:Label><br />
                   </td></tr>
                                 <tr><td><asp:Label ID="Lblnote1" runat="server" ></asp:Label> /  <asp:Label ID="Lblnote2" runat="server" ></asp:Label> / 
                                      <asp:Label ID="Lblnote3" runat="server" ></asp:Label><br />
                   </td></tr>
               <tr>    <td>
              <asp:Label ID="Lblnote4" runat="server" ></asp:Label><br />
            </td>
            </tr></table>

          </div></td>
            </tr>      
    

            <tr>
                <td  class="tdorta"  style="height:50px; text-align:center; font-size:20px; font-weight:bold; font-family:'Trebuchet MS'">
                       <asp:Button ID="Buttonlbaddplan" runat="server" style="height:40px; Width:120px" Text="Plan"  Font-Bold="true" BackColor="Yellow" OnClick="Buttonlbaddplan_Click" />   
                       <asp:Button ID="Buttonlbaddcancel2" runat="server" style="height:40px; Width:120px" Text="Cancel"  Font-Bold="true" BackColor="Yellow" OnClick="Buttonlbaddcancel_Click" />         
                </td></tr>         
           </table>

        </asp:Panel>
           
        <asp:ModalPopupExtender 
            ID="MPEdrawing2" runat="server" 
            TargetControlID="buttonshowpopuppadd2" 
            PopupControlID="panellbadd2"
            BackgroundCssClass="modalbodyarka" ></asp:ModalPopupExtender>

        <br />


<asp:Label ID="Label2" runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label>
</div>
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
