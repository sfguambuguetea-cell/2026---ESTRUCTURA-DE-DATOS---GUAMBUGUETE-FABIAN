using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    // ==========================================
    // ESTRUCTURAS DE DATOS
    // ==========================================

    // CONJUNTO:
    // Almacena los jugadores y evita nombres repetidos.
    static HashSet<string> jugadores = new HashSet<string>();

    // MAPA:
    // Relaciona cada equipo con su conjunto de jugadores.
    static Dictionary<string, HashSet<string>> equipos =
        new Dictionary<string, HashSet<string>>();

    // DICCIONARIO:
    // Relaciona cada jugador con su equipo.
    static Dictionary<string, string> jugadorEquipo =
        new Dictionary<string, string>();

    static void Main()
    {
        // ==========================================
        // EQUIPO PRINCIPAL
        // ==========================================

        equipos["Independiente del Valle"] =
            new HashSet<string>();

        // Jugadores iniciales
        RegistrarJugador("Moisés Ramírez", "Independiente del Valle");
        RegistrarJugador("Junior Sornoza", "Independiente del Valle");
        RegistrarJugador("Richard Schunke", "Independiente del Valle");

        bool salir = false;

        while (!salir)
        {
            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("       SISTEMA DE TORNEO DE FÚTBOL");
            Console.WriteLine("       INDEPENDIENTE DEL VALLE");
            Console.WriteLine("==============================================");
            Console.WriteLine("1. Registrar equipo");
            Console.WriteLine("2. Registrar jugador");
            Console.WriteLine("3. Mostrar equipos");
            Console.WriteLine("4. Mostrar jugadores de un equipo");
            Console.WriteLine("5. Buscar jugador");
            Console.WriteLine("6. Eliminar jugador");
            Console.WriteLine("7. Reporte general");
            Console.WriteLine("8. Medir tiempo de búsqueda");
            Console.WriteLine("9. Salir");
            Console.WriteLine("==============================================");

            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    RegistrarEquipo();
                    break;

                case "2":
                    RegistrarJugadorMenu();
                    break;

                case "3":
                    MostrarEquipos();
                    break;

                case "4":
                    MostrarJugadores();
                    break;

                case "5":
                    BuscarJugador();
                    break;

                case "6":
                    EliminarJugador();
                    break;

                case "7":
                    ReporteGeneral();
                    break;

                case "8":
                    MedirTiempo();
                    break;

                case "9":
                    salir = true;
                    Console.WriteLine("\nPrograma finalizado.");
                    break;

                default:
                    Console.WriteLine("\nOpción incorrecta.");
                    Pausar();
                    break;
            }
        }
    }

    // ==========================================
    // REGISTRAR EQUIPO
    // ==========================================

    static void RegistrarEquipo()
    {
        Console.Clear();

        Console.WriteLine("========== REGISTRAR EQUIPO ==========\n");

        Console.Write("Ingrese el nombre del equipo: ");
        string equipo = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(equipo))
        {
            Console.WriteLine("El nombre no puede estar vacío.");
        }
        else if (equipos.ContainsKey(equipo))
        {
            Console.WriteLine("El equipo ya está registrado.");
        }
        else
        {
            equipos.Add(equipo, new HashSet<string>());

            Console.WriteLine(
                "Equipo registrado correctamente.");
        }

        Pausar();
    }

    // ==========================================
    // REGISTRAR JUGADOR DESDE EL MENÚ
    // ==========================================

    static void RegistrarJugadorMenu()
    {
        Console.Clear();

        Console.WriteLine("========== REGISTRAR JUGADOR ==========\n");

        Console.Write("Ingrese el equipo: ");
        string equipo = Console.ReadLine();

        if (!equipos.ContainsKey(equipo))
        {
            Console.WriteLine("El equipo no existe.");
            Pausar();
            return;
        }

        Console.Write("Ingrese el nombre del jugador: ");
        string jugador = Console.ReadLine();

        RegistrarJugador(jugador, equipo);

        Pausar();
    }

    // ==========================================
    // REGISTRAR JUGADOR
    // ==========================================

    static void RegistrarJugador(string jugador, string equipo)
    {
        if (string.IsNullOrWhiteSpace(jugador))
        {
            Console.WriteLine("El nombre del jugador es obligatorio.");
            return;
        }

        // El HashSet evita jugadores duplicados
        if (jugadores.Contains(jugador))
        {
            Console.WriteLine(
                "El jugador ya está registrado.");
            return;
        }

        // Agregar jugador al conjunto general
        jugadores.Add(jugador);

        // Agregar jugador al conjunto del equipo
        equipos[equipo].Add(jugador);

        // Agregar relación jugador -> equipo
        jugadorEquipo.Add(jugador, equipo);

        Console.WriteLine(
            "Jugador registrado correctamente.");
    }

    // ==========================================
    // MOSTRAR EQUIPOS
    // ==========================================

    static void MostrarEquipos()
    {
        Console.Clear();

        Console.WriteLine("========== EQUIPOS REGISTRADOS ==========\n");

        if (equipos.Count == 0)
        {
            Console.WriteLine("No existen equipos.");
        }
        else
        {
            foreach (var equipo in equipos)
            {
                Console.WriteLine(
                    "Equipo: " + equipo.Key);

                Console.WriteLine(
                    "Jugadores: " + equipo.Value.Count);

                Console.WriteLine("------------------------------------------");
            }
        }

        Pausar();
    }

    // ==========================================
    // MOSTRAR JUGADORES DE UN EQUIPO
    // ==========================================

    static void MostrarJugadores()
    {
        Console.Clear();

        Console.WriteLine(
            "========== JUGADORES DEL EQUIPO ==========\n");

        Console.Write("Ingrese el equipo: ");
        string equipo = Console.ReadLine();

        if (!equipos.ContainsKey(equipo))
        {
            Console.WriteLine("El equipo no existe.");
            Pausar();
            return;
        }

        Console.WriteLine("\nEquipo: " + equipo);
        Console.WriteLine("------------------------------------------");

        if (equipos[equipo].Count == 0)
        {
            Console.WriteLine("No hay jugadores registrados.");
        }
        else
        {
            int numero = 1;

            foreach (string jugador in equipos[equipo])
            {
                Console.WriteLine(
                    numero + ". " + jugador);

                numero++;
            }
        }

        Pausar();
    }

    // ==========================================
    // BUSCAR JUGADOR
    // ==========================================

    static void BuscarJugador()
    {
        Console.Clear();

        Console.WriteLine("========== BUSCAR JUGADOR ==========\n");

        Console.Write("Ingrese el nombre del jugador: ");
        string jugador = Console.ReadLine();

        if (jugadorEquipo.ContainsKey(jugador))
        {
            string equipo = jugadorEquipo[jugador];

            Console.WriteLine("\nJugador encontrado.");
            Console.WriteLine("Jugador: " + jugador);
            Console.WriteLine("Equipo: " + equipo);
        }
        else
        {
            Console.WriteLine(
                "\nEl jugador no está registrado.");
        }

        Pausar();
    }

    // ==========================================
    // ELIMINAR JUGADOR
    // ==========================================

    static void EliminarJugador()
    {
        Console.Clear();

        Console.WriteLine("========== ELIMINAR JUGADOR ==========\n");

        Console.Write("Ingrese el nombre del jugador: ");
        string jugador = Console.ReadLine();

        if (jugadorEquipo.ContainsKey(jugador))
        {
            string equipo = jugadorEquipo[jugador];

            // Eliminar del conjunto del equipo
            equipos[equipo].Remove(jugador);

            // Eliminar del conjunto general
            jugadores.Remove(jugador);

            // Eliminar del diccionario
            jugadorEquipo.Remove(jugador);

            Console.WriteLine(
                "Jugador eliminado correctamente.");
        }
        else
        {
            Console.WriteLine(
                "El jugador no existe.");
        }

        Pausar();
    }

    // ==========================================
    // REPORTE GENERAL
    // ==========================================

    static void ReporteGeneral()
    {
        Console.Clear();

        Console.WriteLine("==============================================");
        Console.WriteLine("          REPORTE GENERAL DEL TORNEO");
        Console.WriteLine("==============================================");

        Console.WriteLine(
            "\nTotal de equipos: " + equipos.Count);

        Console.WriteLine(
            "Total de jugadores: " + jugadores.Count);

        Console.WriteLine("\n==============================================");
        Console.WriteLine("       EQUIPOS Y JUGADORES");
        Console.WriteLine("==============================================");

        foreach (var equipo in equipos)
        {
            Console.WriteLine(
                "\nEquipo: " + equipo.Key);

            Console.WriteLine(
                "Cantidad de jugadores: " +
                equipo.Value.Count);

            foreach (string jugador in equipo.Value)
            {
                Console.WriteLine("   - " + jugador);
            }
        }

        Console.WriteLine("\n==============================================");
        Console.WriteLine("       DICCIONARIO JUGADOR -> EQUIPO");
        Console.WriteLine("==============================================");

        foreach (var registro in jugadorEquipo)
        {
            Console.WriteLine(
                registro.Key + " -> " + registro.Value);
        }

        Console.WriteLine("\n==============================================");
        Console.WriteLine("       CONJUNTO DE JUGADORES");
        Console.WriteLine("==============================================");

        foreach (string jugador in jugadores)
        {
            Console.WriteLine("- " + jugador);
        }

        Pausar();
    }

    // ==========================================
    // MEDICIÓN DEL TIEMPO DE EJECUCIÓN
    // ==========================================

    static void MedirTiempo()
    {
        Console.Clear();

        Console.WriteLine("========== TIEMPO DE EJECUCIÓN ==========\n");

        Console.Write("Ingrese un jugador para buscar: ");
        string jugador = Console.ReadLine();

        Stopwatch reloj = new Stopwatch();

        reloj.Start();

        bool encontrado = jugadorEquipo.ContainsKey(jugador);

        reloj.Stop();

        Console.WriteLine();

        if (encontrado)
        {
            Console.WriteLine(
                "Jugador encontrado.");
        }
        else
        {
            Console.WriteLine(
                "Jugador no encontrado.");
        }

        Console.WriteLine(
            "Tiempo de búsqueda: " +
            reloj.ElapsedTicks +
            " ticks.");

        Console.WriteLine(
            "Tiempo aproximado: " +
            reloj.Elapsed.TotalMilliseconds +
            " ms.");

        Pausar();
    }

    // ==========================================
    // PAUSAR EL PROGRAMA
    // ==========================================

    static void Pausar()
    {
        Console.WriteLine(
            "\nPresione ENTER para continuar...");

        Console.ReadLine();
    }
}