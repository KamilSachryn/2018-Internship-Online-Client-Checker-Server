Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks

Public Class Form1

    Dim localPort As Integer = 20
    Dim localip As IPAddress = GetLocalIP()
    Dim listener As TcpListener

    Dim connections As New List(Of Connection)

    Dim xmlController As New XMLController


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'begin listening for connections
        StartListener()
        ' XMLController.AddItemToXML(New Connection("1.2.3.4.5", "name goes here", ConnectionStatus.Offline, False, "Time since last check goes here"))
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
        Console.WriteLine("Thread " & id & " started, waiting for connection...")

        While True
            Dim socket As Socket = listener.AcceptSocket()
            Dim ip As String = socket.RemoteEndPoint.ToString()
            ip = ip.Substring(0, ip.LastIndexOf(":"))



            Console.WriteLine("Connected " & ip & "on thread ID " & id)
            Dim currCon As New Connection(ip, "Zach test", ConnectionStatus.Online, True.ToString(), DateTime.Now)
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

            Dim currCon As New ListViewItem(con.getIP())
            currCon.SubItems.Add(con.getName())
            currCon.SubItems.Add(con.getStatusAsString())
            currCon.SubItems.Add(con.getSendEmailOnCrash())
            currCon.SubItems.Add((DateTime.Now - DateTime.Parse(con.getTimeSInceLastCheck())).TotalMinutes.ToString().Substring(0, 1) + " minutes ago")
            'Adds a item to the main listview with the IP, and string based on connection status
            listView_Connections.Items.Add(currCon)



        End If
    End Sub

    Public Delegate Sub updateViewFromXMLDeleage()
    Public Sub UpdateViewFromXML()

        If Me.InvokeRequired Then
            Dim del As updateViewFromXMLDeleage = New updateViewFromXMLDeleage(AddressOf UpdateViewFromXML)
            Me.Invoke(del)
        Else
            listView_Connections.Items.Clear()
            Dim connectionList As List(Of Connection) = XMLController.ReadXML()

            For Each connection In connectionList
                addConToViewFromXML(connection)
            Next
        End If








    End Sub

    Private Sub btn_toggleServerCheck_Click(sender As Object, e As EventArgs) Handles btn_toggleServerCheck.Click

    End Sub

    Private Sub btn_forceRefresh_Click(sender As Object, e As EventArgs) Handles btn_forceRefresh.Click

    End Sub

    Private Sub tb_lowerIPRange_TextChanged(sender As Object, e As EventArgs) Handles tb_lowerIPRange.TextChanged

    End Sub

    Private Sub tb_higherIPRange_TextChanged(sender As Object, e As EventArgs) Handles tb_higherIPRange.TextChanged

    End Sub

    Private Sub listView_Connections_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listView_Connections.SelectedIndexChanged

    End Sub

    Private Sub listView_Connections_Disposed(sender As Object, e As EventArgs) Handles listView_Connections.Disposed

    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Application.Exit()
        End
    End Sub
End Class




