Imports System.Drawing

Public Class Collector

    Private Sub Collector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim g As Graphics = e.Graphics
        Dim pn As Pen = New Pen(Color.Green, 1.9)

        g.DrawLine(pn, 10, 410, 15, 410)
        'g.DrawLine(pn, 10, 30, 100, 30)
        'g.DrawLine(pn, 10, 40, 100, 50)

        Dim i As Integer = 1
        Dim y As Integer = 1
        While i < 401
            g.DrawLine(pn, y, 410, y + 2, 410)
            i += 1
            y = y + 3
        End While
    End Sub
End Class