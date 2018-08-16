<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Settings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tb_EmailAddress = New System.Windows.Forms.TextBox()
        Me.tb_RefreshRate = New System.Windows.Forms.TextBox()
        Me.tb_TimeToOffline = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ep_Email = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ep_refresh = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ep_offlineTime = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.btn_Save = New System.Windows.Forms.Button()
        CType(Me.ep_Email, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ep_refresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ep_offlineTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(53, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Email"
        '
        'tb_EmailAddress
        '
        Me.tb_EmailAddress.Location = New System.Drawing.Point(91, 13)
        Me.tb_EmailAddress.Name = "tb_EmailAddress"
        Me.tb_EmailAddress.Size = New System.Drawing.Size(100, 20)
        Me.tb_EmailAddress.TabIndex = 1
        '
        'tb_RefreshRate
        '
        Me.tb_RefreshRate.Location = New System.Drawing.Point(91, 39)
        Me.tb_RefreshRate.Name = "tb_RefreshRate"
        Me.tb_RefreshRate.Size = New System.Drawing.Size(100, 20)
        Me.tb_RefreshRate.TabIndex = 2
        '
        'tb_TimeToOffline
        '
        Me.tb_TimeToOffline.Location = New System.Drawing.Point(91, 65)
        Me.tb_TimeToOffline.Name = "tb_TimeToOffline"
        Me.tb_TimeToOffline.Size = New System.Drawing.Size(100, 20)
        Me.tb_TimeToOffline.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Refresh Rate"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Time to offline"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(197, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "miliseconds"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(197, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "minutes"
        '
        'ep_Email
        '
        Me.ep_Email.ContainerControl = Me
        '
        'ep_refresh
        '
        Me.ep_refresh.ContainerControl = Me
        '
        'ep_offlineTime
        '
        Me.ep_offlineTime.ContainerControl = Me
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'btn_Save
        '
        Me.btn_Save.Location = New System.Drawing.Point(198, 100)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(75, 23)
        Me.btn_Save.TabIndex = 8
        Me.btn_Save.Text = "Save"
        Me.btn_Save.UseVisualStyleBackColor = True
        '
        'Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(285, 385)
        Me.Controls.Add(Me.btn_Save)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tb_TimeToOffline)
        Me.Controls.Add(Me.tb_RefreshRate)
        Me.Controls.Add(Me.tb_EmailAddress)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Settings"
        Me.Text = "Form2"
        CType(Me.ep_Email, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ep_refresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ep_offlineTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents tb_EmailAddress As TextBox
    Friend WithEvents tb_RefreshRate As TextBox
    Friend WithEvents tb_TimeToOffline As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents ep_Email As ErrorProvider
    Friend WithEvents ep_refresh As ErrorProvider
    Friend WithEvents ep_offlineTime As ErrorProvider
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents btn_Save As Button
End Class
