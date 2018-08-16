Public Class MessageController

    Dim email As String
    Sub New(email As String)
        Me.email = email
    End Sub

    Public Sub SendEmail(con As Connection)
        Console.WriteLine("Email sent for con ip" + con.getIP())
    End Sub

    Public Sub SetEmail(email As String)
        Me.email = email
    End Sub

End Class
