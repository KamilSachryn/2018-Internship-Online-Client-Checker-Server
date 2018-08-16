Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports Online_Check_Server

Public Class Connection
    Public ip As IPAddress
    Public name As String
    Public status As ConnectionStatus
    Public sendEmailOnCrash As Boolean
    Public timeOfLastCheck As DateTime
    Public tempSendEmailOnCrash As Boolean

    'Enum for use in the GUI
    'Keeps track of what the connection state is.
    Public Enum ConnectionStatus
        Online = 0
        Offline = 1
    End Enum


    Sub New(ip As IPAddress, name As String, status As ConnectionStatus, sendEmailOnCrash As String, timeSinceLastCheck As DateTime)
        Me.ip = ip
        Me.name = name
        Me.status = status
        Me.sendEmailOnCrash = sendEmailOnCrash
        Me.timeOfLastCheck = timeSinceLastCheck
        Me.tempSendEmailOnCrash = sendEmailOnCrash

        Console.WriteLine("NEW CON CREATED WITH IP = " + Me.ip.ToString())

    End Sub

    'REMOVED to keep the program uniform, was getting confusing when every other function called different variables

    'Sub New(ip As IPAddress, name As String, status As String, sendEmailOnCrash As String, timeSinceLastCheck As DateTime)
    '    Me.ip = ip
    '    Me.name = name
    '    Me.status = convertStringToConnectionStatus(status)
    '    Me.sendEmailOnCrash = sendEmailOnCrash
    '    Me.timeOfLastCheck = timeSinceLastCheck
    '    Me.tempSendEmailOnCrash = sendEmailOnCrash

    '    Console.WriteLine("NEW CON CREATED WITH IP = " + Me.ip.ToString())

    'End Sub

    Public Function getIP() As String
        Dim strIP As String = ip.ToString()
        'strIP = strIP.Substring(0, strIP.LastIndexOf(":"))
        Return strIP

    End Function

    Public Function getStatus() As ConnectionStatus
        Return status
    End Function

    Public Function getName() As String
        Return name
    End Function

    Public Sub setName(name As String)
        Me.name = name
    End Sub
    Public Function getSendEmailOnCrash() As String
        If sendEmailOnCrash Then
            Return "True"
        Else
            Return "False"
        End If
    End Function
    Public Function getTimeOfLastCheck() As DateTime
        Return timeOfLastCheck
    End Function

    Public Function getStatusAsString() As String
        If status = ConnectionStatus.Offline Then
            Return "Offline"
        ElseIf status = ConnectionStatus.Online Then
            Return "Online"
        Else
            Return "TBD"
        End If
    End Function

    Public Sub flipEmailStatus()
        sendEmailOnCrash = Not sendEmailOnCrash
    End Sub

    Public Sub SetOffline()

        status = ConnectionStatus.Offline

    End Sub

    Public Sub SetOnline()
        status = ConnectionStatus.Online
    End Sub

    Public Shared Function convertStringToConnectionStatus(status As String) As ConnectionStatus
        If status = "Online" Then
            Return ConnectionStatus.Online
        ElseIf status = "Offline" Then
            Return ConnectionStatus.Offline
        Else
            Return ConnectionStatus.Offline
        End If

    End Function

    Public Overrides Function ToString() As String
        Dim output As String = ""

        output += "IP: " + ip.ToString()
        output += Environment.NewLine + "NAME: " + name.ToString()
        output += Environment.NewLine + "STATUS: " + status.ToString()
        output += Environment.NewLine + "EMAIL: " + sendEmailOnCrash.ToString()
        output += Environment.NewLine + "TIME: " + timeOfLastCheck.ToString()


        Return output
    End Function


    Public Overrides Function Equals(obj As Object) As Boolean
        Dim connection = TryCast(obj, Connection)
        Return connection IsNot Nothing AndAlso
               EqualityComparer(Of IPAddress).Default.Equals(ip, connection.ip)
    End Function

    Public Sub ForceRefresh()
        Dim conIP As String = getIP()
        Dim conPort As Integer = 21
        Dim refreshMessage As String = "Refresh"

        SendMessageToIP(conIP, conPort, refreshMessage)
    End Sub

    Public Sub SendMessageToIP(conIP As String, conPort As Integer, message As String)
        Try
            Console.WriteLine("1")

            Dim client As TcpClient = New TcpClient(conIP, conPort)
            Try
                Console.WriteLine("2")
                Dim stream As Stream = client.GetStream()
                Console.WriteLine("3")
                Dim streamWriter As StreamWriter = New StreamWriter(stream)
                Console.WriteLine("4")
                streamWriter.AutoFlush = True
                Console.WriteLine("5")
                streamWriter.WriteLine(message)
                Console.WriteLine("6")
                stream.Close()
                Console.WriteLine("7")
            Finally
                Console.WriteLine("8")
                client.Close()
                Console.WriteLine("9")
            End Try
            Console.WriteLine("10")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Console.WriteLine("11")


    End Sub


End Class

