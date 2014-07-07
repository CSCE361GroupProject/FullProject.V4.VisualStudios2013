Public Class Picture
#Region "Private Variables"
    Private _sLongitude As String
    Private _sLatitude As String
    Private _sCaption As String
    Private _sPictureID As String
    Private _lCommentList As DataTable
    Private _sUserID As String
    Private _sImagePath As String
#End Region


#Region "Properties"
    Property Longitude As String
        Get
            Return _sLongitude
        End Get
        Set(ByVal value As String)
            _sLongitude = validateInput(value)
        End Set
    End Property

    Property Latitude As String
        Get
            Return _sLatitude
        End Get
        Set(ByVal value As String)
            _sLatitude = validateInput(value)
        End Set
    End Property

    Property Caption As String
        Get
            Return _sCaption
        End Get
        Set(ByVal value As String)
            _sCaption = validateInput(value)
        End Set
    End Property

    Property PictureID As String
        Get
            Return _sPictureID
        End Get
        Set(ByVal value As String)
            _sPictureID = validateInput(value)
        End Set
    End Property

    Property UserID As String
        Get
            Return _sUserID
        End Get
        Set(ByVal value As String)
            _sUserID = validateInput(value)
        End Set
    End Property

    Property CommentList As DataTable
        Get
            Return _lCommentList
        End Get
        Set(ByVal value As DataTable)
            _lCommentList = value
        End Set
    End Property

    Property ImagePath As String
        Get
            Return _sImagePath
        End Get
        Set(ByVal value As String)
            _sImagePath = validateInput(value)
        End Set
    End Property

#End Region

#Region "Constructors"
    Sub New(ByVal sPictureID As String)
        PictureID = sPictureID
    End Sub

    'Full constructor
    Sub New(ByVal sLongitude As String, ByVal sLatitude As String, ByVal sCaption As String, ByVal sUserID As String, ByVal sImagePath As String)
        Longitude = sLongitude
        Latitude = sLatitude
        Caption = sCaption
        UserID = sUserID
        ImagePath = sImagePath
    End Sub

#End Region



#Region "Validation"
    'Ensures that no extra whitespace is added to database - also make sure there are no null values
    Public Function validateInput(ByVal value) As String
        Dim validatedInput As String
        validatedInput = Trim(value & "")
        Return validatedInput
    End Function

    'Makes sure that ImagePath is not empty
    Public Function validateImagePath() As Results
        Dim oResults As New Results

        If Trim(ImagePath & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "ImagePath cannot be blank."
        End If

        Return oResults
    End Function

    'Makes sure that UserID is not empty
    Public Function validateUserID() As Results
        Dim oResults As New Results

        If Trim(UserID & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "UserID be blank."
        End If

        Return oResults
    End Function

    'Makes sure that PictureID is not empty
    Public Function validatePictureID() As Results
        Dim oResults As New Results

        If Trim(PictureID & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "PictureID be blank."
        End If

        Return oResults
    End Function

    'If lat/long is empty - set to middle area of avery hall
    Public Sub validateLatitude()
        Dim oResults As New Results

        If Trim(Latitude & "") = "" Then
            Dim averyMid As Double = 40.819452
            Dim boundiff As Double = 0.0003215
            Dim random As New Random
            Dim sign As Integer = random.Next(0, 1) * 2 - 1

            Latitude = (averyMid + (random.NextDouble() * boundiff * sign)).ToString
            oResults.sMessage = "Latitude set to middle area of Avery."
        End If

    End Sub

    'If lat/long is empty - set to middle area of avery hall 
    Public Sub validateLongitude()
        Dim oResults As New Results

        If Trim(Longitude & "") = "" Then
            Dim averyMid As Double = -96.704503
            Dim boundiff As Double = 0.0003215
            Dim random As New Random
            Dim sign As Integer = random.Next(0, 1) * 2 - 1

            Longitude = (averyMid + (random.NextDouble() * boundiff * sign)).ToString
            oResults.sMessage = "Longitude set to middle area of Avery."
        End If
    End Sub

    'Verifies that all fields required for a database add have values
    Public Function validateAllFields() As Results
        Dim oResults As New Results
        oResults.bSuccess = False 'false until proven true

        If validateUserID().bSuccess Then
            If validateImagePath.bSuccess Then
                oResults.bSuccess = True
            Else
                oResults.sMessage = validateImagePath.sMessage
            End If
        Else
            oResults.sMessage = validateUserID.sMessage
        End If

        validateLatitude()
        validateLongitude()

        Return oResults
    End Function
#End Region


#Region "Add/Delete/Search Pictures"
    Public Function addPicture() As Results

        Dim oPictureData As New PictureData
        Dim oResults As Results = validateAllFields()

        If oResults.bSuccess Then
            oPictureData.AddPicture(ImagePath, Longitude, Latitude, Caption, UserID)
            oResults.sMessage = "Upload successful!"
        End If

        Return oResults
    End Function

    'Doesn't require any valid parameters - just gets every photo from the db
    Public Function getAllPictures() As DataTable
        Dim oPictureData As New PictureData
        Dim oDataTable As DataTable = oPictureData.getAllPictures
        Return oDataTable
    End Function

    'Updates the picture properties from the database when given a photoid - used for loading picture to display
    Public Sub getPicture()
        Dim oPictureData As New PictureData
        Dim oDataTable As DataTable = oPictureData.getPictureByID(PictureID)

        If oDataTable.Rows.Count = 1 Then
            Caption = oDataTable.Rows(0).Item("Caption")
            UserID = oDataTable.Rows(0).Item("UserID")
            ImagePath = oDataTable.Rows(0).Item("ImageFileLoc")

            CommentList = getCommentList()
        End If
    End Sub

    'Gets all pictures submitted by user
    Public Function getPicturesByUserID() As DataTable
        Dim oPictureData As New PictureData
        Dim oDataTable As New DataTable
        Dim oResults As Results = validateUserID()


        If oResults.bSuccess Then
            oDataTable = oPictureData.getPicturesByUserID(UserID)
        End If

        Return oDataTable
    End Function

    'DONE: works
    Public Function getPicturesByCommenter() As DataTable
        Dim oPictureData As New PictureData
        Dim oDataTable As New DataTable
        Dim oResults As Results = validateUserID()


        If oResults.bSuccess Then
            oDataTable = oPictureData.GetPicturesByCommenterJoinTable(UserID)
        End If

        Return oDataTable
    End Function

    'DONE: works
    Public Function getPicturesByUsername(ByVal sUsername As String) As DataTable
        Dim oPictureData As New PictureData
        Dim oDataTable As New DataTable

        If Not sUsername = "" Then
            oDataTable = oPictureData.SearchPictureByUploaderUsername(sUsername)
        End If

        Return oDataTable
    End Function



    'Called method in PictureData deletes all comments on photo, then photo itself
    Public Sub deletePhoto()
        Dim oPictureData As New PictureData
        Dim oResults As Results = validatePictureID()

        If oResults.bSuccess Then
            oPictureData.DeletePicture(PictureID)
        End If
    End Sub

#End Region

#Region "Helper Methods"
    Public Function getCommentList() As DataTable
        Dim oDataTable As DataTable
        Dim oPictureData As New PictureData
        oDataTable = oPictureData.GetCommentUserJoinTable(PictureID)

        Return oDataTable
    End Function

#End Region

End Class
