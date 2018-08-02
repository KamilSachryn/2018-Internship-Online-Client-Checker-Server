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
        Me.listView_Connections = New System.Windows.Forms.ListView()
        Me.col_ip = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.col_status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.col_Name = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.col_sendEmail = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.gb_IpRange = New System.Windows.Forms.GroupBox()
        Me.tb_lowerIPRange = New System.Windows.Forms.TextBox()
        Me.tb_higherIPRange = New System.Windows.Forms.TextBox()
        Me.lbl_IPRangeFrom = New System.Windows.Forms.Label()
        Me.lbl_IPRangeTo = New System.Windows.Forms.Label()
        Me.gb_serverControls = New System.Windows.Forms.GroupBox()
        Me.btn_toggleServerCheck = New System.Windows.Forms.Button()
        Me.btn_forceRefresh = New System.Windows.Forms.Button()
        Me.col_MinSinceLastCheck = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.gb_IpRange.SuspendLayout()
        Me.gb_serverControls.SuspendLayout()
        Me.SuspendLayout()
        '
        'listView_Connections
        '
        Me.listView_Connections.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.col_ip, Me.col_Name, Me.col_status, Me.col_sendEmail, Me.col_MinSinceLastCheck})
        Me.listView_Connections.GridLines = True
        Me.listView_Connections.Location = New System.Drawing.Point(36, 31)
        Me.listView_Connections.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.listView_Connections.Name = "listView_Connections"
        Me.listView_Connections.Size = New System.Drawing.Size(917, 558)
        Me.listView_Connections.TabIndex = 0
        Me.listView_Connections.UseCompatibleStateImageBehavior = False
        Me.listView_Connections.View = System.Windows.Forms.View.Details
        '
        'col_ip
        '
        Me.col_ip.Text = "Server IP"
        Me.col_ip.Width = 122
        '
        'col_status
        '
        Me.col_status.DisplayIndex = 1
        Me.col_status.Text = "Server Status"
        Me.col_status.Width = 149
        '
        'col_Name
        '
        Me.col_Name.Text = "Server Name"
        Me.col_Name.Width = 153
        '
        'col_sendEmail
        '
        Me.col_sendEmail.Text = "Send Email On Crash"
        Me.col_sendEmail.Width = 218
        '
        'gb_IpRange
        '
        Me.gb_IpRange.Controls.Add(Me.lbl_IPRangeTo)
        Me.gb_IpRange.Controls.Add(Me.lbl_IPRangeFrom)
        Me.gb_IpRange.Controls.Add(Me.tb_higherIPRange)
        Me.gb_IpRange.Controls.Add(Me.tb_lowerIPRange)
        Me.gb_IpRange.Location = New System.Drawing.Point(985, 31)
        Me.gb_IpRange.Name = "gb_IpRange"
        Me.gb_IpRange.Size = New System.Drawing.Size(428, 106)
        Me.gb_IpRange.TabIndex = 1
        Me.gb_IpRange.TabStop = False
        Me.gb_IpRange.Text = "IP Range"
        '
        'tb_lowerIPRange
        '
        Me.tb_lowerIPRange.Location = New System.Drawing.Point(69, 61)
        Me.tb_lowerIPRange.Name = "tb_lowerIPRange"
        Me.tb_lowerIPRange.Size = New System.Drawing.Size(100, 29)
        Me.tb_lowerIPRange.TabIndex = 0
        '
        'tb_higherIPRange
        '
        Me.tb_higherIPRange.Location = New System.Drawing.Point(288, 61)
        Me.tb_higherIPRange.Name = "tb_higherIPRange"
        Me.tb_higherIPRange.Size = New System.Drawing.Size(100, 29)
        Me.tb_higherIPRange.TabIndex = 1
        '
        'lbl_IPRangeFrom
        '
        Me.lbl_IPRangeFrom.AutoSize = True
        Me.lbl_IPRangeFrom.Location = New System.Drawing.Point(6, 65)
        Me.lbl_IPRangeFrom.Name = "lbl_IPRangeFrom"
        Me.lbl_IPRangeFrom.Size = New System.Drawing.Size(57, 25)
        Me.lbl_IPRangeFrom.TabIndex = 2
        Me.lbl_IPRangeFrom.Text = "From"
        '
        'lbl_IPRangeTo
        '
        Me.lbl_IPRangeTo.AutoSize = True
        Me.lbl_IPRangeTo.Location = New System.Drawing.Point(216, 65)
        Me.lbl_IPRangeTo.Name = "lbl_IPRangeTo"
        Me.lbl_IPRangeTo.Size = New System.Drawing.Size(42, 25)
        Me.lbl_IPRangeTo.TabIndex = 3
        Me.lbl_IPRangeTo.Text = "To:"
        '
        'gb_serverControls
        '
        Me.gb_serverControls.Controls.Add(Me.btn_forceRefresh)
        Me.gb_serverControls.Controls.Add(Me.btn_toggleServerCheck)
        Me.gb_serverControls.Location = New System.Drawing.Point(962, 194)
        Me.gb_serverControls.Name = "gb_serverControls"
        Me.gb_serverControls.Size = New System.Drawing.Size(451, 161)
        Me.gb_serverControls.TabIndex = 2
        Me.gb_serverControls.TabStop = False
        Me.gb_serverControls.Text = "Server Controls"
        '
        'btn_toggleServerCheck
        '
        Me.btn_toggleServerCheck.Location = New System.Drawing.Point(23, 67)
        Me.btn_toggleServerCheck.Name = "btn_toggleServerCheck"
        Me.btn_toggleServerCheck.Size = New System.Drawing.Size(177, 45)
        Me.btn_toggleServerCheck.TabIndex = 0
        Me.btn_toggleServerCheck.Text = "Enable / DIsable"
        Me.btn_toggleServerCheck.UseVisualStyleBackColor = True
        '
        'btn_forceRefresh
        '
        Me.btn_forceRefresh.Location = New System.Drawing.Point(244, 67)
        Me.btn_forceRefresh.Name = "btn_forceRefresh"
        Me.btn_forceRefresh.Size = New System.Drawing.Size(116, 45)
        Me.btn_forceRefresh.TabIndex = 1
        Me.btn_forceRefresh.Text = "Refresh "
        Me.btn_forceRefresh.UseVisualStyleBackColor = True
        '
        'col_MinSinceLastCheck
        '
        Me.col_MinSinceLastCheck.Text = "Time Since Last Check"
        Me.col_MinSinceLastCheck.Width = 233
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1467, 831)
        Me.Controls.Add(Me.gb_serverControls)
        Me.Controls.Add(Me.gb_IpRange)
        Me.Controls.Add(Me.listView_Connections)
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.gb_IpRange.ResumeLayout(False)
        Me.gb_IpRange.PerformLayout()
        Me.gb_serverControls.ResumeLayout(False)
        Me.ResumeLayout(False)

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
End Class
