Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Text

Public Class Form1
    Public SelectServer As String
    Public IPServer As String
    Public tnew As Integer
    Dim ipConfig(10) As String
    Dim Folder As IO.FileInfo
    Dim nOsname As String
    Dim proto As String
    Dim serverf As String
    Dim servers As String
    Dim nOs As Integer
    Dim pingurl As String
    Dim c As Integer = 0
    Private ConfigPathname As String = Application.StartupPath & "\data\settings.dat"
    Private configport As String = Application.StartupPath & "\data\port.dat"

    Private Function GetyourIP()
        Dim sR As StreamReader = New StreamReader(Application.StartupPath & "\data\servers.dat")
        Dim line As String

        'Server IP 1 - Respective server name

        nOs = 1
        nOsname = " CyberVPN Romania Server - " & nOs
        line = sR.ReadLine()
        ipConfig(nOs) = line
        ComboBox1.Items.Add(nOsname)
        nOs = nOs + 1

        'Server IP 2 - Respective server name

        nOsname = " CyberVPN Romania Server - " & nOs
        line = sR.ReadLine()
        ipConfig(nOs) = line
        ComboBox1.Items.Add(nOsname)
        nOs = nOs + 1

        'Server IP 3 - Respective server name

        nOsname = " CyberVPN UK Server - " & nOs
        line = sR.ReadLine()
        ipConfig(nOs) = line
        ComboBox1.Items.Add(nOsname)
        nOs = nOs + 1

        'Server IP 4 - Respective server name

        nOsname = " CyberVPN USA Server - " & nOs
        line = sR.ReadLine()
        ipConfig(nOs) = line
        ComboBox1.Items.Add(nOsname)
        sR.Close()

        'Same way add more servers, if it is needed.

    End Function
    Public Sub myConnection()
        If ChromeRadioButton1.Checked = True Then
            proto = "udp"
        ElseIf ChromeRadioButton2.Checked = True Then
            proto = "tcp"
        End If

        'servers refers TCP & serverf refers UDP

        If Setting.ChromeCheckbox1.Checked = True Then
            If Setting.ChromeCheckbox5.Checked = True Then

                ' TCP Config File with Proxy & Custom Header

                servers = "--client --dev tun2 --remote " & IPServer & " --proto " & proto & " --port " & TextBox1.Text & "--nobind " & _
"--keepalive 20 60 --reneg-sec 432000 --resolv-retry infinite --cipher AES-128-CBC --fast-io --pull --auth-user-pass data\user.txt " & _
"--persist-key --persist-tun --ca data\vpnbook.ca --comp-lzo --verb 3 --redirect-gateway --route-delay 2 " & _
"--http-proxy-timeout 9 --http-proxy " & Setting.TextBox1.Text & " " & Setting.TextBox2.Text & " --http-proxy-option CUSTOM-HEADER Host " & Setting.TextBox3.Text & " --http-proxy-option CUSTOM-HEADER X-Online-Host " & Setting.TextBox7.Text & _
" --log data\logfile.tmp --status data\status.dat 1 "
            ElseIf Setting.ChromeCheckbox5.Checked = False Then

                ' TCP Config File with Proxy Only

                servers = "--client --dev tun2 --remote " & IPServer & " --proto " & proto & " --port " & TextBox1.Text & "--nobind " & _
"--keepalive 20 60 --reneg-sec 432000 --resolv-retry infinite --cipher AES-128-CBC  --fast-io --pull --auth-user-pass data\user.txt " & _
"--persist-key --persist-tun --ca data\vpnbook.ca --comp-lzo --verb 3 --redirect-gateway --route-delay 2 " & _
"--http-proxy-timeout 9 --http-proxy " & Setting.TextBox1.Text & " " & Setting.TextBox2.Text & " " & _
" --log data\logfile.tmp --status data\status.dat 1 "

            End If
        Else

            ' TCP Config File without Proxy 

            servers = "--client --dev tun2 --remote " & IPServer & " --proto " & proto & " --port " & TextBox1.Text & "--lport " & TextBox2.Text & _
" --keepalive 20 60 --reneg-sec 432000 --resolv-retry infinite --cipher AES-128-CBC  --fast-io --pull --auth-user-pass data\user.txt " & _
"--persist-key --persist-tun --ca data\vpnbook.ca --comp-lzo --verb 3 --redirect-gateway --route-delay 2 " & _
" --log data\logfile.tmp --status data\status.dat 1 "

        End If

        ' UDP Config File

        serverf = "--client --dev tun2 --remote " & IPServer & " --proto " & proto & " --port " & TextBox1.Text & "  --lport " & TextBox2.Text & _
    "--keepalive 20 60 --reneg-sec 432000 --resolv-retry infinite --cipher AES-128-CBC  --fast-io --pull --tun-mtu 1500 --auth-user-pass data\user.txt " & _
    "--persist-key --persist-tun --ca data\vpnbook.ca --comp-lzo --verb 3 --redirect-gateway --route-delay 2 --explicit-exit-notify 2 " & _
    " --log data\logfile.tmp --status data\status.dat 1 "


        If ChromeRadioButton1.Checked = True Then
            SelectServer = serverf
        ElseIf ChromeRadioButton2.Checked = True Then
            SelectServer = servers
        End If

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        ComboBox1.Items.Clear()
        Call GetyourIP()
        Try
            Dim Filenum As Integer = FreeFile()
            FileOpen(Filenum, Application.StartupPath & "\data\logfile.tmp", OpenMode.Output)
            FileClose()
        Catch ex As Exception

        End Try
        loadsettings()
        Try
            If New FileInfo(configport).Exists Then
                Dim xoption As New _Set
                Dim xoptionPort As _Set.PortRow
                xoption.ReadXml(configport, System.Data.XmlReadMode.IgnoreSchema)
                If xoption.Port.Rows.Count > 0 Then
                    xoptionPort = xoption.Port.Rows.Item(0)
                    If Not xoptionPort.IsRportNull Then
                        TextBox1.Text = xoptionPort.Rport
                    End If
                    If Not xoptionPort.IsLportNull Then
                        TextBox2.Text = xoptionPort.Lport
                    End If
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ChromeButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles portbut.Click
        If portbut.Text = "Edit Ports" Then
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            portbut.Text = "OK"
        ElseIf portbut.Text = "OK" Then
            TextBox1.Enabled = False
            TextBox2.Enabled = False
            portbut.Text = "Edit Ports"
            Try
                Dim xoption As New _Set
                Dim xoptionPort As _Set.PortRow
                xoptionPort = xoption.Port.NewPortRow
                xoptionPort.Rport = TextBox1.Text
                xoptionPort.Lport = TextBox2.Text
                xoption.Port.AddPortRow(xoptionPort)
                xoption.WriteXml(configport, System.Data.XmlWriteMode.IgnoreSchema)
            Catch ex As Exception
                MsgBox("Settings Not Saved - Error", , "Cyber VPN")
            End Try
        End If
    End Sub

    Private Sub ComboBox1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedValueChanged
        IPServer = ipConfig(Microsoft.VisualBasic.Right(ComboBox1.Text, 2))
    End Sub

    Private Sub ButtonBlue1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBlue1.Click
        loadsettings()
        tnew = 0
        Try
            If Setting.ChromeCheckbox2.Checked = False Then
                Setting.TextBox5.Enabled = True
                If Setting.TextBox5.Text = "" Then
                    pingurl = " -t"
                Else
                    pingurl = " -t -n " + Setting.TextBox5.Text
                End If
            ElseIf Setting.ChromeCheckbox2.Checked = True Then
                Setting.TextBox5.Enabled = False
                pingurl = " -t"
            End If
        Catch ex As Exception

        End Try
        If ButtonBlue1.Text = "Connect" Then
            If ComboBox1.Text = "" Then
                MessageBox.Show("Please select your server!")
            Else
                Try
                    Dim Filenum As Integer = FreeFile()
                    FileOpen(Filenum, Application.StartupPath & "\data\logfile.tmp", OpenMode.Output)
                    FileClose()
                Catch ex As Exception

                End Try
                Label2.Text = "Connecting"
                NotifyIcon1.Icon = My.Resources.connectingn
                Label2.ForeColor = Color.FromArgb(239, 105, 0)
                ButtonBlue1.Text = "Connecting"
                NotifyIcon1.ShowBalloonTip(3000, "Cyber VPN Beta", "Status: Connecting", ToolTipIcon.Info)
                myConnection()
                If Setting.ChromeCheckbox3.Checked = True Then
                    Shell("cmd /c ping " + Setting.TextBox4.Text + pingurl, AppWinStyle.Hide)
                End If
                Shell(Application.StartupPath & "\bin\openvpn " & SelectServer, AppWinStyle.Hide)
                TimerLog.Start()
            End If
        ElseIf ButtonBlue1.Text = "Disconnect" Then
            NotifyIcon1.Icon = My.Resources.newidle
            c = 0
            Dim g As String
            g = "taskkill /f /im openvpn.exe"
            Shell("cmd /c" & g, vbHide)
            g = "taskkill /f /im ping.exe"
            Shell("cmd /c" & g, vbHide)
            TimerLog.Stop()
            Label2.ForeColor = Color.FromArgb(253, 106, 72)
            Label2.Text = "Disconnected"
            ButtonBlue1.Text = "Connect"
            Try
                Dim Filenum As Integer = FreeFile()
                FileOpen(Filenum, Application.StartupPath & "\data\logfile.tmp", OpenMode.Output)
                FileClose()
            Catch ex As Exception

            End Try
        ElseIf ButtonBlue1.Text = "Connecting" Then
            If MsgBox("Do You want to Disconnect Cyber VPN", MsgBoxStyle.YesNo, "Cyber VPN") = vbYes Then
                NotifyIcon1.Icon = My.Resources.newidle
                c = 0
                Dim g As String
                g = "taskkill /f /im openvpn.exe"
                Shell("cmd /c" & g, vbHide)
                g = "taskkill /f /im ping.exe"
                Shell("cmd /c" & g, vbHide)
                TimerLog.Stop()
                Label2.ForeColor = Color.FromArgb(253, 106, 72)
                Label2.Text = "Disconnected"
                ButtonBlue1.Text = "Connect"
                Try
                    Dim Filenum As Integer = FreeFile()
                    FileOpen(Filenum, Application.StartupPath & "\data\logfile.tmp", OpenMode.Output)
                    FileClose()
                Catch ex As Exception

                End Try
            End If
        End If
    End Sub

    Private Sub ChromeButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChromeButton2.Click
        Dim g As String
        g = "taskkill /f /im openvpn.exe"
        Shell("cmd /c" & g, vbHide)
        g = "taskkill /f /im ping.exe"
        Shell("cmd /c" & g, vbHide)
        TimerLog.Stop()
        loadsettings()
        Label2.Text = "Re-Connecting"
        Label2.ForeColor = Color.FromArgb(253, 52, 52)
        ButtonBlue1.Text = "Connecting"
        NotifyIcon1.ShowBalloonTip(3000, "Cyber VPN Beta", "Status: Connecting", ToolTipIcon.Info)
        myConnection()
        If Setting.ChromeCheckbox3.Checked = True Then
            Shell("cmd /c ping " + Setting.TextBox4.Text + " " + Setting.pingurl, AppWinStyle.Hide)
        End If
        Shell(Application.StartupPath & "\bin\openvpn " & SelectServer, AppWinStyle.Hide)
        TimerLog.Start()
    End Sub
    Private Sub TimerLog_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerLog.Tick
        If tnew = 0 Then
            log_view.RichTextBox1.Text = ""
        End If
        Try
            Logs.Text = ""
            Dim textline(10000) As String
            Dim TestfileX As String = Application.StartupPath & "\data\logfile.tmp"
            Dim log_ctr As Integer
            Dim last_log As Integer
            FileOpen(1, TestfileX, OpenMode.Input, OpenAccess.Read, OpenShare.LockRead)
            Do Until EOF(1)
                textline(log_ctr) = LineInput(1)
                log_ctr = log_ctr + 1
                If last_log <> log_ctr Then
                    For n = last_log To log_ctr - 1
                        If tnew = 0 Then
                            log_view.RichTextBox1.Text = log_view.RichTextBox1.Text & textline(n) & vbCrLf
                        End If
                        If InStrRev(textline(n), "OpenVPN") > 0 Then
                            Logs.SelectedText = Logs.SelectedText & "Connecting to Network......." & vbCrLf
                        ElseIf InStrRev(textline(n), "this configuration to call") > 0 Then
                            Logs.SelectedText = Logs.SelectedText & "Bypassing Firewall......" & vbCrLf & "Accessing Server....please wait" & vbCrLf
                        ElseIf InStrRev(textline(n), "Peer Connection Initiated with 0.0.0.0:0") > 0 Then
                            Logs.SelectedText = Logs.SelectedText & "Initiating Server...." & vbCrLf
                        ElseIf InStrRev(textline(n), "TAP-WIN32 device") > 0 Then
                            Logs.SelectedText = Logs.SelectedText & "Peer Connection Initiated with 0.0.0.0:0:1194" & vbCrLf
                        ElseIf InStrRev(textline(n), "NETSH: C:\WINDOWS\system32\netsh.exe interface ip set address {") > 0 Then
                            Logs.SelectedText = Logs.SelectedText & "Warning!! You Have been detected!!" & vbCrLf & "Deleting the content of your Drive C:..." & vbCrLf
                        ElseIf InStrRev(textline(n), "Successful ARP Flush on interface") > 0 Then
                            Logs.SelectedText = Logs.SelectedText & "Please wait........" & vbCrLf
                        ElseIf InStrRev(textline(n), "Initialization Sequence Completed") > 0 Then
                            Logs.SelectedText = "Initialization Sequence Completed" & vbCrLf
                            If c = 0 Then
                                NotifyIcon1.Icon = My.Resources.connected
                                NotifyIcon1.ShowBalloonTip(3000, "Cyber VPN Beta", "Status: Cyber VPN Connected Successfully", ToolTipIcon.Info)
                                Me.WindowState = FormWindowState.Minimized
                                Me.Hide()
                                c = 1
                                If Setting.ChromeCheckbox7.Checked = True Then
                                    Process.Start(Setting.TextBox6.Text)
                                End If
                                tnew = 1
                            End If
                            Label2.Text = "Connected"
                            ButtonBlue1.Text = "Disconnect"
                            Label2.ForeColor = Color.Green
                            Logs.SelectedText = Logs.SelectedText & "Your Connected, Enjoy Your Browsing" & vbCrLf & "//Thanks for Using\\"

                        ElseIf InStrRev(textline(n), "AUTH: Received AUTH_FAILED control message") > 0 Then
                            Logs.SelectedText = "Authentication Failed" & vbCrLf
                            Logs.SelectedText = "Check your Account Details" & vbCrLf
                            Label2.Text = "AUTH FAILED"
                            Label2.ForeColor = Color.Red
                            Dim g As String
                            g = "taskkill /f /im openvpn.exe"
                            Shell("cmd /c" & g, vbHide)
                            g = "taskkill /f /im ping.exe"
                            Shell("cmd /c" & g, vbHide)
                            ButtonBlue1.Text = "Connect"
                            TimerLog.Stop()
                            NotifyIcon1.ShowBalloonTip(3000, "Cyber VPN Beta", "Status: Authentication Failed", ToolTipIcon.Info)

                        ElseIf InStrRev(textline(n), "Inactivity timeout (--ping-restart), restarting") > 0 Then
                            Dim g As String
                            g = "taskkill /f /im openvpn.exe"
                            Shell("cmd /c" & g, vbHide)
                            g = "taskkill /f /im ping.exe"
                            Shell("cmd /c" & g, vbHide)
                            ButtonBlue1.Text = "Connect"
                            Label2.Text = "Disconnected"
                            Logs.Text = "======Inactivity timeout======" & vbNewLine & "Unable to connect..." & vbCrLf & "Please Try to connect again" & vbCrLf & vbCrLf & "=======Try Again======"
                            TimerLog.Stop()
                            NotifyIcon1.ShowBalloonTip(3000, "Cyber VPN Beta", "Status: Force Reconnect", ToolTipIcon.Info)
                            Label2.Text = "Trying to Force Reconnect"
                            Label2.ForeColor = Color.Red
                            Timer1.Enabled = True
                            tnew = 0
                        ElseIf InStrRev(textline(n), "Exiting") > 0 Then
                            Dim g As String
                            g = "taskkill /f /im openvpn.exe"
                            Shell("cmd /c" & g, vbHide)
                            g = "taskkill /f /im ping.exe"
                            Shell("cmd /c" & g, vbHide)
                            ButtonBlue1.Text = "Connect"
                            Label2.Text = "Disconnected"
                            Logs.Text = "Unable to connect..." & vbCrLf & "Please Try to connect again"
                            TimerLog.Enabled = False
                        End If
                    Next
                    last_log = log_ctr
                End If
            Loop
        Catch ex As Exception

        End Try
        Try
            FileClose(1)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ButtonBlue3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBlue3.Click
        TextBox1.Text = "53"
        TextBox2.Text = "1194"
        ChromeRadioButton1.Checked = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        myConnection()
        If Setting.ChromeCheckbox3.Checked = True Then
            Shell("cmd /c ping " + Setting.TextBox4.Text + " " + Setting.pingurl, AppWinStyle.Hide)
        End If
        Shell("cmd ipconfig/flushdns ipconfig/registerdns", AppWinStyle.Hide)
        Shell(Application.StartupPath & "\bin\openvpn.exe " & SelectServer, AppWinStyle.Hide)
        TimerLog.Start()
        Label2.Text = "Force Reconnect"
        Label2.ForeColor = Color.Orange
        Timer1.Enabled = False
    End Sub

    Private Sub ChromeButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChromeButton1.Click
        Try
            log_view.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ChromeButton4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChromeButton4.Click
        account.Show()
    End Sub

    Private Sub ButtonBlue2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBlue2.Click
        Setting.Show()
    End Sub

    Private Sub NotifyIcon1_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles NotifyIcon1.DoubleClick
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Show()
            Me.WindowState = FormWindowState.Normal
        End If
        Me.Activate()
        Me.Focus()
    End Sub

    Private Sub ShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowToolStripMenuItem.Click
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub ChromeButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChromeButton3.Click
        about.Show()
    End Sub

    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
        Process.Start("http://facebook.com/Cyber")
    End Sub

    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click
        Process.Start("http://twitter.com/Cyber")
    End Sub

    Private Sub loadsettings()
        Try
            If New FileInfo(ConfigPathname).Exists Then
                Dim xoption As New _Set
                Dim xoptionRow As _Set.OptionsRow
                Dim xpingerRow As _Set.PingerRow
                Dim xproxyRow As _Set.ProxyRow
                xoption.ReadXml(ConfigPathname, System.Data.XmlReadMode.IgnoreSchema)
                If xoption.Options.Rows.Count > 0 Then
                    xoptionRow = xoption.Options.Rows.Item(0)
                    If Not xoptionRow.IsPingNull() Then
                        Setting.ChromeCheckbox3.Checked = xoptionRow.Ping
                    End If
                    If Not xoptionRow.IsReconnectNull() Then
                        Setting.ChromeCheckbox4.Checked = xoptionRow.Reconnect
                    End If
                    If Not xoptionRow.IsHeaderNull() Then
                        Setting.ChromeCheckbox5.Checked = xoptionRow.Header
                    End If
                    If Not xoptionRow.IsStartupNull() Then
                        Setting.ChromeCheckbox6.Checked = xoptionRow.Startup
                    End If
                    If Not xoptionRow.IsPageNull Then
                        Setting.ChromeCheckbox7.Checked = xoptionRow.Page
                    End If
                    If Not xoptionRow.IsUrlNull Then
                        Setting.TextBox6.Text = xoptionRow.Url
                    End If
                End If
                If xoption.Pinger.Rows.Count > 0 Then
                    xpingerRow = xoption.Pinger.Rows.Item(0)
                    If Not xpingerRow.IsIPNull() Then
                        Setting.TextBox4.Text = xpingerRow.IP
                    End If
                    If Not xpingerRow.IsPingdataNull() Then
                        Setting.TextBox5.Text = xpingerRow.Pingdata
                    End If
                    If Not xpingerRow.Is_ContinueNull Then
                        Setting.ChromeCheckbox2.Checked = xpingerRow._Continue
                    End If
                End If
                If xoption.Proxy.Rows.Count > 0 Then
                    xproxyRow = xoption.Proxy.Rows.Item(0)
                    If Not xproxyRow.IsProxyenableNull Then
                        Setting.ChromeCheckbox1.Checked = xproxyRow.Proxyenable
                    End If
                    If Not xproxyRow.IsProxyaddNull Then
                        Setting.TextBox1.Text = xproxyRow.Proxyadd
                    End If
                    If Not xproxyRow.IsProxyportNull Then
                        Setting.TextBox2.Text = xproxyRow.Proxyport
                    End If
                    If Not xproxyRow.IsHostNull Then
                        Setting.TextBox3.Text = xproxyRow.Host
                    End If
                    If Not xproxyRow.IsOnlineNull Then
                        Setting.TextBox7.Text = xproxyRow.Online
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub HideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideToolStripMenuItem.Click
        Me.Hide()
        about.Hide()
        account.Hide()
        Setting.Hide()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Try
            Dim g As String
            g = "taskkill /f /im openvpn.exe"
            Shell("cmd /c" & g, vbHide)
            g = "taskkill /f /im ping.exe"
            Shell("cmd /c" & g, vbHide)
        Catch ex As Exception

        End Try
        about.Close()
        account.Close()
        Setting.Close()
        Me.Close()
        End
    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click
        Process.Start("http://prothemes.biz/")
    End Sub

    Private Sub DisconnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisconnectToolStripMenuItem.Click
        Try
            If ButtonBlue1.Text = "Disconnect" Then
                NotifyIcon1.Icon = My.Resources.newidle
                c = 0
                Dim g As String
                g = "taskkill /f /im openvpn.exe"
                Shell("cmd /c" & g, vbHide)
                g = "taskkill /f /im ping.exe"
                Shell("cmd /c" & g, vbHide)
                TimerLog.Stop()
                Label2.ForeColor = Color.FromArgb(253, 106, 72)
                Label2.Text = "Disconnected"
                ButtonBlue1.Text = "Connect"
                Try
                    Dim Filenum As Integer = FreeFile()
                    FileOpen(Filenum, Application.StartupPath & "\data\logfile.tmp", OpenMode.Output)
                    FileClose()
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

