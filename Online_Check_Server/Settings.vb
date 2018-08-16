Imports System.ComponentModel
Imports System.Text.RegularExpressions

Public Class Settings
    Private Default_Email = ""
    Private Default_TimeToOffline = "2"
    Private Default_RefreshRate = "10000"


    Public Email As String = ""
    Public TimeToOffline As String = ""
    Public RefreshRate As String = ""

    Private mainForm As Form1

    Sub New(mainForm As Form1)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.mainForm = mainForm

        Dim list As List(Of String) = XMLController.loadSettings()

        Email = list(0)
        RefreshRate = list(1)
        TimeToOffline = list(2)

        tb_EmailAddress.Text = Email
        tb_RefreshRate.Text = RefreshRate
        tb_TimeToOffline.Text = TimeToOffline



        If tb_EmailAddress.Text = "" Then
            tb_EmailAddress.Text = Default_Email
            Email = Default_Email
        End If
        If tb_RefreshRate.Text = "" Then
            tb_RefreshRate.Text = Default_RefreshRate
            RefreshRate = Default_RefreshRate
        End If
        If tb_TimeToOffline.Text = "" Then
            tb_TimeToOffline.Text = Default_TimeToOffline
            TimeToOffline = Default_TimeToOffline
        End If






    End Sub

    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub tb_RefreshRate_TextChanged(sender As Object, e As EventArgs) Handles tb_RefreshRate.TextChanged
        Dim tb As TextBox = sender
        Dim regexString = "^[0-9]*$" 'Numbers only
        ' if lower and higher ip matches regex
        If Not Regex.IsMatch(tb.Text, regexString) Then
            ep_refresh.SetError(tb, "Must be a number")
        Else
            ep_refresh.SetError(tb, "")
            RefreshRate = tb.Text
        End If


    End Sub

    Private Sub tb_TimeToOffline_TextChanged(sender As Object, e As EventArgs) Handles tb_TimeToOffline.TextChanged

        Dim tb As TextBox = sender
        Dim regexString = "^[0-9]*$" 'numbers only
        ' if lower and higher ip matches regex
        If Not Regex.IsMatch(tb.Text, regexString) Then
            ep_offlineTime.SetError(tb, "Must be a number")
        Else
            ep_offlineTime.SetError(tb, "")
            TimeToOffline = tb.Text
        End If

    End Sub

    Private Sub tb_EmailAddress_TextChanged(sender As Object, e As EventArgs) Handles tb_EmailAddress.TextChanged

        Dim tb As TextBox = sender
        Dim regexString = "^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$" 'matches email
        ' if lower and higher ip matches regex
        If Not Regex.IsMatch(tb.Text, regexString) Then
            ep_Email.SetError(tb, "Must be a valid email")
        Else
            ep_Email.SetError(tb, "")
            Email = tb.Text
        End If

    End Sub

    Private Sub Settings_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing


        Me.Hide()
        e.Cancel = True

    End Sub

    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click



        If ep_Email.GetError(tb_EmailAddress) Is "" _
                        And ep_refresh.GetError(tb_RefreshRate) Is "" _
                        And ep_offlineTime.GetError(tb_TimeToOffline) Is "" Then
            Dim list As New List(Of String)
            list.AddRange({Email, RefreshRate, TimeToOffline})
            XMLController.saveSettings(list)
            mainForm.UpdateSettingsValues()

        Else
            MsgBox("Invalid inputs, settings not saved", MsgBoxStyle.Exclamation, "Error")


        End If

    End Sub
End Class