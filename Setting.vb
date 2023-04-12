Imports System.IO
Imports Microsoft.VisualBasic.Devices

Public Class Setting
    Dim pingcom As String
    Public Shared pingurl As String
    Private ConfigPathname As String = Application.StartupPath & "\data\settings.dat"
    Dim Info As New ComputerInfo

    Private Sub Setting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If New FileInfo(ConfigPathname).Exists Then
            Dim xoption As New _Set
            Dim xoptionRow As _Set.OptionsRow
            Dim xpingerRow As _Set.PingerRow
            Dim xproxyRow As _Set.ProxyRow
            xoption.ReadXml(ConfigPathname, System.Data.XmlReadMode.IgnoreSchema)
            If xoption.Options.Rows.Count > 0 Then
                xoptionRow = xoption.Options.Rows.Item(0)
                If Not xoptionRow.IsPingNull() Then
                    ChromeCheckbox3.Checked = xoptionRow.Ping
                End If
                If Not xoptionRow.IsReconnectNull() Then
                    ChromeCheckbox4.Checked = xoptionRow.Reconnect
                End If
                If Not xoptionRow.IsHeaderNull() Then
                    ChromeCheckbox5.Checked = xoptionRow.Header
                End If
                If Not xoptionRow.IsStartupNull() Then
                    ChromeCheckbox6.Checked = xoptionRow.Startup
                End If
                If Not xoptionRow.IsPageNull Then
                    ChromeCheckbox7.Checked = xoptionRow.Page
                End If
                If Not xoptionRow.IsUrlNull Then
                    TextBox6.Text = xoptionRow.Url
                End If
            End If
            If xoption.Pinger.Rows.Count > 0 Then
                xpingerRow = xoption.Pinger.Rows.Item(0)
                If Not xpingerRow.IsIPNull() Then
                    TextBox4.Text = xpingerRow.IP
                End If
                If Not xpingerRow.IsPingdataNull() Then
                    TextBox5.Text = xpingerRow.Pingdata
                End If
                If Not xpingerRow.Is_ContinueNull Then
                    ChromeCheckbox2.Checked = xpingerRow._Continue
                End If
            End If
            If xoption.Proxy.Rows.Count > 0 Then
                xproxyRow = xoption.Proxy.Rows.Item(0)
                If Not xproxyRow.IsProxyenableNull Then
                    ChromeCheckbox1.Checked = xproxyRow.Proxyenable
                End If
                If Not xproxyRow.IsProxyaddNull Then
                    TextBox1.Text = xproxyRow.Proxyadd
                End If
                If Not xproxyRow.IsProxyportNull Then
                    TextBox2.Text = xproxyRow.Proxyport
                End If
                If Not xproxyRow.IsHostNull Then
                    TextBox3.Text = xproxyRow.Host
                End If
                If Not xproxyRow.IsOnlineNull Then
                    TextBox7.Text = xproxyRow.Online
                End If
            End If
        End If
        If System.Environment.Is64BitOperatingSystem.ToString = False Then
            Label15.Text = "32-bit Operating System"
        ElseIf System.Environment.Is64BitOperatingSystem.ToString = True Then
            Label15.Text = "64-bit Operating System"
        Else
            Label15.Text = "Unknown Error"
        End If
        Label18.Text = Info.OSFullName
        If ChromeCheckbox2.Checked = False Then
            TextBox5.Enabled = True
            If TextBox5.Text = "" Then
                pingurl = " -t"
            Else
                pingurl = " -t -n " + TextBox5.Text
            End If
        ElseIf ChromeCheckbox2.Checked = True Then
            TextBox5.Enabled = False
            pingurl = " -t"
        End If
        If ChromeCheckbox1.Checked = True Then
            TextBox1.Enabled = True
            TextBox2.Enabled = True
        ElseIf ChromeCheckbox1.Checked = False Then
            TextBox1.Enabled = False
            TextBox2.Enabled = False
        End If
        If ChromeCheckbox7.Checked = True Then
            TextBox6.Enabled = True
        ElseIf ChromeCheckbox7.Checked = False Then
            TextBox6.Enabled = False
        End If
    End Sub
    Private Sub ChromeSeparator1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChromeSeparator1.Click

    End Sub
    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub ChromeCheckbox1_CheckedChanged(ByVal sender As Object) Handles ChromeCheckbox1.CheckedChanged
        If ChromeCheckbox1.Checked = True Then
            TextBox1.Enabled = True
            TextBox2.Enabled = True
        ElseIf ChromeCheckbox1.Checked = False Then
            TextBox1.Enabled = False
            TextBox2.Enabled = False
        End If
    End Sub
    Private Sub ButtonBlue3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBlue3.Click
        Dim g As String
        g = "taskkill /f /im ping.exe"
        Shell("cmd /c" & g, vbHide)
        MsgBox("Ping Process are restarted successfully", , "Cyber VPN")
        Shell("cmd /c ping " + TextBox4.Text + pingurl, AppWinStyle.Hide)
    End Sub

    Private Sub TabPage4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage4.Click

    End Sub

    Private Sub ButtonBlue5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBlue5.Click
        WebBrowser1.Navigate("http://www.speedtest.net/mini/speedtest.swf?v=2.2.0")
    End Sub

    Private Sub ChromeCheckbox2_CheckedChanged(ByVal sender As Object) Handles ChromeCheckbox2.CheckedChanged
        If ChromeCheckbox2.Checked = False Then
            TextBox5.Enabled = True
            If TextBox5.Text = "" Then
                pingurl = " -t"
            Else
                pingurl = " -t -n " + TextBox5.Text
            End If
        ElseIf ChromeCheckbox2.Checked = True Then
            TextBox5.Enabled = False
            pingurl = " -t"
        End If
    End Sub

    Private Sub ButtonBlue10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBlue10.Click
        Try
            Dim xoption As New _Set
            Dim xoptionRow As _Set.OptionsRow
            xoptionRow = xoption.Options.NewOptionsRow
            xoptionRow.Ping = ChromeCheckbox3.Checked
            xoptionRow.Reconnect = ChromeCheckbox4.Checked
            xoptionRow.Header = ChromeCheckbox5.Checked
            xoptionRow.Startup = ChromeCheckbox6.Checked
            xoptionRow.Page = ChromeCheckbox7.Checked
            xoptionRow.Url = TextBox6.Text
            xoption.Options.AddOptionsRow(xoptionRow)
            Dim xpingerRow As _Set.PingerRow
            xpingerRow = xoption.Pinger.NewPingerRow
            xpingerRow.IP = TextBox4.Text.Trim()
            xpingerRow.Pingdata = TextBox5.Text.Trim()
            xpingerRow._Continue = ChromeCheckbox2.Checked
            xoption.Pinger.AddPingerRow(xpingerRow)
            Dim xproxyRow As _Set.ProxyRow
            xproxyRow = xoption.Proxy.NewProxyRow
            xproxyRow.Proxyenable = ChromeCheckbox1.Checked
            xproxyRow.Proxyadd = TextBox1.Text
            xproxyRow.Proxyport = TextBox2.Text
            xproxyRow.Host = TextBox3.Text
            xproxyRow.Online = TextBox7.Text
            xoption.Proxy.AddProxyRow(xproxyRow)
            xoption.WriteXml(ConfigPathname, System.Data.XmlWriteMode.IgnoreSchema)
            MsgBox("Cyber VPN Settings Saved Successfully", , "Cyber VPN")
        Catch ex As Exception
            MsgBox("Settings Not Saved - Error", , "Cyber VPN")
        End Try
    End Sub

    Private Sub ChromeButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChromeButton2.Click
        Try
            Dim xoption As New _Set
            Dim xoptionRow As _Set.OptionsRow
            xoptionRow = xoption.Options.NewOptionsRow
            xoptionRow.Ping = ChromeCheckbox3.Checked
            xoptionRow.Reconnect = ChromeCheckbox4.Checked
            xoptionRow.Header = ChromeCheckbox5.Checked
            xoptionRow.Startup = ChromeCheckbox6.Checked
            xoptionRow.Page = ChromeCheckbox7.Checked
            xoptionRow.Url = TextBox6.Text
            xoption.Options.AddOptionsRow(xoptionRow)
            Dim xpingerRow As _Set.PingerRow
            xpingerRow = xoption.Pinger.NewPingerRow
            xpingerRow.IP = TextBox4.Text.Trim()
            xpingerRow.Pingdata = TextBox5.Text.Trim()
            xpingerRow._Continue = ChromeCheckbox2.Checked
            xoption.Pinger.AddPingerRow(xpingerRow)
            Dim xproxyRow As _Set.ProxyRow
            xproxyRow = xoption.Proxy.NewProxyRow
            xproxyRow.Proxyenable = ChromeCheckbox1.Checked
            xproxyRow.Proxyadd = TextBox1.Text
            xproxyRow.Proxyport = TextBox2.Text
            xproxyRow.Host = TextBox3.Text
            xproxyRow.Online = TextBox7.Text
            xoption.Proxy.AddProxyRow(xproxyRow)
            xoption.WriteXml(ConfigPathname, System.Data.XmlWriteMode.IgnoreSchema)
            MsgBox("Pinger Settings Saved Successfully", , "Cyber VPN")
        Catch ex As Exception
            MsgBox("Pinger Not Saved - Error", , "Cyber VPN")
        End Try
    End Sub
    Private Sub ChromeCheckbox7_CheckedChanged(ByVal sender As Object) Handles ChromeCheckbox7.CheckedChanged
        If ChromeCheckbox7.Checked = True Then
            TextBox6.Enabled = True
        ElseIf ChromeCheckbox7.Checked = False Then
            TextBox6.Enabled = False
        End If
    End Sub

    Private Sub ButtonBlue6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBlue6.Click
        Shell(Application.StartupPath & "\bin\addtapWin32.bat", AppWinStyle.NormalFocus)
    End Sub

    Private Sub ChromeCheckbox6_CheckedChanged(ByVal sender As Object) Handles ChromeCheckbox6.CheckedChanged
        If ChromeCheckbox6.Checked = True Then
            My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue("Cyber VPN Beta", Application.ExecutablePath)
        ElseIf ChromeCheckbox6.Checked = False Then
            My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).DeleteValue("Cyber VPN Beta")
        End If
    End Sub

    Private Sub ButtonBlue7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBlue7.Click
        Shell(Application.StartupPath & "\bin\deltapallWin32.bat", AppWinStyle.NormalFocus)
    End Sub

    Private Sub ButtonBlue9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBlue9.Click
        Shell(Application.StartupPath & "\bin\addtapWin64.bat", AppWinStyle.NormalFocus)
    End Sub

    Private Sub ButtonBlue8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBlue8.Click
        Shell(Application.StartupPath & "\bin\deltapallWin64.bat", AppWinStyle.NormalFocus)
    End Sub

    Private Sub ChromeButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChromeButton1.Click
        Dim g As String
        g = "taskkill /f /im ping.exe"
        Shell("cmd /c" & g, vbHide)
        MsgBox("All Ping Process are Stopped Successfully", , "Cyber VPN")
    End Sub

    Private Sub ButtonBlue4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBlue4.Click
        TextBox4.Text = "google.com"
        TextBox5.Text = "8"
        TextBox5.Enabled = False
        ChromeCheckbox2.Checked = True
        MsgBox("Default Settings Loaded Successfully", , "Cyber VPN")
    End Sub

    Private Sub ButtonBlue1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBlue1.Click
        Try
            Dim xoption As New _Set
            Dim xoptionRow As _Set.OptionsRow
            xoptionRow = xoption.Options.NewOptionsRow
            xoptionRow.Ping = ChromeCheckbox3.Checked
            xoptionRow.Reconnect = ChromeCheckbox4.Checked
            xoptionRow.Header = ChromeCheckbox5.Checked
            xoptionRow.Startup = ChromeCheckbox6.Checked
            xoptionRow.Page = ChromeCheckbox7.Checked
            xoptionRow.Url = TextBox6.Text
            xoption.Options.AddOptionsRow(xoptionRow)
            Dim xpingerRow As _Set.PingerRow
            xpingerRow = xoption.Pinger.NewPingerRow
            xpingerRow.IP = TextBox4.Text.Trim()
            xpingerRow.Pingdata = TextBox5.Text.Trim()
            xpingerRow._Continue = ChromeCheckbox2.Checked
            xoption.Pinger.AddPingerRow(xpingerRow)
            Dim xproxyRow As _Set.ProxyRow
            xproxyRow = xoption.Proxy.NewProxyRow
            xproxyRow.Proxyenable = ChromeCheckbox1.Checked
            xproxyRow.Proxyadd = TextBox1.Text
            xproxyRow.Proxyport = TextBox2.Text
            xproxyRow.Host = TextBox3.Text
            xproxyRow.Online = TextBox7.Text
            xoption.Proxy.AddProxyRow(xproxyRow)
            xoption.WriteXml(ConfigPathname, System.Data.XmlWriteMode.IgnoreSchema)
            MsgBox("Proxy Settings Saved Successfully", , "Cyber VPN")
        Catch ex As Exception
            MsgBox("Settings Not Saved - Error", , "Cyber VPN")
        End Try
    End Sub

    Private Sub ChromeButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChromeButton3.Click
        Try
            Dim web As New Net.WebClient
            Dim source As String = web.DownloadString("http://prothemes.biz/cyber-updater.txt")
            If source.Contains("cyber1.0") Then
                Label22.Text = "You have the Latest Version...."
            ElseIf source.Contains("update") Then
                Dim newweb As New Net.WebClient
                Dim newsource As String = newweb.DownloadString("http://prothemes.biz/cyber-updater.txt")
                System.Diagnostics.Process.Start(newsource)
                Label22.Text = "New Version Available...."
                MsgBox("Download the Newer Version: " & vbCrLf & newsource, , "Updater")
            End If
        Catch ex As Exception
            MsgBox("Can't Check for Update: Check your Internet Connection", , "Error")
        End Try
    End Sub

    Private Sub ChromeButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChromeButton4.Click
        If ChromeButton4.Text = "Edit" Then
            TextBox3.Enabled = True
            TextBox7.Enabled = True
            ChromeButton4.Text = "OK"
        ElseIf ChromeButton4.Text = "OK" Then
            TextBox3.Enabled = False
            TextBox7.Enabled = False
            ChromeButton4.Text = "Edit"
        Try
            Dim xoption As New _Set
            Dim xoptionRow As _Set.OptionsRow
            xoptionRow = xoption.Options.NewOptionsRow
            xoptionRow.Ping = ChromeCheckbox3.Checked
            xoptionRow.Reconnect = ChromeCheckbox4.Checked
            xoptionRow.Header = ChromeCheckbox5.Checked
            xoptionRow.Startup = ChromeCheckbox6.Checked
            xoptionRow.Page = ChromeCheckbox7.Checked
            xoptionRow.Url = TextBox6.Text
            xoption.Options.AddOptionsRow(xoptionRow)
            Dim xpingerRow As _Set.PingerRow
            xpingerRow = xoption.Pinger.NewPingerRow
            xpingerRow.IP = TextBox4.Text.Trim()
            xpingerRow.Pingdata = TextBox5.Text.Trim()
            xpingerRow._Continue = ChromeCheckbox2.Checked
            xoption.Pinger.AddPingerRow(xpingerRow)
            Dim xproxyRow As _Set.ProxyRow
            xproxyRow = xoption.Proxy.NewProxyRow
            xproxyRow.Proxyenable = ChromeCheckbox1.Checked
            xproxyRow.Proxyadd = TextBox1.Text
            xproxyRow.Proxyport = TextBox2.Text
            xproxyRow.Host = TextBox3.Text
            xproxyRow.Online = TextBox7.Text
            xoption.Proxy.AddProxyRow(xproxyRow)
            xoption.WriteXml(ConfigPathname, System.Data.XmlWriteMode.IgnoreSchema)
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class