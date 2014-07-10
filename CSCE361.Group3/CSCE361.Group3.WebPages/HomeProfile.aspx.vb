Imports CSCE361.Group3.BizLogic

Public Class HomeProfile
    Inherits System.Web.UI.Page

    Private _sUserID As String
    Private _sUsername As String
    Private _sName
    Private _sPhotoID As String
    Private _dtPictures As DataTable
    Private _sSelectedUser As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'NOTE: click event from map marker is not a postback

        'If dtPicture is uninitialized (first visit) - default to full list of pictures
        If Session("dtPictures") Is Nothing Then
            Session("dtPictures") = getAllPictures()
        End If

        'resaves userid and picture list into variable every page load to keep it in scope
        _sUserID = Session("userid").ToString
        _dtPictures = Session("dtPictures")
        _sSelectedUser = ddlUsers.SelectedValue

        If Not IsPostBack Then
            'Fill user-specific fields
            fillFields()

            'If userid is not present in the querystring then go back to login (ensures not trying to access page without being a user)
            If _sUserID = "" Then
                Response.Redirect("~/Login.aspx")
            End If

            'Loads markers on to map
            literal1.Text = API_Google.populateGoogleMap(_dtPictures)

            'Prepare user list
            bindUserList()
        End If
        ddlUsers.SelectedValue = _sSelectedUser
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

    Private Sub bindUserList()
        Dim oProfile As New Profile("")
        Dim oDataTable As DataTable = oProfile.getAllUsers

        ddlUsers.DataSource = oDataTable
        ddlUsers.DataTextField = "Username"
        ddlUsers.DataBind()
    End Sub
#End Region


#Region "My Photos and Uploading"
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Dim sLongitude As String = ""
        Dim sLatitude As String = ""
        Dim geoData() As String = {"", ""}
        Dim filepath As String = ""

        Dim oResults As New Results
        oResults.sMessage = ""

        If fuPhoto.HasFile Then
            'Validate the correct filetype is chosen for upload
            Dim sFilename As String = fuPhoto.FileName
            sFilename = sFilename.Substring(sFilename.IndexOf("."))

            If sFilename.ToLower = ".jpg" Or sFilename.ToLower = ".png" Then
                'Uploads the file to the web server for processing by exif
                Dim folderpath As String = Server.MapPath("~/Uploads/")
                filepath = folderpath & fuPhoto.FileName
                fuPhoto.SaveAs(filepath)

                'Get fileupload item and convert to 64string before passing into API_Imgur upload helper class
                Dim imageLength As Integer = fuPhoto.PostedFile.ContentLength
                Dim imageBtye(imageLength) As Byte
                fuPhoto.PostedFile.InputStream.Read(imageBtye, 0, imageLength)

                oResults = API_Imgur.uploadImage(Convert.ToBase64String(imageBtye))

                'Extracts the exif data from server file
                geoData = API_ExifLib.getGeoData(filepath)
                sLongitude = geoData(1)
                sLatitude = geoData(0)


                Dim oPicture As New Picture(sLongitude, sLatitude, tbCaption.Text, _sUserID, oResults.sMessage)
                Dim oResults2 As Results = oPicture.addPicture()
                If oResults2.bSuccess Then
                    lblSuccess.ForeColor = Drawing.Color.Green
                Else
                    lblSuccess.ForeColor = Drawing.Color.Red
                End If
                lblSuccess.Text = oResults2.sMessage
            Else
                lblSuccess.Text = "Chosen file is the wrong file type."
                lblSuccess.ForeColor = Drawing.Color.Red
            End If
        Else
            lblSuccess.Text = "Please select a picture to upload."
            lblSuccess.ForeColor = Drawing.Color.Red
        End If

        tbCaption.Text = ""

        'Rebind full list of pictures on google maps
        literal1.Text = API_Google.populateGoogleMap(getAllPictures)
    End Sub


    'Returns all pictures in the database as a datatable for google maps use
    Public Function getAllPictures() As DataTable
        Dim oDataTable As New DataTable
        Dim oPicture As New Picture("")
        oDataTable = oPicture.getAllPictures()
        Return oDataTable
    End Function

    'DONE: works
    Protected Sub btnViewAllPhotos_Click(sender As Object, e As EventArgs) Handles btnViewAllPhotos.Click
        Session("dtPictures") = getAllPictures()
        literal1.Text = API_Google.populateGoogleMap(getAllPictures)

        lblSuccess.Visible = False
    End Sub

    'DONE: works
    Protected Sub btnViewMyPhotos_Click(sender As Object, e As EventArgs) Handles btnViewMyPhotos.Click
        Dim oPicture As New Picture("")
        oPicture.UserID = _sUserID
        Dim oDataTable As New DataTable
        oDataTable = oPicture.getPicturesByUserID()

        If oDataTable.Rows.Count > 0 Then
            Session("dtPictures") = oDataTable
        End If

        'Response.Redirect("~/HomeProfile.aspx?login=1")
        literal1.Text = API_Google.populateGoogleMap(oDataTable)

        lblSuccess.Visible = False
    End Sub

    'DONE: works
    Protected Sub btnViewCommentPhotos_Click(sender As Object, e As EventArgs) Handles btnViewCommentPhotos.Click
        Dim oPicture As New Picture("")
        oPicture.UserID = _sUserID
        Dim oDataTable As DataTable = oPicture.getPicturesByCommenter()

        If oDataTable.Rows.Count > 0 Then
            Session("dtPicture") = oDataTable
        End If

        literal1.Text = API_Google.populateGoogleMap(oDataTable)

        lblSuccess.Visible = False
    End Sub
#End Region


#Region "By Selected User"
    'DONE: works
    Protected Sub btnByPhoto_Click(sender As Object, e As EventArgs) Handles btnByPhoto.Click
        Dim oPicture As New Picture("")

        Dim oDataTable As New DataTable
        Dim sUsername = ddlUsers.SelectedValue
        oDataTable = oPicture.getPicturesByUsername(sUsername)


        Session("dtPictures") = oDataTable
        Session("selectedUser") = sUsername

        literal1.Text = API_Google.populateGoogleMap(oDataTable)
        lblSuccess.Visible = False
    End Sub

    'DONE: works
    Protected Sub btnByComment_Click(sender As Object, e As EventArgs) Handles btnByComment.Click
        Dim oPicture As New Picture("")
        Dim sUsername = ddlUsers.SelectedValue

        Dim oProfile As New Profile(sUsername)
        Dim oIntResults As IntegerResults = oProfile.searchProfileByUsername()

        oPicture.UserID = oIntResults.lID
        Dim oDataTable As DataTable = oPicture.getPicturesByCommenter()

        Session("dtPictures") = oDataTable
        Session("selectedUser") = sUsername

        literal1.Text = API_Google.populateGoogleMap(oDataTable)
        lblSuccess.Visible = False
    End Sub
#End Region


#Region "ByDistance"
    'Event handler for clicking view photos within selected distance
    Protected Sub btnByDistance_Click(sender As Object, e As EventArgs) Handles btnByDistance.Click
        If tbSelectedPointLatLng.Text = "" Then
            lblDistanceSelect.Text = "Please select a point on the map."
            lblDistanceSelect.ForeColor = Drawing.Color.Red
        ElseIf tbDistance.Text = "" Then
            lblDistanceSelect.Text = "Please enter a radius distance."
            lblDistanceSelect.ForeColor = Drawing.Color.Red
        ElseIf Not checkEnteredDistance() Then
            lblDistanceSelect.Text = "Only enter whole numbers please."
            lblDistanceSelect.ForeColor = Drawing.Color.Red
        Else
            Dim radius As Integer = Convert.ToInt32(tbDistance.Text)
            Dim sLatLng() As String = parseSelectedLatLng()
            Dim newPictures As DataTable = filterPicturesByDist(sLatLng(0), sLatLng(1), radius, _dtPictures)

            Session("dtPictures") = newPictures

            literal1.Text = API_Google.populateGoogleMap(newPictures)
            lblDistanceSelect.Visible = False
        End If
    End Sub

    'DONE: works
    Private Function checkEnteredDistance() As Boolean
        Dim regex As New Regex("^\d+$")
        Return regex.IsMatch(tbDistance.Text)
    End Function

    'DONE: works
    Private Function parseSelectedLatLng() As String()
        Dim sLatLng() As String = {"", ""}

        Dim sSelected As String = tbSelectedPointLatLng.Text
        Dim sSplit As String() = sSelected.Split(",")
        Dim sLat As String = sSplit(0).Substring(1)
        Dim sLong As String = sSplit(1).Substring(1, sSplit(1).Length - 2)

        sLatLng(0) = sLat
        sLatLng(1) = sLong

        Return sLatLng
    End Function

    'Filters current datatable of pictures by distance
    Private Function filterPicturesByDist(ByVal latitude As String, ByVal longitude As String, ByVal ndistFeet As Integer, ByVal oPictures As DataTable) As DataTable
        Dim oNewPictures As New DataTable
        oNewPictures = oPictures.Clone()

        For i As Integer = 0 To oPictures.Rows.Count - 1
            'check if the picture at this index is within the allowed distance
            Dim distanceBetween As Double = gpsDistanceInFeet(latitude, longitude, oPictures.Rows(i).Item("Latitude"), oPictures.Rows(i).Item("Longitude"))
            If distanceBetween < ndistFeet Then
                oNewPictures.ImportRow(oPictures.Rows(i))
            End If
        Next
        Return oNewPictures
    End Function

    'Gets the distance between to geo points in feet
    Private Function gpsDistanceInFeet(ByVal latA As String, ByVal longA As String, ByVal latB As String, ByVal longB As String) As Integer
        'Source: http://www.codeproject.com/Articles/12269/Distance-between-locations-using-latitude-and-long
        Dim dDistance As Double
        Dim dLatAinRad As Double = latA * (Math.PI / 180.0)
        Dim dLongAinRad As Double = longA * (Math.PI / 180.0)

        Dim dLatBinRad As Double = latB * (Math.PI / 180.0)
        Dim dLongBinRad As Double = longB * (Math.PI / 180.0)

        Dim dLatitude As Double = dLatBinRad - dLatAinRad
        Dim dLongitude As Double = dLongBinRad - dLongAinRad

        Dim a As Double = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) + Math.Cos(dLatAinRad) * Math.Cos(dLatBinRad) * Math.Pow(Math.Sin(dLongitude / 2.0), 2.0)
        Dim c As Double = 2.0 * Math.Asin(Math.Sqrt(a))
        Dim mEarthRadius As Double = 3962.165805
        Dim ftEarthRadius As Double = mEarthRadius * 5280

        dDistance = ftEarthRadius * c
        Return dDistance
    End Function

    'Helper function for distance between points
    Private Function deg2rad(ByVal deg As Double) As Double
        Return (deg * Math.PI / 180.0)
    End Function
#End Region

End Class