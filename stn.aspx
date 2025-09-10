<%@ Page Language="C#" AutoEventWireup="true" CodeFile="stn.aspx.cs" Inherits="stn" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html  xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="refresh" content="300" />
    <title>MonitoringPilot - LIVE SCREEN</title>
 <link href="css/stil.css" rel="stylesheet" />
        
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }


    </style>

    <script type="text/javascript" src="js/jquery-1.11.2.js"></script>
<script type="text/javascript">

    window.onload = SetScroll;

    function SetScroll() {
        var objDiv = document.getElementById("<%=UpdatePanelCanliekran.ClientID%>");
        objDiv.scrollTop = objDiv.scrollHeight;
    }

    function zilcal() {document.getElementById("zil").play();}

</script>

</head>
<body onkeydown = "return (event.keyCode!=13)" style="    width:1260px;">
    <form id="form1" runat="server" >
        <audio id="zil" src="alert9.mp3"></audio>

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

           <table  style="width:1260px; height:25px;  color:#111; font-weight:bold;  z-index:10; background-image:url(images/boslukaltcizgi.png)"><tr  style="height:25px">
            <td style="text-align:right; width:52%;"> <div  style="margin-top:-9px;">
<asp:Label ID="LblVarid" ForeColor="Red" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarid" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarno" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarno" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarbasla" runat="server" Text=""></asp:Label><asp:Literal ID="varbaslar" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarbit" runat="server" Text=""></asp:Label> <asp:Literal ID="varbiter" runat="server" Text="" Visible="false"></asp:Literal> 
                <asp:Literal ID="Litlastup" runat="server" Text="" Visible="false"></asp:Literal> 
           </div > </td><td  style="text-align:left;  width:38%;"> <div  style="float:left;  position:absolute; margin-top:-16px;">  <asp:Panel ID="Panel1" runat="server">
<asp:UpdatePanel ID="UpdatePanelHeader" runat="server" UpdateMode="Always" ><ContentTemplate>
                
                    <asp:Timer ID="gerisayimtik" runat="server" OnTick="gerisayimtik_Tick"  Interval="3000"></asp:Timer>
<asp:Label ID="LblReeltime" runat="server" Text=""></asp:Label> / JobCounter: <asp:Label ID="LblCounter" ForeColor="Red" Font-Size="14px" runat="server" Text=""></asp:Label> &nbsp;
    <asp:Label  ID="Lblindicator1" ForeColor="black"  Height="16px" Font-Size="12px"  runat="server" ></asp:Label><asp:Label  ID="Lblindicator2" ForeColor="black"  Height="16px"  Font-Size="12px"  runat="server"  ></asp:Label><asp:Label  ID="Lblindicator3" ForeColor="black"   Height="16px"  Font-Size="12px"  runat="server"   ></asp:Label><asp:Label  ID="Lblindicator4" ForeColor="black"    Height="16px"  Font-Size="12px"  runat="server"   ></asp:Label><asp:Label  ID="Lblindicator5" ForeColor="black"    Height="16px"  Font-Size="12px"  runat="server"   ></asp:Label><asp:Label  ID="Lblindicator6" ForeColor="black"    Height="16px" Font-Size="12px"  runat="server"   ></asp:Label><asp:Label  ID="Lblindicator7" ForeColor="black"    Height="16px" Font-Size="10px"  runat="server"   ></asp:Label>

 </ContentTemplate></asp:UpdatePanel></asp:Panel> </div >
                </td><td  style="text-align:right;  width:10%;">
 
</td></tr>
</table>


    <div  style="width:100%;  z-index:10;  text-align:left;">
     <asp:Panel ID="Panel2" runat="server">
 <asp:UpdatePanel ID="UpdatePanelCanliekran" runat="server" UpdateMode="Always" ><ContentTemplate>




    <table  style="width:1335px; float:left; color:white">
<tr  style="height:20px; "><td  style="text-align:left;  width:1140px; font-size:12px; color:#1111ee;"><b>OnLeave(Changing/AddedPilot) :</b> <asp:Label ID="Lblizinliler" runat="server" Text=""></asp:Label>
    <asp:Label ID="Litizinler" runat="server" Text=""></asp:Label></td>
    <td  rowspan="3" style=" text-align:left; width:205px; font-size:12px; color:#1111ee;">  
        <asp:Label ID="Lblsaat" runat="server"   style="font-size:50px; font-family:Calibri; text-align:left; width:205px; color:#1111ee; float:left"></asp:Label></td></tr>

<tr  style="height:20px"><td  style="text-align:left">
    <asp:Label ID="LBDuyuru" runat="server"  style="font-size:12px; color:#1111ee;"><b>Notice :</b></asp:Label> 
    <asp:Label ID="LblDuyuru" runat="server" Font-Size="12px" ForeColor="#660066"  Text=""></asp:Label>
    </td></tr>

<tr  style="height:20px"><td style="text-align:left; width:100% ;"><span class="tablobaslik1" style="cursor:default;"><b><asp:LinkButton runat="server"  style="text-decoration:none"  Text="DARICA" ID="siptus" OnClick="siptus_Click"></asp:LinkButton> </b>&nbsp;/&nbsp;
    <asp:Label ID="LBonlined" runat="server"></asp:Label></span><asp:label  ID="LblNosayd" runat="server" Visible="false" ></asp:label></td>
</tr> </table>  

        <asp:DataList ID="DLDarica"  runat="server" Width="1340px"  CellPadding="2" HorizontalAlign="Left" OnItemDataBound="DLDarica_ItemDataBound" OnItemCommand="DLDarica_ItemCommand">

           <HeaderTemplate>
                                    <table  class="tablo1"  style="width:1260px;">
                                        <tr class="trbaslik1"  style="height:20px;">
                        <td style="width:2% ">No</td>
                        <td style="width:12% ">Pilot Name</td>
                        <td style="width:2% ">Jb</td>
                        <td style="width:3% ">W.Hr</td>
                        <td style="width:4% ">Fatig.</td>
                        <td style="width:3%; ">R/8</td>
                        <td style="width:12% ">Ship Name</td>
                        <td style="width:6% ">Flag</td>
                        <td style="width:4% ">Grt</td>
                        <td style="width:3% ">Type</td>
                        <td style="width:3% "><asp:Label ID="Label3" runat="server"  Text="IMDG" ToolTip="Dangerous Cargo"></asp:Label></td>
                        <td style="width:3% "><asp:Label ID="Label23" runat="server"  Text="TBP-T" ToolTip="Total Bollard Pull"></asp:Label></td>
                        <td style="width:10% ">Departure</td>
                        <td style="width:10% ">Arrival</td>
                        <td style="text-align:center; width:5% ">Off-Stn</td>
                        <td style="text-align:center; width:5% ">POB</td>
                        <td style="text-align:center; width:5% ">POff</td>
                        <td style="text-align:center; width:5% ">On-Stn</td>

                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                        <tr  class="trsatir1"  style="height:20px;">
                        <td  style="width:2% "><asp:label  ID="LblNo" runat="server" ForeColor="Black" Text='<%# Container.ItemIndex +1 %>' ></asp:label></td>
                        <td  style="width:12% " >
                            <asp:label  ID="lblDurum" runat="server" Text='<%#Eval("durum")%>' Visible="false" ></asp:label>
                            <asp:label ID="LBpgecmis" runat="server"  style=" text-decoration:none;"  Font-Italic='<%#(Eval("kidem").ToString())=="5"? true : false%>' Font-Bold='<%#Convert.ToInt32((Eval("kidem")))>2? true : false%>'><%#Eval("degismeciadisoyadi")%></asp:label>
                            <asp:label  ID="LblKapno" runat="server" Text='<%#Eval("kapno")%>' Visible="False"></asp:label>
                            <asp:label  ID="Lblkidem" runat="server" Text='<%#Eval("kidem")%>' Visible="False"></asp:label>
                        </td>
                         <td  style=" width:2% ; "><%#Eval("toplamissayisi")%></td>
                        <td  style=" width:3% ; "><%#Eval("toplamissuresi")%></td>
                        <td  style=" width:4% ; "><asp:label ID="Lblyorulma" runat="server"  Text='<%#Eval("yorulma")%>'></asp:label><asp:label ID="Lbllastday" runat="server"  Visible="false" Text='<%#Eval("lastday")%>'></asp:label></td>
                     

                        <td  style=" width:3% ;   "><%#Eval("lastseven")%></td>
                        <td  style=" width:12% ; "><asp:label ID="Lblgemiadi" runat="server" Text='<%#Eval("gemiadi")%>'  ToolTip=""></asp:label><asp:label ID="Lblimono" runat="server" Visible="false" Text='<%#Eval("imono")%>' ></asp:label></td>
                        <td  style=" width:6% ; "><%#Eval("bayrak").ToString().Length>9? (Eval("bayrak") as string).Substring(0,10)+"." : Eval("bayrak")%></td>
                        <td  style=" width:4% ; "><%#Eval("grt")%></td>
                        <td  style=" width:3% ; "><%#Eval("tip").ToString().Length>1? (Eval("tip") as string).Substring(0,3) : Eval("tip")%></td>
                                                    <td  style="width:3%;" ><asp:label  ID="Lbldc" runat="server"> </asp:label></td>
                                                    <td  style="width:3%;" ><asp:label  ID="Lbltbp" runat="server"> </asp:label></td>
                        <td  style=" width:10% ; "><%#Eval("binisyeri") %> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %>   </td>
                        <td  style=" width:10% ; "><%#Eval("inisyeri") %> <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %><asp:label ID="Lblinisyerigiz" runat="server"  Visible="false" Text='<%#Eval("inisyeri")%>'></asp:label></td>
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
                        </td>       

                    </tr>
    
            </ItemTemplate>
        
            <FooterTemplate></table></FooterTemplate>
        
        </asp:DataList>




 <table  style="width:100%">
<tr  style="height:20px"><td style="text-align:left; width:100%;  "><span  class="tablobaslik0"  style="cursor:default;"><b><asp:LinkButton runat="server" style="text-decoration:none" Text="YARIMCA" ID="sipyar" OnClick="sipyar_Click"></asp:LinkButton></b>&nbsp;/&nbsp;
    <asp:Label ID="LBonliney" runat="server"></asp:Label></span><asp:label  ID="LblNosayy" runat="server" Visible="false" ></asp:label></td>
</tr> </table>
            
        <asp:DataList ID="DLDaricay" runat="server" Width="1340px"  CellPadding="2" HorizontalAlign="Left" OnItemDataBound="DLDaricay_ItemDataBound" OnItemCommand="DLDaricay_ItemCommand">

          <HeaderTemplate>
                                    <table  class="tablo0"  style="width:1260px;">
                                        <tr  class="trbaslik0"  style="height:20px;">
                        <td style="width:2% ">No</td>
                        <td style="width:12% ">Pilot Name</td>
                        <td style="width:2% ">Jb</td>
                        <td style="width:3% ">W.Hr</td>
                        <td style="width:4% ">Fatig.</td>
                        <td style="width:3%; ">R/8</td>
                        <td style="width:12% ">Ship Name</td>
                        <td style="width:6% ">Flag</td>
                        <td style="width:4% ">Grt</td>
                        <td style="width:3% ">Tp</td>
                        <td style="width:3% "><asp:Label ID="Label3" runat="server"  Text="IMDG" ToolTip="Dangerous Cargo"></asp:Label></td>
                        <td style="width:3% "><asp:Label ID="Label23" runat="server"  Text="TBP-T" ToolTip="Total Bollard Pull"></asp:Label></td>
                        <td style="width:10% ">Departure</td>
                        <td style="width:10% ">Arrival</td>
                        <td style="text-align:center; width:5% ">Off-Stn</td>
                        <td style="text-align:center; width:5% ">POB</td>
                        <td style="text-align:center; width:5% ">POff</td>
                        <td style="text-align:center; width:5% ">On-Stn</td>
                    </tr>
            </HeaderTemplate>
   
            <ItemTemplate>
                       <tr  class="trsatir0"  style="height:20px;">
                        <td  style="width:2% "><asp:label  ID="LblNoy" runat="server" ForeColor="Black" Text='<%# Container.ItemIndex +1 %>' ></asp:label></td>
                        <td  style="width:12% ">
                            <asp:label  ID="lblDurumy" runat="server" Text='<%#Eval("durum")%>'  Visible="false"></asp:label>
                            <asp:label ID="LBpgecmisy" runat="server"  style="text-decoration:none; "   Font-Italic='<%#(Eval("kidem").ToString())=="5"? true : false%>' Font-Bold='<%#Convert.ToInt32((Eval("kidem")))>2? true : false%>'><%#Eval("degismeciadisoyadi")%></asp:label>
                            <asp:label  ID="LblKapnoy" runat="server" Text='<%#Eval("kapno")%>' Visible="False"></asp:label>
                            <asp:label  ID="Lblkidemy"  runat="server" Text='<%#Eval("kidem")%>' Visible="False"></asp:label>
                        </td>
                         <td  style=" width:2% ; "><%#Eval("toplamissayisi")%></td>
                        <td  style=" width:3% ; "><%#Eval("toplamissuresi")%></td>
                        <td  style=" width:4% ; "><asp:label ID="Lblyorulmay" runat="server"  Text='<%#Eval("yorulma")%>'></asp:label><asp:label ID="Lbllastdayy" runat="server" Visible="false"  Text='<%#Eval("lastday")%>'></asp:label></td>
                        <td  style=" width:3% ;   "><%#Eval("lastseven")%></td>
                        <td  style=" width:12%  ; "><asp:label ID="Lblgemiadiy" runat="server" Text='<%#Eval("gemiadi")%>'></asp:label><asp:label ID="Lblimonoy" runat="server" Visible="false" Text='<%#Eval("imono")%>' ></asp:label></td>
                        <td  style=" width:6% ; "><%#Eval("bayrak").ToString().Length>9? (Eval("bayrak") as string).Substring(0,10)+"." : Eval("bayrak")%></td>
                        <td  style=" width:4%  ; "><%#Eval("grt")%></td>
                        <td  style=" width:3%  ; "><%#Eval("tip").ToString().Length>1? (Eval("tip") as string).Substring(0,3) : Eval("tip")%></td>
                                                   <td  style="width:3%;" ><asp:label  ID="Lbldcy" runat="server"> </asp:label></td>
                                                    <td  style="width:3%;" ><asp:label  ID="Lbltbpy" runat="server"> </asp:label></td>
                        <td   style=" width:10%  ; "><%#Eval("binisyeri") %> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %>   </td>
                        <td   style=" width:10%  ; "><%#Eval("inisyeri") %> <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %><asp:label ID="Lblinisyerigizy" runat="server"  Visible="false" Text='<%#Eval("inisyeri")%>'></asp:label></td>
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

                    </tr>
    
            </ItemTemplate> <FooterTemplate></table></FooterTemplate>
        
        </asp:DataList>





 <div style="height:150px; clear:both; font-size:15px;  Color:#000000; float:left;">  <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="20px"></asp:Label>
</br>
</br><span style="height:60px; text-align:center; font-size:20px; color:yellow; background-color:black; font-weight:bold; font-family:'Trebuchet MS'">GÜNCELLEME :</span>

</br><b>** Fatigue hesaplamalarında manevra tipleri zorluk katsayıları kaldırılmıştır. Demirleme,kalkış,yanaşma,kalkıştan yanaşma,çift demirle yanaşma, herbirinin fatigue katsayısı eşittir.</b> 
  <%--   </br>
<table  class="trsatir2" style="text-align:center"><tr>
    <td style="background-color:#ffb3fd; height:40px; width:150px; text-align:center">% 0-20 </br> Efortless Working</td>
    <td style="background-color:#77ff6d; width:150px">% 20-25 </br> Easy Working</td>
    <td style="background-color:#ffff00; width:150px">% 25-30 </br> Normal Working</td>
    <td style="background-color:#ff7700; width:150px">% 30-35 </br> Hard Working</td>
    <td style="background-color:#ff0000; width:150px">% 35-40 </br> Overload Working</td>
    <td style="background-color:#505050; width:150px">% 40-... </br> Risky Working</td>
       </tr></table>    
     </br>

     
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


<asp:Label ID="Label2" runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label>
</div>
 </ContentTemplate>       
           </asp:UpdatePanel>
  


   </asp:Panel> 
    </div>

    </form>
</body>
</html>
