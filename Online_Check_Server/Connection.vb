Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks

Public Class Connection
    Public ip As String
    Public name As String
    Public status As Form1.ConnectionStatus
    Public sendEmailOnCrash As String
    Public timeSinceLastCheck As DateTime


    Sub New(ip As String, name As String, status As Form1.ConnectionStatus, sendEmailOnCrash As String, timeSinceLastCheck As DateTime)
        Me.ip = ip ' + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
        Me.name = name
        Me.status = status
        Me.sendEmailOnCrash = sendEmailOnCrash
        Me.timeSinceLastCheck = timeSinceLastCheck

        Console.WriteLine("NEW CON CREATED WITH IP = " + Me.ip)

    End Sub
    Sub New(ip As String, name As String, status As String, sendEmailOnCrash As String, timeSinceLastCheck As DateTime)
        Me.ip = ip '+ DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
        Me.name = name
        Me.status = convertStringToConnectionStatus(status)
        Me.sendEmailOnCrash = sendEmailOnCrash
        Me.timeSinceLastCheck = timeSinceLastCheck

        Console.WriteLine("NEW CON CREATED WITH IP = " + Me.ip)

    End Sub

    Public Function getIP() As String
        Return ip

    End Function

    Public Function getStatus() As Form1.ConnectionStatus
        Return status
    End Function

    Public Function getName() As String
        Return name
    End Function
    Public Function getSendEmailOnCrash() As String
        Return sendEmailOnCrash.ToString()
    End Function
    Public Function getTimeSInceLastCheck() As String
        Return timeSinceLastCheck
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

    Public Function convertStringToConnectionStatus(status As String) As Form1.ConnectionStatus
        If status = "Online" Then
            Return Form1.ConnectionStatus.Online
        ElseIf status = "Offline" Then
            Return Form1.ConnectionStatus.Offline
        Else
            Return Form1.ConnectionStatus.Offline
        End If

    End Function

    Public Overloads Function Equals(ByVal other As Connection)
        If ip = other.ip Then
            Return True
        Else
            Return False
        End If
    End Function

End Class

