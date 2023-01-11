using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BO;

namespace DL
{
    public class Alugueres
    {
        static List<Aluguer> aluguers;

        static Alugueres()
        {
            aluguers = new List<Aluguer>();
        }

        #region Metodos
        /// <summary>
        /// Show- Serve para Mostrar todos os aluguers referentas á bicibox
        /// </summary>
        /// <returns></returns>
        public static string Show()
        {
            string s = "";
            int i = 0;
            foreach (Aluguer a in aluguers)
            {
                s += String.Format("Aluguer Nº:{0} | Nome= {1} | CodV= {2} | Horas= {3} | Data= {4} | Custo= {5}$\n", i++, a.U.Nome, a.V.Cod, a.Horas, a.D.ToString(), a.Preco);
            }
            return s;
        }

        /// <summary>
        /// Adiciona Aluguer BiciBox
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool AdicionarAluBic(Aluguer a) //Adicionar Aluguer a BiciBox
        {
            if (aluguers.Contains(a) || a.U.Saldo < a.Preco) return false; //Conter um aluguer igual
            aluguers.Add(a);
            a.V.Estado = Estado.Ocupado;
            a.U.Saldo = a.U.Saldo - a.Preco;
            return true;
        } 

        /// <summary>
        /// Cria Aluguer e adiciona-o á Bicibox
        /// Verifica se veiculo existe Bicibox se veiculo esta disponivel e a pessoa não tem um aluguer ativo
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool CriaAluguvBicc(Utilizador u, Veiculo v, int h) //Faz tudo 
        {
            Aluguer a = new Aluguer(u, v, h);
            CalcValor(a);
            AdicionarAluBic(a);
            return true;
        }

        /// <summary>
        /// Calcula valor do aluguer conforme veiculo e horas
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool CalcValor(Aluguer a)
        {
            a.Preco = a.V.Custo() * a.Horas;
            return true;
        }

        /// <summary>
        /// Existe Alguma Pessoa com um aluguer em uso
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool ExistePessoaAlug(int id)
        {
            foreach (Aluguer u in aluguers)
            {
                if (u.U.Cod == id && u.V.Estado == Estado.Ocupado && u.Ativo)
                    return true; //Utilizador com o mesmo id e a usar um veiculo ocupado e aluguer ativo
            }
            return false;
        }

        /// <summary>
        /// Altera Estado Aluguer
        /// </summary>
        /// <param name="id"> Id do Veículo</param>
        /// <returns></returns>
        public static bool EntregaVeiculo(int id)
        {
            foreach (Aluguer u in aluguers) //Percorre Lista de Aluguers 
            {
                if (u.V.Cod == id && u.Ativo) //e quando encontra o veiculo devolve cod do utilizador
                {
                    u.V.Estado = Estado.Disponivel;
                    u.Ativo = false;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Quem usa Este Veiculo
        /// </summary>
        /// <param name="id"> Id do Veículo</param>
        /// <returns></returns>
        public static int QuemUsaEsteVeiculo(int id)
        {
            foreach (Aluguer u in aluguers) //Percorre Lista de Aluguers 
            {
                if (u.V.Cod == id) //e quando encontra o veiculo devolve cod do utilizador
                return (u.U.Cod);
            }
            return -1;
            }

        /// <summary>
        /// Cancela Aluguer
        /// </summary>
        /// <param name="id"> Id do Veículo</param>
        /// <returns></returns>
        public static bool CancelaAluguer(int id)
        {
            foreach (Aluguer u in aluguers) //Percorre Lista de Aluguers 
            {
                if (u.V.Cod == id && DateTime.Now < u.D.AddMinutes(5))  //So pode cancelar se ainda não passou 5 minutos
                { 
                EntregaVeiculo(id);
                u.U.DevolveSaldo(u.Preco); //
                return true;
            }
            }
            return false;
        }

        #endregion
    }
}
