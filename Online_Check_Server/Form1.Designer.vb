<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.listView_Connections = New System.Windows.Forms.ListView()
        Me.col_ip = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.col_Name = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.col_status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.col_sendEmail = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.col_MinSinceLastCheck = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.gb_IpRange = New System.Windows.Forms.GroupBox()
        Me.lbl_IPRangeTo = New System.Windows.Forms.Label()
        Me.lbl_IPRangeFrom = New System.Windows.Forms.Label()
        Me.tb_higherIPRange = New System.Windows.Forms.TextBox()
        Me.tb_lowerIPRange = New System.Windows.Forms.TextBox()
        Me.gb_serverControls = New System.Windows.Forms.GroupBox()
        Me.btn_forceRefresh = New System.Windows.Forms.Button()
        Me.btn_toggleServerCheck = New System.Windows.Forms.Button()
        Me.tb_ServerName = New System.Windows.Forms.TextBox()
        Me.gb_ServerName = New System.Windows.Forms.GroupBox()
        Me.error_ip = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransferClientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.gb_IpRange.SuspendLayout()
        Me.gb_serverControls.SuspendLayout()
        Me.gb_ServerName.SuspendLayout()
        CType(Me.error_ip, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'listView_Connections
        '
        Me.listView_Connections.AllowColumnReorder = True
        Me.listView_Connections.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.col_ip, Me.col_Name, Me.col_status, Me.col_sendEmail, Me.col_MinSinceLastCheck})
        Me.listView_Connections.FullRowSelect = True
        Me.listView_Connections.GridLines = True
        Me.listView_Connections.LabelEdit = True
        Me.listView_Connections.Location = New System.Drawing.Point(12, 27)
        Me.listView_Connections.Name = "listView_Connections"
        Me.listView_Connections.Size = New System.Drawing.Size(618, 304)
        Me.listView_Connections.TabIndex = 0
        Me.listView_Connections.UseCompatibleStateImageBehavior = False
        Me.listView_Connections.View = System.Windows.Forms.View.Details
        '
        'col_ip
        '
        Me.col_ip.Text = "Server IP"
        Me.col_ip.Width = 122
        '
        'col_Name
        '
        Me.col_Name.Text = "Server Name"
        Me.col_Name.Width = 153
        '
        'col_status
        '
        Me.col_status.Text = "Server Status"
        Me.col_status.Width = 87
        '
        'col_sendEmail
        '
        Me.col_sendEmail.Text = "Send Email On Crash"
        Me.col_sendEmail.Width = 115
        '
        'col_MinSinceLastCheck
        '
        Me.col_MinSinceLastCheck.Text = "Time Since Last Check"
        Me.col_MinSinceLastCheck.Width = 136
        '
        'gb_IpRange
        '
        Me.gb_IpRange.Controls.Add(Me.lbl_IPRangeTo)
        Me.gb_IpRange.Controls.Add(Me.lbl_IPRangeFrom)
        Me.gb_IpRange.Controls.Add(Me.tb_higherIPRange)
        Me.gb_IpRange.Controls.Add(Me.tb_lowerIPRange)
        Me.gb_IpRange.Location = New System.Drawing.Point(12, 336)
        Me.gb_IpRange.Margin = New System.Windows.Forms.Padding(2)
        Me.gb_IpRange.Name = "gb_IpRange"
        Me.gb_IpRange.Padding = New System.Windows.Forms.Padding(2)
        Me.gb_IpRange.Size = New System.Drawing.Size(233, 87)
        Me.gb_IpRange.TabIndex = 1
        Me.gb_IpRange.TabStop = False
        Me.gb_IpRange.Text = "IP Range"
        '
        'lbl_IPRangeTo
        '
        Me.lbl_IPRangeTo.AutoSize = True
        Me.lbl_IPRangeTo.Location = New System.Drawing.Point(118, 35)
        Me.lbl_IPRangeTo.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_IPRangeTo.Name = "lbl_IPRangeTo"
        Me.lbl_IPRangeTo.Size = New System.Drawing.Size(23, 13)
        Me.lbl_IPRangeTo.TabIndex = 3
        Me.lbl_IPRangeTo.Text = "To:"
        '
        'lbl_IPRangeFrom
        '
        Me.lbl_IPRangeFrom.AutoSize = True
        Me.lbl_IPRangeFrom.Location = New System.Drawing.Point(3, 35)
        Me.lbl_IPRangeFrom.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_IPRangeFrom.Name = "lbl_IPRangeFrom"
        Me.lbl_IPRangeFrom.Size = New System.Drawing.Size(30, 13)
        Me.lbl_IPRangeFrom.TabIndex = 2
        Me.lbl_IPRangeFrom.Text = "From"
        '
        'tb_higherIPRange
        '
        Me.tb_higherIPRange.Location = New System.Drawing.Point(145, 33)
        Me.tb_higherIPRange.Margin = New System.Windows.Forms.Padding(2)
        Me.tb_higherIPRange.Name = "tb_higherIPRange"
        Me.tb_higherIPRange.Size = New System.Drawing.Size(84, 20)
        Me.tb_higherIPRange.TabIndex = 1
        '
        'tb_lowerIPRange
        '
        Me.tb_lowerIPRange.Location = New System.Drawing.Point(38, 33)
        Me.tb_lowerIPRange.Margin = New System.Windows.Forms.Padding(2)
        Me.tb_lowerIPRange.Name = "tb_lowerIPRange"
        Me.tb_lowerIPRange.Size = New System.Drawing.Size(76, 20)
        Me.tb_lowerIPRange.TabIndex = 0
        '
        'gb_serverControls
        '
        Me.gb_serverControls.Controls.Add(Me.btn_forceRefresh)
        Me.gb_serverControls.Controls.Add(Me.btn_toggleServerCheck)
        Me.gb_serverControls.Location = New System.Drawing.Point(249, 336)
        Me.gb_serverControls.Margin = New System.Windows.Forms.Padding(2)
        Me.gb_serverControls.Name = "gb_serverControls"
        Me.gb_serverControls.Padding = New System.Windows.Forms.Padding(2)
        Me.gb_serverControls.Size = New System.Drawing.Size(246, 87)
        Me.gb_serverControls.TabIndex = 2
        Me.gb_serverControls.TabStop = False
        Me.gb_serverControls.Text = "Server Controls"
        '
        'btn_forceRefresh
        '
        Me.btn_forceRefresh.Location = New System.Drawing.Point(133, 36)
        Me.btn_forceRefresh.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_forceRefresh.Name = "btn_forceRefresh"
        Me.btn_forceRefresh.Size = New System.Drawing.Size(63, 24)
        Me.btn_forceRefresh.TabIndex = 1
        Me.btn_forceRefresh.Text = "Refresh "
        Me.btn_forceRefresh.UseVisualStyleBackColor = True
        '
        'btn_toggleServerCheck
        '
        Me.btn_toggleServerCheck.Location = New System.Drawing.Point(13, 36)
        Me.btn_toggleServerCheck.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_toggleServerCheck.Name = "btn_toggleServerCheck"
        Me.btn_toggleServerCheck.Size = New System.Drawing.Size(97, 24)
        Me.btn_toggleServerCheck.TabIndex = 0
        Me.btn_toggleServerCheck.Text = "Enable / DIsable"
        Me.btn_toggleServerCheck.UseVisualStyleBackColor = True
        '
        'tb_ServerName
        '
        Me.tb_ServerName.Location = New System.Drawing.Point(15, 40)
        Me.tb_ServerName.Name = "tb_ServerName"
        Me.tb_ServerName.Size = New System.Drawing.Size(100, 20)
        Me.tb_ServerName.TabIndex = 3
        '
        'gb_ServerName
        '
        Me.gb_ServerName.Controls.Add(Me.tb_ServerName)
        Me.gb_ServerName.Location = New System.Drawing.Point(500, 336)
        Me.gb_ServerName.Name = "gb_ServerName"
        Me.gb_ServerName.Size = New System.Drawing.Size(130, 87)
        Me.gb_ServerName.TabIndex = 4
        Me.gb_ServerName.TabStop = False
        Me.gb_ServerName.Text = "Server Name"
        '
        'error_ip
        '
        Me.error_ip.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.error_ip.ContainerControl = Me
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem, Me.TransferClientToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(642, 24)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'TransferClientToolStripMenuItem
        '
        Me.TransferClientToolStripMenuItem.Name = "TransferClientToolStripMenuItem"
        Me.TransferClientToolStripMenuItem.Size = New System.Drawing.Size(95, 20)
        Me.TransferClientToolStripMenuItem.Text = "Transfer Client"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(642, 428)
        Me.Controls.Add(Me.gb_ServerName)
        Me.Controls.Add(Me.gb_serverControls)
        Me.Controls.Add(Me.gb_IpRange)
        Me.Controls.Add(Me.listView_Connections)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.gb_IpRange.ResumeLayout(False)
        Me.gb_IpRange.PerformLayout()
        Me.gb_serverControls.ResumeLayout(False)
        Me.gb_ServerName.ResumeLayout(False)
        Me.gb_ServerName.PerformLayout()
        CType(Me.error_ip, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents listView_Connections As ListView
    Friend WithEvents col_ip As ColumnHeader
    Friend WithEvents col_status As ColumnHeader
    Friend WithEvents col_Name As ColumnHeader
    Friend WithEvents col_sendEmail As ColumnHeader
    Friend WithEvents col_MinSinceLastCheck As ColumnHeader
    Friend WithEvents gb_IpRange As GroupBox
    Friend WithEvents lbl_IPRangeTo As Label
    Friend WithEvents lbl_IPRangeFrom As Label
    Friend WithEvents tb_higherIPRange As TextBox
    Friend WithEvents tb_lowerIPRange As TextBox
    Friend WithEvents gb_serverControls As GroupBox
    Friend WithEvents btn_forceRefresh As Button
    Friend WithEvents btn_toggleServerCheck As Button
    Friend WithEvents tb_ServerName As TextBox
    Friend WithEvents gb_ServerName As GroupBox
    Friend WithEvents error_ip As ErrorProvider
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TransferClientToolStripMenuItem As ToolStripMenuItem
End Class
