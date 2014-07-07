Imports CSCE361.Group3.BizLogic

Public Class Login1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'load pictures
        Image1.ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/b/b1/Nebraska_State_Capitol_Highsmith.jpeg"
        Image2.ImageUrl = "http://i.imgur.com/83iHK.jpg"

        'check if user is logged in
        Dim user As MembershipUser = Membership.GetUser(HttpContext.Current.User.Identity.Name)

        If Not user Is Nothing Then
            Dim sUsername As String = user.ToString

            If Not sUsername = "" Then
                Dim oProfile As New BizLogic.Profile(sUsername)
                Dim oIntResults As IntegerResults = oProfile.searchProfileByUsername

                'Set session variable to pass between pages
                Session("username") = oProfile.Username
                Session("userid") = oIntResults.lID

                'If user/profile does not exist - go to registration
                If oIntResults.bSuccess Then
                    Session("username") = oProfile.Username
                    Response.Redirect("~/HomeProfile.aspx?login=1")
                Else
                    Response.Redirect("~/Registration.aspx")
                End If
            End If
        End If

        'Dim sUsername As String = login


    End Sub

    'TODO: Add twitter authentication logic in Sprint 4
    'Protected Sub btnTwitter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTwitter.Click
    '    'Search if username exists in database
    '    If Not tbUserName.Text = "" Then
    '        Dim oProfile As New BizLogic.Profile(tbUserName.Text)
    '        Dim oIntResults As IntegerResults = oProfile.searchProfileByUsername

    '        'Set session variable to pass between pages
    '        Session("username") = oProfile.Username
    '        Session("userid") = oIntResults.lID

    '        'If user/profile does not exist - go to registration
    '        If oIntResults.bSuccess Then
    '            Session("username") = oProfile.Username
    '            Response.Redirect("~/HomeProfile.aspx?login=1")
    '        Else
    '            Response.Redirect("~/Registration.aspx")
    '        End If
    '    Else
    '        lblSuccess.Visible = True
    '        lblSuccess.Text = "Please enter a username."
    '        lblSuccess.ForeColor = Drawing.Color.Red
    '    End If
    'End Sub

End Class