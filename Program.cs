using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaTareas
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Completada { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Tarea(int id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
            Completada = false;
            FechaCreacion = DateTime.Now;
        }

        public override string ToString()
        {
            string estado = Completada ? "✓" : " ";
            return $"[{estado}] {Id}. {Descripcion} ({FechaCreacion:dd/MM/yyyy})";
        }
    }

    public class GestorTareas
    {
        private List<Tarea> tareas;
        private int siguienteId;

        public GestorTareas()
        {
            tareas = new List<Tarea>();
            siguienteId = 1;
        }

        public void AgregarTarea(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                Console.WriteLine("La descripción no puede estar vacía.");
                return;
            }

            var nuevaTarea = new Tarea(siguienteId, descripcion);
            tareas.Add(nuevaTarea);
            siguienteId++;
            Console.WriteLine($"Tarea agregada: {descripcion}");
        }

        public void ListarTareas()
        {
            if (tareas.Count == 0)
            {
                Console.WriteLine("No hay tareas en la lista.");
                return;
            }

            Console.WriteLine("\n=== LISTA DE TAREAS ===");
            foreach (var tarea in tareas.OrderBy(t => t.Id))
            {
                Console.WriteLine(tarea);
            }
            Console.WriteLine();
        }

        public void MarcarCompletada(int id)
        {
            var tarea = tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null)
            {
                Console.WriteLine($"No se encontró la tarea con ID {id}.");
                return;
            }

            tarea.Completada = true;
            Console.WriteLine($"Tarea {id} marcada como completada.");
        }

        public void EliminarTarea(int id)
        {
            var tarea = tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null)
            {
                Console.WriteLine($"No se encontró la tarea con ID {id}.");
                return;
            }

            tareas.Remove(tarea);
            Console.WriteLine($"Tarea {id} eliminada.");
        }

        public void MostrarEstadisticas()
        {
            int total = tareas.Count;
            int completadas = tareas.Count(t => t.Completada);
            int pendientes = total - completadas;

            Console.WriteLine($"\n=== ESTADÍSTICAS ===");
            Console.WriteLine($"Total de tareas: {total}");
            Console.WriteLine($"Completadas: {completadas}");
            Console.WriteLine($"Pendientes: {pendientes}");
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var gestor = new GestorTareas();
            bool salir = false;

            Console.WriteLine("=== GESTOR DE TAREAS SIMPLE ===");
            Console.WriteLine("¡Bienvenido a tu lista de tareas!");

            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingresa la descripción de la tarea: ");
                        string descripcion = Console.ReadLine();
                        gestor.AgregarTarea(descripcion);
                        break;

                    case "2":
                        gestor.ListarTareas();
                        break;

                    case "3":
                        Console.Write("Ingresa el ID de la tarea a completar: ");
                        if (int.TryParse(Console.ReadLine(), out int idCompletar))
                        {
                            gestor.MarcarCompletada(idCompletar);
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;

                    case "4":
                        Console.Write("Ingresa el ID de la tarea a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int idEliminar))
                        {
                            gestor.EliminarTarea(idEliminar);
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;

                    case "5":
                        gestor.MostrarEstadisticas();
                        break;

                    case "6":
                        salir = true;
                        Console.WriteLine("¡Hasta luego!");
                        break;

                    default:
                        Console.WriteLine("Opción inválida. Por favor, elige una opción del 1 al 6.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
            Console.WriteLine("1. Agregar tarea");
            Console.WriteLine("2. Listar tareas");
            Console.WriteLine("3. Marcar tarea como completada");
            Console.WriteLine("4. Eliminar tarea");
            Console.WriteLine("5. Mostrar estadísticas");
            Console.WriteLine("6. Salir");
            Console.Write("\nElige una opción: ");
        }
    }
}