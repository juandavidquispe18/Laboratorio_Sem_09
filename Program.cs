using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_Sem_09
{
    internal class Program
    {
        static Dictionary<string, int> ventas = new Dictionary<string, int>();
        static Dictionary<string, int> devoluciones = new Dictionary<string, int>();
        static Dictionary<string, int> inventario = new Dictionary<string, int>();
        static Dictionary<string, decimal> caja = new Dictionary<string, decimal>();
        static void Main()
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("Tienda de Don Lucas");
            Console.WriteLine("================================");
            int opcion;
            bool mostrar_opciones = true;

            do {

                if (mostrar_opciones) {

                    Console.WriteLine("1: Registrar venta");
                    Console.WriteLine("2: Registrar devolución");
                    Console.WriteLine("3: Cerrar Caja");
                    Console.WriteLine("4: Salir");
                    Console.WriteLine("================================");
                    Console.Write("Ingrese una opción: ");
                }
                opcion = int.Parse(Console.ReadLine());

                switch (opcion) {

                    case 1:
                        Registrar_Venta();
                        break;
                    case 2:
                        Registrar_Devolucion();
                        break;
                    case 3:
                        Cerrar_Caja();
                        mostrar_opciones = false;
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Error: Opción no válida");
                        break;
                }
            } while (opcion != 4);
        }

        static void Registrar_Venta()
        {
            Console.Clear();
            Mostrar_Categorias();

            int categoria;
            do {

                Console.Write("Ingrese una opción: ");
                categoria = int.Parse(Console.ReadLine());
            } while (categoria < 1 || categoria > 4);

            string categoria_nombre = Obtener_Categoria_Nombre(categoria);

            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine($"Registrar venta de {categoria_nombre}");
            Console.WriteLine("=================================");
            Console.Write("Ingrese cantidad (unidades): ");
            int cantidad = int.Parse(Console.ReadLine());
            Console.Write("Ingrese precio: S/ ");
            decimal precio = decimal.Parse(Console.ReadLine());

            if (!ventas.ContainsKey(categoria_nombre))
                ventas[categoria_nombre] = 0;
            ventas[categoria_nombre] += cantidad;

            if (!inventario.ContainsKey(categoria_nombre))
                inventario[categoria_nombre] = 0;
            inventario[categoria_nombre] += cantidad;

            if (!caja.ContainsKey(categoria_nombre))
                caja[categoria_nombre] = 0;
            caja[categoria_nombre] += cantidad * precio;

            Console.WriteLine("=================================");
            Console.WriteLine($"Se han ingresado {cantidad} unidades");
            Console.WriteLine($"Se han ingresado S/ {cantidad * precio:F2} en caja");
            Console.WriteLine("=================================");
            Mostrar_Opciones_Categoria(categoria);
        }

        static void Registrar_Devolucion()
        {
            Console.Clear();
            Mostrar_Categorias();

            int categoria;
            do {

                Console.WriteLine("=================================");
                Console.Write("Ingrese una opción: ");
                categoria = int.Parse(Console.ReadLine());
            } while (categoria < 1 || categoria > 4);

            string categoria_nombre = Obtener_Categoria_Nombre(categoria);

            Console.WriteLine("=================================");
            Console.WriteLine($"Registrar devolución de {categoria_nombre}");
            Console.WriteLine("=================================");
            Console.Write("Ingrese cantidad (unidades): ");
            int cantidad = int.Parse(Console.ReadLine());
            Console.Write("Ingrese precio: S/ ");
            decimal precio = decimal.Parse(Console.ReadLine());

            if (!devoluciones.ContainsKey(categoria_nombre))
                devoluciones[categoria_nombre] = 0;
            devoluciones[categoria_nombre] += cantidad;

            if (!inventario.ContainsKey(categoria_nombre))
                inventario[categoria_nombre] = 0;
            inventario[categoria_nombre] -= cantidad;

            if (!caja.ContainsKey(categoria_nombre))
                caja[categoria_nombre] = 0;
            caja[categoria_nombre] -= cantidad * precio;

            Console.WriteLine("=================================");
            Console.WriteLine($"Se han regresado {cantidad} unidades");
            Console.WriteLine($"Se han devuelto S/ {cantidad * precio:F2} de caja");
            Console.WriteLine("=================================");
            Mostrar_Opciones_Categoria(categoria);
        }

        static void Cerrar_Caja()
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("Tienda de Don Lucas");
            Console.WriteLine("=================================");
            Console.WriteLine("1: Registrar venta");  
            Console.WriteLine("2: Registrar devolución");
            Console.WriteLine("3: Cerrar Caja");
            Console.WriteLine("=================================");
            Console.WriteLine("Cerrando Caja");
            Console.WriteLine("=================================");
            Console.WriteLine("Totales");

            decimal total_caja_general = 0;

            foreach (var categoria in ventas.Keys) {

                int vendidos = ventas[categoria];
                int devueltos = devoluciones.ContainsKey(categoria) ? devoluciones[categoria] : 0;
                int total_inventario = inventario.ContainsKey(categoria) ? inventario[categoria] : 0;
                decimal total_caja_categoria = caja.ContainsKey(categoria) ? caja[categoria] : 0;

                Console.WriteLine("=================================");
                Console.WriteLine($"             | {vendidos} vendidos");
                Console.WriteLine($"{categoria.PadRight(12)} | {devueltos} devueltos");
                Console.WriteLine($"             | {total_inventario} en total");
                Console.WriteLine($"             | S/ {total_caja_categoria:F2} en caja");
                total_caja_general += total_caja_categoria;
            }

            Console.WriteLine("=================================");
            Console.WriteLine($"Queda en caja S/{total_caja_general:F2}");
        }

        static void Mostrar_Categorias()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("Registrar Venta de:");
            Console.WriteLine("=================================");
            Console.WriteLine("1: Limpieza");
            Console.WriteLine("2: Abarrotes");
            Console.WriteLine("3: Golosinas");
            Console.WriteLine("4: Electrónicos");
            Console.WriteLine("5: <- Regresar");
        }

        static void Mostrar_Opciones_Categoria(int categoria)
        {
            Console.WriteLine("1: Registrar mas productos");
            Console.WriteLine("2: <- Regresar");
            Console.WriteLine("=================================");
            Console.Write("Ingrese una opción: ");
            int opcion = int.Parse(Console.ReadLine());

            if (opcion == 1)
            {

                if (categoria >= 1 && categoria <= 4)
                {
                    Registrar_Venta();
                }
                else if (categoria == 5)
                {
                    Main();
                }
            }
            else if (opcion == 2)
            {
                Main();
            }
        }

        static string Obtener_Categoria_Nombre(int categoria)
        {
            switch (categoria)
            {

                case 1:
                    return "Limpieza";
                case 2:
                    return "Abarrotes";
                case 3:
                    return "Golosinas";
                case 4:
                    return "Electrónicos";
                default:
                    return "";
            }
        }
    }
}
