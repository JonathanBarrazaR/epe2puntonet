Module Module1
    Class Cliente
        Public rut As String
        Public nombre As String
        Public telefono As String
    End Class

    Class Mesa
        Public id As Integer
        Public estado As String
    End Class

    Class Reserva
        Public id As Integer
        Public cliente As Cliente
        Public cantidadPersonas As Integer
        Public mesa As Mesa
        Public cancelada As Boolean
        Public motivoCancelacion As String
    End Class

    Sub Main()
        Dim reservas As New List(Of Reserva)
        Dim mesas(3, 4) As Mesa

        For i As Integer = 0 To 3
            For j As Integer = 0 To 4
                mesas(i, j) = New Mesa
                mesas(i, j).id = i * 5 + j + 1
                mesas(i, j).estado = "Libre"
            Next
        Next

        While True
            Console.WriteLine("1. Realizar reserva")
            Console.WriteLine("2. Buscar reserva")
            Console.WriteLine("3. Cancelar reserva")
            Console.WriteLine("4. Mostrar lista de reservas")
            Console.WriteLine("5. Mostrar mapa de mesas")
            Console.WriteLine("6. Reporte de reservas canceladas")
            Console.WriteLine("7. Reporte de mesas más solicitadas")
            Console.WriteLine("8. Reporte de clientes")
            Console.WriteLine("9. Salir")

            Dim opcion As Integer = Convert.ToInt32(Console.ReadLine())

            Select Case opcion
                Case 1
                    realizarReserva(reservas, mesas)
                Case 2
                    buscarReserva(reservas)
                Case 3
                    cancelarReserva(reservas, mesas)
                Case 4
                    mostrarListaDeReservas(reservas)
                Case 5
                    mostrarMapaDeMesas(mesas)
                Case 6
                    reporteDeReservasCanceladas(reservas)
                Case 7
                    reporteDeMesasMasSolicitadas(reservas, mesas)
                Case 8
                    reporteDeClientes(reservas)
                Case 9
                    Exit While
                Case Else
                    Console.WriteLine("Opción inválida")
            End Select
        End While
    End Sub

    Sub realizarReserva(reservas As List(Of Reserva), mesas As Mesa(,))
        Console.Write("Ingrese el RUT del cliente: ")
        Dim rut As String = Console.ReadLine()
        Console.Write("Ingrese el nombre del cliente: ")
        Dim nombre As String = Console.ReadLine()
        Console.Write("Ingrese el número de teléfono del cliente: ")
        Dim telefono As String = Console.ReadLine()
        Console.Write("Ingrese la cantidad de personas: ")
        Dim cantidadPersonas As Integer = Convert.ToInt32(Console.ReadLine())

        Dim cliente As New Cliente
        cliente.rut = rut
        cliente.nombre = nombre
        cliente.telefono = telefono

        Dim reserva As New Reserva
        reserva.cliente = cliente
        reserva.cantidadPersonas = cantidadPersonas

        Dim mesaEncontrada As Boolean = False
        For i As Integer = 0 To 3
            For j As Integer = 0 To 4
                If mesas(i, j).estado = "Libre" Then
                    If mesas(i, j).id <= 10 And cantidadPersonas <= 4 Then
                        mesas(i, j).estado = "Ocupada"
                        reserva.mesa = mesas(i, j)
                        mesaEncontrada = True
                        Exit For
                    ElseIf mesas(i, j).id >= 11 And mesas(i, j).id <= 17 And cantidadPersonas >= 5 And cantidadPersonas <= 8 Then
                        mesas(i, j).estado = "Ocupada"
                        reserva.mesa = mesas(i, j)
                        mesaEncontrada = True
                        Exit For
                    ElseIf mesas(i, j).id >= 18 And mesas(i, j).id <= 20 And cantidadPersonas >= 9 And cantidadPersonas <= 12 Then
                        mesas(i, j).estado = "Ocupada"
                        reserva.mesa = mesas(i, j)
                        mesaEncontrada = True
                        Exit For
                    End If
                End If
            Next
            If mesaEncontrada Then
                Exit For
            End If
        Next

        If Not mesaEncontrada Then
            Console.WriteLine("No hay mesas disponibles")
        Else
            reservas.Add(reserva)
        End If
    End Sub

    Sub buscarReserva(reservas As List(Of Reserva))
        Console.Write("Ingrese el RUT del cliente: ")
        Dim rut As String = Console.ReadLine()

        For Each reserva As Reserva In reservas
            If reserva.cliente.rut = rut Then
                Console.WriteLine("Reserva encontrada:")
                Console.WriteLine("Nombre del cliente: " & reserva.cliente.nombre)
                Console.WriteLine("Número de teléfono del cliente: " & reserva.cliente.telefono)
                Console.WriteLine("Cantidad de personas: " & reserva.cantidadPersonas)
                Console.WriteLine("Mesa asignada: " & reserva.mesa.id)
                Exit Sub
            End If
        Next

        Console.WriteLine("Reserva no encontrada")
    End Sub

    Sub cancelarReserva(reservas As List(Of Reserva), mesas As Mesa(,))
        Console.Write("Ingrese el RUT del cliente: ")
        Dim rut As String = Console.ReadLine()

        For Each reserva As Reserva In reservas
            If reserva.cliente.rut = rut Then
                Console.Write("Ingrese el motivo de cancelación: ")
                Dim motivoCancelacion As String = Console.ReadLine()

                reserva.cancelada = True
                reserva.motivoCancelacion = motivoCancelacion
                reserva.mesa.estado = "Libre"

                Console.WriteLine("Reserva cancelada")
                Exit Sub
            End If
        Next

        Console.WriteLine("Reserva no encontrada")
    End Sub

    Sub mostrarListaDeReservas(reservas As List(Of Reserva))
        For Each reserva As Reserva In reservas
            Console.WriteLine("Reserva:")
            Console.WriteLine("Nombre del cliente: " & reserva.cliente.nombre)
            Console.WriteLine("Número de teléfono del cliente: " & reserva.cliente.telefono)
            Console.WriteLine("Cantidad de personas: " & reserva.cantidadPersonas)
            Console.WriteLine("Mesa asignada: " & reserva.mesa.id)
            Console.WriteLine("Cancelada: " & reserva.cancelada)
            Console.WriteLine("Motivo de cancelación: " & reserva.motivoCancelacion)
            Console.WriteLine()
        Next
    End Sub

    Sub mostrarMapaDeMesas(mesas As Mesa(,))
        For i As Integer = 0 To 3
            For j As Integer = 0 To 4
                Console.Write(mesas(i, j).id & " - " & mesas(i, j).estado & " | ")
            Next
            Console.WriteLine()
        Next
    End Sub

    Sub reporteDeReservasCanceladas(reservas As List(Of Reserva))
        For Each reserva As Reserva In reservas
            If reserva.cancelada Then
                Console.WriteLine("Reserva cancelada:")
                Console.WriteLine("Nombre del cliente: " & reserva.cliente.nombre)
                Console.WriteLine("Número de teléfono del cliente: " & reserva.cliente.telefono)
                Console.WriteLine("Cantidad de personas: " & reserva.cantidadPersonas)
                Console.WriteLine("Mesa asignada: " & reserva.mesa.id)
                Console.WriteLine("Motivo de cancelación: " & reserva.motivoCancelacion)
                Console.WriteLine()
            End If
        Next
    End Sub

    Sub reporteDeMesasMasSolicitadas(reservas As List(Of Reserva), mesas As Mesa(,))
        Dim mesaMasSolicitada As Mesa = Nothing
        Dim cantidadReservasMesaMasSolicitada As Integer = 0

        For Each mesa As Mesa In mesas
            Dim cantidadReservasMesa As Integer = 0
            For Each reserva As Reserva In reservas
                If reserva.mesa.id = mesa.id Then
                    cantidadReservasMesa += 1
                End If
            Next

            If cantidadReservasMesa > cantidadReservasMesaMasSolicitada Then
                cantidadReservasMesaMasSolicitada = cantidadReservasMesa
                mesaMasSolicitada = mesa
            End If
        Next

        Console.WriteLine("Mesa más solicitada:")
        Console.WriteLine("Mesa: " & mesaMasSolicitada.id)
        Console.WriteLine("Cantidad de reservas: " & cantidadReservasMesaMasSolicitada)
    End Sub

    Sub reporteDeClientes(reservas As List(Of Reserva))
        Dim clientes As New List(Of Cliente)

        For Each reserva As Reserva In reservas
            Dim clienteEncontrado As Boolean = False
            For Each cliente As Cliente In clientes
                If cliente.rut = reserva.cliente.rut Then
                    clienteEncontrado = True
                    Exit For
                End If
            Next

            If Not clienteEncontrado Then
                clientes.Add(reserva.cliente)
            End If
        Next

        For Each cliente As Cliente In clientes
            Console.WriteLine("Cliente:")
            Console.WriteLine("RUT: " & cliente.rut)
            Console.WriteLine("Nombre: " & cliente.nombre)
            Console.WriteLine("Número de teléfono: " & cliente.telefono)
            Console.WriteLine()
        Next
    End Sub
End Module