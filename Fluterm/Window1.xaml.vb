Class Window1 

    Private Sub OnClosed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Debug.Print("OnClosed")
    End Sub

    Private Sub OnClosing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Debug.Print("OnClosing")
    End Sub

    ' When clicked on
    Private Sub OnActivated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Debug.Print("OnActivated")
        ComboBox_ComPort.Items().Add(String.Format("Xyzzy {0}", ComboBox_ComPort.Items().Count()))
    End Sub

    ' When clicked outside
    Private Sub OnDeactivated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Deactivated
        Debug.Print("OnDeactivated")
    End Sub

    ' When opening (?)
    Private Sub OnInitialized(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Initialized
        Debug.Print("OnInitialized")
        ComboBox_ComPort.Items().Add("OnInitialized") ' Adderas efter att ComboBoxed populerats.
        GetSerialPortNames()
    End Sub

    ' On init: Between OnInitialized and OnActivated
    Private Sub OnSizeChanged(ByVal sender As System.Object, ByVal e As System.Windows.SizeChangedEventArgs) Handles MyBase.SizeChanged
        Debug.Print("OnSizeChanged")
    End Sub

    Private Sub OnMenuItem_Help_About_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MenuItem_Help_About.Click
        Debug.Print("OnMenuItem_Help_About_Click")
        Dim box = New AboutBox1
        box.ShowDialog()
        Debug.Print("OnMenuItem_Help_About_Click finished")
    End Sub

    Private Sub OnMenuItem_File_Exit_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MenuItem_File_Exit.Click
        Debug.Print("OnMenuItem_File_Exit_Click")
    End Sub

    Function ReceiveSerialData() As String
        ' Receive strings from a serial port.
        Debug.Print("ReceiveSerialData")
        Dim returnStr As String = ""

        Dim com1 As IO.Ports.SerialPort = Nothing
        Try
            com1 = My.Computer.Ports.OpenSerialPort("COM1")
            com1.ReadTimeout = 10000
            Do
                Dim Incoming As String = com1.ReadLine()
                If Incoming Is Nothing Then
                    Exit Do
                Else
                    returnStr &= Incoming & vbCrLf
                End If
            Loop
        Catch ex As TimeoutException
            returnStr = "Error: Serial Port read timed out."
        Finally
            If com1 IsNot Nothing Then com1.Close()
        End Try

        Return returnStr
    End Function

    Sub GetSerialPortNames()
        ' Show all available COM ports.
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox_ComPort.Items.Add(sp)
        Next
    End Sub

    Private Sub OnSelectionChanged_ComPort(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles ComboBox_ComPort.SelectionChanged
        Debug.Print("OnSelectionChanged_ComPort")
        Debug.Print(e.Source().ToString()) 'System.Windows.Controls.ComboBox Items.Count:2
        Debug.Print(e.AddedItems().Item(0).ToString()) 'Item(0) ska det alltid vara. Output är "System.Windows.Controls.ComboBoxItem: Rad 2" eller liknande beroende på val
        Dim comboboxItem As ComboBoxItem
        comboboxItem = CType(e.AddedItems().Item(0), ComboBoxItem)
        Debug.Print(comboboxItem.Content) 'Output är "Rad 2" beroende på val.

        Debug.Print("OnSelectionChanged_ComPort finished")
    End Sub
End Class
