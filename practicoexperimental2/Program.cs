using System;
using System.Collections.Generic;

namespace ParqueDiversiones
{
    // Clase Persona
    class Persona
    {
        public string Nombre { get; set; }

        public Persona(string nombre)
        {
            Nombre = nombre;
        }
    }

    // Clase Atraccion
    class Atraccion
    {
        private Queue<Persona> cola;
        private const int CAPACIDAD = 30;

        public Atraccion()
        {
            cola = new Queue<Persona>();
        }

        // Registrar llegada
        public void LlegadaPersona(Persona persona)
        {
            if (cola.Count < CAPACIDAD)
            {
                cola.Enqueue(persona);

                Console.WriteLine(
                    persona.Nombre +
                    " ocupa el asiento #" +
                    cola.Count);
            }
            else
            {
                Console.WriteLine("No hay asientos disponibles para " + persona.Nombre);
            }
        }

        // Iniciar la atracción
        public void IniciarAtraccion()
        {
            if (cola.Count == CAPACIDAD)
            {
                Console.WriteLine("\n======================================");
                Console.WriteLine("TODOS LOS ASIENTOS HAN SIDO VENDIDOS");
                Console.WriteLine("La atracción comienza...\n");

                int numero = 1;

                while (cola.Count > 0)
                {
                    Persona persona = cola.Dequeue();

                    Console.WriteLine(
                        numero + ". " +
                        persona.Nombre +
                        " sube a la atracción.");

                    numero++;
                }

                Console.WriteLine("\nTodos los pasajeros disfrutaron de la atracción.");
            }
            else
            {
                Console.WriteLine("\nTodavía quedan asientos disponibles.");
                Console.WriteLine("Asientos ocupados: " + cola.Count + "/" + CAPACIDAD);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Atraccion montañaRusa = new Atraccion();

            // Llegan 30 personas
            for (int i = 1; i <= 30; i++)
            {
                Persona persona = new Persona("Persona " + i);
                montañaRusa.LlegadaPersona(persona);
            }

            // Intento de vender más asientos
            montañaRusa.LlegadaPersona(new Persona("Persona 31"));

            // Inicia la atracción
            montañaRusa.IniciarAtraccion();

            Console.ReadKey();
        }
    }
}