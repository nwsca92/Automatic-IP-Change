Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Linq
Imports System.Net
Imports System.IO
Imports System.Text.Json
Imports System.Drawing


Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ComboBox1.Items.Clear() ' Optional: clear existing items

        Dim adapters As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()

        For Each adapter As NetworkInterface In adapters
            ComboBox1.Items.Add(adapter.Name)
        Next

        ' Optional: select the first adapter by default
        If ComboBox1.Items.Count > 0 Then
            ComboBox1.SelectedIndex = 0
        End If
    End Sub

    Private Sub SaveSettings(ip As String, mask As String, gateway As String, dnsList As List(Of String), adapterName As String)
        Dim settings As New NetworkSettings With {
        .InterfaceName = adapterName,
        .IPAddress = ip,
        .SubnetMask = mask,
        .Gateway = gateway,
        .DnsServers = dnsList,
        .ApplyTime = DateTime.Now
    }

        Dim json As String = JsonSerializer.Serialize(settings, New JsonSerializerOptions With {.WriteIndented = True})
        Dim filePath As String = Path.Combine(Application.StartupPath, "SavedNetwork.json")

        Try
            File.WriteAllText(filePath, json)
        Catch ex As Exception
            MessageBox.Show("Failed to save settings: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub LoadSavedSettings()
        Dim filePath As String = Path.Combine(Application.StartupPath, "SavedNetwork.json")
        If Not File.Exists(filePath) Then
            ' No saved settings
            Return
        End If

        Try
            Dim json As String = File.ReadAllText(filePath)
            Dim settings As NetworkSettings = JsonSerializer.Deserialize(Of NetworkSettings)(json)

            ' Display or auto-fill saved values
            TextBox1.Text = $"Last saved: {settings.IPAddress}/{settings.SubnetMask} via {settings.InterfaceName}"
            ' You can use this info later to restore the IP programmatically

        Catch ex As Exception
            MessageBox.Show("Could not load saved settings: " & ex.Message)
        End Try

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        DisplayAdapterInfo()
    End Sub
    Private Sub DisplayAdapterInfo()
        TextBox1.Text = "IP Address: -"
        TextBox2.Text = "Subnet Mask: -"
        TextBox3.Text = "Gateway: -"
        TextBox4.Text = "DNS: -"

        Dim selectedItem As String = ComboBox1.SelectedItem?.ToString()
        If String.IsNullOrEmpty(selectedItem) OrElse selectedItem = "Tidak ditemukan Wi-Fi adapter " Then
            Return
        End If

        ' Get adapter info
        Dim adapter As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces() _
            .FirstOrDefault(Function(nic) nic.Name = selectedItem AndAlso
                            nic.NetworkInterfaceType = NetworkInterfaceType.Wireless80211)

        If adapter Is Nothing OrElse adapter.OperationalStatus <> OperationalStatus.Up Then
            TextBox1.Text = "IP Address: (Adapter tidak aktif)"
            Return
        End If

        Dim ipProps As IPInterfaceProperties = adapter.GetIPProperties()

        ' --- IPv4 Address & Subnet Mask ---
        Dim unicastAddress As UnicastIPAddressInformation = ipProps.UnicastAddresses _
            .FirstOrDefault(Function(addr) addr.Address.AddressFamily = AddressFamily.InterNetwork)

        If unicastAddress IsNot Nothing Then
            TextBox1.Text = "IP Address: " & unicastAddress.Address.ToString()
            TextBox2.Text = "Subnet Mask: " & unicastAddress.IPv4Mask.ToString()
        End If

        ' --- Default Gateway ---
        If ipProps.GatewayAddresses.Count > 0 Then
            Dim gateway As GatewayIPAddressInformation = ipProps.GatewayAddresses(0)
            TextBox3.Text = "Gateway: " & gateway.Address.ToString()
        Else
            TextBox3.Text = "Gateway: (none)"
        End If

        ' --- DNS Servers ---
        Dim dnsList As List(Of String) = New List(Of String)
        For Each dns As IPAddress In ipProps.DnsAddresses
            If dns.AddressFamily = AddressFamily.InterNetwork Then ' IPv4 only
                dnsList.Add(dns.ToString())
            End If
        Next

        If dnsList.Count > 0 Then
            TextBox4.Text = "DNS: " & String.Join(", ", dnsList)
        Else
            TextBox4.Text = "DNS: (automatic / not assigned)"
        End If
    End Sub
    Public Class NetworkSettings
        Public Property InterfaceName As String
        Public Property IPAddress As String
        Public Property SubnetMask As String
        Public Property Gateway As String
        Public Property DnsServers As List(Of String)
        Public Property ApplyTime As Date ' Optional: when it was saved
    End Class
    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Get current values displayed (or fetch again from adapter)
        Dim ip As String = TextBox1.Text.Replace("IP Address: ", "")
        Dim mask As String = TextBox2.Text.Replace("Subnet Mask: ", "")
        Dim gateway As String = TextBox3.Text.Replace("Gateway: ", "")
        Dim dnsText As String = TextBox4.Text.Replace("DNS: ", "")
        Dim dnsList As New List(Of String)

        If dnsText.Contains(",") Then
            dnsList.AddRange(dnsText.Split(",").Select(Function(s) s.Trim()))
        ElseIf Not String.IsNullOrEmpty(dnsText) AndAlso Not dnsText.Contains("automatic") Then
            dnsList.Add(dnsText)
        End If

        Dim selectedAdapter As String = ComboBox1.SelectedItem?.ToString()

        If selectedAdapter = "Tidak ditemukan Wi-Fi adapter" Or String.IsNullOrEmpty(selectedAdapter) Then
            MessageBox.Show("Pilih adapter yang sesuai.")
            Return
        End If

        SaveSettings(ip, mask, gateway, dnsList, selectedAdapter)
        MessageBox.Show("Pengaturan berhasil disimpan!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ApplySavedSettings(settings As NetworkSettings)
        If settings Is Nothing Then
            MessageBox.Show("Parameter tidak ditemukan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Confirm action
        Dim result As DialogResult = MessageBox.Show(
            $"Ganti IP statis untuk '{settings.InterfaceName}'?{vbCrLf}{vbCrLf}" &
            $"IP: {settings.IPAddress}{vbCrLf}" &
            $"Mask: {settings.SubnetMask}{vbCrLf}" &
            $"Gateway: {settings.Gateway}{vbCrLf}" &
            $"DNS: {String.Join(", ", settings.DnsServers)}",
            "Apply Network Settings",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If result = DialogResult.No Then Return

        Try
            ' 1. Set static IP and subnet
            RunNetsh($"interface ip set address ""{settings.InterfaceName}"" static {settings.IPAddress} {settings.SubnetMask} {settings.Gateway}")

            ' 2. Set primary DNS
            If settings.DnsServers.Count > 0 Then
                RunNetsh($"interface ip set dns ""{settings.InterfaceName}"" static {settings.DnsServers(0)}")

                ' Set secondary/tertiary DNS as "add" (not "static")
                For i As Integer = 1 To settings.DnsServers.Count - 1
                    RunNetsh($"interface ip add dns ""{settings.InterfaceName}"" {settings.DnsServers(i)} index={i + 1}")
                Next
            End If

            MessageBox.Show("Berhasil mengganti IP ke statis!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Gagal mengganti IP ke statis: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RunNetsh(args As String)
        Dim startInfo As New ProcessStartInfo With {
            .FileName = "netsh",
            .Arguments = args,
            .UseShellExecute = False,
            .RedirectStandardOutput = True,
            .RedirectStandardError = True,
            .CreateNoWindow = True,
            .WindowStyle = ProcessWindowStyle.Hidden
        }

        Using p As Process = Process.Start(startInfo)
            p.WaitForExit()
            Dim output As String = p.StandardOutput.ReadToEnd()
            Dim errorOut As String = p.StandardError.ReadToEnd()

            If p.ExitCode <> 0 Then
                Throw New Exception($"Exit Code {p.ExitCode}{If(String.IsNullOrEmpty(errorOut), "", "; Error: " & errorOut)}")
            End If
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim filePath As String = Path.Combine(Application.StartupPath, "SavedNetwork.json")
        If Not File.Exists(filePath) Then
            MessageBox.Show("Parameter jaringan tidak ditemukan, coba simpan dulu", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim json As String = File.ReadAllText(filePath)
            Dim savedSettings As NetworkSettings = JsonSerializer.Deserialize(Of NetworkSettings)(json)
            ApplySavedSettings(savedSettings)

        Catch ex As Exception
            MessageBox.Show("Tidak bisa mengembalikan parameter: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim selectedAdapter As String = ComboBox1.SelectedItem?.ToString()
        If String.IsNullOrEmpty(selectedAdapter) OrElse selectedAdapter = "No Wi-Fi adapter found" Then
            MessageBox.Show("Select a valid Wi-Fi adapter.")
            Return
        End If

        Dim result As DialogResult = MessageBox.Show($"Ganti '{selectedAdapter}' ke otomatis (DHCP)?", "DHCP", MessageBoxButtons.YesNo)
        If result = DialogResult.No Then Return

        Try
            RunNetsh($"interface ip set address ""{selectedAdapter}"" dhcp")
            RunNetsh($"interface ip set dns ""{selectedAdapter}"" dhcp")
            MessageBox.Show("Jaringan berganti ke DHCP.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Gagal mengganti ke DHCP: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        ' ✅ 1. Update Jam (Kanan)
        ToolStripStatusLabel3.Text = DateTime.Now.ToString("dd-MMM-yyyy  HH:mm:ss")

        ' ✅ 2. Cek Status Jaringan
        If Not IsNetworkConnected() Then
            ToolStripStatusLabel1.Text = "Status: No Network 🔴"
            ToolStripStatusLabel1.ForeColor = Color.Red
            ToolStripStatusLabel1.Text = "(No network)"
        Else
            If IsInternetAvailable() Then
                ToolStripStatusLabel1.Text = "Status: Online 🌐"
                ToolStripStatusLabel1.ForeColor = Color.Green
            Else
                ToolStripStatusLabel1.Text = "Status: Limited (No Internet) 🟡"
                ToolStripStatusLabel1.ForeColor = Color.Orange
            End If
        End If
        If ComboBox1.Items.Count > 0 AndAlso ComboBox1.SelectedItem IsNot Nothing Then
            Dim ipText As String = TextBox1.Text
            If ipText.Contains("192.") Or ipText.Contains("10.") Or ipText.Contains("172.") Then
                Dim ip As String = ipText.Replace("IP Address: ", "")
                ToolStripStatusLabel2.Text = $"IPv4: {ip}"
            Else
                ToolStripStatusLabel2.Text = "(IP not assigned)"
            End If
        Else
            ToolStripStatusLabel2.Text = "(No adapter)"
        End If
    End Sub

    Private Function IsNetworkConnected() As Boolean
        Return NetworkInterface.GetIsNetworkAvailable()
    End Function

    ' Fungsi: Cek apakah bisa akses internet (ping Google DNS)
    Private Function IsInternetAvailable() As Boolean
        Try
            Using ping As New Ping()
                Dim reply = ping.Send("8.8.8.8", 1500) ' Timeout 1.5 detik
                Return reply.Status = IPStatus.Success
            End Using
        Catch
            Return False
        End Try
    End Function

    ' Fungsi: Cek apakah gateway bisa dijangkau (opsional)
    Private Function PingGateway() As Boolean
        Dim gateway As String = ToolStripStatusLabel1.Text.Replace("Gateway: ", "").Trim()

        If String.IsNullOrEmpty(gateway) OrElse gateway = "unknown" Then
            Return False
        End If

        Try
            Using ping As New Ping()
                Dim reply = ping.Send(gateway, 1000)
                Return reply.Status = IPStatus.Success
            End Using
        Catch
            Return False
        End Try
    End Function
End Class
