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
    'Make sure we dont spam email
    Public tempSendEmailOnCrash As Boolean

    'For use in sending messages to the Client
    Private RefreshMessage As String = "Refresh"
    Private conIP As String = getIP()
    Private conPort As Integer = 21


    'Enum for use in the GUI
    'Keeps track of what the connection state is.
    Public Enum ConnectionStatus
        Online = 0
        Offline = 1
    End Enum

    'Creates a new connection object
    Sub New(ip As IPAddress, name As String, status As ConnectionStatus, sendEmailOnCrash As String, timeSinceLastCheck As DateTime)
        Me.ip = ip
        Me.name = name
        Me.status = status
        Me.sendEmailOnCrash = sendEmailOnCrash
        Me.timeOfLastCheck = timeSinceLastCheck
        Me.tempSendEmailOnCrash = sendEmailOnCrash

        Console.WriteLine("NEW CON CREATED WITH IP = " + Me.ip.ToString())

    End Sub

    'returns IP address
    Public Function getIP() As String
        Dim strIP As String = ip.ToString()
        Return strIP

    End Function

    'Returns status
    Public Function getStatus() As ConnectionStatus
        Return status
    End Function

    'returns name
    Public Function getName() As String
        Return name
    End Function

    'sets name
    Public Sub setName(name As String)
        Me.name = name
    End Sub

    'return if send email on crash
    Public Function getSendEmailOnCrash() As String

        Return sendEmailOnCrash

    End Function

    'return last check time
    Public Function getTimeOfLastCheck() As DateTime
        Return timeOfLastCheck
    End Function

    'Get status as a string for use in some functions
    'Not entirely needed, but makes our life easier in some places
    Public Function getStatusAsString() As String
        If status = ConnectionStatus.Offline Then
            Return "Offline"
        ElseIf status = ConnectionStatus.Online Then
            Return "Online"
        Else
            Return "TBD"
        End If
    End Function


    'if on, set to off, if off, set to on
    Public Sub flipEmailStatus()
        sendEmailOnCrash = Not sendEmailOnCrash
    End Sub

    'set connection status as offline
    Public Sub SetOffline()

        status = ConnectionStatus.Offline

    End Sub

    'set connection status as online
    Public Sub SetOnline()
        status = ConnectionStatus.Online
    End Sub

    'turns a string into a conenction status.
    'TODO: remove this and always use the enum
    Public Shared Function convertStringToConnectionStatus(status As String) As ConnectionStatus
        If status = "Online" Then
            Return ConnectionStatus.Online
        ElseIf status = "Offline" Then
            Return ConnectionStatus.Offline
        Else
            Return ConnectionStatus.Offline
        End If

    End Function

    'get all info from object for debug purposes
    Public Overrides Function ToString() As String
        Dim output As String = ""

        output += "IP: " + ip.ToString()
        output += Environment.NewLine + "NAME: " + name.ToString()
        output += Environment.NewLine + "STATUS: " + status.ToString()
        output += Environment.NewLine + "EMAIL: " + sendEmailOnCrash.ToString()
        output += Environment.NewLine + "TIME: " + timeOfLastCheck.ToString()


        Return output
    End Function

    'for use in DEBUG
    'checks if a connection is equal to another
    Public Overrides Function Equals(obj As Object) As Boolean
        Dim connection = TryCast(obj, Connection)
        Return connection IsNot Nothing AndAlso
               EqualityComparer(Of IPAddress).Default.Equals(ip, connection.ip)
    End Function

    'Send a refresh message to the client
    'message is handled in the client to forcefully make a connection to the server
    Public Sub ForceRefresh()

        SendMessageToIP(conIP, conPort, refreshMessage)
    End Sub


    'standard connection to client 
    'sends a string message
    'client then does an action based on the message
    Public Sub SendMessageToIP(conIP As String, conPort As Integer, message As String)
        Try

            Dim client As TcpClient = New TcpClient(conIP, conPort)
            Try
                Dim stream As Stream = client.GetStream()
                Dim streamWriter As StreamWriter = New StreamWriter(stream)
                streamWriter.AutoFlush = True
                streamWriter.WriteLine(message)
                stream.Close()
            Finally
                client.Close()
            End Try
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try


    End Sub


End Class

