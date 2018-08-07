Public Class MessageController

    Shared Sub SendEmail(con As Connection)
        Console.WriteLine("Email sent for con ip" + con.getIP())
    End Sub

End Class
