<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tools
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
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btnAdamet = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnAdamet
        '
        Me.btnAdamet.Location = New System.Drawing.Point(12, 12)
        Me.btnAdamet.Name = "btnAdamet"
        Me.btnAdamet.Size = New System.Drawing.Size(165, 23)
        Me.btnAdamet.TabIndex = 0
        Me.btnAdamet.Text = "Travian map adam edici"
        Me.btnAdamet.UseVisualStyleBackColor = True
        '
        'Tools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(782, 455)
        Me.Controls.Add(Me.btnAdamet)
        Me.Name = "Tools"
        Me.Text = "Tools"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnAdamet As System.Windows.Forms.Button
End Class
