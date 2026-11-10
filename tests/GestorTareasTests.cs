using System;
using System.IO;
using ListaTareas;
using Xunit;

namespace ListaTareas.Tests;

public class GestorTareasTests
{
    [Fact]
    public void AgregarTarea_DescripcionVacia_NoAgregaTarea()
    {
        var output = CaptureOutput(() =>
        {
            var gestor = new GestorTareas();
            gestor.AgregarTarea("   ");
            gestor.ListarTareas();
        });

        Assert.Contains("La descripción no puede estar vacía.", output);
        Assert.Contains("No hay tareas en la lista.", output);
    }

    [Fact]
    public void AgregarTarea_DescripcionValida_AgregaTareaConIdentificador()
    {
        var output = CaptureOutput(() =>
        {
            var gestor = new GestorTareas();
            gestor.AgregarTarea("Comprar leche");
            gestor.ListarTareas();
        });

        Assert.Contains("Tarea agregada: Comprar leche", output);
        Assert.Contains("1. Comprar leche", output);
    }

    [Fact]
    public void MarcarCompletada_IdExistente_MuestraTareaCompletada()
    {
        var output = CaptureOutput(() =>
        {
            var gestor = new GestorTareas();
            gestor.AgregarTarea("Preparar informe");
            gestor.MarcarCompletada(1);
            gestor.ListarTareas();
        });

        Assert.Contains("Tarea 1 marcada como completada.", output);
        Assert.Contains("[✓] 1. Preparar informe", output);
    }

    [Fact]
    public void EliminarTarea_IdExistente_EliminaLaTarea()
    {
        var output = CaptureOutput(() =>
        {
            var gestor = new GestorTareas();
            gestor.AgregarTarea("Tarea temporal");
            gestor.EliminarTarea(1);
            gestor.ListarTareas();
        });

        Assert.Contains("Tarea 1 eliminada.", output);
        Assert.Contains("No hay tareas en la lista.", output);
        Assert.DoesNotContain("1. Tarea temporal", output);
    }

    [Fact]
    public void MostrarEstadisticas_ConTareas_MuestraTotalesCorrectos()
    {
        var output = CaptureOutput(() =>
        {
            var gestor = new GestorTareas();
            gestor.AgregarTarea("Primera");
            gestor.AgregarTarea("Segunda");
            gestor.MarcarCompletada(1);
            gestor.MostrarEstadisticas();
        });

        Assert.Contains("Total de tareas: 2", output);
        Assert.Contains("Completadas: 1", output);
        Assert.Contains("Pendientes: 1", output);
    }

    private static string CaptureOutput(Action action)
    {
        var originalOutput = Console.Out;
        using var writer = new StringWriter();

        try
        {
            Console.SetOut(writer);
            action();
            return writer.ToString();
        }
        finally
        {
            Console.SetOut(originalOutput);
        }
    }
}
