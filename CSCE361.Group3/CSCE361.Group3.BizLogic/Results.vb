Public Class Results
#Region "Constructor(s)"
    Sub New()
        bSuccess = True
    End Sub
    Sub New(ByVal Success As Boolean, ByVal Message As String)
        bSuccess = Success
        sMessage = Message
    End Sub
#End Region
    Public bSuccess As Boolean
    Public sMessage As String
End Class

Public Class IntegerResults
    'Inherits Results
#Region "Constructor(s)"
    Sub New()
        bSuccess = True
    End Sub
    Sub New(ByVal Success As Boolean, ByVal Message As String)
        bSuccess = Success
        sMessage = Message
    End Sub
    Sub New(ByVal Success As Boolean, ByVal Message As String, ByVal nID As Long)
        bSuccess = Success
        sMessage = Message
        lID = nID
    End Sub
#End Region
    Public bSuccess As Boolean    '//Inherited from Results class
    Public sMessage As String    '//Inherited from Results class
    Public lID As Long
End Class

Public Class DataTableResult
#Region "Constructor(s)"
    Inherits Results
    Sub New()
        bSuccess = True
    End Sub
    Sub New(ByVal Success As Boolean, ByVal Message As String)
        bSuccess = Success
        sMessage = Message
    End Sub
#End Region
    'Public bSuccess As Boolean    '//Inherited from Results class
    'Public sMessage As String    '//Inherited from Results class
    Public dtData As DataTable
End Class

Public Class ListResult
#Region "Constructor(s)"
    Sub New()
        bSuccess = True
        list = New List(Of String)
    End Sub
    Sub New(ByVal Success As Boolean, ByVal Message As String)
        bSuccess = Success
        sMessage = Message
    End Sub
#End Region
    Public list As List(Of String)
    Public bSuccess As Boolean
    Public sMessage As String
End Class

Public Class StringResult
#Region "Constructor(s)"
    Sub New()
        bSuccess = True
        sMessage = ""
        sResult = ""
    End Sub
    Sub New(ByVal Success As Boolean, ByVal Message As String, ByVal Result As String)
        bSuccess = Success
        sMessage = Message
        sResult = Result
    End Sub
#End Region
    Public bSuccess As Boolean
    Public sMessage As String
    Public sResult As String
End Class
