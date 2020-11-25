using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoletoNet
{
    public enum EnumCodigoMovimento_Unicred
    {
        InstrucaoConfirmada = 02,
        InstrucaoRejeitada = 03,
    }
    public class CodigoMovimento_Unicred : AbstractCodigoMovimento, ICodigoMovimento
    {
        internal CodigoMovimento_Unicred()
        {
        }
    
        public CodigoMovimento_Unicred(int codigo)
        {
            try
            {
                carregar(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        private void carregar(int codigo)
        {
            try
            {
                this.Banco = new Banco_Unicred();

                var movimento = (EnumCodigoMovimento_Unicred)codigo;
                Codigo = codigo;
                //Descricao = descricoes[movimento];
            }
            catch (Exception ex)
            {
            //  throw new BoletoNetException("Código de movimento é inválido", ex);
            }
        }
        
        private readonly Dictionary<EnumCodigoMovimento_Safra, string> descricoes = new Dictionary<EnumCodigoMovimento_Safra, string>()
        {
            //{ EnumCodigoMovimento_Unicred.EntradaConfirmada, "Entrada Confirmada" },
        };

        private readonly Dictionary<EnumCodigoMovimento_Unicred, TipoOcorrenciaRetorno> correspondentesFebraban = new Dictionary<EnumCodigoMovimento_Unicred, TipoOcorrenciaRetorno>()
        {
            //{ EnumCodigoMovimento_Unicred.EntradaConfirmada, TipoOcorrenciaRetorno.EntradaConfirmada },
        };

        public override TipoOcorrenciaRetorno ObterCorrespondenteFebraban()
        {
            return ObterCorrespondenteFebraban(correspondentesFebraban, (EnumCodigoMovimento_Unicred)Codigo);
        }
    }
}
