<%@ Page Language="C#" AutoEventWireup="true" CodeFile="portinfo.aspx.cs" Inherits="portinfo" EnableEventValidation="false" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html  xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot - PORT INFO</title>
 <link href="css/stil.css" rel="stylesheet" />
    <style type="text/css">
   
      
        .auto-style1 {
            width: 100%; 
                 }
.panellbadd
{
border: 1px  groove #111;
background-color:white;
}
.modalbodyarka
{
background-color: #333333;
filter: alpha(opacity:70);
opacity: 0.6;
z-index: 10000;
}
 
.my_style a {
        text-decoration: none;
    }


    .my_style a:hover {
        text-decoration: underline;
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


.kucukharf {
    text-transform:lowercase;
    direction:ltr;
    overflow:auto;
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

</head>
<body onkeydown = "return (event.keyCode!=13)" >
    <form id="form1" runat="server" >

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
                                <asp:Timer ID="gerisayimtik" runat="server" OnTick="gerisayimtik_Tick" Interval="300000"></asp:Timer>
                                <asp:Label ID="LblReeltime" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </asp:Panel>
                    
                </td>
                <td style="text-align: right; width: 10%; ">

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
                    <li id="mainmanu7" runat="server"><a href="mapsection.aspx" target="_blank">Map</a></li>
                </ul>
                            </li>

                        </ul>
                    </div>

                </td>
            </tr>
        </table>

        <div style="width: 100%; text-align: left;">
 <asp:Panel ID="summariall" runat="server" >


<table  style="width:1340px; float:right;   color:white">
 <tr  style="height:22px;  border-color:red; ">
     <td style="text-align:left;">
                      <asp:Button ID="ButtonLiveScreen" style="height:25px; Width:80px; font-size:x-small;  text-align:center;" runat="server" Text="Live Screen" OnClick="ButtonLiveScreen_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"   /> &nbsp;&nbsp;&nbsp;

     </td>
        <td style="text-align:right">
                <asp:LinkButton ID="LBonline" runat="server"  ForeColor="#111" OnClick="LBonline_Click"  OnClientClick="this.disabled='true'; "  ></asp:LinkButton> &nbsp;&nbsp
                <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="#111"  OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "  ></asp:LinkButton>&nbsp;&nbsp;
 </td></tr></table>  



<table  style="width:100% ;"> <tr><td style="text-align:left; width:100% ; height:25px"></td></tr>
<tr><td style="text-align:left; width:100% ;"><span style="font-family:'Trebuchet MS'; color:#222; font-size:small; margin:0; "><b>PORT INFORMATIONS</b><br /><br /></span></td>
</tr></table>  


     
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>          

   
      <div class="clearup"> </div>
   <div  style="clear:both; text-align:left;"> 

      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="id" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"  OnSelectedIndexChanging="GridView1_SelectedIndexChanging"  CellPadding="2" ForeColor="#222222"  GridLines="Vertical" OnRowCommand="GridView1_RowCommand1" >

            <Columns>
                
<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/arrowr.png"  HeaderText=""  SelectText=""   ShowSelectButton="True" />

                                <asp:TemplateField ControlStyle-Width="40px" ItemStyle-HorizontalAlign="center" HeaderText="No">
                                     <ItemTemplate >
                                         <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate><ItemStyle Width="40px" />
                                 </asp:TemplateField>


<asp:TemplateField HeaderText="Port Name"  ControlStyle-Width="130px"  >
                    <ItemTemplate><asp:LinkButton ID="Litemportlimanadi" runat="server" Text='<%# Bind("limanadi") %>'  CommandArgument="<%# Container.DataItemIndex %>"  CommandName="linkle" CssClass="my_style"></asp:LinkButton></ItemTemplate>
                <ItemStyle Width="130px"  Height="25px"/></asp:TemplateField>

<asp:TemplateField HeaderText="Responsible"   ControlStyle-Width="130px" ControlStyle-BorderWidth="0px" ControlStyle-Height="25px"  >
                    <EditItemTemplate><asp:TextBox ID="TBeditportrihtimadi" runat="server" CssClass="kucukharf"  Text='<%# Bind("yetkili") %>' MaxLength="30" ></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportrihtimadi" runat="server" Text='<%# Bind("yetkili") %>'></asp:Label> </ItemTemplate>
                <ItemStyle Width="130px"  Height="25px"/></asp:TemplateField>

<asp:TemplateField HeaderText="Tel.no"   ControlStyle-Width="80px" ControlStyle-BorderWidth="0px" ControlStyle-Height="25px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditportlimanbolge" runat="server" Text='<%# Bind("telno") %>' MaxLength="11" ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="TBeditportlimanbolge" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender> </EditItemTemplate>
                    <ItemTemplate> <asp:Label ID="Litemportlimanbolge" runat="server" Text='<%# Bind("telno") %>'></asp:Label> </ItemTemplate>
                <ItemStyle Width="80px"  Height="25px" /></asp:TemplateField>

<asp:TemplateField HeaderText="Gsm no"  ControlStyle-Width="80px" ControlStyle-BorderWidth="0px" ControlStyle-Height="25px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditportzorluk" runat="server" MaxLength="11" Text='<%# Bind("cepno") %>' ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="TBeditportzorluk" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportzorluk" runat="server" Text='<%# Bind("cepno") %>'></asp:Label></ItemTemplate>
                <ItemStyle Width="80px"  Height="25px" /></asp:TemplateField>

<asp:TemplateField HeaderText="Fax no"   ControlStyle-Width="80px" ControlStyle-BorderWidth="0px" ControlStyle-Height="25px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditportresp" runat="server" Text='<%# Bind("faxno") %>' MaxLength="11" ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" TargetControlID="TBeditportresp" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportresp" runat="server" Text='<%# Bind("faxno") %>'></asp:Label></ItemTemplate>
<ItemStyle Width="80px"  Height="25px" /></asp:TemplateField>

<asp:TemplateField HeaderText="Warning" ControlStyle-Width="550px" ControlStyle-BorderWidth="0px"  >
                    <EditItemTemplate><asp:TextBox ID="TBeditportkalkissuresi" runat="server" Height="250px"  CssClass="kucukharf"   TextMode="MultiLine" Text='<%# Bind("uyari") %>' ></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportkalkissuresi" runat="server" Text='<%# Bind("uyari") %>'></asp:Label></ItemTemplate>
<ItemStyle Width="550px"  Height="25px" />
</asp:TemplateField>

                <asp:TemplateField HeaderText="Plan"   ControlStyle-Width="150px" ControlStyle-BorderWidth="0px" ControlStyle-Height="25px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditportgoster" runat="server" Text='<%# Bind("kroki") %>' MaxLength="200"  ></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportgoster" runat="server" Text='<%# Bind("kroki") %>'  ></asp:Label></ItemTemplate>
             <ItemStyle Width="150px"  Height="25px"/></asp:TemplateField>

               <asp:TemplateField HeaderText="Plan"   ControlStyle-Width="150px" ControlStyle-BorderWidth="0px" ControlStyle-Height="25px" >
                    <ItemTemplate><asp:Label ID="Litemportgostery" runat="server" Text='<%# Bind("kroki") %>'  ></asp:Label></ItemTemplate>
             <ItemStyle Width="150px"  Height="25px"/></asp:TemplateField>

<asp:CommandField  ItemStyle-Width="100px" ShowEditButton="True"  EditImageUrl="images/edit.png"  EditText=""   UpdateText="" UpdateImageUrl="images/save.png"  CancelImageUrl="images/cancel.png"   CancelText=""  ButtonType="Image" >
                <ItemStyle Width="100px"  HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:CommandField>

                
            </Columns>

           
            <EditRowStyle BorderStyle="None" BackColor="#77bbdF" />
            <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#fff7ff"   Font-Size="12px" />
            <AlternatingRowStyle BackColor="#ffe7ff"   Font-Size="12px"/>
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#111" />

        </asp:GridView>


                 <br />




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
                       <asp:Button ID="Buttonlbaddcancel" runat="server" style="height:30px; Width:80px" Text="Cancel"  OnClick="Buttonlbaddcancel_Click" />         
                </td></tr>         
           </table>

        </asp:Panel>
           
        <asp:ModalPopupExtender 
            ID="MPEdrawing" runat="server" 
            TargetControlID="buttonshowpopuppadd" 
            PopupControlID="panellbadd"
            BackgroundCssClass="modalbodyarka" ></asp:ModalPopupExtender>

        <br />

       </div>
                    <div style="height:150px; clear:both; float:left;">  <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label></div>
                 </ContentTemplate></asp:UpdatePanel>

           </asp:Panel>
  

    </div>

    </form>
</body>
</html>
