using System;
using System.Collections.Concurrent;
using System.Threading;

class Program
{
    static BlockingCollection<(int PedidoId, string Prato)> pedidos = new();
    static Random random = new();
    static object estoqueLock = new();

    static int arrozEstoque = 0;
    static int carneEstoque = 0;
    static int macarraoEstoque = 0;
    static int molhoEstoque = 0;

    static void Main()
    {
        // Criar threads para 5 Garçons
        for (int i = 1; i <= 5; i++)
        {
            int garcomId = i;
            new Thread(() => Garcom(garcomId)).Start();
        }

        // Criar threads para 3 Chefs
        for (int i = 1; i <= 3; i++)
        {
            int chefId = i;
            new Thread(() => Chef(chefId)).Start();
        }

        // Manter o programa em execução
        Console.ReadLine();
    }

    static void Garcom(int garcomId)
    {
        int pedidoId = 1;
        string[] pratos = { "Prato Executivo", "Prato Italiano", "Prato Especial" };

        while (true)
        {
            Thread.Sleep(random.Next(1000, 10000)); // Tempo aleatório entre 1 e 10 segundos
            string prato = pratos[random.Next(pratos.Length)];
            pedidos.Add((pedidoId, prato));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[Garçom {garcomId}] - Envio de Pedido {pedidoId}: {prato}");
            Console.ResetColor();
            pedidoId++;
        }
    }

    static void Chef(int chefId)
    {
        while (true)
        {
            var pedido = pedidos.Take();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Chef {chefId}] Inicio da Preparação do Pedido {pedido.PedidoId}");
            Console.ResetColor();

            PrepararPrato(pedido.Prato, chefId);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Chef {chefId}] Fim da Preparação do Pedido {pedido.PedidoId}");
            Console.ResetColor();
        }
    }

    static void PrepararPrato(string prato, int chefId)
    {
        lock (estoqueLock)
        {
            if (prato == "Prato Executivo")
            {
                if (arrozEstoque < 1) Produzir("Arroz", 3, ref arrozEstoque, chefId);
                if (carneEstoque < 1) Produzir("Carne", 2, ref carneEstoque, chefId);
                arrozEstoque--;
                carneEstoque--;
            }
            else if (prato == "Prato Italiano")
            {
                if (macarraoEstoque < 1) Produzir("Macarrão", 4, ref macarraoEstoque, chefId);
                if (molhoEstoque < 1) Produzir("Molho", 2, ref molhoEstoque, chefId);
                macarraoEstoque--;
                molhoEstoque--;
            }
            else if (prato == "Prato Especial")
            {
                if (arrozEstoque < 1) Produzir("Arroz", 3, ref arrozEstoque, chefId);
                if (carneEstoque < 1) Produzir("Carne", 2, ref carneEstoque, chefId);
                if (molhoEstoque < 1) Produzir("Molho", 2, ref molhoEstoque, chefId);
                arrozEstoque--;
                carneEstoque--;
                molhoEstoque--;
            }
        }

        Thread.Sleep(1000); // 1 segundo por porção usada
    }

    static void Produzir(string componente, int quantidade, ref int estoque, int chefId)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[Chef {chefId}] Iniciando produção de {componente}");
        Console.ResetColor();

        Thread.Sleep(2000); // 2 segundos por componente
        estoque += quantidade;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[Chef {chefId}] Finalizou produção de {componente}. Estoque atualizado: {estoque} unidades");
        Console.ResetColor();
    }
}