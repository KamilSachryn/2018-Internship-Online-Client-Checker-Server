Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
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
    Dim timerTickTime As Integer = 3000

    Dim MINS_TO_CHECK_SERVERS = 1



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
        UpdateViewFromXML()


        'select the first item in the listbox if any exist
        If listView_Connections.Items.Count > 0 Then
            listView_Connections.TopItem.Selected = True
            previousSelectedListViewItem = listView_Connections.TopItem
        End If

        timer = New Timers.Timer(timerTickTime)
        AddHandler timer.Elapsed, New ElapsedEventHandler(AddressOf TimerTick)
        timer.Enabled = True

        isLoaded = True
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
        '    Console.WriteLine("Thread " & id & " started, waiting for connection...")

        While True
            Dim socket As Socket = listener.AcceptSocket()
            Dim ip As String = socket.RemoteEndPoint.ToString()
            ip = ip.Substring(0, ip.LastIndexOf(":"))



            Console.WriteLine("Connected " & ip & "on thread ID " & id)
            Dim currCon As New Connection(IPAddress.Parse(ip), "Unnamed", ConnectionStatus.Online, True.ToString(), DateTime.Now)
            'addConToViewAndXML(currCon)
            XMLController.AddItemToXML(currCon)
            UpdateViewFromXML()




            Try
                Dim networkStream As NetworkStream = New NetworkStream(socket)
                Dim streamReader As StreamReader = New StreamReader(networkStream)
                Dim streamWriter As StreamWriter = New StreamWriter(networkStream)
                streamWriter.AutoFlush = True
                streamWriter.WriteLine("Message from SERVER")
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
            For Each con As Connection In XMLController.GetAllConnectionsFromXML()


            Next
            UpdateViewFromXML()
            For Each item As ListViewItem In listView_Connections.Items
                Dim timeSinceCheck As String = item.SubItems(ListViewConsIDs.TimeSinceCheck).Text
                Console.WriteLine("Time since check = " + timeSinceCheck)
                timeSinceCheck = timeSinceCheck.Substring(0, timeSinceCheck.IndexOf(" "))
                Console.WriteLine("Time since check as int = " + timeSinceCheck)
                If Integer.Parse(timeSinceCheck) > MINS_TO_CHECK_SERVERS Then
                    Console.WriteLine("Time since check exceeds " + MINS_TO_CHECK_SERVERS.ToString() + " minutes")
                    SetStatusToOffline(XMLController.GetConnectionFromXML(IPAddress.Parse(item.SubItems(ListViewConsIDs.ServerIP).Text)))
                End If

                If item.SubItems(ListViewConsIDs.ServerStatus).Text = OFFLINE_TEXT _
                And item.SubItems(ListViewConsIDs.SendEmail).Text = NO_SEND_EMAIL_TEXT Then
                    MessageController.SendEmail(XMLController.GetConnectionFromXML(IPAddress.Parse(item.SubItems(ListViewConsIDs.ServerIP).Text)))

                End If
            Next
        End If

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

    'Enum for use in the GUI
    'Keeps track of what the connection state is.
    Public Enum ConnectionStatus
        Online = 0
        Offline = 1
    End Enum

    'Uses a delegate if called from a thread
    'Delegate required due to .net calling functions from new threads
    Public Delegate Sub addConToViewDelegate(con As Connection)
    'Adds a new connection to the view when any connection is made on any thread
    Sub addConToViewFromXML(con As Connection)

        'If called from a thread, send it back through the delegate
        If Me.InvokeRequired Then
            Dim del As addConToViewDelegate = New addConToViewDelegate(AddressOf addConToViewFromXML)
            Dim params() = {con}
            Me.Invoke(del, params)

        Else 'If called from the deleagte
            Console.WriteLine("Function addConToViewFromXML called")
            Dim currCon As New ListViewItem(con.getIP())
            currCon.SubItems.Add(con.getName())
            currCon.SubItems.Add(con.getStatusAsString())
            currCon.SubItems.Add(con.getSendEmailOnCrash())
            Dim time As String = (DateTime.Now - con.getTimeOfLastCheck()).TotalMinutes.ToString()
            time = time.Substring(0, time.IndexOf(".")) + " minutes ago"
            'Console.WriteLine(time)
            currCon.SubItems.Add(time)
            'Adds a item to the main listview with the IP, and string based on connection status
            listView_Connections.Items.Add(currCon)



        End If
    End Sub

    '''Public Delegate Sub updateViewFromXMLDeleage()
    '''Public Sub UpdateViewFromXML()

    '''    If Me.InvokeRequired Then
    '''        Dim del As updateViewFromXMLDeleage = New updateViewFromXMLDeleage(AddressOf UpdateViewFromXML)
    '''        Me.Invoke(del)
    '''    Else
    '''        Console.WriteLine(">>>entered UpdateView")
    '''        'Create list of connections saved in XML, limit to ip range
    '''        Dim connectionList As List(Of Connection) = XMLController.GetAllConnectionsFromXML()
    '''        connectionList.RemoveAll(AddressOf IsNotBetweenIPs)
    '''        'Create list of ListViewItems currently shown
    '''        Dim connectionsInViewList As New List(Of ListViewItem)
    '''        For Each item As ListViewItem In listView_Connections.Items
    '''            connectionsInViewList.Add(item)
    '''        Next
    '''        'connectionsInViewList.AddRange(listView_Connections.Items)

    '''        Dim connectionsInConnectionListAndViewList As New List(Of Connection)
    '''        Dim connectionsInConnectionListAndNotViewList As New List(Of Connection)

    '''        For Each item As ListViewItem In connectionsInViewList
    '''            Dim con As Connection = XMLController.GetConnectionFromXML(IPAddress.Parse(item.Text))
    '''            If connectionList.Contains(con) Then
    '''                Console.WriteLine("Added con to  in view list")
    '''                connectionsInConnectionListAndViewList.Add(con)
    '''            Else
    '''                Console.WriteLine("Added con to not in view list")
    '''                '      connectionsInConnectionListAndNotViewList.Add(con)
    '''            End If
    '''        Next

    '''        For Each con As Connection In connectionList
    '''            If Not connectionsInConnectionListAndViewList.Contains(con) Then
    '''                connectionsInConnectionListAndNotViewList.Add(con)
    '''            End If
    '''        Next



    '''        For Each item As ListViewItem In connectionsInViewList
    '''            Console.WriteLine("Ding dong")
    '''            Dim itemAsCon As Connection = XMLController.GetConnectionFromXML(IPAddress.Parse(item.Text))
    '''            For Each con As Connection In connectionsInConnectionListAndViewList

    '''                If con.Equals(itemAsCon) Then
    '''                    Console.WriteLine("ping pong")
    '''                    Dim newItem As New ListViewItem(con.getIP())
    '''                    newItem.SubItems.Add(con.getName())
    '''                    newItem.SubItems.Add(ONLINE_TEXT)
    '''                    newItem.SubItems.Add(con.getSendEmailOnCrash())
    '''                    Dim time As String = (DateTime.Now - con.getTimeOfLastCheck()).TotalMinutes.ToString()
    '''                    time = time.Substring(0, time.IndexOf(".")) + " minutes ago"
    '''                    'Console.WriteLine(time)
    '''                    newItem.SubItems.Add(time)
    '''                    'Adds a item to the main listview with the IP, and string based on connection status
    '''                    'listView_Connections.Items.Add(newItem)
    '''                    item = newItem

    '''                End If
    '''            Next
    '''        Next

    '''        For Each con As Connection In connectionsInConnectionListAndNotViewList
    '''            Dim newItem As New ListViewItem(con.getIP())
    '''            newItem.SubItems.Add(con.getName())
    '''            newItem.SubItems.Add(con.getStatusAsString())
    '''            newItem.SubItems.Add(con.getSendEmailOnCrash())
    '''            Dim time As String = (DateTime.Now - con.getTimeOfLastCheck()).TotalMinutes.ToString()
    '''            time = time.Substring(0, time.IndexOf(".")) + " minutes ago"
    '''            'Console.WriteLine(time)
    '''            newItem.SubItems.Add(time)
    '''            'Adds a item to the main listview with the IP, and string based on connection status
    '''            listView_Connections.Items.Add(newItem)
    '''        Next

    '''    End If

    '''    For Each item As ListViewItem In listView_Connections.Items
    '''        item.UseItemStyleForSubItems = False
    '''        If item.SubItems(ListViewConsIDs.ServerStatus).Text = ONLINE_TEXT Then
    '''            item.SubItems(ListViewConsIDs.ServerStatus).BackColor = ONLINE_COLOR
    '''        Else
    '''            item.SubItems(ListViewConsIDs.ServerStatus).BackColor = OFFLINE_COLOR
    '''        End If
    '''    Next
    '''End Sub

    '''Public Function IsNotBetweenIPs(con As Connection) As Boolean
    '''    If (IpToIntRep(con.ip) > IpToIntRep(ipListLower)) And (IpToIntRep(con.ip) < IpToIntRep(ipListHigher)) Then
    '''        Return False
    '''    Else
    '''        Return True
    '''    End If
    '''End Function

    Public Delegate Sub updateViewFromXMLDeleage()
    Public Sub UpdateViewFromXML()

        If Me.InvokeRequired Then
            Dim del As updateViewFromXMLDeleage = New updateViewFromXMLDeleage(AddressOf UpdateViewFromXML)
            Me.Invoke(del)
        Else
            listView_Connections.Items.Clear()
            Dim connectionList As List(Of Connection) = XMLController.GetAllConnectionsFromXML()


            For Each connection In connectionList

                If (IpToIntRep(connection.ip) > IpToIntRep(ipListLower)) And (IpToIntRep(connection.ip) < IpToIntRep(ipListHigher)) Then
                    addConToViewFromXML(connection)
                End If


            Next

            'set connection state color for each con
            For Each item As ListViewItem In listView_Connections.Items
                item.UseItemStyleForSubItems = False
                If item.SubItems(ListViewConsIDs.ServerStatus).Text = ONLINE_TEXT Then
                    item.SubItems(ListViewConsIDs.ServerStatus).BackColor = ONLINE_COLOR
                Else
                    item.SubItems(ListViewConsIDs.ServerStatus).BackColor = OFFLINE_COLOR
                End If
            Next
        End If
    End Sub

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

        Dim currCon As Connection = XMLController.GetConnectionFromXML(IPAddress.Parse(listView_Connections.Items(0).Text))
        currCon.flipEmailStatus()
        XMLController.AddItemToXML(currCon)

        UpdateViewFromXML()
    End Sub

    Private Sub btn_forceRefresh_Click(sender As Object, e As EventArgs) Handles btn_forceRefresh.Click

        UpdateViewFromXML()

        For Each con As Connection In XMLController.GetAllConnectionsFromXML()
            Dim messageSuccesful = con.ForceSendMessage()
        Next

    End Sub

    Private Sub tb_lowerIPRange_TextChanged(sender As Object, e As EventArgs) Handles tb_lowerIPRange.TextChanged, tb_higherIPRange.TextChanged

        Dim regexString = "[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
        ' if lower and higher ip matches regex
        '  If RegularExpressions.Regex.IsMatch(tb_lowerIPRange.Text, regexString) Then
        If IPAddress.TryParse(tb_lowerIPRange.Text, ipListLower) Then


        Else
            ipListLower = IPAddress.Parse("0.0.0.0")
        End If

        If IPAddress.TryParse(tb_higherIPRange.Text, ipListHigher) Then


        Else
            ipListHigher = IPAddress.Parse("255.255.255.255")
        End If


        UpdateViewFromXML()


    End Sub


    Private Sub listView_Connections_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listView_Connections.SelectedIndexChanged


        'enable and disable the buttons if a listviewitem is selected
        If listView_Connections.SelectedItems.Count <> 0 Then
            previousSelectedListViewItem = listView_Connections.SelectedItems(0)
            btn_forceRefresh.Enabled = True
            btn_toggleServerCheck.Enabled = True
            tb_ServerName.Enabled = True
            tb_ServerName.Text = listView_Connections.Items(0).SubItems(ListViewConsIDs.ServerName).Text
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

            Dim currCon As Connection = XMLController.GetConnectionFromXML(IPAddress.Parse(previousSelectedListViewItem.Text))
            currCon.setName(tb_ServerName.Text)
            XMLController.AddItemToXML(currCon)

            'added due to crash relating to executing this code before loading the GUI
            If isLoaded Then
                UpdateViewFromXML()
            End If
        End If


    End Sub




    Private Sub SetStatusToOffline(con As Connection)

        con.SetOffline()
        XMLController.AddItemToXML(con)

        'added due to crash relating to executing this code before loading the GUI
        If isLoaded Then
            UpdateViewFromXML()
        End If
    End Sub

    Private Sub UpdateServerCheckTimers()
        For Each item As ListViewItem In listView_Connections.Items

        Next
        UpdateViewFromXML()

    End Sub
End Class




