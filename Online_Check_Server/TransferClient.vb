Imports System.ComponentModel
Imports System.Text.RegularExpressions

Public Class TransferClient

    Dim mainForm As Form1
    Sub New(mainForm As Form1)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.mainForm = mainForm


    End Sub

    'on clicking X, hide the window
    'needed because otherwise it'll either crash the program due to null variables
    'or it'll just close it.
    Private Sub Settings_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing


        Me.Hide()
        e.Cancel = True

    End Sub

    'Sends client to remote machine and install it.
    'TODO: all of this.
    'Good luck.
    Private Sub btn_SendClient_Click(sender As Object, e As EventArgs) Handles btn_SendClient.Click
        If ep_ClientIPTB.GetError(tb_ClientIP) = "" Then
            MsgBox("Valid IP, FUNCTION NOT IMPLEMENTED")
            'TODO: Send client to client machine.
        Else
            MsgBox("Invalid IP format")
        End If
    End Sub

    'on changing IP text box, verify its a correect ip format
    Private Sub tb_ClientIP_TextChanged(sender As Object, e As EventArgs) Handles tb_ClientIP.TextChanged
        Dim tb As TextBox = sender
        Dim regexString = "^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$" 'matches valid IP address
        If Not Regex.IsMatch(tb.Text, regexString) Then
            ep_ClientIPTB.SetError(tb, "Must be a valid IP")
        Else
            ep_ClientIPTB.SetError(tb, "")
        End If
    End Sub
End Class