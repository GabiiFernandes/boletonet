using BoletoNet.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoletoNet
{
    public enum EnumCodigoMovimento_Safra
    {
        EntradaConfirmada = 02,
        EntradaRejeitada = 03,
        TransferenciaDeCarteiraEntrada = 04,
        TransferenciaDeCarteiraBaixa = 05,
        LiquidacaoNormal = 06,
        LiquidacaoParcial = 07,
        BaixadoAutomaticamente = 09,
        BaixadoConformeInstrucoes = 10,
        TitulosSemSer = 11,
        AbatimentoConcedido = 12,
        AbatimentoCancelado = 13,
        VencimentoAlterado = 14,
        LiquidacaoEmCartorio = 15,
        BaixaPorEntregaFrancoDePagamento = 16,
        ConfirmacaoDeInstrucaoDeProtesto = 19,
        ConfirmacaoDeSustentarProtesto = 20,
        TransferenciaDeCedente = 21,
        TituloEnviadoACartorio = 23,
        BaixaDeTituloProtestado = 40,
        LiquidacaoDeTituloBaixado = 41,
        TituloRetiradoDoCartorio = 42,
        DespesaDeCartorio = 43,
        ValorDoTituloAlterado = 51
    }

    public class CodigoMovimento_Safra : AbstractCodigoMovimento, ICodigoMovimento
    {
        internal CodigoMovimento_Safra()
        {
        }

        public CodigoMovimento_Safra(int codigo)
        {
            try
            {
                Carregar(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        private void Carregar(int codigo)
        {
            try
            {
                this.Banco = new Banco_Safra();

                var movimento = (EnumCodigoMovimento_Safra)codigo;
                Codigo = codigo;
                Descricao = descricoes[movimento];
            }
            catch (Exception ex)
            {
                throw new BoletoNetException("Código de movimento é inválido", ex);
            }
        }

        public override TipoOcorrenciaRetorno ObterCorrespondenteFebraban()
        {
            return ObterCorrespondenteFebraban(correspondentesFebraban, (EnumCodigoMovimento_Safra)Codigo);
        }

        private readonly Dictionary<EnumCodigoMovimento_Safra, string> descricoes = new Dictionary<EnumCodigoMovimento_Safra, string>()
        {
            { EnumCodigoMovimento_Safra.EntradaConfirmada, "Entrada Confirmada" },
            { EnumCodigoMovimento_Safra.EntradaRejeitada, "Entrada Rejeitada" },
            { EnumCodigoMovimento_Safra.TransferenciaDeCarteiraEntrada, "Transferência de carteira(Entrada)" },
            { EnumCodigoMovimento_Safra.TransferenciaDeCarteiraBaixa, "Transferência de carteira(Baixa)" },
            { EnumCodigoMovimento_Safra.LiquidacaoNormal, "Liquidação normal" },
            { EnumCodigoMovimento_Safra.LiquidacaoParcial, "Liquidação parcial" },
            { EnumCodigoMovimento_Safra.BaixadoAutomaticamente, "Baixado Automaticamente" },
            { EnumCodigoMovimento_Safra.BaixadoConformeInstrucoes, "Baixado conforme instruções" },
            { EnumCodigoMovimento_Safra.TitulosSemSer, "Títulos em Ser(Para arquivo mensal)" },
            { EnumCodigoMovimento_Safra.AbatimentoConcedido, "Abatimento concedido" },
            { EnumCodigoMovimento_Safra.AbatimentoCancelado, "Abatimento cancelado" },
            { EnumCodigoMovimento_Safra.VencimentoAlterado, "Vencimento alterado" },
            { EnumCodigoMovimento_Safra.LiquidacaoEmCartorio, "Liquidação em cartório" },
            { EnumCodigoMovimento_Safra.BaixaPorEntregaFrancoDePagamento, "Baixado por entrega franco de pagamento" },
            { EnumCodigoMovimento_Safra.ConfirmacaoDeInstrucaoDeProtesto, "Confirmação de instrução de protesto" },
            { EnumCodigoMovimento_Safra.ConfirmacaoDeSustentarProtesto, "Confirmação de sustentar protesto" },
            { EnumCodigoMovimento_Safra.TransferenciaDeCedente, "Transferência de cedente" },
            { EnumCodigoMovimento_Safra.TituloEnviadoACartorio, "Título enviado a cartório" },
            { EnumCodigoMovimento_Safra.BaixaDeTituloProtestado, "Baixa de título protestado" },
            { EnumCodigoMovimento_Safra.LiquidacaoDeTituloBaixado, "Liquidação de título baixado" },
            { EnumCodigoMovimento_Safra.TituloRetiradoDoCartorio, "Título retirado do cartório" },
            { EnumCodigoMovimento_Safra.DespesaDeCartorio, "Despesa de cartório" },
            { EnumCodigoMovimento_Safra.ValorDoTituloAlterado, "Valor do título alterado" },
        };

        private readonly Dictionary<EnumCodigoMovimento_Safra, TipoOcorrenciaRetorno> correspondentesFebraban = new Dictionary<EnumCodigoMovimento_Safra, TipoOcorrenciaRetorno>()
        {
            { EnumCodigoMovimento_Safra.EntradaConfirmada, TipoOcorrenciaRetorno.EntradaConfirmada },
            { EnumCodigoMovimento_Safra.EntradaRejeitada, TipoOcorrenciaRetorno.EntradaRejeitada },
            { EnumCodigoMovimento_Safra.TransferenciaDeCarteiraEntrada, TipoOcorrenciaRetorno.TransferenciaDeCarteiraEntrada },
            { EnumCodigoMovimento_Safra.TransferenciaDeCarteiraBaixa, TipoOcorrenciaRetorno.TransferenciaDeCarteiraBaixa },
            { EnumCodigoMovimento_Safra.LiquidacaoNormal, TipoOcorrenciaRetorno.Liquidacao },
            //{ EnumCodigoMovimento_Safra.LiquidacaoParcial, TipoOcorrenciaRetorno.ConfirmacaoDoRecebimentoDaInstrucaoDeDesconto }, //verificar
            { EnumCodigoMovimento_Safra.BaixadoAutomaticamente, TipoOcorrenciaRetorno.Baixa },
            //{ EnumCodigoMovimento_Safra.BaixadoConformeInstrucoes, "Baixado conforme instruções" }, //verificar
            { EnumCodigoMovimento_Safra.TitulosSemSer, TipoOcorrenciaRetorno.TitulosEmCarteira },
            { EnumCodigoMovimento_Safra.AbatimentoConcedido, TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeAbatimento },
            { EnumCodigoMovimento_Safra.AbatimentoCancelado, TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeCancelamentoAbatimento },
            { EnumCodigoMovimento_Safra.VencimentoAlterado, TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoAlteracaoDeVencimento },
            //{ EnumCodigoMovimento_Safra.LiquidacaoEmCartorio, "Liquidação em cartório" }, //verificar
            { EnumCodigoMovimento_Safra.BaixaPorEntregaFrancoDePagamento, TipoOcorrenciaRetorno.FrancoDePagamento },
            { EnumCodigoMovimento_Safra.ConfirmacaoDeInstrucaoDeProtesto, TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeProtesto },
            { EnumCodigoMovimento_Safra.ConfirmacaoDeSustentarProtesto, TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeSustacaoCancelamentoDeProtesto },
            //{ EnumCodigoMovimento_Safra.TransferenciaDeCedente, "Transferência de cedente" }, //verificar
            { EnumCodigoMovimento_Safra.TituloEnviadoACartorio, TipoOcorrenciaRetorno.RemessaACartorio },
            { EnumCodigoMovimento_Safra.BaixaDeTituloProtestado, TipoOcorrenciaRetorno.ProtestadoEBaixado },
            //{ EnumCodigoMovimento_Safra.LiquidacaoDeTituloBaixado, "Liquidação de título baixado" },
            //{ EnumCodigoMovimento_Safra.TituloRetiradoDoCartorio, "Título retirado do cartório" },
            //{ EnumCodigoMovimento_Safra.DespesaDeCartorio, "Despesa de cartório" },
            { EnumCodigoMovimento_Safra.ValorDoTituloAlterado, TipoOcorrenciaRetorno.ConfirmacaoDeAlteracaoDoValorNominalDoTitulo },
        };
    }
}
