Imports MySql.Data.MySqlClient

Public Class Profile

#Region "Private Variables"
    Private _sUsername As String
    Private _sFirstName As String
    Private _sLastName As String
    Private _sUserID As String
    Private _sProfilePicturePath As String
    Private _lCommentList As List(Of Comment)
    Private _lPictureList As List(Of Picture)
#End Region

#Region "Properties"
    Property Username As String
        Get
            Return _sUsername
        End Get
        Set(ByVal value As String)
            _sUsername = validateInput(value)
        End Set
    End Property

    Property FirstName As String
        Get
            Return _sFirstName
        End Get
        Set(ByVal value As String)
            _sFirstName = validateInput(value)
        End Set
    End Property

    Property LastName As String
        Get
            Return _sLastName
        End Get
        Set(ByVal value As String)
            _sLastName = validateInput(value)
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

    Property ProfilePicturePath As String
        Get
            Return _sProfilePicturePath
        End Get
        Set(ByVal value As String)
            _sProfilePicturePath = validateInput(value)
        End Set
    End Property

    Property CommentList As List(Of Comment)
        Get
            Return _lCommentList
        End Get
        Set(ByVal value As List(Of Comment))
            _lCommentList = value
        End Set
    End Property

    Property PictureList As List(Of Picture)
        Get
            Return _lPictureList
        End Get
        Set(ByVal value As List(Of Picture))
            _lPictureList = value
        End Set
    End Property
#End Region

#Region "Constructors"
    'Constructor for checking if user exists
    Sub New(ByVal sUsername As String)
        Username = sUsername
    End Sub

    'Full Constructor
    Sub New(ByVal sUsername As String, ByVal sFirstName As String, ByVal sLastName As String)
        Username = sUsername
        FirstName = sFirstName
        LastName = sLastName
    End Sub

    Sub New(ByVal sUserID As String, ByVal sUsername As String)
        UserID = sUserID
        Username = sUsername
    End Sub

#End Region


#Region "Validation"
    'Ensures that no extra whitespace is added to database - also make sure there are no null values
    Public Function validateInput(ByVal value) As String
        Dim validatedInput As String
        validatedInput = Trim(value & "")
        Return validatedInput
    End Function

    'Makes sure that username is not empty
    Public Function validateUsername() As Results
        Dim oResults As New Results

        If Trim(Username & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "Username cannot be blank."
        End If

        Return oResults
    End Function

    'Makes sure that firstname is not empty
    Public Function validateFirstname() As Results
        Dim oResults As New Results

        If Trim(FirstName & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "Firstname cannot be blank."
        End If

        Return oResults
    End Function

    'Makes sure that lastname is not empty
    Public Function validateLastname() As Results
        Dim oResults As New Results

        If Trim(LastName & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "Lastname cannot be blank."
        End If

        Return oResults
    End Function

    'Makes sure that profile pic path is not empty
    Public Function validateProfilePic() As Results
        Dim oResults As New Results

        If Trim(ProfilePicturePath & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "Profile picture path cannot be blank."
        End If

        Return oResults
    End Function

    'Makes sure that userID is not empty
    Public Function validateUserID() As Results
        Dim oResults As New Results

        If Trim(UserID & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "UserID cannot be blank."
        End If

        Return oResults
    End Function

    'Verifies that all fields required for a datbase add have values
    Public Function validateAllFields() As Results
        Dim oResults As New Results
        oResults.bSuccess = False 'false until proven true

        If validateUsername().bSuccess Then
            If validateFirstname.bSuccess Then
                If validateLastname.bSuccess Then
                    oResults.bSuccess = True
                Else
                    oResults.sMessage = validateLastname().sMessage
                End If
            Else
                oResults.sMessage = validateFirstname.sMessage
            End If
        Else
            oResults.sMessage = validateUsername.sMessage
        End If

        Return oResults
    End Function

#End Region

#Region "Add/Search Profiles"
    'DONE: works
    Public Function addProfileWithPic() As Results
        Dim oResults As Results = validateAllFields()
        Dim oProfileData As New ProfileData

        If oResults.bSuccess Then
            oProfileData.AddProfileWithPic(Username, FirstName, LastName, ProfilePicturePath)
        End If

        Return oResults

    End Function

    'DONE: works
    Public Function addProfile() As Results
        Dim oResults As Results = validateAllFields()
        Dim oProfileData As New ProfileData

        If oResults.bSuccess Then
            oProfileData.AddProfile(Username, FirstName, LastName)
        End If

        Return oResults
    End Function

    'DONE: works
    Public Function searchProfileByUsername() As IntegerResults
        Dim oIntResults As New IntegerResults
        Dim oDatatable As New DataTable
        Dim oProfileData As New ProfileData
        oIntResults.bSuccess = False 'resets success to false so no false positives

        'Makes sure that username is not empty
        Dim oResults As Results = validateUsername()
        If oResults.bSuccess Then
            oDatatable = oProfileData.SearchProfileByUsername(Username)
        End If

        'If returned datatable has a single row, set success to true and return userID
        If oDatatable.Rows.Count = 1 Then
            oIntResults.bSuccess = True
            oIntResults.lID = oDatatable.Rows(0).Item("UserID")
        End If

        Return oIntResults
    End Function

    'Done: works
    Public Function searchProfileByID() As DataTable
        Dim oResults As Results = validateUserID()
        Dim oDataTable As New DataTable
        Dim oProfileData As New ProfileData

        If oResults.bSuccess Then
            oDataTable = oProfileData.SearchProfileByID(UserID)
        End If

        Return oDataTable
    End Function

    'DONE: works
    'Gets basic user info
    Public Sub getUserByID()
        Dim oProfileData As New ProfileData
        Dim oDataTable As DataTable = oProfileData.SearchProfileByID(UserID)

        If oDataTable.Rows.Count = 1 Then
            Username = oDataTable.Rows(0).Item("Username")
            FirstName = oDataTable.Rows(0).Item("FirstName")
            LastName = oDataTable.Rows(0).Item("LastName")
            ProfilePicturePath = oDataTable.Rows(0).Item("ProfilePictureFileLoc")
        End If
    End Sub

    'DONE: works
    Public Function getAllUsers() As DataTable
        Dim oProfileData As New ProfileData
        Dim oDataTable As DataTable = oProfileData.getAllUsers
        Return oDataTable
    End Function

#End Region

End Class
