Imports Microsoft.AspNet.Membership.OpenAuth

Public Module AuthConfig
    Sub RegisterOpenAuth()
        ' See http://go.microsoft.com/fwlink/?LinkId=252803 for details on setting up this ASP.NET
        ' application to support logging in via external services.

        OpenAuth.AuthenticationClients.AddTwitter(
            consumerKey:="MfZQCvXR4W3ylLZh9nEGfw4XC",
            consumerSecret:="umyO7Jw9poQsOC7kD38Uq9ZBR7Mu5FEnQReengUmhQo6IvT4fn")

        'OpenAuth.AuthenticationClients.AddFacebook(
        '    appId:= "your Facebook app id",
        '    appSecret:= "your Facebook app secret")

        'OpenAuth.AuthenticationClients.AddMicrosoft(
        '    clientId:= "your Microsoft account client id",
        '    clientSecret:= "your Microsoft account client secret")

        'OpenAuth.AuthenticationClients.AddGoogle()
    End Sub
End Module
