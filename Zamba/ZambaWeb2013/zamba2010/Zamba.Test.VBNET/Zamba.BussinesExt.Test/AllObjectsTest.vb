Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class AllObjectsTest

    <TestMethod()> Public Sub SiElParametroEsNuloSeDebeDovolverMensajeDeParaMetroNulo()
        Dim msjParOImpar As String
        Try
            msjParOImpar = Core.CommonFunctions.EsNumeroParImpar(Nothing)
        Catch ex As Exception
            Assert.AreEqual("El valor ingresado es nulo", ex.Message)
        End Try
    End Sub


    <TestMethod()> Public Sub SiElParametroEsVacioSeDebeDovolverMensajeDeParaMetroVacio()
        Dim msjParOImpar As String
        Try
            msjParOImpar = Core.CommonFunctions.EsNumeroParImpar(String.Empty)
        Catch ex As Exception
            Assert.AreEqual("El valor ingresado es vacio", ex.Message)
        End Try
    End Sub


    <TestMethod()> Public Sub SiElParametroNoContieneSoloNumerosSeDebeDovolverMensajeDeParametroDeContenidoNoValido()
        Dim msjParOImpar As String
        Try
            msjParOImpar = Core.CommonFunctions.EsNumeroParImpar(",,")
        Catch ex As Exception
            Assert.AreEqual("El contenido del string no es valido para hacer una conversion", ex.Message)
        End Try
    End Sub

    <TestMethod()> Public Sub SiElParametroEsStringConUnNumeroImparDevuelvoMensajeDeNumeroImPar()
        Dim msjParOImpar As String

        msjParOImpar = Core.CommonFunctions.EsNumeroParImpar("3")
        Assert.AreEqual("Impar", msjParOImpar)

    End Sub

    <TestMethod()> Public Sub SiElParametroEsStringConUnNumeroParDevuelvoMensajeDeNumeroPar()
        Dim msjParOImpar As String

        msjParOImpar = Core.CommonFunctions.EsNumeroParImpar("6")
        Assert.AreEqual("Par", msjParOImpar)

    End Sub



End Class