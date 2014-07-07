Public Class Comment

#Region "Private Variables"
    Private _sComment As String
    Private _sPictureID As String
    Private _sCommentID As String
    Private _sUserID As String
#End Region

#Region "Properties"
    Property CommentContent As String
        Get
            Return _sComment
        End Get
        Set(ByVal value As String)
            _sComment = validateInput(value)
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

    Property CommentID As String
        Get
            Return _sCommentID
        End Get
        Set(ByVal value As String)
            _sCommentID = validateInput(value)
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

#End Region

#Region "Constructors"
    Sub New(ByVal sCommentID As String)
        CommentID = sCommentID
    End Sub

    'Full constructor
    Sub New(ByVal sComment As String, ByVal sPictureID As String, ByVal sUserID As String)
        CommentContent = sComment
        PictureID = sPictureID
        UserID = sUserID
    End Sub
#End Region

#Region "Validation"
    'Ensures that no extra whitespace is added to database - also make sure there are no null values
    Public Function validateInput(ByVal value) As String
        Dim validatedInput As String
        validatedInput = Trim(value & "")
        Return validatedInput
    End Function

    'Makes sure that comment content is not empty
    Public Function validateCommentContent() As Results
        Dim oResults As New Results

        If Trim(CommentContent & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "Comment content cannot be blank."
        End If

        Return oResults
    End Function

    'Makes sure that userid is not empty
    Public Function validateUserID() As Results
        Dim oResults As New Results

        If Trim(UserID & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "USerID cannot be blank."
        End If

        Return oResults
    End Function

    'Makes sure that CommentID is not empty
    Public Function validateCommentID() As Results
        Dim oResults As New Results

        If Trim(CommentID & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "CommentID cannot be blank."
        End If

        Return oResults
    End Function

    'Makes sure that PictureID is not empty
    Public Function validatePictureID() As Results
        Dim oResults As New Results

        If Trim(PictureID & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "PictureID cannot be blank."
        End If

        Return oResults
    End Function


    'Verifies that all fields required for a datbase add have values
    Public Function validateAllFields() As Results
        Dim oResults As New Results
        oResults.bSuccess = False 'false until proven true

        If validateUserID().bSuccess Then
            If validatePictureID.bSuccess Then
                If validateCommentContent.bSuccess Then
                    oResults.bSuccess = True
                Else
                    oResults.sMessage = validateCommentContent().sMessage
                End If
            Else
                oResults.sMessage = validatePictureID.sMessage
            End If
        Else
            oResults.sMessage = validateUserID.sMessage
        End If

        Return oResults
    End Function

#End Region

#Region "Add/Delete/Search Comments"

    Public Function addComment() As Results
        Dim oResults As Results
        Dim oCommentData As New CommentData

        oResults = validateAllFields()

        If oResults.bSuccess Then
            oCommentData.AddComment(UserID, PictureID, CommentContent)
        End If

        Return oResults
    End Function

    Public Function deleteComment() As Results
        Dim oResults As New Results
        Dim oCommentData As New CommentData
        oResults = validateCommentID()

        If oResults.bSuccess Then
            oCommentData.DeleteComment(CommentID)
        End If

        Return oResults
    End Function

#End Region


End Class
