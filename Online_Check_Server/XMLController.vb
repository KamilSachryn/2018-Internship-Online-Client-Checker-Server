Imports System.Xml
Public Class XMLController

    Shared clientListFileLocation As String = "ClientList.xml"
    Shared settingsFileLocation As String = "Settings.xml"

    'Dim xmlReader As XmlReader '= XmlReader.Create("ClientList.xml")
    Shared ClientListXmlDoc As New XmlDocument()
    Shared SettingsXmlDoc As New XmlDocument()
    Sub New()
        If System.IO.File.Exists(clientListFileLocation) Then
            Console.WriteLine("Client List FILE EXISTS")
        Else
            Console.WriteLine("Client List FILE DOES NOT EXIST")
            ' Create XmlWriterSettings.
            Dim settings As XmlWriterSettings = New XmlWriterSettings()
            settings.Indent = True

            ' Create XmlWriter.
            Using writer As XmlWriter = XmlWriter.Create(clientListFileLocation, settings)
                ' Begin writing.
                writer.WriteStartDocument()
                writer.WriteStartElement("Connections") ' Root.


                ' End document.
                writer.WriteEndElement()
                writer.WriteEndDocument()
            End Using
            Console.WriteLine("FILE CREATED")
        End If

        If System.IO.File.Exists(settingsFileLocation) Then
            Console.WriteLine("Settings FILE EXISTS")
        Else
            Console.WriteLine("Settings FILE DOES NOT EXIST")
            ' Create XmlWriterSettings.
            Dim settings As XmlWriterSettings = New XmlWriterSettings()
            settings.Indent = True

            ' Create XmlWriter.
            Using writer As XmlWriter = XmlWriter.Create(settingsFileLocation, settings)
                ' Begin writing.
                writer.WriteStartDocument()
                writer.WriteStartElement("Settings") ' Root.


                ' End document.
                writer.WriteEndElement()
                writer.WriteEndDocument()
            End Using
            Console.WriteLine("FILE CREATED")
        End If

        ClientListXmlDoc.Load(clientListFileLocation)
        SettingsXmlDoc.Load(settingsFileLocation)



    End Sub



    Public Shared Sub AddItemToClientListXML(connection As Connection)

        Dim doc As XDocument = XDocument.Load(clientListFileLocation)

        Dim node As XElement = doc.Descendants("Connection").FirstOrDefault(Function(cd) cd.Element("IP").Value = connection.getIP())

        'If the current connection does not exist, create it, otherwise skip if
        If node Is Nothing Then
            Console.WriteLine(">>>>>>>>IS NOTHING<<<<<<<<<<<<")

            Dim currEle = New XElement("Connection")
            doc.Element("Connections").Add(currEle)
            node = currEle
        End If

        'Console.WriteLine("START SETTING OR CHANGING NEW CONNECTION ___")
        'Console.WriteLine(connection.ToString())
        'Console.WriteLine("END   SETTING OR CHANGING NEW CONNECTION ___")


        'updated current connection settings
        node.SetElementValue("IP", connection.getIP())
        node.SetElementValue("Name", connection.getName())
        node.SetElementValue("Status", connection.getStatusAsString())
        node.SetElementValue("Email", connection.getSendEmailOnCrash())
        node.SetElementValue("Time", connection.getTimeOfLastCheck())

        doc.Save(clientListFileLocation)

    End Sub

    Public Shared Function GetAllConnectionsFromClientListXML() As List(Of Connection)
        Dim cons As List(Of Connection) = New List(Of Connection)

        Try
            Dim doc As XmlDocument
            Dim nodeList As XmlNodeList
            Dim currentNode As XmlNode
            'Create the XML Document
            doc = New XmlDocument()
            'Load the Xml file
            doc.Load(clientListFileLocation)
            'Get the list of name nodes 
            nodeList = doc.SelectNodes("/Connections/Connection")
            'Loop through the nodes
            For Each currentNode In nodeList
                cons.Add(New Connection(Net.IPAddress.Parse(currentNode.ChildNodes.Item(0).InnerText),
                                            currentNode.ChildNodes.Item(1).InnerText,
                                            Connection.convertStringToConnectionStatus(currentNode.ChildNodes.Item(2).InnerText),
                                             currentNode.ChildNodes.Item(3).InnerText,
                                            DateTime.Parse(currentNode.ChildNodes.Item(4).InnerText)))
            Next
        Catch errorVariable As Exception
            'Error trapping
            Console.Write(errorVariable.ToString())
        End Try

        Return cons

    End Function

    Public Shared Function GetConnectionFromClientListXML(ip As Net.IPAddress) As Connection
        Dim cons As List(Of Connection) = New List(Of Connection)

        Try
            Dim doc As XmlDocument
            Dim nodeList As XmlNodeList
            Dim currentNode As XmlNode
            'Create the XML Document
            doc = New XmlDocument()
            'Load the Xml file
            doc.Load(clientListFileLocation)
            'Get the list of name nodes 
            nodeList = doc.SelectNodes("/Connections/Connection")
            'Loop through the nodes
            For Each currentNode In nodeList
                If Net.IPAddress.Parse(currentNode.ChildNodes.Item(0).InnerText).ToString() = ip.ToString() Then
                    Return New Connection(Net.IPAddress.Parse(currentNode.ChildNodes.Item(0).InnerText),
                                            currentNode.ChildNodes.Item(1).InnerText,
                                            Connection.convertStringToConnectionStatus(currentNode.ChildNodes.Item(2).InnerText),
                                             currentNode.ChildNodes.Item(3).InnerText,
                                            DateTime.Parse(currentNode.ChildNodes.Item(4).InnerText))
                End If
            Next
        Catch errorVariable As Exception
            'Error trapping
            Console.Write(errorVariable.ToString())
        End Try

        Return Nothing


    End Function

    Public Shared Function loadSettings() As List(Of String)
        Dim list As New List(Of String)



        Try
            Dim doc As XmlDocument
            Dim nodeList As XmlNodeList
            Dim currentNode As XmlNode
            'Create the XML Document
            doc = New XmlDocument()
            'Load the Xml file
            doc.Load(settingsFileLocation)
            'Get the list of name nodes 
            nodeList = doc.SelectNodes("/Settings/Setting")
            'Loop through the nodes
            For Each currentNode In nodeList
                list.Add(currentNode.ChildNodes(0).InnerText)
                list.Add(currentNode.ChildNodes(1).InnerText)
                list.Add(currentNode.ChildNodes(2).InnerText)

                Return list
            Next
        Catch errorVariable As Exception
            'Error trapping
            Console.Write(errorVariable.ToString())
        End Try

        If list.Count = 0 Then
            list.AddRange({"", "", ""})
        End If

        Return list

    End Function

    Public Shared Sub saveSettings(list As List(Of String))

        Dim doc As XDocument = XDocument.Load(settingsFileLocation)




        Dim node As XElement = doc.Descendants("Setting").FirstOrDefault()


        'If the current connection does not exist, create it, otherwise skip if
        If node Is Nothing Then
            Dim currEle = New XElement("Setting")
            doc.Element("Settings").Add(currEle)
            node = currEle
        End If

        'Console.WriteLine("START SETTING OR CHANGING NEW CONNECTION ___")
        'Console.WriteLine(connection.ToString())
        'Console.WriteLine("END   SETTING OR CHANGING NEW CONNECTION ___")


        'updated current connection settings
        node.SetElementValue("Email", list(0))
        node.SetElementValue("RefreshRate", list(1))
        node.SetElementValue("TimeToOffline", list(2))


        doc.Save(settingsFileLocation)

    End Sub


End Class
