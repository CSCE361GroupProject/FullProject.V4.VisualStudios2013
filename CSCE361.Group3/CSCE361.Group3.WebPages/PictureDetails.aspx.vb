Imports CSCE361.Group3.BizLogic

Public Class PictureDetails
    Inherits System.Web.UI.Page

    Private _sUserID As String
    Private _sUsername As String
    Private _sName
    Private _sPhotoID As String
    Private _dtPictures As DataTable
    Private _sSelectedUser As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'resaves userid and picture list into variable every page load to keep it in scope
        _sUserID = Session("userid").ToString
        _dtPictures = Session("dtPictures")

        If Not IsPostBack Then
            'If userid is not present in the querystring then go back to login (ensures not trying to access page without being a user)
            If _sUserID = "" Then
                Response.Redirect("~/Login.aspx")
            End If

            'Parse querystring to get user data and save to private
            parseQueryString()

            'Fill user-specific fields
            fillFields()

            'Load picture
            loadPicture(_sPhotoID)

        Else
            'resets button handlers on postback (otherwise delete buttons dont work when page reloads)
            For Each repItem As RepeaterItem In rptComments.Items
                Dim rptDelete As Button = repItem.FindControl("rptbtnDelete")
                AddHandler rptDelete.Click, AddressOf repeaterDelete
            Next
        End If

    End Sub



#Region "Loading and filling fields"
    'Fills visible fields and private variables for other method use
    Private Sub fillFields()
        Dim oProfile As New Profile(_sUserID, _sUsername)
        Dim oDataTable As DataTable = oProfile.searchProfileByID()

        If oDataTable.Rows.Count = 1 Then
            _sName = oDataTable.Rows(0).Item("FirstName") & " " & oDataTable.Rows(0).Item("LastName")
            _sUsername = oDataTable.Rows(0).Item("Username")
            Dim lblusername As Label = Master.FindControl("username")
            lblusername.Text = "Welcome, " & _sUsername & "!"
        Else
            _sUserID = ""
        End If
    End Sub

    Private Sub parseQueryString()

        Dim sPhotoID As String = Request.QueryString("photoid")
        _sPhotoID = sPhotoID

    End Sub

    'Loads picture, picture info, and comments - is launched when an id is found in the query string
    Public Sub loadPicture(ByVal sPhotoID As String)
        Dim oPicture As New Picture(sPhotoID)
        oPicture.getPicture()
        imagePhoto.ImageUrl = oPicture.ImagePath

        'bind comment list to grid
        rptComments.DataSource = oPicture.CommentList
        rptComments.DataBind()

        'disable delete button on comments when necessary
        disableButtons(oPicture.UserID)

        'set caption and uploader info
        lblPicCaption.Text = """" & oPicture.Caption & """"
        Dim oProfile As New Profile(oPicture.UserID, "")
        oProfile.getUserByID()

        lblPicUploader.Text = "Uploaded by: " & oProfile.FirstName & " " & oProfile.LastName

        'enable/disable delete button for picture
        Dim sUserID As String = _sUserID
        If oPicture.UserID = sUserID Then
            btnPicDelete.Enabled = True
        Else
            btnPicDelete.Enabled = False
            btnPicDelete.BackColor = Drawing.Color.Transparent
            btnPicDelete.ForeColor = Drawing.Color.Transparent
            btnPicDelete.BorderColor = Drawing.Color.Transparent
            btnPicDelete.Text = ""
        End If

    End Sub
#End Region


#Region "Photo and Comments Section"
    'DONE: works
    Protected Sub btnAddComment_Click(sender As Object, e As EventArgs) Handles btnAddComment.Click
        Dim sPhotoID As String = Request.QueryString("photoid")

        If tbAddComment.Text = "" Then
            lblAddSuccess.Text = "Please enter a comment."
            lblAddSuccess.ForeColor = Drawing.Color.Red
            lblAddSuccess.Visible = True
        Else
            Dim oComment As New Comment(tbAddComment.Text, sPhotoID, _sUserID)
            oComment.addComment()

            'reload repeater
            Dim oPicture As New Picture(sPhotoID)
            oPicture.getPicture()

            'bind comment list to grid
            rptComments.DataSource = oPicture.CommentList
            rptComments.DataBind()

            'Re-disables buttons to account for new comment
            disableButtons(oPicture.UserID)

            lblAddSuccess.Visible = False
        End If

        'clears text 
        tbAddComment.Text = ""
    End Sub

    'Deletes selected picture and subsequent comments - if button is enabled
    Protected Sub btnPicDelete_Click(sender As Object, e As EventArgs) Handles btnPicDelete.Click
        Dim oPicture As New Picture(Request.QueryString("photoid"))
        oPicture.deletePhoto()

        'Rebind full list of pictures on google maps
        Session("dtPictures") = getAllPictures()

        'Needed for clearing off selected photo
        Response.Redirect("~\HomeProfile.aspx?login=1")
    End Sub

    'Handler for delete button in comment repeater view
    'Deletes comment then reloads/rebinds data
    Protected Sub repeaterDelete(sender As Object, e As EventArgs)
        Dim button As Button = sender
        Dim repeaterItem As RepeaterItem = button.NamingContainer
        Dim index As Integer = repeaterItem.ItemIndex

        Dim labelID As Label = repeaterItem.FindControl("lblcommentid")
        Dim commentID As String = labelID.Text

        Dim comment As New Comment(commentID)
        comment.deleteComment()

        Dim sPhotoID As String = Request.QueryString("photoid")
        Dim oPicture As New Picture(sPhotoID)
        oPicture.getPicture()
        rptComments.DataSource = oPicture.CommentList
        rptComments.DataBind()

        loadPicture(Request.QueryString("photoid"))

        Page_Load(sender, e)
    End Sub

    'Disables/enables buttons in comments repeater based on picture and comment ownership rights
    Protected Sub disableButtons(ByVal sUserID As String)
        For Each repItem As RepeaterItem In rptComments.Items
            Dim rptDelete As Button = repItem.FindControl("rptbtnDelete")
            Dim lbluserid As Label = repItem.FindControl("lbluserid")

            If sUserID = Session("userid") Then
                rptDelete.Enabled = True
            ElseIf lbluserid.Text = Session("userid") Then
                rptDelete.Enabled = True
            Else
                rptDelete.Enabled = False
                rptDelete.Visible = False
            End If
        Next
    End Sub
#End Region



    Public Function getAllPictures() As DataTable
        Dim oDataTable As New DataTable
        Dim oPicture As New Picture("")
        oDataTable = oPicture.getAllPictures()
        Return oDataTable
    End Function
End Class