using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    public interface IInstrucao
    {
        /// <summary>
        /// Valida os dados referentes à instrução
        /// </summary>
        void Valida();

        void InstanciaInstrucao(int codigoBanco);

        IBanco Banco { get; set; }
        int Codigo { get; set; }
        string Descricao { get; set; }
        int QuantidadeDias { get; set; }
    }
}
