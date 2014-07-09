<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PictureDetails.aspx.vb" Inherits="CSCE361.Group3.WebPages.PictureDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
       <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>

    <script type="text/javascript">
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
</script>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="max-height:500px; width:1200px; float:left; border:none">
         <div id="image" class="DisplayPhoto">
             <asp:Image ID="imagePhoto" runat="server" CssClass="imagePhoto"/>
         </div>
    </div>
    <br />
    <div>
            <div style="text-align:center; width:1200px; float:left; height:25px; border-top: 1px solid black; background-color:transparent">
                <asp:Label ID="lblPicCaption" runat="server"/>
            </div>
            <div style="text-align:center; width:600px; float:left; height:46px; border-bottom:1px solid black; background-color:transparent">
                <asp:Label ID="lblPicUploader" runat="server" />
            </div>
            <div style="text-align:center; width:600px; float:left;border-bottom:1px solid black; background-color:transparent">
                <asp:Button ID="btnPicDelete" runat="server" Text="Delete"  OnClientClick="return confirm('Are you sure you want to delete this picture?');"/>
            </div>
    </div>
    <div style="display:block; height:15px; width:1200px" ></div>
    <div class="DisplayRepeater">
        <br />
        <asp:Repeater ID="rptComments" runat="server"> 
            <HeaderTemplate>
                <table style="width:100%; border:1">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="width:0px">
                        <asp:Label ID="lblcommentid" runat="server" Style="display:none" Text='<%# DataBinder.Eval(Container.DataItem, "CommentID")%>' />
                    </td>
                    <td style="width:0px">
                        <asp:Label ID="lbluserid" runat="server" Style="display:none" Text='<%# DataBinder.Eval(Container.DataItem, "Profile")%>' />
                    </td>
                    <td style="width:100px">
                        <%# DataBinder.Eval(Container.DataItem, "Username")%>
                    </td>
                    <td style="width:700px">
                        <%# DataBinder.Eval(Container.DataItem, "Content")%>
                    </td>
                    <td>
                        <asp:Button ID="rptbtnDelete" runat="server" CssClass="rptDelete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this comment?');" />
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="background-color: lightsteelblue">
                    <td style="width:0px">
                        <asp:Label ID="lblcommentid" runat="server" Style="display:none" Text='<%# DataBinder.Eval(Container.DataItem, "CommentID")%>' />
                    </td>
                    <td style="width:0px">
                        <asp:Label ID="lbluserid" runat="server" Style="display:none" Text='<%# DataBinder.Eval(Container.DataItem, "Profile")%>' />
                    </td>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "Username")%>
                    </td>
                    <td style="width:700px">
                        <%# DataBinder.Eval(Container.DataItem, "Content")%>
                    </td>
                    <td>
                        <asp:Button ID="rptbtnDelete" runat="server" CssClass="rptDelete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this comment?');" />
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div style="text-align:center">
        <div id="comments" runat="server" style="background-color:#FEFDFA; width:1150px; height:800px">
            <div id="commentSubmit" class="divCaptionSubmit">
                <div style="text-align:center; vertical-align:middle; height:100%">
                <br />
                    <asp:TextBox ID="tbAddComment" runat="server" Width="300px" Height="25px" MaxLength="40" BorderColor="#D00000"/>
                    <asp:Button ID="btnAddComment" runat="server" text="Submit Comment" /> <br />
                    <asp:Label ID="lblAddSuccess" runat="server" />
                </div>
            </div>
        </div>
    </div>

<div class="loader"></div>

</asp:Content>
