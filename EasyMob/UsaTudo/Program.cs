using System;
using System.IO;
using System.Collections.Generic;
using BL;
using BO;

namespace UsaTudo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Cria Utilizadores
            Utilizador u1 = new Utilizador(1, "ana", Funcao.Utilizador);
            Utilizador u2 = new Utilizador(2, "joao", Funcao.Funcionario);
            Utilizador u3 = new Utilizador(2, "tiago", Funcao.Mecanico);
           
            //Cria Veiculos
            Veiculo v1 = new Veiculo(1);
            Veiculo v2 = new Veiculo(2);
            VeiculoE v3 = new VeiculoE(Tipo.Bicicletael, 50, 3);
            VeiculoE v4 = new VeiculoE(Tipo.Bicicletael, 40, 3);
      
            //Introduz Veiculos Na Bicibox
            BL.RegrasNegocio.AdicionarVeiBic(v4);
            BL.RegrasNegocio.AdicionarVeiBic(v3); // Não adiciona tem um id que já existe na bicibox
            BL.RegrasNegocio.AdicionarVeiBic(v2);
            BL.RegrasNegocio.AdicionarVeiBic(v1);
            Console.WriteLine("Lista Veiculos:\n" + BL.RegrasNegocio.ShowV());

            u1.AdicionaSaldo(50);
            u3.AdicionaSaldo(5);

            //BiciBox.AdicionarAluBic(a1);
            //CriaAluguer e Adiciona se der
            BL.RegrasNegocio.CriaAluguvBicc(u1, v1, 8);
            BL.RegrasNegocio.CriaAluguvBicc(u2, v2, 2); //Não criou aluguer pois usuario não tinha saldo
            BL.RegrasNegocio.CriaAluguvBicc(u3, v4, 4);


            v2.AlterarEstadoV(u2, Estado.Avariado);//Altera Estado pois utilizador tem permissões

            //Exibe Lista Veiculos da Bicibox
            Console.WriteLine("Lista Veiculos:\n"+ BL.RegrasNegocio.ShowV());

            

            //Ordena Lista Veiculos por id
            List<Veiculo> ordv= DL.BiciBox.Veiculos();
            ordv.Sort(); // Ordena num arraylist auxiliar

            //Apresenta Lista Veiculos Ordenada
            foreach (Veiculo v in ordv) 
            {
                Console.WriteLine(v.ToString());
            }

            try
            {
                RegrasNegocio.EntregaVeiculo(1); //Utilizador Entrega Veiculo ao fim de utilização
                RegrasNegocio.CancelaAluguer(3); //Cancela Aluguer não paga
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: {0}.", e.Message);
            }


            //Exibe Lista Veiculos da Bicibox
            Console.WriteLine("Lista Veiculos:\n" + BL.RegrasNegocio.ShowV());

            //Guarda os veiculos existentes na bixibox
            BL.RegrasNegocio.SaveAll(@"c:\temp\bici.bin"); 
            BL.RegrasNegocio.LoadAll(@"c:\temp\bici.bin");
        }
    }
}
