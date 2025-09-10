<%@ Page Language="C#" AutoEventWireup="true" CodeFile="survey5.aspx.cs" Inherits="survey5" EnableEventValidation="false" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html  xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot - ARGE</title>
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

.panellbadd
{
width: 700px;
height: 500px;
border: 1px  groove #111;
border-radius: 8px;
background-color:lightsalmon;
text-align:left;
color:black;
font-size:14px;
}
.modalbodyarka
{
background-color: #333333;
filter: alpha(opacity:70);
opacity: 0.6;
z-index: 10000;
}
.tdsag {
text-align:left;
vertical-align:top;
border-radius: 8px;
height:35px;
}

.tdorta {
text-align:center;
background-color:aliceblue;
border-radius: 8px;
height:30px;
}


.sagusttuslist {
	list-style-type: none;
}

.sagusttuslista {
    width:300px;
    height:26px;
    background-color: lightsalmon;
	float: left;
	padding: 12px;
	font-family:'Trebuchet MS';
	font-size: 24px;
	color: #000;
	text-decoration: none;
    text-align:center;
	transition: background-color 1s, color 1s;
	-o-transition: background-color 1s, color 1s;
	-moz-transition: background-color 1s, color 1s;
	-webkit-transition: background-color 1s, color 1s;
        border-top-right-radius: 10px;
    border-bottom-left-radius: 10px;
    -moz-border-radius-topright: 10px;
    -moz-border-radius-bottomleft: 10px;
    -webkit-border-top-right-radius: 10px;
    -webkit-border-bottom-left-radius: 10px;
}
.sagusttuslista:hover {
	background-color: #ff8300;
	text-decoration: none;
	color: #fff;
        border-top-right-radius: 10px;
    border-bottom-left-radius: 10px;
    -moz-border-radius-topright: 10px;
    -moz-border-radius-bottomleft: 10px;
    -webkit-border-top-right-radius: 10px;
    -webkit-border-bottom-left-radius: 10px;
}

.sagusttuslistb {
    width:250px;
    height:26px;
    background-color: lightseagreen;
	float: left;
	padding: 12px;
	font-family:'Trebuchet MS';
	font-size: 24px;
	color: #000;
	text-decoration: none;
    text-align:center;
	transition: background-color 1s, color 1s;
	-o-transition: background-color 1s, color 1s;
	-moz-transition: background-color 1s, color 1s;
	-webkit-transition: background-color 1s, color 1s;
        border-top-right-radius: 10px;
    border-bottom-left-radius: 10px;
    -moz-border-radius-topright: 10px;
    -moz-border-radius-bottomleft: 10px;
    -webkit-border-top-right-radius: 10px;
    -webkit-border-bottom-left-radius: 10px;
}
.sagusttuslistb:hover {
	background-color: forestgreen;
	text-decoration: none;
	color: #fff;
        border-top-right-radius: 10px;
    border-bottom-left-radius: 10px;
    -moz-border-radius-topright: 10px;
    -moz-border-radius-bottomleft: 10px;
    -webkit-border-top-right-radius: 10px;
    -webkit-border-bottom-left-radius: 10px;
}
        
       #sonuc3{
            resize:none;
       }
       #sonuc4{
            resize:none;
       }
    </style>

    <script type="text/javascript" src="js/jquery-1.11.2.js"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=DLDarica] td").bind("click", function () {
            var row = $(this).parent();
            $("[id*=DLDarica] tr").each(function () {
                if ($(this)[0] != row[0]) {
                    $("td", this).removeClass("selected_row");
                }
            });
            $("td", row).each(function () {
                if (!$(this).hasClass("selected_row")) {
                    $(this).addClass("selected_row");
                } else {
                    $(this).removeClass("selected_row");
                }
            });
        });
    });


   


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
 <asp:Panel ID="summariall" runat="server" >
           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
               <ContentTemplate>

<table  style="width:1340px; float:right;   color:white">
 <tr  style="height:22px;  border-color:red; ">
     <td style="text-align:left;">
                      <asp:Button ID="ButtonLiveScreen" style="height:20px; Width:110px; font-size:x-small;  text-align:center;" runat="server" Text="Live Screen" OnClick="ButtonLiveScreen_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"   /> &nbsp;&nbsp;&nbsp;
                             <asp:DropdownList ID="DDLarges"  runat="server"  AutoPostBack="true" OnSelectedIndexChanged="DDLarges_SelectedIndexChanged" Width="300px" Height="25px"  ></asp:DropdownList>
     </td>
        <td style="text-align:right">
                <asp:LinkButton ID="LBonline" runat="server"  ForeColor="#111" OnClick="LBonline_Click"  OnClientClick="this.disabled='true'; "  ></asp:LinkButton> &nbsp;&nbsp
                <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="#111"  OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "  ></asp:LinkButton>&nbsp;&nbsp;
 </td></tr></table>  

<asp:Label ID="tikkapno" runat="server" Visible="false" Text=""></asp:Label>

<table  style="width:1340px;"> <tr><td style="text-align:left; width:100% ; height:25px" colspan="2"></td></tr>
<tr><td style="text-align:left; width:100% ;" colspan="2">
     
    <table  style="width:100%; border:4px dashed darkorchid; border-radius:4px;"> <tr><td style="text-align:center; width:100% ; height:50px; font-size:larger; color:darkblue;">
        <span style="font-family:'Trebuchet MS'; vertical-align:top; color:#222; margin:0; font-size:large;"><b>YÖNETİM KURULU ADAY ADAYI BAŞVURU SAYFASI </b></span></td></tr></table>  
    <br /><br /></td></tr>

    <tr>
    <td style="text-align:left; vertical-align:top;  width:30% ;">
        
          <asp:GridView ID="GridView7" Width="270px" runat="server"  OnRowDataBound="GridView7_RowDataBound"  AutoGenerateColumns="False" CellPadding="0" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical"  OnSelectedIndexChanging="GridView7_SelectedIndexChanging"  AllowPaging="True"  >
                             <AlternatingRowStyle BackColor="White" />
                             <Columns >
                                 
                                  <asp:TemplateField ControlStyle-Width="30px" HeaderText="No">
                                     <ItemTemplate>
                                         <asp:Label ID="siraod" runat="server" Text='<%# Container.DataItemIndex +1   %>'></asp:Label>
                                      <asp:Label ID="anketno" runat="server" Visible="false" Text='<%# Bind("id") %>'></asp:Label>
                                         <asp:Label ID="kapno" runat="server" Visible="false" Text='<%# Bind("kapno") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="200px" HeaderText="Tüm Aday Listesi">
                                     <ItemTemplate>
                                          <div style="text-align:left"><asp:LinkButton OnClick="pilotod_Click" ID="pilotod" runat="server" Text='<%# Bind("kapadisoyadi") %>' CommandName='<%# Bind("resimyolu") %>' CommandArgument='<%# Bind("kapno") %>'></asp:LinkButton> </div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="40px" HeaderText="LSA Puanı">
                                     <ItemTemplate>
                                          <div style="text-align:center"><asp:Label ID="wizbit" runat="server" Text='<%#Eval("lsapuan")%>'></asp:Label></div>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                             </Columns>
                             
                             <HeaderStyle BackColor="#507CD1" Font-Names="Trebuchet MS" Font-Size="12px" Height="30px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB" Font-Names="Trebuchet MS"  Font-Size="12px" Height="25px"  HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False"  />
                             <SelectedRowStyle BackColor="#c1cDe1"  ForeColor="#333333" />
                         </asp:GridView>
        
        
        <br /></td> 
    
    <td style="text-align:left; vertical-align:top;  width:30% ;">  <span style="font-family:'Trebuchet MS'; vertical-align:top; color:#222; margin:0; font-size:small;">      
            Adaylık başvurusu yapmak isteyenler aşağıdaki bilgileri kısa kara tecrübelerini, kurs ve  
            kabiliyetlerini doldurarak bilgilerini kaydetmelidir. Ondan sonra aşağıdaki liderlik değerlendirme 
            tuşuna basara açılacak penceredeki değerlendirme sorularını yanıtladıktan sonra adaylık başvurusu 
            sisteme kaydedilmiş olur. İsteyen herkes aday olabilir. <br /> <br />

        <b>Adaylarda aranacak önemli vasıflar:        </b> <br />
<b>1)</b> Şirketin geleceği için bütün gücüyle çalışmalı, her zaman şirketin geleceğini düşünmeli, yöneticilik vasıflarında eksiklikleri varsa tamamlamaya çalışmalı <br />

<b>2)</b> Otoriter olmamalı, kontrol meraklısı olmamalı, görevlendirmeli ve herzaman bütün işleri takip altında tutmalı <br />

<b>3) </b>Şeffaf olmalı, iş tanımlarını net yapmalı, verileri saklamamalı, gidişattan astlarını haberdar etmeli <br />

<b>4) </b>Geleceği görmek için çaba göstermeli, çevreyi piyasa şartlarını dikkatle takip edip gelişmelere kayıtsız kalmamalı <br />

<b>5) </b>Sorumluluk sahibi olmalı, şirket için hangi işin önemli olduğunu bilirlemeli ve hangi işi yapıyorsa o işe odaklanmalı, koltukta Rehavete kapılmamalı<br />

<b>6) </b>İkna kabiliyeti ve insanlarla iletişimi iyi olmalı, gerek vücut dili gerek ses tonu ve kullandığı kelimelerle insanları etkileyebilmeli, etraflarına pozitif enerji verebilmeli <br />

<b>7) </b>İyi dinlemeli, kendini iyi ifade etmeli, fikir alabilmeli, gerektiğinde çözüme en kısa sürede en doğru şekilde ulaşmaya çalışmalı  <br />

<b>8) </b>Yeteneğe önem vermekle birlikte takım oyununun en önemli başarı anahtarı olduğunu bilmeli, tüm organizasyonun bir dişilinin parçaları gibi sağlıklı şekilde işlemesi için herkesi yönlendirmeli, yüreklendirmeli <br />
                                                        </span>
            </br>
        <span style="font-family:'Trebuchet MS'; vertical-align:top; color:#33f; margin:0; font-size:medium;"> 
        Sizde bu özellikleri taşıyorsanız şirketin bekası için mutlaka adaylığınızı koyunuz. </span>
        </br></br> <br /> <br /> <br /> 
           </td></tr>

    <tr>
    <td style="text-align:left; vertical-align:top; width:30% ;">
        
        <img id="adayresim" src="images/adaysec/cansel.jpg" style="width:290px ;" runat="server"/><br /><br />

    </td> 
        <td style="text-align:left; ">   
            <b style="color:red; font-size:larger; ">Aday Adayı : <asp:Label ID="sonuc1" runat="server"></asp:Label></b></br></br>
            <b style="color:red">Mezuniyet Yılı : </b><asp:TextBox ID="sonuc2" runat="server" Height="25px" Width="50px" MaxLength="4" Text=""></asp:TextBox></br></br>
            <b style="color:red">Karadaki Tecrübeler :</b>&nbsp<asp:TextBox ID="remLen" runat="server" BorderWidth="0px"  Height="20px" Width="25px" BackColor="#ffFFFF" Enabled="false" ></asp:TextBox> </br> 
            <asp:TextBox ID="sonuc3" runat="server" Height="100px" Width="1000px" TextMode="MultiLine" Text="" placeholder="Karadaki çalışma hayatınıza ait tecrübeleriniz" onkeyup="textCounter(sonuc3, this.form.remLen, 500);" onkeydown="textCounter(sonuc3, this.form.remLen, 500);"></asp:TextBox>
            </br></br>
            <b style="color:red">Kurslar ve Yeteknekler :</b>&nbsp<asp:TextBox ID="remLen2" runat="server" BorderWidth="0px"  Height="20px" Width="25px" BackColor="#ffFFFF" Enabled="false" ></asp:TextBox> </br>
            <asp:TextBox ID="sonuc4" runat="server" Height="100px" Width="1000px" TextMode="MultiLine" Text=""  placeholder="Aldığınız kurslar ve Yeteknekleriniz"  onkeyup="textCounter(sonuc4, this.form.remLen2, 500);" onkeydown="textCounter(sonuc4, this.form.remLen2, 500);"></asp:TextBox></br></br>

            <asp:LinkButton ID="LBKaydetTest" OnClick="LBKaydetTest_Click" class="sagusttuslista" runat="server"  Visible="false" Text="ADAYLIĞIMI KAYDET"></asp:LinkButton>
            <asp:LinkButton ID="LBAdayBasvuru" OnClick="LBAdayBasvuru_Click" class="sagusttuslistb" runat="server" Visible="false"  Text="Aday Olmak İstiyorum"></asp:LinkButton>
            <asp:LinkButton ID="LBadayduzelt" OnClick="LBadayduzelt_Click" class="sagusttuslistb" runat="server" Visible="false"  Text="Bilgilerimi Düzelt"></asp:LinkButton>

            </td></tr>




</table> <br /><br /><br />


                         <div class="clearup"> </div>
   <div  style="clear:both; text-align:left;"> 


                        


            <%--
2-Adaylık süreci bittikten sonra oylama sayfası aktif edilir. Burada oy verme kriteri olarak; 
* Kişi kendine oy veremez
* Herkes kendisi hariç yönetimde olmasını istediği 6 ismi ve 3 yedeği seçer,
* Kullanılan oylar değiştirilemez,
* Kişilerin birbirlerini yönlendirmemesi ve herkesin gönlünden geceni öğrenebilmek açısından oylama kapalı olacak ve herkes oy verene kadar oy sayımı yapılmayacaktır,
* Bütün oylar kullanıldıktan sonra sonuçlar görüntülenebilecektir ve pdf olarak grupta paylaşılır

--%>

 

      

      <div class="clearup"> </div>
   <div  style="clear:both; text-align:left;"> 




        <asp:Button id="buttonshowpopuppadd" runat="server" style="display:none;" />
        <asp:Panel ID="panellbadd"   CssClass="panellbadd" runat="server"> 

<table   style=" text-align:center; vertical-align:central; width:700px; height:400px; border:0px solid red">                        
                     <tr>
                <td  class="tdorta" >
                 <p ><b>LEADERSHIP SKILL ASSESSMENT TEST</b></p></td>
            </tr>          
     <tr id="kayok" runat="server">
                <td  style=" text-align:center;" ><asp:Label ID="TBpaddportno"  Visible="false"   runat="server" ></asp:Label> <br /> <br /> 
                    <b><asp:Label ID="Label2"   style=" text-align:center; font-size:20px; padding:5px;" Text="ADAY KAYDINIZ YAPILMIŞTIR."  runat="server" ></asp:Label></b> <br /> <br /> 
                </td>
            </tr>

    <tr id="aciklama" runat="server">
                <td  class="tdsag" ><div  style="padding:5px; margin-top:-10px; font-size:14px;  line-height:24px;  text-align:center;"><b>Açıklama:</b> <br /> 
                    Bu aşamadan sonra isteğe bağlı olarak kendi liderlik yeteneğinizi ölçmek amacı ile bir test hazırlanmıştır ve bu testi yaparak liderlik yeteneğinizi ölçebilirsiniz. Testteki sorular, online yabancı 
                    Leadership Skill Assessment test sistelerinden alınıp, Türkçeleştirilerek hazırlanmıştır. <br /> 
                    Soruların cevaplarında yanlış cevap yoktur, herbir secenek farklı değerlerde puan kazandırır.<br />  Puanlama 10 ile 90 arasındadır.<br /> 
                    Sonucunuz 60 ile 90 arasında ise Liderlik vasfını taşıdığınızı bir referans olarak gösterir. Amaç yüksek puan almak değil, kendi karakterinize uygun cevabı bulmaktır.<br /> 
                    Test 40 sorudan oluşmaktadır, 15 dakika kadar sürmektedir. Testi bir defa çözebilirsiniz, yarıda bırakırsanız çözdüğünüz kadar soru değerlendirilir. Vaktiniz varsa testi çözmek istiyorsanız aşağıdaki Teste Başla butonuna tıklayınız.
                    <br /></div> </td>
    </tr>


            
    <tr id="sorular" runat="server">
                <td  class="tdorta" ><br /> <div  style="padding:5px; margin-top:20px; font-size:14px; line-height:26px; text-align:center;"><asp:Label ID="TBpaddportname" runat="server"  style=" text-align:center; font-size:20px; padding:5px;"  ></asp:Label> <br /> <br /> 
                    <asp:Label ID="Lsecbir" runat="server"  style=" text-align:center; font-size:20px; padding:5px;"   ></asp:Label>
                    <asp:Label ID="Lseciki" runat="server"   Visible="false" ></asp:Label>
                    <asp:Label ID="Laktif" runat="server"   Visible="false" ></asp:Label>
               <br /> <br />  </td>
    </tr>
                        

                
    <tr id="cevaplar" runat="server">
                <td   style="padding:5px; margin-top:20px; text-align:center;" ><div style="padding:5px; margin-top:-10px; margin-left:50px; font-size:14px; text-align:center;"><b></b> <br /> <br />
                    <asp:RadioButtonList ID="TBpaddsec" runat="server" CellSpacing="20"   style=" Font-Size:16px;"  RepeatDirection="Horizontal" CellPadding="20">
                                <asp:ListItem Value="1">Hiçbir Zaman</asp:ListItem>
                                <asp:ListItem Value="2">Çok Nadir</asp:ListItem>
                                <asp:ListItem Value="3">Bazen</asp:ListItem>
                                <asp:ListItem Value="4">Genellikle</asp:ListItem>
                                <asp:ListItem Value="5">Her Zaman</asp:ListItem>
                    </asp:RadioButtonList>
                    <br /> <br /> </div>
                </td>
    </tr>
           

    <tr>
                <td  class="tdorta"  style="height:80px;  text-align:center; font-size:20px; font-weight:bold; font-family:'Trebuchet MS'">
                       
                       <asp:Button ID="ButtonTesteBasla" runat="server" style="height:30px; Width:120px" BackColor="SpringGreen" Visible="false" Text="TESTE BAŞLA"  OnClick="ButtonTesteBasla_Click"  />&nbsp;&nbsp; 
                       <asp:Button ID="Buttonlbadd" runat="server" style="height:30px; Width:120px" Text=" NEXT " Visible="false"  OnClick="Buttonlbadd_Click" CommandArgument="1"  />&nbsp;&nbsp; 
                       <asp:Button ID="Buttonlbaddcancel" runat="server" style="height:30px; Width:120px" Text="İptal"   />         
                </td>

    </tr>         

           </table>

        </asp:Panel>
        </div>
                  <br /> <br /> 
<asp:ModalPopupExtender 
            ID="ModalPopupExtenderlbadd" runat="server" 
            CancelControlID="Buttonlbaddcancel"
            TargetControlID="buttonshowpopuppadd" 
            PopupControlID="panellbadd"
            BackgroundCssClass="modalbodyarka" ></asp:ModalPopupExtender>

        <br />

                    <div style="height:150px; clear:both; float:left;">  <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label></div>
                 </ContentTemplate></asp:UpdatePanel>

           </asp:Panel>
  

    </div>

    </form>
</body>
</html>
