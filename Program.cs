using System;
using System.Collections.Generic;

namespace Clase3arreglos
{
    internal class Program
    {
        static List<Articulo> articulos = new List<Articulo>(); // Lista para almacenar los artículos

        static void Main(string[] args)
        {
            menu();
            Console.Read();
        }

        static void menu()
        {
            int opcion = 0;

            try
            {
                do
                {
                    Console.WriteLine("********** Menú *************");
                    Console.WriteLine("1- Agregar Artículo");
                    Console.WriteLine("2- Modificar Artículo");
                    Console.WriteLine("3- Borrar Artículo");
                    Console.WriteLine("4- Consultar Artículos");
                    Console.WriteLine("5- Buscar por Código o Nombre");
                    Console.WriteLine("6- Facturación");
                    Console.WriteLine("7- Salir");
                    Console.WriteLine("*****************************");
                    Console.WriteLine("Digite una opción:");

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
                            case 6: Facturar(); break;
                            case 7: break;
                            default:
                                Console.WriteLine("Opción incorrecta");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero.");
                    }

                } while (opcion != 7);

            }
            catch (Exception)
            {
                Console.WriteLine("Opción inválida");
            }
        }

        static void AgregarArticulos()
        {
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

            Articulo articulo = new Articulo(codigo, nombre, precio, cantidad, bodega);
            articulos.Add(articulo);

            Console.WriteLine("Artículo agregado exitosamente.");
        }

        static void ModificarArticulo()
        {
            Console.WriteLine("Ingrese el código del artículo a modificar:");
            string input = Console.ReadLine() ?? "";
            if (!int.TryParse(input, out int codigo))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero para el código.");
                return;
            }

            Articulo articulo = articulos.Find(a => a.Codigo == codigo);
            if (articulo != null)
            {
                Console.WriteLine("Ingrese el nuevo nombre del artículo:");
                articulo.Nombre = Console.ReadLine();

                Console.WriteLine("Ingrese el nuevo precio del artículo:");
                input = Console.ReadLine() ?? "";
                if (!float.TryParse(input, out float nuevoPrecio))
                {
                    Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido para el precio.");
                    return;
                }
                articulo.Precio = nuevoPrecio;

                Console.WriteLine("Ingrese la nueva cantidad del artículo:");
                input = Console.ReadLine() ?? "";
                if (!int.TryParse(input, out int nuevaCantidad))
                {
                    Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero para la cantidad.");
                    return;
                }
                articulo.Cantidad = nuevaCantidad;

                Console.WriteLine("Ingrese la nueva bodega del artículo:");
                articulo.Bodega = Console.ReadLine();

                Console.WriteLine("Artículo modificado exitosamente.");
            }
            else
            {
                Console.WriteLine("Artículo no encontrado.");
            }
        }

        static void BorrarArticulo()
        {
            Console.WriteLine("Ingrese el código del artículo a borrar:");
            string input = Console.ReadLine() ?? "";
            if (!int.TryParse(input, out int codigo))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero para el código.");
                return;
            }

            Articulo articulo = articulos.Find(a => a.Codigo == codigo);
            if (articulo != null)
            {
                articulos.Remove(articulo);
                Console.WriteLine("Artículo borrado exitosamente.");
            }
            else
            {
                Console.WriteLine("Artículo no encontrado.");
            }
        }

        static void ConsultarArticulos()
        {
            if (articulos.Count > 0)
            {
                foreach (Articulo articulo in articulos)
                {
                    Console.WriteLine($"Código: {articulo.Codigo}, Nombre: {articulo.Nombre}, Precio: {articulo.Precio}, Cantidad: {articulo.Cantidad}, Bodega: {articulo.Bodega}");
                }
            }
            else
            {
                Console.WriteLine("No hay artículos registrados.");
            }
        }

        static void BuscarArticulo()
        {
            Console.WriteLine("Ingrese el código o nombre del artículo a buscar:");
            string busqueda = Console.ReadLine();

            var articulosEncontrados = articulos.FindAll(a => a.Codigo.ToString() == busqueda || a.Nombre.ToLower().Contains(busqueda.ToLower()));

            if (articulosEncontrados.Count > 0)
            {
                foreach (Articulo articulo in articulosEncontrados)
                {
                    Console.WriteLine($"Código: {articulo.Codigo}, Nombre: {articulo.Nombre}, Precio: {articulo.Precio}, Cantidad: {articulo.Cantidad}, Bodega: {articulo.Bodega}");
                }
            }
            else
            {
                Console.WriteLine("No se encontraron artículos.");
            }
        }

        static void Facturar()
        {
            float totalFactura = 0;
            foreach (Articulo articulo in articulos)
            {
                totalFactura += articulo.Precio * articulo.Cantidad;
            }

            Console.WriteLine($"El total de la factura es: {totalFactura}");
        }
    }

    class Articulo
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public float Precio { get; set; }
        public int Cantidad { get; set; }
        public string Bodega { get; set; }

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
