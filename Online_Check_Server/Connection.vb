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
    Public status As Form1.ConnectionStatus
    Public sendEmailOnCrash As Boolean
    Public timeOfLastCheck As DateTime
    Public tempSendEmailOnCrash As Boolean


    Sub New(ip As IPAddress, name As String, status As Form1.ConnectionStatus, sendEmailOnCrash As String, timeSinceLastCheck As DateTime)
        Me.ip = ip
        Me.name = name
        Me.status = status
        Me.sendEmailOnCrash = sendEmailOnCrash
        Me.timeOfLastCheck = timeSinceLastCheck
        Me.tempSendEmailOnCrash = sendEmailOnCrash

        Console.WriteLine("NEW CON CREATED WITH IP = " + Me.ip.ToString())

    End Sub
    Sub New(ip As IPAddress, name As String, status As String, sendEmailOnCrash As String, timeSinceLastCheck As DateTime)
        Me.ip = ip
        Me.name = name
        Me.status = convertStringToConnectionStatus(status)
        Me.sendEmailOnCrash = sendEmailOnCrash
        Me.timeOfLastCheck = timeSinceLastCheck
        Me.tempSendEmailOnCrash = sendEmailOnCrash

        Console.WriteLine("NEW CON CREATED WITH IP = " + Me.ip.ToString())

    End Sub

    Public Function getIP() As String
        Dim strIP As String = ip.ToString()
        'strIP = strIP.Substring(0, strIP.LastIndexOf(":"))
        Return strIP

    End Function

    Public Function getStatus() As Form1.ConnectionStatus
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
        If status = Form1.ConnectionStatus.Offline Then
            Return "Offline"
        ElseIf status = Form1.ConnectionStatus.Online Then
            Return "Online"
        Else
            Return "TBD"
        End If
    End Function

    Public Sub flipEmailStatus()
        sendEmailOnCrash = Not sendEmailOnCrash
    End Sub

    Public Sub SetOffline()

        status = Form1.ConnectionStatus.Offline

    End Sub

    Public Sub SetOnline()
        status = Form1.ConnectionStatus.Online
    End Sub

    Public Function convertStringToConnectionStatus(status As String) As Form1.ConnectionStatus
        If status = "Online" Then
            Return Form1.ConnectionStatus.Online
        ElseIf status = "Offline" Then
            Return Form1.ConnectionStatus.Offline
        Else
            Return Form1.ConnectionStatus.Offline
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

    Public Function ForceSendMessage() As Boolean
        Return True
    End Function


End Class

