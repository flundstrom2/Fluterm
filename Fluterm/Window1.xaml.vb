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

        'ProgressBar1.Visible = True
        ' Set Minimum to 1 to represent the first file being copied.
        ProgressBar1.Minimum = 1
        ' Set Maximum to the total number of files to copy.
        ProgressBar1.Maximum = 100
        ' Set the initial value of the ProgressBar.
        ProgressBar1.Value = 25
        ' Set the Step property to a value of 1 to represent each file being copied.
        ' ProgressBar1.Step = 1


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
        Debug.Print(e.Source().ToString()) 'Output är "System.Windows.Controls.ComboBox Items.Count:13"
        Debug.Print(e.AddedItems().Item(0).ToString()) 'Item(0) ska det alltid vara. Item(0) är String. Output är "COM3" eller liknande beroende på val
        Dim combobox As System.Windows.Controls.ComboBox
        combobox = CType(e.Source, System.Windows.Controls.ComboBox)
        Debug.Print(combobox.Items.Count) '13

        Debug.Print("OnSelectionChanged_ComPort finished")
    End Sub
End Class
