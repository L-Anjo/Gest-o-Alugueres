using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DL;

namespace BL
{
    public class RegrasNegocio
    {

        public static string Show()
        {
            return (DL.Alugueres.Show());
        }
        public static string ShowV()
        {
            return (DL.BiciBox.ShowV());
        }
        public static bool AdicionarVeiBic(Veiculo v)
        {
            if (ExisteVeiBic(v.Cod) == true || DL.BiciBox.Totalv > DL.BiciBox.Maxv) return false; //Verificar se existe algum veiculo criado com o mesmo codigo
            return DL.BiciBox.AdicionarVeiBic(v);
        }
        public static bool EntregaVeiculo(int cod)
        {
            int p = QuemUsaEsteVeiculo(cod);
            if (ExistePessoaAlug(p) == false) return false; //Verifica Se a pessoa tem algum aluguer em uso
            return DL.Alugueres.EntregaVeiculo(cod);
        }
        public static bool CancelaAluguer(int id)
        {
            return DL.Alugueres.CancelaAluguer(id);
        }

        public static int QuemUsaEsteVeiculo(int id)
        {
            return DL.Alugueres.QuemUsaEsteVeiculo(id);
        }

        public static bool AdicionarAluBic(Aluguer a)
        {
            if(a.U.Saldo < a.Preco) return false;
            return DL.Alugueres.AdicionarAluBic((a));
        }
        public static bool CriaAluguvBicc(Utilizador u, Veiculo v, int h)
        {
            if ((ExisteVeiBic(v.Cod) == false) || (ExistePessoaAlug(u.Cod) == true) || v.Estado == Estado.Ocupado || h < 1) return false; //Se nao existir na bicibox não cria aluguer
            return DL.Alugueres.CriaAluguvBicc(u, v, h);
        }

        public static bool CalcValor(Aluguer a)
        {
            return DL.Alugueres.CalcValor(a);
        }

        public static bool ExisteVeiBic(int id)
        {
            return DL.BiciBox.ExisteVeiBic((id));
        }
        public static bool ExistePessoaAlug(int id)
        {
            return DL.Alugueres.ExistePessoaAlug((id));
        }

        public static bool SaveAll(string fileName)
        {
            return DL.BiciBox.SaveAll(fileName);
        }
        public static bool LoadAll(string fileName)
        {
            return DL.BiciBox.LoadAll(fileName);
        }
        public static List<Veiculo> Veiculos()
        {
            return DL.BiciBox.Veiculos();
        }

    }
}
