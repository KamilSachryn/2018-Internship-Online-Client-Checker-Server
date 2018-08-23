Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Timers

Public Class Form1


    Dim localPort As Integer = 20
    Dim localip As IPAddress = GetLocalIP()
    Dim listener As TcpListener

    Dim connections As New List(Of Connection)

    Dim xmlController As New XMLController

    Dim ipListLower As IPAddress = IPAddress.Parse("0.0.0.0")
    Dim ipListHigher As IPAddress = IPAddress.Parse("255.255.255.255")

    Dim isLoaded As Boolean = False

    Dim ONLINE_COLOR As Color = Color.Green
    Dim OFFLINE_COLOR As Color = Color.Red
    Dim ONLINE_TEXT As String = "Online"
    Dim OFFLINE_TEXT As String = "Offline"

    Dim SEND_EMAIL_TEXT As String = "True"
    Dim NO_SEND_EMAIL_TEXT As String = "False"

    Dim previousSelectedListViewItem As ListViewItem = Nothing

    Dim timer As Timers.Timer
    Dim timerTickTime As Integer = 10000

    Dim MINS_TO_CHECK_SERVERS = 1

    Dim email As String = ""

    Dim messageController As New MessageController(email)

    Dim form_Settings As New Settings(Me)
    Dim form_SendClient As New TransferClient(Me)



    Enum ListViewConsIDs
        ServerIP = 0
        ServerName = 1
        ServerStatus = 2
        SendEmail = 3
        TimeSinceCheck = 4
    End Enum

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'begin listening for connections
        StartListener()
        ' XMLController.AddItemToXML(New Connection("1.2.3.4.5", "name goes here", ConnectionStatus.Offline, False, "Time since last check goes here"))

        UpdateSettingsValues()
        UpdateViewFromXML()


        'select the first item in the listbox if any exist
        If listView_Connections.Items.Count > 0 Then
            listView_Connections.TopItem.Selected = True
            previousSelectedListViewItem = listView_Connections.TopItem
        End If

        'time for automatic refresh and time since last online check
        timer = New Timers.Timer(timerTickTime)
        AddHandler timer.Elapsed, New ElapsedEventHandler(AddressOf TimerTick)
        timer.Enabled = True

        'Make sure to avoid exceptions relating to executing functions before the GUI is loaded
        isLoaded = True



        UpdateViewFromXML()
    End Sub


    'Creates 255 threads to listen for incomming pings
    'Allows for 255 constant connections
    Public Sub StartListener()
        Console.Write("Trying to create listener on ip " & localip.ToString() & " , port " & localPort & " ...")
        listener = New TcpListener(localip, localPort)
        listener.Start()
        Console.WriteLine("Listener created successfully")
        Console.WriteLine()
        Console.WriteLine("LISTENING ON IP: " & localip.ToString())
        Console.WriteLine()
        Console.Write("Trying to create thread(s)...")

        'using multiple threads for multiple connections at once
        Dim Threads(255) As Thread
        For i As Integer = 0 To Threads.Length
            Dim threadID = i
            Dim thread = New Thread(Sub() Service(threadID))
            thread.Start()
        Next
        Console.WriteLine("Threads Started")

    End Sub

    'Ran on each thread, listens for an incomming connection
    Public Sub Service(id As Integer)

        'Loop infinitely to constantly scan for incomming packets
        While True
            Dim socket As Socket = listener.AcceptSocket()
            Dim ip As String = socket.RemoteEndPoint.ToString()
            ip = ip.Substring(0, ip.LastIndexOf(":"))



            Console.WriteLine("Connected " & ip & "on thread ID " & id)
            Dim currCon As New Connection(IPAddress.Parse(ip), "Unnamed", Connection.ConnectionStatus.Online, True.ToString(), DateTime.Now)
            'addConToViewAndXML(currCon)
            XMLController.AddItemToClientListXML(currCon)
            UpdateViewFromXML()




            Try
                Dim networkStream As NetworkStream = New NetworkStream(socket)
                Dim streamWriter As StreamWriter = New StreamWriter(networkStream)
                streamWriter.AutoFlush = True
                streamWriter.WriteLine("Message from SERVER")

                Dim streamReader As StreamReader = New StreamReader(networkStream)
                Console.WriteLine("Message from connected client: " & streamReader.ReadLine())


                networkStream.Close()
            Catch e As Exception
                Console.WriteLine("EXCEPTION: " & e.Message)
            End Try

            socket.Close()
        End While
    End Sub



    Public Delegate Sub TimerTickDeleage(ByVal sender As Object, ByVal e As ElapsedEventArgs)

    'Runs every timerTrickTime(3000?) seconds
    Public Sub TimerTick(ByVal sender As Object, ByVal e As ElapsedEventArgs)


        If Me.InvokeRequired Then
            Dim del As TimerTickDeleage = New TimerTickDeleage(AddressOf TimerTick)
            Dim params() = {sender, e}
            Me.Invoke(del, params)

        Else
            Console.WriteLine("Tick")

            UpdateViewFromXML()


        End If

    End Sub

    Private Sub CheckServerForOffline(item As ListViewItem)
        ' For Each item As ListViewItem In listView_Connections.Items
        Dim timeSinceCheck As String = item.SubItems(ListViewConsIDs.TimeSinceCheck).Text
        Dim con As Connection = XMLController.GetConnectionFromClientListXML(IPAddress.Parse(item.SubItems(ListViewConsIDs.ServerIP).Text))
        Console.WriteLine("Time since check = " + timeSinceCheck)
        timeSinceCheck = timeSinceCheck.Substring(0, timeSinceCheck.IndexOf(" "))
        Console.WriteLine("Time since check as int = " + timeSinceCheck)
        If Integer.Parse(timeSinceCheck) > MINS_TO_CHECK_SERVERS Then
            Console.WriteLine("Time since check exceeds " + MINS_TO_CHECK_SERVERS.ToString() + " minutes")
            con.SetOffline()
            XMLController.AddItemToClientListXML(con)
        Else
            con.SetOnline()
            XMLController.AddItemToClientListXML(con)
        End If

        If item.SubItems(ListViewConsIDs.ServerStatus).Text = OFFLINE_TEXT _
        And item.SubItems(ListViewConsIDs.SendEmail).Text = SEND_EMAIL_TEXT Then
            con.sendEmailOnCrash = False
            item.SubItems(ListViewConsIDs.SendEmail).Text = NO_SEND_EMAIL_TEXT
            XMLController.AddItemToClientListXML(con)



            messageController.SendEmail(XMLController.GetConnectionFromClientListXML(IPAddress.Parse(item.SubItems(ListViewConsIDs.ServerIP).Text)))
            MsgBox("Server at IP " + con.getIP() + " is down")
        End If
        '  Next
    End Sub

    'returns Local ip for use in creating TCPListener
    Public Function GetLocalIP() As IPAddress
        Dim host = Dns.GetHostEntry(Dns.GetHostName())

        For Each ip In host.AddressList

            If ip.AddressFamily = AddressFamily.InterNetwork Then
                Return ip
            End If
        Next

        Throw New Exception("No network adapters with an IPv4 address in the system!")
    End Function


    Public Delegate Sub updateViewFromXMLDeleage()
    Public Sub UpdateViewFromXML()

        If Me.InvokeRequired Then
            Dim del As updateViewFromXMLDeleage = New updateViewFromXMLDeleage(AddressOf UpdateViewFromXML)
            Me.Invoke(del)
        Else

            'listView_Connections.Items.Clear()
            Dim connectionListFromFile As List(Of Connection) = XMLController.GetAllConnectionsFromClientListXML()


            'Loop through all connection in XML
            For Each connection In connectionListFromFile

                'Limits selected con to be within the currently selected IP range
                If Not IsNotBetweenLimitIPs(connection) Then

                    Dim foundItem As ListViewItem = Nothing
                    'Check if connection is already in the view
                    For Each item As ListViewItem In listView_Connections.Items
                        If connection.getIP() = item.SubItems(0).Text Then
                            Console.WriteLine("Con found, editing view to update")
                            foundItem = item
                        End If
                    Next


                    If foundItem Is Nothing Then
                        'if item is new, add it


                        Console.WriteLine("Function addConToViewFromXML called")

                        'Create list view item with IP as the first item, add subsequent items
                        foundItem = New ListViewItem(connection.getIP())
                        foundItem.SubItems.Add(connection.getName())
                        foundItem.SubItems.Add(connection.getStatusAsString())
                        foundItem.SubItems.Add(connection.getSendEmailOnCrash())
                        foundItem.SubItems.Add(getTimeDifferenceInConnection(connection))





                        'Adds a item to the main listview with the IP, and string based on connection status
                        listView_Connections.Items.Add(foundItem)

                    Else
                        'if item is already in the view, update it
                        'Check each item first incase its the same, limits flickering
                        If foundItem.SubItems(0).Text IsNot connection.getIP() Then
                            foundItem.SubItems(0).Text = connection.getIP()
                        End If

                        If foundItem.SubItems(1).Text IsNot connection.getName() Then
                            foundItem.SubItems(1).Text = connection.getName()
                        End If

                        If foundItem.SubItems(2).Text IsNot connection.getStatusAsString() Then
                            foundItem.SubItems(2).Text = connection.getStatusAsString()
                        End If

                        If foundItem.SubItems(3).Text IsNot connection.getSendEmailOnCrash() Then
                            foundItem.SubItems(3).Text = connection.getSendEmailOnCrash()
                        End If

                        If foundItem.SubItems(4).Text IsNot getTimeDifferenceInConnection(connection) Then
                            foundItem.SubItems(4).Text = getTimeDifferenceInConnection(connection)
                        End If

                    End If

                    CheckServerForOffline(foundItem)

                    'Make sure the Online Status is the correct color
                    foundItem.UseItemStyleForSubItems = False
                    If foundItem.SubItems(ListViewConsIDs.ServerStatus).Text = ONLINE_TEXT Then
                        foundItem.SubItems(ListViewConsIDs.ServerStatus).BackColor = ONLINE_COLOR
                    Else
                        foundItem.SubItems(ListViewConsIDs.ServerStatus).BackColor = OFFLINE_COLOR
                    End If

                Else
                    'make sure item is not in the view if it is not needed
                    'if found, remove it
                    Dim foundItem As ListViewItem = Nothing
                    'Check if connection is already in the view
                    For Each item As ListViewItem In listView_Connections.Items
                        If connection.getIP() = item.SubItems(0).Text Then
                            item.Remove()
                        End If
                    Next
                End If



            Next


        End If


    End Sub

    Public Function getTimeDifferenceInConnection(input As Connection) As String
        Dim time As String = (DateTime.Now - input.getTimeOfLastCheck()).TotalMinutes.ToString()
        time = time.Substring(0, time.IndexOf(".")) + " minutes ago"
        Return time
    End Function

    Public Function IsNotBetweenLimitIPs(con As Connection) As Boolean
        If (IpToIntRep(con.ip) > IpToIntRep(ipListLower)) And (IpToIntRep(con.ip) < IpToIntRep(ipListHigher)) Then
            Return False
        Else
            Return True
        End If
    End Function

    'Creates an integer representation of an IP address for use in comparing them
    Public Function IpToIntRep(ip As IPAddress) As Int64
        Dim val As Int64 = 0
        Dim ipStr As String = ip.ToString()
        Dim subIpList As New List(Of Integer)

        While ipStr.IndexOf(".") <> -1
            subIpList.Add(ipStr.Substring(0, ipStr.IndexOf(".")))
            ipStr = ipStr.Substring(ipStr.IndexOf(".") + 1, ipStr.Length - 1 - ipStr.IndexOf("."))
        End While
        subIpList.Add(ipStr)

        For i As Integer = 0 To 3
            val += subIpList(i) * (Math.Pow(255, 3 - i))
        Next

        Return val



    End Function

    Private Sub btn_toggleServerCheck_Click(sender As Object, e As EventArgs) Handles btn_toggleServerCheck.Click

        ' Console.WriteLine("toggling ip: " + listView_Connections.Items(0).ToString())

        Dim currCon As Connection = XMLController.GetConnectionFromClientListXML(IPAddress.Parse(listView_Connections.SelectedItems(0).Text))
        currCon.flipEmailStatus()
        XMLController.AddItemToClientListXML(currCon)

        UpdateViewFromXML()
    End Sub

    Private Sub btn_forceRefresh_Click(sender As Object, e As EventArgs) Handles btn_forceRefresh.Click

        UpdateViewFromXML()

        Dim conList As List(Of Connection) = XMLController.GetAllConnectionsFromClientListXML()

        Dim Threads(conList.Count) As Thread
        For Each con As Connection In conList
            'Dim thread = New Thread(Sub() Service(threadID))
            Dim thread = New Thread(Sub() con.ForceRefresh())
            thread.Start()
        Next


    End Sub

    Private Sub tb_lowerIPRange_TextChanged(sender As Object, e As EventArgs) Handles tb_lowerIPRange.TextChanged, tb_higherIPRange.TextChanged


        Dim item As TextBox = sender
        Dim startingIpListLower = ipListLower
        Dim startingIpListHigher = ipListHigher
        Dim regexString = "^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"
        ' if lower and higher ip matches regex
        If Regex.IsMatch(item.Text, regexString) Then
            error_ip.SetError(item, "")

            '  If RegularExpressions.Regex.IsMatch(tb_lowerIPRange.Text, regexString) Then
            If IPAddress.TryParse(tb_lowerIPRange.Text, ipListLower) Then
                'If ip parsing worked for the LOWER range

            Else
                ipListLower = IPAddress.Parse("0.0.0.0")
            End If

            If IPAddress.TryParse(tb_higherIPRange.Text, ipListHigher) Then
                'if ip parsing worked for the HIGHER range

            Else
                ipListHigher = IPAddress.Parse("255.255.255.255")
            End If

            If startingIpListHigher.Equals(ipListHigher) And startingIpListLower.Equals(ipListLower) Then
                'if they match, dont do anything
            Else
                'if one of the addresses has changed, update view
                UpdateViewFromXML()
            End If

        Else
            error_ip.SetError(item, "Invalid IPv4 format")

        End If




    End Sub


    Private Sub listView_Connections_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listView_Connections.SelectedIndexChanged

        'enable and disable the buttons if a listviewitem is selected
        If listView_Connections.SelectedItems.Count <> 0 Then
            previousSelectedListViewItem = listView_Connections.SelectedItems(0)
            btn_forceRefresh.Enabled = True
            btn_toggleServerCheck.Enabled = True
            tb_ServerName.Enabled = True
            tb_ServerName.Text = listView_Connections.SelectedItems(0).SubItems(ListViewConsIDs.ServerName).Text
        Else
            btn_forceRefresh.Enabled = False
            btn_toggleServerCheck.Enabled = False
            tb_ServerName.Enabled = False
        End If

        'change server name if clicked and selected

    End Sub


    Private Sub listView_Connections_Disposed(sender As Object, e As EventArgs) Handles listView_Connections.Disposed

    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Application.Exit()
        End
    End Sub

    Private Sub tb_ServerName_TextChanged(sender As Object, e As EventArgs) Handles tb_ServerName.TextChanged

        ' Console.WriteLine("toggling ip: " + listView_Connections.Items(0).ToString())
        If previousSelectedListViewItem IsNot Nothing Then
            previousSelectedListViewItem.Selected = True

            Dim currCon As Connection = XMLController.GetConnectionFromClientListXML(IPAddress.Parse(previousSelectedListViewItem.Text))
            currCon.setName(tb_ServerName.Text)
            XMLController.AddItemToClientListXML(currCon)

            'added due to crash relating to executing this code before loading the GUI
            If isLoaded Then
                UpdateViewFromXML()
            End If
        End If
        previousSelectedListViewItem.Selected = True

    End Sub




    Private Sub SetStatusToOffline(con As Connection)

        con.SetOffline()
        XMLController.AddItemToClientListXML(con)

        'added due to crash relating to executing this code before loading the GUI
        If isLoaded Then
            UpdateViewFromXML()
        End If
    End Sub

    'on click Settings
    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        If form_Settings.Visible Then
            form_Settings.Hide()
        Else
            form_Settings.Show()
        End If
    End Sub

    Public Sub UpdateSettingsValues()
        timerTickTime = form_Settings.RefreshRate
        MINS_TO_CHECK_SERVERS = form_Settings.TimeToOffline
        email = form_Settings.Email


        If timer IsNot Nothing Then
            timer.Interval = timerTickTime
        End If

        messageController.SetEmail(email)

        Console.WriteLine("settings updated")
    End Sub

    Private Sub TransferClientToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferClientToolStripMenuItem.Click

        If form_SendClient.Visible Then
            form_SendClient.Hide()
        Else
            form_SendClient.Show()
        End If
    End Sub

End Class




