Imports System.Xml
Public Class XMLController
    Shared fileLocation As String = "ClientList.xml"
    'Dim xmlReader As XmlReader '= XmlReader.Create("ClientList.xml")
    Shared xmlDoc As New XmlDocument()
    Sub New()
        If System.IO.File.Exists(fileLocation) Then
            Console.WriteLine("FILE EXISTS")
        Else
            Console.WriteLine("FILE DOES NOT EXIST")
            ' Create XmlWriterSettings.
            Dim settings As XmlWriterSettings = New XmlWriterSettings()
            settings.Indent = True

            ' Create XmlWriter.
            Using writer As XmlWriter = XmlWriter.Create(fileLocation, settings)
                ' Begin writing.
                writer.WriteStartDocument()
                writer.WriteStartElement("Connections") ' Root.


                ' End document.
                writer.WriteEndElement()
                writer.WriteEndDocument()
            End Using
            Console.WriteLine("FILE CREATED")
        End If

        xmlDoc.Load(fileLocation)


    End Sub



    Public Shared Sub AddItemToXML(connection As Connection)

        Dim doc As XDocument = XDocument.Load(fileLocation)

        Dim node As XElement = doc.Descendants("Connection").FirstOrDefault(Function(cd) cd.Element("IP").Value = connection.getIP())

        'If the current connection does not exist, create it, otherwise skip if
        If node Is Nothing Then
            Console.WriteLine(">>>>>>>>IS NOTHING<<<<<<<<<<<<")

            Dim currEle = New XElement("Connection")
            doc.Element("Connections").Add(currEle)
            node = currEle
        End If


        'updated current connection settings
        node.SetElementValue("IP", connection.getIP())
        node.SetElementValue("Name", connection.getName())
        node.SetElementValue("Status", connection.getStatusAsString())
        node.SetElementValue("Email", connection.getSendEmailOnCrash())
        node.SetElementValue("Time", connection.getTimeSInceLastCheck())

        doc.Save(fileLocation)

    End Sub

    Public Shared Function ReadXML() As List(Of Connection)
        Dim cons As List(Of Connection) = New List(Of Connection)

        Try
            Dim doc As XmlDocument
            Dim nodeList As XmlNodeList
            Dim currentNode As XmlNode
            'Create the XML Document
            doc = New XmlDocument()
            'Load the Xml file
            doc.Load(fileLocation)
            'Get the list of name nodes 
            nodeList = doc.SelectNodes("/Connections/Connection")
            'Loop through the nodes
            For Each currentNode In nodeList
                cons.Add(New Connection(currentNode.ChildNodes.Item(0).InnerText,
                                            currentNode.ChildNodes.Item(1).InnerText,
                                            currentNode.ChildNodes.Item(2).InnerText,
                                             currentNode.ChildNodes.Item(3).InnerText,
                                            DateTime.Parse(currentNode.ChildNodes.Item(4).InnerText)))
            Next
        Catch errorVariable As Exception
            'Error trapping
            Console.Write(errorVariable.ToString())
        End Try

        Return cons

    End Function


End Class
