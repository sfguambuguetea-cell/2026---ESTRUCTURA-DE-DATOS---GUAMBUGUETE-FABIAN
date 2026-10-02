using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

class Grafo
{
    private List<string> vertices;
    private int[,] matriz;

    // Constructor
    public Grafo(List<string> vertices)
    {
        this.vertices = vertices;

        // Crear matriz de adyacencia
        matriz = new int[vertices.Count, vertices.Count];
    }

    // ============================================================
    // AGREGAR UNA ARISTA
    // ============================================================

    public void AgregarArista(string origen, string destino)
    {
        int posicionOrigen = vertices.IndexOf(origen);
        int posicionDestino = vertices.IndexOf(destino);

        if (posicionOrigen != -1 && posicionDestino != -1)
        {
            // Grafo no dirigido
            matriz[posicionOrigen, posicionDestino] = 1;
            matriz[posicionDestino, posicionOrigen] = 1;
        }
    }

    // ============================================================
    // MOSTRAR VÉRTICES
    // ============================================================

    public void MostrarVertices()
    {
        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine("          VERTICES DEL GRAFO");
        Console.WriteLine("==========================================");

        for (int i = 0; i < vertices.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {vertices[i]}");
        }
    }

    // ============================================================
    // MOSTRAR ARISTAS
    // ============================================================

    public void MostrarAristas()
    {
        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine("           ARISTAS DEL GRAFO");
        Console.WriteLine("==========================================");

        bool existenAristas = false;

        for (int i = 0; i < vertices.Count; i++)
        {
            for (int j = i + 1; j < vertices.Count; j++)
            {
                if (matriz[i, j] == 1)
                {
                    Console.WriteLine(
                        $"{vertices[i]} <----------> {vertices[j]}"
                    );

                    existenAristas = true;
                }
            }
        }

        if (!existenAristas)
        {
            Console.WriteLine("No existen aristas.");
        }
    }

    // ============================================================
    // MOSTRAR MATRIZ DE ADYACENCIA
    // ============================================================

    public void MostrarMatriz()
    {
        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine("        MATRIZ DE ADYACENCIA");
        Console.WriteLine("==========================================");

        Console.WriteLine();

        // Encabezado
        Console.Write("             ");

        foreach (string vertice in vertices)
        {
            Console.Write($"{vertice,-16}");
        }

        Console.WriteLine();

        // Matriz
        for (int i = 0; i < vertices.Count; i++)
        {
            Console.Write($"{vertices[i],-13}");

            for (int j = 0; j < vertices.Count; j++)
            {
                Console.Write($"{matriz[i, j],-16}");
            }

            Console.WriteLine();
        }
    }

    // ============================================================
    // MOSTRAR GRADO DE LOS VÉRTICES
    // ============================================================

    public void MostrarGrados()
    {
        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine("          GRADO DE LOS VERTICES");
        Console.WriteLine("==========================================");

        for (int i = 0; i < vertices.Count; i++)
        {
            int grado = 0;

            for (int j = 0; j < vertices.Count; j++)
            {
                grado += matriz[i, j];
            }

            Console.WriteLine(
                $"{vertices[i],-20} -> {grado} conexión(es)"
            );
        }
    }

    // ============================================================
    // CONSULTAR CONEXIONES DE UN VÉRTICE
    // ============================================================

    public void ConsultarVertice(string nombre)
    {
        int posicion = vertices.IndexOf(nombre);

        if (posicion == -1)
        {
            Console.WriteLine();
            Console.WriteLine("El componente no existe en el grafo.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine($"     CONEXIONES DE: {nombre}");
        Console.WriteLine("==========================================");

        bool tieneConexiones = false;

        for (int j = 0; j < vertices.Count; j++)
        {
            if (matriz[posicion, j] == 1)
            {
                Console.WriteLine($"- {vertices[j]}");

                tieneConexiones = true;
            }
        }

        if (!tieneConexiones)
        {
            Console.WriteLine("Este componente no tiene conexiones.");
        }
    }

    // ============================================================
    // MOSTRAR MATRIZ PARA COPIAR EN GRAPH ONLINE
    // ============================================================

    public void MostrarMatrizGraphOnline()
    {
        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine("       MATRIZ PARA GRAPH ONLINE");
        Console.WriteLine("==========================================");

        for (int i = 0; i < vertices.Count; i++)
        {
            for (int j = 0; j < vertices.Count; j++)
            {
                Console.Write(matriz[i, j]);

                if (j < vertices.Count - 1)
                {
                    Console.Write(" ");
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine("Orden de los vertices:");

        for (int i = 0; i < vertices.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {vertices[i]}");
        }
    }

    // ============================================================
    // OBTENER LISTA DE VÉRTICES
    // ============================================================

    public List<string> ObtenerVertices()
    {
        return vertices;
    }
}

class Program
{
    // ============================================================
    // CARGAR GRAFO DESDE ARCHIVO
    // ============================================================

    static Grafo CargarGrafo(string nombreArchivo)
    {
        if (!File.Exists(nombreArchivo))
        {
            throw new FileNotFoundException(
                $"No se encontró el archivo: {nombreArchivo}"
            );
        }

        string[] lineas = File.ReadAllLines(nombreArchivo);

        HashSet<string> conjuntoVertices = new HashSet<string>();

        // --------------------------------------------------------
        // PRIMERA PASADA:
        // Obtener todos los vértices
        // --------------------------------------------------------

        foreach (string linea in lineas)
        {
            if (string.IsNullOrWhiteSpace(linea))
                continue;

            string[] partes = linea.Split(';');

            if (partes.Length != 2)
                continue;

            string origen = partes[0].Trim();
            string destino = partes[1].Trim();

            if (!string.IsNullOrWhiteSpace(origen))
                conjuntoVertices.Add(origen);

            if (!string.IsNullOrWhiteSpace(destino))
                conjuntoVertices.Add(destino);
        }

        List<string> vertices = conjuntoVertices.ToList();

        // Crear el grafo
        Grafo grafo = new Grafo(vertices);

        // --------------------------------------------------------
        // SEGUNDA PASADA:
        // Crear las conexiones
        // --------------------------------------------------------

        foreach (string linea in lineas)
        {
            if (string.IsNullOrWhiteSpace(linea))
                continue;

            string[] partes = linea.Split(';');

            if (partes.Length != 2)
                continue;

            string origen = partes[0].Trim();
            string destino = partes[1].Trim();

            grafo.AgregarArista(origen, destino);
        }

        return grafo;
    }

    // ============================================================
    // PROCESAR GRAFO
    // ============================================================

    static void ProcesarGrafo(
        string nombreArchivo,
        string nombreSistema)
    {
        Console.Clear();

        Console.WriteLine("==========================================");
        Console.WriteLine("       SISTEMA DE PROCESAMIENTO");
        Console.WriteLine("==========================================");

        Console.WriteLine();
        Console.WriteLine($"Sistema: {nombreSistema}");
        Console.WriteLine($"Archivo: {nombreArchivo}");

        Stopwatch reloj = Stopwatch.StartNew();

        Grafo grafo;

        try
        {
            grafo = CargarGrafo(nombreArchivo);
        }
        catch (Exception error)
        {
            reloj.Stop();

            Console.WriteLine();
            Console.WriteLine("ERROR:");
            Console.WriteLine(error.Message);

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();

            return;
        }

        reloj.Stop();

        // Mostrar información
        grafo.MostrarVertices();

        grafo.MostrarAristas();

        grafo.MostrarMatriz();

        grafo.MostrarGrados();

        grafo.MostrarMatrizGraphOnline();

        // --------------------------------------------------------
        // TIEMPO DE EJECUCIÓN
        // --------------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine("          TIEMPO DE EJECUCIÓN");
        Console.WriteLine("==========================================");

        Console.WriteLine(
            $"Tiempo de carga y construcción: " +
            $"{reloj.Elapsed.TotalMilliseconds:F4} ms"
        );

        // --------------------------------------------------------
        // CONSULTA
        // --------------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine("       CONSULTAR COMPONENTE");
        Console.WriteLine("==========================================");

        Console.Write("Ingrese el nombre del componente: ");

        string componente = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(componente))
        {
            componente = componente.Trim();

            grafo.ConsultarVertice(componente);
        }

        Console.WriteLine();
        Console.WriteLine("Presione una tecla para regresar al menú...");
        Console.ReadKey();
    }

    // ============================================================
    // MENÚ PRINCIPAL
    // ============================================================

    static void Main()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("==========================================");
            Console.WriteLine("       PROYECTO DE GRAFOS EN C#");
            Console.WriteLine("==========================================");

            Console.WriteLine();
            Console.WriteLine("1. Grafo del sistema hidráulico");
            Console.WriteLine("2. Grafo del sistema eléctrico");
            Console.WriteLine("3. Salir");

            Console.WriteLine();
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":

                    ProcesarGrafo(
                        "sistema_hidraulico.txt",
                        "Sistema hidráulico de maquinaria pesada"
                    );

                    break;

                case "2":

                    ProcesarGrafo(
                        "sistema_electrico.txt",
                        "Sistema eléctrico de maquinaria pesada"
                    );

                    break;

                case "3":

                    Console.WriteLine();
                    Console.WriteLine("Programa finalizado.");

                    return;

                default:

                    Console.WriteLine();
                    Console.WriteLine(
                        "Opción incorrecta."
                    );

                    Console.WriteLine(
                        "Presione una tecla para continuar..."
                    );

                    Console.ReadKey();

                    break;
            }
        }
    }
}
