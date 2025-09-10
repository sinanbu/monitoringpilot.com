<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
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
                #TBjurnotdaily {
            resize:none;
        }
                        #TBjurnotlast {
            resize:none;
        }
                        #TBDuyuru{
            resize:none;
        }

        .selected_row {
            background-color: #252a44;
            border-bottom-color: gray;
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


    </style>

    <script type="text/javascript" src="js/jquery-1.11.2.js"></script>
<script type="text/javascript">

    window.onload = SetScroll;

    function SetScroll() {
        var objDiv = document.getElementById("<%=UpdatePanelCanliekran.ClientID%>");
        objDiv.scrollTop = objDiv.scrollHeight;
    }



</script>

    <script type="text/javascript"> 

function textCounter(field, countfield, maxlimit)
{ 

if (field.value.length > maxlimit)
   field.value = field.value.substring(0, maxlimit);
else
   countfield.value = maxlimit - field.value.length;
}

</script>




</head>
<body onkeydown = "return (event.keyCode!=13)" >
    <form id="form1" runat="server" >

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

           <table  style="width:1340px; color:#111; font-weight:bold; height:25px;  z-index:10; background-image:url(images/boslukaltcizgi.png)"><tr  style="height:25px">
            <td style="text-align:right; width:62%;"> 
          <asp:Label ID="LblVarid" ForeColor="Red" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarid" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarno" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarno" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarbasla" runat="server" Text=""></asp:Label><asp:Literal ID="varbaslar" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarbit" runat="server" Text=""></asp:Label> <asp:Literal ID="varbiter" runat="server" Text="" Visible="false"></asp:Literal> 
                <asp:Literal ID="Litlastup" runat="server" Text="" Visible="false"></asp:Literal> 
            </td><td  style="text-align:left;  width:28%;">  <asp:Panel ID="Panel1" runat="server">
<asp:UpdatePanel ID="UpdatePanelHeader" runat="server" UpdateMode="Conditional" ><ContentTemplate>
                
                    <asp:Timer ID="gerisayimtik" runat="server" OnTick="gerisayimtik_Tick"  Interval="120000"></asp:Timer>
<asp:Label ID="LblReeltime" runat="server" Text=""></asp:Label>
 </ContentTemplate></asp:UpdatePanel></asp:Panel> 
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
                    <li id="mainmanu7" runat="server"><a href="mapsection.aspx" >Map</a></li>
                </ul>
            </li>
        
        </ul>
    </div> 



 
</td></tr>
</table>


    <div  style="width:100%;  z-index:10;  text-align:left;">
     <asp:Panel ID="Panel2" runat="server">
 <asp:UpdatePanel ID="UpdatePanelCanliekran" runat="server" UpdateMode="Conditional" ><ContentTemplate>



<table  style="width:1340px; float:right;  color:white">
 <tr  style="height:22px;  border-color:red; ">
     <td style="text-align:left; width:38%;">
                <button runat="server"  id="Bdaricaships" style=" border:1px solid black;   cursor:pointer;  background-color:#7DA3CD; height: 25px; Width: 90px; font-size: 11px; text-align: center;"  onclick="window.location.href='daricaships.aspx'" >Darıca Ships</button>&nbsp;
<%--                <button runat="server" id="Byalovaships" style="border: 1px solid black; cursor: pointer; background-color: #9FF3A1; height: 25px; Width: 90px; font-size: 11px; text-align: center;" onclick="window.location.href='yalovaships.aspx'">Yalova Ships</button>&nbsp;--%>
                <button runat="server" id="Byarimcaships" style=" border:1px solid black;   cursor:pointer;  background-color:#9B86B5; height: 25px; Width: 90px; font-size: 11px; text-align: center;"  onclick="window.location.href='yarimcaships.aspx'" >Yarımca Ships</button>&nbsp;
                <asp:Button ID="ButtonRefresh" style=" border:1px solid black; cursor: pointer; background-color:white; height:25px; Width:90px; font-size:11px;  text-align:center" runat="server" Text="Refresh" OnClick="ButtonRefresh_Click"   UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" /> 
</td>

     <td style="text-align:center;  width:24%;">
                <asp:Button ID="ButtonVacation" style="height:25px; Width:90px; font-size:11px;  cursor: pointer; vertical-align:text-bottom; text-align:center" runat="server" Text="OnLeave"  OnClick="ButtonVacation_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"  /> &nbsp;&nbsp;               
                <asp:Button ID="ButtonTKVYsp" style="height:25px; Width:90px; font-size:11px;  cursor: pointer; vertical-align:text-bottom; text-align:center" runat="server" Text="Reinforcement"  OnClick="ButtonTKVYsp_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"  /> &nbsp;&nbsp;
                <asp:Button ID="ButtonChangeWatch" style="height:25px; Width:90px; font-size:11px;  cursor: pointer; vertical-align:text-bottom;  text-align:center" runat="server" Text="Change Watch"  OnClick="ButtonChangeWatch_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"  /> &nbsp;&nbsp;
  </td>  <td style="text-align:right;  width:38%;">&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="LBonline" runat="server"  ForeColor="#111" OnClick="LBonline_Click"  OnClientClick="this.disabled='true'; "  ></asp:LinkButton> &nbsp;&nbsp
                <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="#111"  OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "  ></asp:LinkButton>&nbsp;&nbsp;
 </td></tr></table>  


    <table  style="width:100%; float:left; color:white">
<tr  style="height:22px; "><td  style="text-align:left; font-size:12px; color:#1111ee; width:1140px;"><b>OnLeave(Changing/AddedPilot) :</b> <asp:Label ID="Lblizinliler" runat="server" Text=""></asp:Label>
    <asp:Label ID="Litizinler" runat="server" Text=""></asp:Label></td><td style="text-align:right; font-size:12px; color:#1111ee; width:200px;"> <asp:Label ID="Lblsaat" runat="server"   style="font-size:24px; font-family:'OCR A Std'; text-align:left; width:200px; color:#1111ee;"></asp:Label> </td></tr>

<tr  style="height:22px"><td  style="text-align:left">
    <asp:LinkButton ID="LBDuyuru" runat="server" OnClick="LBDuyuru_Click" Font-Underline="false" ForeColor="#1111ee" ><b>Notice :</b></asp:LinkButton> 
    <asp:Label ID="LblDuyuru" runat="server"  Font-Size="12px" ForeColor="#660066" Text=""></asp:Label>
    <asp:TextBox ID="TBDuyuru" runat="server" MaxLength="700" BorderWidth="0px" BackColor="#ffFFFF" Visible="false" Height="40px" Width="1180px"  TextMode="MultiLine" Text="" onkeyup="textCounter(TBDuyuru, this.form.remLen, 700);" onkeydown="textCounter(TBDuyuru, this.form.remLen, 700);" ></asp:TextBox> <br/>
    <asp:LinkButton ID="LBDkaydet" Visible="false" OnClick="LBDkaydet_Click"   OnClientClick="this.disabled='true'; "  BackColor="gray" BorderColor="white" Height="20px" ForeColor="#111111" BorderStyle="Solid" BorderWidth="1px" Font-Underline="false" runat="server">&nbsp&nbsp Save &nbsp&nbsp</asp:LinkButton>
    <asp:LinkButton ID="LBDCancel" Visible="false" OnClick="LBDCancel_Click" BackColor="GRAY" BorderColor="white" Height="20px" ForeColor="#111111" BorderStyle="Solid" BorderWidth="1px" Font-Underline="false" runat="server">&nbsp&nbsp Cancel &nbsp&nbsp</asp:LinkButton> &nbsp&nbsp &nbsp&nbsp <asp:TextBox ID="remLen" runat="server" MaxLength="700" BorderWidth="0px"  Height="20px" Width="25px" BackColor="#ffFFFF" Enabled="false" Visible="false"></asp:TextBox><asp:Label ID="LBLleftch" ForeColor="Black" runat="server" Text=" characters left."  Visible="false"></asp:Label>
    </td></tr>


        <tr  style="height:22px"><td  style="text-align:left">
    <asp:LinkButton ID="LBjdaily" runat="server" OnClick="LBjdaily_Click" Font-Underline="false" ForeColor="#ee1111" ><b>Daily Jurnal :</b></asp:LinkButton> 
    <asp:Label ID="LBLjdaily" runat="server"  Font-Size="12px" ForeColor="#660066" Text=""></asp:Label>
<asp:TextBox ID="TBjurnotdaily"  runat="server" BorderWidth="0px" BackColor="#ffFFFF"  MaxLength="700" Visible="False"  Height="40px" Width="1180px"  TextMode="MultiLine"  onkeyup="textCounter(TBjurnotdaily, this.form.remLen2, 700);" onkeydown="textCounter(TBjurnotdaily, this.form.remLen2, 700);" ></asp:TextBox><br/>
    <asp:LinkButton ID="LBjkaydet" Visible="false"  OnClick="LBjkaydet_Click"  OnClientClick="this.disabled='true'; "  BackColor="gray" BorderColor="white" Height="20px" ForeColor="#111111" BorderStyle="Solid" BorderWidth="1px" Font-Underline="false" runat="server">&nbsp&nbsp Save &nbsp&nbsp</asp:LinkButton>
    <asp:LinkButton ID="LBjCancel" Visible="false" OnClick="LBjCancel_Click"  BackColor="GRAY" BorderColor="white" Height="20px" ForeColor="#111111" BorderStyle="Solid" BorderWidth="1px" Font-Underline="false" runat="server">&nbsp&nbsp Cancel &nbsp&nbsp</asp:LinkButton> &nbsp&nbsp &nbsp&nbsp <asp:TextBox ID="remLen2" runat="server" MaxLength="700" BorderWidth="0px"  Height="20px" Width="25px" BackColor="#ffFFFF" Enabled="false" Visible="false"></asp:TextBox><asp:Label ID="LBLleftchdaily" ForeColor="Black" runat="server" Text=" characters left."  Visible="false"></asp:Label>
    </td></tr>

        


    </table>  
                    


<table  style="width:100% ;">
<tr><td style="text-align:left; width:100% ;"><span class="tablobaslik1" style="cursor:default;"><b>DARICA</b>&nbsp;/&nbsp;
    <asp:Label ID="LBonlined" runat="server"></asp:Label></span><asp:label  ID="LblNosayd" runat="server" Visible="false" ></asp:label></td>
</tr> </table>  

        <asp:DataList ID="DLDarica"  runat="server" Width="1340px"  CellPadding="2" HorizontalAlign="Left" OnItemDataBound="DLDarica_ItemDataBound" OnItemCommand="DLDarica_ItemCommand">

           <HeaderTemplate>
                                    <table  class="tablo1">
                                        <tr class="trbaslik1" >
                        <td style="width:2% ">No</td>
                        <td style="width:10% ">Pilot Name</td>
                        <td style="width:4% ">Fatig.</td>
                        <td style="width:12% ">Ship Name</td>
                        <td style="width:4% ">Grt</td>
                        <td style="width:3% ">Typ</td>
                        <td style="width:5% ">Agent</td>
                        <td style="width:12% ">Departure</td>
                        <td style="width:12% ">Arrival</td>
                        <td style="width:11% ">Tugs</td>
                        <td style="text-align:center; width:6% ">OffStation</td>
                        <td style="text-align:center; width:6% ">POB</td>
                        <td style="text-align:center; width:6% ">POff</td>
                        <td style="text-align:center; width:6% ">OnStation</td>
                        <td style="text-align:center; width:6% ">Process</td>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                        <tr  class="trsatir1">
                        <td  style="width:2% "><asp:label  ID="LblNo" runat="server" ForeColor="Black" Text='<%# Container.ItemIndex +1 %>' ></asp:label></td>
                        <td  style="width:10% " >
                            <asp:label  ID="lblDurum" runat="server" Text='<%#Eval("durum")%>' Visible="false" ></asp:label>
                            <asp:label ID="LBpgecmis" runat="server" style=" text-decoration:none;"  Font-Italic='<%#(Eval("kidem").ToString())=="5"? true : false%>' Font-Bold='<%#Convert.ToInt32((Eval("kidem")))>2? true : false%>'><%#Eval("degismeciadisoyadi")%></asp:label>
                            <asp:label  ID="LblKapno" runat="server" Text='<%#Eval("kapno")%>' Visible="False"></asp:label>
                            <asp:label  ID="Lblkidem" runat="server" Text='<%#Eval("kidem")%>' Visible="False"></asp:label>
                        </td>
                        <td  style=" width:4% ; "><asp:label ID="Lblyorulma" runat="server"  Text='<%#Eval("yorulma")%>'></asp:label><asp:label ID="Lbllastday" runat="server"  Text='<%#Eval("lastday")%>' Visible="false"></asp:label></td>
                        <td  style=" width:12% ; ">
<asp:LinkButton ID="Lblgemiadi"  runat="server" CssClass="butongemi1" Font-Underline="false"  Text='<%#Eval("gemiadi")%>' ToolTip="" CommandName="jurnot"  ></asp:LinkButton>
                            
                            <asp:label ID="Lblimono" runat="server" Visible="false" Text='<%#Eval("imono")%>' ></asp:label></td>
                       
                        <td  style=" width:4% ; "><%#Eval("grt")%></td>
                        <td  style=" width:3% ; "><%#Eval("tip").ToString().Length>1? (Eval("tip") as string).Substring(0,3) : Eval("tip")%></td>
                         <td  style=" width:5% ; "><asp:label ID="Lblacente" runat="server" Text="" ToolTip=""></asp:label></td>
                        <td  style=" width:12% ; "><asp:LinkButton ID="binisport" runat="server" Text='<%# Bind("binisyeri") %>'  CommandName="linkleb" CssClass="tablobaslik1"></asp:LinkButton> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %>   </td>
                        <td  style=" width:12% ; "><asp:LinkButton ID="inisport" runat="server" Text='<%# Bind("inisyeri") %>'  CommandName="linklei" CssClass="tablobaslik1"></asp:LinkButton> <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %><asp:label ID="Lblinisyerigiz" runat="server"  Visible="false" Text='<%#Eval("inisyeri")%>'></asp:label></td>

                        <td  style=" width:11% ; ">
                            <%#Eval("rom1")!=null && Eval("rom1").ToString()!="" && Eval("rom1").ToString()!="..." ? Eval("rom1") : ""%>
                            <%#Eval("rom2")!=null && Eval("rom2").ToString()!="" && Eval("rom2").ToString()!="..." ? "," + Eval("rom2") : ""%>
                            <%#Eval("rom3")!=null && Eval("rom3").ToString()!="" && Eval("rom3").ToString()!="..." ? "," +  Eval("rom3") : ""%>
                            <%#Eval("rom4")!=null && Eval("rom4").ToString()!="" && Eval("rom4").ToString()!="..." ? "," +  Eval("rom4") : ""%>
                            <%#Eval("rom5")!=null && Eval("rom5").ToString()!="" && Eval("rom5").ToString()!="..." ? ",[" +  Eval("rom5")+ "]" : ""%>
                        </td>

                        <td  style="text-align:center; width:5%  ">
                           <asp:LinkButton ID="BtnIstasyoncikis"  runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>' CommandArgument='<%#Eval("kapno")%>'  ToolTip="Departed from Station"  CommandName="Istasyoncikis"  CssClass="islemtuslari1" ></asp:LinkButton>
                            <asp:label  ID="LblIstasyoncikis" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>' ></asp:label>
                             <asp:label  ID="LblIstasyoncikiseta" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>'  ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td  style="text-align:center; width:5%  ">
                            <asp:LinkButton ID="BtnPob" runat="server"  Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>' CommandArgument='<%#Eval("kapno") %>' CommandName="Gemiyebinis"  ToolTip="Boarding"  CssClass="islemtuslari1"  ></asp:LinkButton>
                                        <asp:label  ID="LblPob" runat="server"   Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>' ></asp:label>
                                        <asp:label  ID="LblPobeta" runat="server" Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td  style="text-align:center; width:5%  ">
                            <asp:LinkButton ID="BtnPoff" runat="server" Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>' CommandArgument='<%#Eval("kapno")%>' CommandName="Gemideninis"  ToolTip="Leaving from vessel"   CssClass="islemtuslari1"  ></asp:LinkButton>
                               <asp:label  ID="LblPoff" runat="server"  Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>' ></asp:label>
                            <asp:label  ID="LblPoffeta" runat="server" Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td  style="text-align:center; width:5%  ">
                            <asp:LinkButton ID="BtnIstasyongelis" runat="server"  Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>' CommandArgument='<%#Eval("kapno")%>' CommandName="Istasyongelis"  ToolTip="Arrived Station"   CssClass="islemtuslari1"  ></asp:LinkButton>
                                <asp:label  ID="LblIstasyongelis" runat="server"  Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>' ></asp:label>
                             <asp:label  ID="LblIstasyongeliseta" runat="server" Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>'  ForeColor="#aaaaaa"></asp:label>
                        </td>                                                           
                        <td  style="text-align:center; width:5%  ">
                            <asp:LinkButton ID="BtnProcessiptal" runat="server" Text="Cancel" CommandArgument='<%#Eval("kapno")%>' CommandName="Processiptal"  ToolTip="Cancel Job"  CssClass="islemtuslari1"  ></asp:LinkButton>
                            <asp:LinkButton ID="BtnProcessChgStn" runat="server" Text="CS" CommandArgument='<%#Eval("kapno")%>' CommandName="ProcessChgStn" ToolTip="Change Station"  CssClass="islemtuslari1"  ></asp:LinkButton>
                            <asp:LinkButton ID="BtnProcessChgDest" runat="server" Text="CD" CommandArgument='<%#Eval("kapno")%>' CommandName="BtnProcessChgDest" ToolTip="Change Destination"  CssClass="islemtuslari1"  ></asp:LinkButton>

                            </td>

                    </tr>
    
            </ItemTemplate>
        
            <FooterTemplate></table></FooterTemplate>
        
        </asp:DataList>




 <table  style="width:100%">
<tr><td style="text-align:left; width:100% "><br /><span  class="tablobaslik0"  style="cursor:default;"><b>YARIMCA</b>&nbsp;/&nbsp;
    <asp:Label ID="LBonliney" runat="server"></asp:Label></span><asp:label  ID="LblNosayy" runat="server" Visible="false" ></asp:label></td>
</tr> </table>
            
        <asp:DataList ID="DLDaricay" runat="server" Width="1340px"  CellPadding="2" HorizontalAlign="Left" OnItemDataBound="DLDaricay_ItemDataBound" OnItemCommand="DLDaricay_ItemCommand">

          <HeaderTemplate>
                                    <table  class="tablo0">
                                        <tr  class="trbaslik0">
                        <td style="width:2% ">No</td>
                        <td style="width:10% ">Pilot Name</td>
                        <td style="width:4% ">Fatig.</td>
                        <td style="width:12% ">Ship Name</td>
                        <td style="width:4% ">Grt</td>
                        <td style="width:3% ">Typ</td>
                        <td style="width:5% ">Agent</td>
                        <td style="width:12% ">Departure</td>
                        <td style="width:12% ">Arrival</td>
                        <td style="width:11% ">Tugs</td>
                        <td style="text-align:center; width:6% ">OffStation</td>
                        <td style="text-align:center; width:6% ">POB</td>
                        <td style="text-align:center; width:6% ">POff</td>
                        <td style="text-align:center; width:6% ">OnStation</td>
                        <td style="text-align:center; width:6% ">Process</td>
                    </tr>
            </HeaderTemplate>
   
            <ItemTemplate>
                       <tr  class="trsatir0">
                        <td  style="width:2% "><asp:label  ID="LblNoy" runat="server" ForeColor="Black" Text='<%# Container.ItemIndex +1 %>' ></asp:label></td>
                        <td  style="width:10% ">
                            <asp:label  ID="lblDurumy" runat="server" Text='<%#Eval("durum")%>' Visible="false" ></asp:label>
                            <asp:label ID="LBpgecmisy" runat="server"  style="text-decoration:none; "   Font-Italic='<%#(Eval("kidem").ToString())=="5"? true : false%>' Font-Bold='<%#Convert.ToInt32((Eval("kidem")))>2? true : false%>'><%#Eval("degismeciadisoyadi")%></asp:label>
                            <asp:label  ID="LblKapnoy" runat="server" Text='<%#Eval("kapno")%>' Visible="False"></asp:label>
                            <asp:label  ID="Lblkidemy" runat="server" Text='<%#Eval("kidem")%>' Visible="False"></asp:label>
                        </td>
                        <td  style=" width:4% ; "><asp:label ID="Lblyorulmay" runat="server" Text='<%#Eval("yorulma")%>'></asp:label><asp:label ID="Lbllastdayy" runat="server" Visible="false" Text='<%#Eval("lastday")%>'></asp:label></td>
                        <td  style=" width:12%  ; "">
                            <asp:LinkButton ID="Lblgemiadiy"  runat="server" CssClass="butongemi0" Font-Underline="false"  Text='<%#Eval("gemiadi")%>' ToolTip=""  CommandName="jurnot"   ></asp:LinkButton>
                            <asp:label ID="Lblimonoy" runat="server" Visible="false" Text='<%#Eval("imono")%>' ></asp:label></td>
                        <td  style=" width:4%  ; ""><%#Eval("grt")%></td>
                        <td  style=" width:3%  ; ""><%#Eval("tip").ToString().Length>1? (Eval("tip") as string).Substring(0,3) : Eval("tip")%></td>
                          <td  style=" width:5% ; "><asp:label ID="Lblacente" runat="server" Text="" ToolTip=""></asp:label></td>
                        <td   style=" width:12%  ; ""><asp:LinkButton ID="binisport" runat="server" Text='<%# Bind("binisyeri") %>'  CommandName="linkleb" CssClass="tablobaslik0"></asp:LinkButton> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %>   </td>
                        <td   style=" width:12%  ; ""><asp:LinkButton ID="inisport" runat="server" Text='<%# Bind("inisyeri") %>'  CommandName="linklei" CssClass="tablobaslik0"></asp:LinkButton> <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %><asp:label ID="Lblinisyerigizy" runat="server"  Visible="false" Text='<%#Eval("inisyeri")%>'></asp:label></td>

                        <td  style=" width:11% ; ">
                            <%#Eval("rom1")!=null && Eval("rom1").ToString()!="" && Eval("rom1").ToString()!="..." ? Eval("rom1") : ""%>
                            <%#Eval("rom2")!=null && Eval("rom2").ToString()!="" && Eval("rom2").ToString()!="..." ? "," + Eval("rom2") : ""%>
                            <%#Eval("rom3")!=null && Eval("rom3").ToString()!="" && Eval("rom3").ToString()!="..." ? "," +  Eval("rom3") : ""%>
                            <%#Eval("rom4")!=null && Eval("rom4").ToString()!="" && Eval("rom4").ToString()!="..." ? "," +  Eval("rom4") : ""%>
                            <%#Eval("rom5")!=null && Eval("rom5").ToString()!="" && Eval("rom5").ToString()!="..." ? ",[" +  Eval("rom5")+ "]" : ""%>

                        </td>
                        <td   style="text-align:center; width:6% ">
                            <asp:LinkButton ID="BtnIstasyoncikisy" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>' CommandArgument='<%#Eval("kapno")%>'  ToolTip="Departed from Station"  CommandName="Istasyoncikis"  CssClass="islemtuslari0" ></asp:LinkButton>
                                <asp:label  ID="LblIstasyoncikisy" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>' ></asp:label>
                             <asp:label  ID="LblIstasyoncikisetay" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td   style="text-align:center; width:6% ">
                            <asp:LinkButton ID="BtnPoby" runat="server"            Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>' CommandArgument='<%#Eval("kapno") %>' CommandName="Gemiyebinis"  ToolTip="Boarding"  CssClass="islemtuslari0"  ></asp:LinkButton>
                             <asp:label  ID="LblPoby" runat="server"   Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>' ></asp:label>
                             <asp:label  ID="LblPobetay" runat="server" Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td   style="text-align:center; width:6% ">
                           <asp:LinkButton ID="BtnPoffy" runat="server" Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>' CommandArgument='<%#Eval("kapno")%>' CommandName="Gemideninis"  ToolTip="Leaving from vessel"   CssClass="islemtuslari0"  ></asp:LinkButton>
                               <asp:label  ID="LblPoffy" runat="server"  Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>' ></asp:label>
                            <asp:label  ID="LblPoffetay" runat="server" Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td   style="text-align:center; width:6% ">
                            <asp:LinkButton ID="BtnIstasyongelisy" runat="server" Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>' CommandArgument='<%#Eval("kapno")%>' CommandName="Istasyongelis"  ToolTip="Arrived Station"   CssClass="islemtuslari0"  ></asp:LinkButton>
                                <asp:label  ID="LblIstasyongelisy"   runat="server" Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>' ></asp:label>
                             <asp:label  ID="LblIstasyongelisetay" runat="server" Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>'  ForeColor="#aaaaaa"></asp:label>
                        </td>                                                           
                        <td   style="text-align:center; width:6% ">
                            <asp:LinkButton ID="BtnProcessiptaly" runat="server" Text="Cancel" CommandArgument='<%#Eval("kapno")%>' CommandName="Processiptal"  ToolTip="Cancel Job"  CssClass="islemtuslari0"  ></asp:LinkButton>
                            <asp:LinkButton ID="BtnProcessChgStny"   runat="server" Text="CS" CommandArgument='<%#Eval("kapno")%>' CommandName="ProcessChgStn" ToolTip="Change Station"  CssClass="islemtuslari0" ></asp:LinkButton>
                            <asp:LinkButton ID="BtnProcessChgDesty" runat="server" Text="CD" CommandArgument='<%#Eval("kapno")%>' CommandName="BtnProcessChgDest" ToolTip="Change Destination"  CssClass="islemtuslari0" ></asp:LinkButton>

                            </td>

                    </tr>
    
            </ItemTemplate> <FooterTemplate></table></FooterTemplate>
        
        </asp:DataList>



<%-- modal message ok başlar    --%>
 <asp:Button id="buttonMessagePanelok" runat="server" style="display:none;" />
        <asp:Panel ID="panelMessagePanelok"   CssClass="panelmessagevarch" runat="server"   Style="display:none; ">  
    <div style="text-align:center; margin-top:10px;">  <span  style="text-align:center; font-weight:bold;  font-size:14px;"> <br />WATCH CHANGING.  </span> <br /> <br />  <br /> 
The Watch has been finished and changed with next one.<br /> PLEASE CONFIRM !  <br /> <br /></div>


            
        <div  style="clear:both; font-size:11px;">  <br /> 
    
          &nbsp;&nbsp;&nbsp;

               <asp:Label ID="baslik1" runat="server">  <div  class="yazibigbold" style="text-align:center"> <p></p>
                         Watch No:<asp:Label ID="Lwid" runat="server" ForeColor="Red" Text=""></asp:Label>
                         &nbsp;&nbsp;&nbsp;&nbsp;  Finished: <asp:Label ID="Lwfinish" runat="server" ForeColor="Red"  Text=""></asp:Label>
                     </div></asp:Label><br />
 
 

        <div  style="clear:both; ">       
<br /><br /> <br /> 
            <asp:Button ID="BWCacceptedok" runat="server" style="height:30px; Width:180px" Text="Confirmed Watch Finished"  OnClick="BWCacceptedok_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" />&nbsp;&nbsp; 
            <asp:Button ID="Bclosedcnl" runat="server" style="height:30px; Width:80px" Text="Cancel"  />  <br />
<br /> 

<asp:UpdateProgress ID="UpdateProgress1r"  runat="server">
                <ProgressTemplate><div><img src="images/progress.gif" /></div>
                </ProgressTemplate></asp:UpdateProgress><br />


        </div>
                </div>





        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopupWatch" runat="server" TargetControlID="buttonMessagePanelok" PopupControlID="panelMessagePanelok" BackgroundCssClass="modalbodyarka" CancelControlID="Bclosedcnl"  ></asp:ModalPopupExtender>
<%--modal message ok biter --%>


      
<%-- modal message başlar --%>
 <asp:Button id="buttonMessagePanel" runat="server" style="display:none;" />
        <asp:Panel ID="panelMessagePanel"   CssClass="panelmessage" runat="server"   Style="display: none; ">  
    <div style="text-align:center; font-weight:bold;">        
<br /> <asp:Literal ID="Litmodalmesssage" runat="server" Text=""></asp:Literal>
         <br /><br /></div>
            <asp:Button ID="Baccepted" runat="server" style="height:30px; Width:80px" Text="OK"  OnClick="Baccepted_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" />&nbsp;&nbsp; 
            <asp:Button ID="Bclosed" runat="server" style="height:30px; Width:80px"  Text="Close"  />  
<br /><br />
        </asp:Panel>


        <asp:ModalPopupExtender ID="ModalPopupMessage" runat="server" TargetControlID="buttonMessagePanel" PopupControlID="panelMessagePanel" BackgroundCssClass="modalbodyarka" CancelControlID="Bclosed" ></asp:ModalPopupExtender>
<%-- modal message biter --%>


<%-- istasyon değiştirme ve onay messageları hepsi birarada, Baccepted  butonuna onclick olayları farklı --%>
 <asp:Button id="buttononayMessagePanel" runat="server" style="display:none;" /> 
        <asp:Panel ID="panelonayMessagePanel"    CssClass="panelmessage" runat="server"   Style="display: none; "> 
    <div style="text-align:center; font-weight:bold;"> <br /> <asp:Literal ID="Litmodstnmes"  runat="server" Text=""></asp:Literal> <br /><br />
        <asp:TextBox ID="Lblprocesstime" Height="25px"  Font-Size="medium" runat="server" Width="160px"></asp:TextBox><asp:MaskedEditExtender  CultureName="tr-TR"  ID="MaskedEditExtender3" runat="server" TargetControlID="Lblprocesstime" ErrorTooltipEnabled="true" MaskType="DateTime" Mask="99/99/9999 99:99"></asp:MaskedEditExtender><br />  <br /></div>

<asp:Button ID="BacpCSok"   runat="server" style="height:30px; Width:80px" Text="Yes"    OnClick="BacpCSok_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..'; "  />&nbsp;&nbsp; 
            <asp:Button ID="BacpCScancel" runat="server" style="height:30px; Width:80px" Text="Cancel"  />  
<br /><br /> <br />
            <asp:Literal ID="LtCjobText" Visible="false"  runat="server" Text="If Job Canceled:" ></asp:Literal>
<asp:DropDownList ID="DDCjob" runat="server" Visible="false" Width="50px" Height="19px"  OnSelectedIndexChanged="DDCjob_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> 
    <asp:TextBox ID="DDCjobReason" Width="200px"  BackColor="LightGreen" BorderStyle="Solid" BorderColor="ForestGreen" BorderWidth="1" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
      <br /><br />                           



        </asp:Panel>                            
        <asp:ModalPopupExtender ID="ModalPopupCSonayMessage" runat="server" TargetControlID="buttononayMessagePanel" PopupControlID="panelonayMessagePanel" BackgroundCssClass="modalbodyarka" CancelControlID="BacpCScancel" ></asp:ModalPopupExtender>



<%-- modal onay message biter --%>


<%-- Dest değiştirme --%>
 <asp:Button id="buttononayMessagePanelcd" runat="server" style="display:none;" /> 
        <asp:Panel ID="panelonayMessagePanelcd"    CssClass="panelmessagecd" runat="server"   Style="display: none; ">  
    <div style="text-align:center; font-weight:bold;">        
<br /> <asp:Literal ID="Litmodstnmescd"  runat="server" Text=""></asp:Literal><br /><br /></div>

<asp:DropDownList ID="DDLdesplace"  runat="server" Height="21px" Width="154px"  OnSelectedIndexChanged="DDLdesplace_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
<asp:DropDownList ID="DDLdesplaceno"  runat="server" Height="21px" Width="100px" ></asp:DropDownList><br /><br />

            <asp:Button ID="BacpCSokcd" CommandArgument="" runat="server" style="height:30px; Width:80px" Text="Yes"   OnClick="BacpCSokcd_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"  />&nbsp;&nbsp; 
            <asp:Button ID="BacpCScancelcd" runat="server" style="height:30px; Width:80px" Text="Cancel" />  
<br /><br />
        </asp:Panel>
        <asp:ModalPopupExtender ID="MPCDonay" runat="server" TargetControlID="buttononayMessagePanelcd" PopupControlID="panelonayMessagePanelcd" BackgroundCssClass="modalbodyarka" CancelControlID="BacpCScancelcd" ></asp:ModalPopupExtender>
<%-- modal onay message biter --%>



      <%-- modal Takviye başlar    --%>
 <asp:Button id="buttonTakviye" runat="server" style="display:none;" />
        <asp:Panel ID="panelTakviye"   CssClass="paneltakviye" runat="server"   Style="display: none; ">  
    <div style="text-align:center; font-weight:bold;">        
<br /><strong>SEND REINFORCEMENT</strong> <br /><br />
         <table  style="width:100%; padding:2px;" >
             <tr>
             <td>Darıca Station</td><td></td><td>Yarımca Station</td></tr>
             <tr>
             <td  style="width:45%;" ><asp:ListBox ID="ListBoxDarica" Height="250px" Width="180px" runat="server"  SelectionMode="Multiple"></asp:ListBox></td>
             <td  style="width:10%;" >
                            <asp:LinkButton ID="LBtakmover"  OnClick="LBtakmover_Click" runat="server" Font-Underline="false"><img id="takokr" src="images/takokr.png" /></asp:LinkButton><br /><br />
                            <asp:LinkButton ID="LBtakmovel"  OnClick="LBtakmovel_Click" runat="server" Font-Underline="false"><img id="takokl" src="images/takokl.png" /></asp:LinkButton></td>
             <td  style="width:45%;" ><asp:ListBox ID="ListBoxYarimca"  Height="250px" Width="180px" runat="server" SelectionMode="Multiple"></asp:ListBox>
                 </td></tr></table> 
            <br />
        <asp:Literal ID="LitTakdeptime"  runat="server" Text=""></asp:Literal> <asp:TextBox ID="TBtaktaleptime"  runat="server" Width="120px"></asp:TextBox><asp:MaskedEditExtender  CultureName="tr-TR"  ID="MaskedEditExtender1" runat="server" TargetControlID="TBtaktaleptime" ErrorTooltipEnabled="true" MaskType="DateTime" DisplayMoney="Left"  AcceptNegative="Left" Mask="99/99/9999 99:99"></asp:MaskedEditExtender>
            <br /><br />
            <asp:Button ID="ButtonTiste" runat="server" style="height:30px; Width:150px" Text="Require Reinforcement"  OnClick="ButtonTiste_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" />
            <asp:Button ID="ButtonTistekiptal" runat="server" style="height:30px; Width:150px" Text="No Need Reinforcement"  OnClick="ButtonTistekiptal_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" />
            <asp:Button ID="ButtonTgonder" runat="server" style="height:30px; Width:80px" Text="Send"  OnClick="ButtonTgonder_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" />&nbsp;&nbsp; 
            <asp:Button ID="ButtonTiptal" runat="server" style="height:30px; Width:80px" Text="Cancel"  />  
<br />
        <asp:Label ForeColor="Red" ID="LitTakuyari"  Font-Size="X-Small" runat="server" Visible="false" Text=""></asp:Label> <br /> </div>
        </asp:Panel>
<asp:ModalPopupExtender ID="ModalPopupTakviye" runat="server" TargetControlID="buttonTakviye" PopupControlID="panelTakviye" BackgroundCssClass="modalbodyarka" CancelControlID="ButtonTiptal" ></asp:ModalPopupExtender>
<%-- modal Takviye biter --%>


      <%-- modal izinli başlar    --%>
 <asp:Button id="Buttonizinli" runat="server" style="display:none;" />
        <asp:Panel ID="panelizinli"   CssClass="panelizinli" runat="server"   Style="display: none; ">  
    <div style="text-align:center; font-weight:bold;"> <br /><asp:Label runat="server" Width="90%" BackColor="LightBlue" ><strong>ON-LEAVE / REPLACED / OFFDUTY / ADD PILOTS</strong></asp:Label> <br /><br />
       
          <table  style="width:100%; padding:2px; text-align:center" >
             <tr><td colspan="2">
                 <asp:RadioButton ID="wac1" runat="server" AutoPostBack="true" OnCheckedChanged="wac1_CheckedChanged" Text="1.Watch" GroupName="vardiya" />&nbsp;&nbsp;&nbsp;
                 <asp:RadioButton ID="wac2" runat="server"  AutoPostBack="true"  OnCheckedChanged="wac2_CheckedChanged" Text="2.Watch"  GroupName="vardiya"/>&nbsp;&nbsp;&nbsp;
                 <asp:RadioButton ID="wac3" runat="server"  AutoPostBack="true"  OnCheckedChanged="wac3_CheckedChanged" Text="3.Watch" GroupName="vardiya"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:CheckBox ID="CBaddpilot" Checked="false" Text="Add Pilot" OnCheckedChanged="CBaddpilot_CheckedChanged" AutoPostBack="true"  runat="server" /> 
                 </td>
             </tr>
              <tr><td colspan="2">&nbsp; </br></td></tr>
             <tr><td>Onleave Pilot</td>
                 <td>Replacing Pilot</td>
             </tr>

             <tr><td ><asp:DropDownList ID="DDLizinlifull" AutoPostBack="true"  OnSelectedIndexChanged="DDLizinlifull_SelectedIndexChanged" runat="server" Height="21px" Width="200px" ></asp:DropDownList><br /><br /></td>
                 <td> <asp:DropDownList ID="DDLdegisfull" AutoPostBack="true"  OnSelectedIndexChanged="DDLdegisfull_SelectedIndexChanged" runat="server" Height="21px" Width="200px"></asp:DropDownList><br /><br /></td>
            </tr>

            <tr><td><asp:ListBox ID="ListBizin" Height="160px" Width="200px" runat="server" SelectionMode="Single" AutoPostBack="true" OnSelectedIndexChanged="ListBizin_SelectedIndexChanged"></asp:ListBox></td>
                <td><asp:ListBox ID="ListBdegis" Height="160px" Width="200px" runat="server" SelectionMode="Single" AutoPostBack="true" OnSelectedIndexChanged="ListBdegis_SelectedIndexChanged"></asp:ListBox></td>
            </tr>         </table> 

            <br />
        <table  style="width:91%; font-size:10px; margin-left:auto; margin-right:auto;" ><td><td style="width:25%; background-color:white;  border:1px solid black; " >Onleave</td><td style="width:25%; background-color:yellow;  border:1px solid black; " >Replaced</td><td style="width:25%; background-color:red;  border:1px solid black; " >OffDuty</td><td style="width:25%; background-color:green;  border:1px solid black; " >AddedPilot</td></tr></table>
        <br />
            <asp:Button ID="Buttonizinver" runat="server" style="height:30px; Width:80px" Text="Confirm"   OnClick="Buttonizinver_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" />&nbsp;&nbsp;  
          <asp:Button ID="Buttonizinsil" runat="server" style="height:30px; Width:200px" ForeColor="Red" Text="Cancel OnLeave/Replacing"  Visible="false"  OnClick="Buttonizinsil_Click"/>  &nbsp;&nbsp;
            <asp:Button ID="Buttonizinyok" runat="server" style="height:30px; Width:80px" Text="Close"  /> 
<br /><br /> </div>
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopupizinli" runat="server" TargetControlID="Buttonizinli" PopupControlID="panelizinli" BackgroundCssClass="modalbodyarka" CancelControlID="Buttonizinyok" ></asp:ModalPopupExtender>
<%-- modal izinli biter --%>


      
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
                                        <td  colspan="2"  ><br />Last Notes :</td>
                                         </tr> <tr>
                                        <td colspan="2" >
                                        <asp:ListBox ID="TBjurnotlast"  runat="server" Visible="True" Width="260px" BackColor="#FFFFE3"  Rows="5" Wrap="true" TextMode="MultiLine" Height="100px"></asp:ListBox></td>
                                        <asp:TextBox ID="TBjurnotfalse" runat="server" Visible="False" Width="260px" Wrap="true" TextMode="MultiLine" ></asp:TextBox>
                                    </tr>

                                     <tr>
                                        <td colspan="2" >
                                            <asp:TextBox ID="TBjurnot"  placeholder="Write New Note..."  CssClass="ilkharfbuyuk" MaxLength="500" runat="server" Text="" Visible="True" Width="260px" BackColor="#FFFFE3"  Rows="2" Wrap="true" TextMode="MultiLine" Height="40px"></asp:TextBox></td>
                                    </tr>

                                      <tr>
                                        <td colspan="2" class="paneltusnoborder" ><asp:LinkButton ID="Bshowbyd" runat="server"  OnClick="Bshowbyd_Click"  Text="Show Byd"  />  <br /><br /> 
                                        </td>   </tr>
                                    <tr>
                                        
                                        <td colspan="2"  class="paneltusnoborder" >
                                            <asp:Button ID="Bacceptedjur" OnClick="Bacceptedjur_Click"  runat="server" style="height:30px; Width:80px" Text="Save" BackColor="Green" BorderWidth="1"   CssClass="btn"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" />&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                 
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



 <div style="height:150px; clear:both; font-size:15px;  Color:#000000; float:left;">  <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="20px"></asp:Label>
</br><strong>GÜNCELLEME:</strong>
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
</body>
</html>
