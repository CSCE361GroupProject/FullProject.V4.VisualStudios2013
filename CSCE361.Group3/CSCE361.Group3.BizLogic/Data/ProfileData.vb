Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ProfileData
    'DONE: works - has input validation
    Public Sub AddProfile(ByVal username As String, ByVal firstName As String, ByVal lastName As String)

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim strSQL As String = "INSERT INTO User (Username, FirstName, LastName) VALUES (@Username, @FirstName, @LastName);"
        Dim myCommand As New MySqlCommand(strSQL, myConnection)
        myCommand.Connection.Open()

        myCommand.Parameters.Add("@Username", MySqlDbType.String).Value = username
        myCommand.Parameters.Add("@FirstName", MySqlDbType.String).Value = firstName
        myCommand.Parameters.Add("@LastName", MySqlDbType.String).Value = lastName

        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()
    End Sub

    'DONE: works - has input validation
    Public Sub AddProfileWithPic(ByVal username As String, ByVal firstName As String, ByVal lastName As String, ByVal profilePictureLoc As String)
        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim strSQL As String = "INSERT INTO User (Username, FirstName, LastName, ProfilePictureFileLoc) VALUES (@Username, @FirstName, @LastName, @ProfilePictureFileLoc);"
        Dim myCommand As New MySqlCommand(strSQL, myConnection)
        myCommand.Connection.Open()

        myCommand.Parameters.Add("@Username", MySqlDbType.String).Value = username
        myCommand.Parameters.Add("@FirstName", MySqlDbType.String).Value = firstName
        myCommand.Parameters.Add("@LastName", MySqlDbType.String).Value = lastName
        myCommand.Parameters.Add("@ProfilePictureFileLoc", MySqlDbType.String).Value = profilePictureLoc

        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()
    End Sub


    'DONE: tested and works
    Public Function SearchProfileByUsername(ByVal sUsername As String) As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter

        Dim query As String = "SELECT * FROM User WHERE `Username` = '" & sUsername & "';"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function


    'DONE: works
    Public Function SearchProfileByID(ByVal sUserID As String) As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter

        Dim query As String = "SELECT * FROM User WHERE `UserID` = '" & sUserID & "';"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function

    'DONE: works
    Public Function getAllUsers() As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter

        Dim query As String = "SELECT * FROM User;"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function

End Class
