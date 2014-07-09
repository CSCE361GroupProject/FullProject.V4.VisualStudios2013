<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.vb" Inherits="CSCE361.Group3.WebPages.Login1" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
       <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>

    <script type="text/javascript">
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
</script>
</asp:Content>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>--%>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <div id="primary">
        <asp:Image ID="Image2" runat="server" CssClass="img"/>
    </div>

    <div id="content">
        <center>
            <p>Hello Huskers!</p>
            <p>Welcome to the UNL Campus Map project! To begin uploading your pictures of campus, click on the "Twitter" button below. </p>
            <p>Once logged in, new users will encounter a brief registration and existing users will be taken directly to their profile. </p>
            <p>We look forward to seeing your photos!</p> <br />
            
            <label>Username:</label> <asp:TextBox ID="tbUserName" runat="server" /> <br />
           
            <asp:Button ID="btnTwitter" runat="server" Text="Click here to login"/> <br />

            <asp:Label ID="lblSuccess" runat="server" Visible="false" />        
            <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
        </center>
    </div>

    <div id="secondary">
       <asp:Image ID="Image1" runat="server" CssClass="img"/>
    </div>

    <div class="loader"></div>

</asp:Content>
