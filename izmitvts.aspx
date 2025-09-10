<%@ Page Language="C#" AutoEventWireup="true" CodeFile="izmitvts.aspx.cs" Inherits="izmitvts" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
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

           <table  style="width:1340px; height:38px;  color:#111; font-weight:bold;  z-index:10; ">
               <tr  style="height:36px;">
<td  style="text-align:left;  width:30%; vertical-align:central;">
 <a href="http://www.monitoringpilot.com/" class="logo"><img src="images/logovts.png" width="161" height="36" alt="" /></a> </td>
            <td style="text-align:center; vertical-align:bottom; width:40%;"> 
         
                 <asp:Panel ID="Panel1" runat="server">
<asp:UpdatePanel ID="UpdatePanelHeader" runat="server" UpdateMode="Always" ><ContentTemplate>
  <asp:Label ID="LblVarid" ForeColor="Red" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarid" runat="server" Text="" Visible="false"></asp:Literal> 
                                                                                       <asp:Literal ID="varbilvarno" runat="server" Text="" Visible="false"></asp:Literal>
                          <asp:Label ID="LblVarbit" runat="server" Text=""></asp:Label> <asp:Literal ID="varbiter" runat="server" Text="" Visible="false"></asp:Literal> 
                 
                    <asp:Timer ID="gerisayimtik" runat="server" OnTick="gerisayimtik_Tick"  Interval="3000"></asp:Timer>

 </ContentTemplate></asp:UpdatePanel></asp:Panel> 
                           </td>
               
               <td  style="text-align:right;  width:30%;"> </td></tr>
               <tr  style="height:2px; background-image:url(images/boslukaltcizgiv.png); background-repeat:repeat-x;"><td colspan="3">&nbsp;
                                                                                                                     </td></tr>
</table>


    <div  style="width:100%;  z-index:10;  text-align:left;">
     <asp:Panel ID="Panel2" runat="server">
 <asp:UpdatePanel ID="UpdatePanelCanliekran" runat="server" UpdateMode="Always" ><ContentTemplate>




    <table  style="width:100%; float:left; color:white">
<tr  style="height:22px"><td  style="text-align:left">
    <asp:Label ID="LBDuyuru" runat="server"  style="font-size:12px; color:#1111ee;"><b>Notice :</b></asp:Label> 
    <asp:Label ID="LblDuyuru" runat="server"  Font-Size="12px" ForeColor="#111111"  Text=""></asp:Label>
    </td></tr></table>  
                    


<table  style="width:100% ;">
<tr><td style="text-align:left;"><span class="tablobaslik1" style="cursor:default;"><b>SEKTÖR HEREKE</b></td>
    <td style="text-align:right;"><span class="tablobaslik1" style="cursor:default;"><b>Darıca Pilot Station / Operator : </b>
    <asp:Label ID="LBonlined" runat="server"></asp:Label></span><asp:label  ID="LblNosayd" runat="server" Visible="false" ></asp:label></td>

</tr> </table>  

        <asp:DataList ID="DLDarica"  runat="server" Width="1340px"  CellPadding="2" HorizontalAlign="Left" OnItemDataBound="DLDarica_ItemDataBound" OnItemCommand="DLDarica_ItemCommand">

           <HeaderTemplate>
                                    <table  class="tablo1">
                                        <tr class="trbaslik1" >
                        <td style="width:2% ">No</td>
                        <td style="width:13% ">Pilot Name</td>
                        <td style="width:13% ">Ship Name</td>
                        <td style="width:8% ">Flag</td>
                        <td style="width:5% ">Grt</td>
                        <td style="width:4% ">Type</td>
                        <td style="width:14% ">Departure Port</td>
                        <td style="width:14% ">Arrival Port</td>
                        <td style="text-align:center; width:3% ">Anc</td>
                        <td style="text-align:center; width:6% ">OffStation</td>
                        <td style="text-align:center; width:6% ">POB Time</td>
                        <td style="text-align:center; width:6% ">P.Off Time</td>
                        <td style="text-align:center; width:6% ">OnStation</td>

                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                        <tr  class="trsatir1">
                        <td  style="width:2% "><asp:label  ID="LblNo" runat="server" ForeColor="Black" Text='<%# Container.ItemIndex +1 %>' ></asp:label></td>
                        <td  style="width:13% " >
                            <asp:label  ID="lblDurum" runat="server" Text='<%#Eval("durum")%>' Visible="false" ></asp:label>
                            <asp:label ID="LBpgecmis" runat="server"  style=" text-decoration:none;"  Font-Italic='<%#(Eval("kidem").ToString())=="5"? true : false%>' Font-Bold='<%#Convert.ToInt32((Eval("kidem")))>2? true : false%>'><%#Eval("degismeciadisoyadi")%></asp:label>
                            <asp:label  ID="LblKapno" runat="server" Text='<%#Eval("kapno")%>' Visible="False"></asp:label>
                            <asp:label  ID="Lblkidem" runat="server" Text='<%#Eval("kidem")%>' Visible="False"></asp:label>
                        </td>
                       
                        <td  style=" width:13% ; "><asp:label ID="Lblgemiadi" runat="server" Text='<%#Eval("gemiadi")%>'  ToolTip=""></asp:label><asp:label ID="Lblimono" runat="server" Visible="false" Text='<%#Eval("imono")%>' ></asp:label></td>
                        <td  style=" width:8% ; "><%#Eval("bayrak").ToString().Length>9? (Eval("bayrak") as string).Substring(0,10)+"." : Eval("bayrak")%></td>
                        <td  style=" width:5% ; "><%#Eval("grt")%></td>
                        <td  style=" width:4% ; "><%#Eval("tip").ToString().Length>1? (Eval("tip") as string).Substring(0,3) : Eval("tip")%></td>
                        <td  style=" width:14% ; "><asp:LinkButton ID="binisport" runat="server" Text='<%# Bind("binisyeri") %>'  CommandName="linkleb" CssClass="tablobaslik1"></asp:LinkButton> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %> </td>
                        <td  style=" width:14% ; "><asp:LinkButton ID="inisport" runat="server" Text='<%# Bind("inisyeri") %>'  CommandName="linklei" CssClass="tablobaslik1"></asp:LinkButton>  <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %><asp:label ID="Lblinisyerigiz" Visible="false" runat="server" Text='<%#Eval("inisyeri")%>'></asp:label></td>
                      <td  style="text-align:center;  width:3% ; "><asp:label ID="Ldemiryeri" runat="server" Text=""></asp:label>  </td>
                        <td  style="text-align:center; width:6%  ">
                             <asp:label  ID="LblIstasyoncikis" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>' ></asp:label>
                             <asp:label  ID="LblIstasyoncikiseta" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>'  ForeColor="#aaaaaa"></asp:label>
                        </td>

                        <td  style="text-align:center; width:6%  ">
                                        <asp:label  ID="LblPob" runat="server"   Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>' ></asp:label>
                                        <asp:label  ID="LblPobeta" runat="server" Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td  style="text-align:center; width:6%  ">
                               <asp:label  ID="LblPoff" runat="server"  Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>' ></asp:label>
                            <asp:label  ID="LblPoffeta" runat="server" Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td  style="text-align:center; width:6%  ">
                                <asp:label  ID="LblIstasyongelis" runat="server"  Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>' ></asp:label>
                             <asp:label  ID="LblIstasyongeliseta" runat="server" Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>'  ForeColor="#aaaaaa"></asp:label>
                        </td> 

                    </tr>
    
            </ItemTemplate>
        
            <FooterTemplate></table></FooterTemplate>
        
        </asp:DataList>




 <table  style="width:100%">
<tr>
    <td style="text-align:left; vertical-align:bottom;"><span class="tablobaslik0"><b>SEKTÖR KÖRFEZ</b></td>
    <td style="text-align:right; "><br /><span  class="tablobaslik0"  style="cursor:default;"><b>Yarımca Pilot Station / Operator : </b>
    <asp:Label ID="LBonliney" runat="server"></asp:Label></span><asp:label  ID="LblNosayy" runat="server" Visible="false" ></asp:label></td>
</tr> </table>
            
        <asp:DataList ID="DLDaricay" runat="server" Width="1340px"  CellPadding="2" HorizontalAlign="Left" OnItemDataBound="DLDaricay_ItemDataBound" OnItemCommand="DLDaricay_ItemCommand">

          <HeaderTemplate>
                                    <table  class="tablo0">
                                        <tr  class="trbaslik0">
                        <td style="width:2% ">No</td>
                        <td style="width:13% ">Pilot Name</td>
                        <td style="width:13% ">Ship Name</td>
                        <td style="width:8% ">Flag</td>
                        <td style="width:5% ">Grt</td>
                        <td style="width:4% ">Type</td>
                        <td style="width:14% ">Departure Port</td>
                        <td style="width:14% ">Arrival Port</td>
                        <td style="text-align:center; width:3% ">Anc</td>
                        <td style="text-align:center; width:6% ">OffStation</td>
                        <td style="text-align:center; width:6% ">POB Time</td>
                        <td style="text-align:center; width:6% ">P.Off Time</td>
                        <td style="text-align:center; width:6% ">OnStation</td>
                    </tr>
            </HeaderTemplate>
   
            <ItemTemplate>
                       <tr  class="trsatir0">
                        <td  style="width:2% "><asp:label  ID="LblNoy" runat="server" ForeColor="Black" Text='<%# Container.ItemIndex +1 %>' ></asp:label></td>
                        <td  style="width:13% ">
                            <asp:label  ID="lblDurumy" runat="server" Text='<%#Eval("durum")%>'  Visible="false"></asp:label>
                            <asp:label ID="LBpgecmisy" runat="server"  style="text-decoration:none; "   Font-Italic='<%#(Eval("kidem").ToString())=="5"? true : false%>' Font-Bold='<%#Convert.ToInt32((Eval("kidem")))>2? true : false%>'><%#Eval("degismeciadisoyadi")%></asp:label>
                            <asp:label  ID="LblKapnoy" runat="server" Text='<%#Eval("kapno")%>' Visible="False"></asp:label>
                            <asp:label  ID="Lblkidemy"  runat="server" Text='<%#Eval("kidem")%>' Visible="False"></asp:label>
                        </td>
                       
                        <td  style=" width:13%  ; "><asp:label ID="Lblgemiadiy" runat="server" Text='<%#Eval("gemiadi")%>'></asp:label><asp:label ID="Lblimonoy" runat="server" Visible="false" Text='<%#Eval("imono")%>' ></asp:label></td>
                        <td  style=" width:8% ; "><%#Eval("bayrak").ToString().Length>9? (Eval("bayrak") as string).Substring(0,10)+"." : Eval("bayrak")%></td>
                        <td  style=" width:5%  ; "><%#Eval("grt")%></td>
                        <td  style=" width:4%  ; "><%#Eval("tip").ToString().Length>1? (Eval("tip") as string).Substring(0,3) : Eval("tip")%></td>
                        <td   style=" width:14%  ; "><asp:LinkButton ID="binisport" runat="server" Text='<%# Bind("binisyeri") %>'  CommandName="linkleb" CssClass="tablobaslik1"></asp:LinkButton> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %> </td>
                        <td   style=" width:14%  ; "><asp:LinkButton ID="inisport" runat="server" Text='<%# Bind("inisyeri") %>'  CommandName="linklei" CssClass="tablobaslik1"></asp:LinkButton>  <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %><asp:label ID="Lblinisyerigizy" Visible="false" runat="server" Text='<%#Eval("inisyeri")%>'></asp:label></td>
                                              <td  style="text-align:center;  width:3% ; "><asp:label ID="Ldemiryeriy" runat="server" Text=""></asp:label></td>
                                                   <td   style="text-align:center; width:6% ">
                                <asp:label  ID="LblIstasyoncikisy" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>' ></asp:label>
                             <asp:label  ID="LblIstasyoncikisetay" runat="server" Text='<%#Eval("istasyoncikis").ToString().Length>0? (Eval("istasyoncikis") as string).Substring(11,5) : Eval("istasyoncikis")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td   style="text-align:center; width:6% ">
                             <asp:label  ID="LblPoby" runat="server"   Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>' ></asp:label>
                             <asp:label  ID="LblPobetay" runat="server" Text='<%#Eval("pob").ToString().Length>0? (Eval("pob") as string).Substring(11,5) : Eval("pob")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                        <td   style="text-align:center; width:6% ">
                               <asp:label  ID="LblPoffy" runat="server"  Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>' ></asp:label>
                            <asp:label  ID="LblPoffetay" runat="server" Text='<%#Eval("Poff").ToString().Length>0? (Eval("Poff") as string).Substring(11,5) : Eval("Poff")%>'   ForeColor="#aaaaaa"></asp:label>
                        </td>
                                                 <td   style="text-align:center; width:6% ">
                                <asp:label  ID="LblIstasyongelisy"   runat="server" Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>' ></asp:label>
                             <asp:label  ID="LblIstasyongelisetay" runat="server" Text='<%#Eval("istasyongelis").ToString().Length>0? (Eval("istasyongelis") as string).Substring(11,5) : Eval("istasyongelis")%>'  ForeColor="#aaaaaa"></asp:label>
                        </td>     

                    </tr>
    
            </ItemTemplate> <FooterTemplate></table></FooterTemplate>
        
        </asp:DataList>


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



 <div style="height:150px; clear:both; float:left;">  <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label>

</div>
 </ContentTemplate>       
           </asp:UpdatePanel>
  


   </asp:Panel> 
    </div>

    </form>
</body>
</html>
