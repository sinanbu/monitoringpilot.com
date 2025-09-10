<%@ Page Language="C#" AutoEventWireup="true" CodeFile="arge.aspx.cs" Inherits="yonet_arge"  MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>PMTR</title>

    <link href="css/all.css" rel="stylesheet" />


</head>

<body>
    <form id="form1" runat="server">
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

   <div id="main">
  <div id="header"> <a href="#" class="logo"><img src="img/logo.png" width="46" height="48" alt="" /></a>
<div id="header-right"><asp:Literal ID="Litpagebaslik" runat="server" ></asp:Literal></div>
    <ul id="top-navigation">
      <li class="active" ><span><span>Settings</span></span></li>
      <li><span><span><a href="Statistics.aspx">Statistics</a></span></span></li>
      <li><span><span><a href="vacations.aspx">Vacations</a></span></span></li>
      <li><span><span><a href="onlineusers.aspx">Online Users</a></span></span></li>
      <li><span><span><a href="../main.aspx">Pilot Monitor</a></span></span></li>
      <li><span><span> <asp:LinkButton ID="LinkButton1" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton></span></span></li>
    </ul>
  </div>
  <div id="middle">

    <div id="left-column"> 
      <h3>Users</h3>
      <ul class="nav"><asp:Literal ID="Litmenu1" Text="" runat="server"> 
        <li><a href="Admins.aspx">Admins</a></li></asp:Literal>
        <li><a href="operators.aspx">Operators</a></li>
        <li><a href="pilots.aspx">Pilots</a></li>
      </ul>

      <h3>Places</h3>
      <ul class="nav">
        <li><a href="portsandberths.aspx">Ports and Berths</a></li>
        <li><a href="anchorageplaces.aspx">Anchorage Places</a></li>

      </ul>

      <h3>Distances</h3><ul class="nav">
        <li><a href="distances.aspx">Distances</a></li></ul>

      <h3>Flags</h3> <ul class="nav">        
        <li><a href="flags.aspx">Flags</a></li></ul>

      <h3>Ship Types</h3><ul class="nav">
        <li><a href="shiptypes.aspx">Ship Types</a></li></ul>

      <h3>Pilot Stations</h3><ul class="nav">
        <li><a href="pilotstations.aspx">Pilot Stations</a></li></ul>

</div>


       <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
    <div id="center-column">
      <div class="top-bar"> 
         
          <ul>
  <li class="sagusttuslist"><asp:LinkButton ID="LBaddnewportberth" class="sagusttuslista" runat="server" OnClick="LBaddnewportberth_Click">Add New AR-GE</asp:LinkButton></li>
</ul>

        <h1><a href="yonetim.aspx">Settings</a> > ARGE</h1>
          
                  <div class="breadcrumbs"></div>
      </div>
      <br />
      <div class="select-bar">
        <label>
           <%-- <asp:TextBox ID="TBtextara" runat="server"></asp:TextBox>--%>
        </label>
        <label>
           <%-- <asp:Button ID="LBSubmit" runat="server" Text="Search" OnClick="LBSubmit_Click"></asp:Button>--%>
        </label>
      </div>


      <div class="table">
        


       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="747px" 
            OnRowCommand="GridView1_RowCommand"  OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"  OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="2" ForeColor="#333333"  GridLines="Vertical"  AllowPaging="True" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>

<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/arrowr.png"  HeaderText=""  ItemStyle-Width="27px"  SelectText="" ShowSelectButton="True" />

<asp:TemplateField HeaderText="No" ControlStyle-Width="20px" >
                    <FooterTemplate></FooterTemplate>
                    <ItemTemplate><asp:Label ID="Litemportlimanno" runat="server" Text='<%# Bind("anketno") %>'></asp:Label></ItemTemplate>

                    <ControlStyle  ></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Trebuchet MS" Font-Size="Small" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
            </asp:TemplateField>

<asp:TemplateField HeaderText="Anket Adý"  ControlStyle-Width="140px" >
                    <EditItemTemplate> <asp:TextBox ID="TBeditportlimanadi" runat="server" CssClass="kucukharf"  Text='<%# Bind("anketadi") %>'></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:LinkButton ID="Litemportlimanadi" runat="server"  Text='<%# Bind("anketadi") %>' CommandArgument="<%# Container.DataItemIndex %>" CommandName="linkle"></asp:LinkButton></ItemTemplate>

                     <ControlStyle></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
            </asp:TemplateField>

<asp:TemplateField HeaderText="Sayfa"  ControlStyle-Width="150px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditportgoster" runat="server" Text='<%# Bind("aciklama") %>'  ></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportgoster" runat="server" Text='<%# Bind("aciklama") %>'></asp:Label></ItemTemplate>

                    <ControlStyle  ></ControlStyle>
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
          </asp:TemplateField>

<asp:TemplateField HeaderText="1.Seç"  ControlStyle-Width="50px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditportsec" runat="server" Text='<%# Bind("secbir") %>'  ></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportsec" runat="server" Text='<%# Bind("secbir") %>'></asp:Label></ItemTemplate>

                    <ControlStyle  ></ControlStyle>
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
          </asp:TemplateField>


<asp:TemplateField HeaderText="2.Seç"  ControlStyle-Width="50px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditportsec2" runat="server" Text='<%# Bind("seciki") %>'  ></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportsec2" runat="server" Text='<%# Bind("seciki") %>'></asp:Label></ItemTemplate>

                    <ControlStyle  ></ControlStyle>
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
          </asp:TemplateField>


<asp:TemplateField HeaderText="3.Seç"  ControlStyle-Width="50px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditportsec3" runat="server" Text='<%# Bind("secuc") %>'  ></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportsec3" runat="server" Text='<%# Bind("secuc") %>'></asp:Label></ItemTemplate>

                    <ControlStyle  ></ControlStyle>
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
          </asp:TemplateField>

<asp:TemplateField HeaderText="Date"  ControlStyle-Width="50px" >
                    <ItemTemplate><asp:Label ID="Litemportsectarih" runat="server" Text='<%# Bind("ankettarihi") %>'></asp:Label></ItemTemplate>

                    <ControlStyle  ></ControlStyle>
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="XX-Small" />
          </asp:TemplateField>

<asp:TemplateField HeaderText="X"  ControlStyle-Width="20px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditportsecaktif" runat="server" Text='<%# Bind("aktif") %>'  ></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportsecaktif" runat="server" Text='<%# Bind("aktif") %>'></asp:Label></ItemTemplate>

                    <ControlStyle  ></ControlStyle>
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
          </asp:TemplateField>





<asp:CommandField  ItemStyle-Width="30px" ShowEditButton="True" HeaderText="Up"  EditImageUrl="../images/edit.png"  EditText=""   UpdateText="" UpdateImageUrl="../images/save.png"  CancelImageUrl="../images/cancel.png"   CancelText=""  ButtonType="Image" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Small"></ItemStyle>
                </asp:CommandField>

<asp:CommandField ItemStyle-Width="30px" ShowDeleteButton="True"  HeaderText="Del"   DeleteImageUrl="../images/cancelsil.png" DeleteText=""    ButtonType="Image">
            <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Small"></ItemStyle>
                </asp:CommandField>

            </Columns>

            <EditRowStyle BorderStyle="None" BackColor="#77bbdF" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />

        </asp:GridView>
    

        <asp:Button id="buttonshowpopuppadd" runat="server" style="display:none;" />
        <asp:Panel ID="panellbadd"   CssClass="panellbadd" runat="server"> 

<table  class="panellbadd">                        
                     <tr>
                <td   class="tdorta" >
                 <p >Add New Page </p></td>
            </tr>          
            <tr>
                <td  class="tdsag" ><div>Anket No: <br />
                    <asp:TextBox ID="TBpaddportno"  runat="server"  Enabled="false"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td  class="tdsag" ><div>Anket Adý: <br />
                    <asp:TextBox ID="TBpaddportname" runat="server"   CssClass="kucukharf" ></asp:TextBox>
                </td>
            </tr>
                        <tr>
                <td  class="tdsag" ><div>Açýklama: <br />
                    <asp:TextBox ID="TBpaddgoster"  runat="server" ></asp:TextBox>
                </td>
            </tr>
                <tr>
                <td  class="tdsag" ><div>1.Seçenek: <br />
                    <asp:TextBox ID="TBpaddsec"  runat="server" ></asp:TextBox>
                </td>
            </tr>
                <tr>
                <td  class="tdsag" ><div>2.Seçenek: <br />
                    <asp:TextBox ID="TBpaddsec2"  runat="server" ></asp:TextBox>
                </td>
            </tr>
                <tr>
                <td  class="tdsag" ><div>3.Seçenek: <br />
                    <asp:TextBox ID="TBpaddsec3"  runat="server" ></asp:TextBox>
                </td>
            </tr>
                <tr>
                <td  class="tdsag" ><div>Aktif <br />
                    <asp:TextBox ID="TBpaddsecaktif"  runat="server" ></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td  class="tdorta"  style="height:50px; text-align:center; font-size:20px; font-weight:bold; font-family:'Trebuchet MS'">
                       
                       <asp:Button ID="Buttonlbadd" runat="server" style="height:30px; Width:80px" Text="Save"  OnClick="Buttonlbadd_Click"  />&nbsp;&nbsp; 
                       <asp:Button ID="Buttonlbaddcancel" runat="server" style="height:30px; Width:80px" Text="Cancel"   />         
                </td></tr>         
           </table>

        </asp:Panel>

                  <br /> <br /> 
<asp:ModalPopupExtender 
            ID="ModalPopupExtenderlbadd" runat="server" 
            CancelControlID="Buttonlbaddcancel"
            TargetControlID="buttonshowpopuppadd" 
            PopupControlID="panellbadd"
            BackgroundCssClass="modalbodyarka" ></asp:ModalPopupExtender>

        <br />


          <%-- modal onay message baþlar  
    onay için bu panele kes yapýþtýr. Baccepted  butonuna onclick olayý yaz. --%>
 <asp:Button id="buttononayMessagePanel" runat="server" style="display:none;" /> 
        <asp:Panel ID="panelonayMessagePanel"    CssClass="panelmessage" runat="server" >  
    <div style="text-align:center; font-weight:bold;">        
<br /> Are you sure to delete the record? <br /><br /></div>
            <asp:Button ID="Baccepted" CommandName="0" runat="server" style="height:30px; Width:80px" Text="Yes"  OnClick="Baccepted_Click"  />&nbsp;&nbsp; 
            <asp:Button ID="Bclosed" runat="server" style="height:30px; Width:80px" Text="Cancel" />  
<br /><br />
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopuponayMessage" runat="server" TargetControlID="buttononayMessagePanel" PopupControlID="panelonayMessagePanel" BackgroundCssClass="modalbodyarka" CancelControlID="Bclosed" ></asp:ModalPopupExtender>
<%-- modal onay message biter --%>

            </ContentTemplate></asp:UpdatePanel>

      <!--
        <div class="select"> <strong>Other Pages: </strong>
          <select>
            <option>1</option>
          </select>
        </div>
      -->

      </div>
    </div>
    
  </div>
  <div id="footer"></div>   

</div>





    
  
    </form>
</body>
</html>
