<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sipy.aspx.cs" Inherits="sipy" EnableEventValidation="false" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html  xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" >
    <title>MonitoringPilot - Ships In Ports</title>

<%--    <script>
    $(document).ready(function () {
    $("#clientScreenWidth").val($(window).width());
    $("#clientScreenHeight").val($(window).height());
    });
    var iHeight = window.innerHeight;
    var iWidth = window.innerWidth;
 </script>
       <script type="text/javascript">
res = "&res="+screen.width+"x"+screen.height+"&d="+screen.colorDepth
top.location.href="detectscreen.aspx?action=set"+res
</script>--%>

    
<script type="text/javascript">
$(document).ready(function() {
    $("#width").val($(window).width());
    $("#height").val($(window).height());
});

//let btn = document.querySelector('button');
//let screenSize = document.querySelector('screenSize');
//let windowSize = document.querySelector('windowSize');
//let contentSize = document.querySelector('contentSize');

//btn.addEventListener('click', () => {
//    screenSize.innerText = 'Screen Height: ${screen.height} - Screen Width: ${screen.height}';
//    windowSize.innerText = 'window Height: ${window.height} - window Width: ${window.height}';
//    screenSize.innerText = 'content Height: ${content.height} - content Width: ${content.height}';});

</script>

       

 <%--<link href="css/stil.css" rel="stylesheet" />--%>
    <style type="text/css" runat="server" id="pagestyle">
        html {
            /*background: url("images/bolgeler/shipsonmap.jpg") no-repeat left fixed #000;
    background-size: cover;*/
            overflow: hidden; /* Hide scrollbars */
        }
        body {
            overflow-y: hidden; /* Hide vertical scrollbar */
  overflow-x: hidden; /* Hide horizontal scrollbar */
        }

    /* Hide scrollbar for Chrome, Safari and Opera 
.scr1::-webkit-scrollbar {
  display: none;
}*/

/* Hide scrollbar for IE, Edge and Firefox */
.scr {
  -ms-overflow-style: none;  /* IE and Edge */
  scrollbar-width: none;  /* Firefox */


}


           .harita
           {
               /*background-color:aquamarine;
               background:url("images/bolgeler/shipsonmapdar.jpg") no-repeat left fixed #777;*/
               position:absolute;
               float:left;
               vertical-align:top;
               z-index:0;
               top:0px;
               left:0px;
               width:1280px;
               height:auto;
               -ms-overflow-style: none;  /* IE and Edge */

           }/*screen and (min-width:1280px),

           @media screen and (min-device-width:1281px) {
	        .harita{width:1920px;}
            }*/



           .divsol
           {
               position:relative;
               float:left;
               left:8px;
               width:50%;

           }
                      .divsag
           {
               position:relative;
               float:left;
               left:8px;
               width:50%;

           }
           .menualt
           {
               border:0px solid red;
               position:fixed;
               z-index:1000;
               bottom:0%;
               right:0%;
               width:20%;
               height:15%;
               background-color:darkorange;
           }
           .tusyar
           {
               border:1px solid black;
               border-radius:2px;
               position:relative;
               background-color:white;
               width:80%;
               height:16%;
               top:2px;
               float:left;
               left:2px;
               font-size:10px;
               font-family:Verdana, Geneva, Tahoma, sans-serif;
               font-weight:bold;
               text-align:center;
               text-decoration:none;
           }

            .label
           {
               border-bottom:1px solid black;
               position:relative;
               /*background-color:white;
               width:44%;
               height:5%;*/
               top:2px;
               float:left;
               left:2px;
               font-size:12px;
               font-family:Verdana, Geneva, Tahoma, sans-serif;
               font-weight:bold;
               text-decoration:none;
           }

     .kroman {
               position:absolute;
               z-index:1;
  transform: rotate(-36deg);
  -webkit-transform:rotate(-36deg);
  -moz-transform: rotate(-36deg);
  -ms-transform: rotate(-36deg);
  -o-transform: rotate(-36deg);

           }
          .diler1 {
               position:absolute;
               z-index:1;
  transform: rotate(-30deg);
  -webkit-transform:rotate(-30deg);
  -moz-transform: rotate(-30deg);
  -ms-transform: rotate(-30deg);
  -o-transform: rotate(-30deg);

           }
               .diler2 {
               position:absolute;
               z-index:1;
  transform: rotate(-10deg);
  -webkit-transform:rotate(-10deg);
  -moz-transform: rotate(-10deg);
  -ms-transform: rotate(-10deg);
  -o-transform: rotate(-10deg);

           }
     .nuh {
     position:absolute;
     z-index:1;
       transform: rotate(-38deg);
-webkit-transform:rotate(-38deg);
  -moz-transform: rotate(-38deg);
   -ms-transform: rotate(-38deg);
    -o-transform: rotate(-38deg);

           }
     .evyap1{
                position:absolute;
               z-index:1;
                 transform: rotate(316deg);
          -webkit-transform:rotate(316deg);
            -moz-transform: rotate(316deg);
             -ms-transform: rotate(316deg);
              -o-transform: rotate(316deg);
           }
     .evyap2{
                position:absolute;
               z-index:1;
                 transform: rotate(348deg);
          -webkit-transform:rotate(348deg);
            -moz-transform: rotate(348deg);
             -ms-transform: rotate(348deg);
              -o-transform: rotate(348deg);
           }
     .gubre{
                position:absolute;
               z-index:1;
                 transform: rotate(196deg);
          -webkit-transform:rotate(196deg);
            -moz-transform: rotate(196deg);
             -ms-transform: rotate(196deg);
              -o-transform: rotate(196deg);
           }
     .turkuaz{
                position:absolute;
               z-index:1;
                 transform: rotate(117deg);
          -webkit-transform:rotate(117deg);
            -moz-transform: rotate(117deg);
             -ms-transform: rotate(117deg);
              -o-transform: rotate(117deg);
           }
   .marmara{
                position:absolute;
               z-index:1;
                 transform: rotate(117deg);
          -webkit-transform:rotate(117deg);
            -moz-transform: rotate(117deg);
             -ms-transform: rotate(117deg);
              -o-transform: rotate(117deg);
           }

      .rota1{
                position:absolute;
               z-index:1;
                 transform: rotate(174deg);
          -webkit-transform:rotate(174deg);
            -moz-transform: rotate(174deg);
             -ms-transform: rotate(174deg);
              -o-transform: rotate(174deg);
           }
            .rota2{
                position:absolute;
               z-index:1;
                 transform: rotate(-96deg);
          -webkit-transform:rotate(-96deg);
            -moz-transform: rotate(-96deg);
             -ms-transform: rotate(-96deg);
              -o-transform: rotate(-96deg);
           }
                  .milangaz{
                position:absolute;
               z-index:1;
                 transform: rotate(174deg);
          -webkit-transform:rotate(174deg);
            -moz-transform: rotate(174deg);
             -ms-transform: rotate(174deg);
              -o-transform: rotate(174deg);
           }
                 
        .igsas1{
                position:absolute;
               z-index:1;
                 transform: rotate(1deg);
          -webkit-transform:rotate(1deg);
            -moz-transform: rotate(1deg);
             -ms-transform: rotate(1deg);
              -o-transform: rotate(1deg);
           } 
                  .igsas2{
                position:absolute;
               z-index:1;
                 transform: rotate(181deg);
          -webkit-transform:rotate(181deg);
            -moz-transform: rotate(181deg);
             -ms-transform: rotate(181deg);
              -o-transform: rotate(181deg);
           }
      .igsas3{
                position:absolute;
               z-index:1;
                 transform: rotate(70deg);
          -webkit-transform:rotate(70deg);
            -moz-transform: rotate(70deg);
             -ms-transform: rotate(70deg);
              -o-transform: rotate(70deg);
           }
              .dp1{
                position:absolute;
               z-index:1;
                 transform: rotate(236deg);
          -webkit-transform:rotate(236deg);
            -moz-transform: rotate(236deg);
             -ms-transform: rotate(236deg);
              -o-transform: rotate(236deg);
           } 
                  .dp2{
                position:absolute;
               z-index:1;
                 transform: rotate(156deg);
          -webkit-transform:rotate(156deg);
            -moz-transform: rotate(156deg);
             -ms-transform: rotate(156deg);
              -o-transform: rotate(156deg);
           }
                  .oyak{
                position:absolute;
               z-index:1;
                 transform: rotate(156deg);
          -webkit-transform:rotate(156deg);
            -moz-transform: rotate(156deg);
             -ms-transform: rotate(156deg);
              -o-transform: rotate(156deg);
           }
                       .tupf1{
                position:absolute;
               z-index:1;
                 transform: rotate(14deg);
          -webkit-transform:rotate(14deg);
            -moz-transform: rotate(14deg);
             -ms-transform: rotate(14deg);
              -o-transform: rotate(14deg);
           }
                       .tupf1a{
                position:absolute;
               z-index:1;
                 transform: rotate(0deg);
          -webkit-transform:rotate(0deg);
            -moz-transform: rotate(0deg);
             -ms-transform: rotate(0deg);
              -o-transform: rotate(0deg);
           }
              .tupf2{
                position:absolute;
               z-index:1;
                 transform: rotate(194deg);
          -webkit-transform:rotate(194deg);
            -moz-transform: rotate(194deg);
             -ms-transform: rotate(194deg);
              -o-transform: rotate(194deg);
           }
                .tupf3{
                position:absolute;
               z-index:1;
                 transform: rotate(155deg);
          -webkit-transform:rotate(155deg);
            -moz-transform: rotate(155deg);
             -ms-transform: rotate(155deg);
              -o-transform: rotate(155deg);
           }
                                .tupf3a{
                position:absolute;
               z-index:1;
                 transform: rotate(240deg);
          -webkit-transform:rotate(240deg);
            -moz-transform: rotate(240deg);
             -ms-transform: rotate(240deg);
              -o-transform: rotate(240deg);
           }
             .tupp{
                position:absolute;
               z-index:1;
                 transform: rotate(9deg);
          -webkit-transform:rotate(9deg);
            -moz-transform: rotate(9deg);
             -ms-transform: rotate(9deg);
              -o-transform: rotate(9deg);
           }
        .poas1{
                position:absolute;
               z-index:1;
                 transform: rotate(199deg);
          -webkit-transform:rotate(199deg);
            -moz-transform: rotate(199deg);
             -ms-transform: rotate(199deg);
              -o-transform: rotate(199deg);
           }
         .poas2{
                position:absolute;
               z-index:1;
                 transform: rotate(19deg);
          -webkit-transform:rotate(19deg);
            -moz-transform: rotate(19deg);
             -ms-transform: rotate(19deg);
              -o-transform: rotate(19deg);
           }
        .shell{
                position:absolute;
               z-index:1;
                 transform: rotate(70deg);
          -webkit-transform:rotate(70deg);
            -moz-transform: rotate(70deg);
             -ms-transform: rotate(70deg);
              -o-transform: rotate(70deg);
           }
        .koruma1{
                position:absolute;
               z-index:1;
                 transform: rotate(70deg);
          -webkit-transform:rotate(70deg);
            -moz-transform: rotate(70deg);
             -ms-transform: rotate(70deg);
              -o-transform: rotate(70deg);
           }
                .koruma2{
                position:absolute;
               z-index:1;
                 transform: rotate(250deg);
          -webkit-transform:rotate(250deg);
            -moz-transform: rotate(250deg);
             -ms-transform: rotate(250deg);
              -o-transform: rotate(250deg);
           }
        .aktas{
                position:absolute;
               z-index:1;
                 transform: rotate(80deg);
          -webkit-transform:rotate(80deg);
            -moz-transform: rotate(80deg);
             -ms-transform: rotate(80deg);
              -o-transform: rotate(80deg);
           }
        .ford{
                position:absolute;
               z-index:1;
                 transform: rotate(270deg);
          -webkit-transform:rotate(270deg);
            -moz-transform: rotate(270deg);
             -ms-transform: rotate(270deg);
              -o-transform: rotate(270deg);
           }

                .auto{
                position:absolute;
               z-index:1;
                 transform: rotate(96deg);
          -webkit-transform:rotate(96deg);
            -moz-transform: rotate(96deg);
             -ms-transform: rotate(96deg);
              -o-transform: rotate(96deg);
           }

                .limas{
                position:absolute;
               z-index:1;
                 transform: rotate(122deg);
          -webkit-transform:rotate(122deg);
            -moz-transform: rotate(122deg);
             -ms-transform: rotate(122deg);
              -o-transform: rotate(122deg);
           }

        .kosbas{
                position:absolute;
               z-index:1;
                 transform: rotate(270deg);
          -webkit-transform:rotate(270deg);
            -moz-transform: rotate(270deg);
             -ms-transform: rotate(270deg);
              -o-transform: rotate(270deg);
           }






    </style>

    <script type="text/javascript">

    //    $(document).ready(function () {
    //$("#width").val($(content).width());
    //$("#height").val($(content).height());

    //$("#width").val() = $(content).width();
    //$("#height").val() = $(content).height();
//});
        
        function SetHiddenField() {
            $("#width").val($(window).width());
            $("#height").val($(window).height());
        }
        </script>

</head>
<body onkeydown = "return (event.keyCode!=13)" style="width: 100%; " >
    <form id="form1" runat="server" >


        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

 <asp:HiddenField  runat="server"  ID="height"  value="" ClientIDMode="Static"/>
 <asp:HiddenField  runat="server" ID="width"  value="" ClientIDMode="Static"/>
        
       
 <asp:Panel ID="summariall" runat="server" >
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>
                   <asp:Timer ID="gerisayimtik" runat="server" OnTick="gerisayimtik_Tick" Interval="1000"></asp:Timer>

           
                
                   <div id="taban" runat="server" class="harita" >
                      <%-- <button id="button">Get size</button>
                        <div  id="screenSize" ></div>
                             <div   id="windowSize"></div>
                                  <div   id="contentSize"></div>--%>

                      <%-- <asp:TextBox id="harita" runat="server" Text="test"></asp:TextBox>--%>

                       
       
                     <div   id="menualt" runat="server" class="menualt">
                         <div   id="divsol" runat="server" class="divsol">
<asp:LinkButton ID="LBmapdarica" runat="server" text="DARICA SHIPS" CssClass="tusyar" OnClick="LBmapdarica_Click"/></br>
                                       <asp:label runat="server" id="label1" class="label">In Ports:</asp:label>        </br>   
                                       <asp:label runat="server" id="label2" class="label">Esk.Anch:</asp:label>        </br>
                             </div>
                         <div   id="divsag" runat="server" class="divsag">
<asp:LinkButton ID="LBmapyarimca" runat="server" text="YARIMCA SHIPS" CssClass="tusyar" OnClick="LBmapyarimca_Click"/></br>
                                       <asp:label runat="server" id="label3" class="label">In Ports:</asp:label>        </br>   
                                       <asp:label runat="server" id="label4" class="label">Her.Anch:</asp:label>        </br> 
                                       <asp:label runat="server" id="label5" class="label">Yar.Anch:</asp:label>        </br> 
                                       <asp:label runat="server" id="label6" class="label">Izm.Anch:</asp:label>        </br> 
                             </div>
                     </div>

                       <asp:Image ID="harita" runat="server" ImageUrl="~/images/bolgeler/shipsonmapyar.jpg"  class="harita"/>


                    <asp:Image ID="kroman1"  runat="server"   class="kroman"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender3" TargetControlID="kroman1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel1" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel1" runat="server"><%=kroman1.AlternateText%></asp:Panel>

                    <asp:Image ID="kroman2"  runat="server"   class="kroman"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender4" TargetControlID="kroman2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel2" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel2" runat="server"><%=kroman2.AlternateText%></asp:Panel>

                    <asp:Image ID="kroman3"  runat="server"   class="kroman"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender5" TargetControlID="kroman3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel3" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel3" runat="server"><%=kroman3.AlternateText%></asp:Panel>

                    <asp:Image ID="kroman4"  runat="server"   class="kroman"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender6" TargetControlID="kroman4" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel4" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel4" runat="server"><%=kroman4.AlternateText%></asp:Panel>

                    <asp:Image ID="kroman5"  runat="server"   class="kroman"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender7" TargetControlID="kroman5" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel5" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel5" runat="server"><%=kroman5.AlternateText%></asp:Panel>

                    <asp:Image ID="diler1"  runat="server"   class="diler1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender8" TargetControlID="diler1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel6" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel6" runat="server"><%=diler1.AlternateText%></asp:Panel>

                    <asp:Image ID="diler2"  runat="server"   class="diler1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender9" TargetControlID="diler2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel7" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel7" runat="server"><%=diler2.AlternateText%></asp:Panel>

                    <asp:Image ID="diler3"  runat="server"   class="diler1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender10" TargetControlID="diler3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel8" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel8" runat="server"><%=diler3.AlternateText%></asp:Panel>

                    <asp:Image ID="diler4"  runat="server"   class="diler1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender11" TargetControlID="diler4" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel9" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel9" runat="server"><%=diler4.AlternateText%></asp:Panel>

                    <asp:Image ID="diler5"  runat="server"   class="diler2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender12" TargetControlID="diler5" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel10" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel10" runat="server"><%=diler5.AlternateText%></asp:Panel>

                    <asp:Image ID="diler6"  runat="server"   class="diler1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender13" TargetControlID="diler6" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel11" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel11" runat="server"><%=diler6.AlternateText%></asp:Panel>

                    <asp:Image ID="diler7"  runat="server"   class="diler1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender14" TargetControlID="diler7" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel12" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel12" runat="server"><%=diler7.AlternateText%></asp:Panel>

                    <asp:Image ID="diler8"  runat="server"   class="diler1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender15" TargetControlID="diler8" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel13" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel13" runat="server"><%=diler8.AlternateText%></asp:Panel>

                    <asp:Image ID="nuh1"  runat="server"   class="nuh"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender16" TargetControlID="nuh1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel14" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel14" runat="server"><%=nuh1.AlternateText%></asp:Panel>

                    <asp:Image ID="nuh2"  runat="server"   class="nuh"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender17" TargetControlID="nuh2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel15" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel15" runat="server"><%=nuh2.AlternateText%></asp:Panel>

                    <asp:Image ID="nuh3"  runat="server"   class="nuh"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender18" TargetControlID="nuh3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel16" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel16" runat="server"><%=nuh3.AlternateText%></asp:Panel>

                    <asp:Image ID="nuh4"  runat="server"   class="nuh"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender19" TargetControlID="nuh4" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel17" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel17" runat="server"><%=nuh4.AlternateText%></asp:Panel>

                    <asp:Image ID="evyap1"  runat="server"   class="evyap1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender20" TargetControlID="evyap1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel18" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel18" runat="server"><%=evyap1.AlternateText%></asp:Panel>

                    <asp:Image ID="evyap2"  runat="server"   class="evyap1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender21" TargetControlID="evyap2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel19" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel19" runat="server"><%=evyap2.AlternateText%></asp:Panel>

                    <asp:Image ID="evyap3"  runat="server"   class="evyap1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender22" TargetControlID="evyap3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel20" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel20" runat="server"><%=evyap3.AlternateText%></asp:Panel>

                    <asp:Image ID="evyap4"  runat="server"   class="evyap1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender23" TargetControlID="evyap4" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel21" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel21" runat="server"><%=evyap4.AlternateText%></asp:Panel>

                    <asp:Image ID="evyap5"  runat="server"   class="evyap1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender24" TargetControlID="evyap5" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel22" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel22" runat="server"><%=evyap5.AlternateText%></asp:Panel>

                    <asp:Image ID="evyap6"  runat="server"   class="evyap1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender25" TargetControlID="evyap6" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel23" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel23" runat="server"><%=evyap6.AlternateText%></asp:Panel>

                    <asp:Image ID="evyap7"  runat="server"   class="evyap1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender26" TargetControlID="evyap7" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel24" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel24" runat="server"><%=evyap7.AlternateText%></asp:Panel>

                    <asp:Image ID="evyap8"  runat="server"   class="evyap2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender27" TargetControlID="evyap8" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel25" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel25" runat="server"><%=evyap8.AlternateText%></asp:Panel>

                    <asp:Image ID="evyap9"  runat="server"   class="evyap2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender28" TargetControlID="evyap9" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel26" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel26" runat="server"><%=evyap9.AlternateText%></asp:Panel>

                    <asp:Image ID="evyap10"  runat="server"   class="evyap2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender29" TargetControlID="evyap10" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel27" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel27" runat="server"><%=evyap10.AlternateText%></asp:Panel>

                    <asp:Image ID="evyap11"  runat="server"   class="evyap2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender30" TargetControlID="evyap11" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel28" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel28" runat="server"><%=evyap11.AlternateText%></asp:Panel>

                    <asp:Image ID="gubre1"  runat="server"   class="gubre"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender31" TargetControlID="gubre1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel29" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel29" runat="server"><%=gubre1.AlternateText%></asp:Panel>

                    <asp:Image ID="gubre2"  runat="server"   class="gubre"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender32" TargetControlID="gubre2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel30" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel30" runat="server"><%=gubre2.AlternateText%></asp:Panel>

                    <asp:Image ID="gubre3"  runat="server"   class="gubre"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender33" TargetControlID="gubre3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel31" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel31" runat="server"><%=gubre3.AlternateText%></asp:Panel>

                    <asp:Image ID="turkuaz"  runat="server"   class="turkuaz"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender34" TargetControlID="turkuaz" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel32" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel32" runat="server"><%=turkuaz.AlternateText%></asp:Panel>

                    <asp:Image ID="marmara"  runat="server"   class="marmara"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender35" TargetControlID="marmara" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel33" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel33" runat="server"><%=marmara.AlternateText%></asp:Panel>

                    <asp:Image ID="rota1"  runat="server"   class="rota1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender36" TargetControlID="rota1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel34" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel34" runat="server"><%=rota1.AlternateText%></asp:Panel>

                    <asp:Image ID="rota2"  runat="server"   class="rota2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender37" TargetControlID="rota2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel35" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel35" runat="server"><%=rota2.AlternateText%></asp:Panel>

                    <asp:Image ID="rota3"  runat="server"   class="rota2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender38" TargetControlID="rota3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel36" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel36" runat="server"><%=rota3.AlternateText%></asp:Panel>

                    <asp:Image ID="rota4"  runat="server"   class="rota2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender39" TargetControlID="rota4" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel37" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel37" runat="server"><%=rota4.AlternateText%></asp:Panel>

                    <asp:Image ID="rota5"  runat="server"   class="rota2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender40" TargetControlID="rota5" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel38" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel38" runat="server"><%=rota5.AlternateText%></asp:Panel>

                    <asp:Image ID="rota6"  runat="server"   class="rota1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender41" TargetControlID="rota6" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel39" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel39" runat="server"><%=rota6.AlternateText%></asp:Panel>

                    <asp:Image ID="milangaz"  runat="server"   class="milangaz"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender42" TargetControlID="milangaz" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel40" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel40" runat="server"><%=milangaz.AlternateText%></asp:Panel>

                    <asp:Image ID="igsas1"  runat="server"   class="igsas2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender43" TargetControlID="igsas1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel41" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel41" runat="server"><%=igsas1.AlternateText%></asp:Panel>

                    <asp:Image ID="igsas2"  runat="server"   class="igsas2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender44" TargetControlID="igsas2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel42" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel42" runat="server"><%=igsas2.AlternateText%></asp:Panel>

                    <asp:Image ID="igsas3"  runat="server"   class="igsas2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender45" TargetControlID="igsas3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel43" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel43" runat="server"><%=igsas3.AlternateText%></asp:Panel>

                    <asp:Image ID="igsas4"  runat="server"   class="igsas1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender46" TargetControlID="igsas4" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel44" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel44" runat="server"><%=igsas4.AlternateText%></asp:Panel>

                    <asp:Image ID="igsas5"  runat="server"   class="igsas1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender47" TargetControlID="igsas5" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel45" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel45" runat="server"><%=igsas5.AlternateText%></asp:Panel>

                    <asp:Image ID="igsas6"  runat="server"   class="igsas3"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender48" TargetControlID="igsas6" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel46" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel46" runat="server"><%=igsas6.AlternateText%></asp:Panel>

                    <asp:Image ID="igsas7"  runat="server"   class="igsas3"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender49" TargetControlID="igsas7" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel47" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel47" runat="server"><%=igsas7.AlternateText%></asp:Panel>

                    <asp:Image ID="dp1"  runat="server"   class="dp1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender50" TargetControlID="dp1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel48" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel48" runat="server"><%=dp1.AlternateText%></asp:Panel>

                    <asp:Image ID="dp2"  runat="server"   class="dp1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender51" TargetControlID="dp2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel49" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel49" runat="server"><%=dp2.AlternateText%></asp:Panel>

                    <asp:Image ID="dp3"  runat="server"   class="dp1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender52" TargetControlID="dp3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel50" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel50" runat="server"><%=dp3.AlternateText%></asp:Panel>

                    <asp:Image ID="dp4"  runat="server"   class="dp2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender53" TargetControlID="dp4" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel51" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel51" runat="server"><%=dp4.AlternateText%></asp:Panel>

                    <asp:Image ID="dp5"  runat="server"   class="dp2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender54" TargetControlID="dp5" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel52" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel52" runat="server"><%=dp5.AlternateText%></asp:Panel>

                    <asp:Image ID="dp6"  runat="server"   class="dp2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender55" TargetControlID="dp6" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel53" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel53" runat="server"><%=dp6.AlternateText%></asp:Panel>

                    <asp:Image ID="oyak1"  runat="server"   class="oyak"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender56" TargetControlID="oyak1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel54" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel54" runat="server"><%=oyak1.AlternateText%></asp:Panel>

                    <asp:Image ID="oyak2"  runat="server"   class="oyak"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender57" TargetControlID="oyak2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel55" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel55" runat="server"><%=oyak2.AlternateText%></asp:Panel>

                    <asp:Image ID="oyak3"  runat="server"   class="oyak"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender58" TargetControlID="oyak3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel56" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel56" runat="server"><%=oyak3.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf11"  runat="server"   class="tupf1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender59" TargetControlID="tupf11" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel57" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel57" runat="server"><%=tupf11.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf12"  runat="server"   class="tupf1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender60" TargetControlID="tupf12" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel58" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel58" runat="server"><%=tupf12.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf13"  runat="server"   class="tupf1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender61" TargetControlID="tupf13" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel59" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel59" runat="server"><%=tupf13.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf14"  runat="server"   class="tupf1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender62" TargetControlID="tupf14" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel60" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel60" runat="server"><%=tupf14.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf15"  runat="server"   class="tupf1a"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender63" TargetControlID="tupf15" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel61" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel61" runat="server"><%=tupf15.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf16"  runat="server"   class="tupf1a"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender64" TargetControlID="tupf16" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel62" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel62" runat="server"><%=tupf16.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf21"  runat="server"   class="tupf2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender65" TargetControlID="tupf21" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel63" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel63" runat="server"><%=tupf21.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf22"  runat="server"   class="tupf2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender66" TargetControlID="tupf22" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel64" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel64" runat="server"><%=tupf22.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf31"  runat="server"   class="tupf3"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender67" TargetControlID="tupf31" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel65" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel65" runat="server"><%=tupf31.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf32"  runat="server"   class="tupf3"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender68" TargetControlID="tupf32" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel66" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel66" runat="server"><%=tupf32.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf33"  runat="server"   class="tupf3"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender69" TargetControlID="tupf33" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel67" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel67" runat="server"><%=tupf33.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf34"  runat="server"   class="tupf3"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender70" TargetControlID="tupf34" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel68" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel68" runat="server"><%=tupf34.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf35"  runat="server"   class="tupf3"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender71" TargetControlID="tupf35" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel69" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel69" runat="server"><%=tupf35.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf36"  runat="server"   class="tupf3"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender72" TargetControlID="tupf36" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel70" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel70" runat="server"><%=tupf36.AlternateText%></asp:Panel>

                    <asp:Image ID="tupf37"  runat="server"   class="tupf3a"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender73" TargetControlID="tupf37" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel71" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel71" runat="server"><%=tupf37.AlternateText%></asp:Panel>

                    <asp:image   id="tupp"  runat="server"  class="tupp"/> 
<asp:BalloonPopupExtender ID="BalloonPopupExtender1" TargetControlID="tupp" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel72" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel72" runat="server"><%=tupp.AlternateText%></asp:Panel>

                    <asp:Image ID="poas1"  runat="server"   class="poas1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender74" TargetControlID="poas1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel73" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel73" runat="server"><%=poas1.AlternateText%></asp:Panel>

                    <asp:Image ID="poas2"  runat="server"   class="poas1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender75" TargetControlID="poas2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel74" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel74" runat="server"><%=poas2.AlternateText%></asp:Panel>

                    <asp:Image ID="poas3"  runat="server"   class="poas2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender76" TargetControlID="poas3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel75" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel75" runat="server"><%=poas3.AlternateText%></asp:Panel>
                       
                    <asp:Image ID="shell"  runat="server"   class="shell"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender77" TargetControlID="shell" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel76" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel76" runat="server"><%=shell.AlternateText%></asp:Panel>

                    <asp:Image ID="koruma1"  runat="server"   class="koruma2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender78" TargetControlID="koruma1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel77" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel77" runat="server"><%=koruma1.AlternateText%></asp:Panel>

                    <asp:Image ID="koruma2"  runat="server"   class="koruma2"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender79" TargetControlID="koruma2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel78" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel78" runat="server"><%=koruma2.AlternateText%></asp:Panel>

                    <asp:Image ID="koruma3"  runat="server"   class="koruma1"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender80" TargetControlID="koruma3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel79" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel79" runat="server"><%=koruma3.AlternateText%></asp:Panel>

                    <asp:Image ID="aktas"  runat="server"   class="aktas"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender81" TargetControlID="aktas" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel80" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel80" runat="server"><%=aktas.AlternateText%></asp:Panel>

                    <asp:Image ID="ford1"  runat="server"   class="ford"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender82" TargetControlID="ford1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel81" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel81" runat="server"><%=ford1.AlternateText%></asp:Panel>

                    <asp:Image ID="ford2"  runat="server"   class="ford"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender83" TargetControlID="ford2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel82" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel82" runat="server"><%=ford2.AlternateText%></asp:Panel>

                    <asp:Image ID="auto1"  runat="server"   class="auto"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender84" TargetControlID="auto1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel83" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel83" runat="server"><%=auto1.AlternateText%></asp:Panel>

                    <asp:Image ID="auto2"  runat="server"   class="auto"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender85" TargetControlID="auto2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel84" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel84" runat="server"><%=auto2.AlternateText%></asp:Panel>

                    <asp:Image ID="auto3"  runat="server"   class="auto"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender86" TargetControlID="auto3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel85" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel85" runat="server"><%=auto3.AlternateText%></asp:Panel>

                    <asp:Image ID="auto4"  runat="server"   class="auto"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender87" TargetControlID="auto4" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel86" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel86" runat="server"><%=auto4.AlternateText%></asp:Panel>
                                             
                    <asp:Image ID="limas1"  runat="server"   class="limas"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender88" TargetControlID="limas1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel87" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel87" runat="server"><%=limas1.AlternateText%></asp:Panel>

                    <asp:Image ID="limas2"  runat="server"   class="limas"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender89" TargetControlID="limas2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel88" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel88" runat="server"><%=limas2.AlternateText%></asp:Panel>

                    <asp:Image ID="limas3"  runat="server"   class="limas"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender90" TargetControlID="limas3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel89" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel89" runat="server"><%=limas3.AlternateText%></asp:Panel>

                    <asp:Image ID="limas4"  runat="server"   class="limas"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender91" TargetControlID="limas4" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel90" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel90" runat="server"><%=limas4.AlternateText%></asp:Panel>

                    <asp:Image ID="limas5"  runat="server"   class="limas"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender92" TargetControlID="limas5" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel91" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel91" runat="server"><%=limas5.AlternateText%></asp:Panel>

                    <asp:Image ID="limas6"  runat="server"   class="limas"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender93" TargetControlID="limas6" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel92" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel92" runat="server"><%=limas6.AlternateText%></asp:Panel>

                    <asp:Image ID="limas7"  runat="server"   class="limas"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender94" TargetControlID="limas7" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel93" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel93" runat="server"><%=limas7.AlternateText%></asp:Panel>

                    <asp:Image ID="limas8"  runat="server"   class="limas"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender95" TargetControlID="limas8" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel94" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel94" runat="server"><%=limas8.AlternateText%></asp:Panel>
                       
                    <asp:Image ID="kosbas1"  runat="server"   class="kosbas"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender96" TargetControlID="kosbas1" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel95" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel95" runat="server"><%=kosbas1.AlternateText%></asp:Panel>
                       
                    <asp:Image ID="kosbas2"  runat="server"   class="kosbas"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender97" TargetControlID="kosbas2" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel96" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel96" runat="server"><%=kosbas2.AlternateText%></asp:Panel>

                    <asp:Image ID="kosbas3"  runat="server"   class="kosbas"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender98" TargetControlID="kosbas3" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel97" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel97" runat="server"><%=kosbas3.AlternateText%></asp:Panel>
                       
                   <asp:Image ID="kosbas4"  runat="server"   class="kosbas"/>
<asp:BalloonPopupExtender ID="BalloonPopupExtender99" TargetControlID="kosbas4" UseShadow="true" DisplayOnClick="true" CacheDynamicResults="false"
Position="BottomRight" BalloonPopupControlID="Panel98" BalloonStyle="Rectangle" DisplayOnFocus="true" runat="server" BalloonSize="Small"  />
<asp:Panel ID="Panel98" runat="server"><%=kosbas4.AlternateText%></asp:Panel>                                        
                  </div>
               



               </ContentTemplate></asp:UpdatePanel>

           </asp:Panel>
  



    </form>
</body>
</html>
