Imports CSCE361.Group3.BizLogic

Public Class Registration
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("username") = "" Then
                Response.Redirect("~\Login.aspx")
            Else
                'load pictures
                Image1.ImageUrl = "http://bloximages.chicago2.vip.townnews.com/journalstar.com/content/tncms/assets/v3/editorial/9/5d/95d6012d-c6c5-56ff-960d-58d066caf5a7/509b3691239ee.preview-620.jpg"
                Image2.ImageUrl = "http://ucomm.unl.edu/resources/downloads/photos/unl_plaza.jpg"

                'Parse query string and fill username textbox
                Dim sUsername As String = Session("username")
                tbUsername.Text = sUsername
            End If

        End If
    End Sub

    Protected Sub btnRegister_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRegister.Click
        'Add user from fields
        Dim oProfile As New BizLogic.Profile(tbUsername.Text, tbFirstName.Text, tbLastName.Text)
        Dim oResults As Results = oProfile.addProfile()

        'Validates user - checks for missing fields - displays error if fails
        If oResults.bSuccess Then
            'Build query string to pass variables between pages
            Dim sQueryString As String
            sQueryString = "?userid=" & getUserID(oProfile.Username)

            'save userid between pages in session variable 
            Session("userid") = getUserID(oProfile.Username)

            'Loads profile
            Response.Redirect("~\HomeProfile.aspx?login=1")
        Else
            'Show error message is add not successful
            lblSuccess.Text = oResults.sMessage
            lblSuccess.Visible = True
            lblSuccess.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Private Function getUserID(ByVal sUsername) As String
        Dim oProfile As New Profile(sUsername)
        Dim oIntResults As IntegerResults = oProfile.searchProfileByUsername()
        Return oIntResults.lID
    End Function
End Class