<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TransferClient
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
        Me.btn_SendClient = New System.Windows.Forms.Button()
        Me.tb_ClientIP = New System.Windows.Forms.TextBox()
        Me.ep_ClientIPTB = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.ep_ClientIPTB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Client IP"
        '
        'btn_SendClient
        '
        Me.btn_SendClient.Location = New System.Drawing.Point(89, 32)
        Me.btn_SendClient.Name = "btn_SendClient"
        Me.btn_SendClient.Size = New System.Drawing.Size(75, 23)
        Me.btn_SendClient.TabIndex = 1
        Me.btn_SendClient.Text = "Send Client"
        Me.btn_SendClient.UseVisualStyleBackColor = True
        '
        'tb_ClientIP
        '
        Me.tb_ClientIP.Location = New System.Drawing.Point(64, 6)
        Me.tb_ClientIP.Name = "tb_ClientIP"
        Me.tb_ClientIP.Size = New System.Drawing.Size(100, 20)
        Me.tb_ClientIP.TabIndex = 2
        '
        'ep_ClientIPTB
        '
        Me.ep_ClientIPTB.ContainerControl = Me
        '
        'TransferClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(181, 67)
        Me.Controls.Add(Me.tb_ClientIP)
        Me.Controls.Add(Me.btn_SendClient)
        Me.Controls.Add(Me.Label1)
        Me.Name = "TransferClient"
        Me.Text = "TransferClient"
        CType(Me.ep_ClientIPTB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btn_SendClient As Button
    Friend WithEvents tb_ClientIP As TextBox
    Friend WithEvents ep_ClientIPTB As ErrorProvider
End Class
