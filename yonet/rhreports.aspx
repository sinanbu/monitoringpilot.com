<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rhreports.aspx.cs" Inherits="rhreports" EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot - PORT INFO</title>
    <link href="../css/stil.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .selected_row {
            background-color: #252a44;
            border-bottom-color: gray;
        }


        .kucukharf {
            text-transform: lowercase;
            direction: ltr;
            overflow: auto;
        }

        .dikeyBaslik { 
            font-family:'Arial Narrow', Arial, sans-serif;
            font-size:10px; 
            -webkit-transform: rotate(-90deg); 
            -moz-transform: rotate(-90deg); 
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3); 
            border:1px solid black;
        }
        .normalsutun {
            font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
            font-size: 12px;
            border: 1px solid black;
            border-spacing:0px 0px;
            grid-cell:none;
            text-align:center;
            color:black;
                
        }

        .duzyazi {
            text-decoration:none;
            cursor:default;
        }

.tabloREP{
     width: 1340px; 
     border-spacing:0px 0px;
}

.trbaslikREP{
    background-color: #80e8ff;
    color: black;
    font-weight: bold; 
    height: 22px; 
}
.trsatirREP {
    border:0px solid #cdebbe;
    height: 22px; 
        background-color: #fff;
        color:black;
}

    </style>

    <script type="text/javascript" src="../js/jquery-1.11.2.js"></script>
 

</head>
<body onkeydown="return (event.keyCode!=13)">
    <form id="form1" runat="server">

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

        <asp:UpdatePanel ID="UpdatePanelHeader" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server">
                    <table style="width: 1340px; height: 25px; color: white; background-image: url(../images/boslukaltcizgi.png)">
                        <tr style="height: 25px">
                            <td>
                                <asp:Label ID="LblVarid" ForeColor="Red" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarid" runat="server" Text="" Visible="false"></asp:Literal>
                                <asp:Label ID="LblVarno" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarno" runat="server" Text="" Visible="false"></asp:Literal>
                                <asp:Label ID="LblVarbasla" runat="server" Text=""></asp:Label><asp:Literal ID="varbaslar" runat="server" Text="" Visible="false"></asp:Literal>
                                <asp:Label ID="LblVarbit" runat="server" Text=""></asp:Label>
                                <asp:Literal ID="varbiter" runat="server" Text="" Visible="false"></asp:Literal>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <asp:Panel ID="summariall" runat="server">


                <table style="width: 1340px; float: right; color: white">
                    <tr style="height: 22px; border-color: red;">
                        <td style="text-align: left;">

                                <asp:TextBox ID="TextBox7" runat="server" MaxLength="10"    Font-Size="Small" Width="90px" Height="22px" ></asp:TextBox>
<asp:MaskedEditExtender  CultureName="tr-TR"  ID="MaskedEditExtender7" runat="server" TargetControlID="TextBox7" ErrorTooltipEnabled="true" MaskType="Date" DisplayMoney="Left"  AcceptNegative="Left" Mask="99/99/9999"></asp:MaskedEditExtender>
<asp:Button ID="LBworkrest" runat="server"   OnClick="LBworkrest_Click" Text="W/R Hours" Width="100px" Height="24px"></asp:Button>

<asp:Label ID="Ldikbols" runat="server" Width="11px" Text=" "></asp:Label> 
 <asp:LinkButton ID="LBgeri"  Width="20px" runat="server" OnClick="LBgeri_Click" Font-Bold="true" style="text-align:center" Font-Size="10px" Text="<<" BackColor="#9fc0c1" BorderStyle="Solid" BorderWidth="1px" Font-Underline="false"></asp:LinkButton> 
                             <asp:Label ID="Ldikbol" runat="server" Width="3px" Text=" "></asp:Label> 
                             <asp:LinkButton ID="LBileri"  Width="20px" runat="server" OnClick="LBileri_Click"  Font-Bold="true" style="text-align:center"  Font-Size="10px"  Text=">>" BackColor="#c9dcdc"  BorderStyle="Solid" BorderWidth="1px" Font-Underline="false" ></asp:LinkButton>
   <asp:Label ID="Lwoidgunluk2" runat="server"  ></asp:Label>                     
                           &nbsp;&nbsp;
                   <asp:LinkButton ID="LBrange" runat="server" Font-Underline="false" ForeColor="Black" CssClass="duzyazi"  OnClick="LBrange_Click" Text="Off Stn. <> On Stn." ></asp:LinkButton>
                        </td>
                        <td style="text-align: right">
<asp:Button ID="ButtonLiveScreen" Style="height: 25px; Width: 80px; font-size: x-small; text-align: center;" runat="server" Text="One Day" OnClick="ButtonLiveScreen_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" />
 
                            <asp:LinkButton ID="LBonline" runat="server"  OnClick="LBonline_Click" OnClientClick="this.disabled='true'; "></asp:LinkButton>
                            &nbsp;&nbsp
                <asp:LinkButton ID="LBonlineoff" Text="SignOff"  OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "></asp:LinkButton>&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>







                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>


                        <div class="clearup"></div>
                        <div style="clear: both; text-align: left;">
  <br />  <br />
                <table style="width: 100%; background-color:white;  border-spacing:0px 0px; grid-cell:none; border:1px solid black">
                        <tr><td  style="width: 14%; border-right:1px solid black" >&nbsp;</td>
                             <td style="text-align: center;  height:30px ">
                            <span style="font-family: 'Trebuchet MS'; color: #222; font-size:20px;"><b><asp:Label ID="baslik" runat="server"></asp:Label> </b><br />
                          
                        </span></td>
                            <td  style="width: 10%; border-left:1px solid black; font-size:11px;" >REV.NO:1 <br />REV.TAR:19.08.2010</td>
                    </tr>
                </table>
        <asp:DataList ID="DLDarica"  runat="server" Width="1340px"  CellPadding="0" HorizontalAlign="Left" OnItemDataBound="DLDarica_ItemDataBound" >

           <HeaderTemplate>
                                    <table  class="tabloREP">
                                        <tr class="trbaslikREP"  style="height:50px " >
                        <td style="width:2% ;  border:1px solid black; "> </td>
                        <td style="width:20% ; border:1px solid black; ">Kılavuz Kaptan</td>
                        <td class="dikeyBaslik">00:30</td>
                        <td class="dikeyBaslik">01:00</td>
                        <td class="dikeyBaslik">01:30</td>
                        <td class="dikeyBaslik">02:00</td>
                        <td class="dikeyBaslik">02:30</td>
                        <td class="dikeyBaslik">03:00</td>
                        <td class="dikeyBaslik">03:30</td>
                        <td class="dikeyBaslik">04:00</td>
                        <td class="dikeyBaslik">04:30</td>
                        <td class="dikeyBaslik">05:00</td>
                        <td class="dikeyBaslik">05:30</td>
                        <td class="dikeyBaslik">06:00</td>
                        <td class="dikeyBaslik">06:30</td>
                        <td class="dikeyBaslik">07:00</td>
                        <td class="dikeyBaslik">07:30</td>
                        <td class="dikeyBaslik">08:00</td>
                        <td class="dikeyBaslik">08:30</td>
                        <td class="dikeyBaslik">09:00</td>
                        <td class="dikeyBaslik">09:30</td>
                        <td class="dikeyBaslik">10:00</td>
                        <td class="dikeyBaslik">10:30</td>
                        <td class="dikeyBaslik">11:00</td>
                        <td class="dikeyBaslik">11:30</td>
                        <td class="dikeyBaslik">12:00</td>
                        <td class="dikeyBaslik">12:30</td>
                        <td class="dikeyBaslik">13:00</td>
                        <td class="dikeyBaslik">13:30</td>
                        <td class="dikeyBaslik">14:00</td>
                        <td class="dikeyBaslik">14:30</td>
                        <td class="dikeyBaslik">15:00</td>
                        <td class="dikeyBaslik">15:30</td>
                        <td class="dikeyBaslik">16:00</td>
                        <td class="dikeyBaslik">16:30</td>
                        <td class="dikeyBaslik">17:00</td>
                        <td class="dikeyBaslik">17:30</td>
                        <td class="dikeyBaslik">18:00</td>
                        <td class="dikeyBaslik">18:30</td>
                        <td class="dikeyBaslik">19:00</td>
                        <td class="dikeyBaslik">19:30</td>
                        <td class="dikeyBaslik">20:00</td>
                        <td class="dikeyBaslik">20:30</td>
                        <td class="dikeyBaslik">21:00</td>
                        <td class="dikeyBaslik">21:30</td>
                        <td class="dikeyBaslik">22:00</td>
                        <td class="dikeyBaslik">22:30</td>
                        <td class="dikeyBaslik">23:00</td>
                        <td class="dikeyBaslik">23:30</td>
                        <td class="dikeyBaslik">24:00</td>
                        <td  class=  "normalsutun" style="text-align:center; width:5% ">MAN.<BR />SAY.</td>
                        <td  class=  "normalsutun" style="text-align:center; width:5% ">T.Ç.S.</td>
                        <td  class=  "normalsutun" style="text-align:center; width:5% ">T.İ.S.</td>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                        <tr  class="trsatirREP">
                        <td   style="text-align:left;  border :0px solid black;  background-color:lightgray;"><asp:label  ID="LblNo" runat="server" ForeColor="Black" Text='<%# Container.ItemIndex +1 %>' ></asp:label></td>
                        <td   style="text-align:left; border:1px solid black; background-color:lightgray;"><asp:label ID="LBpilotismi" runat="server"  Text='<%#Eval("degismecikapno")%>'></asp:label>
                            <asp:label  ID="LblKapno" runat="server" Text='' Visible="False"></asp:label>
                        </td>

<%--                    <asp:LinkButton ID="Lblgemiadi"  runat="server" CssClass="butongemi1" Font-Underline="false"  Text='<%#Eval("gemiadi")%>' ToolTip=""  CommandName="jurnot" ></asp:LinkButton>--%>

                        <td runat="server" id="td1" class=  "normalsutun"><asp:label  ID="Label1" runat="server"  ></asp:label></td>
                        <td runat="server" id="td2" class=  "normalsutun"><asp:label  ID="Label2" runat="server"  ></asp:label></td>
                        <td runat="server" id="td3" class=  "normalsutun"><asp:label  ID="Label3" runat="server"  ></asp:label></td>
                        <td runat="server" id="td4" class=  "normalsutun"><asp:label  ID="Label4" runat="server"  ></asp:label></td>
                        <td runat="server" id="td5" class=  "normalsutun"><asp:label  ID="Label5" runat="server"  ></asp:label></td>
                        <td runat="server" id="td6" class=  "normalsutun"><asp:label  ID="Label6" runat="server"  ></asp:label></td>
                        <td runat="server" id="td7" class=  "normalsutun"><asp:label  ID="Label7" runat="server"  ></asp:label></td>
                        <td runat="server" id="td8" class=  "normalsutun"><asp:label  ID="Label8" runat="server"  ></asp:label></td>
                        <td runat="server" id="td9" class=  "normalsutun"><asp:label  ID="Label9" runat="server"   ></asp:label></td>
                        <td runat="server" id="td10" class=  "normalsutun"><asp:label  ID="Label10" runat="server"  ></asp:label></td>

                        <td runat="server" id="td11" class=  "normalsutun"><asp:label  ID="Label11" runat="server"  ></asp:label></td>                                                           
                        <td runat="server" id="td12" class=  "normalsutun"><asp:label  ID="Label12" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td13" class=  "normalsutun"><asp:label  ID="Label13" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td14" class=  "normalsutun"><asp:label  ID="Label14" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td15" class=  "normalsutun"><asp:label  ID="Label15" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td16" class=  "normalsutun"><asp:label  ID="Label16" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td17" class=  "normalsutun"><asp:label  ID="Label17" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td18" class=  "normalsutun"><asp:label  ID="Label18" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td19" class=  "normalsutun"><asp:label  ID="Label19" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td20" class=  "normalsutun"><asp:label  ID="Label20" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td21" class=  "normalsutun"><asp:label  ID="Label21" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td22" class=  "normalsutun"><asp:label  ID="Label22" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td23" class=  "normalsutun"><asp:label  ID="Label23" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td24" class=  "normalsutun"><asp:label  ID="Label24" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td25" class=  "normalsutun"><asp:label  ID="Label25" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td26" class=  "normalsutun"><asp:label  ID="Label26" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td27" class=  "normalsutun"><asp:label  ID="Label27" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td28" class=  "normalsutun"><asp:label  ID="Label28" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td29" class=  "normalsutun"><asp:label  ID="Label29" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td30" class=  "normalsutun"><asp:label  ID="Label30" runat="server"  ></asp:label></td> 

                        <td runat="server" id="td31" class=  "normalsutun"><asp:label  ID="Label31" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td32" class=  "normalsutun"><asp:label  ID="Label32" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td33" class=  "normalsutun"><asp:label  ID="Label33" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td34" class=  "normalsutun"><asp:label  ID="Label34" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td35" class=  "normalsutun"><asp:label  ID="Label35" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td36" class=  "normalsutun"><asp:label  ID="Label36" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td37" class=  "normalsutun"><asp:label  ID="Label37" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td38" class=  "normalsutun"><asp:label  ID="Label38" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td39" class=  "normalsutun"><asp:label  ID="Label39" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td40" class=  "normalsutun"><asp:label  ID="Label40" runat="server"  ></asp:label></td> 

                        <td runat="server" id="td41" class=  "normalsutun"><asp:label  ID="Label41" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td42" class=  "normalsutun"><asp:label  ID="Label42" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td43" class=  "normalsutun"><asp:label  ID="Label43" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td44" class=  "normalsutun"><asp:label  ID="Label44" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td45" class=  "normalsutun"><asp:label  ID="Label45" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td46" class=  "normalsutun"><asp:label  ID="Label46" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td47" class=  "normalsutun"><asp:label  ID="Label47" runat="server"  ></asp:label></td> 
                        <td runat="server" id="td48" class=  "normalsutun"><asp:label  ID="Label48" runat="server"  ></asp:label></td> 

                        <td runat="server" id="td49"  class=  "normalsutun" style="text-align:center; background-color:lightgray; "><asp:label  ID="Label49" runat="server"  ></asp:label></td>
                        <td runat="server" id="td50"  class=  "normalsutun" style="text-align:center; background-color:lightgray;  "><asp:label  ID="Label50" runat="server"  ></asp:label></td>
                        <td runat="server" id="td51"  class=  "normalsutun" style="text-align:center; background-color:lightgray;  "><asp:label  ID="Label51" runat="server"  ></asp:label>
                            <asp:LinkButton ID="BtnProcessiptal" Visible="false" runat="server" Text="Cancel" CommandName="Processiptal"  ToolTip="Cancel Job"  CssClass="islemtuslari1"  ></asp:LinkButton></td>

                    </tr>
    
            </ItemTemplate>
        
            <FooterTemplate>
                
                <tr  class="trsatirREP">
                        <td class=  "normalsutun" colspan="40">&nbsp;</td> 
                        <td class=  "normalsutun" colspan="10"  style="text-align:left; ">Toplam Çalışılan/Dinlenilen Saat</td> 
                        <td class=  "normalsutun" style="text-align:center; ">:</td>
                        <td class=  "normalsutun" style="text-align:center; "><asp:label  ID="Label52" runat="server" Text="0" ></asp:label></td>
                        <td class=  "normalsutun" style="text-align:center; "><asp:label  ID="Label53" runat="server"   Text="0" ></asp:label></td>
                </tr>

                <tr  class="trsatirREP">
                        <td class=  "normalsutun" colspan="40">&nbsp;</td> 
                        <td class=  "normalsutun" colspan="10"  style="text-align:left; ">Kılavuz Kaptan Sayısı</td> 
                        <td class=  "normalsutun" style="text-align:center; ">:</td>
                        <td class=  "normalsutun" style="text-align:center; "  colspan="2" ><asp:label  ID="Label54" runat="server"  Text="0"  ></asp:label></td>
                </tr>

                <tr  class="trsatirREP">
                        <td class=  "normalsutun" colspan="40">&nbsp;</td> 
                        <td class=  "normalsutun" colspan="10"  style="text-align:left; ">Ortalama Pilot Başına Manevra</td> 
                        <td class=  "normalsutun" style="text-align:center; ">:</td>
                        <td class=  "normalsutun" style="text-align:center; "  colspan="2" ><asp:label  ID="Label55" runat="server"  Text="0"  ></asp:label></td>
                </tr>

                <tr  class="trsatirREP">
                        <td class=  "normalsutun" colspan="40">&nbsp;</td> 
                        <td class=  "normalsutun" colspan="10"  style="text-align:left; ">Ort.Pilot Başına Çalışılan/Dinlenilen Saat</td> 
                        <td class=  "normalsutun" style="text-align:center; ">:</td>
                        <td class=  "normalsutun" style="text-align:center; "><asp:label  ID="Label56" runat="server"  Text="0"  ></asp:label></td>
                        <td class=  "normalsutun" style="text-align:center; "><asp:label  ID="Label57" runat="server"  Text="0"  ></asp:label></td>
                </tr>

                
                </table></FooterTemplate>
        
        </asp:DataList>

                            <asp:label  ID="Label49x" runat="server"  Text="0"  Visible="false" ></asp:label>
                            <asp:label  ID="Label50x" runat="server"  Text="0"  Visible="false" ></asp:label>
                            <asp:label  ID="Label51x" runat="server"  Text="0"  Visible="false" ></asp:label>

                        </div>
                        <div style="height: 150px; clear: both; float: left;">
                            <asp:Label ID="Label100" runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label></div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </asp:Panel>

<%-- istasyon değiştirme ve onay messageları hepsi birarada, Baccepted  butonuna onclick olayları farklı --%>
 <asp:Button id="buttononayMessagePanel" runat="server" style="display:none;" /> 
        <asp:Panel ID="panelonayMessagePanel"    CssClass="panelmessage" runat="server"   Style="display: none; "> 
    <div style="text-align:center; font-weight:bold;"> <br /> <asp:Literal ID="Litmodstnmes"  runat="server" Text=""></asp:Literal> <br /><br />
        <asp:TextBox ID="Lblprocesstime" Height="25px"  Font-Size="medium" runat="server" Width="160px"></asp:TextBox><asp:MaskedEditExtender  CultureName="tr-TR"  ID="MaskedEditExtender3" runat="server" TargetControlID="Lblprocesstime" ErrorTooltipEnabled="true" MaskType="DateTime" Mask="99/99/9999 99:99"></asp:MaskedEditExtender><br />  <br /></div>

            <asp:Button ID="BacpCSok"   runat="server" style="height:30px; Width:80px" Text="Yes"     UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..'; "  />&nbsp;&nbsp; 
            <asp:Button ID="BacpCScancel" runat="server" style="height:30px; Width:80px" Text="Cancel"  />  
<br /><br />



        </asp:Panel>                            
        <asp:ModalPopupExtender ID="ModalPopupCSonayMessage" runat="server" TargetControlID="buttononayMessagePanel" PopupControlID="panelonayMessagePanel" BackgroundCssClass="modalbodyarka" CancelControlID="BacpCScancel" ></asp:ModalPopupExtender>



<%-- modal onay message biter --%>

        </div>

    </form>
</body>
</html>
