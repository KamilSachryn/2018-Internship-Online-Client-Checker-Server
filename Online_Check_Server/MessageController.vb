Public Class MessageController

    Dim email As String
    'standard constructor
    Sub New(email As String)
        Me.email = email
    End Sub

    'Sends email based on con
    'if this function is missing, Kazi didnt give me the code.
    'Email him at kzislm@gmail.com because thats his job.
    Public Sub SendEmail(con As Connection)
        Console.WriteLine("Email sent for con ip" + con.getIP())
    End Sub


    'set email on changing text box
    Public Sub SetEmail(email As String)
        Me.email = email
    End Sub

End Class
