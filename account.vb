Imports System.Runtime.InteropServices
Imports System.IO

Public Class account
    Private Const EM_SETCUEBANNER As Integer = &H1501
    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As Int32
    End Function

    Private Sub SetHintText(ByVal control As Control, ByVal text As String)
        SendMessage(control.Handle, EM_SETCUEBANNER, 0, text)
    End Sub
    Private Sub account_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetHintText(TextBox1, "Enter User Name")
        SetHintText(TextBox2, "Enter Password")
        Call Load_acc()
    End Sub
    Private Sub Load_acc()
        Dim ioLine As String
        Dim ioLines As String
        Try
            Dim ioFile As New StreamReader(Application.StartupPath & "\data\user.txt")
            ioLine = ioFile.ReadLine
            ioLines = ioLine
            TextBox1.Text = ioLines
            TextBox2.Text = ioFile.ReadLine
            ioFile.Close()
        Catch
            MessageBox.Show("Error opening " & "user.txt", "Cyber VPN")
        End Try
    End Sub

    Private Sub ButtonBlue1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBlue1.Click
        Try
            FileOpen(1, Application.StartupPath & "\data\user.txt", OpenMode.Output)
        Catch
            MessageBox.Show("Error opening " & "user.txt", "Cyber VPN")
        End Try

        Try
            PrintLine(1, TextBox1.Text)
            Print(1, TextBox2.Text)
        Catch
            MessageBox.Show("Error writing file", "Cyber VPN")
        End Try
        MessageBox.Show("Cyber VPN Account Saved Successfully", "Cyber VPN")
        FileClose(1)
        Me.Hide()
    End Sub

    Private Sub ChromeCheckbox1_CheckedChanged(ByVal sender As Object) Handles ChromeCheckbox1.CheckedChanged
        If ChromeCheckbox1.Checked = True Then
            TextBox2.PasswordChar = vbNullChar
        ElseIf ChromeCheckbox1.Checked = False Then
            TextBox2.PasswordChar = "*"
        End If
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        Process.Start("http://prothemes.biz/index.php?route=account/register")
    End Sub
End Class