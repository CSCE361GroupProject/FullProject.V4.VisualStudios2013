<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="HomeProfile.aspx.vb" Inherits="CSCE361.Group3.WebPages.HomeProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyD2rcIH-iLZGgm8xneO7lNqPq5HlBYjbd4&sensor=false" >
    </script>

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
    <div id="profilemenu">
    <center>

<%--    <div id="profileinfo"> 
        <asp:Image ID="imgProfilePic" runat="server" CssClass="img2" /> <br />
        <asp:Label ID="lblName" runat="server" Text="FirstName LastName"/> <br /> <br />
    </div>--%>
    <br />  Menu: <br /> <%--<br />--%>
       
    <div>
        <asp:Label ID="lblCaption" runat="server" Text="Caption:"></asp:Label>
        <asp:TextBox ID="tbCaption" runat="server"></asp:TextBox> <br />
        <asp:FileUpload ID="fuPhoto" runat="server" /> <br /> <br />
       
        <asp:Button ID="btnUpload" runat="server" Text="Upload photo"  OnClientClick="return confirm('Are you sure you want to upload this picture?');" />
        
        <br />  
        
        <asp:Label ID="lblSuccess" runat="server"></asp:Label>

        <br /> <br />
        <asp:Button ID="btnViewAllPhotos" runat="server" Text="View all photos" /> <br />
        <asp:Button ID="btnViewMyPhotos" runat="server" Text="View my photos" /> <br /> 
        <asp:Button ID="btnViewCommentPhotos" runat="server" Text="View photo's I've commented on" /> <br />
    </div>
    </center>

        <div id="search" >
        <br /> <br />
        <center>Filter Photos: <br /> <br />
            <asp:Label ID="lblSelectUser" runat="server" Text="Select User:"></asp:Label> 
            <asp:DropDownList ID="ddlUsers" runat="server"> </asp:DropDownList> <br />
            <asp:Button ID="btnByPhoto" runat="server" Text="View photo's uploaded by selected user" /> <br />
            <asp:Button ID="btnByComment" runat="server" Text="View photo's commented on by selected user" /> <br /> <br />

            <asp:Label ID="selectedPoint" runat="server" Text="Selected Point: "/> <asp:Textbox ID="tbSelectedPointLatLng" runat="server" onfocus="this.blur()" /> <br />
            <asp:Label ID="lblDistance" runat="server" Text="Distance in feet:"></asp:Label>
            <asp:TextBox ID="tbDistance" runat="server"></asp:TextBox> <br />
            <asp:Button ID="btnByDistance" runat="server" Text="View photos within selected distance" /> <br />
            <asp:Label ID="lblDistanceSelect" runat="server"></asp:Label>

    
            <br /> <br /> <br />
            <asp:Label ID="hiddenfield" runat="server" Visible="false"></asp:Label>
        </center>
    </div>
</div>

<br /> <br />

<div id="googlemap" style="width:740px; height:740px" onload="initialize()">
    <center>Google Map's Div Placeholder</center>
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
</div>

<br /> <br />


<%--<div id="comments" runat="server" style="background-color:#FEFDFA; width:1150px">
    <div id="image" class="divPicComment"><asp:Image ID="imagePhoto" runat="server" CssClass="imagePhoto"/></div>
    <div id="commentList" class="divPicComment" style="overflow:scroll">
        <center>
        <!-- fix problem with overflow not giving scroll bar -->
        <br />
            <asp:Repeater ID="rptComments" runat="server"> 
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="height:25px">
                        <div id="test" class="rptCommentID"><asp:Label ID="lblcommentid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommentID")%>' /></div>
                        <div class="rptCommentID"><asp:Label ID="lbluserid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Profile")%>' /></div>
                        <div class="rptName"> <%# DataBinder.Eval(Container.DataItem, "Username")%></div>
                        <div class="rptComment"><%# DataBinder.Eval(Container.DataItem, "Content")%></div>
                        <div class="rptDelete"><asp:Button ID="rptbtnDelete" runat="server" CssClass="rptDelete" text="Delete" OnClientClick="return confirm('Are you sure you want to delete this comment?');"/></div>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </center>
    </div>
    <div id="imageInfo" class="divCaptionSubmit">
        <div style="text-align:center; width:573px; float:left; height:25px"><asp:Label ID="lblPicCaption" runat="server"/></div> <br />
        <div style="text-align:center; width:286px; float:left; height:25px"><asp:Label ID="lblPicUploader" runat="server" /></div>
        <div style="text-align:center; width:287px; float:left"><asp:Button ID="btnPicDelete" runat="server" Text="Delete"  OnClientClick="return confirm('Are you sure you want to delete this picture?');"/></div>
    </div>
    <div id="commentSubmit" class="divCaptionSubmit">
        <div style="text-align:center; vertical-align:middle; height:100%">
        <br />
            <asp:TextBox ID="tbAddComment" runat="server" Width="300px" Height="25px" MaxLength="40"/>
            <asp:Button ID="btnAddComment" runat="server" text="Submit Comment" /> <br />
            <asp:Label ID="lblAddSuccess" runat="server" />
        </div>
    </div>
</div>--%>



<div class="loader"></div>
</asp:Content>
