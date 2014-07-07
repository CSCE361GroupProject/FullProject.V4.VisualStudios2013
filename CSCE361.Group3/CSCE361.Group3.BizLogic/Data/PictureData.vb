Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class PictureData

    'DONE: works
    Public Sub AddPicture(ByVal imageFileLoc As String, ByVal longitude As String, ByVal latitude As String, ByVal caption As String, ByVal userID As String)

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim strSQL As String = "INSERT INTO `Photo` (ImageFileLoc, Longitude, Latitude, Caption, UserID) VALUES (@ImageFileLoc, @Longitude, @Latitude, @Caption, @UserID)"
        Dim myCommand As New MySqlCommand(strSQL, myConnection)
        myCommand.Connection.Open()

        myCommand.Parameters.Add("@ImageFileLoc", MySqlDbType.String).Value = imageFileLoc
        myCommand.Parameters.Add("@Longitude", MySqlDbType.Double).Value = Convert.ToDouble(longitude)
        myCommand.Parameters.Add("@Latitude", MySqlDbType.Double).Value = Convert.ToDouble(latitude)
        myCommand.Parameters.Add("@UserID", MySqlDbType.Int32).Value = Convert.ToInt32(userID)
        myCommand.Parameters.Add("@Caption", MySqlDbType.String).Value = caption

        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()
    End Sub

    'DONE: works
    Public Sub DeletePicture(ByVal photoID As String)
        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim deleteCommentsQuery As String = "DELETE FROM Comment WHERE PhotoID = " & photoID & ";"
        Dim myCommand As New MySqlCommand(deleteCommentsQuery)
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()

        Dim strSQL As String = "DELETE FROM Photo WHERE PhotoID = " & photoID & ";"
        Dim myCommand2 As New MySqlCommand(strSQL)
        myCommand2.Connection = myConnection
        myConnection.Open()
        myCommand2.ExecuteNonQuery()
        myCommand2.Connection.Close()
    End Sub


    'Only search pictures uploaded by a specific user
    'DONE: works
    Public Function SearchPictureByUploaderUsername(ByVal sUsername As String) As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter

        Dim userID As String = GetUserID(sUsername)

        Dim query As String = "SELECT * FROM Photo WHERE `UserID` = '" & userID & "';"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function


    Public Function getAllPictures() As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter


        Dim query As String = "SELECT * FROM Photo;"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function

    'DONE: works
    Public Function getPictureByID(ByVal sPhotoID As String) As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter

        Dim query As String = "SELECT * FROM Photo WHERE photoID =" & Convert.ToInt32(sPhotoID) & ";"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function


    'used to generate a datatable with all the necessary comment data for displaying with a photo
    'DONE: works
    Public Function GetCommentUserJoinTable(ByVal pictureID As String) As DataTable
        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"
        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim query As String = "SELECT u.UserID 'Profile', u.FirstName 'First', u.LastName 'Last', c.content 'Content', c.CommentDate 'Time', c.CommentID FROM User u, Comment c WHERE(c.PhotoID = @PhotoID And c.UserID = u.UserID)"

        Dim mysqlCmd As New MySqlCommand(query, myConnection)
        mysqlCmd.Connection.Open()
        mysqlCmd.Parameters.Add("@PhotoID", MySqlDbType.String).Value = pictureID

        Dim drJoin As MySqlDataReader = mysqlCmd.ExecuteReader
        Dim dtJoin As New DataTable
        dtJoin.Load(drJoin)


        mysqlCmd.Dispose()
        myConnection.Close()

        Return dtJoin
    End Function


    'DONE: works
    Public Function GetPicturesByCommenterJoinTable(ByVal userID As String) As DataTable
        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"
        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim query As String = "SELECT DISTINCT p.PhotoID, p.Latitude, p.Longitude FROM Photo p, Comment c WHERE(c.UserID = @UserID AND p.PhotoID = c.PhotoID)"

        Dim mysqlCmd As New MySqlCommand(query, myConnection)
        mysqlCmd.Connection.Open()
        mysqlCmd.Parameters.Add("@UserID", MySqlDbType.String).Value = userID

        Dim drJoin As MySqlDataReader = mysqlCmd.ExecuteReader
        Dim dtJoin As New DataTable
        dtJoin.Load(drJoin)


        mysqlCmd.Dispose()
        myConnection.Close()

        Return dtJoin
    End Function

    'DONE: works
    Public Function getPicturesByUserID(ByVal userID As String) As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter

        Dim query As String = "SELECT * FROM Photo WHERE UserID =" & Convert.ToInt32(userID) & ";"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function


    'Helper Methods
    'DONE: works
    Private Function GetUserID(ByVal username As String)

        Dim userID As String
        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim strSQL As String = "SELECT UserID FROM User WHERE Username = '" & username & "';"

        Dim myCommand As New MySqlCommand(strSQL, myConnection)
        myConnection.Open()
        Dim myReader As MySqlDataReader
        myReader = myCommand.ExecuteReader()
        Try
            myReader.Read()
            userID = myReader.GetString(0)
        Finally
            myReader.Close()
            myConnection.Close()
        End Try

        Return userID

    End Function

End Class
