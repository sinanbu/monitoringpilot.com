<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sipd.aspx.cs" Inherits="sipd" EnableEventValidation="false" %>
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
    <style type="text/css">
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

           }
           .taban
           {
               /*position:static;
               vertical-align:top;
               top:0px;
               left:0px;
               width:100%;
               height:auto;*/

           }
           .menualt
           {
               border:0px solid red;
               position:fixed;
               z-index:1000;
               bottom:0%;
               right:0%;
               width:9%;
               height:30%;
               background-color:darkorange;
           }
           .tusyar
           {
               border:1px solid black;
               border-radius:2px;
               position:relative;
               background-color:white;
               width:94%;
               height:8%;
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




           .aslan1 {
               position:absolute;
               z-index:1;
  transform: rotate(-47deg);
  -webkit-transform:rotate(-47deg);
  -moz-transform: rotate(-47deg);
  -ms-transform: rotate(-47deg);
  -o-transform: rotate(-47deg);

           }

                      .aslan2 {
                position:absolute;
               z-index:1;
                 transform: rotate(40deg);
  -webkit-transform:rotate(40deg);
  -moz-transform: rotate(40deg);
  -ms-transform: rotate(40deg);
  -o-transform: rotate(40deg);
           }

         .aslan3 {
                position:absolute;
               z-index:1;
                 transform: rotate(220deg);
  -webkit-transform:rotate(220deg);
  -moz-transform: rotate(220deg);
  -ms-transform: rotate(220deg);
  -o-transform: rotate(220deg);
           }
      .belde {
                position:absolute;
               z-index:1;
                 transform: rotate(230deg);
  -webkit-transform:rotate(230deg);
  -moz-transform: rotate(230deg);
  -ms-transform: rotate(230deg);
  -o-transform: rotate(230deg);
           }

 .poliport {
                position:absolute;
               z-index:1;
                 transform: rotate(273deg);
  -webkit-transform:rotate(273deg);
  -moz-transform: rotate(273deg);
  -ms-transform: rotate(273deg);
  -o-transform: rotate(273deg);
           }


 .colak1 {
                position:absolute;
               z-index:1;
                 transform: rotate(268deg);
  -webkit-transform:rotate(268deg);
  -moz-transform: rotate(268deg);
  -ms-transform: rotate(268deg);
  -o-transform: rotate(268deg);
           }

  .colak2 {
                position:absolute;
               z-index:1;
           }

  .yilport1 {
                position:absolute;
               z-index:1;
                 transform: rotate(259deg);
  -webkit-transform:rotate(259deg);
  -moz-transform: rotate(259deg);
  -ms-transform: rotate(259deg);
  -o-transform: rotate(259deg);
           }
    .yilport2 {
                position:absolute;
               z-index:1;
         transform: rotate(27deg);
 -webkit-transform: rotate(27deg);
    -moz-transform: rotate(27deg);
     -ms-transform: rotate(27deg);
      -o-transform: rotate(27deg);
           }
        .yilport3 {
                position:absolute;
               z-index:1;
         transform: rotate(230deg);
  -webkit-transform:rotate(230deg);
    -moz-transform: rotate(230deg);
     -ms-transform: rotate(230deg);
      -o-transform: rotate(230deg);
           }
            .yilport4 {
                position:absolute;
               z-index:1;
         transform: rotate(187deg);
  -webkit-transform:rotate(187deg);
    -moz-transform: rotate(187deg);
     -ms-transform: rotate(187deg);
      -o-transform: rotate(187deg);
           }
                .yilport5 {
                position:absolute;
               z-index:1;
                 transform: rotate(273deg);
  -webkit-transform:rotate(273deg);
  -moz-transform: rotate(273deg);
  -ms-transform: rotate(273deg);
  -o-transform: rotate(273deg);
           }
                    .yilport6 {
                position:absolute;
               z-index:1;
                 transform: rotate(350deg);
  -webkit-transform:rotate(350deg);
  -moz-transform: rotate(350deg);
  -ms-transform: rotate(350deg);
  -o-transform: rotate(350deg);
           }
.altintel1{
                position:absolute;
               z-index:1;
                 transform: rotate(131deg);
          -webkit-transform:rotate(131deg);
            -moz-transform: rotate(131deg);
             -ms-transform: rotate(131deg);
              -o-transform: rotate(131deg);
           }

.solventas1{
                position:absolute;
               z-index:1;
                 transform: rotate(262deg);
         -webkit-transform: rotate(262deg);
            -moz-transform: rotate(262deg);
             -ms-transform: rotate(262deg);
              -o-transform: rotate(262deg);
           }
.solventas2{
                position:absolute;
               z-index:1;
                 transform: rotate(249deg);
         -webkit-transform: rotate(249deg);
            -moz-transform: rotate(249deg);
             -ms-transform: rotate(249deg);
              -o-transform: rotate(249deg);
           }
.efesan1{
                position:absolute;
               z-index:1;
                 transform: rotate(227deg);
         -webkit-transform: rotate(227deg);
            -moz-transform: rotate(227deg);
             -ms-transform: rotate(227deg);
              -o-transform: rotate(227deg);
           }
.efesan2{
                position:absolute;
               z-index:1;
                 transform: rotate(158deg);
         -webkit-transform: rotate(158deg);
            -moz-transform: rotate(158deg);
             -ms-transform: rotate(158deg);
              -o-transform: rotate(158deg);
           }
.generji1{
                position:absolute;
               z-index:1;
                 transform: rotate(324deg);
         -webkit-transform: rotate(328deg);
            -moz-transform: rotate(324deg);
             -ms-transform: rotate(324deg);
              -o-transform: rotate(324deg);
           }
.generji2{
                position:absolute;
               z-index:1;
                 transform: rotate(52deg);
         -webkit-transform: rotate(60deg);
            -moz-transform: rotate(52deg);
             -ms-transform: rotate(52deg);
              -o-transform: rotate(52deg);
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
                   <asp:Timer ID="gerisayimtik" runat="server" OnTick="gerisayimtik_Tick" Interval="3000"></asp:Timer>

           
                
                   <div id="taban" runat="server" class="harita" >
                      <%-- <button id="button">Get size</button>
                        <div  id="screenSize" ></div>
                             <div   id="windowSize"></div>
                                  <div   id="contentSize"></div>--%>

                      <%-- <asp:TextBox id="harita" runat="server" Text="test"></asp:TextBox>--%>

                       
       
                     <div   id="menualt" runat="server" class="menualt">
                         
<asp:LinkButton ID="LBmapdarica" runat="server" text="DARICA SHIPS" CssClass="tusyar" OnClick="LBmapdarica_Click"/></br>
                                       <asp:label runat="server" id="label1" class="label">In Ports:</asp:label>        </br>   
                                       <asp:label runat="server" id="label2" class="label">Esk.Anch:</asp:label>        </br>
                                        </br> 
<asp:LinkButton ID="LBmapyarimca" runat="server" text="YARIMCA SHIPS" CssClass="tusyar" OnClick="LBmapyarimca_Click"/></br>
                                       <asp:label runat="server" id="label3" class="label">In Ports:</asp:label>        </br>   
                                       <asp:label runat="server" id="label4" class="label">Her.Anch:</asp:label>        </br> 
                                       <asp:label runat="server" id="label5" class="label">Yar.Anch:</asp:label>        </br> 
                                       <asp:label runat="server" id="label6" class="label">Izm.Anch:</asp:label>        </br> 

                     </div>

                       <asp:Image ID="harita" runat="server" ImageUrl="~/images/bolgeler/shipsonmapdar.jpg"  CommandName="" class="harita"/>


                    <asp:ImageButton ID="aslan1" runat="server"  CommandName="" class="aslan1"/>
                    <asp:ImageButton ID="aslan2" runat="server"  CommandName="" class="aslan2"/>
                    <asp:ImageButton ID="aslan3" runat="server"  CommandName="" class="aslan3"/>
                    <asp:ImageButton ID="aslan4" runat="server"  CommandName="" class="aslan3"/>

                    <asp:ImageButton ID="belde1"  runat="server" CommandName=""  class="belde"/>
                    <asp:ImageButton ID="belde2" runat="server"  CommandName="" class="belde"/>
                    <asp:ImageButton ID="belde3" runat="server"  CommandName="" class="belde"/>                    
                    <asp:ImageButton ID="belde4" runat="server"  CommandName="" class="belde"/>
                 
                    <asp:ImageButton ID="poliport1"  runat="server" CommandName=""  class="poliport"/>
                    <asp:ImageButton ID="poliport2"  runat="server" CommandName=""  class="poliport"/>
                    <asp:ImageButton ID="poliport3"  runat="server" CommandName=""  class="poliport"/>
                    <asp:ImageButton ID="poliport4"  runat="server" CommandName=""  class="poliport"/>
                    <asp:ImageButton ID="poliport5"  runat="server" CommandName=""  class="poliport"/>
                    <asp:ImageButton ID="poliport6"  runat="server" CommandName=""  class="poliport"/>
                    <asp:ImageButton ID="poliport7"  runat="server" CommandName=""  class="poliport"/>
                    <asp:ImageButton ID="poliport8"  runat="server" CommandName=""  class="poliport"/>
                    <asp:ImageButton ID="poliport9"  runat="server" CommandName=""  class="poliport"/>
                    <asp:ImageButton ID="poliport10"  runat="server" CommandName=""  class="poliport"/>

                    <asp:ImageButton ID="colakoglu1"  runat="server" CommandName=""  class="colak1"/>
                    <asp:ImageButton ID="colakoglu2"  runat="server" CommandName=""  class="colak1"/>
                    <asp:ImageButton ID="colakoglu3"  runat="server" CommandName=""  class="colak1"/>
                    <asp:ImageButton ID="colakoglu4"  runat="server" CommandName=""  class="colak2"/>
                    <asp:ImageButton ID="colakoglu5"  runat="server" CommandName=""  class="colak2"/>
                    <asp:ImageButton ID="colakoglu6"  runat="server" CommandName=""  class="colak2"/>
                    <asp:ImageButton ID="colakoglu7"  runat="server" CommandName=""  class="colak2"/>

                    <asp:ImageButton ID="yilport1"  runat="server" CommandName=""  class="yilport1"/>
                    <asp:ImageButton ID="yilport2"  runat="server" CommandName=""  class="yilport1"/>
                    <asp:ImageButton ID="yilport3"  runat="server" CommandName=""  class="yilport2"/>
                    <asp:ImageButton ID="yilport4"  runat="server" CommandName=""  class="yilport2"/>
                    <asp:ImageButton ID="yilport5"  runat="server" CommandName=""  class="yilport3"/>
                    <asp:ImageButton ID="yilport6"  runat="server" CommandName=""  class="yilport3"/>
                    <asp:ImageButton ID="yilport7"  runat="server" CommandName=""  class="yilport4"/>
                    <asp:ImageButton ID="yilport8"  runat="server" CommandName=""  class="yilport4"/>
                    <asp:ImageButton ID="yilport9"  runat="server" CommandName=""  class="yilport5"/>
                    <asp:ImageButton ID="yilport10"  runat="server" CommandName=""  class="yilport6"/>

                    <asp:ImageButton ID="altintel1"  runat="server" CommandName=""  class="altintel1"/>
                    <asp:ImageButton ID="altintel2"  runat="server" CommandName=""  class="altintel1"/>
                    <asp:ImageButton ID="altintel3"  runat="server" CommandName=""  class="altintel1"/>

                    <asp:ImageButton ID="solventas1"  runat="server" CommandName=""  class="solventas1"/>
                    <asp:ImageButton ID="solventas2"  runat="server" CommandName=""  class="solventas1"/>
                    <asp:ImageButton ID="solventas3"  runat="server" CommandName=""  class="solventas1"/>
                    <asp:ImageButton ID="solventas4"  runat="server" CommandName=""  class="solventas1"/>
                    <asp:ImageButton ID="solventas5"  runat="server" CommandName=""  class="solventas2"/>
                    <asp:ImageButton ID="solventas6"  runat="server" CommandName=""  class="solventas2"/>
                    <asp:ImageButton ID="solventas7"  runat="server" CommandName=""  class="solventas2"/>
                    <asp:ImageButton ID="solventas8"  runat="server" CommandName=""  class="solventas2"/>

                    <asp:ImageButton ID="efesan1"  runat="server" CommandName=""  class="efesan1"/>
                    <asp:ImageButton ID="efesan2"  runat="server" CommandName=""  class="efesan1"/>
                    <asp:ImageButton ID="efesan3"  runat="server" CommandName=""  class="efesan1"/>
                    <asp:ImageButton ID="efesan4"  runat="server" CommandName=""  class="efesan1"/>
                    <asp:ImageButton ID="efesan5"  runat="server" CommandName=""  class="efesan2"/>
                    <asp:ImageButton ID="efesan6"  runat="server" CommandName=""  class="efesan2"/>

                    <asp:ImageButton ID="generji1"  runat="server" CommandName=""  class="generji1"/>
                    <asp:ImageButton ID="generji2"  runat="server" CommandName=""  class="generji1"/>
                    <asp:ImageButton ID="generji3"  runat="server" CommandName=""  class="generji2"/>

                    <asp:ImageButton ID="kroman1"  runat="server" CommandName=""  class="kroman"/>
                    <asp:ImageButton ID="kroman2"  runat="server" CommandName=""  class="kroman"/>
                    <asp:ImageButton ID="kroman3"  runat="server" CommandName=""  class="kroman"/>
                    <asp:ImageButton ID="kroman4"  runat="server" CommandName=""  class="kroman"/>
                    <asp:ImageButton ID="kroman5"  runat="server" CommandName=""  class="kroman"/>

                    <asp:ImageButton ID="diler1"  runat="server" CommandName=""  class="diler1"/>
                    <asp:ImageButton ID="diler2"  runat="server" CommandName=""  class="diler1"/>
                    <asp:ImageButton ID="diler3"  runat="server" CommandName=""  class="diler1"/>
                    <asp:ImageButton ID="diler4"  runat="server" CommandName=""  class="diler1"/>
                    <asp:ImageButton ID="diler5"  runat="server" CommandName=""  class="diler2"/>
                    <asp:ImageButton ID="diler6"  runat="server" CommandName=""  class="diler1"/>
                    <asp:ImageButton ID="diler7"  runat="server" CommandName=""  class="diler1"/>
                    <asp:ImageButton ID="diler8"  runat="server" CommandName=""  class="diler1"/>

                    <asp:ImageButton ID="nuh1"  runat="server" CommandName=""  class="nuh"/>
                    <asp:ImageButton ID="nuh2"  runat="server" CommandName=""  class="nuh"/>
                    <asp:ImageButton ID="nuh3"  runat="server" CommandName=""  class="nuh"/>
                    <asp:ImageButton ID="nuh4"  runat="server" CommandName=""  class="nuh"/>
                        
                  </div>
               
               </ContentTemplate></asp:UpdatePanel>

           </asp:Panel>
  



    </form>
</body>
</html>
