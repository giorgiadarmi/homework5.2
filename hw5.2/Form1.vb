Imports System.Drawing.Drawing2D

Public Class Form1

    Public b As Bitmap
    Public g As Graphics
    Public SmallFont As New Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Pixel)
    'dataset

    'viewport
    Public Viewport As New Rectangle(400, 50, 500, 200)
    Sub InitializeGraphics()
        Me.b = New Bitmap(Me.PictureBox1.Width, Me.PictureBox1.Height)
        Me.g = Graphics.FromImage(b)
        Me.g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.InitializeGraphics()
        Me.DrawScene()
    End Sub

    Sub DrawScene()
        g.Clear(Color.White)
        Me.g.DrawRectangle(Pens.Green, Viewport)
        Me.PictureBox1.Image = b
    End Sub
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Application.CurrentCulture = Globalization.CultureInfo.InvariantCulture
    End Sub

    Private Viewport_At_Mouse_Down As Rectangle
    Private MouseLocation_At_Mouse_Down As Point
    Private Dragging_Started As Boolean
    Private Resizing_Started As Boolean
    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        If Me.Viewport.Contains(e.X, e.Y) Then
            Me.Viewport_At_Mouse_Down = Me.Viewport
            Me.MouseLocation_At_Mouse_Down = New Point(e.X, e.Y)

            If e.Button = Windows.Forms.MouseButtons.Left Then
                Me.Dragging_Started = True
            ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
                Resizing_Started = True
            End If
        End If
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        Me.PictureBox1.Focus()
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If Me.Dragging_Started Then
            Dim Delta_X As Integer = e.X - Me.MouseLocation_At_Mouse_Down.X
            Dim Delta_Y As Integer = e.Y - Me.MouseLocation_At_Mouse_Down.Y

            Me.Viewport.X = Me.MouseLocation_At_Mouse_Down.X + Delta_X
            Me.Viewport.Y = Me.MouseLocation_At_Mouse_Down.Y + Delta_Y

            'Update of the drawing
            Me.DrawScene()
        ElseIf Me.Resizing_Started Then
            Dim Delta_X As Integer = e.X - Me.MouseLocation_At_Mouse_Down.X
            Dim Delta_Y As Integer = e.Y - Me.MouseLocation_At_Mouse_Down.Y

            Me.Viewport.Width = Me.MouseLocation_At_Mouse_Down.X + Delta_X
            Me.Viewport.Height = Me.MouseLocation_At_Mouse_Down.Y + Delta_Y

            Me.DrawScene()

        End If

    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        Me.Dragging_Started = False
        Me.Resizing_Started = False
    End Sub

    Private Sub PictureBox1_MouseWheel(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseWheel
        Dim Change_X As Integer = CInt(Me.Viewport.Width / 10)
        Dim Change_Y As Integer = CInt(Me.Viewport.Height * Change_X / Me.Viewport.Width)

        If e.Delta > 0 Then
            Me.Viewport.X -= Change_X
            Me.Viewport.Width += 2 * Change_X

            Me.Viewport.Y -= Change_Y
            Me.Viewport.Height += 2 * Change_Y

            Me.DrawScene()

        ElseIf e.Delta < 0 Then
            Me.Viewport.X += Change_X
            Me.Viewport.Width -= 2 * Change_X

            Me.Viewport.Y += Change_Y
            Me.Viewport.Height -= 2 * Change_Y

            Me.DrawScene()
        End If
    End Sub


End Class
