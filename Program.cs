using System;

namespace Clase3arreglos
{
    internal class Program
    {
        // Arreglo para almacenar hasta 100 artículos
        static Articulo[] articulos = new Articulo[100];
        // Contador para llevar la cuenta de los artículos agregados
        static int contadorArticulos = 0;

        static void Main(string[] args)
        {
            // Iniciar el menú principal
            menu();
            Console.Read();
        }

        /// <summary>
        /// Método para mostrar el menú principal y manejar la interacción con el usuario.
        /// </summary>
        static void menu()
        {
            int opcion = 0;

            try
            {
                do
                {
                    // Mostrar opciones del menú
                    Console.WriteLine("********** Menú *************");
                    Console.WriteLine("1- Agregar Artículo");
                    Console.WriteLine("2- Modificar Artículo");
                    Console.WriteLine("3- Borrar Artículo");
                    Console.WriteLine("4- Consultar Artículos");
                    Console.WriteLine("5- Buscar por Código o Nombre");
                    Console.WriteLine("6- Salir");
                    Console.WriteLine("*****************************");
                    Console.WriteLine("Digite una opción:");

                    // Leer la opción del usuario, asegurando que sea válida
                    string input = Console.ReadLine() ?? "";
                    if (int.TryParse(input, out opcion))
                    {
                        switch (opcion)
                        {
                            case 1: AgregarArticulos(); break;
                            case 2: ModificarArticulo(); break;
                            case 3: BorrarArticulo(); break;
                            case 4: ConsultarArticulos(); break;
                            case 5: BuscarArticulo(); break;
                            case 6: break;
                            default:
                                Console.WriteLine("Opción incorrecta");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero.");
                    }

                } while (opcion != 6); // El menú se repite hasta que el usuario elija la opción 6 (Salir)

            }
            catch (Exception)
            {
                Console.WriteLine("Opción inválida");
            }
        }

        /// <summary>
        /// Método para agregar un nuevo artículo al arreglo.
        /// </summary>
        static void AgregarArticulos()
        {
            // Verificar si el arreglo ya alcanzó su límite de 100 artículos
            if (contadorArticulos >= 100)
            {
                Console.WriteLine("No se pueden agregar más artículos. Capacidad máxima alcanzada.");
                return;
            }

            // Solicitar datos del artículo
            Console.WriteLine("Ingrese el código del artículo:");
            string input = Console.ReadLine() ?? "";
            if (!int.TryParse(input, out int codigo))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero para el código.");
                return;
            }

            Console.WriteLine("Ingrese el nombre del artículo:");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el precio del artículo:");
            input = Console.ReadLine() ?? "";
            if (!float.TryParse(input, out float precio))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido para el precio.");
                return;
            }

            Console.WriteLine("Ingrese la cantidad del artículo:");
            input = Console.ReadLine() ?? "";
            if (!int.TryParse(input, out int cantidad))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero para la cantidad.");
                return;
            }

            Console.WriteLine("Ingrese la bodega donde se almacenará el artículo:");
            string bodega = Console.ReadLine();

            // Agregar el artículo al arreglo y aumentar el contador
            articulos[contadorArticulos] = new Articulo(codigo, nombre, precio, cantidad, bodega);
            contadorArticulos++;

            Console.WriteLine("Artículo agregado exitosamente.");
        }

        /// <summary>
        /// Método para modificar los datos de un artículo ya existente.
        /// </summary>
        static void ModificarArticulo()
        {
            Console.WriteLine("Ingrese el código del artículo a modificar:");
            string input = Console.ReadLine() ?? "";
            if (!int.TryParse(input, out int codigo))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero para el código.");
                return;
            }

            // Buscar el índice del artículo por su código
            int index = BuscarIndicePorCodigo(codigo);
            if (index != -1)
            {
                // Si se encuentra, permitir modificar los atributos
                Console.WriteLine("Ingrese el nuevo nombre del artículo:");
                articulos[index].Nombre = Console.ReadLine();

                Console.WriteLine("Ingrese el nuevo precio del artículo:");
                input = Console.ReadLine() ?? "";
                if (!float.TryParse(input, out float nuevoPrecio))
                {
                    Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido para el precio.");
                    return;
                }
                articulos[index].Precio = nuevoPrecio;

                Console.WriteLine("Ingrese la nueva cantidad del artículo:");
                input = Console.ReadLine() ?? "";
                if (!int.TryParse(input, out int nuevaCantidad))
                {
                    Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero para la cantidad.");
                    return;
                }
                articulos[index].Cantidad = nuevaCantidad;

                Console.WriteLine("Ingrese la nueva bodega del artículo:");
                articulos[index].Bodega = Console.ReadLine();

                Console.WriteLine("Artículo modificado exitosamente.");
            }
            else
            {
                Console.WriteLine("Artículo no encontrado.");
            }
        }

        /// <summary>
        /// Método para borrar un artículo del arreglo.
        /// </summary>
        static void BorrarArticulo()
        {
            Console.WriteLine("Ingrese el código del artículo a borrar:");
            string input = Console.ReadLine() ?? "";
            if (!int.TryParse(input, out int codigo))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero para el código.");
                return;
            }

            // Buscar el índice del artículo por su código
            int index = BuscarIndicePorCodigo(codigo);
            if (index != -1)
            {
                // Desplazar los artículos restantes para llenar el espacio vacío
                for (int i = index; i < contadorArticulos - 1; i++)
                {
                    articulos[i] = articulos[i + 1];
                }
                // Borrar el último artículo
                articulos[contadorArticulos - 1] = null;
                contadorArticulos--;
                Console.WriteLine("Artículo borrado exitosamente.");
            }
            else
            {
                Console.WriteLine("Artículo no encontrado.");
            }
        }

        /// <summary>
        /// Método para consultar todos los artículos almacenados.
        /// </summary>
        static void ConsultarArticulos()
        {
            if (contadorArticulos > 0)
            {
                // Recorrer el arreglo e imprimir la información de cada artículo
                for (int i = 0; i < contadorArticulos; i++)
                {
                    Console.WriteLine($"Código: {articulos[i].Codigo}, Nombre: {articulos[i].Nombre}, Precio: {articulos[i].Precio}, Cantidad: {articulos[i].Cantidad}, Bodega: {articulos[i].Bodega}");
                }
            }
            else
            {
                Console.WriteLine("No hay artículos registrados.");
            }
        }

        /// <summary>
        /// Método para buscar un artículo por su código o nombre.
        /// </summary>
        static void BuscarArticulo()
        {
            Console.WriteLine("Ingrese el código o nombre del artículo a buscar:");
            string busqueda = Console.ReadLine();

            bool encontrado = false;
            for (int i = 0; i < contadorArticulos; i++)
            {
                // Verificar si el código o el nombre coinciden con la búsqueda
                if (articulos[i].Codigo.ToString() == busqueda || articulos[i].Nombre.ToLower().Contains(busqueda.ToLower()))
                {
                    Console.WriteLine($"Código: {articulos[i].Codigo}, Nombre: {articulos[i].Nombre}, Precio: {articulos[i].Precio}, Cantidad: {articulos[i].Cantidad}, Bodega: {articulos[i].Bodega}");
                    encontrado = true;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("No se encontraron artículos.");
            }
        }

        /// <summary>
        /// Método auxiliar para buscar el índice de un artículo en el arreglo por su código.
        /// </summary>
        /// <param name="codigo">Código del artículo a buscar</param>
        /// <returns>Índice del artículo en el arreglo o -1 si no se encuentra</returns>
        static int BuscarIndicePorCodigo(int codigo)
        {
            for (int i = 0; i < contadorArticulos; i++)
            {
                if (articulos[i].Codigo == codigo)
                {
                    return i;
                }
            }
            return -1; // Retorna -1 si no encuentra el artículo
        }
    }

    /// <summary>
    /// Clase que representa un artículo con sus atributos.
    /// </summary>
    class Articulo
    {
        public int Codigo { get; set; } // Código único del artículo
        public string Nombre { get; set; } // Nombre del artículo
        public float Precio { get; set; } // Precio del artículo
        public int Cantidad { get; set; } // Cantidad disponible del artículo
        public string Bodega { get; set; } // Bodega donde se almacena el artículo

        // Constructor para inicializar un artículo
        public Articulo(int codigo, string nombre, float precio, int cantidad, string bodega)
        {
            Codigo = codigo;
            Nombre = nombre;
            Precio = precio;
            Cantidad = cantidad;
            Bodega = bodega;
        }
    }
}
